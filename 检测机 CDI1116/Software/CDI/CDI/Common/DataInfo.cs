using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Windows.Forms;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.XML;
using Colibri.CommonModule.State;
using Measure;
using CDI.GUI;

namespace CDI
{
    public enum LogOutItem
    {
        不注销,
        三十秒,
        一分钟,
        二分钟,
        三分钟,
        五分钟,
        十分钟,
        十五分钟,
        二十分钟,
    }
    [Flags]
    public enum DataComp
    {
        NoComp = 0,
        AddRef = 0x1,
        AddComp = 0x2,
        AddAll = AddRef | AddComp,
    }
    public class ClassDataStation
    {
        private event CallBackCommonFunc _asyncRefresh;
        private List<IDataDisp> _dispList = new List<IDataDisp>();
        public string StationName;
        private ClassDataInfo _celldata = null;
        public ClassDataInfo CellData { get { return _celldata; } set { _celldata = value; Refresh(); } }
        public ClassDataStation(string name)
        {
            StationName = name;
        }
        public void AddDisp(IDataDisp disp)
        {
            if (disp != null)
            {
                _dispList.Add(disp);
                disp.DataStation = this;
                _asyncRefresh -= disp.RefreshData;
                _asyncRefresh += disp.RefreshData;
            }
        }
        public void RemoveDisp(IDataDisp disp)
        {
            if (disp != null)
            {
                _dispList.Remove(disp);
                disp.DataStation = null;
                _asyncRefresh -= disp.RefreshData;
            }
        }
        private object RefreshLock = new object();
        private void RefreshAsyncReturn(IAsyncResult result)
        {
            ClassCommonSetting.CallBackCommonAsyncReturn(result, "Refresh");
        }
        private void notifyRefreshSubscribers()
        {
            lock (RefreshLock)
            {
                if (_asyncRefresh != null)
                    foreach (CallBackCommonFunc handler in _asyncRefresh.GetInvocationList())
                        handler.BeginInvoke(RefreshAsyncReturn, handler);
            }
        }
        public void Refresh()
        {
            //notifyRefreshSubscribers();

            foreach (IDataDisp disp in _dispList)
            {
                BaseForm.DoInvokeRequired((Control)disp, () => disp.RefreshData());
            }
        }
        public void TransferFrom(ClassDataStation source)
        {
            CellData = source.CellData;
            source.CellData = null;
            if (CellData != null)
            {
                CellData.isPickingError = false;
            }
        }
    }
    /// <summary>
    /// 电芯数据类
    /// </summary>
    public class ClassDataInfo
    {
        public const double CELLPITCH = 135;
        private int _index;
        /// <summary>
        /// 电芯索引
        /// </summary>
        public int Index { get { return _index; } }
        public bool isPickingError = false;
        /// <summary>
        /// 电芯条码
        /// </summary>
        public string Barcode = "";
        /// <summary> 
        /// 上料NG。目前只是条码NG
        /// </summary>
        public bool LoadNG
        {
            get { return Barcode == ""; }
        }
        /// <summary>
        /// 电芯下料NG
        /// </summary>
        public bool DataNG
        {
            get { return CCDNG || ThicknessNG; }
        }
        public bool CCDNG
        {
            get { return Data.CCDNG; }
        }
        public bool ThicknessNG
        {
            get { return Data.ThicknessNG; }
        }
        public string NGItem
        {
            get { return Data.NGItem; }
        }
        public string NGDataInfoString
        {
            get
            {
                if (NGItem != "")
                    return string.Format("{0}: {1}", Barcode, NGItem);
                else
                    return "";
            }
        }
        public bool UnloadSorted = false;
        /// <summary>
        /// 电芯外观尺寸测量值和电芯厚度测量值
        /// </summary>
        public Cst.Struct_MeasDatas Data;
        public static Cst.Struct_MeasDatas DataSpec;
        private ClassDataInfo(int no, Cst.Struct_MeasDatas spec)
        {
            _index = no;
            Data = spec;
            Data.Clear();
        }
        public static ClassDataInfo NewCellData(int cellindex = -1)
        {
            return new ClassDataInfo(cellindex, DataSpec);
        }
    }
    /// <summary>
    /// 通用参数类
    /// </summary>
    public abstract class ClassParameter : BaseClass, IParameterControl
    {
        private List<IParameterControl> _ParaControls = new List<IParameterControl>();
        /// <summary>
        /// 添加参数修改显示界面
        /// </summary>
        /// <param name="ParaInterface">参数修改显示界面</param>
        public void AddParaInterface(IParameterControl ParaInterface)
        {
            if (_ParaControls.Contains(ParaInterface)) return;
            _ParaControls.Add(ParaInterface);
            ConnectToInterface(ParaInterface);
            SetDataToInterface(ParaInterface);
        }
        public void MoveAllInterfaceTo(ClassParameter prod)
        {
            IParameterControl[] contorls = _ParaControls.ToArray();
            foreach (IParameterControl control in contorls)
            {
                prod.AddParaInterface(control);
                RemoveInterface(control);
            }
            prod.RefreshInterface();
        }
        /// <summary>
        /// 移除参数修改显示界面
        /// </summary>
        /// <param name="ParaInterface">参数修改显示界面</param>
        public void RemoveInterface(IParameterControl ParaInterface)
        {
            if (ParaInterface == null) return;
            _ParaControls.Remove(ParaInterface);
            DisconnectFromInterface(ParaInterface);
        }
        private string _xmlfile;
        /// <summary>
        /// 获取参数保存文件名
        /// </summary>
        public string XmlFile
        {
            get { return _xmlfile; }
        }
        public ClassParameter(string name)
        {
            Name = name;
            _xmlfile = name + ".xml";
        }

