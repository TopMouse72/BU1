using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;//画图
using System.Threading;//线程

namespace SVSCamera
{
    public partial class CameraSetting : UserControl
    {
        SVS_Camera svscamera;//封装类对象

        //图像buffer结构体
        private struct imagebufferStruct
        {
            public byte[] imagebytes;
        }

        public bool CameraOprEnable
        {
            get { return AcquisitionMode_comboBox.Enabled; }
            set
            {
                AcquisitionMode_comboBox.Enabled =
                 Cam_start.Enabled =
                 SoftTrigger_button.Enabled =
                 Cam_stop.Enabled =
                 SaveImage.Enabled =
                 label1.Enabled = value;
            }
        }
        private imagebufferStruct[] outputbuffer;//黑白
        private imagebufferStruct[] outputbufferRGB;//彩色
        private ColorPalette imgpal = null;
        private Bitmap[] bimage;
        private Bitmap[] bimageRGB;
        private Rectangle outRectangle;
        private Graphics gpanel;
        private Thread imagproc;
        private bool threadStop = false;

        private bool drawing = false;
        private bool notFirstImage = false;
        private int IsColor = 0;

        public Panel DispPanel { get; set; }

        public CameraSetting()
        {
            InitializeComponent();
        }

        private void ConnectCamera()
        {
            notFirstImage = false;
            threadStop = true;
            svscamera.AddCallBack(MyDelegateFunc);
            imagproc = new Thread(new ThreadStart(this.imageProcessing));
            imagproc.IsBackground = true;
        }
        private void RemoveCamera()
        {
            if (svscamera == null) return;
            svscamera.StopLive();
            svscamera.RemoveCallBack(MyDelegateFunc);
        }
        //画图
        private void imageProcessing()
        {
            if (DispPanel == null) return;
            gpanel.Clear(DispPanel.BackColor);
            while (threadStop == false)
            {
                try
                {
                    if (drawing == true)
                    {
                        drawing = false;
                        if (IsColor != 0)
                        {
                            lock (bimageRGB[0])
                            {
                                gpanel.DrawImage(bimageRGB[0], outRectangle);
                            }
                        }
                        else if (IsColor == 0)
                        {
                            lock (bimage[0])
                            {
                                gpanel.DrawImage(bimage[0], outRectangle);
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        //委托拷贝图像数据
        public void MyDelegateFunc(byte[] imageBuffer, int Xsize, int Ysize, int color)
        {
            int size;
            IsColor = color;
            if (color == 0)
            {
                size = Xsize * Ysize;
                System.Buffer.BlockCopy(imageBuffer, 0, outputbuffer[0].imagebytes, 0, size);
            }
            else if (color == 1)
            {
                size = Xsize * Ysize * 3;
                System.Buffer.BlockCopy(imageBuffer, 0, outputbufferRGB[0].imagebytes, 0, size);

            }

            if (notFirstImage == false)
            {
                notFirstImage = true;
                if (threadStop == true)
                {
                    threadStop = false;
                    drawing = true;
                    imagproc.Start();
                }
            }
            else
            {
                drawing = true;
            }
        }

        //Buffer初始化
        public void InitBuffer(int CamWidth, int CamHeight)
        {
            outputbuffer = new imagebufferStruct[1];
            outputbuffer[0].imagebytes = new byte[CamHeight * CamWidth];
            bimage = new Bitmap[1];
            unsafe
            {
                fixed (byte* MonoPtr = outputbuffer[0].imagebytes)
                {
                    bimage[0] = new Bitmap(CamWidth, CamHeight, CamWidth, PixelFormat.Format8bppIndexed, (IntPtr)MonoPtr);
                    imgpal = bimage[0].Palette;

                    for (uint i = 0; i < 256; i++)
                    {
                        imgpal.Entries[i] = Color.FromArgb(

                           (byte)0xFF,
                           (byte)i,
                           (byte)i,
                           (byte)i);
                    }
                    bimage[0].Palette = imgpal;
                    imgpal = bimage[0].Palette;
                }
            }

            outputbufferRGB = new imagebufferStruct[1];
            outputbufferRGB[0].imagebytes = new byte[3 * CamHeight * CamWidth];
            bimageRGB = new Bitmap[1];

            unsafe
            {
                fixed (byte* ColorPtr = outputbufferRGB[0].imagebytes)
                {
                    bimageRGB[0] = new Bitmap(CamWidth, CamHeight, (3 * CamWidth), PixelFormat.Format24bppRgb, (IntPtr)ColorPtr);
                }
            }
            drawing = false;

            if (DispPanel == null) return;
            outRectangle = new Rectangle(0, 0, DispPanel.Width, DispPanel.Height);
            gpanel = DispPanel.CreateGraphics();
        }

        //查找相机
        private void Selected_device_Click(object sender, EventArgs e)
        {
            Selected_comboBox.Items.Clear();

            Selected_comboBox.Text = "查找中...";
            Selected_comboBox.Refresh();


            SVS_CameraSys.getCameras();//获得相机个数
            SetCamera();
        }
        public void SetCamera()
        {
            Selected_comboBox.Items.Clear();
            if (SVS_CameraSys.CameraList == null) return;
            int number = SVS_CameraSys.CameraList.Length;
            if (number > 0)
            {
                Selected_comboBox.Text = "选择相机";
                for (int n = 0; n < number; n++)
                {
                    Selected_comboBox.Items.Add(SVS_CameraSys.CameraList[n].modelname + "SN:" + SVS_CameraSys.CameraList[n].SN);
                }
            }
            else
            {
                Selected_comboBox.Text = "没有找到相机";
            }

            Selected_comboBox.Focus();
        }

        public void GetCameraInfo()
        {
            Selected_comboBox.Text = "选择相机";
            for (int n = 0; n < SVS_CameraSys.CameraCount; n++)
            {
                Selected_comboBox.Items.Add(SVS_CameraSys.CameraList[n].modelname + "SN:" + SVS_CameraSys.CameraList[n].SN);
            }

        }
        //打开指定相机
        private void Selected_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = Selected_comboBox.SelectedIndex;
            RemoveCamera();
            svscamera = SVS_CameraSys.CameraList[n];
            ConnectCamera();

            if (DispPanel != null)
                if (DispPanel.Width / DispPanel.Height > svscamera.camWidth / svscamera.camHeight)
                    DispPanel.Width = DispPanel.Height * svscamera.camWidth / svscamera.camHeight;
                else
                    DispPanel.Height = DispPanel.Width * svscamera.camHeight / svscamera.camWidth;

            InitBuffer(svscamera.camWidth, svscamera.camHeight);

            //Label显示分辨率
            Cam_Width.Text = svscamera.camWidth.ToString();
            Cam_Height.Text = svscamera.camHeight.ToString();

            //滚动条初始化
            Frame_value.Text = svscamera.framerate.ToString();
            hScrollBarFrame.Maximum = (int)svscamera.maxFrames * 10;
            hScrollBarFrame.Minimum = (int)svscamera.minFrames * 10;
            hScrollBarFrame.SmallChange = (hScrollBarFrame.Maximum - hScrollBarFrame.Minimum) / 10;
            hScrollBarFrame.LargeChange = hScrollBarFrame.SmallChange * 5;
            hScrollBarFrame.Value = (int)svscamera.framerate * 10;

            labelExplorValue.Text = ((svscamera.ExpTime) / 1000).ToString();
            hScrollBarExp.Maximum = /*(int)svscamera.ExpTimeMax*/10000 * 100;
            hScrollBarExp.Minimum = (int)svscamera.ExpTimeMin * 100;
            hScrollBarExp.SmallChange = 1000;
            hScrollBarExp.LargeChange = 1000 * 20;
            hScrollBarExp.Value = (int)svscamera.ExpTime * 100;

            Gain_value.Text = svscamera.GainValue.ToString();
            hScrollBarGain.Maximum = (int)svscamera.GainValueMax * 10;
            hScrollBarGain.Minimum = 0;
            hScrollBarGain.SmallChange = (hScrollBarGain.Maximum - hScrollBarGain.Minimum) / 100;
            hScrollBarGain.LargeChange = hScrollBarGain.SmallChange * 5;
            hScrollBarGain.Value = (int)svscamera.GainValue * 10;
        }



        //取图模式
        private void AcquisitionMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (svscamera != null)
            {
                int index = AcquisitionMode_comboBox.SelectedIndex;
                if (index == 1)
                {
                    SoftTrigger_button.Enabled = true;

                }
                else
                {
                    SoftTrigger_button.Enabled = false;
                }
                svscamera.SetSVSCameraMode(index);
            }
        }
        //软触发
        private void SoftTrigger_button_Click(object sender, EventArgs e)
        {
            if (svscamera != null) svscamera.SvsSnapImage();
        }
        //开始取图
        private void Cam_start_Click(object sender, EventArgs e)
        {
            if (svscamera != null) svscamera.StartLive();
        }
        //停止取图
        private void Cam_stop_Click(object sender, EventArgs e)
        {
            if (svscamera != null) svscamera.StopLive();
        }
        //保存图片对话框
        private void SaveImage_Click(object sender, EventArgs e)
        {
            if (svscamera != null)
            {
                svscamera.StopLive();
                this.saveFileDialog1.ShowDialog();
                svscamera.StartLive();

            }
        }
        //保存图片
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string SaveFilePath;
            SaveFilePath = this.saveFileDialog1.FileName;
            if (svscamera.bayerPttern)
            {
                bimageRGB[0].Save(SaveFilePath, ImageFormat.Bmp);
            }
            else
            {
                bimage[0].Save(SaveFilePath, ImageFormat.Bmp);
            }
        }

        //帧率滚动条
        private void hScrollBarFrame_ValueChanged(object sender, EventArgs e)
        {
            if (svscamera != null)
            {
                svscamera.framerate = ((float)hScrollBarFrame.Value / 10);
                svscamera.SetSvsCameraFps(svscamera.framerate);
                Frame_value.Text = svscamera.framerate.ToString();
            }
        }
        //曝光滚动条
        private void hScrollBarExp_ValueChanged(object sender, EventArgs e)
        {
            if (svscamera != null)
            {
                svscamera.ExpTime = ((float)hScrollBarExp.Value / 100);
                svscamera.SetSvsCameraExp(svscamera.ExpTime);
                labelExplorValue.Text = (svscamera.ExpTime / 1000).ToString();
            }
        }
        //增益滚动条
        private void hScrollBarGain_ValueChanged(object sender, EventArgs e)
        {
            if (svscamera != null)
            {
                svscamera.GainValue = ((float)hScrollBarGain.Value / 10);
                svscamera.SetSvsCameraGain(svscamera.GainValue);
                Gain_value.Text = (svscamera.GainValue).ToString();
            }
        }
    }
}