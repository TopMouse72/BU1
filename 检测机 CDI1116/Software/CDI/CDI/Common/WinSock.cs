using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;
using CDI;

namespace NetWork
{
    public class WinSock : ResponsePublisher
    {
        public bool isConnectCamera;
        #region WinSock
        private TcpClient client;
        private static byte[] localhost = { 127, 0, 0, 1 };
        private TcpListener listener = new TcpListener(new System.Net.IPAddress(localhost), 2014);
        private NetworkStream stream;
        private byte[] data;
        private string receivedstring = "";
        #endregion WinSock

        public void WaitingConnect()
        {
            listener.Start();
            client = listener.AcceptTcpClient();
            isConnectCamera = true;
            stream = client.GetStream();
            data = new byte[client.ReceiveBufferSize];
        }
        public void DisConnect()
        {
            if (client != null && client.Connected)
            {
                stream.Close();
                stream.Dispose();
                client.Close();
            }
        }
        public const string MEASDONE = "MeasDone";
        public const string CAPFINISH = "CaptureFinish";
        private const string BARCODE = "Bar";
        private const string PATH = "Path";
        private const string PRODUCT = "P";
        private const string CAMTRIG = "T";
        private const string MEASPIC = "M";
        private const string BATCHMEAS = "BatchMeas";
        private const string COMMAND_PRODUCT = PRODUCT + ":";
        private const string COMMAND_CAMTRIG = CAMTRIG + ":";
        private const string COMMAND_MEAS = MEASPIC + ":";
        private const string RET_MEASDONE = MEASDONE + ":";
        private const string RET_CAPFINISH = CAPFINISH + ":";
        private const string END = "End;";
        private const string COMMAND_BARCODE = BARCODE + ":";
        private const string COMMAND_PATH = PATH + ":";
        private const string COMMAND_BATCH_MEAS = BATCHMEAS + ":";
        private const string COMMAND_CAMLIVE = "CamLive;";
        private const string COMMAND_CAMSTOP = "CamStop;";

        public void SendCommand(string sendstring)
        {
            if (stream.CanWrite)
            {
                byte[] data = Encoding.Default.GetBytes(sendstring);
                stream.Write(data, 0, data.Length);
                ClassCommonSetting.ProgramLog(LogFile.Level.Info, "WinSock", "发送指令: " + sendstring);
            }
        }
        public void SendCommandCamLive(bool Live)
        {
            if (Live)
                SendCommand(COMMAND_CAMLIVE);
            else
                SendCommand(COMMAND_CAMSTOP);
        }
        public string SendCommandProductChange(string product)
        {
            string str = $"{COMMAND_PRODUCT}{product};";
            SendCommand(str);
            return str;
        }
        public string SendCommandCamTrig(int cellindex, string barcode, string path = "")
        {
            if (path == "") path = CommonFunction.DataPath + "Pic\\";
            string str =$"{COMMAND_CAMTRIG}{cellindex},{COMMAND_BARCODE}{barcode},{COMMAND_PATH}{path};";
            SendCommand(str);
            return str;
        }
        public string SendCommandMeas(bool isStandard, string path = "")
        {
            if (path == "") path = CommonFunction.DataPath + "Pic\\";
            string str = $"{COMMAND_MEAS}{isStandard},{COMMAND_PATH}{path};";
            SendCommand(str);
            return str;
        }
        public string SendCommandBatchMeas(string path)
        {
            if (path == "") return "";
            string str = $"{COMMAND_BATCH_MEAS}{path};";
            SendCommand(str);
            return str;
        }
        //public string SendCommandOpenSet(int setmode)
        //{
        //    string str = COMMANDOPENSET + setmode + ";";
        //    SendCommand(str);
        //    return str;
        //}
        private string GetFirst(string data, int start, ref int meas,ref int cap,ref int batch)
        {
            string first = "";
            int min = 9999;
            meas = data.IndexOf(RET_MEASDONE, start);
            cap = data.IndexOf(RET_CAPFINISH, start);
            batch = data.IndexOf(BATCHMEAS, start);
            if (meas == -1 && cap == -1 && batch == -1) return "";
            if (meas != -1 && meas < min)
            {
                min = meas;
                first = MEASDONE;
            }
            if (cap != -1 && cap < min)
            {
                min = cap;
                first = CAPFINISH;
            }
            if (batch != -1 && batch < min)
            {
                min = batch;
                first = BATCHMEAS;
            }
            return first;
        }
        public void GetData()
        {
            if (isConnectCamera)
            {
                if (client.Available > 0)
                {
                    int startIndex = 0;
                    int endindex = 0;
                    string temp;
                    int recbytes = stream.Read(data, 0, data.Length);
                    int MeasIndex = 0, CapIndex = 0, BatchIndex = 0;
                    receivedstring = Encoding.Default.GetString(data).Substring(0, recbytes);
                    ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WinSock", "Winsock receive: " + receivedstring);
                    do
                    {
                        string comm = GetFirst(receivedstring, startIndex, ref MeasIndex, ref CapIndex, ref BatchIndex);
                        if (comm == "") return;
                        switch(comm)
                        {
                            case MEASDONE:
                                //Meas first or only Meas exist.
                                endindex = receivedstring.IndexOf(END, MeasIndex);
                                temp = receivedstring.Substring(MeasIndex + RET_MEASDONE.Length, endindex - MeasIndex - RET_MEASDONE.Length);
                                ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WinSock", "Receive Measure done: " + temp);
                                notifyDoneEventSubscribers(this, new StateEventArgs(MEASDONE, temp));
                                break;
                            case CAPFINISH:
                                //Cap first or only Cap exist.
                                endindex = receivedstring.IndexOf(";", CapIndex);
                                temp = receivedstring.Substring(CapIndex + RET_CAPFINISH.Length, endindex - CapIndex - RET_CAPFINISH.Length);
                                ClassCommonSetting.ProgramLog(LogFile.Level.Debug, "WinSock", "Receive from Cap finish: " + temp);
                                notifyDoneEventSubscribers(this, new StateEventArgs(CAPFINISH, temp));
                                break;
                        }
                            startIndex = endindex;
                    } while (true);
                }
            }
        }
    }
}
