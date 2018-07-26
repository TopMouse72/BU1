namespace SVS封装
{
    partial class CameraParam
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Selected_device = new System.Windows.Forms.Button();
            this.Selected_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AcquisitionMode_comboBox = new System.Windows.Forms.ComboBox();
            this.SoftTrigger_button = new System.Windows.Forms.Button();
            this.Cam_start = new System.Windows.Forms.Button();
            this.Cam_stop = new System.Windows.Forms.Button();
            this.SaveImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Cam_Width = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cam_Height = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Frame_trackBar = new System.Windows.Forms.TrackBar();
            this.Frame_value = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Exp_trackBar = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.Gain_trackBar = new System.Windows.Forms.TrackBar();
            this.Gain_value = new System.Windows.Forms.Label();
            this.Exp_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Frame_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exp_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gain_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 327);
            this.panel1.TabIndex = 0;
            // 
            // Selected_device
            // 
            this.Selected_device.Location = new System.Drawing.Point(363, 9);
            this.Selected_device.Name = "Selected_device";
            this.Selected_device.Size = new System.Drawing.Size(92, 26);
            this.Selected_device.TabIndex = 1;
            this.Selected_device.Text = "查找设备";
            this.Selected_device.UseVisualStyleBackColor = true;
            this.Selected_device.Click += new System.EventHandler(this.Selected_device_Click);
            // 
            // Selected_comboBox
            // 
            this.Selected_comboBox.FormattingEnabled = true;
            this.Selected_comboBox.Location = new System.Drawing.Point(363, 41);
            this.Selected_comboBox.Name = "Selected_comboBox";
            this.Selected_comboBox.Size = new System.Drawing.Size(222, 20);
            this.Selected_comboBox.TabIndex = 2;
            this.Selected_comboBox.SelectedIndexChanged += new System.EventHandler(this.Selected_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "取图模式：";
            // 
            // AcquisitionMode_comboBox
            // 
            this.AcquisitionMode_comboBox.FormattingEnabled = true;
            this.AcquisitionMode_comboBox.Items.AddRange(new object[] {
            "实时模式",
            "软触发模式",
            "外部触发模式"});
            this.AcquisitionMode_comboBox.Location = new System.Drawing.Point(365, 88);
            this.AcquisitionMode_comboBox.Name = "AcquisitionMode_comboBox";
            this.AcquisitionMode_comboBox.Size = new System.Drawing.Size(116, 20);
            this.AcquisitionMode_comboBox.TabIndex = 4;
            this.AcquisitionMode_comboBox.SelectedIndexChanged += new System.EventHandler(this.AcquisitionMode_comboBox_SelectedIndexChanged);
            // 
            // SoftTrigger_button
            // 
            this.SoftTrigger_button.Location = new System.Drawing.Point(509, 86);
            this.SoftTrigger_button.Name = "SoftTrigger_button";
            this.SoftTrigger_button.Size = new System.Drawing.Size(70, 22);
            this.SoftTrigger_button.TabIndex = 5;
            this.SoftTrigger_button.Text = "软触发";
            this.SoftTrigger_button.UseVisualStyleBackColor = true;
            this.SoftTrigger_button.Click += new System.EventHandler(this.SoftTrigger_button_Click);
            // 
            // Cam_start
            // 
            this.Cam_start.Location = new System.Drawing.Point(363, 118);
            this.Cam_start.Name = "Cam_start";
            this.Cam_start.Size = new System.Drawing.Size(74, 29);
            this.Cam_start.TabIndex = 6;
            this.Cam_start.Text = "开始取图";
            this.Cam_start.UseVisualStyleBackColor = true;
            this.Cam_start.Click += new System.EventHandler(this.Cam_start_Click);
            // 
            // Cam_stop
            // 
            this.Cam_stop.Location = new System.Drawing.Point(443, 118);
            this.Cam_stop.Name = "Cam_stop";
            this.Cam_stop.Size = new System.Drawing.Size(71, 29);
            this.Cam_stop.TabIndex = 7;
            this.Cam_stop.Text = "停止取图";
            this.Cam_stop.UseVisualStyleBackColor = true;
            this.Cam_stop.Click += new System.EventHandler(this.Cam_stop_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Location = new System.Drawing.Point(524, 118);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(67, 29);
            this.SaveImage.TabIndex = 8;
            this.SaveImage.Text = "保存图片";
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "分辨率：";
            // 
            // Cam_Width
            // 
            this.Cam_Width.AutoSize = true;
            this.Cam_Width.Location = new System.Drawing.Point(406, 167);
            this.Cam_Width.Name = "Cam_Width";
            this.Cam_Width.Size = new System.Drawing.Size(17, 12);
            this.Cam_Width.TabIndex = 10;
            this.Cam_Width.Text = "宽";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(443, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "X";
            // 
            // Cam_Height
            // 
            this.Cam_Height.AutoSize = true;
            this.Cam_Height.Location = new System.Drawing.Point(460, 167);
            this.Cam_Height.Name = "Cam_Height";
            this.Cam_Height.Size = new System.Drawing.Size(17, 12);
            this.Cam_Height.TabIndex = 12;
            this.Cam_Height.Text = "高";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(359, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "帧率：";
            // 
            // Frame_trackBar
            // 
            this.Frame_trackBar.Location = new System.Drawing.Point(387, 199);
            this.Frame_trackBar.Name = "Frame_trackBar";
            this.Frame_trackBar.Size = new System.Drawing.Size(150, 45);
            this.Frame_trackBar.TabIndex = 14;
            this.Frame_trackBar.Scroll += new System.EventHandler(this.Frame_trackBar_Scroll);
            // 
            // Frame_value
            // 
            this.Frame_value.AutoSize = true;
            this.Frame_value.Location = new System.Drawing.Point(543, 199);
            this.Frame_value.Name = "Frame_value";
            this.Frame_value.Size = new System.Drawing.Size(35, 12);
            this.Frame_value.TabIndex = 15;
            this.Frame_value.Text = "Frame";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(359, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "曝光：";
            // 
            // Exp_trackBar
            // 
            this.Exp_trackBar.Location = new System.Drawing.Point(387, 241);
            this.Exp_trackBar.Name = "Exp_trackBar";
            this.Exp_trackBar.Size = new System.Drawing.Size(127, 45);
            this.Exp_trackBar.TabIndex = 14;
            this.Exp_trackBar.Scroll += new System.EventHandler(this.Exp_trackBar_Scroll);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(359, 289);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "增益：";
            // 
            // Gain_trackBar
            // 
            this.Gain_trackBar.Location = new System.Drawing.Point(387, 289);
            this.Gain_trackBar.Name = "Gain_trackBar";
            this.Gain_trackBar.Size = new System.Drawing.Size(151, 45);
            this.Gain_trackBar.TabIndex = 14;
            this.Gain_trackBar.Scroll += new System.EventHandler(this.Gain_trackBar_Scroll);
            // 
            // Gain_value
            // 
            this.Gain_value.AutoSize = true;
            this.Gain_value.Location = new System.Drawing.Point(544, 289);
            this.Gain_value.Name = "Gain_value";
            this.Gain_value.Size = new System.Drawing.Size(29, 12);
            this.Gain_value.TabIndex = 15;
            this.Gain_value.Text = "Gain";
            // 
            // Exp_textBox
            // 
            this.Exp_textBox.Location = new System.Drawing.Point(520, 244);
            this.Exp_textBox.Name = "Exp_textBox";
            this.Exp_textBox.Size = new System.Drawing.Size(59, 21);
            this.Exp_textBox.TabIndex = 16;
            this.Exp_textBox.TextChanged += new System.EventHandler(this.Exp_textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(580, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "S";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Bitmap|*.bmp|All files|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 331);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Exp_textBox);
            this.Controls.Add(this.Gain_value);
            this.Controls.Add(this.Frame_value);
            this.Controls.Add(this.Gain_trackBar);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Exp_trackBar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Frame_trackBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Cam_Height);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Cam_Width);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.Cam_stop);
            this.Controls.Add(this.Cam_start);
            this.Controls.Add(this.SoftTrigger_button);
            this.Controls.Add(this.AcquisitionMode_comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Selected_comboBox);
            this.Controls.Add(this.Selected_device);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "曝光_增益_触发";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Frame_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Exp_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gain_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Selected_device;
        private System.Windows.Forms.ComboBox Selected_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AcquisitionMode_comboBox;
        private System.Windows.Forms.Button SoftTrigger_button;
        private System.Windows.Forms.Button Cam_start;
        private System.Windows.Forms.Button Cam_stop;
        private System.Windows.Forms.Button SaveImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Cam_Width;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Cam_Height;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar Frame_trackBar;
        private System.Windows.Forms.Label Frame_value;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar Exp_trackBar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar Gain_trackBar;
        private System.Windows.Forms.Label Gain_value;
        private System.Windows.Forms.TextBox Exp_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

