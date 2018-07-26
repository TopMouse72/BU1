namespace CDI.GUI
{
    partial class ManualPanelZone上料传送
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataDispCell1 = new CDI.GUI.DataDisp();
            this.dataDispCell2 = new CDI.GUI.DataDisp();
            this.dataDispCell3 = new CDI.GUI.DataDisp();
            this.dataDispCell4 = new CDI.GUI.DataDisp();
            this.dataDispCell5 = new CDI.GUI.DataDisp();
            this.dataDispCell6 = new CDI.GUI.DataDisp();
            this.buttonStartBarcode = new System.Windows.Forms.Button();
            this.buttonStartLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectBoxIgnoreSMEMA = new Colibri.CommonModule.ToolBox.SelectBox();
            this.labelGetCell = new System.Windows.Forms.Label();
            this.buttonNewCellReady = new System.Windows.Forms.Button();
            this.selectBoxSMEMAAvailable = new Colibri.CommonModule.ToolBox.SelectBox();
            this.pointPosUCLoadInConveyorBackDistance = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.motorUCLoadInConveyor = new Colibri.CommonModule.ToolBox.MotorUC();
            this.pointPosUCLoadInConveyorCellPitch = new Colibri.CommonModule.ToolBox.PointPosUC();
            this.buttonJogStart = new System.Windows.Forms.Button();
            this.buttonJogStop = new System.Windows.Forms.Button();
            this.selectBoxDirection = new Colibri.CommonModule.ToolBox.SelectBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 206);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 168);
            // 
            // buttonPortReset
            // 
            this.buttonPortReset.Location = new System.Drawing.Point(119, 168);
            this.buttonPortReset.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.dataDispCell1);
            this.groupBox2.Controls.Add(this.dataDispCell2);
            this.groupBox2.Controls.Add(this.dataDispCell3);
            this.groupBox2.Controls.Add(this.dataDispCell4);
            this.groupBox2.Controls.Add(this.dataDispCell5);
            this.groupBox2.Controls.Add(this.dataDispCell6);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1212, 139);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电芯";
            // 
            // dataDispCell1
            // 
            this.dataDispCell1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell1.DataStation = null;
            this.dataDispCell1.Location = new System.Drawing.Point(1011, 20);
            this.dataDispCell1.Name = "dataDispCell1";
            this.dataDispCell1.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell1.TabIndex = 18;
            this.dataDispCell1.Text = "电芯1";
            // 
            // dataDispCell2
            // 
            this.dataDispCell2.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell2.DataStation = null;
            this.dataDispCell2.Location = new System.Drawing.Point(810, 20);
            this.dataDispCell2.Name = "dataDispCell2";
            this.dataDispCell2.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell2.TabIndex = 17;
            this.dataDispCell2.Text = "电芯1";
            // 
            // dataDispCell3
            // 
            this.dataDispCell3.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell3.DataStation = null;
            this.dataDispCell3.Location = new System.Drawing.Point(609, 20);
            this.dataDispCell3.Name = "dataDispCell3";
            this.dataDispCell3.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell3.TabIndex = 16;
            this.dataDispCell3.Text = "电芯1";
            // 
            // dataDispCell4
            // 
            this.dataDispCell4.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell4.DataStation = null;
            this.dataDispCell4.Location = new System.Drawing.Point(408, 20);
            this.dataDispCell4.Name = "dataDispCell4";
            this.dataDispCell4.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell4.TabIndex = 15;
            this.dataDispCell4.Text = "电芯1";
            // 
            // dataDispCell5
            // 
            this.dataDispCell5.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell5.DataStation = null;
            this.dataDispCell5.Location = new System.Drawing.Point(207, 20);
            this.dataDispCell5.Name = "dataDispCell5";
            this.dataDispCell5.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell5.TabIndex = 14;
            this.dataDispCell5.Text = "电芯1";
            // 
            // dataDispCell6
            // 
            this.dataDispCell6.BackColor = System.Drawing.Color.MediumAquamarine;
            this.dataDispCell6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDispCell6.DataStation = null;
            this.dataDispCell6.Location = new System.Drawing.Point(6, 20);
            this.dataDispCell6.Name = "dataDispCell6";
            this.dataDispCell6.Size = new System.Drawing.Size(195, 100);
            this.dataDispCell6.TabIndex = 13;
            this.dataDispCell6.Text = "电芯1";
            // 
            // buttonStartBarcode
            // 
            this.buttonStartBarcode.Location = new System.Drawing.Point(533, 168);
            this.buttonStartBarcode.Name = "buttonStartBarcode";
            this.buttonStartBarcode.Size = new System.Drawing.Size(110, 35);
            this.buttonStartBarcode.TabIndex = 5;
            this.buttonStartBarcode.Text = "上料扫码";
            this.buttonStartBarcode.UseVisualStyleBackColor = true;
            this.buttonStartBarcode.Click += new System.EventHandler(this.buttonStartBarcode_Click);
            // 
            // buttonStartLoad
            // 
            this.buttonStartLoad.Location = new System.Drawing.Point(417, 168);
            this.buttonStartLoad.Name = "buttonStartLoad";
            this.buttonStartLoad.Size = new System.Drawing.Size(110, 35);
            this.buttonStartLoad.TabIndex = 6;
            this.buttonStartLoad.Text = "上料传送";
            this.buttonStartLoad.UseVisualStyleBackColor = true;
            this.buttonStartLoad.Click += new System.EventHandler(this.buttonStartLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectBoxIgnoreSMEMA);
            this.groupBox1.Location = new System.Drawing.Point(954, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 79);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数（只读）";
            this.groupBox1.Visible = false;
            // 
            // selectBoxIgnoreSMEMA
            // 
            this.selectBoxIgnoreSMEMA.AutoSize = true;
            this.selectBoxIgnoreSMEMA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxIgnoreSMEMA.BackColor = System.Drawing.Color.YellowGreen;
            this.selectBoxIgnoreSMEMA.BorderColor = System.Drawing.Color.Black;
            this.selectBoxIgnoreSMEMA.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxIgnoreSMEMA.Location = new System.Drawing.Point(6, 20);
            this.selectBoxIgnoreSMEMA.Name = "selectBoxIgnoreSMEMA";
            this.selectBoxIgnoreSMEMA.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxIgnoreSMEMA.ReadOnly = true;
            this.selectBoxIgnoreSMEMA.Size = new System.Drawing.Size(143, 23);
            this.selectBoxIgnoreSMEMA.TabIndex = 8;
            this.selectBoxIgnoreSMEMA.Text = "忽略上料SMEMA信号";
            this.selectBoxIgnoreSMEMA.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxIgnoreSMEMA.UseVisualStyleBackColor = false;
            // 
            // labelGetCell
            // 
            this.labelGetCell.AutoSize = true;
            this.labelGetCell.Location = new System.Drawing.Point(3, 151);
            this.labelGetCell.Name = "labelGetCell";
            this.labelGetCell.Size = new System.Drawing.Size(121, 13);
            this.labelGetCell.TabIndex = 12;
            this.labelGetCell.Text = "最新电芯数据索引：0";
            // 
            // buttonNewCellReady
            // 
            this.buttonNewCellReady.Location = new System.Drawing.Point(301, 168);
            this.buttonNewCellReady.Name = "buttonNewCellReady";
            this.buttonNewCellReady.Size = new System.Drawing.Size(110, 35);
            this.buttonNewCellReady.TabIndex = 6;
            this.buttonNewCellReady.Text = "检查新料";
            this.buttonNewCellReady.UseVisualStyleBackColor = true;
            this.buttonNewCellReady.Click += new System.EventHandler(this.buttonNewCellReady_Click);
            // 
            // selectBoxSMEMAAvailable
            // 
            this.selectBoxSMEMAAvailable.AutoSize = true;
            this.selectBoxSMEMAAvailable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxSMEMAAvailable.BorderColor = System.Drawing.Color.Black;
            this.selectBoxSMEMAAvailable.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxSMEMAAvailable.Location = new System.Drawing.Point(778, 173);
            this.selectBoxSMEMAAvailable.Name = "selectBoxSMEMAAvailable";
            this.selectBoxSMEMAAvailable.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxSMEMAAvailable.ReadOnly = true;
            this.selectBoxSMEMAAvailable.Size = new System.Drawing.Size(138, 23);
            this.selectBoxSMEMAAvailable.TabIndex = 8;
            this.selectBoxSMEMAAvailable.Text = "SMEMA信号Available";
            this.selectBoxSMEMAAvailable.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxSMEMAAvailable.UseVisualStyleBackColor = false;
            this.selectBoxSMEMAAvailable.Click += new System.EventHandler(this.selectBoxSMEMAAvailable_Click);
            // 
            // pointPosUCLoadInConveyorBackDistance
            // 
            this.pointPosUCLoadInConveyorBackDistance.Axis = null;
            this.pointPosUCLoadInConveyorBackDistance.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCLoadInConveyorBackDistance.BindingUC = this.motorUCLoadInConveyor;
            this.pointPosUCLoadInConveyorBackDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadInConveyorBackDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadInConveyorBackDistance.Location = new System.Drawing.Point(294, 307);
            this.pointPosUCLoadInConveyorBackDistance.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadInConveyorBackDistance.Name = "pointPosUCLoadInConveyorBackDistance";
            this.pointPosUCLoadInConveyorBackDistance.PointName = "BackDistance";
            this.pointPosUCLoadInConveyorBackDistance.Size = new System.Drawing.Size(472, 61);
            this.pointPosUCLoadInConveyorBackDistance.TabIndex = 19;
            this.pointPosUCLoadInConveyorBackDistance.TextBoxPosLeft = 258;
            this.pointPosUCLoadInConveyorBackDistance.TextDisplay = "回退距离";
            // 
            // motorUCLoadInConveyor
            // 
            this.motorUCLoadInConveyor.AutoSize = true;
            this.motorUCLoadInConveyor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.motorUCLoadInConveyor.Axis = null;
            this.motorUCLoadInConveyor.AxisName = "传送带";
            this.motorUCLoadInConveyor.BackColor = System.Drawing.Color.LightGreen;
            this.motorUCLoadInConveyor.HomeEnabled = true;
            this.motorUCLoadInConveyor.Location = new System.Drawing.Point(3, 236);
            this.motorUCLoadInConveyor.Name = "motorUCLoadInConveyor";
            this.motorUCLoadInConveyor.Size = new System.Drawing.Size(283, 190);
            this.motorUCLoadInConveyor.TabIndex = 18;
            // 
            // pointPosUCLoadInConveyorCellPitch
            // 
            this.pointPosUCLoadInConveyorCellPitch.Axis = null;
            this.pointPosUCLoadInConveyorCellPitch.BackColor = System.Drawing.Color.LightGreen;
            this.pointPosUCLoadInConveyorCellPitch.BindingUC = this.motorUCLoadInConveyor;
            this.pointPosUCLoadInConveyorCellPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointPosUCLoadInConveyorCellPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pointPosUCLoadInConveyorCellPitch.Location = new System.Drawing.Point(294, 236);
            this.pointPosUCLoadInConveyorCellPitch.Margin = new System.Windows.Forms.Padding(5);
            this.pointPosUCLoadInConveyorCellPitch.Name = "pointPosUCLoadInConveyorCellPitch";
            this.pointPosUCLoadInConveyorCellPitch.PointName = "CellPitch";
            this.pointPosUCLoadInConveyorCellPitch.Size = new System.Drawing.Size(472, 61);
            this.pointPosUCLoadInConveyorCellPitch.TabIndex = 19;
            this.pointPosUCLoadInConveyorCellPitch.TextBoxPosLeft = 258;
            this.pointPosUCLoadInConveyorCellPitch.TextDisplay = "物料间距";
            // 
            // buttonJogStart
            // 
            this.buttonJogStart.Location = new System.Drawing.Point(974, 168);
            this.buttonJogStart.Name = "buttonJogStart";
            this.buttonJogStart.Size = new System.Drawing.Size(110, 35);
            this.buttonJogStart.TabIndex = 20;
            this.buttonJogStart.Text = "持续转开始";
            this.buttonJogStart.UseVisualStyleBackColor = true;
            this.buttonJogStart.Click += new System.EventHandler(this.buttonJogStart_Click);
            // 
            // buttonJogStop
            // 
            this.buttonJogStop.Location = new System.Drawing.Point(1090, 168);
            this.buttonJogStop.Name = "buttonJogStop";
            this.buttonJogStop.Size = new System.Drawing.Size(110, 35);
            this.buttonJogStop.TabIndex = 21;
            this.buttonJogStop.Text = "持续转停止";
            this.buttonJogStop.UseVisualStyleBackColor = true;
            this.buttonJogStop.Click += new System.EventHandler(this.buttonJogStop_Click);
            // 
            // selectBoxDirection
            // 
            this.selectBoxDirection.AutoSize = true;
            this.selectBoxDirection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBoxDirection.BackColor = System.Drawing.Color.YellowGreen;
            this.selectBoxDirection.BorderColor = System.Drawing.Color.Black;
            this.selectBoxDirection.CheckColor = System.Drawing.Color.Lime;
            this.selectBoxDirection.Checked = true;
            this.selectBoxDirection.Location = new System.Drawing.Point(974, 211);
            this.selectBoxDirection.Name = "selectBoxDirection";
            this.selectBoxDirection.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.selectBoxDirection.Size = new System.Drawing.Size(80, 23);
            this.selectBoxDirection.TabIndex = 8;
            this.selectBoxDirection.Text = "正向移动";
            this.selectBoxDirection.UnCheckColor = System.Drawing.Color.DarkGreen;
            this.selectBoxDirection.UseVisualStyleBackColor = false;
            // 
            // ManualPanelZone上料传送
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.selectBoxDirection);
            this.Controls.Add(this.buttonJogStart);
            this.Controls.Add(this.buttonJogStop);
            this.Controls.Add(this.pointPosUCLoadInConveyorCellPitch);
            this.Controls.Add(this.pointPosUCLoadInConveyorBackDistance);
            this.Controls.Add(this.motorUCLoadInConveyor);
            this.Controls.Add(this.selectBoxSMEMAAvailable);
            this.Controls.Add(this.labelGetCell);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonNewCellReady);
            this.Controls.Add(this.buttonStartLoad);
            this.Controls.Add(this.buttonStartBarcode);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualPanelZone上料传送";
            this.Size = new System.Drawing.Size(1224, 489);
            this.Controls.SetChildIndex(this.buttonPortReset, 0);
            this.Controls.SetChildIndex(this.buttonReset, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.buttonStartBarcode, 0);
            this.Controls.SetChildIndex(this.labelStatus, 0);
            this.Controls.SetChildIndex(this.buttonStartLoad, 0);
            this.Controls.SetChildIndex(this.buttonNewCellReady, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.labelGetCell, 0);
            this.Controls.SetChildIndex(this.selectBoxSMEMAAvailable, 0);
            this.Controls.SetChildIndex(this.motorUCLoadInConveyor, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadInConveyorBackDistance, 0);
            this.Controls.SetChildIndex(this.pointPosUCLoadInConveyorCellPitch, 0);
            this.Controls.SetChildIndex(this.buttonJogStop, 0);
            this.Controls.SetChildIndex(this.buttonJogStart, 0);
            this.Controls.SetChildIndex(this.selectBoxDirection, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonStartBarcode;
        private System.Windows.Forms.Button buttonStartLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxIgnoreSMEMA;
        private System.Windows.Forms.Label labelGetCell;
        private System.Windows.Forms.Button buttonNewCellReady;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxSMEMAAvailable;
        private DataDisp dataDispCell6;
        private DataDisp dataDispCell5;
        private DataDisp dataDispCell4;
        private DataDisp dataDispCell3;
        private DataDisp dataDispCell2;
        private DataDisp dataDispCell1;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadInConveyorBackDistance;
        private Colibri.CommonModule.ToolBox.MotorUC motorUCLoadInConveyor;
        private Colibri.CommonModule.ToolBox.PointPosUC pointPosUCLoadInConveyorCellPitch;
        private System.Windows.Forms.Button buttonJogStart;
        private System.Windows.Forms.Button buttonJogStop;
        private Colibri.CommonModule.ToolBox.SelectBox selectBoxDirection;
    }
}
