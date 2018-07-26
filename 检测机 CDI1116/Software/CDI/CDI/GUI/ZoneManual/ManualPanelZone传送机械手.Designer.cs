namespace CDI.GUI
{
    partial class ManualPanelZone传送机械手
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
            this.dataDispCellLoadRight = new CDI.GUI.DataDisp();
            this.dataDispCellLoadMid = new CDI.GUI.DataDisp();
            this.dataDispCellLoadLeft = new CDI.GUI.DataDisp();
            this.dataDispCellUnloadRight = new CDI.GUI.DataDisp();
            this.dataDispCellUnloadMid = new CDI.GUI.DataDisp();
            this.dataDispCellUnloadLeft = new CDI.GUI.DataDisp();
            this.buttonPick = new System.Windows.Forms.Button();
            this.buttonPlace = new System.Windows.Forms.Button();
            this.buttonWorkFlow = new System.Windows.Forms.Button();
            this.pointPosUCTransPNPXUnload = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCTransPNPX = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCTransPNPXLoad = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.pointPosUCTransPNPXSafeLimit = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 191);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 153);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(119, 153);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.dataDispCellLoadRight);
            this.groupBox2.Controls.Add(this.dataDispCellLoadMid);
            this.groupBox2.Controls.Add(this.dataDispCellLoadLeft);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 138);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "上料电芯";
            // 
            // dataDispCellLoadRight
            // 
            this.dataDispCellLoadRight.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellLoadRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellLoadRight.DataStation = null;
            this.dataDispCellLoadRight.Location = new System.Drawing.Point(408, 19);
            this.dataDispCellLoadRight.Name = "dataDispCellLoadRight";
            this.dataDispCellLoadRight.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellLoadRight.TabIndex = 18;
            this.dataDispCellLoadRight.Text = "电芯1";
            // 
            // dataDispCellLoadMid
            // 
            this.dataDispCellLoadMid.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellLoadMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellLoadMid.DataStation = null;
            this.dataDispCellLoadMid.Location = new System.Drawing.Point(207, 19);
            this.dataDispCellLoadMid.Name = "dataDispCellLoadMid";
            this.dataDispCellLoadMid.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellLoadMid.TabIndex = 17;
            this.dataDispCellLoadMid.Text = "电芯1";
            // 
            // dataDispCellLoadLeft
            // 
            this.dataDispCellLoadLeft.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellLoadLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellLoadLeft.DataStation = null;
            this.dataDispCellLoadLeft.Location = new System.Drawing.Point(6, 19);
            this.dataDispCellLoadLeft.Name = "dataDispCellLoadLeft";
            this.dataDispCellLoadLeft.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellLoadLeft.TabIndex = 16;
            this.dataDispCellLoadLeft.Text = "电芯1";
            // 
            // dataDispCellUnloadRight
            // 
            this.dataDispCellUnloadRight.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellUnloadRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellUnloadRight.DataStation = null;
            this.dataDispCellUnloadRight.Location = new System.Drawing.Point(408, 19);
            this.dataDispCellUnloadRight.Name = "dataDispCellUnloadRight";
            this.dataDispCellUnloadRight.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellUnloadRight.TabIndex = 15;
            this.dataDispCellUnloadRight.Text = "电芯1";
            // 
            // dataDispCellUnloadMid
            // 
            this.dataDispCellUnloadMid.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellUnloadMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellUnloadMid.DataStation = null;
            this.dataDispCellUnloadMid.Location = new System.Drawing.Point(207, 19);
            this.dataDispCellUnloadMid.Name = "dataDispCellUnloadMid";
            this.dataDispCellUnloadMid.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellUnloadMid.TabIndex = 14;
            this.dataDispCellUnloadMid.Text = "电芯1";
            // 
            // dataDispCellUnloadLeft
            // 
            this.dataDispCellUnloadLeft.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCellUnloadLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCellUnloadLeft.DataStation = null;
            this.dataDispCellUnloadLeft.Location = new System.Drawing.Point(6, 19);
            this.dataDispCellUnloadLeft.Name = "dataDispCellUnloadLeft";
            this.dataDispCellUnloadLeft.Size = new System.Drawing.Size(195, 100);
            this.dataDispCellUnloadLeft.TabIndex = 13;
            this.dataDispCellUnloadLeft.Text = "电芯1";
            // 
            // buttonPick
            // 
            this.buttonPick.Location = new System.Drawing.Point(348, 153);
            this.buttonPick.Name = "buttonPick";
            this.buttonPick.Size = new System.Drawing.Size(110, 35);
            this.buttonPick.TabIndex = 16;
            this.buttonPick.Text = "加载物料";
            this.buttonPick.UseVisualStyleBackColor = true;
            this.buttonPick.Click += new System.EventHandler(this.buttonPick_Click);
            // 
            // buttonPlace
            // 
            this.buttonPlace.Location = new System.Drawing.Point(464, 153);
            this.buttonPlace.Name = "buttonPlace";
            this.buttonPlace.Size = new System.Drawing.Size(110, 35);
            this.buttonPlace.TabIndex = 16;
            this.buttonPlace.Text = "卸载物料";
            this.buttonPlace.UseVisualStyleBackColor = true;
            this.buttonPlace.Click += new System.EventHandler(this.buttonPlace_Click);
            // 
            // buttonWorkFlow
            // 
            this.buttonWorkFlow.Location = new System.Drawing.Point(615, 153);
            this.buttonWorkFlow.Name = "buttonWorkFlow";
            this.buttonWorkFlow.Size = new System.Drawing.Size(110, 35);
            this.buttonWorkFlow.TabIndex = 18;
            this.buttonWorkFlow.Text = "完整流程";
            this.buttonWorkFlow.UseVisualStyleBackColor = true;
            this.buttonWorkFlow.Click += new System.EventHandler(this.buttonWorkFlow_Click);
            // 
            // pointPosUCTransPNPXUnload
            // 
            this.pointPosUCTransPNPXUnload.Axis = null;
            this.pointPosUCTransPNPXUnload.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCTransPNPXUnload.BindingUC = this.motorUCTransPNPX;
            this.pointPosUCTransPNPXUnload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTransPNPXUnload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTransPNPXUnload.Location = new System.Drawing.Point(294, 363);
            this.pointPosUCTransPNPXUnload.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTransPNPXUnload.Name = "pointPosUCTransPNPXUnload";
            this.pointPosUCTransPNPXUnload.PointName = "Unload";
            this.pointPosUCTransPNPXUnload.Size = new System.Drawing.Size(548, 61);
            this.pointPosUCTransPNPXUnload.TabIndex = 21;
            this.pointPosUCTransPNPXUnload.TextBoxPosLeft = 338;
            this.pointPosUCTransPNPXUnload.TextDisplay = "放料位";
            // 
            // motorUCTransPNPX
            // 
            this.motorUCTransPNPX.AutoSize = true;
            this.motorUCTransPNPX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCTransPNPX.Axis = null;
            this.motorUCTransPNPX.AxisName = "传送PNPX";
            this.motorUCTransPNPX.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCTransPNPX.HomeEnabled = true;
            this.motorUCTransPNPX.Location = new System.Drawing.Point(3, 221);
            this.motorUCTransPNPX.Name = "motorUCTransPNPX";
            this.motorUCTransPNPX.Size = new System.Drawing.Size(283, 190);
            this.motorUCTransPNPX.TabIndex = 19;
            // 
            // pointPosUCTransPNPXLoad
            // 
            this.pointPosUCTransPNPXLoad.Axis = null;
            this.pointPosUCTransPNPXLoad.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCTransPNPXLoad.BindingUC = this.motorUCTransPNPX;
            this.pointPosUCTransPNPXLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTransPNPXLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTransPNPXLoad.Location = new System.Drawing.Point(294, 293);
            this.pointPosUCTransPNPXLoad.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTransPNPXLoad.Name = "pointPosUCTransPNPXLoad";
            this.pointPosUCTransPNPXLoad.PointName = "Load";
            this.pointPosUCTransPNPXLoad.Size = new System.Drawing.Size(548, 61);
            this.pointPosUCTransPNPXLoad.TabIndex = 22;
            this.pointPosUCTransPNPXLoad.TextBoxPosLeft = 334;
            this.pointPosUCTransPNPXLoad.TextDisplay = "取料位";
            // 
            // pointPosUCTransPNPXSafeLimit
            // 
            this.pointPosUCTransPNPXSafeLimit.Axis = null;
            this.pointPosUCTransPNPXSafeLimit.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCTransPNPXSafeLimit.BindingUC = this.motorUCTransPNPX;
            this.pointPosUCTransPNPXSafeLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCTransPNPXSafeLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCTransPNPXSafeLimit.Location = new System.Drawing.Point(294, 221);
            this.pointPosUCTransPNPXSafeLimit.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCTransPNPXSafeLimit.Name = "pointPosUCTransPNPXSafeLimit";
            this.pointPosUCTransPNPXSafeLimit.PointName = "SafeLimit";
            this.pointPosUCTransPNPXSafeLimit.Size = new System.Drawing.Size(548, 61);
            this.pointPosUCTransPNPXSafeLimit.TabIndex = 23;
            this.pointPosUCTransPNPXSafeLimit.TextBoxPosLeft = 334;
            this.pointPosUCTransPNPXSafeLimit.TextDisplay = "上料PNP安全限位";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.dataDispCellUnloadLeft);
            this.groupBox1.Controls.Add(this.dataDispCellUnloadMid);
            this.groupBox1.Controls.Add(this.dataDispCellUnloadRight);
            this.groupBox1.Location = new System.Drawing.Point(619, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 138);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下料电芯";
            // 
            // ManualPanelZone传送机械手
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pointPosUCTransPNPXUnload);
            this.Controls.Add(this.pointPosUCTransPNPXLoad);
            this.Controls.Add(this.pointPosUCTransPNPXSafeLimit);
            this.Controls.Add(this.motorUCTransPNPX);
            this.Controls.Add(this.buttonWorkFlow);
            this.Controls.Add(this.buttonPlace);
            this.Controls.Add(this.buttonPick);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualPanelZone传送机械手";
            this.Size = new System.Drawing.Size(1311, 449);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.buttonPick, 0);
            this.Controls.SetChildIndex(this.buttonPlace, 0);
            this.Controls.SetChildIndex(this.buttonWorkFlow, 0);
            this.Controls.SetChildIndex(this.motorUCTransPNPX, 0);
            this.Controls.SetChildIndex(this.pointPosUCTransPNPXSafeLimit, 0);
            this.Controls.SetChildIndex(this.pointPosUCTransPNPXLoad, 0);
            this.Controls.SetChildIndex(this.pointPosUCTransPNPXUnload, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DataDisp dataDispCellLoadRight;
        private DataDisp dataDispCellLoadMid;
        private DataDisp dataDispCellLoadLeft;
        private DataDisp dataDispCellUnloadRight;
        private DataDisp dataDispCellUnloadMid;
        private DataDisp dataDispCellUnloadLeft;
        private System.Windows.Forms.Button buttonPick;
        private System.Windows.Forms.Button buttonPlace;
        private System.Windows.Forms.Button buttonWorkFlow;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTransPNPXUnload;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTransPNPXLoad;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCTransPNPXSafeLimit;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCTransPNPX;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
