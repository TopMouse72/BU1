using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class MessageBoxVacuumHandle : Form
    {
        public static DialogResult Show(string Caption, ClassDataStation[] stations)
        {
            MessageBoxVacuumHandle form = new MessageBoxVacuumHandle();
            form.Text = "真空错误处理: " + Caption;
            form.StationDatas = stations;
            return form.ShowDialog();
        }
        private ClassDataStation[] _stations;
        private ClassDataStation[] StationDatas
        {
            set
            {
                _stations = value;
                if (_stations[(int)EnumCellIndex.左电芯].CellData != null)
                    selectBoxLeft.Checked = _stations[(int)EnumCellIndex.左电芯].CellData.isPickingError;
                else
                    selectBoxLeft.Enabled = false;
                if (_stations[(int)EnumCellIndex.中电芯].CellData != null)
                    selectBoxMiddle.Checked = _stations[(int)EnumCellIndex.中电芯].CellData.isPickingError;
                else
                    selectBoxMiddle.Enabled = false;
                if (_stations[(int)EnumCellIndex.右电芯].CellData != null)
                    selectBoxRight.Checked = _stations[(int)EnumCellIndex.右电芯].CellData.isPickingError;
                else
                    selectBoxRight.Enabled = false;
            }
        }
        public MessageBoxVacuumHandle()
        {
            InitializeComponent();
        }
        private void buttonRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void buttonIgnore_Click(object sender, EventArgs e)
        {
            if (_stations[(int)EnumCellIndex.左电芯].CellData != null) _stations[(int)EnumCellIndex.左电芯].CellData.isPickingError = selectBoxLeft.Checked;
            if (_stations[(int)EnumCellIndex.中电芯].CellData != null) _stations[(int)EnumCellIndex.中电芯].CellData.isPickingError = selectBoxMiddle.Checked;
            if (_stations[(int)EnumCellIndex.右电芯].CellData != null) _stations[(int)EnumCellIndex.右电芯].CellData.isPickingError = selectBoxRight.Checked;

            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
    }
}
