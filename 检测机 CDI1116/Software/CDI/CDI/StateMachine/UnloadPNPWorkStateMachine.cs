using System;
using System.Collections;
using System.Collections.Generic;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for UnloadPNPWorkStateMachine
    /// </summary>
    public class UnloadPNPWorkStateMachine : BaseStateMachine
    {
        public UnloadPNPWorkStateMachine(BaseClass ownerObject, string ExtendName = "")
            : base("UnloadPNPWorkStateMachine", ownerObject, ExtendName)
        {
            // TODO: Add constract code here, if needed.

        }
        protected override void DoOnStart()
        {
            base.DoOnStart();
            ClassWorkZones.Instance.WorkZone下料机械手.IsWorkFree = false;
        }
        public override void NextHandle(BaseState sender, StateEventArgs e)
        {
            if (e.eventName == "UnloadPNPPlaceFinish")
            {
                ClassWorkZones.Instance.WorkZone下料机械手.IsWorkFree = true;
                StateMachineFinish(e.eventName, e.eventInfo);
            }
            else
            {
                if (e.eventName == "CCDMotorBackToGetPart")
                    notifyDoneEventSubscribers(this, e);
                getNextState(sender, e);
            }
        }
    }
}