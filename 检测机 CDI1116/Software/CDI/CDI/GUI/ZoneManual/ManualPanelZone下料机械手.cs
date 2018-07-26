using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDI.Zone;
using CDI.StateMachine;
using Colibri.CommonModule;

namespace CDI.GUI
{
    public partial class ManualPanelZone下料机械手 : BaseZoneManualPanel
    {
        public ManualPanelZone下料机械手()
        {
            InitializeComponent();
        }
        private ClassZone下料机械手 zone
        {
            get { return (ClassZone下料机械手)OwnerZone; }
        }
        protected override void Init()
        {
            base.Init();
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCUnloadPNPY.Axis = zone.AxisUnloadPNPY;
        }
        public override void RefreshCellData(object sender, EventArgs e)
        {
            lock (RefreshLock)
            {
                base.RefreshCellData(sender, e);
                CellDataRefresh(DataDispCellLeft);
                CellDataRefresh(DataDispCellMiddle);
                CellDataRefresh(DataDispCellRight);
            }
        }
        private void buttonPickPart_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionMove(ClassZone下料机械手.EnumPoint.Pick);
                if (res != null) return res;
                else
                    return zone.ActionUnloadPNPStartPick(ClassWorkZones.Instance.DoLoadOutPNPPick, ClassWorkZones.Instance.AfterLoadOutPNPPick);
            });
        }

        private void buttonPlacePart_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionMove(ClassZone下料机械手.EnumPoint.Place);
                if (res != null) return res;
                else
                    return zone.ActionUnloadPNPStartPlace(ClassWorkZones.Instance.DoLoadOutPNPPlace, ClassWorkZones.Instance.AfterLoadOutPNPPlace);
            });
        }

        private void buttonWorkFlow_Click(object sender, EventArgs e)
        {
            StartSM(sender, ClassWorkFlow.Instance.UnloadPNPWorkSM, "UnloadPNPPlaceFinish");
        }
    }
}