using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;//多线程
using System.Linq;
using System.Runtime.InteropServices;
using Colibri.CommonModule.ToolBox;

namespace VisionUnit
{
    public class VSTest : VSBase
    {
        [DllImport("Vision4.dll", EntryPoint = "Vision_InitMil")]
        public static extern short Vision_InitMil(int iWidth, int iHeight, int iBand);

        [DllImport("Vision4.dll", EntryPoint = "Vision_SetHwnd")]
        public static extern void Vision_SetHwnd(IntPtr hWnd);

        [DllImport("Vision4.dll", EntryPoint = "Vision_SetHwndRelease")]
        public static extern void Vision_SetHwndRelease();

        [DllImport("Vision4.dll", EntryPoint = "Vision_Init")]
        public static extern short Vision_Init(IntPtr hWnd, int iWidth, int iHeight, int iBand);

        [DllImport("Vision4.dll", EntryPoint = "Vision_UnInit")]
        public static extern void Vision_UnInit();

        [DllImport("Vision4.dll", EntryPoint = "Vision_CKCopy")]
        public static extern void Vision_CKCopy(IntPtr pSrc, int width, int height);
        [DllImport("Vision4.dll", EntryPoint = "Vision_CKCopy")]
        public static extern void Vision_GetBuffer(byte[] src, int width, int height);

        [DllImport("Vision4.dll", EntryPoint = "Vision_CopyImageToBuffer")]
        public static extern void Vision_CopyImageToBuffer(int Dir);

        [DllImport("Vision4.dll", EntryPoint = "Vision_GetHwnd")]
        public static extern IntPtr Vision_GetHwnd(int StartX, int StartY, int Width, int Height);//获取MilBuffer区域图的指针;

        [DllImport("Vision4.dll", EntryPoint = "Vision_DrawRect")]
        public static extern void Vision_DrawRect(int StartX, int StartY, int EndX, int EndY, bool Fill, int Color); //color:RED 249, GREEN 250, BLUE 252;

        [DllImport("Vision4.dll", EntryPoint = "Vision_DrawArc")]
        public static extern void Vision_DrawArc(int x, int y, int Diah, int Diav, double ArcStart, double ArcEnd, bool Fill, int Color);////x,y画点坐标,diah,diav半径,arcstart起始角0-360,fill填充,color:RED 249, GREEN 250, BLUE 252

        [DllImport("Vision4.dll", EntryPoint = "Vision_DrawCross")]
        public static extern void Vision_DrawCross(int x, int y, int r, int Color, int LWidth);//LWidth = 1,2,3,4,5,6,7,8,9,10 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8

        [DllImport("Vision4.dll", EntryPoint = "Vision_DrawLine")]
        public static extern void Vision_DrawLine(int StartX, int StartY, int EndX, int EndY, int Color, int LWidth);//LWidth = 1,2,3,4,5,6,7,8,9,10 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8

        [DllImport("Vision4.dll", EntryPoint = "Vision_DrawText")]
        public static extern void Vision_DrawText(int x, int y, int FontSize, int Color, string str);

        [DllImport("Vision4.dll", EntryPoint = "Vision_MarkClean")]
        public static extern void Vision_MarkClean();

        [DllImport("Vision4.dll", EntryPoint = "Vision_ShowRect")]
        public static extern void Vision_ShowRect(Rectangle Rect, int Color, int LWidth);//LWidth = 1,2,3,5,7,8,9,10 画线宽度,ZoomDisp=100用2，ZoomDisp=50用3，ZoomDisp=25用5，ZoomDisp=12用8

        [DllImport("Vision4.dll", EntryPoint = "Vision_CreatRoi")]
        public static extern void Vision_CreatRoi(Rectangle Rect);////

        [DllImport("Vision4.dll", EntryPoint = "Vision_GetRoi")]
        public static extern void Vision_GetRoi(ref Rectangle Rect);////

        [DllImport("Vision4.dll", EntryPoint = "Vision_MouseMoveShowRoi")]
        public static extern void Vision_MouseMoveShowRoi(Point Pnt, ref Point OutPnt);

        [DllImport("Vision4.dll", EntryPoint = "Vision_MouseLUpShowRoi")]
        public static extern void Vision_MouseLUpShowRoi();

