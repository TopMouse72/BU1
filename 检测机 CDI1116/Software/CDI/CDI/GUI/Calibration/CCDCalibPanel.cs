using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule;
using Measure;
using CDI.Zone;

namespace CDI.GUI
{
    public partial class CCDCalibPanel : BasePanel, iProdParameterControl, iGaugeParameterControl
    {
        private DataTable DT = new DataTable();
        public ClassZone尺寸测量 zone = ClassWorkZones.Instance.WorkZone尺寸测量;
        private DataColumn colType, colCellWidth, colCellLength, colAlTabDist, colNiTabDist, colAlTabLen, colNiTabLen, colTabDist, colAlSealantHi, colNiSealantHi, colShoulderWidth;
        public CCDCalibPanel()
        {
            InitializeComponent();
            comboBoxStdPosition.Items.AddRange(Enum.GetNames(typeof(EnumCellIndex)));
            colType = DT.Columns.Add("型号");
            colCellWidth = DT.Columns.Add("电芯宽度");
            colCellLength = DT.Columns.Add("电芯长度");
            colAlTabDist = DT.Columns.Add("Al Tab边距");
            colNiTabDist = DT.Columns.Add("Ni Tab边距");
            colAlTabLen = DT.Columns.Add("Al Tab长度");
            colNiTabLen = DT.Columns.Add("Ni Tab长度");
            colTabDist = DT.Columns.Add("Tab间距");
            colAlSealantHi = DT.Columns.Add("Al Tab小白胶高度");
            colNiSealantHi = DT.Columns.Add("Ni Tab小白胶高度");
            colShoulderWidth = DT.Columns.Add("肩宽");
            dataGridView1.DataSource = DT;
            ClassCommonSetting.SysParam.CurrentProductParam.AddParaInterface(this);
        }

        public VacuumSetting VacuumLoadPNP { get; set; }
        public VacuumSetting VacuumTransPNPLoad { get; set; }
        public VacuumSetting VacuumTransPNPUnload { get; set; }
        public VacuumSetting VacuumUnloadPNP { get; set; }
        public VacuumSetting VacuumSortingPNP { get; set; }
        public bool BackSideUp { get; set; }
        public double TopSealHeight { get; set; }
        public double TopHeight { get; set; }
        public double TopClampWidth { get; set; }
        public bool ClampDisable { get; set; }

        private double _MeasCount = 0;
        private Cst.Struct_MeasDatas _ProdCellDataSpec;
        private double MeasCount
        {
            get { return _MeasCount; }
            set { _MeasCount = value; labelMeasCounter.Text = _MeasCount.ToString(); }
        }
        public Cst.Struct_MeasDatas ProdCellDataSpec
        {
            get
            {
                return _ProdCellDataSpec;
            }
            set
            {
                _ProdCellDataSpec = value;
            }
        }
        public int MeasAmount
        {
            get { return (int)numericUpDownMeasAmount.Value; }
            set { numericUpDownMeasAmount.Value = value; }
        }

        public new string ProductName { get; set; }
        //public double RefThickness { get; set; }
        //public double ThicknessMeasRefLeft { get; set; }
        //public double ThicknessMeasRefMid { get; set; }
        //public double ThicknessMeasRefRight { get; set; }
        private string _gaugename;
        public string UseGauge
        {
            get
            { return _gaugename; }
            set
            {
                _gaugename = value;
                labelGauge.Text = $"使用标准块：{_gaugename}。";
            }
        }
        private ClassGaugeParameter _gauge;
        public ClassGaugeParameter Gauge
        {
            set
            {
                _gauge = value;
                if (_gauge != null)
                {
                    textBoxXSlope.Text = _gauge.CCDXLinear.Slope.ToString();
                    textBoxXIntercept.Text = _gauge.CCDXLinear.Intercept.ToString();
                    selectBoxXEnable.Checked = _gauge.CCDXLinear.Enable;
                    textBoxYSlope.Text = _gauge.CCDYLinear.Slope.ToString();
                    textBoxYIntercept.Text = _gauge.CCDYLinear.Intercept.ToString();
                    selectBoxYEnable.Checked = _gauge.CCDYLinear.Enable;
                    ProdCellDataSpec = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec;
                    ShowStd();
                }
            }
        }

