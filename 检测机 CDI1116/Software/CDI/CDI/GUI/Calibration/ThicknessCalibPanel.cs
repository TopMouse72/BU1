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
    public partial class ThicknessCalibPanel : BasePanel, iProdParameterControl, iGaugeParameterControl
    {
        private DataTable DT = new DataTable();
        public ClassZone厚度测量 zone = ClassWorkZones.Instance.WorkZone厚度测量;
        private DataColumn colStdLeft, colDataLeft, colStdMid, colDataMid, colStdRight, colDataRight;
        public ThicknessCalibPanel()
        {
            InitializeComponent();
            colStdLeft = DT.Columns.Add("标准块（左）");
            colDataLeft = DT.Columns.Add("数据（左）");
            colStdMid = DT.Columns.Add("标准块（中）");
            colDataMid = DT.Columns.Add("数据（中）");
            colStdRight = DT.Columns.Add("标准块（右）");
            colDataRight = DT.Columns.Add("数据（右）");
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

        private Cst.Struct_MeasDatas _ProdCellDataSpec;
        private int MeasCount { get => int.Parse(labelMeasCounter.Text); set => labelMeasCounter.Text = value.ToString(); }
       
        public int MeasAmount { get => (int)numericUpDownMeasAmount.Value; set => numericUpDownMeasAmount.Value = value; }
        public Cst.Struct_MeasDatas ProdCellDataSpec
        {
            get
            {
                _ProdCellDataSpec.CellThickness.StdLarge = double.Parse(textBoxStdLarge.Text);
                _ProdCellDataSpec.CellThickness.StdMean = double.Parse(textBoxStdMean.Text);
                _ProdCellDataSpec.CellThickness.StdSmall = double.Parse(textBoxStdSmall.Text);
                return _ProdCellDataSpec;
            }
            set
            {
                _ProdCellDataSpec = value;
                textBoxStdLarge.Text = _ProdCellDataSpec.CellThickness.StdLarge.ToString();
                textBoxStdMean.Text = _ProdCellDataSpec.CellThickness.StdMean.ToString();
                textBoxStdSmall.Text = _ProdCellDataSpec.CellThickness.StdSmall.ToString();
            }
        }
        public new string ProductName { get; set; }
        private string _gaugename;
        public string UseGauge
        {
            get
            { return _gaugename; }
            set
            {
                _gaugename = value;
                labelGauge.Text = $"使用标准块：{_gaugename}。厚度参数在产品界面标准块管理中设置。";
            }
        }
        private ClassGaugeParameter _gauge;
        public ClassGaugeParameter Gauge
        {
            set
            {
                _gauge = value;
                ProdCellDataSpec = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec;
                if (_gauge != null)
                {
                    textBoxCurRefThickness.Text = _gauge.RefThickness.ToString();
                    textBoxNewRefThickness.Text = _gauge.RefThickness.ToString();
                    textBoxLeftReference.Text = _gauge.ThicknessMeasRefLeft.ToString();
                    textBoxMidReference.Text = _gauge.ThicknessMeasRefMid.ToString();
                    textBoxRightReference.Text = _gauge.ThicknessMeasRefRight.ToString();
                    textBoxLeftSlope.Text = _gauge.ThicknessLeftLinear.Slope.ToString();
                    textBoxLeftIntercept.Text = _gauge.ThicknessLeftLinear.Intercept.ToString();
                    selectBoxLeftEnable.Checked = _gauge.ThicknessLeftLinear.Enable;
                    textBoxMidSlope.Text = _gauge.ThicknessMidLinear.Slope.ToString();
                    textBoxMidIntercept.Text = _gauge.ThicknessMidLinear.Intercept.ToString();
                    selectBoxMidEnable.Checked = _gauge.ThicknessMidLinear.Enable;
                    textBoxRightSlope.Text = _gauge.ThicknessRightLinear.Slope.ToString();
                    textBoxRightIntercept.Text = _gauge.ThicknessRightLinear.Intercept.ToString();
                    selectBoxRightEnable.Checked = _gauge.ThicknessRightLinear.Enable;
                }
            }
        }

        public event EventHandler SaveParaEvent;

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _gauge.RefThickness = double.Parse(textBoxNewRefThickness.Text);
            textBoxCurRefThickness.Text = textBoxNewRefThickness.Text;
            _gauge.ThicknessMeasRefLeft = double.Parse(textBoxLeftReference.Text);
            _gauge.ThicknessMeasRefMid = double.Parse(textBoxMidReference.Text);
            _gauge.ThicknessMeasRefRight = double.Parse(textBoxRightReference.Text);
            _gauge.ThicknessLeftLinear.Slope = double.Parse(textBoxLeftSlope.Text);
            _gauge.ThicknessLeftLinear.Intercept = double.Parse(textBoxLeftIntercept.Text);
            _gauge.ThicknessLeftLinear.Enable = selectBoxLeftEnable.Checked;
            _gauge.ThicknessMidLinear.Slope = double.Parse(textBoxMidSlope.Text);
            _gauge.ThicknessMidLinear.Intercept = double.Parse(textBoxMidIntercept.Text);
            _gauge.ThicknessMidLinear.Enable = selectBoxMidEnable.Checked;
            _gauge.ThicknessRightLinear.Slope = double.Parse(textBoxRightSlope.Text);
            _gauge.ThicknessRightLinear.Intercept = double.Parse(textBoxRightIntercept.Text);
            _gauge.ThicknessRightLinear.Enable = selectBoxRightEnable.Checked;
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
        private bool DoRefCali(object sender, EnumCellIndex cell, ref double newRef)
        {
            zone.SetZero();
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
                if (i == (int)cell)
                {
                    zone.ThicknessDataStations[i].CellData = ClassDataInfo.NewCellData();
                    zone.ThicknessCellVacuums[i].SetOutPortStatus(true);
                }
                else
                {
                    zone.ThicknessDataStations[i].CellData = null;
                    zone.ThicknessCellVacuums[i].SetOutPortStatus(false);
                }
            TimeClass.Delay(500);

            bool res = DoAction(sender, () => { return zone.ActionStartThicknessMeas(DataComp.NoComp); }) == null;
            if (res)
                switch (cell)
                {
                    case EnumCellIndex.左电芯:
                        newRef = zone.ThicknessDataStations[cell].CellData.Data.CellThickness.Value
                                + _gauge.RefThickness;
                        break;
                    case EnumCellIndex.中电芯:
                        newRef = zone.ThicknessDataStations[cell].CellData.Data.CellThickness.Value
                                + _gauge.RefThickness;
                        break;
                    case EnumCellIndex.右电芯:
                        newRef = zone.ThicknessDataStations[cell].CellData.Data.CellThickness.Value
                                + _gauge.RefThickness;
                        break;
                }
            zone.ThicknessDataStations[cell].CellData = null;
            zone.ThicknessCellVacuums[cell].SetOutPortStatus(false);
            return res;
        }
        private double avgRefLeft, avgRefMid, avgRefRight;
        private void GetAvgRef()
        {
            int count = 0;
            avgRefLeft = avgRefMid = avgRefRight = 0;
            foreach (KeyValuePair<string, ClassGaugeParameter> gauge in ClassCommonSetting.SysParam.Gauges)
                if (gauge.Key != ClassCommonSetting.SysParam.CurrentProductParam.UseGauge &&
                    gauge.Value.ThicknessMeasRefLeft != 0 &&
                    gauge.Value.ThicknessMeasRefMid != 0 &&
                    gauge.Value.ThicknessMeasRefRight != 0)
                {
                    avgRefLeft += gauge.Value.ThicknessMeasRefLeft;
                    avgRefMid += gauge.Value.ThicknessMeasRefMid;
                    avgRefRight += gauge.Value.ThicknessMeasRefRight;
                    count++;
                }
            if (count > 0)
            {
                avgRefLeft /= count;
                avgRefMid /= count;
                avgRefRight /= count;
            }
        }
        private void buttonStartLeftRefCali_Click(object sender, EventArgs e)
        {
            double temp = 0;
            if (DoRefCali(sender, EnumCellIndex.左电芯, ref temp))
            {
                textBoxLeftReference.Text = temp.ToString("0.000");
                GetAvgRef();
                string err = "";
                double diff = Math.Abs(temp - _gauge.ThicknessMeasRefLeft);
                if (diff > 0.01)
                    err += $"原基准值为{_gauge.ThicknessMeasRefLeft:0.000}，当前值为{temp:0.000}，变化超过0.01。";
                if (Math.Abs(temp - avgRefLeft) >= 0.01)
                {
                    if (err != "") err += Environment.NewLine + Environment.NewLine;
                    err += $"所有标准块平均基准值为{avgRefLeft:0.000}，当前值为{temp:0.000}，相差超过0.01。";
                }
                if (err != "")
                {
                    err = "左基准值警告：" + Environment.NewLine + Environment.NewLine + err;
                    MessageBox.Show(err, "基准值问题", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonStartMidRefCali_Click(object sender, EventArgs e)
        {
            double temp = 0;
            if (DoRefCali(sender, EnumCellIndex.中电芯, ref temp))
            {
                textBoxMidReference.Text = temp.ToString("0.000");
                GetAvgRef();
                string err = "";
                double diff = Math.Abs(temp - _gauge.ThicknessMeasRefMid);
                if (diff > 0.01)
                    err += $"原基准值为{_gauge.ThicknessMeasRefMid:0.000}，当前值为{temp:0.000}，变化超过0.01。";
                if (Math.Abs(temp - avgRefMid) >= 0.01)
                {
                    if (err != "") err += Environment.NewLine + Environment.NewLine;
                    err += $"所有标准块平均基准值为{avgRefMid:0.000}，当前值为{temp:0.000}，相差超过0.01。";
                }
                if (err != "")
                {
                    err = "中基准值警告：" + Environment.NewLine + Environment.NewLine + err;
                    MessageBox.Show(err, "基准值问题", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonStartRightRefCali_Click(object sender, EventArgs e)
        {
            double temp = 0;
            if (DoRefCali(sender, EnumCellIndex.右电芯, ref temp))
            {
                textBoxRightReference.Text = temp.ToString("0.000");
                GetAvgRef();
                string err = "";
                double diff = Math.Abs(temp - _gauge.ThicknessMeasRefRight);
                if (diff > 0.01)
                    err += $"原基准值为{_gauge.ThicknessMeasRefRight:0.000}，当前值为{temp:0.000}，变化超过0.01。";
                if (Math.Abs(temp - avgRefRight) >= 0.01)
                {
                    if (err != "") err += Environment.NewLine + Environment.NewLine;
                    err += $"所有标准块平均基准值为{avgRefRight:0.000}，当前值为{temp:0.000}，相差超过0.01。";
                }
                if (err != "")
                {
                    err = "中基准值警告：" + Environment.NewLine + Environment.NewLine + err;
                    MessageBox.Show(err, "基准值问题", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonCountReset_Click(object sender, EventArgs e)
        {
            DT.Clear();
        }

        private void buttonStartMeasCali_Click(object sender, EventArgs e)
        {
            labelStatus.Text = " ";
            if (comboBoxLeft.Text == "" || comboBoxMid.Text == "" || comboBoxRight.Text == "")
            {
                MessageBox.Show("需要设置标准块测试位置。");
                return;
            }
            zone.SetZero();
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
            {
                zone.ThicknessDataStations[i].CellData = ClassDataInfo.NewCellData();
                zone.ThicknessCellVacuums[i].SetOutPortStatus(true);
            }
            TimeClass.Delay(500);
            bool res = DoAction(sender, () => { return zone.ActionStartThicknessMeas(DataComp.AddAll); }) == null;
            if (res)
            {
                DataRow newrow = DT.NewRow();
                newrow[colStdLeft] = comboBoxLeft.Text;
                newrow[colDataLeft] = zone.ThicknessDataStations[EnumCellIndex.左电芯].CellData.Data.CellThickness.Value.ToString("0.000");
                newrow[colStdMid] = comboBoxMid.Text;
                newrow[colDataMid] = zone.ThicknessDataStations[EnumCellIndex.中电芯].CellData.Data.CellThickness.Value.ToString("0.000");
                newrow[colStdRight] = comboBoxRight.Text;
                newrow[colDataRight] = zone.ThicknessDataStations[EnumCellIndex.右电芯].CellData.Data.CellThickness.Value.ToString("0.000");
                DT.Rows.Add(newrow);
                if (!Cst.Struct_DataInfo.CheckData(comboBoxLeft.Text, _ProdCellDataSpec.CellThickness, ref zone.ThicknessDataStations[EnumCellIndex.左电芯].CellData.Data.CellThickness, 0.01))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataLeft.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataLeft.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxMid.Text, _ProdCellDataSpec.CellThickness, ref zone.ThicknessDataStations[EnumCellIndex.中电芯].CellData.Data.CellThickness, 0.01))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataMid.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataMid.Caption].Style.ForeColor = Color.Yellow;
                }
                if (!Cst.Struct_DataInfo.CheckData(comboBoxRight.Text, _ProdCellDataSpec.CellThickness, ref zone.ThicknessDataStations[EnumCellIndex.右电芯].CellData.Data.CellThickness, 0.01))
                {
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataRight.Caption].Style.BackColor = Color.Red;
                    dataGridView1.Rows[DT.Rows.Count - 1].Cells[colDataRight.Caption].Style.ForeColor = Color.Yellow;
                }
            }
            for (int i = 0; i < ClassBaseWorkZone.CELLCOUNT; i++)
            {
                zone.ThicknessDataStations[i].CellData = null;
                zone.ThicknessCellVacuums[i].SetOutPortStatus(false);
            }
        }
        private void Calc(EnumCellIndex cell)
        {
            double[] x = new double[DT.Rows.Count];
            double[] y = new double[DT.Rows.Count];
            DataColumn xCol = null, yCol = null;
            double K = 1, B = 0;
            switch (cell)
            {
                case EnumCellIndex.左电芯:
                    xCol = colDataLeft;
                    yCol = colStdLeft;
                    break;
                case EnumCellIndex.中电芯:
                    xCol = colDataMid;
                    yCol = colStdMid;
                    break;
                case EnumCellIndex.右电芯:
                    xCol = colDataRight;
                    yCol = colStdRight;
                    break;
            }
            GetData(xCol, yCol, x, y);
            GetLine(x, y, ref K, ref B);
            switch (cell)
            {
                case EnumCellIndex.左电芯:
                    _gauge.ThicknessLeftLinear.Slope = K;
                    textBoxLeftSlope.Text = K.ToString();
                    _gauge.ThicknessLeftLinear.Intercept = B;
                    textBoxLeftIntercept.Text = B.ToString();
                    break;
                case EnumCellIndex.中电芯:
                    _gauge.ThicknessMidLinear.Slope = K;
                    textBoxMidSlope.Text = K.ToString();
                    _gauge.ThicknessMidLinear.Intercept = B;
                    textBoxMidIntercept.Text = B.ToString();
                    break;
                case EnumCellIndex.右电芯:
                    _gauge.ThicknessRightLinear.Slope = K;
                    textBoxRightSlope.Text = K.ToString();
                    _gauge.ThicknessRightLinear.Intercept = B;
                    textBoxRightIntercept.Text = B.ToString();
                    break;
            }
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
                MessageBox.Show("计数值已达到测量次数，需要切换标准块位置。");
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

        private void SaveData(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "") return;
            Stream sm = saveFileDialog1.OpenFile();
            StreamWriter sw = new StreamWriter(sm, Encoding.UTF8);
            string data;
            data = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                        "",
                        "标准块（左）",
                        "标准块值（左）",
                        "数据（左）",
                        "标准块（中）",
                        "标准块值（中）",
                        "数据（中）",
                        "标准块（右）",
                        "标准块值（右）",
                        "数据（右）");
            sw.WriteLine(data);
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                data = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                    i + 1,
                    DT.Rows[i][colStdLeft],
                    _ProdCellDataSpec.CellThickness.GetStdData(DT.Rows[i][colStdLeft].ToString()),
                    DT.Rows[i][colDataLeft],
                    DT.Rows[i][colStdMid],
                    _ProdCellDataSpec.CellThickness.GetStdData(DT.Rows[i][colStdMid].ToString()),
                    DT.Rows[i][colDataMid],
                    DT.Rows[i][colStdRight],
                    _ProdCellDataSpec.CellThickness.GetStdData(DT.Rows[i][colStdRight].ToString()),
                    DT.Rows[i][colDataRight]);
                sw.WriteLine(data);
            }
            sw.Close();
        }
        private void GetData(DataColumn xCol, DataColumn yCol, double[] x, double[] y)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                x[i] = double.Parse(DT.Rows[i][xCol].ToString());
                y[i] = _ProdCellDataSpec.CellThickness.GetStdData(DT.Rows[i][yCol].ToString());

            }
        }
        private void GetLine(double[] x, double[] y, ref double K, ref double B)
        {
            double _averageX = 0;
            double _averageY = 0;
            double _sumX = 0;
            double _sumY = 0;
            double _sumXY = 0;
            double _sumXX = 0;
            for (int i = 0; i < x.Length; i++)
            {
                _sumX += x[i];
                _sumY += y[i];
                _sumXX += x[i] * x[i];
                _sumXY += x[i] * y[i];
            }
            _averageX = _sumX / x.Length;
            _averageY = _sumY / x.Length;
            K = (_sumXY - _averageY * _sumX) / (_sumXX - _averageX * _sumX);
            B = _averageY - K * _averageX;
        }

        private void buttonCalcLinear_Click(object sender, EventArgs e)
        {
            Calc(EnumCellIndex.中电芯);
            Calc(EnumCellIndex.右电芯);
            Calc(EnumCellIndex.左电芯);
        }
    }
}