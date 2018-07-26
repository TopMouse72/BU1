using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule.ToolBox;
using SVSCamera;
using System.Net.Sockets;
using VisionUnit;

namespace CDIVisionControl
{

    public partial class MainForm : Form, IVisionSubscriber, IResponseSubscriber
    {
        private FormCalibration VisionCalibration;
        private TcpClient client;
        private NetworkStream stream;
        string version;
        VisionControl[] VisionList;
        public bool imagedone = true;
        private object WinSockWriteLock = new object();
        private void WriteWinSock(string str)
        {
            lock (WinSockWriteLock)
            {
                if (stream != null)
                    stream.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
            }
        }
        public MainForm()
        {
            InitializeComponent();
            cameraSetting1.DispPanel = panelCamera;
            VisionCalibration = FormCalibration.Instance;
            cameraSetting1.GetCameraInfo();
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            VisionList = new VisionControl[Enum.GetNames(typeof(BufferIndex)).Length];

            VisionCalibration.CamUpdate += VisionSetting1.UpdateCameraPara;

            VisionSetting1.ShotOneFrame = ShotOneFrame;
            VisionSetting1.SetVisionIndex = SetVisionIndex;
            for (int i = 0; i < Enum.GetNames(typeof(BufferIndex)).Length; i++) VSCamTrig[i] = false;

            VSGlobalControl.ShotOneCallBack = ShotOneFrame;
        }
        private void SetVisionIndex(BufferIndex index)
        {
            switch (index)
            {
                case BufferIndex.RightCell:
                    optionBox1.Checked = true;
                    break;
                case BufferIndex.MiddleCell:
                    optionBox2.Checked = true;
                    break;
                case BufferIndex.LeftCell:
                    optionBox3.Checked = true;
                    break;
                case BufferIndex.Test:
                    optionBox4.Checked = true;
                    break;
            }

        }
        public void DoneHandler(BaseClass sender, StateEventArgs e)
        {
            string strResp;
            string VsNum;
            string szTime;
            int index;
            VsNum = e.eventName.Substring(9, 1);

            if (e.eventName.Substring(0, 8) == MEASDONE)
            {
                index = int.Parse(e.eventInfo);

                strResp = $"{e.eventName}," +
                    $"{ VisionList[index].Barcode}," +
                    $"{VisionList[index].CellDataSpec.CellWidth.Value}," +
                    $"{VisionList[index].CellDataSpec.CellLength.Value}," +
                    $"{VisionList[index].CellDataSpec.AlTabDistance.Value}," +
                    $"{VisionList[index].CellDataSpec.NiTabDistance.Value}," +
                    $"{VisionList[index].CellDataSpec.AlTabDistanceMax.Value}," +
                    $"{VisionList[index].CellDataSpec.NiTabDistanceMax.Value}," +
                    $"{VisionList[index].CellDataSpec.AlTabLength.Value}," +
                    $"{VisionList[index].CellDataSpec.NiTabLength.Value}," +
                    $"{VisionList[index].CellDataSpec.TabDistance.Value}," +
                    $"{VisionList[index].CellDataSpec.AlSealantHeight.Value}," +
                    $"{VisionList[index].CellDataSpec.NiSealantHeight.Value}," +
                    $"{VisionList[index].CellDataSpec.ShoulderWidth.Value}," +
                    $"End;";
                if (stream != null)
                {
                    DateTime temp = DateTime.Now;
                    string RecTime = temp.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    WriteWinSock(strResp);
                    BaseForm.SetControlText(textBoxStatus, RecTime + "\t<--" + strResp + Environment.NewLine);
                }
                BaseForm.DoInvokeRequired(listBoxMeasResult, () =>
                {
                    listBoxMeasResult.Items.Clear();
                    listBoxMeasResult.Items.AddRange(VisionList[index].GetDispResult());
                });
            }
        }

        public void EnableChangeHandler(BaseClass sender, bool Enabled)
        {
        }

