using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule.Forms;
using System.Drawing;
using Colibri.CommonModule.ToolBox;

namespace CDI.GUI
{
    internal enum enumDispColor
    {
        Wheat,
        Gold,
        GreenYellow,
        DarkSeaGreen,
        PaleGreen,
        YellowGreen,
        DeepSkyBlue,
        SpringGreen,
        Plum,
    }
    public partial class CellTextBox : TextBox, IDataDisp
    {
        private enumDispColor _backcolor;
        private ClassDataStation _dataStation;
        public ClassDataStation DataStation
        {
            get { return _dataStation; }
            set
            {
                _dataStation = value;
                if (_dataStation != null)
                    RefreshData();
            }
        }

        private object RefreshLock = new object();
        public void RefreshData()
        {
            //lock (RefreshLock)
            //{
            if (_dataStation.CellData == null)
            {
                BaseForm.SetControlText(this, "");
                BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.White);
                BaseForm.DoInvokeRequired(this, () => BaseForm.SetHelpTip(this, ""));
            }
            else
            {
                BaseForm.SetControlText(this, _dataStation.CellData.Index.ToString());
                BaseForm.DoInvokeRequired(this, () => BaseForm.SetHelpTip(this, _dataStation.CellData.Barcode));
                _backcolor = (enumDispColor)((_dataStation.CellData.Index - 1) % 9);
                switch (_backcolor)
                {
                    case enumDispColor.Wheat:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.Wheat);
                        break;
                    case enumDispColor.Gold:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.Gold);
                        break;
                    case enumDispColor.GreenYellow:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.GreenYellow);
                        break;
                    case enumDispColor.DarkSeaGreen:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.DarkSeaGreen);
                        break;
                    case enumDispColor.PaleGreen:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.PaleGreen);
                        break;
                    case enumDispColor.YellowGreen:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.YellowGreen);
                        break;
                    case enumDispColor.DeepSkyBlue:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.DeepSkyBlue);
                        break;
                    case enumDispColor.SpringGreen:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.SpringGreen);
                        break;
                    case enumDispColor.Plum:
                        BaseForm.DoInvokeRequired(this, () => this.BackColor = Color.Plum);
                        break;
                }
            }
            //}
        }
    }
    public partial class CellSelectBox : SelectBox, IDataDisp
    {
        public override bool Checked { get => base.Checked; }
        private ClassDataStation _dataStation;
        public ClassDataStation DataStation
        {
            get { return _dataStation; }
            set
            {
                _dataStation = value;
                if (_dataStation != null)
                    RefreshData();
            }
        }

        public void RefreshData()
        {
            Checked = _dataStation != null;
        }
    }
}