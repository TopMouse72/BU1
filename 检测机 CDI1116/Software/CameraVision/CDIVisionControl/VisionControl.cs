using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Colibri.CommonModule;
using Colibri.CommonModule.XML;
using Colibri.CommonModule.Event;
using VisionUnit;
using Colibri.CommonModule.State;
using Colibri.CommonModule.ToolBox;
using Measure;
using CDI;

namespace CDIVisionControl
{
    public enum CalcMode
    {
        Max,
        Avg,
        Min,
    }
    public enum CamCDIRoi
    {
        TopEdge = 0,
        LeftEdge = 1,
        BottomEdge = 2,
        RightEdgeUp = 3,
        RightEdgeMid = 4,
        RightEdgeDn = 5,
        NiTopEdge = 6,
        NiRightEdge = 7,
        NiBottomEdge = 8,
        AlTopEdge = 9,
        AlRightEdge = 10,
        AlBottomEdge = 11,
        NiSealant = 12,
        AlSealant = 13,
    }
    public enum CamCDIRoiName
    {
        上边缘 = 0,
        左边缘 = 1,
        下边缘 = 2,
        右边缘上 = 3,
        右边缘 = 4,
        右边缘下 = 5,
        Ni上边缘 = 6,
        Ni右边缘 = 7,
        Ni下边缘 = 8,
        Al上边缘 = 9,
        Al右边缘 = 10,
        Al下边缘 = 11,
        Ni小白胶阈值 = 12,
        Al小白胶阈值 = 13,
    }
    public enum ProductRoi
    {
        LeftEdge = 0,
        BottomEdge = 1,
    }
    public enum SaveImageOption
    {
        SaveNG,
        SaveAll,
    }
    public enum BufferIndex
    {
        RightCell = 0,
        MiddleCell = 1,
        LeftCell = 2,
        Test = 3,
    }
    public static class VSGlobalControl
    {
        public const double PAT_WIDTH_MM = 3;
        public const double PAT_HEIGHT_MM = 1;
        public static double PAT_FIND_AREA_X_MM;
        public static double PAT_FIND_AREA_Y_MM;
        public static EventHandler ShotOneCallBack;
        public static ClassSystemParameter SysParam = new ClassSystemParameter();
        public static string CurrentProductName { get => SysParam.CurrentProduct; }
        public static string UseGaugeName { get => SysParam.CurrentProductParam.UseGauge; }
        public static Cst.Cam_Param m_CamParam = new Cst.Cam_Param();
        public static Cst.Struct_MeasDatas CurrentCellDataSpec
        {
            get
            {
                if (SysParam.CurrentProduct != "")
                    return SysParam.CurrentProductParam.CellDataSpec;
                else
                    return new Cst.Struct_MeasDatas();
            }
        }
        public static Cst.Struct_VParam m_VParam = new Cst.Struct_VParam();
        public static Cst.Struct_VParam m_VGaugeParam = new Cst.Struct_VParam();
        public static int SettingMode = 0;
        public static bool imagedone = true;
        public static bool bUseProductROI = true;
        public static SaveImageOption SaveOption = SaveImageOption.SaveNG;
        private static string DefaultBackupConfigPath
        {
            get
            {
                string filePath = CommonFunction.DataPath + "ConfigBackUp\\";
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                return filePath;
            }
        }
        public static void CheckAndRestore(string source, string backup)
        {
            if (backup != "")
                if (!File.Exists(source))
                {
                    if (File.Exists(backup))
                        File.Copy(backup, source);
                }
                else
                {
                    if (!File.Exists(backup))
                        File.Copy(source, backup);
                }
        }
        public static void ReadWriteCameraParam(bool bRead = true)
        {
            string ParamFile = "CamParam.xml";
            if (bRead)
            {
                CheckAndRestore(CommonFunction.DefaultConfigPath + ParamFile, DefaultBackupConfigPath + ParamFile);
                m_CamParam.CameraSize.Width = 1280;
                m_CamParam.CameraSize.Height = 1024;
                m_CamParam.CameraKX = 1;
                m_CamParam.CameraKY = 1;
                object temp = new SerialXML().ReadSetting(ParamFile, "CameraPara", typeof(Cst.Cam_Param));
                if (temp != null) m_CamParam = (Cst.Cam_Param)temp;
            }
            else
            {
                new SerialXML().SaveSetting(ParamFile, "CameraPara", m_CamParam, FileMode.Create);
                File.Copy(CommonFunction.DefaultConfigPath + ParamFile, DefaultBackupConfigPath + ParamFile, true);
            }
        }
        private static void LoadPat(PatName pat)
        {
            if (UseGaugeName == "") return;
            string PatFileName = "Pat" + UseGaugeName + pat;
            CheckAndRestore(CommonFunction.DefaultConfigPath + PatFileName + ".mmo", DefaultBackupConfigPath + PatFileName + ".mmo");
            if (File.Exists(CommonFunction.DefaultConfigPath + PatFileName + ".mmo"))
            {
                VSRightCell.Vision_LoadPatTemp(CommonFunction.DefaultConfigPath, PatFileName, (int)pat);
                VSMiddleCell.Vision_LoadPatTemp(CommonFunction.DefaultConfigPath, PatFileName, (int)pat);
                VSLeftCell.Vision_LoadPatTemp(CommonFunction.DefaultConfigPath, PatFileName, (int)pat);
                VSTest.Vision_LoadPatTemp(CommonFunction.DefaultConfigPath, PatFileName, (int)pat);
            }
        }
        public static void ReadWriteProdVisionParam(bool bRead = true)
        {
            string ParamFile = $"VisionParam{CurrentProductName}.xml";
            string GaugeFile = $"VisionGauge{UseGaugeName}.xml";
            if (!bRead)
            {
                File.Copy(CommonFunction.DefaultConfigPath + ParamFile, DefaultBackupConfigPath + ParamFile, true);
                new SerialXML().SaveSetting(ParamFile, "VisionParam" + CurrentProductName, m_VParam, FileMode.Create);
                new SerialXML().SaveSetting(GaugeFile, "VisionGauge" + UseGaugeName, m_VGaugeParam, FileMode.Create);
            }
            else
            {
                CheckAndRestore(CommonFunction.DefaultConfigPath + ParamFile, DefaultBackupConfigPath + ParamFile);
                InitVP();
                if (File.Exists(CommonFunction.DefaultConfigPath + ParamFile))
                {
                    Cst.Struct_Image[] ims = m_VParam.Im;
                    object temp = new SerialXML().ReadSetting(ParamFile, "VisionParam" + CurrentProductName, typeof(Cst.Struct_VParam));
                    if (temp != null) m_VParam = (Cst.Struct_VParam)temp;
                    for (int i = 0; i < (m_VParam.Im != null && m_VParam.Im.Length < ims.Length ? m_VParam.Im.Length : ims.Length); i++)
                        if (m_VParam.Im != null) ims[i] = m_VParam.Im[i];
                    m_VParam.Im = ims;
                }

                m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.Name = CamCDIRoiName.上边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.Name = CamCDIRoiName.左边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.Name = CamCDIRoiName.下边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Name = CamCDIRoiName.右边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Al下边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Al右边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Al上边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Ni下边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Ni右边缘.ToString();
                m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Name = CamCDIRoiName.Ni上边缘.ToString();

                for (int i = 0; i < m_VParam.Im.Length; i++)
                {
                    if (m_VParam.Im[i].EdgeMultiplePoint.Robust.RobustSampleNum == 0)
                        if (i <= (int)CamCDIRoi.RightEdgeDn)
                            m_VParam.Im[i].EdgeMultiplePoint.Robust.RobustSampleNum = 25;
                        else
                            m_VParam.Im[i].EdgeMultiplePoint.Robust.RobustSampleNum = 15;
                    if (m_VParam.Im[i].EdgeMultiplePoint.Robust.MaxRobustCount == 0) m_VParam.Im[i].EdgeMultiplePoint.Robust.MaxRobustCount = 100;
                    if (m_VParam.Im[i].EdgeMultiplePoint.IgnoreLength != 0 && m_VParam.Im[i].EdgeMultiplePoint.IgnoreLength1 == 0)
                        m_VParam.Im[i].EdgeMultiplePoint.IgnoreLength1 = m_VParam.Im[i].EdgeMultiplePoint.IgnoreLength;
                }

                if (File.Exists(CommonFunction.DefaultConfigPath + GaugeFile))
                {
                    object temp = new SerialXML().ReadSetting(GaugeFile, "VisionGauge" + UseGaugeName, typeof(Cst.Struct_VParam));
                    if (temp != null) m_VGaugeParam = (Cst.Struct_VParam)temp;
                    LoadPat(PatName.TopRef);
                    LoadPat(PatName.BottomRef);
                    LoadPat(PatName.TopLeft);
                    LoadPat(PatName.BottomLeft);
                }
                m_VGaugeParam.Im = new Cst.Struct_Image[Enum.GetNames(typeof(CamCDIRoi)).Length];
                m_VGaugeParam.MeasureMethod = m_VParam.MeasureMethod;
                //m_VGaugeParam.UseBottomAsBase = m_VParam.UseBottomAsBase;
                for (int j = 0; j < m_VParam.Im.Length; j++)
                    m_VGaugeParam.Im[j] = m_VGaugeParam.Im[j];
            }
        }
        private enum PatName
        {
            TopRef = 0,
            BottomRef = 1,
            TopLeft = 2,
            BottomLeft = 3,
        }
        public static void SetROIByRef(bool NewPattern)
        {
            if (NewPattern)
            {
                SetPat(PatName.TopRef, m_VGaugeParam.TopRef, 0);
                SetPat(PatName.BottomRef, m_VGaugeParam.BottomRef, 1);
                SetPat(PatName.TopLeft, m_VGaugeParam.TopLeft, 2);
                SetPat(PatName.BottomLeft, m_VGaugeParam.BottomLeft, 3);
            }
            SetROI();
        }
        private static void SetPat(PatName patname, Cst.dPoint patpoint, int PatNo)
        {
            double ratiox = VSGlobalControl.PAT_WIDTH_MM / m_CamParam.CameraKX;
            double ratioy = VSGlobalControl.PAT_HEIGHT_MM / m_CamParam.CameraKY;
            Rectangle r1 = new Rectangle((int)(patpoint.X - ratiox), (int)(patpoint.Y - ratioy), (int)(patpoint.X + ratiox), (int)(patpoint.Y + ratioy));

            VSTest.Vision_CreatRoi(r1);
            VSTest.Vision_SavePatTemp(CommonFunction.DefaultConfigPath, "Pat" + UseGaugeName + patname);
            VSTest.Vision_LoadPatTemp(CommonFunction.DefaultConfigPath, "Pat" + UseGaugeName + patname, PatNo);
            File.Copy(CommonFunction.DefaultConfigPath + "Pat" + UseGaugeName + patname + ".mmo", DefaultBackupConfigPath + "Pat" + UseGaugeName + patname + ".mmo", true);
        }

        public static void SetROI()
        {
            Rectangle temp1 = new Rectangle(), temp2 = new Rectangle();
            #region 电芯ROI设置
            //Top
            temp1.X = (int)(m_VParam.AvgRef.X - (5 + CurrentCellDataSpec.CellLength.Mean) / m_CamParam.CameraKX);
            temp1.Y = (int)(m_VParam.TopRef.Y - 5 / m_CamParam.CameraKY);
            temp1.Width = (int)(m_VParam.AvgRef.X + 5 / m_CamParam.CameraKX);
            temp1.Height = (int)(m_VParam.TopRef.Y + 5 / m_CamParam.CameraKY);
            m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.Rect = temp1;
            //Left
            temp1.Width = (int)(temp1.X + 10 / m_CamParam.CameraKX);
            temp1.Height = (int)(temp1.Y + (10 + CurrentCellDataSpec.CellWidth.Mean) / m_CamParam.CameraKY);
            m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.Rect = temp1;
            //Bottom
            temp1.Y = (int)(temp1.Height - 10 / m_CamParam.CameraKY);
            temp1.Width = (int)(m_VParam.AvgRef.X + 5 / m_CamParam.CameraKX);
            m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.Rect = temp1;
            //Right
            temp1.X = (int)(temp1.Width - 10 / m_CamParam.CameraKX);
            temp1.Y = (int)(m_VParam.TopRef.Y - 5 / m_CamParam.CameraKY);
            m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Rect = temp1;
            //Al Tab
            temp1.X = (int)(m_VParam.AvgRef.X + CurrentCellDataSpec.AlSealantHeight.Mean / m_CamParam.CameraKX);
            temp1.Width = (int)(m_VParam.AvgRef.X + (5 + CurrentCellDataSpec.AlTabLength.Mean) / m_CamParam.CameraKX);
            //Ni Tab
            temp2.X = (int)(m_VParam.AvgRef.X + CurrentCellDataSpec.NiSealantHeight.Mean / m_CamParam.CameraKX);
            temp2.Width = (int)(m_VParam.AvgRef.X + (5 + CurrentCellDataSpec.NiTabLength.Mean) / m_CamParam.CameraKX);
            double TabHeight = CurrentCellDataSpec.TabDistance.Mean / 2;
            if (m_VParam.UseBottomAsBase)
            {
                //Bottom base
                temp1.Height = (int)(m_VParam.BottomRef.Y + 100 - CurrentCellDataSpec.AlTabDistance.Mean / m_CamParam.CameraKY);
                temp1.Y = (int)(temp1.Height - 100 - TabHeight / m_CamParam.CameraKY);
                temp2.Height = (int)(m_VParam.BottomRef.Y + 100 - CurrentCellDataSpec.NiTabDistance.Mean / m_CamParam.CameraKY);
                temp2.Y = (int)(temp2.Height - 100 - TabHeight / m_CamParam.CameraKY);
            }
            else
            {
                //Top base
                temp1.Y = (int)(m_VParam.TopRef.Y - 100 + CurrentCellDataSpec.AlTabDistance.Mean / m_CamParam.CameraKY);
                temp1.Height = (int)(temp1.Y + 100 + TabHeight / m_CamParam.CameraKY);
                temp2.Y = (int)(m_VParam.TopRef.Y - 100 + CurrentCellDataSpec.NiTabDistance.Mean / m_CamParam.CameraKY);
                temp2.Height = (int)(temp2.Y + 100 + TabHeight / m_CamParam.CameraKY);
            }
            m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect = temp1;
            m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect = temp1;
            m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect = temp1;
            m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect = temp2;
            m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect = temp2;
            m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect = temp2;
            #endregion 电芯ROI设置
            #region 标准块ROI设置
            //Top
            temp1.X = (int)(m_VGaugeParam.AvgRef.X - (5 + CurrentCellDataSpec.CellLength.StdMean) / m_CamParam.CameraKX);
            temp1.Y = (int)(m_VGaugeParam.TopRef.Y - 5 / m_CamParam.CameraKY);
            temp1.Width = (int)(m_VGaugeParam.AvgRef.X + 5 / m_CamParam.CameraKX);
            temp1.Height = (int)(m_VGaugeParam.TopRef.Y + 5 / m_CamParam.CameraKY);
            m_VGaugeParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.Rect = temp1;
            //Left
            temp1.Width = (int)(temp1.X + 10 / m_CamParam.CameraKX);
            temp1.Height = (int)(temp1.Y + (10 + CurrentCellDataSpec.CellWidth.StdMean) / m_CamParam.CameraKY);
            m_VGaugeParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.Rect = temp1;
            //Bottom
            temp1.Y = (int)(temp1.Height - 10 / m_CamParam.CameraKY);
            temp1.Width = (int)(m_VGaugeParam.AvgRef.X + 5 / m_CamParam.CameraKX);
            m_VGaugeParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.Rect = temp1;
            //Right
            temp1.X = (int)(temp1.Width - 10 / m_CamParam.CameraKX);
            temp1.Y = (int)(m_VGaugeParam.TopRef.Y - 5 / m_CamParam.CameraKY);
            m_VGaugeParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Rect = temp1;
            //Al Tab
            temp1.X = (int)(m_VGaugeParam.AvgRef.X + CurrentCellDataSpec.AlSealantHeight.StdMean / m_CamParam.CameraKX);
            temp1.Width = (int)(m_VGaugeParam.AvgRef.X + (5 + CurrentCellDataSpec.AlTabLength.StdMean) / m_CamParam.CameraKX);
            //Ni Tab
            temp2.X = (int)(m_VGaugeParam.AvgRef.X + CurrentCellDataSpec.NiSealantHeight.StdMean / m_CamParam.CameraKX);
            temp2.Width = (int)(m_VGaugeParam.AvgRef.X + (5 + CurrentCellDataSpec.NiTabLength.StdMean) / m_CamParam.CameraKX);
            TabHeight = CurrentCellDataSpec.TabDistance.StdMean / 2;
            if (m_VGaugeParam.UseBottomAsBase)
            {
                //Bottom base
                temp1.Height = (int)(m_VGaugeParam.BottomRef.Y + 100 - CurrentCellDataSpec.AlTabDistance.StdMean / m_CamParam.CameraKY);
                temp1.Y = (int)(temp1.Height - 100 - TabHeight / m_CamParam.CameraKY);
                temp2.Height = (int)(m_VGaugeParam.BottomRef.Y + 100 - CurrentCellDataSpec.NiTabDistance.StdMean / m_CamParam.CameraKY);
                temp2.Y = (int)(temp2.Height - 100 - TabHeight / m_CamParam.CameraKY);
            }
            else
            {
                //Top base
                temp1.Y = (int)(m_VGaugeParam.TopRef.Y - 100 + CurrentCellDataSpec.AlTabDistance.StdMean / m_CamParam.CameraKY);
                temp1.Height = (int)(temp1.Y + 100 + TabHeight / m_CamParam.CameraKY);
                temp2.Y = (int)(m_VGaugeParam.TopRef.Y - 100 + CurrentCellDataSpec.NiTabDistance.StdMean / m_CamParam.CameraKY);
                temp2.Height = (int)(temp2.Y + 100 + TabHeight / m_CamParam.CameraKY);
            }
            m_VGaugeParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect = temp1;
            m_VGaugeParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect = temp1;
            m_VGaugeParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect = temp1;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect = temp2;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect = temp2;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect = temp2;
            VSTest.Vision_DrawRect(temp1.X, temp1.Y, temp1.Width, temp1.Height, false, VSBase.COLOR_BLUE);
            VSTest.Vision_DrawRect(temp2.X, temp2.Y, temp2.Width, temp2.Height, false, VSBase.COLOR_YELLOW);
            #endregion 标准块ROI设置
        }
        public static int InitParm()
        {
            ReadWriteCameraParam();
            ReadWriteProdVisionParam();
            SysParam.LoadParameter();
            return 0;
        }

