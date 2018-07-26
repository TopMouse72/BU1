using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colibri.CommonModule;
using Colibri.CommonModule.ToolBox;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;

namespace CDI
{
    public class ErrorInfoWithPause : ErrorInfo, IFormattable
    {
        public string Source;
        public bool NeedPause = false;
        public ErrorReturnHandler ErrorHandle = null;
        public ErrorInfoWithPause(string message, ErrorLevel level, bool retry = false, bool pause = false, ErrorReturnHandler handle = null) : base(message, level, retry)
        {
            NeedPause = pause;
            ErrorHandle = handle;
        }
        public string ToString()
        {
            return $"Source: {Source}; Message: {Message}: Level: {Level}";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }
    }
    public class ClassErrorHandle
    {
        public static bool isWorkFlowRunning = false;
        private static ErrorPublisher _errPublisher = new ErrorPublisher();
        public const int TIMEOUT = 6000;
        private static List<ErrorInfoWithPause> retryList = new List<ErrorInfoWithPause>();
        public static List<ErrorInfoWithPause> ErrorList { get { return retryList; } }
        private static void AddRetry(string source, ErrorInfoWithPause err)
        {
            if (err.Level == ErrorLevel.Alarm)
                if (err.NeedRetry && err.ErrorHandle != null)
                {
                    err.Source = source;
                    if (FindRetry(source, err.Message) == null)
                        retryList.Add(err);
                }
        }
        private static void RemoveRetry(string source, ErrorInfoWithPause err)
        {
            if (err.Level == ErrorLevel.Alarm)
                if (err.NeedRetry && err.ErrorHandle != null)
                    RemoveRetry(err.Source, err.Message);
        }
        private static ErrorInfoWithPause FindRetry(string source, string message)
        {
            foreach (ErrorInfoWithPause err in retryList)
                if (err.Source == source && err.Message == message)
                    return err;
            return null;
        }
        private static void RemoveRetry(string source, string message)
        {
            ErrorInfoWithPause err = FindRetry(source, message);
            if (err != null) retryList.Remove(err);
        }
        public static bool CheckAlarmListStatus(string source, ErrorInfoWithPause alarmErr = null)
        {
            if (alarmErr != null) RemoveRetry(source, alarmErr);
            bool NoAlarm = retryList.Count == 0;
            if (NoAlarm)
            {
                Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.报警结束, "报警结束");
            }
            return NoAlarm;
        }
        public static event ErrorListUserControl.NewErrorEventHandler NewErrorEvent
        {
            add
            {
                if (_errorShowPanel != null)
                {
                    _errorShowPanel.NewErrorEvent -= value;
                    _errorShowPanel.NewErrorEvent += value;
                }
            }
            remove { if (_errorShowPanel != null) _errorShowPanel.NewErrorEvent -= value; }
        }
        public static event ErrorListUserControl.ErrorItemCountChangeHandler ErrorItemCountChangeEvent
        {
            add
            {
                if (_errorShowPanel != null)
                {
                    _errorShowPanel.ErrorItemCountChangeEvent -= value;
                    _errorShowPanel.ErrorItemCountChangeEvent += value;
                }
            }
            remove { if (_errorShowPanel != null) _errorShowPanel.ErrorItemCountChangeEvent -= value; }
        }
        public static event ErrorListUserControl.ErrorEventHandler ErrorStop
        {
            add
            {
                if (_errorShowPanel != null)
                {
                    _errorShowPanel.ErrorStopEvent -= value;
                    _errorShowPanel.ErrorStopEvent += value;
                }
            }
            remove { if (_errorShowPanel != null) _errorShowPanel.ErrorStopEvent -= value; }
        }
        private static ErrorListUserControl _errorShowPanel;
        public static ErrorListUserControl ErrorDisplayPanel
        {
            get { return _errorShowPanel; }
        }
        public static void AddErrorDispPanel(ErrorListUserControl panel)
        {
            _errorShowPanel = panel;
            HookErrorPanelEvent(panel);
        }
        private static void HookErrorPanelEvent(ErrorListUserControl panel)
        {
            if (panel != null)
            {
                _errPublisher.subscribeMeToErrorEvents(panel);
                panel.ErrorExitProgramEvent += ErrorExitProgramEvent;
                panel.ErrorStopEvent += ErrorStopEvent;
            }
        }
        public static void HookErrorEvent(IErrorSubscriber subscriber)
        {
            _errPublisher.subscribeMeToErrorEvents(subscriber);
        }
        private static void ErrorExitProgramEvent()
        {
            CommonFunction.SysPublisher.notifyProgramExitEventSubscribers(null, new StateEventArgs("出错退出", ""));
        }

        private static void ErrorStopEvent()
        {
            CommonFunction.SysPublisher.notifyStopEventSubscribers(null, new StateEventArgs("出错停止", ""));
        }
        public static void ShowError(string Source, string ErrorMessage, ErrorLevel level, bool retry = false, bool pause = false, ErrorReturnHandler HandleFunc = null)
        {
            ShowError(Source, new ErrorInfoWithPause(ErrorMessage, level, retry, pause, HandleFunc));
        }
        public static void ShowError(string source, ErrorInfoWithPause error, ErrorReturnHandler HandleFunc = null)
        {
            if (HandleFunc != null)
                error.ErrorHandle = HandleFunc;
            string src = source;
            //if (isWorkFlowRunning) src = "流程：" + src;
            string err = error.Message;
            AddRetry(source, error);

            if (_errorShowPanel != null)
            {
                switch (error.Level)
                {
                    case ErrorLevel.Alarm:
                        if (error.NeedRetry && error.ErrorHandle != null)
                            err += "。检查并按OK或Retry重试。";
                        if (isWorkFlowRunning && error.NeedPause)
                        {
                            err += "。流程暂停。";
                            CommonFunction.SysPublisher.notifyPauseEventSubscribers(null, new StateEventArgs("暂停", source + ": " + error.Message));
                        }
                        break;
                    case ErrorLevel.Notice:
                        if (isWorkFlowRunning && error.NeedPause)
                        {
                            err += "。流程暂停。";
                            CommonFunction.SysPublisher.notifyPauseEventSubscribers(null, new StateEventArgs("暂停", source + ": " + error.Message));
                        }
                        break;
                    case ErrorLevel.Error:
                    case ErrorLevel.Fatal:
                        if (isWorkFlowRunning)
                        {
                            err = $"流程出错({error.Message})。流程停止。";
                            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WorkFlow", "流程因错误停止");
                            BaseStateMachine.NotifyErrorStop(null, null);
                        }
                        break;
                }
                _errPublisher.notifyShowErrorEventSubscribers(src, err, error.Level, ClassCommonSetting.Log, error.NeedRetry, error.ErrorHandle);
                Statistic.ShowErrorHandler(source, error.Message, error.Level, null, error.NeedRetry, error.NeedPause, error.ErrorHandle);
            }
        }

        private string _err = "";
        public string ErrMessage
        {
            get { return _err; }
        }
        public bool NoError
        {
            get { return _err == ""; }
        }
        public void CollectErrInfo(string result)
        {
            if (result != "")
                _err += result + " ";
        }
        public void CollectErrInfo(ErrorInfoWithPause result)
        {
            if (result != null && result.Message != "")
                _err += result.Message + " ";
        }
        public void Clear()
        {
            _err = "";
        }
    }
}