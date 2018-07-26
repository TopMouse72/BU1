using System;
using System.Threading;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for UnloadPNPWorkIdle
    /// </summary>
    public class UnloadPNPWorkIdle : BaseState
    {
        public UnloadPNPWorkIdle(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
    }

    /// <summary>
    /// Summary description for UnloadPNPWorkPickPart
    /// </summary>
    public class UnloadPNPWorkPickPart : BaseState
    {
        public UnloadPNPWorkPickPart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
            _afterMeasCallBack = ClassWorkZones.Instance.SaveMeasData;
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "Start": //From state Idle
                        ClassZone下料机械手 UnloadZone = ClassWorkZones.Instance.WorkZone下料机械手;
                        ErrorInfoWithPause res = UnloadZone.ActionMove(ClassZone下料机械手.EnumPoint.Pick);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("下料PNP取料", res);
                            return;
                        }
                        oldres = null;
                        ErrorReturnHandler("下料PNP取料", "", ErrorDialogResult.Retry);
                        break;
                }
            }
        }
        private CallBackCommonFunc _afterMeasCallBack;
        private void CallBackAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "AfterCCDMeas");
        }
        private ErrorInfoWithPause res = null;
        private ErrorInfoWithPause oldres = null;
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (!owner.IsRunning) return;
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                ClassZone下料机械手 UnloadZone = ClassWorkZones.Instance.WorkZone下料机械手;
                if (message != "")
                {
                    ClassBaseWorkZone.HandleVacuumFailCell("下料机械手", UnloadZone.UnloadPNPDataStations.ToArray());
                    for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        ClassWorkZones.Instance.WorkZone尺寸测量.CCDMeasDataStations[i].TransferFrom(ClassWorkZones.Instance.WorkZone下料机械手.UnloadPNPDataStations[i]);
                }
                res = UnloadZone.ActionUnloadPNPStartPick(ClassWorkZones.Instance.DoLoadOutPNPPick, ClassWorkZones.Instance.AfterLoadOutPNPPick);
                if (res == null)
                {
                    //while (!UnloadZone.AxisUnloadPNPY.MoveTo(ClassZone下料机械手.EnumPoint.Place, false))
                    //{
                    //    if (UnloadZone.DispMotionError(UnloadZone.AxisUnloadPNPY, ClassZone下料机械手.EnumPoint.Place) != null)
                    //        return;
                    //}
                    ///////同步调用测量工作区域X电机回到GetPart位置
                    res = ClassWorkZones.Instance.WorkZone尺寸测量.ActionToGetPart(false);
                    if (res != null)
                    {
                        ClassErrorHandle.ShowError(source, res);
                        return;
                    }
                    ///////异步调用测量工作区域X电机回到GetPart位置
                    //ClassWorkZones.Instance.WorkZone尺寸测量.AsyncActionMotorMove(ClassWorkZones.Instance.WorkZone尺寸测量.AxisOutlineMeasX,
                    //    ClassZone尺寸测量.EnumPointX.GetPart);
                    res = UnloadZone.ActionMove(ClassZone下料机械手.EnumPoint.Place, false);
                    if (res != null)
                    {
                        ClassErrorHandle.ShowError(source, res);
                        return;
                    }
                    if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                    {
                        #region CCD All finish
                        if (!ClassCommonSetting.CheckTimeOut(() => { return ClassWorkZones.Instance.WorkZone尺寸测量.isCCDAllFinish; }))
                        {
                            string cell = "";
                            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                            {
                                if (!ClassWorkZones.Instance.WorkZone尺寸测量.MeasDone[i])
                                    cell += " " + ((EnumCellIndex)i).ToString();
                            }
                            ClassErrorHandle.ShowError(source, "图像检测数据没有全部返回:" + cell, ErrorLevel.Notice);
                        }
                        #endregion CCD All finish
                    }
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("UnloadPNPPickFinish");
                    if (_afterMeasCallBack != null)
                        _afterMeasCallBack.BeginInvoke(CallBackAsyncReturn, _afterMeasCallBack);
                    //ClassWorkZones.Instance.WorkZone尺寸测量.AxisOutlineMeasX.WaitStop(ClassErrorHandle.TIMEOUT);
                    ClassWorkZones.Instance.WorkZone尺寸测量.IsWorkFree = true;
                    DoneReturn("CCDMotorBackToGetPart");
                }
                else
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
            }
        }
    }
    /// <summary>
    /// Summary description for UnloadPNPWorkPlacePart
    /// </summary>
    public class UnloadPNPWorkPlacePart : BaseState
    {
        public UnloadPNPWorkPlacePart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                switch (inputEventArg.eventName)
                {
                    case "UnloadPNPPickFinish": //From state PickPart
                    case "WaitUnloadOutEmpty":
                        if (!ClassWorkZones.Instance.WorkZone下料传送.IsUnLoadHavePartEmpty)
                        {
                            DoneReturn("WaitUnloadOutEmpty");
                            return;
                        }
                        base.run(inputEventArg);
                        ClassZone下料机械手 UnloadZone = ClassWorkZones.Instance.WorkZone下料机械手;
                        ErrorInfoWithPause res = UnloadZone.ActionMove(ClassZone下料机械手.EnumPoint.Place);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("下料PNP放料", res);
                            return;
                        }
                        ErrorReturnHandler("", "", ErrorDialogResult.OK);
                        break;
                }
            }
        }
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (!owner.IsRunning) return;
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                ClassBaseWorkZone.HandleVacuumFailCell("下料机械手", ClassWorkZones.Instance.WorkZone下料机械手.UnloadPNPDataStations.ToArray());
                ClassWorkZones.Instance.WorkZone下料传送.IsPlacingPart = true;
                ClassZone下料机械手 UnloadZone = ClassWorkZones.Instance.WorkZone下料机械手;
                ErrorInfoWithPause res = UnloadZone.ActionUnloadPNPStartPlace(ClassWorkZones.Instance.DoLoadOutPNPPlace, ClassWorkZones.Instance.AfterLoadOutPNPPlace);
                if (res != null)
                {
                    ClassErrorHandle.ShowError("下料PNP放料", res);
                    ClassWorkZones.Instance.WorkZone下料传送.IsPlacingPart = false;
                    return;
                }
                ClassWorkZones.Instance.WorkZone下料传送.IsPlacingPart = false;
                UnloadZone.AxisUnloadPNPY.MoveTo(ClassZone下料机械手.EnumPoint.Buffer, false);
                DoneReturn("UnloadPNPPlaceFinish");
            }
        }
    }
}