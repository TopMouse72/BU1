using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVSCamera
{
    public partial class CAMSetting : Form
    {
        public CAMSetting()
        {
            InitializeComponent();
            cameraSetting1.DispPanel = panel1;
        }
        private static CAMSetting _instance;
        public static CAMSetting Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CAMSetting();
                }
                return _instance;
            }
        }

        private void CAMSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
