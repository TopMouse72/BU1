using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.MotionSystem;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using Measure;

namespace CDI.Zone
{
    public class ClassZone传送机械手 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            TransPNPX = HardwareAxisName.TransPNPX,//传送机械手X轴
        }
        public enum EnumPointPNPX
        {
            SafeLimit,
            Load,
            Unload,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            TransPNPCyUp = HardwareInportName.TransPNPCyUp,//传送机械手气缸上
            TransPNPCyDown = HardwareInportName.TransPNPCyDown,//传送机械手气缸下
            TransPNPVacSensLoadLeft = HardwareInportName.TransPNPVacSensLoadLeft,//传送机械手进料左吸头真空
            TransPNPVacSensLoadMid = HardwareInportName.TransPNPVacSensLoadMid,//传送机械手进料中吸头真空
            TransPNPVacSensLoadRight = HardwareInportName.TransPNPVacSensLoadRight,//传送机械手进料右吸头真空
            TransPNPVacSensUnloadLeft = HardwareInportName.TransPNPVacSensUnloadLeft,//传送机械手出料左吸头真空
            TransPNPVacSensUnloadMid = HardwareInportName.TransPNPVacSensUnloadMid,//传送机械手出料中吸头真空
            TransPNPVacSensUnloadRight = HardwareInportName.TransPNPVacSensUnloadRight,//传送机械手出料右吸头真空
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            TransPNPCyUp = HardwareOutportName.TransPNPCyUp,//传送机械手气缸上控制
            TransPNPCyDown = HardwareOutportName.TransPNPCyDown,//传送机械手气缸下控制
            TransPNPVacLoadLeft = HardwareOutportName.TransPNPVacLoadLeft,
            TransPNPVacLoadLeftBack = HardwareOutportName.TransPNPVacLoadLeftBack,//传送机械手进料左吸头后腔真空
            TransPNPVacLoadLeftCent = HardwareOutportName.TransPNPVacLoadLeftCent,//传送机械手进料左吸头中腔真空
            TransPNPVacLoadLeftFront = HardwareOutportName.TransPNPVacLoadLeftFront,//传送机械手进料左吸头前腔真空
            TransPNPVacLoadMid = HardwareOutportName.TransPNPVacLoadMid,
            TransPNPVacLoadMidBack = HardwareOutportName.TransPNPVacLoadMidBack,//传送机械手进料中吸头后腔真空
            TransPNPVacLoadMidCent = HardwareOutportName.TransPNPVacLoadMidCent,//传送机械手进料中吸头中腔真空
            TransPNPVacLoadMidFront = HardwareOutportName.TransPNPVacLoadMidFront,//传送机械手进料中吸头前腔真空
            TransPNPVacLoadRight = HardwareOutportName.TransPNPVacLoadRight,
            TransPNPVacLoadRightBack = HardwareOutportName.TransPNPVacLoadRightBack,//传送机械手进料右吸头后腔真空
            TransPNPVacLoadRightCent = HardwareOutportName.TransPNPVacLoadRightCent,//传送机械手进料右吸头中腔真空
            TransPNPVacLoadRightFront = HardwareOutportName.TransPNPVacLoadRightFront,//传送机械手进料右吸头前腔真空
            TransPNPBlowLoad = HardwareOutportName.TransPNPBlowLoad,//传送机械手进料吹气
            TransPNPVacUnloadLeft = HardwareOutportName.TransPNPVacUnloadLeft,
            TransPNPVacUnloadLeftBack = HardwareOutportName.TransPNPVacUnloadLeftBack,//传送机械手出料左吸头后腔真空
            TransPNPVacUnloadLeftCent = HardwareOutportName.TransPNPVacUnloadLeftCent,//传送机械手出料左吸头中腔真空
            TransPNPVacUnloadLeftFront = HardwareOutportName.TransPNPVacUnloadLeftFront,//传送机械手出料左吸头前腔真空
            TransPNPVacUnloadMid = HardwareOutportName.TransPNPVacUnloadMid,
            TransPNPVacUnloadMidBack = HardwareOutportName.TransPNPVacUnloadMidBack,//传送机械手出料中吸头后腔真空
            TransPNPVacUnloadMidCent = HardwareOutportName.TransPNPVacUnloadMidCent,//传送机械手出料中吸头中腔真空
            TransPNPVacUnloadMidFront = HardwareOutportName.TransPNPVacUnloadMidFront,//传送机械手出料中吸头前腔真空
            TransPNPVacUnloadRight = HardwareOutportName.TransPNPVacUnloadRight,
            TransPNPVacUnloadRightBack = HardwareOutportName.TransPNPVacUnloadRightBack,//传送机械手出料右吸头后腔真空
            TransPNPVacUnloadRightCent = HardwareOutportName.TransPNPVacUnloadRightCent,//传送机械手出料右吸头中腔真空
            TransPNPVacUnloadRightFront = HardwareOutportName.TransPNPVacUnloadRightFront,//传送机械手出料右吸头前腔真空
            TransPNPBlowUnload = HardwareOutportName.TransPNPBlowUnload,//传送机械手出料吹气
        }
        public CylinderControl PNPCylinder;
        public CellCollection<ClassAirUnit> LoadCellVacuums = new CellCollection<ClassAirUnit>();
        public CellCollection<ClassAirUnit> UnloadCellVacuums = new CellCollection<ClassAirUnit>();
        public CellCollection<ClassAirPort> LoadCellVacSensor = new CellCollection<ClassAirPort>();
        public CellCollection<ClassAirPort> UnloadCellVacSensor = new CellCollection<ClassAirPort>();
        public ClassAirPort LoadCellBlow, UnloadCellBlow;
        public CAxisBase AxisTransPNPX;
        public ClassZone传送机械手() : base(EnumZoneName.Zone传送机械手.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                LoadCellVacuums.Add(i, new ClassAirUnit());
                UnloadCellVacuums.Add(i, new ClassAirUnit());
                LoadCellVacSensor.Add(i, new ClassAirPort());
                UnloadCellVacSensor.Add(i, new ClassAirPort());
                TransLoadDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
                TransUnloadDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
            }
            LoadCellBlow = new ClassAirPort();
            UnloadCellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisTransPNPX = ThisAxis(EnumAxisName.TransPNPX);
            AxisTransPNPX.AddPoints(typeof(EnumPointPNPX));
            PNPCylinder = new CylinderControl(/*"Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.TransPNPCyDown),
                        ThisOutport(EnumOutportName.TransPNPCyUp),
                        ThisInport(EnumInportName.TransPNPCyDown),
                        ThisInport(EnumInportName.TransPNPCyUp));
            ZoneSettingPanel = new SettingPanelZone传送机械手();
            ZoneManualPanel = new ManualPanelZone传送机械手();

            LoadCellBlow.Port = ThisOutport(EnumOutportName.TransPNPBlowLoad);
            UnloadCellBlow.Port = ThisOutport(EnumOutportName.TransPNPBlowUnload);
            LoadCellVacuums[EnumCellIndex.左电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadLeft);
            LoadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadLeftBack);
            LoadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadLeftCent);
            LoadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadLeftFront);
            LoadCellVacuums[EnumCellIndex.中电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadMid);
            LoadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadMidBack);
            LoadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadMidCent);
            LoadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadMidFront);
            LoadCellVacuums[EnumCellIndex.右电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadRight);
            LoadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadRightBack);
            LoadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadRightCent);
            LoadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacLoadRightFront);
            UnloadCellVacuums[EnumCellIndex.左电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadLeft);
            UnloadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadLeftBack);
            UnloadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadLeftCent);
            UnloadCellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadLeftFront);
            UnloadCellVacuums[EnumCellIndex.中电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadMid);
            UnloadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadMidBack);
            UnloadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadMidCent);
            UnloadCellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadMidFront);
            UnloadCellVacuums[EnumCellIndex.右电芯].MainPort.Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadRight);
            UnloadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadRightBack);
            UnloadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadRightCent);
            UnloadCellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.TransPNPVacUnloadRightFront);
            LoadCellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.TransPNPVacSensLoadLeft);
            LoadCellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.TransPNPVacSensLoadMid);
            LoadCellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.TransPNPVacSensLoadRight);
            UnloadCellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.TransPNPVacSensUnloadLeft);
            UnloadCellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.TransPNPVacSensUnloadMid);
            UnloadCellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.TransPNPVacSensUnloadRight);
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            base.ResetOutPort();
            for (int i = 0; i < LoadCellVacuums.Count; i++)
            {
                LoadCellVacuums[i].MainPort.SetOutPortStatus(false);
                foreach (ClassAirPort vacuum in LoadCellVacuums[i].AirPorts)
                    vacuum.SetOutPortStatus(false);
            }
            for (int i = 0; i < UnloadCellVacuums.Count; i++)
            {
                UnloadCellVacuums[i].MainPort.SetOutPortStatus(false);
                foreach (ClassAirPort vacuum in UnloadCellVacuums[i].AirPorts)
                    vacuum.SetOutPortStatus(false);
            }
            TimeClass.Delay(200);
            LoadCellBlow.SetOutPortStatus(false);
            UnloadCellBlow.SetOutPortStatus(false);
            if (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                return new ErrorInfoWithPause("气缸上升错误", ErrorLevel.Error);
            else
                return null;
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisTransPNPX.ServoOn = true;
            AxisTransPNPX.SetZero();
            if (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("气缸无法上移");
            if (err.NoError)
            {
                AxisTransPNPX.HomeFinish = true;
                AxisTransPNPX.NeedToCheckSafety = false;
                double speed = AxisTransPNPX.DefaultSpeed;
                AxisTransPNPX.DefaultSpeed = Math.Abs(AxisTransPNPX.JogSpeed);
                AxisTransPNPX.StepMove(-80);
                AxisTransPNPX.DefaultSpeed = speed;
                AxisTransPNPX.NeedToCheckSafety = true;
                err.CollectErrInfo(AxisTransPNPX.Home(EnumPointPNPX.Unload.ToString()));
            }
            for (int i = 0; i < CELLCOUNT; i++)
            {
                TransLoadDataStations[i].CellData = null;
                TransUnloadDataStations[i].CellData = null;
            }
        }
        #region Event
        public override void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ProductChangeHandler(sender, e);
            if (ClassCommonSetting.SysParam.CurrentProductParam == null) return;
            foreach (ClassAirUnit cell in LoadCellVacuums.Values)
            {
                cell.AirPorts[(int)EnumVacuumIndex.后真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPLoad.VacuumBackEnable;
                cell.AirPorts[(int)EnumVacuumIndex.中真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPLoad.VacuumCentEnable;
                cell.AirPorts[(int)EnumVacuumIndex.前真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPLoad.VacuumFrontEnable;
            }
            foreach (ClassAirUnit cell in UnloadCellVacuums.Values)
            {
                cell.AirPorts[(int)EnumVacuumIndex.后真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPUnload.VacuumBackEnable;
                cell.AirPorts[(int)EnumVacuumIndex.中真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPUnload.VacuumCentEnable;
                cell.AirPorts[(int)EnumVacuumIndex.前真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumTransPNPUnload.VacuumFrontEnable;
            }
        }
        protected override void InPortActive(string inPort)
        {
        }

        protected override void InPortDeActive(string inPort)
        {
        }
        #endregion Event
        #region Data
        public CellCollection<ClassDataStation> TransLoadDataStations = new CellCollection<ClassDataStation>();
        public CellCollection<ClassDataStation> TransUnloadDataStations = new CellCollection<ClassDataStation>();
        public string GetDataInfoString()
        {
            return "Load " + GetDataInfoString(TransLoadDataStations) + " Unload " + GetDataInfoString(TransUnloadDataStations);
        }
        public void AddLoadDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            TransLoadDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            TransLoadDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            TransLoadDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        public void AddUnloadDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            TransUnloadDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            TransUnloadDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            TransUnloadDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
            {
                need |= TransLoadDataStations[i].CellData != null;
                need |= TransUnloadDataStations[i].CellData != null;
            }
            return need;
        }
        public ErrorInfoWithPause ActionMove(EnumPointPNPX point, bool NeedWait = true)
        {
            ErrorInfoWithPause res = null;
            //气缸上移
            while (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("传送机械手气缸上移超时错。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("传送PNP移动", "传送机械手气缸上移超时错");
                if (res != null) return res;
            }
            //电机移动
            if (!MotorSafetyCheck.InPositionRange(AxisTransPNPX, point, 0, 0.005))
                while (!AxisTransPNPX.MoveTo(point, NeedWait))
                //return DispMotionError(AxisTransPNPX, point);
                {
                    res = DispMotionError(AxisTransPNPX, point);
                    if (res != null) return res;
                }
            return null;
        }
        public void AirLoadControl(EnumCellIndex CellIndex, EnumAirControl status)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return;
            if (status == EnumAirControl.None) return;
            LoadCellVacuums[CellIndex].SetUnitStatus(status == EnumAirControl.Vacuum);
            LoadCellBlow.SetOutPortStatus(status == EnumAirControl.Blow);
        }
        public void AirUnloadControl(EnumCellIndex CellIndex, EnumAirControl status)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return;
            if (status == EnumAirControl.None) return;
            UnloadCellVacuums[CellIndex].SetUnitStatus(status == EnumAirControl.Vacuum);
            UnloadCellBlow.SetOutPortStatus(status == EnumAirControl.Blow);
        }
        public void AirLoadControl(EnumAirControl LeftVacuum, EnumAirControl MiddleVacuum, EnumAirControl RightVacuum)
        {
            AirLoadControl(EnumCellIndex.左电芯, LeftVacuum);
            AirLoadControl(EnumCellIndex.中电芯, MiddleVacuum);
            AirLoadControl(EnumCellIndex.右电芯, RightVacuum);
        }
        public void AirUnloadControl(EnumAirControl LeftVacuum, EnumAirControl MiddleVacuum, EnumAirControl RightVacuum)
        {
            AirUnloadControl(EnumCellIndex.左电芯, LeftVacuum);
            AirUnloadControl(EnumCellIndex.中电芯, MiddleVacuum);
            AirUnloadControl(EnumCellIndex.右电芯, RightVacuum);
        }
        public string CheckLoadVacuumStatus(EnumCellIndex CellIndex)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return "";
            bool status = LoadCellVacSensor[CellIndex].GetInPortStatus();
            string res = "";
            switch (CellIndex)
            {
                case EnumCellIndex.左电芯:
                    res += status ? "" : " 上料左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res += status ? "" : " 上料中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res += status ? "" : " 上料右真空";
                    break;
            }
            TransLoadDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public string CheckUnloadVacuumStatus(EnumCellIndex CellIndex)
        {
            if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) return "";
            bool status = UnloadCellVacSensor[CellIndex].GetInPortStatus();
            string res = "";
            switch (CellIndex)
            {
                case EnumCellIndex.左电芯:
                    res += status ? "" : " 下料左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res += status ? "" : " 下料中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res += status ? "" : " 下料右真空";
                    break;
            }
            TransUnloadDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public string CheckVacuumStatus()
        {
            bool loadleft = TransLoadDataStations[EnumCellIndex.左电芯].CellData != null;
            bool loadmiddle = TransLoadDataStations[EnumCellIndex.中电芯].CellData != null;
            bool loadright = TransLoadDataStations[EnumCellIndex.右电芯].CellData != null;
            bool unloadleft = TransUnloadDataStations[EnumCellIndex.左电芯].CellData != null;
            bool unloadmiddle = TransUnloadDataStations[EnumCellIndex.中电芯].CellData != null;
            bool unloadright = TransUnloadDataStations[EnumCellIndex.右电芯].CellData != null;
            string res = "";
            res = ClassCommonSetting.CheckTimeOut(() =>
            {
                string vac = "";
                if (loadleft) vac += CheckLoadVacuumStatus(EnumCellIndex.左电芯);
                if (loadmiddle) vac += CheckLoadVacuumStatus(EnumCellIndex.中电芯);
                if (loadright) vac += CheckLoadVacuumStatus(EnumCellIndex.右电芯);
                if (unloadleft) vac += CheckUnloadVacuumStatus(EnumCellIndex.左电芯);
                if (unloadmiddle) vac += CheckUnloadVacuumStatus(EnumCellIndex.中电芯);
                if (unloadright) vac += CheckUnloadVacuumStatus(EnumCellIndex.右电芯);
                return vac;
            });
            if (res != "")
                return "传送PNP真空错误:" + res;
            else
                return "";
        }
        private void AfterPickAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "TransPNPAfterPick");
        }
        public ErrorInfoWithPause ActionStartLoad(out bool AllPick, CallBackCommonFunc ActionTransLoad, CallBackCommonFunc AfterActionTransLoad)
        {
            ErrorInfoWithPause res = ActionMove(EnumPointPNPX.Load);
            if (res != null)
            {
                AllPick = false;
                return res;
            }
            //Cylinder down
            while (!PNPCylinder.SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
            {
                //res = new ErrorInfoWithPause("传送机械手气缸下移超时错。", ErrorLevel.Error);
                res = WaitAlarmPause("传送PNP取料", "传送机械手气缸下移超时错");
                if (res != null)
                {
                    AllPick = false;
                    return res;
                }
            }
            if (ActionTransLoad != null) ActionTransLoad();
            //Cylinder up
            while (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
            {
                //res = new ErrorInfoWithPause("传送机械手气缸上移超时错。", ErrorLevel.Error);
                //AllPick = false;
                //return res;
                res = WaitAlarmPause("传送PNP取料", "传送机械手气缸上移超时错");
                if (res != null)
                {
                    AllPick = false;
                    return res;
                }
            }
            if (AfterActionTransLoad != null) AfterActionTransLoad();
            //Check vacuum
            string vac = CheckVacuumStatus();
            AllPick = vac.Trim() == "";
            if (!AllPick) return new ErrorInfoWithPause(vac.Trim(), ErrorLevel.Alarm, true);
            return null;
        }
        public ErrorInfoWithPause ActionStartUnload(CallBackCommonFunc ActionTransUnload, CallBackCommonFunc AfterActionTransUnload)
        {
            ErrorInfoWithPause res;
            res = ActionMove(EnumPointPNPX.Unload);
            if (res != null) return res;
            //Check vacuum
            string vac = CheckVacuumStatus();
            bool AllPick = vac.Trim() == "";
            if (!AllPick) return new ErrorInfoWithPause("放料前真空检测错误: " + vac, ErrorLevel.Alarm, true);
            //Cylinder down
            while (!PNPCylinder.SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("传送机械手气缸下移超时错。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("传送PNP放料", "传送机械手气缸下移超时错");
                if (res != null) return res;
            }
            if (ActionTransUnload != null) ActionTransUnload();
            //Cylinder up
            while (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("传送机械手气缸上移超时错。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("传送PNP放料", "传送机械手气缸上移超时错");
                if (res != null) return res;
            }
            if (AfterActionTransUnload != null) AfterActionTransUnload();
            return null;
        }
        #endregion Action
    }
}