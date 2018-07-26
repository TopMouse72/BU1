using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using Colibri.CommonModule.MotionSystem;
using HardwarePool;
using Colibri.CommonModule.Forms;
using CDI.GUI;
using CDI.StateMachine;
using System.Windows.Forms;

namespace CDI.Zone
{
    public class ClassZone上料传送 : ClassBaseWorkZone
    {
        public enum EnumPointConveyor
        {
            CellPitch,
            BackDistance,
        }
        public enum EnumAxisName
        {
            LoadInConveyor = HardwareAxisName.LoadInConveyor,//上料传送皮带轴
        }
        /// <summary>
        /// 输入端口名称枚举
        /// </summary>
        public enum EnumInportName
        {
            LoadInConvLoad = HardwareInportName.LoadInConvLoad,//上料传送来料
            LoadInConvInPosRight = HardwareInportName.LoadInConvInPosRight,//上料传送到位右
            LoadInConvInPosMid = HardwareInportName.LoadInConvInPosMid,//上料传送到位中
            LoadInConvInPosLeft = HardwareInportName.LoadInConvInPosLeft,//上料传送到位左
            LoadInSMEMALoadReady = HardwareInportName.LoadInSMEMALoadReady,//外框SMEMA上料Ready
        }
        /// <summary>
        /// 输出端口名称枚举
        /// </summary>
        public enum EnumOutportName
        {
            LoadInSMEMALoadAvailable = HardwareOutportName.LoadInSMEMALoadAvailable,//外框SMEMA上料Available
        }
        public enum EnumSerialPortName
        {
            LoadInBarcode = HardwareSerialPortName.LoadInBarcode,//上料扫码串口
        }
        //private bool IsManualReady = false;
        public CAxisBase AxisLoadInConveyor;
        public ClassZone上料传送() : base(EnumZoneName.Zone上料传送.ToString())
        {
            for (int i = 0; i < LoadInDataStations.Length; i++)
                LoadInDataStations[i] = new ClassDataStation("电芯" + (i + 1).ToString());
        }
        public override void ZoneInit()
        {
            base.ZoneInit();
            AssignHardware(typeof(EnumAxisName), typeof(EnumInportName), typeof(EnumOutportName), typeof(EnumSerialPortName));
            AxisLoadInConveyor = ThisAxis(EnumAxisName.LoadInConveyor); AxisLoadInConveyor.AddPoints(typeof(EnumPointConveyor));
            ZoneSettingPanel = new SettingPanelZone上料传送();
            ZoneManualPanel = new ManualPanelZone上料传送();
        }
        protected override void Reset(ClassErrorHandle err)
        {
            AxisLoadInConveyor.ServoOn = true;
            AxisLoadInConveyor.HomeFinish = true;
            AxisLoadInConveyor.StopMove();
            if (ThisInport(EnumInportName.LoadInConvInPosRight).status || ThisInport(EnumInportName.LoadInConvInPosMid).status || ThisInport(EnumInportName.LoadInConvInPosLeft).status)
                err.CollectErrInfo("传送带取料位置有物料需要移除。");
            for (int i = 0; i < 6; i++)
                LoadInDataStations[i].CellData = null;
        }
        #region Event
        protected override void InPortActive(string inPort)
        {
        }
        protected override void InPortDeActive(string inPort)
        {
        }
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            ThisOutport(EnumOutportName.LoadInSMEMALoadAvailable).SetOutput(false);
            base.StartHandler(sender, e);
            if (ThisInport(EnumInportName.LoadInConvInPosRight).status || ThisInport(EnumInportName.LoadInConvInPosMid).status || ThisInport(EnumInportName.LoadInConvInPosLeft).status)
                ClassErrorHandle.ShowError(this.Name, "传送带取料位置有物料需要移除。", ErrorLevel.Error);
            else
            {
                for (int i = 0; i < LoadInDataStations.Length; i++)
                    LoadInDataStations[i].CellData = null;
                ClassWorkFlow.Instance.GetCell = 0;
            }
        }
        //public override void StopHandler(BaseClass sender, StateEventArgs e)
        //{
        //    base.StopHandler(sender, e);
        //    //ActionSMEMAAvaliable(false);
        //}
        public override void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            base.PauseHandler(sender, e);
            pausedAvaliable = SMEMAAvailable;
            //ActionSMEMAAvaliable(false);
            ActionSMEMAAvailable(false);
        }
        //public override void ResumeHandler(BaseClass sender, StateEventArgs e)
        //{
        //    base.ResumeHandler(sender, e);
        //}
        #endregion Event
        #region Data
        public ClassDataStation[] LoadInDataStations = new ClassDataStation[6];
        public string GetDataInfoString()
        {
            return GetDataInfoString(LoadInDataStations);
        }
        public void AddDisp(IDataDisp disp6, IDataDisp disp5, IDataDisp disp4, IDataDisp disp3, IDataDisp disp2, IDataDisp disp1)
        {
            LoadInDataStations[0].AddDisp(disp1);
            LoadInDataStations[1].AddDisp(disp2);
            LoadInDataStations[2].AddDisp(disp3);
            LoadInDataStations[3].AddDisp(disp4);
            LoadInDataStations[4].AddDisp(disp5);
            LoadInDataStations[5].AddDisp(disp6);
        }
        #endregion Data
        #region Action
        public bool HavePart()
        {
            bool need = false;
            need |= ThisInport(EnumInportName.LoadInConvInPosRight).status;
            need |= ThisInport(EnumInportName.LoadInConvInPosMid).status;
            need |= ThisInport(EnumInportName.LoadInConvInPosLeft).status;
            for (int i = 1; i < 5; i++)
                need |= LoadInDataStations[i].CellData != null;
            need |= ThisInport(EnumInportName.LoadInConvLoad).status;
            return need;
        }
        public string StartBarcodeScan(bool HavePart = false)
        {
            string res = "";
            string barcode = "";
            if (HavePart)
            {
                res = StartSerialReading(ThisSerialPortData(EnumSerialPortName.LoadInBarcode), HardwareSerialProtocolName.BarcodeRead, ref barcode);
                if (res != "")
                {
                    barcode = "";
                    ThisSerialPortData(EnumSerialPortName.LoadInBarcode).Stop();
                }
                else
                    barcode = barcode.Trim();
            }
            else
            {
                if (GRRStartOffset < 0)
                    barcode = ClassWorkFlow.Instance.GetCell.ToString();
                else
                    barcode = (((ClassWorkFlow.Instance.GetCell + GRRStartOffset - 1) % 10) + 1).ToString();
            }
            if (LoadInDataStations[LoadInDataStations.Length - 1].CellData != null)
                LoadInDataStations[LoadInDataStations.Length - 1].CellData.Barcode = barcode;
            if (barcode != "")
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, this.Name, "获取条码" + barcode);
            else
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, this.Name, "获取条码失败(" + res + ")");
            return res;
        }
        /// <summary>
        /// 检测传送带是否可以传送物料。条件是SMEMA的ready信号为true，并且InPos传感器没有检测到物料。手动模式下，或是SMEMA忽略的情况下，SMEMA相关信号忽略。
        /// </summary>
        /// <returns></returns>
        public ErrorInfoWithPause CheckLoadReady()
        {
            ErrorInfoWithPause res = null;
            bool isBusy;
            //1. 检查到位传感器是否有料或是否有数据（空跑）
            isBusy = ThisInport(EnumInportName.LoadInConvInPosRight).status;
            isBusy |= LoadInDataStations[0].CellData != null;
            if (isBusy)
            {
                //如果右边有物料（没有被取走）
                //则设置非空结果
                res = new ErrorInfoWithPause("右电芯处有物料没有被取走。", ErrorLevel.Notice);
            }
            else
                //没有则设置空结果
                res = null;

            //2. 检查上料传感器是否触发
            if (ClassWorkFlow.Instance.FeedNewPart)
            {
                //没有停止上料
                if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                {
                    //非空跑
                    if (!ThisInport(EnumInportName.LoadInConvLoad).status)
                    {
                        //没有上料
                        //则设置非空结果
                        //Set Avaliable true to let MDM to feed new part.
                        res = new ErrorInfoWithPause("左边没有放物料", ErrorLevel.Notice);
                        if (_isRunning)
                            ActionSMEMAAvailable(true);
                    }
                    else
                    {
                        //有上料，返回isBusy结果
                        //Part loaded.
                        //Set Avaliable false to stop MDM feeding part.
                        ActionSMEMAAvailable(false);
                    }
                }
                //空跑则返回结果不变
            }
            else
            {
                //停止上料
                //Set Avaliable false.
                ActionSMEMAAvailable(false);
                if (!HavePart())
                {
                    //No any parts on conveyor, set none empty result.
                    res = new ErrorInfoWithPause("停止上料中", ErrorLevel.Notice);
                }
                //else if have parts then keep the result no change.
            }
            return res;
        }
        public bool BarcodeEnabled = true;
        public int GRRStartOffset = -1;
        /// <summary>
        /// 开始传送准备。
        /// 开始的条件是SMEMA的ready信号为true，并且InPos传感器没有检测到物料。
        /// 传送带运行，
        /// SMEMA avaliable为false。
        /// </summary>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public ErrorInfoWithPause ActionStartLoad(CallBackCommonFunc AfterActionLoadNew)
        {
            ErrorInfoWithPause res = null;
            string barcodeReadRes;
            ////Go back to align the new cell.
            AxisLoadInConveyor.SetZero();
            bool havenewpart = ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑;// && ThisInport(EnumInportName.LoadInConvLoad).status;
            double offsetTemp = 0;
            if (ClassWorkFlow.Instance.LoadMode == EnumLoadMode.自动)
            {
                offsetTemp = ClassCommonSetting.SysParam.CurrentProductParam.CellDataSpec.CellWidth.Mean - ClassCommonSetting.SysParam.Products[CALIBPROD].CellDataSpec.CellWidth.Mean;
                offsetTemp /= 2;
                offsetTemp += ThisAxis(EnumAxisName.LoadInConveyor).PointList[EnumPointConveyor.BackDistance].Position;
                while (!ThisAxis(EnumAxisName.LoadInConveyor).MoveTo(offsetTemp))
                //return DispMotionError(AxisLoadInConveyor, "回移");
                {
                    res = DispMotionError(AxisLoadInConveyor, "回移");
                    if (res != null) return res;
                }
            }
            if ((havenewpart || ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) && ClassWorkFlow.Instance.FeedNewPart)
                LoadInDataStations[LoadInDataStations.Length - 1].CellData = ClassDataInfo.NewCellData(++ClassWorkFlow.Instance.GetCell);
            else
                LoadInDataStations[LoadInDataStations.Length - 1].CellData = null;
            //Go to next pitch position and start scan barcode
            AxisLoadInConveyor.SetZero();
            while (!AxisLoadInConveyor.MoveTo(EnumPointConveyor.CellPitch, true, -offsetTemp))
            //return DispMotionError(AxisLoadInConveyor, "进料移动");
            {
                res = DispMotionError(AxisLoadInConveyor, "进料移动");
                if (res != null) return res;
            }
            ////If PNP can pick
            //if (ThisInport(EnumInportName.LoadInConvInPos).status)
            //{
            //    NotifyDoneEvent(EnumEventName.NewCellLoad);
            //}
            if (LoadInDataStations[LoadInDataStations.Length - 1].CellData != null)
            {
                havenewpart &= BarcodeEnabled;
                if (ClassWorkFlow.Instance.LoadMode == EnumLoadMode.自动)
                    barcodeReadRes = StartBarcodeScan(havenewpart);
                else
                    do
                    {
                        barcodeReadRes = StartBarcodeScan(havenewpart);
                        if (barcodeReadRes != "")
                            BaseForm.DoInvokeRequired(MainForm.instance,
                                () =>
                                {
                                    while (MessageBox.Show(MainForm.instance, $"条码枪扫码出错: {barcodeReadRes}。{Environment.NewLine}请检查电芯位置是否正确或者是否有电芯。{Environment.NewLine}" +
                                        "点“重试”重新扫条码。如果无电芯或需要移除电芯，则点“取消”放弃扫码。",
                                        "条码枪扫码", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                                    {
                                        if (MessageBox.Show(MainForm.instance, "确定要取消并移除电芯？", "取消电芯", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            LoadInDataStations[LoadInDataStations.Length - 1].CellData = null;
                                            break;
                                        }
                                    }
                                });
                    } while (LoadInDataStations[LoadInDataStations.Length - 1].CellData != null && barcodeReadRes != "");
                if (LoadInDataStations[LoadInDataStations.Length - 1].CellData != null)
                    ClassCommonSetting.ProgramLog(LogFile.Level.Notice, this.Name, "加载新物料，索引号" + ClassWorkFlow.Instance.GetCell.ToString() + " NG结果为" + (LoadInDataStations[LoadInDataStations.Length - 1].CellData.LoadNG ? "NG" : "OK"));
            }
            if (AfterActionLoadNew != null) AfterActionLoadNew();
            return null;
        }
        public ErrorInfoWithPause CheckLoadin()
        {
            ErrorInfoWithPause res = null;
            if (ClassWorkFlow.Instance.WorkMode != EnumWorkMode.空跑)
                if (ThisInport(EnumInportName.LoadInConvInPosRight).status || LoadInDataStations[0].CellData != null)
                    ClassCommonSetting.CheckTimeOut(() =>
                    {
                        res = CheckLoadSensor();
                        return res == null;
                    });
                    //for (int i = 0; i < 50; i++)
                    //{
                    //    System.Windows.Forms.Application.DoEvents();
                    //    res = CheckLoadSensor();
                    //    if (res != null)
                    //    {
                    //        System.Threading.Thread.Sleep(20);
                    //    }
                    //    else
                    //        break;
                    //}
            return res;
        }
        public void SetLoadin()
        {
            if (!ThisInport(EnumInportName.LoadInConvInPosLeft).status && LoadInDataStations[(int)EnumCellIndex.左电芯].CellData != null)
                LoadInDataStations[(int)EnumCellIndex.左电芯].CellData = null;
            if (!ThisInport(EnumInportName.LoadInConvInPosMid).status && LoadInDataStations[(int)EnumCellIndex.中电芯].CellData != null)
                LoadInDataStations[(int)EnumCellIndex.中电芯].CellData = null;
            if (!ThisInport(EnumInportName.LoadInConvInPosRight).status && LoadInDataStations[(int)EnumCellIndex.右电芯].CellData != null)
                LoadInDataStations[(int)EnumCellIndex.右电芯].CellData = null;
        }
        private ErrorInfoWithPause CheckLoadSensor()
        {
            string temp = "";
            if (ThisInport(EnumInportName.LoadInConvInPosLeft).status != (LoadInDataStations[(int)EnumCellIndex.左电芯].CellData != null))
                temp += " " + EnumCellIndex.左电芯.ToString();
            if (ThisInport(EnumInportName.LoadInConvInPosMid).status != (LoadInDataStations[(int)EnumCellIndex.中电芯].CellData != null))
                temp += " " + EnumCellIndex.中电芯.ToString();
            if (ThisInport(EnumInportName.LoadInConvInPosRight).status != (LoadInDataStations[(int)EnumCellIndex.右电芯].CellData != null))
                temp += " " + EnumCellIndex.右电芯.ToString();
            if (temp != "")
                return new ErrorInfoWithPause("上料电芯与数据不符:" + temp, ErrorLevel.Alarm, true, false);
            else
                return null;
        }
        private bool SMEMAAvailable = false;
        private bool pausedAvaliable;
        private object SMEMALock = new object();
        /// <summary>
        /// 设置SMEMA的Available信号。
        /// 流程没有运行，流程是空跑状态，或者流程运行但是暂停，Available信号始终设为false。
        /// </summary>
        /// <param name="Available">avaliable信号状态</param>
        /// <returns>返回执行结果。成功返回空字符。</returns>
        public ErrorInfoWithPause ActionSMEMAAvailable(bool Available)
        {
            lock (SMEMALock)
            {
                if (!_isRunning) Available = false;
                if (ClassWorkFlow.Instance.WorkMode == EnumWorkMode.空跑) Available = false;
                if (_isPausing) Available = false;
                ThisOutport(EnumOutportName.LoadInSMEMALoadAvailable).SetOutput(Available);
                if (SMEMAAvailable != Available)
                {
                    SMEMAAvailable = Available;
                    ClassCommonSetting.ProgramLog(LogFile.Level.Debug, this.Name, "SMEMA Available信号设为" + Available.ToString());
                }
                return null;
            }
        }
        #endregion Action
    }
}