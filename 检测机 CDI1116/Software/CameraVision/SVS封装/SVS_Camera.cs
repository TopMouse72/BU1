using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colibri.CommonModule.Event;
using GigEApi;//使用相机自带的SDK名称空间
using System.Timers;

namespace SVSCamera
{
    public static class SVS_CameraSys
    {
        public static int hCameraContainer;//相机个数
        public static GigeApi myApi = new GigeApi();//SDK类对象
        private static GigeApi.SVSGigeApiReturn errorflag;//SDK调用函数时的返回值
        public static SVS_Camera[] CameraList;
        private static int camAnz;//相机个数返回值
        public static int CameraCount
        {
            get { return camAnz; }
        }

        //获得相机个数
        public static int getCameras()
        {
            camAnz = 0;
            if (hCameraContainer != GigeApi.SVGigE_NO_CLIENT)
            {
                myApi.Gige_CameraContainer_delete(hCameraContainer);
            }
            hCameraContainer = myApi.Gige_CameraContainer_create(GigeApi.SVGigETL_Type.SVGigETL_TypeFilter);

            if (hCameraContainer >= 0)
            {
                errorflag = myApi.Gige_CameraContainer_discovery(hCameraContainer);
                if (errorflag == GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {


                    camAnz = myApi.Gige_CameraContainer_getNumberOfCameras(hCameraContainer);


                }

            }

            if (camAnz > 0)
            {
                CameraList = new SVS_Camera[camAnz];
                for (int i = 0; i < camAnz; i++)
                {
                    CameraList[i] = new SVS_Camera();
                    CameraList[i].CameraIndex = i;
                    CameraList[i].InitSvsCamer(1);
                    CameraList[i].SetSVSCameraMode(0);
                    CameraList[i].hCamera = SVS_CameraSys.myApi.Gige_CameraContainer_getCamera(SVS_CameraSys.hCameraContainer, i);
                    CameraList[i].modelname = SVS_CameraSys.myApi.Gige_Camera_getModelName(CameraList[i].hCamera);
                    CameraList[i].SN = SVS_CameraSys.myApi.Gige_Camera_getSerialNumber(CameraList[i].hCamera);

                    CameraList[i].GetSvsCamFps();//帧率初始化
                    CameraList[i].GetExpTime();//曝光时间初始化
                    CameraList[i].GetGainValue();//增益初始化
                    CameraList[i].StopLive();
                }
            }
            return camAnz;
        }

        public static void ReleaseAllCamera()
        {
            if (CameraList != null)
                for (int i = 0; i < CameraList.Length; i++)
                    if (CameraList[i] != null) CameraList[i].Release();
        }
    }
    public class SVS_Camera : VisionPublisher
    {
        float timeout = 15.0f;//定义连接超时变量
        public IntPtr hCamera;//指定相机的变量
        public IntPtr hStreamingChannel;//数据通道
        public string modelname;//相机名称
        public int PacketSize;//数据包大小
        public int bitCount;//图像位数
        public bool bayerPttern;//是否彩色格式
        public int bufferCount = 1;//buffer个数
        public int camWidth, camHeight;//分辨率

        public int CameraIndex;//打开指定相机

        public GigeApi.SVSGigeApiReturn errorflag;//SDK调用函数时的返回值
        public bool flag = false;//判断相机个数标志位
        public string SN = "";//相机SN号

        GigeApi.SVGIGE_PIXEL_DEPTH PixelDepth;//图像位深度
        GigeApi.GVSP_PIXEL_TYPE PixelType;//图像颜色类型

        public float framerate = 1.0f;//帧率
        public float maxFrames = 40.0f;//最大帧率
        public float minFrames = 1.0f;//最小帆率

        public float ExpTime = 0.0f;//曝光时间
        public float ExpTimeMax = 1.0f;//最大曝光时间
        public float ExpTimeMin = 0.0f;//最小曝光时间

        public float GainValue = 0.0f;//增益值
        public float GainValueMax = 1.0f;//最大增益

        public struct imagebufferStruct //图像buffer结构体
        {
            public byte[] imagebytes;
        }

        public imagebufferStruct[] imagebuffer;//黑白图像buffer
        public imagebufferStruct[] imagebufferRGB;//彩色图像buffer

