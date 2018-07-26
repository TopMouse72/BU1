using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using Colibri.CommonModule;
using Colibri.CommonModule.Port;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule.Event;
using Colibri.MotionModule.Googol.GTS;
using GoogolExtIO;

namespace HardwarePool
{
    /// <summary>
    /// 定义硬件变量
    /// </summary>
    public class SystemHardware : IOSubscriber
    {
        private HardwareInfoList hardwares = new HardwareInfoList();
        private IOPublisher m_ioPublisher = new IOPublisher();
        private SystemPublisher m_systemPublisher = new SystemPublisher();
        private CAxisCardBase[] m_MotionCards = new CAxisCardBase[4];
        private BaseIOSys[] m_IOSysList = new BaseIOSys[5];
        private SerialPortList m_SystemSerialPorts = new SerialPortList();
        private SerialPortControl[] m_SerialPortControlList = new SerialPortControl[Enum.GetNames(typeof(HardwareSerialPortName)).Length];
        private SerialPortData[] m_SerialPortDataList = new SerialPortData[Enum.GetNames(typeof(HardwareSerialPortName)).Length];
        public SerialPortList SystemSerialPorts
        {
            get { return m_SystemSerialPorts; }
        }
        private static SystemHardware m_hardware;
        public static SystemHardware instance
        {
            get
            {
                if (m_hardware == null)
                    m_hardware = new SystemHardware();
                return m_hardware;
            }
        }
        public bool isBusy;
        public override bool IsSimulation
        {
            get { return base.IsSimulation; }
            set
            {
                base.IsSimulation = value;
                for (int i = 0; i < m_MotionCards.Length; i++)
                    m_MotionCards[i].IsSimulation = value;
            }
        }
        public SystemHardware()
        {
            hardwares = HardwareInfoList.ReadSetting();
            HardwareInfoList.SaveSetting(hardwares);
            CommonFunction.CheckAndRestore(CommonFunction.DefaultConfigPath + "MotionConfig0.cfg", CommonFunction.DefaultBackupConfigPath + "MotionConfig0.cfg");
            CommonFunction.CheckAndRestore(CommonFunction.DefaultConfigPath + "MotionConfig1.cfg", CommonFunction.DefaultBackupConfigPath + "MotionConfig1.cfg");
            CommonFunction.CheckAndRestore(CommonFunction.DefaultConfigPath + "MotionConfig2.cfg", CommonFunction.DefaultBackupConfigPath + "MotionConfig2.cfg");
            CommonFunction.CheckAndRestore(CommonFunction.DefaultConfigPath + "MotionConfig3.cfg", CommonFunction.DefaultBackupConfigPath + "MotionConfig3.cfg");
            CommonFunction.CheckAndRestore(CommonFunction.DefaultConfigPath + "ExtMdl1.cfg", CommonFunction.DefaultBackupConfigPath + "ExtMdl1.cfg");

            m_MotionCards[0] = new GTS_CAxisCard(new GTS_CardArg(0, "GTS-400#1", GTS_CardType.GTS400PG), CommonFunction.DefaultConfigPath + "MotionConfig0.cfg");
            m_MotionCards[1] = new GTS_CAxisCard(new GTS_CardArg(1, "GTS-400#2", GTS_CardType.GTS400PG), CommonFunction.DefaultConfigPath + "MotionConfig1.cfg");
            m_MotionCards[2] = new GTS_CAxisCard(new GTS_CardArg(2, "GTS-400#3", GTS_CardType.GTS400PG), CommonFunction.DefaultConfigPath + "MotionConfig2.cfg");
            m_MotionCards[3] = new GTS_CAxisCard(new GTS_CardArg(3, "GTS-400#4", GTS_CardType.GTS400PG), CommonFunction.DefaultConfigPath + "MotionConfig3.cfg");

            m_IOSysList[0] = ((GTS_CAxisCard)m_MotionCards[0]).IOSys;
            m_IOSysList[1] = ((GTS_CAxisCard)m_MotionCards[1]).IOSys;
            m_IOSysList[2] = ((GTS_CAxisCard)m_MotionCards[2]).IOSys;
            m_IOSysList[3] = ((GTS_CAxisCard)m_MotionCards[3]).IOSys;
            m_IOSysList[4] = new GoogleExtIOSystem("GoogleExt", 4, 64, 64, CommonFunction.DefaultConfigPath + "ExtMdl1.cfg");

            for (int i = 0; i < m_SerialPortControlList.Length; i++)
            {
                m_SerialPortControlList[i] = new SerialPortControl(m_SystemSerialPorts, ((HardwareSerialPortName)i).ToString());
                m_SerialPortDataList[i] = new SerialPortData(m_SerialPortControlList[i]);
            }
            BaseIOPort temp;
            for (int i = 0; i < 64; i++)
            {
                temp = m_IOSysList[i / 16].GetInPort(i % 16);
                temp.PortName = "In" + ((HardwareInportName)i).ToString();
                temp.PortCode = "EXI" + i.ToString("000");
                if (temp.ReadSetting()) temp.WriteSetting();
                temp = m_IOSysList[i / 16].GetOutPort(i % 16);
                temp.PortName = "Out" + ((HardwareOutportName)i).ToString();
                temp.PortCode = "EXO" + i.ToString("000");
                if (temp.ReadSetting()) temp.WriteSetting();
                temp.SetOutput(false);
            }
            for (int i = 0; i < 64; i++)
            {
                temp = m_IOSysList[4].GetInPort(i);
                temp.PortName = "In" + ((HardwareInportName)(i + 64)).ToString();
                temp.PortCode = "EXI" + (i + 64).ToString("000");
                if (temp.ReadSetting()) temp.WriteSetting();
                temp = m_IOSysList[4].GetOutPort(i);
                temp.PortName = "Out" + ((HardwareOutportName)(i + 64)).ToString();
                temp.PortCode = "EXO" + (i + 64).ToString("000");
                if (temp.ReadSetting()) temp.WriteSetting();
                temp.SetOutput(false);
            }
        }
        public BaseIOPort GetInPort(HardwareInportName PortIndex)
        {
            if ((int)PortIndex < 16)//GTS-400#1
                return m_IOSysList[0].GetInPort((int)PortIndex);
            else if ((int)PortIndex < 32)//GTS-400#2
                return m_IOSysList[1].GetInPort((int)PortIndex - 16);
            else if ((int)PortIndex < 48)//GTS-400#3
                return m_IOSysList[2].GetInPort((int)PortIndex - 32);
            else if ((int)PortIndex < 64)//GTS-400#4
                return m_IOSysList[3].GetInPort((int)PortIndex - 48);
            else if ((int)PortIndex < 128)//GoogleExt
                return m_IOSysList[4].GetInPort((int)PortIndex - 64);
            else return null;

        }
        public BaseIOPort GetInPort(string PortName)
        {
            if (!Enum.TryParse(PortName, out HardwareInportName temp)) return null;
            return GetInPort(temp);
        }
        public BaseIOPort GetOutPort(HardwareOutportName PortIndex)
        {
            if ((int)PortIndex < 16)//GTS-400#1
                return m_IOSysList[0].GetOutPort((int)PortIndex);
            else if ((int)PortIndex < 32)//GTS-400#2
                return m_IOSysList[1].GetOutPort((int)PortIndex - 16);
            else if ((int)PortIndex < 48)//GTS-400#3
                return m_IOSysList[2].GetOutPort((int)PortIndex - 32);
            else if ((int)PortIndex < 64)//GTS-400#4
                return m_IOSysList[3].GetOutPort((int)PortIndex - 48);
            else if ((int)PortIndex < 128)//GoogleExt
                return m_IOSysList[4].GetOutPort((int)PortIndex - 64);
            else return null;

        }
        public BaseIOPort GetOutPort(string PortName)
        {
            if (!Enum.TryParse(PortName, out HardwareOutportName temp)) return null;
            return GetOutPort(temp);
        }
        public CAxisBase GetAxis(HardwareAxisName motor)
        {
            int index = (int)motor;
            if (index < 4)
                return m_MotionCards[0].GetAxis(index);
            else if (index < 8)
                return m_MotionCards[1].GetAxis(index - 4);
            else if (index < 12)
                return m_MotionCards[2].GetAxis(index - 8);
            else if (index < 16)
                return m_MotionCards[3].GetAxis(index - 12);
            else
                return null;
        }
        public CAxisBase GetAxis(string motor)
        {
            if (!Enum.TryParse(motor, out HardwareAxisName temp)) return null;
            return GetAxis(temp);
        }
        public SerialPortData GetSerialPort(HardwareSerialPortName PortName)
        {
            int index = (int)PortName;
            return m_SerialPortDataList[index];
        }
        public SerialPortData GetSerialPort(string PortName)
        {
            if (!Enum.TryParse(PortName, out HardwareSerialPortName temp)) return null;
            return GetSerialPort(temp);
        }
        public void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                m_MotionCards[i].IsSimulation = IsSimulation;
                m_MotionCards[i].Init();
            }
            m_IOSysList[4].IsSimulation = IsSimulation;
            m_IOSysList[4].InitialCard(0);
            GTS_CAxis axis;
            foreach (HardwareAxisName axisname in Enum.GetValues(typeof(HardwareAxisName)))
            {
                axis = new GTS_CAxis(m_MotionCards[(int)axisname / 4], (short)((int)axisname % 4), axisname.ToString(), 1, AxisType.Step, "mm", HomeMode.Limiter, HomeDir.Negative);
                m_MotionCards[(int)axisname / 4].AddAxis(axis);
                axis.Init();
            }
        }
        public void Release()
        {
            //for (int i = 0; i < 64; i++)
            //{
            //    m_IOSysList[i / 16].GetOutPort(i % 16).SetOutput(false);
            //}
            //for (int i = 0; i < 64; i++)
            //{
            //    m_IOSysList[4].GetOutPort(i).SetOutput(false);
            //}
            for (int i = 0; i < m_IOSysList.Length; i++)
            {
                m_IOSysList[i].StopInputAcquisition();
            }
            TimeClass.Delay(200);
            for (int i = 0; i < 4; i++)
                m_MotionCards[i].Release();
        }
        private void ThicknessPortOnOpen(SerialPort port)
        {
            port.DtrEnable = true;
            port.RtsEnable = false;
        }
        public void Open()
        {
            if (!IsSimulation)
            {
                GetSerialPort(HardwareSerialPortName.LoadInBarcode).OwnerPort.PortOpen(true);
                GetSerialPort(HardwareSerialPortName.OutlineMeasLightController).OwnerPort.PortOpen(true);
                GetSerialPort(HardwareSerialPortName.ThicknessSensorLeft).OwnerPort.PortOpen(true, ThicknessPortOnOpen);
                GetSerialPort(HardwareSerialPortName.ThicknessSensorMid).OwnerPort.PortOpen(true, ThicknessPortOnOpen);
                GetSerialPort(HardwareSerialPortName.ThicknessSensorRight).OwnerPort.PortOpen(true, ThicknessPortOnOpen);
                foreach (BaseIOSys iosys in m_IOSysList)
                {
                    TimeClass.Delay(12);
                    iosys.StartInputAcquisition();
                }
            }
        }
    }
}