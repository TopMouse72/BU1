using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDI.Zone;
using Measure;

namespace CDI.GUI
{
    public partial class ManualPanelZone厚度测量 : BaseZoneManualPanel
    {
        public ManualPanelZone厚度测量()
        {
            InitializeComponent();
        }
        protected override void Init()
        {
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCThicknessMeasY.Axis = zone.AxisThicknessMeasY;
        }
        private ClassZone厚度测量 zone
        {
            get { return (ClassZone厚度测量)OwnerZone; }
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
        public override void RefreshParameter()
        {
            base.RefreshParameter();
            textBoxDelayTime.Text = ClassCommonSetting.SysParam.ThicknessMeasDelayTime.ToString();
            if (ClassCommonSetting.SysParam.CurrentUsedGauge == null)
                MessageBox.Show("当前产品没有选择标准块。");
            else
            {
                textBoxReferenceLeft.Text = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefLeft.ToString();
                textBoxReferenceMid.Text = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefMid.ToString();
                textBoxReferenceRight.Text = ClassCommonSetting.SysParam.CurrentUsedGauge.ThicknessMeasRefRight.ToString();
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionLoad(); });
        }

        private void buttonReadData_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.StartThicknessReading(DataComp.AddAll); });
        }

        private void buttonUnload_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionUnload(); });
        }

        private void buttonMeas_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionStartThicknessMeas(DataComp.AddAll, false); });
        }

        private void buttonCyUp_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.AllCyUp(); });
        }

        private void buttonCyDown_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.AllCyDown(); });
        }

        private void buttonCylinderTest_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionCylinderTest(); });
        }
    }
}