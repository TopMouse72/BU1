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
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;

namespace CDI.GUI
{
    public partial class ManualPanelZone传送机械手 : BaseZoneManualPanel
    {
        public ManualPanelZone传送机械手()
        {
            InitializeComponent();
        }
        private ClassZone传送机械手 zone
        {
            get { return (ClassZone传送机械手)OwnerZone; }
        }
        protected override void Init()
        {
            zone.AddLoadDisp(dataDispCellLoadLeft, dataDispCellLoadMid, dataDispCellLoadRight);
            zone.AddUnloadDisp(dataDispCellUnloadLeft, dataDispCellUnloadMid, dataDispCellUnloadRight);

            motorUCTransPNPX.Axis = zone.AxisTransPNPX;
        }
        public override void RefreshCellData(object sender, EventArgs e)
        {
            lock (RefreshLock)
            {
                base.RefreshCellData(sender, e);
                CellDataRefresh(dataDispCellLoadRight);
                CellDataRefresh(dataDispCellLoadMid);
                CellDataRefresh(dataDispCellLoadLeft);
                CellDataRefresh(dataDispCellUnloadRight);
                CellDataRefresh(dataDispCellUnloadMid);
                CellDataRefresh(dataDispCellUnloadLeft);
            }
        }
        private void buttonPick_Click(object sender, EventArgs e)
        {
            bool temp;
            DoAction(sender, () => { return zone.ActionStartLoad(out temp, ClassWorkZones.Instance.DoTransPNPLoad, ClassWorkZones.Instance.AfterTransPNPLoad); });
        }

        private void buttonPlace_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartUnload(ClassWorkZones.Instance.DoTransPNPUnload, ClassWorkZones.Instance.AfterLoadOutPNPPick); });
        }

        private void buttonWorkFlow_Click(object sender, EventArgs e)
        {
            StartSM(sender, ClassWorkFlow.Instance.TransPNPWorkSM, "TransPNPPlaceFinish");
        }
    }
}