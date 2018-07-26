namespace CDI.GUI
{
    partial class ThicknessCalibPanel
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCalcLinear = new System.Windows.Forms.Button();
            this.selectBoxRightEnable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxMidEnable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxLeftEnable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.label23 = new System.Windows.Forms.Label();
            this.numericUpDownMeasAmount = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxRightIntercept = new System.Windows.Forms.TextBox();
            this.textBoxRightSlope = new System.Windows.Forms.TextBox();
            this.textBoxMidIntercept = new System.Windows.Forms.TextBox();
            this.textBoxStdSmall = new System.Windows.Forms.TextBox();
            this.textBoxMidSlope = new System.Windows.Forms.TextBox();
            this.textBoxLeftIntercept = new System.Windows.Forms.TextBox();
            this.labelMeasCounter = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxLeftSlope = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxStdMean = new System.Windows.Forms.TextBox();
            this.textBoxStdLarge = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelGauge = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonCountReset = new System.Windows.Forms.Button();
            this.buttonStartMeasCali = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRight = new System.Windows.Forms.ComboBox();
            this.comboBoxMid = new System.Windows.Forms.ComboBox();
            this.comboBoxLeft = new System.Windows.Forms.ComboBox();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.buttonSaveCali = new System.Windows.Forms.Button();
            this.buttonSaveStdValue = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRightReference = new System.Windows.Forms.TextBox();
            this.textBoxMidReference = new System.Windows.Forms.TextBox();
            this.textBoxLeftReference = new System.Windows.Forms.TextBox();
            this.buttonStartRightRefCali = new System.Windows.Forms.Button();
            this.buttonStartMidRefCali = new System.Windows.Forms.Button();
            this.buttonStartLeftRefCali = new System.Windows.Forms.Button();
            this.buttonSaveRef = new System.Windows.Forms.Button();
            this.buttonSaveRefThickness = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNewRefThickness = new System.Windows.Forms.TextBox();
            this.textBoxCurRefThickness = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMeasAmount)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(724, 633);
            this.dataGridView1.TabIndex = 376;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCalcLinear);
            this.groupBox1.Controls.Add(this.selectBoxRightEnable);
            this.groupBox1.Controls.Add(this.selectBoxMidEnable);
            this.groupBox1.Controls.Add(this.selectBoxLeftEnable);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.numericUpDownMeasAmount);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBoxRightIntercept);
            this.groupBox1.Controls.Add(this.textBoxRightSlope);
            this.groupBox1.Controls.Add(this.textBoxMidIntercept);
            this.groupBox1.Controls.Add(this.textBoxStdSmall);
            this.groupBox1.Controls.Add(this.textBoxMidSlope);
            this.groupBox1.Controls.Add(this.textBoxLeftIntercept);
            this.groupBox1.Controls.Add(this.labelMeasCounter);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.textBoxLeftSlope);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.textBoxStdMean);
            this.groupBox1.Controls.Add(this.textBoxStdLarge);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelGauge);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.buttonCountReset);
            this.groupBox1.Controls.Add(this.buttonStartMeasCali);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxRight);
            this.groupBox1.Controls.Add(this.comboBoxMid);
            this.groupBox1.Controls.Add(this.comboBoxLeft);
            this.groupBox1.Controls.Add(this.buttonSaveData);
            this.groupBox1.Controls.Add(this.buttonSaveCali);
            this.groupBox1.Controls.Add(this.buttonSaveStdValue);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Location = new System.Drawing.Point(730, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 364);
            this.groupBox1.TabIndex = 377;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标准块线性测试";
            // 
            // buttonCalcLinear
            // 
            this.buttonCalcLinear.Location = new System.Drawing.Point(138, 246);
            this.buttonCalcLinear.Name = "buttonCalcLinear";
            this.buttonCalcLinear.Size = new System.Drawing.Size(84, 27);
            this.buttonCalcLinear.TabIndex = 9;
            this.buttonCalcLinear.Text = "计算线性";
            this.buttonCalcLinear.UseVisualStyleBackColor = true;
            this.buttonCalcLinear.Click += new System.EventHandler(this.buttonCalcLinear_Click);
            // 
            // selectBoxRightEnable
            // 
            this.selectBoxRightEnable.AutoSize = true;
            this.selectBoxRightEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxRightEnable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxRightEnable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxRightEnable.Location = new System.Drawing.Point(240, 330);
            this.selectBoxRightEnable.Name = "selectBoxRightEnable";
            this.selectBoxRightEnable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxRightEnable.Size = new System.Drawing.Size(56, 23);
            this.selectBoxRightEnable.TabIndex = 8;
            this.selectBoxRightEnable.Text = "有效";
            this.selectBoxRightEnable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxRightEnable.UseVisualStyleBackColor = true;
            // 
            // selectBoxMidEnable
            // 
            this.selectBoxMidEnable.AutoSize = true;
            this.selectBoxMidEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxMidEnable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxMidEnable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxMidEnable.Location = new System.Drawing.Point(240, 304);
            this.selectBoxMidEnable.Name = "selectBoxMidEnable";
            this.selectBoxMidEnable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxMidEnable.Size = new System.Drawing.Size(56, 23);
            this.selectBoxMidEnable.TabIndex = 8;
            this.selectBoxMidEnable.Text = "有效";
            this.selectBoxMidEnable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxMidEnable.UseVisualStyleBackColor = true;
            // 
            // selectBoxLeftEnable
            // 
            this.selectBoxLeftEnable.AutoSize = true;
            this.selectBoxLeftEnable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxLeftEnable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxLeftEnable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxLeftEnable.Location = new System.Drawing.Point(240, 278);
            this.selectBoxLeftEnable.Name = "selectBoxLeftEnable";
            this.selectBoxLeftEnable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxLeftEnable.Size = new System.Drawing.Size(56, 23);
            this.selectBoxLeftEnable.TabIndex = 8;
            this.selectBoxLeftEnable.Text = "有效";
            this.selectBoxLeftEnable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxLeftEnable.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.ForeColor = System.Drawing.Color.Yellow;
            this.label23.Location = new System.Drawing.Point(6, 254);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(93, 15);
            this.label23.TabIndex = 7;
            this.label23.Text = "4. 计算线性结果";
            // 
            // numericUpDownMeasAmount
            // 
            this.numericUpDownMeasAmount.Location = new System.Drawing.Point(76, 206);
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
            this.label20.Location = new System.Drawing.Point(6, 181);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 15);
            this.label20.TabIndex = 4;
            this.label20.Text = "3. 开始测量";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(151, 336);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(14, 13);
            this.label33.TabIndex = 4;
            this.label33.Text = "B";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(59, 336);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 13);
            this.label27.TabIndex = 4;
            this.label27.Text = "K";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.ForeColor = System.Drawing.Color.Yellow;
            this.label19.Location = new System.Drawing.Point(6, 113);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(129, 15);
            this.label19.TabIndex = 3;
            this.label19.Text = "2. 设置标准块测试位置";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(151, 310);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(14, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "B";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(59, 310);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "K";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(249, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 13);
            this.label21.TabIndex = 5;
            this.label21.Text = "mm";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(151, 284);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 4;
            this.label31.Text = "B";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(59, 284);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(14, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "K";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.ForeColor = System.Drawing.Color.Yellow;
            this.label15.Location = new System.Drawing.Point(6, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 15);
            this.label15.TabIndex = 3;
            this.label15.Text = "1. 设置标准块厚度";
            // 
            // textBoxRightIntercept
            // 
            this.textBoxRightIntercept.Location = new System.Drawing.Point(171, 333);
            this.textBoxRightIntercept.Name = "textBoxRightIntercept";
            this.textBoxRightIntercept.Size = new System.Drawing.Size(63, 20);
            this.textBoxRightIntercept.TabIndex = 3;
            // 
            // textBoxRightSlope
            // 
            this.textBoxRightSlope.Location = new System.Drawing.Point(79, 333);
            this.textBoxRightSlope.Name = "textBoxRightSlope";
            this.textBoxRightSlope.Size = new System.Drawing.Size(63, 20);
            this.textBoxRightSlope.TabIndex = 3;
            // 
            // textBoxMidIntercept
            // 
            this.textBoxMidIntercept.Location = new System.Drawing.Point(171, 307);
            this.textBoxMidIntercept.Name = "textBoxMidIntercept";
            this.textBoxMidIntercept.Size = new System.Drawing.Size(63, 20);
            this.textBoxMidIntercept.TabIndex = 3;
            // 
            // textBoxStdSmall
            // 
            this.textBoxStdSmall.Location = new System.Drawing.Point(168, 82);
            this.textBoxStdSmall.Name = "textBoxStdSmall";
            this.textBoxStdSmall.ReadOnly = true;
            this.textBoxStdSmall.Size = new System.Drawing.Size(75, 20);
            this.textBoxStdSmall.TabIndex = 2;
            // 
            // textBoxMidSlope
            // 
            this.textBoxMidSlope.Location = new System.Drawing.Point(79, 307);
            this.textBoxMidSlope.Name = "textBoxMidSlope";
            this.textBoxMidSlope.Size = new System.Drawing.Size(63, 20);
            this.textBoxMidSlope.TabIndex = 3;
            // 
            // textBoxLeftIntercept
            // 
            this.textBoxLeftIntercept.Location = new System.Drawing.Point(171, 281);
            this.textBoxLeftIntercept.Name = "textBoxLeftIntercept";
            this.textBoxLeftIntercept.Size = new System.Drawing.Size(63, 20);
            this.textBoxLeftIntercept.TabIndex = 3;
            // 
            // labelMeasCounter
            // 
            this.labelMeasCounter.AutoSize = true;
            this.labelMeasCounter.Location = new System.Drawing.Point(79, 233);
            this.labelMeasCounter.Name = "labelMeasCounter";
            this.labelMeasCounter.Size = new System.Drawing.Size(13, 13);
            this.labelMeasCounter.TabIndex = 4;
            this.labelMeasCounter.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(11, 233);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "测量计数：";
            // 
            // textBoxLeftSlope
            // 
            this.textBoxLeftSlope.Location = new System.Drawing.Point(79, 281);
            this.textBoxLeftSlope.Name = "textBoxLeftSlope";
            this.textBoxLeftSlope.Size = new System.Drawing.Size(63, 20);
            this.textBoxLeftSlope.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(11, 210);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 13);
            this.label22.TabIndex = 4;
            this.label22.Text = "测量次数：";
            // 
            // textBoxStdMean
            // 
            this.textBoxStdMean.Location = new System.Drawing.Point(87, 82);
            this.textBoxStdMean.Name = "textBoxStdMean";
            this.textBoxStdMean.ReadOnly = true;
            this.textBoxStdMean.Size = new System.Drawing.Size(75, 20);
            this.textBoxStdMean.TabIndex = 2;
            // 
            // textBoxStdLarge
            // 
            this.textBoxStdLarge.Location = new System.Drawing.Point(6, 82);
            this.textBoxStdLarge.Name = "textBoxStdLarge";
            this.textBoxStdLarge.ReadOnly = true;
            this.textBoxStdLarge.Size = new System.Drawing.Size(75, 20);
            this.textBoxStdLarge.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(196, 65);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "右工位";
            // 
            // labelGauge
            // 
            this.labelGauge.AutoSize = true;
            this.labelGauge.ForeColor = System.Drawing.Color.Red;
            this.labelGauge.Location = new System.Drawing.Point(6, 42);
            this.labelGauge.Name = "labelGauge";
            this.labelGauge.Size = new System.Drawing.Size(79, 13);
            this.labelGauge.TabIndex = 1;
            this.labelGauge.Text = "使用标准块：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(115, 65);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "中";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "中工位";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 65);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "大";
            // 
            // buttonCountReset
            // 
            this.buttonCountReset.Location = new System.Drawing.Point(118, 203);
            this.buttonCountReset.Name = "buttonCountReset";
            this.buttonCountReset.Size = new System.Drawing.Size(71, 27);
            this.buttonCountReset.TabIndex = 2;
            this.buttonCountReset.Text = "清空数据";
            this.buttonCountReset.UseVisualStyleBackColor = true;
            this.buttonCountReset.Click += new System.EventHandler(this.buttonCountReset_Click);
            // 
            // buttonStartMeasCali
            // 
            this.buttonStartMeasCali.Location = new System.Drawing.Point(195, 203);
            this.buttonStartMeasCali.Name = "buttonStartMeasCali";
            this.buttonStartMeasCali.Size = new System.Drawing.Size(77, 27);
            this.buttonStartMeasCali.TabIndex = 2;
            this.buttonStartMeasCali.Text = "开始测量";
            this.buttonStartMeasCali.UseVisualStyleBackColor = true;
            this.buttonStartMeasCali.Click += new System.EventHandler(this.buttonStartMeasCali_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "左工位";
            // 
            // comboBoxRight
            // 
            this.comboBoxRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRight.FormattingEnabled = true;
            this.comboBoxRight.Items.AddRange(new object[] {
            "小",
            "中",
            "大"});
            this.comboBoxRight.Location = new System.Drawing.Point(6, 148);
            this.comboBoxRight.Name = "comboBoxRight";
            this.comboBoxRight.Size = new System.Drawing.Size(75, 21);
            this.comboBoxRight.TabIndex = 0;
            // 
            // comboBoxMid
            // 
            this.comboBoxMid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMid.FormattingEnabled = true;
            this.comboBoxMid.Items.AddRange(new object[] {
            "小",
            "中",
            "大"});
            this.comboBoxMid.Location = new System.Drawing.Point(87, 148);
            this.comboBoxMid.Name = "comboBoxMid";
            this.comboBoxMid.Size = new System.Drawing.Size(75, 21);
            this.comboBoxMid.TabIndex = 0;
            // 
            // comboBoxLeft
            // 
            this.comboBoxLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeft.FormattingEnabled = true;
            this.comboBoxLeft.Items.AddRange(new object[] {
            "小",
            "中",
            "大"});
            this.comboBoxLeft.Location = new System.Drawing.Point(168, 148);
            this.comboBoxLeft.Name = "comboBoxLeft";
            this.comboBoxLeft.Size = new System.Drawing.Size(75, 21);
            this.comboBoxLeft.TabIndex = 0;
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(278, 203);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(77, 27);
            this.buttonSaveData.TabIndex = 2;
            this.buttonSaveData.Text = "导出数据";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.SaveData);
            // 
            // buttonSaveCali
            // 
            this.buttonSaveCali.Location = new System.Drawing.Point(310, 302);
            this.buttonSaveCali.Name = "buttonSaveCali";
            this.buttonSaveCali.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveCali.TabIndex = 2;
            this.buttonSaveCali.Text = "保存";
            this.buttonSaveCali.UseVisualStyleBackColor = true;
            this.buttonSaveCali.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveStdValue
            // 
            this.buttonSaveStdValue.Location = new System.Drawing.Point(287, 74);
            this.buttonSaveStdValue.Name = "buttonSaveStdValue";
            this.buttonSaveStdValue.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveStdValue.TabIndex = 2;
            this.buttonSaveStdValue.Text = "设定";
            this.buttonSaveStdValue.UseVisualStyleBackColor = true;
            this.buttonSaveStdValue.Visible = false;
            this.buttonSaveStdValue.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(9, 336);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(43, 13);
            this.label30.TabIndex = 4;
            this.label30.Text = "右工位";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(9, 310);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(43, 13);
            this.label29.TabIndex = 4;
            this.label29.Text = "中工位";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(9, 284);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "左工位";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBoxRightReference);
            this.groupBox2.Controls.Add(this.textBoxMidReference);
            this.groupBox2.Controls.Add(this.textBoxLeftReference);
            this.groupBox2.Controls.Add(this.buttonStartRightRefCali);
            this.groupBox2.Controls.Add(this.buttonStartMidRefCali);
            this.groupBox2.Controls.Add(this.buttonStartLeftRefCali);
            this.groupBox2.Controls.Add(this.buttonSaveRef);
            this.groupBox2.Controls.Add(this.buttonSaveRefThickness);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxNewRefThickness);
            this.groupBox2.Controls.Add(this.textBoxCurRefThickness);
            this.groupBox2.Location = new System.Drawing.Point(730, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 200);
            this.groupBox2.TabIndex = 378;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标定基准";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(278, 165);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "毫米";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(278, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "毫米";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(278, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "毫米";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(145, 165);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "基准值";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(145, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "基准值";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(145, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "基准值";
            // 
            // textBoxRightReference
            // 
            this.textBoxRightReference.Location = new System.Drawing.Point(188, 163);
            this.textBoxRightReference.Name = "textBoxRightReference";
            this.textBoxRightReference.Size = new System.Drawing.Size(84, 20);
            this.textBoxRightReference.TabIndex = 3;
            // 
            // textBoxMidReference
            // 
            this.textBoxMidReference.Location = new System.Drawing.Point(188, 129);
            this.textBoxMidReference.Name = "textBoxMidReference";
            this.textBoxMidReference.Size = new System.Drawing.Size(84, 20);
            this.textBoxMidReference.TabIndex = 3;
            // 
            // textBoxLeftReference
            // 
            this.textBoxLeftReference.Location = new System.Drawing.Point(188, 96);
            this.textBoxLeftReference.Name = "textBoxLeftReference";
            this.textBoxLeftReference.Size = new System.Drawing.Size(84, 20);
            this.textBoxLeftReference.TabIndex = 3;
            // 
            // buttonStartRightRefCali
            // 
            this.buttonStartRightRefCali.Location = new System.Drawing.Point(6, 158);
            this.buttonStartRightRefCali.Name = "buttonStartRightRefCali";
            this.buttonStartRightRefCali.Size = new System.Drawing.Size(133, 27);
            this.buttonStartRightRefCali.TabIndex = 2;
            this.buttonStartRightRefCali.Text = "开始右工位基准标定";
            this.buttonStartRightRefCali.UseVisualStyleBackColor = true;
            this.buttonStartRightRefCali.Click += new System.EventHandler(this.buttonStartRightRefCali_Click);
            // 
            // buttonStartMidRefCali
            // 
            this.buttonStartMidRefCali.Location = new System.Drawing.Point(6, 125);
            this.buttonStartMidRefCali.Name = "buttonStartMidRefCali";
            this.buttonStartMidRefCali.Size = new System.Drawing.Size(133, 27);
            this.buttonStartMidRefCali.TabIndex = 2;
            this.buttonStartMidRefCali.Text = "开始中工位基准标定";
            this.buttonStartMidRefCali.UseVisualStyleBackColor = true;
            this.buttonStartMidRefCali.Click += new System.EventHandler(this.buttonStartMidRefCali_Click);
            // 
            // buttonStartLeftRefCali
            // 
            this.buttonStartLeftRefCali.Location = new System.Drawing.Point(6, 92);
            this.buttonStartLeftRefCali.Name = "buttonStartLeftRefCali";
            this.buttonStartLeftRefCali.Size = new System.Drawing.Size(133, 27);
            this.buttonStartLeftRefCali.TabIndex = 2;
            this.buttonStartLeftRefCali.Text = "开始左工位基准标定";
            this.buttonStartLeftRefCali.UseVisualStyleBackColor = true;
            this.buttonStartLeftRefCali.Click += new System.EventHandler(this.buttonStartLeftRefCali_Click);
            // 
            // buttonSaveRef
            // 
            this.buttonSaveRef.Location = new System.Drawing.Point(313, 125);
            this.buttonSaveRef.Name = "buttonSaveRef";
            this.buttonSaveRef.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveRef.TabIndex = 2;
            this.buttonSaveRef.Text = "保存";
            this.buttonSaveRef.UseVisualStyleBackColor = true;
            this.buttonSaveRef.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveRefThickness
            // 
            this.buttonSaveRefThickness.Location = new System.Drawing.Point(230, 28);
            this.buttonSaveRefThickness.Name = "buttonSaveRefThickness";
            this.buttonSaveRefThickness.Size = new System.Drawing.Size(56, 27);
            this.buttonSaveRefThickness.TabIndex = 2;
            this.buttonSaveRefThickness.Text = "设定";
            this.buttonSaveRefThickness.UseVisualStyleBackColor = true;
            this.buttonSaveRefThickness.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "毫米";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(3, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(223, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "然后点开始按钮开启测试流程并自动标定";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(3, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "放置标定块在要标定基准的工位中，";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "设定值";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "当前值";
            // 
            // textBoxNewRefThickness
            // 
            this.textBoxNewRefThickness.Location = new System.Drawing.Point(96, 35);
            this.textBoxNewRefThickness.Name = "textBoxNewRefThickness";
            this.textBoxNewRefThickness.Size = new System.Drawing.Size(84, 20);
            this.textBoxNewRefThickness.TabIndex = 0;
            // 
            // textBoxCurRefThickness
            // 
            this.textBoxCurRefThickness.Location = new System.Drawing.Point(6, 35);
            this.textBoxCurRefThickness.Name = "textBoxCurRefThickness";
            this.textBoxCurRefThickness.ReadOnly = true;
            this.textBoxCurRefThickness.Size = new System.Drawing.Size(84, 20);
            this.textBoxCurRefThickness.TabIndex = 0;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStatus.Location = new System.Drawing.Point(730, 576);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(18, 25);
            this.labelStatus.TabIndex = 379;
            this.labelStatus.Text = " ";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV文件|*.csv";
            // 
            // ThicknessCalibPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ThicknessCalibPanel";
            this.Size = new System.Drawing.Size(1200, 633);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMeasAmount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxRightEnable;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxMidEnable;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxLeftEnable;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numericUpDownMeasAmount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxRightIntercept;
        private System.Windows.Forms.TextBox textBoxRightSlope;
        private System.Windows.Forms.TextBox textBoxMidIntercept;
        private System.Windows.Forms.TextBox textBoxStdSmall;
        private System.Windows.Forms.TextBox textBoxMidSlope;
        private System.Windows.Forms.TextBox textBoxLeftIntercept;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxLeftSlope;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxStdMean;
        private System.Windows.Forms.TextBox textBoxStdLarge;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonCountReset;
        private System.Windows.Forms.Button buttonStartMeasCali;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRight;
        private System.Windows.Forms.ComboBox comboBoxMid;
        private System.Windows.Forms.ComboBox comboBoxLeft;
        private System.Windows.Forms.Button buttonSaveCali;
        private System.Windows.Forms.Button buttonSaveStdValue;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRightReference;
        private System.Windows.Forms.TextBox textBoxMidReference;
        private System.Windows.Forms.TextBox textBoxLeftReference;
        private System.Windows.Forms.Button buttonStartRightRefCali;
        private System.Windows.Forms.Button buttonStartMidRefCali;
        private System.Windows.Forms.Button buttonStartLeftRefCali;
        private System.Windows.Forms.Button buttonSaveRef;
        private System.Windows.Forms.Button buttonSaveRefThickness;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNewRefThickness;
        private System.Windows.Forms.TextBox textBoxCurRefThickness;
        private System.Windows.Forms.Label labelMeasCounter;
        protected System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonCalcLinear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.Label labelGauge;
    }
}