        event EventHandler IParameterControl.SaveParaEvent
        {
            add
            {
            }

            remove
            {
            }
        }

        protected abstract void Clone(ClassParameter source);
        protected virtual void ConnectToInterface(IParameterControl control)
        {
            control.SaveParaEvent -= SaveParaEventHandler;
            control.SaveParaEvent += SaveParaEventHandler;
        }
        protected virtual void DisconnectFromInterface(IParameterControl control)
        {
            control.SaveParaEvent -= SaveParaEventHandler;
        }
        public virtual void SetDataToInterface(IParameterControl control) { }
        public virtual void GetDataFromInterface(IParameterControl control) { }
        public void RefreshInterface()
        {
            foreach (IParameterControl control in _ParaControls)
                SetDataToInterface(control);
        }
        protected void SaveParaEventHandler(object sender, EventArgs e)
        {
            GetDataFromInterface((IParameterControl)sender);
            SaveParameter();
            RefreshInterface();
        }
        public virtual void LoadParameter()
        {
            object temp = new SerialXML().ReadSetting(XmlFile, Name, GetType());
            if (temp != null) Clone((ClassParameter)temp);
        }
        public virtual void SaveParameter()
        {
            new SerialXML().SaveSetting(XmlFile, Name, this, FileMode.Create);
        }
    }
    public class ClassSystemParameter : ClassParameter, ISystemParameterControl
    {
        private List<iGaugeParameterControl> _gaugeControlList = new List<iGaugeParameterControl>();
        #region 参数
        private string _CurrentProduct = "";
        private Dictionary<string, ClassProdParameter> _Products = new Dictionary<string, ClassProdParameter>();
        private Dictionary<string, ClassGaugeParameter> _Gauges = new Dictionary<string, ClassGaugeParameter>();
        public string CCDSavePath { get;
        //set { _CCDSavePath = value; }
        } = CommonFunction.DataPath + "Pic\\";
        public string DataSavePath { get; } = CommonFunction.DataPath + "data\\";
        public string HistoryPath { get; } = CommonFunction.DataPath + "History\\";

