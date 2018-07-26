using System;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for SortingPNPWorkIdle
    /// </summary>
    public class SortingPNPWorkIdle : BaseState
    {
        public SortingPNPWorkIdle(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
    }

    /// <summary>
    /// Summary description for SortingPNPWorkPickPart
    /// </summary>
    public class SortingPNPWorkPickPart : BaseState
    {
        ClassZoneNG挑选机械手 sortingZone = ClassWorkZones.Instance.WorkZoneNG挑选机械手;
        public SortingPNPWorkPickPart(BaseStateMachine ownerObject, string stateName)
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
                        ErrorInfoWithPause res = sortingZone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.Pick);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("NG挑选PNP取料", res);
                            return;
                        }
                        oldres = null;
                        ErrorReturnHandler("NG挑选PNP取料", "", ErrorDialogResult.Retry);
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
                    ClassBaseWorkZone.HandleVacuumFailCell(source, sortingZone.SortNGDataStations.ToArray());
                    for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                        if (ClassWorkZones.Instance.WorkZone下料传送.UnloadOutDataStations[i].CellData == null)
                            ClassWorkZones.Instance.WorkZone下料传送.UnloadOutDataStations[i].TransferFrom(sortingZone.SortNGDataStations[i]);
                }
                res = sortingZone.ActionSortPNPStartPick(ClassWorkZones.Instance.DoSortPNPPick, ClassWorkZones.Instance.AfterSortPNPPick);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("SortingPNPPickFinish");
                }
            }
        }
    }

    /// <summary>
    /// Summary description for SortingPNPWorkPlaceNG
    /// </summary>
    public class SortingPNPWorkPlaceNG : BaseState
    {
        ClassZoneNG挑选机械手 sortingZone = ClassWorkZones.Instance.WorkZoneNG挑选机械手;
        public SortingPNPWorkPlaceNG(BaseStateMachine ownerObject, string stateName)
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
                    case "SortingPNPPickFinish": //From state PickPart
                        oldres = null;
                        ErrorReturnHandler("NG挑选PNP放料", "", ErrorDialogResult.Retry);
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
                res = ClassWorkZones.Instance.WorkZoneNG挑选机械手.UpdateRow();
                if (res == null)
                {
                    ClassBaseWorkZone.HandleVacuumFailCell(source, sortingZone.SortNGDataStations.ToArray());
                    res = sortingZone.ActionSortPNPStartPlaceNG(ClassWorkZones.Instance.AfterSortPNPPlace);
                    if (res != null)
                    {
                        ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                        oldres = res;
                    }
                    else
                    {
                        ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                        DoneReturn("SortingPNPPlaceNGFinish");
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
}