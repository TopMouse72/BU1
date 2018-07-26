using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule.Forms;

namespace CDI.GUI
{
    public partial class MonitorPanel : UserControl, IPPMUpdate
    {
        public MonitorPanel()
        {
            InitializeComponent();
            label1.Text = "产量统计（" + (DateTime.Now - new TimeSpan(8, 0, 0)).Date.ToShortDateString() + "）：";
    }

        public void DisplayYieldValue(double Day, double Night, double Total)
        {
            if (optionBoxToday.Checked)
            {
                BaseForm.SetControlText(labelDayYield, string.Format("白班优率：{0:0.00%}", Day));
                BaseForm.SetControlText(labelNightYield, string.Format("晚班优率：{0:0.00%}", Night));
                BaseForm.SetControlText(labelTotalYield, string.Format("合计优率：{0:0.00%}", Total));
            }
        }
        public void DisplayDTValue(double Day,double Night,double Total)
        {
            BaseForm.SetControlText(labelDayDT, string.Format("白班DT：{0:0.00%}", Day));
            BaseForm.SetControlText(labelNightDT, string.Format("晚班DT：{0:0.00%}", Night));
            BaseForm.SetControlText(labelTotalDT, string.Format("合计DT：{0:0.00%}", Total));
        }
        public void ShowPPM(long count, double ppm, double lastppm, TimeSpan laps, TimeSpan singlelaps)
        {
            BaseForm.SetControlText(labelPPM, string.Format("PPM：{1:0.00}", ppm, lastppm));
        }

        private void optionBoxBeforeYesterday_Click(object sender, EventArgs e)
        {
            Statistic.Load(DateTime.Now - new TimeSpan(2, 0, 0, 0));
            label1.Text = "产量统计（" + Statistic.Load(DateTime.Now - new TimeSpan(2, 0, 0, 0)).ToShortDateString() + "）：";
        }

        private void optionBoxYesterday_Click(object sender, EventArgs e)
        {
            ;
            label1.Text = "产量统计（" + Statistic.Load(DateTime.Now - new TimeSpan(1, 0, 0, 0)).ToShortDateString() + "）：";
  }

        private void optionBoxToday_Click(object sender, EventArgs e)
        {
            ;
            label1.Text = "产量统计（" + Statistic.Load(DateTime.Now).ToShortDateString() + "）：";
        }
    }
}