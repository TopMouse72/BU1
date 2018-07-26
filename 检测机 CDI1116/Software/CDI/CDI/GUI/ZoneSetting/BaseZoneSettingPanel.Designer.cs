namespace CDI.GUI
{
    partial class BaseZoneSettingPanel
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
            this.groupBoxInport = new System.Windows.Forms.GroupBox();
            this.groupBoxOutPort = new System.Windows.Forms.GroupBox();
            this.comboBoxAxis = new System.Windows.Forms.ComboBox();
            this.labelAxis = new System.Windows.Forms.Label();
            this.groupBoxParameter = new System.Windows.Forms.GroupBox();
            this.buttonSavePara = new System.Windows.Forms.Button();
            this.groupBoxParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInport
            // 
            this.groupBoxInport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxInport.Location = new System.Drawing.Point(3, 3);
            this.groupBoxInport.Name = "groupBoxInport";
            this.groupBoxInport.Size = new System.Drawing.Size(190, 148);
            this.groupBoxInport.TabIndex = 3;
            this.groupBoxInport.TabStop = false;
            this.groupBoxInport.Text = "输入端口";
            // 
            // groupBoxOutPort
            // 
            this.groupBoxOutPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxOutPort.Location = new System.Drawing.Point(199, 3);
            this.groupBoxOutPort.Name = "groupBoxOutPort";
            this.groupBoxOutPort.Size = new System.Drawing.Size(282, 148);
            this.groupBoxOutPort.TabIndex = 4;
            this.groupBoxOutPort.TabStop = false;
            this.groupBoxOutPort.Text = "输出端口";
            // 
            // comboBoxAxis
            // 
            this.comboBoxAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAxis.FormattingEnabled = true;
            this.comboBoxAxis.Location = new System.Drawing.Point(39, 156);
            this.comboBoxAxis.Name = "comboBoxAxis";
            this.comboBoxAxis.Size = new System.Drawing.Size(140, 20);
            this.comboBoxAxis.TabIndex = 5;
            this.comboBoxAxis.SelectedIndexChanged += new System.EventHandler(this.comboBoxAxis_SelectedIndexChanged);
            // 
            // labelAxis
            // 
            this.labelAxis.AutoSize = true;
            this.labelAxis.Location = new System.Drawing.Point(2, 159);
            this.labelAxis.Name = "labelAxis";
            this.labelAxis.Size = new System.Drawing.Size(29, 12);
            this.labelAxis.TabIndex = 6;
            this.labelAxis.Text = "电机";
            // 
            // groupBoxParameter
            // 
            this.groupBoxParameter.Controls.Add(this.buttonSavePara);
            this.groupBoxParameter.Location = new System.Drawing.Point(286, 159);
            this.groupBoxParameter.Name = "groupBoxParameter";
            this.groupBoxParameter.Size = new System.Drawing.Size(198, 114);
            this.groupBoxParameter.TabIndex = 9;
            this.groupBoxParameter.TabStop = false;
            this.groupBoxParameter.Text = "参数";
            this.groupBoxParameter.Visible = false;
            // 
            // buttonSavePara
            // 
            this.buttonSavePara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSavePara.Location = new System.Drawing.Point(121, 81);
            this.buttonSavePara.Name = "buttonSavePara";
            this.buttonSavePara.Size = new System.Drawing.Size(70, 27);
            this.buttonSavePara.TabIndex = 0;
            this.buttonSavePara.Text = "保存";
            this.buttonSavePara.UseVisualStyleBackColor = true;
            // 
            // BaseZoneSettingPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBoxParameter);
            this.Controls.Add(this.labelAxis);
            this.Controls.Add(this.groupBoxInport);
            this.Controls.Add(this.comboBoxAxis);
            this.Controls.Add(this.groupBoxOutPort);
            this.Name = "BaseZoneSettingPanel";
            this.Size = new System.Drawing.Size(487, 276);
            this.groupBoxParameter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxInport;
        protected System.Windows.Forms.GroupBox groupBoxOutPort;
        public System.Windows.Forms.ComboBox comboBoxAxis;
        public System.Windows.Forms.Label labelAxis;
        protected System.Windows.Forms.GroupBox groupBoxParameter;
        protected System.Windows.Forms.Button buttonSavePara;
    }
}
