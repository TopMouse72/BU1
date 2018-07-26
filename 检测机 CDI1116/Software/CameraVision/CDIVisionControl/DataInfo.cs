using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.XML;
using Colibri.CommonModule.State;
using Measure;

namespace CDI
{
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
                disp.RefreshData();
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
        public ClassDataInfo(int no, Cst.Struct_MeasDatas spec)
        {
            _index = no;
            Data = spec;
            Data.Clear();
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
        #region 参数
        private string _CurrentProduct = "";
        private Dictionary<string, ClassProdParameter> _Products = new Dictionary<string, ClassProdParameter>();
        private int _VacuumDelayTime = 500;
        private int _ThicknessMeasDelayTime = 500;
        private bool _LoadInSMEMAIgnored = true;
        private bool _UnloadOutSMEMAIgnored = true;
        private string _CCDSavePath = CommonFunction.DataPath + "Pic\\";
        public string CCDSavePath
        {
            get { return _CCDSavePath; }
            //set { _CCDSavePath = value; }
        }
        private string datapath = CommonFunction.DataPath + "data\\";
        public string DataSavePath
        {
            get { return datapath; }
        }
        public string CurrentProduct
        {
            get { return _CurrentProduct; }
            set
            {
                if (_CurrentProduct != value && Products.ContainsKey(value))
                    if (_CurrentProduct != "" && _Products[_CurrentProduct] != null && _Products[value] != null)
                        _Products[_CurrentProduct].MoveAllInterfaceTo(_Products[value]);
                _CurrentProduct = value;
                CommonFunction.SysPublisher.notifyProductChangeEventSubscribers(this, new StateEventArgs(_CurrentProduct, ""));
            }
        }
        [XmlIgnore]
        public ClassProdParameter CurrentProductParam
        {
            get
            {
                if (CurrentProduct != "")
                    return _Products[CurrentProduct];
                else
                    return new ClassProdParameter("Default");
            }
        }
        [XmlIgnore]
        public Dictionary<string, ClassProdParameter> Products { get { return _Products; } set { _Products = value; } }
        public int VacuumDelayTime { get { return _VacuumDelayTime; } set { _VacuumDelayTime = value; } }
        public int ThicknessMeasDelayTime { get { return _ThicknessMeasDelayTime; } set { _ThicknessMeasDelayTime = value; } }
        public bool LoadInSMEMAIgnored { get { return _LoadInSMEMAIgnored; } set { _LoadInSMEMAIgnored = value; } }
        public bool UnloadOutSMEMAIgnored { get { return _UnloadOutSMEMAIgnored; } set { _UnloadOutSMEMAIgnored = value; } }
        #endregion 参数
        protected override void Clone(ClassParameter source)
        {
            ClassSystemParameter SysParaInterface = (ClassSystemParameter)source;
            VacuumDelayTime = SysParaInterface.VacuumDelayTime;
            ThicknessMeasDelayTime = SysParaInterface.ThicknessMeasDelayTime;
            Products = SysParaInterface.Products;
            CurrentProduct = SysParaInterface.CurrentProduct;
            LoadInSMEMAIgnored = SysParaInterface.LoadInSMEMAIgnored;
            UnloadOutSMEMAIgnored = SysParaInterface.UnloadOutSMEMAIgnored;
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
        }
        public override void LoadParameter()
        {
            base.LoadParameter();
            GetProductList();
        }
        public override void SaveParameter()
        {
            base.SaveParameter();
            foreach (ClassProdParameter prod in _Products.Values)
                prod.SaveParameter();
        }
        public override void GetDataFromInterface(IParameterControl control)
        {
            base.GetDataFromInterface(control);
            ISystemParameterControl SysParaInterface = (ISystemParameterControl)control;
            VacuumDelayTime = SysParaInterface.VacuumDelayTime;
            ThicknessMeasDelayTime = SysParaInterface.ThicknessMeasDelayTime;
            Products = SysParaInterface.Products;
            CurrentProduct = SysParaInterface.CurrentProduct;
            LoadInSMEMAIgnored = SysParaInterface.LoadInSMEMAIgnored;
            UnloadOutSMEMAIgnored = SysParaInterface.UnloadOutSMEMAIgnored;
        }
        public override void SetDataToInterface(IParameterControl control)
        {
            base.SetDataToInterface(control);
            ISystemParameterControl SysParaInterface = (ISystemParameterControl)control;
            SysParaInterface.VacuumDelayTime = VacuumDelayTime;
            SysParaInterface.ThicknessMeasDelayTime = ThicknessMeasDelayTime;
            SysParaInterface.Products = Products;
            SysParaInterface.CurrentProduct = CurrentProduct;
            SysParaInterface.LoadInSMEMAIgnored = LoadInSMEMAIgnored;
            SysParaInterface.UnloadOutSMEMAIgnored = UnloadOutSMEMAIgnored;
        }
        public ClassSystemParameter() : base("SystemParameter")
        {

        }

        event EventHandler IParameterControl.SaveParaEvent
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
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
        public bool BackSideUp = false;
        public double TopHeight = 5;
        public double TopClampWidth = 10;
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
            BackSideUp = ProdParaInterface.BackSideUp;
            TopSealHeight = ProdParaInterface.TopSealHeight;
            TopHeight = ProdParaInterface.TopHeight;
            TopClampWidth = ProdParaInterface.TopClampWidth;
            CellDataSpec = ProdParaInterface.CellDataSpec;

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
            BackSideUp = ProdParaInterface.BackSideUp;
            TopSealHeight = ProdParaInterface.TopSealHeight;
            TopHeight = ProdParaInterface.TopHeight;
            TopClampWidth = ProdParaInterface.TopClampWidth;
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
            ProdParaInterface.BackSideUp = BackSideUp;
            ProdParaInterface.TopSealHeight = TopSealHeight;
            ProdParaInterface.TopHeight = TopHeight;
            ProdParaInterface.TopClampWidth = TopClampWidth;
            ProdParaInterface.ProdCellDataSpec = CellDataSpec;

            //ProdParaInterface.RefThickness = RefThickness;
            //ProdParaInterface.ThicknessMeasRefLeft = ThicknessMeasRefLeft;
            //ProdParaInterface.ThicknessMeasRefMid = ThicknessMeasRefMid;
            //ProdParaInterface.ThicknessMeasRefRight = ThicknessMeasRefRight;
            ProdParaInterface.MeasAmount = MeasAmount;
            ProdParaInterface.UseGauge = UseGauge;
        }
        private ClassProdParameter() : base("ProdParameter") { }
        public ClassProdParameter(string ProdName) : base("ProdParameter" + ProdName)
        {
            _ProdName = ProdName;
        }
        [XmlIgnore]
        private static Cst.Struct_VParam m_VParam = new Cst.Struct_VParam();
        [XmlIgnore]
        private static Cst.Struct_VParam m_VGaugeParam = new Cst.Struct_VParam();
        public static void ReadWriteProdVisionParam(String productname, string gaugename, bool bRead = true)
        {
            if (productname != "")
            {
                string ParamFile = $"VisionParam{productname}.xml";
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
            if (gaugename != "")
            {
                string GaugeFile = $"VisionGauge{gaugename}.xml";
                if (!bRead)
                {
                    new SerialXML().SaveSetting(GaugeFile, "VisionGauge" + gaugename, m_VGaugeParam, FileMode.Create);
                }
                else
                {
                    object temp = new SerialXML().ReadSetting(GaugeFile, "VisionGauge" + gaugename, typeof(Cst.Struct_VParam));
                    if (temp != null) m_VGaugeParam = (Cst.Struct_VParam)temp;
                }
            }
        }
    }
}