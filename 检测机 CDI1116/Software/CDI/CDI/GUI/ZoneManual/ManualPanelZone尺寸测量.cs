using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CDI.Zone;
using System.Threading;
using Colibri.CommonModule;

namespace CDI.GUI
{
    public partial class ManualPanelZone尺寸测量 : BaseZoneManualPanel
    {
        public ManualPanelZone尺寸测量()
        {
            InitializeComponent();
        }
        protected override void Init()
        {
            zone.AddDisp(DataDispCellLeft, DataDispCellMiddle, DataDispCellRight);

            motorUCOutlineMeasX.Axis = zone.AxisOutlineMeasX;
        }
        private ClassZone尺寸测量 zone
        {
            get { return (ClassZone尺寸测量)OwnerZone; }
        }
        public override void RefreshCellData(object sender, EventArgs e)
        {
            lock (RefreshLock)
            {
                base.RefreshCellData(sender, e);
                CellDataRefresh(DataDispCellLeft);
                CellDataRefresh(DataDispCellMiddle);
                CellDataRefresh(DataDispCellRight);
            }
        }
        private void buttonMeas_Click(object sender, EventArgs e)
        {
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionStartCCDMeas(DataComp.AddAll);
                while (!zone.isCCDAllFinish)
                    Thread.Sleep(1);
                return res;
            });
        }

        private void buttonMoveToGetPart_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionToGetPart(); });
        }

        private void StartCapture_Click(object sender, EventArgs e)
        {
            //SVS_CameraSys.CameraList[0].SvsSnapImage();
        }

        private void StopCapture_Click(object sender, EventArgs e)
        {
            //SVS_CameraSys.CameraList[0].StopLive();
        }

        //CAMSetting Camset;
        private void CameraSetting_Click(object sender, EventArgs e)
        {
            //Camset = CAMSetting.Instance;
            //Camset.Show();
        }
        EnumCellIndex cellindex = EnumCellIndex.右电芯;
        private void buttonMeasOnce_Click(object sender, EventArgs e)
        {
            if (MotorSafetyCheck.InPositionRange(zone.AxisOutlineMeasX, ClassZone尺寸测量.EnumPointX.Start, ClassDataInfo.CELLPITCH * (int)EnumCellIndex.右电芯))
                cellindex = EnumCellIndex.右电芯;
            else if (MotorSafetyCheck.InPositionRange(zone.AxisOutlineMeasX, ClassZone尺寸测量.EnumPointX.Start, ClassDataInfo.CELLPITCH * (int)EnumCellIndex.中电芯))
                cellindex = EnumCellIndex.中电芯;
            else if (MotorSafetyCheck.InPositionRange(zone.AxisOutlineMeasX, ClassZone尺寸测量.EnumPointX.Start, ClassDataInfo.CELLPITCH * (int)EnumCellIndex.左电芯))
                cellindex = EnumCellIndex.左电芯;
            else
            {
                MessageBox.Show("尺寸测量需要先将指定工位移到相机下方。请先将要测量的工位移动到相机下方。");
                return;
            }
            if (zone.CCDMeasDataStations[cellindex].CellData == null)
            {
                zone.CCDMeasDataStations[cellindex].CellData = ClassDataInfo.NewCellData();
                //zone.CCDMeasDataStations[cellindex].CellData.Barcode = "TEST";
            }
            DoAction(sender, () =>
            {
                ErrorInfoWithPause res = zone.ActionOneCCDMeas(cellindex, DataComp.AddAll);
                while (!zone.isCCDAllFinish)
                    Thread.Sleep(1);
                zone.CCDMeasDataStations[cellindex].CellData = zone.BufferDatas[(int)cellindex];
                return res;
            });
        }

        private void buttonMoveToLeftCell_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionToMeasPos(EnumCellIndex.左电芯); });
        }

        private void buttonMoveToMidCell_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionToMeasPos(EnumCellIndex.中电芯); });
        }

        private void buttonMoveToRightCell_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionToMeasPos(EnumCellIndex.右电芯); });
        }

        private void buttonSnapShot_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.SnapShot(3, "抓取一帧"); });
        }

        private void buttonMoveToPickPart_Click(object sender, EventArgs e)
        {
            DoAction(sender, () => { return zone.ActionToUnloadPart(); });
        }

        private void buttonCDIVision_Click(object sender, EventArgs e)
        {
            ClassCommonSetting.OpenCDIVision();
            MessageBox.Show(((Button)sender).Text + "完成。");
        }
    }
}