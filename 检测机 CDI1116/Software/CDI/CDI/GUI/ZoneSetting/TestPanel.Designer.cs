namespace CDI.GUI
{
    partial class TestPanel
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonShowAlarm = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.optionBoxResultNG = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBoxResultOK = new Colibri.CommonModule.ToolBox.OptionBox();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(32, 34);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(73, 50);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "开启流程开始状态";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(124, 34);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(73, 50);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "关闭流程开始状态";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonShowAlarm
            // 
            this.buttonShowAlarm.Location = new System.Drawing.Point(32, 109);
            this.buttonShowAlarm.Name = "buttonShowAlarm";
            this.buttonShowAlarm.Size = new System.Drawing.Size(73, 50);
            this.buttonShowAlarm.TabIndex = 0;
            this.buttonShowAlarm.Text = "显示Alarm";
            this.buttonShowAlarm.UseVisualStyleBackColor = true;
            this.buttonShowAlarm.Click += new System.EventHandler(this.buttonShowAlarm_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(234, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(345, 407);
            this.listBox1.TabIndex = 1;
            // 
            // optionBoxResultNG
            // 
            this.optionBoxResultNG.AutoSize = true;
            this.optionBoxResultNG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxResultNG.BorderColor = System.Drawing.Color.Black;
            this.optionBoxResultNG.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxResultNG.ForeColor = System.Drawing.Color.OrangeRed;
            this.optionBoxResultNG.Group = 1;
            this.optionBoxResultNG.Location = new System.Drawing.Point(32, 211);
            this.optionBoxResultNG.Name = "optionBoxResultNG";
            this.optionBoxResultNG.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.optionBoxResultNG.Size = new System.Drawing.Size(74, 23);
            this.optionBoxResultNG.TabIndex = 393;
            this.optionBoxResultNG.Text = "结果NG";
            this.optionBoxResultNG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.optionBoxResultNG.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxResultNG.UseVisualStyleBackColor = true;
            // 
            // optionBoxResultOK
            // 
            this.optionBoxResultOK.AutoSize = true;
            this.optionBoxResultOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxResultOK.BorderColor = System.Drawing.Color.Black;
            this.optionBoxResultOK.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxResultOK.Checked = true;
            this.optionBoxResultOK.ForeColor = System.Drawing.Color.OrangeRed;
            this.optionBoxResultOK.Group = 1;
            this.optionBoxResultOK.GroupChecked = true;
            this.optionBoxResultOK.Location = new System.Drawing.Point(32, 175);
            this.optionBoxResultOK.Name = "optionBoxResultOK";
            this.optionBoxResultOK.Padding = new System.Windows.Forms.Padding(17, 0, 0, 0);
            this.optionBoxResultOK.Size = new System.Drawing.Size(73, 23);
            this.optionBoxResultOK.TabIndex = 394;
            this.optionBoxResultOK.Text = "结果OK";
            this.optionBoxResultOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.optionBoxResultOK.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxResultOK.UseVisualStyleBackColor = true;
            // 
            // TestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optionBoxResultNG);
            this.Controls.Add(this.optionBoxResultOK);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonShowAlarm);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "TestPanel";
            this.Size = new System.Drawing.Size(579, 407);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonShowAlarm;
        private System.Windows.Forms.ListBox listBox1;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxResultNG;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxResultOK;
    }
}
