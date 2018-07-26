namespace CDI.GUI
{
    partial class SettingPanelZone下料传送
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
            this.uioFlatPortInUnloadOutHavePartLeft = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInUnloadOutHavePartMid = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInUnloadOutHavePartRight = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInUnloadOutUnloadLeft = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.selectBoxIgnoreSMEMA = new Colibri.CommonModule.ToolBox.SelectBox();
            this.uioFlatPortInUnloadOutUnloadRight = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInUnloadOutUnloadMid = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.groupBoxInport.SuspendLayout();
            this.groupBoxOutPort.SuspendLayout();
            this.groupBoxParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInport
            // 
            this.groupBoxInport.AutoSize = true;
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutUnloadRight);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutSMEMAUnloadAvailable);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutUnloadMid);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutHavePartRight);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutUnloadLeft);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutHavePartMid);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInUnloadOutHavePartLeft);
            this.groupBoxInport.Size = new System.Drawing.Size(234, 172);
            // 
            // groupBoxOutPort
            // 
            this.groupBoxOutPort.AutoSize = true;
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutUnloadOutSMEMAUnloadReady);
            this.groupBoxOutPort.Location = new System.Drawing.Point(243, 3);
            this.groupBoxOutPort.Size = new System.Drawing.Size(120, 68);
            // 
            // comboBoxAxis
            // 
            this.comboBoxAxis.Location = new System.Drawing.Point(44, 218);
            // 
            // labelAxis
            // 
            this.labelAxis.Location = new System.Drawing.Point(7, 221);
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Controls.Add(this.selectBoxIgnoreSMEMA);
            this.groupBoxParameter.Location = new System.Drawing.Point(243, 80);
            this.groupBoxParameter.Size = new System.Drawing.Size(154, 63);
            this.groupBoxParameter.Visible = true;
            this.groupBoxParameter.Controls.SetChildIndex(this.buttonSavePara, 0);
            this.groupBoxParameter.Controls.SetChildIndex(this.selectBoxIgnoreSMEMA, 0);
            // 
            // buttonSavePara
            // 
            this.buttonSavePara.Location = new System.Drawing.Point(77, 27);
            this.buttonSavePara.Visible = false;
            // 
            // uioFlatPortInUnloadOutHavePartLeft
            // 
            this.uioFlatPortInUnloadOutHavePartLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutHavePartLeft.HelpTip = null;
            this.uioFlatPortInUnloadOutHavePartLeft.Location = new System.Drawing.Point(6, 20);
            this.uioFlatPortInUnloadOutHavePartLeft.Name = "uioFlatPortInUnloadOutHavePartLeft";
            this.uioFlatPortInUnloadOutHavePartLeft.Port = null;
            this.uioFlatPortInUnloadOutHavePartLeft.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutHavePartLeft.TabIndex = 9;
            this.uioFlatPortInUnloadOutHavePartLeft.Text = "左有物料";
            // 
            // uioFlatPortInUnloadOutHavePartMid
            // 
            this.uioFlatPortInUnloadOutHavePartMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutHavePartMid.HelpTip = null;
            this.uioFlatPortInUnloadOutHavePartMid.Location = new System.Drawing.Point(6, 54);
            this.uioFlatPortInUnloadOutHavePartMid.Name = "uioFlatPortInUnloadOutHavePartMid";
            this.uioFlatPortInUnloadOutHavePartMid.Port = null;
            this.uioFlatPortInUnloadOutHavePartMid.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutHavePartMid.TabIndex = 9;
            this.uioFlatPortInUnloadOutHavePartMid.Text = "中有物料";
            // 
            // uioFlatPortInUnloadOutHavePartRight
            // 
            this.uioFlatPortInUnloadOutHavePartRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutHavePartRight.HelpTip = null;
            this.uioFlatPortInUnloadOutHavePartRight.Location = new System.Drawing.Point(6, 89);
            this.uioFlatPortInUnloadOutHavePartRight.Name = "uioFlatPortInUnloadOutHavePartRight";
            this.uioFlatPortInUnloadOutHavePartRight.Port = null;
            this.uioFlatPortInUnloadOutHavePartRight.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutHavePartRight.TabIndex = 9;
            this.uioFlatPortInUnloadOutHavePartRight.Text = "右有物料";
            // 
            // uioFlatPortInUnloadOutUnloadLeft
            // 
            this.uioFlatPortInUnloadOutUnloadLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutUnloadLeft.HelpTip = null;
            this.uioFlatPortInUnloadOutUnloadLeft.Location = new System.Drawing.Point(120, 20);
            this.uioFlatPortInUnloadOutUnloadLeft.Name = "uioFlatPortInUnloadOutUnloadLeft";
            this.uioFlatPortInUnloadOutUnloadLeft.Port = null;
            this.uioFlatPortInUnloadOutUnloadLeft.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutUnloadLeft.TabIndex = 9;
            this.uioFlatPortInUnloadOutUnloadLeft.Text = "下料左";
            // 
            // uioFlatPortInUnloadOutSMEMAUnloadAvailable
            // 
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.HelpTip = null;
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.Location = new System.Drawing.Point(6, 124);
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.Name = "uioFlatPortInUnloadOutSMEMAUnloadAvailable";
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.Port = null;
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.TabIndex = 9;
            this.uioFlatPortInUnloadOutSMEMAUnloadAvailable.Text = "SMEMA空闲";
            // 
            // uioFlatPortOutUnloadOutSMEMAUnloadReady
            // 
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.HelpTip = null;
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.Location = new System.Drawing.Point(6, 20);
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.Name = "uioFlatPortOutUnloadOutSMEMAUnloadReady";
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.Port = null;
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.TabIndex = 9;
            this.uioFlatPortOutUnloadOutSMEMAUnloadReady.Text = "SMEMA准备好";
            // 
            // selectBoxIgnoreSMEMA
            // 
            this.selectBoxIgnoreSMEMA.AutoSize = true;
            this.selectBoxIgnoreSMEMA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxIgnoreSMEMA.BorderColor = System.Drawing.Color.Black;
            this.selectBoxIgnoreSMEMA.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxIgnoreSMEMA.Location = new System.Drawing.Point(6, 27);
            this.selectBoxIgnoreSMEMA.Name = "selectBoxIgnoreSMEMA";
            this.selectBoxIgnoreSMEMA.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxIgnoreSMEMA.Size = new System.Drawing.Size(143, 23);
            this.selectBoxIgnoreSMEMA.TabIndex = 8;
            this.selectBoxIgnoreSMEMA.Text = "忽略下料SMEMA信号";
            this.selectBoxIgnoreSMEMA.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxIgnoreSMEMA.UseVisualStyleBackColor = true;
            this.selectBoxIgnoreSMEMA.CheckedChanged += new System.EventHandler(this.selectBoxIgnoreSMEMA_CheckedChanged);
            // 
            // uioFlatPortInUnloadOutUnloadRight
            // 
            this.uioFlatPortInUnloadOutUnloadRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutUnloadRight.HelpTip = null;
            this.uioFlatPortInUnloadOutUnloadRight.Location = new System.Drawing.Point(120, 91);
            this.uioFlatPortInUnloadOutUnloadRight.Name = "uioFlatPortInUnloadOutUnloadRight";
            this.uioFlatPortInUnloadOutUnloadRight.Port = null;
            this.uioFlatPortInUnloadOutUnloadRight.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutUnloadRight.TabIndex = 10;
            this.uioFlatPortInUnloadOutUnloadRight.Text = "下料右";
            // 
            // uioFlatPortInUnloadOutUnloadMid
            // 
            this.uioFlatPortInUnloadOutUnloadMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInUnloadOutUnloadMid.HelpTip = null;
            this.uioFlatPortInUnloadOutUnloadMid.Location = new System.Drawing.Point(120, 55);
            this.uioFlatPortInUnloadOutUnloadMid.Name = "uioFlatPortInUnloadOutUnloadMid";
            this.uioFlatPortInUnloadOutUnloadMid.Port = null;
            this.uioFlatPortInUnloadOutUnloadMid.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInUnloadOutUnloadMid.TabIndex = 11;
            this.uioFlatPortInUnloadOutUnloadMid.Text = "下料中";
            // 
            // SettingPanelZone下料传送
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SettingPanelZone下料传送";
            this.Size = new System.Drawing.Size(400, 242);
            this.groupBoxInport.ResumeLayout(false);
            this.groupBoxOutPort.ResumeLayout(false);
            this.groupBoxParameter.ResumeLayout(false);
            this.groupBoxParameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutHavePartLeft;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutHavePartRight;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutSMEMAUnloadAvailable;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutUnloadLeft;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutHavePartMid;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutUnloadOutSMEMAUnloadReady;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxIgnoreSMEMA;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutUnloadRight;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInUnloadOutUnloadMid;
    }
}
