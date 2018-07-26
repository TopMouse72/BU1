using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule.Port;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;

namespace CDI.Zone
{
    public class ClassZone厚度测量 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            ThicknessMeasY = HardwareAxisName.ThicknessMeasY,//厚度测量X轴
        }
        public enum EnumPointX
        {
            Idle,
            Meas,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            ThicknessMeasCyLeftUp = HardwareInportName.ThicknessMeasCyLeftUp,//厚度测量左气缸上
            ThicknessMeasCyLeftDown = HardwareInportName.ThicknessMeasCyLeftDown,//厚度测量左气缸下
            ThicknessMeasCyMidUp = HardwareInportName.ThicknessMeasCyMidUp,//厚度测量中气缸上
            ThicknessMeasCyMidDown = HardwareInportName.ThicknessMeasCyMidDown,//厚度测量中气缸下
            ThicknessMeasCyRightUp = HardwareInportName.ThicknessMeasCyRightUp,//厚度测量右气缸上
            ThicknessMeasCyRightDown = HardwareInportName.ThicknessMeasCyRightDown,//厚度测量右气缸下
            ThicknessMeasVacSensLeft = HardwareInportName.ThicknessMeasVacSensLeft,//厚度测量左吸头真空
            ThicknessMeasVacSensMid = HardwareInportName.ThicknessMeasVacSensMid,//厚度测量中吸头真空
            ThicknessMeasVacSensRight = HardwareInportName.ThicknessMeasVacSensRight,//厚度测量右吸头真空
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            ThicknessMeasCyLeftUp = HardwareOutportName.ThicknessMeasCyLeftUp,//厚度检测左气缸上控制
            ThicknessMeasCyLeftDown = HardwareOutportName.ThicknessMeasCyLeftDown,//厚度检测左气缸下控制
            ThicknessMeasCyMidUp = HardwareOutportName.ThicknessMeasCyMidUp,//厚度检测中气缸上控制
            ThicknessMeasCyMidDown = HardwareOutportName.ThicknessMeasCyMidDown,//厚度检测中气缸下控制
            ThicknessMeasCyRightUp = HardwareOutportName.ThicknessMeasCyRightUp,//厚度检测右气缸上控制
            ThicknessMeasCyRightDown = HardwareOutportName.ThicknessMeasCyRightDown,//厚度检测右气缸下控制
            ThicknessMeasVacLeft = HardwareOutportName.ThicknessMeasVacLeft,//厚度检测左吸头真空
            ThicknessMeasVacMid = HardwareOutportName.ThicknessMeasVacMid,//厚度检测中吸头真空
            ThicknessMeasVacRight = HardwareOutportName.ThicknessMeasVacRight,//厚度检测右吸头真空
            ThicknessMeasBlow = HardwareOutportName.ThicknessMeasBlow,//厚度检测吹气
        }
        public enum EnumSerialPortName
        {
            ThicknessSensorLeft = HardwareSerialPortName.ThicknessSensorLeft,
            ThicknessSensorMid = HardwareSerialPortName.ThicknessSensorMid,
            ThicknessSensorRight = HardwareSerialPortName.ThicknessSensorRight,
        }
        public CellCollection<CylinderControl> Cylinder = new CellCollection<CylinderControl>();
        public CellCollection<ClassAirPort> ThicknessCellVacuums = new CellCollection<ClassAirPort>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public CellCollection<SerialPortData> SerialPort = new CellCollection<SerialPortData>();
        public ClassAirPort CellBlow;
        public CAxisBase AxisThicknessMeasY;
        public ClassZone厚度测量() : base(EnumZoneName.Zone厚度测量.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                ThicknessCellVacuums.Add(i, new ClassAirPort());
                CellVacSensor.Add(i, new ClassAirPort());
                ThicknessDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
            }
            _serialReading = StartSerialReading;
            CellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName), typeof(EnumSerialPortName));
            AxisThicknessMeasY = ThisAxis(EnumAxisName.ThicknessMeasY); AxisThicknessMeasY.AddPoints(typeof(EnumPointX));
            SerialPort.Add(EnumCellIndex.左电芯, ThisSerialPortData(EnumSerialPortName.ThicknessSensorLeft));
            SerialPort.Add(EnumCellIndex.中电芯, ThisSerialPortData(EnumSerialPortName.ThicknessSensorMid));
            SerialPort.Add(EnumCellIndex.右电芯, ThisSerialPortData(EnumSerialPortName.ThicknessSensorRight));
            Cylinder.Add(EnumCellIndex.左电芯, new CylinderControl(/*"左电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.ThicknessMeasCyLeftDown),
                        ThisOutport(EnumOutportName.ThicknessMeasCyLeftUp),
                        ThisInport(EnumInportName.ThicknessMeasCyLeftDown),
                        ThisInport(EnumInportName.ThicknessMeasCyLeftUp)));
            Cylinder.Add(EnumCellIndex.中电芯, new CylinderControl(/*"中电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.ThicknessMeasCyMidDown),
                        ThisOutport(EnumOutportName.ThicknessMeasCyMidUp),
                        ThisInport(EnumInportName.ThicknessMeasCyMidDown),
                        ThisInport(EnumInportName.ThicknessMeasCyMidUp)));
            Cylinder.Add(EnumCellIndex.右电芯, new CylinderControl(/*"右电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.ThicknessMeasCyRightDown),
                        ThisOutport(EnumOutportName.ThicknessMeasCyRightUp),
                        ThisInport(EnumInportName.ThicknessMeasCyRightDown),
                        ThisInport(EnumInportName.ThicknessMeasCyRightUp)));
            ThicknessCellVacuums[EnumCellIndex.左电芯].Port = ThisOutport(EnumOutportName.ThicknessMeasVacLeft);
            ThicknessCellVacuums[EnumCellIndex.中电芯].Port = ThisOutport(EnumOutportName.ThicknessMeasVacMid);
            ThicknessCellVacuums[EnumCellIndex.右电芯].Port = ThisOutport(EnumOutportName.ThicknessMeasVacRight);
            CellBlow.Port = ThisOutport(EnumOutportName.ThicknessMeasBlow);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.ThicknessMeasVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.ThicknessMeasVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.ThicknessMeasVacSensRight);
            ZoneSettingPanel = new SettingPanelZone厚度测量();
            ZoneManualPanel = new ManualPanelZone厚度测量();
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            base.ResetOutPort();
            for (int i = 0; i < CELLCOUNT; i++)
                ThicknessCellVacuums[i].SetOutPortStatus(false);
            CellBlow.SetOutPortStatus(false);
            return AllCyUp();
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisThicknessMeasY.ServoOn = true;
            Cylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("左气缸无法上移");
            if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("中气缸无法上移");
            if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("右气缸无法上移");
            if (err.NoError) err.CollectErrInfo(MotorReset(AxisThicknessMeasY, EnumPointX.Idle));
            SetZero();
            for (int i = 0; i < CELLCOUNT; i++)
            {
                ThicknessDataStations[i].CellData = null;
                ReadDone[i] = false;
            }
        }
        public void SetZero()
        {
            string temp = "";
            StartSerialReading(SerialPort[EnumCellIndex.左电芯], HardwareSerialProtocolName.ThicknessPreSet, ref temp);
            StartSerialReading(SerialPort[EnumCellIndex.中电芯], HardwareSerialProtocolName.ThicknessPreSet, ref temp);
            StartSerialReading(SerialPort[EnumCellIndex.右电芯], HardwareSerialProtocolName.ThicknessPreSet, ref temp);
        }
        #region Event
        protected override void InPortActive(string inPort)
        {
        }

        protected override void InPortDeActive(string inPort)
        {
        }
        #endregion Event
        #region Data
        public CellCollection<ClassDataStation> ThicknessDataStations = new CellCollection<ClassDataStation>();
        public string GetDataInfoString()
        {
            return GetDataInfoString(ThicknessDataStations);
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            ThicknessDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            ThicknessDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            ThicknessDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        private bool[] ReadDone = new bool[CELLCOUNT];
        public delegate string StartSerialReadingHandle(EnumCellIndex cellindex, HardwareSerialProtocolName protocol, DataComp NeedComp);
        protected StartSerialReadingHandle _serialReading;
        private void SerialReadingAsyncReturn(IAsyncResult result)
        {
            StartSerialReadingHandle handler = (StartSerialReadingHandle)result.AsyncState;
            try
            {
                handler.EndInvoke(result);
                result.AsyncWaitHandle.Close();
            }
            catch (Exception e)
            {
                ClassCommonSetting.ThrowException(handler, "SerialReading", e);
            }
        }
        private void AsyncStartSerialReading(EnumCellIndex index, HardwareSerialProtocolName protocol, DataComp NeedComp)
        {
            if (_serialReading != null)
            {
                _serialReading.BeginInvoke(index, protocol, NeedComp, SerialReadingAsyncReturn, _serialReading);
            }
        }
        private void GetData(EnumCellIndex index, string Data, DataComp NeedComp)
        {
            double temp;
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "测厚", $"{index}测厚GetData得到的数据：{Data}");
            if (!double.TryParse(Data.Replace("?", ""), out temp))
            {
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "测厚", "GetData得到的数据转换为double失败，数据没有处理");
                return;
            }
            ThicknessDataStations[index].CellData.Data.CellThickness.Value = temp;// double.Parse(Data.Replace("?", ""));
            if (NeedComp != DataComp.NoComp && ClassCommonSetting.SysParam.CurrentUsedGauge != null)
            {
                double reference = 0;
                double slope = 0, intercept = 0;
                bool enable = false;
                switch (index)
                {
                    case EnumCellIndex.中电芯:
                        reference = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefMid;
                        slope = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMidLinear.Slope;
                        intercept = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMidLinear.Intercept;
                        enable = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMidLinear.Enable;
                        break;
                    case EnumCellIndex.右电芯:
                        reference = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefRight;
                        slope = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessRightLinear.Slope;
                        intercept = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessRightLinear.Intercept;
                        enable = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessRightLinear.Enable;
                        break;
                    case EnumCellIndex.左电芯:
                        reference = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefLeft;
                        slope = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessLeftLinear.Slope;
                        intercept = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessLeftLinear.Intercept;
                        enable = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessLeftLinear.Enable;
                        break;
                }
                if ((NeedComp & DataComp.AddRef) != DataComp.NoComp)
                    ThicknessDataStations[index].CellData.Data.CellThickness.Value = reference - ThicknessDataStations[index].CellData.Data.CellThickness.Value;
                if ((NeedComp & DataComp.AddComp) != DataComp.NoComp && enable)
                    ThicknessDataStations[index].CellData.Data.CellThickness.Value = ThicknessDataStations[index].CellData.Data.CellThickness.Value * slope + intercept;
            }
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "测厚", $"获取原始数据{temp}，最终数据{ThicknessDataStations[index].CellData.Data.CellThickness.Value:0.000}");
            ThicknessDataStations[index].Refresh();
        }
        protected string StartSerialReading(EnumCellIndex index, HardwareSerialProtocolName protocol, DataComp NeedComp)
        {
            string Data = "";
            int i = (int)index;
            ReadDone[i] = false;
            string res = StartSerialReading(SerialPort[i], HardwareSerialProtocolName.ThicknessRead, ref Data);
            if (res != "")
                ClassErrorHandle.ShowError("读取测厚数据", String.Format("读取{0}测量数据出错：{1}", index, res), ErrorLevel.Error);
            else
            {
                GetData(index, Data, NeedComp);
                ReadDone[i] = true;
            }
            ReadDone[i] = true;
            return res;
        }
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= ThicknessDataStations[i].CellData != null;
            return need;
        }
        public ErrorInfoWithPause StartThicknessReading(DataComp NeedComp)
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.正常) return null;
            //ErrorInfoWithPause result;
            for (int i = 0; i < CELLCOUNT; i++)
            {
                if (ThicknessDataStations[i].CellData != null)
                    StartSerialReading((EnumCellIndex)i, HardwareSerialProtocolName.ThicknessRead, NeedComp);
                else
                    ReadDone[i] = true;
            }
            System.Threading.Thread.Sleep(10);
            //while (!ReadDone[0] || !ReadDone[1] || !ReadDone[2])
            //    System.Windows.Forms.Application.DoEvents();
            return null;
        }
        public ErrorInfoWithPause AllCyDown()
        {
            ErrorInfoWithPause res = null;
            string temp = "";

            if (ThicknessDataStations[EnumCellIndex.左电芯].CellData != null)
            {
                Cylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_DOWN, 0, false);
            }
            if (ThicknessDataStations[EnumCellIndex.中电芯].CellData != null)
            {
                Cylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_DOWN, 0, false);
            }
            if (ThicknessDataStations[EnumCellIndex.右电芯].CellData != null)
            {
                Cylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_DOWN, 0, false);
            }
            while (true)
            {
                temp = "";
                if (ThicknessDataStations[EnumCellIndex.左电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 左气缸";
                if (ThicknessDataStations[EnumCellIndex.中电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 中气缸";
                if (ThicknessDataStations[EnumCellIndex.右电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 右气缸";
                if (temp != "") //res = new ErrorInfoWithPause("厚度测量气缸下移错误:" + temp, ErrorLevel.Error);
                {
                    res = WaitAlarmPause("厚度测量", "厚度测量气缸下移超时错: " + temp);
                    if (res != null) return res;
                }
                else
                    break;
            }
            return null;
        }
        public ErrorInfoWithPause AllCyUp()
        {
            ErrorInfoWithPause res = null;
            string temp = "";
            while (true)
            {
                temp = "";
                for (int i = 0; i < CELLCOUNT; i++)
                    Cylinder[i].SetCylinderState(CYLIND_UP, 0, false);
                if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp += " 左气缸";
                if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp += " 中气缸";
                if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp += " 右气缸";
                if (temp != "") //res = new ErrorInfoWithPause("厚度测量气缸上移错误:" + temp, ErrorLevel.Error);
                {
                    res = WaitAlarmPause("厚度测量", "厚度测量气缸上移超时错: " + temp);
                    if (res != null) return res;
                }
                else
                    break;
            }
            return null;
        }
        public string CheckVacuumStatus(EnumCellIndex CellIndex)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return "";
            bool status = CellVacSensor[CellIndex].GetInPortStatus();
            string res = "";
            switch (CellIndex)
            {
                case EnumCellIndex.左电芯:
                    res = status ? "" : " 左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res = status ? "" : " 中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res = status ? "" : " 右真空";
                    break;
            }
            ThicknessDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = ThicknessDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = ThicknessDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = ThicknessDataStations[EnumCellIndex.右电芯].CellData != null;
            string res = "";
            res = ClassCommonSetting.CheckTimeOut(() =>
            {
                string vac = "";
                if (left) vac += CheckVacuumStatus(EnumCellIndex.左电芯);
                if (middle) vac += CheckVacuumStatus(EnumCellIndex.中电芯);
                if (right) vac += CheckVacuumStatus(EnumCellIndex.右电芯);
                return vac;
            });
            if (res != "")
                return new ErrorInfoWithPause("厚度检测工位真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        public ErrorInfoWithPause ActionLoad()
        {
            ErrorInfoWithPause res = null;
            while (!AxisThicknessMeasY.MoveTo(EnumPointX.Meas))
            //return DispMotionError(AxisThicknessMeasY, EnumPointX.Meas);
            {
                res = DispMotionError(AxisThicknessMeasY, EnumPointX.Meas);
                if (res != null) return res;
            }
            return AllCyDown();
        }
        public ErrorInfoWithPause ActionUnload()
        {
            ErrorInfoWithPause res;
            res = AllCyUp();
            if (res != null) return res;
            while (!AxisThicknessMeasY.MoveTo(EnumPointX.Idle))
            //return DispMotionError(AxisThicknessMeasY, EnumPointX.Idle);
            {
                res = DispMotionError(AxisThicknessMeasY, EnumPointX.Idle);
                if (res != null) return res;
            }
            return null;
        }
        public bool CheckHaveParts()
        {
            return ThicknessDataStations[0].CellData != null || ThicknessDataStations[1].CellData != null || ThicknessDataStations[2].CellData != null;
        }
        DataComp DataNeedComp;
        public ErrorInfoWithPause ActionStartThicknessMeas(DataComp NeedComp, bool CheckVacuum = true)
        {
            DataNeedComp = NeedComp;
            DateTime start = DateTime.Now;
            if (!HavePart()) return null;
            ErrorInfoWithPause res;
            //if (CheckVacuum)
            //{
            //    res = CheckVacuumStatus();
            //    if (res != null) return res;
            //}
            TimeSpan span1 = DateTime.Now - start;
            res = ActionLoad();
            TimeSpan span2 = DateTime.Now - start;
            if (res != null) return res;
            #region 开始实时读取
            //if (ThicknessDataStations[0].CellData != null)
            //{
            //    SerialPort[0].SerialDataReceiveEvent += ClassZone厚度测量_SerialDataReceiveEvent0;
            //    StartSerialRealSend(SerialPort[0], HardwareSerialProtocolName.ThicknessRead);
            //}
            //if (ThicknessDataStations[1].CellData != null)
            //{
            //    SerialPort[1].SerialDataReceiveEvent += ClassZone厚度测量_SerialDataReceiveEvent1;
            //    StartSerialRealSend(SerialPort[1], HardwareSerialProtocolName.ThicknessRead);
            //}
            //if (ThicknessDataStations[2].CellData != null)
            //{
            //    SerialPort[2].SerialDataReceiveEvent += ClassZone厚度测量_SerialDataReceiveEvent2;
            //    StartSerialRealSend(SerialPort[2], HardwareSerialProtocolName.ThicknessRead);
            //}
            #endregion 开始实时读取
            TimeClass.Delay(ClassCommonSetting.SysParam.ThicknessMeasDelayTime);
            //for (int i = 0; i < CELLCOUNT; i++)
            //    if (ThicknessDataStations[i].CellData != null)
            //        GetData((EnumCellIndex)i, SerialPort[i].Data, NeedComp);
            #region 实时读取结束
            //for (int i = 0; i < CELLCOUNT; i++)
            //    if (ThicknessDataStations[i].CellData != null)
            //        StopSerialRealSend(SerialPort[i]);
            //if (ThicknessDataStations[0].CellData != null)
            //    SerialPort[0].SerialDataReceiveEvent -= ClassZone厚度测量_SerialDataReceiveEvent0;
            //if (ThicknessDataStations[1].CellData != null)
            //    SerialPort[1].SerialDataReceiveEvent -= ClassZone厚度测量_SerialDataReceiveEvent1;
            //if (ThicknessDataStations[2].CellData != null)
            //    SerialPort[2].SerialDataReceiveEvent -= ClassZone厚度测量_SerialDataReceiveEvent2;
            #endregion 实时读取结束
            TimeSpan span3 = DateTime.Now - start;
            res = StartThicknessReading(NeedComp);
            TimeSpan span4 = DateTime.Now - start;
            if (res != null) return res;
            res = ActionUnload();
            TimeSpan span5 = DateTime.Now - start;
            TimeUsage.UpdateTimeUsage(TimeUsageItem.ZoneThickness, span5.TotalSeconds,
                string.Format("检查真空: {0:0.00}s\n加载电芯: {1:0.00}s\n延时等待: {2:0.00}s\n读取厚度: {3:0.00}s\n卸载电芯: {4:0.00}s",
                                        span1.TotalSeconds,
                                        (span2 - span1).TotalSeconds,
                                        (span3 - span2).TotalSeconds,
                                        (span4 - span3).TotalSeconds,
                                        (span5 - span4).TotalSeconds));
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                ThicknessCellVacuums[i].SetOutPortStatus(false);
            return res;
        }

        private void ClassZone厚度测量_SerialDataReceiveEvent2(string data)
        {
            GetData((EnumCellIndex)2, data, DataNeedComp);
            AutoPanel.Instance.labelLeftThickness.Text = ThicknessDataStations[2].CellData.Data.CellThickness.Value.ToString("0.000");
        }

        private void ClassZone厚度测量_SerialDataReceiveEvent1(string data)
        {
            GetData((EnumCellIndex)1, data, DataNeedComp);
            AutoPanel.Instance.labelMidThickness.Text = ThicknessDataStations[1].CellData.Data.CellThickness.Value.ToString("0.000");
        }

        private void ClassZone厚度测量_SerialDataReceiveEvent0(string data)
        {
            GetData((EnumCellIndex)0, data, DataNeedComp);
            AutoPanel.Instance.labelRightThickness.Text = ThicknessDataStations[0].CellData.Data.CellThickness.Value.ToString("0.000");
        }

        private bool isCylinderTest = false;
        public ErrorInfoWithPause ActionCylinderTest()
        {

            ErrorInfoWithPause res = null;
            isCylinderTest = !isCylinderTest;
            for (int i = 0; i < CELLCOUNT; i++)
                if (ThicknessDataStations[i].CellData == null)
                    ThicknessDataStations[i].CellData = ClassDataInfo.NewCellData();
            while (isCylinderTest)
            {
                res = AllCyDown();
                if (res != null) { isCylinderTest = false; break; }
                if (!isCylinderTest) break;
                TimeClass.Delay(ClassCommonSetting.SysParam.ThicknessMeasDelayTime);
                if (!isCylinderTest) break;
                res = AllCyUp();
                if (res != null) { isCylinderTest = false; break; }
                if (!isCylinderTest) break;
                TimeClass.Delay(ClassCommonSetting.SysParam.ThicknessMeasDelayTime);
                if (!isCylinderTest) break;
            }
            return res;
        }
        #endregion Action
    }
}