        [DllImport("Vision4.dll", EntryPoint = "Vision_MouseLDnShowRoi")]
        public static extern void Vision_MouseLDnShowRoi(Point Pnt);

        [DllImport("Vision4.dll", EntryPoint = "Vision_SetDrawRect")]
        public static extern void Vision_SetDrawRect(bool state);

        [DllImport("Vision4.dll", EntryPoint = "Vision_OpenBmp")]
        public static extern void Vision_OpenBmp(string Path);

        [DllImport("Vision4.dll", EntryPoint = "Vision_SaveBmp")]
        public static extern void Vision_SaveBmp(string Path);

        [DllImport("Vision4.dll", EntryPoint = "Vision_ZoomDisp")]
        public static extern void Vision_ZoomDisp(int Percent);//Percent =4、5、6、7、8、9、10、11、12、15、18、22、25、50、100、200、400，（表示原图比例值如25=25%、50=50%、100=100%、200=200%）

        [DllImport("Vision4.dll", EntryPoint = "Vision_Binary")]
        public static extern void Vision_Binary(int Value);


        //BLOB
        [DllImport("Vision4.dll", EntryPoint = "Vision_FindBlobSinglePoint")]
        public static extern short Vision_FindBlobSinglePoint(bool WhiteBlob, int Thres, int MinArea, int MaxArea, Rectangle Rect, ref double cx, ref double cy, ref double Area);//返回值大于0正确//WhiteBlob白斑 Thres域值 MinArea最小值设定 Rect查找区域   cx, cy, Area为计算后所得斑点坐标及面积//10ms

        [DllImport("Vision4.dll", EntryPoint = "Vision_FindBlobMultiplePoint")]
        public static extern int Vision_FindBlobMultiplePoint(bool WhiteBlob, int Thres, int MinArea, int MaxArea, Rectangle Rect); //返回值为斑点数量

        [DllImport("Vision4.dll", EntryPoint = "Vision_GetBlobMultiplePointResult")]
        public static extern void Vision_GetBlobMultiplePointResult(int Number, ref double cx, ref double cy, ref double Area);//按Number取BLOB值,Number从0开始至Number-1,值Area从大到小排


        //PAT
        [DllImport("Vision4.dll", EntryPoint = "Vision_SavePatTemp")]
        public static extern void Vision_SavePatTemp(string Path, string TempName);//Path路径 TempName名称如“SS”

        [DllImport("Vision4.dll", EntryPoint = "Vision_LoadPatTemp")]
        public static extern short Vision_LoadPatTemp(string Path, string TempName, int TempNO);//返回值等于0正确////Path路径 TempName名称 TempNO模板号：0 ~ 99

        [DllImport("Vision4.dll", EntryPoint = "Vision_FindPat")]
        public static extern short Vision_FindPat(int TempNO, Rectangle Rect, double AcceptValue, bool FindAngleFlag, ref double cx, ref double cy, ref double Angle, ref double Score);//返回值大于0正确//TempNO模板号：0 ~ 99 Rect查找区域 AcceptValue接受值：10 ~ 100 FindAngleFlag是否查找角度//没角度5~20ms,带角度100~120ms

        //StepMark 
        [DllImport("Vision4.dll", EntryPoint = "Vision_FindStepMark")]
        public static extern short Vision_FindStepMark(Rectangle Rect, int Dir, bool MultiplePoint, bool PrintMultiplePoint, ref double cx, ref double cy);//返回值等于0正确//Rect查找区域 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,MultiplePoint=true查找多个点，PrintMultiplePoint=true打印多个点//3~5ms

        [DllImport("Vision4.dll", EntryPoint = "Vision_FindStepMarkMultiplePoint")]
        public static extern short Vision_FindStepMarkMultiplePoint(Rectangle Rect, int Dir, bool PrintMultiplePoint);//返回值为点数量//Rect查找区域 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling,PrintMultiplePoint=true打印并显示多个点

        [DllImport("Vision4.dll", EntryPoint = "Vision_GetStepMarkMultiplePoint")]
        public static extern void Vision_GetStepMarkMultiplePoint(int Number, ref double px, ref double py);//如Vision_FindStepMarkMultiplePoint返回值为50，按Number取点从0开始至49

