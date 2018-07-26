namespace CDI.GUI
{
    partial class BaseZoneManualPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonPortReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStatus.Location = new System.Drawing.Point(3, 335);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(18, 25);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = " ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(329, 324);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(110, 35);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "复位";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(750, 311);
            this.buttonPortReset.Name = "buttonPortReset";
            this.buttonPortReset.Size = new System.Drawing.Size(110, 35);
            this.buttonPortReset.TabIndex = 4;
            this.buttonPortReset.Text = "区域内气路关闭，气缸上移";
            this.buttonPortReset.UseVisualStyleBackColor = true;
            this.buttonPortReset.Click += new System.EventHandler(this.buttonPortReset_Click);
            // 
            // BaseZoneManualPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.buttonPortReset);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelStatus);
            this.Name = "BaseZoneManualPanel";
            this.Size = new System.Drawing.Size(863, 495);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label labelStatus;
        protected System.Windows.Forms.Button buttonReset;
        protected System.Windows.Forms.Button buttonPortReset;
    }
}
