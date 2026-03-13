// 保留 using System.Drawing; 給 WinForms/Bitmap 用，但之後幾何一律用 CvPoint
using Microsoft.Win32;      // 讀本機 AMS Net ID
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;
using static TwinCATDemo.Form1;
using CvPoint = OpenCvSharp.Point;
using CvPoint2f = OpenCvSharp.Point2f;
// WinForms 幾何型別的別名（避免和 OpenCvSharp 衝突）
using WinPoint = System.Drawing.Point;
using WinSize = System.Drawing.Size;
using System.Linq;




namespace TwinCATDemo
{
    
    public partial class Form1 : Form
    {
        // 視訊
        private VideoCapture _cap;
        private Task _loop;
        private CancellationTokenSource _cts;

        // 穩定化參數
        private const int STABLE_HOLD_MS = 200;   // 需連續維持時間
        private const int STABLE_NEED_DIFF = 10;  // 需達到之 margin
        private string _lastWinner = "";
        private Stopwatch _stableWatch = new();

        // 可調參數（與你程式一致）
        private readonly Scalar _lowerYCrCb = new Scalar(0, 133, 77);   // YCrCb: Y,Cr,Cb
        private readonly Scalar _upperYCrCb = new Scalar(255, 173, 127);
        private const double MIN_AREA_RATIO = 0.07; // ROI 面積 7%

        // ROI 設定（右側 1/3、60% 高、邊距 24px）
        private const double ROI_W_PCT = 1.0 / 3.0;
        private const double ROI_H_PCT = 0.60;
        private const int ROI_MARGIN = 24;

        // ADS
        private AdsPublisher _ads;

        // AI 分頁專用
        private BindingList<AiRow> _aiRows = new BindingList<AiRow>();
        private System.Windows.Forms.Timer _aiTimer;
        private bool _aiInited = false;           // 避免重覆初始化
        private const string PlcPrefix = "GVL.";  // ★ 如果你的變數不是放在 GVL，改成 "MAIN." 或實際路徑

        // == DIDO ==
        private const string PlcDidoPrefix = "GVL."; // 若在 MAIN 就改成 "MAIN."
        private bool _didoInited = false;

        private PictureBox[] _diIcon = new PictureBox[8];
        private Label[] _diName = new Label[8];

        private PictureBox[] _doLink = new PictureBox[8];
        private PictureBox[] _doSwitch = new PictureBox[8];
        private Label[] _doState = new Label[8];

        private uint[] _hDI = new uint[8];
        private uint[] _hDO = new uint[8];

        private bool[] _doOpen = new bool[8]; // true=open(1, 斷開), false=close(0, 接起來)
        private System.Windows.Forms.Timer _didoTimer;


        // ---- Dark theme palette ----
        private static readonly Color C_BG = Color.FromArgb(18, 20, 26);   // 視窗底色
        private static readonly Color C_BG2 = Color.FromArgb(24, 27, 35);   // 表格底色
        private static readonly Color C_BG3 = Color.FromArgb(30, 34, 45);   // 表頭色
        private static readonly Color C_ROW_ALT = Color.FromArgb(28, 32, 42);   // 間隔列
        private static readonly Color C_TEXT = Color.Gainsboro;
        private static readonly Color C_MUTED = Color.FromArgb(170, 178, 189);
        private static readonly Color C_LINE = Color.FromArgb(55, 60, 74);   // 表格線
        private static readonly Color C_SEL = Color.FromArgb(70, 85, 110);  // 選取列
        private static readonly Color C_ACCENT = Color.FromArgb(20, 128, 108); // 上方標題或點綴色

        public Form1()
        {
            InitializeComponent();     // 只做這件事
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 設計器開啟時直接略過任何初始化
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            // 掃相機索引
            for (int i = 0; i < 5; i++) cboCamera.Items.Add(i);
            if (cboCamera.Items.Count > 0) cboCamera.SelectedIndex = 0;

            // 連 ADS（失敗也不要炸掉）
            _ads = new AdsPublisher(msg => Debug.WriteLine("[ADS] " + msg));
            _ads.Connect("auto", 851);   // 用上面不拋例外的版本
            // 初始化 AI 分頁
            AI_Tab_InitOnce();   // 初始化 tab_AI（僅第一次呼叫會生效）
            DIDO_InitOnce();   // ← 新增這行（放在 ApplyDarkTheme(); UpdateHeaderByAds(); 之後都可）

            ApplyDarkTheme();     // 主題套用
            UpdateHeaderByAds();  // 依連線結果，若有 lblAdsState 就顯示 Connected/Disconnected
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCapture();

            // 釋放 AI tab 的變數 Handle（用同一條 _ads）
            try
            {
                if (_ads != null && _ads.IsOk)
                    foreach (var r in _aiRows)
                        _ads.DeleteHandle(r.Handle);
            }
            catch { }

            try { _didoTimer?.Stop(); _didoTimer?.Dispose(); } catch { }
            try
            {
                if (_ads != null && _ads.IsOk)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (_hDI[i] != 0) _ads.DeleteHandle(_hDI[i]);
                        if (_hDO[i] != 0) _ads.DeleteHandle(_hDO[i]);
                    }
                }
            }
            catch { }

            // 最後再釋放 _ads（你已有）
            try { _ads?.Dispose(); } catch { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StopCapture();
            _cts = new CancellationTokenSource();
            int camIdx = cboCamera.SelectedIndex >= 0 ? (int)cboCamera.SelectedItem : 0;

            _cap = new VideoCapture(camIdx, VideoCaptureAPIs.ANY);
            _cap.Open(camIdx);
            _cap.FrameWidth = 1024;
            _cap.FrameHeight = 768;

            _stableWatch.Reset();
            _lastWinner = "";

            _loop = Task.Run(() => CaptureLoop(_cts.Token));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCapture();
        }

        private void StopCapture()
        {
            try { _cts?.Cancel(); _loop?.Wait(300); } catch { }
            try { _cap?.Release(); _cap?.Dispose(); } catch { }
            _cap = null;
        }