        public void ErrorHandler(BaseClass sender, FailureException e)
        {
        }
        bool[] VSCamTrig = new bool[Enum.GetNames(typeof(BufferIndex)).Length];
        public void ImageCapturedHandler(string CameraName, byte[] ImageBuffer)
        {
            trigspan = DateTime.Now - trigstart;
            BaseForm.SetControlText(Controls["labelTrig" + trigindex.ToString()], trigspan.TotalMilliseconds.ToString());
            DateTime temp = DateTime.Now;
            string RecTime = temp.ToString("yyyy-MM-dd HH:mm:ss") + "." + temp.Millisecond.ToString("000");
            string strResp = null;

            if (VSCamTrig[0])
            {
                BaseForm.SetControlText(textBoxStatus, RecTime + "\t" + "T1 end with laps " + trigspan.TotalMilliseconds.ToString() + "ms" + Environment.NewLine);
                optionBox1.Checked = true;
                strResp = "CaptureFinish:1;";
                VSCamTrig[0] = false;
                WriteWinSock(strResp);
            }
            else if (VSCamTrig[1])
            {
                BaseForm.SetControlText(textBoxStatus, RecTime + "\t" + "T2 end with laps " + trigspan.TotalMilliseconds.ToString() + "ms" + Environment.NewLine);
                optionBox2.Checked = true;
                strResp = "CaptureFinish:2;";
                VSCamTrig[1] = false;
                WriteWinSock(strResp);
            }
            else if (VSCamTrig[2])
            {
                BaseForm.SetControlText(textBoxStatus, RecTime + "\t" + "T3 end with laps " + trigspan.TotalMilliseconds.ToString() + "ms" + Environment.NewLine);
                optionBox3.Checked = true;
                strResp = "CaptureFinish:3;";
                VSCamTrig[2] = false;
                WriteWinSock(strResp);
            }
            else if (VSCamTrig[3])
            {
                BaseForm.SetControlText(textBoxStatus, RecTime + "\t" + "T4 end with laps " + trigspan.TotalMilliseconds.ToString() + "ms" + Environment.NewLine);
                optionBox4.Checked = true;
                strResp = "CaptureFinish:4;";
                VSCamTrig[3] = false;
                WriteWinSock(strResp);
            }
            //imagedone = true;
        }

        public void InvalidateHandler(BaseClass sender)
        {
        }

