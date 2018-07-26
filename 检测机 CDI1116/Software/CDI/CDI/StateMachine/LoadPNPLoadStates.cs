using System;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for LoadPNPLoadIdle
    /// </summary>
    public class LoadPNPLoadIdle : BaseState
    {
        public LoadPNPLoadIdle(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
    }

    /// <summary>
    /// Summary description for LoadPNPLoadPickPart
    /// </summary>
    public class LoadPNPLoadPickPart : BaseState
    {
        public LoadPNPLoadPickPart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "Start": //From state Idle
                        ClassWorkZones.Instance.WorkZone上料机械手.isPicking = true;
                        ErrorInfoWithPause res = ClassWorkZones.Instance.WorkZone上料机械手.ActionMove(ClassZone上料机械手.EnumPointY.Pick);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("上料PNP取料", res);
                            return;
                        }
                        oldres = null;
                        ErrorReturnHandler("上料PNP取料", "", ErrorDialogResult.Retry);
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
                if (message != "")
                {
                    ClassBaseWorkZone.HandleVacuumFailCell(source, ClassWorkZones.Instance.WorkZone上料机械手.LoadPNPDataStations.ToArray());
                    for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations[i].TransferFrom(ClassWorkZones.Instance.WorkZone上料机械手.LoadPNPDataStations[i]);
                }
                res = ClassWorkZones.Instance.WorkZone上料机械手.ActionLoadPNPStartPick(ClassWorkZones.Instance.DoLoadInPNPPick, ClassWorkZones.Instance.AfterLoadInPNPPick);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("LoadPNPPickPartFinish");
                    ClassWorkZones.Instance.WorkZone上料机械手.isPicking = false;
                }
            }
        }
    }

    /// <summary>
    /// Summary description for LoadPNPLoadPlacePart
    /// </summary>
    public class LoadPNPLoadPlacePart : BaseState
    {
        public LoadPNPLoadPlacePart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                switch (inputEventArg.eventName)
                {
                    case "LoadPNPPickPartFinish": //From state PickPart
                    case "WaitTopAlignFree":
                        if (inputEventArg.eventName == "LoadPNPPickPartFinish")
                        {
                            if (ClassWorkZones.Instance.WorkZone上料机械手.AllNG)
                            {
                                DoneReturn("AllNG");
                                return;
                            }
                            else
                                while (!ClassWorkZones.Instance.WorkZone上料机械手.AxisLoadPNPY.MoveTo(ClassZone上料机械手.EnumPointY.Buffer, false))
                                {
                                    if (ClassWorkZones.Instance.WorkZone上料机械手.DispMotionError(ClassWorkZones.Instance.WorkZone上料机械手.AxisLoadPNPY, ClassZone上料机械手.EnumPointY.Buffer) != null)
                                        return;
                                }
                        }
                        if (!ClassWorkZones.Instance.WorkZone顶封边定位.isTopAlignFree)
                        {
                            DoneReturn("WaitTopAlignFree");
                        }
                        else
                        {
                            base.run(inputEventArg);
                            ErrorInfoWithPause res = ClassWorkZones.Instance.WorkZone上料机械手.ActionMove(ClassZone上料机械手.EnumPointY.Place);
                            if (res != null)
                            {
                                ClassErrorHandle.ShowError("上料PNP放料", res);
                                return;
                            }
                            oldres = null;
                            ErrorReturnHandler("上料PNP放料", "", ErrorDialogResult.Retry);
                        }
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
                ClassBaseWorkZone.HandleVacuumFailCell(source, ClassWorkZones.Instance.WorkZone上料机械手.LoadPNPDataStations.ToArray());
                res = ClassWorkZones.Instance.WorkZone上料机械手.ActionLoadPNPStartPlace(ClassWorkZones.Instance.DoLoadInPNPPlace, ClassWorkZones.Instance.AfterLoadInPNPPlace);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("LoadPNPPlacePartFinish");
                    ClassWorkZones.Instance.WorkZone顶封边定位.isTopAlignFree = false;
                }
            }
        }
    }
    /// <summary>
    /// Summary description for LoadPNPLoadPlaceNGPart
    /// </summary>
    public class LoadPNPLoadPlaceNGPart : BaseState
    {
        ClassZone上料机械手 LoadZone = ClassWorkZones.Instance.WorkZone上料机械手;
        public LoadPNPLoadPlaceNGPart(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "LoadPNPPlacePartFinish": //From state PlacePart
                    case "AllNG":
                        if (!LoadZone.HaveNG)
                        {
                            DoneReturn("LoadPNPPlaceNGFinish");
                        }
                        else
                        {
                            oldres = null;
                            ErrorReturnHandler("上料PNP放NG料", "", ErrorDialogResult.Retry);
                        }
                        break;
                }
            }
        }
        public override void DoneReturn(string EventName, string EventInfo = "")
        {
            LoadZone.ActionCounter++;
            LoadZone.ActionCounter %= 500;
            if (LoadZone.ActionCounter == 0)
            {
                LoadZone.AxisLoadPNPY.MoveTo(0);
                ClassWorkZones.Instance.WorkZone上料传送.AxisLoadInConveyor.NeedToCheckSafety = false;
                LoadZone.AxisLoadPNPY.Home();
                ClassWorkZones.Instance.WorkZone上料传送.AxisLoadInConveyor.NeedToCheckSafety = true;
                LoadZone.AxisLoadPNPY.MoveTo(ClassZone上料机械手.EnumPointY.Pick);
            }
            base.DoneReturn(EventName, EventInfo);
        }
        private ErrorInfoWithPause res = null;
        private ErrorInfoWithPause oldres = null;
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (!owner.IsRunning) return;
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                res = LoadZone.CheckNGBoxAvaliable();
                if (res == null)
                {
                    res = LoadZone.ActionMove(ClassZone上料机械手.EnumPointY.PlaceNG);
                    if (res != null)
                    {
                        ClassErrorHandle.ShowError(source, res);
                        return;
                    }
                    res = LoadZone.ActionLoadPNPStartPlaceNG(ClassWorkZones.Instance.AfterLoadInPNPPlaceNG);
                    if (res != null)
                    {
                        ClassErrorHandle.ShowError(source, res);
                        return;
                    }
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("LoadPNPPlaceNGFinish");
                }
                else
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
            }
        }
    }
}