        private void CaptureLoop(CancellationToken ct)
        {
            using var frame = new Mat();
            while (!ct.IsCancellationRequested)
            {
                if (_cap == null || !_cap.IsOpened()) { Thread.Sleep(30); continue; }
                _cap.Read(frame);
                if (frame.Empty()) { Thread.Sleep(5); continue; }

                // 計算固定 ROI（畫面右側 1/3、60% 高、邊距 24）
                var roiRect = CalcFixedRoi(frame.Width, frame.Height);
                var display = frame.Clone(); // 用於疊圖顯示

                // 只在 ROI 內做手部偵測
                var roi = new Mat(display, roiRect);
                var result = ProcessRoiAndClassify(roi);

                // 疊圖：ROI 框
                Cv2.Rectangle(display, roiRect, new Scalar(0, 255, 255), 2);

                // 疊圖：在 ROI 區繪出 bbox、凸包等（已在 ProcessRoiAndClassify 畫在 roi 上）
                // 將 roi 畫回 display（此處 roi 是 display 的子矩形，不需貼回）

                // 顯示
                var bmp = BitmapConverter.ToBitmap(display);
                SafeUI(() => picView.Image = bmp);

                // 更新 UI 文案與穩定化
                ApplyStabilizationAndOutput(result);

                // 控制 FPS
                Thread.Sleep(10);
            }
        }

        private Rect CalcFixedRoi(int w, int h)
        {
            int roiW = (int)(w * ROI_W_PCT);
            int roiH = (int)(h * ROI_H_PCT);
            int x1 = w - ROI_MARGIN;
            int x0 = x1 - roiW;
            int y0 = (h - roiH) / 2;
            return new Rect(
                Math.Max(0, x0),
                Math.Max(0, y0),
                Math.Min(roiW, w - Math.Max(0, x0) - (w - x1)),
                roiH
            );
        }