        public event EventHandler SaveParaEvent;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _gauge.CCDXLinear.Slope = double.Parse(textBoxXSlope.Text);
            _gauge.CCDXLinear.Intercept = double.Parse(textBoxXIntercept.Text);
            _gauge.CCDXLinear.Enable = selectBoxXEnable.Checked;
            _gauge.CCDYLinear.Slope = double.Parse(textBoxYSlope.Text);
            _gauge.CCDYLinear.Intercept = double.Parse(textBoxYIntercept.Text);
            _gauge.CCDYLinear.Enable = selectBoxYEnable.Checked;
            if (SaveParaEvent != null) SaveParaEvent(this, null);
            _gauge.SaveParameter();
        }
        protected ErrorInfoWithPause DoAction(object sender, Func<ErrorInfoWithPause> act)
        {
            ErrorInfoWithPause res = act();
            if (res != null)
            {
                string temp = res.Message;
                labelStatus.Text = "执行" + ((Control)sender).Text + "出错: " + temp;
            }
            return res;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = (dataGridView1.Rows.Count).ToString();
            //}
            //dataGridView1.Refresh();
            MeasCount = dataGridView1.Rows.Count % (int)numericUpDownMeasAmount.Value;
            if (MeasCount == 0)
                MessageBox.Show("计数值已达到测量次数，需要更换标准块。");
        }

