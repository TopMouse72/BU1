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
    /// <summary>
    /// LoadInZone手动界面
    /// </summary>
    public partial class SettingPanelZone上料传送 : BaseZoneSettingPanel
    {
        private bool EnableSaving = true;
        public SettingPanelZone上料传送() : base()
        {
            InitializeComponent();
        }
        protected override void AssignParameter()
        {
            EnableSaving = false;
            base.AssignParameter();
            selectBoxIgnoreSMEMA.Checked = ClassCommonSetting.SysParam.LoadInSMEMAIgnored;
            EnableSaving = true;
        }

        private void selectBoxIgnoreSMEMA_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSaving)
            {
                ClassCommonSetting.SysParam.LoadInSMEMAIgnored = selectBoxIgnoreSMEMA.Checked;
                ClassCommonSetting.SysParam.SaveParameter();
                if (WorkZone.ZoneManualPanel != null) WorkZone.ZoneManualPanel.RefreshParameter();
            }
        }
    }
}