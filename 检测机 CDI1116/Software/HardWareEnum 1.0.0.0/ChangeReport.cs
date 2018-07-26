using System;

namespace HardwarePool
{
    public class HardwareChangeReport
    {
        #region set for different version of any port index changing.
        public readonly static DateTime CurrentUpdateDate = new DateTime(2016, 11, 26);
        public readonly static string strDescription = "Original designed motor/inport/outport layout.";
        public readonly static int[] damagedInPort = { };
        public readonly static int[] damagedOutPort = { };
        #endregion set for different version of any port index changing.
    }
}