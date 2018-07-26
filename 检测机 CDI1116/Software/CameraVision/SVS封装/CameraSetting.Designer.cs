namespace SVSCamera
{
    partial class CameraSetting
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
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelExplorValue = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.hScrollBarFrame = new System.Windows.Forms.HScrollBar();
            this.hScrollBarExp = new System.Windows.Forms.HScrollBar();
            this.hScrollBarGain = new System.Windows.Forms.HScrollBar();
            this.Frame_value = new System.Windows.Forms.Label();
            this.Gain_value = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Selected_device
            // 
            this.Selected_device.Location = new System.Drawing.Point(62, 49);
            this.Selected_device.Name = "Selected_device";
            this.Selected_device.Size = new System.Drawing.Size(92, 28);
            this.Selected_device.TabIndex = 1;
            this.Selected_device.Text = "查找设备";
            this.Selected_device.UseVisualStyleBackColor = true;
            this.Selected_device.Visible = false;
            this.Selected_device.Click += new System.EventHandler(this.Selected_device_Click);
            // 
            // Selected_comboBox
            // 
            this.Selected_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Selected_comboBox.FormattingEnabled = true;
            this.Selected_comboBox.Location = new System.Drawing.Point(3, 21);
            this.Selected_comboBox.Name = "Selected_comboBox";
            this.Selected_comboBox.Size = new System.Drawing.Size(151, 21);
            this.Selected_comboBox.TabIndex = 2;
            this.Selected_comboBox.SelectedIndexChanged += new System.EventHandler(this.Selected_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "取图模式：";
            // 
            // AcquisitionMode_comboBox
            // 
            this.AcquisitionMode_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AcquisitionMode_comboBox.FormattingEnabled = true;
            this.AcquisitionMode_comboBox.Items.AddRange(new object[] {
            "实时模式",
            "软触发模式",
            "外部触发模式"});
            this.AcquisitionMode_comboBox.Location = new System.Drawing.Point(3, 65);
            this.AcquisitionMode_comboBox.Name = "AcquisitionMode_comboBox";
            this.AcquisitionMode_comboBox.Size = new System.Drawing.Size(116, 21);
            this.AcquisitionMode_comboBox.TabIndex = 4;
            this.AcquisitionMode_comboBox.SelectedIndexChanged += new System.EventHandler(this.AcquisitionMode_comboBox_SelectedIndexChanged);
            // 
            // SoftTrigger_button
            // 
            this.SoftTrigger_button.Location = new System.Drawing.Point(87, 93);
            this.SoftTrigger_button.Name = "SoftTrigger_button";
            this.SoftTrigger_button.Size = new System.Drawing.Size(67, 31);
            this.SoftTrigger_button.TabIndex = 5;
            this.SoftTrigger_button.Text = "软触发";
            this.SoftTrigger_button.UseVisualStyleBackColor = true;
            this.SoftTrigger_button.Click += new System.EventHandler(this.SoftTrigger_button_Click);
            // 
            // Cam_start
            // 
            this.Cam_start.Location = new System.Drawing.Point(3, 93);
            this.Cam_start.Name = "Cam_start";
            this.Cam_start.Size = new System.Drawing.Size(74, 31);
            this.Cam_start.TabIndex = 6;
            this.Cam_start.Text = "开始取图";
            this.Cam_start.UseVisualStyleBackColor = true;
            this.Cam_start.Click += new System.EventHandler(this.Cam_start_Click);
            // 
            // Cam_stop
            // 
            this.Cam_stop.Location = new System.Drawing.Point(3, 130);
            this.Cam_stop.Name = "Cam_stop";
            this.Cam_stop.Size = new System.Drawing.Size(74, 31);
            this.Cam_stop.TabIndex = 7;
            this.Cam_stop.Text = "停止取图";
            this.Cam_stop.UseVisualStyleBackColor = true;
            this.Cam_stop.Click += new System.EventHandler(this.Cam_stop_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Location = new System.Drawing.Point(87, 130);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(67, 31);
            this.SaveImage.TabIndex = 8;
            this.SaveImage.Text = "保存图片";
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "分辨率：";
            // 
            // Cam_Width
            // 
            this.Cam_Width.AutoSize = true;
            this.Cam_Width.Location = new System.Drawing.Point(51, 172);
            this.Cam_Width.Name = "Cam_Width";
            this.Cam_Width.Size = new System.Drawing.Size(19, 13);
            this.Cam_Width.TabIndex = 10;
            this.Cam_Width.Text = "宽";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "X";
            // 
            // Cam_Height
            // 
            this.Cam_Height.AutoSize = true;
            this.Cam_Height.Location = new System.Drawing.Point(105, 172);
            this.Cam_Height.Name = "Cam_Height";
            this.Cam_Height.Size = new System.Drawing.Size(19, 13);
            this.Cam_Height.TabIndex = 12;
            this.Cam_Height.Text = "高";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "帧率：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 259);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "曝光：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 304);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "增益：";
            // 
            // labelExplorValue
            // 
            this.labelExplorValue.AutoSize = true;
            this.labelExplorValue.Location = new System.Drawing.Point(75, 246);
            this.labelExplorValue.Name = "labelExplorValue";
            this.labelExplorValue.Size = new System.Drawing.Size(20, 13);
            this.labelExplorValue.TabIndex = 17;
            this.labelExplorValue.Text = "ms";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Bitmap|*.bmp|All files|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // hScrollBarFrame
            // 
            this.hScrollBarFrame.Location = new System.Drawing.Point(31, 213);
            this.hScrollBarFrame.Name = "hScrollBarFrame";
            this.hScrollBarFrame.Size = new System.Drawing.Size(119, 13);
            this.hScrollBarFrame.TabIndex = 18;
            this.hScrollBarFrame.ValueChanged += new System.EventHandler(this.hScrollBarFrame_ValueChanged);
            // 
            // hScrollBarExp
            // 
            this.hScrollBarExp.Location = new System.Drawing.Point(31, 259);
            this.hScrollBarExp.Name = "hScrollBarExp";
            this.hScrollBarExp.Size = new System.Drawing.Size(119, 13);
            this.hScrollBarExp.TabIndex = 18;
            this.hScrollBarExp.ValueChanged += new System.EventHandler(this.hScrollBarExp_ValueChanged);
            // 
            // hScrollBarGain
            // 
            this.hScrollBarGain.Location = new System.Drawing.Point(31, 304);
            this.hScrollBarGain.Name = "hScrollBarGain";
            this.hScrollBarGain.Size = new System.Drawing.Size(119, 13);
            this.hScrollBarGain.TabIndex = 18;
            this.hScrollBarGain.ValueChanged += new System.EventHandler(this.hScrollBarGain_ValueChanged);
            // 
            // Frame_value
            // 
            this.Frame_value.AutoSize = true;
            this.Frame_value.Location = new System.Drawing.Point(75, 200);
            this.Frame_value.Name = "Frame_value";
            this.Frame_value.Size = new System.Drawing.Size(36, 13);
            this.Frame_value.TabIndex = 15;
            this.Frame_value.Text = "Frame";
            // 
            // Gain_value
            // 
            this.Gain_value.AutoSize = true;
            this.Gain_value.Location = new System.Drawing.Point(75, 291);
            this.Gain_value.Name = "Gain_value";
            this.Gain_value.Size = new System.Drawing.Size(29, 13);
            this.Gain_value.TabIndex = 15;
            this.Gain_value.Text = "Gain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "相机：";
            // 
            // CameraSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.hScrollBarGain);
            this.Controls.Add(this.hScrollBarExp);
            this.Controls.Add(this.hScrollBarFrame);
            this.Controls.Add(this.labelExplorValue);
            this.Controls.Add(this.Gain_value);
            this.Controls.Add(this.Frame_value);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
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
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Selected_comboBox);
            this.Controls.Add(this.Selected_device);
            this.Name = "CameraSetting";
            this.Size = new System.Drawing.Size(157, 317);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelExplorValue;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.HScrollBar hScrollBarFrame;
        private System.Windows.Forms.HScrollBar hScrollBarExp;
        private System.Windows.Forms.HScrollBar hScrollBarGain;
        private System.Windows.Forms.Label Frame_value;
        private System.Windows.Forms.Label Gain_value;
        private System.Windows.Forms.Label label3;
    }
}

