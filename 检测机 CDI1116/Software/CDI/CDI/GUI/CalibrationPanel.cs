using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Measure;

namespace CDI.GUI
{
    public partial class CalibrationPanel : UserControl, iProdParameterControl
    {
        private ThicknessCalibPanel thickness = new ThicknessCalibPanel();
        private CCDCalibPanel ccd = new CCDCalibPanel();
        private GRRPanel grr = new GRRPanel();

        public event EventHandler SaveParaEvent;

        public CalibrationPanel()
        {
            InitializeComponent();
        }
        public void InitGUI()
        {
            tabControlCali.TabPages[0].Controls.Add(thickness); thickness.BringToFront();
            tabControlCali.TabPages[1].Controls.Add(ccd); ccd.BringToFront();
            tabControlCali.TabPages[2].Controls.Add(grr);
            thickness.Dock = DockStyle.Fill;
            ccd.Dock = DockStyle.Fill;
            grr.Dock = DockStyle.Fill;
            ClassCommonSetting.SysParam.AddGaugeInterface(thickness);
            ClassCommonSetting.SysParam.AddGaugeInterface(ccd);
        }
        public bool CalibrationEnable
        {
            set { thickness.Visible = value; ccd.Visible = value; }
        }

        public string ProductName { get; set; }
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
        public Cst.Struct_MeasDatas ProdCellDataSpec { get; set; }
        public int MeasAmount { get; set; }
        private string _gauge;
        public string UseGauge
        {
            get { return _gauge; }
            set
            {
                _gauge = value;
                thickness.Visible =
                ccd.Visible = _gauge != "";
            }
        }
    }
}