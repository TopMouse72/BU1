using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CDI.GUI
{
    public partial class SettingPanelZone下料传送 : BaseZoneSettingPanel
    {
        private bool EnableSaving = true;
        public SettingPanelZone下料传送() : base()
        {
            InitializeComponent();
        }
        protected override void AssignParameter()
        {
            EnableSaving = false;
            base.AssignParameter();
            selectBoxIgnoreSMEMA.Checked = ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored;
            EnableSaving = true;
        }

        private void selectBoxIgnoreSMEMA_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSaving)
            {
                ClassCommonSetting.SysParam.UnloadOutSMEMAIgnored = selectBoxIgnoreSMEMA.Checked;
                ClassCommonSetting.SysParam.SaveParameter();
                if (WorkZone.ZoneManualPanel != null) WorkZone.ZoneManualPanel.RefreshParameter();
            }
        }
    }
}
