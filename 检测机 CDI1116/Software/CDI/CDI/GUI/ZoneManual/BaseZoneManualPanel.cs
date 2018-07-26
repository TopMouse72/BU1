using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Colibri.CommonModule.IOSystem;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class BaseZoneManualPanel : BasePanel
    {
        protected object RefreshLock = new object();
        private ClassBaseWorkZone _owner;
        public ClassBaseWorkZone OwnerZone
        {
            get { return _owner; }
            set { _owner = value; Init(); }
        }
        public BaseZoneManualPanel() : base()
        {
            InitializeComponent();
        }
        protected virtual void Init() { }
        public virtual void RefreshCellData(object sender, EventArgs e) { }
        public virtual void RefreshParameter() { }
        private void GetActionResult(object sender, bool res, string message = "")
        {
            if (res)
            {
                labelStatus.Text = "执行" + ((Control)sender).Text + "正常";
                labelStatus.BackColor = Color.PaleGreen;
                labelStatus.ForeColor = Color.RoyalBlue;
            }
            else
            {
                labelStatus.Text = "执行" + ((Control)sender).Text + "出错";
                if (message != "") labelStatus.Text += ": " + res;
                labelStatus.BackColor = Color.Red;
                labelStatus.ForeColor = Color.Yellow;
            }
        }
        protected void DoAction(object sender, Func<ErrorInfoWithPause> act)
        {
            if (ClassCommonSetting.CheckGeneralAir != null)
                if (ClassCommonSetting.CheckGeneralAir() != null) return;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            labelStatus.Text = "开始执行" + ((Control)sender).Text;
            ErrorInfoWithPause res = act();
            sw.Stop();
            if (res == null)
                labelStatus.Text = "执行" + ((Control)sender).Text + "正常 用时:" + sw.ElapsedMilliseconds.ToString() + "ms";
            else
            {
                string temp = res.Message;
                //if (res.NeedRetry) temp += "(可重试)";
                labelStatus.Text = "执行" + ((Control)sender).Text + "出错: " + temp;
            }
        }
        protected void DoAction(object sender, Func<string> act)
        {
            if (ClassCommonSetting.CheckGeneralAir != null)
                if (ClassCommonSetting.CheckGeneralAir() != null) return;
            labelStatus.Text = "开始执行" + ((Control)sender).Text;
            string res = act();
            if (res == "")
                labelStatus.Text = "执行" + ((Control)sender).Text + "正常";
            else
            {
                //if (retry) res += "(可重试)";
                labelStatus.Text = "执行" + ((Control)sender).Text + "出错: " + res;
            }
        }
        protected void buttonReset_Click(object sender, EventArgs e)
        {
            if (ClassCommonSetting.CheckGeneralAir != null)
                if (ClassCommonSetting.CheckGeneralAir() != null) return;
            labelStatus.Text = "开始复位。";
            _owner.SubscribeMeToResponseEvent(this);
            _owner.DoReset();
        }
        BaseStateMachine currentSM;
        Stopwatch currentSMsw = new Stopwatch();
        string finishevent;
        protected void StartSM(object sender, BaseStateMachine sm, string FinishEventName)
        {
            if (ClassCommonSetting.CheckGeneralAir != null)
                if (ClassCommonSetting.CheckGeneralAir() != null) return;
            if (currentSM != null && currentSM.IsRunning)
                labelStatus.Text = "上一次流程未结束，无法开始新流程。";
            else
            {
                currentSM = sm;
                finishevent = FinishEventName;
                currentSMsw.Reset();
                currentSMsw.Start();
                labelStatus.Text = "开始执行" + ((Control)sender).Text;
                currentSM.subscribeMeToResponseEvents(this);
                currentSM.StartHandler(null, null);
            }
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            if (sender == _owner)
            {
                BaseForm.SetControlText(labelStatus, "复位完成。");
                _owner.UnsubcribeFromResponseEvent(this);
            }
            if (sender == currentSM)
            {
                if (e.eventName == finishevent)
                {
                    currentSMsw.Stop();
                    currentSM.unsubscribeMeFromResponseEvents(this);
                    BaseForm.SetControlText(labelStatus, "流程执行完成 用时:" + currentSMsw.ElapsedMilliseconds.ToString() + "ms");
                }
            }
        }
        public override void ErrorHandler(BaseClass sender, FailureException e)
        {
            base.ErrorHandler(sender, e);
            if (sender == _owner)
            {
                BaseForm.SetControlText(labelStatus, "复位出错：" + e.Message);
                _owner.UnsubcribeFromResponseEvent(this);
            }
            if (sender == currentSM)
            {
                currentSM.unsubscribeMeFromResponseEvents(this);
                BaseForm.SetControlText(labelStatus, "流程执行出错");
            }
        }

        private void buttonPortReset_Click(object sender, EventArgs e)
        {
            _owner.ResetOutPort();
        }
    }
}