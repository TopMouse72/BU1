using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDI.Zone;
using Colibri.CommonModule.ToolBox;

namespace CDI.GUI
{
    public partial class ManualOperationPanel : UserControl
    {
        private ClassWorkZones _zones = ClassWorkZones.Instance;
        public ManualOperationPanel()
        {
            InitializeComponent();
        }
        public void InitGUI()
        {
            TabPage temp;
            foreach (ClassBaseWorkZone zone in _zones.ToArray())
            {
                temp = new TabPage(zone.Name);
                temp.AutoScroll = true;
                tabControlZone.TabPages.Add(temp);
                temp.Size = tabControlZone.ClientSize;
                temp.Controls.Add(zone.ZoneManualPanel);
                zone.ZoneManualPanel.Dock = DockStyle.Fill;
                //zone.ZoneManualPanel.Size = temp.ClientSize;
                zone.ZoneManualPanel.Top = 0;
                zone.ZoneManualPanel.Left = 0;
            }
            temp = new TabPage("Test");
            temp.Controls.Add(new TestPanel());
            tabControlZone.TabPages.Add(temp);

        }
    }
}