namespace CDI.GUI
{
    partial class ManualPanelZone外框架
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
            this.buttonSystemReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 179);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 240);
            this.buttonReset.Visible = false;
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(134, 240);
            this.buttonPortReset.Visible = false;
            // 
            // buttonSystemReset
            // 
            this.buttonSystemReset.Location = new System.Drawing.Point(3, 141);
            this.buttonSystemReset.Name = "buttonSystemReset";
            this.buttonSystemReset.Size = new System.Drawing.Size(110, 35);
            this.buttonSystemReset.TabIndex = 11;
            this.buttonSystemReset.Text = "系统复位";
            this.buttonSystemReset.UseVisualStyleBackColor = true;
            this.buttonSystemReset.Click += new System.EventHandler(this.buttonSystemReset_Click);
            // 
            // ManualPanelZone外框架
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.buttonSystemReset);
            this.Name = "ManualPanelZone外框架";
            this.Size = new System.Drawing.Size(809, 481);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.buttonSystemReset, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSystemReset;
    }
}
