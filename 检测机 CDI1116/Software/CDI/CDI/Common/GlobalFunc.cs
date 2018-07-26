using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Colibri.CommonModule;
using Colibri.CommonModule.ToolBox;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using Colibri.CommonModule.IOSystem;

namespace CDI
{
    public delegate ErrorInfoWithPause AsyncDoAction();
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
        public static void ThrowException(Delegate Handler, string eventname, Exception e)
        {
            string ErrString = $"事件名称：{eventname}{Environment.NewLine}事件来源：{Handler.Target}{Environment.NewLine}调用函数：{Handler.Method.Name}{Environment.NewLine}运行错误：{e.Message}";
            ProgramLog(LogFile.Level.Error, Handler.Target.ToString(), ErrString);
            BaseStateMachine.StateMachineLogFile.LogError(ErrString);
            Colibri.CommonModule.Forms.BaseForm.DoInvokeRequired(MainForm.instance,
                () =>
            MessageBox.Show(MainForm.instance, $"{ErrString}{Environment.NewLine}" +
            $"该错误将保存在{LogFile.DefaultLogPath}Log{DateTime.Today:yyyyMMdd}.LOG中。{Environment.NewLine}{Environment.NewLine}" +
            $"将此错误截屏保存并发送给科瑞工程师进行分析。" +
            $"点“确定”程序将非正常退出。"));
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
                ClassCommonSetting.ThrowException(handler, Handle, e);
            }
        }
        public static void ProgramLog(LogFile.Level loglevel, string source, string log)
        {
            lock (LogLock)
            {
                string fileName = $"{LogFile.DefaultLogPath}Log{DateTime.Today:yyyyMMdd}.LOG";
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
        public static void DeleteConfigFile(string ConfigFileName)
        {
            string[] files = Directory.GetFiles(CommonFunction.DefaultConfigPath, ConfigFileName);
            foreach (string file in files) File.Delete(file);
            files = Directory.GetFiles(CommonFunction.DefaultBackupConfigPath, ConfigFileName);
            foreach (string file in files) File.Decrypt(file);
        }
    }
}