        private void Connect()
        {
            if (client != null)
            {
                //recieveThread.Abort();
                stream.Close();
                stream.Dispose();
                client.Close();
            }
            try
            {
                //client = new TcpClient("127..16.73.64", 2014);
                client = new TcpClient("127.0.0.1", 2014);
                stream = client.GetStream();
                timer1.Interval = 100;
                timer1.Start();
                //recieveThread = new Thread(new ThreadStart(ThreadReceive));
                //recieveThread.IsBackground = true;
                //recieveThread.Start();
            }
            catch
            {
                stream = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] data = new byte[client.ReceiveBufferSize];
            string received = string.Empty;
            if (client.Available > 0)
            {
                int recbytes = stream.Read(data, 0, data.Length);
                received = Encoding.Default.GetString(data, 0, recbytes);
                processdata(received);
            }
        }
        private void ShotOneFrame(object sender, EventArgs e)
        {
            VisionList[(int)sender].VSCamTrig = true;
            selectBoxRealDisp_Click(null, null);
            if (selectBoxRealDisp.Checked != false) selectBoxRealDisp.Checked = false;
            trigstart = DateTime.Now;
            VSCamTrig[(int)sender] = true;
            SVS_CameraSys.CameraList[0].SvsSnapImage();

        }
        private const string MEASDONE = "MeasDone";
        private const string CAPFINISH = "CaptureFinish";
        private const string BARCODE = "Bar";
        private const string PATH = "Path";
        private const string PRODUCT = "P";
        private const string CAMTRIG = "T";
        private const string MEASPIC = "M";
        private const string BATCHMEAS = "BatchMeas";
        private const string COMMAND_PRODUCT = PRODUCT + ":";
        private const string COMMAND_CAMTRIG = CAMTRIG + ":";
        private const string COMMAND_MEAS = MEASPIC + ":";
        private const string COMMAND_BATCH_MEAS = BATCHMEAS + ":";
        private const string COMMAND_END = ";";
        private const string COMMAND_BARCODE = BARCODE + ":";
        private const string COMMAND_PATH = PATH + ":";
        private const string COMMA = ",";
        private const string COMMAND_MEASPIC = MEASPIC + ":";
        private const string COMMAND_CAMLIVE = "CamLive;";
        private const string COMMAND_CAMSTOP = "CamStop;";
        private string CurrentProductName
        {
            get { return VisionSetting1.CurrentProductName; }
            set
            {
                VisionSetting1.CurrentProductName = value;
                this.Text = $"CDI Vision: {version} 当前产品: {VSGlobalControl.CurrentProductName} 使用标准块：{VSGlobalControl.UseGaugeName}";
                this.tabPage1.Text = $"图像设置  产品: {VSGlobalControl.CurrentProductName} 标准块：{VSGlobalControl.UseGaugeName}";
                DispProduct();
            }
        }
        private void DispProduct()
        {
            listBoxProduct.Items.Clear();
            listBoxProduct.Items.Add(VSGlobalControl.SysParam.CurrentProduct);
            listBoxProduct.Items.Add(string.Format("宽度:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.CellWidth.Mean,
                VSGlobalControl.CurrentCellDataSpec.CellWidth.Tolerance));
            listBoxProduct.Items.Add(string.Format("长度:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.CellLength.Mean,
                VSGlobalControl.CurrentCellDataSpec.CellLength.Tolerance));
            listBoxProduct.Items.Add(string.Format("Al边距:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.AlTabDistance.Mean,
                VSGlobalControl.CurrentCellDataSpec.AlTabDistance.Tolerance));
            listBoxProduct.Items.Add(string.Format("Ni边距:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.NiTabDistance.Mean,
                VSGlobalControl.CurrentCellDataSpec.NiTabDistance.Tolerance));
            listBoxProduct.Items.Add(string.Format("Al长度:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.AlTabLength.Mean,
                VSGlobalControl.CurrentCellDataSpec.AlTabLength.Tolerance));
            listBoxProduct.Items.Add(string.Format("Ni长度:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.NiTabLength.Mean,
                VSGlobalControl.CurrentCellDataSpec.NiTabLength.Tolerance));
            listBoxProduct.Items.Add(string.Format("Tab间距:\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.TabDistance.Mean,
                VSGlobalControl.CurrentCellDataSpec.TabDistance.Tolerance));
            listBoxProduct.Items.Add(string.Format("Al小白胶:\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.Mean,
                VSGlobalControl.CurrentCellDataSpec.AlSealantHeight.Tolerance));
            listBoxProduct.Items.Add(string.Format("Ni小白胶:\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.Mean,
                VSGlobalControl.CurrentCellDataSpec.NiSealantHeight.Tolerance));
            listBoxProduct.Items.Add(string.Format("肩宽:\t\t{0:0.000} +/-{1:0.000} mm",
                VSGlobalControl.CurrentCellDataSpec.ShoulderWidth.Mean,
                VSGlobalControl.CurrentCellDataSpec.ShoulderWidth.Tolerance));
        }
        private string GetFirst(string data, int startindex)
        {
            string first = "";
            int minindex = 9999;
            productindex = data.IndexOf(COMMAND_PRODUCT, startindex);
            if (productindex != -1 && productindex < minindex) { first = PRODUCT; minindex = productindex; }
            camtrigindex = data.IndexOf(COMMAND_CAMTRIG, startindex);
            if (camtrigindex != -1 && camtrigindex < minindex) { first = CAMTRIG; minindex = camtrigindex; }
            batchindex = data.IndexOf(COMMAND_BATCH_MEAS, startindex);
            if (batchindex != -1 && batchindex < minindex) { first = BATCHMEAS; minindex = batchindex; }
            measindex = data.IndexOf(COMMAND_MEAS, startindex);
            if (measindex != -1 && measindex < minindex) { first = MEASPIC; minindex = measindex; }
            camliveindex = data.IndexOf(COMMAND_CAMLIVE, startindex);
            if (camliveindex != -1 && camliveindex < minindex) { first = COMMAND_CAMLIVE; minindex = camliveindex; }
            camstopindex = data.IndexOf(COMMAND_CAMSTOP, startindex);
            if (camstopindex != -1 && camstopindex < minindex) { first = COMMAND_CAMSTOP; minindex = camstopindex; }
            return first;
        }
        DateTime trigstart;
        TimeSpan trigspan;
        int trigindex = 4, measpicindex = 4;
        int productindex = 0, camtrigindex = 0, batchindex = 0, measindex = 0, camliveindex = 0, camstopindex = 0;
        private void processdata(string recvdata)
        {
            string szRecv = recvdata;
            DateTime temp = DateTime.Now;
            string RecTime = temp.ToString("yyyy-MM-dd HH:mm:ss") + "." + temp.Millisecond.ToString("000");
            textBoxStatus.Text = RecTime + "\t-->" + recvdata + Environment.NewLine;
            string TrigNum = null;
            string MeasNum = null;
            int startindex = 0;
            int endindex = 0;
            int CodeIndex = 0, PathIndex = 0;
            int CommaIndex = 0;
            string comm = "";
            int index = 0;
            do
            {
                comm = GetFirst(szRecv, startindex);
                if (comm == "") return;
                switch (comm)
                {
                    case PRODUCT:
                        VSGlobalControl.SysParam.LoadParameter();
                        endindex = szRecv.IndexOf(COMMAND_END, productindex);
                        CurrentProductName = szRecv.Substring(productindex + COMMAND_PRODUCT.Length, endindex - productindex - COMMAND_PRODUCT.Length);
                        this.Refresh();
                        startindex = endindex;
                        break;
                    case CAMTRIG:
                        endindex = szRecv.IndexOf(COMMAND_END, camtrigindex);
                        CommaIndex = szRecv.IndexOf(COMMA, camtrigindex);
                        TrigNum = szRecv.Substring(camtrigindex + COMMAND_CAMTRIG.Length, CommaIndex - camtrigindex - COMMAND_CAMTRIG.Length);
                        index = int.Parse(TrigNum) - 1;
                        trigindex = index + 1;

                        CodeIndex = szRecv.IndexOf(BARCODE, CommaIndex);
                        CommaIndex = szRecv.IndexOf(COMMA, CodeIndex);
                        VisionList[index].Barcode = szRecv.Substring(CodeIndex + COMMAND_BARCODE.Length, CommaIndex - CodeIndex - COMMAND_BARCODE.Length);

                        PathIndex = szRecv.IndexOf(PATH, CommaIndex);
                        VisionList[index].szFilePath = szRecv.Substring(PathIndex + COMMAND_PATH.Length, endindex - PathIndex - COMMAND_PATH.Length);
                        VisionList[index].VSCamTrig = true;
                        ShotOneFrame(index, null);

                        startindex = endindex;
                        break;
                    case BATCHMEAS:
                        endindex = szRecv.IndexOf(COMMAND_END, batchindex);
                        string batchpath = szRecv.Substring(batchindex + COMMAND_BATCH_MEAS.Length, endindex - batchindex - COMMAND_BATCH_MEAS.Length);
                        VisionSetting1.BatchMeas(batchpath);
                        startindex = endindex;
                        break;
                    case MEASPIC:
                        endindex = szRecv.IndexOf(COMMAND_END, measindex);
                        int optionIndex = szRecv.IndexOf(COMMA, measindex);
                        bool isStandard = bool.Parse(szRecv.Substring(measindex + COMMAND_MEASPIC.Length, optionIndex - measindex - COMMAND_MEASPIC.Length));
                        PathIndex = szRecv.IndexOf(PATH, measindex);
                        VisionList[(int)BufferIndex.Test].szFilePath = szRecv.Substring(PathIndex + COMMAND_PATH.Length, endindex - PathIndex - COMMAND_PATH.Length);
                        if (VisionSetting1.Barcode == "") VisionSetting1.Barcode = "TEST";
                        if (isStandard && VisionSetting1.Barcode.IndexOf("Standard") < 0) VisionSetting1.Barcode += "Standard";
                        VisionList[(int)BufferIndex.Test].StartMeas(VisionSetting1.Barcode, false);

                        startindex = endindex;
                        break;
                    case COMMAND_CAMLIVE:
                        endindex = szRecv.IndexOf(COMMAND_END, camliveindex);
                        selectBoxRealDisp.Checked = true;
                        startindex = endindex;
                        break;
                    case COMMAND_CAMSTOP:
                        endindex = szRecv.IndexOf(COMMAND_END, camstopindex);
                        selectBoxRealDisp.Checked = false;
                        startindex = endindex;
                        break;
                }
            }
            while (true);
        }
        private void ShowPosition(Point mousepoint)
        {
            labelPosition.Text = string.Format("({0},{1})", mousepoint.X, mousepoint.Y);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            VSGlobalControl.InitParm();
            for (int i = 0; i < Enum.GetNames(typeof(BufferIndex)).Length; i++)
            {
                VisionList[i] = new VisionControl((BufferIndex)i);
                VisionList[i].ResPublisher.subscribeMeToResponseEvents(this);
            }
            VisionCalibration.Owner = VisionList[(int)BufferIndex.Test];

            SVS_CameraSys.getCameras();
            if (SVS_CameraSys.CameraCount > 0)
            {
                SVS_CameraSys.CameraList[0].subscribeMeToVisionEvents(this);
                SVS_CameraSys.CameraList[0].subscribeMeToVisionEvents(VisionList[0]);
                SVS_CameraSys.CameraList[0].subscribeMeToVisionEvents(VisionList[1]);
                SVS_CameraSys.CameraList[0].subscribeMeToVisionEvents(VisionList[2]);
                SVS_CameraSys.CameraList[0].subscribeMeToVisionEvents(VisionList[3]);
                SVS_CameraSys.CameraList[0].SetSVSCameraMode(1);
                VSGlobalControl.m_CamParam.CameraSize.Width = SVS_CameraSys.CameraList[0].camWidth;
                VSGlobalControl.m_CamParam.CameraSize.Height = SVS_CameraSys.CameraList[0].camHeight;
            }

            //cameraset.cameraSetting1.SetCamera();
            cameraSetting1.SetCamera();
            VisionList[(int)BufferIndex.RightCell].VisionInit(CamDisplayRightCell, ShowPosition);
            VisionList[(int)BufferIndex.MiddleCell].VisionInit(CamDisplayMiddleCell, ShowPosition);
            VisionList[(int)BufferIndex.LeftCell].VisionInit(CamDisplayLeftCell, ShowPosition);
            VisionList[(int)BufferIndex.Test].VisionInit(CamDisplayTest, ShowPosition);

            CurrentProductName = VSGlobalControl.SysParam.CurrentProduct;
            Connect();

            VisionSetting1.VisionList = VisionList;
            VisionSetting1.CameraDispList[(int)BufferIndex.RightCell] = CamDisplayRightCell;
            VisionSetting1.CameraDispList[(int)BufferIndex.MiddleCell] = CamDisplayMiddleCell;
            VisionSetting1.CameraDispList[(int)BufferIndex.LeftCell] = CamDisplayLeftCell;
            VisionSetting1.CameraDispList[(int)BufferIndex.Test] = CamDisplayTest;
            VisionSetting1.SettingInit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SVS_CameraSys.CameraList != null)
            {
                SVS_CameraSys.CameraList[0].unsubscribeMeFromVisionEvents(this);
                SVS_CameraSys.CameraList[0].unsubscribeMeFromVisionEvents(VisionList[0]);
                SVS_CameraSys.CameraList[0].unsubscribeMeFromVisionEvents(VisionList[1]);
                SVS_CameraSys.CameraList[0].unsubscribeMeFromVisionEvents(VisionList[2]);
                SVS_CameraSys.CameraList[0].unsubscribeMeFromVisionEvents(VisionList[3]);
                SVS_CameraSys.CameraList[0].SetSVSCameraMode(1);
            }
            System.Threading.Thread.Sleep(500);
            if (client != null) client.Close();
            SVS_CameraSys.ReleaseAllCamera();
            e.Cancel = false;
        }

