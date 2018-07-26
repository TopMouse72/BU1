using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace GooGolExtMdl
{
    public class GooGol_ExtMdl
    {
        [DllImport("gts.dll")]
        public static extern short GT_OpenExtMdlGts(string pDllName);
        [DllImport("gts.dll")]
        public static extern short GT_CloseExtMdlGts();
        [DllImport("gts.dll")]
        public static extern short GT_SwitchtoCardNoExtMdlGts(short card);
        [DllImport("gts.dll")]
        public static extern short GT_ResetExtMdlGts();
        [DllImport("gts.dll")]
        public static extern short GT_LoadExtConfigGts(string fileName);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtIoValueGts(short mdl, ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtIoValueGts(short mdl, ref ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtIoBitGts(short mdl, short index, ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtIoBitGts(short mdl, short index, ref ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtAdValueGts(short mdl, short chn, ref ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtAdVoltageGts(short mdl, short chn, ref double value);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtDaValueGts(short mdl, short chn, ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtDaVoltageGts(short mdl, short chn, double value);
        [DllImport("gts.dll")]
        public static extern short GT_GetStsExtMdlGts(short mdl, short chn, ref ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtDoValueGts(short mdl, ref ushort value);

        


    
    }
}
