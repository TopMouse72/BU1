using System;
using System.Drawing;
using System.Windows.Forms;
using CDI.StateMachine;
using CDI.Zone;
using Colibri.CommonModule.Forms;

namespace CDI.GUI
{
    public partial class ManualPanelZoneNG挑选机械手 : BaseZoneManualPanel
    {
        public ManualPanelZoneNG挑选机械手()
        {
            InitializeComponent();
        }
        protected override void Init()
        {
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCSortingPNPY.Axis = zone.AxisSortingPNPY;
            motorUCSortingPNPZ.Axis = zone.AxisSortingPNPZ;

            zone.UpdateNGBoxEvent += UpdateNGBox;
        }
        private ClassZoneNG挑选机械手 zone
        {
            get { return (ClassZoneNG挑选机械手)OwnerZone; }
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
            Control temp;
            for (int i = 0; i < 4; i++)
            {
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "LeftCount"];
                BaseForm.SetControlText(temp, zone.NGBoxCellCount[i, (int)EnumCellIndex.左电芯].ToString());
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "MidCount"];
                BaseForm.SetControlText(temp, zone.NGBoxCellCount[i, (int)EnumCellIndex.中电芯].ToString());
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "RightCount"];
                BaseForm.SetControlText(temp, zone.NGBoxCellCount[i, (int)EnumCellIndex.右电芯].ToString());
                temp = groupBoxNGBox.Controls["labelNGBoxIndex" + (i + 1).ToString()];
                BaseForm.DoInvokeRequired(temp, () => temp.BackColor = zone.CurrentNGBoxRow == i ? Color.SkyBlue : Color.Transparent);
                ClassCommonSetting.UpdateIOStatus(groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "Full"], zone.NGBoxFullSensor[i]);
            }
            //ClassCommonSetting.UpdateIOStatus(labelNGBox1Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPNGBoxFull1));
            //ClassCommonSetting.UpdateIOStatus(labelNGBox2Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPNGBoxFull2));
            //ClassCommonSetting.UpdateIOStatus(labelNGBox3Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPNGBoxFull3));
            //ClassCommonSetting.UpdateIOStatus(labelNGBox4Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.ThisInport(ClassZoneNG挑选机械手.EnumInportName.SortingPNPNGBoxFull4));
            ClassCommonSetting.UpdateIOStatus(labelNGBoxBack, zone.NGBoxBackSenser);
            ClassCommonSetting.UpdateIOStatus(labelNGBoxFront, zone.NGBoxFrontSensor);
            BaseForm.SetControlText(labelCurrentUse, zone.IsUseBackNGBox ? "后NG盒" : "前NG盒");
        }

        private void buttonPNPPick_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.Pick); });
        }
        private void buttonPNPNGBox1_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.NGBox1); });
        }

        private void buttonPNPNGBox2_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.NGBox2); });
        }

        private void buttonPNPNGBox3_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.NGBox3); });
        }

        private void buttonPNPNGBox4_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionMove(ClassZoneNG挑选机械手.EnumPointPNPY.NGBox4); });
        }

        private void buttonPNPStartPick_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionSortPNPStartPick(ClassWorkZones.Instance.DoSortPNPPick, ClassWorkZones.Instance.AfterSortPNPPick); });
        }

        private void buttonPNPStartPlace_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionSortPNPStartPlaceNG(ClassWorkZones.Instance.AfterSortPNPPlace); });
        }

        private void buttonAllAction_Click(object sender, EventArgs e)
        {
            ClassZone下料传送 unoadzone = ClassWorkZones.Instance.WorkZone下料传送;
            RefreshCellData(sender, e); 
            StartSM(sender, ClassWorkFlow.Instance.SortingPNPWorkSM, "SortingPNPPlaceNGFinish");
        }
    }
}