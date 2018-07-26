namespace CDI.GUI
{
    partial class ManualPanelZone上料机械手
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
            this.buttonStartPlace = new System.Windows.Forms.Button();
            this.buttonStartNGPlace = new System.Windows.Forms.Button();
            this.buttonStartPick = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DataDispCellRight = new CDI.GUI.DataDisp();
            this.DataDispCellMiddle = new CDI.GUI.DataDisp();
            this.DataDispCellLeft = new CDI.GUI.DataDisp();
            this.groupBoxNGBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNGBox = new System.Windows.Forms.Label();
            this.labelNGBoxFull = new System.Windows.Forms.Label();
            this.labelNGBoxRightCount = new System.Windows.Forms.Label();
            this.labelNGBoxMidCount = new System.Windows.Forms.Label();
            this.labelNGBoxLeftCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonWorkFlow = new System.Windows.Forms.Button();
            this.pointPosUCLoadPNPYSafeLimit = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCLoadPNPY = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCLoadPNPYPick = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCLoadPNPZ = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCLoadPNPYPlace = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPYBuffer = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPZPlaceNG = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPZPlace = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPZPick = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPYPlaceNG = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCLoadPNPZIdle = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.groupBox2.SuspendLayout();
            this.groupBoxNGBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 193);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 155);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(119, 155);
            // 
            // buttonStartPlace
            // 
            this.buttonStartPlace.Location = new System.Drawing.Point(431, 153);
            this.buttonStartPlace.Name = "buttonStartPlace";
            this.buttonStartPlace.Size = new System.Drawing.Size(110, 35);
            this.buttonStartPlace.TabIndex = 12;
            this.buttonStartPlace.Text = "放料";
            this.buttonStartPlace.UseVisualStyleBackColor = true;
            this.buttonStartPlace.Click += new System.EventHandler(this.buttonStartPlace_Click);
            // 
            // buttonStartNGPlace
            // 
            this.buttonStartNGPlace.Location = new System.Drawing.Point(547, 153);
            this.buttonStartNGPlace.Name = "buttonStartNGPlace";
            this.buttonStartNGPlace.Size = new System.Drawing.Size(110, 35);
            this.buttonStartNGPlace.TabIndex = 11;
            this.buttonStartNGPlace.Text = "放NG料";
            this.buttonStartNGPlace.UseVisualStyleBackColor = true;
            this.buttonStartNGPlace.Click += new System.EventHandler(this.buttonStartNGPlace_Click);
            // 
            // buttonStartPick
            // 
            this.buttonStartPick.Location = new System.Drawing.Point(315, 153);
            this.buttonStartPick.Name = "buttonStartPick";
            this.buttonStartPick.Size = new System.Drawing.Size(110, 35);
            this.buttonStartPick.TabIndex = 10;
            this.buttonStartPick.Text = "取料";
            this.buttonStartPick.UseVisualStyleBackColor = true;
            this.buttonStartPick.Click += new System.EventHandler(this.buttonStartPick_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.DataDispCellRight);
            this.groupBox2.Controls.Add(this.DataDispCellMiddle);
            this.groupBox2.Controls.Add(this.DataDispCellLeft);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 138);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电芯";
            // 
            // DataDispCellRight
            // 
            this.DataDispCellRight.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellRight.DataStation = null;
            this.DataDispCellRight.Location = new System.Drawing.Point(408, 19);
            this.DataDispCellRight.Name = "DataDispCellRight";
            this.DataDispCellRight.Size = new System.Drawing.Size(195, 100);
            this.DataDispCellRight.TabIndex = 3;
            this.DataDispCellRight.Text = "右电芯";
            // 
            // DataDispCellMiddle
            // 
            this.DataDispCellMiddle.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellMiddle.DataStation = null;
            this.DataDispCellMiddle.Location = new System.Drawing.Point(207, 19);
            this.DataDispCellMiddle.Name = "DataDispCellMiddle";
            this.DataDispCellMiddle.Size = new System.Drawing.Size(195, 100);
            this.DataDispCellMiddle.TabIndex = 4;
            this.DataDispCellMiddle.Text = "中电芯";
            // 
            // DataDispCellLeft
            // 
            this.DataDispCellLeft.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellLeft.DataStation = null;
            this.DataDispCellLeft.Location = new System.Drawing.Point(6, 19);
            this.DataDispCellLeft.Name = "DataDispCellLeft";
            this.DataDispCellLeft.Size = new System.Drawing.Size(195, 100);
            this.DataDispCellLeft.TabIndex = 5;
            this.DataDispCellLeft.Text = "左电芯";
            // 
            // groupBoxNGBox
            // 
            this.groupBoxNGBox.AutoSize = true;
            this.groupBoxNGBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxNGBox.Controls.Add(this.label1);
            this.groupBoxNGBox.Controls.Add(this.label16);
            this.groupBoxNGBox.Controls.Add(this.label3);
            this.groupBoxNGBox.Controls.Add(this.labelNGBox);
            this.groupBoxNGBox.Controls.Add(this.labelNGBoxFull);
            this.groupBoxNGBox.Controls.Add(this.labelNGBoxRightCount);
            this.groupBoxNGBox.Controls.Add(this.labelNGBoxMidCount);
            this.groupBoxNGBox.Controls.Add(this.labelNGBoxLeftCount);
            this.groupBoxNGBox.Controls.Add(this.label2);
            this.groupBoxNGBox.Location = new System.Drawing.Point(618, 3);
            this.groupBoxNGBox.Name = "groupBoxNGBox";
            this.groupBoxNGBox.Size = new System.Drawing.Size(236, 105);
            this.groupBoxNGBox.TabIndex = 14;
            this.groupBoxNGBox.TabStop = false;
            this.groupBoxNGBox.Text = "NG盒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "NG盒抽出再关上，NG电芯数量会清零。";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(171, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "NG盒抽出";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "NG盒满";
            // 
            // labelNGBox
            // 
            this.labelNGBox.BackColor = System.Drawing.Color.ForestGreen;
            this.labelNGBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNGBox.Location = new System.Drawing.Point(174, 49);
            this.labelNGBox.Name = "labelNGBox";
            this.labelNGBox.Size = new System.Drawing.Size(31, 16);
            this.labelNGBox.TabIndex = 0;
            this.labelNGBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNGBoxFull
            // 
            this.labelNGBoxFull.BackColor = System.Drawing.Color.ForestGreen;
            this.labelNGBoxFull.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNGBoxFull.Location = new System.Drawing.Point(128, 49);
            this.labelNGBoxFull.Name = "labelNGBoxFull";
            this.labelNGBoxFull.Size = new System.Drawing.Size(31, 16);
            this.labelNGBoxFull.TabIndex = 0;
            this.labelNGBoxFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNGBoxRightCount
            // 
            this.labelNGBoxRightCount.BackColor = System.Drawing.Color.LightGreen;
            this.labelNGBoxRightCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNGBoxRightCount.Location = new System.Drawing.Point(80, 49);
            this.labelNGBoxRightCount.Name = "labelNGBoxRightCount";
            this.labelNGBoxRightCount.Size = new System.Drawing.Size(31, 16);
            this.labelNGBoxRightCount.TabIndex = 0;
            this.labelNGBoxRightCount.Text = "0";
            this.labelNGBoxRightCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNGBoxMidCount
            // 
            this.labelNGBoxMidCount.BackColor = System.Drawing.Color.LightGreen;
            this.labelNGBoxMidCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNGBoxMidCount.Location = new System.Drawing.Point(43, 49);
            this.labelNGBoxMidCount.Name = "labelNGBoxMidCount";
            this.labelNGBoxMidCount.Size = new System.Drawing.Size(31, 16);
            this.labelNGBoxMidCount.TabIndex = 0;
            this.labelNGBoxMidCount.Text = "0";
            this.labelNGBoxMidCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNGBoxLeftCount
            // 
            this.labelNGBoxLeftCount.BackColor = System.Drawing.Color.LightGreen;
            this.labelNGBoxLeftCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNGBoxLeftCount.Location = new System.Drawing.Point(6, 49);
            this.labelNGBoxLeftCount.Name = "labelNGBoxLeftCount";
            this.labelNGBoxLeftCount.Size = new System.Drawing.Size(31, 16);
            this.labelNGBoxLeftCount.TabIndex = 0;
            this.labelNGBoxLeftCount.Text = "0";
            this.labelNGBoxLeftCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "电芯数量";
            // 
            // buttonWorkFlow
            // 
            this.buttonWorkFlow.Location = new System.Drawing.Point(695, 153);
            this.buttonWorkFlow.Name = "buttonWorkFlow";
            this.buttonWorkFlow.Size = new System.Drawing.Size(110, 35);
            this.buttonWorkFlow.TabIndex = 11;
            this.buttonWorkFlow.Text = "完整流程";
            this.buttonWorkFlow.UseVisualStyleBackColor = true;
            this.buttonWorkFlow.Click += new System.EventHandler(this.buttonWorkFlow_Click);
            // 
            // pointPosUCLoadPNPYSafeLimit
            // 
            this.pointPosUCLoadPNPYSafeLimit.Axis = null;
            this.pointPosUCLoadPNPYSafeLimit.BackColor = System.Drawing.Color.PaleGreen;
            this.pointPosUCLoadPNPYSafeLimit.BindingUC = this.motorUCLoadPNPY;
            this.pointPosUCLoadPNPYSafeLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPYSafeLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPYSafeLimit.Location = new System.Drawing.Point(3, 629);
            this.pointPosUCLoadPNPYSafeLimit.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPYSafeLimit.Name = "pointPosUCLoadPNPYSafeLimit";
            this.pointPosUCLoadPNPYSafeLimit.PointName = "SafeLimit";
            this.pointPosUCLoadPNPYSafeLimit.Size = new System.Drawing.Size(538, 59);
            this.pointPosUCLoadPNPYSafeLimit.TabIndex = 21;
            this.pointPosUCLoadPNPYSafeLimit.TextBoxPosLeft = 324;
            this.pointPosUCLoadPNPYSafeLimit.TextDisplay = "传送PNP安全限位";
            // 
            // motorUCLoadPNPY
            // 
            this.motorUCLoadPNPY.AutoSize = true;
            this.motorUCLoadPNPY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCLoadPNPY.Axis = null;
            this.motorUCLoadPNPY.AxisName = "机械手Y";
            this.motorUCLoadPNPY.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCLoadPNPY.HomeEnabled = true;
            this.motorUCLoadPNPY.Location = new System.Drawing.Point(5, 223);
            this.motorUCLoadPNPY.Name = "motorUCLoadPNPY";
            this.motorUCLoadPNPY.Size = new System.Drawing.Size(283, 190);
            this.motorUCLoadPNPY.TabIndex = 20;
            // 
            // pointPosUCLoadPNPYPick
            // 
            this.pointPosUCLoadPNPYPick.Axis = null;
            this.pointPosUCLoadPNPYPick.BackColor = System.Drawing.Color.PaleGreen;
            this.pointPosUCLoadPNPYPick.BindingUC = this.motorUCLoadPNPY;
            this.pointPosUCLoadPNPYPick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPYPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPYPick.Location = new System.Drawing.Point(3, 490);
            this.pointPosUCLoadPNPYPick.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPYPick.Name = "pointPosUCLoadPNPYPick";
            this.pointPosUCLoadPNPYPick.PointName = "Pick";
            this.pointPosUCLoadPNPYPick.Size = new System.Drawing.Size(491, 59);
            this.pointPosUCLoadPNPYPick.TabIndex = 23;
            this.pointPosUCLoadPNPYPick.TextBoxPosLeft = 277;
            this.pointPosUCLoadPNPYPick.TextDisplay = "取料位";
            // 
            // motorUCLoadPNPZ
            // 
            this.motorUCLoadPNPZ.AutoSize = true;
            this.motorUCLoadPNPZ.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCLoadPNPZ.Axis = null;
            this.motorUCLoadPNPZ.AxisName = "机械手Z";
            this.motorUCLoadPNPZ.BackColor = System.Drawing.Color.LightSeaGreen;
            this.motorUCLoadPNPZ.HomeEnabled = true;
            this.motorUCLoadPNPZ.Location = new System.Drawing.Point(506, 223);
            this.motorUCLoadPNPZ.Name = "motorUCLoadPNPZ";
            this.motorUCLoadPNPZ.Size = new System.Drawing.Size(283, 190);
            this.motorUCLoadPNPZ.TabIndex = 22;
            // 
            // pointPosUCLoadPNPYPlace
            // 
            this.pointPosUCLoadPNPYPlace.Axis = null;
            this.pointPosUCLoadPNPYPlace.BackColor = System.Drawing.Color.PaleGreen;
            this.pointPosUCLoadPNPYPlace.BindingUC = this.motorUCLoadPNPY;
            this.pointPosUCLoadPNPYPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPYPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPYPlace.Location = new System.Drawing.Point(3, 697);
            this.pointPosUCLoadPNPYPlace.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPYPlace.Name = "pointPosUCLoadPNPYPlace";
            this.pointPosUCLoadPNPYPlace.PointName = "Place";
            this.pointPosUCLoadPNPYPlace.Size = new System.Drawing.Size(491, 59);
            this.pointPosUCLoadPNPYPlace.TabIndex = 25;
            this.pointPosUCLoadPNPYPlace.TextBoxPosLeft = 277;
            this.pointPosUCLoadPNPYPlace.TextDisplay = "放料位";
            // 
            // pointPosUCLoadPNPYBuffer
            // 
            this.pointPosUCLoadPNPYBuffer.Axis = null;
            this.pointPosUCLoadPNPYBuffer.BackColor = System.Drawing.Color.PaleGreen;
            this.pointPosUCLoadPNPYBuffer.BindingUC = this.motorUCLoadPNPY;
            this.pointPosUCLoadPNPYBuffer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPYBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPYBuffer.Location = new System.Drawing.Point(3, 559);
            this.pointPosUCLoadPNPYBuffer.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPYBuffer.Name = "pointPosUCLoadPNPYBuffer";
            this.pointPosUCLoadPNPYBuffer.PointName = "Buffer";
            this.pointPosUCLoadPNPYBuffer.Size = new System.Drawing.Size(491, 59);
            this.pointPosUCLoadPNPYBuffer.TabIndex = 24;
            this.pointPosUCLoadPNPYBuffer.TextBoxPosLeft = 277;
            this.pointPosUCLoadPNPYBuffer.TextDisplay = "等待放料位";
            // 
            // pointPosUCLoadPNPZPlaceNG
            // 
            this.pointPosUCLoadPNPZPlaceNG.Axis = null;
            this.pointPosUCLoadPNPZPlaceNG.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCLoadPNPZPlaceNG.BindingUC = this.motorUCLoadPNPZ;
            this.pointPosUCLoadPNPZPlaceNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPZPlaceNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPZPlaceNG.Location = new System.Drawing.Point(504, 421);
            this.pointPosUCLoadPNPZPlaceNG.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPZPlaceNG.Name = "pointPosUCLoadPNPZPlaceNG";
            this.pointPosUCLoadPNPZPlaceNG.PointName = "PlaceNG";
            this.pointPosUCLoadPNPZPlaceNG.Size = new System.Drawing.Size(502, 59);
            this.pointPosUCLoadPNPZPlaceNG.TabIndex = 29;
            this.pointPosUCLoadPNPZPlaceNG.TextBoxPosLeft = 288;
            this.pointPosUCLoadPNPZPlaceNG.TextDisplay = "放NG料高度";
            // 
            // pointPosUCLoadPNPZPlace
            // 
            this.pointPosUCLoadPNPZPlace.Axis = null;
            this.pointPosUCLoadPNPZPlace.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCLoadPNPZPlace.BindingUC = this.motorUCLoadPNPZ;
            this.pointPosUCLoadPNPZPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPZPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPZPlace.Location = new System.Drawing.Point(504, 697);
            this.pointPosUCLoadPNPZPlace.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPZPlace.Name = "pointPosUCLoadPNPZPlace";
            this.pointPosUCLoadPNPZPlace.PointName = "Place";
            this.pointPosUCLoadPNPZPlace.Size = new System.Drawing.Size(502, 59);
            this.pointPosUCLoadPNPZPlace.TabIndex = 28;
            this.pointPosUCLoadPNPZPlace.TextBoxPosLeft = 288;
            this.pointPosUCLoadPNPZPlace.TextDisplay = "放料高度";
            // 
            // pointPosUCLoadPNPZPick
            // 
            this.pointPosUCLoadPNPZPick.Axis = null;
            this.pointPosUCLoadPNPZPick.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCLoadPNPZPick.BindingUC = this.motorUCLoadPNPZ;
            this.pointPosUCLoadPNPZPick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPZPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPZPick.Location = new System.Drawing.Point(504, 490);
            this.pointPosUCLoadPNPZPick.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPZPick.Name = "pointPosUCLoadPNPZPick";
            this.pointPosUCLoadPNPZPick.PointName = "Pick";
            this.pointPosUCLoadPNPZPick.Size = new System.Drawing.Size(502, 59);
            this.pointPosUCLoadPNPZPick.TabIndex = 27;
            this.pointPosUCLoadPNPZPick.TextBoxPosLeft = 288;
            this.pointPosUCLoadPNPZPick.TextDisplay = "取料高度";
            // 
            // pointPosUCLoadPNPYPlaceNG
            // 
            this.pointPosUCLoadPNPYPlaceNG.Axis = null;
            this.pointPosUCLoadPNPYPlaceNG.BackColor = System.Drawing.Color.PaleGreen;
            this.pointPosUCLoadPNPYPlaceNG.BindingUC = this.motorUCLoadPNPY;
            this.pointPosUCLoadPNPYPlaceNG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPYPlaceNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPYPlaceNG.Location = new System.Drawing.Point(3, 421);
            this.pointPosUCLoadPNPYPlaceNG.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPYPlaceNG.Name = "pointPosUCLoadPNPYPlaceNG";
            this.pointPosUCLoadPNPYPlaceNG.PointName = "PlaceNG";
            this.pointPosUCLoadPNPYPlaceNG.Size = new System.Drawing.Size(491, 59);
            this.pointPosUCLoadPNPYPlaceNG.TabIndex = 26;
            this.pointPosUCLoadPNPYPlaceNG.TextBoxPosLeft = 277;
            this.pointPosUCLoadPNPYPlaceNG.TextDisplay = "放NG料位";
            // 
            // pointPosUCLoadPNPZIdle
            // 
            this.pointPosUCLoadPNPZIdle.Axis = null;
            this.pointPosUCLoadPNPZIdle.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCLoadPNPZIdle.BindingUC = this.motorUCLoadPNPZ;
            this.pointPosUCLoadPNPZIdle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadPNPZIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadPNPZIdle.Location = new System.Drawing.Point(504, 559);
            this.pointPosUCLoadPNPZIdle.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadPNPZIdle.Name = "pointPosUCLoadPNPZIdle";
            this.pointPosUCLoadPNPZIdle.PointName = "Idle";
            this.pointPosUCLoadPNPZIdle.Size = new System.Drawing.Size(502, 59);
            this.pointPosUCLoadPNPZIdle.TabIndex = 21;
            this.pointPosUCLoadPNPZIdle.TextBoxPosLeft = 288;
            this.pointPosUCLoadPNPZIdle.TextDisplay = "安全高度";
            // 
            // ManualPanelZone上料机械手
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.pointPosUCLoadPNPZPlaceNG);
            this.Controls.Add(this.pointPosUCLoadPNPZPlace);
            this.Controls.Add(this.pointPosUCLoadPNPZPick);
            this.Controls.Add(this.pointPosUCLoadPNPYPlaceNG);
            this.Controls.Add(this.pointPosUCLoadPNPYPlace);
            this.Controls.Add(this.pointPosUCLoadPNPYBuffer);
            this.Controls.Add(this.pointPosUCLoadPNPYPick);
            this.Controls.Add(this.motorUCLoadPNPZ);
            this.Controls.Add(this.pointPosUCLoadPNPZIdle);
            this.Controls.Add(this.pointPosUCLoadPNPYSafeLimit);
            this.Controls.Add(this.motorUCLoadPNPY);
            this.Controls.Add(this.groupBoxNGBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonStartPlace);
            this.Controls.Add(this.buttonWorkFlow);
            this.Controls.Add(this.buttonStartNGPlace);
            this.Controls.Add(this.buttonStartPick);
            this.Name = "ManualPanelZone上料机械手";
            this.Size = new System.Drawing.Size(1423, 860);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.buttonStartPick, 0);
            this.Controls.SetChildIndex(this.buttonStartNGPlace, 0);
            this.Controls.SetChildIndex(this.buttonWorkFlow, 0);
            this.Controls.SetChildIndex(this.buttonStartPlace, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBoxNGBox, 0);
            this.Controls.SetChildIndex(this.motorUCLoadPNPY, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPYSafeLimit, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPZIdle, 0);
            this.Controls.SetChildIndex(this.motorUCLoadPNPZ, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPYPick, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPYBuffer, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPYPlace, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPYPlaceNG, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPZPick, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPZPlace, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadPNPZPlaceNG, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBoxNGBox.ResumeLayout(false);
            this.groupBoxNGBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStartPlace;
        private System.Windows.Forms.Button buttonStartNGPlace;
        private System.Windows.Forms.Button buttonStartPick;
        private System.Windows.Forms.GroupBox groupBox2;
        private DataDisp DataDispCellRight;
        private DataDisp DataDispCellMiddle;
        private DataDisp DataDispCellLeft;
        private System.Windows.Forms.GroupBox groupBoxNGBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelNGBox;
        private System.Windows.Forms.Label labelNGBoxFull;
        private System.Windows.Forms.Label labelNGBoxRightCount;
        private System.Windows.Forms.Label labelNGBoxMidCount;
        private System.Windows.Forms.Label labelNGBoxLeftCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonWorkFlow;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPYSafeLimit;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCLoadPNPY;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPYPick;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCLoadPNPZ;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPYPlace;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPYBuffer;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPZPlaceNG;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPZPlace;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPZPick;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPYPlaceNG;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadPNPZIdle;
    }
}
