using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule.MotionSystem;
using Colibri.CommonModule.Event;
using CDI.Zone;
using Colibri.CommonModule;
using Colibri.CommonModule.State;

namespace CDI.GUI
{
    /// <summary>
    /// WorkZone手动界面基类
    /// </summary>
    public partial class BaseZoneSettingPanel : BasePanel
    {
        public BaseZoneSettingPanel() : base()
        {
            InitializeComponent();
        }
        private void comboBoxAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBoxAxis = (ComboBox)sender;
            if (comboBoxAxis.SelectedIndex >= 0)
            {
                CAxisBase axis = HardwarePool.SystemHardware.instance.GetAxis(comboBoxAxis.Text);
                axis.ConfigurationControl.BringToFront();
            }
        }
    }
}