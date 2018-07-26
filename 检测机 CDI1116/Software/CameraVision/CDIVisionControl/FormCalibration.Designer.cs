namespace CDIVisionControl
{
    partial class FormCalibration
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonShot = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGetBlob = new System.Windows.Forms.Button();
            this.buttonSet = new System.Windows.Forms.Button();
            this.BlobBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxMinArea = new System.Windows.Forms.TextBox();
            this.Thres = new System.Windows.Forms.TextBox();
            this.CheckBoxWhiteBlob = new Colibri.CommonModule.ToolBox.SelectBox();
            this.TextBoxMaxArea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.textBoxCol = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxPixelWidthX = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxPixelWidthY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxRowPitch = new System.Windows.Forms.TextBox();
            this.textBoxColPitch = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.optionBox50 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBox100 = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBox25 = new Colibri.CommonModule.ToolBox.OptionBox();
            ((System.ComponentModel.ISupportInitialize)(this.BlobBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(-2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. 采集图像";
            // 
            // buttonShot
            // 
            this.buttonShot.Location = new System.Drawing.Point(4, 37);
            this.buttonShot.Name = "buttonShot";
            this.buttonShot.Size = new System.Drawing.Size(65, 41);
            this.buttonShot.TabIndex = 1;
            this.buttonShot.Text = "抓取一帧";
            this.buttonShot.UseVisualStyleBackColor = true;
            this.buttonShot.Click += new System.EventHandler(this.buttonShot_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(330, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "3. 设置斑点参数";
            // 
            // buttonGetBlob
            // 
            this.buttonGetBlob.Location = new System.Drawing.Point(633, 62);
            this.buttonGetBlob.Name = "buttonGetBlob";
            this.buttonGetBlob.Size = new System.Drawing.Size(72, 34);
            this.buttonGetBlob.TabIndex = 17;
            this.buttonGetBlob.Text = "标定";
            this.buttonGetBlob.UseVisualStyleBackColor = true;
            this.buttonGetBlob.Click += new System.EventHandler(this.buttonGetBlob_Click);
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(633, 20);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(72, 36);
            this.buttonSet.TabIndex = 15;
            this.buttonSet.Text = "设置";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // BlobBar
            // 
            this.BlobBar.Location = new System.Drawing.Point(322, 63);
            this.BlobBar.Maximum = 255;
            this.BlobBar.Minimum = 1;
            this.BlobBar.Name = "BlobBar";
            this.BlobBar.Size = new System.Drawing.Size(247, 45);
            this.BlobBar.TabIndex = 14;
            this.BlobBar.TickFrequency = 5;
            this.BlobBar.Value = 127;
            this.BlobBar.Scroll += new System.EventHandler(this.BlobBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "最小面积";
            // 
            // TextBoxMinArea
            // 
            this.TextBoxMinArea.Location = new System.Drawing.Point(389, 34);
            this.TextBoxMinArea.Name = "TextBoxMinArea";
            this.TextBoxMinArea.Size = new System.Drawing.Size(52, 20);
            this.TextBoxMinArea.TabIndex = 11;
            this.TextBoxMinArea.Text = "1000";
            // 
            // Thres
            // 
            this.Thres.Location = new System.Drawing.Point(575, 68);
            this.Thres.Name = "Thres";
            this.Thres.Size = new System.Drawing.Size(52, 20);
            this.Thres.TabIndex = 10;
            this.Thres.Text = "127";
            // 
            // CheckBoxWhiteBlob
            // 
            this.CheckBoxWhiteBlob.AutoSize = true;
            this.CheckBoxWhiteBlob.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CheckBoxWhiteBlob.BorderColor = System.Drawing.Color.Black;
            this.CheckBoxWhiteBlob.CheckColor = System.Drawing.Color.Lime;
            this.CheckBoxWhiteBlob.Location = new System.Drawing.Point(566, 31);
            this.CheckBoxWhiteBlob.Name = "CheckBoxWhiteBlob";
            this.CheckBoxWhiteBlob.Padding = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.CheckBoxWhiteBlob.Size = new System.Drawing.Size(64, 23);
            this.CheckBoxWhiteBlob.TabIndex = 9;
            this.CheckBoxWhiteBlob.Text = "白斑";
            this.CheckBoxWhiteBlob.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.CheckBoxWhiteBlob.UseVisualStyleBackColor = true;
            // 
            // TextBoxMaxArea
            // 
            this.TextBoxMaxArea.Location = new System.Drawing.Point(508, 34);
            this.TextBoxMaxArea.Name = "TextBoxMaxArea";
            this.TextBoxMaxArea.Size = new System.Drawing.Size(52, 20);
            this.TextBoxMaxArea.TabIndex = 11;
            this.TextBoxMaxArea.Text = "2000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "最大面积";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(139, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "2. 设置标定板";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "行数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(139, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "列数";
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(174, 33);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(52, 20);
            this.textBoxRow.TabIndex = 11;
            this.textBoxRow.Text = "28";
            // 
            // textBoxCol
            // 
            this.textBoxCol.Location = new System.Drawing.Point(174, 62);
            this.textBoxCol.Name = "textBoxCol";
            this.textBoxCol.Size = new System.Drawing.Size(52, 20);
            this.textBoxCol.TabIndex = 11;
            this.textBoxCol.Text = "45";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.PeachPuff;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(0, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(920, 490);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // textBoxPixelWidthX
            // 
            this.textBoxPixelWidthX.BackColor = System.Drawing.Color.Khaki;
            this.textBoxPixelWidthX.ForeColor = System.Drawing.Color.Red;
            this.textBoxPixelWidthX.Location = new System.Drawing.Point(731, 33);
            this.textBoxPixelWidthX.Name = "textBoxPixelWidthX";
            this.textBoxPixelWidthX.ReadOnly = true;
            this.textBoxPixelWidthX.Size = new System.Drawing.Size(112, 20);
            this.textBoxPixelWidthX.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(716, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(713, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "分辨率标定结果";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(716, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Y";
            // 
            // textBoxPixelWidthY
            // 
            this.textBoxPixelWidthY.BackColor = System.Drawing.Color.Khaki;
            this.textBoxPixelWidthY.ForeColor = System.Drawing.Color.Red;
            this.textBoxPixelWidthY.Location = new System.Drawing.Point(731, 62);
            this.textBoxPixelWidthY.Name = "textBoxPixelWidthY";
            this.textBoxPixelWidthY.ReadOnly = true;
            this.textBoxPixelWidthY.Size = new System.Drawing.Size(112, 20);
            this.textBoxPixelWidthY.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(232, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "行距";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(232, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "列距";
            // 
            // textBoxRowPitch
            // 
            this.textBoxRowPitch.Location = new System.Drawing.Point(267, 34);
            this.textBoxRowPitch.Name = "textBoxRowPitch";
            this.textBoxRowPitch.Size = new System.Drawing.Size(52, 20);
            this.textBoxRowPitch.TabIndex = 11;
            this.textBoxRowPitch.Text = "1";
            // 
            // textBoxColPitch
            // 
            this.textBoxColPitch.Location = new System.Drawing.Point(267, 63);
            this.textBoxColPitch.Name = "textBoxColPitch";
            this.textBoxColPitch.Size = new System.Drawing.Size(52, 20);
            this.textBoxColPitch.TabIndex = 11;
            this.textBoxColPitch.Text = "1";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(849, 39);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(72, 34);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // optionBox50
            // 
            this.optionBox50.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox50.AutoSize = true;
            this.optionBox50.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox50.BorderColor = System.Drawing.Color.Black;
            this.optionBox50.CheckColor = System.Drawing.Color.Lime;
            this.optionBox50.Location = new System.Drawing.Point(72, 37);
            this.optionBox50.Name = "optionBox50";
            this.optionBox50.Padding = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.optionBox50.Size = new System.Drawing.Size(52, 23);
            this.optionBox50.TabIndex = 22;
            this.optionBox50.Text = "50";
            this.optionBox50.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox50.UseVisualStyleBackColor = true;
            this.optionBox50.Click += new System.EventHandler(this.optionBox50_Click);
            // 
            // optionBox100
            // 
            this.optionBox100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox100.AutoSize = true;
            this.optionBox100.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox100.BorderColor = System.Drawing.Color.Black;
            this.optionBox100.CheckColor = System.Drawing.Color.Lime;
            this.optionBox100.Location = new System.Drawing.Point(72, 67);
            this.optionBox100.Name = "optionBox100";
            this.optionBox100.Padding = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.optionBox100.Size = new System.Drawing.Size(58, 23);
            this.optionBox100.TabIndex = 23;
            this.optionBox100.Text = "100";
            this.optionBox100.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox100.UseVisualStyleBackColor = true;
            this.optionBox100.Click += new System.EventHandler(this.optionBox100_Click);
            // 
            // optionBox25
            // 
            this.optionBox25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionBox25.AutoSize = true;
            this.optionBox25.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBox25.BorderColor = System.Drawing.Color.Black;
            this.optionBox25.CheckColor = System.Drawing.Color.Lime;
            this.optionBox25.Location = new System.Drawing.Point(72, 7);
            this.optionBox25.Name = "optionBox25";
            this.optionBox25.Padding = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.optionBox25.Size = new System.Drawing.Size(52, 23);
            this.optionBox25.TabIndex = 24;
            this.optionBox25.Text = "25";
            this.optionBox25.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBox25.UseVisualStyleBackColor = true;
            this.optionBox25.Click += new System.EventHandler(this.optionBox25_Click);
            // 
            // FormCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 592);
            this.Controls.Add(this.optionBox50);
            this.Controls.Add(this.optionBox25);
            this.Controls.Add(this.textBoxPixelWidthY);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxPixelWidthX);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonGetBlob);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.BlobBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxColPitch);
            this.Controls.Add(this.textBoxCol);
            this.Controls.Add(this.textBoxRowPitch);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.TextBoxMaxArea);
            this.Controls.Add(this.TextBoxMinArea);
            this.Controls.Add(this.Thres);
            this.Controls.Add(this.CheckBoxWhiteBlob);
            this.Controls.Add(this.buttonShot);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.optionBox100);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCalibration";
            this.Text = "图像校准";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCalibration_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.BlobBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonShot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetBlob;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.TrackBar BlobBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxMinArea;
        private System.Windows.Forms.TextBox Thres;
        private Colibri.CommonModule.ToolBox.SelectBox CheckBoxWhiteBlob;
        private System.Windows.Forms.TextBox TextBoxMaxArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.TextBox textBoxCol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxPixelWidthX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPixelWidthY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxRowPitch;
        private System.Windows.Forms.TextBox textBoxColPitch;
        private System.Windows.Forms.Button buttonSave;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox50;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox100;
        private Colibri.CommonModule.ToolBox.OptionBox optionBox25;
    }
}