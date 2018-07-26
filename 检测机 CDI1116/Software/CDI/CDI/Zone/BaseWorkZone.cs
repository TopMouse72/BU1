using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.Port;
using Colibri.CommonModule.ToolBox;
using HardwarePool;
using CDI.GUI;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using Measure;

namespace CDI.Zone
{
    public delegate ErrorInfoWithPause AsyncActionMotorMove(CAxisBase axis, object point, bool NeedWait = true);
    public delegate void SafetyIOStatusChangeEvent(BaseIOPort port);
    /// <summary>
    /// WorkZone名称枚举
    /// </summary>
    public enum EnumWorkZoneName
    {
        LoadIn,
        LoadPNP,
        TopAlign,
        TransPNP,
        ThicknessMeas,
        OutlineMeas,
        UnloadPNP,
        UnloadOut,
        SortingPNP,
        Frame,
    }
    ///// <summary>
    ///// 流程事件名称
    ///// </summary>
    //public enum EnumEventName
    //{
    //    NewCellReady,//有新物料，Loadin可上料
    //    NewCellLoad,//新物料加载，LoadPNP可取料
    //    LoadInFinish,//上料结束，LoadPNP可以取料
    //    LoadPNPPickFinish,//LoadPNP取料结束，LoadIn可以上料

