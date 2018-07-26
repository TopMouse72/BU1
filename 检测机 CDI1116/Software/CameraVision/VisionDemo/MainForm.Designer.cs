namespace CDIVisionControl
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CamDisplayMiddleCell = new Colibri.CommonModule.ToolBox.CameraDispControl();
            this.CamDisplayLeftCell = new Colibri.CommonModule.ToolBox.CameraDispControl();
            this.CamDisplayTest = new Colibri.CommonModule.ToolBox.CameraDispControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CamDisplayRightCell = new Colibri.CommonModule.ToolBox.CameraDispControl();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.listBoxMeasResult = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonConnectCDI = new System.Windows.Forms.Button();
            this.optionBox1 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBox2 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBox3 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBox4 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelTrig1 = new System.Windows.Forms.Label();
            this.labelTrig2 = new System.Windows.Forms.Label();
            this.labelTrig3 = new System.Windows.Forms.Label();
            this.labelTrig4 = new System.Windows.Forms.Label();
            this.optionBoxSaveAll = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBoxSaveNG = new Colibri.CommonModule.ToolBox.OptionBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCalibration = new System.Windows.Forms.Button();
            this.selectBoxRealDisp = new Colibri.CommonModule.ToolBox.SelectBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBoxProduct = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.VisionSetting1 = new CDIVisionControl.VisionSetting();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelCamera = new System.Windows.Forms.Panel();
            this.cameraSetting1 = new SVSCamera.CameraSetting();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonZoom25 = new System.Windows.Forms.Button();
            this.buttonZoom50 = new System.Windows.Forms.Button();
            this.buttonZoom200 = new System.Windows.Forms.Button();
            this.buttonZoom100 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CamDisplayMiddleCell
            // 
            this.CamDisplayMiddleCell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamDisplayMiddleCell.AutoScroll = true;
            this.CamDisplayMiddleCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CamDisplayMiddleCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CamDisplayMiddleCell.Location = new System.Drawing.Point(0, 0);
            this.CamDisplayMiddleCell.Name = "CamDisplayMiddleCell";
            this.CamDisplayMiddleCell.Size = new System.Drawing.Size(530, 577);
            this.CamDisplayMiddleCell.TabIndex = 3;
            // 
            // CamDisplayLeftCell
            // 
            this.CamDisplayLeftCell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamDisplayLeftCell.AutoScroll = true;
            this.CamDisplayLeftCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CamDisplayLeftCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CamDisplayLeftCell.Location = new System.Drawing.Point(0, 0);
            this.CamDisplayLeftCell.Name = "CamDisplayLeftCell";
            this.CamDisplayLeftCell.Size = new System.Drawing.Size(530, 577);
            this.CamDisplayLeftCell.TabIndex = 4;
            // 
            // CamDisplayTest
            // 
            this.CamDisplayTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamDisplayTest.AutoScroll = true;
            this.CamDisplayTest.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CamDisplayTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CamDisplayTest.Location = new System.Drawing.Point(0, 0);
            this.CamDisplayTest.Name = "CamDisplayTest";
            this.CamDisplayTest.Size = new System.Drawing.Size(530, 577);
            this.CamDisplayTest.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CamDisplayRightCell
            // 
            this.CamDisplayRightCell.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CamDisplayRightCell.AutoScroll = true;
            this.CamDisplayRightCell.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CamDisplayRightCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CamDisplayRightCell.Location = new System.Drawing.Point(0, 0);
            this.CamDisplayRightCell.Name = "CamDisplayRightCell";
            this.CamDisplayRightCell.Size = new System.Drawing.Size(530, 577);
            this.CamDisplayRightCell.TabIndex = 6;
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.Location = new System.Drawing.Point(536, 111);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxStatus.Size = new System.Drawing.Size(261, 75);
            this.textBoxStatus.TabIndex = 7;
            // 
            // listBoxMeasResult
            // 
            this.listBoxMeasResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMeasResult.BackColor = System.Drawing.Color.Bisque;
            this.listBoxMeasResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxMeasResult.FormattingEnabled = true;
            this.listBoxMeasResult.Location = new System.Drawing.Point(800, 14);
            this.listBoxMeasResult.Name = "listBoxMeasResult";
            this.listBoxMeasResult.Size = new System.Drawing.Size(211, 160);
            this.listBoxMeasResult.TabIndex = 8;
            this.listBoxMeasResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBoxMeasResult_DrawItem);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(798, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "测量结果";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(742, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "接收指令";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonConnectCDI
            // 
            this.buttonConnectCDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnectCDI.Location = new System.Drawing.Point(343, 22);
            this.buttonConnectCDI.Name = "buttonConnectCDI";
            this.buttonConnectCDI.Size = new System.Drawing.Size(78, 42);
            this.buttonConnectCDI.TabIndex = 0;
            this.buttonConnectCDI.Text = "连接CDI";
            this.buttonConnectCDI.UseVisualStyleBackColor = true;
            this.buttonConnectCDI.Visible = false;
            this.buttonConnectCDI.Click += new System.EventHandler(this.buttonConnectCDI_Click);
            // 
            // optionBox1
            // 
            this.optionBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox1.AutoSize = true;
            this.optionBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox1.BorderColor = System.Drawing.Color.Black;
            this.optionBox1.CheckColor = System.Drawing.Color.Lime;
            this.optionBox1.Location = new System.Drawing.Point(537, 18);
            this.optionBox1.Name = "optionBox1";
            this.optionBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBox1.Size = new System.Drawing.Size(73, 22);
            this.optionBox1.TabIndex = 15;
            this.optionBox1.Text = "右工位";
            this.optionBox1.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox1.UseVisualStyleBackColor = true;
            this.optionBox1.CheckedChanged += new System.EventHandler(this.optionBoxCamra_Click);
            // 
            // optionBox2
            // 
            this.optionBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox2.AutoSize = true;
            this.optionBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox2.BorderColor = System.Drawing.Color.Black;
            this.optionBox2.CheckColor = System.Drawing.Color.Lime;
            this.optionBox2.Location = new System.Drawing.Point(537, 41);
            this.optionBox2.Name = "optionBox2";
            this.optionBox2.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBox2.Size = new System.Drawing.Size(73, 22);
            this.optionBox2.TabIndex = 15;
            this.optionBox2.Text = "中工位";
            this.optionBox2.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox2.UseVisualStyleBackColor = true;
            this.optionBox2.CheckedChanged += new System.EventHandler(this.optionBoxCamra_Click);
            // 
            // optionBox3
            // 
            this.optionBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox3.AutoSize = true;
            this.optionBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox3.BorderColor = System.Drawing.Color.Black;
            this.optionBox3.CheckColor = System.Drawing.Color.Lime;
            this.optionBox3.Location = new System.Drawing.Point(537, 64);
            this.optionBox3.Name = "optionBox3";
            this.optionBox3.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBox3.Size = new System.Drawing.Size(73, 22);
            this.optionBox3.TabIndex = 15;
            this.optionBox3.Text = "左工位";
            this.optionBox3.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox3.UseVisualStyleBackColor = true;
            this.optionBox3.CheckedChanged += new System.EventHandler(this.optionBoxCamra_Click);
            // 
            // optionBox4
            // 
            this.optionBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox4.AutoSize = true;
            this.optionBox4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox4.BorderColor = System.Drawing.Color.Black;
            this.optionBox4.CheckColor = System.Drawing.Color.Lime;
            this.optionBox4.Checked = true;
            this.optionBox4.GroupChecked = true;
            this.optionBox4.Location = new System.Drawing.Point(538, 87);
            this.optionBox4.Name = "optionBox4";
            this.optionBox4.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBox4.Size = new System.Drawing.Size(61, 22);
            this.optionBox4.TabIndex = 15;
            this.optionBox4.Text = "测试";
            this.optionBox4.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox4.UseVisualStyleBackColor = true;
            this.optionBox4.CheckedChanged += new System.EventHandler(this.optionBoxCamra_Click);
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(1, 1);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(41, 12);
            this.labelPosition.TabIndex = 17;
            this.labelPosition.Text = "(0, 0)";
            // 
            // labelTrig1
            // 
            this.labelTrig1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrig1.AutoSize = true;
            this.labelTrig1.Location = new System.Drawing.Point(536, 120);
            this.labelTrig1.Name = "labelTrig1";
            this.labelTrig1.Size = new System.Drawing.Size(11, 12);
            this.labelTrig1.TabIndex = 18;
            this.labelTrig1.Text = "0";
            this.labelTrig1.Visible = false;
            // 
            // labelTrig2
            // 
            this.labelTrig2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrig2.AutoSize = true;
            this.labelTrig2.Location = new System.Drawing.Point(556, 120);
            this.labelTrig2.Name = "labelTrig2";
            this.labelTrig2.Size = new System.Drawing.Size(11, 12);
            this.labelTrig2.TabIndex = 19;
            this.labelTrig2.Text = "0";
            this.labelTrig2.Visible = false;
            // 
            // labelTrig3
            // 
            this.labelTrig3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrig3.AutoSize = true;
            this.labelTrig3.Location = new System.Drawing.Point(575, 120);
            this.labelTrig3.Name = "labelTrig3";
            this.labelTrig3.Size = new System.Drawing.Size(11, 12);
            this.labelTrig3.TabIndex = 20;
            this.labelTrig3.Text = "0";
            this.labelTrig3.Visible = false;
            // 
            // labelTrig4
            // 
            this.labelTrig4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrig4.AutoSize = true;
            this.labelTrig4.Location = new System.Drawing.Point(594, 120);
            this.labelTrig4.Name = "labelTrig4";
            this.labelTrig4.Size = new System.Drawing.Size(11, 12);
            this.labelTrig4.TabIndex = 21;
            this.labelTrig4.Text = "0";
            this.labelTrig4.Visible = false;
            // 
            // optionBoxSaveAll
            // 
            this.optionBoxSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBoxSaveAll.AutoSize = true;
            this.optionBoxSaveAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxSaveAll.BorderColor = System.Drawing.Color.Black;
            this.optionBoxSaveAll.CheckColor = System.Drawing.Color.Lime;
            this.optionBoxSaveAll.Group = 1;
            this.optionBoxSaveAll.Location = new System.Drawing.Point(16, 63);
            this.optionBoxSaveAll.Name = "optionBoxSaveAll";
            this.optionBoxSaveAll.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBoxSaveAll.Size = new System.Drawing.Size(85, 22);
            this.optionBoxSaveAll.TabIndex = 15;
            this.optionBoxSaveAll.Text = "保存全部";
            this.optionBoxSaveAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.optionBoxSaveAll.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxSaveAll.UseVisualStyleBackColor = true;
            this.optionBoxSaveAll.Click += new System.EventHandler(this.optionBoxSaveAll_Click);
            // 
            // optionBoxSaveNG
            // 
            this.optionBoxSaveNG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBoxSaveNG.AutoSize = true;
            this.optionBoxSaveNG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxSaveNG.BorderColor = System.Drawing.Color.Black;
            this.optionBoxSaveNG.CheckColor = System.Drawing.Color.Lime;
            this.optionBoxSaveNG.Checked = true;
            this.optionBoxSaveNG.Group = 1;
            this.optionBoxSaveNG.GroupChecked = true;
            this.optionBoxSaveNG.Location = new System.Drawing.Point(20, 37);
            this.optionBoxSaveNG.Name = "optionBoxSaveNG";
            this.optionBoxSaveNG.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.optionBoxSaveNG.Size = new System.Drawing.Size(73, 22);
            this.optionBoxSaveNG.TabIndex = 15;
            this.optionBoxSaveNG.Text = "保存NG";
            this.optionBoxSaveNG.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxSaveNG.UseVisualStyleBackColor = true;
            this.optionBoxSaveNG.Click += new System.EventHandler(this.optionBoxSaveNG_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "图片保存";
            // 
            // buttonCalibration
            // 
            this.buttonCalibration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalibration.Location = new System.Drawing.Point(23, 96);
            this.buttonCalibration.Name = "buttonCalibration";
            this.buttonCalibration.Size = new System.Drawing.Size(78, 43);
            this.buttonCalibration.TabIndex = 0;
            this.buttonCalibration.Text = "图像标定";
            this.buttonCalibration.UseVisualStyleBackColor = true;
            this.buttonCalibration.Click += new System.EventHandler(this.buttonCalibration_Click);
            // 
            // selectBoxRealDisp
            // 
            this.selectBoxRealDisp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBoxRealDisp.AutoSize = true;
            this.selectBoxRealDisp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxRealDisp.BorderColor = System.Drawing.Color.Black;
            this.selectBoxRealDisp.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxRealDisp.Location = new System.Drawing.Point(624, 87);
            this.selectBoxRealDisp.Name = "selectBoxRealDisp";
            this.selectBoxRealDisp.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.selectBoxRealDisp.Size = new System.Drawing.Size(109, 22);
            this.selectBoxRealDisp.TabIndex = 16;
            this.selectBoxRealDisp.Text = "相机实时显示";
            this.selectBoxRealDisp.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxRealDisp.UseVisualStyleBackColor = true;
            this.selectBoxRealDisp.CheckedChanged += new System.EventHandler(this.selectBoxRealDisp_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "工位图片 ";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(531, 192);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(481, 383);
            this.tabControl1.TabIndex = 22;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBoxProduct);
            this.tabPage3.Controls.Add(this.optionBoxSaveNG);
            this.tabPage3.Controls.Add(this.buttonCalibration);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.optionBoxSaveAll);
            this.tabPage3.Controls.Add(this.buttonConnectCDI);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(473, 357);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "选项";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listBoxProduct
            // 
            this.listBoxProduct.Dock = System.Windows.Forms.DockStyle.Right;
            this.listBoxProduct.FormattingEnabled = true;
            this.listBoxProduct.ItemHeight = 12;
            this.listBoxProduct.Location = new System.Drawing.Point(214, 0);
            this.listBoxProduct.Name = "listBoxProduct";
            this.listBoxProduct.Size = new System.Drawing.Size(259, 357);
            this.listBoxProduct.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.VisionSetting1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 357);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "图像设置(测试位操作)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // VisionSetting1
            // 
            this.VisionSetting1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.VisionSetting1.Barcode = "TEST";
            this.VisionSetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VisionSetting1.Location = new System.Drawing.Point(3, 3);
            this.VisionSetting1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.VisionSetting1.Name = "VisionSetting1";
            this.VisionSetting1.Size = new System.Drawing.Size(467, 351);
            this.VisionSetting1.TabIndex = 3;
            this.VisionSetting1.CameraLive += new System.EventHandler(this.VisionSetting1_CameraLive);
            this.VisionSetting1.CameraStop += new System.EventHandler(this.VisionSetting1_CameraStop);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelCamera);
            this.tabPage2.Controls.Add(this.cameraSetting1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(473, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相机设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelCamera
            // 
            this.panelCamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCamera.BackColor = System.Drawing.Color.Transparent;
            this.panelCamera.Location = new System.Drawing.Point(169, 3);
            this.panelCamera.Name = "panelCamera";
            this.panelCamera.Size = new System.Drawing.Size(296, 293);
            this.panelCamera.TabIndex = 2;
            // 
            // cameraSetting1
            // 
            this.cameraSetting1.AutoSize = true;
            this.cameraSetting1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cameraSetting1.CameraOprEnable = true;
            this.cameraSetting1.DispPanel = null;
            this.cameraSetting1.Location = new System.Drawing.Point(6, 3);
            this.cameraSetting1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cameraSetting1.Name = "cameraSetting1";
            this.cameraSetting1.Size = new System.Drawing.Size(157, 294);
            this.cameraSetting1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonZoom25);
            this.groupBox2.Controls.Add(this.buttonZoom50);
            this.groupBox2.Controls.Add(this.buttonZoom200);
            this.groupBox2.Controls.Add(this.buttonZoom100);
            this.groupBox2.Location = new System.Drawing.Point(636, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(127, 81);
            this.groupBox2.TabIndex = 206;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示";
            // 
            // buttonZoom25
            // 
            this.buttonZoom25.Location = new System.Drawing.Point(6, 18);
            this.buttonZoom25.Name = "buttonZoom25";
            this.buttonZoom25.Size = new System.Drawing.Size(55, 26);
            this.buttonZoom25.TabIndex = 137;
            this.buttonZoom25.Text = "25%";
            this.buttonZoom25.UseVisualStyleBackColor = true;
            this.buttonZoom25.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // buttonZoom50
            // 
            this.buttonZoom50.Location = new System.Drawing.Point(66, 18);
            this.buttonZoom50.Name = "buttonZoom50";
            this.buttonZoom50.Size = new System.Drawing.Size(55, 25);
            this.buttonZoom50.TabIndex = 135;
            this.buttonZoom50.Text = "50%";
            this.buttonZoom50.UseVisualStyleBackColor = true;
            this.buttonZoom50.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // buttonZoom200
            // 
            this.buttonZoom200.Location = new System.Drawing.Point(66, 47);
            this.buttonZoom200.Name = "buttonZoom200";
            this.buttonZoom200.Size = new System.Drawing.Size(55, 25);
            this.buttonZoom200.TabIndex = 136;
            this.buttonZoom200.Text = "200%";
            this.buttonZoom200.UseVisualStyleBackColor = true;
            this.buttonZoom200.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // buttonZoom100
            // 
            this.buttonZoom100.Location = new System.Drawing.Point(6, 48);
            this.buttonZoom100.Name = "buttonZoom100";
            this.buttonZoom100.Size = new System.Drawing.Size(55, 25);
            this.buttonZoom100.TabIndex = 136;
            this.buttonZoom100.Text = "100%";
            this.buttonZoom100.UseVisualStyleBackColor = true;
            this.buttonZoom100.Click += new System.EventHandler(this.buttonZoom_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 577);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.selectBoxRealDisp);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelTrig4);
            this.Controls.Add(this.labelTrig3);
            this.Controls.Add(this.labelTrig2);
            this.Controls.Add(this.labelTrig1);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.optionBox4);
            this.Controls.Add(this.optionBox2);
            this.Controls.Add(this.optionBox3);
            this.Controls.Add(this.optionBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxMeasResult);
            this.Controls.Add(this.CamDisplayTest);
            this.Controls.Add(this.CamDisplayLeftCell);
            this.Controls.Add(this.CamDisplayMiddleCell);
            this.Controls.Add(this.CamDisplayRightCell);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CDI Vision";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Colibri.CommonModule.ToolBox.CameraDispControl CamDisplayMiddleCell;
        private Colibri.CommonModule.ToolBox.CameraDispControl CamDisplayLeftCell;
        private Colibri.CommonModule.ToolBox.CameraDispControl CamDisplayTest;
        private System.Windows.Forms.Timer timer1;
        private Colibri.CommonModule.ToolBox.CameraDispControl CamDisplayRightCell;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.ListBox listBoxMeasResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonConnectCDI;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox1;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox2;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox3;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox4;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelTrig1;
        private System.Windows.Forms.Label labelTrig2;
        private System.Windows.Forms.Label labelTrig3;
        private System.Windows.Forms.Label labelTrig4;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxSaveAll;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxSaveNG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCalibration;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxRealDisp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox listBoxProduct;
        private System.Windows.Forms.Panel panelCamera;
        private SVSCamera.CameraSetting cameraSetting1;
        private VisionSetting VisionSetting1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonZoom25;
        private System.Windows.Forms.Button buttonZoom50;
        private System.Windows.Forms.Button buttonZoom200;
        private System.Windows.Forms.Button buttonZoom100;
    }
}

