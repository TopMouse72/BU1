using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Measure;
using CDI.StateMachine;
using CDI.Zone;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using MSOffice;

namespace CDI.GUI
{
    public partial class GRRPanel : BasePanel, iProdParameterControl
    {
        private ExcelOfficeOperation m_ExcelOperation;
        private string ReportModelFileColibri = CommonFunction.DefaultConfigPath + "GRR模板_Colibri.xlsx";
        private string BackupReportModelFileColibri = CommonFunction.DefaultBackupConfigPath + "GRR模板_Colibri.xlsx";
        private string ReportModelFileATL = CommonFunction.DefaultConfigPath + "GRR模板_ATL.xlsx";
        private string BackupReportModelFileATL = CommonFunction.DefaultBackupConfigPath + "GRR模板_ATL.xlsx";
        private string GRRSaveLocation = "d:\\GRR.xlsx";
        public GRRPanel()
        {
            InitializeComponent();
            labelSaveLocation.Text += GRRSaveLocation;
            ClassCommonSetting.SysParam.CurrentProductParam.AddParaInterface(this);
            ClassWorkZones.Instance.GRRDTViewer = dataGridViewGRR;
            ClassWorkZones.Instance.GRRDT.RowChanged += DataRowChangeEventHandler;
            _callback = AfterGRRTest;
            CommonFunction.CheckAndRestore(ReportModelFileColibri, BackupReportModelFileColibri);
            CommonFunction.CheckAndRestore(ReportModelFileATL, BackupReportModelFileATL);
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
        private Cst.Struct_MeasDatas _spec;
        public Cst.Struct_MeasDatas ProdCellDataSpec
        {
            get { return _spec; }
            set
            {
                _spec = value;
                textBoxTol厚度.Text = (_spec.CellThickness.GRRTolerance).ToString();
                textBoxTol长度.Text = (_spec.CellLength.GRRTolerance).ToString();
                textBoxTol宽度.Text = (_spec.CellWidth.GRRTolerance).ToString();
                textBoxTolAlTab边距.Text = (_spec.AlTabDistance.GRRTolerance).ToString();
                textBoxTolNiTab边距.Text = (_spec.NiTabDistance.GRRTolerance).ToString();
                textBoxTolAlTab最大边距.Text = (_spec.AlTabDistance.GRRTolerance).ToString();
                textBoxTolNiTab最大边距.Text = (_spec.NiTabDistance.GRRTolerance).ToString();
                textBoxTolAlTab长度.Text = (_spec.AlTabLength.GRRTolerance).ToString();
                textBoxTolNiTab长度.Text = (_spec.NiTabLength.GRRTolerance).ToString();
                textBoxTolTab间距.Text = (_spec.TabDistance.GRRTolerance).ToString();
                textBoxTolAlSealant高度.Text = (_spec.AlSealantHeight.GRRTolerance).ToString();
                textBoxTolNiSealant高度.Text = (_spec.NiSealantHeight.GRRTolerance).ToString();
                textBoxTol肩宽.Text = (_spec.ShoulderWidth.GRRTolerance).ToString();
            }
        }
        //public double RefThickness { get; set; }
        //public double ThicknessMeasRefLeft { get; set; }
        //public double ThicknessMeasRefMid { get; set; }
        //public double ThicknessMeasRefRight { get; set; }
        public int MeasAmount { get; set; }
        public new string ProductName { get; set; }

        public event EventHandler SaveParaEvent;

        private int _count = 0;
        private int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                BaseForm.SetControlText(labelMeasCounter, _count + "/9");
            }
        }

        public string UseGauge { get; set; }

