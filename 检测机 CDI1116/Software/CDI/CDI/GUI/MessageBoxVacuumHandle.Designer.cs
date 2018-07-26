namespace CDI.GUI
{
    partial class MessageBoxVacuumHandle
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
            this.selectBoxLeft = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxMiddle = new Colibri.CommonModule.ToolBox.SelectBox();
            this.selectBoxRight = new Colibri.CommonModule.ToolBox.SelectBox();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonIgnore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectBoxLeft
            // 
            this.selectBoxLeft.AutoSize = true;
            this.selectBoxLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxLeft.BorderColor = System.Drawing.Color.Black;
            this.selectBoxLeft.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxLeft.Location = new System.Drawing.Point(120, 12);
            this.selectBoxLeft.Name = "selectBoxLeft";
            this.selectBoxLeft.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.selectBoxLeft.Size = new System.Drawing.Size(65, 22);
            this.selectBoxLeft.TabIndex = 0;
            this.selectBoxLeft.Text = "左工位";
            this.selectBoxLeft.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxLeft.UseVisualStyleBackColor = true;
            // 
            // selectBoxMiddle
            // 
            this.selectBoxMiddle.AutoSize = true;
            this.selectBoxMiddle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxMiddle.BorderColor = System.Drawing.Color.Black;
            this.selectBoxMiddle.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxMiddle.Location = new System.Drawing.Point(216, 12);
            this.selectBoxMiddle.Name = "selectBoxMiddle";
            this.selectBoxMiddle.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.selectBoxMiddle.Size = new System.Drawing.Size(65, 22);
            this.selectBoxMiddle.TabIndex = 1;
            this.selectBoxMiddle.Text = "中工位";
            this.selectBoxMiddle.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxMiddle.UseVisualStyleBackColor = true;
            // 
            // selectBoxRight
            // 
            this.selectBoxRight.AutoSize = true;
            this.selectBoxRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxRight.BorderColor = System.Drawing.Color.Black;
            this.selectBoxRight.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxRight.Location = new System.Drawing.Point(312, 12);
            this.selectBoxRight.Name = "selectBoxRight";
            this.selectBoxRight.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.selectBoxRight.Size = new System.Drawing.Size(65, 22);
            this.selectBoxRight.TabIndex = 2;
            this.selectBoxRight.Text = "右工位";
            this.selectBoxRight.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxRight.UseVisualStyleBackColor = true;
            // 
            // buttonRetry
            // 
            this.buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRetry.Location = new System.Drawing.Point(195, 106);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(91, 31);
            this.buttonRetry.TabIndex = 3;
            this.buttonRetry.Text = "重试";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "真空出错电芯：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "如果想对这些电芯重新进行操作，点“重试”按钮。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(377, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "如果想忽略掉这些电芯，选择并取出要忽略的电芯后点“忽略”按钮。";
            // 
            // buttonIgnore
            // 
            this.buttonIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIgnore.Location = new System.Drawing.Point(312, 106);
            this.buttonIgnore.Name = "buttonIgnore";
            this.buttonIgnore.Size = new System.Drawing.Size(91, 31);
            this.buttonIgnore.TabIndex = 3;
            this.buttonIgnore.Text = "忽略";
            this.buttonIgnore.UseVisualStyleBackColor = true;
            this.buttonIgnore.Click += new System.EventHandler(this.buttonIgnore_Click);
            // 
            // MessageBoxVacuumHandle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 149);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonIgnore);
            this.Controls.Add(this.buttonRetry);
            this.Controls.Add(this.selectBoxRight);
            this.Controls.Add(this.selectBoxMiddle);
            this.Controls.Add(this.selectBoxLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxVacuumHandle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "真空错误处理";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Colibri.CommonModule.ToolBox.SelectBox selectBoxLeft;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxMiddle;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxRight;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonIgnore;
    }
}