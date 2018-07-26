using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
    public class ClassZone顶封边定位 : ClassBaseWorkZone
    {
        public enum EnumAxisName
        {
            TopAlignSide = HardwareAxisName.TopAlignSide,//顶封边定位侧向定位轴
            TopAlignBottom = HardwareAxisName.TopAlignBottom,//顶封边定位底部定位轴
            TopAlignXSide = HardwareAxisName.TopAlignXSide,//顶封边定位X轴
            TopAlignZClamp = HardwareAxisName.TopAlignZClamp,//顶封边定位Z夹紧轴
            TopAlignTop = HardwareAxisName.TopAlignTop,// 顶封边定位头部调整轴
        }
        public enum EnumPointAlign
        {
            Release,
            Clamp,
        }
        public enum EnumPointX
        {
            Idle,
            Step,
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            TopAlignCyLeftUp = HardwareInportName.TopAlignCyLeftUp,//顶封边定位左气缸上
            TopAlignCyLeftDown = HardwareInportName.TopAlignCyLeftDown,//顶封边定位左气缸下
            TopAlignCyMidUp = HardwareInportName.TopAlignCyMidUp,//顶封边定位中气缸上
            TopAlignCyMidDown = HardwareInportName.TopAlignCyMidDown,//顶封边定位中气缸下
            TopAlignCyRightUp = HardwareInportName.TopAlignCyRightUp,//顶封边定位右气缸上
            TopAlignCyRightDown = HardwareInportName.TopAlignCyRightDown,//顶封边定位右气缸下
            TopAlignVacSensLeft = HardwareInportName.TopAlignVacSensLeft,//顶封边定位左吸头真空
            TopAlignVacSensMid = HardwareInportName.TopAlignVacSensMid,//顶封边定位中吸头真空
            TopAlignVacSensRight = HardwareInportName.TopAlignVacSensRight,//顶封边定位右吸头真空
            TopAlignSoftCySensLeft = HardwareInportName.TopAlignSoftCySensLeft,//顶封边定位左柔性气缸
            TopAlignSoftCySensMid = HardwareInportName.TopAlignSoftCySensMid,//顶封边定位中柔性气缸
            TopAlignSoftCySensRight = HardwareInportName.TopAlignSoftCySensRight,//顶封边定位右柔性气缸
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            TopAlignCyLeftUp = HardwareOutportName.TopAlignCyLeftUp,//顶封边定位左气缸上控制
            TopAlignCyLeftDown = HardwareOutportName.TopAlignCyLeftDown,//顶封边定位左气缸下控制
            TopAlignCyMidUp = HardwareOutportName.TopAlignCyMidUp,//顶封边定位中气缸上控制
            TopAlignCyMidDown = HardwareOutportName.TopAlignCyMidDown,//顶封边定位中气缸下控制
            TopAlignCyRightUp = HardwareOutportName.TopAlignCyRightUp,//顶封边定位右气缸上控制
            TopAlignCyRightDown = HardwareOutportName.TopAlignCyRightDown,//顶封边定位右气缸下控制
            TopAlignVacLeft = HardwareOutportName.TopAlignVacLeft,//顶封边定位左吸头真空
            TopAlignVacMid = HardwareOutportName.TopAlignVacMid,//顶封边定位中吸头真空
            TopAlignVacRight = HardwareOutportName.TopAlignVacRight,//顶封边定位右吸头真空
            TopAlignBlow = HardwareOutportName.TopAlignBlow,//顶封边定位吹气
            TopAlignSoftCyLeft = HardwareOutportName.TopAlignSoftCyLeft,//顶封边定位柔性左气缸控制
            TopAlignSoftCyMid = HardwareOutportName.TopAlignSoftCyMid,//顶封边定位柔性中气缸控制
            TopAlignSoftCyRight = HardwareOutportName.TopAlignSoftCyRight,//顶封边定位柔性右气缸控制
        }
        public CellCollection<CylinderControl> Cylinder = new CellCollection<CylinderControl>();
        public CellCollection<ClassAirPort> TopAlignCellVacuums = new CellCollection<ClassAirPort>();
        public CellCollection<ClassAirPort> CellVacSensor = new CellCollection<ClassAirPort>();
        public ClassAirPort CellBlow;
        public CAxisBase AxisTopAlignSide, AxisTopAlignBottom, AxisTopAlignXSide, AxisTopAlignZClamp, AxisTopAlignTop;

        public ClassZone顶封边定位() : base(EnumZoneName.Zone顶封边定位.ToString())
        {
            for (int i = 0; i < CELLCOUNT; i++)
            {
                TopAlignCellVacuums.Add((EnumCellIndex)i, new ClassAirPort());
                CellVacSensor.Add((EnumCellIndex)i, new ClassAirPort());
                TopAlignDataStations.Add((EnumCellIndex)i, new ClassDataStation(((EnumCellIndex)i).ToString()));
            }
            CellBlow = new ClassAirPort();
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName));
            AxisTopAlignBottom = ThisAxis(EnumAxisName.TopAlignBottom); AxisTopAlignBottom.AddPoints(typeof(EnumPointAlign));
            AxisTopAlignSide = ThisAxis(EnumAxisName.TopAlignSide); AxisTopAlignSide.AddPoints(typeof(EnumPointAlign));
            AxisTopAlignTop = ThisAxis(EnumAxisName.TopAlignTop); AxisTopAlignTop.AddPoints(typeof(EnumPointAlign));
            AxisTopAlignXSide = ThisAxis(EnumAxisName.TopAlignXSide); AxisTopAlignXSide.AddPoints(typeof(EnumPointX));
            AxisTopAlignZClamp = ThisAxis(EnumAxisName.TopAlignZClamp); AxisTopAlignZClamp.AddPoints(typeof(EnumPointAlign));

            Cylinder.Add(EnumCellIndex.左电芯, new CylinderControl(/*"左电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.TopAlignCyLeftDown),
                        ThisOutport(EnumOutportName.TopAlignCyLeftUp),
                        ThisInport(EnumInportName.TopAlignCyLeftDown),
                        ThisInport(EnumInportName.TopAlignCyLeftUp)));
            Cylinder.Add(EnumCellIndex.中电芯, new CylinderControl(/*"中电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.TopAlignCyMidDown),
                        ThisOutport(EnumOutportName.TopAlignCyMidUp),
                        ThisInport(EnumInportName.TopAlignCyMidDown),
                        ThisInport(EnumInportName.TopAlignCyMidUp)));
            Cylinder.Add(EnumCellIndex.右电芯, new CylinderControl(/*"右电芯压紧气缸", "下降", "上升",*/
                        ThisOutport(EnumOutportName.TopAlignCyRightDown),
                        ThisOutport(EnumOutportName.TopAlignCyRightUp),
                        ThisInport(EnumInportName.TopAlignCyRightDown),
                        ThisInport(EnumInportName.TopAlignCyRightUp)));
            TopAlignCellVacuums[EnumCellIndex.左电芯].Port = ThisOutport(EnumOutportName.TopAlignVacLeft);
            TopAlignCellVacuums[EnumCellIndex.中电芯].Port = ThisOutport(EnumOutportName.TopAlignVacMid);
            TopAlignCellVacuums[EnumCellIndex.右电芯].Port = ThisOutport(EnumOutportName.TopAlignVacRight);
            CellVacSensor[EnumCellIndex.左电芯].Port = ThisInport(EnumInportName.TopAlignVacSensLeft);
            CellVacSensor[EnumCellIndex.中电芯].Port = ThisInport(EnumInportName.TopAlignVacSensMid);
            CellVacSensor[EnumCellIndex.右电芯].Port = ThisInport(EnumInportName.TopAlignVacSensRight);
            CellBlow.Port = ThisOutport(EnumOutportName.TopAlignBlow);
            ZoneSettingPanel = new SettingPanelZone顶封边定位();
            ZoneManualPanel = new ManualPanelZone顶封边定位();
            AxisTopAlignXSide.HomeLimitReturnDis = 10;
        }

        protected override void Reset(ClassErrorHandle err)
        {
            AxisTopAlignBottom.ServoOn = true;
            AxisTopAlignSide.ServoOn = true;
            AxisTopAlignTop.ServoOn = true;
            AxisTopAlignXSide.ServoOn = true;
            AxisTopAlignZClamp.ServoOn = true;
            Cylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("左气缸无法上移");
            if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("中气缸无法上移");
            if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT)) err.CollectErrInfo("右气缸无法上移");
            if (err.NoError) err.CollectErrInfo(AxisTopAlignZClamp.Home());
            if (err.NoError)
            {
                AxisTopAlignTop.HomeFinish = true;
                AxisTopAlignTop.MoveTo(-10000);
            }
            if (err.NoError) err.CollectErrInfo(AxisTopAlignTop.Home());
            if (err.NoError) err.CollectErrInfo(AxisTopAlignXSide.Home(EnumPointX.Idle.ToString()));
            if (err.NoError) err.CollectErrInfo(AxisTopAlignBottom.Home());
            if (err.NoError) err.CollectErrInfo(AxisTopAlignSide.Home());
            for (int i = 0; i < CELLCOUNT; i++)
                TopAlignDataStations[i].CellData = null;
        }
        #region Event
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            isTopAlignFree = true;
        }
        public override ErrorInfoWithPause ResetOutPort()
        {
            ErrorInfoWithPause res = null;
            string err = "";
            base.ResetOutPort();
            for (int i = 0; i < TopAlignCellVacuums.Count; i++)
                TopAlignCellVacuums[i].SetOutPortStatus(false);
            CellBlow.SetOutPortStatus(false);
            Cylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_UP, 0, false);
            Cylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_UP, 0, false);
            ThisOutport(EnumOutportName.TopAlignSoftCyLeft).SetOutput(true);
            ThisOutport(EnumOutportName.TopAlignSoftCyMid).SetOutput(true);
            ThisOutport(EnumOutportName.TopAlignSoftCyRight).SetOutput(true);
            if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "左气缸 ";
            if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "中气缸 ";
            if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                err += "右气缸 ";
            if (err != "")
                res = new ErrorInfoWithPause("气缸上升错误: " + err.Trim(), ErrorLevel.Error);
            return res;
        }
        protected override void InPortActive(string inPort)
        {
        }

        protected override void InPortDeActive(string inPort)
        {
        }
        #endregion Event
        #region Data
        public CellCollection<ClassDataStation> TopAlignDataStations = new CellCollection<ClassDataStation>();
        double[] offset
        {
            get { return ClassCommonSetting.GetTopAlignZClampOffset(ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellWidth.Mean, ClassCommonSetting.SysParam.CurrentProductParam.TopClampWidth); }
        }
        public string GetDataInfoString()
        {
            return GetDataInfoString(TopAlignDataStations);
        }
        public void AddDisp(IDataDisp leftDisp, IDataDisp midDisp, IDataDisp rightDisp)
        {
            TopAlignDataStations[EnumCellIndex.左电芯].AddDisp(leftDisp);
            TopAlignDataStations[EnumCellIndex.中电芯].AddDisp(midDisp);
            TopAlignDataStations[EnumCellIndex.右电芯].AddDisp(rightDisp);
        }
        #endregion Data
        #region Action
        public bool isTopAlignFree = true;
        public int ClampCount = 2;
        public ErrorInfoWithPause ActionStartClamp()
        {
            if (ClassWorkFlow.Instance.IsGRR || ClassCommonSetting.SysParam.CurrentProductParam.ClampDisable) return null;
            string err = "";
            ErrorInfoWithPause res = null;
            //Z松开
            //if (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Release))
            //    return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
            for (int i = 0; i < offset.Length; i++)
            {
                //X走位
                while (!AxisTopAlignXSide.MoveTo(EnumPointX.Idle, true, offset[i]))
                //return DispMotionError(AxisTopAlignXSide, "移到第" + (i + 1).ToString() + "次夹紧位置");
                {
                    res = DispMotionError(AxisTopAlignXSide, "移到第" + (i + 1).ToString() + "次夹紧位置");
                    if (res != null) return res;
                }
                //Z夹紧
                double zoffset = 0;
                if (ClassCommonSetting.SysParam.CurrentProductParam.BackSideUp)
                    zoffset = ClassCommonSetting.SysParam.CurrentProductParam.TopHeight;
                while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Clamp, true, zoffset))
                //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Clamp);
                {
                    res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Clamp);
                    if (res != null) return res;
                }
                //气缸下
                while (true)
                {
                    err = "";
                    for (int k = 0; k < CELLCOUNT; k++)
                        if (TopAlignDataStations[k].CellData != null) Cylinder[k].SetCylinderState(CYLIND_DOWN, 0, false);
                    if (TopAlignDataStations[EnumCellIndex.左电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                            err += " 左气缸";
                    if (TopAlignDataStations[EnumCellIndex.中电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                            err += " 中气缸";
                    if (TopAlignDataStations[EnumCellIndex.右电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT))
                            err += " 右气缸";
                    if (err != "") //return new ErrorInfoWithPause("顶封位气缸下移错误:" + err, ErrorLevel.Error);
                    {
                        res = WaitAlarmPause("顶封边定位", "顶封位气缸下移超时错: " + err);
                        if (res != null) return res;
                    }
                    else
                        break;
                }

                //TimeClass.Delay(100);
                //气缸上
                while (true)
                {
                    err = "";
                    for (int k = 0; k < CELLCOUNT; k++)
                        if (TopAlignDataStations[k].CellData != null) Cylinder[k].SetCylinderState(CYLIND_UP, 0, false);
                    System.Threading.Thread.Sleep(100);
                    while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Clamp, true, zoffset - 1))
                    //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
                    {
                        res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
                        if (res != null) return res;
                    }
                    if (TopAlignDataStations[EnumCellIndex.左电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                            err += " 左气缸";
                    if (TopAlignDataStations[EnumCellIndex.中电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                            err += " 中气缸";
                    if (TopAlignDataStations[EnumCellIndex.右电芯].CellData != null)
                        if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_UP, ClassErrorHandle.TIMEOUT))
                            err += " 右气缸";
                    if (err != "") //return new ErrorInfoWithPause("顶封位气缸上移错误:" + err, ErrorLevel.Error);
                    {
                        res = WaitAlarmPause("顶封边定位", "顶封位气缸上移超时错: " + err);
                        if (res != null) return res;
                    }
                    else
                        break;
                }
                //Z松开
            }
            //Z松开
            ThisOutport(EnumOutportName.TopAlignSoftCyLeft).SetOutput(false);
            ThisOutport(EnumOutportName.TopAlignSoftCyMid).SetOutput(false);
            ThisOutport(EnumOutportName.TopAlignSoftCyRight).SetOutput(false);
            //if (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Release, false)) return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
            return null;
        }
        public bool HavePart()
        {
            bool need = false;
            for (int i = 0; i < CELLCOUNT; i++)
                need |= TopAlignDataStations[i].CellData != null;
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
            TopAlignDataStations[CellIndex].CellData.isPickingError = !status;
            return res;
        }
        public ErrorInfoWithPause CheckVacuumStatus()
        {
            bool left = TopAlignDataStations[EnumCellIndex.左电芯].CellData != null;
            bool middle = TopAlignDataStations[EnumCellIndex.中电芯].CellData != null;
            bool right = TopAlignDataStations[EnumCellIndex.右电芯].CellData != null;
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
                return new ErrorInfoWithPause("顶封位真空错误:" + res, ErrorLevel.Alarm, true);
            else
                return null;
        }
        public ErrorInfoWithPause ActionAlign()
        {
            ErrorInfoWithPause res = null;
            ThisOutport(EnumOutportName.TopAlignSoftCyLeft).SetOutput(true);
            ThisOutport(EnumOutportName.TopAlignSoftCyMid).SetOutput(true);
            ThisOutport(EnumOutportName.TopAlignSoftCyRight).SetOutput(true);
            //侧定位
            double offsetTemp = 0;
            double mainlenprod, mainlencali;
            offsetTemp = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellWidth.Mean - ClassCommonSetting.SysParam.Products[CALIBPROD].CellDataSpec.CellWidth.Mean;
            offsetTemp /= 2;
            while (!AxisTopAlignSide.MoveTo(EnumPointAlign.Clamp, false, -offsetTemp))
            //return DispMotionError(AxisTopAlignSide, EnumPointAlign.Clamp);
            {
                res = DispMotionError(AxisTopAlignSide, EnumPointAlign.Clamp);
                if (res != null) return res;
            }
            //底部定位
            mainlenprod = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellLength.Mean - ClassCommonSetting.SysParam.CurrentProductParam.TopSealHeight;
            mainlencali = ClassCommonSetting.SysParam.Products[CALIBPROD].CellDataSpec.CellLength.Mean - ClassCommonSetting.SysParam.Products[CALIBPROD].TopSealHeight;
            offsetTemp = -1 * (mainlenprod - mainlencali);
            while (!AxisTopAlignBottom.MoveTo(EnumPointAlign.Clamp, false, offsetTemp))
            //return DispMotionError(AxisTopAlignBottom, EnumPointAlign.Clamp);
            {
                res = DispMotionError(AxisTopAlignBottom, EnumPointAlign.Clamp);
                if (res != null) return res;
            }
            //Z松开
            while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Release, false))
            //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
                if (res != null) return res;
            }
            //顶部定位
            if (!(ClassWorkFlow.Instance.IsGRR || ClassCommonSetting.SysParam.CurrentProductParam.ClampDisable))
            {
                while (!AxisTopAlignTop.MoveTo(EnumPointAlign.Clamp))
                //return DispMotionError(AxisTopAlignTop, EnumPointAlign.Clamp);
                {
                    res = DispMotionError(AxisTopAlignTop, EnumPointAlign.Clamp);
                    if (res != null) return res;
                }
            }
            //X走位
            while (!AxisTopAlignXSide.MoveTo(EnumPointX.Idle, true, offset[0]))
            //return DispMotionError(AxisTopAlignXSide, EnumPointX.Idle);
            {
                res = DispMotionError(AxisTopAlignXSide, EnumPointX.Idle);
                if (res != null) return res;
            }
            //double zoffset = 0;
            //if (ClassCommonSetting.SysParam.CurrentProductParam.BackSideUp)
            //    zoffset = ClassCommonSetting.SysParam.CurrentProductParam.TopHeight;
            //if (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Clamp, true, zoffset - 1))
            //    return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Clamp);
            AxisTopAlignBottom.WaitStop(ClassErrorHandle.TIMEOUT);
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                for (int i = 0; i < CELLCOUNT; i++)
                    if (TopAlignDataStations[i].CellData != null)
                        TopAlignCellVacuums[i].SetOutPortStatus(true);
            //Check vacuum
            //ErrorInfoWithPause temp;
            //temp = CheckVacuumStatus();
            //if (temp != null) return temp;
            return null;
        }
        public ErrorInfoWithPause ActionRelease()
        {
            ErrorInfoWithPause res = null;
            //Z松开
            ThisOutport(EnumOutportName.TopAlignSoftCyLeft).SetOutput(false);
            ThisOutport(EnumOutportName.TopAlignSoftCyMid).SetOutput(false);
            ThisOutport(EnumOutportName.TopAlignSoftCyRight).SetOutput(false);
            while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Release, false))
            //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
                if (res != null) return res;
            }
            //顶部松开
            while (!AxisTopAlignTop.MoveTo(EnumPointAlign.Release, false))
            //return DispMotionError(AxisTopAlignTop, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignTop, EnumPointAlign.Release);
                if (res != null) return res;
            }
            //底部松开
            while (!AxisTopAlignBottom.MoveTo(EnumPointAlign.Release, false))
            //return DispMotionError(AxisTopAlignBottom, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignBottom, EnumPointAlign.Release);
                if (res != null) return res;
            }
            //侧松开
            while (!AxisTopAlignSide.MoveTo(EnumPointAlign.Release, false))
            //return DispMotionError(AxisTopAlignSide, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignSide, EnumPointAlign.Release);
                if (res != null) return res;
            }
            TimeClass time = new TimeClass();
            time.StartAlarmClock(ClassErrorHandle.TIMEOUT);
            while (!ThisInport(EnumInportName.TopAlignSoftCySensLeft).status
                || !ThisInport(EnumInportName.TopAlignSoftCySensMid).status
                || !ThisInport(EnumInportName.TopAlignSoftCySensRight).status)
            {
                System.Threading.Thread.Sleep(1);
                if (time.TimeOut)
                //return new ErrorInfoWithPause("柔性气缸不动作。", ErrorLevel.Error);
                {
                    res = WaitAlarmPause("顶封边定位", "柔性气缸不动作");
                    if (res != null) return res;
                    time.StartAlarmClock(ClassErrorHandle.TIMEOUT);
                }
            }
            time.StopAlarmClock();
            //X走位
            AxisTopAlignZClamp.WaitStop(ClassErrorHandle.TIMEOUT);
            while (!AxisTopAlignXSide.MoveTo(EnumPointX.Idle, false, offset[0]))
            //return DispMotionError(AxisTopAlignXSide, EnumPointX.Idle);
            {
                res = DispMotionError(AxisTopAlignXSide, EnumPointX.Idle);
                if (res != null) return res;
            }
            //AxisTopAlignTop.WaitStop(ClassErrorInfo.TIMEOUT);
            //AxisTopAlignBottom.WaitStop(ClassErrorInfo.TIMEOUT);
            //AxisTopAlignSide.WaitStop(ClassErrorInfo.TIMEOUT);
            for (int i = 0; i < CELLCOUNT; i++)
                TopAlignCellVacuums[i].SetOutPortStatus(false);
            return null;
        }
        public ErrorInfoWithPause ActionStartTopAlignWorkFlow(bool CheckVacuum = true)
        {
            if (!HavePart()) return null;
            ErrorInfoWithPause res;
            //if (CheckVacuum)
            //{
            //    res = CheckVacuumStatus();
            //    if (res != null) return res;
            //}
            res = ActionAlign();
            if (res != null) return res;
            res = ActionStartClamp();
            if (res != null) return res;
            res = ActionRelease();
            return res;
        }
        public ErrorInfoWithPause AllCyDown()
        {
            ErrorInfoWithPause res = null;
            string temp = "";
            while (true)
            {
                if (TopAlignDataStations[EnumCellIndex.左电芯].CellData != null)
                {
                    Cylinder[EnumCellIndex.左电芯].SetCylinderState(CYLIND_DOWN, 0, false);
                }
                if (TopAlignDataStations[EnumCellIndex.中电芯].CellData != null)
                {
                    Cylinder[EnumCellIndex.中电芯].SetCylinderState(CYLIND_DOWN, 0, false);
                }
                if (TopAlignDataStations[EnumCellIndex.右电芯].CellData != null)
                {
                    Cylinder[EnumCellIndex.右电芯].SetCylinderState(CYLIND_DOWN, 0, false);
                }
                temp = "";
                if (TopAlignDataStations[EnumCellIndex.左电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.左电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 左气缸";
                if (TopAlignDataStations[EnumCellIndex.中电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.中电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 中气缸";
                if (TopAlignDataStations[EnumCellIndex.右电芯].CellData != null)
                    if (!Cylinder[EnumCellIndex.右电芯].WaitCylinderState(CYLIND_DOWN, ClassErrorHandle.TIMEOUT)) temp += " 右气缸";
                if (temp != "") //res = new ErrorInfoWithPause("顶封气缸下移错误:" + temp, ErrorLevel.Error);
                {
                    res = WaitAlarmPause("顶封边定位", "顶封气缸下移超时错: " + temp);
                    if (res != null) return res;
                }
                else
                    break;
            }
            return res;
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
                if (temp != "") //res = new ErrorInfoWithPause("顶封气缸上移错误:" + temp, ErrorLevel.Error);
                {
                    res = WaitAlarmPause("顶封边定位", "顶封气缸上移超时错: " + temp);
                    if (res != null) return res;
                }
                else
                    break;
            }
            return res;
        }
        private bool isCylinderTest = false;
        public ErrorInfoWithPause ActionCylinderTest()
        {

            ErrorInfoWithPause res = null;
            isCylinderTest = !isCylinderTest;
            for (int i = 0; i < CELLCOUNT; i++)
                if (TopAlignDataStations[i].CellData == null)
                    TopAlignDataStations[i].CellData = ClassDataInfo.NewCellData();
            double zoffset = 0;
            if (ClassCommonSetting.SysParam.CurrentProductParam.BackSideUp)
                zoffset = ClassCommonSetting.SysParam.CurrentProductParam.TopHeight;
            if (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Clamp, true, zoffset))
                while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Clamp))
                //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Clamp);
                {
                    res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Clamp);
                    if (res != null) return res;
                }
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
            while (!AxisTopAlignZClamp.MoveTo(EnumPointAlign.Release))
            //return DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
            {
                res = DispMotionError(AxisTopAlignZClamp, EnumPointAlign.Release);
                if (res != null) return res;
            }
            return res;
        }
        #endregion Action
    }
}