        private void Measure1_CheckedChanged(object sender, EventArgs e)
        {
            for (int index = 0; index < Enum.GetNames(typeof(BufferIndex)).Length; index++)
            {
                VisionList[index].AsyncMeasure = VisionList[index].Measure;
            }
        }

        private void Measure2_CheckedChanged(object sender, EventArgs e)
        {
            for (int index = 0; index < Enum.GetNames(typeof(BufferIndex)).Length; index++)
            {
                VisionList[index].AsyncMeasure = VisionList[index].Measure2;
            }
        }

        private void buttonConnectCDI_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void optionBoxCamra_Click(object sender, EventArgs e)
        {
            if (((OptionBox)sender).Checked)
                switch (((OptionBox)sender).Text)
                {
                    case "右工位":
                        BaseForm.DoInvokeRequired(CamDisplayRightCell, () => CamDisplayRightCell.BringToFront());
                        break;
                    case "中工位":
                        BaseForm.DoInvokeRequired(CamDisplayMiddleCell, () => CamDisplayMiddleCell.BringToFront());
                        break;
                    case "左工位":
                        BaseForm.DoInvokeRequired(CamDisplayLeftCell, () => CamDisplayLeftCell.BringToFront());
                        break;
                    case "测试":
                        BaseForm.DoInvokeRequired(CamDisplayTest, () => CamDisplayTest.BringToFront());
                        break;
                }
            BaseForm.DoInvokeRequired(labelPosition, () => labelPosition.BringToFront());
        }
        private void optionBoxSaveNG_Click(object sender, EventArgs e)
        {
            VSGlobalControl.SaveOption = SaveImageOption.SaveNG;
        }

