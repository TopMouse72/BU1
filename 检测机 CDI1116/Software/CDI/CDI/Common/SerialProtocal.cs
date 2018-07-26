using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule.Port;
using HardwarePool;

namespace CDI
{
    public enum HardwareSerialProtocolName
    {
        BarcodeRead,
        ThicknessRead,
        ThicknessPreSet,
        LightControl,
    }
    public class SerialProtocalCollection
    {
        private static SerialProtocalCollection _instance;
        public static SerialProtocalCollection instance
        {
            get
            {
                if (_instance == null) _instance = new SerialProtocalCollection();
                return _instance;
            }
        }
        private SerialProtocolControl m_SerialProtocolBarcodeRead = new SerialProtocolControl(HardwareSerialProtocolName.BarcodeRead.ToString());
        private SerialProtocolControl m_SerialProtocolThicknessSens = new SerialProtocolControl(HardwareSerialProtocolName.ThicknessRead.ToString());
        private SerialProtocolControl m_SerialProtocolLightControl = new SerialProtocolControl(HardwareSerialProtocolName.LightControl.ToString());
        private SerialProtocolControl m_SerialProtocolThicknessPreSet = new SerialProtocolControl(HardwareSerialProtocolName.ThicknessPreSet.ToString());
        public SerialProtocolControl SerialProtocolBarcodeRead
        {
            get { return m_SerialProtocolBarcodeRead; }
        }
        public SerialProtocolControl SerialProtocolThickness
        {
            get { return m_SerialProtocolThicknessSens; }
        }
        public SerialProtocolControl SerialProtocolCCDLightControl
        {
            get { return m_SerialProtocolLightControl; }
        }
        public SerialProtocolControl SerialProtocolThicknessPreSet
        {
            get { return m_SerialProtocolThicknessPreSet; }
        }
        public void ConnectProtocol()
        {
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.LoadInBarcode).AddProtocol(m_SerialProtocolBarcodeRead);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.OutlineMeasLightController).AddProtocol(m_SerialProtocolLightControl);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorLeft).AddProtocol(m_SerialProtocolThicknessSens);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorMid).AddProtocol(m_SerialProtocolThicknessSens);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorRight).AddProtocol(m_SerialProtocolThicknessSens);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorLeft).AddProtocol(m_SerialProtocolThicknessPreSet);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorMid).AddProtocol(m_SerialProtocolThicknessPreSet);
            SystemHardware.instance.GetSerialPort(HardwareSerialPortName.ThicknessSensorRight).AddProtocol(m_SerialProtocolThicknessPreSet);
        }
    }
}
