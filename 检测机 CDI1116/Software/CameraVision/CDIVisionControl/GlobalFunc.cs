using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Colibri.CommonModule;
using Colibri.CommonModule.ToolBox;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using Colibri.CommonModule.IOSystem;

namespace CDI
{
    public delegate ErrorInfo AsyncDoAction();
    public delegate void CallBackCommonFunc();
    public delegate void DisplayCellData(ClassDataInfo CellData);
    public static partial class ClassCommonSetting
    {
        public static AsyncDoAction CheckGeneralAir;
        public static ClassSystemParameter SysParam = new ClassSystemParameter();
        private static object LogLock = new object();
        private static LogFile _log = LogFile.instance;
        private static List<ILogControl> _logControl = new List<ILogControl>();
        public static LogFile Log { get { return _log; } }
        public static void AddLogControl(ILogControl control)
        {
            _logControl.Add(control);
        }
        public static void ThrowException(object target, string eventname, Exception e)
        {
            string ErrString = target.ToString() + " " + eventname + " event handler runtime error: " + e.Message;
            ProgramLog(LogFile.Level.Error, target.ToString(), ErrString);
            BaseStateMachine.StateMachineLogFile.LogError(ErrString);
            MessageBox.Show(ErrString);
            throw new Exception(ErrString);
        }
        public static void CallBackCommonAsyncReturn(IAsyncResult result, string Handle)
        {
            CallBackCommonFunc handler = (CallBackCommonFunc)result.AsyncState;
            try
            {
                handler.EndInvoke(result);
                result.AsyncWaitHandle.Close();
            }
            catch (Exception e)
            {
                ClassCommonSetting.ThrowException(handler.Target, Handle, e);
            }
        }
        public static void ProgramLog(LogFile.Level loglevel, string source, string log)
        {
            lock (LogLock)
            {
                string fileName = string.Format("{0}Log{1:yyyyMMdd}.LOG", LogFile.DefaultLogPath, DateTime.Today);
                if (_log.LogFileName != fileName)
                {
                    _log = new LogFile(fileName, true, LogFile.Level.All, true);
                    foreach (ILogControl control in _logControl)
                        _log.AddLogControl(control);
                }
                switch (loglevel)
                {
                    case LogFile.Level.Debug:
                        _log.LogDebug(source, log);
                        break;
                    case LogFile.Level.Error:
                        _log.LogError(source, log);
                        break;
                    case LogFile.Level.Fatal:
                        _log.LogFatal(source, log);
                        break;
                    case LogFile.Level.Info:
                        _log.LogInfo(source, log);
                        break;
                    case LogFile.Level.Notice:
                        _log.LogNotice(source, log);
                        break;
                    case LogFile.Level.Success:
                        _log.LogSuccess(source, log);
                        break;
                    case LogFile.Level.Warning:
                        _log.LogWarning(source, log);
                        break;
                }
            }
        }
    }
}
