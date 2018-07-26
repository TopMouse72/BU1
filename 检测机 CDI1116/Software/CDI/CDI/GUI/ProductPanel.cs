using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Measure;
using Colibri.CommonModule;
using CDI.Zone;
using MSOffice;

namespace CDI.GUI
{
    public partial class ProductPanel : UserControl, ISystemParameterControl, iProdParameterControl
    {
        public ProductPanel()
        {
            InitializeComponent();
            comboBoxLogOutTime.Items.AddRange(Enum.GetNames(typeof(LogOutItem)));
        }
        private VacuumSetting _VacuumLoadPNP;
        private VacuumSetting _VacuumTransPNPLoad;
        private VacuumSetting _VacuumTransPNPUnload;
        private VacuumSetting _VacuumUnloadPNP;
        private VacuumSetting _VacuumSortingPNP;
        private string _CurrentProduct;
        private Dictionary<string, ClassProdParameter> _Products;
        private Dictionary<string, ClassGaugeParameter> _Gauges;
        private Cst.Struct_MeasDatas _CellDataSpec;
        public int VacuumDelayTime
        {
            get
            {
                return int.Parse(textBoxDelayTime.Text);
            }
            set
            {
                textBoxDelayTime.Text = value.ToString();
            }
        }
        public LogOutItem LogOutTime
        {
            get
            {
                if (comboBoxLogOutTime.SelectedIndex < 0)
                    comboBoxLogOutTime.SelectedIndex = (int)LogOutItem.三分钟;
                return (LogOutItem)comboBoxLogOutTime.SelectedIndex;
            }
            set
            {
                comboBoxLogOutTime.SelectedIndex = (int)value;
            }
        }
        public int ThicknessMeasDelayTime { get; set; }
        //public double ThicknessMeasRefLeft { get; set; }
        //public double ThicknessMeasRefMid { get; set; }
        //public double ThicknessMeasRefRight { get; set; }
        public string CurrentProduct
        {
            get { return _CurrentProduct; }
            set
            {
                _CurrentProduct = value;
                labelCurrentProd.Text = "当前产品：" + _CurrentProduct;
                buttonDeleteProduct.Enabled = _CurrentProduct != SelectedProduct && _CurrentProduct != ClassBaseWorkZone.CALIBPROD;
                listBoxProduct.Text = _CurrentProduct;
                SelectedProduct = _CurrentProduct;
            }
        }
        public Dictionary<string, ClassProdParameter> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                listBoxProduct.Items.Clear();
                if (!_Products.ContainsKey(ClassBaseWorkZone.CALIBPROD))//若当前产品中没有calibration，则添加
                {
                    _Products.Add(ClassBaseWorkZone.CALIBPROD, new ClassProdParameter(ClassBaseWorkZone.CALIBPROD));
                    if (SaveParaEvent != null) SaveParaEvent(this, null);
                }
                if (_Products.Count > 0)
                    listBoxProduct.Items.AddRange(_Products.Keys.ToArray());
            }
        }
        private string isGaugeChanged { get => Products[SelectedProduct].UseGauge != UseGauge ? $"{Products[SelectedProduct].UseGauge}->{UseGauge}" : ""; }
        public Dictionary<string, ClassGaugeParameter> Gauges
        {
            get { return _Gauges; }
            set
            {
                _Gauges = value;
                listBoxGauge.Items.Clear();
                if (_Gauges.Count > 0)
                {
                    listBoxGauge.Items.Clear();
                    comboBoxUseGauge.Items.Clear();
                    listBoxGauge.Items.AddRange(_Gauges.Keys.ToArray());
                    comboBoxUseGauge.Items.AddRange(_Gauges.Keys.ToArray());
                }
            }
        }
        public bool LoadInSMEMAIgnored { get; set; }
        public bool UnloadOutSMEMAIgnored { get; set; }
        public new string ProductName { get { return textBoxProductName.Text; } set { textBoxProductName.Text = value; } }
        public VacuumSetting VacuumLoadPNP
        {
            get
            {
                _VacuumLoadPNP.GetData(); return _VacuumLoadPNP;
            }
            set
            {
                _VacuumLoadPNP = value;
                _VacuumLoadPNP.SettingForm = vacuumSettingDispLoadPNP;
            }
        }
        public VacuumSetting VacuumTransPNPLoad
        {
            get
            {
                _VacuumTransPNPLoad.GetData(); return _VacuumTransPNPLoad;
            }
            set
            {
                _VacuumTransPNPLoad = value;
                _VacuumTransPNPLoad.SettingForm = vacuumSettingDispTransPNPLoad;
            }
        }

        public VacuumSetting VacuumTransPNPUnload
        {
            get
            {
                _VacuumTransPNPUnload.GetData(); return _VacuumTransPNPUnload;
            }
            set
            {
                _VacuumTransPNPUnload = value;
                _VacuumTransPNPUnload.SettingForm = vacuumSettingDispTransPNPUnload;
            }
        }

        public VacuumSetting VacuumUnloadPNP
        {
            get
            {
                _VacuumUnloadPNP.GetData(); return _VacuumUnloadPNP;
            }
            set
            {
                _VacuumUnloadPNP = value;
                _VacuumUnloadPNP.SettingForm = vacuumSettingDispUnloadPNP;
            }
        }

        public VacuumSetting VacuumSortingPNP
        {
            get
            {
                _VacuumSortingPNP.GetData(); return _VacuumSortingPNP;
            }
            set
            {
                _VacuumSortingPNP = value;
                _VacuumSortingPNP.SettingForm = vacuumSettingDispSortingPNP;
            }
        }

        public bool BackSideUp { get { return selectBoxBackSideUp.Checked; } set { selectBoxBackSideUp.Checked = value; } }
        public double TopSealHeight
        {
            get
            {
                double temp;
                if (double.TryParse(textBoxTopSealHeight.Text.Trim(), out temp))
                    return temp;
                else
                    return 0;
            }
            set { textBoxTopSealHeight.Text = value.ToString(); }
        }
        public double TopHeight
        {
            get
            {
                double temp;
                if (double.TryParse(textBoxTopHeight.Text.Trim(), out temp))
                    return temp;
                else
                    return 0;
            }
            set { textBoxTopHeight.Text = value.ToString(); }
        }
        public double TopClampWidth
        {
            get
            {
                double temp;
                if (double.TryParse(textBoxTopClampWidth.Text.Trim(), out temp))
                    return temp;
                else
                    return 0;
            }
            set { textBoxTopClampWidth.Text = value.ToString(); }
        }
        public bool ClampDisable
        {
            get { return selectBoxClampDisable.Checked; }
            set { selectBoxClampDisable.Checked = value; }
        }
        public Cst.Struct_MeasDatas ProdCellDataSpec
        {
            get { return _CellDataSpec; }
            set { _CellDataSpec = value; FillCellData(); }
        }

        //public double RefThickness { get; set; }
        //public double StdThicknessLarge { get; set; }
        //public double StdThicknessMean { get; set; }
        //public double StdThicknessSmall { get; set; }
        public int MeasAmount { get; set; }
        //public LinearPara StationLeftLinear { get; set; }
        //public LinearPara StationMidLinear { get; set; }
        //public LinearPara StationRightLinear { get; set; }
        public int OdsSaveInterval
        {
            get
            {
                return int.Parse(textBoxOdsInterval.Text.Trim());
            }
            set
            {
                textBoxOdsInterval.Text = value.ToString();
            }
        }
        public string OdsSavePath
        {
            get
            {
                return textBoxOdsPath.Text.Trim();
            }
            set
            {
                textBoxOdsPath.Text = value;
            }
        }

        public int ManualLoadDelayTime { get => int.Parse(textBoxManualLoadDelayTime.Text); set => textBoxManualLoadDelayTime.Text = value.ToString(); }
        public string UseGauge
        {
            get => comboBoxUseGauge.Text;
            set { if (value == "") comboBoxUseGauge.SelectedIndex = -1; else comboBoxUseGauge.Text = value; }
        }

        private void FillCellData()
        {
            textBoxMeanCellWidth.Text = $"{_CellDataSpec.CellWidth.Mean:0.000}";
            textBoxToleranceCellWidth.Text = $"{_CellDataSpec.CellWidth.Tolerance:0.000}";
            textBoxULCellWidth.Text = $"{_CellDataSpec.CellWidth.ULimit:0.000}";
            textBoxLLCellWidth.Text = $"{_CellDataSpec.CellWidth.LLimit:0.000}";
            selectBoxNGDisableCellWidth.Checked = _CellDataSpec.CellWidth.CheckNGDisable;

            textBoxMeanCellLength.Text = $"{_CellDataSpec.CellLength.Mean:0.000}";
            textBoxToleranceCellLength.Text = $"{_CellDataSpec.CellLength.Tolerance:0.000}";
            textBoxULCellLength.Text = $"{_CellDataSpec.CellLength.ULimit:0.000}";
            textBoxLLCellLength.Text = $"{_CellDataSpec.CellLength.LLimit:0.000}";
            selectBoxNGDisableCellLength.Checked = _CellDataSpec.CellLength.CheckNGDisable;

            textBoxMeanNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.Mean:0.000}";
            textBoxToleranceNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.Tolerance:0.000}";
            textBoxULNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.ULimit:0.000}";
            textBoxLLNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.LLimit:0.000}";
            selectBoxNGDisableNiTabDistance.Checked = _CellDataSpec.NiTabDistance.CheckNGDisable;

            textBoxMeanAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.Mean:0.000}";
            textBoxToleranceAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.Tolerance:0.000}";
            textBoxULAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.ULimit:0.000}";
            textBoxLLAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.LLimit:0.000}";
            selectBoxNGDisableAlTabDistance.Checked = _CellDataSpec.AlTabDistance.CheckNGDisable;

            textBoxMeanNiTabLength.Text = $"{_CellDataSpec.NiTabLength.Mean:0.000}";
            textBoxToleranceNiTabLength.Text = $"{_CellDataSpec.NiTabLength.Tolerance:0.000}";
            textBoxULNiTabLength.Text = $"{_CellDataSpec.NiTabLength.ULimit:0.000}";
            textBoxLLNiTabLength.Text = $"{_CellDataSpec.NiTabLength.LLimit:0.000}";
            selectBoxNGDisableNiTabLength.Checked = _CellDataSpec.NiTabLength.CheckNGDisable;

            textBoxMeanAlTabLength.Text = $"{_CellDataSpec.AlTabLength.Mean:0.000}";
            textBoxToleranceAlTabLength.Text = $"{_CellDataSpec.AlTabLength.Tolerance:0.000}";
            textBoxULAlTabLength.Text = $"{_CellDataSpec.AlTabLength.ULimit:0.000}";
            textBoxLLAlTabLength.Text = $"{_CellDataSpec.AlTabLength.LLimit:0.000}";
            selectBoxNGDisableAlTabLength.Checked = _CellDataSpec.AlTabLength.CheckNGDisable;

            textBoxMeanNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.Mean:0.000}";
            textBoxToleranceNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.Tolerance:0.000}";
            textBoxULNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.ULimit:0.000}";
            textBoxLLNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.LLimit:0.000}";
            selectBoxNGDisableNiSealantHeight.Checked = _CellDataSpec.NiSealantHeight.CheckNGDisable;

            textBoxMeanAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.Mean:0.000}";
            textBoxToleranceAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.Tolerance:0.000}";
            textBoxULAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.ULimit:0.000}";
            textBoxLLAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.LLimit:0.000}";
            selectBoxNGDisableAlSealantHeight.Checked = _CellDataSpec.AlSealantHeight.CheckNGDisable;

            //textBoxMeanTabDistance.Text = _CellDataSpec.TabDistance.Mean.ToString();
            textBoxToleranceTabDistance.Text = $"{_CellDataSpec.TabDistance.Tolerance:0.000}";
            textBoxULTabDistance.Text = $"{_CellDataSpec.TabDistance.ULimit:0.000}";
            textBoxLLTabDistance.Text = $"{_CellDataSpec.TabDistance.LLimit:0.000}";
            selectBoxNGDisableTabDistance.Checked = _CellDataSpec.TabDistance.CheckNGDisable;

            textBoxMeanThickness.Text = $"{_CellDataSpec.CellThickness.Mean:0.000}";
            textBoxToleranceThickness.Text = $"{_CellDataSpec.CellThickness.Tolerance:0.000}";
            textBoxULThickness.Text = $"{_CellDataSpec.CellThickness.ULimit:0.000}";
            textBoxLLThickness.Text = $"{_CellDataSpec.CellThickness.LLimit:0.000}";
            selectBoxNGDisableThickness.Checked = _CellDataSpec.CellThickness.CheckNGDisable;

            textBoxMeanShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.Mean:0.000}";
            textBoxToleranceShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.Tolerance:0.000}";
            textBoxULShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.ULimit:0.000}";
            textBoxLLShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.LLimit:0.000}";
            selectBoxNGDisableShoulderWidth.Checked = _CellDataSpec.ShoulderWidth.CheckNGDisable;
        }
        private bool TryParseDouble(TextBox control, ref double data)
        {
            double temp;
            bool res = double.TryParse(control.Text, out temp);
            if (res)
                data = temp;
            else
            {
                MessageBox.Show("数据格式出错。");
                control.Focus();
                control.SelectAll();
            }
            return res;
        }
        private void GetCellData()
        {

            if (!TryParseDouble(textBoxMeanCellWidth, ref _CellDataSpec.CellWidth.Mean)) return;
            if (!TryParseDouble(textBoxToleranceCellWidth, ref _CellDataSpec.CellWidth.Tolerance)) return;
            _CellDataSpec.CellWidth.CheckNGDisable = selectBoxNGDisableCellWidth.Checked;

            if (!TryParseDouble(textBoxMeanCellLength, ref _CellDataSpec.CellLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceCellLength, ref _CellDataSpec.CellLength.Tolerance)) return;
            _CellDataSpec.CellLength.CheckNGDisable = selectBoxNGDisableCellLength.Checked;

            if (!TryParseDouble(textBoxMeanNiTabDistance, ref _CellDataSpec.NiTabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabDistance, ref _CellDataSpec.NiTabDistance.Tolerance)) return;
            _CellDataSpec.NiTabDistance.CheckNGDisable = selectBoxNGDisableNiTabDistance.Checked;

            if (!TryParseDouble(textBoxMeanAlTabDistance, ref _CellDataSpec.AlTabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabDistance, ref _CellDataSpec.AlTabDistance.Tolerance)) return;
            _CellDataSpec.AlTabDistance.CheckNGDisable = selectBoxNGDisableAlTabDistance.Checked;

            if (!TryParseDouble(textBoxMeanNiTabDistance, ref _CellDataSpec.NiTabDistanceMax.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabDistance, ref _CellDataSpec.NiTabDistanceMax.Tolerance)) return;
            _CellDataSpec.NiTabDistanceMax.CheckNGDisable = selectBoxNGDisableNiTabDistance.Checked;

            if (!TryParseDouble(textBoxMeanAlTabDistance, ref _CellDataSpec.AlTabDistanceMax.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabDistance, ref _CellDataSpec.AlTabDistanceMax.Tolerance)) return;
            _CellDataSpec.AlTabDistanceMax.CheckNGDisable = selectBoxNGDisableAlTabDistance.Checked;

            if (!TryParseDouble(textBoxMeanNiTabLength, ref _CellDataSpec.NiTabLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabLength, ref _CellDataSpec.NiTabLength.Tolerance)) return;
            _CellDataSpec.NiTabLength.CheckNGDisable = selectBoxNGDisableNiTabLength.Checked;

            if (!TryParseDouble(textBoxMeanAlTabLength, ref _CellDataSpec.AlTabLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabLength, ref _CellDataSpec.AlTabLength.Tolerance)) return;
            _CellDataSpec.AlTabLength.CheckNGDisable = selectBoxNGDisableAlTabLength.Checked;

            if (!TryParseDouble(textBoxMeanNiSealantHeight, ref _CellDataSpec.NiSealantHeight.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiSealantHeight, ref _CellDataSpec.NiSealantHeight.Tolerance)) return;
            _CellDataSpec.NiSealantHeight.CheckNGDisable = selectBoxNGDisableNiSealantHeight.Checked;

            if (!TryParseDouble(textBoxMeanAlSealantHeight, ref _CellDataSpec.AlSealantHeight.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlSealantHeight, ref _CellDataSpec.AlSealantHeight.Tolerance)) return;
            _CellDataSpec.AlSealantHeight.CheckNGDisable = selectBoxNGDisableAlSealantHeight.Checked;

            if (!TryParseDouble(textBoxMeanTabDistance, ref _CellDataSpec.TabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceTabDistance, ref _CellDataSpec.TabDistance.Tolerance)) return;
            _CellDataSpec.TabDistance.CheckNGDisable = selectBoxNGDisableTabDistance.Checked;

            if (!TryParseDouble(textBoxMeanThickness, ref _CellDataSpec.CellThickness.Mean)) return;
            if (!TryParseDouble(textBoxToleranceThickness, ref _CellDataSpec.CellThickness.Tolerance)) return;
            _CellDataSpec.CellThickness.CheckNGDisable = selectBoxNGDisableThickness.Checked;

            if (!TryParseDouble(textBoxMeanShoulderWidth, ref _CellDataSpec.ShoulderWidth.Mean)) return;
            if (!TryParseDouble(textBoxToleranceShoulderWidth, ref _CellDataSpec.ShoulderWidth.Tolerance)) return;
            _CellDataSpec.ShoulderWidth.CheckNGDisable = selectBoxNGDisableShoulderWidth.Checked;
        }
        public event EventHandler SaveParaEvent;
        private bool isSaveToExcel;
        private void buttonSaveProduct_Click(object sender, EventArgs e)
        {
            if (isGaugeChanged != "")
                if (MessageBox.Show($"使用标准块有变更({isGaugeChanged})，是否确定？", "标准块变更", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    UseGauge = Products[SelectedProduct].UseGauge;
                    MessageBox.Show("放弃保存。");
                    return;
                }
            isSaveToExcel = true;
            SaveProduct();
        }
        private void SaveProduct()
        {
            if (SelectedProduct == "")
            {
                MessageBox.Show("没有产品被选择，无法保存。");
                return;
            }
            else
            {
                GetCellData();
                _Products[SelectedProduct].GetDataFromInterface(this);
            }
            string temp = SelectedProduct;
            if (SaveParaEvent != null) SaveParaEvent(this, null);
            listBoxProduct.Text = temp;
            listBoxProduct_SelectedIndexChanged(null, null);
            if (isSaveToExcel)
                if (MessageBox.Show("是否将产品 " + SelectedProduct + " 的测量参数保存为Excel文件？", "保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Excel文件(*.xlsx)|*.xlsx";
                    save.FileName = SelectedProduct + ".xlsx";
                    if (save.ShowDialog() == DialogResult.OK)
                        SaveParaToExcel(save.FileName, SelectedProduct);
                }
            if (SelectedProduct == CurrentProduct)
            {
                _Products[SelectedProduct].RefreshInterface();
                CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(null, new Colibri.CommonModule.State.StateEventArgs(CurrentProduct, ""));
            }
        }
        private string _selectedproduct = "";
        private string SelectedProduct
        {
            get { return _selectedproduct; }
            set
            {
                _selectedproduct = value;
                if (_selectedproduct != "")
                {
                    _Products[_selectedproduct].SetDataToInterface(this);
                    textBoxProductName.Text = _Products[_selectedproduct].ProductName;
                    buttonDeleteProduct.Enabled = _CurrentProduct != _selectedproduct && _CurrentProduct != ClassBaseWorkZone.CALIBPROD;
                }
            }
        }
        private void listBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProduct.SelectedIndex >= 0)
            {
                SelectedProduct = listBoxProduct.Text;
            }
        }

        private void buttonCurrentProduct_Click(object sender, EventArgs e)
        {
            if (SelectedProduct != "")
            {
                CurrentProduct = SelectedProduct;
                if (SaveParaEvent != null) SaveParaEvent(this, null);
                CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(null, new Colibri.CommonModule.State.StateEventArgs(CurrentProduct, ""));
            }
        }

        private void buttonSaveAsProduct_Click(object sender, EventArgs e)
        {
            isSaveToExcel = true;
            string prod = textBoxProductName.Text.Trim();
            if (prod == "")
            {
                MessageBox.Show("另存的产品名称没有设置。输入产品名称后重新另存。");
                return;
            }
            if (prod == ClassBaseWorkZone.CALIBPROD)
            {
                MessageBox.Show("calibration是用于标定的虚拟产品，不可覆盖");
                return;
            }
            if (MessageBox.Show($"将要把产品 {SelectedProduct} 的参数另存到产品 {prod}。{Environment.NewLine}确认请按“确定”，否则请按“取消”退回。", "另存为", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return;
            if (_Products.ContainsKey(prod))
            {
                if (MessageBox.Show("另存的产品已经存在，需要覆盖吗？", "另存为", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                else
                    _Products[prod].GetDataFromInterface(this);
            }
            else
            {
                GetCellData();
                ClassProdParameter.ReadWriteProdVisionParam(SelectedProduct);
                ClassProdParameter.ReadWriteProdVisionParam(prod, false);
                ClassProdParameter newprod = new ClassProdParameter(prod);
                newprod.GetDataFromInterface(this);
                _Products.Add(prod, newprod);
            }
            if (SaveParaEvent != null) SaveParaEvent(this, null);
            listBoxProduct.Text = prod;
            if (isSaveToExcel)
                if (MessageBox.Show("是否将产品 " + prod + " 的测量参数保存为Excel文件？", "保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Excel文件(*.xlsx)|*.xlsx";
                    save.FileName = prod + ".xlsx";
                    if (save.ShowDialog() == DialogResult.OK)
                        SaveParaToExcel(save.FileName, prod);
                }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            if (SelectedProduct == ClassBaseWorkZone.CALIBPROD)
            {
                MessageBox.Show("calibration是用于标定的虚拟产品，不可删除");
                return;
            }
            if (SelectedProduct == CurrentProduct)
            {
                MessageBox.Show("不能删除当前产品。");
                return;
            }
            if (SelectedProduct != "")
            {
                if (MessageBox.Show($"确定要删除 {SelectedProduct} 吗？删除之后不可恢复。", "删除产品", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _Products.Remove(SelectedProduct);
                    ClassCommonSetting.DeleteConfigFile($"ProdParameter{SelectedProduct}.xml");
                    ClassCommonSetting.DeleteConfigFile($"VisionParam{SelectedProduct}.xml");
                    if (SaveParaEvent != null) SaveParaEvent(this, null);
                    SelectedProduct = "";
                }
            }
            else
                MessageBox.Show("当前没有选择产品。");
        }

        private void CalcClampCount(object sender, EventArgs e)
        {
            labelCalcClampCount.Text = "夹紧次数：";
            double cellwidth, clampwidth;
            if (!double.TryParse(textBoxMeanCellWidth.Text.Trim(), out cellwidth)) return;
            if (!double.TryParse(textBoxTopClampWidth.Text.Trim(), out clampwidth)) return;
            double[] offset = ClassCommonSetting.GetTopAlignZClampOffset(cellwidth, clampwidth);
            labelCalcClampCount.Text += offset.Length.ToString();
        }

        private void buttonLoadPara_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "产品参数Excel模板文件(*.csv; *.xls; *.xlsx)| *.csv; *.xls; *.xlsx";
            string paraFile = "";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                paraFile = fileDialog.FileName;
                if (MessageBox.Show("确定要从 " + paraFile + " 中加载并覆盖产品 " + SelectedProduct + " 的测量参数吗？加载后原有测量参数将会被覆盖。", "加载参数", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                LoadParaFromExcel(paraFile);
                FillCellData();
                isSaveToExcel = false;
                SaveProduct();
            }
        }
        private ExcelOfficeOperation m_ExcelOperation;
        private void LoadParaFromExcel(string strFilePath)
        {
            m_ExcelOperation = new ExcelOfficeOperation(strFilePath, "");

            _CellDataSpec.CellThickness.Mean = double.Parse(m_ExcelOperation.GetCellText(2, 2, 1));
            _CellDataSpec.CellWidth.Mean = double.Parse(m_ExcelOperation.GetCellText(3, 2, 1));
            _CellDataSpec.CellLength.Mean = double.Parse(m_ExcelOperation.GetCellText(4, 2, 1));
            _CellDataSpec.NiTabDistance.Mean = double.Parse(m_ExcelOperation.GetCellText(5, 2, 1));
            _CellDataSpec.AlTabDistance.Mean = double.Parse(m_ExcelOperation.GetCellText(6, 2, 1));
            _CellDataSpec.NiTabLength.Mean = double.Parse(m_ExcelOperation.GetCellText(7, 2, 1));
            _CellDataSpec.AlTabLength.Mean = double.Parse(m_ExcelOperation.GetCellText(8, 2, 1));
            _CellDataSpec.NiSealantHeight.Mean = double.Parse(m_ExcelOperation.GetCellText(9, 2, 1));
            _CellDataSpec.AlSealantHeight.Mean = double.Parse(m_ExcelOperation.GetCellText(10, 2, 1));
            _CellDataSpec.TabDistance.Mean = double.Parse(m_ExcelOperation.GetCellText(11, 2, 1));
            _CellDataSpec.ShoulderWidth.Mean = double.Parse(m_ExcelOperation.GetCellText(12, 2, 1));
            _CellDataSpec.CellThickness.Tolerance = double.Parse(m_ExcelOperation.GetCellText(2, 3, 1));
            _CellDataSpec.CellWidth.Tolerance = double.Parse(m_ExcelOperation.GetCellText(3, 3, 1));
            _CellDataSpec.CellLength.Tolerance = double.Parse(m_ExcelOperation.GetCellText(4, 3, 1));
            _CellDataSpec.NiTabDistance.Tolerance = double.Parse(m_ExcelOperation.GetCellText(5, 3, 1));
            _CellDataSpec.AlTabDistance.Tolerance = double.Parse(m_ExcelOperation.GetCellText(6, 3, 1));
            _CellDataSpec.NiTabLength.Tolerance = double.Parse(m_ExcelOperation.GetCellText(7, 3, 1));
            _CellDataSpec.AlTabLength.Tolerance = double.Parse(m_ExcelOperation.GetCellText(8, 3, 1));
            _CellDataSpec.NiSealantHeight.Tolerance = double.Parse(m_ExcelOperation.GetCellText(9, 3, 1));
            _CellDataSpec.AlSealantHeight.Tolerance = double.Parse(m_ExcelOperation.GetCellText(10, 3, 1));
            _CellDataSpec.TabDistance.Tolerance = double.Parse(m_ExcelOperation.GetCellText(11, 3, 1));
            _CellDataSpec.ShoulderWidth.Tolerance = double.Parse(m_ExcelOperation.GetCellText(12, 3, 1));


            m_ExcelOperation.Quit();

        }
        private void SaveParaToExcel(string strFilePath, string prod)
        {
            string[] datanames = Enum.GetNames(typeof(EnumDataName));
            m_ExcelOperation = new ExcelOfficeOperation("", strFilePath);
            m_ExcelOperation.SheetRename(1, prod);
            m_ExcelOperation.SetCellText(1, 1, prod, 1);
            m_ExcelOperation.SetCellText(1, 2, "标准值", 1);
            m_ExcelOperation.SetCellText(1, 3, "公差", 1);
            m_ExcelOperation.SetCellText(2, 1, _CellDataSpec.CellThickness.DataName, 1);
            m_ExcelOperation.SetCellText(3, 1, _CellDataSpec.CellWidth.DataName, 1);
            m_ExcelOperation.SetCellText(4, 1, _CellDataSpec.CellLength.DataName, 1);
            m_ExcelOperation.SetCellText(5, 1, _CellDataSpec.NiTabDistance.DataName, 1);
            m_ExcelOperation.SetCellText(6, 1, _CellDataSpec.AlTabDistance.DataName, 1);
            m_ExcelOperation.SetCellText(7, 1, _CellDataSpec.NiTabLength.DataName, 1);
            m_ExcelOperation.SetCellText(8, 1, _CellDataSpec.AlTabLength.DataName, 1);
            m_ExcelOperation.SetCellText(9, 1, _CellDataSpec.NiSealantHeight.DataName, 1);
            m_ExcelOperation.SetCellText(10, 1, _CellDataSpec.AlSealantHeight.DataName, 1);
            m_ExcelOperation.SetCellText(11, 1, _CellDataSpec.TabDistance.DataName, 1);
            m_ExcelOperation.SetCellText(12, 1, _CellDataSpec.ShoulderWidth.DataName, 1);
            m_ExcelOperation.SetCellText(2, 2, _CellDataSpec.CellThickness.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(3, 2, _CellDataSpec.CellWidth.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(4, 2, _CellDataSpec.CellLength.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(5, 2, _CellDataSpec.NiTabDistance.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(6, 2, _CellDataSpec.AlTabDistance.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(7, 2, _CellDataSpec.NiTabLength.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(8, 2, _CellDataSpec.AlTabLength.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(9, 2, _CellDataSpec.NiSealantHeight.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(10, 2, _CellDataSpec.AlSealantHeight.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(11, 2, _CellDataSpec.TabDistance.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(12, 2, _CellDataSpec.ShoulderWidth.Mean.ToString(), 1);
            m_ExcelOperation.SetCellText(2, 3, _CellDataSpec.CellThickness.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(3, 3, _CellDataSpec.CellWidth.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(4, 3, _CellDataSpec.CellLength.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(5, 3, _CellDataSpec.NiTabDistance.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(6, 3, _CellDataSpec.AlTabDistance.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(7, 3, _CellDataSpec.NiTabLength.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(8, 3, _CellDataSpec.AlTabLength.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(9, 3, _CellDataSpec.NiSealantHeight.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(10, 3, _CellDataSpec.AlSealantHeight.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(11, 3, _CellDataSpec.TabDistance.Tolerance.ToString(), 1);
            m_ExcelOperation.SetCellText(12, 3, _CellDataSpec.ShoulderWidth.Tolerance.ToString(), 1);
            m_ExcelOperation.SaveAll();
            m_ExcelOperation.Quit();

        }

        private void buttonSysSave_Click(object sender, EventArgs e)
        {
            int VacuumDelayTime;
            if (!int.TryParse(textBoxDelayTime.Text.Trim(), out VacuumDelayTime))
            {
                textBoxDelayTime.Focus();
                textBoxDelayTime.SelectAll();
                MessageBox.Show("数值格式有错误，请重新输入。");
                return;
            }
            int ManualLoadDelayTime;
            if (!int.TryParse(textBoxManualLoadDelayTime.Text.Trim(), out ManualLoadDelayTime))
            {
                textBoxManualLoadDelayTime.Focus();
                textBoxManualLoadDelayTime.SelectAll();
                MessageBox.Show("数值格式有错误，请重新输入。");
                return;
            }
            int OdsInterval;
            if (!int.TryParse(textBoxOdsInterval.Text.Trim(), out OdsInterval))
            {
                textBoxOdsInterval.Focus();
                textBoxOdsInterval.SelectAll();
                MessageBox.Show("数值格式有错误，请重新输入。");
                return;
            }
            if (!Directory.Exists(textBoxOdsPath.Text.Trim()))
                if (MessageBox.Show("Ods路径不存在，是否新建？如新建则点“是”，否则点“否”，本次Ods路径设置不保存。", "保存Ods路径", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(textBoxOdsPath.Text.Trim());
            ClassCommonSetting.SysParam.VacuumDelayTime = VacuumDelayTime;
            ClassCommonSetting.SysParam.ManualLoadDelayTime = ManualLoadDelayTime;
            ClassCommonSetting.SysParam.OdsSaveInterval = OdsInterval;
            ClassCommonSetting.SysParam.OdsSavePath = OdsSavePath;
            ClassCommonSetting.SysParam.LogOutTime = LogOutTime;
            if (SaveParaEvent != null) SaveParaEvent(this, e);
        }

        private void buttonOdsPathBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            if (textBoxOdsPath.Text != "")
                folderBrowserDialog.SelectedPath = textBoxOdsPath.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxOdsPath.Text = folderBrowserDialog.SelectedPath + "\\";
        }

        private void textBoxMeanTabDistance_TextChanged(object sender, EventArgs e)
        {
            double NiDist, AlDist;
            if (double.TryParse(textBoxMeanNiTabDistance.Text, out NiDist) && double.TryParse(textBoxMeanAlTabDistance.Text, out AlDist))
            {
                _CellDataSpec.TabDistance.Mean = Math.Abs(NiDist - AlDist);
                textBoxMeanTabDistance.Text = _CellDataSpec.TabDistance.Mean.ToString();
            }
        }

        private void buttonUpdateTolerance_Click(object sender, EventArgs e)
        {
            if (!TryParseDouble(textBoxMeanCellWidth, ref _CellDataSpec.CellWidth.Mean)) return;
            if (!TryParseDouble(textBoxToleranceCellWidth, ref _CellDataSpec.CellWidth.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanCellLength, ref _CellDataSpec.CellLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceCellLength, ref _CellDataSpec.CellLength.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanNiTabDistance, ref _CellDataSpec.NiTabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabDistance, ref _CellDataSpec.NiTabDistance.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanAlTabDistance, ref _CellDataSpec.AlTabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabDistance, ref _CellDataSpec.AlTabDistance.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanNiTabDistance, ref _CellDataSpec.NiTabDistanceMax.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabDistance, ref _CellDataSpec.NiTabDistanceMax.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanAlTabDistance, ref _CellDataSpec.AlTabDistanceMax.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabDistance, ref _CellDataSpec.AlTabDistanceMax.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanNiTabLength, ref _CellDataSpec.NiTabLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiTabLength, ref _CellDataSpec.NiTabLength.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanAlTabLength, ref _CellDataSpec.AlTabLength.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlTabLength, ref _CellDataSpec.AlTabLength.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanNiSealantHeight, ref _CellDataSpec.NiSealantHeight.Mean)) return;
            if (!TryParseDouble(textBoxToleranceNiSealantHeight, ref _CellDataSpec.NiSealantHeight.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanAlSealantHeight, ref _CellDataSpec.AlSealantHeight.Mean)) return;
            if (!TryParseDouble(textBoxToleranceAlSealantHeight, ref _CellDataSpec.AlSealantHeight.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanTabDistance, ref _CellDataSpec.TabDistance.Mean)) return;
            if (!TryParseDouble(textBoxToleranceTabDistance, ref _CellDataSpec.TabDistance.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanThickness, ref _CellDataSpec.CellThickness.Mean)) return;
            if (!TryParseDouble(textBoxToleranceThickness, ref _CellDataSpec.CellThickness.Tolerance)) return;
            if (!TryParseDouble(textBoxMeanShoulderWidth, ref _CellDataSpec.ShoulderWidth.Mean)) return;
            if (!TryParseDouble(textBoxToleranceShoulderWidth, ref _CellDataSpec.ShoulderWidth.Tolerance)) return;

            textBoxULCellWidth.Text = $"{_CellDataSpec.CellWidth.ULimit:0.000}";
            textBoxLLCellWidth.Text = $"{_CellDataSpec.CellWidth.LLimit:0.000}";
            textBoxULCellLength.Text = $"{_CellDataSpec.CellLength.ULimit:0.000}";
            textBoxLLCellLength.Text = $"{_CellDataSpec.CellLength.LLimit:0.000}";
            textBoxULNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.ULimit:0.000}";
            textBoxLLNiTabDistance.Text = $"{_CellDataSpec.NiTabDistance.LLimit:0.000}";
            textBoxULAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.ULimit:0.000}";
            textBoxLLAlTabDistance.Text = $"{_CellDataSpec.AlTabDistance.LLimit:0.000}";
            textBoxULNiTabLength.Text = $"{_CellDataSpec.NiTabLength.ULimit:0.000}";
            textBoxLLNiTabLength.Text = $"{_CellDataSpec.NiTabLength.LLimit:0.000}";
            textBoxULAlTabLength.Text = $"{_CellDataSpec.AlTabLength.ULimit:0.000}";
            textBoxLLAlTabLength.Text = $"{_CellDataSpec.AlTabLength.LLimit:0.000}";
            textBoxULNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.ULimit:0.000}";
            textBoxLLNiSealantHeight.Text = $"{_CellDataSpec.NiSealantHeight.LLimit:0.000}";
            textBoxULAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.ULimit:0.000}";
            textBoxLLAlSealantHeight.Text = $"{_CellDataSpec.AlSealantHeight.LLimit:0.000}";
            textBoxULTabDistance.Text = $"{_CellDataSpec.TabDistance.ULimit:0.000}";
            textBoxLLTabDistance.Text = $"{_CellDataSpec.TabDistance.LLimit:0.000}";
            textBoxULThickness.Text = $"{_CellDataSpec.CellThickness.ULimit:0.000}";
            textBoxLLThickness.Text = $"{_CellDataSpec.CellThickness.LLimit:0.000}";
            textBoxULShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.ULimit:0.000}";
            textBoxLLShoulderWidth.Text = $"{_CellDataSpec.ShoulderWidth.LLimit:0.000}";
        }

        private void buttonUpdateLimit_Click(object sender, EventArgs e)
        {
            double UL = 0, LL = 0;
            if (!TryParseDouble(textBoxULCellWidth, ref UL)) return;
            if (!TryParseDouble(textBoxLLCellWidth, ref LL)) return;
            textBoxMeanCellWidth.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceCellWidth.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULCellLength, ref UL)) return;
            if (!TryParseDouble(textBoxLLCellLength, ref LL)) return;
            textBoxMeanCellLength.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceCellLength.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULNiTabDistance, ref UL)) return;
            if (!TryParseDouble(textBoxLLNiTabDistance, ref LL)) return;
            textBoxMeanNiTabDistance.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceNiTabDistance.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULAlTabDistance, ref UL)) return;
            if (!TryParseDouble(textBoxLLAlTabDistance, ref LL)) return;
            textBoxMeanAlTabDistance.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceAlTabDistance.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULNiTabLength, ref UL)) return;
            if (!TryParseDouble(textBoxLLNiTabLength, ref LL)) return;
            textBoxMeanNiTabLength.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceNiTabLength.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULAlTabLength, ref UL)) return;
            if (!TryParseDouble(textBoxLLAlTabLength, ref LL)) return;
            textBoxMeanAlTabLength.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceAlTabLength.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULNiSealantHeight, ref UL)) return;
            if (!TryParseDouble(textBoxLLNiSealantHeight, ref LL)) return;
            textBoxMeanNiSealantHeight.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceNiSealantHeight.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULAlSealantHeight, ref UL)) return;
            if (!TryParseDouble(textBoxLLAlSealantHeight, ref LL)) return;
            textBoxMeanAlSealantHeight.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceAlSealantHeight.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULThickness, ref UL)) return;
            if (!TryParseDouble(textBoxLLThickness, ref LL)) return;
            textBoxMeanThickness.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceThickness.Text = $"{(UL - LL) / 2:0.000}";
            if (!TryParseDouble(textBoxULShoulderWidth, ref UL)) return;
            if (!TryParseDouble(textBoxLLShoulderWidth, ref LL)) return;
            textBoxMeanShoulderWidth.Text = $"{(UL + LL) / 2:0.000}";
            textBoxToleranceShoulderWidth.Text = $"{(UL - LL) / 2:0.000}";
        }
        private string _selectedgauge = "";
        private string SelectedGauge
        {
            get { return _selectedgauge; }
            set
            {
                _selectedgauge = value;
                textBoxGaugeName.Text = _selectedgauge;
                dataGridViewGauge.DataSource = null;
                dataGridViewGauge.DataSource = _selectedgauge == "" ? null : Gauges[_selectedgauge];

                textBoxCurRefThickness.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].RefThickness.ToString();
                textBoxLeftReference.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessMeasRefLeft.ToString();
                textBoxMidReference.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessMeasRefMid.ToString();
                textBoxRightReference.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessMeasRefRight.ToString();

                textBoxLeftSlope.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessLeftLinear.Slope.ToString();
                textBoxLeftIntercept.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessLeftLinear.Intercept.ToString();
                selectBoxLeftEnable.Checked = _selectedgauge == "" ? false : Gauges[_selectedgauge].ThicknessLeftLinear.Enable;

                textBoxMidSlope.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessMidLinear.Slope.ToString();
                textBoxMidIntercept.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessMidLinear.Intercept.ToString();
                selectBoxMidEnable.Checked = _selectedgauge == "" ? false : Gauges[_selectedgauge].ThicknessMidLinear.Enable;

                textBoxRightSlope.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessRightLinear.Slope.ToString();
                textBoxRightIntercept.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].ThicknessRightLinear.Intercept.ToString();
                selectBoxRightEnable.Checked = _selectedgauge == "" ? false : Gauges[_selectedgauge].ThicknessRightLinear.Enable;

                textBoxXSlope.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].CCDXLinear.Slope.ToString();
                textBoxXIntercept.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].CCDXLinear.Intercept.ToString();
                selectBoxXEnable.Checked = _selectedgauge == "" ? false : Gauges[_selectedgauge].CCDXLinear.Enable;

                textBoxYSlope.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].CCDYLinear.Slope.ToString();
                textBoxYIntercept.Text = _selectedgauge == "" ? "" : Gauges[_selectedgauge].CCDYLinear.Intercept.ToString();
                selectBoxYEnable.Checked = _selectedgauge == "" ? false : Gauges[_selectedgauge].CCDYLinear.Enable;
            }
        }
        private void listBoxGauge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGauge.SelectedIndex >= 0)
            {
                dataGridViewGauge.Columns.Clear();
                SelectedGauge = listBoxGauge.Text;
            }
        }

        private void buttonSaveGauge_Click(object sender, EventArgs e)
        {
            if (SelectedGauge == "")
            {
                MessageBox.Show("没有标准块被选择。无法保存。");
                return;
            }
            if (!CheckGaugeValue())
                MessageBox.Show("标准块参数格式有错误，请检查并更正。");
            else if (SaveParaEvent != null) SaveParaEvent(this, null);
        }
        private bool CheckGaugeValue()
        {
            if (dataGridViewGauge.DataSource == null) return true;
            double temp;
            bool isOK = true;
            for (int i = 0; i < _Gauges[SelectedGauge].Rows.Count; i++)
            {
                if (!double.TryParse(_Gauges[SelectedGauge].Rows[i][GaugeColName.大.ToString()].ToString(), out temp))
                {
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.大].Style.BackColor = Color.Red;
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.大].Style.ForeColor = Color.Yellow;
                    isOK = false;
                }
                else
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.大].Style = dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.参数].Style;

                if (!double.TryParse(_Gauges[SelectedGauge].Rows[i][GaugeColName.中.ToString()].ToString(), out temp))
                {
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.中].Style.BackColor = Color.Red;
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.中].Style.ForeColor = Color.Yellow;
                    isOK = false;
                }
                else
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.中].Style = dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.参数].Style;
                if (!double.TryParse(_Gauges[SelectedGauge].Rows[i][GaugeColName.小.ToString()].ToString(), out temp))
                {
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.小].Style.BackColor = Color.Red;
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.小].Style.ForeColor = Color.Yellow;
                    isOK = false;
                }
                else
                    dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.小].Style = dataGridViewGauge.Rows[i].Cells[(int)GaugeColName.参数].Style;
            }
            return isOK;
        }
        private void buttonSaveAsGauge_Click(object sender, EventArgs e)
        {
            string gauge = textBoxGaugeName.Text.Trim();
            if (gauge == "")
            {
                MessageBox.Show("另存的标准块名称没有设置。输入标准块名称后重新另存。");
                return;
            }
            if (SelectedGauge == gauge)
            {
                MessageBox.Show("另存的标准块名称与当前选择标准块名称相同，输入正确的标准块名称后重新另存。");
                return;
            }
            if (MessageBox.Show($"将要把标准块 {SelectedGauge} 的参数另存到标准块 {gauge}。{Environment.NewLine}确认请按“确定”，否则请按“取消”退回。", "另存为", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return;
            if (!CheckGaugeValue())
            {
                MessageBox.Show("标准块参数格式有错误，请检查并更正。");
                return;
            }
            if (_Gauges.ContainsKey(gauge))
            {
                if (MessageBox.Show("另存为的标准块已经存在，需要覆盖吗？", "另存为", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                else
                    _Gauges[gauge] = _Gauges[SelectedGauge].Copy();
            }
            else
            {
                ClassGaugeParameter newtable = _Gauges[SelectedGauge].Copy();
                newtable.TableName = gauge;
                _Gauges.Add(gauge, newtable);
            }
            if (SaveParaEvent != null) SaveParaEvent(this, null);
            listBoxGauge.Text = gauge;
        }

        private void buttonDeleteGauge_Click(object sender, EventArgs e)
        {
            if (SelectedGauge != "")
            {
                string temp = "";
                string[] prods = ClassCommonSetting.SysParam.GaugeBindingProduct(SelectedGauge);
                if (prods.Length > 0)
                {
                    temp += $"以下是使用标准块 {SelectedGauge} 的产品：{Environment.NewLine}";
                    foreach (string prod in prods)
                        temp += $"    {prod}{Environment.NewLine}";
                    temp += "删除标准块之后，需要为这些产品指定其它标准块。" + Environment.NewLine + Environment.NewLine;
                }
                temp += $"确定要删除 {SelectedGauge} 吗？删除之后不可恢复。";
                if (MessageBox.Show(temp, "删除标准块", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _Gauges.Remove(SelectedGauge);
                    dataGridViewGauge.DataSource = null;
                    ClassCommonSetting.DeleteConfigFile($"Gauge{SelectedGauge}.xml");
                    ClassCommonSetting.DeleteConfigFile($"GPara{SelectedGauge}.xml");
                    ClassCommonSetting.DeleteConfigFile($"VisionGauge{SelectedGauge}.xml");
                    ClassCommonSetting.DeleteConfigFile($"Pat{SelectedGauge}*.mmo");
                    foreach (string prod in prods)
                        _Products[prod].UseGauge = "";
                    if (SaveParaEvent != null) SaveParaEvent(this, null);
                    SelectedGauge = "";
                    if (SelectedProduct != "")
                        _Products[SelectedProduct].RefreshInterface();
                    CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(null, new Colibri.CommonModule.State.StateEventArgs(CurrentProduct, ""));
                }
            }
            else
                MessageBox.Show("当前没有选择标准块。");
        }

        private void buttonNewGauge_Click(object sender, EventArgs e)
        {
            string gauge = textBoxGaugeName.Text.Trim();
            if (gauge == "")
            {
                MessageBox.Show("没有指定新标准块名称，无法新建。");
                return;
            }
            if (_Gauges.ContainsKey(gauge))
            {
                MessageBox.Show("新标准块名称已经存在，无法新建。");
                return;
            }
            dataGridViewGauge.Columns.Clear();
            ClassGaugeParameter newtable = new ClassGaugeParameter(gauge);
            foreach (string col in Enum.GetNames(typeof(GaugeColName)))
                newtable.Columns.Add(col);
            foreach (string para in Enum.GetNames(typeof(GaugeParaName)))
                newtable.Rows.Add(para);
            _Gauges.Add(gauge, newtable);
            SelectedGauge = gauge;
            dataGridViewGauge.DataSource = newtable;
            if (SaveParaEvent != null) SaveParaEvent(this, null);
        }

        private void dataGridViewGauge_DataSourceChanged(object sender, EventArgs e)
        {
            CheckGaugeValue();
        }

        private void dataGridViewGauge_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CheckGaugeValue();
        }
    }
}