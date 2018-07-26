using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDI;
using CDI.Zone;

namespace CDI.GUI
{
    /// <summary>
    /// 主界面设置界面
    /// </summary>
    public partial class ConfigurationPanel : UserControl
    {
        private ClassWorkZones _zones = ClassWorkZones.Instance;
        public ConfigurationPanel()
        {
            InitializeComponent();
        }
        public void InitGUI()
        {
            TabPage temp;
            foreach (ClassBaseWorkZone zone in _zones.ToArray())
            {
                temp = new TabPage(zone.Name);
                temp.AutoScroll = true;
                temp.Size = tabControlZone.ClientSize;
                temp.Controls.Add(zone.ZoneSettingPanel);
                //zone.ZoneSettingPanel.Size = temp.ClientSize;
                zone.ZoneSettingPanel.Dock = DockStyle.Fill;
                zone.ZoneSettingPanel.Top = 0;
                zone.ZoneSettingPanel.Left = 0;
                tabControlZone.TabPages.Add(temp);
            }
            temp = new TabPage("程序测试");
            temp.Controls.Add(new TestPanel());
            tabControlZone.TabPages.Add(temp);
            HardwarePool.SystemHardware.instance.SystemSerialPorts.PortListGUI = serialPortInfoListGUI1;
            SerialProtocalCollection.instance.SerialProtocolBarcodeRead.PortProtocolSettingGUI = protocolSettingBarcode;
            SerialProtocalCollection.instance.SerialProtocolCCDLightControl.PortProtocolSettingGUI = protocolSettingLightControl;
            SerialProtocalCollection.instance.SerialProtocolThickness.PortProtocolSettingGUI = protocolSettingThicknessMeas;
            SerialProtocalCollection.instance.SerialProtocolThicknessPreSet.PortProtocolSettingGUI = protocolSettingThicknessSetZero;

            ClassWorkZones.Instance.WorkZone上料传送.ThisSerialPortData(ClassZone上料传送.EnumSerialPortName.LoadInBarcode).OwnerPort.PortSettingGUI = serialPortSettingGUILoadInBarcode;
            ClassWorkZones.Instance.WorkZone上料传送.ThisSerialPortData(ClassZone上料传送.EnumSerialPortName.LoadInBarcode).OwnerPort.PortTestGUI = serialPortTestGUILoadInBarcode;
            ClassWorkZones.Instance.WorkZone上料传送.ThisSerialPortData(ClassZone上料传送.EnumSerialPortName.LoadInBarcode).ProtocolDisplayGUI = serialPortSettingGUILoadInBarcode;
            ClassWorkZones.Instance.WorkZone上料传送.ThisSerialPortData(ClassZone上料传送.EnumSerialPortName.LoadInBarcode).AddDispStatusControl(serialPortSettingGUILoadInBarcode);
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorLeft).OwnerPort.PortSettingGUI = serialPortSettingGUIThicknessSensorLeft;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorLeft).OwnerPort.PortTestGUI = serialPortTestGUIThicknessSensorLeft;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorLeft).ProtocolDisplayGUI = serialPortSettingGUIThicknessSensorLeft;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorMid).OwnerPort.PortSettingGUI = serialPortSettingGUIThicknessSensorMid;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorMid).OwnerPort.PortTestGUI = serialPortTestGUIThicknessSensorMid;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorMid).ProtocolDisplayGUI = serialPortSettingGUIThicknessSensorMid;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorRight).OwnerPort.PortSettingGUI = serialPortSettingGUIThicknessSensorRight;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorRight).OwnerPort.PortTestGUI = serialPortTestGUIThicknessSensorRight;
            ClassWorkZones.Instance.WorkZone厚度测量.ThisSerialPortData(ClassZone厚度测量.EnumSerialPortName.ThicknessSensorRight).ProtocolDisplayGUI = serialPortSettingGUIThicknessSensorRight;
            ClassWorkZones.Instance.WorkZone尺寸测量.ThisSerialPortData(ClassZone尺寸测量.EnumSerialPortName.OutlineMeasLightController).OwnerPort.PortSettingGUI = serialPortSettingGUIOutlineMeasLightController;
            ClassWorkZones.Instance.WorkZone尺寸测量.ThisSerialPortData(ClassZone尺寸测量.EnumSerialPortName.OutlineMeasLightController).OwnerPort.PortTestGUI = serialPortTestGUIOutlineMeasLightController;
            ClassWorkZones.Instance.WorkZone尺寸测量.ThisSerialPortData(ClassZone尺寸测量.EnumSerialPortName.OutlineMeasLightController).ProtocolDisplayGUI = serialPortSettingGUIOutlineMeasLightController;
        }
    }
}
