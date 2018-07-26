using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule.Event;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using System.Threading;
using Measure;

namespace CDI.Zone
{
    /// <summary>
    /// 工作区域名称列表
    /// </summary>
    public enum EnumZoneName
    {
        Zone外框架,
        Zone上料传送,
        Zone上料机械手,
        Zone顶封边定位,
        Zone传送机械手,
        Zone厚度测量,
        Zone尺寸测量,
        Zone下料机械手,
        Zone下料传送,
        ZoneNG挑选机械手,
    }
    /// <summary>
    /// 数据传递过程
    /// </summary>
    public enum EnumDataTransfer
    {
        DataReset,
        /// <summary>
        /// 上料传送区域加载新数据。加载物料后执行
        /// </summary>
        LoadNewData,
        /// <summary>
        /// 上料机械手区域PNP取料。取料前执行
        /// </summary>
        LoadPNPPick,
        /// <summary>
        /// 上料机械手区域PNP放料。放料后执行
        /// </summary>
        LoadPNPPlace,
        /// <summary>
        /// 上料机械手区域PNP放NG料。放NG料后执行
        /// </summary>
        LoadPNPPlaceNG,
        /// <summary>
        /// 传送机械手区域取料。取料前执行
        /// </summary>
        TransPNPPick,
        /// <summary>
        /// 传送机械手区域放料。放料后执行
        /// </summary>
        TransPNPPlace,
        /// <summary>
        /// 下料机械手区域取料。取料前执行
        /// </summary>
        UnloadPNPPick,
        /// <summary>
        /// 下料机械手区域放料。放料后执行
        /// </summary>
        UnloadPNPPlace,
        /// <summary>
        /// 电芯移到下料传送区域下料。下料后执行
        /// </summary>
        UnloadOutShiftOut,
        /// <summary>
        /// 下料传送区域下料。NG分拣下料后执行
        /// </summary>
        UnloadOutFinishData,
        /// <summary>
        /// NG挑选机械手区域取料。取料前执行
        /// </summary>
        SortingPNPPick,
        /// <summary>
        /// NG挑选机械手区域放料。放料后执行
        /// </summary>
        SortingPNPPlace,
    }
    public class DataSPCInfo
    {
        public DateTime time;
        public int count;
        public DataSPCInfo(DateTime tm, int c)
        {
            time = tm;
            count = c;
        }
    }
    public class DataSPC
    {
        private DataSPCInfo[] dataRecord = new DataSPCInfo[100];
        private double _ppm;
        public double PPM { get { return _ppm; } }
        private double _lastppm;
        public double LastPPM { get { return _lastppm; } }
        private long _count = 0;
        private DateTime _start;
        private DateTime _lasttime;
        private DateTime _end;
        private TimeSpan _span, _singlespan;
        private List<IPPMUpdate> _guis = new List<IPPMUpdate>();
        public void AddGUI(IPPMUpdate gui)
        {
            if (_guis.Contains(gui)) return;
            _guis.Add(gui);
        }
        public void ResetPPM()
        {
            _count = 0;
            dataRecord = new DataSPCInfo[100];
            UpdatePPM(0);
        }
        long firstCount;
        public void UpdatePPM(int AddCount)
        {
            if (_count == 0)
            {
                firstCount = AddCount;
                _start = DateTime.Now;
                _lasttime = _start;
                _ppm = 0;
            }
            _count += AddCount;
            _end = DateTime.Now;
            _span = _end - _start;
            _singlespan = _end - _lasttime;
            _lasttime = _end;
            for (int i = dataRecord.Length - 1; i > 0; i--)
                dataRecord[i] = dataRecord[i - 1];
            dataRecord[0] = new DataSPCInfo(_end, AddCount);
            CalcPPM();
            CalcLastPPM(3);
            if (_ppm > 0)
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", string.Format("Count: {0} PPM: {1} Last PPM: {2} Cycle Time: {3}ms Total Time: {4}", _count, _ppm, _lastppm, _singlespan.TotalMilliseconds, _span));
            foreach(IPPMUpdate gui in _guis)
                gui.ShowPPM(_count, _ppm, _lastppm, _span, _singlespan);
        }
        private void CalcPPM()
        {
            if (_span.TotalMilliseconds > 0)
                _ppm = (_count - firstCount) / _span.TotalMilliseconds * 60000;
            else
                _ppm = 0;
        }
        private void CalcLastPPM(int LastMinites = 1)
        {
            DateTime LastTime = _end.AddMinutes(-LastMinites);
            int count = 0;
            if (dataRecord.Length > 0)
                for (int i = 1; i < dataRecord.Length; i++)
                {
                    if (dataRecord[i] != null && dataRecord[i].time > LastTime)
                        count += dataRecord[i].count;
                    else
                        break;
                }
            _lastppm = (count / LastMinites)+1;
        }
    }
    /// <summary>
    /// WorkZone集合
    /// </summary>
    public class ClassWorkZones : SystemSubscriber
    {
        public const string ColIndex = "序号";
        public const string ColTime = "时间";
        public const string ColBarcode = "条码";
        public static string ColThickness = EnumDataName.厚度.ToString();
        public static string ColCellWidth = EnumDataName.宽度.ToString();
        public static string ColCellLength = EnumDataName.长度.ToString();
        public static string ColAlTabDist = EnumDataName.AlTab边距.ToString();
        public static string ColNiTabDist = EnumDataName.NiTab边距.ToString();
        public static string ColAlTabDistMax = EnumDataName.AlTab最大边距.ToString();
        public static string ColNiTabDistMax = EnumDataName.NiTab最大边距.ToString();
        public static string ColAlTabLen = EnumDataName.AlTab长度.ToString();
        public static string ColNiTabLen = EnumDataName.NiTab长度.ToString();
        public static string ColTabDist = EnumDataName.Tab间距.ToString();
        public static string ColAlSealantHi = EnumDataName.AlSealant高度.ToString();
        public static string ColNiSealantHi = EnumDataName.NiSealant高度.ToString();
        public static string ColShoulderWidth = EnumDataName.肩宽.ToString();
        public const string ColResult = "结果";
        public static string ColNGItem = "NG项";