        public string CurrentProduct
        {
            get { return _CurrentProduct; }
            set
            {
                if (_CurrentProduct != value)
                    if (_CurrentProduct != "" && _Products[_CurrentProduct] != null && _Products[value] != null)
                        _Products[_CurrentProduct].MoveAllInterfaceTo(_Products[value]);
                _CurrentProduct = value;
                if (_Products.ContainsKey(value)) ClassDataInfo.DataSpec = CurrentProductParam.CellDataSpec;
                CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(this, new StateEventArgs(_CurrentProduct, ""));
            }
        }
        [XmlIgnore]
        public ClassProdParameter CurrentProductParam
        {
            get
            {
                if (CurrentProduct != null && CurrentProduct != "")
                    return _Products[CurrentProduct];
                else
                    return new ClassProdParameter("Default");
            }
        }
        [XmlIgnore]
        public Dictionary<string, ClassProdParameter> Products { get { return _Products; } set { _Products = value; } }
        [XmlIgnore]
        public Dictionary<string, ClassGaugeParameter> Gauges { get { return _Gauges; } set { _Gauges = value; } }
        [XmlIgnore]
        public ClassGaugeParameter CurrentUsedGauge
        {
            get
            {
                if (CurrentProductParam.UseGauge != "" && _Gauges.ContainsKey(CurrentProductParam.UseGauge))
                    return _Gauges[CurrentProductParam.UseGauge];
                else
                    return null;
            }
        }
        public int VacuumDelayTime { get; set; } = 500;

        public int ManualLoadDelayTime { get; set; } = 500;

        public int ThicknessMeasDelayTime { get; set; } = 500;

        public bool LoadInSMEMAIgnored { get; set; } = true;

        public bool UnloadOutSMEMAIgnored { get; set; } = true;

        public int OdsSaveInterval { get; set; } = 120;
        public string OdsSavePath { get; set; }
        public LogOutItem LogOutTime { get; set; } = LogOutItem.三分钟;

        public CDI.StateMachine.EnumLoadMode LoadMode = CDI.StateMachine.EnumLoadMode.自动;
        #endregion 参数

