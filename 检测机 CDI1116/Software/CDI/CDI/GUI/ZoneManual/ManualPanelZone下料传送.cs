using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule.Forms;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class ManualPanelZone下料传送 : BaseZoneManualPanel
    {
        public ManualPanelZone下料传送()
        {
            InitializeComponent();
        }
        private ClassZone下料传送 zone
        {
            get { return (ClassZone下料传送)OwnerZone; }
        }
        protected override void Init()
        {
            zone.AddDisp(dataDispCell6, dataDispCell5, dataDispCell4, dataDispCell3, dataDispCell2, dataDispCell1);

            motorUCUnloadOutConveyor.Axis = zone.AxisUnloadOutConveyor;
        }
        public override void RefreshCellData(object sender, EventArgs e)
        {
            lock (RefreshLock)
            {
                base.RefreshCellData(sender, e);
                CellDataRefresh(dataDispCell1);
                CellDataRefresh(dataDispCell2);
                CellDataRefresh(dataDispCell3);
                CellDataRefresh(dataDispCell4);
                CellDataRefresh(dataDispCell5);
                CellDataRefresh(dataDispCell6);
            }
        }
        public override void RefreshParameter()
        {
            base.RefreshParameter();
            selectBoxIgnoreSMEMA.Checked = ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored;
        }

        private void selectBoxSMEMAReady_Click(object sender, EventArgs e)
        {
            zone.ActionSMEMAReady(selectBoxSMEMAReady.Checked);
        }

        private void buttonStartUnload_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartUnload(ClassWorkZones.Instance.AfterLoadOut); });
        }

        private void buttonStartShift_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartShift(ClassWorkZones.Instance.AfterLoadOutShift); });
        }

        private void buttonJogStart_Click(object sender, EventArgs e)
        {
            zone.AxisUnloadOutConveyor.JogSpeed = Math.Abs(zone.AxisUnloadOutConveyor.JogSpeed);
            if (!selectBoxDirection.Checked)
                zone.AxisUnloadOutConveyor.JogSpeed = -zone.AxisUnloadOutConveyor.JogSpeed;
            zone.AxisUnloadOutConveyor.Jog();
        }

        private void buttonJogStop_Click(object sender, EventArgs e)
        {
            zone.AxisUnloadOutConveyor.StopMove();
            zone.AxisUnloadOutConveyor.JogSpeed = Math.Abs(zone.AxisUnloadOutConveyor.JogSpeed);
        }
    }
}
