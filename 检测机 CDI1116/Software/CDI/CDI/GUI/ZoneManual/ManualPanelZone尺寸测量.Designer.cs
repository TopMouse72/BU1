namespace CDI.GUI
{
    partial class ManualPanelZone尺寸测量
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
            this.buttonMeas = new System.Windows.Forms.Button();
            this.buttonMoveToGetPart = new System.Windows.Forms.Button();
            this.buttonMeasOnce = new System.Windows.Forms.Button();
            this.buttonMoveToLeftCell = new System.Windows.Forms.Button();
            this.buttonMoveToRightCell = new System.Windows.Forms.Button();
            this.buttonMoveToMidCell = new System.Windows.Forms.Button();
            this.buttonSnapShot = new System.Windows.Forms.Button();
            this.pointPosUCOutlineMeasXPartOut = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCOutlineMeasX = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCOutlineMeasXPitch = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCOutlineMeasXStart = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCOutlineMeasXGetPart = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.buttonMoveToPickPart = new System.Windows.Forms.Button();
            this.buttonCDIVision = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 400);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(618, 22);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(734, 23);
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
            this.groupBox2.Size = new System.Drawing.Size(609, 394);
            this.groupBox2.TabIndex = 3;
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
            this.DataDispCellRight.Size = new System.Drawing.Size(195, 356);
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
            this.DataDispCellMiddle.Size = new System.Drawing.Size(195, 356);
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
            this.DataDispCellLeft.Size = new System.Drawing.Size(195, 356);
            this.DataDispCellLeft.TabIndex = 5;
            this.DataDispCellLeft.Text = "左电芯";
            // 
            // buttonMeas
            // 
            this.buttonMeas.Location = new System.Drawing.Point(618, 341);
            this.buttonMeas.Name = "buttonMeas";
            this.buttonMeas.Size = new System.Drawing.Size(110, 35);
            this.buttonMeas.TabIndex = 6;
            this.buttonMeas.Text = "完整流程";
            this.buttonMeas.UseVisualStyleBackColor = true;
            this.buttonMeas.Click += new System.EventHandler(this.buttonMeas_Click);
            // 
            // buttonMoveToGetPart
            // 
            this.buttonMoveToGetPart.Location = new System.Drawing.Point(734, 104);
            this.buttonMoveToGetPart.Name = "buttonMoveToGetPart";
            this.buttonMoveToGetPart.Size = new System.Drawing.Size(110, 35);
            this.buttonMoveToGetPart.TabIndex = 6;
            this.buttonMoveToGetPart.Text = "移到上料位";
            this.buttonMoveToGetPart.UseVisualStyleBackColor = true;
            this.buttonMoveToGetPart.Click += new System.EventHandler(this.buttonMoveToGetPart_Click);
            // 
            // buttonMeasOnce
            // 
            this.buttonMeasOnce.Location = new System.Drawing.Point(734, 186);
            this.buttonMeasOnce.Name = "buttonMeasOnce";
            this.buttonMeasOnce.Size = new System.Drawing.Size(110, 35);
            this.buttonMeasOnce.TabIndex = 6;
            this.buttonMeasOnce.Text = "单次测量";
            this.buttonMeasOnce.UseVisualStyleBackColor = true;
            this.buttonMeasOnce.Click += new System.EventHandler(this.buttonMeasOnce_Click);
            // 
            // buttonMoveToLeftCell
            // 
            this.buttonMoveToLeftCell.Location = new System.Drawing.Point(618, 145);
            this.buttonMoveToLeftCell.Name = "buttonMoveToLeftCell";
            this.buttonMoveToLeftCell.Size = new System.Drawing.Size(110, 35);
            this.buttonMoveToLeftCell.TabIndex = 6;
            this.buttonMoveToLeftCell.Text = "移到左电芯";
            this.buttonMoveToLeftCell.UseVisualStyleBackColor = true;
            this.buttonMoveToLeftCell.Click += new System.EventHandler(this.buttonMoveToLeftCell_Click);
            // 
            // buttonMoveToRightCell
            // 
            this.buttonMoveToRightCell.Location = new System.Drawing.Point(850, 145);
            this.buttonMoveToRightCell.Name = "buttonMoveToRightCell";
            this.buttonMoveToRightCell.Size = new System.Drawing.Size(110, 35);
            this.buttonMoveToRightCell.TabIndex = 7;
            this.buttonMoveToRightCell.Text = "移到右电芯";
            this.buttonMoveToRightCell.UseVisualStyleBackColor = true;
            this.buttonMoveToRightCell.Click += new System.EventHandler(this.buttonMoveToRightCell_Click);
            // 
            // buttonMoveToMidCell
            // 
            this.buttonMoveToMidCell.Location = new System.Drawing.Point(734, 145);
            this.buttonMoveToMidCell.Name = "buttonMoveToMidCell";
            this.buttonMoveToMidCell.Size = new System.Drawing.Size(110, 35);
            this.buttonMoveToMidCell.TabIndex = 8;
            this.buttonMoveToMidCell.Text = "移到中电芯";
            this.buttonMoveToMidCell.UseVisualStyleBackColor = true;
            this.buttonMoveToMidCell.Click += new System.EventHandler(this.buttonMoveToMidCell_Click);
            // 
            // buttonSnapShot
            // 
            this.buttonSnapShot.Location = new System.Drawing.Point(618, 300);
            this.buttonSnapShot.Name = "buttonSnapShot";
            this.buttonSnapShot.Size = new System.Drawing.Size(110, 35);
            this.buttonSnapShot.TabIndex = 9;
            this.buttonSnapShot.Text = "抓取一帧";
            this.buttonSnapShot.UseVisualStyleBackColor = true;
            this.buttonSnapShot.Click += new System.EventHandler(this.buttonSnapShot_Click);
            // 
            // pointPosUCOutlineMeasXPartOut
            // 
            this.pointPosUCOutlineMeasXPartOut.Axis = null;
            this.pointPosUCOutlineMeasXPartOut.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCOutlineMeasXPartOut.BindingUC = this.motorUCOutlineMeasX;
            this.pointPosUCOutlineMeasXPartOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCOutlineMeasXPartOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCOutlineMeasXPartOut.Location = new System.Drawing.Point(366, 641);
            this.pointPosUCOutlineMeasXPartOut.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCOutlineMeasXPartOut.Name = "pointPosUCOutlineMeasXPartOut";
            this.pointPosUCOutlineMeasXPartOut.PointName = "PartOut";
            this.pointPosUCOutlineMeasXPartOut.Size = new System.Drawing.Size(507, 61);
            this.pointPosUCOutlineMeasXPartOut.TabIndex = 12;
            this.pointPosUCOutlineMeasXPartOut.TextBoxPosLeft = 293;
            this.pointPosUCOutlineMeasXPartOut.TextDisplay = "下料位";
            // 
            // motorUCOutlineMeasX
            // 
            this.motorUCOutlineMeasX.AutoSize = true;
            this.motorUCOutlineMeasX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCOutlineMeasX.Axis = null;
            this.motorUCOutlineMeasX.AxisName = "CCD测量X";
            this.motorUCOutlineMeasX.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCOutlineMeasX.HomeEnabled = true;
            this.motorUCOutlineMeasX.Location = new System.Drawing.Point(3, 428);
            this.motorUCOutlineMeasX.Name = "motorUCOutlineMeasX";
            this.motorUCOutlineMeasX.Size = new System.Drawing.Size(283, 190);
            this.motorUCOutlineMeasX.TabIndex = 11;
            // 
            // pointPosUCOutlineMeasXPitch
            // 
            this.pointPosUCOutlineMeasXPitch.Axis = null;
            this.pointPosUCOutlineMeasXPitch.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCOutlineMeasXPitch.BindingUC = this.motorUCOutlineMeasX;
            this.pointPosUCOutlineMeasXPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCOutlineMeasXPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCOutlineMeasXPitch.Location = new System.Drawing.Point(366, 570);
            this.pointPosUCOutlineMeasXPitch.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCOutlineMeasXPitch.Name = "pointPosUCOutlineMeasXPitch";
            this.pointPosUCOutlineMeasXPitch.PointName = "Pitch";
            this.pointPosUCOutlineMeasXPitch.Size = new System.Drawing.Size(507, 61);
            this.pointPosUCOutlineMeasXPitch.TabIndex = 13;
            this.pointPosUCOutlineMeasXPitch.TextBoxPosLeft = 293;
            this.pointPosUCOutlineMeasXPitch.TextDisplay = "工位间距";
            // 
            // pointPosUCOutlineMeasXStart
            // 
            this.pointPosUCOutlineMeasXStart.Axis = null;
            this.pointPosUCOutlineMeasXStart.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCOutlineMeasXStart.BindingUC = this.motorUCOutlineMeasX;
            this.pointPosUCOutlineMeasXStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCOutlineMeasXStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCOutlineMeasXStart.Location = new System.Drawing.Point(366, 499);
            this.pointPosUCOutlineMeasXStart.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCOutlineMeasXStart.Name = "pointPosUCOutlineMeasXStart";
            this.pointPosUCOutlineMeasXStart.PointName = "Start";
            this.pointPosUCOutlineMeasXStart.Size = new System.Drawing.Size(507, 61);
            this.pointPosUCOutlineMeasXStart.TabIndex = 14;
            this.pointPosUCOutlineMeasXStart.TextBoxPosLeft = 293;
            this.pointPosUCOutlineMeasXStart.TextDisplay = "第一测量位";
            // 
            // pointPosUCOutlineMeasXGetPart
            // 
            this.pointPosUCOutlineMeasXGetPart.Axis = null;
            this.pointPosUCOutlineMeasXGetPart.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCOutlineMeasXGetPart.BindingUC = this.motorUCOutlineMeasX;
            this.pointPosUCOutlineMeasXGetPart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCOutlineMeasXGetPart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCOutlineMeasXGetPart.Location = new System.Drawing.Point(366, 428);
            this.pointPosUCOutlineMeasXGetPart.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCOutlineMeasXGetPart.Name = "pointPosUCOutlineMeasXGetPart";
            this.pointPosUCOutlineMeasXGetPart.PointName = "GetPart";
            this.pointPosUCOutlineMeasXGetPart.Size = new System.Drawing.Size(507, 61);
            this.pointPosUCOutlineMeasXGetPart.TabIndex = 15;
            this.pointPosUCOutlineMeasXGetPart.TextBoxPosLeft = 293;
            this.pointPosUCOutlineMeasXGetPart.TextDisplay = "上料位";
            // 
            // buttonMoveToPickPart
            // 
            this.buttonMoveToPickPart.Location = new System.Drawing.Point(734, 228);
            this.buttonMoveToPickPart.Name = "buttonMoveToPickPart";
            this.buttonMoveToPickPart.Size = new System.Drawing.Size(110, 35);
            this.buttonMoveToPickPart.TabIndex = 16;
            this.buttonMoveToPickPart.Text = "移到下料位";
            this.buttonMoveToPickPart.UseVisualStyleBackColor = true;
            this.buttonMoveToPickPart.Click += new System.EventHandler(this.buttonMoveToPickPart_Click);
            // 
            // buttonCDIVision
            // 
            this.buttonCDIVision.Location = new System.Drawing.Point(802, 300);
            this.buttonCDIVision.Name = "buttonCDIVision";
            this.buttonCDIVision.Size = new System.Drawing.Size(110, 43);
            this.buttonCDIVision.TabIndex = 18;
            this.buttonCDIVision.Text = "重新打开CDIVision";
            this.buttonCDIVision.UseVisualStyleBackColor = true;
            this.buttonCDIVision.Click += new System.EventHandler(this.buttonCDIVision_Click);
            // 
            // ManualPanelZone尺寸测量
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.buttonCDIVision);
            this.Controls.Add(this.buttonMoveToPickPart);
            this.Controls.Add(this.pointPosUCOutlineMeasXPartOut);
            this.Controls.Add(this.pointPosUCOutlineMeasXPitch);
            this.Controls.Add(this.pointPosUCOutlineMeasXStart);
            this.Controls.Add(this.pointPosUCOutlineMeasXGetPart);
            this.Controls.Add(this.motorUCOutlineMeasX);
            this.Controls.Add(this.buttonSnapShot);
            this.Controls.Add(this.buttonMoveToRightCell);
            this.Controls.Add(this.buttonMoveToMidCell);
            this.Controls.Add(this.buttonMoveToLeftCell);
            this.Controls.Add(this.buttonMoveToGetPart);
            this.Controls.Add(this.buttonMeasOnce);
            this.Controls.Add(this.buttonMeas);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualPanelZone尺寸测量";
            this.Size = new System.Drawing.Size(1235, 852);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonMeas, 0);
            this.Controls.SetChildIndex(this.buttonMeasOnce, 0);
            this.Controls.SetChildIndex(this.buttonMoveToGetPart, 0);
            this.Controls.SetChildIndex(this.buttonMoveToLeftCell, 0);
            this.Controls.SetChildIndex(this.buttonMoveToMidCell, 0);
            this.Controls.SetChildIndex(this.buttonMoveToRightCell, 0);
            this.Controls.SetChildIndex(this.buttonSnapShot, 0);
            this.Controls.SetChildIndex(this.motorUCOutlineMeasX, 0);
            this.Controls.SetChildIndex(this.pointPosUCOutlineMeasXGetPart, 0);
            this.Controls.SetChildIndex(this.pointPosUCOutlineMeasXStart, 0);
            this.Controls.SetChildIndex(this.pointPosUCOutlineMeasXPitch, 0);
            this.Controls.SetChildIndex(this.pointPosUCOutlineMeasXPartOut, 0);
            this.Controls.SetChildIndex(this.buttonMoveToPickPart, 0);
            this.Controls.SetChildIndex(this.buttonCDIVision, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DataDisp DataDispCellRight;
        private DataDisp DataDispCellMiddle;
        private DataDisp DataDispCellLeft;
        private System.Windows.Forms.Button buttonMeas;
        private System.Windows.Forms.Button buttonMoveToGetPart;
        private System.Windows.Forms.Button buttonMeasOnce;
        private System.Windows.Forms.Button buttonMoveToLeftCell;
        private System.Windows.Forms.Button buttonMoveToRightCell;
        private System.Windows.Forms.Button buttonMoveToMidCell;
        private System.Windows.Forms.Button buttonSnapShot;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCOutlineMeasXPartOut;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCOutlineMeasXPitch;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCOutlineMeasXStart;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCOutlineMeasXGetPart;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCOutlineMeasX;
        private System.Windows.Forms.Button buttonMoveToPickPart;
        private System.Windows.Forms.Button buttonCDIVision;
    }
}
