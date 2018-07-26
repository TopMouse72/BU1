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
using CDI.StateMachine;

namespace CDI.GUI
{
    public partial class ManualPanelZone上料传送 : BaseZoneManualPanel
    {
        public ManualPanelZone上料传送() : base()
        {
            InitializeComponent();
        }
        protected override void Init()
        {
            zone.AddDisp(dataDispCell6, dataDispCell5, dataDispCell4, dataDispCell3, dataDispCell2, dataDispCell1);

            motorUCLoadInConveyor.Axis = zone.AxisLoadInConveyor;
        }
        private ClassZone上料传送 zone
        {
            get { return (ClassZone上料传送)OwnerZone; }
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
                BaseForm.SetControlText(labelGetCell, "最新电芯数据索引：" + ClassWorkFlow.Instance.GetCell.ToString());
            }
        }
        public override void RefreshParameter()
        {
            base.RefreshParameter();
            selectBoxIgnoreSMEMA.Checked = ClassCommonSetting.SysParam.LoadInSMEMAIgnored;
        }
        private void buttonStartBarcode_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.StartBarcodeScan(true); });
        }

        private void buttonStartLoad_Click(object sender, EventArgs e)
        {
            //string ready = zone.CheckStartLoadReady();
            //if (ready != "")
            //    DoAction(buttonNewCellReady, () => { return ready; });
            //else
            //{
                DoAction(sender, () => { return zone.ActionStartLoad(ClassWorkZones.Instance.AfterLoadInLoad); });
            //}
        }

        private void buttonNewCellReady_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.CheckLoadReady(); });
        }

        private void selectBoxSMEMAAvailable_Click(object sender, EventArgs e)
        {
            zone.ActionSMEMAAvailable(selectBoxSMEMAAvailable.Checked);
        }

        private void buttonJogStart_Click(object sender, EventArgs e)
        {
            zone.AxisLoadInConveyor.JogSpeed = Math.Abs(zone.AxisLoadInConveyor.JogSpeed);
            if (!selectBoxDirection.Checked)
                zone.AxisLoadInConveyor.JogSpeed = -zone.AxisLoadInConveyor.JogSpeed;
            zone.AxisLoadInConveyor.Jog();
        }

        private void buttonJogStop_Click(object sender, EventArgs e)
        {
            zone.AxisLoadInConveyor.StopMove();
            zone.AxisLoadInConveyor.JogSpeed = Math.Abs(zone.AxisLoadInConveyor.JogSpeed);
        }
    }
}