        protected override void Clone(ClassParameter source)
        {
            ClassSystemParameter SysParaInterface = (ClassSystemParameter)source;
            VacuumDelayTime = SysParaInterface.VacuumDelayTime;
            ManualLoadDelayTime = SysParaInterface.ManualLoadDelayTime;
            ThicknessMeasDelayTime = SysParaInterface.ThicknessMeasDelayTime;
            Products = SysParaInterface.Products;
            Gauges = SysParaInterface.Gauges;
            CurrentProduct = SysParaInterface.CurrentProduct;
            LoadInSMEMAIgnored = SysParaInterface.LoadInSMEMAIgnored;
            UnloadOutSMEMAIgnored = SysParaInterface.UnloadOutSMEMAIgnored;
            OdsSaveInterval = SysParaInterface.OdsSaveInterval;
            OdsSavePath = SysParaInterface.OdsSavePath;
            LogOutTime = SysParaInterface.LogOutTime;
            LoadMode = SysParaInterface.LoadMode;
        }
        public void GetProductList()
        {
            string[] files = Directory.GetFiles(CommonFunction.DefaultConfigPath, "ProdParameter*.xml");
            for (int i = 0; i < files.Length; i++)
            {
                int start = files[i].LastIndexOf("\\");
                int end = files[i].LastIndexOf(".");
                int offset = "ProdParameter".Length;
                ClassProdParameter newprod = new ClassProdParameter(files[i].Substring(start + 1 + offset, end - start - 1 - offset));
                newprod.LoadParameter();
                Products.Add(newprod.ProductName, newprod);
            }
            if (_Products.ContainsKey(CurrentProduct)) ClassDataInfo.DataSpec = CurrentProductParam.CellDataSpec;
        }
        public void GetGaugeList()
        {
            string[] files = Directory.GetFiles(CommonFunction.DefaultConfigPath, "Gauge*.xml");
            for (int i = 0; i < files.Length; i++)
            {
                int start = files[i].LastIndexOf("\\");
                int end = files[i].LastIndexOf(".");
                int offset = "Gauge".Length;
                string GaugeName = files[i].Substring(start + 1 + offset, end - start - 1 - offset);
                ClassGaugeParameter table = new ClassGaugeParameter(GaugeName);
                table.LoadParameter();
                _Gauges.Add(table.TableName, table);
            }
        }
        public string[] GaugeBindingProduct(string GaugeName)
        {
            List<string> temp = new List<string>();
            foreach (KeyValuePair<string, ClassProdParameter> prod in _Products)
                if (prod.Value.UseGauge == GaugeName)
                    temp.Add(prod.Key);
            return temp.ToArray();
        }
        public override void LoadParameter()
        {
            base.LoadParameter();
            GetProductList();
            GetGaugeList();
            if (CurrentProductParam.UseGauge != "" && _Gauges.ContainsKey(CurrentProductParam.UseGauge))
                CurrentProductParam.SetGaugeValue(_Gauges[CurrentProductParam.UseGauge]);
        }
        public override void SaveParameter()
        {
            base.SaveParameter();
            if (CurrentProductParam.UseGauge != "" && _Gauges.ContainsKey(CurrentProductParam.UseGauge))
            {
                CurrentProductParam.SetGaugeValue(_Gauges[CurrentProductParam.UseGauge]);
                foreach (iGaugeParameterControl control in _gaugeControlList)
                    control.Gauge = _Gauges[CurrentProductParam.UseGauge];
            }
            else
                foreach (iGaugeParameterControl control in _gaugeControlList)
                    control.Gauge = null;
            foreach (ClassProdParameter prod in _Products.Values)
                prod.SaveParameter();
            foreach (ClassGaugeParameter table in _Gauges.Values)
                table.SaveParameter();
            ClassCommonSetting.SocketToAOI.SendCommandProductChange(CurrentProduct);
        }
        public override void GetDataFromInterface(IParameterControl control)
        {
            base.GetDataFromInterface(control);
            ISystemParameterControl SysParaInterface = (ISystemParameterControl)control;
            VacuumDelayTime = SysParaInterface.VacuumDelayTime;
            ManualLoadDelayTime = SysParaInterface.ManualLoadDelayTime;
            ThicknessMeasDelayTime = SysParaInterface.ThicknessMeasDelayTime;
            Products = SysParaInterface.Products;
            Gauges = SysParaInterface.Gauges;
            CurrentProduct = SysParaInterface.CurrentProduct;
            LoadInSMEMAIgnored = SysParaInterface.LoadInSMEMAIgnored;
            UnloadOutSMEMAIgnored = SysParaInterface.UnloadOutSMEMAIgnored;
            OdsSaveInterval = SysParaInterface.OdsSaveInterval;
            OdsSavePath = SysParaInterface.OdsSavePath;
            LogOutTime = SysParaInterface.LogOutTime;
        }
        public override void SetDataToInterface(IParameterControl control)
        {
            base.SetDataToInterface(control);
            ISystemParameterControl SysParaInterface = (ISystemParameterControl)control;
            SysParaInterface.VacuumDelayTime = VacuumDelayTime;
            SysParaInterface.ManualLoadDelayTime = ManualLoadDelayTime;
            SysParaInterface.ThicknessMeasDelayTime = ThicknessMeasDelayTime;
            SysParaInterface.Products = Products;
            SysParaInterface.Gauges = Gauges;
            SysParaInterface.CurrentProduct = CurrentProduct;
            SysParaInterface.LoadInSMEMAIgnored = LoadInSMEMAIgnored;
            SysParaInterface.UnloadOutSMEMAIgnored = UnloadOutSMEMAIgnored;
            SysParaInterface.OdsSaveInterval = OdsSaveInterval;
            SysParaInterface.OdsSavePath = OdsSavePath;
            SysParaInterface.LogOutTime = LogOutTime;
        }
        public ClassSystemParameter() : base("SystemParameter")
        {

        }
        public void AddGaugeInterface(iGaugeParameterControl control)
        {
            if (_gaugeControlList.Contains(control)) return;
            _gaugeControlList.Add(control);
            if (CurrentProductParam != null && CurrentProductParam.UseGauge != "" && _Gauges.ContainsKey(CurrentProductParam.UseGauge))
                control.Gauge = _Gauges[CurrentProductParam.UseGauge];
        }
        public event EventHandler SaveParaEvent;
    }
    public class VacuumSetting
    {
        private VacuumSettingDisp _disp;
        public bool VacuumBackEnable = true;
        public bool VacuumCentEnable = true;
        public bool VacuumFrontEnable = true;
        [XmlIgnore]
        public VacuumSettingDisp SettingForm
        {
            get { return _disp; }
            set
            {
                _disp = value;
                if (_disp != null)
                {
                    _disp.VacuumBackEnable = VacuumBackEnable;
                    _disp.VacuumCentEnable = VacuumCentEnable;
                    _disp.VacuumFrontEnable = VacuumFrontEnable;
                }
            }
        }
        public void GetData()
        {
            if (_disp != null)
            {
                VacuumBackEnable = _disp.VacuumBackEnable;
                VacuumCentEnable = _disp.VacuumCentEnable;
                VacuumFrontEnable = _disp.VacuumFrontEnable;
            }
        }
    }
    public class ClassProdParameter : ClassParameter
    {
        #region 参数
        private string _ProdName = "";
        public string ProductName
        {
            get { return _ProdName; }
        }
        public VacuumSetting VacuumLoadPNP = new VacuumSetting();
        public VacuumSetting VacuumTransPNPLoad = new VacuumSetting();
        public VacuumSetting VacuumTransPNPUnload = new VacuumSetting();
        public VacuumSetting VacuumUnloadPNP = new VacuumSetting();
        public VacuumSetting VacuumSortingPNP = new VacuumSetting();
        public bool BackSideUp = false;
        public double TopHeight = 5;
        public double TopClampWidth = 10;
        public bool ClampDisable = true;
        public double TopSealHeight = 0;
        public Cst.Struct_MeasDatas CellDataSpec = new Cst.Struct_MeasDatas();
        public string UseGauge = "";
        #endregion 参数
        //#region 标定参数
        //public double RefThickness;
        //public double ThicknessMeasRefLeft = 0;
        //public double ThicknessMeasRefMid = 0;
        //public double ThicknessMeasRefRight = 0;
        //#endregion 标定参数
        public int MeasAmount = 10;
        protected override void Clone(ClassParameter source)
        {
            ClassProdParameter ProdParaInterface = (ClassProdParameter)source;
            VacuumLoadPNP = ProdParaInterface.VacuumLoadPNP;
            VacuumTransPNPLoad = ProdParaInterface.VacuumTransPNPLoad;
            VacuumTransPNPUnload = ProdParaInterface.VacuumTransPNPUnload;
            VacuumUnloadPNP = ProdParaInterface.VacuumUnloadPNP;
            VacuumSortingPNP = ProdParaInterface.VacuumSortingPNP;
            BackSideUp = ProdParaInterface.BackSideUp;
            TopSealHeight = ProdParaInterface.TopSealHeight;
            TopHeight = ProdParaInterface.TopHeight;
            TopClampWidth = ProdParaInterface.TopClampWidth;
            ClampDisable = ProdParaInterface.ClampDisable;
            CellDataSpec = ProdParaInterface.CellDataSpec;
            CellDataSpec.MeasDataInit();

            //RefThickness = ProdParaInterface.RefThickness;
            //ThicknessMeasRefLeft = ProdParaInterface.ThicknessMeasRefLeft;
            //ThicknessMeasRefMid = ProdParaInterface.ThicknessMeasRefMid;
            //ThicknessMeasRefRight = ProdParaInterface.ThicknessMeasRefRight;
            MeasAmount = ProdParaInterface.MeasAmount;
            UseGauge = ProdParaInterface.UseGauge;
        }
        public override void GetDataFromInterface(IParameterControl control)
        {
            base.GetDataFromInterface(control);
            iProdParameterControl ProdParaInterface = (iProdParameterControl)control;
            _ProdName = ProdParaInterface.ProductName;
            VacuumLoadPNP = ProdParaInterface.VacuumLoadPNP;
            VacuumTransPNPLoad = ProdParaInterface.VacuumTransPNPLoad;
            VacuumTransPNPUnload = ProdParaInterface.VacuumTransPNPUnload;
            VacuumUnloadPNP = ProdParaInterface.VacuumUnloadPNP;
            VacuumSortingPNP = ProdParaInterface.VacuumSortingPNP;
            BackSideUp = ProdParaInterface.BackSideUp;
            TopSealHeight = ProdParaInterface.TopSealHeight;
            TopHeight = ProdParaInterface.TopHeight;
            TopClampWidth = ProdParaInterface.TopClampWidth;
            ClampDisable = ProdParaInterface.ClampDisable;
            CellDataSpec = ProdParaInterface.ProdCellDataSpec;

            //RefThickness = ProdParaInterface.RefThickness;
            //ThicknessMeasRefLeft = ProdParaInterface.ThicknessMeasRefLeft;
            //ThicknessMeasRefMid = ProdParaInterface.ThicknessMeasRefMid;
            //ThicknessMeasRefRight = ProdParaInterface.ThicknessMeasRefRight;
            MeasAmount = ProdParaInterface.MeasAmount;
            UseGauge = ProdParaInterface.UseGauge;
        }
        public override void SetDataToInterface(IParameterControl control)
        {
            base.SetDataToInterface(control);
            iProdParameterControl ProdParaInterface = (iProdParameterControl)control;
            ProdParaInterface.ProductName = ProductName;
            ProdParaInterface.VacuumLoadPNP = VacuumLoadPNP;
            ProdParaInterface.VacuumTransPNPLoad = VacuumTransPNPLoad;
            ProdParaInterface.VacuumTransPNPUnload = VacuumTransPNPUnload;
            ProdParaInterface.VacuumUnloadPNP = VacuumUnloadPNP;
            ProdParaInterface.VacuumSortingPNP = VacuumSortingPNP;
            ProdParaInterface.BackSideUp = BackSideUp;
            ProdParaInterface.TopSealHeight = TopSealHeight;
            ProdParaInterface.TopHeight = TopHeight;
            ProdParaInterface.TopClampWidth = TopClampWidth;
            ProdParaInterface.ClampDisable = ClampDisable;
            ProdParaInterface.ProdCellDataSpec = CellDataSpec;

            //ProdParaInterface.RefThickness = RefThickness;
            //ProdParaInterface.ThicknessMeasRefLeft = ThicknessMeasRefLeft;
            //ProdParaInterface.ThicknessMeasRefMid = ThicknessMeasRefMid;
            //ProdParaInterface.ThicknessMeasRefRight = ThicknessMeasRefRight;
            ProdParaInterface.MeasAmount = MeasAmount;
            ProdParaInterface.UseGauge = UseGauge;
        }
        private ClassProdParameter() : base("ProdParameter") { CellDataSpec.MeasDataInit(); }
        public ClassProdParameter(string ProdName) : base("ProdParameter" + ProdName)
        {
            _ProdName = ProdName;
        }
        [XmlIgnore]
        private static Cst.Struct_VParam m_VParam = new Cst.Struct_VParam();
        public static void ReadWriteProdVisionParam(String productname, bool bRead = true)
        {
            if (productname != "")
            {
                string ParamFile = "VisionParam" + productname + ".xml";

                if (!bRead)
                {
                    new SerialXML().SaveSetting(ParamFile, "VisionParam" + productname, m_VParam, FileMode.Create);
                }
                else
                {
                    object temp = new SerialXML().ReadSetting(ParamFile, "VisionParam" + productname, typeof(Cst.Struct_VParam));
                    if (temp != null) m_VParam = (Cst.Struct_VParam)temp;
                }
            }
        }
        public void SetGaugeValue(ClassGaugeParameter gauge)
        {
            CellDataSpec.CellThickness.SetGaugeValue(gauge[GaugeParaName.厚度]);
            CellDataSpec.CellWidth.SetGaugeValue(gauge[GaugeParaName.宽度]);
            CellDataSpec.CellLength.SetGaugeValue(gauge[GaugeParaName.长度]);
            CellDataSpec.NiTabDistance.SetGaugeValue(gauge[GaugeParaName.NiTab边距]);
            CellDataSpec.AlTabDistance.SetGaugeValue(gauge[GaugeParaName.AlTab边距]);
            CellDataSpec.NiTabDistanceMax.SetGaugeValue(gauge[GaugeParaName.NiTab边距]);
            CellDataSpec.AlTabDistanceMax.SetGaugeValue(gauge[GaugeParaName.AlTab边距]);
            CellDataSpec.NiTabLength.SetGaugeValue(gauge[GaugeParaName.NiTab长度]);
            CellDataSpec.AlTabLength.SetGaugeValue(gauge[GaugeParaName.AlTab长度]);
            CellDataSpec.NiSealantHeight.SetGaugeValue(gauge[GaugeParaName.NiSealant高度]);
            CellDataSpec.AlSealantHeight.SetGaugeValue(gauge[GaugeParaName.AlSealatnt高度]);
            CellDataSpec.TabDistance.SetGaugeValue(gauge[GaugeParaName.Tab间距]);
            CellDataSpec.ShoulderWidth.SetGaugeValue(gauge[GaugeParaName.肩宽]);
        }
    }
    public enum GaugeColName
    {
        参数,
        大,
        中,
        小,
    }
    public enum GaugeParaName
    {
        厚度 = EnumDataName.厚度,
        宽度 = EnumDataName.宽度,
        长度 = EnumDataName.长度,
        NiTab边距 = EnumDataName.NiTab边距,
        AlTab边距 = EnumDataName.AlTab边距,
        NiTab长度 = EnumDataName.NiTab长度,
        AlTab长度 = EnumDataName.AlTab长度,
        NiSealant高度 = EnumDataName.NiSealant高度,
        AlSealatnt高度 = EnumDataName.AlSealant高度,
        Tab间距 = EnumDataName.Tab间距,
        肩宽 = EnumDataName.肩宽,
    }
    public class ClassGaugeParameter : DataTable
    {
        #region 标定参数
        public double RefThickness;
        public double ThicknessMeasRefLeft = 0;
        public double ThicknessMeasRefMid = 0;
        public double ThicknessMeasRefRight = 0;
        public LinearPara ThicknessLeftLinear;
        public LinearPara ThicknessMidLinear;
        public LinearPara ThicknessRightLinear;
        public LinearPara CCDXLinear;
        public LinearPara CCDYLinear;

