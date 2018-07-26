using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;
using CDI.Zone;
using CDI.StateMachine;
using Colibri.CommonModule.State;
using Measure;
using Colibri.CommonModule.Event;

namespace CDI.GUI
{
    public partial class AutoPanel : BasePanel, IDataStatusGUI, IPPMUpdate, /*ILogControl,*/ iProdParameterControl, IErrorSubscriber, ITimeUsage
    {
        private static AutoPanel _instance;
        public static AutoPanel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AutoPanel();
                return _instance;
            }
        }
        public ClassWorkZones zones;

        public event EventHandler SaveParaEvent;

        //public LogFile.Level LogLevel { get; set; }
        public object Owner { get; set; }
        public new string ProductName { get; set; }
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
        private Cst.Struct_MeasDatas _dataspec;
        public Cst.Struct_MeasDatas ProdCellDataSpec
        {
            get
            {
                return _dataspec;
            }
            set
            {
                _dataspec = value;
                dataGridViewAuto.Columns[ClassWorkZones.ColThickness].HeaderText = ClassWorkZones.ColThickness + (_dataspec.CellThickness.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColCellWidth].HeaderText = ClassWorkZones.ColCellWidth + (_dataspec.CellWidth.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColCellLength].HeaderText = ClassWorkZones.ColCellLength + (_dataspec.CellLength.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColAlTabDist].HeaderText = ClassWorkZones.ColAlTabDist + (_dataspec.AlTabDistance.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColNiTabDist].HeaderText = ClassWorkZones.ColNiTabDist + (_dataspec.NiTabDistance.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColAlTabDistMax].HeaderText = ClassWorkZones.ColAlTabDistMax + (_dataspec.AlTabDistance.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColNiTabDistMax].HeaderText = ClassWorkZones.ColNiTabDistMax + (_dataspec.NiTabDistance.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColAlTabLen].HeaderText = ClassWorkZones.ColAlTabLen + (_dataspec.AlTabLength.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColNiTabLen].HeaderText = ClassWorkZones.ColNiTabLen + (_dataspec.NiTabLength.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColTabDist].HeaderText = ClassWorkZones.ColTabDist + (_dataspec.TabDistance.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColAlSealantHi].HeaderText = ClassWorkZones.ColAlSealantHi + (_dataspec.AlSealantHeight.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColNiSealantHi].HeaderText = ClassWorkZones.ColNiSealantHi + (_dataspec.NiSealantHeight.CheckNGDisable ? "(忽略)" : "");
                dataGridViewAuto.Columns[ClassWorkZones.ColShoulderWidth].HeaderText = ClassWorkZones.ColShoulderWidth + (_dataspec.ShoulderWidth.CheckNGDisable ? "(忽略)" : "");
            }
        }
        //public double RefThickness { get; set; }
        //public double ThicknessMeasRefLeft { get; set; }
        //public double ThicknessMeasRefMid { get; set; }
        //public double ThicknessMeasRefRight { get; set; }
        public int MeasAmount { get; set; }
        public string UseGauge { get; set; }

        private TimeClass timer = new TimeClass();
        public AutoPanel()
        {
            InitializeComponent();
            switch (ClassWorkFlow.Instance.WorkMode)
            {
                case EnumWorkMode.正常: optionBoxWorkModeNormal.Text = EnumWorkMode.正常.ToString(); optionBoxWorkModeNormal.Checked = true; break;
                case EnumWorkMode.空跑: optionBoxWorkModeDry.Text = EnumWorkMode.空跑.ToString(); optionBoxWorkModeDry.Checked = true; break;
            }
            switch (ClassWorkFlow.Instance.UnloadMode)
            {
                case EnumUnloadMode.全NG: optionBoxUnloadAllNG.Text = EnumUnloadMode.全NG.ToString(); optionBoxUnloadAllNG.Checked = true; break;
                case EnumUnloadMode.全OK: optionBoxUnloadAllOK.Text = EnumUnloadMode.全OK.ToString(); optionBoxUnloadAllOK.Checked = true; break;
                case EnumUnloadMode.正常: optionBoxUnloadNormal.Text = EnumUnloadMode.正常.ToString(); optionBoxUnloadNormal.Checked = true; break;
            }
            switch (ClassWorkFlow.Instance.LoadMode)
            {
                case EnumLoadMode.手动: optionBoxLoadManual.Text = EnumLoadMode.手动.ToString(); optionBoxLoadManual.Checked = true; break;
                case EnumLoadMode.自动: optionBoxLoadAuto.Text = EnumLoadMode.自动.ToString(); optionBoxLoadAuto.Checked = true; break;
            }
            ClassErrorHandle.AddErrorDispPanel(errorListUserControl1);
            zones = ClassWorkZones.Instance;
            zones.AddStatusGUI(this);
            zones.CellDTViewer = dataGridViewAuto;
            zones.CellDT.RowChanged += DataRowChangeEventHandler;
            //LogLevel = LogFile.Level.Info;
            //ClassCommonSetting.Log.AddLogControl(this);
            AddDisp();
            CommonFunction.SysPublisher.subscribeMeToSystemEvents(this);
            ClassCommonSetting.SysParam.CurrentProductParam.AddParaInterface(this);
            ClassWorkZones.Instance.WorkZoneNG挑选机械手.UpdateNGBoxEvent += UpdateSortingNGBox;
            ClassWorkZones.Instance.WorkZone上料机械手.UpdateNGBoxEvent += UpdateLoadNGBox;
            ClassWorkZones.Instance.WorkZone外框架.SafetyStatusChange += WorkZone外框架_SafetyStatusChange;
            WorkZone外框架_SafetyStatusChange(ClassWorkZones.Instance.WorkZone外框架.ThisInport(ClassZone外框架.EnumInportName.FrameDoorOpen));
            timer.ThreadTimerTickEvent += timer1_Tick;
            textBoxWorkFlowStatus.Text = Statistic.WFStatus.ToString();
            BaseForm.SetHelpTip(pictureBoxDebugSwitch, "鼠标点击进行切换。手动模式下流程暂停，切换回自动模式下才能进行恢复流程操作。");
        }

        private void WorkZone外框架_SafetyStatusChange(Colibri.CommonModule.IOSystem.BaseIOPort port)
        {
            if (port.PortName == "In" + ClassZone外框架.EnumInportName.FrameDoorOpen.ToString())
                BaseForm.SetControlText(labelSafetyStatusDoor, $"门禁: {(port.status ? "打开" : "关闭")}");
        }

        public void HookErrorEvent()
        {
            ClassErrorHandle.ErrorItemCountChangeEvent += ClassErrorHandle_ErrorItemCountChangeEvent;
            ClassErrorHandle.HookErrorEvent(this);
        }
        private static object alarmlock = new object();

        public void ShowErrorHandler(string Source, string ErrorMessage, ErrorLevel level, LogFile log, bool retry, ErrorReturnHandler HandleFunc)
        {
            //BaseForm.DoInvokeRequired(labelShowAlarm, () =>
            // {
            //     switch (level)
            //     {
            //         case ErrorLevel.Alarm:
            //             if (retry && HandleFunc != null)
            //                 labelShowAlarm.Visible = true;
            //             break;
            //         case ErrorLevel.Error:
            //         case ErrorLevel.Fatal:
            //             labelShowAlarm.Visible = true;
            //             break;
            //     }
            // });
        }

        private void ClassErrorHandle_ErrorItemCountChangeEvent(int ErrorItemCount)
        {
            //if (ErrorItemCount == 0)
            //    BaseForm.DoInvokeRequired(labelShowAlarm, () => labelShowAlarm.Visible = false);
        }

        private void AddDisp()
        {
            ClassWorkZones.Instance.WorkZoneNG挑选机械手.AddDisp(textBoxSortNGDatas2, textBoxSortNGDatas1, textBoxSortNGDatas0);
            ClassWorkZones.Instance.WorkZone上料传送.AddDisp(textBoxLoadInDatas5, textBoxLoadInDatas4, textBoxLoadInDatas3, textBoxLoadInDatas2, textBoxLoadInDatas1, textBoxLoadInDatas0);
            ClassWorkZones.Instance.WorkZone上料机械手.AddDisp(textBoxLoadPNPDatas2, textBoxLoadPNPDatas1, textBoxLoadPNPDatas0);
            ClassWorkZones.Instance.WorkZone下料传送.AddDisp(textBoxUnloadOutDatas5, textBoxUnloadOutDatas4, textBoxUnloadOutDatas3, textBoxUnloadOutDatas2, textBoxUnloadOutDatas1, textBoxUnloadOutDatas0);
            ClassWorkZones.Instance.WorkZone下料机械手.AddDisp(textBoxUnloadPNPDatas2, textBoxUnloadPNPDatas1, textBoxUnloadPNPDatas0);
            ClassWorkZones.Instance.WorkZone传送机械手.AddLoadDisp(textBoxTransLoadDatas2, textBoxTransLoadDatas1, textBoxTransLoadDatas0);
            ClassWorkZones.Instance.WorkZone传送机械手.AddUnloadDisp(textBoxTransUnloadDatas2, textBoxTransUnloadDatas1, textBoxTransUnloadDatas0);
            ClassWorkZones.Instance.WorkZone厚度测量.AddDisp(textBoxThicknessDatas2, textBoxThicknessDatas1, textBoxThicknessDatas0);
            ClassWorkZones.Instance.WorkZone尺寸测量.AddDisp(textBoxCCDMeasDatas2, textBoxCCDMeasDatas1, textBoxCCDMeasDatas0);
            ClassWorkZones.Instance.WorkZone顶封边定位.AddDisp(textBoxTopAlignDatas2, textBoxTopAlignDatas1, textBoxTopAlignDatas0);
        }
        #region Event
        public void UpdateState(object sender, EventArgs e)
        {
            BaseForm.SetControlText(textBoxWorkFlowStatus, sender.ToString());
        }
        private DateTime workflowStart;
        public override void StartHandler(BaseClass sender, StateEventArgs e)
        {
            base.StartHandler(sender, e);
            BaseForm.DoInvokeRequired(selectBoxNoLoad, () => selectBoxNoLoad.Checked = false);
            workflowStart = DateTime.Now;
            timer.StartTimer(1000, 1000);
            BaseForm.SetControlText(cellTextBoxWorkFlowStart, workflowStart.ToLongTimeString());
        }
        public override void StopHandler(BaseClass sender, StateEventArgs e)
        {
            base.StopHandler(sender, e);
            BaseForm.SetControlText(cellTextBoxWorkFlowStart, "无");
            timer.StopTimer();
        }
        public override void PauseHandler(BaseClass sender, StateEventArgs e)
        {
            base.PauseHandler(sender, e);
        }
        public override void ResumeHandler(BaseClass sender, StateEventArgs e)
        {
            base.ResumeHandler(sender, e);
        }
        public override void EStopHandler(BaseClass sender, StateEventArgs e)
        {
            base.EStopHandler(sender, e);
        }
        public override void ProductChangeHandler(BaseClass sender, StateEventArgs e)
        {
        }
        private void DataRowChangeEventHandler(object sender, DataRowChangeEventArgs e)
        {
            dataGridViewAuto.FirstDisplayedScrollingRowIndex = dataGridViewAuto.RowCount - 1;
        }
        #endregion Event
        protected override void SetAllControlEnable(bool Enable)
        {
            BaseForm.DoInvokeRequired(buttonResetAllZone, () => buttonResetAllZone.Enabled = Enable);
        }
        public void UpdateWorkFlowDataStatus(EnumDataTransfer transfer)
        {
        }

        public void ShowPPM(long count, double ppm, double lastppm, TimeSpan laps, TimeSpan singlelaps)
        {
            BaseForm.SetControlText(textBoxCount, count.ToString());
            BaseForm.SetControlText(textBoxPPM, string.Format("{1:0.00}", ppm, lastppm));
            BaseForm.SetControlText(textBoxLaps, String.Format("{0:00}.{1:000}秒", singlelaps.Seconds, singlelaps.Milliseconds, laps.Hours, laps.Minutes, laps.Seconds, laps.Milliseconds));
        }

        //public void TransferData(string data)
        //{
        //    BaseForm.DoInvokeRequired(listBoxLog, () =>
        //    {
        //        listBoxLog.Items.Add(data);
        //        listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        //    });
        //}

        private void optionBoxUnloadNormal_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.UnloadMode = EnumUnloadMode.正常;
            labelNGBoxIndex1.Text =
                labelNGBoxIndex3.Text = "厚度：";
            labelNGBoxIndex2.Text =
                labelNGBoxIndex4.Text = "尺寸：";
        }

        private void optionBoxUnloadAllOK_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.UnloadMode = EnumUnloadMode.全OK;
        }

        private void optionBoxUnloadAllNG_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.UnloadMode = EnumUnloadMode.全NG;
            labelNGBoxIndex1.Text = "NG盒1";
            labelNGBoxIndex2.Text = "NG盒2";
            labelNGBoxIndex3.Text = "NG盒3";
            labelNGBoxIndex4.Text = "NG盒4";
        }

        private void selectBoxNoLoad_CheckedChanged(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.FeedNewPart = !selectBoxNoLoad.Checked;
        }

        private void buttonResetAllZone_Click(object sender, EventArgs e)
        {
            ClassWorkZones.Instance.ResetAllZones();
        }

        private void optionBoxWorkModeDry_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.WorkMode = EnumWorkMode.空跑;
        }

        private void optionBoxWorkModeNormal_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.WorkMode = EnumWorkMode.正常;
        }
        private void UpdateSortingNGBox()
        {
            Control temp;
            for (int i = 0; i < 4; i++)
            {
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "LeftCount"];
                BaseForm.DoInvokeRequired(temp, () => ClassBaseWorkZone.SetNGText(temp, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxCellCount[i, (int)EnumCellIndex.左电芯]));
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "MidCount"];
                BaseForm.DoInvokeRequired(temp, () => ClassBaseWorkZone.SetNGText(temp, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxCellCount[i, (int)EnumCellIndex.中电芯]));
                temp = groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "RightCount"];
                BaseForm.DoInvokeRequired(temp, () => ClassBaseWorkZone.SetNGText(temp, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxCellCount[i, (int)EnumCellIndex.右电芯]));
                temp = groupBoxNGBox.Controls["labelNGBoxIndex" + (i + 1).ToString()];
                BaseForm.DoInvokeRequired(temp, () => temp.BackColor = ClassWorkZones.Instance.WorkZoneNG挑选机械手.CurrentNGBoxRow == i ? Color.SkyBlue : Color.Transparent);
                ClassCommonSetting.UpdateIOStatus(groupBoxNGBox.Controls["labelNGBox" + (i + 1).ToString() + "Full"], ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFullSensor[i]);
            }
            //ClassCommonSetting.UpdateIOStatus(labelNGBox1Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFullSensor[0]);
            //ClassCommonSetting.UpdateIOStatus(labelNGBox2Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFullSensor[1]);
            //ClassCommonSetting.UpdateIOStatus(labelNGBox3Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFullSensor[2]);
            //ClassCommonSetting.UpdateIOStatus(labelNGBox4Full, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFullSensor[3]);
            ClassCommonSetting.UpdateIOStatus(labelNGBoxBack, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxBackSenser);
            ClassCommonSetting.UpdateIOStatus(labelNGBoxFront, ClassWorkZones.Instance.WorkZoneNG挑选机械手.NGBoxFrontSensor);
            BaseForm.SetControlText(labelCurrentUse, ClassWorkZones.Instance.WorkZoneNG挑选机械手.IsUseBackNGBox ? "后NG盒" : "前NG盒");
        }
        private void UpdateLoadNGBox()
        {
            BaseForm.DoInvokeRequired(labelNGBoxLeftCount, () => ClassBaseWorkZone.SetNGText(labelNGBoxLeftCount, ClassWorkZones.Instance.WorkZone上料机械手.NGBoxCellCount[EnumCellIndex.左电芯]));
            BaseForm.DoInvokeRequired(labelNGBoxMidCount, () => ClassBaseWorkZone.SetNGText(labelNGBoxMidCount, ClassWorkZones.Instance.WorkZone上料机械手.NGBoxCellCount[EnumCellIndex.中电芯]));
            BaseForm.DoInvokeRequired(labelNGBoxRightCount, () => ClassBaseWorkZone.SetNGText(labelNGBoxRightCount, ClassWorkZones.Instance.WorkZone上料机械手.NGBoxCellCount[EnumCellIndex.右电芯]));
            ClassCommonSetting.UpdateIOStatus(labelNGBoxFull, ClassWorkZones.Instance.WorkZone上料机械手.NGBoxFullSensor);
            ClassCommonSetting.UpdateIOStatus(labelNGBox, ClassWorkZones.Instance.WorkZone上料机械手.NGBoxSensor);
        }

        private void optionBoxFrontKM_Click(object sender, EventArgs e)
        {
            ClassWorkZones.Instance.WorkZone外框架.SwitchKM(false);
        }

        private void optionBoxBackKM_Click(object sender, EventArgs e)
        {
            ClassWorkZones.Instance.WorkZone外框架.SwitchKM(true);
        }

        private void buttonClearDispData_Click(object sender, EventArgs e)
        {
            ClassWorkZones.Instance.CellDT.Clear();
        }

        private void optionBoxLoadManual_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.LoadMode = EnumLoadMode.手动;
        }

        private void optionBoxLoadAuto_Click(object sender, EventArgs e)
        {
            ClassWorkFlow.Instance.LoadMode = EnumLoadMode.自动;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - workflowStart;
            BaseForm.SetControlText(cellTextBoxRunTime, string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds));
        }

        private void buttonClearTotal_Click(object sender, EventArgs e)
        {
            Statistic.ClearTotal();
        }

        private void pictureBoxDebugSwitch_Click(object sender, EventArgs e)
        {
            if (ClassWorkFlow.Instance.DebugMode == WFDebugMode.Auto)
            {
                pictureBoxDebugSwitch.BackgroundImage = Properties.Resources.Manual;
                ClassWorkFlow.Instance.DebugMode = WFDebugMode.Manual;
                //手动模式下暂停流程
                CommonFunction.SysPublisher.notifyPauseEventSubscribers(null, new StateEventArgs("暂停", "切换到手动模式"));
            }
            else
            {
                pictureBoxDebugSwitch.BackgroundImage = Properties.Resources.Auto;
                ClassWorkFlow.Instance.DebugMode = WFDebugMode.Auto;
            }
        }

         public void GetTimeUsage(TimeUsageItem item, double TotalTime, string message)
        {
            switch(item)
            {
                case TimeUsageItem.ZoneCCD:
                    BaseForm.SetControlText(labelCCD, "CCD尺寸测量(用时: " + TotalTime.ToString("0.00s)"));
                    BaseForm.DoInvokeRequired(labelCCD, () => BaseForm.SetHelpTip(labelCCD, message));
                    break;
                case TimeUsageItem.ZoneThickness:
                    BaseForm.SetControlText(labelThickness, "厚度测量(用时: " + TotalTime.ToString("0.00s)"));
                    BaseForm.DoInvokeRequired(labelThickness, () => BaseForm.SetHelpTip(labelThickness, message));
                    break;
                case TimeUsageItem.ZoneTopAlign:
                    BaseForm.SetControlText(labelTopAlign, "顶封边定位(用时: " + TotalTime.ToString("0.00s)"));
                    BaseForm.DoInvokeRequired(labelTopAlign, () => BaseForm.SetHelpTip(labelTopAlign, message));
                    break;
                case TimeUsageItem.ZoneTransPNP:
                    BaseForm.SetControlText(labelTransPNP, "传送PNP(用时: " + TotalTime.ToString("0.00s)"));
                    BaseForm.DoInvokeRequired(labelTransPNP, () => BaseForm.SetHelpTip(labelTransPNP, message));
                    break;
            }
        }
    }
}