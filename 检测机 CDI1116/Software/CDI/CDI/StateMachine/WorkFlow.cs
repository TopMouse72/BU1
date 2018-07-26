using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    public enum WFContFrom
    {
        StartButton,
        StopButton,
        PauseButton,
        GRR,
        GeneralAirError,
        DoorSensor,
        Alarm,
    }
    public enum EnumRunLockMode
    {
        //手动模式。打到手动模式，流程暂停，开始按钮无效。
        手动,
        //正常运行模式。门禁触发暂停，暂停时开始按钮无效，门关上才有效。
        自动,
    }
    /// <summary>
    /// Run mode
    /// </summary>
    public enum EnumWorkMode
    {
        /// <summary>
        /// Run without parts to check work flow and motion
        /// </summary>
        空跑,
        /// <summary>
        /// Normal run.
        /// </summary>
        正常,
    }
    /// <summary>
    /// Load mode
    /// </summary>
    public enum EnumLoadMode
    {
        手动,
        自动,
    }
    public enum EnumUnloadMode
    {
        正常,
        全OK,
        全NG,
    }
    public enum WFDebugMode
    {
        Auto,
        Manual,
    }
    public class ClassWorkFlow : IOSubscriber, ISystemSubscriber
    {
        private ClassWorkZones workzones;
        private CDIMainStateMachine _CDIMainSM;
        private LoadPNPLoadStateMachine _LoadPNPLoadSM;
        private SortingPNPWorkStateMachine _SortingPNPWorkSM;
        private TransPNPWorkStateMachine _TransPNPWorkSM;
        private UnloadPNPWorkStateMachine _UnloadPNPWorkSM;
        public bool IsManual = false;
        public bool WorkFlowEnable = true;
        public bool IsGRR = false;
        public int ProductCount = -1;
        public EnumWorkMode WorkMode = EnumWorkMode.正常;
        public EnumUnloadMode UnloadMode = EnumUnloadMode.正常;
        public EnumLoadMode LoadMode
        {
            get { return ClassCommonSetting.SysParam.LoadMode; }
            set { ClassCommonSetting.SysParam.LoadMode = value; ClassCommonSetting.SysParam.SaveParameter(); }
        }
        public WFDebugMode DebugMode = WFDebugMode.Auto;
        public int GetCell = 0;
        private bool _FeedNewPart = true;
        public ClassTimeUsage TimeUsage = new ClassTimeUsage();
        public bool FeedNewPart
        {
            get { return _FeedNewPart && (ProductCount <= 0 || GetCell < ProductCount); }
            set { _FeedNewPart = value; }
        }
        private List<IWorkFlowControl> _workFlowControl = new List<IWorkFlowControl>();
        private static ClassWorkFlow _workflow = null;
        public static ClassWorkFlow Instance
        {
            get
            {
                if (_workflow == null) _workflow = new ClassWorkFlow();
                return _workflow;
            }
        }
        public bool IsRunning
        {
            get { return _CDIMainSM != null && _CDIMainSM.IsRunning; }
        }
        public CDIMainStateMachine CDIMainSM
        {
            get { return _CDIMainSM; }
        }
        public LoadPNPLoadStateMachine LoadPNPLoadSM
        {
            get { return _LoadPNPLoadSM; }
        }
        public SortingPNPWorkStateMachine SortingPNPWorkSM
        {
            get { return _SortingPNPWorkSM; }
        }
        public TransPNPWorkStateMachine TransPNPWorkSM
        {
            get { return _TransPNPWorkSM; }
        }
        public UnloadPNPWorkStateMachine UnloadPNPWorkSM
        {
            get { return _UnloadPNPWorkSM; }
        }
        public void AddGUI(IWorkFlowControl gui)
        {
            if (_workFlowControl.Contains(gui)) return;
            _workFlowControl.Add(gui);
        }
        public void RemoveGUI(IWorkFlowControl gui)
        {
            _workFlowControl.Remove(gui);
        }
        public void WorkFlowInit()
        {
            workzones = ClassWorkZones.Instance;
            _CDIMainSM = new CDIMainStateMachine(this);
            _LoadPNPLoadSM = new LoadPNPLoadStateMachine(this);
            _SortingPNPWorkSM = new SortingPNPWorkStateMachine(this);
            _TransPNPWorkSM = new TransPNPWorkStateMachine(this);
            _UnloadPNPWorkSM = new UnloadPNPWorkStateMachine(this);
            foreach (ClassBaseWorkZone zone in workzones.ToArray())
            {
                zone.SubscribeMeToResponseEvent(this);
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            //DisplayStatus("接收到事件：" + e.eventName);
            //switch ((EnumEventName)Enum.Parse(typeof(EnumEventName), e.eventName))
            //{
            //    case EnumEventName.NewCellReady:
            //    case EnumEventName.LoadPNPPickFinish:
            //        workzones.WorkZone上料传送工作区域.ActionStartLoad(IsManual);
            //        break;
            //    case EnumEventName.NewCellLoad:
            //        workzones.WorkZone上料机械手工作区域.ActionPNPPick();
            //        break;
            //}
        }

        public void ResetHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public void StartHandler(BaseClass sender, StateEventArgs e)
        {
            if (!IsGRR) ProductCount = -1;
            BaseStateMachine.StateMachineLogFile = new LogFile(string.Format("{0}SM{1:yyyyMMdd HHmmss}.LOG", LogFile.DefaultLogPath, DateTime.Now), true, LogFile.Level.All, true);
            _CDIMainSM.StartHandler(sender, e);
            Statistic.IsWorkFlowRunning = true;
        }

        public void StopHandler(BaseClass sender, StateEventArgs e)
        {
            _CDIMainSM.StopHandler(sender, null);
            _LoadPNPLoadSM.StopHandler(sender, null);
            _SortingPNPWorkSM.StopHandler(sender, null);
            _TransPNPWorkSM.StopHandler(sender, null);
            _UnloadPNPWorkSM.StopHandler(sender, null);
            BaseStateMachine.StateMachineLogFile.Stop();
            Statistic.IsWorkFlowRunning = false;
        }

        public void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            _CDIMainSM.PauseHandler(sender, null);
            _LoadPNPLoadSM.PauseHandler(sender, null);
            _SortingPNPWorkSM.PauseHandler(sender, null);
            _TransPNPWorkSM.PauseHandler(sender, null);
            _UnloadPNPWorkSM.PauseHandler(sender, null);
        }
        public void ResumeHandler(BaseClass sender, StateEventArgs e)
        {
            _CDIMainSM.ResumeHandler(sender, null);
            _LoadPNPLoadSM.ResumeHandler(sender, null);
            _SortingPNPWorkSM.ResumeHandler(sender, null);
            _TransPNPWorkSM.ResumeHandler(sender, null);
            _UnloadPNPWorkSM.ResumeHandler(sender, null);
        }

        public void EStopHandler(BaseClass sender, StateEventArgs e)
        {
            _CDIMainSM.EStopHandler(sender, null);
            _LoadPNPLoadSM.EStopHandler(sender, null);
            _SortingPNPWorkSM.EStopHandler(sender, null);
            _TransPNPWorkSM.EStopHandler(sender, null);
            _UnloadPNPWorkSM.EStopHandler(sender, null);
            Statistic.IsWorkFlowRunning = false;
        }

        public void RetryHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {

        }

        public void ModeChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public void LoginHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public void LogoutHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public void ProgramExitHandler(BaseClass sender, StateEventArgs e)
        {
        }
    }
}