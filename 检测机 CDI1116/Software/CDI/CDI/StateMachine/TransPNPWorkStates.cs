using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for TransPNPWorkIdle
    /// </summary>
    public class TransPNPWorkIdle : BaseState
    {
        public TransPNPWorkIdle(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
    }

    /// <summary>
    /// Summary description for TransPNPWorkPickPart
    /// </summary>
    public class TransPNPWorkPickPart : BaseState
    {
        //bool EventStart = false;
        public TransPNPWorkPickPart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
            //EventStart = false;
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "Start": //From state Idle
                        //EventStart = true;
                        oldres = null;
                        ErrorReturnHandler("传送PNP取料", "", ErrorDialogResult.Retry);
                        break;
                }
            }
        }
        private ErrorInfoWithPause res = null;
        private ErrorInfoWithPause oldres = null;
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (!owner.IsRunning) return;
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                bool AllPick = true;
                ClassZone传送机械手 TransZone = ClassWorkZones.Instance.WorkZone传送机械手;
                if (message != "")
                {
                    ClassBaseWorkZone.HandleVacuumFailCell("传送机械手上料", TransZone.TransLoadDataStations.ToArray());
                    ClassBaseWorkZone.HandleVacuumFailCell("传送机械手下料", TransZone.TransUnloadDataStations.ToArray());
                    for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                    {
                        ClassWorkZones.Instance.WorkZone顶封边定位.TopAlignDataStations[i].TransferFrom(TransZone.TransLoadDataStations[i]);
                        ClassWorkZones.Instance.WorkZone厚度测量.ThicknessDataStations[i].TransferFrom(TransZone.TransUnloadDataStations[i]);
                    }
                }
                res = TransZone.ActionStartLoad(out AllPick, ClassWorkZones.Instance.DoTransPNPLoad, ClassWorkZones.Instance.AfterTransPNPLoad);
                if (res == null)
                {
                    if (!AllPick)
                    {
                        res = new ErrorInfoWithPause("吸料后真空检测错误", ErrorLevel.Alarm, true);
                        ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                        oldres = res;
                    }
                    else
                    {
                        res = TransZone.ActionMove(ClassZone传送机械手.EnumPointPNPX.Unload);
                        if (res != null)
                            ClassErrorHandle.ShowError(source, res);
                        else
                        {
                            ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                            DoneReturn("TransPNPPickFinish");
                        }
                    }
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
    /// Summary description for TransPNPWorkPlacePart
    /// </summary>
    public class TransPNPWorkPlacePart : BaseState
    {
        //bool EventTransPNPPickFinish = false;
        public TransPNPWorkPlacePart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                switch (inputEventArg.eventName)
                {
                    case "TransPNPPickFinish": //From state PickPart
                    case "WaitCCDReady":
                        if (!ClassWorkZones.Instance.WorkZone尺寸测量.IsWorkFree ||
                            !MotorSafetyCheck.InPositionRange(ClassWorkZones.Instance.WorkZone尺寸测量.AxisOutlineMeasX,
                            ClassZone尺寸测量.EnumPointX.GetPart))
                        {
                            DoneReturn("WaitCCDReady");
                            return;
                        }
                        base.run(inputEventArg);
                        oldres = null;
                        ErrorReturnHandler("传送PNP放料", "", ErrorDialogResult.Retry);
                        break;
                }
            }
        }
        private ErrorInfoWithPause res = null;
        private ErrorInfoWithPause oldres = null;
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (!owner.IsRunning) return;
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                ClassZone传送机械手 TransZone = ClassWorkZones.Instance.WorkZone传送机械手;
                ClassBaseWorkZone.HandleVacuumFailCell("传送机械手上料", TransZone.TransLoadDataStations.ToArray());
                ClassBaseWorkZone.HandleVacuumFailCell("传送机械手下料", TransZone.TransUnloadDataStations.ToArray());
                //ClassWorkZones.Instance.WorkZone尺寸测量.ActionToGetPart();
                res = TransZone.ActionStartUnload(ClassWorkZones.Instance.DoTransPNPUnload, ClassWorkZones.Instance.AfterTransPNPUnload);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("TransPNPPlaceFinish");
                }
            }
        }
    }
}