using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule.Event;
using CDI.GUI;
using CDI.Zone;
using CDI.StateMachine;
using Colibri.CommonModule.State;

namespace CDI
{
    public partial class MainForm : Colibri.CommonModule.Forms.MainForm
    {
        private static MainForm _main;
        public static MainForm instance
        {
            get
            {
                if (_main == null) _main = new MainForm();
                return _main;
            }
        }
        private ClassWorkZones zones;
        private ClassWorkFlow workflow;
        private ManualOperationPanel manual;
        private ConfigurationPanel setting;
        private ProductPanel product;
        private AutoPanel auto;
        private CalibrationPanel calibration;
        private MonitorPanel monitor;
        public MainForm() : base("CDI检测机系统", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
        {
            InitializeComponent();
            //Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "Main", "Run version: " + Version);
            //初始化硬件
            SplashProcessInfo = "初始化硬件...";
            HardwarePool.SystemHardware.instance.Init();
            HardwarePool.SystemHardware.instance.Open();
            SerialProtocalCollection.instance.ConnectProtocol();
            SplashProcessInfo = "加载参数，打开CDIVision...";
            ClassCommonSetting.CommonInit();
            this.LogOutTime = GetLogOutTime(ClassCommonSetting.SysParam.LogOutTime);

            //初始化工作区域
            SplashProcessInfo = "初始化工作区域...";
            zones = ClassWorkZones.Instance;
            zones.AllZoneInit();
            SplashProcessInfo = "初始化工作流程...";
            workflow = ClassWorkFlow.Instance;
            workflow.WorkFlowInit();
            //初始化主界面
            SplashProcessInfo = "初始化界面...";
            auto = AutoPanel.Instance;
            setting = new ConfigurationPanel();
            manual = new ManualOperationPanel();
            product = new ProductPanel();
            calibration = new CalibrationPanel();
            monitor = new MonitorPanel();
            AddInterface(PanelType.AutoFace, auto);
            AddInterface(PanelType.SettingFace, setting);
            AddInterface(PanelType.ManualFace, manual);
            AddInterface(PanelType.ProductFace, product);
            AddInterface(PanelType.DebugFace, calibration);
            AddInterface(PanelType.MonitorFace, monitor);
            ClassCommonSetting.SysParam.AddParaInterface(product);
            ClassCommonSetting.SysParam.CurrentProductParam.SetDataToInterface(product);
            ClassCommonSetting.SysParam.CurrentProductParam.AddParaInterface(calibration);

            zones.SPC.AddGUI(auto);
            zones.SPC.AddGUI(monitor);

            //事件挂钩
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(zones);
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(workflow);
            zones.WorkZone外框架.HookErrorEvent();
            auto.HookErrorEvent();
            workflow.TimeUsage.AddTimeUsageGUI(auto);
            zones.AddTimeUsage(auto);
            ClassCommonSetting.SocketToAOI.subscribeMeToResponseEvents(zones.WorkZone尺寸测量);
            HookErrorEvent();
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(this);
            this.UserLoggedIn += MainForm_UserLoggedIn1;
            this.UserLoggedOut += MainForm_UserLoggedOut1;
            product.SaveParaEvent += Product_SaveParaEvent;
            //发送事件
            CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(null, new StateEventArgs(ClassCommonSetting.SysParam.CurrentProduct, ""));
        }

        private void Product_SaveParaEvent(object sender, EventArgs e)
        {
            this.LogOutTime = GetLogOutTime(ClassCommonSetting.SysParam.LogOutTime);
        }
        private int GetLogOutTime(LogOutItem time)
        {
            switch(time)
            {
                case LogOutItem.不注销:
                    return 0;
                case LogOutItem.三十秒:
                    return 30;
                case LogOutItem.一分钟:
                    return 60;
                case LogOutItem.二分钟:
                    return 120;
                case LogOutItem.三分钟:
                    return 180;
                case LogOutItem.五分钟:
                    return 300;
                case LogOutItem.十分钟:
                    return 600;
                case LogOutItem.十五分钟:
                    return 900;
                case LogOutItem.二十分钟:
                    return 1200;
                default:
                    return 30;
            }
        }

        private void MainForm_UserLoggedOut1(BaseClass sender, StateEventArgs e)
        {
            if (_currUser != null)
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "用户", "注销用户：" + _currUser.Name);
        }
        private UserInfo _currUser;
        private void MainForm_UserLoggedIn1(BaseClass sender, StateEventArgs e)
        {
            _currUser = (UserInfo)sender;
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "用户", "登录用户：" + _currUser.Name + "，权限：" + _currUser.UserLevel);
        }

        private void HookErrorEvent()
        {
            ClassErrorHandle.NewErrorEvent += NewErrorEvent;
        }
        private void NewErrorEvent(object sender, ErrorLevel level)
        {
            ShowMainDlg(PanelIndex.MainAuto);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                //ClassBaseWorkZone.ActionEnable = false;
                CommonFunction.SysPublisher.notifyStopEventSubscribers(null, new StateEventArgs("停止", "程序退出"));
                TimeClass.Delay(3000);
                CommonFunction.SysPublisher.notifyProgramExitEventSubscribers(null, new StateEventArgs("程序退出", ""));
                ClassCommonSetting.CommonRelease();
                HardwarePool.SystemHardware.instance.Release();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            manual.InitGUI();
            setting.InitGUI();
            calibration.InitGUI();
            Statistic.MessageDisplayBeforeWindow = MainForm.instance;
            Statistic.UpdateYield += monitor.DisplayYieldValue;
            Statistic.UpdateDT += monitor.DisplayDTValue;
            Statistic.UpdateState += auto.UpdateState;
            Statistic.Init(ClassCommonSetting.SysParam.HistoryPath, monitor.dataGridViewDay, monitor.dataGridViewNight, monitor.dataGridViewAlarm, monitor.dataGridViewWorkFlow, auto.dataGridViewTotal);
        }

        private void MainForm_MainPanelChanged(PanelIndex panel)
        {
            ClassWorkFlow.Instance.WorkFlowEnable = panel == PanelIndex.MainAuto;
            if (panel == PanelIndex.MainDebug)
                if (ClassCommonSetting.SysParam.CurrentProductParam.UseGauge == "")
                {
                    calibration.CalibrationEnable = false;
                    MessageBox.Show("当前产品没有选择标准块，厚度和CCD校准不能使用。");
                }
                else
                    calibration.CalibrationEnable = true;
        }
    }
}