        private Dictionary<string, Dictionary<EnumDataName, List<double>>> GRRData = new Dictionary<string, Dictionary<EnumDataName, List<double>>>();
        private void CollectData()
        {
            GRRData.Clear();
            foreach (DataRow row in ClassWorkZones.Instance.GRRDT.Rows)
            {
                string barcode = row[ClassWorkZones.ColBarcode].ToString();
                //有无条码保存
                if (!GRRData.ContainsKey(barcode))
                {
                    //无条码
                    GRRData.Add(barcode, new Dictionary<EnumDataName, List<double>>());
                    foreach (EnumDataName type in Enum.GetValues(typeof(EnumDataName)))
                        GRRData[barcode].Add(type, new List<double>());
                }
                GRRData[barcode][EnumDataName.厚度].Add(double.Parse(row[ClassWorkZones.ColThickness].ToString()));
                GRRData[barcode][EnumDataName.宽度].Add(double.Parse(row[ClassWorkZones.ColCellWidth].ToString()));
                GRRData[barcode][EnumDataName.长度].Add(double.Parse(row[ClassWorkZones.ColCellLength].ToString()));
                GRRData[barcode][EnumDataName.AlTab边距].Add(double.Parse(row[ClassWorkZones.ColAlTabDist].ToString()));
                GRRData[barcode][EnumDataName.NiTab边距].Add(double.Parse(row[ClassWorkZones.ColNiTabDist].ToString()));
                GRRData[barcode][EnumDataName.AlTab最大边距].Add(double.Parse(row[ClassWorkZones.ColAlTabDistMax].ToString()));
                GRRData[barcode][EnumDataName.NiTab最大边距].Add(double.Parse(row[ClassWorkZones.ColNiTabDistMax].ToString()));
                GRRData[barcode][EnumDataName.AlTab长度].Add(double.Parse(row[ClassWorkZones.ColAlTabLen].ToString()));
                GRRData[barcode][EnumDataName.NiTab长度].Add(double.Parse(row[ClassWorkZones.ColNiTabLen].ToString()));
                GRRData[barcode][EnumDataName.Tab间距].Add(double.Parse(row[ClassWorkZones.ColTabDist].ToString()));
                GRRData[barcode][EnumDataName.AlSealant高度].Add(double.Parse(row[ClassWorkZones.ColAlSealantHi].ToString()));
                GRRData[barcode][EnumDataName.NiSealant高度].Add(double.Parse(row[ClassWorkZones.ColNiSealantHi].ToString()));
                GRRData[barcode][EnumDataName.肩宽].Add(double.Parse(row[ClassWorkZones.ColShoulderWidth].ToString()));
            }
        }
        private void SaveGRRWithColibriModel()
        {
            CollectData();
            //MessageBox.Show("准备生成GRR报告。使用科瑞GRR模板。");
            string[] datanames = Enum.GetNames(typeof(EnumDataName));
            m_ExcelOperation = new ExcelOfficeOperation(ReportModelFileColibri, GRRSaveLocation);
            //MessageBox.Show("新建Excel程序成功。");
            m_ExcelOperation.BuildGRRSheet(Enum.GetNames(typeof(EnumDataName)));
            //MessageBox.Show("Excel文件工作表创建成功，开始导入公差。");
            string tol, grr;
            int part;
            for (int p = 0; p < datanames.Length; p++)
            {
                tol = ((TextBox)this.Controls.Find("textBoxTol" + datanames[p], true)[0]).Text;
                m_ExcelOperation.SetCellText(7, 9, tol, datanames[p]);
            }
            //MessageBox.Show("公差导入完成。开始导入数据。");
            for (int p = 0; p < datanames.Length; p++)
            {
                part = 0;
                foreach (Dictionary<EnumDataName, List<double>> datas in GRRData.Values)
                {
                    for (int i = 0; i < 9; i++)
                        m_ExcelOperation.SetCellText(11 + part, 3 + i + (int)(i / 3) * 2, datas[(EnumDataName)p][i].ToString(), datanames[p]);
                    part++;
                }
                m_ExcelOperation.SetCellText(5, 13, DateTime.Now.ToShortDateString(), datanames[p]);
                m_ExcelOperation.SetCellText(6, 4, datanames[p], datanames[p]);
            }
            //MessageBox.Show("数据导入完成，开始获取GRR结果。");
            for (int p = 0; p < datanames.Length; p++)
            {
                grr = m_ExcelOperation.GetCellText(66, 7, datanames[p]);
                //if (double.Parse(grr.Replace("%", "")) > 10) grr = ((1 + double.Parse(grr.Replace("%", "")) / 100) * 0.05).ToString("0.00%");
                BaseForm.SetControlText(Controls.Find("textBoxGRR" + datanames[p], true)[0], grr);
            }
            //MessageBox.Show("GRR结果已获取。准备保存报告。");
            m_ExcelOperation.SaveAll();
            m_ExcelOperation.Quit();
        }
        private void SaveGRRWithATLModel()
        {
            CollectData();
            //MessageBox.Show("准备生成GRR报告。使用客户GRR模板。");
            string[] datanames = Enum.GetNames(typeof(EnumDataName));
            m_ExcelOperation = new ExcelOfficeOperation(ReportModelFileATL, GRRSaveLocation);
            //MessageBox.Show("新建Excel程序成功。开始导入数据。");
            string tol, grr;
            int row = 9, col = 4;
            int part;
            for (int p = 0; p < datanames.Length; p++)
            {
                //参数
                col = -1;
                switch ((EnumDataName)p)
                {
                    case EnumDataName.厚度: col = 4; break;
                    case EnumDataName.宽度: col = 5; break;
                    case EnumDataName.长度: col = 6; break;
                    case EnumDataName.AlTab边距: col = 7; break;
                    case EnumDataName.AlTab最大边距: col = 8; break;
                    case EnumDataName.NiTab边距: col = 9; break;
                    case EnumDataName.NiTab最大边距: col = 10; break;
                    case EnumDataName.Tab间距: col = 11; break;
                    case EnumDataName.AlSealant高度: col = 12; break;
                    case EnumDataName.NiSealant高度: col = 13; break;
                }
                if (col != -1)
                {
                    tol = m_ExcelOperation.GetCellText(7, col, 1);
                    BaseForm.SetControlText(Controls.Find("textBoxTol" + datanames[p], true)[0], tol);
                    part = 0;
                    foreach (Dictionary<EnumDataName, List<double>> datas in GRRData.Values)
                    {
                        for (int i = 0; i < 9; i++)
                            m_ExcelOperation.SetCellText(row + part * 3 + i + (int)(i / 3) * 27, col, datas[(EnumDataName)p][i].ToString(), 1);
                        part++;
                    }
                }
            }
            //MessageBox.Show("数据导入完成，开始获取GRR结果。");
            //Get summary
            for (int p = 0; p < datanames.Length; p++)
            {
                switch ((EnumDataName)p)
                {
                    case EnumDataName.厚度: row = 7; break;
                    case EnumDataName.宽度: row = 8; break;
                    case EnumDataName.长度: row = 9; break;
                    case EnumDataName.AlTab边距: row = 10; break;
                    case EnumDataName.AlTab最大边距: row = 11; break;
                    case EnumDataName.NiTab边距: row = 12; break;
                    case EnumDataName.NiTab最大边距: row = 13; break;
                    case EnumDataName.Tab间距: row = 14; break;
                    case EnumDataName.AlSealant高度: row = 15; break;
                    case EnumDataName.NiSealant高度: row = 16; break;
                    default: row = -1; break;
                }
                if (row > 0)
                    grr = m_ExcelOperation.GetCellText(row, 7, 2);
                else
                    grr = "NA";
                BaseForm.SetControlText(Controls.Find("textBoxGRR" + datanames[p], true)[0], grr);
            }
            //MessageBox.Show("GRR结果已获取。准备保存报告。");
            m_ExcelOperation.SaveAll();
            m_ExcelOperation.Quit();
        }
        private void SaveTol()
        {
            _spec.CellThickness.GRRTolerance = double.Parse(textBoxTol厚度.Text);
            _spec.CellLength.GRRTolerance = double.Parse(textBoxTol长度.Text);
            _spec.CellWidth.GRRTolerance = double.Parse(textBoxTol宽度.Text);
            _spec.AlTabDistance.GRRTolerance = double.Parse(textBoxTolAlTab边距.Text);
            _spec.NiTabDistance.GRRTolerance = double.Parse(textBoxTolNiTab边距.Text);
            _spec.AlTabDistance.GRRTolerance = double.Parse(textBoxTolAlTab最大边距.Text);
            _spec.NiTabDistance.GRRTolerance = double.Parse(textBoxTolNiTab最大边距.Text);
            _spec.AlTabLength.GRRTolerance = double.Parse(textBoxTolAlTab长度.Text);
            _spec.NiTabLength.GRRTolerance = double.Parse(textBoxTolNiTab长度.Text);
            _spec.TabDistance.GRRTolerance = double.Parse(textBoxTolTab间距.Text);
            _spec.AlSealantHeight.GRRTolerance = double.Parse(textBoxTolAlSealant高度.Text);
            _spec.NiSealantHeight.GRRTolerance = double.Parse(textBoxTolNiSealant高度.Text);
            _spec.ShoulderWidth.GRRTolerance = double.Parse(textBoxTol肩宽.Text);
            ClassCommonSetting.SysParam.CurrentProductParam.GetDataFromInterface(this);
            if (SaveParaEvent != null) SaveParaEvent(this, null);
        }
        private EnumUnloadMode oldunloadmode;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if ((ClassWorkZones.Instance.GRRDT.Rows.Count % int.Parse(textBoxSampleCount.Text)) != 0)
            {
                MessageBox.Show("GRR数据个数有错误。请删除错误的数据后重新开始。");
                return;
            }
            if (ClassWorkZones.Instance.WorkZone外框架.ActionStartWorkFlow(WFContFrom.GRR) == "")
            {
                oldunloadmode = ClassWorkFlow.Instance.UnloadMode;
                ClassWorkFlow.Instance.UnloadMode = EnumUnloadMode.全NG;
                ClassWorkFlow.Instance.ProductCount = int.Parse(textBoxSampleCount.Text);
                ClassWorkZones.Instance.WorkZone上料传送.BarcodeEnabled = selectBoxXBarcodeEnable.Checked;
                Count = ClassWorkZones.Instance.GRRDT.Rows.Count / int.Parse(textBoxSampleCount.Text);
                ClassWorkZones.Instance.WorkZone上料传送.GRRStartOffset = Count;
                Count++;
                ClassWorkFlow.Instance.CDIMainSM.subscribeMeToResponseEvents(this);
            }
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.IsGRR = false;
            ClassWorkZones.Instance.WorkZone外框架.ActionStopWorkFlow(WFContFrom.GRR);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Count = 0;
            ClassWorkZones.Instance.GRRDT.Clear();
        }
        public override void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            base.DoneHandler(sender, e);
            ClassWorkFlow.Instance.IsGRR = false;
            ClassWorkZones.Instance.WorkZone外框架.ActionStopWorkFlow(WFContFrom.GRR);
            ClassWorkFlow.Instance.UnloadMode = oldunloadmode;
            ClassWorkFlow.Instance.CDIMainSM.unsubscribeMeFromResponseEvents(this);
            if (Count < 9)
            {
                ClassErrorHandle.ShowError("GRR", $"GRR第{Count}次测量已经完成。", ErrorLevel.Notice);
            }
            else
            {
                AfterGRRTest();
            }
            ClassWorkZones.Instance.WorkZone上料传送.BarcodeEnabled = true;
            ClassWorkZones.Instance.WorkZone上料传送.GRRStartOffset = -1;
        }
        private void DataRowChangeEventHandler(object sender, DataRowChangeEventArgs e)
        {
            if (dataGridViewGRR.RowCount > 0)
                dataGridViewGRR.FirstDisplayedScrollingRowIndex = dataGridViewGRR.RowCount - 1;
        }
        private CallBackCommonFunc _callback;//For GRR data processing only for GRR testing with 90 pictures.
        private object CallBackLock = new object();
        private void CallBackAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "GRRTest");
        }
        private void AfterGRRTest()
        {
            SaveGRR();
            isPicMeas = false;
            ClassErrorHandle.ShowError("GRR", "GRR测量完成。", ErrorLevel.Notice);
        }
        private void dataGridViewGRR_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (isPicMeas && dataGridViewGRR.RowCount == int.Parse(textBoxSampleCount.Text) * 9)
            {
                ClassWorkFlow.Instance.IsGRR = false;
                if (_callback != null)
                    _callback.BeginInvoke(CallBackAsyncReturn, _callback);
            }
        }
        private bool isPicMeas = false;
        private void buttonBatchMeas_Click(object sender, EventArgs e)
        {
            SaveTol();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ClassWorkFlow.Instance.IsGRR = true;
                isPicMeas = true;
                buttonClear_Click(null, null);
                ClassCommonSetting.SocketToAOI.SendCommandBatchMeas(folderBrowserDialog1.SelectedPath);
            }
        }

        private void buttonSaveGRR_Click(object sender, EventArgs e)
        {
            SaveGRR();
        }

        private void SaveGRR()
        {
            BaseForm.SetControlText(labelSaveStatus, "正在导出数据到GRR模板文件。");
            if (optionBoxGRRModelATL.Checked)
                SaveGRRWithATLModel();
            else
                SaveGRRWithColibriModel();
            BaseForm.SetControlText(labelSaveStatus, "");
            SaveTol();
        }
        private void selectBoxDisplayResult_Click(object sender, EventArgs e)
        {
            label12.Visible =
            textBoxGRR厚度.Visible =
            textBoxGRR长度.Visible =
            textBoxGRR宽度.Visible =
            textBoxGRRAlTab边距.Visible =
            textBoxGRRNiTab边距.Visible =
            textBoxGRRAlTab最大边距.Visible =
            textBoxGRRNiTab最大边距.Visible =
            textBoxGRRAlTab长度.Visible =
            textBoxGRRNiTab长度.Visible =
            textBoxGRRTab间距.Visible =
            textBoxGRRAlSealant高度.Visible =
            textBoxGRRNiSealant高度.Visible =
            textBoxGRR肩宽.Visible = selectBoxDisplayResult.Checked;
        }

        private void selectBoxDisplayTol_Click(object sender, EventArgs e)
        {
            label11.Visible =
            textBoxTol厚度.Visible =
            textBoxTol长度.Visible =
            textBoxTol宽度.Visible =
            textBoxTolAlTab边距.Visible =
            textBoxTolNiTab边距.Visible =
            textBoxTolAlTab最大边距.Visible =
            textBoxTolNiTab最大边距.Visible =
            textBoxTolAlTab长度.Visible =
            textBoxTolNiTab长度.Visible =
            textBoxTolTab间距.Visible =
            textBoxTolAlSealant高度.Visible =
            textBoxTolNiSealant高度.Visible =
            textBoxTol肩宽.Visible = selectBoxDisplayTol.Checked;
        }

        private void buttonImportData_Click(object sender, EventArgs e)
        {
            string[] line;
            string[] data;
            MessageBox.Show("将从Data文件夹中选择保存的数据文件中导入90行数据。");
            openFileDialog1.InitialDirectory = ClassCommonSetting.SysParam.DataSavePath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream file = openFileDialog1.OpenFile();
                byte[] buffer = new byte[30000];
                file.Read(buffer, 0, 30000);
                string strings = Encoding.UTF8.GetString(buffer);
                line = strings.Split('\n');
                GRRData.Clear();
                ClassWorkZones.Instance.GRRDT.Clear();
                for (int i = 2; i < 92; i++)
                {
                    data = line[i].Split(',');
                    DataRow row = ClassWorkZones.Instance.GRRDT.NewRow();
                    row[ClassWorkZones.ColIndex] = i - 1;
                    row[ClassWorkZones.ColTime] = data[3];
                    row[ClassWorkZones.ColBarcode] = data[0];
                    row[ClassWorkZones.ColThickness] = data[4];
                    row[ClassWorkZones.ColCellWidth] = data[5];
                    row[ClassWorkZones.ColCellLength] = data[7];
                    row[ClassWorkZones.ColAlTabDist] = data[8];
                    row[ClassWorkZones.ColNiTabDist] = data[10];
                    row[ClassWorkZones.ColAlTabDistMax] = data[9];
                    row[ClassWorkZones.ColNiTabDistMax] = data[11];
                    row[ClassWorkZones.ColAlTabLen] = data[12];
                    row[ClassWorkZones.ColNiTabLen] = data[13];
                    row[ClassWorkZones.ColTabDist] = data[14];
                    row[ClassWorkZones.ColAlSealantHi] = data[15];
                    row[ClassWorkZones.ColNiSealantHi] = data[16];
                    row[ClassWorkZones.ColShoulderWidth] = data[6];
                    ClassWorkZones.Instance.GRRDT.Rows.Add(row);
                }
            }
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "") return;
            Stream sm = saveFileDialog1.OpenFile();
            StreamWriter sw = new StreamWriter(sm, Encoding.UTF8);
            string data;
            data = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                        "索引",
                        "时间",
                        "条码",
                        "厚度",
                        "宽度",
                        "长度",
                        "AlTab边距",
                        "NiTab边距",
                        "AlTab最大边距",
                        "NiTab最大边距",
                        "AlTab长度",
                        "NiTab长度",
                        "Tab间距",
                        "Al小白胶高度",
                        "Ni小白胶高度",
                        "肩宽");
            sw.WriteLine(data);
            for (int i = 0; i < ClassWorkZones.Instance.GRRDT.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < ClassWorkZones.Instance.GRRDT.Columns.Count; j++)
                    data += ClassWorkZones.Instance.GRRDT.Rows[i][j] + ",";
                sw.WriteLine(data);
            }
            sw.Close();
        }
    }
}