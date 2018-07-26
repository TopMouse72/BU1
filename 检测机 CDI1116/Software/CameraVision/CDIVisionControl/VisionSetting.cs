using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Colibri.CommonModule.ToolBox;
using Measure;
using VisionUnit;

namespace CDIVisionControl
{
    public delegate void SetVisionIndexHandle(BufferIndex index);
    public partial class VisionSetting : UserControl
    {
        public EventHandler ShotOneFrame;
        public SetVisionIndexHandle SetVisionIndex;
        public VisionControl[] VisionList = new VisionControl[Enum.GetNames(typeof(BufferIndex)).Length];
        public CameraDispControl[] CameraDispList = new CameraDispControl[Enum.GetNames(typeof(BufferIndex)).Length];
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurrentProductName
        {
            get { return VSGlobalControl.SysParam.CurrentProduct; }
            set
            {
                VSGlobalControl.ReadWriteProdVisionParam();
                //VSGlobalControl.SysParam.CurrentProduct = value;
                if (VSGlobalControl.m_VParam.UseBottomAsBase)
                    optionBoxBottomBaseCell.Checked = true;
                else
                    optionBoxUpBaseCell.Checked = true;
                if (VSGlobalControl.m_VGaugeParam.UseBottomAsBase)
                    optionBoxBottomBaseGauge.Checked = true;
                else
                    optionBoxUpBaseGauge.Checked = true;
                comboBoxMeasureMethod.Text = VSGlobalControl.m_VParam.MeasureMethod.ToString();
                numericUpDownThresholdStd.Value = VSGlobalControl.m_VGaugeParam.ThresholdStd;
                numericUpDownThresholdCell.Value = VSGlobalControl.m_VParam.ThresholdCell;
                VSGlobalControl.SetROI();
                VSGlobalControl.ReadWriteProdVisionParam(false);
                VSGlobalControl.CleanAllTestMark();
            }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UseGauge
        {
            get { return VSGlobalControl.UseGaugeName; }
        }
        public string Barcode
        {
            get { return textBoxBarcode.Text.Trim(); }
            set { textBoxBarcode.Text = value.Trim(); }
        }
        public VisionSetting()
        {
            InitializeComponent();
            comboBoxROI.Items.Add(CamCDIRoiName.上边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.左边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.下边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.右边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Al上边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Al下边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Al右边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Ni上边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Ni下边缘);
            comboBoxROI.Items.Add(CamCDIRoiName.Ni右边缘);
            comboBoxMeasureMethod.Items.AddRange(Enum.GetNames(typeof(MeasMethod)));
        }
        public void SettingInit()
        {
            CameraDispList[(int)BufferIndex.Test].AddMouseUp(CamDisplay4_MouseUp);
            for (int i = 0; i < Enum.GetNames(typeof(BufferIndex)).Length; i++)
                CameraDispList[i].AddDragDrop(CamDisplay4_DragDrop);
            UpdateCameraPara(null, null);
        }
        private void buttonCameraSave_Click(object sender, EventArgs e)
        {
            string error = "";
            int temp;
            //if (!double.TryParse(textBoxImageKX.Text.Trim(), out VSGlobalControl.m_CamParam.CameraKX))
            //    error += labelKX.Text + Environment.NewLine;
            //if (!double.TryParse(textBoxImageKY.Text.Trim(), out VSGlobalControl.m_CamParam.CameraKY))
            //    error += labelKY.Text + Environment.NewLine;
            //if (!int.TryParse(textBoxImageWidth.Text.Trim(), out temp))
            //    error += labelImageWidth.Text + Environment.NewLine;
            //else
            //    VSGlobalControl.m_CamParam.CameraSize.Width = temp;
            //if (!int.TryParse(textBoxImageHeight.Text.Trim(), out temp))
            //    error += labelImageHeight.Text + Environment.NewLine;
            //else
            //    VSGlobalControl.m_CamParam.CameraSize.Height = temp;
            if (!double.TryParse(textBoxLineTolerance.Text.Trim(), out VSGlobalControl.m_CamParam.LineTolerance))
                error += labelLineTol.Text + Environment.NewLine;
            if (error != "")
            {
                MessageBox.Show("保存失败，数据格式有误: " + Environment.NewLine + error);
                return;
            }
            else
                VSGlobalControl.ReadWriteCameraParam(false);
        }
        private void ID_OPEN_BMP_Click(object sender, EventArgs e)
        {
            IntPtr hW;
            switch (((Button)sender).Name)
            {
                case "buttonFromRight":
                    hW = VSRightCell.Vision_GetHwnd(0, 0, VisionList[(int)BufferIndex.RightCell].imgWidth, VisionList[(int)BufferIndex.RightCell].imgHeight);
                    VSTest.Vision_CKCopy(hW, VisionList[(int)BufferIndex.RightCell].imgWidth, VisionList[(int)BufferIndex.RightCell].imgHeight);
                    VSTest.Vision_CopyImageToBuffer(1);
                    break;
                case "buttonFromMiddle":
                    hW = VSMiddleCell.Vision_GetHwnd(0, 0, VisionList[(int)BufferIndex.MiddleCell].imgWidth, VisionList[(int)BufferIndex.MiddleCell].imgHeight);
                    VSTest.Vision_CKCopy(hW, VisionList[(int)BufferIndex.MiddleCell].imgWidth, VisionList[(int)BufferIndex.MiddleCell].imgHeight);
                    VSTest.Vision_CopyImageToBuffer(1);
                    break;
                case "buttonFromLeft":
                    hW = VSLeftCell.Vision_GetHwnd(0, 0, VisionList[(int)BufferIndex.LeftCell].imgWidth, VisionList[(int)BufferIndex.LeftCell].imgHeight);
                    VSTest.Vision_CKCopy(hW, VisionList[(int)BufferIndex.LeftCell].imgWidth, VisionList[(int)BufferIndex.LeftCell].imgHeight);
                    VSTest.Vision_CopyImageToBuffer(1);
                    break;
                case "buttonFromFile":
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string szpath = openFileDialog1.FileName;
                        LoadPic(szpath);
                    }
                    break;
                case "buttonFromCamera":
                    if (ShotOneFrame != null) ShotOneFrame((int)BufferIndex.Test, null);
                    break;
            }
        }
        private void LoadPic(string picfile)
        {
            textBoxBarcode.Text = VSTest.LoadPicture(picfile);
            VisionList[(int)BufferIndex.Test].Barcode = textBoxBarcode.Text;
            if (SetVisionIndex != null) SetVisionIndex(BufferIndex.Test);
        }
        public void UpdateCameraPara(object sender, EventArgs e)
        {
            textBoxImageWidth.Text = VSGlobalControl.m_CamParam.CameraSize.Width.ToString();
            textBoxImageHeight.Text = VSGlobalControl.m_CamParam.CameraSize.Height.ToString();
            textBoxImageKX.Text = VSGlobalControl.m_CamParam.CameraKX.ToString();
            textBoxImageKY.Text = VSGlobalControl.m_CamParam.CameraKY.ToString();
            textBoxLineTolerance.Text = VSGlobalControl.m_CamParam.LineTolerance.ToString();
        }
        private void CamDisplay4_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            LoadPic(s[0]);
        }
        private void CamDisplay4_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsRefSetting)
            {
                Cst.dPoint temp = new Cst.dPoint(e.X, e.Y);
                refpoints.Add(temp);
                switch (refpoints.Count)
                {
                    case 1:
                        VisionList[(int)BufferIndex.Test].Vision_DrawCross(e.X * 100 / VisionList[(int)BufferIndex.Test].Zoom, e.Y * 100 / VisionList[(int)BufferIndex.Test].Zoom, 40, VSBase.COLOR_CYAN, Cst.ViewLWidth50);
                        MessageBox.Show("已经设置第一个基准点。还有三个基准点。");
                        break;
                    case 2:
                        VisionList[(int)BufferIndex.Test].Vision_DrawCross(e.X * 100 / VisionList[(int)BufferIndex.Test].Zoom, e.Y * 100 / VisionList[(int)BufferIndex.Test].Zoom, 40, VSBase.COLOR_CYAN, Cst.ViewLWidth50);
                        MessageBox.Show("已经设置第二个基准点。还有两个基准点。");
                        break;
                    case 3:
                        VisionList[(int)BufferIndex.Test].Vision_DrawCross(e.X * 100 / VisionList[(int)BufferIndex.Test].Zoom, e.Y * 100 / VisionList[(int)BufferIndex.Test].Zoom, 40, VSBase.COLOR_CYAN, Cst.ViewLWidth50);
                        MessageBox.Show("已经设置第三个基准点。还有一个基准点。");
                        break;
                    case 4:
                        Cst.dPoint centpoint = Cst.dPoint.GetAvg(refpoints.ToArray());
                        for (int i = 0; i < refpoints.Count; i++)
                            if (refpoints[i].X < centpoint.X)
                            {
                                if (refpoints[i].Y < centpoint.Y)
                                {
                                    //左上
                                    if (RefIndex == REF_GAUGE)
                                        VSGlobalControl.m_VGaugeParam.TopLeft = refpoints[i];
                                    else
                                        VSGlobalControl.m_VParam.TopLeft = refpoints[i];
                                }
                                else
                                {
                                    //左下
                                    if (RefIndex == REF_GAUGE)
                                        VSGlobalControl.m_VGaugeParam.BottomLeft = refpoints[i];
                                    else
                                        VSGlobalControl.m_VParam.BottomLeft = refpoints[i];
                                }
                            }
                            else
                            {
                                if (refpoints[i].Y < centpoint.Y)
                                {
                                    //右上
                                    if (RefIndex == REF_GAUGE)
                                        VSGlobalControl.m_VGaugeParam.TopRef = refpoints[i];
                                    else
                                        VSGlobalControl.m_VParam.TopRef = refpoints[i];
                                }
                                else
                                {
                                    //右下
                                    if (RefIndex == REF_GAUGE)
                                        VSGlobalControl.m_VGaugeParam.BottomRef = refpoints[i];
                                    else
                                        VSGlobalControl.m_VParam.BottomRef = refpoints[i];
                                }
                            }
                        //if (refpoints[0].Y > refpoints[1].Y)
                        //{
                        //    VSGlobalControl.m_VParam.BottomRef = refpoints[0];
                        //    VSGlobalControl.m_VParam.TopRef = refpoints[1];
                        //}
                        //else
                        //{
                        //    VSGlobalControl.m_VParam.BottomRef = refpoints[1];
                        //    VSGlobalControl.m_VParam.TopRef = refpoints[0];
                        //}
                        if (RefIndex == REF_GAUGE)
                        {
                            VSGlobalControl.m_VGaugeParam.BottomRef *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VGaugeParam.TopRef *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VGaugeParam.TopLeft *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VGaugeParam.BottomLeft *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VGaugeParam.AvgRef = Cst.dPoint.GetAvg(new Cst.dPoint[] { VSGlobalControl.m_VGaugeParam.BottomRef, VSGlobalControl.m_VGaugeParam.TopRef });
                            VSGlobalControl.SetROIByRef(MessageBox.Show("是否用当前标准块生成新模板?", "保存模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                            MessageBox.Show("标准块基准设置完成。可点击“开始设置”重新设置基准。");
                            if (MessageBox.Show("是否需要对电芯基准进行设置？如果需要则先放置好电芯，然后点“是”开始电芯基准设置，需要略过则点“否”。", "电芯基准设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                RefIndex = REF_CELL;
                                MessageBox.Show("请先确定电芯已放好，点”确定“开始电芯基准设置。用鼠标点击图像中电芯主体边缘四个顶点。");
                            }
                            else
                                RefIndex = REF_NONE;
                        }
                        else
                        {
                            VSGlobalControl.m_VParam.BottomRef *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VParam.TopRef *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VParam.TopLeft *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VParam.BottomLeft *= (double)100 / VSTest.Zoom;
                            VSGlobalControl.m_VParam.AvgRef = Cst.dPoint.GetAvg(new Cst.dPoint[] { VSGlobalControl.m_VParam.BottomRef, VSGlobalControl.m_VParam.TopRef });
                            VSGlobalControl.SetROIByRef(false);
                            RefIndex = REF_NONE;
                            MessageBox.Show("电芯基准设置完成。可点击“开始设置”重新设置电芯基准。");
                        }
                        VSGlobalControl.ReadWriteProdVisionParam(false);
                        VSGlobalControl.CleanAllTestMark();
                        IsRefSetting = false;
                        break;
                }
            }
        }
        private bool IsRefSetting = false;
        private List<Cst.dPoint> refpoints = new List<Cst.dPoint>();
        private const int REF_NONE = 0;
        private const int REF_CELL = 1;
        private const int REF_GAUGE = 2;
        private int _refIndex = REF_NONE;
        private int RefIndex
        {
            get { return _refIndex; }
            set
            {
                _refIndex = value;
                switch (_refIndex)
                {
                    case REF_NONE: labelRefStatus.Text = "定位：停止"; break;
                    case REF_CELL: labelRefStatus.Text = "定位：设置电芯基准"; break;
                    case REF_GAUGE: labelRefStatus.Text = "定位：设置标准块基准"; break;
                }
            }
        }
        [Description("打开相机回调事件")]
        public event EventHandler CameraLive;
        [Description("关闭相机回调事件")]
        public event EventHandler CameraStop;
        private void buttonStartRefCali_Click(object sender, EventArgs e)
        {
            if (UseGauge == "")
            {
                MessageBox.Show("当前产品没有标准块，不能进行基准设置。需要在CDI产品设置中选择标准块并保存后，再重新进行基准设置。");
                return;
            }
            IsRefSetting = true;
            refpoints.Clear();
            VSTest.Vision_MarkClean();
            if (CameraLive != null)
                foreach (EventHandler handler in CameraLive.GetInvocationList()) CameraLive(sender, e);
            MessageBox.Show("相机已打开。将开始分别对电芯和标准块进行基准的设置。");
            if (MessageBox.Show("是否需要对标准块基准进行设置？如果需要则先放置好标准块，然后点“是”开始标准块基准设置，需要略过则点“否”。", "标准块基准设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RefIndex = REF_GAUGE;
                MessageBox.Show("请先确定标准块已放好，点”确定“开始标准块基准设置。用鼠标点击图像中标准块主体边缘四个顶点。");
            }
            else if (MessageBox.Show("是否需要对电芯基准进行设置？如果需要则先放置好电芯，然后点“是”开始电芯基准设置，需要略过则点“否”。", "电芯基准设置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RefIndex = REF_CELL;
                MessageBox.Show("请先确定电芯已放好，点”确定“开始电芯基准设置。用鼠标点击图像中电芯主体边缘四个顶点。");
            }
            else
                IsRefSetting = false;
        }

        private void buttonMeasMethodSet_Click(object sender, EventArgs e)
        {
            VSGlobalControl.m_VParam.MeasureMethod = (MeasMethod)Enum.Parse(typeof(MeasMethod), comboBoxMeasureMethod.Text);
            VSGlobalControl.m_VParam.UseBottomAsBase = optionBoxBottomBaseCell.Checked;
            VSGlobalControl.m_VGaugeParam.MeasureMethod = (MeasMethod)Enum.Parse(typeof(MeasMethod), comboBoxMeasureMethod.Text);
            VSGlobalControl.m_VGaugeParam.UseBottomAsBase = optionBoxBottomBaseGauge.Checked;
            VSGlobalControl.ReadWriteProdVisionParam(false);
            VSGlobalControl.SetROI();
        }

        private void EdgeTestM_Click(object sender, EventArgs e)
        {
            Cst.Struct_Line temp = new Cst.Struct_Line();

            if (comboBoxROI.SelectedIndex >= 0)
            {
                temp = VisionList[(int)BufferIndex.Test].GetOneLine(VSGlobalControl.m_VParam.Im[(int)selectedROI]);
                VisionList[(int)BufferIndex.Test].Vision_DrawLine(temp, VSBase.COLOR_BLUE);
            }
        }

        private bool IsBatchRunning = false;
        private int count;
        private string[] files;
        bool isFromButton;
        private void buttonMeasure_Click(object sender, EventArgs e)
        {
            if (textBoxBarcode.Text.Trim() == "") textBoxBarcode.Text = "TEST";
            if (optionBoxMeasStd.Checked)
            {
                if (textBoxBarcode.Text.IndexOf("Standard") < 0) textBoxBarcode.Text += "Standard";
            }
            else
                textBoxBarcode.Text = textBoxBarcode.Text.Replace("Standard", "");
            Application.DoEvents();
            VisionList[(int)BufferIndex.Test].StartMeas(textBoxBarcode.Text.Trim(), false);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            IsBatchRunning = false;
        }
        private string[] GetFiles(string sourceFolder, string filters)
        {
            return filters.Split('|').SelectMany(filter => Directory.GetFiles(sourceFolder, filter)).ToArray();
        }
        public void BatchMeas(string folderpath)
        {
            if (!IsBatchRunning)
            {
                IsBatchRunning = true;
                files = GetFiles(folderpath, "*.bmp|*.png|*.jpg");
                count = 0;
            }
            if (IsBatchRunning)
            {
                for (; count < files.Length;)
                {
                    if (!IsBatchRunning) return;
                    labelFileCounter.Text = string.Format("{0}/{1}", count + 1, files.Length);
                    Application.DoEvents();
                    textBoxBarcode.Text = VSTest.LoadPicture(files[count]);
                    if (textBoxBarcode.Text != "")
                        VisionList[(int)BufferIndex.Test].StartMeas(textBoxBarcode.Text, false);
                    count++;
                    if (selectBoxPause.Checked) break;
                }
                if (count == files.Length)
                {
                    if (isFromButton) MessageBox.Show("批量测试完成。");
                    isFromButton = false;
                    IsBatchRunning = false;
                }
            }
        }
        private void buttonBatchMeasure_Click(object sender, EventArgs e)
        {
            if (!IsBatchRunning)
            {
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
                isFromButton = true;
            }
            BatchMeas(folderBrowserDialog1.SelectedPath);
        }
        private void buttonClearMark_Click(object sender, EventArgs e)
        {
            ClearMark();
        }
        public void ClearMark()
        {
            IsRefSetting = false;
            refpoints.Clear();
            VSGlobalControl.CleanAllTestMark(selectBoxShowROI.Checked, optionBoxDispCellRef.Checked);
        }
        private CamCDIRoiName selectedROI;
        private void buttonSaveROI_Click(object sender, EventArgs e)
        {
            if (comboBoxROI.SelectedIndex >= 0)
            {
                selectedROI = (CamCDIRoiName)Enum.Parse(typeof(CamCDIRoiName), comboBoxROI.Text);
                if (selectedROI == CamCDIRoiName.左边缘)
                {
                    numericUpDownIgnoreWidth1.Value = 0;
                    numericUpDownIgnoreWidth2.Value = 0;
                }
                VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.IgnoreLength1 = (double)numericUpDownIgnoreWidth1.Value;
                VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.IgnoreLength2 = (double)numericUpDownIgnoreWidth2.Value;
                VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.Robust.RobustSampleNum = (int)numericUpDownSampleNum.Value;
                VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.Robust.MaxRobustCount = (int)numericUpDownMaxRobustCount.Value;
                VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.PrintMultiplePoint = selectBoxPrint.Checked;
                VSGlobalControl.m_VGaugeParam.Im[(int)selectedROI].EdgeMultiplePoint.IgnoreLength1 = (double)numericUpDownIgnoreWidth1.Value;
                VSGlobalControl.m_VGaugeParam.Im[(int)selectedROI].EdgeMultiplePoint.IgnoreLength2 = (double)numericUpDownIgnoreWidth2.Value;
                VSGlobalControl.m_VGaugeParam.Im[(int)selectedROI].EdgeMultiplePoint.Robust.RobustSampleNum = (int)numericUpDownSampleNum.Value;
                VSGlobalControl.m_VGaugeParam.Im[(int)selectedROI].EdgeMultiplePoint.Robust.MaxRobustCount = (int)numericUpDownMaxRobustCount.Value;
                VSGlobalControl.m_VGaugeParam.Im[(int)selectedROI].EdgeMultiplePoint.PrintMultiplePoint = selectBoxPrint.Checked;
            }
            VSGlobalControl.ReadWriteProdVisionParam(false);
        }

        private void comboBoxROI_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedROI = (CamCDIRoiName)Enum.Parse(typeof(CamCDIRoiName), comboBoxROI.Text);
            Cst.Struct_Image temp = VSGlobalControl.m_VParam.Im[(int)selectedROI];
            numericUpDownIgnoreWidth1.Value = (decimal)temp.EdgeMultiplePoint.IgnoreLength1;
            numericUpDownIgnoreWidth2.Value = (decimal)temp.EdgeMultiplePoint.IgnoreLength2;
            numericUpDownSampleNum.Value = (decimal)temp.EdgeMultiplePoint.Robust.RobustSampleNum;
            numericUpDownMaxRobustCount.Value = (decimal)temp.EdgeMultiplePoint.Robust.MaxRobustCount;
            selectBoxPrint.Checked = VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.PrintMultiplePoint;
            VSGlobalControl.CleanAllTestMark(false);
            VSTest.Vision_ShowRect(VSGlobalControl.m_VParam.Im[(int)selectedROI].EdgeMultiplePoint.Rect, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            if (VisionControl.IsXLine(temp.EdgeMultiplePoint.Dir))
            {
                labelIgnoreWidth1.Text = "左边";
                labelIgnoreWidth2.Text = "右边";
            }
            else
            {
                labelIgnoreWidth1.Text = "上边";
                labelIgnoreWidth2.Text = "下边";
            }
        }

        private void numericUpDownThresholdStd_Click(object sender, EventArgs e)
        {
            VSTest.Vision_Binary((int)numericUpDownThresholdStd.Value);
        }

        private void numericUpDownThresholdCell_Click(object sender, EventArgs e)
        {
            VSTest.Vision_Binary((int)numericUpDownThresholdCell.Value);
        }

        private void buttonSaveThreshold_Click(object sender, EventArgs e)
        {
            VSGlobalControl.m_VParam.ThresholdCell = (int)numericUpDownThresholdCell.Value;
            VSGlobalControl.m_VParam.ThresholdStd = (int)numericUpDownThresholdStd.Value;
            VSGlobalControl.m_VGaugeParam.ThresholdStd = (int)numericUpDownThresholdStd.Value;
            VSGlobalControl.ReadWriteProdVisionParam(false);
        }

        private void buttonCancelRefCali_Click(object sender, EventArgs e)
        {
            IsRefSetting = false;
            RefIndex = REF_NONE;
            refpoints.Clear();
            VSGlobalControl.CleanAllTestMark(selectBoxShowROI.Checked);
            MessageBox.Show("电芯基准设置取消。");
        }

        private void buttonPrePos_Click(object sender, EventArgs e)
        {
            VisionList[(int)BufferIndex.Test].FindPat(optionBoxMeasStd.Checked);
        }

        private void VisionSetting_Paint(object sender, PaintEventArgs e)
        {
            const int SPACE = 6;
            SolidBrush brush = new SolidBrush(Color.Salmon);
            int x, y, width, height;
            Pen pen = new Pen(brush, 2);
            Graphics graphics = e.Graphics;
            //Draw camera area
            x = labelImageWidth.Left - SPACE; y = textBoxImageWidth.Top - SPACE;
            width = buttonCameraSave.Right + SPACE - x; height = buttonCameraSave.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw first step
            x = labelFirstStep.Left - SPACE; y = labelFirstStep.Top - SPACE;
            width = labelFirstStep.Right + SPACE - x;
            height = labelFirstStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = buttonMeasMethodSet.Right + SPACE - x;
            height = buttonMeasMethodSet.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw second step
            x = labelSecondStep.Left - SPACE; y = labelSecondStep.Top - SPACE;
            width = labelSecondStep.Right + SPACE - x;
            height = labelSecondStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = buttonMeasMethodSet.Right + SPACE - x;
            height = labelSecondSecondLine.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw third step
            x = labelThirdStep.Left - SPACE; y = labelThirdStep.Top - SPACE;
            width = labelThirdStep.Right + SPACE - x;
            height = labelThirdStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = buttonMeasMethodSet.Right + SPACE - x;
            height = buttonCancelRefCali.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw forth step
            x = labelForthStep.Left - SPACE; y = labelForthStep.Top - SPACE;
            width = labelForthStep.Right + SPACE - x;
            height = labelForthStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = buttonSaveROI.Right + SPACE - x;
            height = buttonSaveROI.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw fifth step
            x = labelFifthStep.Left - SPACE; y = labelFifthStep.Top - SPACE;
            width = labelFifthStep.Right + SPACE - x;
            height = labelFifthStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = buttonSaveThreshold.Right + SPACE - x;
            height = buttonSaveThreshold.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            //Draw sixth step
            x = labelSixthStep.Left - SPACE; y = labelSixthStep.Top - SPACE;
            width = labelSixthStep.Right + SPACE - x;
            height = labelSixthStep.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
            width = textBoxBarcode.Right + SPACE - x;
            height = buttonMeasure.Bottom + SPACE - y;
            graphics.DrawRectangle(pen, x, y, width, height);
        }
    }
}