        public GigeApi.BAYER_METHOD bayerMethod = GigeApi.BAYER_METHOD.BAYER_METHOD_SIMPLE;
        public GigeApi.StreamCallback GigeCallBack;//回调
        public delegate void MyDelegate(byte[] ImageBuffer, int Xsize, int Ysize, int color);
        private MyDelegate Calldelegate;
        //构造函数
        public SVS_Camera()
        {
        }
        public void AddCallBack(MyDelegate myCallBack)
        {
            Calldelegate -= myCallBack;
            Calldelegate += myCallBack;
        }
        public void RemoveCallBack(MyDelegate myCallBack)
        {
            Calldelegate -= myCallBack;
        }
        //自定义数据回调
        public GigeApi.SVSGigeApiReturn myStreamCallback(int Image, IntPtr Contex)
        {
            GigeApi.SVSGigeApiReturn apiReturn;
            int xSize, ySize, size;
            IntPtr imgPtr;
            imgPtr = SVS_CameraSys.myApi.Gige_Image_getDataPointer(Image);
            xSize = SVS_CameraSys.myApi.Gige_Image_getSizeX(Image);
            ySize = SVS_CameraSys.myApi.Gige_Image_getSizeY(Image);
            size = xSize * ySize;

            if ((int)(imgPtr) == 0)
            {
                return (GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS);
            }

            if (bitCount == 8)
            {
                unsafe
                {
                    if (!bayerPttern)
                    {
                        System.Runtime.InteropServices.Marshal.Copy(imgPtr, imagebuffer[0].imagebytes, 0, size);
                        notifyImageCaptureEventSubscribers("SVSCamera", imagebuffer[0].imagebytes);
                        if (Calldelegate != null) Calldelegate(imagebuffer[0].imagebytes, xSize, ySize, 0);
                    }
                    else
                    {
                        size = xSize * ySize * 3;
                        fixed (byte* dest = imagebufferRGB[0].imagebytes)
                        {
                            apiReturn = SVS_CameraSys.myApi.Gige_Image_getImageRGB(Image, dest, size, bayerMethod);
                        }

                        if (Calldelegate != null) Calldelegate(imagebufferRGB[0].imagebytes, xSize, ySize, 1);
                    }

                }


            }

            return (GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS);
        }
        //相机初始化
        public void InitSvsCamer(int FilterMode)
        {
            //hCamera = new IntPtr(-1);
            //hCameraContainer = -1;
            //PacketSize = 0;

            //switch (FilterMode)
            //{
            //    case 1:  //Filter 模式
            //        if (hCameraContainer != GigeApi.SVGigE_NO_CLIENT)
            //        {
            //            myApi.Gige_CameraContainer_delete(hCameraContainer);
            //        }
            //        hCameraContainer = myApi.Gige_CameraContainer_create(GigeApi.SVGigETL_Type.SVGigETL_TypeFilter);
            //        break;
            //    case 2: //WinSock模式
            //       if (hCameraContainer != GigeApi.SVGigE_NO_CLIENT)
            //        {
            //            myApi.Gige_CameraContainer_delete(hCameraContainer);

            //        }
            //        hCameraContainer = myApi.Gige_CameraContainer_create(GigeApi.SVGigETL_Type.SVGigETL_TypeWinSock);
            //        break; 
            //}


            hCamera = new IntPtr(GigeApi.SVGigE_NO_CAMERA);
            SVS_CameraSys.myApi.Gige_CameraContainer_discovery(SVS_CameraSys.hCameraContainer);//查找相机
            hCamera = SVS_CameraSys.myApi.Gige_CameraContainer_getCamera(SVS_CameraSys.hCameraContainer, CameraIndex);//获得指定相机


            if (hCamera.ToInt32() != 0)
            {
                modelname = SVS_CameraSys.myApi.Gige_Camera_getModelName(hCamera);//获得相机名
                GigeApi.SVSGigeApiReturn ret = SVS_CameraSys.myApi.Gige_Camera_openConnection(hCamera, timeout);
                if (ret != GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                    System.Windows.Forms.MessageBox.Show("相机连接失败: " + ret);//连接相机
                GigeCallBack = new GigeApi.StreamCallback(myStreamCallback);
                SVS_CameraSys.myApi.Gige_Camera_evaluateMaximalPacketSize(hCamera, ref (PacketSize));//获取数据包
                SVS_CameraSys.myApi.Gige_Camera_setBinningMode(hCamera, GigeApi.BINNING_MODE.BINNING_MODE_OFF);//关闭Binning
                SVS_CameraSys.myApi.Gige_StreamingChannel_create(ref (hStreamingChannel), SVS_CameraSys.hCameraContainer, hCamera, 3, GigeCallBack, new IntPtr(0));//创建数据通道

                SVS_CameraSys.myApi.Gige_Camera_getSizeX(hCamera, ref (camWidth));//获得图像宽
                SVS_CameraSys.myApi.Gige_Camera_getSizeY(hCamera, ref (camHeight));//获得图像高



                GigeApi.SVSGigeApiReturn apiReturn;
                PixelDepth = GigeApi.SVGIGE_PIXEL_DEPTH.SVGIGE_PIXEL_DEPTH_8;
                apiReturn = SVS_CameraSys.myApi.Gige_Camera_getPixelDepth(hCamera, ref (PixelDepth));//获得位深度
                if (apiReturn == GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    switch (PixelDepth)
                    {
                        case (GigeApi.SVGIGE_PIXEL_DEPTH.SVGIGE_PIXEL_DEPTH_8): { bitCount = 8; break; }
                        case (GigeApi.SVGIGE_PIXEL_DEPTH.SVGIGE_PIXEL_DEPTH_12): { bitCount = 12; break; }
                        case (GigeApi.SVGIGE_PIXEL_DEPTH.SVGIGE_PIXEL_DEPTH_16): { bitCount = 16; break; }
                        default: { bitCount = 8; break; }

                    }
                }

                bayerPttern = false;
                PixelType = GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_UNKNOWN;
                apiReturn = SVS_CameraSys.myApi.Gige_Camera_getPixelType(hCamera, ref (PixelType));//获得颜色格式
                if (apiReturn == GigeApi.SVSGigeApiReturn.SVGigE_SUCCESS)
                {
                    switch (PixelType)
                    {
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_MONO8: { bayerPttern = false; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_MONO12: { bayerPttern = false; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_MONO16: { bayerPttern = false; break; }

                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_RGB24: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYGR8: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYRG8: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYGB8: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYBG8: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYGR12: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYRG12: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYGB12: { bayerPttern = true; break; }
                        case GigeApi.GVSP_PIXEL_TYPE.GVSP_PIX_BAYBG12: { bayerPttern = true; break; }
                        default: { bayerPttern = false; break; }

                    }
                }

            }

            imagebuffer = new imagebufferStruct[bufferCount];
            imagebuffer[0].imagebytes = new byte[camHeight * camWidth];
            imagebufferRGB = new imagebufferStruct[bufferCount];
            imagebufferRGB[0].imagebytes = new byte[3 * camWidth * camHeight];

        }



        //获得曝光值
        public void GetExpTime()
        {
            SVS_CameraSys.myApi.Gige_Camera_getExposureTime(hCamera, ref (ExpTime));
            SVS_CameraSys.myApi.Gige_Camera_getExposureTimeMax(hCamera, ref (ExpTimeMax));
            SVS_CameraSys.myApi.Gige_Camera_getExposureTimeMin(hCamera, ref (ExpTimeMin));

        }

        //设置曝光值
        public void SetSvsCameraExp(float ExposureValue)
        {
            SVS_CameraSys.myApi.Gige_Camera_setExposureTime(hCamera, ExposureValue);
        }

        //获得帧率值
        public void GetSvsCamFps()
        {
            SVS_CameraSys.myApi.Gige_Camera_getFrameRate(hCamera, ref (framerate));
            SVS_CameraSys.myApi.Gige_Camera_getFrameRateMax(hCamera, ref (maxFrames));
            SVS_CameraSys.myApi.Gige_Camera_getFrameRateMin(hCamera, ref (minFrames));
        }

        //设置帧率值
        public void SetSvsCameraFps(float FpsValue)
        {
            SVS_CameraSys.myApi.Gige_Camera_setFrameRate(hCamera, FpsValue);
        }

        //获得增益值
        public void GetGainValue()
        {
            SVS_CameraSys.myApi.Gige_Camera_getGain(hCamera, ref (GainValue));
            SVS_CameraSys.myApi.Gige_Camera_getGainMax(hCamera, ref (GainValueMax));
        }

        //设置增益值
        public void SetSvsCameraGain(float GValue)
        {
            SVS_CameraSys.myApi.Gige_Camera_setGain(hCamera, GValue);
        }


        //取图模式
        public void SetSVSCameraMode(int ModeValue)
        {
            switch (ModeValue)
            {
                case 0:
                    {
                        SVS_CameraSys.myApi.Gige_Camera_setAcquisitionMode(hCamera, GigeApi.ACQUISITION_MODE.ACQUISITION_MODE_FIXED_FREQUENCY);//时实模式
                        break;
                    }
                case 1:
                    {
                        SVS_CameraSys.myApi.Gige_Camera_setAcquisitionMode(hCamera, GigeApi.ACQUISITION_MODE.ACQUISITION_MODE_SOFTWARE_TRIGGER);//软触发模式
                        break;
                    }
                case 2:
                    {
                        SVS_CameraSys.myApi.Gige_Camera_setAcquisitionMode(hCamera, GigeApi.ACQUISITION_MODE.ACQUISITION_MODE_EXT_TRIGGER_INT_EXPOSURE);//外触发模式
                        break;
                    }
                default:
                    break;
            }
        }

        //触发抓图
        public void SvsSnapImage()
        {
            if (hCamera.ToInt32() != 0)
            {
                SVS_CameraSys.myApi.Gige_Camera_startAcquisitionCycle(hCamera);
            }
        }

        //开始取图
        public void StartLive()
        {
            SVS_CameraSys.myApi.Gige_Camera_setAcquisitionControl(hCamera, GigeApi.ACQUISITION_CONTROL.ACQUISITION_CONTROL_START);
        }

        //停止取图
        public void StopLive()
        {
            SVS_CameraSys.myApi.Gige_Camera_setAcquisitionControl(hCamera, GigeApi.ACQUISITION_CONTROL.ACQUISITION_CONTROL_STOP);
        }

        //退出程序时关闭相机
        public void Release()
        {
            GigeApi.SVSGigeApiReturn apiReturn;
            if (hCamera.ToInt32() != 0)
            {
                apiReturn = SVS_CameraSys.myApi.Gige_Camera_setAcquisitionControl(hCamera, GigeApi.ACQUISITION_CONTROL.ACQUISITION_CONTROL_STOP);

                SVS_CameraSys.myApi.Gige_StreamingChannel_delete(hStreamingChannel);

                SVS_CameraSys.myApi.Gige_Camera_closeConnection(hCamera);


            }
        }

    }
}