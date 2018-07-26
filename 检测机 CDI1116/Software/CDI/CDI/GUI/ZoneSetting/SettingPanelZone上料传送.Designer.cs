namespace CDI.GUI
{
    partial class SettingPanelZone上料传送
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
            this.uioFlatPortInLoadInConvLoad = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInLoadInConvInPosLeft = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInLoadInSMEMALoadReady = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutLoadInSMEMALoadAvailable = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.selectBoxIgnoreSMEMA = new Colibri.CommonModule.ToolBox.SelectBox();
            this.uioFlatPortInLoadInConvInPosMid = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInLoadInConvInPosRight = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.groupBoxInport.SuspendLayout();
            this.groupBoxOutPort.SuspendLayout();
            this.groupBoxParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInport
            // 
            this.groupBoxInport.AutoSize = true;
            this.groupBoxInport.Controls.Add(this.uioFlatPortInLoadInConvLoad);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInLoadInConvInPosRight);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInLoadInConvInPosMid);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInLoadInConvInPosLeft);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInLoadInSMEMALoadReady);
            this.groupBoxInport.Size = new System.Drawing.Size(234, 166);
            // 
            // groupBoxOutPort
            // 
            this.groupBoxOutPort.AutoSize = true;
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutLoadInSMEMALoadAvailable);
            this.groupBoxOutPort.Location = new System.Drawing.Point(243, 3);
            this.groupBoxOutPort.Size = new System.Drawing.Size(120, 67);
            // 
            // comboBoxAxis
            // 
            this.comboBoxAxis.Location = new System.Drawing.Point(42, 175);
            // 
            // labelAxis
            // 
            this.labelAxis.Location = new System.Drawing.Point(5, 178);
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Controls.Add(this.selectBoxIgnoreSMEMA);
            this.groupBoxParameter.Location = new System.Drawing.Point(243, 88);
            this.groupBoxParameter.Size = new System.Drawing.Size(159, 48);
            this.groupBoxParameter.Visible = true;
            this.groupBoxParameter.Controls.SetChildIndex(this.buttonSavePara, 0);
            this.groupBoxParameter.Controls.SetChildIndex(this.selectBoxIgnoreSMEMA, 0);
            // 
            // buttonSavePara
            // 
            this.buttonSavePara.Location = new System.Drawing.Point(82, 16);
            this.buttonSavePara.Visible = false;
            // 
            // uioFlatPortInLoadInConvLoad
            // 
            this.uioFlatPortInLoadInConvLoad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInLoadInConvLoad.HelpTip = null;
            this.uioFlatPortInLoadInConvLoad.Location = new System.Drawing.Point(6, 20);
            this.uioFlatPortInLoadInConvLoad.Name = "uioFlatPortInLoadInConvLoad";
            this.uioFlatPortInLoadInConvLoad.Port = null;
            this.uioFlatPortInLoadInConvLoad.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortInLoadInConvLoad.TabIndex = 0;
            this.uioFlatPortInLoadInConvLoad.Text = "物料加载";
            // 
            // uioFlatPortInLoadInConvInPosLeft
            // 
            this.uioFlatPortInLoadInConvInPosLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInLoadInConvInPosLeft.HelpTip = null;
            this.uioFlatPortInLoadInConvInPosLeft.Location = new System.Drawing.Point(6, 53);
            this.uioFlatPortInLoadInConvInPosLeft.Name = "uioFlatPortInLoadInConvInPosLeft";
            this.uioFlatPortInLoadInConvInPosLeft.Port = null;
            this.uioFlatPortInLoadInConvInPosLeft.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortInLoadInConvInPosLeft.TabIndex = 0;
            this.uioFlatPortInLoadInConvInPosLeft.Text = "物料到位左";
            // 
            // uioFlatPortInLoadInSMEMALoadReady
            // 
            this.uioFlatPortInLoadInSMEMALoadReady.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInLoadInSMEMALoadReady.HelpTip = null;
            this.uioFlatPortInLoadInSMEMALoadReady.Location = new System.Drawing.Point(120, 20);
            this.uioFlatPortInLoadInSMEMALoadReady.Name = "uioFlatPortInLoadInSMEMALoadReady";
            this.uioFlatPortInLoadInSMEMALoadReady.Port = null;
            this.uioFlatPortInLoadInSMEMALoadReady.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortInLoadInSMEMALoadReady.TabIndex = 0;
            this.uioFlatPortInLoadInSMEMALoadReady.Text = "SMEMA准备好";
            // 
            // uioFlatPortOutLoadInSMEMALoadAvailable
            // 
            this.uioFlatPortOutLoadInSMEMALoadAvailable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutLoadInSMEMALoadAvailable.HelpTip = null;
            this.uioFlatPortOutLoadInSMEMALoadAvailable.Location = new System.Drawing.Point(6, 20);
            this.uioFlatPortOutLoadInSMEMALoadAvailable.Name = "uioFlatPortOutLoadInSMEMALoadAvailable";
            this.uioFlatPortOutLoadInSMEMALoadAvailable.Port = null;
            this.uioFlatPortOutLoadInSMEMALoadAvailable.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortOutLoadInSMEMALoadAvailable.TabIndex = 0;
            this.uioFlatPortOutLoadInSMEMALoadAvailable.Text = "SMEMA空闲";
            // 
            // selectBoxIgnoreSMEMA
            // 
            this.selectBoxIgnoreSMEMA.AutoSize = true;
            this.selectBoxIgnoreSMEMA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxIgnoreSMEMA.BorderColor = System.Drawing.Color.Black;
            this.selectBoxIgnoreSMEMA.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxIgnoreSMEMA.Location = new System.Drawing.Point(6, 18);
            this.selectBoxIgnoreSMEMA.Name = "selectBoxIgnoreSMEMA";
            this.selectBoxIgnoreSMEMA.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.selectBoxIgnoreSMEMA.Size = new System.Drawing.Size(131, 22);
            this.selectBoxIgnoreSMEMA.TabIndex = 7;
            this.selectBoxIgnoreSMEMA.Text = "忽略上料SMEMA信号";
            this.selectBoxIgnoreSMEMA.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxIgnoreSMEMA.UseVisualStyleBackColor = true;
            this.selectBoxIgnoreSMEMA.CheckedChanged += new System.EventHandler(this.selectBoxIgnoreSMEMA_CheckedChanged);
            // 
            // uioFlatPortInLoadInConvInPosMid
            // 
            this.uioFlatPortInLoadInConvInPosMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInLoadInConvInPosMid.HelpTip = null;
            this.uioFlatPortInLoadInConvInPosMid.Location = new System.Drawing.Point(6, 86);
            this.uioFlatPortInLoadInConvInPosMid.Name = "uioFlatPortInLoadInConvInPosMid";
            this.uioFlatPortInLoadInConvInPosMid.Port = null;
            this.uioFlatPortInLoadInConvInPosMid.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortInLoadInConvInPosMid.TabIndex = 0;
            this.uioFlatPortInLoadInConvInPosMid.Text = "物料到位中";
            // 
            // uioFlatPortInLoadInConvInPosRight
            // 
            this.uioFlatPortInLoadInConvInPosRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInLoadInConvInPosRight.HelpTip = null;
            this.uioFlatPortInLoadInConvInPosRight.Location = new System.Drawing.Point(6, 119);
            this.uioFlatPortInLoadInConvInPosRight.Name = "uioFlatPortInLoadInConvInPosRight";
            this.uioFlatPortInLoadInConvInPosRight.Port = null;
            this.uioFlatPortInLoadInConvInPosRight.Size = new System.Drawing.Size(108, 27);
            this.uioFlatPortInLoadInConvInPosRight.TabIndex = 0;
            this.uioFlatPortInLoadInConvInPosRight.Text = "物料到位右";
            // 
            // SettingPanelZone上料传送
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SettingPanelZone上料传送";
            this.Size = new System.Drawing.Size(405, 198);
            this.groupBoxInport.ResumeLayout(false);
            this.groupBoxOutPort.ResumeLayout(false);
            this.groupBoxParameter.ResumeLayout(false);
            this.groupBoxParameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutLoadInSMEMALoadAvailable;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInLoadInSMEMALoadReady;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInLoadInConvInPosLeft;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInLoadInConvLoad;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxIgnoreSMEMA;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInLoadInConvInPosRight;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInLoadInConvInPosMid;
    }
}