        private void buttonROISet_Click(object sender, EventArgs e)
        {
            //ClassCommonSetting.SocketToAOI.SendCommandOpenSet(0);
        }
        private void ShowStd()
        {
            switch (comboBoxStdType.Text)
            {
                case "小":
                    textBoxCellWidth.Text = _ProdCellDataSpec.CellWidth.StdSmall.ToString();
                    textBoxCellLength.Text = _ProdCellDataSpec.CellLength.StdSmall.ToString();
                    textBoxNiTabDistance.Text = _ProdCellDataSpec.NiTabDistance.StdSmall.ToString();
                    textBoxAlTabDistance.Text = _ProdCellDataSpec.AlTabDistance.StdSmall.ToString();
                    textBoxNiTabLength.Text = _ProdCellDataSpec.NiTabLength.StdSmall.ToString();
                    textBoxAlTabLength.Text = _ProdCellDataSpec.AlTabLength.StdSmall.ToString();
                    textBoxNiSealantHeight.Text = _ProdCellDataSpec.NiSealantHeight.StdSmall.ToString();
                    textBoxAlSealantHeight.Text = _ProdCellDataSpec.AlSealantHeight.StdSmall.ToString();
                    //textBoxTabDistance.Text = _ProdCellDataSpec.TabDistance.StdSmall.ToString();
                    textBoxShoulderWidth.Text = _ProdCellDataSpec.ShoulderWidth.StdSmall.ToString();
                    break;
                case "中":
                    textBoxCellWidth.Text = _ProdCellDataSpec.CellWidth.StdMean.ToString();
                    textBoxCellLength.Text = _ProdCellDataSpec.CellLength.StdMean.ToString();
                    textBoxNiTabDistance.Text = _ProdCellDataSpec.NiTabDistance.StdMean.ToString();
                    textBoxAlTabDistance.Text = _ProdCellDataSpec.AlTabDistance.StdMean.ToString();
                    textBoxNiTabLength.Text = _ProdCellDataSpec.NiTabLength.StdMean.ToString();
                    textBoxAlTabLength.Text = _ProdCellDataSpec.AlTabLength.StdMean.ToString();
                    textBoxNiSealantHeight.Text = _ProdCellDataSpec.NiSealantHeight.StdMean.ToString();
                    textBoxAlSealantHeight.Text = _ProdCellDataSpec.AlSealantHeight.StdMean.ToString();
                    //textBoxTabDistance.Text = _ProdCellDataSpec.TabDistance.StdMean.ToString();
                    textBoxShoulderWidth.Text = _ProdCellDataSpec.ShoulderWidth.StdMean.ToString();
                    break;
                case "大":
                    textBoxCellWidth.Text = _ProdCellDataSpec.CellWidth.StdLarge.ToString();
                    textBoxCellLength.Text = _ProdCellDataSpec.CellLength.StdLarge.ToString();
                    textBoxNiTabDistance.Text = _ProdCellDataSpec.NiTabDistance.StdLarge.ToString();
                    textBoxAlTabDistance.Text = _ProdCellDataSpec.AlTabDistance.StdLarge.ToString();
                    textBoxNiTabLength.Text = _ProdCellDataSpec.NiTabLength.StdLarge.ToString();
                    textBoxAlTabLength.Text = _ProdCellDataSpec.AlTabLength.StdLarge.ToString();
                    textBoxNiSealantHeight.Text = _ProdCellDataSpec.NiSealantHeight.StdLarge.ToString();
                    textBoxAlSealantHeight.Text = _ProdCellDataSpec.AlSealantHeight.StdLarge.ToString();
                    //textBoxTabDistance.Text = _ProdCellDataSpec.TabDistance.StdLarge.ToString();
                    textBoxShoulderWidth.Text = _ProdCellDataSpec.ShoulderWidth.StdLarge.ToString();
                    break;
            }
        }
        private void buttonSetStdValue_Click(object sender, EventArgs e)
        {
            if (comboBoxStdType.Text == "")
            {
                MessageBox.Show("没有选择要修改的标准块类型。");
                return;
            }
            panelStdValue.Size = new Size(panelStdValue.Size.Width, 0);
            panelStdValue.Visible = true;
            for (int i = 0; i < buttonSaveStd.Top + buttonSaveStd.Height + 3; i = i + 4)
            {
                Application.DoEvents();
                panelStdValue.Size = new Size(panelStdValue.Size.Width, i);
            }
        }

        private void buttonLoadStd_Click(object sender, EventArgs e)
        {
            if (!CheckOption()) return;
            EnumCellIndex index = (EnumCellIndex)Enum.Parse(typeof(EnumCellIndex), comboBoxStdPosition.Text);
            zone.ActionToMeasPos(index);
        }

        private void buttonExitStd_Click(object sender, EventArgs e)
        {
            for (int i = buttonSaveStd.Top + buttonSaveStd.Height + 3; i > 0; i--)
                panelStdValue.Size = new Size(panelStdValue.Size.Width, i);
            panelStdValue.Visible = false;
        }

