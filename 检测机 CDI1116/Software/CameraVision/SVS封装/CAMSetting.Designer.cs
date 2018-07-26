namespace SVSCamera
{
    partial class CAMSetting
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cameraSetting1 = new SVSCamera.CameraSetting();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(164, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 333);
            this.panel1.TabIndex = 1;
            // 
            // cameraSetting1
            // 
            this.cameraSetting1.AutoSize = true;
            this.cameraSetting1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cameraSetting1.DispPanel = null;
            this.cameraSetting1.Location = new System.Drawing.Point(1, 0);
            this.cameraSetting1.Name = "cameraSetting1";
            this.cameraSetting1.Size = new System.Drawing.Size(157, 294);
            this.cameraSetting1.TabIndex = 0;
            // 
            // CAMSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 333);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cameraSetting1);
            this.Name = "CAMSetting";
            this.Text = "CAMSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CAMSetting_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CameraSetting cameraSetting1;
        private System.Windows.Forms.Panel panel1;
    }
}