        private void VisionSetting1_CameraLive(object sender, EventArgs e)
        {
            selectBoxRealDisp.Checked = true;
        }

        private void VisionSetting1_CameraStop(object sender, EventArgs e)
        {
            selectBoxRealDisp.Checked = false;
        }

        private void optionBoxSaveAll_Click(object sender, EventArgs e)
        {
            VSGlobalControl.SaveOption = SaveImageOption.SaveAll;
        }

        private void buttonCalibration_Click(object sender, EventArgs e)
        {
            VisionCalibration.Show(this);
        }

        private void selectBoxRealDisp_Click(object sender, EventArgs e)
        {
            if (SVS_CameraSys.CameraList != null && SVS_CameraSys.CameraList[0] != null)
                if (selectBoxRealDisp.Checked)
                {
                    VisionSetting1.ClearMark();
                    optionBox4.Checked = true;
                    SVS_CameraSys.CameraList[0].SetSVSCameraMode(0);
                }
                else
                    SVS_CameraSys.CameraList[0].SetSVSCameraMode(1);
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text.IndexOf("图像设置") >= 0)
                optionBox4.Checked = true;
        }

        private void listBoxMeasResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0) return;
            if (listBoxMeasResult.Items[e.Index].ToString().Contains("OK"))
                e.Graphics.DrawString(listBoxMeasResult.Items[e.Index].ToString(), e.Font, Brushes.Green, e.Bounds);
            else if (listBoxMeasResult.Items[e.Index].ToString().Contains("NG"))
                e.Graphics.DrawString(listBoxMeasResult.Items[e.Index].ToString(), e.Font, Brushes.Red, e.Bounds);
            else
                e.Graphics.DrawString(listBoxMeasResult.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds);
        }
        private void buttonZoom_Click(object sender, EventArgs e)
        {
            int zoom = int.Parse(((Button)sender).Text.Replace("%",""));
            VSTest.Control_Zoom(zoom);
        }
    }
}