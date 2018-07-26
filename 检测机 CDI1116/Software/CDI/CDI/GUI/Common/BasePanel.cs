using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class BasePanel : UserControl, ISystemSubscriber, IIOSubscriber
    {
        private ClassBaseWorkZone _workzone;
        public virtual ClassBaseWorkZone WorkZone
        {
            get { return _workzone; }
            set { _workzone = value; AssignParameter(); }
        }
        public BasePanel()
        {
            InitializeComponent();
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(this);
        }
        #region Event
        public virtual void EStopHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(true);
        }

        public virtual void LoginHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(true);
        }

        public virtual void LogoutHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(false);
        }

        public virtual void ModeChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(true);
        }

        public virtual void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ProgramExitHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ResetHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(false);
        }

        public virtual void ResumeHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(false);
        }

        public virtual void RetryHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void StartHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(false);
        }

        public virtual void StopHandler(BaseClass sender, StateEventArgs e)
        {
            SetAllControlEnable(true);
        }

        public virtual void DoneHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void ErrorHandler(BaseClass sender, FailureException e)
        {
        }

        public virtual void EnableChangeHandler(BaseClass sender, bool Enabled)
        {
        }

        public virtual void InvalidateHandler(BaseClass sender)
        {
        }

        public virtual void InputOnHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void InputOffHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void InputActiveHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void InputDeActiveHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void OutputOnHandler(BaseClass sender, StateEventArgs e)
        {
        }

        public virtual void OutputOffHandler(BaseClass sender, StateEventArgs e)
        {
        }
        #endregion Event
        protected virtual void SetAllControlEnable(bool Enable)
        {
            //foreach (Control con in Controls)
            //    BaseForm.DoInvokeRequired(con, () => con.Enabled = Enable);
        }
        protected virtual void AssignParameter() { }
        public void CellDataRefresh(DataDisp disp)
        {
            BaseForm.DoInvokeRequired(disp, () => disp.RefreshData());
        }
    }
}