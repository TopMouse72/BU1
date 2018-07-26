using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Colibri.CommonModule;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;

namespace NetWork
{
    public class WinSock : ResponsePublisher
    {
        public bool isConnectCamera;
        #region WinSock
        TcpClient client;
        static byte[] localhost = { 127, 0, 0, 1 };
        TcpListener listener = new TcpListener(new System.Net.IPAddress(localhost), 2014);
        NetworkStream stream;
        byte[] data;
        string receivedstring;
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
        private const string RESMEASDONE = MEASDONE + ":";
        private const string RESCAPFINISH = CAPFINISH + ":";
        private const string END = "End;";
        private const string COMMANDCAMTRIG = "T:";
        private const string COMMANDPRODUCT = "P:";
        public void SendCommand(string sendstring)
        {
            if (stream.CanWrite)
            {
                byte[] data = Encoding.ASCII.GetBytes(sendstring);
                stream.Write(data, 0, data.Length);
            }
        }
        public string SendCommandProductChange(string product)
        {
            string str = COMMANDPRODUCT + product + ";";
            SendCommand(str);
            return str;
        }
        public string SendCommandCamTrig(int cellindex)
        {
            string str = COMMANDCAMTRIG + cellindex + ";";
            SendCommand(str);
            return str;
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
                    int MeasIndex, CapIndex;
                    receivedstring = Encoding.ASCII.GetString(data).Substring(0, recbytes);
                    LogFile.instance.LogDebug("Winsock", "Winsock receive: " + receivedstring);
                    do
                    {
                        MeasIndex = receivedstring.IndexOf(RESMEASDONE, startIndex);
                        CapIndex = receivedstring.IndexOf(RESCAPFINISH, startIndex);
                        //Meas and Cap both don't exist.
                        if (MeasIndex == CapIndex) return;
                        if (MeasIndex < CapIndex && MeasIndex > -1 || CapIndex == -1)
                        {
                            //Meas first or only Meas exist.
                            endindex = receivedstring.IndexOf(END, MeasIndex);
                            temp = receivedstring.Substring(MeasIndex + RESMEASDONE.Length, endindex - MeasIndex - RESMEASDONE.Length);
                            LogFile.instance.LogDebug("WinSock", "Receive Measure done: " + temp);
                            notifyDoneEventSubscribers(this, new StateEventArgs(MEASDONE, temp));
                            startIndex = endindex;
                        }
                        if (MeasIndex == -1 || CapIndex > -1 && MeasIndex > CapIndex)
                        {
                            //Cap first or only Cap exist.
                            endindex = receivedstring.IndexOf(";", CapIndex);
                            temp = receivedstring.Substring(CapIndex + RESCAPFINISH.Length, endindex - CapIndex - RESCAPFINISH.Length);
                            LogFile.instance.LogDebug("WinSock", "Receive from Cap finish: " + temp);
                            notifyDoneEventSubscribers(this, new StateEventArgs(CAPFINISH, temp));
                            startIndex = endindex;
                        }
                    } while (true);
                }
            }
        }
    }
}