        private void buttonRelease_Click(object sender, EventArgs e)
        {
            zone.ResetOutPort();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!CheckOption()) return;
            EnumCellIndex index = (EnumCellIndex)Enum.Parse(typeof(EnumCellIndex), comboBoxStdPosition.Text);
            zone.CellVacuums[index].SetOutPortStatus(true);
            zone.CellBlow.SetOutPortStatus(false);
        }

        private void buttonLoadCell_Click(object sender, EventArgs e)
        {
            bool temp;
            EnumCellIndex index = EnumCellIndex.右电芯;
            buttonRelease_Click(sender, e);
            if (MessageBox.Show("确认顶峰，测厚和CCD测量工位上没有其他电芯或标准块。确认后点\"确定\"按钮。取消则点\"取消\"按钮。", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return;
            MessageBox.Show("将物料放置在顶封工位" + index.ToString() + "的位置。放置好后点击\"确定\"按钮进行加载。");
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
            {
                ClassWorkZones.Instance.WorkZone顶封边定位.TopAlignDataStations[i].CellData = null;
                ClassWorkZones.Instance.WorkZone厚度测量.ThicknessDataStations[i].CellData = null;
            }
            ClassWorkZones.Instance.WorkZone顶封边定位.TopAlignDataStations[index].CellData = ClassDataInfo.NewCellData();
            //CCD移动到放料位
            if (DoAction(sender, () =>
            {
                return zone.ActionToGetPart(false);
            }) != null) return;
            //顶封位定位
            if (DoAction(sender, ClassWorkZones.Instance.WorkZone顶封边定位.ActionAlign) != null) return;
            //顶封位放开
            if (DoAction(sender, ClassWorkZones.Instance.WorkZone顶封边定位.ActionRelease) != null) return;
            zone.AxisOutlineMeasX.WaitStop(ClassErrorHandle.TIMEOUT);
            //传送机械手从顶封位取料
            if (DoAction(sender, () =>
            {
                return ClassWorkZones.Instance.WorkZone传送机械手.ActionStartLoad(out temp, ClassWorkZones.Instance.DoTransPNPLoad, ClassWorkZones.Instance.AfterTransPNPLoad);
            }) != null) return;
            //传送机械手放料到测厚位
            if (DoAction(sender, () =>
            {
                return ClassWorkZones.Instance.WorkZone传送机械手.ActionStartUnload(ClassWorkZones.Instance.DoTransPNPUnload, ClassWorkZones.Instance.AfterTransPNPUnload);
            }) != null) return;
            //传送机械手从测厚位取料
            if (DoAction(sender, () =>
            {
                return ClassWorkZones.Instance.WorkZone传送机械手.ActionStartLoad(out temp, ClassWorkZones.Instance.DoTransPNPLoad, ClassWorkZones.Instance.AfterTransPNPLoad);
            }) != null) return;
            //传送机械手放料到CCD测量位
            if (DoAction(sender, () =>
            {
                return ClassWorkZones.Instance.WorkZone传送机械手.ActionStartUnload(ClassWorkZones.Instance.DoTransPNPUnload, ClassWorkZones.Instance.AfterTransPNPUnload);
            }) != null) return;
            //CCD放物料工位移动到检测位
            if (DoAction(sender, () =>
            {
                return zone.ActionToMeasPos(index);
            }) != null) return;
            MessageBox.Show("物料加载完毕。");
        }

        private void buttonSaveStd_Click(object sender, EventArgs e)
        {
            switch (comboBoxStdType.Text)
            {
                case "小":
                    _ProdCellDataSpec.CellWidth.StdSmall = double.Parse(textBoxCellWidth.Text);
                    _ProdCellDataSpec.CellLength.StdSmall = double.Parse(textBoxCellLength.Text);
                    _ProdCellDataSpec.NiTabDistance.StdSmall = double.Parse(textBoxNiTabDistance.Text);
                    _ProdCellDataSpec.AlTabDistance.StdSmall = double.Parse(textBoxAlTabDistance.Text);
                    _ProdCellDataSpec.NiTabLength.StdSmall = double.Parse(textBoxNiTabLength.Text);
                    _ProdCellDataSpec.AlTabLength.StdSmall = double.Parse(textBoxAlTabLength.Text);
                    _ProdCellDataSpec.NiSealantHeight.StdSmall = double.Parse(textBoxNiSealantHeight.Text);
                    _ProdCellDataSpec.AlSealantHeight.StdSmall = double.Parse(textBoxAlSealantHeight.Text);
                    _ProdCellDataSpec.TabDistance.StdSmall = double.Parse(textBoxTabDistance.Text);
                    _ProdCellDataSpec.ShoulderWidth.StdSmall = double.Parse(textBoxShoulderWidth.Text);
                    break;
                case "中":
                    _ProdCellDataSpec.CellWidth.StdMean = double.Parse(textBoxCellWidth.Text);
                    _ProdCellDataSpec.CellLength.StdMean = double.Parse(textBoxCellLength.Text);
                    _ProdCellDataSpec.NiTabDistance.StdMean = double.Parse(textBoxNiTabDistance.Text);
                    _ProdCellDataSpec.AlTabDistance.StdMean = double.Parse(textBoxAlTabDistance.Text);
                    _ProdCellDataSpec.NiTabLength.StdMean = double.Parse(textBoxNiTabLength.Text);
                    _ProdCellDataSpec.AlTabLength.StdMean = double.Parse(textBoxAlTabLength.Text);
                    _ProdCellDataSpec.NiSealantHeight.StdMean = double.Parse(textBoxNiSealantHeight.Text);
                    _ProdCellDataSpec.AlSealantHeight.StdMean = double.Parse(textBoxAlSealantHeight.Text);
                    _ProdCellDataSpec.TabDistance.StdMean = double.Parse(textBoxTabDistance.Text);
                    _ProdCellDataSpec.ShoulderWidth.StdMean = double.Parse(textBoxShoulderWidth.Text);
                    break;
                case "大":
                    _ProdCellDataSpec.CellWidth.StdLarge = double.Parse(textBoxCellWidth.Text);
                    _ProdCellDataSpec.CellLength.StdLarge = double.Parse(textBoxCellLength.Text);
                    _ProdCellDataSpec.NiTabDistance.StdLarge = double.Parse(textBoxNiTabDistance.Text);
                    _ProdCellDataSpec.AlTabDistance.StdLarge = double.Parse(textBoxAlTabDistance.Text);
                    _ProdCellDataSpec.NiTabLength.StdLarge = double.Parse(textBoxNiTabLength.Text);
                    _ProdCellDataSpec.AlTabLength.StdLarge = double.Parse(textBoxAlTabLength.Text);
                    _ProdCellDataSpec.NiSealantHeight.StdLarge = double.Parse(textBoxNiSealantHeight.Text);
                    _ProdCellDataSpec.AlSealantHeight.StdLarge = double.Parse(textBoxAlSealantHeight.Text);
                    _ProdCellDataSpec.TabDistance.StdLarge = double.Parse(textBoxTabDistance.Text);
                    _ProdCellDataSpec.ShoulderWidth.StdLarge = double.Parse(textBoxShoulderWidth.Text);
                    break;
            }
            if (SaveParaEvent != null) SaveParaEvent(this, null);
        }

        private void buttonCountReset_Click(object sender, EventArgs e)
        {
            DT.Clear();
        }

        private void textBoxTabDistance_TextChanged(object sender, EventArgs e)
        {
            double NiDist, AlDist;
            if (double.TryParse(textBoxNiTabDistance.Text, out NiDist) && double.TryParse(textBoxAlTabDistance.Text, out AlDist))
            {
                switch (comboBoxStdType.Text)
                {
                    case "小":
                        _ProdCellDataSpec.TabDistance.StdSmall = Math.Abs(NiDist - AlDist);
                        break;
                    case "中":
                        _ProdCellDataSpec.TabDistance.StdMean = Math.Abs(NiDist - AlDist);
                        break;
                    case "大":
                        _ProdCellDataSpec.TabDistance.StdLarge = Math.Abs(NiDist - AlDist);
                        break;
                }
                textBoxTabDistance.Text = Math.Abs(NiDist - AlDist).ToString();
            }
        }

        private void comboBoxStdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowStd();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            //dataGridView1.Refresh();
            MeasCount = dataGridView1.Rows.Count % (int)numericUpDownMeasAmount.Value;
        }
        private bool CheckOption()
        {
            if (comboBoxStdType.Text == "")
            {
                MessageBox.Show("没有选择要修改的标准块类型。");
                return false;
            }
            if (comboBoxStdPosition.Text == "")
            {
                MessageBox.Show("需要设置标准块测试位置。");
                return false;
            }
            return true;
        }
        private double StdErr = 0.02;
        private void buttonStartMeasCali_Click(object sender, EventArgs e)
        {
            int index;
            string temp = "";
            switch (comboBoxStdType.Text)
            {
                case "大": temp = "Big"; break;
                case "中": temp = "Middle"; break;
                case "小": temp = "Small"; break;
            }
            labelStatus.Text = "";
            if (!CheckOption()) return;
            EnumCellIndex stdindex = (EnumCellIndex)Enum.Parse(typeof(EnumCellIndex), comboBoxStdPosition.Text);
            bool res = false;
            if (!optionBoxFromFile.Checked)
            {
                ClassCommonSetting.SocketToAOI.SendCommandCamLive(false);
                index = (int)stdindex;
                zone.CCDMeasDataStations[stdindex].CellData = ClassDataInfo.NewCellData();
                zone.CCDMeasDataStations[stdindex].CellData.Barcode = ClassCommonSetting.SysParam.CurrentProductParam.UseGauge + temp + "Standard";
                zone.MeasDone[(int)stdindex] = false;
                zone.BufferDatas[(int)stdindex] = zone.CCDMeasDataStations[(int)stdindex].CellData;
                zone.CellVacuums[stdindex].SetOutPortStatus(true);
                TimeClass.Delay(500);
                res = DoAction(sender, () => { return zone.ActionOneCCDMeas(stdindex, DataComp.AddComp); }) == null;
                if (!res) return;
                while (!zone.MeasDone[(int)stdindex]) Application.DoEvents();
            }
            else
            {
                index = 3;
                res = DoAction(sender, () => { return zone.MeasPicture(true); }) == null;
            }
            if (res)
            {
                DataRow newrow = DT.NewRow();
                newrow[colType] = comboBoxStdType.Text;
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.CellWidth, ref zone.BufferDatas[index].Data.CellWidth, StdErr, selectBoxYEnable.Checked);
                Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.CellLength, ref zone.BufferDatas[index].Data.CellLength, StdErr, true);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlTabDistance, ref zone.BufferDatas[index].Data.AlTabDistance, StdErr, selectBoxYEnable.Checked);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiTabDistance, ref zone.BufferDatas[index].Data.NiTabDistance, StdErr, selectBoxYEnable.Checked);
                Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlTabLength, ref zone.BufferDatas[index].Data.AlTabLength, StdErr, true);
                Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiTabLength, ref zone.BufferDatas[index].Data.NiTabLength, StdErr, true);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.TabDistance, ref zone.BufferDatas[index].Data.TabDistance, StdErr, selectBoxYEnable.Checked);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlSealantHeight, ref zone.BufferDatas[index].Data.AlSealantHeight, StdErr, selectBoxXEnable.Checked);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiSealantHeight, ref zone.BufferDatas[index].Data.NiSealantHeight, StdErr, selectBoxXEnable.Checked);
                ////Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.ShoulderWidth, ref zone.BufferDatas[index].Data.ShoulderWidth, StdErr, selectBoxYEnable.Checked);
                newrow[colCellWidth] = zone.BufferDatas[index].Data.CellWidth.Value.ToString("0.000");
                newrow[colCellLength] = zone.BufferDatas[index].Data.CellLength.Value.ToString("0.000");
                newrow[colAlTabDist] = zone.BufferDatas[index].Data.AlTabDistance.Value.ToString("0.000");
                newrow[colNiTabDist] = zone.BufferDatas[index].Data.NiTabDistance.Value.ToString("0.000");
                newrow[colAlTabLen] = zone.BufferDatas[index].Data.AlTabLength.Value.ToString("0.000");
                newrow[colNiTabLen] = zone.BufferDatas[index].Data.NiTabLength.Value.ToString("0.000");
                newrow[colTabDist] = zone.BufferDatas[index].Data.TabDistance.Value.ToString("0.000");
                newrow[colAlSealantHi] = zone.BufferDatas[index].Data.AlSealantHeight.Value.ToString("0.000");
                newrow[colNiSealantHi] = zone.BufferDatas[index].Data.NiSealantHeight.Value.ToString("0.000");
                newrow[colShoulderWidth] = zone.BufferDatas[index].Data.ShoulderWidth.Value.ToString("0.000");
                DT.Rows.Add(newrow);
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.CellWidth, ref zone.BufferDatas[index].Data.CellWidth, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colCellWidth.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colCellWidth.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.CellLength, ref zone.BufferDatas[index].Data.CellLength, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colCellLength.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colCellLength.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlTabDistance, ref zone.BufferDatas[index].Data.AlTabDistance, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlTabDist.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlTabDist.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiTabDistance, ref zone.BufferDatas[index].Data.NiTabDistance, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiTabDist.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiTabDist.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlTabLength, ref zone.BufferDatas[index].Data.AlTabLength, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlTabLen.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlTabLen.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiTabLength, ref zone.BufferDatas[index].Data.NiTabLength, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiTabLen.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiTabLen.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.TabDistance, ref zone.BufferDatas[index].Data.TabDistance, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colTabDist.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colTabDist.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.AlSealantHeight, ref zone.BufferDatas[index].Data.AlSealantHeight, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlSealantHi.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colAlSealantHi.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.NiSealantHeight, ref zone.BufferDatas[index].Data.NiSealantHeight, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiSealantHi.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colNiSealantHi.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxStdType.Text, _ProdCellDataSpec.ShoulderWidth, ref zone.BufferDatas[index].Data.ShoulderWidth, StdErr))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colShoulderWidth.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colShoulderWidth.Caption].Style.ForeColor = Color.Yellow;
                }
            }

            //zone.CCDMeasDataStations[stdindex].CellData = null;
            //zone.CellVacuums[stdindex].SetOutPortStatus(false);

        }
        private void Calc()
        {
            List<PointF> datasX = new List<PointF>();
            List<PointF> datasY = new List<PointF>();
            DataColumn xCol = null, yCol = colType;
            double K = 1, B = 0;
            //Calc X
            if (selectBoxCellLength.Checked)
            {
                xCol = colCellLength;
                GetData(xCol, yCol, _ProdCellDataSpec.CellLength, datasX);
            }
            if (selectBoxAlSealant.Checked)
            {
                xCol = colAlSealantHi;
                GetData(xCol, yCol, _ProdCellDataSpec.AlSealantHeight, datasX);
            }
            if (selectBoxNiSealant.Checked)
            {
                xCol = colNiSealantHi;
                GetData(xCol, yCol, _ProdCellDataSpec.NiSealantHeight, datasX);
            }
            if (selectBoxShoulderWidth.Checked)
            {
                xCol = colShoulderWidth;
                GetData(xCol, yCol, _ProdCellDataSpec.ShoulderWidth, datasX);
            }
            if (selectBoxAlTabLength.Checked)
            {
                xCol = colAlTabLen;
                GetData(xCol, yCol, _ProdCellDataSpec.AlTabLength, datasX);
            }
            if (selectBoxNiTabLength.Checked)
            {
                xCol = colNiTabLen;
                GetData(xCol, yCol, _ProdCellDataSpec.NiTabLength, datasX);
            }

            //Calc Y
            if (selectBoxCellWidth.Checked)
            {
                xCol = colCellWidth;
                GetData(xCol, yCol, _ProdCellDataSpec.CellWidth, datasY);
            }
            if (selectBoxTabDistance.Checked)
            {
                xCol = colTabDist;
                GetData(xCol, yCol, _ProdCellDataSpec.TabDistance, datasY);
            }
            if (selectBoxAlTabDistance.Checked)
            {
                xCol = colAlTabDist;
                GetData(xCol, yCol, _ProdCellDataSpec.AlTabDistance, datasY);
            }
            if (selectBoxNiTabDistance.Checked)
            {
                xCol = colNiTabDist;
                GetData(xCol, yCol, _ProdCellDataSpec.NiTabDistance, datasY);
            }
            GetLine(datasX, ref K, ref B);
            _gauge.CCDXLinear.Slope = K;
            textBoxXSlope.Text = K.ToString();
            _gauge.CCDXLinear.Intercept = B;
            textBoxXIntercept.Text = B.ToString();
            GetLine(datasY, ref K, ref B);
            _gauge.CCDYLinear.Slope = K;
            textBoxYSlope.Text = K.ToString();
            _gauge.CCDYLinear.Intercept = B;
            textBoxYIntercept.Text = B.ToString();
        }
        private void SaveData(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "") return;
            Stream sm = saveFileDialog1.OpenFile();
            StreamWriter sw = new StreamWriter(sm, Encoding.UTF8);
            string data;
            data = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                        "",
                        "型号",
                        "电芯宽度_标准块",
                        "电芯宽度",
                        "电芯长度_标准块",
                        "电芯长度",
                        "Al Tab边距_标准块",
                        "Al Tab边距",
                        "Ni Tab边距_标准块",
                        "Ni Tab边距",
                        "Al Tab长度_标准块",
                        "Al Tab长度",
                        "Ni Tab长度_标准块",
                        "Ni Tab长度",
                        "Tab间距_标准块",
                        "Tab间距",
                        "Al Tab小白胶高度_标准块",
                        "Al Tab小白胶高度",
                        "Ni Tab小白胶高度_标准块",
                        "Ni Tab小白胶高度",
                        "肩宽_标准块",
                        "肩宽");
            sw.WriteLine(data);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                data = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                    i + 1,
                    DT.Rows[i][colType],
                    _ProdCellDataSpec.CellWidth.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colCellWidth],
                    _ProdCellDataSpec.CellLength.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colCellLength],
                    _ProdCellDataSpec.AlTabDistance.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colAlTabDist],
                    _ProdCellDataSpec.NiTabDistance.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colNiTabDist],
                    _ProdCellDataSpec.AlTabLength.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colAlTabLen],
                     _ProdCellDataSpec.NiTabLength.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colNiTabLen],
                     _ProdCellDataSpec.TabDistance.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colTabDist],
                    _ProdCellDataSpec.AlSealantHeight.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colAlSealantHi],
                    _ProdCellDataSpec.NiSealantHeight.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colNiSealantHi],
                    _ProdCellDataSpec.ShoulderWidth.GetStdData(DT.Rows[i][colType].ToString()),
                    DT.Rows[i][colShoulderWidth]
                    );
                sw.WriteLine(data);
            }
            sw.Close();
        }
        private void GetData(DataColumn xCol, DataColumn yCol, Cst.Struct_DataInfo StdData, List<PointF> datas)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
                datas.Add(new PointF(float.Parse(DT.Rows[i][xCol].ToString()),
                    (float)StdData.GetStdData(DT.Rows[i][yCol].ToString())));
        }
        private void GetLine(List<PointF> datas, ref double K, ref double B)
        {
            double _averageX = 0;
            double _averageY = 0;
            double _sumX = 0;
            double _sumY = 0;
            double _sumXY = 0;
            double _sumXX = 0;
            foreach (PointF point in datas)
            {
                _sumX += point.X;
                _sumY += point.Y;
                _sumXX += point.X * point.X;
                _sumXY += point.X * point.Y;
            }
            _averageX = _sumX / datas.Count;
            _averageY = _sumY / datas.Count;
            K = (_sumXY - _averageY * _sumX) / (_sumXX - _averageX * _sumX);
            B = _averageY - K * _averageX;
        }

        private void buttonCalcLinear_Click(object sender, EventArgs e)
        {
            Calc();
        }
    }
}