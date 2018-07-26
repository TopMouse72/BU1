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
    public partial class ManualPanelZone上料机械手 : BaseZoneManualPanel
    {
        public ManualPanelZone上料机械手()
        {
            InitializeComponent();
        }
        private ClassZone上料机械手 zone
        {
            get { return (ClassZone上料机械手)OwnerZone; }
        }
        protected override void Init()
        {
            base.Init();
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCLoadPNPY.Axis = zone.AxisLoadPNPY;
            motorUCLoadPNPZ.Axis = zone.AxisLoadPNPZ;

            zone.UpdateNGBoxEvent += UpdateNGBox;
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
        private void UpdateNGBox()
        {
            BaseForm.SetControlText(labelNGBoxLeftCount, zone.NGBoxCellCount[EnumCellIndex.左电芯].ToString());
            BaseForm.SetControlText(labelNGBoxMidCount, zone.NGBoxCellCount[EnumCellIndex.中电芯].ToString());
            BaseForm.SetControlText(labelNGBoxRightCount, zone.NGBoxCellCount[EnumCellIndex.右电芯].ToString());
            ClassCommonSetting.UpdateIOStatus(labelNGBoxFull, zone.NGBoxFullSensor);
            ClassCommonSetting.UpdateIOStatus(labelNGBox, zone.NGBoxSensor);
        }

        private void buttonStartPick_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionMove(ClassZone上料机械手.EnumPointY.Pick);
                if (res != null) return res;
                else
                    return zone.ActionLoadPNPStartPick(ClassWorkZones.Instance.DoLoadInPNPPick, ClassWorkZones.Instance.AfterLoadInPNPPick);
            });
        }

        private void buttonStartPlace_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionMove(ClassZone上料机械手.EnumPointY.Place);
                if (res != null) return res;
                else
                    return zone.ActionLoadPNPStartPlace(ClassWorkZones.Instance.DoLoadInPNPPlace, ClassWorkZones.Instance.AfterLoadInPNPPlace);
            });
        }

        private void buttonStartNGPlace_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionMove(ClassZone上料机械手.EnumPointY.PlaceNG);
                if (res != null) return res;
                else
                    return zone.ActionLoadPNPStartPlaceNG(ClassWorkZones.Instance.AfterLoadInPNPPlaceNG);
            });
        }

        private void buttonWorkFlow_Click(object sender, EventArgs e)
        {
            StartSM(sender, ClassWorkFlow.Instance.LoadPNPLoadSM, "LoadPNPPlaceNGFinish");
        }
    }
}
