namespace CDI.GUI
{
    partial class ManualPanelZone厚度测量
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
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonReadData = new System.Windows.Forms.Button();
            this.buttonUnload = new System.Windows.Forms.Button();
            this.buttonMeas = new System.Windows.Forms.Button();
            this.buttonCyDown = new System.Windows.Forms.Button();
            this.buttonCyUp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxReferenceRight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxReferenceMid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxReferenceLeft = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDelayTime = new System.Windows.Forms.TextBox();
            this.DataDispCellLeft = new CDI.GUI.DataDisp();
            this.DataDispCellMiddle = new CDI.GUI.DataDisp();
            this.DataDispCellRight = new CDI.GUI.DataDisp();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pointPosUCThicknessMeasYMeas = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCThicknessMeasY = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCThicknessMeasYIdle = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.buttonCylinderTest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 189);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(618, 141);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(734, 141);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(918, 17);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(110, 35);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "进测量位";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonReadData
            // 
            this.buttonReadData.Location = new System.Drawing.Point(864, 100);
            this.buttonReadData.Name = "buttonReadData";
            this.buttonReadData.Size = new System.Drawing.Size(110, 35);
            this.buttonReadData.TabIndex = 5;
            this.buttonReadData.Text = "读数据";
            this.buttonReadData.UseVisualStyleBackColor = true;
            this.buttonReadData.Click += new System.EventHandler(this.buttonReadData_Click);
            // 
            // buttonUnload
            // 
            this.buttonUnload.Location = new System.Drawing.Point(918, 182);
            this.buttonUnload.Name = "buttonUnload";
            this.buttonUnload.Size = new System.Drawing.Size(110, 35);
            this.buttonUnload.TabIndex = 6;
            this.buttonUnload.Text = "出测量位";
            this.buttonUnload.UseVisualStyleBackColor = true;
            this.buttonUnload.Click += new System.EventHandler(this.buttonUnload_Click);
            // 
            // buttonMeas
            // 
            this.buttonMeas.Location = new System.Drawing.Point(918, 245);
            this.buttonMeas.Name = "buttonMeas";
            this.buttonMeas.Size = new System.Drawing.Size(110, 35);
            this.buttonMeas.TabIndex = 7;
            this.buttonMeas.Text = "完整流程";
            this.buttonMeas.UseVisualStyleBackColor = true;
            this.buttonMeas.Click += new System.EventHandler(this.buttonMeas_Click);
            // 
            // buttonCyDown
            // 
            this.buttonCyDown.Location = new System.Drawing.Point(864, 59);
            this.buttonCyDown.Name = "buttonCyDown";
            this.buttonCyDown.Size = new System.Drawing.Size(110, 35);
            this.buttonCyDown.TabIndex = 9;
            this.buttonCyDown.Text = "气缸下";
            this.buttonCyDown.UseVisualStyleBackColor = true;
            this.buttonCyDown.Click += new System.EventHandler(this.buttonCyDown_Click);
            // 
            // buttonCyUp
            // 
            this.buttonCyUp.Location = new System.Drawing.Point(864, 141);
            this.buttonCyUp.Name = "buttonCyUp";
            this.buttonCyUp.Size = new System.Drawing.Size(110, 35);
            this.buttonCyUp.TabIndex = 8;
            this.buttonCyUp.Text = "气缸上";
            this.buttonCyUp.UseVisualStyleBackColor = true;
            this.buttonCyUp.Click += new System.EventHandler(this.buttonCyUp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxReferenceRight);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxReferenceMid);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxReferenceLeft);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxDelayTime);
            this.groupBox1.Location = new System.Drawing.Point(618, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 132);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数（只读）";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(135, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "毫米";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "右基准值";
            // 
            // textBoxReferenceRight
            // 
            this.textBoxReferenceRight.BackColor = System.Drawing.Color.YellowGreen;
            this.textBoxReferenceRight.Location = new System.Drawing.Point(70, 104);
            this.textBoxReferenceRight.Name = "textBoxReferenceRight";
            this.textBoxReferenceRight.ReadOnly = true;
            this.textBoxReferenceRight.Size = new System.Drawing.Size(59, 20);
            this.textBoxReferenceRight.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "毫米";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "中基准值";
            // 
            // textBoxReferenceMid
            // 
            this.textBoxReferenceMid.BackColor = System.Drawing.Color.YellowGreen;
            this.textBoxReferenceMid.Location = new System.Drawing.Point(70, 78);
            this.textBoxReferenceMid.Name = "textBoxReferenceMid";
            this.textBoxReferenceMid.ReadOnly = true;
            this.textBoxReferenceMid.Size = new System.Drawing.Size(59, 20);
            this.textBoxReferenceMid.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "毫米";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "左基准值";
            // 
            // textBoxReferenceLeft
            // 
            this.textBoxReferenceLeft.BackColor = System.Drawing.Color.YellowGreen;
            this.textBoxReferenceLeft.Location = new System.Drawing.Point(70, 52);
            this.textBoxReferenceLeft.Name = "textBoxReferenceLeft";
            this.textBoxReferenceLeft.ReadOnly = true;
            this.textBoxReferenceLeft.Size = new System.Drawing.Size(59, 20);
            this.textBoxReferenceLeft.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "毫秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "测量延时";
            // 
            // textBoxDelayTime
            // 
            this.textBoxDelayTime.BackColor = System.Drawing.Color.YellowGreen;
            this.textBoxDelayTime.Location = new System.Drawing.Point(70, 20);
            this.textBoxDelayTime.Name = "textBoxDelayTime";
            this.textBoxDelayTime.ReadOnly = true;
            this.textBoxDelayTime.Size = new System.Drawing.Size(59, 20);
            this.textBoxDelayTime.TabIndex = 4;
            // 
            // DataDispCellLeft
            // 
            this.DataDispCellLeft.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellLeft.DataStation = null;
            this.DataDispCellLeft.Location = new System.Drawing.Point(6, 19);
            this.DataDispCellLeft.Name = "DataDispCellLeft";
            this.DataDispCellLeft.Size = new System.Drawing.Size(195, 141);
            this.DataDispCellLeft.TabIndex = 5;
            this.DataDispCellLeft.Text = "左电芯";
            // 
            // DataDispCellMiddle
            // 
            this.DataDispCellMiddle.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellMiddle.DataStation = null;
            this.DataDispCellMiddle.Location = new System.Drawing.Point(207, 19);
            this.DataDispCellMiddle.Name = "DataDispCellMiddle";
            this.DataDispCellMiddle.Size = new System.Drawing.Size(195, 141);
            this.DataDispCellMiddle.TabIndex = 4;
            this.DataDispCellMiddle.Text = "中电芯";
            // 
            // DataDispCellRight
            // 
            this.DataDispCellRight.BackColor = System.Drawing.Color.MediumAquamarine;
            this.DataDispCellRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataDispCellRight.DataStation = null;
            this.DataDispCellRight.Location = new System.Drawing.Point(408, 19);
            this.DataDispCellRight.Name = "DataDispCellRight";
            this.DataDispCellRight.Size = new System.Drawing.Size(195, 141);
            this.DataDispCellRight.TabIndex = 3;
            this.DataDispCellRight.Text = "右电芯";
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
            this.groupBox2.Size = new System.Drawing.Size(609, 179);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电芯";
            // 
            // pointPosUCThicknessMeasYMeas
            // 
            this.pointPosUCThicknessMeasYMeas.Axis = null;
            this.pointPosUCThicknessMeasYMeas.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCThicknessMeasYMeas.BindingUC = this.motorUCThicknessMeasY;
            this.pointPosUCThicknessMeasYMeas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCThicknessMeasYMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCThicknessMeasYMeas.Location = new System.Drawing.Point(294, 290);
            this.pointPosUCThicknessMeasYMeas.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCThicknessMeasYMeas.Name = "pointPosUCThicknessMeasYMeas";
            this.pointPosUCThicknessMeasYMeas.PointName = "Meas";
            this.pointPosUCThicknessMeasYMeas.Size = new System.Drawing.Size(453, 61);
            this.pointPosUCThicknessMeasYMeas.TabIndex = 17;
            this.pointPosUCThicknessMeasYMeas.TextBoxPosLeft = 239;
            this.pointPosUCThicknessMeasYMeas.TextDisplay = "测量位";
            // 
            // motorUCThicknessMeasY
            // 
            this.motorUCThicknessMeasY.AutoSize = true;
            this.motorUCThicknessMeasY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCThicknessMeasY.Axis = null;
            this.motorUCThicknessMeasY.AxisName = "厚度Y";
            this.motorUCThicknessMeasY.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCThicknessMeasY.HomeEnabled = true;
            this.motorUCThicknessMeasY.Location = new System.Drawing.Point(3, 219);
            this.motorUCThicknessMeasY.Name = "motorUCThicknessMeasY";
            this.motorUCThicknessMeasY.Size = new System.Drawing.Size(283, 190);
            this.motorUCThicknessMeasY.TabIndex = 16;
            // 
            // pointPosUCThicknessMeasYIdle
            // 
            this.pointPosUCThicknessMeasYIdle.Axis = null;
            this.pointPosUCThicknessMeasYIdle.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCThicknessMeasYIdle.BindingUC = this.motorUCThicknessMeasY;
            this.pointPosUCThicknessMeasYIdle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCThicknessMeasYIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCThicknessMeasYIdle.Location = new System.Drawing.Point(294, 219);
            this.pointPosUCThicknessMeasYIdle.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCThicknessMeasYIdle.Name = "pointPosUCThicknessMeasYIdle";
            this.pointPosUCThicknessMeasYIdle.PointName = "Idle";
            this.pointPosUCThicknessMeasYIdle.Size = new System.Drawing.Size(453, 61);
            this.pointPosUCThicknessMeasYIdle.TabIndex = 18;
            this.pointPosUCThicknessMeasYIdle.TextBoxPosLeft = 239;
            this.pointPosUCThicknessMeasYIdle.TextDisplay = "放料位";
            // 
            // buttonCylinderTest
            // 
            this.buttonCylinderTest.Location = new System.Drawing.Point(980, 99);
            this.buttonCylinderTest.Name = "buttonCylinderTest";
            this.buttonCylinderTest.Size = new System.Drawing.Size(110, 35);
            this.buttonCylinderTest.TabIndex = 7;
            this.buttonCylinderTest.Text = "气缸测试";
            this.buttonCylinderTest.UseVisualStyleBackColor = true;
            this.buttonCylinderTest.Click += new System.EventHandler(this.buttonCylinderTest_Click);
            // 
            // ManualPanelZone厚度测量
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.pointPosUCThicknessMeasYMeas);
            this.Controls.Add(this.pointPosUCThicknessMeasYIdle);
            this.Controls.Add(this.motorUCThicknessMeasY);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCyDown);
            this.Controls.Add(this.buttonCyUp);
            this.Controls.Add(this.buttonCylinderTest);
            this.Controls.Add(this.buttonMeas);
            this.Controls.Add(this.buttonUnload);
            this.Controls.Add(this.buttonReadData);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualPanelZone厚度测量";
            this.Size = new System.Drawing.Size(1297, 479);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.buttonLoad, 0);
            this.Controls.SetChildIndex(this.buttonReadData, 0);
            this.Controls.SetChildIndex(this.buttonUnload, 0);
            this.Controls.SetChildIndex(this.buttonMeas, 0);
            this.Controls.SetChildIndex(this.buttonCylinderTest, 0);
            this.Controls.SetChildIndex(this.buttonCyUp, 0);
            this.Controls.SetChildIndex(this.buttonCyDown, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.motorUCThicknessMeasY, 0);
            this.Controls.SetChildIndex(this.pointPosUCThicknessMeasYIdle, 0);
            this.Controls.SetChildIndex(this.pointPosUCThicknessMeasYMeas, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Button buttonLoad;
        protected System.Windows.Forms.Button buttonReadData;
        protected System.Windows.Forms.Button buttonUnload;
        protected System.Windows.Forms.Button buttonMeas;
        protected System.Windows.Forms.Button buttonCyDown;
        protected System.Windows.Forms.Button buttonCyUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDelayTime;
        private DataDisp DataDispCellLeft;
        private DataDisp DataDispCellMiddle;
        private DataDisp DataDispCellRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxReferenceRight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxReferenceMid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxReferenceLeft;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCThicknessMeasYMeas;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCThicknessMeasYIdle;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCThicknessMeasY;
        protected System.Windows.Forms.Button buttonCylinderTest;
    }
}