        private static void PreSetROI()
        {
            m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_DECREASE;
            m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
        }
        private static void PreSetGaugeROI()
        {
            m_VGaugeParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_DECREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_DECREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_X_INCREASE;
            m_VGaugeParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Dir = Cst.FIND_DIR_Y_INCREASE;
        }
        public static void InitVP()
        {
            int j;
            m_VParam.Im = new Cst.Struct_Image[Enum.GetNames(typeof(CamCDIRoi)).Length];
            m_VParam.MeasureMethod = MeasMethod.Meas1;
            for (j = 0; j < m_VParam.Im.Length; j++)
            {
                m_VParam.Im[j].BlobSinglePoint = new Cst.Struct_BlobSinglePoint();
                m_VParam.Im[j].Pat = new Cst.Struct_Pat();
                m_VParam.Im[j].Edge = new Cst.Struct_Edge();
                m_VParam.Im[j].EdgeMultiplePoint = new Cst.Struct_EdgeMultiplePoint();
                m_VParam.Im[j].StepMark = new Cst.Struct_StepMark();
                m_VParam.Im[j].StepMarkMultiplePoint = new Cst.Struct_StepMarkMultiplePoint();
                m_VParam.Im[j].RectRGB = new Cst.Struct_RectRGB();
                m_VParam.Im[j].CorNer = new Cst.Struct_CorNer();
                m_VParam.Im[j].Circle = new Cst.Struct_Circle();
                m_VParam.Im[j].ErrBlob = new Cst.Struct_ErrBlob();
                m_VParam.Im[j].bIsUsed = false;
                m_VParam.Im[j].type = 0;

                m_VParam.Im[j].BlobSinglePoint.Rect = Cst.DefaultRect;
                m_VParam.Im[j].BlobSinglePoint.WhiteBlob = true;
                m_VParam.Im[j].BlobSinglePoint.Thres = 128;
                m_VParam.Im[j].BlobSinglePoint.MinArea = 10;
                m_VParam.Im[j].BlobSinglePoint.ModelX = 0.0;
                m_VParam.Im[j].BlobSinglePoint.ModelY = 0.0;

                m_VParam.Im[j].Pat.PatSeachRect = Cst.DefaultRect;
                m_VParam.Im[j].Pat.PatTempRect = Cst.DefaultRect;
                m_VParam.Im[j].Pat.PatAcceptValue = 60.0;
                m_VParam.Im[j].Pat.PatFindAngleFlag = false;
                m_VParam.Im[j].Pat.ModelX = 0.0;
                m_VParam.Im[j].Pat.ModelY = 0.0;

                m_VParam.Im[j].Edge.Rect = Cst.DefaultRect;
                m_VParam.Im[j].Edge.Thres = 128;
                m_VParam.Im[j].Edge.Dir = 1;
                m_VParam.Im[j].Edge.MultiplePoint = false;
                m_VParam.Im[j].Edge.PrintMultiplePoint = false;
                m_VParam.Im[j].Edge.ModelX = 0.0;
                m_VParam.Im[j].Edge.ModelY = 0.0;
                m_VParam.Im[j].Edge.IgnoreLength = 1;

                m_VParam.Im[j].EdgeMultiplePoint.Rect = Cst.DefaultRect;
                m_VParam.Im[j].EdgeMultiplePoint.Thres = 128;
                m_VParam.Im[j].EdgeMultiplePoint.Dir = 1;
                m_VParam.Im[j].EdgeMultiplePoint.PrintMultiplePoint = false;
                m_VParam.Im[j].EdgeMultiplePoint.IgnoreLength = 1;
                m_VParam.Im[j].EdgeMultiplePoint.IgnoreLength1 = 1;
                m_VParam.Im[j].EdgeMultiplePoint.IgnoreLength2 = 1;
                m_VParam.Im[j].EdgeMultiplePoint.Robust.RobustSampleNum = 0;
                m_VParam.Im[j].EdgeMultiplePoint.Robust.MaxRobustCount = 0;

                m_VParam.Im[j].StepMark.Rect = Cst.DefaultRect;
                m_VParam.Im[j].StepMark.Dir = 1;
                m_VParam.Im[j].StepMark.MultiplePoint = false;
                m_VParam.Im[j].StepMark.PrintMultiplePoint = false;
                m_VParam.Im[j].StepMark.ModelX = 0.0;
                m_VParam.Im[j].StepMark.ModelY = 0.0;

                m_VParam.Im[j].StepMarkMultiplePoint.Rect = Cst.DefaultRect;
                m_VParam.Im[j].StepMarkMultiplePoint.Dir = 1;
                m_VParam.Im[j].StepMarkMultiplePoint.PrintMultiplePoint = false;

                m_VParam.Im[j].RectRGB.Rect = Cst.DefaultRect;
                m_VParam.Im[j].RectRGB.R = 0;
                m_VParam.Im[j].RectRGB.G = 0;
                m_VParam.Im[j].RectRGB.B = 0;

                m_VParam.Im[j].CorNer.RectX = Cst.DefaultRect;
                m_VParam.Im[j].CorNer.RectY = Cst.DefaultRect;
                m_VParam.Im[j].CorNer.DirX = 1;
                m_VParam.Im[j].CorNer.DirY = 2;
                m_VParam.Im[j].CorNer.ThresX = 128;
                m_VParam.Im[j].CorNer.ThresY = 128;
                m_VParam.Im[j].CorNer.UseThres = false;
                m_VParam.Im[j].CorNer.ModelX = 0.0;
                m_VParam.Im[j].CorNer.ModelY = 0.0;

                m_VParam.Im[j].Circle.Rect = Cst.DefaultRect;
                m_VParam.Im[j].Circle.Thres = 128;
                m_VParam.Im[j].Circle.ArcStart = 0;
                m_VParam.Im[j].Circle.ArcEnd = 360;
                m_VParam.Im[j].Circle.Dir = 0;
                m_VParam.Im[j].Circle.PrintArcPoint = false;
                m_VParam.Im[j].Circle.ModelX = 0.0;
                m_VParam.Im[j].Circle.ModelY = 0.0;

                m_VParam.Im[j].ErrBlob.Rect = Cst.DefaultRect;
                m_VParam.Im[j].ErrBlob.Thres = 128;
                m_VParam.Im[j].ErrBlob.ErrType = 0;
                m_VParam.Im[j].ErrBlob.Dir = 1;
                m_VParam.Im[j].ErrBlob.MinArea = 10;
                m_VParam.Im[j].ErrBlob.PrintErr = true;
            }
            PreSetROI();
        }

