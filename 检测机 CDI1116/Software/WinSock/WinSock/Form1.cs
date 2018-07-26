using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.Forms;
using NetWork;
using Measure;
using Colibri.CommonModule.State;

namespace WinSockTest
{
    public partial class Form1 : Form, ILogControl,IResponseSubscriber
    {
        #region Data
        private MeasParameter.Struct_CCDDatas[] meas = new MeasParameter.Struct_CCDDatas[3];
        #endregion Data
        private WinSock socket = new WinSock();
        public Form1()
        {
            InitializeComponent();
            LogFile.instance.Start();
            socket.WaitingConnect();
            LogFile.instance.AddLogControl(this);
            this.LogLevel = LogFile.Level.Debug;
            timer1.Start();
        }

        public LogFile.Level LogLevel { get; set; }
        object IDataControl.Owner { get; set; }

        public void TransferData(string data)
        {
            BaseForm.DoInvokeRequired(textBox2, () => textBox2.Text += data + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = socket.SendCommandProductChange("NewTest");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = socket.SendCommandCamTrig(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = socket.SendCommandCamTrig(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = socket.SendCommandCamTrig(3);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            socket.GetData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1.Text = socket.SendCommandCamTrig(1)
            + socket.SendCommandCamTrig(2)
            + socket.SendCommandCamTrig(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            socket.subscribeMeToResponseEvents(this);
            label1.Text = socket.SendCommandCamTrig(1);
            LogFile.instance.LogInfo("发送相机触发1");
        }
        private int GetData(string data)
        {
            string[] datas = data.Split(',');
            ListBox list = null;
            switch (datas[0])
            {
                case "1": list = listBox1; break;
                case "2": list = listBox2; break;
                case "3": list = listBox3; break;
            }
            if (list != null)
            {
                BaseForm.DoInvokeRequired(list, () =>
                {
                    list.Items.Clear();
                    for (int i = 1; i < datas.Length - 1; i++)
                        list.Items.Add(datas[i]);
                });
            }
            return int.Parse(datas[0]);
        }
        public void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            switch (e.eventName)
            {
                case WinSock.CAPFINISH:
                    if (e.eventInfo == "1")
                    {
                        BaseForm.DoInvokeRequired(label1, () => label1.Text += socket.SendCommandCamTrig(2));
                        LogFile.instance.LogInfo("发送相机触发2");
                    }
                    else if (e.eventInfo == "2")
                    {
                        BaseForm.DoInvokeRequired(label1, () => label1.Text += socket.SendCommandCamTrig(3));
                        LogFile.instance.LogInfo("发送相机触发3");
                    }
                    break;
                case WinSock.MEASDONE:
                    LogFile.instance.LogInfo("CCD测量完成" + GetData(e.eventInfo));
                    break;
            }
        }

        public void ErrorHandler(BaseClass sender, FailureException e)
        {
        }

        public void EnableChangeHandler(BaseClass sender, bool Enabled)
        {
        }

        public void InvalidateHandler(BaseClass sender)
        {
        }
    }
}
