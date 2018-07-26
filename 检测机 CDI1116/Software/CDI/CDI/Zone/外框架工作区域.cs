using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using System.Windows.Forms;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;

namespace CDI.Zone
{
    /// <summary>
    /// Frame work zone。
    /// </summary>
    public class ClassZone外框架 : ClassBaseWorkZone
    {
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            FrameButtonStart = HardwareInportName.FrameButtonStart,//外框Start按钮
            FrameButtonPause = HardwareInportName.FrameButtonPause,//外框Pause按钮
            FrameButtonStop = HardwareInportName.FrameButtonStop,//外框Stop按钮
            FrameButtonEmergency = HardwareInportName.FrameButtonEmergency,//外框急停按钮
            FrameButtonEmergency2 = HardwareInportName.FrameButtonEmergency2,
            FrameAirPressSens = HardwareInportName.FrameAirPressSens,//外框气压
            FrameVacuumSens = HardwareInportName.FrameVacuumSens,//外框真空
            FrameDoorOpen = HardwareInportName.FrameDoorOpen,//外框门禁
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            FrameButtonLightStart = HardwareOutportName.FrameButtonLightStart,//外框Start按钮灯
            FrameButtonLightPause = HardwareOutportName.FrameButtonLightPause,//外框Pause按钮灯
            FrameButtonLightStop = HardwareOutportName.FrameButtonLightStop,//外框Stop按钮灯
            FrameLightGreen = HardwareOutportName.FrameLightGreen,//外框三色灯绿色
            FrameLightYellow = HardwareOutportName.FrameLightYellow,//外框三色灯黄色
            FrameLightRed = HardwareOutportName.FrameLightRed,//外框三色灯红色
            FrameBuzz = HardwareOutportName.FrameBuzz,//外框三色灯蜂鸣器
            FramePowerOn = HardwareOutportName.FramePowerOn,//外框电源开
            FramePowerOff = HardwareOutportName.FramePowerOff,//外框电源关
            FrameKMSwitch = HardwareOutportName.FrameKMSwitch,
        }
        public ClassZone外框架() : base(EnumZoneName.Zone外框架.ToString()) { }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(null, typeof(EnumInportName), typeof(EnumOutportName));
            ZoneSettingPanel = new SettingPanelZone外框架();
            ZoneManualPanel = new ManualPanelZone外框架();
            if (ThisInport(EnumInportName.FrameButtonEmergency).status || ThisInport(EnumInportName.FrameButtonEmergency2).status)
                MessageBox.Show("急停按钮被按下。");
            else
            {
                ThisOutport(EnumOutportName.FrameBuzz).SetOutput(false);
                ThisOutport(EnumOutportName.FrameKMSwitch).SetOutput(false);
                CheckGeneralAir();
            }
        }
        public ErrorInfoWithPause CheckGeneralAir()
        {
            if (SystemHardware.instance.IsSimulation) return null;
            string err = "";
            ErrorInfoWithPause Error = null;
            if (!ThisInport(EnumInportName.FrameAirPressSens).status) err += "总气压" + Environment.NewLine;
            if (!ThisInport(EnumInportName.FrameVacuumSens).status) err += "总真空" + Environment.NewLine;
            if (err != "")
                Error = new ErrorInfoWithPause(err, ErrorLevel.Alarm);
            return Error;
        }
        #region Event
        private void BuzzSoundTimer(object sender, EventArgs e)
        {
            ThisOutport(EnumOutportName.FrameBuzz).SetOutput(!ThisOutport(EnumOutportName.FrameBuzz).status);
        }

        protected override void InPortActive(string inPort)
        {
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.FrameButtonStart:
                    if (ClassWorkFlow.Instance.WorkFlowEnable)
                    {
                        if (ThisInport(EnumInportName.FrameDoorOpen).status/* && ClassWorkFlow.Instance.DebugMode == WFDebugMode.Auto*/)
                        {
                            ClassErrorHandle.ShowError(this.Name, "不能启动或恢复流程运行：外框门被打开。", ErrorLevel.Alarm);
                            return;
                        }
                        //else if (ClassWorkFlow.Instance.DebugMode == WFDebugMode.Auto)
                        //    //自动模式下才能恢复流程
                        if (_isPausing)
                        {
                            ActionResumeWorkFlow(WFContFrom.StartButton);
                        }
                        else if (_isRunning)
                            BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, "流程正在运行中，无法开始。"));
                        else
                        {
                            if (MessageBox.Show($"当前进料方式设置为{ClassWorkFlow.Instance.LoadMode},如果正确则点“是”继续，否则点“否”则取消开始流程。", "开始流程", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                return;
                            else
                                ActionStartWorkFlow(WFContFrom.StartButton);
                        }
                    }
                    break;
                case EnumInportName.FrameButtonPause:
                    if (_isRunning)
                        ActionPauseWorkFlow(WFContFrom.PauseButton);
                    break;
                case EnumInportName.FrameButtonStop:
                    ActionStopWorkFlow(WFContFrom.StopButton);
                    break;
                case EnumInportName.FrameButtonEmergency:
                case EnumInportName.FrameButtonEmergency2:
                    DoEmergency();
                    break;
                case EnumInportName.FrameDoorOpen:
                    if (_isRunning)
                    {
                        //if (ClassWorkFlow.Instance.DebugMode == WFDebugMode.Auto)
                        //    //自动模式下开门才需要报警
                        ClassErrorHandle.ShowError(this.Name, "外框门被打开。", ErrorLevel.Notice, false, true);
                    }
                    else
                        ClassErrorHandle.ShowError(this.Name, "外框门被打开。", ErrorLevel.Notice, false);
                    if (_safetyStatus != null) _safetyStatus(ThisInport(EnumInportName.FrameDoorOpen));
                    break;
            }
        }
        protected override void InPortDeActive(string inPort)
        {
            string err = "";
            switch ((EnumInportName)Enum.Parse(typeof(EnumInportName), inPort))
            {
                case EnumInportName.FrameAirPressSens:
                    err = " 总气压";
                    break;
                case EnumInportName.FrameVacuumSens:
                    err = " 总真空";
                    break;
                case EnumInportName.FrameDoorOpen:
                    if (_safetyStatus != null) _safetyStatus(ThisInport(EnumInportName.FrameDoorOpen));
                    break;
            }
            if (err != "")
            {
                //ActionStopWorkFlow("GeneralAirError");
                if (_isRunning)
                {
                    //ActionPauseWorkFlow(WFContFrom.GeneralAirError);
                    ClassErrorHandle.ShowError("气路", "气路异常:" + err, ErrorLevel.Alarm, false, true);
                }
                else
                    ClassErrorHandle.ShowError("气路", "设备异常：" + err, ErrorLevel.Error);
            }
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            ClassErrorHandle.isWorkFlowRunning = true;
            _isRunning = true;
            LightStartButton();
            _isRunning = true;
            _isPausing = false;

            //Statistic.WFStatus = WorkFlowStatus.待料;
            string strFrom = "流程开始";
            if (e != null) strFrom += "(" + e.eventInfo + ")";
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.待料, strFrom, true);
            Statistic.WorkFlowTimerStart(!ClassWorkFlow.Instance.IsGRR);
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", strFrom);
        }
        public override void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            base.PauseHandler(sender, e);
            LightPauseButton();
            _isPausing = true;
            //Statistic.WFStatus = WorkFlowStatus.待料;
            string strFrom = "流程暂停";
            if (e != null) strFrom += "(" + e.eventInfo + ")";
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.待料, strFrom, true);
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", strFrom);
        }
        public override void ResumeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ResumeHandler(sender, e);
            LightStartButton();
            _isPausing = false;
            //Statistic.WFStatus = WorkFlowStatus.运行;
            string strFrom = "流程恢复";
            if (e != null) strFrom += "(" + e.eventInfo + ")";
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.运行, strFrom, true);
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", strFrom);
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ClassErrorHandle.isWorkFlowRunning = false;
            LightStopButton();
            _isRunning = false;
            //Statistic.WFStatus = WorkFlowStatus.空闲;
            string strFrom = "流程停止";
            if (e != null) strFrom += "(" + e.eventInfo + ")";
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.空闲, strFrom, Statistic.WFStatus != WorkFlowStatus.空闲);
            if (Statistic.WFStatus == WorkFlowStatus.空闲) Statistic.WorkFlowTimerStart(false);
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", strFrom);
        }
        public override void EStopHandler(BaseClass sender, StateEventArgs e)
        {
            base.EStopHandler(sender, e);
            LightStopButton();
            _isRunning = false;
        }
        public override void ProgramExitHandler(BaseClass sender, StateEventArgs e)
        {
            BeepDisable = true;
            base.ProgramExitHandler(sender, e);
            ClassWorkZones.Instance.WorkZone外框架.SwitchKM(false);
            ThisInport(EnumInportName.FrameButtonStop).SetOutput(false);
        }
        #endregion Event
        #region Action
        public void SwitchKM(bool FrontKM)
        {
            ThisOutport(EnumOutportName.FrameKMSwitch).SetOutput(FrontKM);
        }
        public void HookErrorEvent()
        {
            ClassErrorHandle.NewErrorEvent += NewErrorEvent;
            ClassErrorHandle.ErrorItemCountChangeEvent += ErrorItemCountChangeEvent;
            ClassErrorHandle.ErrorStop += ClassErrorInfo_ErrorStop;
        }

        private void ClassErrorInfo_ErrorStop()
        {
            LightStopButton();
        }
        private int ErrorCount;
        private void ErrorItemCountChangeEvent(int ErrorItemCount)
        {
            ErrorCount = ErrorItemCount;
            if (ErrorItemCount == 0)
            {
                ThisOutport(EnumOutportName.FrameLightRed).SetOutput(false);
            }
        }
        private bool BeepDisable = false;
        private void NewErrorEvent(object sender, ErrorLevel level)
        {
            ThisOutport(EnumOutportName.FrameLightRed).SetOutput(true);
            switch (level)
            {
                case ErrorLevel.Alarm:
                case ErrorLevel.Notice:

                    Beep(250, 250, 2);
                    Beep(250, 750);
                    Beep(250, 250, 2);
                    Beep(250, 0);
                    break;
                case ErrorLevel.Error:
                case ErrorLevel.Fatal:
                    Beep(250, 60);
                    Beep(250, 120);
                    Beep(250, 60);
                    Beep(250, 120);
                    Beep(250, 60);
                    Beep(250, 0);
                    break;
            }
        }

        TimeClass timer = new TimeClass();
        private void Beep(int SoundTime, int SilenceTime, int Repeat = 1)
        {
            if (BeepDisable) return;
            for (int i = 0; i < Repeat; i++)
            {
                ThisOutport(EnumOutportName.FrameBuzz).SetOutput(true);
                TimeClass.Delay(SoundTime);
                ThisOutport(EnumOutportName.FrameBuzz).SetOutput(false);
                TimeClass.Delay(SilenceTime);
            }
        }
        private void LightButtonOff()
        {
            ThisOutport(EnumOutportName.FrameButtonLightPause).SetOutput(false);
            ThisOutport(EnumOutportName.FrameButtonLightStart).SetOutput(false);
            ThisOutport(EnumOutportName.FrameButtonLightStop).SetOutput(false);
            ThisOutport(EnumOutportName.FrameLightRed).SetOutput(false);
            ThisOutport(EnumOutportName.FrameLightGreen).SetOutput(false);
            ThisOutport(EnumOutportName.FrameLightYellow).SetOutput(false);
        }
        private void LightStopButton()
        {
            LightButtonOff();
            ThisOutport(EnumOutportName.FrameButtonLightStop).SetOutput(true);
        }
        private void LightStartButton()
        {
            LightButtonOff();
            ThisOutport(EnumOutportName.FrameButtonLightStart).SetOutput(true);
            ThisOutport(EnumOutportName.FrameLightGreen).SetOutput(true);
        }
        private void LightPauseButton()
        {
            LightButtonOff();
            ThisOutport(EnumOutportName.FrameButtonLightPause).SetOutput(true);
            ThisOutport(EnumOutportName.FrameLightYellow).SetOutput(true);
        }
        public bool CheckBeforeStart()
        {
            if (ThisInport(EnumInportName.FrameButtonEmergency).status || ThisInport(EnumInportName.FrameButtonEmergency2).status)
            {
                ClassErrorHandle.ShowError(this.Name, "急停按钮没有松开。", ErrorLevel.Error);
                return false;
            }
            //foreach (HardwareAxisName name in Enum.GetValues(typeof(HardwareAxisName)))
            //    if (!SystemHardware.instance.GetAxis(name).ServoOn)
            //        axisOff += name.ToString() + ", ";
            //if (axisOff != "")
            //{
            //    ClassErrorInfo.ShowError(this.Name, "有电机掉电: " + axisOff, ErrorLevel.Error);
            //    return false;
            //}
            //if (ThisInport(EnumInportName.FrameDoorOpen).status)
            //{
            //    ClassErrorInfo.ShowError(this.Name, "外框门被打开。", ErrorLevel.Error);
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// 开始工作流程。
        /// 发送Start系统事件。
        /// </summary>
        /// <param name="FromThis">执行源</param>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public string ActionStartWorkFlow(WFContFrom FromThis)
        {
            if (ThisInport(EnumInportName.FrameDoorOpen).status/* && ClassWorkFlow.Instance.DebugMode == WFDebugMode.Auto*/)
            {
                ClassErrorHandle.ShowError(this.Name, "不能启动或恢复流程运行：外框门被打开。", ErrorLevel.Alarm);
                return "DoorOpen";
            }
            if (ClassCommonSetting.SysParam.CurrentProduct == CALIBPROD)
            {
                BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, "当前产品“calibration”是用于标定的虚拟产品，不能启动生产流程。"));
                return "WrongProduct";
            }
            ErrorInfoWithPause err = CheckGeneralAir();
            if (err != null) return err.Message;
            if (ErrorCount > 0)
            {
                BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, "启动生产流程之前，所有错误和提示必须全部处理完成！"));
                return "ErrorNotHandle";
            }
            bool CanStart = false;
            BaseForm.DoInvokeRequired(MainForm.instance, () =>
                CanStart = MessageBox.Show(MainForm.instance, "启动生产流程之前，需手动检查并清除设备内（包括NG盒和可能的掉料）所有物料并确认。确认后按“确定”按钮，否则按“取消”按钮。", "启动流程", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK);
            if (!CanStart) return "NotClear";
            string res = ActionSystemReset();
            if (res == "")
            {
                ClassWorkFlow.Instance.IsGRR = FromThis == WFContFrom.GRR;// true;
                CommonFunction.SysPublisher.notifyStartEventSubscribers(this, new StateEventArgs("启动", "来自" + FromThis.ToString()));
            }
            else
            {
                ClassErrorHandle.ShowError("系统复位", res, ErrorLevel.Error);
            }
            return res;
        }
        /// <summary>
        /// 停止工作流程。
        /// 发送Stop系统事件。
        /// </summary>
        /// <param name="FromThis">执行源</param>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public string ActionStopWorkFlow(WFContFrom FromThis)
        {
            CommonFunction.SysPublisher.notifyStopEventSubscribers(this, new StateEventArgs("停止", "来自" + FromThis.ToString()));
            return "";
        }
        /// <summary>
        /// 暂停工作流程。
        /// 发送Pause系统事件。
        /// </summary>
        /// <param name="FromThis">执行源</param>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public string ActionPauseWorkFlow(WFContFrom FromThis)
        {
            CommonFunction.SysPublisher.notifyPauseEventSubscribers(this, new StateEventArgs("暂停", "来自" + FromThis.ToString()));
            return "";
        }
        /// <summary>
        /// 恢复工作流程。
        /// 发送Resume系统事件。
        /// </summary>
        /// <param name="FromThis">执行源</param>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public string ActionResumeWorkFlow(WFContFrom FromThis)
        {
            CommonFunction.SysPublisher.notifyResumeEventSubscribers(this, new StateEventArgs("恢复", "来自" + FromThis.ToString()));
            return "";
        }
        /// <summary>
        /// 急停处理。
        /// 发送EStop系统事件。
        /// </summary>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public string DoEmergency()
        {
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFLow", "流程因急停停止");
            if (_isRunning)
                ClassErrorHandle.ShowError(this.Name, "急停按钮按下，流程因急停停止", ErrorLevel.Alarm);
            else
                ClassErrorHandle.ShowError(this.Name, "急停按钮按下", ErrorLevel.Alarm);

            CommonFunction.SysPublisher.notifyEStopEventSubscribers(this, new StateEventArgs("急停", ""));

            return "";
        }
        public string ActionSystemReset()
        {
            CommonFunction.SysPublisher.notifyStopEventSubscribers(this, null);
            CommonFunction.SysPublisher.notifyResetEventSubscribers(this, null);
            string res;
            if (ClassWorkZones.Instance.ResetAllZones())
            {
                res = ClassWorkZones.Instance.DoSystemReset();
            }
            else
                res = "初始化失败";
            return res;
        }

        protected override void Reset(ClassErrorHandle err)
        {

        }
        #endregion Action
    }
}