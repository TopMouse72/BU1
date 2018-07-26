using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SVS_Encap_Class;//引用封装类名称空间
using System.Drawing.Imaging;//画图
using System.Threading;//线程





namespace SVS封装
{
    public partial class CameraParam : Form
    {
        SVS_Camera svscamera;//封装类对象

        //图像buffer结构体
        private struct imagebufferStruct
        {
            public byte[] imagebytes;
        };

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

        
        
        public CameraParam()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notFirstImage = false;
            threadStop = true;
            svscamera = new SVS_Camera(MyDelegateFunc);
            imagproc = new Thread(new ThreadStart(this.imageProcessing));
            imagproc.IsBackground = true;
        }

        //画图
        private void imageProcessing()
        {
            gpanel.Clear(panel1.BackColor);
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
                                
//                                gpanel.DrawImage(bimageRGB[0], outRectangle);                               
                            }
                        }
                        else if (IsColor == 0)
                        {
                            lock (bimage[0])
                            {
//                                gpanel.DrawImage(bimage[0], outRectangle);
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
        public void InitBuffer(int CamWidth,int CamHeight)
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

            outRectangle = new Rectangle(0, 0, panel1.Width, panel1.Height);
            gpanel = panel1.CreateGraphics();
            drawing = false;
        }

        //查找相机
        private void Selected_device_Click(object sender, EventArgs e)
        {
            Selected_comboBox.Items.Clear();

            Selected_comboBox.Text = "查找中...";
            Selected_comboBox.Refresh();


            int number = svscamera.getCameras();//获得相机个数
            if (number > 0)
            {
                Selected_comboBox.Text = "选择相机";
                for (int n = 0; n < number;n++ )
                {
                    svscamera.FindAllCam(n);
                    Selected_comboBox.Items.Add(svscamera.modelname + "SN:" + svscamera.SN);
                }
            }
            else
            {
                Selected_comboBox.Text = "没有找到相机";
            }

            Selected_comboBox.Focus();
        }

        //打开指定相机
        private void Selected_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = Selected_comboBox.SelectedIndex;

            svscamera.SvsCameraDispose();//关闭相机
            svscamera.CameraIndex = n;//通过索引打开相机
            svscamera.InitSvsCamer(1);//相机初始化 1表示Filter模式，2表示WinSock
            svscamera.SetSVSCameraMode(0);//相机模式          
            InitBuffer(svscamera.camWidth, svscamera.camHeight);

            //Label显示分辨率
            Cam_Width.Text = svscamera.camWidth.ToString();
            Cam_Height.Text = svscamera.camHeight.ToString();

            svscamera.GetSvsCamFps();//帧率初始化
            svscamera.GetExpTime();//曝光时间初始化
            svscamera.GetGainValue();//增益初始化

            //滚动条初始化
            Frame_value.Text = svscamera.framerate.ToString();
            Frame_trackBar.Maximum = (int)svscamera.maxFrames * 10;
            Frame_trackBar.Minimum = (int)svscamera.minFrames * 10;
            Frame_trackBar.TickFrequency = (Frame_trackBar.Maximum - Frame_trackBar.Minimum) / 100;

            Exp_textBox.Text = ((svscamera.ExpTime) / 1000000).ToString();
            Exp_trackBar.Maximum = (int)svscamera.ExpTimeMax * 100;
            Exp_trackBar.Minimum = (int)svscamera.ExpTimeMin * 100;
            Exp_trackBar.TickFrequency = (Exp_trackBar.Maximum - Exp_trackBar.Minimum) / 1000000000;

            Gain_value.Text = svscamera.GainValue.ToString();
            Gain_trackBar.Maximum = (int)svscamera.GainValueMax * 10;
            Gain_trackBar.Minimum = 0;
            Gain_trackBar.TickFrequency = (Gain_trackBar.Maximum - Gain_trackBar.Minimum) / 1000000;




        }

        

        //取图模式
        private void AcquisitionMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
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
        //软触发
        private void SoftTrigger_button_Click(object sender, EventArgs e)
        {
            svscamera.SvsSnapImage();
        }
        //开始取图
        private void Cam_start_Click(object sender, EventArgs e)
        {
            svscamera.StartLive();
        }
        //停止取图
        private void Cam_stop_Click(object sender, EventArgs e)
        {
            svscamera.StopLive();
        }
        //保存图片对话框
        private void SaveImage_Click(object sender, EventArgs e)
        {
            svscamera.StopLive();
            this.saveFileDialog1.ShowDialog();
            svscamera.StartLive();

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
        private void Frame_trackBar_Scroll(object sender, EventArgs e)
        {
            svscamera.framerate = ((float)Frame_trackBar.Value / 10);
            svscamera.SetSvsCameraFps(svscamera.framerate);
            Frame_value.Text = svscamera.framerate.ToString();
        }
        //曝光滚动条
        private void Exp_trackBar_Scroll(object sender, EventArgs e)
        {
            svscamera.ExpTime = ((float)Exp_trackBar.Value / 100);
            svscamera.SetSvsCameraExp(svscamera.ExpTime);
            Exp_textBox.Text = (svscamera.ExpTime / 1000000).ToString();
        }
        //增益滚动条
        private void Gain_trackBar_Scroll(object sender, EventArgs e)
        {
            svscamera.GainValue = ((float)Gain_trackBar.Value / 10);
            svscamera.SetSvsCameraGain(svscamera.GainValue);
            Gain_value.Text = (svscamera.GainValue).ToString();

        }
        //自定义曝光值
        private void Exp_textBox_TextChanged(object sender, EventArgs e)
        {
            svscamera.SetSvsCameraExp(float.Parse(Exp_textBox.Text) * 1000000);
        }


        //退出时关闭相机
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            svscamera.SvsCameraDispose();
            threadStop = true;
            imagproc.Interrupt();
        }

        

    }
}
