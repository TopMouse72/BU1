using System;
using System.Collections;
using System.Collections.Generic;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for SortingPNPWorkStateMachine
    /// </summary>
    public class SortingPNPWorkStateMachine : BaseStateMachine
    {
        public SortingPNPWorkStateMachine(BaseClass ownerObject, string ExtendName = "")
            : base("SortingPNPWorkStateMachine", ownerObject, ExtendName)
        {
            // TODO: Add constract code here, if needed.
            
        }
        protected override void DoOnStart()
        {
            base.DoOnStart();
            ClassWorkZones.Instance.WorkZoneNG挑选机械手.IsWorkFree = false;
        }
        public override void NextHandle(BaseState sender, StateEventArgs e)
        {
            if (e.eventName == "SortingPNPPlaceNGFinish")
            {
                ClassWorkZones.Instance.WorkZoneNG挑选机械手.IsWorkFree = true;
                StateMachineFinish(e);
                //int count = ClassWorkZones.Instance.WorkZoneNG挑选机械手.GetMaxCellIndex();
                //if (ClassWorkFlow.Instance.ProductCount > 0 && count >= ClassWorkFlow.Instance.ProductCount)
                //    notifyDoneEventSubscribers(this, e);
            }
            else
            {
                if (e.eventName == "SortingPNPPickFinish")
                    notifyDoneEventSubscribers(this, e);
                getNextState(sender, e);
            }
        }
    }
}