        #endregion 标定参数
        //无参数构造函数的目的是因为Copy()函数中执行的是DataTable.Copy()，这个操作会生成DataTable的新实例，但是执行的构造函数是无参数构造函数。
        //如果继承了DataTable的ClassGaugeParameter不建立一个无参数构造函数，则执行Copy()时会出现运行时错误，提示没有无参数构造函数。
        private ClassGaugeParameter() { }
        public ClassGaugeParameter(string GaugeName) : base(GaugeName)
        {
            ThicknessLeftLinear.Slope = 1; ThicknessLeftLinear.Intercept = 0; ThicknessLeftLinear.Enable = false;
            ThicknessMidLinear.Slope = 1; ThicknessMidLinear.Intercept = 0; ThicknessMidLinear.Enable = false;
            ThicknessRightLinear.Slope = 1; ThicknessRightLinear.Intercept = 0; ThicknessRightLinear.Enable = false;
            CCDXLinear.Slope = 1; CCDXLinear.Intercept = 0; CCDXLinear.Enable = true;
            CCDYLinear.Slope = 1; CCDYLinear.Intercept = 0; CCDYLinear.Enable = true;
        }
        public ClassGaugeParameter Copy()
        {
            return (ClassGaugeParameter)base.Copy();
        }
        public Cst.GaugeValue this[GaugeParaName para]
        {
            get
            {
                Cst.GaugeValue value = new Cst.GaugeValue();
                foreach (DataRow row in this.Rows)
                    if (row[GaugeColName.参数.ToString()].ToString() == para.ToString())
                    {
                        value.MAX = double.Parse(row[GaugeColName.大.ToString()].ToString());
                        value.AVG = double.Parse(row[GaugeColName.中.ToString()].ToString());
                        value.MIN = double.Parse(row[GaugeColName.小.ToString()].ToString());
                        break;
                    }
                return value;
            }
        }
        public void SaveParameter()
        {
            WriteXml($"{CommonFunction.DefaultConfigPath}Gauge{TableName}.xml", XmlWriteMode.WriteSchema);
            SimpleElement root = new SimpleElement("GaugePara");
            SimpleElement para;

            para = new SimpleElement("RefThickness"); para.Text = RefThickness.ToString(); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessRefLeft"); para.Text = ThicknessMeasRefLeft.ToString(); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessRefMid"); para.Text = ThicknessMeasRefMid.ToString(); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessRefRight"); para.Text = ThicknessMeasRefRight.ToString(); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessLeftLinear"); para.Attributes.Add("Slope", ThicknessLeftLinear.Slope.ToString()); para.Attributes.Add("Intercept", ThicknessLeftLinear.Intercept.ToString()); para.Attributes.Add("Enable", ThicknessLeftLinear.Enable.ToString()); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessMidLinear"); para.Attributes.Add("Slope", ThicknessMidLinear.Slope.ToString()); para.Attributes.Add("Intercept", ThicknessMidLinear.Intercept.ToString()); para.Attributes.Add("Enable", ThicknessMidLinear.Enable.ToString()); root.ChildElements.Add(para);
            para = new SimpleElement("ThicknessRightLinear"); para.Attributes.Add("Slope", ThicknessRightLinear.Slope.ToString()); para.Attributes.Add("Intercept", ThicknessRightLinear.Intercept.ToString()); para.Attributes.Add("Enable", ThicknessRightLinear.Enable.ToString()); root.ChildElements.Add(para);
            para = new SimpleElement("CCDXLinear"); para.Attributes.Add("Slope", CCDXLinear.Slope.ToString()); para.Attributes.Add("Intercept", CCDXLinear.Intercept.ToString()); para.Attributes.Add("Enable", CCDXLinear.Enable.ToString()); root.ChildElements.Add(para);
            para = new SimpleElement("CCDYLinear"); para.Attributes.Add("Slope", CCDYLinear.Slope.ToString()); para.Attributes.Add("Intercept", CCDYLinear.Intercept.ToString()); para.Attributes.Add("Enable", CCDYLinear.Enable.ToString()); root.ChildElements.Add(para);
            XmlDomUtil.writeXMLtoDefulatPathConfigFile(root, 3, $"GPara{TableName}.xml");
        }
        public void LoadParameter()
        {
            ReadXml($"{CommonFunction.DefaultConfigPath}Gauge{TableName}.xml");
            if (File.Exists($"{CommonFunction.DefaultConfigPath}GPara{TableName}.xml"))
            {
                SimpleElement root = XmlDomUtil.readDefaultPathConfigFile($"GPara{TableName}.xml");
                RefThickness = double.Parse(root.ChildElements["RefThickness"].Text);
                ThicknessMeasRefLeft = double.Parse(root.ChildElements["ThicknessRefLeft"].Text);
                ThicknessMeasRefMid = double.Parse(root.ChildElements["ThicknessRefMid"].Text);
                ThicknessMeasRefRight = double.Parse(root.ChildElements["ThicknessRefRight"].Text);
                ThicknessLeftLinear.Slope = double.Parse(root.ChildElements["ThicknessLeftLinear"].Attributes["Slope"]);
                ThicknessLeftLinear.Intercept = double.Parse(root.ChildElements["ThicknessLeftLinear"].Attributes["Intercept"]);
                ThicknessLeftLinear.Enable = bool.Parse(root.ChildElements["ThicknessLeftLinear"].Attributes["Enable"]);
                ThicknessMidLinear.Slope = double.Parse(root.ChildElements["ThicknessMidLinear"].Attributes["Slope"]);
                ThicknessMidLinear.Intercept = double.Parse(root.ChildElements["ThicknessMidLinear"].Attributes["Intercept"]);
                ThicknessMidLinear.Enable = bool.Parse(root.ChildElements["ThicknessMidLinear"].Attributes["Enable"]);
                ThicknessRightLinear.Slope = double.Parse(root.ChildElements["ThicknessRightLinear"].Attributes["Slope"]);
                ThicknessRightLinear.Intercept = double.Parse(root.ChildElements["ThicknessRightLinear"].Attributes["Intercept"]);
                ThicknessRightLinear.Enable = bool.Parse(root.ChildElements["ThicknessRightLinear"].Attributes["Enable"]);
                CCDXLinear.Slope = double.Parse(root.ChildElements["CCDXLinear"].Attributes["Slope"]);
                CCDXLinear.Intercept = double.Parse(root.ChildElements["CCDXLinear"].Attributes["Intercept"]);
                CCDXLinear.Enable = bool.Parse(root.ChildElements["CCDXLinear"].Attributes["Enable"]);
                CCDYLinear.Slope = double.Parse(root.ChildElements["CCDYLinear"].Attributes["Slope"]);
                CCDYLinear.Intercept = double.Parse(root.ChildElements["CCDYLinear"].Attributes["Intercept"]);
                CCDYLinear.Enable = bool.Parse(root.ChildElements["CCDYLinear"].Attributes["Enable"]);
            }
        }
    }
}