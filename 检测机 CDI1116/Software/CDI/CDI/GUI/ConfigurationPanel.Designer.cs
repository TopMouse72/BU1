namespace CDI.GUI
{
    partial class ConfigurationPanel
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
            this.tabPageSerialPort = new System.Windows.Forms.TabPage();
            this.serialPortTestGUIThicknessSensorRight = new Colibri.CommonModule.Port.SerialPortTestGUI();
            this.serialPortTestGUIThicknessSensorMid = new Colibri.CommonModule.Port.SerialPortTestGUI();
            this.serialPortTestGUIThicknessSensorLeft = new Colibri.CommonModule.Port.SerialPortTestGUI();
            this.serialPortTestGUIOutlineMeasLightController = new Colibri.CommonModule.Port.SerialPortTestGUI();
            this.serialPortTestGUILoadInBarcode = new Colibri.CommonModule.Port.SerialPortTestGUI();
            this.serialPortSettingGUIThicknessSensorRight = new Colibri.CommonModule.Port.SerialPortSettingGUI();
            this.serialPortSettingGUIThicknessSensorMid = new Colibri.CommonModule.Port.SerialPortSettingGUI();
            this.serialPortSettingGUIThicknessSensorLeft = new Colibri.CommonModule.Port.SerialPortSettingGUI();
            this.serialPortSettingGUIOutlineMeasLightController = new Colibri.CommonModule.Port.SerialPortSettingGUI();
            this.serialPortSettingGUILoadInBarcode = new Colibri.CommonModule.Port.SerialPortSettingGUI();
            this.serialPortInfoListGUI1 = new Colibri.CommonModule.Port.SerialPortInfoListGUI();
            this.tabControlZone = new System.Windows.Forms.TabControl();
            this.tabPageProtocol = new System.Windows.Forms.TabPage();
            this.protocolSettingThicknessSetZero = new Colibri.CommonModule.Port.ProtocolSetting();
            this.protocolSettingLightControl = new Colibri.CommonModule.Port.ProtocolSetting();
            this.protocolSettingThicknessMeas = new Colibri.CommonModule.Port.ProtocolSetting();
            this.protocolSettingBarcode = new Colibri.CommonModule.Port.ProtocolSetting();
            this.tabPageSerialPort.SuspendLayout();
            this.tabControlZone.SuspendLayout();
            this.tabPageProtocol.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageSerialPort
            // 
            this.tabPageSerialPort.Controls.Add(this.serialPortTestGUIThicknessSensorRight);
            this.tabPageSerialPort.Controls.Add(this.serialPortTestGUIThicknessSensorMid);
            this.tabPageSerialPort.Controls.Add(this.serialPortTestGUIThicknessSensorLeft);
            this.tabPageSerialPort.Controls.Add(this.serialPortTestGUIOutlineMeasLightController);
            this.tabPageSerialPort.Controls.Add(this.serialPortTestGUILoadInBarcode);
            this.tabPageSerialPort.Controls.Add(this.serialPortSettingGUIThicknessSensorRight);
            this.tabPageSerialPort.Controls.Add(this.serialPortSettingGUIThicknessSensorMid);
            this.tabPageSerialPort.Controls.Add(this.serialPortSettingGUIThicknessSensorLeft);
            this.tabPageSerialPort.Controls.Add(this.serialPortSettingGUIOutlineMeasLightController);
            this.tabPageSerialPort.Controls.Add(this.serialPortSettingGUILoadInBarcode);
            this.tabPageSerialPort.Controls.Add(this.serialPortInfoListGUI1);
            this.tabPageSerialPort.Location = new System.Drawing.Point(4, 25);
            this.tabPageSerialPort.Name = "tabPageSerialPort";
            this.tabPageSerialPort.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerialPort.Size = new System.Drawing.Size(976, 682);
            this.tabPageSerialPort.TabIndex = 0;
            this.tabPageSerialPort.Text = "串口设置";
            this.tabPageSerialPort.UseVisualStyleBackColor = true;
            // 
            // serialPortTestGUIThicknessSensorRight
            // 
            this.serialPortTestGUIThicknessSensorRight.AutoSize = true;
            this.serialPortTestGUIThicknessSensorRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortTestGUIThicknessSensorRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortTestGUIThicknessSensorRight.Location = new System.Drawing.Point(492, 540);
            this.serialPortTestGUIThicknessSensorRight.Name = "serialPortTestGUIThicknessSensorRight";
            this.serialPortTestGUIThicknessSensorRight.Size = new System.Drawing.Size(231, 118);
            this.serialPortTestGUIThicknessSensorRight.TabIndex = 45;
            this.serialPortTestGUIThicknessSensorRight.Text = "厚度测量右";
            // 
            // serialPortTestGUIThicknessSensorMid
            // 
            this.serialPortTestGUIThicknessSensorMid.AutoSize = true;
            this.serialPortTestGUIThicknessSensorMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortTestGUIThicknessSensorMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortTestGUIThicknessSensorMid.Location = new System.Drawing.Point(249, 540);
            this.serialPortTestGUIThicknessSensorMid.Name = "serialPortTestGUIThicknessSensorMid";
            this.serialPortTestGUIThicknessSensorMid.Size = new System.Drawing.Size(231, 118);
            this.serialPortTestGUIThicknessSensorMid.TabIndex = 44;
            this.serialPortTestGUIThicknessSensorMid.Text = "厚度测量中";
            // 
            // serialPortTestGUIThicknessSensorLeft
            // 
            this.serialPortTestGUIThicknessSensorLeft.AutoSize = true;
            this.serialPortTestGUIThicknessSensorLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortTestGUIThicknessSensorLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortTestGUIThicknessSensorLeft.Location = new System.Drawing.Point(6, 540);
            this.serialPortTestGUIThicknessSensorLeft.Name = "serialPortTestGUIThicknessSensorLeft";
            this.serialPortTestGUIThicknessSensorLeft.Size = new System.Drawing.Size(231, 118);
            this.serialPortTestGUIThicknessSensorLeft.TabIndex = 43;
            this.serialPortTestGUIThicknessSensorLeft.Text = "厚度测量左";
            // 
            // serialPortTestGUIOutlineMeasLightController
            // 
            this.serialPortTestGUIOutlineMeasLightController.AutoSize = true;
            this.serialPortTestGUIOutlineMeasLightController.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortTestGUIOutlineMeasLightController.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortTestGUIOutlineMeasLightController.Location = new System.Drawing.Point(736, 206);
            this.serialPortTestGUIOutlineMeasLightController.Name = "serialPortTestGUIOutlineMeasLightController";
            this.serialPortTestGUIOutlineMeasLightController.Size = new System.Drawing.Size(231, 118);
            this.serialPortTestGUIOutlineMeasLightController.TabIndex = 42;
            this.serialPortTestGUIOutlineMeasLightController.Text = "光源控制";
            this.serialPortTestGUIOutlineMeasLightController.Visible = false;
            // 
            // serialPortTestGUILoadInBarcode
            // 
            this.serialPortTestGUILoadInBarcode.AutoSize = true;
            this.serialPortTestGUILoadInBarcode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortTestGUILoadInBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortTestGUILoadInBarcode.Location = new System.Drawing.Point(492, 206);
            this.serialPortTestGUILoadInBarcode.Name = "serialPortTestGUILoadInBarcode";
            this.serialPortTestGUILoadInBarcode.Size = new System.Drawing.Size(231, 118);
            this.serialPortTestGUILoadInBarcode.TabIndex = 41;
            this.serialPortTestGUILoadInBarcode.Text = "进料条码";
            // 
            // serialPortSettingGUIThicknessSensorRight
            // 
            this.serialPortSettingGUIThicknessSensorRight.AutoSize = true;
            this.serialPortSettingGUIThicknessSensorRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortSettingGUIThicknessSensorRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortSettingGUIThicknessSensorRight.Location = new System.Drawing.Point(492, 341);
            this.serialPortSettingGUIThicknessSensorRight.Name = "serialPortSettingGUIThicknessSensorRight";
            this.serialPortSettingGUIThicknessSensorRight.Owner = null;
            this.serialPortSettingGUIThicknessSensorRight.Size = new System.Drawing.Size(238, 193);
            this.serialPortSettingGUIThicknessSensorRight.TabIndex = 40;
            this.serialPortSettingGUIThicknessSensorRight.Text = "厚度测量右";
            // 
            // serialPortSettingGUIThicknessSensorMid
            // 
            this.serialPortSettingGUIThicknessSensorMid.AutoSize = true;
            this.serialPortSettingGUIThicknessSensorMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortSettingGUIThicknessSensorMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortSettingGUIThicknessSensorMid.Location = new System.Drawing.Point(249, 341);
            this.serialPortSettingGUIThicknessSensorMid.Name = "serialPortSettingGUIThicknessSensorMid";
            this.serialPortSettingGUIThicknessSensorMid.Owner = null;
            this.serialPortSettingGUIThicknessSensorMid.Size = new System.Drawing.Size(238, 193);
            this.serialPortSettingGUIThicknessSensorMid.TabIndex = 39;
            this.serialPortSettingGUIThicknessSensorMid.Text = "厚度测量中";
            // 
            // serialPortSettingGUIThicknessSensorLeft
            // 
            this.serialPortSettingGUIThicknessSensorLeft.AutoSize = true;
            this.serialPortSettingGUIThicknessSensorLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortSettingGUIThicknessSensorLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortSettingGUIThicknessSensorLeft.Location = new System.Drawing.Point(6, 341);
            this.serialPortSettingGUIThicknessSensorLeft.Name = "serialPortSettingGUIThicknessSensorLeft";
            this.serialPortSettingGUIThicknessSensorLeft.Owner = null;
            this.serialPortSettingGUIThicknessSensorLeft.Size = new System.Drawing.Size(238, 193);
            this.serialPortSettingGUIThicknessSensorLeft.TabIndex = 38;
            this.serialPortSettingGUIThicknessSensorLeft.Text = "厚度测量左";
            // 
            // serialPortSettingGUIOutlineMeasLightController
            // 
            this.serialPortSettingGUIOutlineMeasLightController.AutoSize = true;
            this.serialPortSettingGUIOutlineMeasLightController.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortSettingGUIOutlineMeasLightController.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortSettingGUIOutlineMeasLightController.Location = new System.Drawing.Point(736, 7);
            this.serialPortSettingGUIOutlineMeasLightController.Name = "serialPortSettingGUIOutlineMeasLightController";
            this.serialPortSettingGUIOutlineMeasLightController.Owner = null;
            this.serialPortSettingGUIOutlineMeasLightController.Size = new System.Drawing.Size(238, 193);
            this.serialPortSettingGUIOutlineMeasLightController.TabIndex = 37;
            this.serialPortSettingGUIOutlineMeasLightController.Text = "光源控制";
            this.serialPortSettingGUIOutlineMeasLightController.Visible = false;
            // 
            // serialPortSettingGUILoadInBarcode
            // 
            this.serialPortSettingGUILoadInBarcode.AutoSize = true;
            this.serialPortSettingGUILoadInBarcode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortSettingGUILoadInBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serialPortSettingGUILoadInBarcode.Location = new System.Drawing.Point(492, 7);
            this.serialPortSettingGUILoadInBarcode.Name = "serialPortSettingGUILoadInBarcode";
            this.serialPortSettingGUILoadInBarcode.Owner = null;
            this.serialPortSettingGUILoadInBarcode.Size = new System.Drawing.Size(238, 193);
            this.serialPortSettingGUILoadInBarcode.TabIndex = 36;
            this.serialPortSettingGUILoadInBarcode.Text = "进料条码";
            // 
            // serialPortInfoListGUI1
            // 
            this.serialPortInfoListGUI1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serialPortInfoListGUI1.Location = new System.Drawing.Point(3, 3);
            this.serialPortInfoListGUI1.Margin = new System.Windows.Forms.Padding(0);
            this.serialPortInfoListGUI1.Name = "serialPortInfoListGUI1";
            this.serialPortInfoListGUI1.Size = new System.Drawing.Size(486, 335);
            this.serialPortInfoListGUI1.TabIndex = 35;
            // 
            // tabControlZone
            // 
            this.tabControlZone.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControlZone.Controls.Add(this.tabPageSerialPort);
            this.tabControlZone.Controls.Add(this.tabPageProtocol);
            this.tabControlZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlZone.Location = new System.Drawing.Point(0, 0);
            this.tabControlZone.Name = "tabControlZone";
            this.tabControlZone.SelectedIndex = 0;
            this.tabControlZone.Size = new System.Drawing.Size(984, 711);
            this.tabControlZone.TabIndex = 7;
            // 
            // tabPageProtocol
            // 
            this.tabPageProtocol.Controls.Add(this.protocolSettingThicknessSetZero);
            this.tabPageProtocol.Controls.Add(this.protocolSettingLightControl);
            this.tabPageProtocol.Controls.Add(this.protocolSettingThicknessMeas);
            this.tabPageProtocol.Controls.Add(this.protocolSettingBarcode);
            this.tabPageProtocol.Location = new System.Drawing.Point(4, 25);
            this.tabPageProtocol.Name = "tabPageProtocol";
            this.tabPageProtocol.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProtocol.Size = new System.Drawing.Size(976, 682);
            this.tabPageProtocol.TabIndex = 1;
            this.tabPageProtocol.Text = "通讯协议";
            this.tabPageProtocol.UseVisualStyleBackColor = true;
            // 
            // protocolSettingThicknessSetZero
            // 
            this.protocolSettingThicknessSetZero.AutoSize = true;
            this.protocolSettingThicknessSetZero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.protocolSettingThicknessSetZero.Decode_Size = 0;
            this.protocolSettingThicknessSetZero.Decode_StartIndex = 0;
            this.protocolSettingThicknessSetZero.EndString = null;
            this.protocolSettingThicknessSetZero.EndwithFeedLine = false;
            this.protocolSettingThicknessSetZero.EndWithReturn = false;
            this.protocolSettingThicknessSetZero.Location = new System.Drawing.Point(6, 295);
            this.protocolSettingThicknessSetZero.Name = "protocolSettingThicknessSetZero";
            this.protocolSettingThicknessSetZero.ReceivedSize = 0;
            this.protocolSettingThicknessSetZero.ReturnError = null;
            this.protocolSettingThicknessSetZero.Size = new System.Drawing.Size(354, 279);
            this.protocolSettingThicknessSetZero.StartCommandEnd = null;
            this.protocolSettingThicknessSetZero.StopCommand = null;
            this.protocolSettingThicknessSetZero.TabIndex = 3;
            this.protocolSettingThicknessSetZero.TimeOut = 0;
            // 
            // protocolSettingLightControl
            // 
            this.protocolSettingLightControl.AutoSize = true;
            this.protocolSettingLightControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.protocolSettingLightControl.Decode_Size = 0;
            this.protocolSettingLightControl.Decode_StartIndex = 0;
            this.protocolSettingLightControl.EndString = null;
            this.protocolSettingLightControl.EndwithFeedLine = false;
            this.protocolSettingLightControl.EndWithReturn = false;
            this.protocolSettingLightControl.Location = new System.Drawing.Point(366, 295);
            this.protocolSettingLightControl.Name = "protocolSettingLightControl";
            this.protocolSettingLightControl.ReceivedSize = 0;
            this.protocolSettingLightControl.ReturnError = null;
            this.protocolSettingLightControl.Size = new System.Drawing.Size(354, 279);
            this.protocolSettingLightControl.StartCommandEnd = null;
            this.protocolSettingLightControl.StopCommand = null;
            this.protocolSettingLightControl.TabIndex = 2;
            this.protocolSettingLightControl.TimeOut = 0;
            this.protocolSettingLightControl.Visible = false;
            // 
            // protocolSettingThicknessMeas
            // 
            this.protocolSettingThicknessMeas.AutoSize = true;
            this.protocolSettingThicknessMeas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.protocolSettingThicknessMeas.Decode_Size = 0;
            this.protocolSettingThicknessMeas.Decode_StartIndex = 0;
            this.protocolSettingThicknessMeas.EndString = null;
            this.protocolSettingThicknessMeas.EndwithFeedLine = false;
            this.protocolSettingThicknessMeas.EndWithReturn = false;
            this.protocolSettingThicknessMeas.Location = new System.Drawing.Point(6, 7);
            this.protocolSettingThicknessMeas.Name = "protocolSettingThicknessMeas";
            this.protocolSettingThicknessMeas.ReceivedSize = 0;
            this.protocolSettingThicknessMeas.ReturnError = null;
            this.protocolSettingThicknessMeas.Size = new System.Drawing.Size(354, 279);
            this.protocolSettingThicknessMeas.StartCommandEnd = null;
            this.protocolSettingThicknessMeas.StopCommand = null;
            this.protocolSettingThicknessMeas.TabIndex = 1;
            this.protocolSettingThicknessMeas.TimeOut = 0;
            // 
            // protocolSettingBarcode
            // 
            this.protocolSettingBarcode.AutoSize = true;
            this.protocolSettingBarcode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.protocolSettingBarcode.Decode_Size = 0;
            this.protocolSettingBarcode.Decode_StartIndex = 0;
            this.protocolSettingBarcode.EndString = null;
            this.protocolSettingBarcode.EndwithFeedLine = false;
            this.protocolSettingBarcode.EndWithReturn = false;
            this.protocolSettingBarcode.Location = new System.Drawing.Point(366, 7);
            this.protocolSettingBarcode.Name = "protocolSettingBarcode";
            this.protocolSettingBarcode.ReceivedSize = 0;
            this.protocolSettingBarcode.ReturnError = null;
            this.protocolSettingBarcode.Size = new System.Drawing.Size(354, 279);
            this.protocolSettingBarcode.StartCommandEnd = null;
            this.protocolSettingBarcode.StopCommand = null;
            this.protocolSettingBarcode.TabIndex = 0;
            this.protocolSettingBarcode.TimeOut = 0;
            // 
            // ConfigurationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlZone);
            this.Name = "ConfigurationPanel";
            this.Size = new System.Drawing.Size(984, 711);
            this.tabPageSerialPort.ResumeLayout(false);
            this.tabPageSerialPort.PerformLayout();
            this.tabControlZone.ResumeLayout(false);
            this.tabPageProtocol.ResumeLayout(false);
            this.tabPageProtocol.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageSerialPort;
        private System.Windows.Forms.TabControl tabControlZone;
        private System.Windows.Forms.TabPage tabPageProtocol;
        private Colibri.CommonModule.Port.ProtocolSetting protocolSettingLightControl;
        private Colibri.CommonModule.Port.ProtocolSetting protocolSettingThicknessMeas;
        private Colibri.CommonModule.Port.ProtocolSetting protocolSettingBarcode;
        private Colibri.CommonModule.Port.SerialPortTestGUI serialPortTestGUIThicknessSensorRight;
        private Colibri.CommonModule.Port.SerialPortTestGUI serialPortTestGUIThicknessSensorMid;
        private Colibri.CommonModule.Port.SerialPortTestGUI serialPortTestGUIThicknessSensorLeft;
        private Colibri.CommonModule.Port.SerialPortTestGUI serialPortTestGUIOutlineMeasLightController;
        private Colibri.CommonModule.Port.SerialPortTestGUI serialPortTestGUILoadInBarcode;
        private Colibri.CommonModule.Port.SerialPortSettingGUI serialPortSettingGUIThicknessSensorRight;
        private Colibri.CommonModule.Port.SerialPortSettingGUI serialPortSettingGUIThicknessSensorMid;
        private Colibri.CommonModule.Port.SerialPortSettingGUI serialPortSettingGUIThicknessSensorLeft;
        private Colibri.CommonModule.Port.SerialPortSettingGUI serialPortSettingGUIOutlineMeasLightController;
        private Colibri.CommonModule.Port.SerialPortSettingGUI serialPortSettingGUILoadInBarcode;
        private Colibri.CommonModule.Port.SerialPortInfoListGUI serialPortInfoListGUI1;
        private Colibri.CommonModule.Port.ProtocolSetting protocolSettingThicknessSetZero;
    }
}
