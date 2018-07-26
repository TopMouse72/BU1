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
    public delegate void GetMousePosition(Point mousepoint);
    public class VSBase
    {
        public const short COLOR_WHITE	= 245;
        public const short COLOR_GRAY = 248;
        public const short COLOR_RED = 249;
        public const short COLOR_GREEN = 250;
        public const short COLOR_YELLOW = 251;
        public const short COLOR_BLUE = 252;
        public const short COLOR_FUCHSIA = 253;
        public const short COLOR_CYAN = 254;
        protected static GetMousePosition _getPosition;
        protected static int width, height;
    }
}
