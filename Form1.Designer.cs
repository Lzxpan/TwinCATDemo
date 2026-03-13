namespace TwinCATDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl = new TabControl();
            tab_AI = new TabPage();
            gridAI = new DataGridView();
            Sel = new DataGridViewCheckBoxColumn();
            Group = new DataGridViewTextBoxColumn();
            Senser = new DataGridViewTextBoxColumn();
            Lable = new DataGridViewTextBoxColumn();
            Current = new DataGridViewTextBoxColumn();
            Unit = new DataGridViewTextBoxColumn();
            Low = new DataGridViewTextBoxColumn();
            High = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            lab_circle_blue = new Label();
            pic_circle_blue = new PictureBox();
            lab_circle_red = new Label();
            pic_circle_red = new PictureBox();
            lab_circle_green = new Label();
            pic_circle_green = new PictureBox();
            btnUnselect = new Button();
            btnSelectAll = new Button();
            btnApplySelected = new Button();
            lab_high = new Label();
            lab_low = new Label();
            numHigh = new NumericUpDown();
            numLow = new NumericUpDown();
            tab_DIDO = new TabPage();
            pnlDO = new Panel();
            doState6 = new Label();
            doState5 = new Label();
            doState2 = new Label();
            doState1 = new Label();
            doState8 = new Label();
            doState7 = new Label();
            doState4 = new Label();
            doState3 = new Label();
            doSwitch6 = new PictureBox();
            doSwitch5 = new PictureBox();
            doSwitch2 = new PictureBox();
            doSwitch1 = new PictureBox();
            doSwitch8 = new PictureBox();
            doSwitch7 = new PictureBox();
            doSwitch4 = new PictureBox();
            doSwitch3 = new PictureBox();
            doName6 = new Label();
            doName5 = new Label();
            doName2 = new Label();
            doName1 = new Label();
            doName8 = new Label();
            doName7 = new Label();
            doName4 = new Label();
            doName3 = new Label();
            doLink6 = new PictureBox();
            doLink5 = new PictureBox();
            doLink8 = new PictureBox();
            doLink7 = new PictureBox();
            doLink2 = new PictureBox();
            doLink4 = new PictureBox();
            doLink1 = new PictureBox();
            doLink3 = new PictureBox();
            lblDOTitle = new Label();
            pnlDI = new Panel();
            diName8 = new Label();
            diName7 = new Label();
            diName6 = new Label();
            diName5 = new Label();
            diName4 = new Label();
            diName3 = new Label();
            diName2 = new Label();
            diName1 = new Label();
            diIcon8 = new PictureBox();
            diIcon7 = new PictureBox();
            diIcon6 = new PictureBox();
            diIcon5 = new PictureBox();
            diIcon4 = new PictureBox();
            diIcon3 = new PictureBox();
            diIcon2 = new PictureBox();
            diIcon1 = new PictureBox();
            lblDITitle = new Label();
            tab_OpenCV = new TabPage();
            chkADS = new CheckBox();
            lblMargin = new Label();
            lblGesture = new Label();
            btnStop = new Button();
            btnStart = new Button();
            cboCamera = new ComboBox();
            picView = new PictureBox();
            tabControl.SuspendLayout();
            tab_AI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridAI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_blue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_red).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_green).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHigh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLow).BeginInit();
            tab_DIDO.SuspendLayout();
            pnlDO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)doSwitch6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doLink3).BeginInit();
            pnlDI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)diIcon8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)diIcon1).BeginInit();
            tab_OpenCV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picView).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tab_AI);
            tabControl.Controls.Add(tab_DIDO);
            tabControl.Controls.Add(tab_OpenCV);
            tabControl.Location = new Point(2, 0);
            tabControl.Margin = new Padding(5);
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1262, 758);
            tabControl.TabIndex = 0;
            // 
            // tab_AI
            // 
            tab_AI.Controls.Add(gridAI);
            tab_AI.Controls.Add(lab_circle_blue);
            tab_AI.Controls.Add(pic_circle_blue);
            tab_AI.Controls.Add(lab_circle_red);
            tab_AI.Controls.Add(pic_circle_red);
            tab_AI.Controls.Add(lab_circle_green);
            tab_AI.Controls.Add(pic_circle_green);
            tab_AI.Controls.Add(btnUnselect);
            tab_AI.Controls.Add(btnSelectAll);
            tab_AI.Controls.Add(btnApplySelected);
            tab_AI.Controls.Add(lab_high);
            tab_AI.Controls.Add(lab_low);
            tab_AI.Controls.Add(numHigh);
            tab_AI.Controls.Add(numLow);
            tab_AI.Location = new Point(4, 35);
            tab_AI.Margin = new Padding(5);
            tab_AI.Name = "tab_AI";
            tab_AI.Padding = new Padding(5);
            tab_AI.Size = new Size(1254, 719);
            tab_AI.TabIndex = 0;
            tab_AI.Text = "AI";
            tab_AI.UseVisualStyleBackColor = true;
            // 
            // gridAI
            // 
            gridAI.AllowUserToAddRows = false;
            gridAI.AllowUserToDeleteRows = false;
            gridAI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridAI.Columns.AddRange(new DataGridViewColumn[] { Sel, Group, Senser, Lable, Current, Unit, Low, High, Status });
            gridAI.Location = new Point(3, 103);
            gridAI.Name = "gridAI";
            gridAI.RowHeadersVisible = false;
            gridAI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridAI.Size = new Size(1248, 613);
            gridAI.TabIndex = 13;
            // 
            // Sel
            // 
            Sel.HeaderText = "Sel";
            Sel.Name = "Sel";
            Sel.Width = 54;
            // 
            // Group
            // 
            Group.HeaderText = "Group";
            Group.Name = "Group";
            Group.Width = 320;
            // 
            // Senser
            // 
            Senser.HeaderText = "Senser";
            Senser.Name = "Senser";
            Senser.Width = 220;
            // 
            // Lable
            // 
            Lable.HeaderText = "Lable";
            Lable.Name = "Lable";
            Lable.Width = 120;
            // 
            // Current
            // 
            Current.HeaderText = "Current";
            Current.Name = "Current";
            Current.ReadOnly = true;
            Current.Width = 110;
            // 
            // Unit
            // 
            Unit.HeaderText = "Unit";
            Unit.Name = "Unit";
            Unit.ReadOnly = true;
            Unit.Width = 70;
            // 
            // Low
            // 
            Low.HeaderText = "Low";
            Low.Name = "Low";
            Low.Width = 110;
            // 
            // High
            // 
            High.HeaderText = "High";
            High.Name = "High";
            High.Width = 110;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 140;
            // 
            // lab_circle_blue
            // 
            lab_circle_blue.AutoSize = true;
            lab_circle_blue.Location = new Point(493, 74);
            lab_circle_blue.Name = "lab_circle_blue";
            lab_circle_blue.Size = new Size(134, 26);
            lab_circle_blue.TabIndex = 12;
            lab_circle_blue.Text = "LOW(under)";
            // 
            // pic_circle_blue
            // 
            pic_circle_blue.Image = Properties.Resources.circle_blue_24;
            pic_circle_blue.Location = new Point(463, 76);
            pic_circle_blue.Name = "pic_circle_blue";
            pic_circle_blue.Size = new Size(24, 24);
            pic_circle_blue.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_circle_blue.TabIndex = 11;
            pic_circle_blue.TabStop = false;
            // 
            // lab_circle_red
            // 
            lab_circle_red.AutoSize = true;
            lab_circle_red.Location = new Point(260, 74);
            lab_circle_red.Name = "lab_circle_red";
            lab_circle_red.Size = new Size(124, 26);
            lab_circle_red.TabIndex = 10;
            lab_circle_red.Text = "HIGH(over)";
            // 
            // pic_circle_red
            // 
            pic_circle_red.Image = Properties.Resources.circle_red_24;
            pic_circle_red.Location = new Point(230, 74);
            pic_circle_red.Name = "pic_circle_red";
            pic_circle_red.Size = new Size(24, 24);
            pic_circle_red.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_circle_red.TabIndex = 9;
            pic_circle_red.TabStop = false;
            // 
            // lab_circle_green
            // 
            lab_circle_green.AutoSize = true;
            lab_circle_green.Location = new Point(38, 74);
            lab_circle_green.Name = "lab_circle_green";
            lab_circle_green.Size = new Size(116, 26);
            lab_circle_green.TabIndex = 8;
            lab_circle_green.Text = "OK(within)";
            // 
            // pic_circle_green
            // 
            pic_circle_green.Image = Properties.Resources.circle_green_24;
            pic_circle_green.Location = new Point(8, 74);
            pic_circle_green.Name = "pic_circle_green";
            pic_circle_green.Size = new Size(24, 24);
            pic_circle_green.SizeMode = PictureBoxSizeMode.AutoSize;
            pic_circle_green.TabIndex = 7;
            pic_circle_green.TabStop = false;
            // 
            // btnUnselect
            // 
            btnUnselect.Location = new Point(596, 34);
            btnUnselect.Name = "btnUnselect";
            btnUnselect.Size = new Size(113, 34);
            btnUnselect.TabIndex = 6;
            btnUnselect.Text = "Unselect";
            btnUnselect.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Location = new Point(474, 34);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(116, 34);
            btnSelectAll.TabIndex = 5;
            btnSelectAll.Text = "Select All";
            btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnApplySelected
            // 
            btnApplySelected.Location = new Point(260, 34);
            btnApplySelected.Name = "btnApplySelected";
            btnApplySelected.Size = new Size(208, 34);
            btnApplySelected.TabIndex = 4;
            btnApplySelected.Text = "Apply to Selected";
            btnApplySelected.UseVisualStyleBackColor = true;
            // 
            // lab_high
            // 
            lab_high.AutoSize = true;
            lab_high.Location = new Point(134, 5);
            lab_high.Name = "lab_high";
            lab_high.Size = new Size(59, 26);
            lab_high.TabIndex = 3;
            lab_high.Text = "High";
            // 
            // lab_low
            // 
            lab_low.AutoSize = true;
            lab_low.Location = new Point(8, 5);
            lab_low.Name = "lab_low";
            lab_low.Size = new Size(52, 26);
            lab_low.TabIndex = 2;
            lab_low.Text = "Low";
            // 
            // numHigh
            // 
            numHigh.Location = new Point(134, 34);
            numHigh.Name = "numHigh";
            numHigh.Size = new Size(120, 34);
            numHigh.TabIndex = 1;
            // 
            // numLow
            // 
            numLow.Location = new Point(8, 34);
            numLow.Name = "numLow";
            numLow.Size = new Size(120, 34);
            numLow.TabIndex = 0;
            // 
            // tab_DIDO
            // 
            tab_DIDO.Controls.Add(pnlDO);
            tab_DIDO.Controls.Add(pnlDI);
            tab_DIDO.Location = new Point(4, 35);
            tab_DIDO.Margin = new Padding(5);
            tab_DIDO.Name = "tab_DIDO";
            tab_DIDO.Padding = new Padding(5);
            tab_DIDO.Size = new Size(1254, 719);
            tab_DIDO.TabIndex = 1;
            tab_DIDO.Text = "DI/DO";
            tab_DIDO.UseVisualStyleBackColor = true;
            // 
            // pnlDO
            // 
            pnlDO.BackColor = Color.FromArgb(64, 64, 64);
            pnlDO.Controls.Add(doState6);
            pnlDO.Controls.Add(doState5);
            pnlDO.Controls.Add(doState2);
            pnlDO.Controls.Add(doState1);
            pnlDO.Controls.Add(doState8);
            pnlDO.Controls.Add(doState7);
            pnlDO.Controls.Add(doState4);
            pnlDO.Controls.Add(doState3);
            pnlDO.Controls.Add(doSwitch6);
            pnlDO.Controls.Add(doSwitch5);
            pnlDO.Controls.Add(doSwitch2);
            pnlDO.Controls.Add(doSwitch1);
            pnlDO.Controls.Add(doSwitch8);
            pnlDO.Controls.Add(doSwitch7);
            pnlDO.Controls.Add(doSwitch4);
            pnlDO.Controls.Add(doSwitch3);
            pnlDO.Controls.Add(doName6);
            pnlDO.Controls.Add(doName5);
            pnlDO.Controls.Add(doName2);
            pnlDO.Controls.Add(doName1);
            pnlDO.Controls.Add(doName8);
            pnlDO.Controls.Add(doName7);
            pnlDO.Controls.Add(doName4);
            pnlDO.Controls.Add(doName3);
            pnlDO.Controls.Add(doLink6);
            pnlDO.Controls.Add(doLink5);
            pnlDO.Controls.Add(doLink8);
            pnlDO.Controls.Add(doLink7);
            pnlDO.Controls.Add(doLink2);
            pnlDO.Controls.Add(doLink4);
            pnlDO.Controls.Add(doLink1);
            pnlDO.Controls.Add(doLink3);
            pnlDO.Controls.Add(lblDOTitle);
            pnlDO.Dock = DockStyle.Fill;
            pnlDO.Location = new Point(645, 5);
            pnlDO.Name = "pnlDO";
            pnlDO.Size = new Size(604, 709);
            pnlDO.TabIndex = 1;
            // 
            // doState6
            // 
            doState6.AutoSize = true;
            doState6.ForeColor = SystemColors.ButtonHighlight;
            doState6.Location = new Point(536, 400);
            doState6.Name = "doState6";
            doState6.Size = new Size(62, 26);
            doState6.TabIndex = 4;
            doState6.Text = "close";
            // 
            // doState5
            // 
            doState5.AutoSize = true;
            doState5.ForeColor = SystemColors.ButtonHighlight;
            doState5.Location = new Point(224, 400);
            doState5.Name = "doState5";
            doState5.Size = new Size(62, 26);
            doState5.TabIndex = 4;
            doState5.Text = "close";
            // 
            // doState2
            // 
            doState2.AutoSize = true;
            doState2.ForeColor = SystemColors.ButtonHighlight;
            doState2.Location = new Point(536, 84);
            doState2.Name = "doState2";
            doState2.Size = new Size(62, 26);
            doState2.TabIndex = 4;
            doState2.Text = "close";
            // 
            // doState1
            // 
            doState1.AutoSize = true;
            doState1.ForeColor = SystemColors.ButtonHighlight;
            doState1.Location = new Point(224, 84);
            doState1.Name = "doState1";
            doState1.Size = new Size(62, 26);
            doState1.TabIndex = 4;
            doState1.Text = "close";
            // 
            // doState8
            // 
            doState8.AutoSize = true;
            doState8.ForeColor = SystemColors.ButtonHighlight;
            doState8.Location = new Point(536, 549);
            doState8.Name = "doState8";
            doState8.Size = new Size(62, 26);
            doState8.TabIndex = 4;
            doState8.Text = "close";
            // 
            // doState7
            // 
            doState7.AutoSize = true;
            doState7.ForeColor = SystemColors.ButtonHighlight;
            doState7.Location = new Point(224, 549);
            doState7.Name = "doState7";
            doState7.Size = new Size(62, 26);
            doState7.TabIndex = 4;
            doState7.Text = "close";
            // 
            // doState4
            // 
            doState4.AutoSize = true;
            doState4.ForeColor = SystemColors.ButtonHighlight;
            doState4.Location = new Point(536, 245);
            doState4.Name = "doState4";
            doState4.Size = new Size(62, 26);
            doState4.TabIndex = 4;
            doState4.Text = "close";
            // 
            // doState3
            // 
            doState3.AutoSize = true;
            doState3.ForeColor = SystemColors.ButtonHighlight;
            doState3.Location = new Point(224, 245);
            doState3.Name = "doState3";
            doState3.Size = new Size(62, 26);
            doState3.TabIndex = 4;
            doState3.Text = "close";
            // 
            // doSwitch6
            // 
            doSwitch6.Cursor = Cursors.Hand;
            doSwitch6.Image = Properties.Resources.switch_off;
            doSwitch6.Location = new Point(464, 397);
            doSwitch6.Name = "doSwitch6";
            doSwitch6.Size = new Size(66, 33);
            doSwitch6.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch6.TabIndex = 3;
            doSwitch6.TabStop = false;
            // 
            // doSwitch5
            // 
            doSwitch5.Cursor = Cursors.Hand;
            doSwitch5.Image = Properties.Resources.switch_off;
            doSwitch5.Location = new Point(152, 397);
            doSwitch5.Name = "doSwitch5";
            doSwitch5.Size = new Size(66, 33);
            doSwitch5.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch5.TabIndex = 3;
            doSwitch5.TabStop = false;
            // 
            // doSwitch2
            // 
            doSwitch2.Cursor = Cursors.Hand;
            doSwitch2.Image = Properties.Resources.switch_off;
            doSwitch2.Location = new Point(464, 81);
            doSwitch2.Name = "doSwitch2";
            doSwitch2.Size = new Size(66, 33);
            doSwitch2.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch2.TabIndex = 3;
            doSwitch2.TabStop = false;
            // 
            // doSwitch1
            // 
            doSwitch1.Cursor = Cursors.Hand;
            doSwitch1.Image = Properties.Resources.switch_off;
            doSwitch1.Location = new Point(152, 81);
            doSwitch1.Name = "doSwitch1";
            doSwitch1.Size = new Size(66, 33);
            doSwitch1.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch1.TabIndex = 3;
            doSwitch1.TabStop = false;
            // 
            // doSwitch8
            // 
            doSwitch8.Cursor = Cursors.Hand;
            doSwitch8.Image = Properties.Resources.switch_off;
            doSwitch8.Location = new Point(464, 546);
            doSwitch8.Name = "doSwitch8";
            doSwitch8.Size = new Size(66, 33);
            doSwitch8.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch8.TabIndex = 3;
            doSwitch8.TabStop = false;
            // 
            // doSwitch7
            // 
            doSwitch7.Cursor = Cursors.Hand;
            doSwitch7.Image = Properties.Resources.switch_off;
            doSwitch7.Location = new Point(152, 546);
            doSwitch7.Name = "doSwitch7";
            doSwitch7.Size = new Size(66, 33);
            doSwitch7.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch7.TabIndex = 3;
            doSwitch7.TabStop = false;
            // 
            // doSwitch4
            // 
            doSwitch4.Cursor = Cursors.Hand;
            doSwitch4.Image = Properties.Resources.switch_off;
            doSwitch4.Location = new Point(464, 242);
            doSwitch4.Name = "doSwitch4";
            doSwitch4.Size = new Size(66, 33);
            doSwitch4.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch4.TabIndex = 3;
            doSwitch4.TabStop = false;
            // 
            // doSwitch3
            // 
            doSwitch3.Cursor = Cursors.Hand;
            doSwitch3.Image = Properties.Resources.switch_off;
            doSwitch3.Location = new Point(152, 242);
            doSwitch3.Name = "doSwitch3";
            doSwitch3.Size = new Size(66, 33);
            doSwitch3.SizeMode = PictureBoxSizeMode.StretchImage;
            doSwitch3.TabIndex = 3;
            doSwitch3.TabStop = false;
            // 
            // doName6
            // 
            doName6.AutoSize = true;
            doName6.ForeColor = SystemColors.ButtonHighlight;
            doName6.Location = new Point(364, 400);
            doName6.Name = "doName6";
            doName6.Size = new Size(105, 26);
            doName6.TabIndex = 2;
            doName6.Text = "Output_6";
            // 
            // doName5
            // 
            doName5.AutoSize = true;
            doName5.ForeColor = SystemColors.ButtonHighlight;
            doName5.Location = new Point(52, 400);
            doName5.Name = "doName5";
            doName5.Size = new Size(105, 26);
            doName5.TabIndex = 2;
            doName5.Text = "Output_5";
            // 
            // doName2
            // 
            doName2.AutoSize = true;
            doName2.ForeColor = SystemColors.ButtonHighlight;
            doName2.Location = new Point(364, 84);
            doName2.Name = "doName2";
            doName2.Size = new Size(105, 26);
            doName2.TabIndex = 2;
            doName2.Text = "Output_2";
            // 
            // doName1
            // 
            doName1.AutoSize = true;
            doName1.ForeColor = SystemColors.ButtonHighlight;
            doName1.Location = new Point(52, 84);
            doName1.Name = "doName1";
            doName1.Size = new Size(105, 26);
            doName1.TabIndex = 2;
            doName1.Text = "Output_1";
            // 
            // doName8
            // 
            doName8.AutoSize = true;
            doName8.ForeColor = SystemColors.ButtonHighlight;
            doName8.Location = new Point(364, 549);
            doName8.Name = "doName8";
            doName8.Size = new Size(105, 26);
            doName8.TabIndex = 2;
            doName8.Text = "Output_8";
            // 
            // doName7
            // 
            doName7.AutoSize = true;
            doName7.ForeColor = SystemColors.ButtonHighlight;
            doName7.Location = new Point(52, 549);
            doName7.Name = "doName7";
            doName7.Size = new Size(105, 26);
            doName7.TabIndex = 2;
            doName7.Text = "Output_7";
            // 
            // doName4
            // 
            doName4.AutoSize = true;
            doName4.ForeColor = SystemColors.ButtonHighlight;
            doName4.Location = new Point(364, 245);
            doName4.Name = "doName4";
            doName4.Size = new Size(105, 26);
            doName4.TabIndex = 2;
            doName4.Text = "Output_4";
            // 
            // doName3
            // 
            doName3.AutoSize = true;
            doName3.ForeColor = SystemColors.ButtonHighlight;
            doName3.Location = new Point(52, 245);
            doName3.Name = "doName3";
            doName3.Size = new Size(105, 26);
            doName3.TabIndex = 2;
            doName3.Text = "Output_3";
            // 
            // doLink6
            // 
            doLink6.Image = Properties.Resources.DO_connected;
            doLink6.Location = new Point(312, 381);
            doLink6.Name = "doLink6";
            doLink6.Size = new Size(66, 66);
            doLink6.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink6.TabIndex = 1;
            doLink6.TabStop = false;
            // 
            // doLink5
            // 
            doLink5.Image = Properties.Resources.DO_connected;
            doLink5.Location = new Point(0, 381);
            doLink5.Name = "doLink5";
            doLink5.Size = new Size(66, 66);
            doLink5.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink5.TabIndex = 1;
            doLink5.TabStop = false;
            // 
            // doLink8
            // 
            doLink8.Image = Properties.Resources.DO_connected;
            doLink8.Location = new Point(312, 530);
            doLink8.Name = "doLink8";
            doLink8.Size = new Size(66, 66);
            doLink8.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink8.TabIndex = 1;
            doLink8.TabStop = false;
            // 
            // doLink7
            // 
            doLink7.Image = Properties.Resources.DO_connected;
            doLink7.Location = new Point(0, 530);
            doLink7.Name = "doLink7";
            doLink7.Size = new Size(66, 66);
            doLink7.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink7.TabIndex = 1;
            doLink7.TabStop = false;
            // 
            // doLink2
            // 
            doLink2.Image = Properties.Resources.DO_connected;
            doLink2.Location = new Point(312, 65);
            doLink2.Name = "doLink2";
            doLink2.Size = new Size(66, 66);
            doLink2.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink2.TabIndex = 1;
            doLink2.TabStop = false;
            // 
            // doLink4
            // 
            doLink4.Image = Properties.Resources.DO_connected;
            doLink4.Location = new Point(312, 226);
            doLink4.Name = "doLink4";
            doLink4.Size = new Size(66, 66);
            doLink4.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink4.TabIndex = 1;
            doLink4.TabStop = false;
            // 
            // doLink1
            // 
            doLink1.Image = Properties.Resources.DO_connected;
            doLink1.Location = new Point(0, 65);
            doLink1.Name = "doLink1";
            doLink1.Size = new Size(66, 66);
            doLink1.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink1.TabIndex = 1;
            doLink1.TabStop = false;
            // 
            // doLink3
            // 
            doLink3.Image = Properties.Resources.DO_connected;
            doLink3.Location = new Point(0, 226);
            doLink3.Name = "doLink3";
            doLink3.Size = new Size(66, 66);
            doLink3.SizeMode = PictureBoxSizeMode.StretchImage;
            doLink3.TabIndex = 1;
            doLink3.TabStop = false;
            // 
            // lblDOTitle
            // 
            lblDOTitle.AutoSize = true;
            lblDOTitle.ForeColor = SystemColors.ButtonHighlight;
            lblDOTitle.Location = new Point(6, 9);
            lblDOTitle.Name = "lblDOTitle";
            lblDOTitle.Size = new Size(389, 26);
            lblDOTitle.TabIndex = 0;
            lblDOTitle.Text = "Digital Outputs (DO) close=connected";
            // 
            // pnlDI
            // 
            pnlDI.BackColor = Color.FromArgb(64, 64, 64);
            pnlDI.Controls.Add(diName8);
            pnlDI.Controls.Add(diName7);
            pnlDI.Controls.Add(diName6);
            pnlDI.Controls.Add(diName5);
            pnlDI.Controls.Add(diName4);
            pnlDI.Controls.Add(diName3);
            pnlDI.Controls.Add(diName2);
            pnlDI.Controls.Add(diName1);
            pnlDI.Controls.Add(diIcon8);
            pnlDI.Controls.Add(diIcon7);
            pnlDI.Controls.Add(diIcon6);
            pnlDI.Controls.Add(diIcon5);
            pnlDI.Controls.Add(diIcon4);
            pnlDI.Controls.Add(diIcon3);
            pnlDI.Controls.Add(diIcon2);
            pnlDI.Controls.Add(diIcon1);
            pnlDI.Controls.Add(lblDITitle);
            pnlDI.Dock = DockStyle.Left;
            pnlDI.Location = new Point(5, 5);
            pnlDI.Name = "pnlDI";
            pnlDI.Size = new Size(640, 709);
            pnlDI.TabIndex = 0;
            // 
            // diName8
            // 
            diName8.AutoSize = true;
            diName8.ForeColor = SystemColors.ButtonHighlight;
            diName8.Location = new Point(400, 562);
            diName8.Name = "diName8";
            diName8.Size = new Size(86, 26);
            diName8.TabIndex = 2;
            diName8.Text = "Input_8";
            // 
            // diName7
            // 
            diName7.AutoSize = true;
            diName7.ForeColor = SystemColors.ButtonHighlight;
            diName7.Location = new Point(97, 562);
            diName7.Name = "diName7";
            diName7.Size = new Size(86, 26);
            diName7.TabIndex = 2;
            diName7.Text = "Input_7";
            // 
            // diName6
            // 
            diName6.AutoSize = true;
            diName6.ForeColor = SystemColors.ButtonHighlight;
            diName6.Location = new Point(400, 400);
            diName6.Name = "diName6";
            diName6.Size = new Size(86, 26);
            diName6.TabIndex = 2;
            diName6.Text = "Input_6";
            // 
            // diName5
            // 
            diName5.AutoSize = true;
            diName5.ForeColor = SystemColors.ButtonHighlight;
            diName5.Location = new Point(97, 400);
            diName5.Name = "diName5";
            diName5.Size = new Size(86, 26);
            diName5.TabIndex = 2;
            diName5.Text = "Input_5";
            // 
            // diName4
            // 
            diName4.AutoSize = true;
            diName4.ForeColor = SystemColors.ButtonHighlight;
            diName4.Location = new Point(400, 245);
            diName4.Name = "diName4";
            diName4.Size = new Size(86, 26);
            diName4.TabIndex = 2;
            diName4.Text = "Input_4";
            // 
            // diName3
            // 
            diName3.AutoSize = true;
            diName3.ForeColor = SystemColors.ButtonHighlight;
            diName3.Location = new Point(97, 245);
            diName3.Name = "diName3";
            diName3.Size = new Size(86, 26);
            diName3.TabIndex = 2;
            diName3.Text = "Input_3";
            // 
            // diName2
            // 
            diName2.AutoSize = true;
            diName2.ForeColor = SystemColors.ButtonHighlight;
            diName2.Location = new Point(400, 83);
            diName2.Name = "diName2";
            diName2.Size = new Size(86, 26);
            diName2.TabIndex = 2;
            diName2.Text = "Input_2";
            // 
            // diName1
            // 
            diName1.AutoSize = true;
            diName1.ForeColor = SystemColors.ButtonHighlight;
            diName1.Location = new Point(97, 83);
            diName1.Name = "diName1";
            diName1.Size = new Size(86, 26);
            diName1.TabIndex = 2;
            diName1.Text = "Input_1";
            // 
            // diIcon8
            // 
            diIcon8.Image = Properties.Resources.di_off;
            diIcon8.Location = new Point(306, 530);
            diIcon8.Name = "diIcon8";
            diIcon8.Size = new Size(88, 88);
            diIcon8.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon8.TabIndex = 1;
            diIcon8.TabStop = false;
            // 
            // diIcon7
            // 
            diIcon7.Image = Properties.Resources.di_off;
            diIcon7.Location = new Point(3, 530);
            diIcon7.Name = "diIcon7";
            diIcon7.Size = new Size(88, 88);
            diIcon7.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon7.TabIndex = 1;
            diIcon7.TabStop = false;
            // 
            // diIcon6
            // 
            diIcon6.Image = Properties.Resources.di_off;
            diIcon6.Location = new Point(306, 381);
            diIcon6.Name = "diIcon6";
            diIcon6.Size = new Size(88, 88);
            diIcon6.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon6.TabIndex = 1;
            diIcon6.TabStop = false;
            // 
            // diIcon5
            // 
            diIcon5.Image = Properties.Resources.di_off;
            diIcon5.Location = new Point(3, 381);
            diIcon5.Name = "diIcon5";
            diIcon5.Size = new Size(88, 88);
            diIcon5.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon5.TabIndex = 1;
            diIcon5.TabStop = false;
            // 
            // diIcon4
            // 
            diIcon4.Image = Properties.Resources.di_off;
            diIcon4.Location = new Point(306, 226);
            diIcon4.Name = "diIcon4";
            diIcon4.Size = new Size(88, 88);
            diIcon4.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon4.TabIndex = 1;
            diIcon4.TabStop = false;
            // 
            // diIcon3
            // 
            diIcon3.Image = Properties.Resources.di_off;
            diIcon3.Location = new Point(3, 226);
            diIcon3.Name = "diIcon3";
            diIcon3.Size = new Size(88, 88);
            diIcon3.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon3.TabIndex = 1;
            diIcon3.TabStop = false;
            // 
            // diIcon2
            // 
            diIcon2.Image = Properties.Resources.di_off;
            diIcon2.Location = new Point(306, 65);
            diIcon2.Name = "diIcon2";
            diIcon2.Size = new Size(88, 88);
            diIcon2.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon2.TabIndex = 1;
            diIcon2.TabStop = false;
            // 
            // diIcon1
            // 
            diIcon1.Image = Properties.Resources.di_off;
            diIcon1.Location = new Point(3, 65);
            diIcon1.Name = "diIcon1";
            diIcon1.Size = new Size(88, 88);
            diIcon1.SizeMode = PictureBoxSizeMode.StretchImage;
            diIcon1.TabIndex = 1;
            diIcon1.TabStop = false;
            // 
            // lblDITitle
            // 
            lblDITitle.AutoSize = true;
            lblDITitle.ForeColor = SystemColors.ButtonHighlight;
            lblDITitle.Location = new Point(3, 9);
            lblDITitle.Name = "lblDITitle";
            lblDITitle.Size = new Size(185, 26);
            lblDITitle.TabIndex = 0;
            lblDITitle.Text = "Digital Inputs (DI)";
            // 
            // tab_OpenCV
            // 
            tab_OpenCV.Controls.Add(chkADS);
            tab_OpenCV.Controls.Add(lblMargin);
            tab_OpenCV.Controls.Add(lblGesture);
            tab_OpenCV.Controls.Add(btnStop);
            tab_OpenCV.Controls.Add(btnStart);
            tab_OpenCV.Controls.Add(cboCamera);
            tab_OpenCV.Controls.Add(picView);
            tab_OpenCV.Location = new Point(4, 35);
            tab_OpenCV.Margin = new Padding(5);
            tab_OpenCV.Name = "tab_OpenCV";
            tab_OpenCV.Padding = new Padding(5);
            tab_OpenCV.Size = new Size(1254, 719);
            tab_OpenCV.TabIndex = 2;
            tab_OpenCV.Text = "OpenCV";
            tab_OpenCV.UseVisualStyleBackColor = true;
            // 
            // chkADS
            // 
            chkADS.AutoSize = true;
            chkADS.Checked = true;
            chkADS.CheckState = CheckState.Checked;
            chkADS.Location = new Point(439, 69);
            chkADS.Name = "chkADS";
            chkADS.Size = new Size(142, 30);
            chkADS.TabIndex = 6;
            chkADS.Text = "發佈到 ADS";
            chkADS.UseVisualStyleBackColor = true;
            // 
            // lblMargin
            // 
            lblMargin.AutoSize = true;
            lblMargin.Location = new Point(619, 69);
            lblMargin.Name = "lblMargin";
            lblMargin.Size = new Size(116, 26);
            lblMargin.TabIndex = 5;
            lblMargin.Text = "Margin：0";
            // 
            // lblGesture
            // 
            lblGesture.AutoSize = true;
            lblGesture.Location = new Point(619, 18);
            lblGesture.Name = "lblGesture";
            lblGesture.Size = new Size(177, 26);
            lblGesture.TabIndex = 4;
            lblGesture.Text = "手勢：--（未定）";
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(64, 64, 64);
            btnStop.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnStop.ForeColor = SystemColors.ButtonFace;
            btnStop.Location = new Point(225, 54);
            btnStop.Margin = new Padding(5);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(206, 59);
            btnStop.TabIndex = 3;
            btnStop.Text = "停止";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(64, 64, 64);
            btnStart.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnStart.ForeColor = SystemColors.ButtonFace;
            btnStart.Location = new Point(10, 54);
            btnStart.Margin = new Padding(5);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(205, 59);
            btnStart.TabIndex = 2;
            btnStart.Text = "開始";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // cboCamera
            // 
            cboCamera.BackColor = Color.FromArgb(64, 64, 64);
            cboCamera.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCamera.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            cboCamera.ForeColor = SystemColors.Window;
            cboCamera.FormattingEnabled = true;
            cboCamera.Location = new Point(10, 10);
            cboCamera.Margin = new Padding(5);
            cboCamera.Name = "cboCamera";
            cboCamera.Size = new Size(205, 34);
            cboCamera.TabIndex = 1;
            // 
            // picView
            // 
            picView.Location = new Point(-4, 123);
            picView.Margin = new Padding(5);
            picView.Name = "picView";
            picView.Size = new Size(800, 600);
            picView.TabIndex = 0;
            picView.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1264, 761);
            Controls.Add(tabControl);
            Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "Form1";
            Text = "TwinDEMO V0.9";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            tabControl.ResumeLayout(false);
            tab_AI.ResumeLayout(false);
            tab_AI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridAI).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_blue).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_red).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_circle_green).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHigh).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLow).EndInit();
            tab_DIDO.ResumeLayout(false);
            pnlDO.ResumeLayout(false);
            pnlDO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)doSwitch6).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch5).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch2).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch1).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch8).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch7).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch4).EndInit();
            ((System.ComponentModel.ISupportInitialize)doSwitch3).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink6).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink5).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink8).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink7).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink2).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink4).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink1).EndInit();
            ((System.ComponentModel.ISupportInitialize)doLink3).EndInit();
            pnlDI.ResumeLayout(false);
            pnlDI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)diIcon8).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon7).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon6).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon5).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon4).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon3).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon2).EndInit();
            ((System.ComponentModel.ISupportInitialize)diIcon1).EndInit();
            tab_OpenCV.ResumeLayout(false);
            tab_OpenCV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tab_AI;
        private TabPage tab_DIDO;
        private TabPage tab_OpenCV;
        private PictureBox picView;
        private ComboBox cboCamera;
        private Button btnStop;
        private Button btnStart;
        private Label lblMargin;
        private Label lblGesture;
        private CheckBox chkADS;
        private Button btnApplySelected;
        private Label lab_high;
        private Label lab_low;
        private NumericUpDown numHigh;
        private NumericUpDown numLow;
        private Button btnSelectAll;
        private Button btnUnselect;
        private PictureBox pic_circle_green;
        private Label lab_circle_red;
        private PictureBox pic_circle_red;
        private Label lab_circle_green;
        private DataGridView gridAI;
        private Label lab_circle_blue;
        private PictureBox pic_circle_blue;
        private DataGridViewCheckBoxColumn Sel;
        private DataGridViewTextBoxColumn Group;
        private DataGridViewTextBoxColumn Senser;
        private DataGridViewTextBoxColumn Lable;
        private DataGridViewTextBoxColumn Current;
        private DataGridViewTextBoxColumn Unit;
        private DataGridViewTextBoxColumn Low;
        private DataGridViewTextBoxColumn High;
        private DataGridViewTextBoxColumn Status;
        private Panel pnlDO;
        private Panel pnlDI;
        private PictureBox diIcon1;
        private Label lblDITitle;
        private PictureBox diIcon7;
        private PictureBox diIcon5;
        private PictureBox diIcon3;
        private PictureBox diIcon8;
        private PictureBox diIcon6;
        private PictureBox diIcon4;
        private PictureBox diIcon2;
        private Label diName8;
        private Label diName7;
        private Label diName6;
        private Label diName5;
        private Label diName4;
        private Label diName3;
        private Label diName2;
        private Label diName1;
        private PictureBox doLink3;
        private Label lblDOTitle;
        private PictureBox doSwitch3;
        private Label doName3;
        private Label doState5;
        private Label doState1;
        private Label doState7;
        private Label doState3;
        private PictureBox doSwitch5;
        private PictureBox doSwitch1;
        private PictureBox doSwitch7;
        private Label doName5;
        private Label doName1;
        private Label doName7;
        private PictureBox doLink5;
        private PictureBox doLink7;
        private PictureBox doLink1;
        private Label doState6;
        private Label doState2;
        private Label doState8;
        private Label doState4;
        private PictureBox doSwitch6;
        private PictureBox doSwitch2;
        private PictureBox doSwitch8;
        private PictureBox doSwitch4;
        private Label doName6;
        private Label doName2;
        private Label doName8;
        private Label doName4;
        private PictureBox doLink6;
        private PictureBox doLink8;
        private PictureBox doLink2;
        private PictureBox doLink4;
    }
}