    //}
    /// <summary>
    /// 气压控制
    /// </summary>
    public enum EnumAirControl
    {
        Vacuum,
        Blow,
        Close,
        None,
    }
    /// <summary>
    /// 单个真空控制
    /// </summary>
    public class ClassAirPort
    {
        //public string VacuumPortName = "";
        public bool PortEnable = true;
        public BaseIOPort Port = null;
        public void SetOutPortStatus(bool status)
        {
            if (Port == null) return;
            if (PortEnable)
                Port.SetOutput(status);
            else
                Port.SetOutput(false);
        }
        public bool GetInPortStatus()
        {
            if (Port == null) return false;
            return PortEnable && Port.status;
        }
    }
    /// <summary>
    /// 单个电芯真空组控制
    /// </summary>
    public class ClassAirUnit
    {
        public const int VACCOUNT = 3;
        public ClassAirPort[] AirPorts;
        public ClassAirPort MainPort;
        public ClassAirUnit()
        {
            MainPort = new ClassAirPort();
            AirPorts = new ClassAirPort[VACCOUNT];
            for (int i = 0; i < VACCOUNT; i++)
            {
                AirPorts[i] = new ClassAirPort();
            }
        }
        public void SetUnitStatus(bool status)
        {
            MainPort.SetOutPortStatus(status);
            for (int i = 0; i < VACCOUNT; i++)
                if (AirPorts[i].PortEnable)
                    AirPorts[i].SetOutPortStatus(status);
                else
                    AirPorts[i].SetOutPortStatus(false);
        }
    }
    public enum EnumVacuumIndex
    {
        后真空 = 2, 中真空 = 1, 前真空 = 0,
    }
    public enum EnumCellIndex
    {
        左电芯 = 2, 中电芯 = 1, 右电芯 = 0,
    }
    /// <summary>
    /// WorkZone基类
    /// </summary>
    public abstract class ClassBaseWorkZone : IOSubscriber, ISystemSubscriber
    {
        #region Static
        public const string CALIBPROD = "calibration";
        public static void SetNGText(Control disp, int NGCount)
        {
            disp.Text = NGCount.ToString();
            if (NGCount == 0)
                disp.ForeColor = Color.DodgerBlue;
            else if (NGCount < 10)
                disp.ForeColor = Color.Black;
            else if (NGCount < 20)
                disp.ForeColor = Color.Purple;
            else
                disp.ForeColor = Color.Red;
        }
        /// <summary>
        /// 出料时检查电芯是否NG。NG的条件是下料模式不是全OK，而且运行模式不是空跑，而且电芯数据不为空，则电芯数据NG或下料模式是全NG时，返回NG。
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool CheckCellIsNG(ClassDataInfo cell)
        {
            //Dry run must return false;
            //Unload mode AllOK returns false;
            //Unload mode Normal returns DataNG
            //Unload mode AllNG returns true;
            return ClassWorkFlow.Instance.UnloadMode != EnumUnloadMode.全OK
                && ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑
                && cell != null
                && (cell.DataNG || ClassWorkFlow.Instance.UnloadMode == EnumUnloadMode.全NG);
        }
        public const int CELLCOUNT = 3;
        public const int MAX_NGBOX_CELL_COUNT = 10;
        public const bool CYLIND_UP = false;
        public const bool CYLIND_DOWN = true;
        public const bool CYLIND_RIGHT = true;
        public const bool CYLIND_LEFT = false;
        public const bool CYLIND_FORWARD = true;
        public const bool CYLIND_BACKWARD = false;
        public static bool ActionEnable = false;
        #endregion Static
        public int CheckTime;
        private IOPublisher _ioPublisher = new IOPublisher();
        protected SafetyIOStatusChangeEvent _safetyStatus;
        public event SafetyIOStatusChangeEvent SafetyStatusChange
        {
            add { _safetyStatus -= value; _safetyStatus += value; }
            remove { _safetyStatus -= value; }
        }
        private CallBackCommonFunc asyncReset, updateNGBox;
        private AsyncActionMotorMove asyncMotorMove;
        public event CallBackCommonFunc UpdateNGBoxEvent
        {
            add { updateNGBox -= value; updateNGBox += value; }
            remove { updateNGBox -= value; }
        }
        protected Dictionary<string, CAxisBase> _AxisCollection = new Dictionary<string, CAxisBase>();
        protected Dictionary<string, BaseIOPort> _InPortCollection = new Dictionary<string, BaseIOPort>();
        protected Dictionary<string, BaseIOPort> _OutPortCollection = new Dictionary<string, BaseIOPort>();
        protected Dictionary<string, SerialPortData> _SerialPortCollection = new Dictionary<string, SerialPortData>();
        protected bool _isRunning = false;
        protected bool _isPausing = false;
        protected bool _isManual = false;
        protected bool _isWorkFree = true;
        public virtual bool IsWorkFree
        {
            get { return _isWorkFree; }
            set { _isWorkFree = value; }
        }
        public ClassTimeUsage TimeUsage = new ClassTimeUsage();
        public ClassBaseWorkZone(string ZoneName)
        {

            Name = ZoneName;
            asyncReset = DoReset;
            asyncMotorMove = ActionMotorMove;
            //GetProdParameter();
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(this);
        }
        public IOPublisher IOResponsePublisher
        {
            get { return _ioPublisher; }
        }
        protected BaseZoneSettingPanel _ZoneSettingPanel;
        protected BaseZoneManualPanel _ZoneManualPanel;
        /// <summary>
        /// 添加一个电机类CaxisBase实例。
        /// </summary>
        /// <param name="axis">电机类实例</param>
        public void AddHardware(CAxisBase axis)
        {
            if (axis == null) return;
            if (_AxisCollection.ContainsKey(axis.Name)) return;
            _AxisCollection.Add(axis.Name, axis);
        }
        /// <summary>
        /// 添加一个IO端口类BaseIOPort实例。
        /// </summary>
        /// <param name="port">IO端口类实例</param>
        public void AddHardware(BaseIOPort port)
        {
            if (port == null) return;
            switch (port.Type)
            {
                case IOType.In:
                    if (_InPortCollection.ContainsKey(port.PortName)) return;
                    _InPortCollection.Add(port.PortName, port);
                    port.subscribeMeToIOEvents(this);
                    break;
                case IOType.Out:
                    if (_OutPortCollection.ContainsKey(port.PortName)) return;
                    _OutPortCollection.Add(port.PortName, port);
                    break;
            }
        }
        /// <summary>
        /// 添加一个串口控制类SerialPortControl实例。
        /// </summary>
        /// <param name="serialport">串口控制类实例</param>
        public void AddHardware(SerialPortData serialport)
        {
            if (serialport == null) return;
            if (_SerialPortCollection.ContainsKey(serialport.OwnerPort.PortName)) return;
            _SerialPortCollection.Add(serialport.OwnerPort.PortName, serialport);
        }
        /// <summary>
        /// 返回指定名称的电机类实例。
        /// </summary>
        /// <param name="key">电机名称</param>
        /// <returns></returns>
        public CAxisBase ThisAxis(object key)
        {
            string name = Enum.GetName(key.GetType(), key);
            return ThisAxis(name);
        }
        public CAxisBase ThisAxis(string name)
        {
            if (_AxisCollection.ContainsKey(name))
                return _AxisCollection[name];
            else
                return null;
        }
        /// <summary>
        /// 返回指定名称的输入端口类实例。
        /// </summary>
        /// <param name="key">输入端口名称</param>
        /// <returns></returns>
        public BaseIOPort ThisInport(object key)
        {
            string name = "In" + Enum.GetName(key.GetType(), key);
            if (_InPortCollection.ContainsKey(name))
                return _InPortCollection[name];
            else
                return null;
        }
        /// <summary>
        /// 返回指定名称的输出端口类实例。
        /// </summary>
        /// <param name="key">输出端口名称</param>
        /// <returns></returns>
        public BaseIOPort ThisOutport(object key)
        {
            string name = "Out" + Enum.GetName(key.GetType(), key);
            if (_OutPortCollection.ContainsKey(name))
                return _OutPortCollection[name];
            else
                return null;
        }
        /// <summary>
        /// 返回指定名称的串口控制类实例
        /// </summary>
        /// <param name="key">串口控制类名称</param>
        /// <returns></returns>
        public SerialPortData ThisSerialPortData(object key)
        {
            string name = Enum.GetName(key.GetType(), key);
            if (_SerialPortCollection.ContainsKey(name))
            {
                _SerialPortCollection[name].Log = ClassCommonSetting.Log;
                return _SerialPortCollection[name];
            }
            else
                return null;
        }
        /// <summary>
        /// 根据硬件名称列表，将硬件池SystemHardware中的硬件添加到WorkZone中。
        /// </summary>
        /// <param name="EnumAxisName">电机名称列表</param>
        /// <param name="EnumInportName">输入端口名称列表</param>
        /// <param name="EnumOutportName">输出端口名称列表</param>
        /// <param name="EnumSerialPortName">串口名称列表</param>
        protected void AssignHardware(Type EnumAxisName = null, Type EnumInportName = null, Type EnumOutportName = null, Type EnumSerialPortName = null)
        {
            if (EnumAxisName != null)
                foreach (string name in Enum.GetNames(EnumAxisName))
                    AddHardware(SystemHardware.instance.GetAxis(name));
            if (EnumInportName != null)
                foreach (string name in Enum.GetNames(EnumInportName))
                {
                    AddHardware(SystemHardware.instance.GetInPort(name));
                    //SystemHardware.instance.GetInPort(name).subscribeMeToIOEvents(this);
                }
            if (EnumOutportName != null)
                foreach (string name in Enum.GetNames(EnumOutportName))
                    AddHardware(SystemHardware.instance.GetOutPort(name));
            if (EnumSerialPortName != null)
                foreach (string name in Enum.GetNames(EnumSerialPortName))
                    AddHardware(SystemHardware.instance.GetSerialPort(name));
        }
        /// <summary>
        /// 设置手动界面，关联界面上的控件和WorkZone的硬件。
        /// </summary>
        private void AssignPanel()
        {
            if (_ZoneSettingPanel == null) return;
            _ZoneSettingPanel.WorkZone = this;
            foreach (BaseIOPort port in _InPortCollection.Values)
            {
                Control[] cons = _ZoneSettingPanel.Controls.Find("uioFlatPort" + port.PortName, true);
                if (cons.Length > 0)
                {
                    if (cons[0].GetType() == typeof(UIOFlatPort))
                    {
                        ((UIOFlatPort)cons[0]).Port = port;
                    }
                }
            }
            foreach (BaseIOPort port in _OutPortCollection.Values)
            {
                Control[] cons = _ZoneSettingPanel.Controls.Find("uioFlatPort" + port.PortName, true);
                if (cons.Length > 0)
                {
                    if (cons[0].GetType() == typeof(UIOFlatPort))
                    {
                        ((UIOFlatPort)cons[0]).Port = port;
                    }
                }
            }
            if (_AxisCollection.Count == 0)
            {
                _ZoneSettingPanel.comboBoxAxis.Hide();
                _ZoneSettingPanel.labelAxis.Hide();
            }
            else
            {
                CAxisBase axis;
                _ZoneSettingPanel.comboBoxAxis.Items.AddRange(_AxisCollection.Keys.ToArray());
                foreach (string axisname in _AxisCollection.Keys.ToArray())
                {
                    axis = ThisAxis(axisname);
                    axis.ConfigurationControl.Left = 3;
                    axis.ConfigurationControl.Top = _ZoneSettingPanel.comboBoxAxis.Top + 27;
                    _ZoneSettingPanel.Controls.Add(axis.ConfigurationControl);
                }
            }
        }
        /// <summary>
        /// 获取或设置手动界面。
        /// </summary>
        public BaseZoneSettingPanel ZoneSettingPanel
        {
            get { return _ZoneSettingPanel; }
            set
            {
                _ZoneSettingPanel = value;
                AssignPanel();
            }
        }
        public BaseZoneManualPanel ZoneManualPanel
        {
            get { return _ZoneManualPanel; }
            set
            {
                _ZoneManualPanel = value;
                _ZoneManualPanel.OwnerZone = this;
                _ZoneManualPanel.RefreshParameter();
            }
        }
        protected void WorkZoneActionAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "WorkZoneAction");
        }
        /// <summary>
        /// 工作区域初始化。接收公共System事件
        /// </summary>
        public virtual void ZoneInit()
        {
        }
        protected abstract void InPortActive(string inPort);
        protected abstract void InPortDeActive(string inPort);
        protected abstract void Reset(ClassErrorHandle err);
        protected bool CheckWorkFlowStatus(string Action = "", string Step = "")
        {
            //手动状态
            if (_isManual) return true;
            //退出状态
            if (!_isRunning)
                return false;
            //暂停状态
            if (_isPausing)
            {
                //等待恢复
                while (_isPausing)
                {
                    Application.DoEvents();
                    //暂停时退出
                    if (!_isRunning)
                        return false;
                }
            }
            return true;
        }
        //protected virtual bool CheckPart() { return true; }
        public ErrorInfoWithPause DispMotionError(CAxisBase axis, object position)
        {
            return WaitAlarmPause(axis.Name, axis.Name + "电机移到" + position.ToString() + "位置出错，请检查安全检测失败报警查看出错原因");
        }
        public ErrorInfoWithPause DispMotionError(CAxisBase axis, string action)
        {
            return WaitAlarmPause(axis.Name, axis.Name + "电机" + action + "出错，检查安全检测失败报警查看出错原因");
        }
        public override void InputActiveHandler(BaseClass sender, StateEventArgs e)
        {
            //if (ActionEnable)
            //{
            base.InputActiveHandler(sender, e);
            InPortActive(((BaseIOPort)sender).PortName.Substring(2));
            //}
        }
        public override void InputDeActiveHandler(BaseClass sender, StateEventArgs e)
        {
            //if (ActionEnable)
            //{
            base.InputDeActiveHandler(sender, e);
            InPortDeActive(((BaseIOPort)sender).PortName.Substring(2));
            //}
        }
        public ErrorInfoWithPause ServoOnAllMotor()
        {
            ErrorInfoWithPause res = null;
            string err = "";
            foreach (string axisname in _AxisCollection.Keys.ToArray())
            {
                ThisAxis(axisname).ServoOn = true;
                if (!ThisAxis(axisname).ServoOn || ThisAxis(axisname).Alarm)
                    err += axisname + " ";
            }
            if (err != "")
                res = new ErrorInfoWithPause("电机掉电或报警: " + err.Trim(), ErrorLevel.Error);
            return res;
        }
        public void ResetHandler(BaseClass sender, StateEventArgs e)
        {
        }
        private void ResetAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "Reset");
        }
        public bool Reseting = false;
        private object ResetLock = new object();
        public void AsyncDoReset()
        {
            if (asyncReset != null) asyncReset.BeginInvoke(ResetAsyncReturn, asyncReset);
        }
        private void UpdateNGBoxAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "UpdateNGBox");
        }
        public void DoUpdateNGBox()
        {
            if (updateNGBox != null)
                foreach (CallBackCommonFunc callback in updateNGBox.GetInvocationList())
                    callback.BeginInvoke(UpdateNGBoxAsyncReturn, callback);
        }
        private void MotorMoveAsyncReturn(IAsyncResult result)
        {
            AsyncActionMotorMove handler = (AsyncActionMotorMove)((AsyncResult)result).AsyncDelegate;
            ErrorInfoWithPause res = handler.EndInvoke(result);
            if (res != null)
                ClassErrorHandle.ShowError(this.Name, res);
        }
        public void AsyncActionMotorMove(CAxisBase axis, object point, bool NeedWait = true)
        {
            if (asyncMotorMove != null) asyncMotorMove.BeginInvoke(axis, point, NeedWait, MotorMoveAsyncReturn, asyncMotorMove);
        }
        private ClassErrorHandle ResetErr = new ClassErrorHandle();
        public bool ResetOK
        {
            get { return ResetErr.NoError; }
        }
        public string ResetErrMessage
        {
            get { return ResetErr.ErrMessage; }
        }
        public void DoReset()
        {
            ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WorkFlow", $"{Name}工作区域复位开始");
            ErrorInfoWithPause res = null;
            Reseting = true;
            lock (ResetLock)
            {
                ResetErr = new ClassErrorHandle();
                ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WorkFlow", $"{Name}工作区域所有端口复位");
                res = ResetOutPort();
                if (res != null)
                {
                    ClassErrorHandle.ShowError(this.Name, res);
                    return;
                }
                ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WorkFlow", $"{Name}工作区域所有电机上电");
                ServoOnAllMotor();
                Reset(ResetErr);
                if (ResetErr.NoError)
                    _ioPublisher.notifyDoneEventSubscribers(this, new StateEventArgs("ResetDone", this.Name));
                else
                    _ioPublisher.notifyErrorEventSubscribers(this, new FailureException(ResetErr.ErrMessage));
            }
            Reseting = false;
            ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WorkFlow", $"{Name}工作区域复位结束");
        }
        public virtual void StartHandler(BaseClass sender, StateEventArgs e)
        {
            _isRunning = true;
            _isPausing = false;
            IsWorkFree = true;
            _isManual = false;
            ResetOutPort();
        }

        public virtual void StopHandler(BaseClass sender, StateEventArgs e)
        {
            _isRunning = false;
            _isPausing = false;
        }

        public virtual void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            _isPausing = true;
        }
        /// <summary>
        /// 流程因错误暂停（警告级别错误）。问题处理完后可恢复流程。流程没有运行或恢复流程，返回null。流程停止则返回错误。
        /// </summary>
        /// <param name="source">错误来源</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public ErrorInfoWithPause WaitAlarmPause(string source, string error)
        {
            if (!_isRunning) return new ErrorInfoWithPause("错误:" + error + "。", ErrorLevel.Notice);
            ClassErrorHandle.ShowError(source, error + "。处理完成按开始恢复运行。", ErrorLevel.Alarm, false, true);
            _isPausing = true;
            while (_isPausing)
            {
                Application.DoEvents();
                if (!_isRunning) return new ErrorInfoWithPause("流程停止（" + error + "）。", ErrorLevel.Notice);
            }
            return null;
        }

        public virtual void ResumeHandler(BaseClass sender, StateEventArgs e)
        {
            _isPausing = false;
        }

        public virtual void EStopHandler(BaseClass sender, StateEventArgs e)
        {
            StopHandler(sender, e);
        }

        public virtual void RetryHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ModeChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void LoginHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void LogoutHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ProgramExitHandler(BaseClass sender, StateEventArgs e)
        {
            //StopHandler(sender, e);
            //ResetOutPort();
        }
        public virtual ErrorInfoWithPause ResetOutPort() { return null; }
        public void SubscribeMeToResponseEvent(IIOSubscriber subscriber)
        {
            _ioPublisher.subscribeMeToResponseEvents(subscriber);
        }
        public void UnsubcribeFromResponseEvent(IIOSubscriber subscriber)
        {
            _ioPublisher.unsubscribeMeFromResponseEvents(subscriber);
        }
        //public void NotifyDoneEvent(EnumEventName eventname, string eventinfo = "")
        //{
        //    _ioPublisher.notifyDoneEventSubscribers(this, new StateEventArgs(eventname.ToString(), eventinfo));
        //}
        protected bool GetSerialData(string ReadString, ref string GetData, SerialPortData PortData)
        {
            return PortData.GetReturnData(ReadString, ref GetData, PortData);
        }
        protected string StartSerialReading(SerialPortData serialPortData, HardwareSerialProtocolName protocol, ref string Data)
        {
            string res = serialPortData.StartCommandTrans(protocol.ToString(), GetSerialData);
            if (res == "")
                Data = serialPortData.Data;
            return res;
        }
        protected void StartSerialRealSend(SerialPortData serialPortData, HardwareSerialProtocolName protocol)
        {
            serialPortData.StartRealSend(protocol.ToString(), "", null, 500);
        }
        protected void StopSerialRealSend(SerialPortData serialPortData)
        {
            serialPortData.StopRealSend();
        }
        protected string MotorReset(CAxisBase axis, object StartPos)
        {
            //if (axis.HomeFinish)
            //{
            //    if (!axis.MoveTo(StartPos)) return DispMotionError(axis, StartPos).Message;
            //}
            //else
            return axis.Home(StartPos.ToString());
        }
        protected string GetDataInfoString(CellCollection<ClassDataStation> datas)
        {
            return GetDataInfoString(datas.Values.ToArray<ClassDataStation>());
        }
        protected string GetDataInfoString(ClassDataStation[] datas)
        {
            string str = "";
            for (int i = 0; i < CELLCOUNT; i++)
                if (datas[i].CellData != null)
                    str += datas[i].CellData.Index.ToString() + " ";
                else
                    str += "null ";
            return str.Trim();
        }
        public static void HandleVacuumFailCell(string source, ClassDataStation[] datas)
        {
            string res = "";
            for (int i = 0; i < CELLCOUNT; i++)
                if (datas[i].CellData != null && datas[i].CellData.isPickingError)
                    res += datas[i].StationName + " ";
            res = res.Trim();
            if (res == "") return;
            BaseForm.DoInvokeRequired(MainForm.instance, () =>
            {
                switch (MessageBoxVacuumHandle.Show(source, datas))
                {
                    //case DialogResult.Abort:
                    case DialogResult.Ignore:
                        res = "";
                        for (int i = 0; i < CELLCOUNT; i++)
                            if (datas[i].CellData != null && datas[i].CellData.isPickingError)
                                res += datas[i].StationName + " ";
                        res = res.Trim();
                        BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, "请将需要忽略的电芯（" + res + "）从设备中取出，然后点“确定”按钮继续。"));
                        for (int i = 0; i < CELLCOUNT; i++)
                            if (datas[i].CellData != null && datas[i].CellData.isPickingError)
                                datas[i].CellData = null;
                        break;
                    case DialogResult.Retry:
                        BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, "请再次确定电芯已经处理完毕。点“确定”按钮开始重试。"));
                        break;
                }
            });
        }
        public ErrorInfoWithPause ActionMotorMove(CAxisBase axis, object point, bool NeedWait = true)
        {
            ErrorInfoWithPause res = null;
            while (!axis.MoveTo(point, NeedWait))
            //return DispMotionError(AxisOutlineMeasX, EnumPointX.GetPart);
            {
                res = DispMotionError(axis, point);
                if (res != null) return res;
            }
            return null;
        }
    }
}