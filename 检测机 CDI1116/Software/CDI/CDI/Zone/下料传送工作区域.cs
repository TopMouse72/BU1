using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.MotionSystem;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using System.Windows.Forms;

namespace CDI.Zone
{
    public class ClassZone下料传送 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            UnloadOutConveyor = HardwareAxisName.UnloadOutConveyor,//下料传送皮带轴
        }
        public enum EnumPoint
        {
            Step,
            Shift,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            UnloadOutHavePartLeft = HardwareInportName.UnloadOutHavePartLeft,//下料传送左有物料
            UnloadOutHavePartMid = HardwareInportName.UnloadOutHavePartMid,//下料传送中有物料
            UnloadOutHavePartRight = HardwareInportName.UnloadOutHavePartRight,//下料传送右有物料
            UnloadOutUnloadRight = HardwareInportName.UnloadOutUnloadRight,//下料传送下料右
            UnloadOutUnloadMid = HardwareInportName.UnloadOutUnloadMid,//下料传送下料中
            UnloadOutUnloadLeft = HardwareInportName.UnloadOutUnloadLeft,//下料传送下料左
            UnloadOutSMEMAUnloadAvailable = HardwareInportName.UnloadOutSMEMAUnloadAvailable,//外框SMEMA下料Available
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            UnloadOutSMEMAUnloadReady = HardwareOutportName.UnloadOutSMEMAUnloadReady,//外框SMEMA下料Ready
        }
        public bool IgnoreSMEMA
        {
            get { return ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored; }
            set { ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored = value; }
        }
        public CAxisBase AxisUnloadOutConveyor;
        public ClassZone下料传送() : base(EnumZoneName.Zone下料传送.ToString())
        {
            for (int i = 0; i < UnloadOutDataStations.Length; i++)
                UnloadOutDataStations[i] = new ClassDataStation("电芯" + (i + 1).ToString());
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisUnloadOutConveyor = ThisAxis(EnumAxisName.UnloadOutConveyor);
            AxisUnloadOutConveyor.AddPoints(typeof(EnumPoint));
            ZoneSettingPanel = new SettingPanelZone下料传送();
            ZoneManualPanel = new ManualPanelZone下料传送();
        }

        protected override void Reset(ClassErrorHandle err)
        {
            AxisUnloadOutConveyor.ServoOn = true;
            AxisUnloadOutConveyor.HomeFinish = true;
            if (ThisInport(EnumInportName.UnloadOutHavePartLeft).status ||
                ThisInport(EnumInportName.UnloadOutHavePartMid).status ||
                ThisInport(EnumInportName.UnloadOutHavePartRight).status ||
                ThisInport(EnumInportName.UnloadOutUnloadRight).status)
                err.CollectErrInfo("传送带上有物料需要移除。");
            for (int i = 0; i < 6; i++)
                UnloadOutDataStations[i].CellData = null;
        }
        #region Event
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            ActionSMEMAReady(false);
            IsPlacingPart = false;
            IsSortPicking = false;
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ActionSMEMAReady(false);
        }
        protected override void InPortActive(string inPort)
        {
        }

        protected override void InPortDeActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.UnloadOutUnloadRight:
                    ActionSMEMAReady(false);
                    break;
            }
        }
        #endregion Event
        #region Data
        public ClassDataStation[] UnloadOutDataStations = new ClassDataStation[6];
        //public int GetCell = 0;
        public string GetDataInfoString()
        {
            return GetDataInfoString(UnloadOutDataStations);
        }
        public void AddDisp(IDataDisp disp6, IDataDisp disp5, IDataDisp disp4, IDataDisp disp3, IDataDisp disp2, IDataDisp disp1)
        {
            UnloadOutDataStations[0].AddDisp(disp1);
            UnloadOutDataStations[1].AddDisp(disp2);
            UnloadOutDataStations[2].AddDisp(disp3);
            UnloadOutDataStations[3].AddDisp(disp4);
            UnloadOutDataStations[4].AddDisp(disp5);
            UnloadOutDataStations[5].AddDisp(disp6);
        }
        #endregion Data
        #region Action
        public bool HavePart
        {
            get
            {
                bool need = false;
                need |= ThisInport(EnumInportName.UnloadOutUnloadRight).status;
                need |= ThisInport(EnumInportName.UnloadOutUnloadMid).status;
                need |= ThisInport(EnumInportName.UnloadOutUnloadLeft).status;
                for (int i = 0; i < 3; i++)
                    need |= UnloadOutDataStations[i].CellData != null;
                need |= ThisInport(EnumInportName.UnloadOutHavePartLeft).status;
                need |= ThisInport(EnumInportName.UnloadOutHavePartMid).status;
                need |= ThisInport(EnumInportName.UnloadOutHavePartRight).status;
                return need;
            }
        }
        public bool IsUnLoadHavePartEmpty
        {
            get
            {
                return UnloadOutDataStations[3].CellData == null &&
                    UnloadOutDataStations[4].CellData == null &&
                    UnloadOutDataStations[5].CellData == null;
            }
        }
        public bool IsUnLoadOutEmpty
        {
            get
            {
                return UnloadOutDataStations[0].CellData == null &&
                    UnloadOutDataStations[1].CellData == null &&
                    UnloadOutDataStations[2].CellData == null;
            }
        }
        private bool _isPlacing = false;
        public bool IsPlacingPart
        {
            get { return _isPlacing; }
            set
            {
                _isPlacing = value;
                AxisUnloadOutConveyor.StopMove();
            }
        }
        private bool _isSortPicking = false;
        public bool IsSortPicking
        {
            set { _isSortPicking = value; }
        }
        public int GoolParts
        {
            get
            {
                int count = 0;
                if (UnloadOutDataStations[0].CellData != null && !UnloadOutDataStations[0].CellData.DataNG) count++;
                if (UnloadOutDataStations[1].CellData != null && !UnloadOutDataStations[1].CellData.DataNG) count++;
                if (UnloadOutDataStations[2].CellData != null && !UnloadOutDataStations[2].CellData.DataNG) count++;
                return count;
            }
        }
        public int NGParts
        {
            get
            {
                int count = 0;
                if (UnloadOutDataStations[0].CellData != null && UnloadOutDataStations[0].CellData.DataNG) count++;
                if (UnloadOutDataStations[1].CellData != null && UnloadOutDataStations[1].CellData.DataNG) count++;
                if (UnloadOutDataStations[2].CellData != null && UnloadOutDataStations[2].CellData.DataNG) count++;
                return count;
            }
        }
        public bool NeedSorting
        {
            get
            {
                return CheckCellIsNG(UnloadOutDataStations[0].CellData) ||
                  CheckCellIsNG(UnloadOutDataStations[1].CellData) ||
                  CheckCellIsNG(UnloadOutDataStations[2].CellData);
            }
        }
        private ErrorInfoWithPause CheckNGSensor()
        {
            //waitHandle = false;
            string tempSensor = "", tempData = "";
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    tempSensor = tempData = "";
                    if (ThisInport(EnumInportName.UnloadOutUnloadLeft).status && UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData == null)
                        tempSensor += " " + EnumCellIndex.左电芯.ToString();
                    else if (!ThisInport(EnumInportName.UnloadOutUnloadLeft).status && UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData != null)
                        tempData += " " + EnumCellIndex.左电芯.ToString();
                    if (ThisInport(EnumInportName.UnloadOutUnloadMid).status && UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData == null)
                        tempSensor += " " + EnumCellIndex.中电芯.ToString();
                    else if (!ThisInport(EnumInportName.UnloadOutUnloadMid).status && UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData != null)
                        tempData += " " + EnumCellIndex.中电芯.ToString();
                    if (ThisInport(EnumInportName.UnloadOutUnloadRight).status && UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData == null)
                        tempSensor += " " + EnumCellIndex.右电芯.ToString();
                    else if (!ThisInport(EnumInportName.UnloadOutUnloadRight).status && UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData != null)
                        tempData += " " + EnumCellIndex.右电芯.ToString();
                    if (tempSensor == "" && tempData == "") break;
                }
                if (tempSensor != "" || tempData != "")
                {
                    if (tempSensor == "") tempSensor = "无";
                    if (tempData == "") tempData = "无";
                    ClassCommonSetting.ProgramLog(LogFile.Level.Warning, "WorkFlow", $"NG挑选处下料电芯与数据不符：{tempSensor}{tempData}");
                    Colibri.CommonModule.Forms.BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, $"NG挑选处下料电芯与数据不符。{Environment.NewLine}{Environment.NewLine}" +
                        $"传感器检测到有电芯，但是程序没有数据记录：{tempSensor}。{Environment.NewLine}" +
                        $"错误处理：取出不符的多余电芯。{Environment.NewLine}{Environment.NewLine}" +
                        $"程序有数据记录，但是传感器没有检测到电芯：{tempData}。{Environment.NewLine}" +
                        $"错误处理：如果是电芯位置不对，摆正或取走电芯；如果没有电芯则不用处理。点确定后数据将会被删除忽略掉。{Environment.NewLine}{Environment.NewLine}" +
                        $"需要在流程恢复前清除不符电芯。处理完成后点确定。"));
                    SetUnloadOut();
                }
                else
                    break;
            }
            return null;
        }
        private void SetUnloadOut()
        {
            if (!ThisInport(EnumInportName.UnloadOutUnloadLeft).status && UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData != null)
                UnloadOutDataStations[(int)EnumCellIndex.左电芯].CellData = null;
            if (!ThisInport(EnumInportName.UnloadOutUnloadMid).status && UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData != null)
                UnloadOutDataStations[(int)EnumCellIndex.中电芯].CellData = null;
            if (!ThisInport(EnumInportName.UnloadOutUnloadRight).status && UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData != null)
                UnloadOutDataStations[(int)EnumCellIndex.右电芯].CellData = null;
        }
        public ErrorInfoWithPause ActionSMEMAReady(bool Ready)
        {
            ThisOutport(EnumOutportName.UnloadOutSMEMAUnloadReady).SetOutput(Ready);
            return null;
        }
        public bool CheckUnloadFinish()
        {
            bool res = true;
            for (int i = 0; i <= 2; i++)
                res &= UnloadOutDataStations[i].CellData == null;
            return res;
        }
        public bool CheckUnloadReady()
        {
            //Part on in position sensor. Ready.
            bool res = ThisInport(EnumInportName.UnloadOutUnloadRight).status 
                || ThisInport(EnumInportName.UnloadOutUnloadMid).status
                || ThisInport(EnumInportName.UnloadOutUnloadLeft).status;
            res |= ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑;
            ////Have parts. Ready.
            //for (int i = 1; i < UnloadOutDatas.Length; i++)
            //    res |= UnloadOutDatas[i].CellData != null;
            //else
            //{
            //    res = ThisInport(EnumInportName.UnloadOutHavePartLeft).status ||
            //        ThisInport(EnumInportName.UnloadOutHavePartMid).status ||
            //        ThisInport(EnumInportName.UnloadOutHavePartRight).status;
            //    //InPosition sensor have part, can't start
            //    res &= !ThisInport(EnumInportName.UnloadOutUnload).status;
            //}
            //SMEMA ignored or SMEMA avaliable. Ready.
            //res &= ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored || ThisInport(EnumInportName.UnloadOutSMEMAUnloadAvailable).status;
            //if (res) TimeClass.Delay(500);
            return res;
        }
        public ErrorInfoWithPause ActionStartShift(CallBackCommonFunc AfterUnloadOutShift)
        {
            ErrorInfoWithPause res = null;
            AxisUnloadOutConveyor.SetZero();
            while (!AxisUnloadOutConveyor.MoveTo(EnumPoint.Shift))
            //return DispMotionError(AxisUnloadOutConveyor, "移到NG取料位");
            {
                res = DispMotionError(AxisUnloadOutConveyor, "移到NG取料位");
                if (res != null) return res;
            }
            if (AfterUnloadOutShift != null) AfterUnloadOutShift();
            CheckNGSensor();
            return null;
        }
        public ErrorInfoWithPause ActionStartUnload(CallBackCommonFunc AfterUnloadOut)
        {
            ErrorInfoWithPause res = null;
            IsWorkFree = false;
            AxisUnloadOutConveyor.SetZero();
            while (!AxisUnloadOutConveyor.MoveTo(EnumPoint.Step))
            //return DispMotionError(AxisUnloadOutConveyor, "向外移料");
            {
                res = DispMotionError(AxisUnloadOutConveyor, "向外移料");
                if (res != null) return res;
            }
            if (AfterUnloadOut != null) AfterUnloadOut();
            //if (ThisInport(EnumInportName.UnloadOutUnload).status != (UnloadOutDatas[0] != null))
            //{
            //    ClassErrorInfo.ShowError(this.Name, "下料处电芯有无和数据不相符。", ErrorLevel.Error);
            //    return "下料处电芯有无和数据不相符。";
            //}
            //else if (ThisInport(EnumInportName.UnloadOutUnload).status)
            //    break;
            IsWorkFree = true;
            return null;
        }
        #endregion Action
    }
}