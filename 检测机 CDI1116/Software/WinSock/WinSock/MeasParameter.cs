using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Measure
{
    public class MeasParameter
    {
        public const int MAXCAM = 11;
        public const int MAXROI = 50;
        public const int CAMERACOUNT = 1;
        public const int ROICOUNT = 30;
        public const int ZOOM = 25;//Percent =12、25、50、100、200、400，（表示原图比例值25%、50%、100%、200%、400%）

        public const int BLOBSINGLEPOINT = 1;
        public const int BLOBMULTIPLEPOINT = 2;
        public const int PAT = 3;
        public const int EDGE = 4;
        public const int EDGEMULTIPLEPOINT = 5;
        public const int STEPMARK = 6;
        public const int STEPMARKMULTIPLEPOINT = 7;
        public const int RECTRGB = 8;
        public const int CORNER = 9;
        public const int CIRCLE = 10;
        public const int ERRBLOB = 11;

        public const int FIND_DIR_X = 1;
        public const int FIND_DIR_Y = 2;
        public const int FIND_DIR_X_INCREASE = 3;
        public const int FIND_DIR_X_DECREASE = 4;
        public const int FIND_DIR_Y_INCREASE = 5;
        public const int FIND_DIR_Y_DECREASE = 6;

        public const int ViewLWidth100 = 2;//LWidth = 1,2,3,5,7,8 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8
        public const int ViewLWidth50 = 3;//LWidth = 1,2,3,5,7,8 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8
        public const int ViewLWidth25 = 5;//LWidth = 1,2,3,5,7,8 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8
        public static Rectangle DefaultRect = new Rectangle(10, 10, 100, 100);

        //public const int WM_STR_RECEIVE_FLAG = WM_USER + 1;

        public struct dPoint//点结构 
        {
            public double X;
            public double Y;
            public dPoint(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        public class Sys_Param //系统参数
        {
            public string sUsedProName = "";//用户最后使用的产品名
            public string sSavePath = "";//保存路径

            public string[] CameraSN = new string[CAMERACOUNT];
            public long[] CameraMode = new long[CAMERACOUNT];
            public double[] CameraShutter = new double[CAMERACOUNT];
            public double[] CameraGain = new double[CAMERACOUNT];
            public long[] CameraImageWidth = new long[CAMERACOUNT];
            public long[] CameraImageHighth = new long[CAMERACOUNT];

            public int iPort;//TCP/IP

            public int iShowAlarm;//0:Not Show,1:Show
            public int iSaveBMP;//0=不保存，1=保存


            public Sys_Param()
            {
                sUsedProName = "";
                iPort = 6230;

                iShowAlarm = 0;
                iSaveBMP = 0;
            }
        };

        public class Sys_State //系统状态
        {

            public bool bSysExit;
            public string sProPath;//产品路径
            public bool bCameraActive;
            public bool iTCPTrigFlag;

            public int Obj;
            public int Roi;
            public int Type;

            public bool bDrawCross;
            public bool bDrawZoom;

            public int LogCamera;
            public string LogROI;
            public string LogResultState;

            public double BackValueX;
            public double BackValueY;
            public double LogValue1;
            public double LogValue2;
            public double LogValue3;
            public double LogValue4;
            public double LogValue5;
            public double LogValue6;
            public DateTime time;
            public string barcode; //条码信息
            public string ImagePath;

            public Sys_State()
            {
                bSysExit = false;

                sProPath = "";
                bCameraActive = false;
                iTCPTrigFlag = false;


                Obj = 1;
                Roi = 1;
                Type = 0;

                bDrawCross = false;
                bDrawZoom = false;

                LogCamera = 1;
                LogROI = "1";
                LogResultState = "OK";
                BackValueX = 0.0;
                BackValueY = 0.0;
                LogValue1 = 0.0;
                LogValue2 = 0.0;
                LogValue3 = 0.0;
                LogValue4 = 0.0;
                LogValue5 = 0.0;
                LogValue6 = 0.0;

            }

        }

        public class Cam_Param //相机参数
        {
            public Size CameraSize;
            public double CameraKX;
            public double CameraKY;
            public int CurrentZoom = 100;
            public int LineTolerance = 10;
        }

        public struct Struct_BlobSinglePoint//1 Blob
        {

            public Rectangle Rect;           //查找区域
            public bool WhiteBlob;     // 是否白斑
            public int Thres;          // 域值
            public long MinArea;       // 最小Blob面积
            public long MaxArea;       // 最大Blob面积

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点

        }

        public struct Struct_Pat//（保存为配置文件）
        {
            public Rectangle PatSeachRect;     //查找区域
            public Rectangle PatTempRect;      //模板区域
            public double PatAcceptValue;  // 接受值
            public bool PatFindAngleFlag;  //是否查找角度

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点	
        }

        public struct Struct_Edge // 边界
        {
            public Rectangle Rect;           //查找区域
            public int Thres;          // 域值	
            public int Dir;            // 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,
            public bool MultiplePoint;     // 是否多点查找，
            public bool PrintMultiplePoint;        // 是否打印多个点

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点
            public double IgnoreLength;
        }
        public struct Struct_Line
        {
            public double k;
            public double b;
            public double RAngle;
            public double DAngle;
            public double AvgX;
            public double AvgY;
        }

        public struct Struct_EdgeMultiplePoint // 边界多点
        {
            public Rectangle Rect;                   //查找区域
            public int Thres;                  // 域值	
            public int Dir;                    // 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,
            public bool PrintMultiplePoint;        // 是否打印多个点
            public double IgnoreLength;
            public Struct_Line Line;
            public dPoint FirstEndPoint;
            public dPoint SecondEndPoint;
        }

        public struct Struct_StepMark //边缘
        {
            public Rectangle Rect;           //查找区域
            public int Dir;            // 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,
            public bool MultiplePoint;     // 是否多点查找，
            public bool PrintMultiplePoint;        // 是否打印多个点

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点

        }

        public struct Struct_StepMarkMultiplePoint //边缘
        {
            public Rectangle Rect;           //查找区域
            public int Dir;            // 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,
            public bool PrintMultiplePoint;        // 是否打印多个点
        }

        public struct Struct_RectRGB //GRB
        {
            public Rectangle Rect;           //查找区域

            public int R;               //返回结果
            public int G;
            public int B;
        }

        public struct Struct_CorNer //角点
        {
            public Rectangle RectX;           //查找X区域
            public Rectangle RectY;           //查找X区域
            public int DirX;           // 搜索方向 DirX=1:X, DirX=3:X-Rising, DirX=4:X-Falling
            public int DirY;           // 搜索方向 DirY=2:Y, DirY=5:Y-Rising, DirY=6:Y-Falling
            public int ThresX;         // 域值
            public int ThresY;         // 域值
            public bool UseThres;       //使用二值化功能

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点

        }

        public struct Struct_Circle //圆弧
        {
            public Rectangle Rect;           //查找X区域
            public int Thres;          // 域值	
            public int ArcStart;       //初始角范围:0~360
            public int ArcEnd;         //结束角范围:ArcStart~360
            public int Dir;            // 搜索方向 DirX=0:由圆心向外查找，DirX=1:由外向圆心查找
            public bool PrintArcPoint; // 是否打印弧点

            public double ModelX;//标定为参考模板点
            public double ModelY;//标定为参考模板点

        }

        public struct Struct_ErrBlob //异斑
        {
            public Rectangle Rect;           //查找X区域
            public int Thres;          // 域值	
            public int ErrType;        //查找类型0白斑，1黑斑
            public int Dir;          //查找方向1:X，2:Y, 3:XY
            public int MinArea;        //最小面积
            public bool PrintErr;      // 是否打印

        }
        public struct Struct_Image//
        {
            public string Name;
            public bool bIsUsed;//是否已经被启用了	
            public int type;   // 0:Blob,1:Pat,2:Edge,3:color... 
            public Struct_BlobSinglePoint BlobSinglePoint;// Blob参数
            public Struct_Pat Pat;
            public Struct_Edge Edge;
            public Struct_EdgeMultiplePoint EdgeMultiplePoint;
            public Struct_StepMark StepMark;
            public Struct_StepMarkMultiplePoint StepMarkMultiplePoint;
            public Struct_RectRGB RectRGB;
            public Struct_CorNer CorNer;
            public Struct_Circle Circle;
            public Struct_ErrBlob ErrBlob;

        }

        public struct Struct_VParam
        {
            public Struct_Image[] Im;
            public bool UseBottomAsBase;
        }
        public struct Struct_DataInfo
        {
            public double Value
            {
                get { return Original + Offset; }
                set { Original = value - Offset; }
            }
            public bool DataNG
            {
                get { return Value < LLimit || Value > ULimit; }
            }
            public double Mean, Tolerance;
            public double ULimit { get { return Mean + Tolerance; } }
            public double LLimit { get { return Mean - Tolerance; } }
            public double Original;
            public double Slope;
            public double Intercept;
            public double Offset;
            public double StandardData;
            public double UpdateOffset()
            {
                Offset = StandardData - Original;
                return Offset;
            }
            public void Init()
            {
                Slope = 1;
                Intercept = 0;
                Offset = 0;
                Mean = 0;
                Tolerance = 0;
                Original = 0;
            }
            public void Clear()
            {
                Value = Mean;
                //Value = 0;
            }
        }
        public enum DataName
        {
            CellThickness,
            CellWidth,
            CellLength,
            NiTabDistance,
            AlTabDistance,
            NiTabLength,
            AlTabLength,
            NiSealantHeight,
            AlSealantHeight,
            TabDistance,
        }
        public struct Struct_CCDDatas
        {
            public Struct_DataInfo CellThickness;
            public Struct_DataInfo CellWidth;
            public Struct_DataInfo CellLength;
            public Struct_DataInfo NiTabDistance;
            public Struct_DataInfo AlTabDistance;
            public Struct_DataInfo NiTabLength;
            public Struct_DataInfo AlTabLength;
            public Struct_DataInfo NiSealantHeight;
            public Struct_DataInfo AlSealantHeight;
            public Struct_DataInfo TabDistance;
            public double MeasureLaps;
            public bool DataNG
            {
                get
                {
                    bool res = false;
                    res |= CellThickness.DataNG;
                    res |= CellWidth.DataNG;
                    res |= CellLength.DataNG;
                    res |= AlTabDistance.DataNG;
                    res |= NiTabDistance.DataNG;
                    res |= AlTabLength.DataNG;
                    res |= NiTabLength.DataNG;
                    res |= TabDistance.DataNG;
                    res |= AlSealantHeight.DataNG;
                    res |= NiSealantHeight.DataNG;
                    return res;
                }
            }
            public void Init()
            {
                CellThickness.Init();
                CellWidth.Init();
                CellLength.Init();
                NiTabDistance.Init();
                AlTabDistance.Init();
                NiTabLength.Init();
                AlTabLength.Init();
                NiSealantHeight.Init();
                AlSealantHeight.Init();
                TabDistance.Init();
            }
            public void Clear()
            {
                CellThickness.Clear();
                CellWidth.Clear();
                CellLength.Clear();
                NiTabDistance.Clear();
                AlTabDistance.Clear();
                NiTabLength.Clear();
                AlTabLength.Clear();
                NiSealantHeight.Clear();
                AlSealantHeight.Clear();
                TabDistance.Clear();
            }
        }
        public static Struct_CCDDatas CellDataSpec = new Struct_CCDDatas();
    }
}