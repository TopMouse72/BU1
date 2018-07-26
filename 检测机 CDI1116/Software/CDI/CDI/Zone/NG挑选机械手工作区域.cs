using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.MotionSystem;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;
using Measure;

namespace CDI.Zone
{
    public class ClassZoneNG挑选机械手 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            SortingPNPY = HardwareAxisName.SortingPNPY,//NG挑选机械手Y轴
            SortingPNPZ = HardwareAxisName.SortingPNPZ,//NGr挑选机械手Z轴
        }
        public enum EnumPointPNPY
        {
            Pick = -1,
            NGBox1 = 0,
            NGBox2 = 1,
            NGBox3 = 2,
            NGBox4 = 3,
        }
        public enum EnumPointPNPZ
        {
            Up,
            Pick,
            Place,
        }

        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            SortingPNPCyLeftUp = HardwareInportName.SortingPNPCyLeftUp,//NG挑选机械手左气缸上
            SortingPNPCyLeftDown = HardwareInportName.SortingPNPCyLeftDown,//NG挑选机械手左气缸下
            SortingPNPCyMidUp = HardwareInportName.SortingPNPCyMidUp,//NG挑选机械手中气缸上
            SortingPNPCyMidDown = HardwareInportName.SortingPNPCyMidDown,//NG挑选机械手中气缸下
            SortingPNPCyRightUp = HardwareInportName.SortingPNPCyRightUp,//NG挑选机械手右气缸上
            SortingPNPCyRightDown = HardwareInportName.SortingPNPCyRightDown,//NG挑选机械手右气缸下
            SortingPNPVacSensLeft = HardwareInportName.SortingPNPVacSensLeft,//NG挑选机械手左吸头真空
            SortingPNPVacSensMid = HardwareInportName.SortingPNPVacSensMid,//NG挑选机械手中吸头真空
            SortingPNPVacSensRight = HardwareInportName.SortingPNPVacSensRight,//NG挑选机械手右吸头真空
            SortingPNPNGBoxBack = HardwareInportName.SortingPNPNGBoxBack,//NG挑选机械手后NG盒
            SortingPNPNGBoxFront = HardwareInportName.SortingPNPNGBoxFront,//NG挑选机械手前NG盒
            SortingPNPNGBoxFull1 = HardwareInportName.SortingPNPNGBoxFull1,//NG挑选机械手NG盒1满
            SortingPNPNGBoxFull2 = HardwareInportName.SortingPNPNGBoxFull2,//NG挑选机械手NG盒2满
            SortingPNPNGBoxFull3 = HardwareInportName.SortingPNPNGBoxFull3,//NG挑选机械手NG盒3满
            SortingPNPNGBoxFull4 = HardwareInportName.SortingPNPNGBoxFull4,//NG挑选机械手NG盒4满
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            SortingPNPCyLeftUp = HardwareOutportName.SortingPNPCyLeftUp,//NG挑选机械手左气缸上控制
            SortingPNPCyLeftDown = HardwareOutportName.SortingPNPCyLeftDown,//NG挑选机械手左气缸下控制
            SortingPNPCyMidUp = HardwareOutportName.SortingPNPCyMidUp,//NG挑选机械手中气缸上控制
            SortingPNPCyMidDown = HardwareOutportName.SortingPNPCyMidDown,//NG挑选机械手中气缸下控制
            SortingPNPCyRightUp = HardwareOutportName.SortingPNPCyRightUp,//NG挑选机械手右气缸上控制
            SortingPNPCyRightDown = HardwareOutportName.SortingPNPCyRightDown,//NG挑选机械手右气缸下控制
            SortingPNPVacLeft = HardwareOutportName.SortingPNPVacLeft,
            SortingPNPVacLeftBack = HardwareOutportName.SortingPNPVacLeftBack,//NG挑选机械手左吸头后腔真空
            SortingPNPVacLeftCent = HardwareOutportName.SortingPNPVacLeftCent,//NG挑选机械手左吸头中腔真空
            SortingPNPVacLeftFront = HardwareOutportName.SortingPNPVacLeftFront,//NG挑选机械手左吸头前腔真空
            SortingPNPVacMid = HardwareOutportName.SortingPNPVacMid,
            SortingPNPVacMidBack = HardwareOutportName.SortingPNPVacMidBack,//NG挑选机械手中吸头后腔真空
            SortingPNPVacMidCent = HardwareOutportName.SortingPNPVacMidCent,//NG挑选机械手中吸头中腔真空
            SortingPNPVacMidFront = HardwareOutportName.SortingPNPVacMidFront,//NG挑选机械手中吸头前腔真空
            SortingPNPVacRight = HardwareOutportName.SortingPNPVacRight,
            SortingPNPVacRightBack = HardwareOutportName.SortingPNPVacRightBack,//NG挑选机械手右吸头后腔真空
            SortingPNPVacRightCent = HardwareOutportName.SortingPNPVacRightCent,//NG挑选机械手右吸头中腔真空
            SortingPNPVacRightFront = HardwareOutportName.SortingPNPVacRightFront,//NG挑选机械手右吸头前腔真空
            SortingPNPBlow = HardwareOutportName.SortingPNPBlow,//NG挑选机械手吹气
        }
        public BaseIOPort[] NGBoxFullSensor = new BaseIOPort[4];
        public BaseIOPort NGBoxBackSenser, NGBoxFrontSensor;
        public CellCollection<CylinderControl> PNPCylinder = new CellCollection<CylinderControl>();
        public CellCollection<ClassAirUnit> CellVacuums = new CellCollection<ClassAirUnit>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public ClassAirPort CellBlow;
        public CAxisBase AxisSortingPNPY, AxisSortingPNPZ;
        public ClassZoneNG挑选机械手() : base(EnumZoneName.ZoneNG挑选机械手.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                CellVacuums.Add(i, new ClassAirUnit());
                CellVacSensor.Add(i, new ClassAirPort());
                SortNGDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
                NGBoxDataInfoString[0, i] = "";
                NGBoxDataInfoString[1, i] = "";
                NGBoxDataInfoString[2, i] = "";
                NGBoxDataInfoString[3, i] = "";
            }
            CellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisSortingPNPY = ThisAxis(EnumAxisName.SortingPNPY);
            AxisSortingPNPZ = ThisAxis(EnumAxisName.SortingPNPZ);
            NGBoxFullSensor[0] = ThisInport(EnumInportName.SortingPNPNGBoxFull1);
            NGBoxFullSensor[1] = ThisInport(EnumInportName.SortingPNPNGBoxFull2);
            NGBoxFullSensor[2] = ThisInport(EnumInportName.SortingPNPNGBoxFull3);
            NGBoxFullSensor[3] = ThisInport(EnumInportName.SortingPNPNGBoxFull4);
            NGBoxBackSenser = ThisInport(EnumInportName.SortingPNPNGBoxBack);
            NGBoxFrontSensor = ThisInport(EnumInportName.SortingPNPNGBoxFront);
            AxisSortingPNPY.AddPoints(typeof(EnumPointPNPY));
            AxisSortingPNPZ.AddPoints(typeof(EnumPointPNPZ));
            PNPCylinder.Add(EnumCellIndex.左电芯, new CylinderControl(/*"左电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.SortingPNPCyLeftDown),
                        ThisOutport(EnumOutportName.SortingPNPCyLeftUp),
                        ThisInport(EnumInportName.SortingPNPCyLeftDown),
                        ThisInport(EnumInportName.SortingPNPCyLeftUp)));
            PNPCylinder.Add(EnumCellIndex.中电芯, new CylinderControl(/*"中电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.SortingPNPCyMidDown),
                        ThisOutport(EnumOutportName.SortingPNPCyMidUp),
                        ThisInport(EnumInportName.SortingPNPCyMidDown),
                        ThisInport(EnumInportName.SortingPNPCyMidUp)));
            PNPCylinder.Add(EnumCellIndex.右电芯, new CylinderControl(/*"右电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.SortingPNPCyRightDown),
                        ThisOutport(EnumOutportName.SortingPNPCyRightUp),
                        ThisInport(EnumInportName.SortingPNPCyRightDown),
                        ThisInport(EnumInportName.SortingPNPCyRightUp)));
            CellVacuums[EnumCellIndex.左电芯].MainPort.Port =
                ThisOutport(EnumOutportName.SortingPNPVacLeft);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacLeftBack);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacLeftCent);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacLeftFront);
            CellVacuums[EnumCellIndex.中电芯].MainPort.Port =
                ThisOutport(EnumOutportName.SortingPNPVacMid);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacMidBack);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacMidCent);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacMidFront);
            CellVacuums[EnumCellIndex.右电芯].MainPort.Port =
                ThisOutport(EnumOutportName.SortingPNPVacRight);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacRightBack);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacRightCent);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.SortingPNPVacRightFront);
            CellBlow.Port = ThisOutport(EnumOutportName.SortingPNPBlow);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.SortingPNPVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.SortingPNPVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.SortingPNPVacSensRight);
            ZoneSettingPanel = new SettingPanelZoneNG挑选机械手();
            ZoneManualPanel = new ManualPanelZoneNG挑选机械手();
        }

        public override ErrorInfoWithPause ResetOutPort()
        {
            ErrorInfoWithPause res = null;
            string err = "";
            base.ResetOutPort();
            for (int i = 0; i < CellVacuums.Count; i++)
            {
                CellVacuums[i].MainPort.SetOutPortStatus(false);
                foreach (ClassAirPort vacuum in CellVacuums[i].AirPorts)
                    vacuum.SetOutPortStatus(false);
            }
            CellBlow.SetOutPortStatus(false);
            TimeClass.Delay(200);
            PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            if (!PNPCylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "左气缸 ";
            if (!PNPCylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "中气缸 ";
            if (!PNPCylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "右气缸 ";
            if (err != "")
                res = new ErrorInfoWithPause("气缸上升错误: " + err.Trim(), ErrorLevel.Error);
            return res;
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisSortingPNPY.ServoOn = true;
            AxisSortingPNPZ.ServoOn = true;
            PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            if (!PNPCylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("左气缸无法上移");
            if (!PNPCylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("中气缸无法上移");
            if (!PNPCylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("右气缸无法上移");
            if (err.NoError) err.CollectErrInfo(MotorReset(AxisSortingPNPZ, EnumPointPNPZ.Up));
            if (err.NoError) err.CollectErrInfo(MotorReset(AxisSortingPNPY, EnumPointPNPY.Pick));
            if (ThisInport(EnumInportName.SortingPNPNGBoxBack).status || ThisInport(EnumInportName.SortingPNPNGBoxFull1).status || ThisInport(EnumInportName.SortingPNPNGBoxFull2).status
            || ThisInport(EnumInportName.SortingPNPNGBoxFront).status || ThisInport(EnumInportName.SortingPNPNGBoxFull3).status || ThisInport(EnumInportName.SortingPNPNGBoxFull4).status)
                err.CollectErrInfo("前后NG盒没有关上或全满。");
            IsUseBackNGBox = true;
            CurrentNGBoxRow = (int)EnumPointPNPY.NGBox1;
            for (int i = 0; i < 4; i++)
                for (int k = 0; k < CELLCOUNT; k++)
                {
                    NGBoxCellCount[i, k] = 0;
                    NGBoxDataInfoString[i, k] = "";
                }
            for (int i = 0; i < CELLCOUNT; i++)
                SortNGDataStations[i].CellData = null;
        }
        #region Event
        private void NGBoxFullErrorHandler(string source, string message, ErrorDialogResult result)
        {
            if (result == ErrorDialogResult.OK)
            {
                bool BackNGBoxClean, FrontNGBoxClean;
                if (ClassWorkFlow.Instance.UnloadMode == EnumUnloadMode.全NG)
                {
                    BackNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull1).status && ThisInport(EnumInportName.SortingPNPNGBoxFull2).status;
                    FrontNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull3).status && ThisInport(EnumInportName.SortingPNPNGBoxFull4).status;
                }
                else
                {
                    BackNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull1).status || ThisInport(EnumInportName.SortingPNPNGBoxFull2).status;
                    FrontNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull3).status || ThisInport(EnumInportName.SortingPNPNGBoxFull4).status;
                }
                if (IsUseBackNGBox)
                {
                    if (BackNGBoxClean)
                        ClassErrorHandle.ShowError(source, "后下料NG料盒满。请移走NG料。", ErrorLevel.Notice, false, false, NGBoxFullErrorHandler);
                }
                else
                {
                    if (FrontNGBoxClean)
                        ClassErrorHandle.ShowError(source, "前下料NG料盒满。请移走NG料。", ErrorLevel.Notice, false, false, NGBoxFullErrorHandler);
                }
            }
        }
        public override void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ProductChangeHandler(sender, e);
            if (ClassCommonSetting.SysParam.CurrentProductParam == null) return;
            foreach (ClassAirUnit cell in CellVacuums.Values)
            {
                cell.AirPorts[(int)EnumVacuumIndex.后真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumSortingPNP.VacuumBackEnable;
                cell.AirPorts[(int)EnumVacuumIndex.中真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumSortingPNP.VacuumCentEnable;
                cell.AirPorts[(int)EnumVacuumIndex.前真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumSortingPNP.VacuumFrontEnable;
            }
        }
        protected override void InPortDeActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.SortingPNPNGBoxBack:
                    for (int i = 0; i < CELLCOUNT; i++)
                    {
                        NGBoxCellCount[0, i] = 0;
                        NGBoxCellCount[1, i] = 0;
                        NGBoxDataInfoString[0, i] = "";
                        NGBoxDataInfoString[1, i] = "";
                    }
                    DoUpdateNGBox();
                    break;
                case EnumInportName.SortingPNPNGBoxFront:
                    for (int i = 0; i < CELLCOUNT; i++)
                    {
                        NGBoxCellCount[2, i] = 0;
                        NGBoxCellCount[3, i] = 0;
                        NGBoxDataInfoString[2, i] = "";
                        NGBoxDataInfoString[3, i] = "";
                    }
                    DoUpdateNGBox();
                    break;
                case EnumInportName.SortingPNPNGBoxFull1:
                case EnumInportName.SortingPNPNGBoxFull2:
                case EnumInportName.SortingPNPNGBoxFull3:
                case EnumInportName.SortingPNPNGBoxFull4:
                    DoUpdateNGBox();
                    break;
            }
        }
        protected override void InPortActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.SortingPNPNGBoxBack:
                    ClassErrorHandle.ShowError(this.Name, "后下料NG料盒被移走。移回NG盒后按OK。", ErrorLevel.Notice);
                    DoUpdateNGBox();
                    break;
                case EnumInportName.SortingPNPNGBoxFront:
                    ClassErrorHandle.ShowError(this.Name, "前下料NG料盒被移走。移回NG盒后按OK。", ErrorLevel.Notice);
                    DoUpdateNGBox();
                    break;
                case EnumInportName.SortingPNPNGBoxFull1:
                case EnumInportName.SortingPNPNGBoxFull2:
                case EnumInportName.SortingPNPNGBoxFull3:
                case EnumInportName.SortingPNPNGBoxFull4:
                    DoUpdateNGBox();
                    break;
            }
        }
        #endregion Event
        #region Data
        public CellCollection<ClassDataStation> SortNGDataStations = new CellCollection<ClassDataStation>();
        public ClassDataInfo[,] NGBoxDatas = new ClassDataInfo[4, CELLCOUNT];
        public string[,] NGBoxDataInfoString = new string[4, CELLCOUNT];
        public short[,] NGBoxCellCount = new short[4, CELLCOUNT];
        public string GetDataInfoString()
        {
            return GetDataInfoString(SortNGDataStations);
        }
        public int GetMaxCellIndex()
        {
            if (SortNGDataStations[EnumCellIndex.左电芯].CellData != null)
                return SortNGDataStations[EnumCellIndex.左电芯].CellData.Index;
            else if (SortNGDataStations[EnumCellIndex.中电芯].CellData != null)
                return SortNGDataStations[EnumCellIndex.中电芯].CellData.Index;
            else if (SortNGDataStations[EnumCellIndex.右电芯].CellData != null)
                return SortNGDataStations[EnumCellIndex.右电芯].CellData.Index;
            return -1;
        }
        public bool IsUseBackNGBox = true;
        public int CurrentNGBoxRow;
        public ErrorInfoWithPause UpdateRow()
        {
            int NGBoxIndex = (int)EnumPointPNPY.NGBox1;
            bool BackNGBoxClean, FrontNGBoxClean;
            if (ClassWorkFlow.Instance.UnloadMode == EnumUnloadMode.全NG)
            {
                BackNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull1).status && ThisInport(EnumInportName.SortingPNPNGBoxFull2).status;
                FrontNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull3).status && ThisInport(EnumInportName.SortingPNPNGBoxFull4).status;
            }
            else
            {
                BackNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull1).status || ThisInport(EnumInportName.SortingPNPNGBoxFull2).status;
                FrontNGBoxClean = ThisInport(EnumInportName.SortingPNPNGBoxFull3).status || ThisInport(EnumInportName.SortingPNPNGBoxFull4).status;
            }
            if ((ThisInport(EnumInportName.SortingPNPNGBoxBack).status || BackNGBoxClean) && (ThisInport(EnumInportName.SortingPNPNGBoxFront).status || FrontNGBoxClean))
                return new ErrorInfoWithPause("前后NG盒都没有关上或全满，需清空NG料。", ErrorLevel.Alarm, true);
            if (IsUseBackNGBox && (ThisInport(EnumInportName.SortingPNPNGBoxBack).status || BackNGBoxClean))
                IsUseBackNGBox = false;
            if (!IsUseBackNGBox && (ThisInport(EnumInportName.SortingPNPNGBoxFront).status || FrontNGBoxClean))
                IsUseBackNGBox = true;
            if (IsUseBackNGBox)
            {
                if (!ThisInport(EnumInportName.SortingPNPNGBoxFull1).status)
                    NGBoxIndex = (int)EnumPointPNPY.NGBox1;
                else
                    NGBoxIndex = (int)EnumPointPNPY.NGBox2;
            }
            else
            {
                if (!ThisInport(EnumInportName.SortingPNPNGBoxFull3).status)
                    NGBoxIndex = (int)EnumPointPNPY.NGBox3;
                else
                    NGBoxIndex = (int)EnumPointPNPY.NGBox4;
            }
            CurrentNGBoxRow = NGBoxIndex;
            return null;
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            SortNGDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            SortNGDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            SortNGDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public enum PNPAction
        {
            Pick,
            PlaceNG,
        }
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= SortNGDataStations[i].CellData != null;
            return need;
        }
        /// <summary>
        /// PNP电机移到到指定位置
        /// </summary>
        /// <param name="action"></param>
        public ErrorInfoWithPause ActionMove(EnumPointPNPY action)
        {
            string err = "";
            ErrorInfoWithPause res = null;
            while (true)
            {
                err = "";
                PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
                if (!PNPCylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                    err += " 左气缸";
                if (!PNPCylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                    err += " 中气缸";
                if (!PNPCylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                    err += " 右气缸";
                if (err != "")// return new ErrorInfoWithPause("NG挑选PNP气缸上移超时错:" + err, ErrorLevel.Alarm, false, true);
                {
                    res = WaitAlarmPause("NG挑选PNP移动", "NG挑选PNP气缸上移超时错: " + err);
                    if (res != null) return res;
                }
                else
                    break;
            }
            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Up)) //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
            {
                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
                if (res != null) return res;
            }
            while (!AxisSortingPNPY.MoveTo(action)) //return DispMotionError(AxisSortingPNPY, action);
            {
                res = DispMotionError(AxisSortingPNPY, action);
                if (res != null) return res;
            }
            return null;
        }
        private void SetUnitStatus(EnumCellIndex CellIndex, EnumAirControl status)
        {
            switch (status)
            {
                case EnumAirControl.Blow:
                    CellVacuums[CellIndex].MainPort.SetOutPortStatus(false);
                    CellBlow.SetOutPortStatus(true);
                    for (int i = 0; i < ClassAirUnit.VACCOUNT; i++)
                        CellVacuums[CellIndex].AirPorts[i].SetOutPortStatus(true);
                    break;
                case EnumAirControl.Close:
                    CellVacuums[CellIndex].MainPort.SetOutPortStatus(false);
                    for (int i = 0; i < ClassAirUnit.VACCOUNT; i++)
                        CellVacuums[CellIndex].AirPorts[i].SetOutPortStatus(false);
                    break;
                case EnumAirControl.Vacuum:
                    CellVacuums[CellIndex].MainPort.SetOutPortStatus(true);
                    for (int i = 0; i < ClassAirUnit.VACCOUNT; i++)
                        CellVacuums[CellIndex].AirPorts[i].SetOutPortStatus(CellVacuums[CellIndex].AirPorts[i].PortEnable);
                    break;
            }
        }
        public void AirControl(EnumCellIndex CellIndex, EnumAirControl status)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return;
            if (status == EnumAirControl.None) return;
            SetUnitStatus(CellIndex, status);
        }
        public void AirControl(EnumAirControl LeftVacuum, EnumAirControl MiddleVacuum, EnumAirControl RightVacuum)
        {
            AirControl(EnumCellIndex.左电芯, LeftVacuum);
            AirControl(EnumCellIndex.中电芯, MiddleVacuum);
            AirControl(EnumCellIndex.右电芯, RightVacuum);
        }
        public string CheckVacuumStatus(EnumCellIndex CellIndex)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return "";
            bool status = CellVacSensor[CellIndex].GetInPortStatus();
            string res = "";
            switch (CellIndex)
            {
                case EnumCellIndex.左电芯:
                    res += status ? "" : " 左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res += status ? "" : " 中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res += status ? "" : " 右真空";
                    break;
            }
            SortNGDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = SortNGDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = SortNGDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = SortNGDataStations[EnumCellIndex.右电芯].CellData != null;
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
                return new ErrorInfoWithPause("NG挑选PNP真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        public ErrorInfoWithPause ActionSortPNPStartPick(CallBackCommonFunc ActionSortPNPPick, CallBackCommonFunc AfterActionSortPNPPick)
        {
            ErrorInfoWithPause res = null;
            //PNP Z down
            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Pick, true, ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean - 1))
            //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Pick);
            {
                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Pick);
                if (res != null) return res;
            }
            //Start picking
            if (ActionSortPNPPick != null) ActionSortPNPPick();
            //PNP Z up
            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Up)) //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
            {
                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
                if (res != null) return res;
            }
            if (AfterActionSortPNPPick != null) AfterActionSortPNPPick();
            //Check vacuum
            System.Threading.Thread.Sleep(100);
            res = CheckVacuumStatus();
            return res;
        }
        private bool CheckIfNeedCylinder(int NGCellCount)
        {
            double NGCellHeight = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean * NGCellCount;
            return NGCellHeight < Math.Abs(AxisSortingPNPZ.PointList[EnumPointPNPZ.Place].Position);
        }
        private int GetMaxCount()
        {
            int max = 0;
            for (int i = 0; i < CELLCOUNT; i++)
                if (max < NGBoxCellCount[CurrentNGBoxRow, i]) max = NGBoxCellCount[CurrentNGBoxRow, i];
            return max;
        }
        public ErrorInfoWithPause ActionSortPNPStartPlaceNG(CallBackCommonFunc AfterActionSortPNPPlaceNG)
        {
            ErrorInfoWithPause res = null;
            //Check vacuum
            res = CheckVacuumStatus();
            if (res != null) return res;
            bool NeedCylinderDown;
            //res = UpdateRow();
            //if (res != null) return res;
            if (ClassWorkFlow.Instance.UnloadMode != EnumUnloadMode.全NG)
            {
                for (int i = 0; i < CELLCOUNT; i++)
                {
                    if (CheckCellIsNG(SortNGDataStations[i].CellData))
                    {
                        if (IsUseBackNGBox)
                        {
                            if (SortNGDataStations[i].CellData.ThicknessNG)
                                CurrentNGBoxRow = (int)EnumPointPNPY.NGBox1;
                            else
                                CurrentNGBoxRow = (int)EnumPointPNPY.NGBox2;
                        }
                        else
                        {
                            if (SortNGDataStations[i].CellData.ThicknessNG)
                                CurrentNGBoxRow = (int)EnumPointPNPY.NGBox3;
                            else
                                CurrentNGBoxRow = (int)EnumPointPNPY.NGBox4;
                        }
                        NGBoxCellCount[CurrentNGBoxRow, i]++;
                        string NGData = SortNGDataStations[i].CellData.NGDataInfoString;
                        if (NGData != "")
                            NGBoxDataInfoString[CurrentNGBoxRow, i] += NGData + Environment.NewLine;
                        NeedCylinderDown = CheckIfNeedCylinder(NGBoxCellCount[CurrentNGBoxRow, i]);

                        //PNP Y move away
                        res = ActionMove((EnumPointPNPY)CurrentNGBoxRow);
                        if (res != null) return res;
                        if (NeedCylinderDown)
                        {
                            //PNP Z down
                            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Place, true, 1 + NGBoxCellCount[CurrentNGBoxRow, i] * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                            //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                            {
                                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                                if (res != null) return res;
                            }
                            //Cylinder down
                            while (!PNPCylinder[i].SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                            //return new ErrorInfoWithPause("NG挑选PNP气缸下移超时错。", ErrorLevel.Error);
                            {
                                res = WaitAlarmPause("NG挑选PNP放料", "NG挑选PNP气缸下移超时错");
                                if (res != null) return res;
                            }
                        }
                        else
                        {
                            //PNP Z down
                            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Place, true, -100 + 1 + GetMaxCount() * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                            //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                            {
                                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                                if (res != null) return res;
                            }
                        }
                        //Open blow
                        AirControl((EnumCellIndex)i, EnumAirControl.Blow);
                        //AirControl((EnumCellIndex)i, EnumAirControl.Close);
                        //Delay
                        TimeClass.Delay(400);
                        //Cylinder up
                        while (!PNPCylinder[i].SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                        //return new ErrorInfoWithPause("NG挑选PNP气缸上移超时错。", ErrorLevel.Error);
                        {
                            res = WaitAlarmPause("NG挑选PNP放料", "NG挑选PNP气缸上移超时错");
                            if (res != null) return res;
                        }
                        //Close blow
                        AirControl((EnumCellIndex)i, EnumAirControl.Close);
                    }
                }
            }
            else
            {
                for (int i = 0; i < CELLCOUNT; i++)
                {
                    if (CheckCellIsNG(SortNGDataStations[i].CellData))
                    {
                        NGBoxCellCount[CurrentNGBoxRow, i]++;
                    }
                }
                NeedCylinderDown = CheckIfNeedCylinder(GetMaxCount());

                //PNP Y move away
                res = ActionMove((EnumPointPNPY)CurrentNGBoxRow);
                if (res != null) return res;
                if (NeedCylinderDown)
                {
                    //PNP Z down
                    while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Place, true, 1 + GetMaxCount() * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                    //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                    {
                        res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                        if (res != null) return res;
                    }
                    //Cylinder down
                    PNPCylinder[0].SetCylinderState(CYLIND_DOWN, 0, false);
                    PNPCylinder[1].SetCylinderState(CYLIND_DOWN, 0, false);
                    PNPCylinder[2].SetCylinderState(CYLIND_DOWN, 0, false);
                    while (!PNPCylinder[0].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT) || !PNPCylinder[1].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT) || !PNPCylinder[2].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                    //return new ErrorInfoWithPause("NG挑选PNP气缸下移超时错。", ErrorLevel.Error);
                    {
                        res = WaitAlarmPause("NG挑选PNP放料", "NG挑选PNP气缸下移超时错");
                        if (res != null) return res;
                    }
                }
                else
                {
                    //PNP Z down
                    while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Place, true, -100 + 1 + GetMaxCount() * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                    //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                    {
                        res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Place);
                        if (res != null) return res;
                    }
                }
                //Open blow
                AirControl(EnumAirControl.Blow, EnumAirControl.Blow, EnumAirControl.Blow);
                //AirControl(EnumAirControl.Close, EnumAirControl.Close, EnumAirControl.Close);
                //Delay
                TimeClass.Delay(400);
                //Cylinder up
                PNPCylinder[0].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[1].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[2].SetCylinderState(CYLIND_UP, 0, false);
                while (!PNPCylinder[0].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT) || !PNPCylinder[1].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT) || !PNPCylinder[2].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                //return new ErrorInfoWithPause("NG挑选PNP气缸上移超时错。", ErrorLevel.Error);
                {
                    res = WaitAlarmPause("NG挑选PNP放料", "NG挑选PNP气缸上移超时错");
                    if (res != null) return res;
                }
                //Close blow
                AirControl(EnumAirControl.Close, EnumAirControl.Close, EnumAirControl.Close);
            }

            //PNP Z up
            while (!AxisSortingPNPZ.MoveTo(EnumPointPNPZ.Up)) //return DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
            {
                res = DispMotionError(AxisSortingPNPZ, EnumPointPNPZ.Up);
                if (res != null) return res;
            }
            DoUpdateNGBox();
            //PNP Y move away
            while (!AxisSortingPNPY.MoveTo(EnumPointPNPY.Pick)) //return DispMotionError(AxisSortingPNPY, EnumPointPNPY.Pick);
            {
                res = DispMotionError(AxisSortingPNPY, EnumPointPNPZ.Pick);
                if (res != null) return res;
            }
            if (AfterActionSortPNPPlaceNG != null) AfterActionSortPNPPlaceNG();
            NGBoxFullErrorHandler(this.Name, "", ErrorDialogResult.OK);
            return null;
        }
        //public bool CheckNGBoxAvaliable()
        //{
        //    return !ThisInport(EnumInportName.LoadPNPNGBox).status && !ThisInport(EnumInportName.LoadPNPNGBoxFull).status;
        //}
        #endregion Action
    }
}