        //Edge 
        [DllImport("Vision4.dll", EntryPoint = "Vision_FindEdge")]
        public static extern short Vision_FindEdge(Rectangle Rect, int Thres, int Dir, bool MultiplePoint, bool PrintMultiplePoint, ref double cx, ref double cy);//返回值等于0正确//Rect查找区域,Thres二值化， 搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling, 7:X-右至左, 8:Y-下至上,MultiplePoint=true查找多个点，PrintMultiplePoint=true打印多个点

        [DllImport("Vision4.dll", EntryPoint = "Vision_FindEdgeMultiplePoint")]
        public static extern short Vision_FindEdgeMultiplePoint(Rectangle Rect, int Thres, int Dir, bool PrintMultiplePoint);////返回值为点数量//Rect查找区域 Thres域值,搜索方向 1:X, 2:Y, 3:X-Rising, 4:X-Falling, 5:Y-Rising, 6:Y-Falling, 7:X-右至左, 8:Y-下至上,PrintMultiplePoint=true打印并显示多个点

        [DllImport("Vision4.dll", EntryPoint = "Vision_GetEdgeMultiplePoint")]
        public static extern void Vision_GetEdgeMultiplePoint(int Number, ref double px, ref double py);//如Vision_FindEdgeMultiplePoint返回值为50，按Number取点从0开始至49

        public static short VisionInit(CameraDispControl Display, int Width, int Height, int Band, GetMousePosition ShowPosition)
        {
            _DispControl = Display;
            width = Width;
            height = Height;
            short res = Vision_Init(_DispControl.panelDisp.Handle, Width, Height, Band);
            if (_DispControl != null)
            {
                _DispControl.AddMouseDown(Control_MouseDown);
                _DispControl.AddMouseUp(Control_MouseUp);
                _DispControl.AddMouseMove(Control_MouseMove);
                _getPosition = ShowPosition;
                _DispControl.AddZoom(Control_Zoom);
                _DispControl.AddSaveToPicture(SavePictureEventHandler);
            }
            Control_Zoom(50);
            return res;
        }
        private static void SavePictureEventHandler(string SaveFile)
        {
            Vision_SaveBmp(SaveFile);
        }
        protected static CameraDispControl _DispControl;
        protected static int _zoom = 25;
        public static int Zoom
        {
            get { return _zoom; }
        }
        public static void Control_Zoom(int ZoomPercent)
        {
            _zoom = ZoomPercent;
            Vision_ZoomDisp(_zoom);
            if (_DispControl != null) _DispControl.panelDisp.ClientSize = new Size(width * _zoom / 100, height * _zoom / 100);
        }
        public static void Control_Zoom()
        {
            Vision_ZoomDisp(_zoom);
        }
        private static void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point();
            pt.X = e.X;
            pt.Y = e.Y;
            Vision_MouseLDnShowRoi(pt);
        }
        private static void Control_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = new Point();
            pt.X = e.X;
            pt.Y = e.Y;
            Vision_MouseLUpShowRoi();
        }

        private static void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            point.X = e.Location.X * 100 / _zoom;
            point.Y = e.Location.Y * 100 / _zoom;
            if (_getPosition != null) _getPosition(point);
            if (e.Button == MouseButtons.Left)
            {
                Point pt = new Point();
                pt.X = e.X;
                pt.Y = e.Y;
                Point Outpt = new Point();
                Vision_MouseMoveShowRoi(pt, ref Outpt);
                pt = Outpt;
            }
        }
        public static string LoadPicture(string szpath)
        {
            if (szpath == null) return "";
            FileInfo fileinfo = new FileInfo(szpath);
            Bitmap source = new Bitmap(szpath);
            if (source.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                Bitmap bmp = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics.FromImage(bmp).DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height));
                if (!Directory.Exists("c:\\temp")) Directory.CreateDirectory("c:\\temp");
                bmp.Save("c:\\temp\\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                Vision_OpenBmp("c:\\temp\\temp.bmp");
            }
            else
                Vision_OpenBmp(szpath);
            return fileinfo.Name.Substring(0, fileinfo.Name.Length - fileinfo.Extension.Length).Split(' ')[0];
        }
        public static void DragDropFile(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            LoadPicture(s[0]);
        }
    }
}
