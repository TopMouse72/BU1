using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.Remoting.Messaging;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Measure;
using CDI.Zone;

namespace CDI
{
    #region ZoneData
    public enum TimeUsageItem
    {
        ZoneCCD,
        ZoneThickness,
        ZoneTopAlign,
        ZoneTransPNP,
    }
    public interface ITimeUsage
    {
        void GetTimeUsage(TimeUsageItem item, double TotalTime, string message);
    }
    public class ClassTimeUsage
    {
        private delegate void UpdateTimeUsageHandler(TimeUsageItem item, double total, string message);
        private UpdateTimeUsageHandler _update;
        public ClassTimeUsage()
        {
            _update = update;
        }
        private List<ITimeUsage> _usage = new List<ITimeUsage>();
        public void AddTimeUsageGUI(ITimeUsage gui)
        {
            if (_usage.Contains(gui)) return;
            _usage.Add(gui);
        }
        public void RemoveTimeUsageGUI(ITimeUsage gui)
        {
            _usage.Remove(gui);
        }
        private void UpdateAsyncReturn(IAsyncResult result)
        {
            UpdateTimeUsageHandler handler = (UpdateTimeUsageHandler)((AsyncResult)result).AsyncDelegate;
            try
            {
                handler.EndInvoke(result);
                result.AsyncWaitHandle.Close();
            }
            catch (Exception e)
            {
                ClassCommonSetting.ThrowException(handler, "UpdateTimeUsage", e);
            }
        }
        public void UpdateTimeUsage(TimeUsageItem item, double total, string message)
        {
            _update.BeginInvoke(item, total, message, UpdateAsyncReturn, null);
        }
        private void update(TimeUsageItem item, double total, string message)
        {
            foreach (ITimeUsage gui in _usage)
                gui.GetTimeUsage(item, total, message);
        }
    }
    public interface IDataStatusGUI
    {
        void UpdateWorkFlowDataStatus(EnumDataTransfer transfer);
    }
    public interface IPPMUpdate
    {
        void ShowPPM(long count, double ppm, double lastppm, TimeSpan laps, TimeSpan singlelaps);
    }
    #endregion ZoneData
    #region Parameter
    public interface iProdParameterControl : IParameterControl
    {
        string ProductName { get; set; }
        VacuumSetting VacuumLoadPNP { get; set; }
        VacuumSetting VacuumTransPNPLoad { get; set; }
        VacuumSetting VacuumTransPNPUnload { get; set; }
        VacuumSetting VacuumUnloadPNP { get; set; }
        VacuumSetting VacuumSortingPNP { get; set; }
        bool BackSideUp { get; set; }
        double TopSealHeight { get; set; }
        double TopHeight { get; set; }
        double TopClampWidth { get; set; }
        bool ClampDisable { get; set; }
        Cst.Struct_MeasDatas ProdCellDataSpec { get; set; }

        //double RefThickness { get; set; }
        //double ThicknessMeasRefLeft { get; set; }
        //double ThicknessMeasRefMid { get; set; }
        //double ThicknessMeasRefRight { get; set; }
        int MeasAmount { get; set; }
        string UseGauge { get; set; }
    }
    public interface iGaugeParameterControl : IParameterControl
    {
        ClassGaugeParameter Gauge { set; }
    }
    public interface ISystemParameterControl : IParameterControl
    {
        int VacuumDelayTime { get; set; }
        int ManualLoadDelayTime { get; set; }
        int ThicknessMeasDelayTime { get; set; }
        string CurrentProduct { get; set; }
        Dictionary<string, ClassProdParameter> Products { get; set; }
        Dictionary<string, ClassGaugeParameter> Gauges { get; set; }
        bool LoadInSMEMAIgnored { get; set; }
        bool UnloadOutSMEMAIgnored { get; set; }
        int OdsSaveInterval { get; set; }
        string OdsSavePath { get; set; }
        LogOutItem LogOutTime { get; set; }
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