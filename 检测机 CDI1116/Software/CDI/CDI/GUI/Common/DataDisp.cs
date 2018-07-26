using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule.Forms;
using Measure;

namespace CDI.GUI

{
    public partial class DataDisp : UserControl, IDataDisp
    {
        public DataDisp()
        {
            InitializeComponent();
        }
        private ClassDataStation _dataStation;
        public ClassDataStation DataStation
        {
            get { return _dataStation; }
            set
            {
                _dataStation = value;
                if (_dataStation != null)
                    Text = _dataStation.StationName;
            }
        }
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return labelName.Text; }
            set { labelName.Text = value; }
        }
        private void UpdateData(TextBox dispcontrol, Cst.Struct_DataInfo data)
        {
            dispcontrol.Text = data.Value.ToString("0.000");
            dispcontrol.BackColor = data.DataNG ? Color.Tomato : Color.RoyalBlue;
        }
        private void ResetData(TextBox dispcontrol)
        {
            dispcontrol.Text = "";
            dispcontrol.BackColor = Color.RoyalBlue;
        }
        private object RefreshLock = new object();
        public void RefreshData()
        {
            if (DataStation != null && DataStation.CellData != null)
            {
                int index = this.DataStation.CellData.Index;
                string barcode = this.DataStation.CellData.Barcode;
                bool loadNG = this.DataStation.CellData.LoadNG;
                bool thickNG = this.DataStation.CellData.ThicknessNG;
                bool ccdNG = this.DataStation.CellData.CCDNG;
                Cst.Struct_DataInfo thickness = this.DataStation.CellData.Data.CellThickness;
                Cst.Struct_DataInfo width = this.DataStation.CellData.Data.CellWidth;
                Cst.Struct_DataInfo length = this.DataStation.CellData.Data.CellLength;
                Cst.Struct_DataInfo nidist = this.DataStation.CellData.Data.NiTabDistance;
                Cst.Struct_DataInfo aldist = this.DataStation.CellData.Data.AlTabDistance;
                Cst.Struct_DataInfo nidistmax = this.DataStation.CellData.Data.NiTabDistanceMax;
                Cst.Struct_DataInfo aldistmax = this.DataStation.CellData.Data.AlTabDistanceMax;
                Cst.Struct_DataInfo nilength = this.DataStation.CellData.Data.NiTabLength;
                Cst.Struct_DataInfo allength = this.DataStation.CellData.Data.AlTabLength;
                Cst.Struct_DataInfo nisealant = this.DataStation.CellData.Data.NiSealantHeight;
                Cst.Struct_DataInfo alsealant = this.DataStation.CellData.Data.AlSealantHeight;
                Cst.Struct_DataInfo tabdist = this.DataStation.CellData.Data.TabDistance;
                Cst.Struct_DataInfo shoulder = this.DataStation.CellData.Data.ShoulderWidth;

                BaseForm.SetControlText(textBoxCellIndex, index.ToString());
                BaseForm.SetControlText(textBoxBarcode, barcode);
                BaseForm.DoInvokeRequired(selectBoxLoadNG, () => selectBoxLoadNG.Checked = loadNG);
                BaseForm.DoInvokeRequired(selectBoxThicknessNG, () => selectBoxThicknessNG.Checked = thickNG);
                BaseForm.DoInvokeRequired(selectBoxCCDNG, () => selectBoxCCDNG.Checked = ccdNG);
                BaseForm.DoInvokeRequired(textBoxDataThickness, () => UpdateData(textBoxDataThickness, thickness));
                BaseForm.DoInvokeRequired(textBoxDataWidth, () => UpdateData(textBoxDataWidth, width));
                BaseForm.DoInvokeRequired(textBoxDataLength, () => UpdateData(textBoxDataLength, length));
                BaseForm.DoInvokeRequired(textBoxDataNiTabDist, () => UpdateData(textBoxDataNiTabDist, nidist));
                BaseForm.DoInvokeRequired(textBoxDataAlTabDist, () => UpdateData(textBoxDataAlTabDist, aldist));
                BaseForm.DoInvokeRequired(textBoxDataNiTabDistMax, () => UpdateData(textBoxDataNiTabDistMax, nidistmax));
                BaseForm.DoInvokeRequired(textBoxDataAlTabDistMax, () => UpdateData(textBoxDataAlTabDistMax, aldistmax));
                BaseForm.DoInvokeRequired(textBoxDataNiTabLen, () => UpdateData(textBoxDataNiTabLen, nilength));
                BaseForm.DoInvokeRequired(textBoxDataAlTabLen, () => UpdateData(textBoxDataAlTabLen, allength));
                BaseForm.DoInvokeRequired(textBoxDataNiSealantHi, () => UpdateData(textBoxDataNiSealantHi, nisealant));
                BaseForm.DoInvokeRequired(textBoxDataAlSealantHi, () => UpdateData(textBoxDataAlSealantHi, alsealant));
                BaseForm.DoInvokeRequired(textBoxDataTabGap, () => UpdateData(textBoxDataTabGap, tabdist));
                BaseForm.DoInvokeRequired(textBoxDataShoulderWidth, () => UpdateData(textBoxDataShoulderWidth, shoulder));
                BaseForm.SetControlText(buttonManualNew, "手动删除");
            }
            else
            {
                BaseForm.SetControlText(textBoxCellIndex, "");
                BaseForm.SetControlText(textBoxBarcode, "");
                BaseForm.DoInvokeRequired(selectBoxLoadNG, () => selectBoxLoadNG.Checked = false);
                BaseForm.DoInvokeRequired(selectBoxThicknessNG, () => selectBoxThicknessNG.Checked = false);
                BaseForm.DoInvokeRequired(selectBoxCCDNG, () => selectBoxCCDNG.Checked = false);
                BaseForm.DoInvokeRequired(textBoxDataThickness, () => ResetData(textBoxDataThickness));
                BaseForm.DoInvokeRequired(textBoxDataWidth, () => ResetData(textBoxDataWidth));
                BaseForm.DoInvokeRequired(textBoxDataLength, () => ResetData(textBoxDataLength));
                BaseForm.DoInvokeRequired(textBoxDataNiTabDist, () => ResetData(textBoxDataNiTabDist));
                BaseForm.DoInvokeRequired(textBoxDataAlTabDist, () => ResetData(textBoxDataAlTabDist));
                BaseForm.DoInvokeRequired(textBoxDataNiTabDistMax, () => ResetData(textBoxDataNiTabDistMax));
                BaseForm.DoInvokeRequired(textBoxDataAlTabDistMax, () => ResetData(textBoxDataAlTabDistMax));
                BaseForm.DoInvokeRequired(textBoxDataNiTabLen, () => ResetData(textBoxDataNiTabLen));
                BaseForm.DoInvokeRequired(textBoxDataAlTabLen, () => ResetData(textBoxDataAlTabLen));
                BaseForm.DoInvokeRequired(textBoxDataNiSealantHi, () => ResetData(textBoxDataNiSealantHi));
                BaseForm.DoInvokeRequired(textBoxDataAlSealantHi, () => ResetData(textBoxDataAlSealantHi));
                BaseForm.DoInvokeRequired(textBoxDataTabGap, () => ResetData(textBoxDataTabGap));
                BaseForm.DoInvokeRequired(textBoxDataShoulderWidth, () => ResetData(textBoxDataShoulderWidth));
                BaseForm.SetControlText(buttonManualNew, "手动新增");
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            double temp;
            string err = "";
            if (DataStation == null || DataStation.CellData == null) return;
            if (!double.TryParse(textBoxDataThickness.Text, out temp))
                err += " 厚度";
            else
                DataStation.CellData.Data.CellThickness.Value = temp;
            if (!double.TryParse(textBoxDataWidth.Text, out temp))
                err += " 宽度";
            else
                DataStation.CellData.Data.CellWidth.Value = temp;
            if (!double.TryParse(textBoxDataLength.Text, out temp))
                err += " 长度";
            else
                DataStation.CellData.Data.CellLength.Value = temp;
            if (!double.TryParse(textBoxDataNiTabDist.Text, out temp))
                err += " NiTab边距";
            else
                DataStation.CellData.Data.NiTabDistance.Value = temp;
            if (!double.TryParse(textBoxDataAlTabDist.Text, out temp))
                err += " AlTab边距";
            else
                DataStation.CellData.Data.AlTabDistance.Value = temp;
            if (!double.TryParse(textBoxDataNiTabDistMax.Text, out temp))
                err += " NiTab最大边距";
            else
                DataStation.CellData.Data.NiTabDistanceMax.Value = temp;
            if (!double.TryParse(textBoxDataAlTabDistMax.Text, out temp))
                err += " AlTab最大边距";
            else
                DataStation.CellData.Data.AlTabDistanceMax.Value = temp;
            if (!double.TryParse(textBoxDataNiTabLen.Text, out temp))
                err += " NiTab长度";
            else
                DataStation.CellData.Data.NiTabLength.Value = temp;
            if (!double.TryParse(textBoxDataAlTabLen.Text, out temp))
                err += " AlTab长度";
            else
                DataStation.CellData.Data.AlTabLength.Value = temp;
            if (!double.TryParse(textBoxDataNiSealantHi.Text, out temp))
                err += " Ni小白胶高度";
            else
                DataStation.CellData.Data.NiSealantHeight.Value = temp;
            if (!double.TryParse(textBoxDataAlSealantHi.Text, out temp))
                err += " Al小白胶高度";
            else
                DataStation.CellData.Data.AlSealantHeight.Value = temp;
            if (!double.TryParse(textBoxDataTabGap.Text, out temp))
                err += " Tab间距";
            else
                DataStation.CellData.Data.TabDistance.Value = temp;
            if (!double.TryParse(textBoxDataShoulderWidth.Text, out temp))
                err += " 肩宽";
            else
                DataStation.CellData.Data.ShoulderWidth.Value = temp;
            if (err != "")
                MessageBox.Show("数据格式错误:" + err);
            else
                RefreshData();
        }
        private void buttonManualNew_Click(object sender, EventArgs e)
        {
            if (DataStation.CellData != null)
            {
                if (MessageBox.Show("确定要删除电芯数据吗？点“是”确定删除，点“否”取消。", "删除电芯数据", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                else
                {
                    DataStation.CellData = null;
                    RefreshData();
                }
            }
            else if (textBoxCellIndex.ReadOnly)
            {
                textBoxCellIndex.ReadOnly = false;
                textBoxCellIndex.BackColor = SystemColors.Control;
                textBoxCellIndex.Text = "-1";
                textBoxCellIndex.SelectAll();
                textBoxCellIndex.Focus();
                MessageBox.Show("输入索引号，然后再点一下新增数据按钮。");
            }
            else
            {
                int index;
                if (int.TryParse(textBoxCellIndex.Text.Trim(), out index))
                {
                    textBoxCellIndex.ReadOnly = true;
                    textBoxCellIndex.BackColor = Color.YellowGreen;
                    DataStation.CellData = ClassDataInfo.NewCellData(index);
                    //RefreshData();
                }
                else if (MessageBox.Show("索引号格式不对。点重试输入正确的索引号，或者点取消不新增数据。", "索引号错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    textBoxCellIndex.ReadOnly = true;
                    textBoxCellIndex.BackColor = Color.YellowGreen;
                    if (DataStation.CellData != null) textBoxCellIndex.Text = DataStation.CellData.Index.ToString();
                }
            }
        }

        private void buttonClearBarcode_Click(object sender, EventArgs e)
        {
            if (DataStation.CellData == null)
            {
                MessageBox.Show("无法修改条码。空电芯。");
                return;
            }
            if (textBoxBarcode.ReadOnly)
            {
                textBoxBarcode.ReadOnly = false;
                textBoxBarcode.BackColor = SystemColors.Control;
                textBoxBarcode.SelectAll();
                textBoxBarcode.Focus();
                MessageBox.Show("输入条码，然后再点一下修改按钮保存新条码。");
            }
            else
            {
                if (MessageBox.Show("确定要使用新条码吗？", "修改条码", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    DataStation.CellData.Barcode = textBoxBarcode.Text.Trim();
                textBoxBarcode.ReadOnly = true;
                textBoxBarcode.BackColor = Color.YellowGreen;
                RefreshData();
            }
        }
    }
}