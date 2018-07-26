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
    public class ClassZone下料机械手 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            UnloadPNPY = HardwareAxisName.UnloadPNPY,//下料机械手Y轴
        }
        public enum EnumPoint
        {
            Pick,
            Buffer,
            Place,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            UnloadPNPCyUp = HardwareInportName.UnloadPNPCyUp,//下料机械手气缸上
            UnloadPNPCyDown = HardwareInportName.UnloadPNPCyDown,//下料机械手气缸下
            UnloadPNPVacSensLeft = HardwareInportName.UnloadPNPVacSensLeft,//下料机械手左吸头真空
            UnloadPNPVacSensMid = HardwareInportName.UnloadPNPVacSensMid,//下料机械手中吸头真空
            UnloadPNPVacSensRight = HardwareInportName.UnloadPNPVacSensRight,//下料机械手右吸头真空
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            UnloadPNPCyUp = HardwareOutportName.UnloadPNPCyUp,//下料机械手气缸上控制
            UnloadPNPCyDown = HardwareOutportName.UnloadPNPCyDown,//下料机械手气缸下控制
            UnloadPNPVacLeft = HardwareOutportName.UnloadPNPVacLeft,
            UnloadPNPVacLeftBack = HardwareOutportName.UnloadPNPVacLeftBack,//下料机械手左吸头后腔真空
            UnloadPNPVacLeftCent = HardwareOutportName.UnloadPNPVacLeftCent,//下料机械手左吸头中腔真空
            UnloadPNPVacLeftFront = HardwareOutportName.UnloadPNPVacLeftFront,//下料机械手左吸头前腔真空
            UnloadPNPVacMid = HardwareOutportName.UnloadPNPVacMid,
            UnloadPNPVacMidBack = HardwareOutportName.UnloadPNPVacMidBack,//下料机械手中吸头后腔真空
            UnloadPNPVacMidCent = HardwareOutportName.UnloadPNPVacMidCent,//下料机械手中吸头中腔真空
            UnloadPNPVacMidFront = HardwareOutportName.UnloadPNPVacMidFront,//下料机械手中吸头前腔真空
            UnloadPNPVacRight = HardwareOutportName.UnloadPNPVacRight,
            UnloadPNPVacRightBack = HardwareOutportName.UnloadPNPVacRightBack,//下料机械手右吸头后腔真空
            UnloadPNPVacRightCent = HardwareOutportName.UnloadPNPVacRightCent,//下料机械手右吸头中腔真空
            UnloadPNPVacRightFront = HardwareOutportName.UnloadPNPVacRightFront,//下料机械手右吸头前腔真空
            UnloadPNPBlow = HardwareOutportName.UnloadPNPBlow,//下料机械手吹气
        }
        public CylinderControl PNPCylinder;
        public CellCollection<ClassAirUnit> CellVacuums = new CellCollection<ClassAirUnit>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public ClassAirPort CellBlow;
        public CAxisBase AxisUnloadPNPY;
        public ClassZone下料机械手() : base(EnumZoneName.Zone下料机械手.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                CellVacuums.Add(i, new ClassAirUnit());
                CellVacSensor.Add(i, new ClassAirPort());
                UnloadPNPDataStations.Add(i, new ClassDataStation(((EnumCellIndex)i).ToString()));
            }
            CellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisUnloadPNPY = ThisAxis(EnumAxisName.UnloadPNPY);
            AxisUnloadPNPY.AddPoints(typeof(EnumPoint));
            PNPCylinder = new CylinderControl(/*"Z气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.UnloadPNPCyDown),
                        ThisOutport(EnumOutportName.UnloadPNPCyUp),
                        ThisInport(EnumInportName.UnloadPNPCyDown),
                        ThisInport(EnumInportName.UnloadPNPCyUp));
            CellVacuums[EnumCellIndex.左电芯].MainPort.Port =
                ThisOutport(EnumOutportName.UnloadPNPVacLeft);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacLeftBack);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacLeftCent);
            CellVacuums[EnumCellIndex.左电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacLeftFront);
            CellVacuums[EnumCellIndex.中电芯].MainPort.Port =
                ThisOutport(EnumOutportName.UnloadPNPVacMid);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacMidBack);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacMidCent);
            CellVacuums[EnumCellIndex.中电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacMidFront);
            CellVacuums[EnumCellIndex.右电芯].MainPort.Port =
                ThisOutport(EnumOutportName.UnloadPNPVacRight);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.后真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacRightBack);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.中真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacRightCent);
            CellVacuums[EnumCellIndex.右电芯].AirPorts[(int)EnumVacuumIndex.前真空].Port =
                ThisOutport(EnumOutportName.UnloadPNPVacRightFront);
            CellBlow.Port = ThisOutport(EnumOutportName.UnloadPNPBlow);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.UnloadPNPVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.UnloadPNPVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.UnloadPNPVacSensRight);
            ZoneSettingPanel = new SettingPanelZone下料机械手();
            ZoneManualPanel = new ManualPanelZone下料机械手();
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            base.ResetOutPort();
            for (int i = 0; i < CELLCOUNT; i++)
                UnloadPNPDataStations[i].CellData = null;
            for (int i = 0; i < CellVacuums.Count; i++)
            {
                CellVacuums[i].MainPort.SetOutPortStatus(false);
                foreach (ClassAirPort vacuum in CellVacuums[i].AirPorts)
                    vacuum.SetOutPortStatus(false);
            }
            CellBlow.SetOutPortStatus(false);
            TimeClass.Delay(200);
            if (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                return new ErrorInfoWithPause("气缸上升错误", ErrorLevel.Error);
            else
                return null;
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisUnloadPNPY.ServoOn = true;
            AxisUnloadPNPY.SetZero();
            if (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("气缸无法上移");
            if (err.NoError)
            {
                AxisUnloadPNPY.HomeFinish = true;
                AxisUnloadPNPY.NeedToCheckSafety = false;
                double temp = AxisUnloadPNPY.DefaultSpeed;
                AxisUnloadPNPY.DefaultSpeed = Math.Abs(AxisUnloadPNPY.JogSpeed);
                AxisUnloadPNPY.StepMove(-30);
                AxisUnloadPNPY.DefaultSpeed = temp;
                AxisUnloadPNPY.NeedToCheckSafety = true;
                err.CollectErrInfo(MotorReset(AxisUnloadPNPY, EnumPoint.Place));
            }
        }
        #region Event
        public override void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ProductChangeHandler(sender, e);
            if (ClassCommonSetting.SysParam.CurrentProductParam == null) return;
            foreach (ClassAirUnit cell in CellVacuums.Values)
            {
                cell.AirPorts[(int)EnumVacuumIndex.后真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumUnloadPNP.VacuumBackEnable;
                cell.AirPorts[(int)EnumVacuumIndex.中真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumUnloadPNP.VacuumCentEnable;
                cell.AirPorts[(int)EnumVacuumIndex.前真空].PortEnable = ClassCommonSetting.SysParam.CurrentProductParam.VacuumUnloadPNP.VacuumFrontEnable;
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
        public CellCollection<ClassDataStation> UnloadPNPDataStations = new CellCollection<ClassDataStation>();
        public string GetDataInfoString()
        {
            return GetDataInfoString(UnloadPNPDataStations);
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            UnloadPNPDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            UnloadPNPDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            UnloadPNPDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= UnloadPNPDataStations[i].CellData != null;
            return need;
        }
        /// <summary>
        /// PNP电机移到到指定位置
        /// </summary>
        /// <param name="action"></param>
        public ErrorInfoWithPause ActionMove(EnumPoint action, bool NeedWait = true)
        {
            ErrorInfoWithPause res;
            if (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                return new ErrorInfoWithPause("下料PNP气缸上移超时错。", ErrorLevel.Error);
            while (!AxisUnloadPNPY.MoveTo(action, NeedWait))
            //return DispMotionError(AxisUnloadPNPY, action);
            {
                res = DispMotionError(AxisUnloadPNPY, action);
                if (res != null) return res;
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
                    res += status ? "" : " 左真空";
                    break;
                case EnumCellIndex.中电芯:
                    res += status ? "" : " 中真空";
                    break;
                case EnumCellIndex.右电芯:
                    res += status ? "" : " 右真空";
                    break;
            }
            UnloadPNPDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = UnloadPNPDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = UnloadPNPDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = UnloadPNPDataStations[EnumCellIndex.右电芯].CellData != null;
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
                return new ErrorInfoWithPause("下料PNP真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        public ErrorInfoWithPause ActionUnloadPNPStartPick(CallBackCommonFunc ActionLoadOutPick, CallBackCommonFunc AfterActionLoadOutPick)
        {
            ErrorInfoWithPause res;
            //Cylinder down
            while (!PNPCylinder.SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("下料PNP气缸下移超时错。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("下料PNP取料", "下料PNP气缸下移超时错");
                if (res != null) return res;
            }
            //Start picking
            if (ActionLoadOutPick != null) ActionLoadOutPick();
            //Cylinder up
            while (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("下料PNP气缸上移超时错。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("下料PNP取料", "下料PNP气缸上移超时错");
                if (res != null) return res;
            }
            if (AfterActionLoadOutPick != null) AfterActionLoadOutPick();
            //Check vacuum
            ErrorInfoWithPause temp = CheckVacuumStatus();
            return temp;
        }
        public ErrorInfoWithPause ActionUnloadPNPStartPlace(CallBackCommonFunc ActionLoadOutPlace, CallBackCommonFunc AfterActionLoadOutPlace)
        {
            ErrorInfoWithPause res;
            //Check vacuum
            res = CheckVacuumStatus();
            if (res != null) return res;
            //Cylinder down
            while (!PNPCylinder.SetCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("下料PNP气缸下移错误。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("下料PNP放料", "下料PNP气缸下移错误");
                if (res != null) return res;
            }
            if (ActionLoadOutPlace != null) ActionLoadOutPlace();
            //Cylinder up
            while (!PNPCylinder.SetCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
            //return new ErrorInfoWithPause("下料PNP气缸上移错误。", ErrorLevel.Error);
            {
                res = WaitAlarmPause("下料PNP放料", "下料PNP气缸上移错误");
                if (res != null) return res;
            }
            if (AfterActionLoadOutPlace != null) AfterActionLoadOutPlace();
            return null;
        }
        #endregion Action
    }
}