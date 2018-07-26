namespace CDI.GUI
{
    partial class VacuumSettingDisp
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectBoxVacuumFrontEnabled = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxVacuumCentEnabled = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxVacuumBackEnabled = new Colibri.CommonModule.ToolBox.SelectBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectBoxVacuumFrontEnabled);
            this.groupBox1.Controls.Add(this.selectBoxVacuumCentEnabled);
            this.groupBox1.Controls.Add(this.selectBoxVacuumBackEnabled);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // selectBoxVacuumFrontEnabled
            // 
            this.selectBoxVacuumFrontEnabled.AutoSize = true;
            this.selectBoxVacuumFrontEnabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxVacuumFrontEnabled.BorderColor = System.Drawing.Color.Black;
            this.selectBoxVacuumFrontEnabled.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxVacuumFrontEnabled.Location = new System.Drawing.Point(6, 94);
            this.selectBoxVacuumFrontEnabled.Name = "selectBoxVacuumFrontEnabled";
            this.selectBoxVacuumFrontEnabled.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.selectBoxVacuumFrontEnabled.Size = new System.Drawing.Size(169, 30);
            this.selectBoxVacuumFrontEnabled.TabIndex = 184;
            this.selectBoxVacuumFrontEnabled.Text = "使用真空前吸物料";
            this.selectBoxVacuumFrontEnabled.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxVacuumFrontEnabled.UseVisualStyleBackColor = true;
            // 
            // selectBoxVacuumCentEnabled
            // 
            this.selectBoxVacuumCentEnabled.AutoSize = true;
            this.selectBoxVacuumCentEnabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxVacuumCentEnabled.BorderColor = System.Drawing.Color.Black;
            this.selectBoxVacuumCentEnabled.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxVacuumCentEnabled.Location = new System.Drawing.Point(6, 58);
            this.selectBoxVacuumCentEnabled.Name = "selectBoxVacuumCentEnabled";
            this.selectBoxVacuumCentEnabled.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.selectBoxVacuumCentEnabled.Size = new System.Drawing.Size(169, 30);
            this.selectBoxVacuumCentEnabled.TabIndex = 183;
            this.selectBoxVacuumCentEnabled.Text = "使用真空中吸物料";
            this.selectBoxVacuumCentEnabled.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxVacuumCentEnabled.UseVisualStyleBackColor = true;
            // 
            // selectBoxVacuumBackEnabled
            // 
            this.selectBoxVacuumBackEnabled.AutoSize = true;
            this.selectBoxVacuumBackEnabled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxVacuumBackEnabled.BorderColor = System.Drawing.Color.Black;
            this.selectBoxVacuumBackEnabled.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxVacuumBackEnabled.Location = new System.Drawing.Point(6, 22);
            this.selectBoxVacuumBackEnabled.Name = "selectBoxVacuumBackEnabled";
            this.selectBoxVacuumBackEnabled.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.selectBoxVacuumBackEnabled.Size = new System.Drawing.Size(169, 30);
            this.selectBoxVacuumBackEnabled.TabIndex = 182;
            this.selectBoxVacuumBackEnabled.Text = "使用真空后吸物料";
            this.selectBoxVacuumBackEnabled.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxVacuumBackEnabled.UseVisualStyleBackColor = true;
            // 
            // VacuumSettingDisp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "VacuumSettingDisp";
            this.Size = new System.Drawing.Size(189, 138);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxVacuumFrontEnabled;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxVacuumCentEnabled;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxVacuumBackEnabled;
    }
}
