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
    public partial class SettingPanelZone厚度测量 : BaseZoneSettingPanel
    {
        public SettingPanelZone厚度测量() : base()
        {
            InitializeComponent();
        }
        protected override void AssignParameter()
        {
            base.AssignParameter();
            textBoxDelayTime.Text = ClassCommonSetting.SysParam.ThicknessMeasDelayTime.ToString();
        }

        private void buttonSavePara_Click(object sender, EventArgs e)
        {
            int temp;
            string error = "";
            double refbase;
            if (!int.TryParse(textBoxDelayTime.Text.Trim(), out temp))
                error += labelDelayTime.Text + Environment.NewLine;
            else
                ClassCommonSetting.SysParam.ThicknessMeasDelayTime = temp;

            if (error == "")
            {
                ClassCommonSetting.SysParam.SaveParameter();
                if (WorkZone.ZoneManualPanel != null) WorkZone.ZoneManualPanel.RefreshParameter();
            }
            else
                MessageBox.Show("数值格式有错误，请重新输入。" + Environment.NewLine + error);
        }
    }
}
