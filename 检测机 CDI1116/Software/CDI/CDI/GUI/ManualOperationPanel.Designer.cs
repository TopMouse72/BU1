namespace CDI.GUI
{
    partial class ManualOperationPanel
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
            this.tabControlZone = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControlZone
            // 
            this.tabControlZone.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlZone.Location = new System.Drawing.Point(0, 0);
            this.tabControlZone.Name = "tabControlZone";
            this.tabControlZone.SelectedIndex = 0;
            this.tabControlZone.Size = new System.Drawing.Size(906, 702);
            this.tabControlZone.TabIndex = 0;
            // 
            // ManualOperationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlZone);
            this.Name = "ManualOperationPanel";
            this.Size = new System.Drawing.Size(906, 702);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlZone;
    }
}
