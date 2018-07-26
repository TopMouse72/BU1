using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HardwarePool;
using CDI.GUI;
using Colibri.CommonModule;
using Colibri.CommonModule.ToolBox;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule.Event;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using NetWork;
using Measure;

namespace CDI.Zone
{
    public class ClassZone尺寸测量 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            OutlineMeasX = HardwareAxisName.OutlineMeasX,//CCD尺寸检测X轴
        }
        public enum EnumPointX
        {
            GetPart,
            Start,
            Pitch,
            PartOut,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            OutlineMeasVacSensLeft = HardwareInportName.OutlineMeasVacSensLeft,//CCD尺寸测量左吸头真空
            OutlineMeasVacSensMid = HardwareInportName.OutlineMeasVacSensMid,//CCD尺寸测量中吸头真空
            OutlineMeasVacSensRight = HardwareInportName.OutlineMeasVacSensRight,//CCD尺寸测量右吸头真空
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            OutlineMeasVacLeft = HardwareOutportName.OutlineMeasVacLeft,//CCD尺寸检测左吸头真空
            OutlineMeasVacMid = HardwareOutportName.OutlineMeasVacMid,//CCD尺寸检测中吸头真空
            OutlineMeasVacRight = HardwareOutportName.OutlineMeasVacRight,//CCD尺寸检测右吸头真空
            OutlineMeasBlow = HardwareOutportName.OutlineMeasBlow,//CCD尺寸检测吹气
        }
        public enum EnumSerialPortName
        {
            OutlineMeasLightController = HardwareSerialPortName.OutlineMeasLightController,
        }
        public CameraDispControl CameraDisplay;
        public CellCollection<ClassAirPort> CellVacuums = new CellCollection<ClassAirPort>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public ClassAirPort CellBlow;
        public CAxisBase AxisOutlineMeasX;
        public ClassZone尺寸测量() : base(EnumZoneName.Zone尺寸测量.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                CellVacuums.Add(i, new ClassAirPort());
                CellVacSensor.Add(i, new ClassAirPort());
                CCDMeasDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
            }
            CellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName), typeof(EnumSerialPortName));
            AxisOutlineMeasX = ThisAxis(EnumAxisName.OutlineMeasX);
            AxisOutlineMeasX.AddPoints(typeof(EnumPointX));
            CellVacuums[EnumCellIndex.左电芯].Port = ThisOutport(EnumOutportName.OutlineMeasVacLeft);
            CellVacuums[EnumCellIndex.中电芯].Port = ThisOutport(EnumOutportName.OutlineMeasVacMid);
            CellVacuums[EnumCellIndex.右电芯].Port = ThisOutport(EnumOutportName.OutlineMeasVacRight);
            CellBlow.Port = ThisOutport(EnumOutportName.OutlineMeasBlow);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.OutlineMeasVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.OutlineMeasVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.OutlineMeasVacSensRight);

            ZoneSettingPanel = new SettingPanelZone尺寸测量();
            ZoneManualPanel = new ManualPanelZone尺寸测量();
            for (int i = 0; i <= CELLCOUNT; i++)
                MeasDone[i] = true;
            BufferDatas[CELLCOUNT] = ClassDataInfo.NewCellData();
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            base.ResetOutPort();
            for (int i = 0; i < CellVacuums.Count; i++)
                CellVacuums[i].SetOutPortStatus(false);
            CellBlow.SetOutPortStatus(false);
            return null;
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisOutlineMeasX.ServoOn = true;
            err.CollectErrInfo(MotorReset(AxisOutlineMeasX, EnumPointX.GetPart));
            for (int i = 0; i < CELLCOUNT; i++)
            {
                BufferDatas[i] = null;
                CCDMeasDataStations[i].CellData = null;
                MeasDone[i] = true;
            }
        }
        #region Event
        protected override void InPortActive(string inPort)
        {
        }

        protected override void InPortDeActive(string inPort)
        {
        }
        private bool[] _camTrigFinish = new bool[CELLCOUNT + 1];
        public bool[] MeasDone = new bool[CELLCOUNT + 1];
        private DataComp[] Comps = new DataComp[CELLCOUNT + 1];
        private int GetData(string data)
        {
            string[] datas = data.Split(',');
            int index = int.Parse(datas[0]) - 1;
            string barcode = datas[1].Trim();
            if (index < CELLCOUNT)
            {
                CCDMeasDataStations[index].Refresh();
            }
            if (BufferDatas[index] != null/* && (BufferDatas[index].Barcode == barcode || !ClassWorkFlow.Instance.IsRunning)*/)
            {
                BufferDatas[index].Data.CellWidth = ClassDataInfo.DataSpec.CellWidth;
                BufferDatas[index].Data.CellLength = ClassDataInfo.DataSpec.CellLength;
                BufferDatas[index].Data.AlTabDistance = ClassDataInfo.DataSpec.AlTabDistance;
                BufferDatas[index].Data.NiTabDistance = ClassDataInfo.DataSpec.NiTabDistance;
                BufferDatas[index].Data.AlTabDistanceMax = ClassDataInfo.DataSpec.AlTabDistanceMax;
                BufferDatas[index].Data.NiTabDistanceMax = ClassDataInfo.DataSpec.NiTabDistanceMax;
                BufferDatas[index].Data.AlTabLength = ClassDataInfo.DataSpec.AlTabLength;
                BufferDatas[index].Data.NiTabLength = ClassDataInfo.DataSpec.NiTabLength;
                BufferDatas[index].Data.TabDistance = ClassDataInfo.DataSpec.TabDistance;
                BufferDatas[index].Data.AlSealantHeight = ClassDataInfo.DataSpec.AlSealantHeight;
                BufferDatas[index].Data.NiSealantHeight = ClassDataInfo.DataSpec.NiSealantHeight;
                BufferDatas[index].Data.ShoulderWidth = ClassDataInfo.DataSpec.ShoulderWidth;
                if (index == CELLCOUNT) BufferDatas[index].Barcode = barcode;
                BufferDatas[index].Data.CellWidth.Value = double.Parse(datas[2]);
                BufferDatas[index].Data.CellLength.Value = double.Parse(datas[3]);
                BufferDatas[index].Data.AlTabDistance.Value = double.Parse(datas[4]);
                BufferDatas[index].Data.NiTabDistance.Value = double.Parse(datas[5]);
                BufferDatas[index].Data.AlTabDistanceMax.Value = double.Parse(datas[6]);
                BufferDatas[index].Data.NiTabDistanceMax.Value = double.Parse(datas[7]);
                BufferDatas[index].Data.AlTabLength.Value = double.Parse(datas[8]);
                BufferDatas[index].Data.NiTabLength.Value = double.Parse(datas[9]);
                BufferDatas[index].Data.TabDistance.Value = double.Parse(datas[10]);
                BufferDatas[index].Data.AlSealantHeight.Value = double.Parse(datas[11]);
                BufferDatas[index].Data.NiSealantHeight.Value = double.Parse(datas[12]);
                BufferDatas[index].Data.ShoulderWidth.Value = double.Parse(datas[13]);
                if (index == CELLCOUNT) Comps[index] = DataComp.AddAll;
                if ((Comps[index] & DataComp.AddComp) != DataComp.NoComp && ClassCommonSetting.SysParam.CurrentUsedGauge != null)
                {
                    if (ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Enable)
                    {
                        if (BufferDatas[index].Data.CellLength.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.CellLength.Value = BufferDatas[index].Data.CellLength.Value
                                    * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Slope
                                    + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Intercept;
                        if (BufferDatas[index].Data.AlTabLength.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.AlTabLength.Value = BufferDatas[index].Data.AlTabLength.Value
                                * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Slope
                                + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Intercept;
                        if (BufferDatas[index].Data.NiTabLength.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.NiTabLength.Value = BufferDatas[index].Data.NiTabLength.Value
                                * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Slope
                                + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDXLinear.Intercept;
                    }
                    if (ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Enable)
                    {
                        if (BufferDatas[index].Data.CellWidth.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.CellWidth.Value = BufferDatas[index].Data.CellWidth.Value
                               * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                               + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.TabDistance.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.TabDistance.Value = BufferDatas[index].Data.TabDistance.Value
                               * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                               + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.AlTabDistance.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.AlTabDistance.Value = BufferDatas[index].Data.AlTabDistance.Value
                               * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                               + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.NiTabDistance.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.NiTabDistance.Value = BufferDatas[index].Data.NiTabDistance.Value
                              * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                              + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.AlTabDistanceMax.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.AlTabDistanceMax.Value = BufferDatas[index].Data.AlTabDistanceMax.Value
                               * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                               + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.NiTabDistanceMax.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.NiTabDistanceMax.Value = BufferDatas[index].Data.NiTabDistanceMax.Value
                              * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                              + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                        if (BufferDatas[index].Data.ShoulderWidth.Value != Cst.Struct_DataInfo.NaN)
                            BufferDatas[index].Data.ShoulderWidth.Value = BufferDatas[index].Data.ShoulderWidth.Value
                              * ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Slope
                              + ClassCommonSetting.SysParam.CurrentUsedGauge.CCDYLinear.Intercept;
                    }
                }
                if (index == CELLCOUNT && DisplayPicData != null)
                    DisplayPicData.BeginInvoke(BufferDatas[index], null, null);
            }
            else //if (BufferDatas[index].Barcode == barcode || !ClassWorkFlow.Instance.IsRunning)
                ClassErrorHandle.ShowError(this.Name, ((EnumCellIndex)index).ToString() + "测量数据不符。", ErrorLevel.Error);
            return index;
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            int index;
            switch (e.eventName)
            {
                case WinSock.CAPFINISH:
                    index = int.Parse(e.eventInfo) - 1;
                    _camTrigFinish[index] = true;
                    ClassCommonSetting.ProgramLog(LogFile.Level.Info, this.Name, "相机触发完成：" + (EnumCellIndex)index);
                    break;
                case WinSock.MEASDONE:
                    index = GetData(e.eventInfo);
                    MeasDone[index] = true;
                    ClassCommonSetting.ProgramLog(LogFile.Level.Info, this.Name, "CCD测量完成：" + (EnumCellIndex)index);
                    break;
            }
        }
        #endregion Event
        public DisplayCellData DisplayPicData;
        #region Data
        public CellCollection<ClassDataStation> CCDMeasDataStations = new CellCollection<ClassDataStation>();
        public ClassDataInfo[] BufferDatas = new ClassDataInfo[CELLCOUNT + 1];
        public ClassDataStation MeasTestData = new ClassDataStation("MeasTest");
        public bool isCCDAllFinish
        {
            get { return MeasDone[0] && MeasDone[1] && MeasDone[2]; }
        }
        //private int _CurrentMeasIndex;
        public string GetDataInfoString()
        {
            return GetDataInfoString(CCDMeasDataStations);
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            CCDMeasDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            CCDMeasDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            CCDMeasDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public ErrorInfoWithPause SnapShot(int index, string barcode, string path = "")
        {
            ErrorInfoWithPause res = null;
            _camTrigFinish[index] = false;
            MeasDone[index] = false;
            BufferDatas[index] = CCDMeasDataStations[index].CellData;
            ClassCommonSetting.SocketToAOI.SendCommandCamTrig(index + 1, barcode, path);
            if (!ClassCommonSetting.CheckTimeOut(() => { return _camTrigFinish[index]; }))
                res = new ErrorInfoWithPause("抓取图像超时错。", ErrorLevel.Error);
            return res;
        }
        public ErrorInfoWithPause MeasPicture(bool isStandard, string path = "")
        {
            ErrorInfoWithPause res = null;
            MeasDone[3] = false;
            ClassCommonSetting.SocketToAOI.SendCommandMeas(isStandard, path);
            if (!ClassCommonSetting.CheckTimeOut(() => { return MeasDone[3]; }))
                res = new ErrorInfoWithPause("图像检测超时错。", ErrorLevel.Alarm);
            return res;
        }
        public ErrorInfoWithPause ActionOneCCDMeas(EnumCellIndex cellindex, DataComp NeedComp)
        {
            if (CCDMeasDataStations[cellindex].CellData != null)
            {
                if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                {
                    Comps[(int)cellindex] = NeedComp;
                    SnapShot((int)cellindex, CCDMeasDataStations[cellindex].CellData.Barcode);
                }
                else
                {
                    TimeClass.Delay(400);
                    MeasDone[(int)cellindex] = true;
                }
            }
            return null;
        }
        public ErrorInfoWithPause ActionToGetPart(bool NeedWait = true)
        {
            ErrorInfoWithPause res = null;
            while (!AxisOutlineMeasX.MoveTo(EnumPointX.GetPart, NeedWait))
            //return DispMotionError(AxisOutlineMeasX, EnumPointX.GetPart);
            {
                res = DispMotionError(AxisOutlineMeasX, EnumPointX.GetPart);
                if (res != null) return res;
            }
            return null;
        }
        public ErrorInfoWithPause ActionToUnloadPart()
        {
            ErrorInfoWithPause res = null;
            while (!AxisOutlineMeasX.MoveTo(EnumPointX.PartOut))
            //return DispMotionError(AxisOutlineMeasX, EnumPointX.PartOut);
            {
                res = DispMotionError(AxisOutlineMeasX, EnumPointX.PartOut);
                if (res != null) return res;
            }
            return null;
        }
        public ErrorInfoWithPause ActionToMeasPos(EnumCellIndex cell)
        {
            ErrorInfoWithPause res = null;
            while (!AxisOutlineMeasX.MoveTo(EnumPointX.Start, true, ClassDataInfo.CELLPITCH * (int)cell))
            //return DispMotionError(AxisOutlineMeasX, cell);
            {
                res = DispMotionError(AxisOutlineMeasX, cell);
                if (res != null) return res;
            }
            return null;
        }
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= CCDMeasDataStations[i].CellData != null;
            return need;
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
            CCDMeasDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = CCDMeasDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = CCDMeasDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = CCDMeasDataStations[EnumCellIndex.右电芯].CellData != null;
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
                return new ErrorInfoWithPause("CCD检测真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        private DataComp DataNeedComp;
        public ErrorInfoWithPause ActionStartCCDMeas(DataComp NeedComp)
        {
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
            {
                //Open vacuum
                CellBlow.SetOutPortStatus(false);
                CellVacuums[EnumCellIndex.左电芯].SetOutPortStatus(CCDMeasDataStations[(int)EnumCellIndex.左电芯].CellData != null);
                CellVacuums[EnumCellIndex.中电芯].SetOutPortStatus(CCDMeasDataStations[(int)EnumCellIndex.中电芯].CellData != null);
                CellVacuums[EnumCellIndex.右电芯].SetOutPortStatus(CCDMeasDataStations[(int)EnumCellIndex.右电芯].CellData != null);
            }
            ErrorInfoWithPause res = null;// = CheckVacuumStatus();
            //if (res != null) return res;
            //for (int i = 0; i < CELLCOUNT; i++)
            //{
            //    _camTrigFinish[i] = CCDMeasDataStations[i].CellData == null;
            //    MeasDone[i] = CCDMeasDataStations[i].CellData == null;
            //    BufferDatas[i] = CCDMeasDataStations[i].CellData;
            //}
            DateTime start = DateTime.Now;
            _camTrigFinish[CELLCOUNT] = false;
            if (CCDMeasDataStations[EnumCellIndex.右电芯].CellData != null)
            {
                while (!AxisOutlineMeasX.MoveTo(EnumPointX.Start))
                //return DispMotionError(AxisOutlineMeasX, EnumCellIndex.右电芯);
                {
                    res = DispMotionError(AxisOutlineMeasX, EnumCellIndex.右电芯);
                    if (res != null) return res;
                }
                ActionOneCCDMeas(EnumCellIndex.右电芯, NeedComp);
            }
            TimeSpan span1 = DateTime.Now - start;
            if (CCDMeasDataStations[EnumCellIndex.中电芯].CellData != null)
            {
                while (!AxisOutlineMeasX.MoveTo(EnumPointX.Start, true, AxisOutlineMeasX.PointList[EnumPointX.Pitch].Position))
                //return DispMotionError(AxisOutlineMeasX, EnumCellIndex.中电芯);
                {
                    res = DispMotionError(AxisOutlineMeasX, EnumCellIndex.中电芯);
                    if (res != null) return res;
                }
                ActionOneCCDMeas(EnumCellIndex.中电芯, NeedComp);
            }
            TimeSpan span2 = DateTime.Now - start;
            if (CCDMeasDataStations[EnumCellIndex.左电芯].CellData != null)
            {
                while (!AxisOutlineMeasX.MoveTo(EnumPointX.Start, true, AxisOutlineMeasX.PointList[EnumPointX.Pitch].Position * 2))
                //return DispMotionError(AxisOutlineMeasX, EnumCellIndex.左电芯);
                {
                    res = DispMotionError(AxisOutlineMeasX, EnumCellIndex.左电芯);
                    if (res != null) return res;
                }
                ActionOneCCDMeas(EnumCellIndex.左电芯, NeedComp);
            }
            TimeSpan span3 = DateTime.Now - start;
            while (!AxisOutlineMeasX.MoveTo(EnumPointX.PartOut))
            //return DispMotionError(AxisOutlineMeasX, EnumPointX.PartOut);
            {
                res = DispMotionError(AxisOutlineMeasX, EnumPointX.PartOut);
                if (res != null) return res;
            }
            //while (!_measDone[0] || !_measDone[1] || !_measDone[2])
            //    Thread.Sleep(1);
            //if (AfterCCDMeas != null)
            //{
            //    _afterCallBack = AfterCCDMeas;
            //    _afterCallBack.BeginInvoke(WorkZoneActionAsyncReturn, _afterCallBack);
            //}
            TimeSpan span4 = DateTime.Now - start;
            TimeUsage.UpdateTimeUsage(TimeUsageItem.ZoneCCD, span4.TotalSeconds,
               string.Format("右电芯测量: {0:0.00}s\n中电芯测量: {1:0.00}s\n左电芯测量: {2:0.00}s\n移到出料位: {3:0.00}s",
                                       span1.TotalSeconds,
                                       (span2 - span1).TotalSeconds,
                                       (span3 - span2).TotalSeconds,
                                       (span4 - span3).TotalSeconds));
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                CellVacuums[i].SetOutPortStatus(false);
            return null;
        }
        #endregion Action
    }
}