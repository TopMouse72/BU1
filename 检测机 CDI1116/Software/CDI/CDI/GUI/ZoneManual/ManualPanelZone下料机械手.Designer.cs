namespace CDI.GUI
{
    partial class ManualPanelZone下料机械手
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
            this.buttonWorkFlow = new System.Windows.Forms.Button();
            this.buttonPlacePart = new System.Windows.Forms.Button();
            this.buttonPickPart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DataDispCellRight = new CDI.GUI.DataDisp();
            this.DataDispCellMiddle = new CDI.GUI.DataDisp();
            this.DataDispCellLeft = new CDI.GUI.DataDisp();
            this.pointPosUCUnloadPNPYPick = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCUnloadPNPY = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCUnloadPNPYBuffer = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCUnloadPNPYPlace = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.groupBox2.SuspendLayout();
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
            // buttonWorkFlow
            // 
            this.buttonWorkFlow.Location = new System.Drawing.Point(588, 155);
            this.buttonWorkFlow.Name = "buttonWorkFlow";
            this.buttonWorkFlow.Size = new System.Drawing.Size(110, 35);
            this.buttonWorkFlow.TabIndex = 9;
            this.buttonWorkFlow.Text = "完整流程";
            this.buttonWorkFlow.UseVisualStyleBackColor = true;
            this.buttonWorkFlow.Click += new System.EventHandler(this.buttonWorkFlow_Click);
            // 
            // buttonPlacePart
            // 
            this.buttonPlacePart.Location = new System.Drawing.Point(472, 155);
            this.buttonPlacePart.Name = "buttonPlacePart";
            this.buttonPlacePart.Size = new System.Drawing.Size(110, 35);
            this.buttonPlacePart.TabIndex = 8;
            this.buttonPlacePart.Text = "放料";
            this.buttonPlacePart.UseVisualStyleBackColor = true;
            this.buttonPlacePart.Click += new System.EventHandler(this.buttonPlacePart_Click);
            // 
            // buttonPickPart
            // 
            this.buttonPickPart.Location = new System.Drawing.Point(356, 155);
            this.buttonPickPart.Name = "buttonPickPart";
            this.buttonPickPart.Size = new System.Drawing.Size(110, 35);
            this.buttonPickPart.TabIndex = 7;
            this.buttonPickPart.Text = "取料";
            this.buttonPickPart.UseVisualStyleBackColor = true;
            this.buttonPickPart.Click += new System.EventHandler(this.buttonPickPart_Click);
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
            // pointPosUCUnloadPNPYPick
            // 
            this.pointPosUCUnloadPNPYPick.Axis = null;
            this.pointPosUCUnloadPNPYPick.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCUnloadPNPYPick.BindingUC = this.motorUCUnloadPNPY;
            this.pointPosUCUnloadPNPYPick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCUnloadPNPYPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCUnloadPNPYPick.Location = new System.Drawing.Point(294, 223);
            this.pointPosUCUnloadPNPYPick.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCUnloadPNPYPick.Name = "pointPosUCUnloadPNPYPick";
            this.pointPosUCUnloadPNPYPick.PointName = "Pick";
            this.pointPosUCUnloadPNPYPick.Size = new System.Drawing.Size(467, 62);
            this.pointPosUCUnloadPNPYPick.TabIndex = 31;
            this.pointPosUCUnloadPNPYPick.TextBoxPosLeft = 253;
            this.pointPosUCUnloadPNPYPick.TextDisplay = "取料位";
            // 
            // motorUCUnloadPNPY
            // 
            this.motorUCUnloadPNPY.AutoSize = true;
            this.motorUCUnloadPNPY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCUnloadPNPY.Axis = null;
            this.motorUCUnloadPNPY.AxisName = "机械手Y";
            this.motorUCUnloadPNPY.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCUnloadPNPY.HomeEnabled = true;
            this.motorUCUnloadPNPY.Location = new System.Drawing.Point(3, 223);
            this.motorUCUnloadPNPY.Name = "motorUCUnloadPNPY";
            this.motorUCUnloadPNPY.Size = new System.Drawing.Size(283, 190);
            this.motorUCUnloadPNPY.TabIndex = 29;
            // 
            // pointPosUCUnloadPNPYBuffer
            // 
            this.pointPosUCUnloadPNPYBuffer.Axis = null;
            this.pointPosUCUnloadPNPYBuffer.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCUnloadPNPYBuffer.BindingUC = this.motorUCUnloadPNPY;
            this.pointPosUCUnloadPNPYBuffer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCUnloadPNPYBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCUnloadPNPYBuffer.Location = new System.Drawing.Point(294, 295);
            this.pointPosUCUnloadPNPYBuffer.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCUnloadPNPYBuffer.Name = "pointPosUCUnloadPNPYBuffer";
            this.pointPosUCUnloadPNPYBuffer.PointName = "Buffer";
            this.pointPosUCUnloadPNPYBuffer.Size = new System.Drawing.Size(467, 62);
            this.pointPosUCUnloadPNPYBuffer.TabIndex = 30;
            this.pointPosUCUnloadPNPYBuffer.TextBoxPosLeft = 253;
            this.pointPosUCUnloadPNPYBuffer.TextDisplay = "等待位";
            // 
            // pointPosUCUnloadPNPYPlace
            // 
            this.pointPosUCUnloadPNPYPlace.Axis = null;
            this.pointPosUCUnloadPNPYPlace.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCUnloadPNPYPlace.BindingUC = this.motorUCUnloadPNPY;
            this.pointPosUCUnloadPNPYPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCUnloadPNPYPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCUnloadPNPYPlace.Location = new System.Drawing.Point(294, 367);
            this.pointPosUCUnloadPNPYPlace.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCUnloadPNPYPlace.Name = "pointPosUCUnloadPNPYPlace";
            this.pointPosUCUnloadPNPYPlace.PointName = "Place";
            this.pointPosUCUnloadPNPYPlace.Size = new System.Drawing.Size(467, 62);
            this.pointPosUCUnloadPNPYPlace.TabIndex = 30;
            this.pointPosUCUnloadPNPYPlace.TextBoxPosLeft = 253;
            this.pointPosUCUnloadPNPYPlace.TextDisplay = "放料位";
            // 
            // ManualPanelZone下料机械手
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.pointPosUCUnloadPNPYPick);
            this.Controls.Add(this.pointPosUCUnloadPNPYPlace);
            this.Controls.Add(this.pointPosUCUnloadPNPYBuffer);
            this.Controls.Add(this.motorUCUnloadPNPY);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonWorkFlow);
            this.Controls.Add(this.buttonPlacePart);
            this.Controls.Add(this.buttonPickPart);
            this.Name = "ManualPanelZone下料机械手";
            this.Size = new System.Drawing.Size(867, 460);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.buttonPickPart, 0);
            this.Controls.SetChildIndex(this.buttonPlacePart, 0);
            this.Controls.SetChildIndex(this.buttonWorkFlow, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.motorUCUnloadPNPY, 0);
            this.Controls.SetChildIndex(this.pointPosUCUnloadPNPYBuffer, 0);
            this.Controls.SetChildIndex(this.pointPosUCUnloadPNPYPlace, 0);
            this.Controls.SetChildIndex(this.pointPosUCUnloadPNPYPick, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonWorkFlow;
        private System.Windows.Forms.Button buttonPlacePart;
        private System.Windows.Forms.Button buttonPickPart;
        private System.Windows.Forms.GroupBox groupBox2;
        private DataDisp DataDispCellRight;
        private DataDisp DataDispCellMiddle;
        private DataDisp DataDispCellLeft;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCUnloadPNPYPick;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCUnloadPNPYBuffer;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCUnloadPNPY;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCUnloadPNPYPlace;
    }
}
