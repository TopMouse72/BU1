using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.State;
using Colibri.CommonModule.MotionSystem;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;
using Measure;

namespace CDI.Zone
{
    /// <summary>
    /// 上料机械手工作区域
    /// </summary>
    public class ClassZone上料机械手 : ClassBaseWorkZone
    {
        /// <summary>
        /// PNPYZ电机点位名称
        /// </summary>
        public enum EnumPointY
        {
            SafeLimit,
            Pick,
            Buffer,
            Place,
            PlaceNG,
        }
        public enum EnumPointZ
        {
            Idle,
            Pick,
            Place,
            PlaceNG,
        }
        /// <summary>
        /// PNP电机名称
        /// </summary>
        public enum EnumAxisName
        {
            LoadPNPY = HardwareAxisName.LoadPNPY,//上料机械手Y轴
            LoadPNPZ = HardwareAxisName.LoadPNPZ,//上料机械手Z轴
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            LoadPNPNGBoxFull = HardwareInportName.LoadPNPNGBoxFull,//上料机械手NG盒满
            LoadPNPCyLeftUp = HardwareInportName.LoadPNPCyLeftUp,//上料机械手左气缸上
            LoadPNPCyLeftDown = HardwareInportName.LoadPNPCyLeftDown,//上料机械手左气缸下
            LoadPNPCyMidUp = HardwareInportName.LoadPNPCyMidUp,//上料机械手中气缸上
            LoadPNPCyMidDown = HardwareInportName.LoadPNPCyMidDown,//上料机械手中气缸下
            LoadPNPCyRightUp = HardwareInportName.LoadPNPCyRightUp,//上料机械手右气缸上
            LoadPNPCyRightDown = HardwareInportName.LoadPNPCyRightDown,//上料机械手右气缸下
            LoadPNPVacSensLeft = HardwareInportName.LoadPNPVacSensLeft,//上料机械手左吸头真空
            LoadPNPVacSensMid = HardwareInportName.LoadPNPVacSensMid,//上料机械手中吸头真空
            LoadPNPVacSensRight = HardwareInportName.LoadPNPVacSensRight,//上料枢机手右吸头真空
            LoadPNPNGBox = HardwareInportName.LoadPNPNGBox,//扫码NG盒到位
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            LoadPNPCyLeftUp = HardwareOutportName.LoadPNPCyLeftUp,//上料机械手左气缸上控制
            LoadPNPCyLeftDown = HardwareOutportName.LoadPNPCyLeftDown,//上料机械手左气缸下控制
            LoadPNPCyMidUp = HardwareOutportName.LoadPNPCyMidUp,//上料机械手中气缸上控制
            LoadPNPCyMidDown = HardwareOutportName.LoadPNPCyMidDown,//上料机械手中气缸下控制
            LoadPNPCyRightUp = HardwareOutportName.LoadPNPCyRightUp,//上料机械手右气缸上控制
            LoadPNPCyRightDown = HardwareOutportName.LoadPNPCyRightDown,//上料机械手右气缸下控制
            LoadPNPVacLeft = HardwareOutportName.LoadPNPVacLeft,
            LoadPNPVacLeftBack = HardwareOutportName.LoadPNPVacLeftBack,//上料机械手左吸头后腔真空
            LoadPNPVacLeftCent = HardwareOutportName.LoadPNPVacLeftCent,//上料机械手左吸头中腔真空
            LoadPNPVacLeftFront = HardwareOutportName.LoadPNPVacLeftFront,//上料机械手左吸头前腔真空
            LoadPNPBlowLeft = HardwareOutportName.LoadPNPBlowLeft,//上料机械手左吸头吹气
            LoadPNPVacMid = HardwareOutportName.LoadPNPVacMid,
            LoadPNPVacMidBack = HardwareOutportName.LoadPNPVacMidBack,//上料机械手中吸头后腔真空
            LoadPNPVacMidCent = HardwareOutportName.LoadPNPVacMidCent,//上料机械手中吸头中腔真空
            LoadPNPVacMidFront = HardwareOutportName.LoadPNPVacMidFront,//上料机械手中吸头前腔真空
            LoadPNPBlowMid = HardwareOutportName.LoadPNPBlowMid,//上料机械手中吸头吹气
            LoadPNPVacRight = HardwareOutportName.LoadPNPVacRight,
            LoadPNPVacRightBack = HardwareOutportName.LoadPNPVacRightBack,//上料机械手右吸头后腔真空
            LoadPNPVacRightCent = HardwareOutportName.LoadPNPVacRightCent,//上料机械手右吸头中腔真空
            LoadPNPVacRightFront = HardwareOutportName.LoadPNPVacRightFront,//上料机械手右吸头前腔真空
            LoadPNPBlowRight = HardwareOutportName.LoadPNPBlowRight,//上料机械手右吸头吹气
        }
        public CellCollection<CylinderControl> PNPCylinder = new CellCollection<CylinderControl>();
        public CellCollection<ClassAirUnit> CellVacuums = new CellCollection<ClassAirUnit>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public CellCollection<ClassAirPort> CellBlow = new CellCollection<ClassAirPort>();
        public CAxisBase AxisLoadPNPY, AxisLoadPNPZ;
        public BaseIOPort NGBoxFullSensor, NGBoxSensor;
        public bool isPicking = false;
        public ClassZone上料机械手() : base(EnumZoneName.Zone上料机械手.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                CellVacuums.Add(i, new ClassAirUnit());
                CellVacSensor.Add(i, new ClassAirPort());
                CellBlow.Add(i, new ClassAirPort());
                LoadPNPDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
                NGBoxCellCount.Add(i, 0);
            }
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisLoadPNPY = ThisAxis(EnumAxisName.LoadPNPY); AxisLoadPNPY.AddPoints(typeof(EnumPointY));
            AxisLoadPNPZ = ThisAxis(EnumAxisName.LoadPNPZ); AxisLoadPNPZ.AddPoints(typeof(EnumPointZ));
            NGBoxFullSensor = ThisInport(EnumInportName.LoadPNPNGBoxFull);
            NGBoxSensor = ThisInport(EnumInportName.LoadPNPNGBox);
            PNPCylinder.Add(EnumCellIndex.左电芯, new CylinderControl(/*"左电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.LoadPNPCyLeftDown),
                        ThisOutport(EnumOutportName.LoadPNPCyLeftUp),
                        ThisInport(EnumInportName.LoadPNPCyLeftDown),
                        ThisInport(EnumInportName.LoadPNPCyLeftUp)));
            PNPCylinder.Add(EnumCellIndex.中电芯, new CylinderControl(/*"中电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.LoadPNPCyMidDown),
                        ThisOutport(EnumOutportName.LoadPNPCyMidUp),
                        ThisInport(EnumInportName.LoadPNPCyMidDown),
                        ThisInport(EnumInportName.LoadPNPCyMidUp)));
            PNPCylinder.Add(EnumCellIndex.右电芯, new CylinderControl(/*"右电芯Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.LoadPNPCyRightDown),
                        ThisOutport(EnumOutportName.LoadPNPCyRightUp),
                        ThisInport(EnumInportName.LoadPNPCyRightDown),
                        ThisInport(EnumInportName.LoadPNPCyRightUp)));
            CellVacuums[EnumCellIndex.左电芯].MainPort.Port =
                ThisOutport(EnumOutportName.LoadPNPVacLeft);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
               ThisOutport(EnumOutportName.LoadPNPVacLeftBack);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacLeftCent);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacLeftFront);
            CellVacuums[EnumCellIndex.中电芯].MainPort.Port =
                ThisOutport(EnumOutportName.LoadPNPVacMid);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacMidBack);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacMidCent);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacMidFront);
            CellVacuums[EnumCellIndex.右电芯].MainPort.Port =
                ThisOutport(EnumOutportName.LoadPNPVacRight);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacRightBack);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacRightCent);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.LoadPNPVacRightFront);
            CellBlow[EnumCellIndex.左电芯].Port = ThisOutport(EnumOutportName.LoadPNPBlowLeft);
            CellBlow[EnumCellIndex.中电芯].Port = ThisOutport(EnumOutportName.LoadPNPBlowMid);
            CellBlow[EnumCellIndex.右电芯].Port = ThisOutport(EnumOutportName.LoadPNPBlowRight);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.LoadPNPVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.LoadPNPVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.LoadPNPVacSensRight);
            ZoneSettingPanel = new SettingPanelZone上料机械手();
            ZoneManualPanel = new ManualPanelZone上料机械手();
            DoUpdateNGBox();
            ProductChangeHandler(null, null);
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            ErrorInfoWithPause res = null;
            string err = "";
            base.ResetOutPort();
            PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            for (int i = 0; i < CellVacuums.Count; i++)
            {
                CellVacuums[i].MainPort.SetOutPortStatus(false);
                foreach (ClassAirPort vacuum in CellVacuums[i].AirPorts)
                    vacuum.SetOutPortStatus(false);
            }
            for (int i = 0; i < CellBlow.Count; i++)
                CellBlow[i].SetOutPortStatus(false);
            System.Threading.Thread.Sleep(200);
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
            AxisLoadPNPY.ServoOn = true;
            AxisLoadPNPZ.ServoOn = true;
            PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            if (!PNPCylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("左气缸无法上移");
            if (!PNPCylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("中气缸无法上移");
            if (!PNPCylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("右气缸无法上移");
            if (err.NoError) err.CollectErrInfo(MotorReset(AxisLoadPNPZ, EnumPointZ.Idle));
            if (err.NoError) err.CollectErrInfo(MotorReset(AxisLoadPNPY, EnumPointY.Pick));
            if (ThisInport(EnumInportName.LoadPNPNGBox).status)
                err.CollectErrInfo("NG盒没有关上。");
            for (int i = 0; i < CELLCOUNT; i++)
            {
                NGBoxCellCount[i] = 0;
                LoadPNPDataStations[i].CellData = null;
            }
            isPicking = false;
        }
        public bool HaveNG
        {
            get
            {
                bool temp = false;
                for (int i = 0; i < CELLCOUNT; i++)
                    temp |= LoadPNPDataStations[i].CellData != null && LoadPNPDataStations[i].CellData.LoadNG;
                return temp;
            }
        }
        public bool AllNG
        {
            get
            {
                bool temp = true;
                for (int i = 0; i < CELLCOUNT; i++)
                    temp &= LoadPNPDataStations[i].CellData == null || LoadPNPDataStations[i].CellData.LoadNG;
                return temp;
            }
        }
        #region Event
        public override void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ProductChangeHandler(sender, e);
            if (ClassCommonSetting.SysParam.CurrentProductParam == null) return;
            foreach (ClassAirUnit cell in CellVacuums.Values)
            {
                cell.AirPorts[(int)EnumVacuumIndex.后真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumLoadPNP.VacuumBackEnable;
                cell.AirPorts[(int)EnumVacuumIndex.中真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumLoadPNP.VacuumCentEnable;
                cell.AirPorts[(int)EnumVacuumIndex.前真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumLoadPNP.VacuumFrontEnable;
            }
            for (int i = 0; i < CELLCOUNT; i++)
                NGBoxCellCount[i] = 0;
        }
        public int ActionCounter = 0;
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            for (int i = 0; i < CELLCOUNT; i++)
                NGBoxCellCount[i] = 0;
            ActionCounter = 0;
        }
        private void NGBoxFullErrorHandler(string source, string message, ErrorDialogResult result)
        {
            if (result == ErrorDialogResult.OK)
                if (ThisInport(EnumInportName.LoadPNPNGBoxFull).status)
                    ClassErrorHandle.ShowError(this.Name, "上料NG盒满，请移走NG料后按OK。", ErrorLevel.Notice, false, false, NGBoxFullErrorHandler);
        }
        protected override void InPortDeActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.LoadPNPNGBox:
                    for (int i = 0; i < CELLCOUNT; i++)
                        NGBoxCellCount[i] = 0;
                    DoUpdateNGBox();
                    break;
                case EnumInportName.LoadPNPNGBoxFull:
                    DoUpdateNGBox();
                    break;
            }
        }
        protected override void InPortActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.LoadPNPNGBox:
                    ClassErrorHandle.ShowError(inPort, "上料NG料盒被移走。移回NG盒后按OK。", ErrorLevel.Notice);
                    DoUpdateNGBox();
                    break;
                case EnumInportName.LoadPNPNGBoxFull:
                    DoUpdateNGBox();
                    break;
            }
        }
        #endregion Event
        #region Data
        public CellCollection<ClassDataStation> LoadPNPDataStations = new CellCollection<ClassDataStation>();
        public CellCollection<ClassDataInfo> NGBoxDatas = new CellCollection<ClassDataInfo>();
        public CellCollection<short> NGBoxCellCount = new CellCollection<short>();
        public string GetDataInfoString()
        {
            return GetDataInfoString(LoadPNPDataStations);
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            LoadPNPDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            LoadPNPDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            LoadPNPDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public enum PNPAction
        {
            Pick,
            Place,
            PlaceNG,
        }
        /// <summary>
        /// PNP电机移到到指定位置
        /// </summary>
        /// <param name="action"></param>
        public ErrorInfoWithPause ActionMove(EnumPointY action)
        {
            ErrorInfoWithPause res = null;
            string temp = "";
            while (true)
            {
                temp = "";
                PNPCylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
                PNPCylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
                if (!PNPCylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp = " 左气缸";
                if (!PNPCylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp = " 中气缸";
                if (!PNPCylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) temp = " 右气缸";
                if (temp != "") //return new ErrorInfoWithPause("气缸上移超时错:" + temp, ErrorLevel.Error);
                {
                    res = WaitAlarmPause("上料PNP移动", "气缸上移超时错: " + temp);
                    if (res != null) return res;
                }
                else
                    break;
            }
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Idle))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
                if (res != null) return res;
            }
            while (!AxisLoadPNPY.MoveTo(action))
            //return DispMotionError(AxisLoadPNPY, action);
            {
                res = DispMotionError(AxisLoadPNPY, action);
                if (res != null) return res;
            }
            return null;
        }
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= LoadPNPDataStations[i].CellData != null;
            return need;
        }
        public void AirControl(EnumCellIndex CellIndex, EnumAirControl status)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return;
            if (status == EnumAirControl.None) return;
            CellVacuums[CellIndex].SetUnitStatus(status == EnumAirControl.Vacuum);
            CellBlow[CellIndex].SetOutPortStatus(status == EnumAirControl.Blow);
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
                    res = status ? "" : " 左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res = status ? "" : " 中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res = status ? "" : " 右真空";
                    break;
            }
            LoadPNPDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = LoadPNPDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = LoadPNPDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = LoadPNPDataStations[EnumCellIndex.右电芯].CellData != null;
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
                return new ErrorInfoWithPause("上料PNP真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        public ErrorInfoWithPause ActionLoadPNPStartPick(CallBackCommonFunc ActionPick, CallBackCommonFunc AfterActionPick)
        {
            ErrorInfoWithPause res = null;
            //PNP Z down
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Pick, true, ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean - 1))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Pick);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Pick);
                if (res != null) return res;
            }
            //Start picking
            if (ActionPick != null) ActionPick();
            //PNP Z up
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Idle))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
                if (res != null) return res;
            }
            //TimeClass.Delay(500);
            if (AfterActionPick != null) AfterActionPick();
            //Check vacuum
            res = CheckVacuumStatus();
            return res;
        }
        public ErrorInfoWithPause ActionLoadPNPStartPlace(CallBackCommonFunc ActionPlace, CallBackCommonFunc AfterActionPlace)
        {
            ErrorInfoWithPause res;
            //Check vacuum
            res = CheckVacuumStatus();
            if (res != null) return res;
            //PNP Z down
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Place, true, ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean + 1))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Place);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Place);
                if (res != null) return res;
            }
            //Start placing
            if (ActionPlace != null) ActionPlace();
            //PNP Z up
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Idle))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
                if (res != null) return res;
            }
            //if (!ClassCommonSetting.CheckTimeOut(() => MotorSafetyCheck.GreaterThanPosition(AxisLoadPNPZ, EnumPointZ.Idle, -5)))
            //    return DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
            //Close air
            AirControl((LoadPNPDataStations[EnumCellIndex.左电芯].CellData != null && LoadPNPDataStations[EnumCellIndex.左电芯].CellData.LoadNG) ? EnumAirControl.Vacuum : EnumAirControl.Close,
              (LoadPNPDataStations[EnumCellIndex.中电芯].CellData != null && LoadPNPDataStations[EnumCellIndex.中电芯].CellData.LoadNG) ? EnumAirControl.Vacuum : EnumAirControl.Close,
              (LoadPNPDataStations[EnumCellIndex.右电芯].CellData != null && LoadPNPDataStations[EnumCellIndex.右电芯].CellData.LoadNG) ? EnumAirControl.Vacuum : EnumAirControl.Close);
            //PNP Y move away
            while (!AxisLoadPNPY.MoveTo(EnumPointY.Pick, false))
            //return DispMotionError(AxisLoadPNPY, EnumPointY.Pick);
            {
                res = DispMotionError(AxisLoadPNPY, EnumPointY.Pick);
                if (res != null) return res;
            }
            if (AfterActionPlace != null) AfterActionPlace();
            return null;
        }
        private bool CheckIfNeedCylinder(int NGCellCount)
        {
            double NGCellHeight = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean * NGCellCount;
            return NGCellHeight < Math.Abs(AxisLoadPNPZ.PointList[EnumPointZ.PlaceNG].Position);
        }
        private int GetMaxCount()
        {
            int max = 0;
            for (int i = 0; i < CELLCOUNT; i++)
                if (max < NGBoxCellCount[i]) max = NGBoxCellCount[i];
            return max;
        }
        public ErrorInfoWithPause ActionLoadPNPStartPlaceNG(CallBackCommonFunc AfterActionPlaceNG)
        {
            ErrorInfoWithPause res;
            bool NeedCylinderDown;
            for (int i = 0; i < CELLCOUNT; i++)
            {
                if (LoadPNPDataStations[i].CellData != null && LoadPNPDataStations[i].CellData.LoadNG)
                {
                    NGBoxCellCount[i]++;
                    NeedCylinderDown = CheckIfNeedCylinder(NGBoxCellCount[i]);
                    if (NeedCylinderDown)
                    {
                        //PNP Z down
                        while (!AxisLoadPNPZ.MoveTo(EnumPointZ.PlaceNG, true, 1 + NGBoxCellCount[i] * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                        //return DispMotionError(AxisLoadPNPZ, EnumPointZ.PlaceNG);
                        {
                            res = DispMotionError(AxisLoadPNPZ, EnumPointZ.PlaceNG);
                            if (res != null) return res;
                        }
                        //Cylinder down
                        if (!PNPCylinder[i].SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                            return new ErrorInfoWithPause("上料PNP气缸下移超时错。", ErrorLevel.Error);
                    }
                    else
                    {
                        //PNP Z down
                        while (!AxisLoadPNPZ.MoveTo(EnumPointZ.PlaceNG, true, -100 + 1 + GetMaxCount() * ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellThickness.Mean))
                        //return DispMotionError(AxisLoadPNPZ, EnumPointZ.PlaceNG);
                        {
                            res = DispMotionError(AxisLoadPNPZ, EnumPointZ.PlaceNG);
                            if (res != null) return res;
                        }
                    }
                    //Open blow
                    AirControl((EnumCellIndex)i, EnumAirControl.Blow);
                    //Delay
                    System.Threading.Thread.Sleep(ClassCommonSetting.SysParam.VacuumDelayTime);
                    //Cylinder up
                    while (!PNPCylinder[i].SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                    //return new ErrorInfoWithPause("上料PNP气缸上移超时错。", ErrorLevel.Error);
                    {
                        res = WaitAlarmPause("上料PNP放NG料", "上料PNP气缸上移超时错");
                        if (res != null) return res;
                    }
                    //Close blow
                    AirControl((EnumCellIndex)i, EnumAirControl.Close);
                }
            }
            //PNP Z up
            while (!AxisLoadPNPZ.MoveTo(EnumPointZ.Idle))
            //return DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
            {
                res = DispMotionError(AxisLoadPNPZ, EnumPointZ.Idle);
                if (res != null) return res;
            }
            DoUpdateNGBox();
            NGBoxFullErrorHandler("NGBox", "", ErrorDialogResult.OK);
            //PNP Y move away
            while (!AxisLoadPNPY.MoveTo(EnumPointY.Pick))
            //return DispMotionError(AxisLoadPNPY, EnumPointY.Pick);
            {
                res = DispMotionError(AxisLoadPNPY, EnumPointY.Pick);
                if (res != null) return res;
            }
            if (AfterActionPlaceNG != null) AfterActionPlaceNG();
            return null;
        }
        public ErrorInfoWithPause CheckNGBoxAvaliable()
        {
            if (ThisInport(EnumInportName.LoadPNPNGBox).status || ThisInport(EnumInportName.LoadPNPNGBoxFull).status)
                return new ErrorInfoWithPause("NG盒没有关上或全满。", ErrorLevel.Alarm, true);
            else
                return null;
        }
        #endregion Action
    }
}