        #region Event
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            SPC.ResetPPM();
        }
        #endregion Event
        public DataTable CellDT = new DataTable();
        public DataTable GRRDT = new DataTable();
        private DataGridView _dgv;
        private DataGridView _dgvGrr;
        public DataGridView CellDTViewer
        {
            get { return _dgv; }
            set
            {
                _dgv = value;
                _dgv.DataSource = CellDT;
            }
        }
        public DataGridView GRRDTViewer
        {
            get { return _dgvGrr; }
            set
            {
                _dgvGrr = value;
                _dgvGrr.DataSource = GRRDT;
            }
        }
        public DataSPC SPC = new DataSPC();
        private static ClassWorkZones _zones = null;
        public static ClassWorkZones Instance
        {
            get
            {
                if (_zones == null) _zones = new ClassWorkZones();
                return _zones;
            }
        }
        private Dictionary<EnumZoneName, ClassBaseWorkZone> _zoneList = new Dictionary<EnumZoneName, ClassBaseWorkZone>();
        public ClassZone外框架 WorkZone外框架
        {
            get { return (ClassZone外框架)_zoneList[EnumZoneName.Zone外框架]; }
        }
        public ClassZone上料传送 WorkZone上料传送
        {
            get { return (ClassZone上料传送)_zoneList[EnumZoneName.Zone上料传送]; }
        }
        public ClassZone上料机械手 WorkZone上料机械手
        {
            get { return (ClassZone上料机械手)_zoneList[EnumZoneName.Zone上料机械手]; }
        }
        public ClassZone尺寸测量 WorkZone尺寸测量
        {
            get { return (ClassZone尺寸测量)_zoneList[EnumZoneName.Zone尺寸测量]; }
        }
        public ClassZoneNG挑选机械手 WorkZoneNG挑选机械手
        {
            get { return (ClassZoneNG挑选机械手)_zoneList[EnumZoneName.ZoneNG挑选机械手]; }
        }
        public ClassZone厚度测量 WorkZone厚度测量
        {
            get { return (ClassZone厚度测量)_zoneList[EnumZoneName.Zone厚度测量]; }
        }
        public ClassZone顶封边定位 WorkZone顶封边定位
        {
            get { return (ClassZone顶封边定位)_zoneList[EnumZoneName.Zone顶封边定位]; }
        }
        public ClassZone传送机械手 WorkZone传送机械手
        {
            get { return (ClassZone传送机械手)_zoneList[EnumZoneName.Zone传送机械手]; }
        }
        public ClassZone下料传送 WorkZone下料传送
        {
            get { return (ClassZone下料传送)_zoneList[EnumZoneName.Zone下料传送]; }
        }
        public ClassZone下料机械手 WorkZone下料机械手
        {
            get { return (ClassZone下料机械手)_zoneList[EnumZoneName.Zone下料机械手]; }
        }
        private MotorSafetyCheck _safetycheck;
        public ClassWorkZones()
        {
            foreach (EnumZoneName zonename in Enum.GetValues(typeof(EnumZoneName)))
                _zoneList.Add(zonename, (ClassBaseWorkZone)Activator.CreateInstance(Type.GetType("CDI.Zone.Class" + zonename.ToString())));

            CellDT.Columns.Add(ColIndex);
            CellDT.Columns.Add(ColTime);
            CellDT.Columns.Add(ColBarcode);
            CellDT.Columns.Add(ColThickness);
            CellDT.Columns.Add(ColCellWidth);
            CellDT.Columns.Add(ColCellLength);
            CellDT.Columns.Add(ColAlTabDist);
            CellDT.Columns.Add(ColNiTabDist);
            CellDT.Columns.Add(ColAlTabDistMax);
            CellDT.Columns.Add(ColNiTabDistMax);
            CellDT.Columns.Add(ColAlTabLen);
            CellDT.Columns.Add(ColNiTabLen);
            CellDT.Columns.Add(ColTabDist);
            CellDT.Columns.Add(ColAlSealantHi);
            CellDT.Columns.Add(ColNiSealantHi);
            CellDT.Columns.Add(ColShoulderWidth);
            CellDT.Columns.Add(ColResult);
            CellDT.Columns.Add(ColNGItem);

            GRRDT.Columns.Add(ColIndex);
            GRRDT.Columns.Add(ColTime);
            GRRDT.Columns.Add(ColBarcode);
            GRRDT.Columns.Add(ColThickness);
            GRRDT.Columns.Add(ColCellWidth);
            GRRDT.Columns.Add(ColCellLength);
            GRRDT.Columns.Add(ColAlTabDist);
            GRRDT.Columns.Add(ColNiTabDist);
            GRRDT.Columns.Add(ColAlTabDistMax);
            GRRDT.Columns.Add(ColNiTabDistMax);
            GRRDT.Columns.Add(ColAlTabLen);
            GRRDT.Columns.Add(ColNiTabLen);
            GRRDT.Columns.Add(ColTabDist);
            GRRDT.Columns.Add(ColAlSealantHi);
            GRRDT.Columns.Add(ColNiSealantHi);
            GRRDT.Columns.Add(ColShoulderWidth);

            _dispData = DisplayCCDData;
        }
        /// <summary>
        /// 初始化各WorkZone。
        /// </summary>
        public void AllZoneInit()
        {
            foreach (ClassBaseWorkZone zone in _zoneList.Values)
                zone.ZoneInit();
            _safetycheck = new MotorSafetyCheck(this);
            _safetycheck.AssignMotorSafety();
            WorkZone尺寸测量.DisplayPicData = DisplayCCDData;
            ClassCommonSetting.CheckGeneralAir = WorkZone外框架.CheckGeneralAir;
        }
        public void AddTimeUsage(ITimeUsage gui)
        {
            foreach (ClassBaseWorkZone zone in _zoneList.Values)
                zone.TimeUsage.AddTimeUsageGUI(gui);
        }
        public void RemoveTimeUsage(ITimeUsage gui)
        {
            foreach (ClassBaseWorkZone zone in _zoneList.Values)
                zone.TimeUsage.RemoveTimeUsageGUI(gui);
        }
        public ClassBaseWorkZone[] ToArray()
        {
            return _zoneList.Values.ToArray();
        }
        private delegate void UpdateDataStatusHandler(EnumDataTransfer transfer);
        private event UpdateDataStatusHandler UpdateEvent;
        private void UpdateEventAsyncReturn(IAsyncResult result)
        {
            UpdateDataStatusHandler handler = (UpdateDataStatusHandler)result.AsyncState;
            try
            {
                handler.EndInvoke(result);
                result.AsyncWaitHandle.Close();
            }
            catch (Exception e)
            {
                ClassCommonSetting.ThrowException(handler, "Update", e);
            }
        }
        private void notifyUpdateStatusEventSubscribers(EnumDataTransfer transfer)
        {
            if (UpdateEvent != null)
                foreach (UpdateDataStatusHandler handler in UpdateEvent.GetInvocationList())
                    handler.BeginInvoke(transfer, UpdateEventAsyncReturn, handler);
        }
        private List<IDataStatusGUI> _guis = new List<IDataStatusGUI>();
        public void AddStatusGUI(IDataStatusGUI gui)
        {
            if (gui == null) return;
            if (_guis.Contains(gui)) return;
            _guis.Add(gui);
            UpdateEvent -= gui.UpdateWorkFlowDataStatus;
            UpdateEvent += gui.UpdateWorkFlowDataStatus;
        }
        public void RemoveGUI(IDataStatusGUI gui)
        {
            if (gui == null) return;
            if (!_guis.Contains(gui)) return;
            UpdateEvent -= gui.UpdateWorkFlowDataStatus;
            _guis.Remove(gui);
        }
        private object displock = new object();
        private void UpdateZoneDataStatus(EnumDataTransfer transfer)
        {
            lock (displock)
            {
                switch (transfer)
                {
                    case EnumDataTransfer.LoadNewData:
                        for (int i = 0; i < WorkZone上料传送.LoadInDataStations.Length - 1; i++)
                            WorkZone上料传送.LoadInDataStations[i].TransferFrom(WorkZone上料传送.LoadInDataStations[i + 1]);
                        //WorkZone上料传送.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.LoadPNPPick:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                            WorkZone上料机械手.LoadPNPDataStations[i].TransferFrom(WorkZone上料传送.LoadInDataStations[i]);
                        //WorkZone上料传送.DoRefreshCellData();
                        //WorkZone上料机械手.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.LoadPNPPlace:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZone顶封边定位.TopAlignDataStations[i].CellData = null;
                            if (WorkZone上料机械手.LoadPNPDataStations[i].CellData != null && !WorkZone上料机械手.LoadPNPDataStations[i].CellData.LoadNG)
                                WorkZone顶封边定位.TopAlignDataStations[i].TransferFrom(WorkZone上料机械手.LoadPNPDataStations[i]);
                        }
                        //WorkZone上料机械手.DoRefreshCellData();
                        //WorkZone顶封边定位.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.LoadPNPPlaceNG:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZone上料机械手.NGBoxDatas[i] = WorkZone上料机械手.LoadPNPDataStations[i].CellData;
                            WorkZone上料机械手.LoadPNPDataStations[i].CellData = null;
                        }
                        //WorkZone上料机械手.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.SortingPNPPick:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            if (WorkZone下料传送.UnloadOutDataStations[i].CellData != null) WorkZone下料传送.UnloadOutDataStations[i].CellData.UnloadSorted = true;
                            WorkZoneNG挑选机械手.SortNGDataStations[i].CellData = null;
                            if (WorkZone下料传送.UnloadOutDataStations[i].CellData != null && ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[i].CellData))
                                WorkZoneNG挑选机械手.SortNGDataStations[i].TransferFrom(WorkZone下料传送.UnloadOutDataStations[i]);
                        }
                        //WorkZone下料传送.DoRefreshCellData();
                        //WorkZoneNG挑选机械手.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.SortingPNPPlace:
                        //string NGData;
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZoneNG挑选机械手.NGBoxDatas[WorkZoneNG挑选机械手.CurrentNGBoxRow, i] = WorkZoneNG挑选机械手.SortNGDataStations[i].CellData;
                            WorkZoneNG挑选机械手.SortNGDataStations[i].CellData = null;
                        }
                        //WorkZoneNG挑选机械手.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.TransPNPPick:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZone传送机械手.TransLoadDataStations[i].TransferFrom(WorkZone顶封边定位.TopAlignDataStations[i]);
                            WorkZone传送机械手.TransUnloadDataStations[i].TransferFrom(WorkZone厚度测量.ThicknessDataStations[i]);
                        }
                        //WorkZone传送机械手.DoRefreshCellData();
                        //WorkZone顶封边定位.DoRefreshCellData();
                        //WorkZone厚度测量.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.TransPNPPlace:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZone厚度测量.ThicknessDataStations[i].TransferFrom(WorkZone传送机械手.TransLoadDataStations[i]);
                            WorkZone尺寸测量.CCDMeasDataStations[i].TransferFrom(WorkZone传送机械手.TransUnloadDataStations[i]);
                        }
                        //WorkZone厚度测量.DoRefreshCellData();
                        //WorkZone传送机械手.DoRefreshCellData();
                        //WorkZone尺寸测量.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.UnloadPNPPick:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                            WorkZone下料机械手.UnloadPNPDataStations[i].TransferFrom(WorkZone尺寸测量.CCDMeasDataStations[i]);
                        //WorkZone下料机械手.DoRefreshCellData();
                        //WorkZone尺寸测量.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.UnloadPNPPlace:
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                            WorkZone下料传送.UnloadOutDataStations[3 + i].TransferFrom(WorkZone下料机械手.UnloadPNPDataStations[i]);
                        //WorkZone下料传送.DoRefreshCellData();
                        //WorkZone下料机械手.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.UnloadOutShiftOut:
                        int count = 0;
                        for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        {
                            WorkZone下料传送.UnloadOutDataStations[i].TransferFrom(WorkZone下料传送.UnloadOutDataStations[i + 3]);
                            if (WorkZone下料传送.UnloadOutDataStations[i].CellData != null) count++;
                        }
                        if (count > 0)
                            SPC.UpdatePPM(count);
                        //WorkZone下料传送.DoRefreshCellData();
                        break;
                    case EnumDataTransfer.UnloadOutFinishData:
                        for (int i = 0; i < 3; i++)
                            WorkZone下料传送.UnloadOutDataStations[i].CellData = null;
                        ////WorkZone下料传送.DoRefreshCellData();
                        break;
                }
                notifyUpdateStatusEventSubscribers(transfer);
            }
        }
        public bool ResetAllZones()
        {
            ErrorInfoWithPause res = null;
            bool isOK = true;
            foreach (ClassBaseWorkZone zone in _zoneList.Values)
            {
                res = zone.ResetOutPort();
                if (res != null)
                {
                    ClassErrorHandle.ShowError(zone.Name, res);
                    isOK = false;
                }
                res = zone.ServoOnAllMotor();
                if (res != null)
                {
                    ClassErrorHandle.ShowError(zone.Name, res);
                    isOK = false;
                }
            }
            return isOK && WorkZone外框架.CheckBeforeStart();
        }

        public string DoSystemReset()
        {
            string res;
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "系统复位");
            if (!ResetAllZones())
            {
                res = "重置工作区域错误";
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "系统复位错误: " + res);
                return res;
            }
            WorkZone上料传送.AsyncDoReset();
            WorkZone下料传送.AsyncDoReset();
            WorkZoneNG挑选机械手.AsyncDoReset();
            if (!WorkZone传送机械手.PNPCylinder.SetCylinderState(ClassBaseWorkZone.CYLIND_UP, ClassErrorHandle.TIMEOUT))
            {
                res = "传送机械手气缸上移超时错";
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "系统复位错误: " + res);
                return res;
            }
            WorkZone厚度测量.AsyncDoReset();
            WorkZone尺寸测量.AsyncDoReset();
            WorkZone上料机械手.DoReset();
            WorkZone传送机械手.AsyncDoReset();
            WorkZone下料机械手.AsyncDoReset();
            WorkZone顶封边定位.AsyncDoReset();
            while (WorkZone上料传送.Reseting ||
                WorkZoneNG挑选机械手.Reseting ||
                WorkZone上料机械手.Reseting ||
                WorkZone下料传送.Reseting ||
                WorkZone下料机械手.Reseting ||
                WorkZone传送机械手.Reseting ||
                WorkZone厚度测量.Reseting ||
                WorkZone尺寸测量.Reseting ||
                WorkZone顶封边定位.Reseting)
                Application.DoEvents();
            WorkZoneNG挑选机械手.DoUpdateNGBox();
            WorkZone上料机械手.DoUpdateNGBox();
            res = "";
            foreach (ClassBaseWorkZone zone in this._zoneList.Values)
                if (!zone.ResetOK) res += " " + zone.Name + "(" + zone.ResetErrMessage + ")";
            UpdateZoneDataStatus(EnumDataTransfer.DataReset);
            if (res == "")
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "系统复位成功");
            else
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "系统复位错误: " + res);
            return res;
        }
        #region WorkZoneAction
        public void AfterLoadInLoad()
        {
            UpdateZoneDataStatus(EnumDataTransfer.LoadNewData);
        }
        public void DoLoadInPNPPick()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                EnumAirControl left = WorkZone上料传送.LoadInDataStations[(int)EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                EnumAirControl middle = WorkZone上料传送.LoadInDataStations[(int)EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                EnumAirControl right = WorkZone上料传送.LoadInDataStations[(int)EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                //Open vacuum
                WorkZone上料机械手.AirControl(left, middle, right);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterLoadInPNPPick()
        {
            UpdateZoneDataStatus(EnumDataTransfer.LoadPNPPick);
        }
        public void DoLoadInPNPPlace()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                EnumAirControl left = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.左电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Blow;
                EnumAirControl middle = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.中电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Blow;
                EnumAirControl right = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.右电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Blow;
                //Open blow
                WorkZone上料机械手.AirControl(left, middle, right);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterLoadInPNPPlace()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                EnumAirControl left = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.左电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Close;
                EnumAirControl middle = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.中电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Close;
                EnumAirControl right = WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close : (WorkZone上料机械手.LoadPNPDataStations[EnumCellIndex.右电芯].CellData.LoadNG) ? EnumAirControl.None : EnumAirControl.Close;
                //Close blow
                WorkZone上料机械手.AirControl(left, middle, right);
            }
            UpdateZoneDataStatus(EnumDataTransfer.LoadPNPPlace);
        }
        public void AfterLoadInPNPPlaceNG()
        {
            UpdateZoneDataStatus(EnumDataTransfer.LoadPNPPlaceNG);
        }
        public void DoTransPNPLoad()
        {
            EnumAirControl left, mid, right;
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                //开真空
                left = WorkZone顶封边定位.TopAlignDataStations[EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                mid = WorkZone顶封边定位.TopAlignDataStations[EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                right = WorkZone顶封边定位.TopAlignDataStations[EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                WorkZone传送机械手.AirLoadControl(left, mid, right);
                for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                    WorkZone顶封边定位.TopAlignCellVacuums[i].SetOutPortStatus(false);
                WorkZone顶封边定位.CellBlow.SetOutPortStatus(true);
                left = WorkZone厚度测量.ThicknessDataStations[EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                mid = WorkZone厚度测量.ThicknessDataStations[EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                right = WorkZone厚度测量.ThicknessDataStations[EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close : EnumAirControl.Vacuum;
                WorkZone传送机械手.AirUnloadControl(left, mid, right);
                for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                    WorkZone厚度测量.ThicknessCellVacuums[i].SetOutPortStatus(false);
                WorkZone厚度测量.CellBlow.SetOutPortStatus(true);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterTransPNPLoad()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                WorkZone顶封边定位.CellBlow.SetOutPortStatus(false);
                WorkZone厚度测量.CellBlow.SetOutPortStatus(false);
            }
            UpdateZoneDataStatus(EnumDataTransfer.TransPNPPick);
        }
        public void DoTransPNPUnload()
        {
            //bool left, mid, right;
            //Close vacuum and open blow
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                WorkZone传送机械手.AirLoadControl(EnumAirControl.Close, EnumAirControl.Close, EnumAirControl.Close);
                WorkZone传送机械手.AirUnloadControl(EnumAirControl.Close, EnumAirControl.Close, EnumAirControl.Close);
                WorkZone传送机械手.LoadCellBlow.SetOutPortStatus(true);
                WorkZone传送机械手.UnloadCellBlow.SetOutPortStatus(true);

                if (WorkZone传送机械手.TransLoadDataStations[EnumCellIndex.左电芯].CellData != null)
                    WorkZone厚度测量.ThicknessCellVacuums[EnumCellIndex.左电芯].SetOutPortStatus(true);
                if (WorkZone传送机械手.TransLoadDataStations[EnumCellIndex.中电芯].CellData != null)
                    WorkZone厚度测量.ThicknessCellVacuums[EnumCellIndex.中电芯].SetOutPortStatus(true);
                if (WorkZone传送机械手.TransLoadDataStations[EnumCellIndex.右电芯].CellData != null)
                    WorkZone厚度测量.ThicknessCellVacuums[EnumCellIndex.右电芯].SetOutPortStatus(true);
                if (WorkZone传送机械手.TransUnloadDataStations[EnumCellIndex.左电芯].CellData != null)
                    WorkZone尺寸测量.CellVacuums[EnumCellIndex.左电芯].SetOutPortStatus(true);
                if (WorkZone传送机械手.TransUnloadDataStations[EnumCellIndex.中电芯].CellData != null)
                    WorkZone尺寸测量.CellVacuums[EnumCellIndex.中电芯].SetOutPortStatus(true);
                if (WorkZone传送机械手.TransUnloadDataStations[EnumCellIndex.右电芯].CellData != null)
                    WorkZone尺寸测量.CellVacuums[EnumCellIndex.右电芯].SetOutPortStatus(true);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterTransPNPUnload()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                WorkZone传送机械手.LoadCellBlow.SetOutPortStatus(false);
                WorkZone传送机械手.UnloadCellBlow.SetOutPortStatus(false);
            }
            UpdateZoneDataStatus(EnumDataTransfer.TransPNPPlace);
        }
        private DisplayCellData _dispData;
        private object displayCCDDataLock = new object();
        public static void DisplayDataAsyncReturn(IAsyncResult result)
        {
            DisplayCellData handler = (DisplayCellData)result.AsyncState;
            try
            {
                handler.EndInvoke(result);
                result.AsyncWaitHandle.Close();
            }
            catch (Exception e)
            {
                ClassCommonSetting.ThrowException(handler, "DisplayMainData", e);
            }
        }
        private void SetCellStyle(Cst.Struct_DataInfo data, DataGridView dgv, int row, string colname)
        {
            if (data.CheckNGDisable)
            {
                dgv.Rows[row].Cells[colname].Style.ForeColor = Color.Yellow;
                dgv.Rows[row].Cells[colname].Style.BackColor = Color.RoyalBlue;
            }
            else if (data.DataNG)
            {
                dgv.Rows[row].Cells[colname].Style.BackColor = Color.Red;
                dgv.Rows[row].Cells[colname].Style.ForeColor = Color.Yellow;
            }
        }

        private int ShowDataNum = 100;
        private void DisplayCCDData(ClassDataInfo CellData)
        {
            lock (displayCCDDataLock)
            {
                DataRow newrow;
                newrow = CellDT.NewRow();
                newrow[ColIndex] = CellData.Index.ToString();
                newrow[ColTime] = DateTime.Now.ToString();
                newrow[ColBarcode] = CellData.Barcode;
                newrow[ColThickness] = CellData.Data.CellThickness.Value.ToString("0.000");
                newrow[ColCellWidth] = CellData.Data.CellWidth.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.CellWidth.Value.ToString("0.000");
                newrow[ColCellLength] = CellData.Data.CellLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.CellLength.Value.ToString("0.000");
                newrow[ColAlTabDist] = CellData.Data.AlTabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabDistance.Value.ToString("0.000");
                newrow[ColNiTabDist] = CellData.Data.NiTabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabDistance.Value.ToString("0.000");
                newrow[ColAlTabDistMax] = CellData.Data.AlTabDistanceMax.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabDistanceMax.Value.ToString("0.000");
                newrow[ColNiTabDistMax] = CellData.Data.NiTabDistanceMax.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabDistanceMax.Value.ToString("0.000");
                newrow[ColAlTabLen] = CellData.Data.AlTabLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabLength.Value.ToString("0.000");
                newrow[ColNiTabLen] = CellData.Data.NiTabLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabLength.Value.ToString("0.000");
                newrow[ColTabDist] = CellData.Data.TabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.TabDistance.Value.ToString("0.000");
                newrow[ColAlSealantHi] = CellData.Data.AlSealantHeight.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlSealantHeight.Value.ToString("0.000");
                newrow[ColNiSealantHi] = CellData.Data.NiSealantHeight.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiSealantHeight.Value.ToString("0.000");
                newrow[ColShoulderWidth] = CellData.Data.ShoulderWidth.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.ShoulderWidth.Value.ToString("0.000");
                newrow[ColResult] = CellData.DataNG ? "NG" : "OK";
                newrow[ColNGItem] = CellData.NGItem;

                if (ClassWorkFlow.Instance.IsRunning && CellData.Index > 0)
                    if (ClassCommonSetting.autoLine.BIS_TransfMylarData(
                        CellData.Barcode,                                       // <param name="CELL_NAME">电芯条码</param>
                        CellData.Data.CellThickness.Value.ToString("0.000"),    // <param name="BARCODE_THICK">电芯厚度</param>
                        CellData.Data.CellLength.Value.ToString("0.000"),       // <param name="BARCODE_LENGTH">电芯长度</param>
                        CellData.Data.CellWidth.Value.ToString("0.000"),        // <param name="BARCODE_WIDTH">电芯宽度</param>
                        "0",                                                    // <param name="TOP_EDGE_WIDTH">顶封边宽</param>
                        "0",                                                    // <param name="SEALANT_HEIGHT">sealant高度</param>
                        CellData.Data.NiTabDistance.Value.ToString("0.000"),    // <param name="NITAB_LENGTH">Ni Tab边距</param>
                        CellData.Data.AlTabDistance.Value.ToString("0.000"),    // <param name="ALTAB_LENGTH">Al Tab边距</param>
                        CellData.Data.TabDistance.Value.ToString("0.000"),      // <param name="MID_LENGTH">电芯中心距</param>
                        Environment.MachineName,                                // <param name="MACHINE_NO">上传机器</param>
                        CellData.Data.AlTabLength.Value.ToString("0.000"),      // <param name="AITAB_HEIGHT">Ai Tab高度</param>
                        CellData.Data.NiTabLength.Value.ToString("0.000"),      // <param name="NITAB_HEIGHT">Ni Tab高度</param>
                        CellData.Data.AlSealantHeight.Value.ToString("0.000"),  // <param name="AISEA_HEIGHT">Ai sealant高度</param>
                        CellData.Data.NiSealantHeight.Value.ToString("0.000"),  // <param name="NISEA_HEIGHT">Ni sealant高度</param>
                        "0",                                                    // <param name="TOP_SHOULDER_WIDTH">头部肩宽</param>
                        CellData.DataNG ? "NG" : "OK",                          // <param name="FLAG">OK/NG标识</param>
                        ""                                                      // <param name="REMARK">备注参数</param>
                        ) != 1)
                        ClassErrorHandle.ShowError("上传BIS", "BIS数据上传失败，检查网络连接或AutoLineInterface.dll.config中的IP设置。", ErrorLevel.Alarm);
                BaseForm.DoInvokeRequired(_dgv, () =>
                {
                    CellDT.Rows.Add(newrow);
                    if (CellDT.Rows.Count > ShowDataNum)
                    {
                        CellDT.Rows.RemoveAt(0);
                    }
                    SetCellStyle(CellData.Data.CellThickness, _dgv, CellDT.Rows.Count - 1, ColThickness);
                    SetCellStyle(CellData.Data.CellWidth, _dgv, CellDT.Rows.Count - 1, ColCellWidth);
                    SetCellStyle(CellData.Data.CellLength, _dgv, CellDT.Rows.Count - 1, ColCellLength);
                    SetCellStyle(CellData.Data.AlTabDistance, _dgv, CellDT.Rows.Count - 1, ColAlTabDist);
                    SetCellStyle(CellData.Data.NiTabDistance, _dgv, CellDT.Rows.Count - 1, ColNiTabDist);
                    SetCellStyle(CellData.Data.AlTabDistanceMax, _dgv, CellDT.Rows.Count - 1, ColAlTabDistMax);
                    SetCellStyle(CellData.Data.NiTabDistanceMax, _dgv, CellDT.Rows.Count - 1, ColNiTabDistMax);
                    SetCellStyle(CellData.Data.AlTabLength, _dgv, CellDT.Rows.Count - 1, ColAlTabLen);
                    SetCellStyle(CellData.Data.NiTabLength, _dgv, CellDT.Rows.Count - 1, ColNiTabLen);
                    SetCellStyle(CellData.Data.TabDistance, _dgv, CellDT.Rows.Count - 1, ColTabDist);
                    SetCellStyle(CellData.Data.AlSealantHeight, _dgv, CellDT.Rows.Count - 1, ColAlSealantHi);
                    SetCellStyle(CellData.Data.NiSealantHeight, _dgv, CellDT.Rows.Count - 1, ColNiSealantHi);
                    SetCellStyle(CellData.Data.ShoulderWidth, _dgv, CellDT.Rows.Count - 1, ColShoulderWidth);
                });
                if (ClassWorkFlow.Instance.IsGRR)
                {
                    newrow = GRRDT.NewRow();
                    newrow[ColIndex] = (GRRDT.Rows.Count + 1).ToString();
                    newrow[ColTime] = DateTime.Now.ToString();
                    newrow[ColBarcode] = CellData.Barcode;
                    newrow[ColThickness] = CellData.Data.CellThickness.Value.ToString("0.000");
                    newrow[ColCellWidth] = CellData.Data.CellWidth.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.CellWidth.Value.ToString("0.000");
                    newrow[ColCellLength] = CellData.Data.CellLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.CellLength.Value.ToString("0.000");
                    newrow[ColAlTabDist] = CellData.Data.AlTabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabDistance.Value.ToString("0.000");
                    newrow[ColNiTabDist] = CellData.Data.NiTabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabDistance.Value.ToString("0.000");
                    newrow[ColAlTabDistMax] = CellData.Data.AlTabDistanceMax.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabDistanceMax.Value.ToString("0.000");
                    newrow[ColNiTabDistMax] = CellData.Data.NiTabDistanceMax.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabDistanceMax.Value.ToString("0.000");
                    newrow[ColAlTabLen] = CellData.Data.AlTabLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlTabLength.Value.ToString("0.000");
                    newrow[ColNiTabLen] = CellData.Data.NiTabLength.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiTabLength.Value.ToString("0.000");
                    newrow[ColTabDist] = CellData.Data.TabDistance.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.TabDistance.Value.ToString("0.000");
                    newrow[ColAlSealantHi] = CellData.Data.AlSealantHeight.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.AlSealantHeight.Value.ToString("0.000");
                    newrow[ColNiSealantHi] = CellData.Data.NiSealantHeight.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.NiSealantHeight.Value.ToString("0.000");
                    newrow[ColShoulderWidth] = CellData.Data.ShoulderWidth.Value == Cst.Struct_DataInfo.NaN ? Cst.Struct_DataInfo.NaN.ToString() : CellData.Data.ShoulderWidth.Value.ToString("0.000");
                    BaseForm.DoInvokeRequired(_dgv, () => GRRDT.Rows.Add(newrow));
                }
            }
        }
        private enum StationIndex
        {
            左 = 2,
            中 = 1,
            右 = 0,
        }
        public void SaveMeasData()
        {
            DateTime savetime = DateTime.Now;
            DateTime oldOdsSaveTime = savetime - new TimeSpan(2, 0, 0);
            int saveHour = savetime.Hour / 2 * 2;
            int oldsaveHour = oldOdsSaveTime.Hour / 2 * 2;
            string oldtempFile = ClassCommonSetting.SysParam.DataSavePath + string.Format("OdsData {0:yyyyMMdd} {1}-{2}.csv", oldOdsSaveTime, oldsaveHour, oldsaveHour + 1);
            string OdsDataFile = ClassCommonSetting.SysParam.OdsSavePath + string.Format("OdsData {0:yyyyMMdd} {1}-{2}.csv", oldOdsSaveTime, oldsaveHour, oldsaveHour + 1);
            string datafile = ClassCommonSetting.SysParam.DataSavePath + string.Format("ProdData {0:yyyyMMdd}.csv", savetime.Date);
            string tempFile = ClassCommonSetting.SysParam.DataSavePath + string.Format("OdsData {0:yyyyMMdd} {1}-{2}.csv", savetime, saveHour, saveHour + 1);
            if (!Directory.Exists(ClassCommonSetting.SysParam.DataSavePath))
                Directory.CreateDirectory(ClassCommonSetting.SysParam.DataSavePath);
            bool DatafileExist = File.Exists(datafile);
            bool TempfileExist = File.Exists(tempFile);
            if (ClassCommonSetting.SysParam.OdsSavePath != null && ClassCommonSetting.SysParam.OdsSavePath != "")
                if (File.Exists(oldtempFile)) File.Move(oldtempFile, OdsDataFile);
            string data;
            StreamWriter swData = new StreamWriter(datafile, true, System.Text.Encoding.UTF8);
            StreamWriter swTemp = new StreamWriter(tempFile, true, System.Text.Encoding.UTF8);
            data = string.Format("{0}表示CCD抓边失败，请检查来料是否Tab等存在不良！", Cst.Struct_DataInfo.NaN) + Environment.NewLine;
            data += "二维码,";
            data += "工站,";
            data += "结果,";
            data += "时间,";
            data += EnumDataName.厚度.ToString() + ",";
            data += EnumDataName.宽度.ToString() + ",";
            data += EnumDataName.肩宽.ToString() + ",";
            data += EnumDataName.长度.ToString() + ",";
            data += EnumDataName.AlTab边距.ToString() + ",";
            data += EnumDataName.AlTab最大边距.ToString() + ",";
            data += EnumDataName.NiTab边距.ToString() + ",";
            data += EnumDataName.NiTab最大边距.ToString() + ",";
            data += EnumDataName.AlTab长度.ToString() + ",";
            data += EnumDataName.NiTab长度.ToString() + ",";
            data += EnumDataName.Tab间距.ToString() + ",";
            data += EnumDataName.AlSealant高度.ToString() + ",";
            data += EnumDataName.NiSealant高度.ToString() + ",";
            data += "NG项";
            if (!DatafileExist) swData.WriteLine(data);
            if (!TempfileExist) swTemp.WriteLine(data);
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
            {
                data = "";
                if (WorkZone尺寸测量.BufferDatas[i] == null) WorkZone尺寸测量.BufferDatas[i] = WorkZone尺寸测量.CCDMeasDataStations[i].CellData;
                if (WorkZone尺寸测量.BufferDatas[i] != null)
                {
                    //DisplayCCDData(WorkZone尺寸测量.BufferDatas[i]);
                    _dispData.BeginInvoke(WorkZone尺寸测量.BufferDatas[i], DisplayDataAsyncReturn, _dispData);
                    if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                    {
                        data += WorkZone尺寸测量.BufferDatas[i].Barcode + ",";
                        data += (StationIndex)i + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].DataNG ? "NG," : "OK,";
                        data += DateTime.Now.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.CellThickness.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.CellWidth.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.ShoulderWidth.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.CellLength.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.AlTabDistance.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.AlTabDistanceMax.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.NiTabDistance.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.NiTabDistanceMax.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.AlTabLength.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.NiTabLength.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.TabDistance.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.AlSealantHeight.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.NiSealantHeight.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].Data.ShoulderWidth.Value.ToString() + ",";
                        data += WorkZone尺寸测量.BufferDatas[i].NGItem;
                        swData.WriteLine(data);
                        swTemp.WriteLine(data);
                        WorkZone尺寸测量.BufferDatas[i] = null;
                    }
                }
            }
            swData.Close();
            swTemp.Close();
        }
        public void DoLoadOutPNPPick()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                bool left = WorkZone尺寸测量.CCDMeasDataStations[EnumCellIndex.左电芯].CellData != null;
                bool middle = WorkZone尺寸测量.CCDMeasDataStations[EnumCellIndex.中电芯].CellData != null;
                bool right = WorkZone尺寸测量.CCDMeasDataStations[EnumCellIndex.右电芯].CellData != null;
                //Open vacuum
                WorkZone下料机械手.CellBlow.SetOutPortStatus(false);
                WorkZone下料机械手.CellVacuums[EnumCellIndex.左电芯].SetUnitStatus(left);
                WorkZone下料机械手.CellVacuums[EnumCellIndex.中电芯].SetUnitStatus(middle);
                WorkZone下料机械手.CellVacuums[EnumCellIndex.右电芯].SetUnitStatus(right);
                for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                    WorkZone尺寸测量.CellVacuums[i].SetOutPortStatus(false);
                WorkZone尺寸测量.CellBlow.SetOutPortStatus(true);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterLoadOutPNPPick()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑) WorkZone尺寸测量.CellBlow.SetOutPortStatus(false);
            UpdateZoneDataStatus(EnumDataTransfer.UnloadPNPPick);
        }
        public void DoLoadOutPNPPlace()
        {
            //Open blow
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                    WorkZone下料机械手.CellVacuums[i].SetUnitStatus(false);
                WorkZone下料机械手.CellBlow.SetOutPortStatus(true);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterLoadOutPNPPlace()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑) WorkZone下料机械手.CellBlow.SetOutPortStatus(false);
            UpdateZoneDataStatus(EnumDataTransfer.UnloadPNPPlace);
        }
        public void DoSortPNPPick()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                EnumAirControl left = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData == null ? EnumAirControl.Close :
                    (ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Blow);
                EnumAirControl middle = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData == null ? EnumAirControl.Close :
                    (ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Blow);
                EnumAirControl right = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData == null ? EnumAirControl.Close :
                    (ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Blow);
                //Open vacuum
                WorkZoneNG挑选机械手.AirControl(left, middle, right);
            }
            //Delay
            Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
        }
        public void AfterSortPNPPick()
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                EnumAirControl left = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData != null && ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Close;
                EnumAirControl middle = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData != null && ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Close;
                EnumAirControl right = WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData != null && ClassBaseWorkZone.CheckCellIsNG(WorkZone下料传送.UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData)
                    ? EnumAirControl.Vacuum : EnumAirControl.Close;
                //Open vacuum
                WorkZoneNG挑选机械手.AirControl(left, middle, right);
            }
            UpdateZoneDataStatus(EnumDataTransfer.SortingPNPPick);
        }
        public void AfterSortPNPPlace()
        {
            UpdateZoneDataStatus(EnumDataTransfer.SortingPNPPlace);
        }
        public void AfterLoadOutShift()
        {
            UpdateZoneDataStatus(EnumDataTransfer.UnloadOutShiftOut);
        }
        public void AfterLoadOut()
        {
            UpdateZoneDataStatus(EnumDataTransfer.UnloadOutFinishData);
        }
        #endregion WorkZoneAction
    }
}