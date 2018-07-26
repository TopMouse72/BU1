namespace CDI.GUI
{
    partial class SettingPanelZone厚度测量
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
            this.uioFlatPortInThicknessMeasCyLeftUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasCyMidUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasCyRightUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasCyRightDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasCyMidDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasCyLeftDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasVacSensRight = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasVacSensMid = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortInThicknessMeasVacSensLeft = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyLeftUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyMidUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyRightUp = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyLeftDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyMidDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasCyRightDown = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasVacLeft = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasVacMid = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasVacRight = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.uioFlatPortOutThicknessMeasBlow = new Colibri.CommonModule.ToolBox.UIOFlatPort();
            this.textBoxDelayTime = new System.Windows.Forms.TextBox();
            this.labelDelayTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxInport.SuspendLayout();
            this.groupBoxOutPort.SuspendLayout();
            this.groupBoxParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInport
            // 
            this.groupBoxInport.AutoSize = true;
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasVacSensRight);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasVacSensMid);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasVacSensLeft);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyRightDown);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyMidDown);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyLeftDown);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyRightUp);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyMidUp);
            this.groupBoxInport.Controls.Add(this.uioFlatPortInThicknessMeasCyLeftUp);
            this.groupBoxInport.Size = new System.Drawing.Size(348, 141);
            // 
            // groupBoxOutPort
            // 
            this.groupBoxOutPort.AutoSize = true;
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasVacRight);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyLeftUp);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasVacMid);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyMidUp);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasBlow);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasVacLeft);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyRightUp);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyRightDown);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyLeftDown);
            this.groupBoxOutPort.Controls.Add(this.uioFlatPortOutThicknessMeasCyMidDown);
            this.groupBoxOutPort.Location = new System.Drawing.Point(357, 3);
            this.groupBoxOutPort.Size = new System.Drawing.Size(348, 177);
            // 
            // comboBoxAxis
            // 
            this.comboBoxAxis.Location = new System.Drawing.Point(41, 161);
            // 
            // labelAxis
            // 
            this.labelAxis.Location = new System.Drawing.Point(4, 165);
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Controls.Add(this.label2);
            this.groupBoxParameter.Controls.Add(this.labelDelayTime);
            this.groupBoxParameter.Controls.Add(this.textBoxDelayTime);
            this.groupBoxParameter.Location = new System.Drawing.Point(510, 190);
            this.groupBoxParameter.Size = new System.Drawing.Size(195, 91);
            this.groupBoxParameter.Controls.SetChildIndex(this.textBoxDelayTime, 0);
            this.groupBoxParameter.Controls.SetChildIndex(this.labelDelayTime, 0);
            this.groupBoxParameter.Controls.SetChildIndex(this.label2, 0);
            this.groupBoxParameter.Controls.SetChildIndex(this.buttonSavePara, 0);
            // 
            // buttonSavePara
            // 
            this.buttonSavePara.Location = new System.Drawing.Point(119, 56);
            this.buttonSavePara.Click += new System.EventHandler(this.buttonSavePara_Click);
            // 
            // uioFlatPortInThicknessMeasCyLeftUp
            // 
            this.uioFlatPortInThicknessMeasCyLeftUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyLeftUp.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyLeftUp.Location = new System.Drawing.Point(6, 22);
            this.uioFlatPortInThicknessMeasCyLeftUp.Name = "uioFlatPortInThicknessMeasCyLeftUp";
            this.uioFlatPortInThicknessMeasCyLeftUp.Port = null;
            this.uioFlatPortInThicknessMeasCyLeftUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyLeftUp.TabIndex = 1;
            this.uioFlatPortInThicknessMeasCyLeftUp.Text = "左气缸上";
            // 
            // uioFlatPortInThicknessMeasCyMidUp
            // 
            this.uioFlatPortInThicknessMeasCyMidUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyMidUp.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyMidUp.Location = new System.Drawing.Point(120, 22);
            this.uioFlatPortInThicknessMeasCyMidUp.Name = "uioFlatPortInThicknessMeasCyMidUp";
            this.uioFlatPortInThicknessMeasCyMidUp.Port = null;
            this.uioFlatPortInThicknessMeasCyMidUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyMidUp.TabIndex = 2;
            this.uioFlatPortInThicknessMeasCyMidUp.Text = "中气缸上";
            // 
            // uioFlatPortInThicknessMeasCyRightUp
            // 
            this.uioFlatPortInThicknessMeasCyRightUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyRightUp.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyRightUp.Location = new System.Drawing.Point(234, 22);
            this.uioFlatPortInThicknessMeasCyRightUp.Name = "uioFlatPortInThicknessMeasCyRightUp";
            this.uioFlatPortInThicknessMeasCyRightUp.Port = null;
            this.uioFlatPortInThicknessMeasCyRightUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyRightUp.TabIndex = 3;
            this.uioFlatPortInThicknessMeasCyRightUp.Text = "右气缸上";
            // 
            // uioFlatPortInThicknessMeasCyRightDown
            // 
            this.uioFlatPortInThicknessMeasCyRightDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyRightDown.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyRightDown.Location = new System.Drawing.Point(234, 57);
            this.uioFlatPortInThicknessMeasCyRightDown.Name = "uioFlatPortInThicknessMeasCyRightDown";
            this.uioFlatPortInThicknessMeasCyRightDown.Port = null;
            this.uioFlatPortInThicknessMeasCyRightDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyRightDown.TabIndex = 6;
            this.uioFlatPortInThicknessMeasCyRightDown.Text = "右气缸下";
            // 
            // uioFlatPortInThicknessMeasCyMidDown
            // 
            this.uioFlatPortInThicknessMeasCyMidDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyMidDown.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyMidDown.Location = new System.Drawing.Point(120, 57);
            this.uioFlatPortInThicknessMeasCyMidDown.Name = "uioFlatPortInThicknessMeasCyMidDown";
            this.uioFlatPortInThicknessMeasCyMidDown.Port = null;
            this.uioFlatPortInThicknessMeasCyMidDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyMidDown.TabIndex = 5;
            this.uioFlatPortInThicknessMeasCyMidDown.Text = "中气缸下";
            // 
            // uioFlatPortInThicknessMeasCyLeftDown
            // 
            this.uioFlatPortInThicknessMeasCyLeftDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasCyLeftDown.HelpTip = null;
            this.uioFlatPortInThicknessMeasCyLeftDown.Location = new System.Drawing.Point(6, 57);
            this.uioFlatPortInThicknessMeasCyLeftDown.Name = "uioFlatPortInThicknessMeasCyLeftDown";
            this.uioFlatPortInThicknessMeasCyLeftDown.Port = null;
            this.uioFlatPortInThicknessMeasCyLeftDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasCyLeftDown.TabIndex = 4;
            this.uioFlatPortInThicknessMeasCyLeftDown.Text = "左气缸下";
            // 
            // uioFlatPortInThicknessMeasVacSensRight
            // 
            this.uioFlatPortInThicknessMeasVacSensRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasVacSensRight.HelpTip = null;
            this.uioFlatPortInThicknessMeasVacSensRight.Location = new System.Drawing.Point(234, 93);
            this.uioFlatPortInThicknessMeasVacSensRight.Name = "uioFlatPortInThicknessMeasVacSensRight";
            this.uioFlatPortInThicknessMeasVacSensRight.Port = null;
            this.uioFlatPortInThicknessMeasVacSensRight.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasVacSensRight.TabIndex = 9;
            this.uioFlatPortInThicknessMeasVacSensRight.Text = "右真空开关";
            // 
            // uioFlatPortInThicknessMeasVacSensMid
            // 
            this.uioFlatPortInThicknessMeasVacSensMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasVacSensMid.HelpTip = null;
            this.uioFlatPortInThicknessMeasVacSensMid.Location = new System.Drawing.Point(120, 93);
            this.uioFlatPortInThicknessMeasVacSensMid.Name = "uioFlatPortInThicknessMeasVacSensMid";
            this.uioFlatPortInThicknessMeasVacSensMid.Port = null;
            this.uioFlatPortInThicknessMeasVacSensMid.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasVacSensMid.TabIndex = 8;
            this.uioFlatPortInThicknessMeasVacSensMid.Text = "中真空开关";
            // 
            // uioFlatPortInThicknessMeasVacSensLeft
            // 
            this.uioFlatPortInThicknessMeasVacSensLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortInThicknessMeasVacSensLeft.HelpTip = null;
            this.uioFlatPortInThicknessMeasVacSensLeft.Location = new System.Drawing.Point(6, 93);
            this.uioFlatPortInThicknessMeasVacSensLeft.Name = "uioFlatPortInThicknessMeasVacSensLeft";
            this.uioFlatPortInThicknessMeasVacSensLeft.Port = null;
            this.uioFlatPortInThicknessMeasVacSensLeft.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortInThicknessMeasVacSensLeft.TabIndex = 7;
            this.uioFlatPortInThicknessMeasVacSensLeft.Text = "左真空开关";
            // 
            // uioFlatPortOutThicknessMeasCyLeftUp
            // 
            this.uioFlatPortOutThicknessMeasCyLeftUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyLeftUp.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyLeftUp.Location = new System.Drawing.Point(6, 22);
            this.uioFlatPortOutThicknessMeasCyLeftUp.Name = "uioFlatPortOutThicknessMeasCyLeftUp";
            this.uioFlatPortOutThicknessMeasCyLeftUp.Port = null;
            this.uioFlatPortOutThicknessMeasCyLeftUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyLeftUp.TabIndex = 1;
            this.uioFlatPortOutThicknessMeasCyLeftUp.Text = "左气缸上";
            // 
            // uioFlatPortOutThicknessMeasCyMidUp
            // 
            this.uioFlatPortOutThicknessMeasCyMidUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyMidUp.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyMidUp.Location = new System.Drawing.Point(120, 22);
            this.uioFlatPortOutThicknessMeasCyMidUp.Name = "uioFlatPortOutThicknessMeasCyMidUp";
            this.uioFlatPortOutThicknessMeasCyMidUp.Port = null;
            this.uioFlatPortOutThicknessMeasCyMidUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyMidUp.TabIndex = 2;
            this.uioFlatPortOutThicknessMeasCyMidUp.Text = "中气缸上";
            // 
            // uioFlatPortOutThicknessMeasCyRightUp
            // 
            this.uioFlatPortOutThicknessMeasCyRightUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyRightUp.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyRightUp.Location = new System.Drawing.Point(234, 22);
            this.uioFlatPortOutThicknessMeasCyRightUp.Name = "uioFlatPortOutThicknessMeasCyRightUp";
            this.uioFlatPortOutThicknessMeasCyRightUp.Port = null;
            this.uioFlatPortOutThicknessMeasCyRightUp.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyRightUp.TabIndex = 3;
            this.uioFlatPortOutThicknessMeasCyRightUp.Text = "右气缸上";
            // 
            // uioFlatPortOutThicknessMeasCyLeftDown
            // 
            this.uioFlatPortOutThicknessMeasCyLeftDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyLeftDown.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyLeftDown.Location = new System.Drawing.Point(6, 57);
            this.uioFlatPortOutThicknessMeasCyLeftDown.Name = "uioFlatPortOutThicknessMeasCyLeftDown";
            this.uioFlatPortOutThicknessMeasCyLeftDown.Port = null;
            this.uioFlatPortOutThicknessMeasCyLeftDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyLeftDown.TabIndex = 4;
            this.uioFlatPortOutThicknessMeasCyLeftDown.Text = "左气缸下";
            // 
            // uioFlatPortOutThicknessMeasCyMidDown
            // 
            this.uioFlatPortOutThicknessMeasCyMidDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyMidDown.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyMidDown.Location = new System.Drawing.Point(120, 57);
            this.uioFlatPortOutThicknessMeasCyMidDown.Name = "uioFlatPortOutThicknessMeasCyMidDown";
            this.uioFlatPortOutThicknessMeasCyMidDown.Port = null;
            this.uioFlatPortOutThicknessMeasCyMidDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyMidDown.TabIndex = 5;
            this.uioFlatPortOutThicknessMeasCyMidDown.Text = "中气缸下";
            // 
            // uioFlatPortOutThicknessMeasCyRightDown
            // 
            this.uioFlatPortOutThicknessMeasCyRightDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasCyRightDown.HelpTip = null;
            this.uioFlatPortOutThicknessMeasCyRightDown.Location = new System.Drawing.Point(234, 57);
            this.uioFlatPortOutThicknessMeasCyRightDown.Name = "uioFlatPortOutThicknessMeasCyRightDown";
            this.uioFlatPortOutThicknessMeasCyRightDown.Port = null;
            this.uioFlatPortOutThicknessMeasCyRightDown.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasCyRightDown.TabIndex = 6;
            this.uioFlatPortOutThicknessMeasCyRightDown.Text = "右气缸下";
            // 
            // uioFlatPortOutThicknessMeasVacLeft
            // 
            this.uioFlatPortOutThicknessMeasVacLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasVacLeft.HelpTip = null;
            this.uioFlatPortOutThicknessMeasVacLeft.Location = new System.Drawing.Point(6, 93);
            this.uioFlatPortOutThicknessMeasVacLeft.Name = "uioFlatPortOutThicknessMeasVacLeft";
            this.uioFlatPortOutThicknessMeasVacLeft.Port = null;
            this.uioFlatPortOutThicknessMeasVacLeft.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasVacLeft.TabIndex = 7;
            this.uioFlatPortOutThicknessMeasVacLeft.Text = "左真空开关";
            // 
            // uioFlatPortOutThicknessMeasVacMid
            // 
            this.uioFlatPortOutThicknessMeasVacMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasVacMid.HelpTip = null;
            this.uioFlatPortOutThicknessMeasVacMid.Location = new System.Drawing.Point(120, 93);
            this.uioFlatPortOutThicknessMeasVacMid.Name = "uioFlatPortOutThicknessMeasVacMid";
            this.uioFlatPortOutThicknessMeasVacMid.Port = null;
            this.uioFlatPortOutThicknessMeasVacMid.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasVacMid.TabIndex = 8;
            this.uioFlatPortOutThicknessMeasVacMid.Text = "中真空开关";
            // 
            // uioFlatPortOutThicknessMeasVacRight
            // 
            this.uioFlatPortOutThicknessMeasVacRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasVacRight.HelpTip = null;
            this.uioFlatPortOutThicknessMeasVacRight.Location = new System.Drawing.Point(234, 93);
            this.uioFlatPortOutThicknessMeasVacRight.Name = "uioFlatPortOutThicknessMeasVacRight";
            this.uioFlatPortOutThicknessMeasVacRight.Port = null;
            this.uioFlatPortOutThicknessMeasVacRight.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasVacRight.TabIndex = 9;
            this.uioFlatPortOutThicknessMeasVacRight.Text = "右真空开关";
            // 
            // uioFlatPortOutThicknessMeasBlow
            // 
            this.uioFlatPortOutThicknessMeasBlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uioFlatPortOutThicknessMeasBlow.HelpTip = null;
            this.uioFlatPortOutThicknessMeasBlow.Location = new System.Drawing.Point(6, 129);
            this.uioFlatPortOutThicknessMeasBlow.Name = "uioFlatPortOutThicknessMeasBlow";
            this.uioFlatPortOutThicknessMeasBlow.Port = null;
            this.uioFlatPortOutThicknessMeasBlow.Size = new System.Drawing.Size(108, 29);
            this.uioFlatPortOutThicknessMeasBlow.TabIndex = 7;
            this.uioFlatPortOutThicknessMeasBlow.Text = "吹气";
            // 
            // textBoxDelayTime
            // 
            this.textBoxDelayTime.Location = new System.Drawing.Point(67, 20);
            this.textBoxDelayTime.Name = "textBoxDelayTime";
            this.textBoxDelayTime.Size = new System.Drawing.Size(59, 20);
            this.textBoxDelayTime.TabIndex = 1;
            // 
            // labelDelayTime
            // 
            this.labelDelayTime.AutoSize = true;
            this.labelDelayTime.Location = new System.Drawing.Point(6, 22);
            this.labelDelayTime.Name = "labelDelayTime";
            this.labelDelayTime.Size = new System.Drawing.Size(55, 13);
            this.labelDelayTime.TabIndex = 2;
            this.labelDelayTime.Text = "测量延时";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "毫秒";
            // 
            // SettingPanelZone厚度测量
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SettingPanelZone厚度测量";
            this.Size = new System.Drawing.Size(708, 284);
            this.groupBoxInport.ResumeLayout(false);
            this.groupBoxOutPort.ResumeLayout(false);
            this.groupBoxParameter.ResumeLayout(false);
            this.groupBoxParameter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyLeftUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasVacSensRight;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasVacSensMid;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasVacSensLeft;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyRightDown;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyMidDown;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyLeftDown;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyRightUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortInThicknessMeasCyMidUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasVacRight;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyLeftUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasVacMid;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyMidUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasBlow;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasVacLeft;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyRightUp;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyRightDown;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyLeftDown;
        private Colibri.CommonModule.ToolBox.UIOFlatPort uioFlatPortOutThicknessMeasCyMidDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDelayTime;
        private System.Windows.Forms.TextBox textBoxDelayTime;
    }
}
