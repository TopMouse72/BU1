using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Measure;

namespace CDI
{
    #region Parameter
    public interface iProdParameterControl : IParameterControl
    {
        string ProductName { get; set; }
        bool VacuumBackEnable { get; set; }
        bool VacuumCentEnable { get; set; }
        bool VacuumFrontEnable { get; set; }
        bool BackSideUp { get; set; }
        double TopSealHeight { get; set; }
        double TopHeight { get; set; }
        double TopClampWidth { get; set; }
        Cst.Struct_MeasDatas ProdCellDataSpec { get; set; }

        double RefThickness { get; set; }
        double ThicknessMeasRefLeft { get; set; }
        double ThicknessMeasRefMid { get; set; }
        double ThicknessMeasRefRight { get; set; }
        int MeasAmount { get; set; }
        string UseGauge { get; set; }
    }
    public interface ISystemParameterControl : IParameterControl
    {
        int VacuumDelayTime { get; set; }
        int ThicknessMeasDelayTime { get; set; }
        string CurrentProduct { get; set; }
        Dictionary<string, ClassProdParameter> Products { get; set; }
        bool LoadInSMEMAIgnored { get; set; }
        bool UnloadOutSMEMAIgnored { get; set; }
    }
    #endregion Parameter
    #region Interface
    public interface IDataDisp
    {
        ClassDataStation DataStation { get; set; }
        void RefreshData();
    }
    #endregion Interface
}
