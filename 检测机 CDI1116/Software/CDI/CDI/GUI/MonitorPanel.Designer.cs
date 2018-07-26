namespace CDI.GUI
{
    partial class MonitorPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewAlarm = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewDay = new System.Windows.Forms.DataGridView();
            this.dataGridViewNight = new System.Windows.Forms.DataGridView();
            this.labelTotalYield = new System.Windows.Forms.Label();
            this.labelNightYield = new System.Windows.Forms.Label();
            this.labelDayYield = new System.Windows.Forms.Label();
            this.labelPPM = new System.Windows.Forms.Label();
            this.optionBoxToday = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBoxYesterday = new Colibri.CommonModule.ToolBox.OptionBox();
            this.optionBoxBeforeYesterday = new Colibri.CommonModule.ToolBox.OptionBox();
            this.dataGridViewWorkFlow = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDayDT = new System.Windows.Forms.Label();
            this.labelNightDT = new System.Windows.Forms.Label();
            this.labelTotalDT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkFlow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "产量统计：";
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.AllowUserToAddRows = false;
            this.dataGridViewAlarm.AllowUserToDeleteRows = false;
            this.dataGridViewAlarm.AllowUserToResizeColumns = false;
            this.dataGridViewAlarm.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewAlarm.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewAlarm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAlarm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(3, 415);
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.ReadOnly = true;
            this.dataGridViewAlarm.RowHeadersVisible = false;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(870, 309);
            this.dataGridViewAlarm.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "报警记录：";
            // 
            // dataGridViewDay
            // 
            this.dataGridViewDay.AllowUserToAddRows = false;
            this.dataGridViewDay.AllowUserToDeleteRows = false;
            this.dataGridViewDay.AllowUserToResizeColumns = false;
            this.dataGridViewDay.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewDay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDay.Location = new System.Drawing.Point(3, 29);
            this.dataGridViewDay.Name = "dataGridViewDay";
            this.dataGridViewDay.ReadOnly = true;
            this.dataGridViewDay.RowHeadersVisible = false;
            this.dataGridViewDay.Size = new System.Drawing.Size(781, 360);
            this.dataGridViewDay.TabIndex = 22;
            // 
            // dataGridViewNight
            // 
            this.dataGridViewNight.AllowUserToAddRows = false;
            this.dataGridViewNight.AllowUserToDeleteRows = false;
            this.dataGridViewNight.AllowUserToResizeColumns = false;
            this.dataGridViewNight.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewNight.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewNight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewNight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNight.Location = new System.Drawing.Point(786, 29);
            this.dataGridViewNight.Name = "dataGridViewNight";
            this.dataGridViewNight.ReadOnly = true;
            this.dataGridViewNight.RowHeadersVisible = false;
            this.dataGridViewNight.Size = new System.Drawing.Size(781, 360);
            this.dataGridViewNight.TabIndex = 23;
            // 
            // labelTotalYield
            // 
            this.labelTotalYield.AutoSize = true;
            this.labelTotalYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotalYield.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelTotalYield.Location = new System.Drawing.Point(1570, 231);
            this.labelTotalYield.Name = "labelTotalYield";
            this.labelTotalYield.Size = new System.Drawing.Size(83, 17);
            this.labelTotalYield.TabIndex = 32;
            this.labelTotalYield.Text = "合计优率：";
            // 
            // labelNightYield
            // 
            this.labelNightYield.AutoSize = true;
            this.labelNightYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNightYield.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelNightYield.Location = new System.Drawing.Point(1570, 197);
            this.labelNightYield.Name = "labelNightYield";
            this.labelNightYield.Size = new System.Drawing.Size(83, 17);
            this.labelNightYield.TabIndex = 33;
            this.labelNightYield.Text = "晚班优率：";
            // 
            // labelDayYield
            // 
            this.labelDayYield.AutoSize = true;
            this.labelDayYield.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDayYield.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelDayYield.Location = new System.Drawing.Point(1570, 163);
            this.labelDayYield.Name = "labelDayYield";
            this.labelDayYield.Size = new System.Drawing.Size(83, 17);
            this.labelDayYield.TabIndex = 34;
            this.labelDayYield.Text = "白班优率：";
            // 
            // labelPPM
            // 
            this.labelPPM.AutoSize = true;
            this.labelPPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPPM.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelPPM.Location = new System.Drawing.Point(1570, 135);
            this.labelPPM.Name = "labelPPM";
            this.labelPPM.Size = new System.Drawing.Size(55, 17);
            this.labelPPM.TabIndex = 34;
            this.labelPPM.Text = "PPM：";
            // 
            // optionBoxToday
            // 
            this.optionBoxToday.AutoSize = true;
            this.optionBoxToday.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxToday.BorderColor = System.Drawing.Color.Black;
            this.optionBoxToday.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxToday.Checked = true;
            this.optionBoxToday.GroupChecked = true;
            this.optionBoxToday.Location = new System.Drawing.Point(1573, 97);
            this.optionBoxToday.Name = "optionBoxToday";
            this.optionBoxToday.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.optionBoxToday.Size = new System.Drawing.Size(75, 30);
            this.optionBoxToday.TabIndex = 393;
            this.optionBoxToday.Text = "今天";
            this.optionBoxToday.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxToday.UseVisualStyleBackColor = true;
            this.optionBoxToday.Click += new System.EventHandler(this.optionBoxToday_Click);
            // 
            // optionBoxYesterday
            // 
            this.optionBoxYesterday.AutoSize = true;
            this.optionBoxYesterday.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxYesterday.BorderColor = System.Drawing.Color.Black;
            this.optionBoxYesterday.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxYesterday.Location = new System.Drawing.Point(1573, 63);
            this.optionBoxYesterday.Name = "optionBoxYesterday";
            this.optionBoxYesterday.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.optionBoxYesterday.Size = new System.Drawing.Size(75, 30);
            this.optionBoxYesterday.TabIndex = 394;
            this.optionBoxYesterday.Text = "昨天";
            this.optionBoxYesterday.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxYesterday.UseVisualStyleBackColor = true;
            this.optionBoxYesterday.Click += new System.EventHandler(this.optionBoxYesterday_Click);
            // 
            // optionBoxBeforeYesterday
            // 
            this.optionBoxBeforeYesterday.AutoSize = true;
            this.optionBoxBeforeYesterday.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionBoxBeforeYesterday.BorderColor = System.Drawing.Color.Black;
            this.optionBoxBeforeYesterday.CheckColor = System.Drawing.Color.Aqua;
            this.optionBoxBeforeYesterday.Location = new System.Drawing.Point(1573, 29);
            this.optionBoxBeforeYesterday.Name = "optionBoxBeforeYesterday";
            this.optionBoxBeforeYesterday.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.optionBoxBeforeYesterday.Size = new System.Drawing.Size(75, 30);
            this.optionBoxBeforeYesterday.TabIndex = 395;
            this.optionBoxBeforeYesterday.Text = "前天";
            this.optionBoxBeforeYesterday.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.optionBoxBeforeYesterday.UseVisualStyleBackColor = true;
            this.optionBoxBeforeYesterday.Click += new System.EventHandler(this.optionBoxBeforeYesterday_Click);
            // 
            // dataGridViewWorkFlow
            // 
            this.dataGridViewWorkFlow.AllowUserToAddRows = false;
            this.dataGridViewWorkFlow.AllowUserToDeleteRows = false;
            this.dataGridViewWorkFlow.AllowUserToResizeColumns = false;
            this.dataGridViewWorkFlow.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Yellow;
            this.dataGridViewWorkFlow.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewWorkFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewWorkFlow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewWorkFlow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewWorkFlow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkFlow.Location = new System.Drawing.Point(874, 415);
            this.dataGridViewWorkFlow.Name = "dataGridViewWorkFlow";
            this.dataGridViewWorkFlow.ReadOnly = true;
            this.dataGridViewWorkFlow.RowHeadersVisible = false;
            this.dataGridViewWorkFlow.Size = new System.Drawing.Size(864, 309);
            this.dataGridViewWorkFlow.TabIndex = 396;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(879, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "运行记录：";
            // 
            // labelDayDT
            // 
            this.labelDayDT.AutoSize = true;
            this.labelDayDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDayDT.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelDayDT.Location = new System.Drawing.Point(1570, 264);
            this.labelDayDT.Name = "labelDayDT";
            this.labelDayDT.Size = new System.Drawing.Size(74, 17);
            this.labelDayDT.TabIndex = 34;
            this.labelDayDT.Text = "白班DT：";
            // 
            // labelNightDT
            // 
            this.labelNightDT.AutoSize = true;
            this.labelNightDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNightDT.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelNightDT.Location = new System.Drawing.Point(1570, 301);
            this.labelNightDT.Name = "labelNightDT";
            this.labelNightDT.Size = new System.Drawing.Size(74, 17);
            this.labelNightDT.TabIndex = 33;
            this.labelNightDT.Text = "晚班DT：";
            // 
            // labelTotalDT
            // 
            this.labelTotalDT.AutoSize = true;
            this.labelTotalDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotalDT.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelTotalDT.Location = new System.Drawing.Point(1570, 335);
            this.labelTotalDT.Name = "labelTotalDT";
            this.labelTotalDT.Size = new System.Drawing.Size(74, 17);
            this.labelTotalDT.TabIndex = 32;
            this.labelTotalDT.Text = "合计DT：";
            // 
            // MonitorPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dataGridViewWorkFlow);
            this.Controls.Add(this.optionBoxToday);
            this.Controls.Add(this.optionBoxYesterday);
            this.Controls.Add(this.optionBoxBeforeYesterday);
            this.Controls.Add(this.labelTotalDT);
            this.Controls.Add(this.labelNightDT);
            this.Controls.Add(this.labelTotalYield);
            this.Controls.Add(this.labelNightYield);
            this.Controls.Add(this.labelDayDT);
            this.Controls.Add(this.labelPPM);
            this.Controls.Add(this.labelDayYield);
            this.Controls.Add(this.dataGridViewDay);
            this.Controls.Add(this.dataGridViewAlarm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewNight);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MonitorPanel";
            this.Size = new System.Drawing.Size(1737, 730);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkFlow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTotalYield;
        private System.Windows.Forms.Label labelNightYield;
        private System.Windows.Forms.Label labelDayYield;
        private System.Windows.Forms.Label labelPPM;
        public System.Windows.Forms.DataGridView dataGridViewAlarm;
        public System.Windows.Forms.DataGridView dataGridViewDay;
        public System.Windows.Forms.DataGridView dataGridViewNight;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxToday;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxYesterday;
        private Colibri.CommonModule.ToolBox.OptionBox optionBoxBeforeYesterday;
        public System.Windows.Forms.DataGridView dataGridViewWorkFlow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDayDT;
        private System.Windows.Forms.Label labelNightDT;
        private System.Windows.Forms.Label labelTotalDT;
    }
}
