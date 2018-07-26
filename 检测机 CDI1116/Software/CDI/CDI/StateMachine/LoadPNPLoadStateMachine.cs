using System;
using System.Collections;
using System.Collections.Generic;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for LoadPNPLoadStateMachine
    /// </summary>
    public class LoadPNPLoadStateMachine : BaseStateMachine
    {
        public LoadPNPLoadStateMachine(BaseClass ownerObject, string ExtendName = "")
            : base("LoadPNPLoadStateMachine", ownerObject, ExtendName)
        {
            // TODO: Add constract code here, if needed.

        }
        protected override void DoOnStart()
        {
            base.DoOnStart();
            ClassWorkZones.Instance.WorkZone上料机械手.IsWorkFree = false;
        }
        public override void NextHandle(BaseState sender, StateEventArgs e)
        {
            if (e.eventName == "LoadPNPPlaceNGFinish")
            {
                ClassWorkZones.Instance.WorkZone上料机械手.IsWorkFree = true;
                StateMachineFinish(e);
            }
            else
            {
                if (e.eventName == "LoadPNPPlacePartFinish" || e.eventName == "LoadPNPPickPartFinish")
                    notifyDoneEventSubscribers(this, e);
                getNextState(sender, e);
            }
        }
    }
}