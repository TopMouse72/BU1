namespace CDI.GUI
{
    partial class ManualPanelZone顶封边定位
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DataDispCellRight = new CDI.GUI.DataDisp();
            this.DataDispCellMiddle = new CDI.GUI.DataDisp();
            this.DataDispCellLeft = new CDI.GUI.DataDisp();
            this.buttonAlign = new System.Windows.Forms.Button();
            this.buttonRelease = new System.Windows.Forms.Button();
            this.buttonClamp = new System.Windows.Forms.Button();
            this.buttonWorkFlow = new System.Windows.Forms.Button();
            this.motorUCTopAlignBottom = new Colibri.CommonModule.ToolBox.MotorUC();
            this.motorUCTopAlignSide = new Colibri.CommonModule.ToolBox.MotorUC();
            this.motorUCTopAlignXSide = new Colibri.CommonModule.ToolBox.MotorUC();
            this.motorUCTopAlignZClamp = new Colibri.CommonModule.ToolBox.MotorUC();
            this.motorUCTopAlignTop = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCTopAlignZClampRelease = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignSideClamp = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignSideRelease = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignZClampClamp = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignBottomClamp = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignBottomRelease = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignTopRelease = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignXSideIdle = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTopAlignTopClamp = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.buttonCylinderTest = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 144);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(1020, 54);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(1020, 95);
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
            this.groupBox2.TabIndex = 14;
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
            // buttonAlign
            // 
            this.buttonAlign.Location = new System.Drawing.Point(677, 13);
            this.buttonAlign.Name = "buttonAlign";
            this.buttonAlign.Size = new System.Drawing.Size(110, 35);
            this.buttonAlign.TabIndex = 15;
            this.buttonAlign.Text = "物料对齐";
            this.buttonAlign.UseVisualStyleBackColor = true;
            this.buttonAlign.Click += new System.EventHandler(this.buttonAlign_Click);
            // 
            // buttonRelease
            // 
            this.buttonRelease.Location = new System.Drawing.Point(677, 95);
            this.buttonRelease.Name = "buttonRelease";
            this.buttonRelease.Size = new System.Drawing.Size(110, 35);
            this.buttonRelease.TabIndex = 15;
            this.buttonRelease.Text = "物料松开";
            this.buttonRelease.UseVisualStyleBackColor = true;
            this.buttonRelease.Click += new System.EventHandler(this.buttonRelease_Click);
            // 
            // buttonClamp
            // 
            this.buttonClamp.Location = new System.Drawing.Point(618, 54);
            this.buttonClamp.Name = "buttonClamp";
            this.buttonClamp.Size = new System.Drawing.Size(110, 35);
            this.buttonClamp.TabIndex = 16;
            this.buttonClamp.Text = "夹物料";
            this.buttonClamp.UseVisualStyleBackColor = true;
            this.buttonClamp.Click += new System.EventHandler(this.buttonClamp_Click);
            // 
            // buttonWorkFlow
            // 
            this.buttonWorkFlow.Location = new System.Drawing.Point(849, 95);
            this.buttonWorkFlow.Name = "buttonWorkFlow";
            this.buttonWorkFlow.Size = new System.Drawing.Size(110, 35);
            this.buttonWorkFlow.TabIndex = 17;
            this.buttonWorkFlow.Text = "完整流程";
            this.buttonWorkFlow.UseVisualStyleBackColor = true;
            this.buttonWorkFlow.Click += new System.EventHandler(this.buttonWorkFlow_Click);
            // 
            // motorUCTopAlignBottom
            // 
            this.motorUCTopAlignBottom.AutoSize = true;
            this.motorUCTopAlignBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTopAlignBottom.Axis = null;
            this.motorUCTopAlignBottom.AxisName = "尾推";
            this.motorUCTopAlignBottom.BackColor = System.Drawing.Color.CornflowerBlue;
            this.motorUCTopAlignBottom.HomeEnabled = true;
            this.motorUCTopAlignBottom.Location = new System.Drawing.Point(487, 509);
            this.motorUCTopAlignBottom.Name = "motorUCTopAlignBottom";
            this.motorUCTopAlignBottom.Size = new System.Drawing.Size(283, 190);
            this.motorUCTopAlignBottom.TabIndex = 24;
            // 
            // motorUCTopAlignSide
            // 
            this.motorUCTopAlignSide.AutoSize = true;
            this.motorUCTopAlignSide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTopAlignSide.Axis = null;
            this.motorUCTopAlignSide.AxisName = "侧推";
            this.motorUCTopAlignSide.BackColor = System.Drawing.Color.LightBlue;
            this.motorUCTopAlignSide.HomeEnabled = true;
            this.motorUCTopAlignSide.Location = new System.Drawing.Point(3, 509);
            this.motorUCTopAlignSide.Name = "motorUCTopAlignSide";
            this.motorUCTopAlignSide.Size = new System.Drawing.Size(283, 190);
            this.motorUCTopAlignSide.TabIndex = 28;
            // 
            // motorUCTopAlignXSide
            // 
            this.motorUCTopAlignXSide.AutoSize = true;
            this.motorUCTopAlignXSide.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTopAlignXSide.Axis = null;
            this.motorUCTopAlignXSide.AxisName = "顶部X";
            this.motorUCTopAlignXSide.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCTopAlignXSide.HomeEnabled = true;
            this.motorUCTopAlignXSide.Location = new System.Drawing.Point(3, 172);
            this.motorUCTopAlignXSide.Name = "motorUCTopAlignXSide";
            this.motorUCTopAlignXSide.Size = new System.Drawing.Size(283, 190);
            this.motorUCTopAlignXSide.TabIndex = 29;
            // 
            // motorUCTopAlignZClamp
            // 
            this.motorUCTopAlignZClamp.AutoSize = true;
            this.motorUCTopAlignZClamp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTopAlignZClamp.Axis = null;
            this.motorUCTopAlignZClamp.AxisName = "夹紧";
            this.motorUCTopAlignZClamp.BackColor = System.Drawing.Color.LightSeaGreen;
            this.motorUCTopAlignZClamp.HomeEnabled = true;
            this.motorUCTopAlignZClamp.Location = new System.Drawing.Point(946, 172);
            this.motorUCTopAlignZClamp.Name = "motorUCTopAlignZClamp";
            this.motorUCTopAlignZClamp.Size = new System.Drawing.Size(283, 190);
            this.motorUCTopAlignZClamp.TabIndex = 30;
            // 
            // motorUCTopAlignTop
            // 
            this.motorUCTopAlignTop.AutoSize = true;
            this.motorUCTopAlignTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTopAlignTop.Axis = null;
            this.motorUCTopAlignTop.AxisName = "顶部Y";
            this.motorUCTopAlignTop.BackColor = System.Drawing.Color.Khaki;
            this.motorUCTopAlignTop.HomeEnabled = true;
            this.motorUCTopAlignTop.Location = new System.Drawing.Point(487, 172);
            this.motorUCTopAlignTop.Name = "motorUCTopAlignTop";
            this.motorUCTopAlignTop.Size = new System.Drawing.Size(283, 190);
            this.motorUCTopAlignTop.TabIndex = 31;
            // 
            // pointPosUCTopAlignZClampRelease
            // 
            this.pointPosUCTopAlignZClampRelease.Axis = null;
            this.pointPosUCTopAlignZClampRelease.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCTopAlignZClampRelease.BindingUC = this.motorUCTopAlignZClamp;
            this.pointPosUCTopAlignZClampRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignZClampRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignZClampRelease.Location = new System.Drawing.Point(946, 370);
            this.pointPosUCTopAlignZClampRelease.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignZClampRelease.Name = "pointPosUCTopAlignZClampRelease";
            this.pointPosUCTopAlignZClampRelease.PointName = "Release";
            this.pointPosUCTopAlignZClampRelease.Size = new System.Drawing.Size(447, 61);
            this.pointPosUCTopAlignZClampRelease.TabIndex = 32;
            this.pointPosUCTopAlignZClampRelease.TextBoxPosLeft = 233;
            this.pointPosUCTopAlignZClampRelease.TextDisplay = "松开";
            // 
            // pointPosUCTopAlignSideClamp
            // 
            this.pointPosUCTopAlignSideClamp.Axis = null;
            this.pointPosUCTopAlignSideClamp.BackColor = System.Drawing.Color.LightBlue;
            this.pointPosUCTopAlignSideClamp.BindingUC = this.motorUCTopAlignSide;
            this.pointPosUCTopAlignSideClamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignSideClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignSideClamp.Location = new System.Drawing.Point(0, 776);
            this.pointPosUCTopAlignSideClamp.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignSideClamp.Name = "pointPosUCTopAlignSideClamp";
            this.pointPosUCTopAlignSideClamp.PointName = "Clamp";
            this.pointPosUCTopAlignSideClamp.Size = new System.Drawing.Size(477, 61);
            this.pointPosUCTopAlignSideClamp.TabIndex = 33;
            this.pointPosUCTopAlignSideClamp.TextBoxPosLeft = 263;
            this.pointPosUCTopAlignSideClamp.TextDisplay = "基准夹紧位";
            // 
            // pointPosUCTopAlignSideRelease
            // 
            this.pointPosUCTopAlignSideRelease.Axis = null;
            this.pointPosUCTopAlignSideRelease.BackColor = System.Drawing.Color.LightBlue;
            this.pointPosUCTopAlignSideRelease.BindingUC = this.motorUCTopAlignSide;
            this.pointPosUCTopAlignSideRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignSideRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignSideRelease.Location = new System.Drawing.Point(0, 707);
            this.pointPosUCTopAlignSideRelease.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignSideRelease.Name = "pointPosUCTopAlignSideRelease";
            this.pointPosUCTopAlignSideRelease.PointName = "Release";
            this.pointPosUCTopAlignSideRelease.Size = new System.Drawing.Size(477, 61);
            this.pointPosUCTopAlignSideRelease.TabIndex = 34;
            this.pointPosUCTopAlignSideRelease.TextBoxPosLeft = 263;
            this.pointPosUCTopAlignSideRelease.TextDisplay = "松开";
            // 
            // pointPosUCTopAlignZClampClamp
            // 
            this.pointPosUCTopAlignZClampClamp.Axis = null;
            this.pointPosUCTopAlignZClampClamp.BackColor = System.Drawing.Color.LightSeaGreen;
            this.pointPosUCTopAlignZClampClamp.BindingUC = this.motorUCTopAlignZClamp;
            this.pointPosUCTopAlignZClampClamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignZClampClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignZClampClamp.Location = new System.Drawing.Point(946, 440);
            this.pointPosUCTopAlignZClampClamp.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignZClampClamp.Name = "pointPosUCTopAlignZClampClamp";
            this.pointPosUCTopAlignZClampClamp.PointName = "Clamp";
            this.pointPosUCTopAlignZClampClamp.Size = new System.Drawing.Size(447, 61);
            this.pointPosUCTopAlignZClampClamp.TabIndex = 35;
            this.pointPosUCTopAlignZClampClamp.TextBoxPosLeft = 233;
            this.pointPosUCTopAlignZClampClamp.TextDisplay = "平整";
            // 
            // pointPosUCTopAlignBottomClamp
            // 
            this.pointPosUCTopAlignBottomClamp.Axis = null;
            this.pointPosUCTopAlignBottomClamp.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pointPosUCTopAlignBottomClamp.BindingUC = this.motorUCTopAlignBottom;
            this.pointPosUCTopAlignBottomClamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignBottomClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignBottomClamp.Location = new System.Drawing.Point(487, 778);
            this.pointPosUCTopAlignBottomClamp.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignBottomClamp.Name = "pointPosUCTopAlignBottomClamp";
            this.pointPosUCTopAlignBottomClamp.PointName = "Clamp";
            this.pointPosUCTopAlignBottomClamp.Size = new System.Drawing.Size(472, 61);
            this.pointPosUCTopAlignBottomClamp.TabIndex = 36;
            this.pointPosUCTopAlignBottomClamp.TextBoxPosLeft = 258;
            this.pointPosUCTopAlignBottomClamp.TextDisplay = "基准夹紧位";
            // 
            // pointPosUCTopAlignBottomRelease
            // 
            this.pointPosUCTopAlignBottomRelease.Axis = null;
            this.pointPosUCTopAlignBottomRelease.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pointPosUCTopAlignBottomRelease.BindingUC = this.motorUCTopAlignBottom;
            this.pointPosUCTopAlignBottomRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignBottomRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignBottomRelease.Location = new System.Drawing.Point(487, 707);
            this.pointPosUCTopAlignBottomRelease.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignBottomRelease.Name = "pointPosUCTopAlignBottomRelease";
            this.pointPosUCTopAlignBottomRelease.PointName = "Release";
            this.pointPosUCTopAlignBottomRelease.Size = new System.Drawing.Size(472, 61);
            this.pointPosUCTopAlignBottomRelease.TabIndex = 37;
            this.pointPosUCTopAlignBottomRelease.TextBoxPosLeft = 258;
            this.pointPosUCTopAlignBottomRelease.TextDisplay = "松开";
            // 
            // pointPosUCTopAlignTopRelease
            // 
            this.pointPosUCTopAlignTopRelease.Axis = null;
            this.pointPosUCTopAlignTopRelease.BackColor = System.Drawing.Color.Khaki;
            this.pointPosUCTopAlignTopRelease.BindingUC = this.motorUCTopAlignTop;
            this.pointPosUCTopAlignTopRelease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignTopRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignTopRelease.Location = new System.Drawing.Point(487, 370);
            this.pointPosUCTopAlignTopRelease.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignTopRelease.Name = "pointPosUCTopAlignTopRelease";
            this.pointPosUCTopAlignTopRelease.PointName = "Release";
            this.pointPosUCTopAlignTopRelease.Size = new System.Drawing.Size(449, 61);
            this.pointPosUCTopAlignTopRelease.TabIndex = 38;
            this.pointPosUCTopAlignTopRelease.TextBoxPosLeft = 235;
            this.pointPosUCTopAlignTopRelease.TextDisplay = "移开";
            // 
            // pointPosUCTopAlignXSideIdle
            // 
            this.pointPosUCTopAlignXSideIdle.Axis = null;
            this.pointPosUCTopAlignXSideIdle.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCTopAlignXSideIdle.BindingUC = this.motorUCTopAlignXSide;
            this.pointPosUCTopAlignXSideIdle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignXSideIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignXSideIdle.Location = new System.Drawing.Point(0, 370);
            this.pointPosUCTopAlignXSideIdle.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignXSideIdle.Name = "pointPosUCTopAlignXSideIdle";
            this.pointPosUCTopAlignXSideIdle.PointName = "Idle";
            this.pointPosUCTopAlignXSideIdle.Size = new System.Drawing.Size(477, 61);
            this.pointPosUCTopAlignXSideIdle.TabIndex = 40;
            this.pointPosUCTopAlignXSideIdle.TextBoxPosLeft = 263;
            this.pointPosUCTopAlignXSideIdle.TextDisplay = "平整起始位";
            // 
            // pointPosUCTopAlignTopClamp
            // 
            this.pointPosUCTopAlignTopClamp.Axis = null;
            this.pointPosUCTopAlignTopClamp.BackColor = System.Drawing.Color.Khaki;
            this.pointPosUCTopAlignTopClamp.BindingUC = this.motorUCTopAlignTop;
            this.pointPosUCTopAlignTopClamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTopAlignTopClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTopAlignTopClamp.Location = new System.Drawing.Point(487, 440);
            this.pointPosUCTopAlignTopClamp.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTopAlignTopClamp.Name = "pointPosUCTopAlignTopClamp";
            this.pointPosUCTopAlignTopClamp.PointName = "Clamp";
            this.pointPosUCTopAlignTopClamp.Size = new System.Drawing.Size(449, 61);
            this.pointPosUCTopAlignTopClamp.TabIndex = 41;
            this.pointPosUCTopAlignTopClamp.TextBoxPosLeft = 235;
            this.pointPosUCTopAlignTopClamp.TextDisplay = "平整位";
            // 
            // buttonCylinderTest
            // 
            this.buttonCylinderTest.Location = new System.Drawing.Point(734, 54);
            this.buttonCylinderTest.Name = "buttonCylinderTest";
            this.buttonCylinderTest.Size = new System.Drawing.Size(110, 35);
            this.buttonCylinderTest.TabIndex = 42;
            this.buttonCylinderTest.Text = "气缸测试";
            this.buttonCylinderTest.UseVisualStyleBackColor = true;
            this.buttonCylinderTest.Click += new System.EventHandler(this.buttonCylinderTest_Click);
            // 
            // ManualPanelZone顶封边定位
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.buttonCylinderTest);
            this.Controls.Add(this.pointPosUCTopAlignTopClamp);
            this.Controls.Add(this.pointPosUCTopAlignTopRelease);
            this.Controls.Add(this.pointPosUCTopAlignXSideIdle);
            this.Controls.Add(this.pointPosUCTopAlignZClampClamp);
            this.Controls.Add(this.pointPosUCTopAlignBottomClamp);
            this.Controls.Add(this.pointPosUCTopAlignBottomRelease);
            this.Controls.Add(this.pointPosUCTopAlignZClampRelease);
            this.Controls.Add(this.pointPosUCTopAlignSideClamp);
            this.Controls.Add(this.pointPosUCTopAlignSideRelease);
            this.Controls.Add(this.motorUCTopAlignTop);
            this.Controls.Add(this.motorUCTopAlignZClamp);
            this.Controls.Add(this.motorUCTopAlignXSide);
            this.Controls.Add(this.motorUCTopAlignSide);
            this.Controls.Add(this.motorUCTopAlignBottom);
            this.Controls.Add(this.buttonWorkFlow);
            this.Controls.Add(this.buttonClamp);
            this.Controls.Add(this.buttonRelease);
            this.Controls.Add(this.buttonAlign);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualPanelZone顶封边定位";
            this.Size = new System.Drawing.Size(1464, 1017);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.buttonAlign, 0);
            this.Controls.SetChildIndex(this.buttonRelease, 0);
            this.Controls.SetChildIndex(this.buttonClamp, 0);
            this.Controls.SetChildIndex(this.buttonWorkFlow, 0);
            this.Controls.SetChildIndex(this.motorUCTopAlignBottom, 0);
            this.Controls.SetChildIndex(this.motorUCTopAlignSide, 0);
            this.Controls.SetChildIndex(this.motorUCTopAlignXSide, 0);
            this.Controls.SetChildIndex(this.motorUCTopAlignZClamp, 0);
            this.Controls.SetChildIndex(this.motorUCTopAlignTop, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignSideRelease, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignSideClamp, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignZClampRelease, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignBottomRelease, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignBottomClamp, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignZClampClamp, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignXSideIdle, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignTopRelease, 0);
            this.Controls.SetChildIndex(this.pointPosUCTopAlignTopClamp, 0);
            this.Controls.SetChildIndex(this.buttonCylinderTest, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DataDisp DataDispCellRight;
        private DataDisp DataDispCellMiddle;
        private DataDisp DataDispCellLeft;
        private System.Windows.Forms.Button buttonAlign;
        private System.Windows.Forms.Button buttonRelease;
        private System.Windows.Forms.Button buttonClamp;
        private System.Windows.Forms.Button buttonWorkFlow;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTopAlignBottom;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTopAlignSide;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTopAlignXSide;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTopAlignZClamp;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTopAlignTop;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignZClampRelease;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignSideClamp;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignSideRelease;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignZClampClamp;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignBottomClamp;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignBottomRelease;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignTopRelease;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignXSideIdle;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTopAlignTopClamp;
        protected System.Windows.Forms.Button buttonCylinderTest;
    }
}
