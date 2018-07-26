using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooGolExtMdl;
using Colibri.CommonModule;
using Colibri.CommonModule.IOSystem;

namespace GoogolExtIO
{
    public class GoogleExtIOSystem : BaseIOSys
    {
        public ushort[] InportValueList;
        private int _inCount, _outCount;
        public int InPortCount
        {
            get { return _inCount; }
        }
        public int OutPortCount
        {
            get { return _outCount; }
        }
        public GoogleExtIOSystem(string CardName, int ModCount, int InCount, int OutCount, string configFile = "")
            : base(CardName)
        {
            _inCount = InCount;
            _outCount = OutCount;
            ExtIO.ConfigFile = configFile;
            GoogleExtIOPort temp;
            for (short i = 0; i < _inCount; i++)
            {
                temp = new GoogleExtIOPort(this, "DO" + i.ToString(), i, "DO" + i.ToString(), "", IOType.Out);
                AddPort(temp);
            }
            for (short i = 0; i < _outCount; i++)
            {
                temp = new GoogleExtIOPort(this, "DI" + i.ToString(), i, "DI" + i.ToString(), "", IOType.In);
                AddPort(temp);
            }
            InportValueList = new ushort[ModCount];
            for (int i = 0; i < InportValueList.Length; i++)
                InportValueList[i] = 0;
        }
        public override bool InitialCard(ushort CardNum)
        {
            if (!IsSimulation)
                return ExtIO.InitExtIo() == 0;
            else
                return true;
        }
        public void ReleaseCard()
        {
            if (!IsSimulation)
                ExtIO.UnintExtIO();
        }
        private object inportReadLock = new object();
        protected override void timerTickCallback(object stateInfo)
        {
            lock (inportReadLock)
            {
                for (short i = 0; i < InportValueList.Length; i++)
                    ExtIO.GetExtInput(i, ref InportValueList[i]);
                base.timerTickCallback(stateInfo);
            }
        }
    }
    public class GoogleExtIOPort : BaseIOPort
    {
        public GoogleExtIOSystem Owner
        {
            get { return (GoogleExtIOSystem)m_owner; }
        }
        public GoogleExtIOPort(GoogleExtIOSystem owner, string name, short portno, string portname, string portcode, IOType type)
            : base(owner, name, portno, portname, portcode, type)
        {
        }
        private static object setoutlock = new object();
        public override bool SetOutput(bool OutStat)
        {
            lock (setoutlock)
            {
                if (PortNo < 0 || PortNo >= Owner.OutPortCount) throw new Exception("Set output error: Invalid port number");
                if (!base.SetOutput(OutStat)) return false;
                if (!Owner.IsSimulation)
                    ExtIO.SetExtOutPut((short)(PortNo / 16), (short)(PortNo % 16), _portstatus);
                return true;
            }
        }
        public override void GetStatus()
        {
            if (base.Type == IOType.Out) return;
            if (PortNo < 0 || PortNo >= Owner.InPortCount) throw new Exception("Get input error: Invalid port number");
            _portstatus = (Owner.InportValueList[PortNo / 16] & 1 << (PortNo % 16)) != 0;
            base.GetStatus();
        }
    }
    sealed class ExtIO
    {
        public static string ConfigFile = "";
        public static bool bExtIOInitOK = false;

        public static short InitExtIo()
        {
            short rtn = -1;
            rtn = GooGol_ExtMdl.GT_OpenExtMdlGts("");
            if (rtn != 0)
            {
                return rtn;
            }

            if (File.Exists(ConfigFile))
                rtn = GooGol_ExtMdl.GT_LoadExtConfigGts(ConfigFile);
            if (rtn != 0)
            {
                return rtn;
            }

            bExtIOInitOK = true;
            return rtn;
        }

        public static short UnintExtIO()
        {
            short rtn = -1;

            return rtn;
        }

        private static object SetOutLock = new object();
        public static short SetExtOutPut(short nCard, short nPort, bool bOn)
        {
            lock (SetOutLock)
            {
                short rtn = -1;
                if (bOn)
                    rtn = GooGol_ExtMdl.GT_SetExtIoBitGts(nCard, nPort, 0);
                else
                    rtn = GooGol_ExtMdl.GT_SetExtIoBitGts(nCard, nPort, 1);
                return rtn;
            }
        }
        private static object GetInLock = new object();
        public static short GetExtInput(short nCard, ref ushort Value)
        {
            lock (GetInLock)
            {
                short rtn = -1;
                if (!bExtIOInitOK)
                    return rtn;
                rtn = GooGol_ExtMdl.GT_GetExtIoValueGts(nCard, ref Value);
                Console.WriteLine(Value.ToString());
                return rtn;
            }
        }
    }
}