        public static void InitProductParam(string ProductName, bool bRead)
        {
            ProductName = "VisionParameter" + ProductName + ".xml";
            if (bRead)
            {
                object temp = new SerialXML().ReadSetting(ProductName, ProductName, typeof(Cst.Struct_VParam));
                if (temp != null) m_VParam = (Cst.Struct_VParam)temp;
            }
            else
            {
                new SerialXML().SaveSetting(ProductName, ProductName, m_VParam, FileMode.Create);
            }
        }
        private static void DrawRef(bool isCell)
        {
            double width = PAT_WIDTH_MM * 5;
            double height = PAT_HEIGHT_MM * 5;
            Rectangle temp;
            //Left top
            if (isCell)
                temp = new Rectangle(
                    (int)(m_VParam.AvgRef.X - (CurrentCellDataSpec.CellLength.Mean + width) / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.Mean / 2 + height) / m_CamParam.CameraKY),
                    (int)(m_VParam.AvgRef.X - (CurrentCellDataSpec.CellLength.Mean - width) / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.Mean / 2 - height) / m_CamParam.CameraKY)
                    );
            else
                temp = new Rectangle(
                    (int)(m_VGaugeParam.AvgRef.X - (CurrentCellDataSpec.CellLength.StdMean + width) / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.StdMean / 2 + height) / m_CamParam.CameraKY),
                    (int)(m_VGaugeParam.AvgRef.X - (CurrentCellDataSpec.CellLength.StdMean - width) / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.StdMean / 2 - height) / m_CamParam.CameraKY)
                    );
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            //Left bottom
            if (isCell)
                temp = new Rectangle(
                    (int)(m_VParam.AvgRef.X - (CurrentCellDataSpec.CellLength.Mean + width) / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.Mean / 2 - height) / m_CamParam.CameraKY),
                    (int)(m_VParam.AvgRef.X - (CurrentCellDataSpec.CellLength.Mean - width) / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.Mean / 2 + height) / m_CamParam.CameraKY)
                    );
            else
                temp = new Rectangle(
                    (int)(m_VGaugeParam.AvgRef.X - (CurrentCellDataSpec.CellLength.StdMean + width) / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.StdMean / 2 - height) / m_CamParam.CameraKY),
                    (int)(m_VGaugeParam.AvgRef.X - (CurrentCellDataSpec.CellLength.StdMean - width) / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.StdMean / 2 + height) / m_CamParam.CameraKY)
                    );
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            //Right bottom
            if (isCell)
                temp = new Rectangle(
                    (int)(m_VParam.AvgRef.X - width / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.Mean / 2 - height) / m_CamParam.CameraKY),
                    (int)(m_VParam.AvgRef.X + width / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.Mean / 2 + height) / m_CamParam.CameraKY)
                    );
            else
                temp = new Rectangle(
                    (int)(m_VGaugeParam.AvgRef.X - width / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.StdMean / 2 - height) / m_CamParam.CameraKY),
                    (int)(m_VGaugeParam.AvgRef.X + width / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y + (CurrentCellDataSpec.CellWidth.StdMean / 2 + height) / m_CamParam.CameraKY)
                    );
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            //Right top
            if (isCell)
                temp = new Rectangle(
                    (int)(m_VParam.AvgRef.X - width / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.Mean / 2 + height) / m_CamParam.CameraKY),
                    (int)(m_VParam.AvgRef.X + width / m_CamParam.CameraKX),
                    (int)(m_VParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.Mean / 2 - height) / m_CamParam.CameraKY)
                    );
            else
                temp = new Rectangle(
                    (int)(m_VGaugeParam.AvgRef.X - width / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.StdMean / 2 + height) / m_CamParam.CameraKY),
                    (int)(m_VGaugeParam.AvgRef.X + width / m_CamParam.CameraKX),
                    (int)(m_VGaugeParam.AvgRef.Y - (CurrentCellDataSpec.CellWidth.StdMean / 2 - height) / m_CamParam.CameraKY)
                    );
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_YELLOW, Cst.ViewLWidth25);
            //Al Tab
            if (isCell)
                temp = m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect;
            else
                temp = m_VGaugeParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect;
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_BLUE, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_BLUE, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_BLUE, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_BLUE, Cst.ViewLWidth25);
            //Ni Tab
            if (isCell)
                temp = m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect;
            else
                temp = m_VGaugeParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect;
            VSLeftCell.Vision_ShowRect(temp, VSBase.COLOR_GREEN, Cst.ViewLWidth25);
            VSMiddleCell.Vision_ShowRect(temp, VSBase.COLOR_GREEN, Cst.ViewLWidth25);
            VSRightCell.Vision_ShowRect(temp, VSBase.COLOR_GREEN, Cst.ViewLWidth25);
            VSTest.Vision_ShowRect(temp, VSBase.COLOR_GREEN, Cst.ViewLWidth25);

            //Ref line
            //VSLeftCell.Vision_DrawLine((int)m_VParam.AvgRef.X, 0, (int)m_VParam.AvgRef.X, m_CamParam.CameraSize.Height, VSBase.COLOR_CYAN, Cst.ViewLWidth25);
            //VSMiddleCell.Vision_DrawLine((int)m_VParam.AvgRef.X, 0, (int)m_VParam.AvgRef.X, m_CamParam.CameraSize.Height, VSBase.COLOR_CYAN, Cst.ViewLWidth25);
            //VSRightCell.Vision_DrawLine((int)m_VParam.AvgRef.X, 0, (int)m_VParam.AvgRef.X, m_CamParam.CameraSize.Height, VSBase.COLOR_CYAN, Cst.ViewLWidth25);
            if (isCell)
                VSTest.Vision_DrawLine((int)(m_VParam.AvgRef.X - CurrentCellDataSpec.CellLength.Mean / m_CamParam.CameraKX), 0,
                    (int)(m_VParam.AvgRef.X - CurrentCellDataSpec.CellLength.Mean / m_CamParam.CameraKX), m_CamParam.CameraSize.Height, VSBase.COLOR_CYAN, Cst.ViewLWidth25);
            else
                VSTest.Vision_DrawLine((int)(m_VGaugeParam.AvgRef.X - CurrentCellDataSpec.CellLength.StdMean / m_CamParam.CameraKX), 0,
                 (int)(m_VGaugeParam.AvgRef.X - CurrentCellDataSpec.CellLength.StdMean / m_CamParam.CameraKX), m_CamParam.CameraSize.Height, VSBase.COLOR_CYAN, Cst.ViewLWidth25);
        }
        public static void CleanAllTestMark(bool drawROI = true, bool isCell = true)
        {
            VSRightCell.Vision_MarkClean();
            VSMiddleCell.Vision_MarkClean();
            VSLeftCell.Vision_MarkClean();
            VSTest.Vision_MarkClean();
            if (drawROI)
            {
                DrawRef(isCell);
            }
        }
    }
    public class VisionControl : VisionSubscriber
    {
        public string szFilePath = CommonFunction.DataPath + "pic\\";
        public ResponsePublisher ResPublisher = new ResponsePublisher();
        private Cst.dPoint point;
        private const int MEAS_ROI_OUT_PIXEL = 10;
        private const int DISTCOUNT_IGNORE = 5, DISTCOUNT_CALC = 20;
        private const int SEALANT_WIDTH_PIXEL = 40;
        private const int ROI_BASE_WIDTH_MM = 45;
        private const int RIGHTEDGE_SHOLDER_MM = 3;
        private const double TOP_SHOLDER_WIDTH_MM = 2;
        private const double BOTTOM_SHOLDER_WIDTH_MM = 2;
        private const double LEFT_SHOLDER_WIDTH_MM = 3;

        private Cst.Struct_Line tempLine;
        private Cst.Struct_Line CellLBase = new Cst.Struct_Line(), CellWBase = new Cst.Struct_Line();
        private Cst.Struct_Line CellLMeas = new Cst.Struct_Line(), CellWMeas = new Cst.Struct_Line();
        private Cst.dPoint PointCellLT, PointCellRT, PointCellLB, PointCellRB;
        private Cst.dPoint PointAlTabLT, PointAlTabRT, PointAlTabLB, PointAlTabRB;
        private Cst.dPoint PointNiTabLT, PointNiTabLB, PointNiTabRT, PointNiTabRB;
        private double PointNumB, PointNumD;

        public string Barcode;
        private Rectangle[] EdgeRect = new Rectangle[VSGlobalControl.m_VParam.Im.Length];
        public delegate Cst.Struct_MeasDatas MeasureDel(string barcode);
        public MeasureDel AsyncMeasure;
        private BufferIndex _vs;
        public Cst.Cam_Param m_CamParam = VSGlobalControl.m_CamParam;

        public Cst.Struct_MeasDatas CellDataSpec = new Cst.Struct_MeasDatas();

        private bool FindLBase = true;
        private bool FindWBase = true;

        public int CamRunNum = 0;
        private Cst.Struct_Line TempBase;


        public int imgWidth
        {
            get { return m_CamParam.CameraSize.Width; }
        }
        public int imgHeight
        {
            get { return m_CamParam.CameraSize.Height; }
        }
        public bool VSCamTrig = false;
        public VisionControl(BufferIndex vsindex)
        {
            _vs = vsindex;
        }
        public void VisionInit(CameraDispControl CamDisplay, GetMousePosition ShowPosition)
        {
            bool res = true;
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    res = VSRightCell.VisionInit(CamDisplay, VSGlobalControl.m_CamParam.CameraSize.Width, VSGlobalControl.m_CamParam.CameraSize.Height, 1, ShowPosition) == 0;
                    break;
                case BufferIndex.MiddleCell:
                    res = VSMiddleCell.VisionInit(CamDisplay, VSGlobalControl.m_CamParam.CameraSize.Width, VSGlobalControl.m_CamParam.CameraSize.Height, 1, ShowPosition) == 0;
                    break;
                case BufferIndex.LeftCell:
                    res = VSLeftCell.VisionInit(CamDisplay, VSGlobalControl.m_CamParam.CameraSize.Width, VSGlobalControl.m_CamParam.CameraSize.Height, 1, ShowPosition) == 0;
                    break;
                case BufferIndex.Test:
                    res = VSTest.VisionInit(CamDisplay, VSGlobalControl.m_CamParam.CameraSize.Width, VSGlobalControl.m_CamParam.CameraSize.Height, 1, ShowPosition) == 0;
                    break;
            }
            if (!res) MessageBox.Show(_vs.ToString() + "图像初始化失败。");
        }
        private double CalPointToLine(Cst.Struct_Line Line, Cst.dPoint point, bool isXLine)
        {
            Cst.Struct_Line tempLine = new Cst.Struct_Line();
            tempLine.AvgX = point.X;
            tempLine.AvgY = point.Y;
            tempLine.k = (-1) / (Line.k + 0.000001);
            tempLine.b = tempLine.AvgY - tempLine.k * tempLine.AvgX;
            Cst.dPoint crosspoint = new Cst.dPoint();
            TwoLineCrossPoint(Line, tempLine, ref crosspoint);
            double tempdis;
            double distance;
            tempdis = Math.Sqrt((point.X - crosspoint.X) * (point.X - crosspoint.X) + (point.Y - crosspoint.Y) * (point.Y - crosspoint.Y));
            if (!isXLine)
            {

                if (point.X > crosspoint.X)
                {
                    distance = tempdis;
                }
                else
                    distance = (-1) * tempdis;

            }
            else
            {
                if (point.Y > crosspoint.Y)
                {
                    distance = tempdis;
                }
                else
                    distance = (-1) * tempdis;

            }

            return distance;
        }
        public void Vision_DrawRect(Rectangle Rect, int Color, int LWidth = Cst.ViewLWidth100)
        {
            if (Rect.X < 0) Rect.X = 0;
            if (Rect.X > VSGlobalControl.m_CamParam.CameraSize.Width) Rect.X = VSGlobalControl.m_CamParam.CameraSize.Width;
            if (Rect.Y < 0) Rect.Y = 0;
            if (Rect.Y > VSGlobalControl.m_CamParam.CameraSize.Height) Rect.Y = VSGlobalControl.m_CamParam.CameraSize.Height;
            if (Rect.Width < 0) Rect.Width = 0;
            if (Rect.Width > VSGlobalControl.m_CamParam.CameraSize.Width) Rect.Width = VSGlobalControl.m_CamParam.CameraSize.Width;
            if (Rect.Height < 0) Rect.Height = 0;
            if (Rect.Height > VSGlobalControl.m_CamParam.CameraSize.Height) Rect.Height = VSGlobalControl.m_CamParam.CameraSize.Height;
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Vision_ShowRect(Rect, Color, LWidth);
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Vision_ShowRect(Rect, Color, LWidth);
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Vision_ShowRect(Rect, Color, LWidth);
                    break;

                case BufferIndex.Test:
                    VSTest.Vision_ShowRect(Rect, Color, LWidth);
                    break;
            }
        }
        private void Vision_MarkClean()
        {
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Vision_MarkClean();
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Vision_MarkClean();
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Vision_MarkClean();
                    break;

                case BufferIndex.Test:
                    VSTest.Vision_MarkClean();
                    break;
            }
        }
        public int Zoom
        {
            get
            {
                switch (_vs)
                {
                    case BufferIndex.RightCell: return VSRightCell.Zoom;
                    case BufferIndex.MiddleCell: return VSMiddleCell.Zoom;
                    case BufferIndex.LeftCell: return VSLeftCell.Zoom;
                    case BufferIndex.Test: return VSTest.Zoom;
                    default: return 100;
                }
            }
        }
        public void Vision_DrawLine(int StartX, int StartY, int EndX, int EndY, int Color, int LWidth = Cst.ViewLWidth100)
        {
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Vision_DrawLine(StartX, StartY, EndX, EndY, Color, LWidth);
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Vision_DrawLine(StartX, StartY, EndX, EndY, Color, LWidth);
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Vision_DrawLine(StartX, StartY, EndX, EndY, Color, LWidth);
                    break;

                case BufferIndex.Test:
                    VSTest.Vision_DrawLine(StartX, StartY, EndX, EndY, Color, LWidth);
                    break;
            }
        }
        public void Vision_DrawCross(int x, int y, int r, int Color, int LWidth = Cst.ViewLWidth100)
        {
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Vision_DrawCross(x, y, r, Color, LWidth);
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Vision_DrawCross(x, y, r, Color, LWidth);
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Vision_DrawCross(x, y, r, Color, LWidth);
                    break;

                case BufferIndex.Test:
                    VSTest.Vision_DrawCross(x, y, r, Color, LWidth);
                    break;
            }
        }
        private void TwoLineCrossPoint(Cst.Struct_Line FirstLine, Cst.Struct_Line SecondLine, ref Cst.dPoint crossPoint, bool draw = false)
        {
            if (double.IsInfinity(FirstLine.k))
            {
                crossPoint.X = FirstLine.AvgX;
                crossPoint.Y = SecondLine.AvgY;
            }
            else if (double.IsInfinity(SecondLine.k))
            {
                crossPoint.X = SecondLine.AvgX;
                crossPoint.Y = FirstLine.AvgY;
            }
            else
            {
                crossPoint.X = (SecondLine.b - FirstLine.b) / (FirstLine.k - SecondLine.k);
                crossPoint.Y = crossPoint.X * FirstLine.k + FirstLine.b;
            }
            if (draw)
                Vision_DrawCross((int)crossPoint.X, (int)crossPoint.Y, 100, VSBase.COLOR_FUCHSIA);
        }

        private double GetStdev(Cst.dPoint[] data, bool isXLine)
        {
            double sum = 0;
            int count = data.Length;
            for (int i = 0; i < count - 1; i++)
                sum += isXLine ? data[i].Y : data[i].X;
            double avg = sum / count;
            sum = 0;
            for (int i = 0; i < count - 1; i++)
                sum += Math.Pow((isXLine ? data[i].Y : data[i].X) - avg, 2);
            return Math.Sqrt(sum / (count - 1));
        }
        private Cst.Struct_Line CalLine(Cst.dPoint[] v, bool isXLine)
        {
            Cst.Struct_Line OutLine = new Cst.Struct_Line();
            int i;
            double count = 0;
            double sumx, sumy, sumxy, sumx2, sumy2;
            Cst.dPoint tempP;
            bool bOnePoint = true;
            double Ang = 0.0;

            sumx = 0.0;
            sumy = 0.0;
            sumxy = 0.0;
            sumx2 = 0.0;
            sumy2 = 0.0;
            for (i = 0; i < v.Length; i++)
            {
                sumx += v[i].X;
                sumy += v[i].Y;
                sumxy += (v[i].X * v[i].Y);
                sumx2 += (v[i].X * v[i].X);
                sumy2 += (v[i].Y * v[i].Y);
                count++;
            }
            OutLine.AvgX = sumx / count;
            OutLine.AvgY = sumy / count;
            if (isXLine)
            {
                OutLine.k = (sumxy - sumx * sumy / count) / (sumx2 - sumx * sumx / count);
                if (double.IsNaN(OutLine.k)) OutLine.k = 99999999;
            }
            else
            {
                OutLine.k = (sumxy - sumx * sumy / count) / (sumy2 - sumy * sumy / count);
                if (OutLine.k == 0)
                    OutLine.k = 99999999;
                else if (double.IsNaN(OutLine.k))
                    OutLine.k = 0;
                else
                    OutLine.k = 1 / OutLine.k;
            }
            OutLine.b = OutLine.AvgY - OutLine.k * OutLine.AvgX;
            Ang = Math.Atan(OutLine.k);

            if (sumxy - sumx * sumy / count == 0 && sumx2 - sumx * sumx / count == 0)
            {
                tempP = v[0];
                for (i = 1; i < v.Length; i++)
                {
                    if (tempP.X != v[i].X || tempP.Y != v[i].Y)
                    {
                        bOnePoint = false;
                        break;
                    }
                }
                if (!bOnePoint)
                {
                    Ang = Math.Atan2(v[i].Y - tempP.Y, v[i].X - tempP.X);
                }
            }
            OutLine.RAngle = Ang;
            OutLine.DAngle = -Ang * 180 / Math.PI;
            if (OutLine.DAngle < 0) OutLine.DAngle = 180 + OutLine.DAngle;
            return OutLine;

        }
        private List<Cst.dPoint> GetLinePoints(Cst.Struct_Image ImagePara)
        {
            return GetEdgePoints(ImagePara.EdgeMultiplePoint.Rect, ImagePara.EdgeMultiplePoint.Dir, ImagePara.EdgeMultiplePoint.PrintMultiplePoint);
        }
        private List<Cst.dPoint> GetLinePoints(Cst.Struct_Image ImagePara, Rectangle Rect)
        {
            return GetEdgePoints(Rect, ImagePara.EdgeMultiplePoint.Dir, ImagePara.EdgeMultiplePoint.PrintMultiplePoint);
        }
        private List<Cst.dPoint> GetEdgePoints(Rectangle Rect, int Dir, bool PrintMark)
        {
            int tempsize = 0;
            Cst.dPoint tempPoint = new Cst.dPoint();
            List<Cst.dPoint> point = new List<Cst.dPoint>();
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    tempsize = VSRightCell.Vision_FindStepMarkMultiplePoint(Rect, Dir, PrintMark);
                    break;
                case BufferIndex.MiddleCell:
                    tempsize = VSMiddleCell.Vision_FindStepMarkMultiplePoint(Rect, Dir, PrintMark);
                    break;
                case BufferIndex.LeftCell:
                    tempsize = VSLeftCell.Vision_FindStepMarkMultiplePoint(Rect, Dir, PrintMark);
                    break;
                case BufferIndex.Test:
                    tempsize = VSTest.Vision_FindStepMarkMultiplePoint(Rect, Dir, PrintMark);
                    break;
            }
            if (tempsize > 3)
            {
                for (int i = 0; i < tempsize; i++)
                {
                    switch (_vs)
                    {
                        case BufferIndex.RightCell:
                            VSRightCell.Vision_GetStepMarkMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;
                        case BufferIndex.MiddleCell:
                            VSMiddleCell.Vision_GetStepMarkMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;
                        case BufferIndex.LeftCell:
                            VSLeftCell.Vision_GetStepMarkMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;
                        case BufferIndex.Test:
                            VSTest.Vision_GetStepMarkMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;
                    }

                    point.Add(tempPoint);
                }
            }
            point = Sort(point, IsXLine(Dir));
            return point;
        }
        private List<Cst.dPoint> GetEdgePoints(Rectangle Rect, int Thres, int Dir, bool PrintMark)
        {
            int tempsize = 0;
            Cst.dPoint tempPoint = new Cst.dPoint();
            List<Cst.dPoint> point = new List<Cst.dPoint>();
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    tempsize = VSRightCell.Vision_FindEdgeMultiplePoint(Rect, Thres, Dir, PrintMark);
                    break;

                case BufferIndex.MiddleCell:
                    tempsize = VSMiddleCell.Vision_FindEdgeMultiplePoint(Rect, Thres, Dir, PrintMark);
                    break;

                case BufferIndex.LeftCell:
                    tempsize = VSLeftCell.Vision_FindEdgeMultiplePoint(Rect, Thres, Dir, PrintMark);
                    break;

                case BufferIndex.Test:
                    tempsize = VSTest.Vision_FindEdgeMultiplePoint(Rect, Thres, Dir, PrintMark);
                    break;
            }

            if (tempsize > 3)
            {
                for (int i = 0; i < tempsize; i++)
                {
                    switch (_vs)
                    {
                        case BufferIndex.RightCell:
                            VSRightCell.Vision_GetEdgeMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;

                        case BufferIndex.MiddleCell:
                            VSMiddleCell.Vision_GetEdgeMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;

                        case BufferIndex.LeftCell:
                            VSLeftCell.Vision_GetEdgeMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;

                        case BufferIndex.Test:
                            VSTest.Vision_GetEdgeMultiplePoint(i, ref tempPoint.X, ref tempPoint.Y);
                            break;
                    }

                    point.Add(tempPoint);
                }
            }
            point = Sort(point, IsXLine(Dir));
            return point;
        }
        private List<Cst.dPoint> GetAvaliablePoint(Cst.dPoint[] points, Cst.Struct_Line line, out int[] index, bool isXLine, double tolerance = 0)
        {
            List<Cst.dPoint> avaliable = new List<Cst.dPoint>();
            List<int> iii = new List<int>();
            double temp;
            double tol;
            if (isXLine) tol = tolerance / m_CamParam.CameraKY;
            else tol = tolerance / m_CamParam.CameraKX;
            for (int i = 0; i < points.Length; i++)
            {
                temp = Math.Abs(CalPointToLine(line, points[i], isXLine));
                if (tolerance == 0 || temp <= tol)
                {
                    avaliable.Add(points[i]);
                    iii.Add(i);
                }
            }
            index = iii.ToArray();
            return avaliable;
        }
        private double GetYSquareError(Cst.dPoint[] points, Cst.Struct_Line line)
        {
            double error = 0;
            double temp;
            for (int i = 0; i < points.Length; i++)
            {
                temp = Math.Pow(Math.Abs(CalPointToLine(line, points[i], true)), 2);
                error += temp;
            }
            return error / points.Length;
        }
        private double GetYTolerance(Cst.dPoint[] points, Cst.Struct_Line line)
        {
            double error = 0;
            double temp;
            for (int i = 0; i < points.Length; i++)
            {
                temp = points[i].Y - (points[i].X * line.k + line.b);
                temp = temp * temp;
                if (temp > error) error = temp;
            }
            return error;
        }
        private List<Cst.dPoint> GetSubSetPoints(Cst.dPoint[] points, int start)
        {
            bool same = true;
            List<Cst.dPoint> temp = new List<Cst.dPoint>();
            for (int i = start; i < start + SubPointSize; i++)
                if (i < points.Length) temp.Add(points[i]);
            for (int i = 0; i < temp.Count; i++)
            {
                if (i > 0 && temp[i].X != temp[i - 1].X) same = false;
            }
            if (!same)
                return temp;
            else
                return null;
        }
        int SubPointSize = 0;
        private List<Cst.dPoint> Sort(List<Cst.dPoint> points, bool isXLine)
        {
            List<Cst.dPoint> target = new List<Cst.dPoint>();
            while (points.Count > 0)
            {
                int minindex = 0;
                for (int i = 1; i < points.Count; i++)
                    if (isXLine)
                    {
                        if (points[minindex].X > points[i].X)
                            minindex = i;
                    }
                    else
                    {
                        if (points[minindex].Y > points[i].Y)
                            minindex = i;
                    }
                target.Add(points[minindex]);
                points.Remove(points[minindex]);
            }
            return target;
        }
        private List<double> Sort(List<double> value)
        {
            List<double> target = new List<double>();
            while (value.Count > 0)
            {
                int minindex = 0;
                for (int i = 1; i < value.Count; i++)
                {
                    if (value[minindex] > value[i])
                        minindex = i;
                }
                target.Add(value[minindex]);
                value.Remove(value[minindex]);
            }
            return target;
        }
        private List<PointDist> Sort(List<PointDist> value)
        {
            List<PointDist> target = new List<PointDist>();
            while (value.Count > 0)
            {
                int minindex = 0;
                for (int i = 1; i < value.Count; i++)
                {
                    if (value[minindex].Distance > value[i].Distance)
                        minindex = i;
                }
                target.Add(value[minindex]);
                value.Remove(value[minindex]);
            }
            return target;
        }
        public Cst.Struct_Line GetLineByRobust(Cst.dPoint[] points, bool isXLine, Cst.Struct_Robust Robust, List<Cst.dPoint> AvailablePoints, double tolerance = 0)
        {
            Cst.dPoint[] BestPoints, temp;
            if (points.Length == 0) return Cst.Struct_Line.EmptyLine;
            Cst.Struct_Line RobustLine = CalLine(points, isXLine);
            Cst.Struct_Line BestLine = RobustLine;
            if (Robust.RobustSampleNum == 0)
            {
                for (int i = 0; i < points.Length; i++) AvailablePoints.Add(points[i]);
                return BestLine;
            }
            double MinYSquareError = GetYSquareError(points, RobustLine);
            double YError;
            int[] iii;
            List<Cst.dPoint> subSetPoints;
            if (tolerance == 0) tolerance = m_CamParam.LineTolerance;
            AvailablePoints.AddRange(GetAvaliablePoint(points, RobustLine, out iii, isXLine, tolerance));
            int MaxSize = AvailablePoints.Count, SubSize = 0;
            int RobustCount = points.Length / Robust.RobustSampleNum;
            int RobustStep = 1;
            if (RobustCount > Robust.MaxRobustCount)
            {
                RobustCount = Robust.MaxRobustCount;
                RobustStep = points.Length / (Robust.RobustSampleNum * RobustCount);
            }
            for (int i = 0; i < RobustCount; i++)
            {
                subSetPoints = new List<Cst.dPoint>();
                for (int j = 0; j < Robust.RobustSampleNum; j++)
                    //subSetPoints.Add(points[i * RobustStep + points.Length * j / Robust.RobustSampleNum]);
                    //if ((i * RobustStep + j) * Robust.RobustSampleNum < points.Length)
                    //subSetPoints.Add(points[(i * RobustStep + j) * Robust.RobustSampleNum]);
                    if (Robust.RobustSampleNum * RobustStep * i + i * RobustStep + j < points.Length)
                        subSetPoints.Add(points[Robust.RobustSampleNum * RobustStep * i + i * RobustStep + j]);
                    else
                        subSetPoints.Add(points[points.Length - 1]);
                RobustLine = CalLine(subSetPoints.ToArray(), isXLine);
                YError = GetYSquareError(subSetPoints.ToArray(), RobustLine);
                temp = subSetPoints.ToArray();
                subSetPoints = GetAvaliablePoint(points, RobustLine, out iii, isXLine, m_CamParam.LineTolerance);
                SubSize = subSetPoints.Count;
                if (SubSize > 1)
                {
                    Cst.dPoint avg = Cst.dPoint.GetAvg(subSetPoints.ToArray());
                    //if (MaxSize == 0) MaxSize = SubSize;
                    //if (YError >= 0 && YError / SubSize < MinYSquareError)
                    if (MaxSize < SubSize /* || MaxSize == SubSize && (YError >= 0 && YError / SubSize < MinYSquareError)*/)
                    {
                        MaxSize = SubSize;
                        MinYSquareError = YError / SubSize;
                        BestLine = RobustLine;
                        //BestLine.AvgX = avg.X;
                        //BestLine.AvgY = avg.Y;
                        AvailablePoints.Clear();
                        AvailablePoints.AddRange(subSetPoints);
                        BestPoints = temp;
                    }
                }
            }
            BestLine.isXLine = isXLine;
            return BestLine;
        }

        public Cst.Struct_Line GetOneLine(Cst.Struct_Image ImagePara, Rectangle Rect, double tolerance = 0)
        {
            return GetOneLine(Rect, ImagePara.EdgeMultiplePoint.Dir, ImagePara.EdgeMultiplePoint.PrintMultiplePoint, ImagePara.EdgeMultiplePoint.Robust, tolerance);
        }

        public Cst.Struct_Line GetOneLine(Cst.Struct_Image ImagePara, double tolerance = 0)
        {
            return GetOneLine(ImagePara.EdgeMultiplePoint.Rect, ImagePara.EdgeMultiplePoint.Dir, ImagePara.EdgeMultiplePoint.PrintMultiplePoint, ImagePara.EdgeMultiplePoint.Robust, tolerance);
        }
        public Cst.Struct_Line GetOneLine(Rectangle Rect, int Thres, int Dir, bool PrintMark, Cst.Struct_Robust Robust, double tolerance = 0)
        {
            List<Cst.dPoint> points = new List<Cst.dPoint>();
            points = GetEdgePoints(Rect, Thres, Dir, PrintMark);
            return GetOneLine(points, Dir, Robust, tolerance);
        }
        public Cst.Struct_Line GetOneLine(Rectangle Rect, int Dir, bool PrintMark, Cst.Struct_Robust Robust, double tolerance = 0)
        {
            List<Cst.dPoint> points = new List<Cst.dPoint>();
            points = GetEdgePoints(Rect, Dir, PrintMark);
            return GetOneLine(points, Dir, Robust, tolerance);
        }
        private Cst.Struct_Line GetOneLine(List<Cst.dPoint> points, int Dir, Cst.Struct_Robust Robust, double tolerance = 0)
        {
            bool isXLine = IsXLine(Dir);
            Cst.Struct_Line Line = new Cst.Struct_Line();
            List<Cst.dPoint> AvaliablePoints = new List<Cst.dPoint>();
            if (points.Count > 0)
            {
                Line = this.GetLineByRobust(points.ToArray(), isXLine, Robust, AvaliablePoints, tolerance);
                //Line = CalLine(points.ToArray(), isXLine);
            }
            return Line;
        }
        public void Vision_DrawLine(Cst.Struct_Line Line, int color, int LWidth = Cst.ViewLWidth100)
        {
            double tempk = Line.k + 0.0001;
            if (Line.isXLine)
                Vision_DrawLine(0, (int)Line.b, m_CamParam.CameraSize.Width, (int)(m_CamParam.CameraSize.Width * tempk + Line.b), color, LWidth);
            else
                Vision_DrawLine((int)(-Line.b / tempk), 0, (int)((m_CamParam.CameraSize.Height - Line.b) / tempk), m_CamParam.CameraSize.Height, color, LWidth);
        }
        public static bool IsXLine(int Dir)
        {
            return Dir == Cst.FIND_DIR_Y ||
                Dir == Cst.FIND_DIR_Y_DECREASE ||
                Dir == Cst.FIND_DIR_Y_INCREASE;
        }
        public Cst.Struct_Line GetOneLineOuterPoint(Cst.Struct_Image ImageParas, Rectangle Rect)
        {
            List<Cst.dPoint> point = new List<Cst.dPoint>();
            List<Cst.dPoint> tempPoint = new List<Cst.dPoint>();
            Cst.Struct_Line Line = new Cst.Struct_Line();
            int Dir = ImageParas.EdgeMultiplePoint.Dir;
            bool isXLine = IsXLine(Dir);

            tempPoint = GetLinePoints(ImageParas, Rect);
            Line = CalLine(tempPoint.ToArray(), isXLine);
            point = tempPoint;
            double PtX = 0, PtY = 0;

            for (int i = 0; i < point.Count; i++)
            {
                PtX += point[i].X;
                PtY += point[i].Y;
            }
            PtX /= point.Count;
            PtY /= point.Count;

            //Line = TempBase;
            Line.AvgX = PtX;
            Line.AvgY = PtY;
            Line.b = PtY - Line.k * PtX;
            if (Line.DAngle < 0) Line.DAngle = 180 + Line.DAngle;
            //edgeMethod = temp;
            return Line;
        }
        public void VerifyAngle(ref double angle)
        {
            if (Math.Abs(angle) > Math.PI)
            {
                if (angle > Math.PI / 2)
                {
                    angle = angle - Math.PI / 2;
                }
                else
                    angle = angle + Math.PI / 2;
            }
        }

        private void CalCrossPoint()
        {
            Cst.Struct_Line TopBase;
            Cst.Struct_Line BottomBase;
            Cst.Struct_Line LeftBase;
            Cst.Struct_Line RightBase;
            Cst.Struct_Line NiTopBase;
            Cst.Struct_Line NiRightBase;
            Cst.Struct_Line NiBottomBase;
            Cst.Struct_Line AlTopBase;
            Cst.Struct_Line AlRightBase;
            Cst.Struct_Line AlBottomBase;
            //取上边缘
            TopBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge]);
            //取左边缘
            LeftBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge]);
            //取下边缘
            BottomBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge]);
            //TabWBase = BottomBase;

            //取右边缘
            Cst.Struct_Image[] points = new Cst.Struct_Image[3];
            points[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp];
            points[1] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid];
            points[2] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn];
            Rectangle[] tempRect = new Rectangle[3];
            tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.Rect;
            tempRect[1] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Rect;
            tempRect[2] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.Rect;
            RightBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid]);
            TempBase = RightBase;
            //取Ni上边缘
            if (MeasStd)
                tempRect[0] = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect;
            else
                tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            NiTopBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_BLUE);
            //取Ni下边缘
            if (MeasStd)
                tempRect[0] = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect;
            else
                tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            NiBottomBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_GREEN);
            //取Al上边缘
            if (MeasStd)
                tempRect[0] = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect;
            else
                tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            AlTopBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_BLUE);
            //取Al下边缘
            if(MeasStd)
                tempRect[0] = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect;
            else
                tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            AlBottomBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_GREEN);

            TwoLineCrossPoint(TopBase, LeftBase, ref PointCellLT, true);
            TwoLineCrossPoint(BottomBase, LeftBase, ref PointCellLB, true);
            TwoLineCrossPoint(BottomBase, RightBase, ref PointCellRB, true);
            TwoLineCrossPoint(TopBase, RightBase, ref PointCellRT, true);
            TwoLineCrossPoint(NiTopBase, RightBase, ref PointNiTabLT, true);
            TwoLineCrossPoint(NiBottomBase, RightBase, ref PointNiTabLB, true);
            TwoLineCrossPoint(AlTopBase, RightBase, ref PointAlTabLT, true);
            TwoLineCrossPoint(AlBottomBase, RightBase, ref PointAlTabLB, true);

            //取Ni右边缘
            tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            tempRect[0].Y = (int)NiTopBase.AvgY;
            tempRect[0].Height = (int)NiBottomBase.AvgY;
            NiRightBase = GetOneLineOuterPoint(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_YELLOW);
            //取Al右边缘
            tempRect[0] = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect;
            tempRect[0].X = (int)(RightBase.AvgX + 2 / m_CamParam.CameraKX);
            tempRect[0].Y = (int)AlTopBase.AvgY;
            tempRect[0].Height = (int)AlBottomBase.AvgY;
            AlRightBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge], tempRect[0]);
            Vision_DrawRect(tempRect[0], VSBase.COLOR_YELLOW);

            TwoLineCrossPoint(NiTopBase, NiRightBase, ref PointNiTabRT, true);
            TwoLineCrossPoint(NiBottomBase, NiRightBase, ref PointNiTabRB, true);
            TwoLineCrossPoint(AlTopBase, AlRightBase, ref PointAlTabRT, true);
            TwoLineCrossPoint(AlBottomBase, AlRightBase, ref PointAlTabRB, true);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.SecondEndPoint = PointCellRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.SecondEndPoint = PointCellLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.SecondEndPoint = PointCellRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.FirstEndPoint = PointCellRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.SecondEndPoint = (PointAlTabLT.Y < PointNiTabLT.Y ? PointAlTabLT : PointNiTabLT);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.FirstEndPoint = PointCellRT;// (AlTabLT.Y < NiTabLT.Y ? AlTabLB : NiTabLB);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.SecondEndPoint = PointCellRB;//(AlTabLT.Y < NiTabLT.Y ? NiTabLT : AlTabLT);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.FirstEndPoint = (PointAlTabLT.Y < PointNiTabLT.Y ? PointNiTabLB : PointAlTabLB);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.SecondEndPoint = PointCellRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRT;
        }

        public void GetBaseWidth(CalcMode mode)
        {
            Cst.Struct_EdgeMultiplePoint BaseEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                BaseEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;
            else
                BaseEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();//临时rect
            int tempSize;
            List<Cst.dPoint> vPoint = new List<Cst.dPoint>();

            List<double> vDis = new List<double>();
            //获取上边缘基准检测区域
            tempRect.X = (int)(BaseEdge.FirstEndPoint.X + (BaseEdge.IgnoreLength1 / m_CamParam.CameraKX + 3));
            tempRect.Width = (int)(BaseEdge.SecondEndPoint.X - (BaseEdge.IgnoreLength2 / m_CamParam.CameraKX + 3));
            if (BaseEdge.FirstEndPoint.Y > BaseEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(BaseEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(BaseEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                tempRect.Y = (int)(BaseEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(BaseEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }

            //获取检测点
            vPoint.Clear();
            tempSize = GetBaseLine(tempRect, BaseEdge, vPoint, out BatBaseH);
            if (tempSize < 3)
            {
                FindWBase = false;
                return;
            }

            //获取所有偏差
            //vDis.Clear();
            double tempdis = 0;
            double min = 9999;
            double max = -9999;
            CellWBase = BatBaseH;
            switch (mode)
            {
                case CalcMode.Min:
                    for (int i = 0; i < vPoint.Count; i++)//计算vPoint所有点到拟合直线的距离
                    {
                        tempdis = CalPointToLine(BatBaseH, vPoint[i], true);
                        if (tempdis < min)
                        {
                            min = tempdis;
                            CellWBase.AvgX = vPoint[i].X;
                            CellWBase.AvgY = vPoint[i].Y;
                            CellWBase.b = CellWBase.AvgY - CellWBase.AvgX * CellWBase.k;
                        }
                    }
                    break;
                case CalcMode.Max:
                    for (int i = 0; i < vPoint.Count; i++)//计算vPoint所有点到拟合直线的距离
                    {
                        tempdis = CalPointToLine(BatBaseH, vPoint[i], true);
                        if (tempdis > max)
                        {
                            max = tempdis;
                            CellWBase.AvgX = vPoint[i].X;
                            CellWBase.AvgY = vPoint[i].Y;
                            CellWBase.b = CellWBase.AvgY - CellWBase.AvgX * CellWBase.k;
                        }
                    }
                    break;
            }
            //CellWBase.b += 0.01 / VSGlobalControl.m_CamParam.CameraKY;
            Vision_DrawLine((int)(CellWBase.AvgX - 10000), (int)(CellWBase.AvgY - 10000 * Math.Tan(CellWBase.RAngle)), (int)(CellWBase.AvgX + 10000), (int)(CellWBase.AvgY + 10000 * Math.Tan(CellWBase.RAngle)), VSBase.COLOR_BLUE);//打印第一条拟合出来的线
            Vision_DrawCross((int)CellWBase.AvgX, (int)CellWBase.AvgY, 4, VSBase.COLOR_CYAN);
        }

        private double GetAvg(double[] data)
        {
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
                sum += data[i];
            return sum / data.Length;
        }
        private Cst.dPoint GetAvg(Cst.dPoint[] data)
        {
            Cst.dPoint sum = new Cst.dPoint();
            sum.X = 0;
            sum.Y = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum.X += data[i].X;
                sum.Y += data[i].Y;
            }
            sum.X /= data.Length;
            sum.Y /= data.Length;
            return sum;
        }
        private Cst.dPoint GetAvg(List<Cst.dPoint> MeasurePoints)
        {
            return GetAvg(MeasurePoints.ToArray());
        }

        public void GetBaseLength()
        {
            Cst.Struct_EdgeMultiplePoint RightMidEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint;
            Rectangle RectRightMid = new Rectangle();//临时rect
            double min = 9999, max = -9999;
            int tempSize;
            List<Cst.dPoint> hPoint = new List<Cst.dPoint>();

            List<double> hDis = new List<double>();

            if (RightMidEdge.FirstEndPoint.X < RightMidEdge.SecondEndPoint.X)
            {
                RectRightMid.X = (int)(RightMidEdge.FirstEndPoint.X - MEAS_ROI_OUT_PIXEL);
                RectRightMid.Width = (int)(RightMidEdge.SecondEndPoint.X + MEAS_ROI_OUT_PIXEL);
            }
            else
            {
                RectRightMid.X = (int)(RightMidEdge.SecondEndPoint.X - MEAS_ROI_OUT_PIXEL);
                RectRightMid.Width = (int)(RightMidEdge.FirstEndPoint.X + MEAS_ROI_OUT_PIXEL);
            }
            if (MeasStd)
            {
                RectRightMid.Y = (int)(RightMidEdge.FirstEndPoint.Y + (RightMidEdge.IgnoreLength1 / m_CamParam.CameraKY + 3));//+ SEALANT_WIDTH + 25);
                RectRightMid.Height = (int)(RightMidEdge.SecondEndPoint.Y - (RightMidEdge.IgnoreLength2 / m_CamParam.CameraKY + 3));// SEALANT_WIDTH - 25);
            }
            else
            {
                RectRightMid.Y = (int)(RightMidEdge.FirstEndPoint.Y + (2 / m_CamParam.CameraKY + 3));//+ SEALANT_WIDTH + 25);
                RectRightMid.Height = (int)(RightMidEdge.SecondEndPoint.Y - (2 / m_CamParam.CameraKY + 3));// SEALANT_WIDTH - 25);
            }

            double tempdis = 0;
            hPoint.Clear();
            tempSize = GetBaseLine(RectRightMid, RightMidEdge, hPoint, out CellLBase);
            if (hPoint.Count < 3)
            {
                FindLBase = false;
                return;
            }

            Vision_DrawLine((int)(CellLBase.AvgX + 10000 * Math.Tan(CellLBase.RAngle - Math.PI / 2)), (int)(CellLBase.AvgY - 10000), (int)(CellLBase.AvgX - 10000 * Math.Tan(CellLBase.RAngle - Math.PI / 2)), (int)(CellLBase.AvgY + 10000), VSBase.COLOR_BLUE);
            Vision_DrawCross((int)CellLBase.AvgX, (int)CellLBase.AvgY, 4, VSBase.COLOR_CYAN);
        }
        private double CalWidthDistance(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint tempEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                tempEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
            else
                tempEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();
            List<Cst.dPoint> vPoint = new List<Cst.dPoint>();

            //确定下边缘的区域？？？？
            tempRect.X = (int)(tempEdge.FirstEndPoint.X + (tempEdge.IgnoreLength1 / m_CamParam.CameraKX + 5));
            tempRect.Width = (int)(tempEdge.SecondEndPoint.X - (tempEdge.IgnoreLength2 / m_CamParam.CameraKX + 5));
            if (tempEdge.FirstEndPoint.Y < tempEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(tempEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(tempEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                tempRect.Y = (int)(tempEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(tempEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            return CalDistance(CellWBase, tempRect, tempEdge, calcmode);
        }

        private double CalLengthDistance(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint LeftEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();

            //确定左边缘的查找区域
            if (PointCellLT.X < PointCellLB.X)
            {
                tempRect.X = (int)(PointCellLT.X - MEAS_ROI_OUT_PIXEL);
                tempRect.Width = (int)(PointCellLB.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                tempRect.X = (int)(PointCellLB.X - MEAS_ROI_OUT_PIXEL);
                tempRect.Width = (int)(PointCellLT.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            tempRect.Y = (int)(PointCellLT.Y + (LeftEdge.IgnoreLength1 / m_CamParam.CameraKY));
            tempRect.Height = (int)(PointCellLB.Y - (LeftEdge.IgnoreLength2 / m_CamParam.CameraKY));
            double temp = CalDistance(CellLBase, tempRect, LeftEdge, calcmode, 0);
            CellLMeas = tempLine;
            return temp;
        }
        private double CalLengthDistanceWithLeg(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint LeftEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();
            double Dist;
            List<Cst.dPoint> samples;
            //确定左边缘的查找区域
            if (PointCellLT.X < PointCellLB.X)
            {
                tempRect.X = (int)(PointCellLT.X - MEAS_ROI_OUT_PIXEL);
                tempRect.Width = (int)(PointCellLB.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                tempRect.X = (int)(PointCellLB.X - MEAS_ROI_OUT_PIXEL);
                tempRect.Width = (int)(PointCellLT.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            tempRect.Y = (int)(PointCellLT.Y - MEAS_ROI_OUT_PIXEL);
            tempRect.Height = (int)(PointCellLB.Y + MEAS_ROI_OUT_PIXEL);

            if (calcmode == CalcMode.Avg)
                Dist = CalDistance(CellLBase, tempRect, LeftEdge, calcmode, 0);
            else
            {
                samples = GetEdgePoints(tempRect, LeftEdge.Dir, true);
                samples = PointFilter(samples, 0.2);
                Dist = CalDistance(CellLBase, null, samples, LeftEdge.Dir, calcmode, 0, 20);
                if (samples.Count < 1)
                {
                    Vision_DrawRect(tempRect, VSBase.COLOR_RED);
                    return -1;
                }
                else
                {

                    Vision_DrawRect(tempRect, VSBase.COLOR_CYAN);
                }
            }
            return Dist;
        }
        private double CalDistance(Cst.Struct_Line BaseLine, Rectangle MeasureRect, Cst.Struct_EdgeMultiplePoint MeasureEdge, CalcMode calcmode, int IgnoreCount = DISTCOUNT_IGNORE, int CalcCount = DISTCOUNT_CALC)
        {
            return CalDistance(BaseLine, new Rectangle[] { MeasureRect }, MeasureEdge, calcmode, IgnoreCount, CalcCount);
        }
        private double CalDistance(Cst.Struct_Line BaseLine, Rectangle[] MeasureRects, Cst.Struct_EdgeMultiplePoint MeasureEdge, CalcMode calcmode, int IgnoreCount = DISTCOUNT_IGNORE, int CalcCount = DISTCOUNT_CALC)
        {
            List<Cst.dPoint> MeasurePoints = new List<Cst.dPoint>();
            Cst.Struct_Line[] MeasLine = new Cst.Struct_Line[MeasureRects.Length];
            Cst.Struct_Line line;
            for (int i = 0; i < MeasureRects.Length; i++)
                GetMeasurePoints(MeasureRects[i], MeasureEdge, MeasurePoints, out MeasLine[i]);
            return CalDistance(BaseLine, MeasLine, MeasurePoints, MeasureEdge.Dir, calcmode, IgnoreCount, CalcCount);
        }
        private struct PointDist
        {
            public Cst.dPoint Point;
            public double Distance;
            public PointDist(Cst.dPoint point, double distance)
            {
                Point = point;
                Distance = distance;
            }
        }
        private double CalDistance(Cst.Struct_Line BaseLine, Cst.Struct_Line[] MeasLine, List<Cst.dPoint> MeasurePoints, int Dir, CalcMode calcmode, int IgnoreCount = DISTCOUNT_IGNORE, int CalcCount = DISTCOUNT_CALC)
        {
            if (MeasurePoints.Count == 0) return Cst.Struct_DataInfo.NaN;
            bool isXLine = IsXLine(Dir);
            List<double> MaxDis;
            List<double> MinDis;
            double[] MaxDisPointX = { 0, 0, 0 };
            double[] MaxDisPointY = { 0, 0, 0 };
            double[] MinDisPointX = { 0, 0, 0 };
            double[] MinDisPointY = { 0, 0, 0 };
            double DistResult = 0;
            PointDist distance;
            double CameraK;
            List<PointDist> dis = new List<PointDist>();
            if (calcmode == CalcMode.Avg)
            {
                Cst.dPoint Pt;
                if (MeasLine != null && MeasLine.Length > 0)
                {
                    List<Cst.dPoint> Pts = new List<Cst.dPoint>();
                    foreach (Cst.Struct_Line line in MeasLine)
                        Pts.Add(new Cst.dPoint(line.AvgX, line.AvgY));
                    Pt = GetAvg(Pts);
                }
                else
                {
                    Pt = GetAvg(MeasurePoints);
                }
                DistResult = Math.Abs(CalPointToLine(BaseLine, Pt, isXLine));
                point = Pt;
                Vision_DrawCross((int)Pt.X, (int)Pt.Y, 4, VSBase.COLOR_CYAN);
            }
            else
            {
                for (int j = 0; j < MeasurePoints.Count; j++)
                {
                    distance = new PointDist(MeasurePoints[j], Math.Abs(CalPointToLine(BaseLine, MeasurePoints[j], isXLine)));
                    dis.Add(distance);
                }
                dis = Sort(dis);
                DistResult = 0;
                int startIndex = 0, endIndex = dis.Count - 1;
                if (calcmode == CalcMode.Max)
                {
                    if (dis.Count > IgnoreCount)
                    {
                        endIndex = dis.Count - IgnoreCount - 1;
                        if (dis.Count >= IgnoreCount + CalcCount)
                            startIndex = endIndex - (CalcCount - 1);
                    }
                }
                else
                {
                    if (dis.Count > IgnoreCount)
                    {
                        startIndex = IgnoreCount;
                        if (dis.Count >= IgnoreCount + CalcCount)
                            endIndex = startIndex + (CalcCount - 1);
                    }

                }
                for (int i = startIndex; i <= endIndex; i++)
                {
                    DistResult += dis[i].Distance;
                    Vision_DrawCross((int)dis[i].Point.X, (int)dis[i].Point.Y, 4, VSBase.COLOR_CYAN);
                }
                DistResult /= (endIndex - startIndex + 1);
            }
            if (!isXLine)
            {
                CameraK = m_CamParam.CameraKX;
            }
            else
                CameraK = m_CamParam.CameraKY;
            return DistResult * CameraK;
        }

        private double CalAlTabLength(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint AlRightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();
            List<Cst.dPoint> AlPoint = new List<Cst.dPoint>();

            if (PointAlTabRT.X < PointAlTabRB.X)
            {
                tempRect.X = (int)(PointAlTabRT.X - MEAS_ROI_OUT_PIXEL * 2);
                tempRect.Width = (int)(PointAlTabRB.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            else
            {
                tempRect.X = (int)(PointAlTabRB.X - MEAS_ROI_OUT_PIXEL * 2);
                tempRect.Width = (int)(PointAlTabRT.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            tempRect.Y = (int)(PointAlTabRT.Y + (AlRightEdge.IgnoreLength1 / m_CamParam.CameraKY));
            tempRect.Height = (int)(PointAlTabRB.Y - (AlRightEdge.IgnoreLength2 / m_CamParam.CameraKY));

            double dis, dis1, dis2, dis3;
            dis1 = CalDistance(CellLBase, tempRect, AlRightEdge, calcmode);
            return dis1;
        }

        private int GetBaseLine(Rectangle rect, Cst.Struct_EdgeMultiplePoint edge, List<Cst.dPoint> points, out Cst.Struct_Line BaseLine)
        {
            bool isXLine = IsXLine(edge.Dir);
            List<Cst.dPoint> temp = new List<Cst.dPoint>();
            List<Cst.dPoint> AvailabelPoints = new List<Cst.dPoint>();
            temp = GetEdgePoints(rect, edge.Dir, edge.PrintMultiplePoint);

            //BaseLine = GetLineByRobust(temp.ToArray(), isXLine, edge.Robust, AvailabelPoints);
            BaseLine = CalLine(temp.ToArray(), isXLine);
            AvailabelPoints = temp;
            for (int i = 0; i < AvailabelPoints.Count; i++)
            {
                //if (Math.Abs(CalPointToLine(BaseLine, AvailabelPoints[i], isXLine)) < 0.5)
                //{
                points.Add(AvailabelPoints[i]);
                //}
            }
            if (points.Count < 1)
            {
                Vision_DrawRect(rect, VSBase.COLOR_RED);
                return -1;
            }
            else
            {

                Vision_DrawRect(rect, VSBase.COLOR_CYAN);
            }
            return points.Count;

        }
        private int GetMeasurePoints(Rectangle rect, Cst.Struct_EdgeMultiplePoint edge, List<Cst.dPoint> points, out Cst.Struct_Line line, bool ForceBW = false, double tolerance = 0)
        {
            bool isXLine = IsXLine(edge.Dir);
            List<Cst.dPoint> temp = new List<Cst.dPoint>();
            List<Cst.dPoint> AvailablePoints = new List<Cst.dPoint>();
            if (ForceBW)
                temp = GetEdgePoints(rect, /*edge.Thres*/ Threshold, edge.Dir, edge.PrintMultiplePoint);
            else
                temp = GetEdgePoints(rect, edge.Dir, edge.PrintMultiplePoint);

            line = GetLineByRobust(temp.ToArray(), isXLine, edge.Robust, AvailablePoints, tolerance);
            tempLine = line;
            //for (int i = 0; i < temp.Count; i++)
            //{
            //    if (Math.Abs(CalPointToLine(line, temp[i], isXLine)) < 0.5)
            //    {
            //        points.Add(temp[i]);
            //    }
            //}
            points.AddRange(AvailablePoints);
            if (points.Count < 1)
            {
                Vision_DrawRect(rect, VSBase.COLOR_RED);
                return -1;
            }
            else
            {

                Vision_DrawRect(rect, VSBase.COLOR_CYAN);
            }
            return points.Count;

        }
        //private int GetMeasurePoints(Cst.Struct_Line BaseLine, Rectangle rect, Cst.Struct_EdgeMultiplePoint edge, List<Cst.dPoint> points, ref Cst.Struct_Line line)
        //{
        //    int tempsize = GetMeasurePoints(BaseLine, rect, edge, points);
        //    line = CalLine(points.ToArray(), IsXLine(edge.Dir));//最小二乘法拟合电池水平基准直线
        //    return tempsize;
        //}
        private double CalNITabLength(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint NiRightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint;

            Rectangle tempRect = new Rectangle();

            if (PointNiTabRT.X < PointNiTabRB.X)
            {
                tempRect.X = (int)(PointNiTabRT.X - MEAS_ROI_OUT_PIXEL * 2);
                tempRect.Width = (int)(PointNiTabRB.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            else
            {
                tempRect.X = (int)(PointNiTabRB.X - MEAS_ROI_OUT_PIXEL * 2);
                tempRect.Width = (int)(PointNiTabRT.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            tempRect.Y = (int)(PointNiTabRT.Y + (NiRightEdge.IgnoreLength1 / m_CamParam.CameraKY));
            tempRect.Height = (int)(PointNiTabRB.Y - (NiRightEdge.IgnoreLength2 / m_CamParam.CameraKY));

            Cst.Struct_EdgeMultiplePoint TempEdge = new Cst.Struct_EdgeMultiplePoint();
            TempEdge = NiRightEdge;
            double dis, dis1, dis2, dis3;

            List<Cst.dPoint> MeasurePoints = new List<Cst.dPoint>();
            MeasurePoints.Clear();
            MeasurePoints = GetEdgePoints(tempRect, NiRightEdge.Dir, NiRightEdge.PrintMultiplePoint);
            if (MeasurePoints.Count > 3)
            {
                Vision_DrawRect(tempRect, VSBase.COLOR_CYAN);
                dis1 = CalDistance(CellLBase, null, MeasurePoints, NiRightEdge.Dir, calcmode);
            }
            else
            {
                Vision_DrawRect(tempRect, VSBase.COLOR_RED);
                dis1 = Cst.Struct_DataInfo.NaN;
            }
            return dis1;

        }
        private double CalAlTabDis(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint AlTabEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                AlTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint;
            else
                AlTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint;

            Rectangle tempRect = new Rectangle();

            tempRect.X = (int)(AlTabEdge.FirstEndPoint.X + AlTabEdge.IgnoreLength1 / m_CamParam.CameraKX);
            tempRect.Width = (int)(AlTabEdge.SecondEndPoint.X - AlTabEdge.IgnoreLength2 / m_CamParam.CameraKX);
            if (tempRect.Width - tempRect.X < 1 / m_CamParam.CameraKX) tempRect.X = tempRect.Width - (int)(1 / m_CamParam.CameraKX);
            if (AlTabEdge.FirstEndPoint.Y < AlTabEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(AlTabEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(AlTabEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }
            else
            {
                tempRect.Y = (int)(AlTabEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(AlTabEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }
            double dis1, dis2, dis3, dis;
            dis1 = CalDistance(CellWBase, tempRect, AlTabEdge, calcmode, 0);
            return dis1;
        }

        private double CalNITabDis(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint NiTabEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                NiTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint;
            else
                NiTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint;

            Rectangle tempRect = new Rectangle();

            tempRect.X = (int)(NiTabEdge.FirstEndPoint.X + NiTabEdge.IgnoreLength1 / m_CamParam.CameraKX);
            tempRect.Width = (int)(NiTabEdge.SecondEndPoint.X - NiTabEdge.IgnoreLength2 / m_CamParam.CameraKX);
            if (tempRect.Width - tempRect.X < 1 / m_CamParam.CameraKX) tempRect.X = tempRect.Width - (int)(1 / m_CamParam.CameraKX);
            if (NiTabEdge.FirstEndPoint.Y < NiTabEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(NiTabEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(NiTabEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }
            else
            {
                tempRect.Y = (int)(NiTabEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(NiTabEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }

            Cst.Struct_EdgeMultiplePoint TempEdge = new Cst.Struct_EdgeMultiplePoint();
            TempEdge = NiTabEdge;
            double dis, dis1, dis2, dis3;
            dis1 = CalDistance(CellWBase, tempRect, NiTabEdge, calcmode, 0);

            return dis1;
        }
        private double CalAlSealantHeight()
        {
            return CalSealantHeight(PointAlTabLT, PointAlTabLB, VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlSealant].EdgeMultiplePoint.Thres);
        }

        private double CalNISealantHeight()
        {
            return CalSealantHeight(PointNiTabLT, PointNiTabLB, VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiSealant].EdgeMultiplePoint.Thres);
        }

        private double CalSealantHeight(Cst.dPoint TopPoint, Cst.dPoint BottomPoint, int Thres)
        {
            Rectangle tempRectUp = new Rectangle();
            Rectangle tempRectDn = new Rectangle();

            Cst.Struct_EdgeMultiplePoint edge = new Cst.Struct_EdgeMultiplePoint();
            //edge.Thres = Thres;
            edge.Dir = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Dir;
            edge.PrintMultiplePoint = true;
            edge.Robust.RobustSampleNum = 3;
            edge.Robust.MaxRobustCount = 100;

            tempRectUp.X = (int)(TopPoint.X + 2);
            tempRectUp.Width = (int)(TopPoint.X + 75);
            tempRectDn.X = (int)(BottomPoint.X + 2);
            tempRectDn.Width = (int)(BottomPoint.X + 75);

            tempRectUp.Y = (int)(TopPoint.Y - SEALANT_WIDTH_PIXEL);
            tempRectUp.Height = (int)(TopPoint.Y - 5);
            tempRectDn.Y = (int)(BottomPoint.Y + 5);
            tempRectDn.Height = (int)(BottomPoint.Y + SEALANT_WIDTH_PIXEL);

            Cst.Struct_Line[] MeasLine = new Cst.Struct_Line[2];
            List<Cst.dPoint> pointsUp = new List<Cst.dPoint>();
            List<Cst.dPoint> pointsDn = new List<Cst.dPoint>();
            List<Cst.dPoint> CPoint = new List<Cst.dPoint>();
            GetMeasurePoints(tempRectUp, edge, pointsUp, out MeasLine[0], true, 0.02);
            //pointsUp = GetLinePoints(tempRectUp, VSGlobalControl.m_VParam.Threshold, edge.Dir, edge.PrintMultiplePoint);
            //Vision_DrawRect(tempRectUp, VSBase.COLOR_CYAN);
            if (pointsUp.Count > 0)
                CPoint.Add(GetAvg(pointsUp.ToArray()));
            GetMeasurePoints(tempRectDn, edge, pointsDn, out MeasLine[1], true, 0.02);
            //pointsDn = GetLinePoints(tempRectDn, VSGlobalControl.m_VParam.Threshold, edge.Dir, edge.PrintMultiplePoint);
            //Vision_DrawRect(tempRectDn, VSBase.COLOR_CYAN);
            if (pointsDn.Count > 0)
                CPoint.Add(GetAvg(pointsDn.ToArray()));
            if (MeasStd)
                return CalDistance(CellLBase, new List<Cst.Struct_Line>().ToArray(), CPoint, edge.Dir, CalcMode.Avg);
            else
                return CalDistance(CellLBase, MeasLine, CPoint, edge.Dir, CalcMode.Max, 0, 5);
        }

        //private double CalcSealantHeight(Rectangle tempRect, Cst.Struct_EdgeMultiplePoint edge)
        //{
        //    List<Cst.dPoint> CPoint = new List<Cst.dPoint>();
        //    GetMeasurePoints(TabLBase, tempRect, edge, CPoint);
        //    return CalcDistance(TabLBase, CPoint, edge.Dir, CalcMode.Max);
        //}

        private bool compare(double a, double b)
        {
            return a < b;
        }
        private void SaveCSV(TimeSpan spannedTime, string barcode, double width, double length, double TabAlDis, double TabNIDis, double TabAlLength,
                               double TabNILength, double TabWidth, double AlSealantHeight, double NISealantHeight, double ShoulderWidth)
        {

            string szTimeSave;
            string path;
            string filePath;
            string fileName;
            szTimeSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            path = CommonFunction.DataPath + "data\\";
            fileName = string.Format("CCDMeasure {0:yyyyMMdd}.csv", DateTime.Today);
            filePath = path + fileName;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(filePath)) File.AppendAllText(filePath, "Time,Laps,Barcode,Width,Length,AlTabDist,NiTabDist,AlTabLen,NiTabLen,TabDist,AlTabSeal,NiTabSeal,BaseDAngle,BaseBAngle\n");
            string datastring = "";
            datastring += String.Format("{0},", szTimeSave);
            datastring += string.Format("{0},", spannedTime.TotalMilliseconds);
            datastring += String.Format("{0},", barcode);
            datastring += String.Format("{0},", width);
            datastring += String.Format("{0},", length);
            datastring += String.Format("{0},", TabAlDis);
            datastring += String.Format("{0},", TabNIDis);
            datastring += String.Format("{0},", TabAlLength);
            datastring += String.Format("{0},", TabNILength);
            datastring += String.Format("{0},", TabWidth);
            datastring += String.Format("{0},", AlSealantHeight);
            datastring += String.Format("{0},", NISealantHeight);
            datastring += string.Format("{0},", ShoulderWidth);
            datastring += String.Format("{0},", CellLBase.DAngle);
            datastring += String.Format("{0},", CellWBase.DAngle);
            datastring += Environment.NewLine;
            File.AppendAllText(filePath, datastring);
        }

        private void SavePointCSV(string barcode, Cst.dPoint length, Cst.dPoint width, Cst.dPoint AlDis,
            Cst.dPoint NiDis, Cst.dPoint AlLength, Cst.dPoint NiLength, Cst.dPoint AlSealant, Cst.dPoint NiSealant)
        {

            string szTimeSave;
            string path;
            string filePath;
            string fileName;
            szTimeSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            path = CommonFunction.DataPath + "data\\";
            fileName = string.Format("CCDMeasPoint{0:yyyyMMdd}.csv", DateTime.Today);
            filePath = path + fileName;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(filePath)) File.AppendAllText(filePath, "测试时间,条码,BaseH,BaseH.k,BaseH.b,BaseV,BaseV.k,BaseV.b,LeftBottom,RightBottom,Width,LeftTop,LeftBottom,LengthAVG,Length,AlTabLT,AlTabRT,Al tab Dis,NiTabLT,NiTabRT,Ni tab Dis,AlTabRT,AlTabRB,Al tab Length,NiTabRT,NiTabRB,Ni tab Length,Al tab Sealant,Ni tab Sealant\n");
            string datastring = "";
            datastring += String.Format("{0},", szTimeSave);
            datastring += String.Format("{0},", barcode);
            datastring += String.Format("{0:0.0} : {1:0.0},", CellWBase.AvgX, CellWBase.AvgY);
            datastring += String.Format("{0},", CellWBase.k);
            datastring += String.Format("{0},", CellWBase.b);
            datastring += String.Format("{0:0.0} : {1:0.0},", CellLBase.AvgX, CellLBase.AvgY);
            datastring += String.Format("{0},", CellLBase.k);
            datastring += String.Format("{0},", CellLBase.b);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointCellLB.X, PointCellLB.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointCellRB.X, PointCellRB.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", width.X, width.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointCellLT.X, PointCellLT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointCellLB.X, PointCellLB.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", CellLMeas.AvgX, CellLMeas.AvgY);
            datastring += String.Format("{0:0.0} : {1:0.0},", length.X, length.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointAlTabLT.X, PointAlTabLT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointAlTabRT.X, PointAlTabRT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", AlDis.X, AlDis.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointNiTabLT.X, PointNiTabLT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointNiTabRT.X, PointNiTabRT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", NiDis.X, NiDis.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointAlTabRT.X, PointAlTabRT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointAlTabRB.X, PointAlTabRB.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", AlLength.X, AlLength.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointNiTabRT.X, PointNiTabRT.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", PointNiTabRB.X, PointNiTabRB.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", NiLength.X, NiLength.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", AlSealant.X, AlSealant.Y);
            datastring += String.Format("{0:0.0} : {1:0.0},", NiSealant.X, NiSealant.Y);
            datastring += Environment.NewLine;
            File.AppendAllText(filePath, datastring);
        }

        private void SaveBasePointCSV(string barcode, Cst.dPoint point1, Cst.dPoint point2, Cst.dPoint point3,
            Cst.dPoint point4, Cst.dPoint point5, Cst.dPoint point6, Cst.dPoint point7, Cst.dPoint point8, Cst.dPoint point9, Cst.Struct_Line BaseLine)
        {

            string szTimeSave;
            string path;
            string filePath;
            string fileName;
            szTimeSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            path = Application.StartupPath + "\\data\\BaseVPoint\\";
            fileName = string.Format("{0:yyyyMMdd}.csv", DateTime.Today);
            filePath = path + fileName;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(filePath)) File.AppendAllText(filePath, "测试时间,条码,RightTopX,RightTopY,RightBottomX,RightBottomY,Point1X,Point1Y,Point2X,Point2Y,Point3X,Point3Y,Point4X,Point4Y,Point5X,Point5Y,Point6X,Point6Y,Point7X,Point7Y,Point8X,Point8Y,Point9X,Point9Y\n");
            string datastring = "";
            datastring += String.Format("{0},", szTimeSave);
            datastring += String.Format("{0},", barcode);
            datastring += String.Format("{0:0.0},", PointCellRT.X);
            datastring += String.Format("{0:0.0},", PointCellRT.Y);
            datastring += String.Format("{0:0.0},", PointCellRB.X);
            datastring += String.Format("{0:0.0},", PointCellRB.Y);
            datastring += String.Format("{0:0.0},", point1.X);
            datastring += String.Format("{0:0.0},", point1.Y);
            datastring += String.Format("{0:0.0},", point2.X);
            datastring += String.Format("{0:0.0},", point2.Y);
            datastring += String.Format("{0:0.0},", point3.X);
            datastring += String.Format("{0:0.0},", point3.Y);
            datastring += String.Format("{0:0.0},", point4.X);
            datastring += String.Format("{0:0.0},", point4.Y);
            datastring += String.Format("{0:0.0},", point5.X);
            datastring += String.Format("{0:0.0},", point5.Y);
            datastring += String.Format("{0:0.0},", point6.X);
            datastring += String.Format("{0:0.0},", point6.Y);
            datastring += String.Format("{0:0.0},", point7.X);
            datastring += String.Format("{0:0.0},", point7.Y);
            datastring += String.Format("{0:0.0},", point8.X);
            datastring += String.Format("{0:0.0},", point8.Y);
            datastring += String.Format("{0:0.0},", point9.X);
            datastring += String.Format("{0:0.0},", point9.Y);
            datastring += String.Format("{0},", BaseLine.DAngle);
            datastring += String.Format("{0},", BaseLine.k);
            datastring += Environment.NewLine;
            File.AppendAllText(filePath, datastring);
        }
        private void SaveBasePointCSV(string barcode, Cst.Struct_Line BaseLine, Cst.dPoint[] point)
        {

            string szTimeSave;
            string path;
            string filePath;
            string fileName;
            szTimeSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            path = CommonFunction.DataPath + "\\data\\";
            fileName = string.Format("CCDAllBasePoint{0:yyyyMMdd}.csv", DateTime.Today);
            filePath = path + fileName;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!File.Exists(filePath)) File.AppendAllText(filePath, "测试时间,条码,基准点X,基准点Y,基准K,基准B\n");
            string datastring = "";
            datastring += String.Format("{0},", szTimeSave);
            datastring += String.Format("{0},", barcode);
            datastring += String.Format("{0:0.0},", BaseLine.AvgX);
            datastring += String.Format("{0:0.0},", BaseLine.AvgY);
            datastring += String.Format("{0:0.0},", BaseLine.k);
            datastring += String.Format("{0:0.0},", BaseLine.b);
            for (int i = 0; i < point.Length; i++)
            {
                datastring += string.Format("{0:0.000 } {1:0.000},", point[i].X, point[i].Y);
            }
            datastring += Environment.NewLine;
            File.AppendAllText(filePath, datastring);
        }
        private List<Cst.dPoint> PointFilter(List<Cst.dPoint> samples, double tolerance)
        {
            int Nearby;
            List<Cst.dPoint> temp = new List<Cst.dPoint>();
            for (int i = 0; i < samples.Count; i++)
            {
                Nearby = 0;
                for (int j = 0; j < samples.Count; j++)
                    if (i != j)
                        if (Cst.dPoint.GetDist(samples[i], samples[j]) < tolerance / m_CamParam.CameraKX)
                            Nearby++;
                if (Nearby > 0) temp.Add(samples[i]);
            }
            return temp;
        }

        public string[] GetDispResult()
        {
            List<string> result = new List<string>();
            result.Add($"{DateTime.Now:HH:mm:ss} {Barcode}测量结果:");
            result.Add("宽度:".PadRight(15) + CellDataSpec.CellWidth.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.CellWidth.DataResult.ToString());
            result.Add("长度:".PadRight(15) + CellDataSpec.CellLength.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.CellLength.DataResult.ToString());
            result.Add("Al边距:".PadRight(15) + CellDataSpec.AlTabDistance.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.AlTabDistance.DataResult.ToString());
            result.Add("Ni边距:".PadRight(15) + CellDataSpec.NiTabDistance.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.NiTabDistance.DataResult.ToString());
            result.Add("Al最大边距:".PadRight(15) + CellDataSpec.AlTabDistanceMax.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.AlTabDistanceMax.DataResult.ToString());
            result.Add("Ni最大边距:".PadRight(15) + CellDataSpec.NiTabDistanceMax.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.NiTabDistanceMax.DataResult.ToString());
            result.Add("Al长度:".PadRight(15) + CellDataSpec.AlTabLength.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.AlTabLength.DataResult.ToString());
            result.Add("Ni长度:".PadRight(15) + CellDataSpec.NiTabLength.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.NiTabLength.DataResult.ToString());
            result.Add("Tab间距:".PadRight(15) + CellDataSpec.TabDistance.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.TabDistance.DataResult.ToString());
            result.Add("Al小白胶:".PadRight(15) + CellDataSpec.AlSealantHeight.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.AlSealantHeight.DataResult.ToString());
            result.Add("Ni小白胶:".PadRight(15) + CellDataSpec.NiSealantHeight.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.NiSealantHeight.DataResult.ToString());
            result.Add("肩宽:".PadRight(15) + CellDataSpec.ShoulderWidth.Value.ToString("0.000 mm").PadRight(15) + CellDataSpec.ShoulderWidth.DataResult.ToString());
            result.Add("用时:".PadRight(15) + CellDataSpec.MeasureLaps.ToString("0.000s"));

            return result.ToArray();
        }
        public Cst.Struct_MeasDatas Measure(string barcode)
        {
            StateEventArgs e = new StateEventArgs();
            CellDataSpec = VSGlobalControl.CurrentCellDataSpec;
            Stopwatch t = new Stopwatch();

            t.Start();
            //CalCrossPoint();
            CalCrossByPat();
            //基准偏移到最外点
            //if (VSGlobalControl.m_VParam.UseBottomAsBase)
            //    GetBaseWidth(CalcMode.Max);
            //else
            //    GetBaseWidth(CalcMode.Min);
            GetBaseWidth(CalcMode.Avg);
            GetBaseLength();

            if (!FindWBase || !FindLBase)
            {
                CellDataSpec.CellWidth.Value = 0;
                CellDataSpec.CellLength.Value = 0;
                CellDataSpec.AlTabLength.Value = 0;
                CellDataSpec.NiTabLength.Value = 0;
                CellDataSpec.AlTabDistance.Value = 0;
                CellDataSpec.NiTabDistance.Value = 0;
                CellDataSpec.AlTabDistanceMax.Value = 0;
                CellDataSpec.NiTabDistanceMax.Value = 0;
                CellDataSpec.AlSealantHeight.Value = 0;
                CellDataSpec.NiSealantHeight.Value = 0;
                CellDataSpec.TabDistance.Value = 0;
            }
            CellDataSpec.Angle = CellWBase.DAngle;
            if (MeasStd)
            {
                CellDataSpec.CellWidth.Value = CalWidthDistance(CalcMode.Avg);
                CellDataSpec.CellLength.Value = CalLengthDistanceWithLeg(CalcMode.Avg);
                CellDataSpec.AlTabLength.Value = CalAlTabLength(CalcMode.Avg);
                CellDataSpec.NiTabLength.Value = CalNITabLength(CalcMode.Avg);
            }
            else
            {
                CellDataSpec.CellWidth.Value = CalWidthDistance(CalcMode.Max);
                CellDataSpec.CellLength.Value = CalLengthDistanceWithLeg(CalcMode.Max);
                CellDataSpec.AlTabLength.Value = CalAlTabLength(CalcMode.Max);
                CellDataSpec.NiTabLength.Value = CalNITabLength(CalcMode.Max);
            }
            CellDataSpec.AlTabDistance.Value = CalAlTabDis(CalcMode.Avg);
            CellDataSpec.NiTabDistance.Value = CalNITabDis(CalcMode.Avg);
            CellDataSpec.AlTabDistanceMax.Value = CalAlTabDis(CalcMode.Max);
            CellDataSpec.NiTabDistanceMax.Value = CalNITabDis(CalcMode.Max);
            CellDataSpec.AlSealantHeight.Value = CalAlSealantHeight();
            CellDataSpec.NiSealantHeight.Value = CalNISealantHeight();
            CellDataSpec.ShoulderWidth.Value = 0;
            if (CellDataSpec.AlTabDistance.Value != Cst.Struct_DataInfo.NaN && CellDataSpec.NiTabDistance.Value != Cst.Struct_DataInfo.NaN)
                CellDataSpec.TabDistance.Value = Math.Abs(CellDataSpec.AlTabDistance.Value - CellDataSpec.NiTabDistance.Value);
            else
                CellDataSpec.TabDistance.Value = Cst.Struct_DataInfo.NaN;


            e.eventName = "MeasDone:" + ((int)_vs + 1);
            e.eventInfo = ((int)_vs).ToString();
            this.Barcode = barcode;
            t.Stop();
            CellDataSpec.MeasureLaps = t.Elapsed.TotalSeconds;
            ResPublisher.notifyDoneEventSubscribers(null, e);
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Control_Zoom();
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Control_Zoom();
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Control_Zoom();
                    break;

                case BufferIndex.Test:
                    VSTest.Control_Zoom();
                    break;
            }
            if (CellDataSpec.CCDNG && VSGlobalControl.SaveOption == SaveImageOption.SaveNG)
                SaveBMP(_vs);

            return CellDataSpec;
        }
        private string SaveBMP(BufferIndex vs)
        {
            string file = Barcode + " " + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".bmp";
            if (!Directory.Exists(szFilePath)) Directory.CreateDirectory(szFilePath);
            switch (vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Vision_SaveBmp(szFilePath + file);
                    break;
                case BufferIndex.MiddleCell:
                    VSMiddleCell.Vision_SaveBmp(szFilePath + file);
                    break;
                case BufferIndex.LeftCell:
                    VSLeftCell.Vision_SaveBmp(szFilePath + file);
                    break;
                case BufferIndex.Test:
                    VSTest.Vision_SaveBmp(szFilePath + file);
                    break;
            }
            return file;
        }
        public Cst.Struct_MeasDatas Measure2(string barcode)
        {
            StateEventArgs e = new StateEventArgs();
            CellDataSpec = VSGlobalControl.CurrentCellDataSpec;
            Stopwatch t = new Stopwatch();

            t.Start();
            //CalCrossPoint();
            CalCrossByPat();

            GetBaseWidth2();
            GetBaseLength2();

            if (!FindWBase || !FindLBase)
            {
                CellDataSpec.CellWidth.Value = 0;
                CellDataSpec.CellLength.Value = 0;
                CellDataSpec.AlTabLength.Value = 0;
                CellDataSpec.NiTabLength.Value = 0;
                CellDataSpec.AlTabDistance.Value = 0;
                CellDataSpec.NiTabDistance.Value = 0;
                CellDataSpec.AlTabDistanceMax.Value = 0;
                CellDataSpec.NiTabDistanceMax.Value = 0;
                CellDataSpec.AlSealantHeight.Value = 0;
                CellDataSpec.NiSealantHeight.Value = 0;
                CellDataSpec.TabDistance.Value = 0;
                CellDataSpec.ShoulderWidth.Value = 0;
            }

            CellDataSpec.CellWidth.Value = CalWidthDistance2(CalcMode.Min, CalcMode.Avg);
            CellDataSpec.CellLength.Value = CalLengthDistance2(CalcMode.Min, CalcMode.Avg);
            if (MeasStd)
            {
                CellDataSpec.AlTabLength.Value = CalAlTabLength2(CalcMode.Min, CalcMode.Avg);
                CellDataSpec.NiTabLength.Value = CalNITabLength2(CalcMode.Min, CalcMode.Avg);
                CellDataSpec.AlTabDistance.Value = CalAlTabDis2(CalcMode.Avg);
                CellDataSpec.NiTabDistance.Value = CalNITabDis2(CalcMode.Avg);
            }
            else
            {
                CellDataSpec.AlTabLength.Value = CalAlTabLength2(CalcMode.Avg, CalcMode.Max);
                CellDataSpec.NiTabLength.Value = CalNITabLength2(CalcMode.Avg, CalcMode.Max);
                CellDataSpec.AlTabDistance.Value = CalAlTabDis2(CalcMode.Min);
                CellDataSpec.NiTabDistance.Value = CalNITabDis2(CalcMode.Min);
            }
            CellDataSpec.AlTabDistanceMax.Value = CalAlTabDis2(CalcMode.Max);
            CellDataSpec.NiTabDistanceMax.Value = CalNITabDis2(CalcMode.Max);
            CellDataSpec.AlSealantHeight.Value = CalAlSealantHeight2();
            CellDataSpec.NiSealantHeight.Value = CalNISealantHeight2();
            CellDataSpec.ShoulderWidth.Value = CalShoulderWidth2(CalcMode.Avg, CalcMode.Avg);
            if (CellDataSpec.AlTabDistance.Value != Cst.Struct_DataInfo.NaN && CellDataSpec.NiTabDistance.Value != Cst.Struct_DataInfo.NaN)
                CellDataSpec.TabDistance.Value = Math.Abs(CellDataSpec.AlTabDistance.Value - CellDataSpec.NiTabDistance.Value);
            else
                CellDataSpec.TabDistance.Value = Cst.Struct_DataInfo.NaN;

            //SaveCSV(t.Elapsed, barcode,
            //    CellDataSpec.CellWidth.Value, CellDataSpec.CellLength.Value,
            //    CellDataSpec.AlTabDistance.Value, CellDataSpec.NiTabDistance.Value,
            //    CellDataSpec.AlTabLength.Value, CellDataSpec.NiTabLength.Value,
            //    CellDataSpec.TabDistance.Value,
            //    CellDataSpec.AlSealantHeight.Value, CellDataSpec.NiSealantHeight.Value, CellDataSpec.ShoulderWidth.Value);

            e.eventName = "MeasDone:" + ((int)_vs + 1);
            e.eventInfo = ((int)_vs).ToString();
            this.Barcode = barcode;
            t.Stop();
            CellDataSpec.MeasureLaps = t.Elapsed.TotalSeconds;
            ResPublisher.notifyDoneEventSubscribers(null, e);
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Control_Zoom();
                    break;

                case BufferIndex.MiddleCell:
                    VSMiddleCell.Control_Zoom();
                    break;

                case BufferIndex.LeftCell:
                    VSLeftCell.Control_Zoom();
                    break;

                case BufferIndex.Test:
                    VSTest.Control_Zoom();
                    break;
            }
            if (CellDataSpec.CCDNG && VSGlobalControl.SaveOption == SaveImageOption.SaveNG)
                SaveBMP(_vs);

            return CellDataSpec;
        }
        public bool MeasStd;
        public int Threshold
        {
            get { return MeasStd ? VSGlobalControl.m_VParam.ThresholdStd : VSGlobalControl.m_VParam.ThresholdCell; }
        }
        public void StartMeas(string barcode, bool isAsync)
        {
            MeasStd = barcode.IndexOf("Standard") >= 0;
            switch (_vs)
            {
                case BufferIndex.RightCell:
                    VSRightCell.Control_Zoom();
                    break;
                case BufferIndex.MiddleCell:
                    VSMiddleCell.Control_Zoom();
                    break;
                case BufferIndex.LeftCell:
                    VSLeftCell.Control_Zoom();
                    break;
                case BufferIndex.Test:
                    VSTest.Control_Zoom();
                    break;
            }
            VSGlobalControl.CleanAllTestMark(false);
            switch (VSGlobalControl.m_VParam.MeasureMethod)
            {
                case MeasMethod.Meas1:
                    AsyncMeasure = Measure;
                    break;
                case MeasMethod.Meas2:
                    AsyncMeasure = Measure2;
                    break;
            }
            if (isAsync)
                AsyncMeasure.BeginInvoke(barcode, null, null);
            else
                AsyncMeasure(barcode);
        }
        public override void ImageCapturedHandler(string CameraName, byte[] ImageBuffer)
        {
            if (VSCamTrig)
            {
                switch (_vs)
                {
                    case BufferIndex.RightCell:
                        VSRightCell.Vision_GetBuffer(ImageBuffer, imgWidth, imgHeight);
                        VSRightCell.Vision_CopyImageToBuffer(1);
                        break;

                    case BufferIndex.MiddleCell:
                        VSMiddleCell.Vision_GetBuffer(ImageBuffer, imgWidth, imgHeight);
                        VSMiddleCell.Vision_CopyImageToBuffer(1);
                        break;

                    case BufferIndex.LeftCell:
                        VSLeftCell.Vision_GetBuffer(ImageBuffer, imgWidth, imgHeight);
                        VSLeftCell.Vision_CopyImageToBuffer(1);
                        break;
                }
                VSCamTrig = false;
                if (_vs != BufferIndex.Test)
                {
                    if (VSGlobalControl.SaveOption == SaveImageOption.SaveAll)
                        SaveBMP(_vs);
                    StartMeas(Barcode, true);
                }
            }
            VSTest.Vision_GetBuffer(ImageBuffer, imgWidth, imgHeight);
            VSTest.Vision_CopyImageToBuffer(1);
        }
        //Measure2
        private Cst.Struct_Line BatBaseH = new Cst.Struct_Line();
        private Cst.Struct_Line BatBaseV = new Cst.Struct_Line();
        public void GetBaseWidth2()
        {
            double DisLevel;
            Cst.Struct_EdgeMultiplePoint BaseEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
            {
                BaseEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;
                DisLevel = 20;
            }
            else
            {
                BaseEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
                DisLevel = -20;
            }
            Rectangle tempRect = new Rectangle();//临时rect
            int tempSize;
            List<Cst.dPoint> vPoint = new List<Cst.dPoint>();

            List<double> vDis = new List<double>();


            //获取上边缘基准检测区域
            tempRect.X = (int)(BaseEdge.FirstEndPoint.X + BaseEdge.IgnoreLength1 / m_CamParam.CameraKX);
            tempRect.Width = (int)(BaseEdge.FirstEndPoint.X + ROI_BASE_WIDTH_MM / m_CamParam.CameraKX);
            if (BaseEdge.FirstEndPoint.Y > BaseEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(BaseEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(BaseEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                tempRect.Y = (int)(BaseEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                tempRect.Height = (int)(BaseEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }

            //获取检测点
            vPoint.Clear();
            tempSize = GetBaseLine(tempRect, BaseEdge, vPoint, out BatBaseH);
            if (tempSize < 3)
            {
                FindWBase = false;
                return;
            }

            //基准线外移
            CellWBase = BatBaseH;
            CellWBase.AvgX = BatBaseH.AvgX + DisLevel * Math.Sin(BatBaseH.RAngle);
            CellWBase.AvgY = BatBaseH.AvgY + DisLevel * Math.Cos(BatBaseH.RAngle);
            CellWBase.b = CellWBase.AvgY - CellWBase.AvgX * CellWBase.k;

            Vision_DrawLine((int)(CellWBase.AvgX - 10000), (int)(CellWBase.AvgY - 10000 * Math.Tan(CellWBase.RAngle)), (int)(CellWBase.AvgX + 10000), (int)(CellWBase.AvgY + 10000 * Math.Tan(CellWBase.RAngle)), VSBase.COLOR_BLUE);//打印第一条拟合出来的线
            Vision_DrawCross((int)CellWBase.AvgX, (int)CellWBase.AvgY, 4, VSBase.COLOR_CYAN);
        }

        public void GetBaseLength2()
        {
            Cst.Struct_EdgeMultiplePoint LeftEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint;

            Rectangle RectLeft = new Rectangle();//临时rect
            int tempSize;
            List<Cst.dPoint> hPoint = new List<Cst.dPoint>();

            List<double> hDis = new List<double>();

            double DisLevel = -10;

            if (LeftEdge.FirstEndPoint.X < LeftEdge.SecondEndPoint.X)
            {
                RectLeft.X = (int)(LeftEdge.FirstEndPoint.X - MEAS_ROI_OUT_PIXEL);
                RectLeft.Width = (int)(LeftEdge.SecondEndPoint.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                RectLeft.X = (int)(LeftEdge.SecondEndPoint.X - MEAS_ROI_OUT_PIXEL);
                RectLeft.Width = (int)(LeftEdge.FirstEndPoint.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            RectLeft.Y = (int)(LeftEdge.FirstEndPoint.Y + LeftEdge.IgnoreLength1 / m_CamParam.CameraKY);
            RectLeft.Height = (int)(LeftEdge.SecondEndPoint.Y - LeftEdge.IgnoreLength2 / m_CamParam.CameraKY);



            hPoint.Clear();
            tempSize = GetBaseLine(RectLeft, LeftEdge, hPoint, out BatBaseV);
            if (hPoint.Count < 3)
            {
                FindLBase = false;
                return;
            }

            CellLBase = BatBaseV;
            CellLBase.AvgX = BatBaseV.AvgX + DisLevel * Math.Sin(BatBaseV.DAngle / 180 * Math.PI);
            CellLBase.AvgY = BatBaseV.AvgY + DisLevel * Math.Cos(BatBaseV.DAngle / 180 * Math.PI);
            CellLBase.b = CellLBase.AvgY - CellLBase.k / CellLBase.AvgX;
            //TabLBase.k = -1 / TabWBase.k;
            //TabLBase.b = TabLBase.AvgY - TabLBase.AvgX * TabLBase.k;

            Vision_DrawLine((int)(CellLBase.AvgX + 10000 * Math.Tan(CellLBase.RAngle - Math.PI / 2)), (int)(CellLBase.AvgY - 10000), (int)(CellLBase.AvgX - 10000 * Math.Tan(CellLBase.RAngle - Math.PI / 2)), (int)(CellLBase.AvgY + 10000), VSBase.COLOR_BLUE);
            Vision_DrawCross((int)CellLBase.AvgX, (int)CellLBase.AvgY, 4, VSBase.COLOR_CYAN);
        }

        private double CalWidthDistance2(CalcMode Mincalcmode, CalcMode Maxcalcmode)
        {

            double max, min;
            Cst.Struct_EdgeMultiplePoint tempEdgeMin;
            Cst.Struct_EdgeMultiplePoint tempEdgeMax;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
            {
                tempEdgeMax = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
                tempEdgeMin = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;
            }
            else
            {
                tempEdgeMax = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;
                tempEdgeMin = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
            }
            Rectangle RectMin = new Rectangle();
            Rectangle RectMax = new Rectangle();
            List<Cst.dPoint> vPoint = new List<Cst.dPoint>();

            RectMin.X = (int)(tempEdgeMin.FirstEndPoint.X + tempEdgeMin.IgnoreLength1 / m_CamParam.CameraKX);
            RectMin.Width = (int)(tempEdgeMin.FirstEndPoint.X + (ROI_BASE_WIDTH_MM / m_CamParam.CameraKX));
            if (tempEdgeMin.FirstEndPoint.Y < tempEdgeMin.SecondEndPoint.Y)
            {
                RectMin.Y = (int)(tempEdgeMin.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                RectMin.Height = (int)(tempEdgeMin.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                RectMin.Y = (int)(tempEdgeMin.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                RectMin.Height = (int)(tempEdgeMin.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }

            RectMax.X = (int)(tempEdgeMax.FirstEndPoint.X + tempEdgeMax.IgnoreLength1 / m_CamParam.CameraKX);
            RectMax.Width = (int)(tempEdgeMax.FirstEndPoint.X + (ROI_BASE_WIDTH_MM / m_CamParam.CameraKX));
            if (tempEdgeMax.FirstEndPoint.Y < tempEdgeMax.SecondEndPoint.Y)
            {
                RectMax.Y = (int)(tempEdgeMax.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                RectMax.Height = (int)(tempEdgeMax.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                RectMax.Y = (int)(tempEdgeMax.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL * 3);
                RectMax.Height = (int)(tempEdgeMax.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL * 3);
            }

            max = CalDistance(CellWBase, RectMax, tempEdgeMax, Maxcalcmode);
            min = CalDistance(CellWBase, RectMin, tempEdgeMin, Mincalcmode);

            return (max - min);
        }

        private double CalLengthDistance2(CalcMode Mincalcmode, CalcMode Maxcalcmode)
        {
            Cst.Struct_EdgeMultiplePoint RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint;
            Cst.Struct_EdgeMultiplePoint LeftEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint;
            Rectangle RightRect = new Rectangle();
            Rectangle LeftRect = new Rectangle();

            double Max, Min;

            //确定左边缘的查找区域
            if (PointCellLT.X < PointCellLB.X)
            {
                LeftRect.X = (int)(PointCellLT.X - MEAS_ROI_OUT_PIXEL);
                LeftRect.Width = (int)(PointCellLB.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            else
            {
                LeftRect.X = (int)(PointCellLB.X - MEAS_ROI_OUT_PIXEL);
                LeftRect.Width = (int)(PointCellLT.X + MEAS_ROI_OUT_PIXEL * 3);
            }
            LeftRect.Y = (int)(PointCellLT.Y);
            LeftRect.Height = (int)(PointCellLB.Y);


            //确定右边缘的查找区域
            if (PointNiTabLB.Y > PointAlTabLB.Y)
            {
                //Ni在Al下方
                if (PointNiTabLB.X < PointAlTabLT.X)
                {
                    RightRect.X = (int)(PointNiTabLT.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointAlTabLB.X + MEAS_ROI_OUT_PIXEL);
                }
                else
                {
                    RightRect.X = (int)(PointAlTabLB.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointNiTabLT.X + MEAS_ROI_OUT_PIXEL);
                }
                RightRect.Y = (int)(PointAlTabLB.Y + (SEALANT_WIDTH_PIXEL + 20));
                RightRect.Height = (int)(PointNiTabLT.Y - (SEALANT_WIDTH_PIXEL + 20));
            }
            else
            {
                //Ni在Al上方
                if (PointAlTabLB.X < PointNiTabLT.X)
                {
                    RightRect.X = (int)(PointNiTabLB.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointAlTabLT.X + MEAS_ROI_OUT_PIXEL);
                }
                else
                {
                    RightRect.X = (int)(PointAlTabLT.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointNiTabLB.X + MEAS_ROI_OUT_PIXEL);
                }
                RightRect.Y = (int)(PointNiTabLB.Y + (SEALANT_WIDTH_PIXEL + 20));
                RightRect.Height = (int)(PointAlTabLT.Y - (SEALANT_WIDTH_PIXEL + 20));
            }

            Max = CalDistance(CellLBase, RightRect, RightEdge, Maxcalcmode);
            //Min = CalDistance(CellLBase, LeftRect, LeftEdge, Mincalcmode);

            List<Cst.dPoint> temp = GetEdgePoints(LeftRect, LeftEdge.Dir, LeftEdge.PrintMultiplePoint);
            Min = CalDistance(CellLBase, null, temp, LeftEdge.Dir, Mincalcmode, 5, 10);
            return (Max - Min);
        }

        private double CalAlTabLength2(CalcMode Mincalcmode, CalcMode Maxcalcmode)
        {
            Cst.Struct_EdgeMultiplePoint AlRightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint;
            Cst.Struct_EdgeMultiplePoint RightEdge;
            Rectangle AlRightRect = new Rectangle();
            Rectangle tempRect = new Rectangle();
            double Max, Min;

            if (PointNiTabLT.Y > PointAlTabLT.Y)
            {
                RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint;
                tempRect.X = (int)PointCellRT.X - MEAS_ROI_OUT_PIXEL;
                tempRect.Width = (int)PointCellRT.X + MEAS_ROI_OUT_PIXEL;
                tempRect.Y = (int)(PointCellRT.Y);
                tempRect.Height = (int)(PointCellRT.Y + RIGHTEDGE_SHOLDER_MM / m_CamParam.CameraKY);
            }
            else
            {
                RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint;
                tempRect.X = (int)PointCellRB.X - MEAS_ROI_OUT_PIXEL;
                tempRect.Width = (int)PointCellRB.X + MEAS_ROI_OUT_PIXEL;
                tempRect.Y = (int)(PointCellRB.Y - RIGHTEDGE_SHOLDER_MM / m_CamParam.CameraKY);
                tempRect.Height = (int)(PointCellRB.Y);
            }
            if (PointAlTabRT.X < PointAlTabRB.X)
            {
                AlRightRect.X = (int)(PointAlTabRT.X - MEAS_ROI_OUT_PIXEL * 2);
                AlRightRect.Width = (int)(PointAlTabRB.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            else
            {
                AlRightRect.X = (int)(PointAlTabRB.X - MEAS_ROI_OUT_PIXEL * 2);
                AlRightRect.Width = (int)(PointAlTabRT.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            AlRightRect.Y = (int)(PointAlTabRT.Y + (AlRightEdge.IgnoreLength1 / m_CamParam.CameraKY));
            AlRightRect.Height = (int)(PointAlTabRB.Y - (AlRightEdge.IgnoreLength2 / m_CamParam.CameraKY));

            double dis, dis1, dis2, dis3;
            Max = CalDistance(CellLBase, AlRightRect, AlRightEdge, Maxcalcmode, 5, 10);
            Min = CalDistance(CellLBase, tempRect, RightEdge, Mincalcmode, 5, 10);
            dis1 = Max - Min;
            return dis1;
        }

        private double CalNITabLength2(CalcMode Mincalcmode, CalcMode Maxcalcmode)
        {
            Cst.Struct_EdgeMultiplePoint NiRightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint;
            Cst.Struct_EdgeMultiplePoint RightEdge;
            Rectangle RightRect = new Rectangle();
            Rectangle NiRightRect = new Rectangle();
            double Max, Min;

            if (PointNiTabLT.Y < PointAlTabLT.Y)
            {
                RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint;
                RightRect.X = (int)PointCellRT.X - MEAS_ROI_OUT_PIXEL;
                RightRect.Width = (int)PointCellRT.X + MEAS_ROI_OUT_PIXEL;
                RightRect.Y = (int)(PointCellRT.Y);
                RightRect.Height = (int)(PointCellRT.Y + RIGHTEDGE_SHOLDER_MM / m_CamParam.CameraKY);
            }
            else
            {
                RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint;
                RightRect.X = (int)PointCellRB.X - MEAS_ROI_OUT_PIXEL;
                RightRect.Width = (int)PointCellRB.X + MEAS_ROI_OUT_PIXEL;
                RightRect.Y = (int)(PointCellRB.Y - RIGHTEDGE_SHOLDER_MM / m_CamParam.CameraKY);
                RightRect.Height = (int)(PointCellRB.Y);
            }

            if (PointNiTabRT.X < PointNiTabRB.X)
            {
                NiRightRect.X = (int)(PointNiTabRT.X - MEAS_ROI_OUT_PIXEL * 2);
                NiRightRect.Width = (int)(PointNiTabRB.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            else
            {
                NiRightRect.X = (int)(PointNiTabRB.X - MEAS_ROI_OUT_PIXEL * 2);
                NiRightRect.Width = (int)(PointNiTabRT.X + MEAS_ROI_OUT_PIXEL * 2);
            }
            NiRightRect.Y = (int)(PointNiTabRT.Y + (NiRightEdge.IgnoreLength1 / m_CamParam.CameraKY));
            NiRightRect.Height = (int)(PointNiTabRB.Y - (NiRightEdge.IgnoreLength2 / m_CamParam.CameraKY));
            Cst.Struct_EdgeMultiplePoint tempEdge = new Cst.Struct_EdgeMultiplePoint();
            double dis, dis1, dis2, dis3;
            Max = CalDistance(CellLBase, NiRightRect, NiRightEdge, Maxcalcmode, 5, 10);
            Min = CalDistance(CellLBase, RightRect, RightEdge, Mincalcmode, 5, 10);
            dis1 = Max - Min;
            return dis1;
        }

        private double CalNITabDis2(CalcMode calcmode)
        {
            Cst.Struct_EdgeMultiplePoint NiTabEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                NiTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint;
            else
                NiTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint;

            Rectangle tempRect = new Rectangle();

            tempRect.X = (int)(NiTabEdge.FirstEndPoint.X + NiTabEdge.IgnoreLength1 / m_CamParam.CameraKX);
            tempRect.Width = (int)(NiTabEdge.SecondEndPoint.X - NiTabEdge.IgnoreLength2 / m_CamParam.CameraKX);
            if (tempRect.Width - tempRect.X < 1 / m_CamParam.CameraKX) tempRect.X = tempRect.Width - (int)(1 / m_CamParam.CameraKX);
            if (NiTabEdge.FirstEndPoint.Y < NiTabEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(NiTabEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(NiTabEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }
            else
            {
                tempRect.Y = (int)(NiTabEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(NiTabEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }

            Cst.Struct_EdgeMultiplePoint TempEdge = new Cst.Struct_EdgeMultiplePoint();
            TempEdge = NiTabEdge;
            double dis1, dis2, dis3, dis;
            dis1 = CalDistance(BatBaseH, tempRect, NiTabEdge, calcmode, 0);
            return dis1;
        }

        private double CalAlTabDis2(CalcMode calcmode)
        {

            Cst.Struct_EdgeMultiplePoint AlTabEdge;
            if (VSGlobalControl.m_VParam.UseBottomAsBase)
                AlTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint;
            else
                AlTabEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint;
            Rectangle tempRect = new Rectangle();

            tempRect.X = (int)(AlTabEdge.FirstEndPoint.X + AlTabEdge.IgnoreLength1 / m_CamParam.CameraKX);
            tempRect.Width = (int)(AlTabEdge.SecondEndPoint.X - AlTabEdge.IgnoreLength2 / m_CamParam.CameraKX);
            if (tempRect.Width - tempRect.X < 1 / m_CamParam.CameraKX) tempRect.X = tempRect.Width - (int)(1 / m_CamParam.CameraKX);
            if (AlTabEdge.FirstEndPoint.Y < AlTabEdge.SecondEndPoint.Y)
            {
                tempRect.Y = (int)(AlTabEdge.FirstEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(AlTabEdge.SecondEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }
            else
            {
                tempRect.Y = (int)(AlTabEdge.SecondEndPoint.Y - MEAS_ROI_OUT_PIXEL);
                tempRect.Height = (int)(AlTabEdge.FirstEndPoint.Y + MEAS_ROI_OUT_PIXEL);
            }

            double dis1, dis2, dis3, dis;
            dis1 = CalDistance(BatBaseH, tempRect, AlTabEdge, calcmode, 0);
            return dis1;
        }

        private double CalAlSealantHeight2()
        {
            return CalSealantHeight2(PointAlTabLT, PointAlTabLB, VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlSealant].EdgeMultiplePoint.Thres);
        }

        private double CalNISealantHeight2()
        {
            return CalSealantHeight2(PointNiTabLT, PointNiTabLB, VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiSealant].EdgeMultiplePoint.Thres);
        }

        private double CalSealantHeight2(Cst.dPoint TopPoint, Cst.dPoint BottomPoint, int Thres)
        {
            Rectangle tempRect = new Rectangle();
            Rectangle tempRectUp = new Rectangle();
            Rectangle tempRectDn = new Rectangle();
            Rectangle RightRect = new Rectangle();
            double Max, Min;

            Cst.Struct_EdgeMultiplePoint RightEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint;
            Cst.Struct_EdgeMultiplePoint edge = new Cst.Struct_EdgeMultiplePoint();
            //edge.Thres = Thres;
            edge.Dir = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.Dir;
            edge.PrintMultiplePoint = true;
            edge.Robust.RobustSampleNum = 3;
            edge.Robust.MaxRobustCount = 100;

            //Sealant查找区域
            tempRectUp.X = (int)(TopPoint.X + 5);
            tempRectUp.Width = (int)(TopPoint.X + 75);
            tempRectDn.X = (int)(BottomPoint.X + 5);
            tempRectDn.Width = (int)(BottomPoint.X + 75);

            tempRectUp.Y = (int)(TopPoint.Y - SEALANT_WIDTH_PIXEL);
            tempRectUp.Height = (int)(TopPoint.Y - 2);
            tempRectDn.Y = (int)(BottomPoint.Y + 2);
            tempRectDn.Height = (int)(BottomPoint.Y + SEALANT_WIDTH_PIXEL);

            //极耳间凹槽查找区域
            if (PointNiTabLB.Y > PointAlTabLB.Y)
            {
                //Ni在Al下方
                if (PointNiTabLT.X < PointAlTabLB.X)
                {
                    RightRect.X = (int)(PointNiTabLT.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointAlTabLB.X + MEAS_ROI_OUT_PIXEL);
                }
                else
                {
                    RightRect.X = (int)(PointAlTabLB.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointNiTabLT.X + MEAS_ROI_OUT_PIXEL);
                }
                RightRect.Y = (int)(PointAlTabLB.Y + (SEALANT_WIDTH_PIXEL + 20));
                RightRect.Height = (int)(PointNiTabLT.Y - (SEALANT_WIDTH_PIXEL + 20));
            }
            else
            {
                //Ni在Al上方
                if (PointNiTabLB.X < PointAlTabLT.X)
                {
                    RightRect.X = (int)(PointNiTabLB.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointAlTabLT.X + MEAS_ROI_OUT_PIXEL);
                }
                else
                {
                    RightRect.X = (int)(PointAlTabLT.X - MEAS_ROI_OUT_PIXEL * 3);
                    RightRect.Width = (int)(PointNiTabLB.X + MEAS_ROI_OUT_PIXEL);
                }
                RightRect.Y = (int)(PointNiTabLB.Y + (SEALANT_WIDTH_PIXEL + 20));
                RightRect.Height = (int)(PointAlTabLT.Y - (SEALANT_WIDTH_PIXEL + 20));
            }
            Cst.Struct_Line[] MeasLine = new Cst.Struct_Line[2];
            List<Cst.dPoint> pointsUp = new List<Cst.dPoint>();
            List<Cst.dPoint> pointsDn = new List<Cst.dPoint>();
            List<Cst.dPoint> CPoint = new List<Cst.dPoint>();
            GetMeasurePoints(tempRectUp, edge, pointsUp, out MeasLine[0], true, 0.02);
            if (pointsUp.Count > 0)
                CPoint.Add(GetAvg(pointsUp.ToArray()));
            GetMeasurePoints(tempRectDn, edge, pointsDn, out MeasLine[1], true, 0.02);
            if (pointsDn.Count > 0)
                CPoint.Add(GetAvg(pointsDn.ToArray()));
            if (MeasStd)
                Max = CalDistance(CellLBase, new List<Cst.Struct_Line>().ToArray(), CPoint, edge.Dir, CalcMode.Avg);
            else
                Max = CalDistance(CellLBase, MeasLine, CPoint, edge.Dir, CalcMode.Max, 0, 5);
            Min = CalDistance(CellLBase, RightRect, RightEdge, CalcMode.Avg);
            return (Max - Min);
        }

        private double CalShoulderWidth2(CalcMode Mincalcmode, CalcMode Maxcalcmode)
        {
            Rectangle TopRect = new Rectangle();
            Rectangle BottomRect = new Rectangle();
            double Max, Min;

            Cst.Struct_EdgeMultiplePoint TopEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint;
            Cst.Struct_EdgeMultiplePoint BottomEdge = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint;

            TopRect.X = (int)(PointCellRT.X - (TOP_SHOLDER_WIDTH_MM) / m_CamParam.CameraKX);
            TopRect.Width = (int)(PointCellRT.X);
            TopRect.Y = (int)(PointCellRT.Y - MEAS_ROI_OUT_PIXEL);
            TopRect.Height = (int)(PointCellRT.Y + MEAS_ROI_OUT_PIXEL);

            BottomRect.X = (int)(PointCellRB.X - (BOTTOM_SHOLDER_WIDTH_MM) / m_CamParam.CameraKX);
            BottomRect.Width = (int)(PointCellRB.X);
            BottomRect.Y = (int)(PointCellRB.Y - MEAS_ROI_OUT_PIXEL);
            BottomRect.Height = (int)(PointCellRB.Y + MEAS_ROI_OUT_PIXEL);

            if (VSGlobalControl.m_VParam.UseBottomAsBase)
            {
                Min = CalDistance(CellWBase, BottomRect, BottomEdge, Mincalcmode);
                Max = CalDistance(CellWBase, TopRect, TopEdge, Maxcalcmode);
            }
            else
            {
                Min = CalDistance(CellWBase, TopRect, TopEdge, Mincalcmode);
                Max = CalDistance(CellWBase, BottomRect, BottomEdge, Maxcalcmode);
            }
            return (Max - Min);
        }
        public void SaveData(Cst.dPoint[] points)
        {
            string tempFile = "d:\\test.csv";
            string data = "";
            StreamWriter swTemp = new StreamWriter(tempFile, false, System.Text.Encoding.Default);
            foreach (Cst.dPoint point in points)
            {
                data = point.X + "," + point.Y;
                swTemp.WriteLine(data);
            }
            swTemp.Close();
        }
        private Cst.dPoint PatMatch(int PatNo, double score, Cst.dPoint patpoint)
        {
            double ratiox = VSGlobalControl.PAT_FIND_AREA_X_MM / m_CamParam.CameraKX;
            double ratioy = VSGlobalControl.PAT_FIND_AREA_Y_MM / m_CamParam.CameraKY;
            Rectangle sRect = new Rectangle((int)(patpoint.X - ratiox), (int)(patpoint.Y - ratioy), (int)(patpoint.X + ratiox), (int)(patpoint.Y + ratioy));
            Vision_DrawRect(sRect, VSBase.COLOR_YELLOW);
            double cx = 0;
            double cy = 0;
            double Angle = 0;
            double Score = 0;

            int ret = 0;

            switch (_vs)
            {
                case BufferIndex.RightCell:
                    ret = VSRightCell.Vision_FindPat(PatNo, sRect, score, false, ref cx, ref cy, ref Angle, ref Score);
                    break;
                case BufferIndex.MiddleCell:
                    ret = VSMiddleCell.Vision_FindPat(PatNo, sRect, score, false, ref cx, ref cy, ref Angle, ref Score);
                    break;
                case BufferIndex.LeftCell:
                    ret = VSLeftCell.Vision_FindPat(PatNo, sRect, score, false, ref cx, ref cy, ref Angle, ref Score);
                    break;
                case BufferIndex.Test:
                    ret = VSTest.Vision_FindPat(PatNo, sRect, score, false, ref cx, ref cy, ref Angle, ref Score);
                    break;
            }

            if (ret > 0)
                Vision_DrawCross(Convert.ToInt32(cx), Convert.ToInt32(cy), 5, VSBase.COLOR_GREEN, 2);
            return new Cst.dPoint(cx, cy);
        }
        public void FindPat(bool isCell)
        {
            Cst.dPoint temp = new Cst.dPoint();
            VSGlobalControl.PAT_FIND_AREA_X_MM = 15;
            VSGlobalControl.PAT_FIND_AREA_Y_MM = 15;
            if (isCell)
            {
                PointCellLT = PatMatch(2, 60, VSGlobalControl.m_VParam.TopLeft);
                PointCellLB = PatMatch(3, 60, VSGlobalControl.m_VParam.BottomLeft);
            }
            else
            {
                PointCellLT = PatMatch(2, 60, VSGlobalControl.m_VGaugeParam.TopLeft);
                PointCellLB = PatMatch(3, 60, VSGlobalControl.m_VGaugeParam.BottomLeft);
            }
            VSGlobalControl.PAT_FIND_AREA_X_MM = VSGlobalControl.PAT_WIDTH_MM * 3;
            VSGlobalControl.PAT_FIND_AREA_Y_MM = VSGlobalControl.PAT_HEIGHT_MM * 3;
            double k = -1 / Cst.dPoint.GetK(PointCellLT, PointCellLB);
            if (isCell)
            {
                temp.X = PointCellLT.X + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.Mean / m_CamParam.CameraKX * Math.Cos(k);
                temp.Y = PointCellLT.Y + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.Mean / m_CamParam.CameraKY * Math.Sin(k);
            }
            else
            {
                temp.X = PointCellLT.X + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.StdMean / m_CamParam.CameraKX * Math.Cos(k);
                temp.Y = PointCellLT.Y + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.StdMean / m_CamParam.CameraKY * Math.Sin(k);
            }
            PointCellRT = PatMatch(0, 60, temp);
            if (isCell)
            {
                temp.X = PointCellLB.X + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.Mean / m_CamParam.CameraKX * Math.Cos(k);
                temp.Y = PointCellLB.Y + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.Mean / m_CamParam.CameraKY * Math.Sin(k);
            }
            else
            {
                temp.X = PointCellLB.X + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.StdMean / m_CamParam.CameraKX * Math.Cos(k);
                temp.Y = PointCellLB.Y + VSGlobalControl.SysParam.CurrentProductParam.CellDataSpec.CellLength.StdMean / m_CamParam.CameraKY * Math.Sin(k);
            }
            PointCellRB = PatMatch(1, 60, temp);
            //PointCellRT = PatMatch(0, 60, VSGlobalControl.m_VParam.TopRef);
            //PointCellRB = PatMatch(1, 60, VSGlobalControl.m_VParam.BottomRef);
        }
        private Cst.Struct_Line GetOneLine(Cst.dPoint point1, Cst.dPoint point2)
        {
            Cst.Struct_Line line = new Cst.Struct_Line();
            line.AvgX = (point1.X + point2.X) / 2;
            line.AvgY = (point1.Y + point2.Y) / 2;
            line.k = (point1.Y - point2.Y) / (point1.X - point2.X);
            if (double.IsNaN(line.k))
                line.k =
                    line.b = 99999999;
            else
                line.b = point1.Y - point1.X * line.k;
            return line;
        }
        public void CalCrossByPat()
        {
            Cst.Struct_Line TopBase;
            Cst.Struct_Line BottomBase;
            Cst.Struct_Line LeftBase;
            Cst.Struct_Line RightBase;
            Cst.Struct_Line NiTopBase;
            Cst.Struct_Line NiRightBase;
            Cst.Struct_Line NiBottomBase;
            Cst.Struct_Line AlTopBase;
            Cst.Struct_Line AlRightBase;
            Cst.Struct_Line AlBottomBase;
            Rectangle tempRect;
            FindPat(!MeasStd);
            //取右边缘
            RightBase = GetOneLine(PointCellRT, PointCellRB);
            //取左边缘
            LeftBase = GetOneLine(PointCellLT, PointCellLB);
            //取上边缘
            TopBase = GetOneLine(PointCellLT, PointCellRT);
            //取下边缘
            BottomBase = GetOneLine(PointCellLB, PointCellRB);
            //取Ni上边缘
            if (MeasStd)
            {
                tempRect = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.Mean / m_CamParam.CameraKX);
            }
            NiTopBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_BLUE);
            //取Ni下边缘
            if (MeasStd)
            {
                tempRect = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.Mean / m_CamParam.CameraKX);
            }
            NiBottomBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_GREEN);
            //取Al上边缘
            if (MeasStd)
            {
                tempRect = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.Mean / m_CamParam.CameraKX);
            }
            AlTopBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_BLUE);
            //取Al下边缘
            if (MeasStd)
            {
                tempRect = VSGlobalControl.m_VGaugeParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.Rect;
                tempRect.X = (int)(RightBase.AvgX + 5 + VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX - 5 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.Mean / m_CamParam.CameraKX);
            }
            AlBottomBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_GREEN);

            TwoLineCrossPoint(NiTopBase, RightBase, ref PointNiTabLT, true);
            TwoLineCrossPoint(NiBottomBase, RightBase, ref PointNiTabLB, true);
            TwoLineCrossPoint(AlTopBase, RightBase, ref PointAlTabLT, true);
            TwoLineCrossPoint(AlBottomBase, RightBase, ref PointAlTabLB, true);

            //取Ni右边缘
            tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.Rect;
            if (MeasStd)
            {
                tempRect.X = (int)(RightBase.AvgX - 200 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX + 200 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect.X = (int)(RightBase.AvgX - 200 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX + 200 + VSGlobalControl.CurrentCellDataSpec.NiTabLength.Mean / m_CamParam.CameraKX);
            }
            tempRect.Y = (int)NiTopBase.AvgY;
            tempRect.Height = (int)NiBottomBase.AvgY;
            NiRightBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_YELLOW);
            //取Al右边缘
            tempRect = VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.Rect;
            if (MeasStd)
            {
                tempRect.X = (int)(RightBase.AvgX - 200 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.StdMean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX + 200 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.StdMean / m_CamParam.CameraKX);
            }
            else
            {
                tempRect.X = (int)(RightBase.AvgX - 200 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.Mean / m_CamParam.CameraKX);
                tempRect.Width = (int)(RightBase.AvgX + 200 + VSGlobalControl.CurrentCellDataSpec.AlTabLength.Mean / m_CamParam.CameraKX);
            }
            tempRect.Y = (int)AlTopBase.AvgY;
            tempRect.Height = (int)AlBottomBase.AvgY;
            AlRightBase = GetOneLine(VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge], tempRect, 0.1);
            Vision_DrawRect(tempRect, VSBase.COLOR_YELLOW);

            TwoLineCrossPoint(NiTopBase, NiRightBase, ref PointNiTabRT, true);
            TwoLineCrossPoint(NiBottomBase, NiRightBase, ref PointNiTabRB, true);
            TwoLineCrossPoint(AlTopBase, AlRightBase, ref PointAlTabRT, true);
            TwoLineCrossPoint(AlBottomBase, AlRightBase, ref PointAlTabRB, true);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.TopEdge].EdgeMultiplePoint.SecondEndPoint = PointCellRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.LeftEdge].EdgeMultiplePoint.SecondEndPoint = PointCellLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.FirstEndPoint = PointCellLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.BottomEdge].EdgeMultiplePoint.SecondEndPoint = PointCellRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.FirstEndPoint = PointCellRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeUp].EdgeMultiplePoint.SecondEndPoint = (PointAlTabLT.Y < PointNiTabLT.Y ? PointAlTabLT : PointNiTabLT);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.FirstEndPoint = PointCellRT;// (AlTabLT.Y < NiTabLT.Y ? AlTabLB : NiTabLB);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeMid].EdgeMultiplePoint.SecondEndPoint = PointCellRB;//(AlTabLT.Y < NiTabLT.Y ? NiTabLT : AlTabLT);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.FirstEndPoint = (PointAlTabLT.Y < PointNiTabLT.Y ? PointNiTabLB : PointAlTabLB);
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.RightEdgeDn].EdgeMultiplePoint.SecondEndPoint = PointCellRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlBottomEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlRightEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.FirstEndPoint = PointAlTabLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.AlTopEdge].EdgeMultiplePoint.SecondEndPoint = PointAlTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabLB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiBottomEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabRT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiRightEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRB;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.FirstEndPoint = PointNiTabLT;
            VSGlobalControl.m_VParam.Im[(int)CamCDIRoi.NiTopEdge].EdgeMultiplePoint.SecondEndPoint = PointNiTabRT;
        }
    }
}