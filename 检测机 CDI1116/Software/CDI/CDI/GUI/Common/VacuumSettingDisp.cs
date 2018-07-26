using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDI.GUI
{
    public partial class VacuumSettingDisp : UserControl
    {
        public VacuumSettingDisp()
        {
            InitializeComponent();
        }

        public Color DispColor { get { return groupBox1.BackColor; } set => groupBox1.BackColor = value; }
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get { return groupBox1.Text; } set => groupBox1.Text = value; }
        public bool VacuumBackEnable
        {
            get { return selectBoxVacuumBackEnabled.Checked; }
            set { selectBoxVacuumBackEnabled.Checked = value; }
        }
        public bool VacuumCentEnable
        {
            get { return selectBoxVacuumCentEnabled.Checked; }
            set { selectBoxVacuumCentEnabled.Checked = value; }
        }

        public bool VacuumFrontEnable
        {
            get { return selectBoxVacuumFrontEnabled.Checked; }
            set { selectBoxVacuumFrontEnabled.Checked = value; }
        }


    }
}
