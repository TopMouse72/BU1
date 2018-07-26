using System;
using System.Collections;
using System.Collections.Generic;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.StateMachine
{
    /// <summary>
    /// Summary description for TransPNPWorkStateMachine
    /// </summary>
    public class TransPNPWorkStateMachine : BaseStateMachine
    {
        DateTime start;
        DateTime pick, place;
        public TransPNPWorkStateMachine(BaseClass ownerObject, string ExtendName = "")
            : base("TransPNPWorkStateMachine", ownerObject, ExtendName)
        {
            // TODO: Add constract code here, if needed.
            
        }
        protected override void DoOnStart()
        {
            base.DoOnStart();
            ClassWorkZones.Instance.WorkZone���ͻ�е��.IsWorkFree = false;
            start = DateTime.Now;
        }
        public override void NextHandle(BaseState sender, StateEventArgs e)
        {
            if (e.eventName == "TransPNPPlaceFinish")
            {
                ClassWorkZones.Instance.WorkZone���ͻ�е��.IsWorkFree = true;
                place = DateTime.Now;
                ClassWorkFlow.Instance.TimeUsage.UpdateTimeUsage(TimeUsageItem.ZoneTransPNP, (place - start).TotalSeconds,
                    string.Format("ץ��о: {0:0.00}s\n�ŵ�о: {1:0.00}s", (pick - start).TotalSeconds, (place - pick).TotalSeconds));
                StateMachineFinish(e);
            }
            else
            {
                if (e.eventName == "TransPNPPickFinish")
                {
                    pick = DateTime.Now;
                    notifyDoneEventSubscribers(this, e);
                }
                getNextState(sender, e);
            }
        }
    }
}
