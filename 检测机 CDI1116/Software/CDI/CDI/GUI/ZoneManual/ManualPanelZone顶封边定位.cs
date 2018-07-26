using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class ManualPanelZone顶封边定位 : BaseZoneManualPanel
    {
        public ManualPanelZone顶封边定位()
        {
            InitializeComponent();
        }
        private ClassZone顶封边定位 zone
        {
            get { return (ClassZone顶封边定位)OwnerZone; }
        }
        protected override void Init()
        {
            base.Init();
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCTopAlignBottom.Axis = zone.AxisTopAlignBottom;
            motorUCTopAlignSide.Axis = zone.AxisTopAlignSide;
            motorUCTopAlignTop.Axis = zone.AxisTopAlignTop;
            motorUCTopAlignXSide.Axis = zone.AxisTopAlignXSide;
            motorUCTopAlignZClamp.Axis = zone.AxisTopAlignZClamp;
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

        private void buttonAlign_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionAlign(); });
        }

        private void buttonRelease_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionRelease(); });
        }

        private void buttonWorkFlow_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartTopAlignWorkFlow(false); });
        }

        private void buttonClamp_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartClamp(); });
        }

        private void buttonCylinderTest_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionCylinderTest(); });
        }
    }
}
