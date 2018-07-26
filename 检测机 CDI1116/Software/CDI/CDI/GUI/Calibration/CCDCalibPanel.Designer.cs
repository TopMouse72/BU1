namespace CDI.GUI
{
    partial class CCDCalibPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelGauge = new System.Windows.Forms.Label();
            this.panelStdValue = new System.Windows.Forms.Panel();
            this.buttonExitStd = new System.Windows.Forms.Button();
            this.buttonSaveStd = new System.Windows.Forms.Button();
            this.textBoxShoulderWidth = new System.Windows.Forms.TextBox();
            this.textBoxTabDistance = new System.Windows.Forms.TextBox();
            this.textBoxAlTabLength = new System.Windows.Forms.TextBox();
            this.textBoxAlSealantHeight = new System.Windows.Forms.TextBox();
            this.textBoxNiTabLength = new System.Windows.Forms.TextBox();
            this.textBoxNiSealantHeight = new System.Windows.Forms.TextBox();
            this.textBoxAlTabDistance = new System.Windows.Forms.TextBox();
            this.textBoxNiTabDistance = new System.Windows.Forms.TextBox();
            this.textBoxCellLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCellWidth = new System.Windows.Forms.TextBox();
            this.labelTabDistance = new System.Windows.Forms.Label();
            this.labelAlTabLength = new System.Windows.Forms.Label();
            this.labelAlSealantHeight = new System.Windows.Forms.Label();
            this.labelNiTabLength = new System.Windows.Forms.Label();
            this.labelNiSealantHeight = new System.Windows.Forms.Label();
            this.labelAlTabDistance = new System.Windows.Forms.Label();
            this.labelNiTabDistance = new System.Windows.Forms.Label();
            this.labelCellLength = new System.Windows.Forms.Label();
            this.labelCellWidth = new System.Windows.Forms.Label();
            this.optionBoxFromFile = new Colibri.CommonModule.ToolBox.OptionBox();
            this.buttonCountReset = new System.Windows.Forms.Button();
            this.optionBoxFromCamera = new Colibri.CommonModule.ToolBox.OptionBox();
            this.buttonLoadCell = new System.Windows.Forms.Button();
            this.textBoxYIntercept = new System.Windows.Forms.TextBox();
            this.textBoxXIntercept = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectBoxTabDistance = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxNiTabDistance = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxAlTabDistance = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxShoulderWidth = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxCellWidth = new Colibri.CommonModule.ToolBox.SelectBox();
            this.buttonROISet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.selectBoxNiSealant = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxNiTabLength = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxAlSealant = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxAlTabLength = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxCellLength = new Colibri.CommonModule.ToolBox.SelectBox();
            this.buttonLoadStd = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonRelease = new System.Windows.Forms.Button();
            this.comboBoxStdPosition = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxStdType = new System.Windows.Forms.ComboBox();
            this.buttonCalcLinear = new System.Windows.Forms.Button();
            this.selectBoxYEnable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxXEnable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.label23 = new System.Windows.Forms.Label();
            this.numericUpDownMeasAmount = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxYSlope = new System.Windows.Forms.TextBox();
            this.labelMeasCounter = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxXSlope = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.buttonSetStdValue = new System.Windows.Forms.Button();
            this.buttonStartMeasCali = new System.Windows.Forms.Button();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.buttonSaveCali = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.panelStdValue.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMeasAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV文件|*.csv";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.labelStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStatus.Location = new System.Drawing.Point(649, 563);
            this.labelStatus.Multiline = true;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.ReadOnly = true;
            this.labelStatus.Size = new System.Drawing.Size(276, 96);
            this.labelStatus.TabIndex = 380;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelGauge);
            this.groupBox1.Controls.Add(this.panelStdValue);
            this.groupBox1.Controls.Add(this.optionBoxFromFile);
            this.groupBox1.Controls.Add(this.buttonCountReset);
            this.groupBox1.Controls.Add(this.optionBoxFromCamera);
            this.groupBox1.Controls.Add(this.buttonLoadCell);
            this.groupBox1.Controls.Add(this.textBoxYIntercept);
            this.groupBox1.Controls.Add(this.textBoxXIntercept);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.buttonROISet);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.buttonLoadStd);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.buttonRelease);
            this.groupBox1.Controls.Add(this.comboBoxStdPosition);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.comboBoxStdType);
            this.groupBox1.Controls.Add(this.buttonCalcLinear);
            this.groupBox1.Controls.Add(this.selectBoxYEnable);
            this.groupBox1.Controls.Add(this.selectBoxXEnable);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.numericUpDownMeasAmount);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.textBoxYSlope);
            this.groupBox1.Controls.Add(this.labelMeasCounter);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.textBoxXSlope);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.buttonSetStdValue);
            this.groupBox1.Controls.Add(this.buttonStartMeasCali);
            this.groupBox1.Controls.Add(this.buttonSaveData);
            this.groupBox1.Controls.Add(this.buttonSaveCali);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(649, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 557);
            this.groupBox1.TabIndex = 377;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标准块线性测试";
            // 
            // labelGauge
            // 
            this.labelGauge.AutoSize = true;
            this.labelGauge.ForeColor = System.Drawing.Color.Red;
            this.labelGauge.Location = new System.Drawing.Point(99, 70);
            this.labelGauge.Name = "labelGauge";
            this.labelGauge.Size = new System.Drawing.Size(79, 13);
            this.labelGauge.TabIndex = 398;
            this.labelGauge.Text = "使用标准块：";
            // 
            // panelStdValue
            // 
            this.panelStdValue.BackColor = System.Drawing.Color.Bisque;
            this.panelStdValue.Controls.Add(this.buttonExitStd);
            this.panelStdValue.Controls.Add(this.buttonSaveStd);
            this.panelStdValue.Controls.Add(this.textBoxShoulderWidth);
            this.panelStdValue.Controls.Add(this.textBoxTabDistance);
            this.panelStdValue.Controls.Add(this.textBoxAlTabLength);
            this.panelStdValue.Controls.Add(this.textBoxAlSealantHeight);
            this.panelStdValue.Controls.Add(this.textBoxNiTabLength);
            this.panelStdValue.Controls.Add(this.textBoxNiSealantHeight);
            this.panelStdValue.Controls.Add(this.textBoxAlTabDistance);
            this.panelStdValue.Controls.Add(this.textBoxNiTabDistance);
            this.panelStdValue.Controls.Add(this.textBoxCellLength);
            this.panelStdValue.Controls.Add(this.label4);
            this.panelStdValue.Controls.Add(this.textBoxCellWidth);
            this.panelStdValue.Controls.Add(this.labelTabDistance);
            this.panelStdValue.Controls.Add(this.labelAlTabLength);
            this.panelStdValue.Controls.Add(this.labelAlSealantHeight);
            this.panelStdValue.Controls.Add(this.labelNiTabLength);
            this.panelStdValue.Controls.Add(this.labelNiSealantHeight);
            this.panelStdValue.Controls.Add(this.labelAlTabDistance);
            this.panelStdValue.Controls.Add(this.labelNiTabDistance);
            this.panelStdValue.Controls.Add(this.labelCellLength);
            this.panelStdValue.Controls.Add(this.labelCellWidth);
            this.panelStdValue.Location = new System.Drawing.Point(118, 111);
            this.panelStdValue.Name = "panelStdValue";
            this.panelStdValue.Size = new System.Drawing.Size(151, 25);
            this.panelStdValue.TabIndex = 10;
            this.panelStdValue.Visible = false;
            // 
            // buttonExitStd
            // 
            this.buttonExitStd.Location = new System.Drawing.Point(86, 263);
            this.buttonExitStd.Name = "buttonExitStd";
            this.buttonExitStd.Size = new System.Drawing.Size(56, 27);
            this.buttonExitStd.TabIndex = 198;
            this.buttonExitStd.Text = "退出";
            this.buttonExitStd.UseVisualStyleBackColor = true;
            this.buttonExitStd.Click += new System.EventHandler(this.buttonExitStd_Click);
            // 
            // buttonSaveStd
            // 
            this.buttonSaveStd.Location = new System.Drawing.Point(10, 263);
            this.buttonSaveStd.Name = "buttonSaveStd";
            this.buttonSaveStd.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveStd.TabIndex = 197;
            this.buttonSaveStd.Text = "保存";
            this.buttonSaveStd.UseVisualStyleBackColor = true;
            this.buttonSaveStd.Visible = false;
            this.buttonSaveStd.Click += new System.EventHandler(this.buttonSaveStd_Click);
            // 
            // textBoxShoulderWidth
            // 
            this.textBoxShoulderWidth.Location = new System.Drawing.Point(85, 237);
            this.textBoxShoulderWidth.Name = "textBoxShoulderWidth";
            this.textBoxShoulderWidth.ReadOnly = true;
            this.textBoxShoulderWidth.Size = new System.Drawing.Size(63, 20);
            this.textBoxShoulderWidth.TabIndex = 186;
            // 
            // textBoxTabDistance
            // 
            this.textBoxTabDistance.Location = new System.Drawing.Point(85, 211);
            this.textBoxTabDistance.Name = "textBoxTabDistance";
            this.textBoxTabDistance.ReadOnly = true;
            this.textBoxTabDistance.Size = new System.Drawing.Size(63, 20);
            this.textBoxTabDistance.TabIndex = 186;
            // 
            // textBoxAlTabLength
            // 
            this.textBoxAlTabLength.Location = new System.Drawing.Point(85, 133);
            this.textBoxAlTabLength.Name = "textBoxAlTabLength";
            this.textBoxAlTabLength.ReadOnly = true;
            this.textBoxAlTabLength.Size = new System.Drawing.Size(63, 20);
            this.textBoxAlTabLength.TabIndex = 183;
            // 
            // textBoxAlSealantHeight
            // 
            this.textBoxAlSealantHeight.Location = new System.Drawing.Point(85, 185);
            this.textBoxAlSealantHeight.Name = "textBoxAlSealantHeight";
            this.textBoxAlSealantHeight.ReadOnly = true;
            this.textBoxAlSealantHeight.Size = new System.Drawing.Size(63, 20);
            this.textBoxAlSealantHeight.TabIndex = 185;
            // 
            // textBoxNiTabLength
            // 
            this.textBoxNiTabLength.Location = new System.Drawing.Point(85, 107);
            this.textBoxNiTabLength.Name = "textBoxNiTabLength";
            this.textBoxNiTabLength.ReadOnly = true;
            this.textBoxNiTabLength.Size = new System.Drawing.Size(63, 20);
            this.textBoxNiTabLength.TabIndex = 182;
            // 
            // textBoxNiSealantHeight
            // 
            this.textBoxNiSealantHeight.Location = new System.Drawing.Point(85, 159);
            this.textBoxNiSealantHeight.Name = "textBoxNiSealantHeight";
            this.textBoxNiSealantHeight.ReadOnly = true;
            this.textBoxNiSealantHeight.Size = new System.Drawing.Size(63, 20);
            this.textBoxNiSealantHeight.TabIndex = 184;
            // 
            // textBoxAlTabDistance
            // 
            this.textBoxAlTabDistance.Location = new System.Drawing.Point(85, 81);
            this.textBoxAlTabDistance.Name = "textBoxAlTabDistance";
            this.textBoxAlTabDistance.ReadOnly = true;
            this.textBoxAlTabDistance.Size = new System.Drawing.Size(63, 20);
            this.textBoxAlTabDistance.TabIndex = 181;
            this.textBoxAlTabDistance.TextChanged += new System.EventHandler(this.textBoxTabDistance_TextChanged);
            // 
            // textBoxNiTabDistance
            // 
            this.textBoxNiTabDistance.Location = new System.Drawing.Point(85, 55);
            this.textBoxNiTabDistance.Name = "textBoxNiTabDistance";
            this.textBoxNiTabDistance.ReadOnly = true;
            this.textBoxNiTabDistance.Size = new System.Drawing.Size(63, 20);
            this.textBoxNiTabDistance.TabIndex = 180;
            this.textBoxNiTabDistance.TextChanged += new System.EventHandler(this.textBoxTabDistance_TextChanged);
            // 
            // textBoxCellLength
            // 
            this.textBoxCellLength.Location = new System.Drawing.Point(85, 29);
            this.textBoxCellLength.Name = "textBoxCellLength";
            this.textBoxCellLength.ReadOnly = true;
            this.textBoxCellLength.Size = new System.Drawing.Size(63, 20);
            this.textBoxCellLength.TabIndex = 179;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 194;
            this.label4.Text = "肩宽";
            // 
            // textBoxCellWidth
            // 
            this.textBoxCellWidth.Location = new System.Drawing.Point(85, 3);
            this.textBoxCellWidth.Name = "textBoxCellWidth";
            this.textBoxCellWidth.ReadOnly = true;
            this.textBoxCellWidth.Size = new System.Drawing.Size(63, 20);
            this.textBoxCellWidth.TabIndex = 178;
            // 
            // labelTabDistance
            // 
            this.labelTabDistance.AutoSize = true;
            this.labelTabDistance.Location = new System.Drawing.Point(2, 215);
            this.labelTabDistance.Name = "labelTabDistance";
            this.labelTabDistance.Size = new System.Drawing.Size(50, 13);
            this.labelTabDistance.TabIndex = 194;
            this.labelTabDistance.Text = "Tab间距";
            // 
            // labelAlTabLength
            // 
            this.labelAlTabLength.AutoSize = true;
            this.labelAlTabLength.Location = new System.Drawing.Point(2, 137);
            this.labelAlTabLength.Name = "labelAlTabLength";
            this.labelAlTabLength.Size = new System.Drawing.Size(59, 13);
            this.labelAlTabLength.TabIndex = 187;
            this.labelAlTabLength.Text = "AlTab长度";
            // 
            // labelAlSealantHeight
            // 
            this.labelAlSealantHeight.AutoSize = true;
            this.labelAlSealantHeight.Location = new System.Drawing.Point(2, 189);
            this.labelAlSealantHeight.Name = "labelAlSealantHeight";
            this.labelAlSealantHeight.Size = new System.Drawing.Size(76, 13);
            this.labelAlSealantHeight.TabIndex = 193;
            this.labelAlSealantHeight.Text = "Al小白胶高度";
            // 
            // labelNiTabLength
            // 
            this.labelNiTabLength.AutoSize = true;
            this.labelNiTabLength.Location = new System.Drawing.Point(2, 111);
            this.labelNiTabLength.Name = "labelNiTabLength";
            this.labelNiTabLength.Size = new System.Drawing.Size(60, 13);
            this.labelNiTabLength.TabIndex = 192;
            this.labelNiTabLength.Text = "NiTab长度";
            // 
            // labelNiSealantHeight
            // 
            this.labelNiSealantHeight.AutoSize = true;
            this.labelNiSealantHeight.Location = new System.Drawing.Point(2, 163);
            this.labelNiSealantHeight.Name = "labelNiSealantHeight";
            this.labelNiSealantHeight.Size = new System.Drawing.Size(77, 13);
            this.labelNiSealantHeight.TabIndex = 191;
            this.labelNiSealantHeight.Text = "Ni小白胶高度";
            // 
            // labelAlTabDistance
            // 
            this.labelAlTabDistance.AutoSize = true;
            this.labelAlTabDistance.Location = new System.Drawing.Point(2, 85);
            this.labelAlTabDistance.Name = "labelAlTabDistance";
            this.labelAlTabDistance.Size = new System.Drawing.Size(59, 13);
            this.labelAlTabDistance.TabIndex = 190;
            this.labelAlTabDistance.Text = "AlTab边距";
            // 
            // labelNiTabDistance
            // 
            this.labelNiTabDistance.AutoSize = true;
            this.labelNiTabDistance.Location = new System.Drawing.Point(2, 59);
            this.labelNiTabDistance.Name = "labelNiTabDistance";
            this.labelNiTabDistance.Size = new System.Drawing.Size(60, 13);
            this.labelNiTabDistance.TabIndex = 189;
            this.labelNiTabDistance.Text = "NiTab边距";
            // 
            // labelCellLength
            // 
            this.labelCellLength.AutoSize = true;
            this.labelCellLength.Location = new System.Drawing.Point(2, 33);
            this.labelCellLength.Name = "labelCellLength";
            this.labelCellLength.Size = new System.Drawing.Size(31, 13);
            this.labelCellLength.TabIndex = 188;
            this.labelCellLength.Text = "长度";
            // 
            // labelCellWidth
            // 
            this.labelCellWidth.AutoSize = true;
            this.labelCellWidth.Location = new System.Drawing.Point(2, 7);
            this.labelCellWidth.Name = "labelCellWidth";
            this.labelCellWidth.Size = new System.Drawing.Size(31, 13);
            this.labelCellWidth.TabIndex = 195;
            this.labelCellWidth.Text = "宽度";
            // 
            // optionBoxFromFile
            // 
            this.optionBoxFromFile.AutoSize = true;
            this.optionBoxFromFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxFromFile.BorderColor = System.Drawing.Color.Black;
            this.optionBoxFromFile.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxFromFile.Location = new System.Drawing.Point(211, 375);
            this.optionBoxFromFile.Name = "optionBoxFromFile";
            this.optionBoxFromFile.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.optionBoxFromFile.Size = new System.Drawing.Size(58, 23);
            this.optionBoxFromFile.TabIndex = 396;
            this.optionBoxFromFile.Text = "图片";
            this.optionBoxFromFile.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxFromFile.UseVisualStyleBackColor = true;
            // 
            // buttonCountReset
            // 
            this.buttonCountReset.Location = new System.Drawing.Point(113, 404);
            this.buttonCountReset.Name = "buttonCountReset";
            this.buttonCountReset.Size = new System.Drawing.Size(71, 27);
            this.buttonCountReset.TabIndex = 395;
            this.buttonCountReset.Text = "清空数据";
            this.buttonCountReset.UseVisualStyleBackColor = true;
            this.buttonCountReset.Click += new System.EventHandler(this.buttonCountReset_Click);
            // 
            // optionBoxFromCamera
            // 
            this.optionBoxFromCamera.AutoSize = true;
            this.optionBoxFromCamera.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxFromCamera.BorderColor = System.Drawing.Color.Black;
            this.optionBoxFromCamera.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxFromCamera.Checked = true;
            this.optionBoxFromCamera.GroupChecked = true;
            this.optionBoxFromCamera.Location = new System.Drawing.Point(135, 375);
            this.optionBoxFromCamera.Name = "optionBoxFromCamera";
            this.optionBoxFromCamera.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.optionBoxFromCamera.Size = new System.Drawing.Size(58, 23);
            this.optionBoxFromCamera.TabIndex = 397;
            this.optionBoxFromCamera.Text = "相机";
            this.optionBoxFromCamera.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxFromCamera.UseVisualStyleBackColor = true;
            // 
            // buttonLoadCell
            // 
            this.buttonLoadCell.Location = new System.Drawing.Point(9, 37);
            this.buttonLoadCell.Name = "buttonLoadCell";
            this.buttonLoadCell.Size = new System.Drawing.Size(84, 27);
            this.buttonLoadCell.TabIndex = 202;
            this.buttonLoadCell.Text = "加载物料";
            this.buttonLoadCell.UseVisualStyleBackColor = true;
            this.buttonLoadCell.Click += new System.EventHandler(this.buttonLoadCell_Click);
            // 
            // textBoxYIntercept
            // 
            this.textBoxYIntercept.Location = new System.Drawing.Point(145, 528);
            this.textBoxYIntercept.Name = "textBoxYIntercept";
            this.textBoxYIntercept.Size = new System.Drawing.Size(63, 20);
            this.textBoxYIntercept.TabIndex = 3;
            // 
            // textBoxXIntercept
            // 
            this.textBoxXIntercept.Location = new System.Drawing.Point(145, 502);
            this.textBoxXIntercept.Name = "textBoxXIntercept";
            this.textBoxXIntercept.Size = new System.Drawing.Size(63, 20);
            this.textBoxXIntercept.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.selectBoxTabDistance);
            this.groupBox3.Controls.Add(this.selectBoxNiTabDistance);
            this.groupBox3.Controls.Add(this.selectBoxAlTabDistance);
            this.groupBox3.Controls.Add(this.selectBoxShoulderWidth);
            this.groupBox3.Controls.Add(this.selectBoxCellWidth);
            this.groupBox3.Location = new System.Drawing.Point(9, 289);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 85);
            this.groupBox3.TabIndex = 201;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Y方向参数";
            // 
            // selectBoxTabDistance
            // 
            this.selectBoxTabDistance.AutoSize = true;
            this.selectBoxTabDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxTabDistance.BorderColor = System.Drawing.Color.Black;
            this.selectBoxTabDistance.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxTabDistance.Location = new System.Drawing.Point(180, 23);
            this.selectBoxTabDistance.Name = "selectBoxTabDistance";
            this.selectBoxTabDistance.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxTabDistance.Size = new System.Drawing.Size(75, 23);
            this.selectBoxTabDistance.TabIndex = 9;
            this.selectBoxTabDistance.Text = "Tab间距";
            this.selectBoxTabDistance.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxTabDistance.UseVisualStyleBackColor = true;
            // 
            // selectBoxNiTabDistance
            // 
            this.selectBoxNiTabDistance.AutoSize = true;
            this.selectBoxNiTabDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxNiTabDistance.BorderColor = System.Drawing.Color.Black;
            this.selectBoxNiTabDistance.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxNiTabDistance.Checked = true;
            this.selectBoxNiTabDistance.Location = new System.Drawing.Point(167, 52);
            this.selectBoxNiTabDistance.Name = "selectBoxNiTabDistance";
            this.selectBoxNiTabDistance.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxNiTabDistance.Size = new System.Drawing.Size(88, 23);
            this.selectBoxNiTabDistance.TabIndex = 9;
            this.selectBoxNiTabDistance.Text = "Ni Tab边距";
            this.selectBoxNiTabDistance.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxNiTabDistance.UseVisualStyleBackColor = true;
            // 
            // selectBoxAlTabDistance
            // 
            this.selectBoxAlTabDistance.AutoSize = true;
            this.selectBoxAlTabDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxAlTabDistance.BorderColor = System.Drawing.Color.Black;
            this.selectBoxAlTabDistance.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxAlTabDistance.Checked = true;
            this.selectBoxAlTabDistance.Location = new System.Drawing.Point(6, 52);
            this.selectBoxAlTabDistance.Name = "selectBoxAlTabDistance";
            this.selectBoxAlTabDistance.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxAlTabDistance.Size = new System.Drawing.Size(87, 23);
            this.selectBoxAlTabDistance.TabIndex = 9;
            this.selectBoxAlTabDistance.Text = "Al Tab边距";
            this.selectBoxAlTabDistance.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxAlTabDistance.UseVisualStyleBackColor = true;
            // 
            // selectBoxShoulderWidth
            // 
            this.selectBoxShoulderWidth.AutoSize = true;
            this.selectBoxShoulderWidth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxShoulderWidth.BorderColor = System.Drawing.Color.Black;
            this.selectBoxShoulderWidth.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxShoulderWidth.Location = new System.Drawing.Point(93, 23);
            this.selectBoxShoulderWidth.Name = "selectBoxShoulderWidth";
            this.selectBoxShoulderWidth.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxShoulderWidth.Size = new System.Drawing.Size(56, 23);
            this.selectBoxShoulderWidth.TabIndex = 9;
            this.selectBoxShoulderWidth.Text = "肩宽";
            this.selectBoxShoulderWidth.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxShoulderWidth.UseVisualStyleBackColor = true;
            // 
            // selectBoxCellWidth
            // 
            this.selectBoxCellWidth.AutoSize = true;
            this.selectBoxCellWidth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxCellWidth.BorderColor = System.Drawing.Color.Black;
            this.selectBoxCellWidth.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxCellWidth.Checked = true;
            this.selectBoxCellWidth.Location = new System.Drawing.Point(6, 23);
            this.selectBoxCellWidth.Name = "selectBoxCellWidth";
            this.selectBoxCellWidth.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxCellWidth.Size = new System.Drawing.Size(56, 23);
            this.selectBoxCellWidth.TabIndex = 9;
            this.selectBoxCellWidth.Text = "宽度";
            this.selectBoxCellWidth.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxCellWidth.UseVisualStyleBackColor = true;
            // 
            // buttonROISet
            // 
            this.buttonROISet.Location = new System.Drawing.Point(123, 37);
            this.buttonROISet.Name = "buttonROISet";
            this.buttonROISet.Size = new System.Drawing.Size(84, 27);
            this.buttonROISet.TabIndex = 2;
            this.buttonROISet.Text = "打开ROI设置";
            this.buttonROISet.UseVisualStyleBackColor = true;
            this.buttonROISet.Visible = false;
            this.buttonROISet.Click += new System.EventHandler(this.buttonROISet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.selectBoxNiSealant);
            this.groupBox2.Controls.Add(this.selectBoxNiTabLength);
            this.groupBox2.Controls.Add(this.selectBoxAlSealant);
            this.groupBox2.Controls.Add(this.selectBoxAlTabLength);
            this.groupBox2.Controls.Add(this.selectBoxCellLength);
            this.groupBox2.Location = new System.Drawing.Point(9, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 78);
            this.groupBox2.TabIndex = 201;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X方向参数";
            // 
            // selectBoxNiSealant
            // 
            this.selectBoxNiSealant.AutoSize = true;
            this.selectBoxNiSealant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxNiSealant.BorderColor = System.Drawing.Color.Black;
            this.selectBoxNiSealant.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxNiSealant.Location = new System.Drawing.Point(155, 48);
            this.selectBoxNiSealant.Name = "selectBoxNiSealant";
            this.selectBoxNiSealant.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxNiSealant.Size = new System.Drawing.Size(100, 23);
            this.selectBoxNiSealant.TabIndex = 10;
            this.selectBoxNiSealant.Text = "Ni Tab小白胶";
            this.selectBoxNiSealant.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxNiSealant.UseVisualStyleBackColor = true;
            // 
            // selectBoxNiTabLength
            // 
            this.selectBoxNiTabLength.AutoSize = true;
            this.selectBoxNiTabLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxNiTabLength.BorderColor = System.Drawing.Color.Black;
            this.selectBoxNiTabLength.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxNiTabLength.Checked = true;
            this.selectBoxNiTabLength.Location = new System.Drawing.Point(167, 20);
            this.selectBoxNiTabLength.Name = "selectBoxNiTabLength";
            this.selectBoxNiTabLength.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxNiTabLength.Size = new System.Drawing.Size(88, 23);
            this.selectBoxNiTabLength.TabIndex = 10;
            this.selectBoxNiTabLength.Text = "Ni Tab长度";
            this.selectBoxNiTabLength.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxNiTabLength.UseVisualStyleBackColor = true;
            // 
            // selectBoxAlSealant
            // 
            this.selectBoxAlSealant.AutoSize = true;
            this.selectBoxAlSealant.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxAlSealant.BorderColor = System.Drawing.Color.Black;
            this.selectBoxAlSealant.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxAlSealant.Location = new System.Drawing.Point(6, 48);
            this.selectBoxAlSealant.Name = "selectBoxAlSealant";
            this.selectBoxAlSealant.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxAlSealant.Size = new System.Drawing.Size(99, 23);
            this.selectBoxAlSealant.TabIndex = 11;
            this.selectBoxAlSealant.Text = "Al Tab小白胶";
            this.selectBoxAlSealant.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxAlSealant.UseVisualStyleBackColor = true;
            // 
            // selectBoxAlTabLength
            // 
            this.selectBoxAlTabLength.AutoSize = true;
            this.selectBoxAlTabLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxAlTabLength.BorderColor = System.Drawing.Color.Black;
            this.selectBoxAlTabLength.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxAlTabLength.Checked = true;
            this.selectBoxAlTabLength.Location = new System.Drawing.Point(71, 20);
            this.selectBoxAlTabLength.Name = "selectBoxAlTabLength";
            this.selectBoxAlTabLength.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxAlTabLength.Size = new System.Drawing.Size(87, 23);
            this.selectBoxAlTabLength.TabIndex = 11;
            this.selectBoxAlTabLength.Text = "Al Tab长度";
            this.selectBoxAlTabLength.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxAlTabLength.UseVisualStyleBackColor = true;
            // 
            // selectBoxCellLength
            // 
            this.selectBoxCellLength.AutoSize = true;
            this.selectBoxCellLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxCellLength.BorderColor = System.Drawing.Color.Black;
            this.selectBoxCellLength.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxCellLength.Checked = true;
            this.selectBoxCellLength.Location = new System.Drawing.Point(6, 20);
            this.selectBoxCellLength.Name = "selectBoxCellLength";
            this.selectBoxCellLength.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxCellLength.Size = new System.Drawing.Size(56, 23);
            this.selectBoxCellLength.TabIndex = 9;
            this.selectBoxCellLength.Text = "长度";
            this.selectBoxCellLength.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxCellLength.UseVisualStyleBackColor = true;
            // 
            // buttonLoadStd
            // 
            this.buttonLoadStd.ForeColor = System.Drawing.Color.Red;
            this.buttonLoadStd.Location = new System.Drawing.Point(8, 153);
            this.buttonLoadStd.Name = "buttonLoadStd";
            this.buttonLoadStd.Size = new System.Drawing.Size(82, 27);
            this.buttonLoadStd.TabIndex = 2;
            this.buttonLoadStd.Text = "加载工位";
            this.buttonLoadStd.UseVisualStyleBackColor = true;
            this.buttonLoadStd.Click += new System.EventHandler(this.buttonLoadStd_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.ForeColor = System.Drawing.Color.Red;
            this.buttonOpen.Location = new System.Drawing.Point(109, 153);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(69, 27);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "打开真空";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonRelease
            // 
            this.buttonRelease.ForeColor = System.Drawing.Color.Red;
            this.buttonRelease.Location = new System.Drawing.Point(197, 153);
            this.buttonRelease.Name = "buttonRelease";
            this.buttonRelease.Size = new System.Drawing.Size(69, 27);
            this.buttonRelease.TabIndex = 2;
            this.buttonRelease.Text = "释放真空";
            this.buttonRelease.UseVisualStyleBackColor = true;
            this.buttonRelease.Click += new System.EventHandler(this.buttonRelease_Click);
            // 
            // comboBoxStdPosition
            // 
            this.comboBoxStdPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStdPosition.FormattingEnabled = true;
            this.comboBoxStdPosition.Location = new System.Drawing.Point(78, 111);
            this.comboBoxStdPosition.Name = "comboBoxStdPosition";
            this.comboBoxStdPosition.Size = new System.Drawing.Size(74, 21);
            this.comboBoxStdPosition.TabIndex = 199;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.ForeColor = System.Drawing.Color.Yellow;
            this.label15.Location = new System.Drawing.Point(5, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 15);
            this.label15.TabIndex = 3;
            this.label15.Text = "3. 设置标准块";
            // 
            // comboBoxStdType
            // 
            this.comboBoxStdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStdType.FormattingEnabled = true;
            this.comboBoxStdType.Items.AddRange(new object[] {
            "小",
            "中",
            "大"});
            this.comboBoxStdType.Location = new System.Drawing.Point(78, 85);
            this.comboBoxStdType.Name = "comboBoxStdType";
            this.comboBoxStdType.Size = new System.Drawing.Size(74, 21);
            this.comboBoxStdType.TabIndex = 199;
            this.comboBoxStdType.SelectedIndexChanged += new System.EventHandler(this.comboBoxStdType_SelectedIndexChanged);
            // 
            // buttonCalcLinear
            // 
            this.buttonCalcLinear.Location = new System.Drawing.Point(17, 469);
            this.buttonCalcLinear.Name = "buttonCalcLinear";
            this.buttonCalcLinear.Size = new System.Drawing.Size(84, 27);
            this.buttonCalcLinear.TabIndex = 9;
            this.buttonCalcLinear.Text = "计算线性";
            this.buttonCalcLinear.UseVisualStyleBackColor = true;
            this.buttonCalcLinear.Click += new System.EventHandler(this.buttonCalcLinear_Click);
            // 
            // selectBoxYEnable
            // 
            this.selectBoxYEnable.AutoSize = true;
            this.selectBoxYEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxYEnable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxYEnable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxYEnable.Location = new System.Drawing.Point(214, 527);
            this.selectBoxYEnable.Name = "selectBoxYEnable";
            this.selectBoxYEnable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxYEnable.Size = new System.Drawing.Size(56, 23);
            this.selectBoxYEnable.TabIndex = 8;
            this.selectBoxYEnable.Text = "有效";
            this.selectBoxYEnable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxYEnable.UseVisualStyleBackColor = true;
            // 
            // selectBoxXEnable
            // 
            this.selectBoxXEnable.AutoSize = true;
            this.selectBoxXEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxXEnable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxXEnable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxXEnable.Location = new System.Drawing.Point(214, 501);
            this.selectBoxXEnable.Name = "selectBoxXEnable";
            this.selectBoxXEnable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxXEnable.Size = new System.Drawing.Size(56, 23);
            this.selectBoxXEnable.TabIndex = 8;
            this.selectBoxXEnable.Text = "有效";
            this.selectBoxXEnable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxXEnable.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.ForeColor = System.Drawing.Color.Yellow;
            this.label23.Location = new System.Drawing.Point(6, 453);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(93, 15);
            this.label23.TabIndex = 7;
            this.label23.Text = "6. 计算线性结果";
            // 
            // numericUpDownMeasAmount
            // 
            this.numericUpDownMeasAmount.Location = new System.Drawing.Point(72, 404);
            this.numericUpDownMeasAmount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownMeasAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMeasAmount.Name = "numericUpDownMeasAmount";
            this.numericUpDownMeasAmount.Size = new System.Drawing.Size(33, 20);
            this.numericUpDownMeasAmount.TabIndex = 6;
            this.numericUpDownMeasAmount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.ForeColor = System.Drawing.Color.Yellow;
            this.label20.Location = new System.Drawing.Point(6, 377);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 15);
            this.label20.TabIndex = 4;
            this.label20.Text = "5. 开始测量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 196;
            this.label2.Text = "标准块位置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(5, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "1. 设置ROI";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.ForeColor = System.Drawing.Color.Yellow;
            this.label19.Location = new System.Drawing.Point(5, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 15);
            this.label19.TabIndex = 3;
            this.label19.Text = "2. 选择标准块";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 88);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 196;
            this.label17.Text = "标准块类型";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(132, 532);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(14, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "B";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(49, 532);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "K";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(132, 506);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 4;
            this.label31.Text = "B";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(49, 506);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(14, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "K";
            // 
            // textBoxYSlope
            // 
            this.textBoxYSlope.Location = new System.Drawing.Point(63, 528);
            this.textBoxYSlope.Name = "textBoxYSlope";
            this.textBoxYSlope.Size = new System.Drawing.Size(63, 20);
            this.textBoxYSlope.TabIndex = 3;
            // 
            // labelMeasCounter
            // 
            this.labelMeasCounter.AutoSize = true;
            this.labelMeasCounter.Location = new System.Drawing.Point(75, 430);
            this.labelMeasCounter.Name = "labelMeasCounter";
            this.labelMeasCounter.Size = new System.Drawing.Size(13, 13);
            this.labelMeasCounter.TabIndex = 4;
            this.labelMeasCounter.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(7, 430);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "测量计数：";
            // 
            // textBoxXSlope
            // 
            this.textBoxXSlope.Location = new System.Drawing.Point(63, 502);
            this.textBoxXSlope.Name = "textBoxXSlope";
            this.textBoxXSlope.Size = new System.Drawing.Size(63, 20);
            this.textBoxXSlope.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 408);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 13);
            this.label22.TabIndex = 4;
            this.label22.Text = "测量次数：";
            // 
            // buttonSetStdValue
            // 
            this.buttonSetStdValue.Location = new System.Drawing.Point(168, 85);
            this.buttonSetStdValue.Name = "buttonSetStdValue";
            this.buttonSetStdValue.Size = new System.Drawing.Size(102, 27);
            this.buttonSetStdValue.TabIndex = 2;
            this.buttonSetStdValue.Text = "设置标准块尺寸";
            this.buttonSetStdValue.UseVisualStyleBackColor = true;
            this.buttonSetStdValue.Click += new System.EventHandler(this.buttonSetStdValue_Click);
            // 
            // buttonStartMeasCali
            // 
            this.buttonStartMeasCali.Location = new System.Drawing.Point(199, 404);
            this.buttonStartMeasCali.Name = "buttonStartMeasCali";
            this.buttonStartMeasCali.Size = new System.Drawing.Size(71, 27);
            this.buttonStartMeasCali.TabIndex = 2;
            this.buttonStartMeasCali.Text = "开始测量";
            this.buttonStartMeasCali.UseVisualStyleBackColor = true;
            this.buttonStartMeasCali.Click += new System.EventHandler(this.buttonStartMeasCali_Click);
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(199, 437);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(71, 27);
            this.buttonSaveData.TabIndex = 2;
            this.buttonSaveData.Text = "导出数据";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.SaveData);
            // 
            // buttonSaveCali
            // 
            this.buttonSaveCali.Location = new System.Drawing.Point(122, 469);
            this.buttonSaveCali.Name = "buttonSaveCali";
            this.buttonSaveCali.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveCali.TabIndex = 2;
            this.buttonSaveCali.Text = "保存";
            this.buttonSaveCali.UseVisualStyleBackColor = true;
            this.buttonSaveCali.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 532);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(38, 13);
            this.label29.TabIndex = 4;
            this.label29.Text = "Y方向";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 506);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "X方向";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(6, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 200;
            this.label1.Text = "4. 设置计算参数";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(643, 816);
            this.dataGridView1.TabIndex = 376;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // CCDCalibPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CCDCalibPanel";
            this.Size = new System.Drawing.Size(928, 816);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelStdValue.ResumeLayout(false);
            this.panelStdValue.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMeasAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxYEnable;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxXEnable;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numericUpDownMeasAmount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxYIntercept;
        private System.Windows.Forms.TextBox textBoxYSlope;
        private System.Windows.Forms.TextBox textBoxXIntercept;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxXSlope;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonLoadStd;
        private System.Windows.Forms.Button buttonStartMeasCali;
        private System.Windows.Forms.Button buttonSaveCali;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label labelMeasCounter;
        private System.Windows.Forms.Button buttonCalcLinear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonROISet;
        private System.Windows.Forms.Panel panelStdValue;
        private System.Windows.Forms.Button buttonExitStd;
        private System.Windows.Forms.Button buttonSaveStd;
        private System.Windows.Forms.TextBox textBoxTabDistance;
        private System.Windows.Forms.TextBox textBoxAlTabLength;
        private System.Windows.Forms.TextBox textBoxAlSealantHeight;
        private System.Windows.Forms.TextBox textBoxNiTabLength;
        private System.Windows.Forms.TextBox textBoxNiSealantHeight;
        private System.Windows.Forms.TextBox textBoxAlTabDistance;
        private System.Windows.Forms.TextBox textBoxNiTabDistance;
        private System.Windows.Forms.TextBox textBoxCellLength;
        private System.Windows.Forms.TextBox textBoxCellWidth;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labelTabDistance;
        private System.Windows.Forms.Label labelAlTabLength;
        private System.Windows.Forms.Label labelAlSealantHeight;
        private System.Windows.Forms.Label labelNiTabLength;
        private System.Windows.Forms.Label labelNiSealantHeight;
        private System.Windows.Forms.Label labelAlTabDistance;
        private System.Windows.Forms.Label labelNiTabDistance;
        private System.Windows.Forms.Label labelCellLength;
        private System.Windows.Forms.Label labelCellWidth;
        private System.Windows.Forms.Button buttonSetStdValue;
        private System.Windows.Forms.ComboBox comboBoxStdType;
        private System.Windows.Forms.GroupBox groupBox2;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxCellLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxTabDistance;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxNiTabDistance;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxAlTabDistance;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxCellWidth;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxNiSealant;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxNiTabLength;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxAlSealant;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxAlTabLength;
        private System.Windows.Forms.ComboBox comboBoxStdPosition;
        private System.Windows.Forms.Label label2;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxShoulderWidth;
        private System.Windows.Forms.Button buttonSaveData;
        protected System.Windows.Forms.TextBox labelStatus;
        private System.Windows.Forms.Button buttonRelease;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonLoadCell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxShoulderWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCountReset;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxFromFile;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxFromCamera;
        private System.Windows.Forms.Label labelGauge;
    }
}
