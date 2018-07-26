using System.Windows.Forms;
using System.Threading;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;
using System;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for CDIMainIdle
    /// </summary>
    public class CDIMainIdle : BaseState
    {
        public CDIMainIdle(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
    }

    /// <summary>
    /// Summary description for CDIMainCheckLoadIn
    /// </summary>
    public class CDIMainCheckLoadIn : BaseState
    {
        public CDIMainCheckLoadIn(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                switch (inputEventArg.eventName)
                {
                    case "Start": //From state Idle
                    case "NotReady": //From state CheckLoadIn
                    case "LoadinFinish": //From state StartLoadin
                        ErrorInfoWithPause res = ClassWorkZones.Instance.WorkZone上料传送.CheckLoadReady();
                        if (!ClassWorkZones.Instance.WorkZone上料机械手.isPicking && res == null)
                        {
                            if (ClassWorkFlow.Instance.LoadMode == EnumLoadMode.手动)
                            {
                                Thread.Sleep(ClassCommonSetting.SysParam.ManualLoadDelayTime);
                                //重新检测，防止误操作
                                res = ClassWorkZones.Instance.WorkZone上料传送.CheckLoadReady();
                                if (!(!ClassWorkZones.Instance.WorkZone上料机械手.isPicking && res == null))
                                {
                                    DoneReturn("NotReady");
                                    return;
                                }
                            }
                            DoneReturn("NewCellReady");
                            return;
                        }
                        else if (res != null && res.Message == "停止上料中")
                            Thread.Sleep(5000);
                        DoneReturn("NotReady");
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainStartLoadin
    /// </summary>
    public class CDIMainStartLoadin : BaseState
    {
        TimeClass timer = new TimeClass();
        public CDIMainStartLoadin(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
            timer.ThreadTimerTickEvent += TimerTick;
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            timer.StartTimer(5000, 5000);
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            timer.StopTimer();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            //Statistic.WFStatus = WorkFlowStatus.待料;
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.待料, "没有上料", true);
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                //base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "NewCellReady": //From state CheckLoadIn
                        timer.StopTimer();
                        //Statistic.WFStatus = WorkFlowStatus.运行;
                        Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.运行, "开始上料");
                        if (ClassWorkFlow.Instance.FeedNewPart)
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "开始上料：" + (ClassWorkFlow.Instance.GetCell + 1).ToString());
                        else
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "开始上料：" + (ClassWorkFlow.Instance.GetCell).ToString());

                        ErrorInfoWithPause res = ClassWorkZones.Instance.WorkZone上料传送.ActionStartLoad(ClassWorkZones.Instance.AfterLoadInLoad);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("上料传送带上料", res);
                            return;
                        }
                        if (ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations[ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations.Length - 2].CellData != null &&
                             ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations[ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations.Length - 2].CellData.Barcode == "")
                            Statistic.GetProductInfo(DateTime.Now, 1);
                        oldres = null;
                        ErrorReturnHandler("上料传送带上料", "", ErrorDialogResult.Retry);
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
                ClassZone上料传送 LoadinZone = ClassWorkZones.Instance.WorkZone上料传送;
                if (message != "")
                {
                    Colibri.CommonModule.Forms.BaseForm.DoInvokeRequired(MainForm.instance, () => MessageBox.Show(MainForm.instance, $"无数据有电芯的错误处理：取出不符的多余电芯。{Environment.NewLine}" +
                        $"有数据无电芯的错误处理：无需处理。点确定后数据将会被删除忽略掉。{Environment.NewLine}{Environment.NewLine}" +
                        $"处理完成后点确定。"));
                    LoadinZone.SetLoadin();
                }
                res = LoadinZone.CheckLoadin();
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    timer.StartTimer(5000);
                    DoneReturn("LoadinFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "上料结束：" + (ClassWorkFlow.Instance.GetCell).ToString());
                }

            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainStartLoadPNP
    /// </summary>
    public class CDIMainStartLoadPNP : BaseState
    {
        bool isRunning;
        string parts;
        public CDIMainStartLoadPNP(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            isRunning = false;
            base.StartHandler(sender, e);
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ClassWorkFlow.Instance.LoadPNPLoadSM.StopHandler(sender, e);
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "LoadinFinish": //From state StartLoadin
                    case "WaitPNPFree":
                        if (ClassWorkZones.Instance.WorkZone上料传送.LoadInDataStations[0].CellData == null)// && !ClassWorkZones.Instance.WorkZone上料传送.ThisInport(ClassZone上料传送.EnumInportName.LoadInConvInPos).status)
                        {
                            return;
                        }
                        if (isRunning)
                        {
                            DoneReturn("WaitPNPFree");
                        }
                        else if (owner.IsRunning)
                        {
                            isRunning = true;
                            parts = ClassWorkZones.Instance.WorkZone上料传送.GetDataInfoString();
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "上料机械手开始取物料: " + parts);
                            ClassWorkFlow.Instance.LoadPNPLoadSM.subscribeMeToResponseEvents(this);
                            ClassWorkFlow.Instance.LoadPNPLoadSM.StartHandler(this, null);
                        }
                        break;
                }
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            switch (e.eventName)
            {
                case "LoadPNPPlacePartFinish":
                    DoneReturn("LoadPNPPlaceFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "上料机械手放物料结束: " + parts);
                    break;
                case "LoadPNPPlaceNGFinish":
                    ClassWorkFlow.Instance.LoadPNPLoadSM.unsubscribeMeFromResponseEvents(this);
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "上料机械手放物料结束 ");
                    isRunning = false;
                    break;
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainStartTopAlign
    /// </summary>
    public class CDIMainStartTopAlign : BaseState
    {
        string parts;
        public CDIMainStartTopAlign(BaseStateMachine ownerObject, string stateName)
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
                    case "LoadPNPPlaceFinish": //From state StartLoadPNP
                        parts = ClassWorkZones.Instance.WorkZone顶封边定位.GetDataInfoString();
                        ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "顶峰定位开始: " + parts);
                        oldres = null;
                        ErrorReturnHandler("顶封边定位", "", ErrorDialogResult.Retry);
                        break;
                    case "TransPNPPickFinish": //From state StartTransPNP
                        ClassWorkZones.Instance.WorkZone顶封边定位.isTopAlignFree = true;
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
                ClassBaseWorkZone.HandleVacuumFailCell(source, ClassWorkZones.Instance.WorkZone顶封边定位.TopAlignDataStations.ToArray());
                res = ClassWorkZones.Instance.WorkZone顶封边定位.ActionStartTopAlignWorkFlow(false);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("TopAlignFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "顶峰定位结束: " + parts);
                }
            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainTransPNPReady
    /// </summary>
    public class CDIMainTransPNPReady : BaseState
    {
        public CDIMainTransPNPReady(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            ((CDIMainStateMachine)owner).EventTopAlignFinish = false;
            ((CDIMainStateMachine)owner).EventThichnessMeasFinish = true;
            ((CDIMainStateMachine)owner).EventUnloadPickFinish = true;
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "TopAlignFinish": //From state StartTopAlign
                        ((CDIMainStateMachine)owner).EventTopAlignFinish = true;
                        break;
                    case "ThichnessMeasFinish": //From state StartThicknessMeas
                        ((CDIMainStateMachine)owner).EventThichnessMeasFinish = true;
                        if (!ClassWorkFlow.Instance.FeedNewPart &&
                            !ClassWorkZones.Instance.WorkZone上料机械手.HavePart() &&
                            !ClassWorkZones.Instance.WorkZone顶封边定位.HavePart() &&
                            !ClassWorkZones.Instance.WorkZone上料传送.HavePart())
                            ((CDIMainStateMachine)owner).EventTopAlignFinish = true;
                        break;
                    case "UnloadPickFinish": //From state StartUnloadPNP
                        ((CDIMainStateMachine)owner).EventUnloadPickFinish = true;
                        break;
                }
                if (((CDIMainStateMachine)owner).EventTopAlignFinish && ((CDIMainStateMachine)owner).EventThichnessMeasFinish/* && ((CDIMainStateMachine)owner).EventUnloadPickFinish*/)
                {
                    if (!ClassWorkZones.Instance.WorkZone顶封边定位.HavePart() && !ClassWorkZones.Instance.WorkZone厚度测量.HavePart()) return;
                    ((CDIMainStateMachine)owner).EventTopAlignFinish = false;
                    ((CDIMainStateMachine)owner).EventThichnessMeasFinish = false;
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "厚度测量和顶峰定位完成");
                    DoneReturn("CellWorkAllFinish");
                }
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainStartThicknessMeas
    /// </summary>
    public class CDIMainStartThicknessMeas : BaseState
    {
        string parts;
        public CDIMainStartThicknessMeas(BaseStateMachine ownerObject, string stateName)
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
                    case "TransPNPFinish": //From state StartTransPNP
                        parts = ClassWorkZones.Instance.WorkZone厚度测量.GetDataInfoString();
                        ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "厚度测量开始: " + parts);
                        oldres = null;
                        ErrorReturnHandler("厚度测量", "", ErrorDialogResult.Retry);
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
                ClassBaseWorkZone.HandleVacuumFailCell(source, ClassWorkZones.Instance.WorkZone厚度测量.ThicknessDataStations.ToArray());
                res = ClassWorkZones.Instance.WorkZone厚度测量.ActionStartThicknessMeas(DataComp.AddAll);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("ThichnessMeasFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "厚度测量结束: " + parts);
                }
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainStartCCDMeas
    /// </summary>
    public class CDIMainStartCCDMeas : BaseState
    {
        string parts;
        public CDIMainStartCCDMeas(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                //doneArgs.eventName = "CCDMeasFinish";
                switch (inputEventArg.eventName)
                {
                    case "TransPNPFinish": //From state StartTransPNP
                        if (ClassWorkZones.Instance.WorkZone尺寸测量.HavePart())
                        {
                            ClassWorkZones.Instance.WorkZone尺寸测量.IsWorkFree = false;
                            parts = ClassWorkZones.Instance.WorkZone尺寸测量.GetDataInfoString();
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "CCD尺寸测量开始: " + parts);
                            oldres = null;
                            ErrorReturnHandler("CCD测量", "", ErrorDialogResult.Retry);
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
                ClassBaseWorkZone.HandleVacuumFailCell(source, ClassWorkZones.Instance.WorkZone尺寸测量.CCDMeasDataStations.ToArray());
                res = ClassWorkZones.Instance.WorkZone尺寸测量.ActionStartCCDMeas(DataComp.AddAll);
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                    DoneReturn("CCDMeasFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "CCD尺寸测量流程结束: " + parts);
                }
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainStartUnloadPNP
    /// </summary>
    public class CDIMainStartUnloadPNP : BaseState
    {
        bool EventCCDMeasFinish = false;
        bool EventTransPNPMoveLeftFinish = false;
        bool UnloadPNPFinish = true;
        string parts;
        public CDIMainStartUnloadPNP(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            EventCCDMeasFinish = false;
            EventTransPNPMoveLeftFinish = false;
            UnloadPNPFinish = true;
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ClassWorkFlow.Instance.UnloadPNPWorkSM.StopHandler(sender, e);
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                //doneArgs.eventName = "UnloadPNPFinish";
                //doneArgs.eventName = "UnloadPickFinish";
                switch (inputEventArg.eventName)
                {
                    case "CCDMeasFinish": //From state StartCCDMeas
                        EventCCDMeasFinish = true;
                        break;
                    case "TransPNPMoveLeftFinish":
                        EventTransPNPMoveLeftFinish = true;
                        break;
                }
                if (EventCCDMeasFinish)
                {
                    if (!UnloadPNPFinish)
                    {
                        DoneReturn("WaitPNPFree");
                    }
                    else
                    {
                        EventCCDMeasFinish = false;
                        EventTransPNPMoveLeftFinish = false;
                        UnloadPNPFinish = false;
                        parts = ClassWorkZones.Instance.WorkZone尺寸测量.GetDataInfoString();
                        ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "下料机械手开始取物料: " + parts);
                        ClassWorkFlow.Instance.UnloadPNPWorkSM.subscribeMeToResponseEvents(this);
                        ClassWorkFlow.Instance.UnloadPNPWorkSM.StartHandler(this, null);
                    }
                }
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            switch (e.eventName)
            {
                case "CCDMotorBackToGetPart":
                    DoneReturn("UnloadPickFinish");
                    break;
                case "UnloadPNPPlaceFinish":
                    UnloadPNPFinish = true;
                    ClassWorkFlow.Instance.UnloadPNPWorkSM.unsubscribeMeFromResponseEvents(this);
                    DoneReturn("UnloadPlaceFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "下料机械手放物料结束: " + parts);
                    break;
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainStartUnloadout
    /// </summary>
    public class CDIMainStartUnloadout : BaseState
    {
        public CDIMainStartUnloadout(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                //doneArgs.eventName = "Unload";
                switch (inputEventArg.eventName)
                {
                    case "ReadyUnload":
                        ErrorInfoWithPause res = ClassWorkZones.Instance.WorkZone下料传送.ActionStartUnload(ClassWorkZones.Instance.AfterLoadOut);
                        if (res != null)
                        {
                            ClassErrorHandle.ShowError("下料传送带下料", res);
                            return;
                        }
                        DoneReturn("Unload");
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainStartTransPNP
    /// </summary>
    public class CDIMainStartTransPNP : BaseState
    {
        string parts;
        public CDIMainStartTransPNP(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ClassWorkFlow.Instance.TransPNPWorkSM.StopHandler(sender, e);
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                if (inputEventArg.eventName != "WaitPNPFree") base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "CellWorkAllFinish": //From state TransPNPReady
                    case "WaitPNPFree":
                        if (ClassWorkFlow.Instance.TransPNPWorkSM.IsRunning)
                        {
                            DoneReturn("WaitPNPFree");
                            return;
                        }
                        else if (owner.IsRunning)
                        {
                            parts = ClassWorkZones.Instance.WorkZone厚度测量.GetDataInfoString() + " " + ClassWorkZones.Instance.WorkZone顶封边定位.GetDataInfoString();
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "传送机械手开始传送物料: " + parts);
                            ClassWorkFlow.Instance.TransPNPWorkSM.subscribeMeToResponseEvents(this);
                            ClassWorkFlow.Instance.TransPNPWorkSM.StartHandler(this, null);
                        }
                        break;
                }
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            switch (e.eventName)
            {
                case "TransPNPPlaceFinish":
                    ClassWorkFlow.Instance.TransPNPWorkSM.unsubscribeMeFromResponseEvents(this);
                    //((CDIMainStateMachine)owner).EventTopAlignFinish = false;
                    //((CDIMainStateMachine)owner).EventThichnessMeasFinish = false;
                    ((CDIMainStateMachine)owner).EventUnloadPickFinish = !ClassWorkZones.Instance.WorkZone尺寸测量.HavePart();
                    DoneReturn("TransPNPFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "传送机械手放物料结束: " + parts);
                    break;
                case "TransPNPPickFinish":
                    DoneReturn("TransPNPPickFinish");
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "传送机械手取物料结束: " + parts);
                    break;
            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainTransPNPMoveLeft
    /// </summary>
    public class CDIMainTransPNPMoveLeft : BaseState
    {
        public CDIMainTransPNPMoveLeft(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                //doneArgs.eventName = "TransPNPMoveLeftFinish";
                switch (inputEventArg.eventName)
                {
                    case "LoadPNPPlaceFinish": //From state StartLoadPNP
                        while (!ClassWorkZones.Instance.WorkZone传送机械手.IsWorkFree)
                        {
                            Thread.Sleep(1);
                            if (!owner.IsRunning) return;
                        }
                        while (!ClassWorkZones.Instance.WorkZone传送机械手.AxisTransPNPX.MoveTo(ClassZone传送机械手.EnumPointPNPX.Load))
                        {
                            if (ClassWorkZones.Instance.WorkZone传送机械手.DispMotionError(ClassWorkZones.Instance.WorkZone传送机械手.AxisTransPNPX, ClassZone传送机械手.EnumPointPNPX.Load) != null)
                                return;
                        }
                        DoneReturn("TransPNPMoveLeftFinish");
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Summary description for CDIMainStartNGSorting
    /// </summary>
    public class CDIMainStartNGSorting : BaseState
    {
        bool isSortingPNPPicked;
        bool isUnloadPNPPlaced;
        string parts;
        ClassZone下料传送 unloadoutzone;
        public CDIMainStartNGSorting(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
            unloadoutzone = ClassWorkZones.Instance.WorkZone下料传送;
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            isSortingPNPPicked = true;
            isUnloadPNPPlaced = false;
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            ClassWorkFlow.Instance.SortingPNPWorkSM.StopHandler(sender, e);
        }
        private void StartSorting()
        {
            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "执行StartSorting");
            if (ClassWorkFlow.Instance.SortingPNPWorkSM.IsRunning)
            {
                DoneReturn("WaitPNPFree");
            }
            else if (owner.IsRunning)
            {
                parts = ClassWorkZones.Instance.WorkZone下料传送.GetDataInfoString();
                ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "NG挑选开始: " + parts);
                ClassWorkFlow.Instance.SortingPNPWorkSM.subscribeMeToResponseEvents(this);
                ClassWorkFlow.Instance.SortingPNPWorkSM.StartHandler(this, null);
            }
        }
        private void CheckNGSorting()
        {
            if (isUnloadPNPPlaced && isSortingPNPPicked)
            {
                isUnloadPNPPlaced = false;
                isSortingPNPPicked = false;
                while (!ClassWorkZones.Instance.WorkZone下料机械手.ThisInport(ClassZone下料机械手.EnumInportName.UnloadPNPCyUp).status)
                    Application.DoEvents();
                ErrorInfoWithPause res = unloadoutzone.ActionStartShift(ClassWorkZones.Instance.AfterLoadOutShift);
                if (res != null)
                {
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, $"NG挑选: {res}");
                    ClassErrorHandle.ShowError("NG挑选", res);
                    return;
                }
                if (!ClassWorkFlow.Instance.IsGRR && ClassWorkFlow.Instance.WorkMode == EnumWorkMode.正常)
                    Statistic.GetProductInfo(DateTime.Now, unloadoutzone.GoolParts, unloadoutzone.NGParts);
                if (unloadoutzone.NeedSorting)
                    StartSorting();
                else
                {
                    DoneReturn("SortingPickFinish");
                    isSortingPNPPicked = true;
                    ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "NG挑选没有NG料: " + parts);
                    CheckNGSorting();
                }
            }
            else if (!unloadoutzone.IsPlacingPart && unloadoutzone.UnloadOutDataStations[5].CellData == null && unloadoutzone.UnloadOutDataStations[4].CellData == null && unloadoutzone.UnloadOutDataStations[3].CellData == null)
            {
                unloadoutzone.AxisUnloadOutConveyor.SetZero();
                unloadoutzone.AxisUnloadOutConveyor.MoveTo(5 * ClassDataInfo.CELLPITCH);
                ClassWorkZones.Instance.AfterLoadOut();
            }
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                base.run(inputEventArg);
                switch (inputEventArg.eventName)
                {
                    case "UnloadPlaceFinish": //From state StartUnloadout
                        isUnloadPNPPlaced = true;
                        CheckNGSorting();
                        break;
                    case "WaitPNPFree":
                        StartSorting();
                        break;
                }
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            if (e.eventName == "SortingPNPPickFinish")
            {
                ClassWorkFlow.Instance.SortingPNPWorkSM.unsubscribeMeFromResponseEvents(this);
                DoneReturn("SortingPickFinish");
                isSortingPNPPicked = true;
                ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "NG挑选取物料结束: " + parts);
                CheckNGSorting();
            }
        }
    }

    /// <summary>
    /// Summary description for CDIMainCheckUnloadOut
    /// </summary>
    public class CDIMainCheckUnloadOut : BaseState
    {
        public CDIMainCheckUnloadOut(BaseStateMachine ownerObject, string stateName)
            : base(ownerObject, stateName)
        {
        }
        public override void run(StateEventArgs inputEventArg)
        {
            lock (runLock)
            {
                switch (inputEventArg.eventName)
                {
                    case "SortingPickFinish":
                    case "Unload":
                    case "NotReady":
                        if (ClassWorkZones.Instance.WorkZone下料传送.IsUnLoadOutEmpty)
                        {
                            ClassCommonSetting.SMLogInfo(this.owner.Name, this.Name, "下料传送带上没有物料，下料检查停止。");
                            return;
                        }
                        if (!ClassWorkZones.Instance.WorkZone下料传送.CheckUnloadReady())
                        {
                            DoneReturn("NotReady");
                        }
                        else
                        {
                            base.run(inputEventArg);
                            DoneReturn("ReadyUnload");
                        }
                        break;
                }
            }
        }
    }
}