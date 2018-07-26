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
    public partial class ManualPanelZone外框架 : BaseZoneManualPanel
    {
        public ManualPanelZone外框架()
        {
            InitializeComponent();
        }
        private ClassZone外框架 zone
        {
            get { return (ClassZone外框架)OwnerZone; }
        }
        private void buttonSystemReset_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionSystemReset(); });
        }
    }
}