        private (string winner, int margin, int paper, int rock, int scissors) ProcessRoiAndClassify(Mat roiBgr)
        {
            // 尺寸與色域
            using var roiResized = new Mat();
            Cv2.Resize(roiBgr, roiResized, new OpenCvSharp.Size(256, 256));

            using var ycrcb = new Mat();
            Cv2.CvtColor(roiResized, ycrcb, ColorConversionCodes.BGR2YCrCb);

            // 遮罩
            using var mask = new Mat();
            Cv2.InRange(ycrcb, _lowerYCrCb, _upperYCrCb, mask);
            Cv2.GaussianBlur(mask, mask, new OpenCvSharp.Size(5, 5), 0);
            var kernel5 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(5, 5));
            var kernel7 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(7, 7));
            Cv2.MorphologyEx(mask, mask, MorphTypes.Open, kernel5);
            Cv2.MorphologyEx(mask, mask, MorphTypes.Close, kernel7);

            // 膚色比例（亮度自適應）
            var channels = Cv2.Split(ycrcb);
            double meanY = Cv2.Mean(channels[0]).Val0;
            double skinRatio = Cv2.CountNonZero(mask) / (double)(mask.Rows * mask.Cols);
            double minSkinRatio = (meanY < 60) ? 0.12 : 0.18;

            // 面積判定（以固定 ROI 的 7% 等效到 256x256）
            int minAreaPx = (int)(256 * 256 * MIN_AREA_RATIO);
            if (skinRatio < minSkinRatio)
            {
                DrawInfo(roiResized, "Skin ratio too low");
                return ("", 0, 0, 0, 0);
            }

            // 輪廓
            Cv2.FindContours(mask, out var contours, out _, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            if (contours.Length == 0)
            {
                DrawInfo(roiResized, "No contour");
                return ("", 0, 0, 0, 0);
            }
            var cnt = contours[0];
            double bestA = Cv2.ContourArea(cnt);
            foreach (var c in contours) { double a = Cv2.ContourArea(c); if (a > bestA) { bestA = a; cnt = c; } }
            if (bestA < minAreaPx)
            {
                DrawInfo(roiResized, "Area too small");
                return ("", 0, 0, 0, 0);
            }

            // 幾何特徵
            var bbox = Cv2.BoundingRect(cnt);
            double peri = Cv2.ArcLength(cnt, true);
            var rr = Cv2.MinAreaRect(cnt);
            double rw = Math.Max(1, rr.Size.Width), rh = Math.Max(1, rr.Size.Height);
            double arRot = (rw > rh) ? (rw / rh) : (rh / rw);

            double extent = bestA / (bbox.Width * bbox.Height);
            double circularity = 4.0 * Math.PI * bestA / (peri * peri);

            var hullPts = Cv2.ConvexHull(cnt);
            double hullArea = Math.Max(1.0, Cv2.ContourArea(hullPts));
            double solidity = bestA / hullArea;

            // 凸性缺陷（手指縫）→ vCount / finger / deepest
            int vCount = 0, finger = 0;
            double deepest = 0;
            var hullIdx = Cv2.ConvexHullIndices(cnt);
            if (hullIdx.Length >= 3)
            {
                Vec4i[]? defects = Cv2.ConvexityDefects(cnt, hullIdx);
                if (defects != null && defects.Length > 0)
                {
                    double diag = Math.Sqrt(bbox.Width * bbox.Width + bbox.Height * bbox.Height);
                    double minDepth = diag * 0.08; // 與原則近似

                    foreach (var d in defects)
                    {
                        // OpenCvSharp: 使用 Item0..Item3 取值
                        int startIdx = d.Item0;
                        int endIdx = d.Item1;
                        int farIdx = d.Item2;
                        // depth 為「固定小數點」，要除以 256 才是像素
                        double depth = d.Item3 / 256.0;

                        CvPoint sp = cnt[startIdx];
                        CvPoint ep = cnt[endIdx];
                        CvPoint fp = cnt[farIdx];

                        double ang = Angle(sp, fp, ep);

                        if (depth > minDepth && ang >= 20 && ang <= 80) vCount++;
                        if (depth > minDepth && ang < 70) finger++;
                        if (depth > deepest) deepest = depth;
                    }
                }
            }

            double deepestFrac = deepest / Math.Max(1.0, Math.Sqrt(bbox.Width * bbox.Width + bbox.Height * bbox.Height));

            // 用你的「真實規則」打分
            var scores = ClassifyCSharpLike(
                area: bestA, peri: peri, bboxW: bbox.Width, bboxH: bbox.Height,
                extent: extent, circularity: circularity, solidity: solidity,
                vCount: vCount, finger: finger, deepestFrac: deepestFrac, arRot: arRot
            );

            // 視覺疊圖（在 roiResized 上）
            Cv2.Rectangle(roiResized, bbox, new Scalar(255, 128, 0), 2);
            var hp = hullPts;
            for (int i = 0; i < hp.Length; i++)
            {
                var p1 = hp[i]; var p2 = hp[(i + 1) % hp.Length];
                Cv2.Line(roiResized, p1, p2, new Scalar(0, 255, 255), 2);
            }
            var info = $"P={scores.paper} R={scores.rock} S={scores.scissors}\nTop1={scores.winner} Top2={scores.top2} M={scores.margin:F1}";
            DrawInfo(roiResized, info);

            // 把 roi 疊回原 ROI（不需要另外貼，因為 roiResized 不是原圖的 view；純顯示已足夠）

            // 回傳結果
            return (scores.winner, (int)Math.Round(scores.margin), scores.paper, scores.rock, scores.scissors);
        }

        private static double Angle(CvPoint sp, CvPoint fp, CvPoint ep)
        {
            CvPoint2f v1 = new CvPoint2f(sp.X - fp.X, sp.Y - fp.Y);
            CvPoint2f v2 = new CvPoint2f(ep.X - fp.X, ep.Y - fp.Y);

            double num = v1.X * v2.X + v1.Y * v2.Y;
            double den = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y) *
                         Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y) + 1e-6;
            double cos = Math.Max(-1.0, Math.Min(1.0, num / den));
            return Math.Acos(cos) * 180.0 / Math.PI;
        }


        private (string winner, double margin, int paper, int rock, int scissors, string top2) ClassifyCSharpLike(
            double area, double peri, int bboxW, int bboxH,
            double extent, double circularity, double solidity,
            int vCount, int finger, double deepestFrac, double arRot)
        {
            // 依你 C# 規則的「真實權重與門檻」（前面幫你匯出的版本）
            int shapeP = 0, shapeR = 0, shapeS = 0;

            // Paper
            if (finger >= 5) shapeP += 9;
            else if (finger == 4) shapeP += 8;
            else if (finger == 3) shapeP += 6;
            if (extent >= 0.52) shapeP += 2;
            if (solidity >= 0.60 && solidity <= 0.78) shapeP += 1;
            if (circularity <= 0.22) shapeP += 2;
            if (vCount >= 3) shapeP += 3;
            if ((vCount == 2) && (extent >= 0.50 && extent <= 0.65) && (arRot <= 1.30) && (solidity >= 0.66 && solidity <= 0.80))
            { shapeP += 4; shapeS -= 4; }

            // Rock
            if (finger <= 1) shapeR += 2;
            if (circularity >= 0.35) shapeR += 2;
            if (extent >= 0.48 && extent <= 0.80) shapeR += 2;
            if (solidity >= 0.80) shapeR += 1;
            if (vCount >= 2) shapeR -= 6;
            if (finger >= 3) shapeR -= 6;
            if (vCount <= 1 && deepestFrac < 0.18) shapeR += 3;
            if (arRot >= 1.80 && vCount <= 1 && deepestFrac < 0.18) shapeR += 2;
            if (extent <= 0.46 && vCount <= 1) shapeR += 1;

            // Scissors
            if (finger >= 3) shapeS -= 6;
            if (vCount == 2) shapeS += 7;
            else if (vCount == 1) { if (deepestFrac >= 0.22 && arRot >= 1.60) shapeS += 4; else shapeS += 1; }
            else if (vCount >= 3) shapeS -= 3;
            if (deepestFrac >= 0.18) shapeS += 3;
            if (deepestFrac >= 0.26) shapeS += 1;
            if (arRot >= 1.35) shapeS += 3;
            if (extent <= 0.56) shapeS += 1;
            if (circularity >= 0.40) shapeS -= 2;
            if (solidity >= 0.88) shapeS -= 2;
            if (extent >= 0.60 && extent <= 0.72)
            { if (arRot >= 1.75 && deepestFrac >= 0.28 && solidity <= 0.83) shapeS += 3; else shapeS -= 3; }
            if (extent >= 0.62 && (vCount >= 2 || finger >= 2)) shapeS -= 3;
            if (vCount == 2 && arRot < 1.30) shapeS -= 3;

            int sPaper = shapeP * 7;
            int sRock = shapeR * 6;
            int sScis = shapeS * 7;

            // Top1/Top2
            (string n, int v)[] arr = { ("Paper", sPaper), ("Rock", sRock), ("Scissors", sScis) };
            Array.Sort(arr, (a, b) => b.v.CompareTo(a.v));
            string winner = (arr[0].v > 0 && (arr[0].v - arr[1].v) >= 2) ? arr[0].n : "";
            double margin = arr[0].v - arr[1].v;
            string top2 = arr[1].n;
            return (winner, margin, sPaper, sRock, sScis, top2);
        }

        // 保留這個版本（用 _ads.Publish）
        private void ApplyStabilizationAndOutput((string winner, int margin, int paper, int rock, int scissors) r)
        {
            // UI
            SafeUI(() =>
            {
                lblGesture.Text = $"手勢：{(string.IsNullOrEmpty(r.winner) ? "--（未定）" : r.winner)}";
                lblMargin.Text = $"Margin：{r.margin}";
            });

            // 未達穩定
            if (string.IsNullOrEmpty(r.winner) || r.margin < STABLE_NEED_DIFF)
            {
                _stableWatch.Reset();
                _lastWinner = "";
                if (chkADS.Checked) { try { _ads?.Publish("", 0, false); } catch { } }
                return;
            }

            // 切換手勢，重計時
            if (r.winner != _lastWinner)
            {
                _lastWinner = r.winner;
                _stableWatch.Restart();
                if (chkADS.Checked) { try { _ads?.Publish("", r.margin, false); } catch { } }
                return;
            }

            // 穩定輸出
            if (_stableWatch.IsRunning && _stableWatch.ElapsedMilliseconds >= STABLE_HOLD_MS)
            {
                if (chkADS.Checked)
                {
                    string lab = r.winner.ToLowerInvariant(); // "paper"/"rock"/"scissors"
                    try { _ads?.Publish(lab, r.margin, true); } catch { }
                }
            }
        }


        private static void DrawInfo(Mat img, string text)
        {
            var lines = text.Split('\n');
            int y = 22;
            Cv2.Rectangle(img, new Rect(5, 5, img.Width - 10, 26 * lines.Length + 12), new Scalar(0, 0, 0), -1);
            foreach (var s in lines)
            {
                Cv2.PutText(img, s, new OpenCvSharp.Point(12, y), HersheyFonts.HersheySimplex, 0.6, new Scalar(255, 255, 255), 1, LineTypes.AntiAlias);
                y += 26;
            }
        }

        private void SafeUI(Action a)
        {
            if (IsHandleCreated && InvokeRequired) BeginInvoke(a);
            else a();
        }

        private static bool IsDesignTime()
        {
            // 設計器實例化時為 Designtime；備援判斷避免某些情況 DesignMode = false
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        public class AiRow : INotifyPropertyChanged
        {
            public bool Sel { get; set; }
            public string Group { get; set; }   // RTU AI (REAL, Temperature) / EtherCAT AI(A) ... / EtherCAT AI(V) ...
            public string Sensor { get; set; }  // RTU_Temp_1 / In3238_x / In3438_x
            public string Label { get; set; }   // 顯示別名
            public string Unit { get; set; }    // "°C" / "A" / "V"

            private double _current = double.NaN;
            public double Current
            {
                get => _current;
                set { _current = value; OnPropertyChanged(nameof(Current)); OnPropertyChanged(nameof(Status)); }
            }

            public double Low { get; set; }
            public double High { get; set; }

            public string Status
            {
                get
                {
                    if (double.IsNaN(Current)) return "—";
                    if (Current < Low) return "LOW";
                    if (Current > High) return "HIGH";
                    return "OK";
                }
            }

            [Browsable(false)] public uint Handle { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
        }

        private void AI_Tab_InitOnce()
        {
            if (_aiInited || IsDesignTime()) return;
            _aiInited = true;

            BuildAiRows();         // 5-1 建立 17 筆資料列
            InitAiGrid();          // 5-2 綁定 DataGridView（或程式動態建立欄位）
            WireAiEvents();        // 5-3 綁定按鈕/事件
            ConnectAdsForAi();     // 5-4 連 ADS + 取得 handles
            StartAiPolling();      // 5-5 啟動輪詢讀值
        }

        private void BuildAiRows()
        {
            _aiRows.Clear();

            // 溫度（RTU）
            _aiRows.Add(new AiRow
            {
                Sel = false,
                Group = "RTU AI (Temperature)",
                Sensor = "Sensor",
                Label = "RTU_Temp_1",
                Unit = "°C",
                Low = 0,
                High = 100,
                Current = double.NaN
            });

            // 電流（AI(A)）In3438_1..8，單位 A
            for (int i = 1; i <= 8; i++)
            {
                _aiRows.Add(new AiRow
                {
                    Sel = false,
                    Group = "EtherCAT AI (Current)",
                    Sensor = $"In3438_{i}",
                    Label = $"Current_CH{i}",
                    Unit = "mA",
                    Low = 0,
                    High = 20,
                    Current = double.NaN
                });
            }

            // 電壓（AI(V)）In3238_1..8，單位 V
            for (int i = 1; i <= 8; i++)
            {
                _aiRows.Add(new AiRow
                {
                    Sel = false,
                    Group = "EtherCAT AI (Voltage)",
                    Sensor = $"In3238_{i}",
                    Label = $"Voltage_CH{i}",
                    Unit = "V",
                    Low = 0,
                    High = 10,
                    Current = double.NaN
                });
            }
        }

        private void InitAiGrid()
        {
            EnsureGridColumns();     // ← 一律先確保欄位存在
            if (gridAI == null) return; // 沒找到就先跳出，避免 NRE

            gridAI.AutoGenerateColumns = false;
            gridAI.DataSource = _aiRows;

            gridAI.Columns["Sel"].DataPropertyName = nameof(AiRow.Sel);
            gridAI.Columns["Group"].DataPropertyName = nameof(AiRow.Group);
            gridAI.Columns["Sensor"].DataPropertyName = nameof(AiRow.Sensor);
            gridAI.Columns["Label"].DataPropertyName = nameof(AiRow.Label);
            gridAI.Columns["Current"].DataPropertyName = nameof(AiRow.Current);
            gridAI.Columns["Unit"].DataPropertyName = nameof(AiRow.Unit);
            gridAI.Columns["Low"].DataPropertyName = nameof(AiRow.Low);
            gridAI.Columns["High"].DataPropertyName = nameof(AiRow.High);
            gridAI.Columns["Status"].DataPropertyName = nameof(AiRow.Status);

            gridAI.Columns["Current"].ReadOnly = true;
            gridAI.Columns["Unit"].ReadOnly = true;
            gridAI.Columns["Status"].ReadOnly = true;

            FitRowHeightToFill();
            gridAI.SizeChanged += (_, __) => FitRowHeightToFill();   // 視窗大小改變時重算

            // ---- 讓欄位填滿寬度、同時放大 Group 欄 ----
            gridAI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 建議的比例（FillWeight 是相對份量；數字可再微調）
            var col = gridAI.Columns;
            col["Sel"].FillWeight = 40; col["Sel"].MinimumWidth = 40;
            col["Group"].FillWeight = 280; col["Group"].MinimumWidth = 260; // ← 放大 Group
            col["Sensor"].FillWeight = 190; col["Sensor"].MinimumWidth = 150;
            col["Label"].FillWeight = 190; col["Label"].MinimumWidth = 150;
            col["Current"].FillWeight = 130; col["Current"].MinimumWidth = 110;
            col["Unit"].FillWeight = 70; col["Unit"].MinimumWidth = 60;
            col["Low"].FillWeight = 90; col["Low"].MinimumWidth = 80;
            col["High"].FillWeight = 90; col["High"].MinimumWidth = 80;
            col["Status"].FillWeight = 130; col["Status"].MinimumWidth = 120;

            //（可選）避免使用者拖到太窄
            foreach (DataGridViewColumn c in gridAI.Columns)
                c.Resizable = DataGridViewTriState.True;

            // 讓 grid 真的吃滿容器寬度/高度
            //gridAI.Dock = DockStyle.Fill;
        }

        private void WireAiEvents()
        {
            // 透過查找，不覆寫任何欄位（避免 TabPage 同名衝突）
            var btnApply = this.Controls.Find("btnApplySelected", true).OfType<Button>().FirstOrDefault();
            var btnAll = this.Controls.Find("btnSelectAll", true).OfType<Button>().FirstOrDefault();
            var btnNone = this.Controls.Find("btnUnselect", true).OfType<Button>().FirstOrDefault();

            if (btnApply != null) btnApply.Click += (_, __) => ApplyThresholdToSelected();
            if (btnAll != null) btnAll.Click += (_, __) => SetSelectAll(true);
            if (btnNone != null) btnNone.Click += (_, __) => SetSelectAll(false);

            // 也用查找方式取得 gridAI（若欄位為 null）
            if (gridAI == null)
                gridAI = this.Controls.Find("gridAI", true).OfType<DataGridView>().FirstOrDefault();

            if (gridAI != null)
            {
                gridAI.CellEndEdit += GridAI_CellEndEdit;
                gridAI.CellFormatting += GridAI_CellFormatting;
            }
        }


        private void ConnectAdsForAi()
        {
            if (_ads == null || !_ads.IsOk)
            {
                MessageBox.Show("ADS 尚未連線，無法建立 AI 變數 Handle。請確認 _ads.Connect(...) 成功。");
                return;
            }

            foreach (var r in _aiRows)
            {
                try
                {
                    // ★ 若變數不在 GVL，請把 PlcPrefix 改為 "MAIN." 或你的實際路徑
                    r.Handle = _ads.CreateHandle(PlcPrefix + r.Sensor);
                }
                catch (TwinCAT.Ads.AdsErrorException ex)
                {
                    r.Handle = 0; // 該符號沒有就先跳過，但不中斷其它
                    Debug.WriteLine($"[ADS] Symbol not found: {PlcPrefix + r.Sensor} -> {ex.Message}");
                }
            }
        }

        private void StartAiPolling()
        {
            if (_aiTimer != null) return;

            _aiTimer = new System.Windows.Forms.Timer { Interval = 200 };
            _aiTimer.Tick += (_, __) =>
            {
                if (_ads == null || !_ads.IsOk) return;

                foreach (var r in _aiRows)
                {
                    if (r.Handle == 0) { r.Current = double.NaN; continue; }

                    try
                    {
                        // PLC REAL -> C# float
                        float v = _ads.ReadAny<float>(r.Handle);
                        r.Current = v; // 會自動觸發 Status 計算與重畫
                    }
                    catch
                    {
                        r.Current = double.NaN;
                    }
                }

                gridAI?.Invalidate(); // 讓 CellFormatting 套顏色
            };
            _aiTimer.Start();
        }

        private void ApplyThresholdToSelected()
        {
            double lo = (double)numLow.Value;
            double hi = (double)numHigh.Value;
            foreach (var r in _aiRows)
                if (r.Sel) { r.Low = lo; r.High = hi; }
            gridAI.Refresh();
        }

        private void SetSelectAll(bool on)
        {
            foreach (var r in _aiRows) r.Sel = on;
            gridAI.Refresh();
        }

        private void GridAI_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var name = gridAI.Columns[e.ColumnIndex].Name;
            if (name != "Low" && name != "High") return;

            var row = gridAI.Rows[e.RowIndex];
            var data = row.DataBoundItem as AiRow;
            if (data == null) return;

            try
            {
                double v = Convert.ToDouble(row.Cells[e.ColumnIndex].Value);
                if (name == "Low") data.Low = v;
                else data.High = v;
            }
            catch
            {
                row.Cells["Low"].Value = data.Low;
                row.Cells["High"].Value = data.High;
            }
        }

        private void GridAI_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (gridAI.Columns[e.ColumnIndex].Name != "Current") return;

            var r = gridAI.Rows[e.RowIndex].DataBoundItem as AiRow;
            if (r == null) return;

            e.CellStyle.ForeColor = Color.White;
            switch (r.Status)
            {
                case "OK":
                    e.CellStyle.BackColor = Color.FromArgb(30, 142, 110);  // 暗綠
                    break;
                case "HIGH":
                    e.CellStyle.BackColor = Color.FromArgb(185, 70, 70);   // 暗紅
                    break;
                case "LOW":
                    e.CellStyle.BackColor = Color.FromArgb(65, 90, 160);   // 暗藍
                    break;
                default:
                    e.CellStyle.BackColor = C_BG2;                         // 中性
                    e.CellStyle.ForeColor = C_MUTED;
                    break;
            }
        }

        private void DIDO_InitOnce()
        {
            if (_didoInited || IsDesignTime()) return;
            _didoInited = true;

            // 1) 把設計師放好的控制項，依 Name 收集成陣列
            for (int i = 1; i <= 8; i++)
            {
                _diIcon[i - 1] = this.Controls.Find($"diIcon{i}", true).OfType<PictureBox>().FirstOrDefault();
                _diName[i - 1] = this.Controls.Find($"diName{i}", true).OfType<Label>().FirstOrDefault();

                _doLink[i - 1] = this.Controls.Find($"doLink{i}", true).OfType<PictureBox>().FirstOrDefault();
                _doSwitch[i - 1] = this.Controls.Find($"doSwitch{i}", true).OfType<PictureBox>().FirstOrDefault();
                _doState[i - 1] = this.Controls.Find($"doState{i}", true).OfType<Label>().FirstOrDefault();

                if (_doSwitch[i - 1] != null)
                {
                    _doSwitch[i - 1].Tag = i - 1;              // 0..7
                    _doSwitch[i - 1].Click -= DoSwitch_Click;
                    _doSwitch[i - 1].Click += DoSwitch_Click;
                }
            }

            // 2) 初始圖與文字（避免設計師沒先放）
            for (int i = 0; i < 8; i++)
            {
                if (_diIcon[i] != null) _diIcon[i].Image = Properties.Resources.di_off;

                if (_doLink[i] != null) _doLink[i].Image = Properties.Resources.DO_connected; // 先視為 close
                if (_doSwitch[i] != null) _doSwitch[i].Image = Properties.Resources.switch_off;
                if (_doState[i] != null) _doState[i].Text = "close";
            }

            // 3) 取得 ADS handles
            ConnectAdsForDido();

            // 4) 啟動輪詢（讀 DI & 同步 DO 現值）
            StartDidoPolling();
        }

        private void ConnectAdsForDido()
        {
            if (_ads == null || !_ads.IsOk) return;

            for (int i = 0; i < 8; i++)
            {
                try { _hDI[i] = _ads.CreateHandle($"{PlcDidoPrefix}Input_{i + 1}"); } catch { _hDI[i] = 0; }
                try { _hDO[i] = _ads.CreateHandle($"{PlcDidoPrefix}Output_{i + 1}"); } catch { _hDO[i] = 0; }
            }
        }

        private void StartDidoPolling()
        {
            if (_didoTimer != null) return;
            _didoTimer = new System.Windows.Forms.Timer { Interval = 200 };
            _didoTimer.Tick += (_, __) =>
            {
                if (_ads == null || !_ads.IsOk) return;

                // 讀 DI：1=點亮(di_on)、0=關燈(di_off)
                for (int i = 0; i < 8; i++)
                {
                    try
                    {
                        if (_hDI[i] == 0 || _diIcon[i] == null) continue;
                        bool v = _ads.ReadAny<bool>(_hDI[i]);
                        _diIcon[i].Image = v ? Properties.Resources.di_on : Properties.Resources.di_off;
                    }
                    catch { }
                }

                // 讀 DO：1=open(斷開)、0=close(接起來) → 同步 UI（避免外部更動）
                for (int i = 0; i < 8; i++)
                {
                    try
                    {
                        if (_hDO[i] == 0) continue;
                        bool plc = _ads.ReadAny<bool>(_hDO[i]);           // plc = true 表示 close(1)
                        bool open = !plc;
                        if (open != _doOpen[i])
                        {
                            _doOpen[i] = open;
                            UpdateDoRowUI(i, open);
                        }
                    }
                    catch { }
                }
            };
            _didoTimer.Start();
        }

        private void DoSwitch_Click(object sender, EventArgs e)
        {
            if (_ads == null || !_ads.IsOk) { System.Media.SystemSounds.Beep.Play(); return; }
            var pb = (PictureBox)sender;
            int i = (int)pb.Tag;

            // 切換：true=open(1 斷開)、false=close(0 接起來)
            bool newOpen = !_doOpen[i];
            _doOpen[i] = newOpen;

            try
            {
                if (_hDO[i] != 0) _ads.WriteBool(_hDO[i], !newOpen);
            }
            catch { }

            UpdateDoRowUI(i, newOpen);
        }

        private void UpdateDoRowUI(int i, bool isOpen)
        {
            if (_doLink[i] != null) _doLink[i].Image = isOpen ? Properties.Resources.DO_separated : Properties.Resources.DO_connected;
            if (_doSwitch[i] != null) _doSwitch[i].Image = isOpen ? Properties.Resources.switch_on : Properties.Resources.switch_off;
            if (_doState[i] != null) _doState[i].Text = isOpen ? "open" : "close";
        }

        private void EnsureGridColumns()
        {
            // 找到 gridAI；若欄位沒產生成功也避免 NullRef
            if (gridAI == null)
            {
                // 盡力用名稱尋找（避免設計器欄位名不同）
                var found = this.Controls.Find("gridAI", true);
                gridAI = found.Length > 0 ? found[0] as DataGridView : null;
                if (gridAI == null)
                {
                    MessageBox.Show("找不到 DataGridView 'gridAI'。請確認 tab_AI 上的 Name = gridAI。");
                    return;
                }
            }

            gridAI.AutoGenerateColumns = false;
            gridAI.AllowUserToAddRows = false;
            gridAI.AllowUserToDeleteRows = false;
            gridAI.RowHeadersVisible = false;
            gridAI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridAI.MultiSelect = true;

            // 統一用程式建立，避免名稱不一致
            gridAI.Columns.Clear();

            var sel = new DataGridViewCheckBoxColumn { Name = "Sel", HeaderText = "Sel", Width = 54, DataPropertyName = nameof(AiRow.Sel) };
            gridAI.Columns.Add(sel);

            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group", HeaderText = "Group", Width = 200, DataPropertyName = nameof(AiRow.Group) });
            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sensor", HeaderText = "Sensor", Width = 220, DataPropertyName = nameof(AiRow.Sensor) });
            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Label", HeaderText = "Label", Width = 120, DataPropertyName = nameof(AiRow.Label) });

            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Current", HeaderText = "Current", Width = 110, DataPropertyName = nameof(AiRow.Current), ReadOnly = true });
            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "Unit", Width = 70, DataPropertyName = nameof(AiRow.Unit), ReadOnly = true });

            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Low", HeaderText = "Low", Width = 110, DataPropertyName = nameof(AiRow.Low) });
            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "High", HeaderText = "High", Width = 110, DataPropertyName = nameof(AiRow.High) });

            gridAI.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 120, DataPropertyName = nameof(AiRow.Status), ReadOnly = true });
        }

        private void FitRowHeightToFill()
        {
            if (gridAI == null || _aiRows == null || _aiRows.Count == 0) return;

            int headerH = gridAI.ColumnHeadersHeight;
            int border = 2; // 微調
            int h = (gridAI.ClientSize.Height - headerH - border) / _aiRows.Count;

            // 設定合理上下限，避免太扁或太高
            h = Math.Max(28, Math.Min(60, h));

            gridAI.RowTemplate.Height = h;
            foreach (DataGridViewRow r in gridAI.Rows) r.Height = h;

            gridAI.Invalidate();
        }

        private void ApplyDarkTheme()
        {
            // Form / Tab
            this.BackColor = C_BG;
            tab_AI.BackColor = C_BG;

            // Buttons（若你的名字不同，可改成動態尋找）
            void StyleButton(Button b)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = C_LINE;
                b.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 58, 70);
                b.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 52, 63);
                b.BackColor = Color.FromArgb(43, 48, 59);
                b.ForeColor = C_TEXT;
            }
            foreach (var b in new[] { btnApplySelected, btnSelectAll, btnUnselect })
                if (b != null) StyleButton(b);

            // NumericUpDowns
            void StyleSpin(NumericUpDown n)
            {
                if (n == null) return;

                // 外層
                n.BackColor = C_BG2;
                n.ForeColor = C_TEXT;
                n.BorderStyle = BorderStyle.FixedSingle;

                // 內嵌 TextBox（顯示數值的那格）
                var tb = n.Controls.OfType<TextBox>().FirstOrDefault();
                if (tb != null)
                {
                    tb.BackColor = C_BG2;
                    tb.ForeColor = C_TEXT;
                    tb.BorderStyle = BorderStyle.None;   // 看起來更和諧
                    tb.AutoSize = false;                 // 避免高度被還原
                    tb.Height = n.Height - 2;
                    tb.Location = new System.Drawing.Point(1, 1);
                }

                // 內嵌按鈕區（上下箭頭）—顏色能改到就改，某些主題不完全吃
                if (n.Controls.Count > 0)
                {
                    var btns = n.Controls[0];            // UpDownButtons (internal)
                    btns.BackColor = C_BG2;
                    btns.ForeColor = C_TEXT;
                    // 若仍偏白，可以強制塗底色：
                    // btns.Paint += (_, e) => e.Graphics.Clear(C_BG2);
                }
            }

            StyleSpin(numLow);
            StyleSpin(numHigh);

            // 說明文字 / Legend（用名字找，有就上色）
            foreach (var name in new[] { "lblOk", "lblHigh", "lblLow" })
            {
                var lbl = GetCtl<Label>(name);
                if (lbl != null) lbl.ForeColor = C_TEXT;
            }

            // 若你有放上方 Panel/Title（名字：panelTop / lblTitle），就套色；沒有就略過
            var panelTop = GetCtl<Panel>("panelTop");
            if (panelTop != null) panelTop.BackColor = C_ACCENT;

            var lblTitle = GetCtl<Label>("lblTitle");
            if (lblTitle != null) lblTitle.ForeColor = Color.White;

            // DataGridView
            if (gridAI != null)
            {
                gridAI.BackgroundColor = C_BG2;
                gridAI.BorderStyle = BorderStyle.None;
                gridAI.GridColor = C_LINE;
                gridAI.EnableHeadersVisualStyles = false;

                gridAI.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                gridAI.ColumnHeadersDefaultCellStyle.BackColor = C_BG3;
                gridAI.ColumnHeadersDefaultCellStyle.ForeColor = C_TEXT;
                gridAI.ColumnHeadersDefaultCellStyle.SelectionBackColor = C_BG3;
                gridAI.ColumnHeadersDefaultCellStyle.SelectionForeColor = C_TEXT;
                gridAI.ColumnHeadersDefaultCellStyle.Font = new Font(gridAI.Font, FontStyle.Bold);

                gridAI.DefaultCellStyle.BackColor = C_BG2;
                gridAI.DefaultCellStyle.ForeColor = C_TEXT;
                gridAI.DefaultCellStyle.SelectionBackColor = C_SEL;
                gridAI.DefaultCellStyle.SelectionForeColor = C_TEXT;

                // 相間列
                gridAI.AlternatingRowsDefaultCellStyle.BackColor = C_ROW_ALT;
                gridAI.AlternatingRowsDefaultCellStyle.ForeColor = C_TEXT;
                gridAI.AlternatingRowsDefaultCellStyle.SelectionBackColor = C_SEL;
                gridAI.AlternatingRowsDefaultCellStyle.SelectionForeColor = C_TEXT;

                gridAI.RowHeadersVisible = false;
                gridAI.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

                // 減少閃爍
                try
                {
                    typeof(DataGridView).InvokeMember("DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.SetProperty,
                        null, gridAI, new object[] { true });
                }
                catch { }
            }

            // 右上角狀態徽章（如果有 lblAdsState 就會更新）
            UpdateHeaderByAds();
            StyleAllTabControls();   // 把所有 TabControl（AI / DI/DO / OpenCV）套上暗色 + 自繪
        }


        // 安全尋找控制項（找不到就回 null）
        private T GetCtl<T>(string name) where T : Control =>
            this.Controls.Find(name, true).OfType<T>().FirstOrDefault();

        // 依 _ads.IsOk 更新右上角狀態徽章（若 lblAdsState 不存在就自動略過）
        private void UpdateHeaderByAds()
        {
            var lblState = GetCtl<Label>("lblAdsState");
            if (lblState != null)
            {
                bool ok = _ads?.IsOk == true;
                lblState.Text = ok ? "● Connected" : "● Disconnected";
                lblState.ForeColor = ok ? Color.FromArgb(157, 246, 100) : Color.OrangeRed;
            }
        }

        // 讓整個視窗（含巢狀容器）所有 TabControl 都套暗色
        private void StyleAllTabControls(Control root = null)
        {
            root ??= this;
            foreach (Control c in root.Controls)
            {
                if (c is TabControl tc) StyleTabControl(tc);
                // 遞迴處理巢狀容器
                if (c.HasChildren) StyleAllTabControls(c);
            }
            
        }

        // 對單一 TabControl 上色 + 自繪
        private void StyleTabControl(TabControl tc)
        {
            if (tc == null) return;

            // 基本底色
            tc.BackColor = C_BG;          // 頁籤後方的總底色
            tc.ForeColor = C_TEXT;

            // 頁面（TabPage）底色/文字色
            foreach (TabPage p in tc.TabPages)
            {
                p.UseVisualStyleBackColor = false;
                p.BackColor = C_BG;       // 內容底色
                p.ForeColor = C_TEXT;
            }

            // 啟用自繪頁籤
            tc.DrawMode = TabDrawMode.OwnerDrawFixed;
            // 想要固定較高的頁籤高度可打開這兩行（需要固定寬度也可以調整）
            // tc.SizeMode = TabSizeMode.Fixed;
            // tc.ItemSize = new Size(100, 30); // 寬、 高

            tc.Padding = new WinPoint(18, 5);     // 文字左右/上下間距

            // 先移除舊處理器（避免重覆掛）
            tc.DrawItem -= Tab_DrawItem_Dark;
            tc.DrawItem += Tab_DrawItem_Dark;
            tc.SelectedIndexChanged -= (s, e) => tc.Invalidate();
            tc.SelectedIndexChanged += (s, e) => tc.Invalidate();

            // 重新繪製一下
            tc.Invalidate();
        }

        // 真正畫每個頁籤
        private void Tab_DrawItem_Dark(object sender, DrawItemEventArgs e)
        {
            var tc = (TabControl)sender;
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var tab = tc.TabPages[e.Index];
            var rect = e.Bounds;

            // 背景色：選取 / 未選取
            var bg = selected ? C_BG3 : C_BG2;
            using (var b = new SolidBrush(bg))
                g.FillRectangle(b, rect);

            // 底線（或外框）
            using (var pen = new Pen(C_LINE))
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            // 文字色：選取亮、未選取略暗
            var txtColor = selected ? C_TEXT : C_MUTED;

            var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                        TextFormatFlags.EndEllipsis;
            TextRenderer.DrawText(g, tab.Text, tc.Font, rect, txtColor, flags);
        }

    }

    public sealed class AdsPublisher : IDisposable
    {
        private readonly AdsClient _ads = new AdsClient();
        private uint _hGesture, _hMargin, _hStable;
        private bool _ok;
        // === 新增：對外提供連線狀態/輔助方法 ===
        public string ConnectedNetId { get; private set; }
        public int ConnectedPort { get; private set; }
        public bool IsOk => _ok;

        private readonly Action<string> _log;
        public AdsPublisher(Action<string> log = null) => _log = log ?? (_ => { });
        private void Log(string s) { try { _log(s); } catch { } }

        public bool Connect(string amsNetIdOrAuto, int port = 851, bool throwOnFail = false)
        {
            string id = amsNetIdOrAuto;
            if (string.IsNullOrWhiteSpace(id) || id.Equals("auto", StringComparison.OrdinalIgnoreCase))
            {
                id = ReadLocalAmsNetId();
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = "127.0.0.1.1.1";
                    Log("Local AMS Net ID 未從登錄檔取得，改用 127.0.0.1.1.1");
                }
            }

            try
            {
                _ads.Connect(id, port);
                _hGesture = _ads.CreateVariableHandle("GVL.gGesture");
                _hMargin = _ads.CreateVariableHandle("GVL.gMargin");
                _hStable = _ads.CreateVariableHandle("GVL.gStable");
                _ok = true;
                ConnectedNetId = id;     // ← 這兩行要在成功後設定
                ConnectedPort = port;
                Log($"ADS Connect OK id={id}, port={port}");
                return true;
            }
            catch (Exception ex)
            {
                _ok = false;
                Log($"ADS Connect ERROR: {ex.Message}");
                if (throwOnFail) throw;   // ← 需要時才丟
                return false;             // ← 預設不丟，讓設計器能活
            }
        }


        /// <summary>
        /// label: "paper" / "rock" / "scissors"（其餘皆當 0）
        /// margin: 任意 int
        /// stable: 是否達到穩定條件
        /// </summary>
        public void Publish(string label, int margin, bool stable)
        {
            if (!_ok) { Log("Publish skipped: ADS not connected"); return; }

            short vGesture = (short)(label == "paper" ? 1 :
                                     label == "rock" ? 2 :
                                     label == "scissors" ? 3 : 0);
            short vMargin = (short)Math.Clamp(margin, short.MinValue, short.MaxValue);

            _ads.WriteAny(_hGesture, vGesture);
            _ads.WriteAny(_hMargin, vMargin);
            _ads.WriteAny(_hStable, stable);

            Log($"Write gGesture={vGesture}({label}), gMargin={vMargin}, gStable={stable}");
        }

        public void Dispose()
        {
            try { if (_hGesture != 0) _ads.DeleteVariableHandle(_hGesture); } catch { }
            try { if (_hMargin != 0) _ads.DeleteVariableHandle(_hMargin); } catch { }
            try { if (_hStable != 0) _ads.DeleteVariableHandle(_hStable); } catch { }
            _ads.Dispose();
        }

        public static string ReadLocalAmsNetId()
        {
            try
            {
                using (var k = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Beckhoff\TwinCAT3\System"))
                    if (k?.GetValue("AmsNetId") is string s && !string.IsNullOrWhiteSpace(s)) return s;
                using (var k2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Beckhoff\TwinCAT3\System"))
                    if (k2?.GetValue("AmsNetId") is string s2 && !string.IsNullOrWhiteSpace(s2)) return s2;
            }
            catch { }
            return null;
        }

        public uint CreateHandle(string symbolPath)
        {
            if (!_ok) throw new InvalidOperationException("ADS not connected.");
            return _ads.CreateVariableHandle(symbolPath);
        }
        public void DeleteHandle(uint h)
        {
            if (h != 0)
                try { _ads.DeleteVariableHandle(h); } catch { }
        }
        public T ReadAny<T>(uint handle)
        {
            if (!_ok) throw new InvalidOperationException("ADS not connected.");
            // TwinCAT REAL 對應 C# float
            return (T)_ads.ReadAny(handle, typeof(T));
        }

        public void WriteAny<T>(uint handle, T value)
        {
            if (!_ok) throw new InvalidOperationException("ADS not connected.");
            _ads.WriteAny(handle, value);
        }

        public void WriteBool(uint handle, bool value)
        {
            if (!_ok) throw new InvalidOperationException("ADS not connected.");
            _ads.WriteAny(handle, value);
        }

    }
}
