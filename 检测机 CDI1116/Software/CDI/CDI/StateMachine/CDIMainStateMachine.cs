using System;
using System.Collections;
using System.Collections.Generic;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for CDIMainStateMachine
    /// </summary>
    public class CDIMainStateMachine : BaseStateMachine
    {
        public bool EventTopAlignFinish = false;
        public bool EventThichnessMeasFinish = false;
        public bool EventUnloadPickFinish = false;
        public CDIMainStateMachine(BaseClass ownerObject, string ExtendName = "")
            : base("CDIMainStateMachine", ownerObject, ExtendName)
        {
            // TODO: Add constract code here, if needed.

        }
        public override void NextHandle(BaseState sender, StateEventArgs e)
        {
            if (e.eventName == "TODO: End eventname here")
                StateMachineFinish();
            else
            {
                if (e.eventName == "SortingPickFinish")
                {
                    int count = ClassWorkZones.Instance.WorkZoneNGÌôÑ¡»úÐµÊÖ.GetMaxCellIndex();
                    if (ClassWorkFlow.Instance.ProductCount > 0 && count >= ClassWorkFlow.Instance.ProductCount)
                    {
                        while (ClassWorkFlow.Instance.SortingPNPWorkSM.IsRunning) System.Threading.Thread.Sleep(1);
                        notifyDoneEventSubscribers(this, new StateEventArgs("ProductCountFinish", ClassWorkFlow.Instance.ProductCount.ToString()));
                    }
                }
                getNextState(sender, e);
            }
        }
    }
}
