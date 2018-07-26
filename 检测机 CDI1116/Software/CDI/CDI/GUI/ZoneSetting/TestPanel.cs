using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;

namespace CDI.GUI
{
    public partial class TestPanel : UserControl
    {
        public TestPanel()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Statistic.IsWorkFlowRunning =
                ClassErrorHandle.isWorkFlowRunning = true;
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.运行, "StartTest");
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Statistic.IsWorkFlowRunning =
                ClassErrorHandle.isWorkFlowRunning = false;
            Statistic.GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.空闲, "TestEnd");
        }

        private void buttonShowAlarm_Click(object sender, EventArgs e)
        {
            optionBoxResultNG.Checked = true;
            ErrorReturnHandler("TestSource", "TestMessage", ErrorDialogResult.Retry);
        }
        private ErrorInfoWithPause res = null;
        private ErrorInfoWithPause oldres = null;
        private void ErrorReturnHandler(string source, string message, ErrorDialogResult result)
        {
            if (result == ErrorDialogResult.OK || result == ErrorDialogResult.Retry)
            {
                if (optionBoxResultNG.Checked)
                    res = new ErrorInfoWithPause("Test error.", ErrorLevel.Alarm, true, false, ErrorReturnHandler);
                else
                    res = null;
                if (res != null)
                {
                    ClassErrorHandle.ShowError(source, res, ErrorReturnHandler);
                    oldres = res;
                }
                else
                {
                    ClassErrorHandle.CheckAlarmListStatus(source, oldres);
                }
                BaseForm.DoInvokeRequired(listBox1, () =>
                {
                    listBox1.Items.Clear();
                    foreach (ErrorInfoWithPause err in ClassErrorHandle.ErrorList)
                        listBox1.Items.Add(err.ToString());
                });
            }
        }
    }
}
