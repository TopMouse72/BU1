using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Colibri.CommonModule;
using Colibri.CommonModule.State;
using Colibri.CommonModule.IOSystem;
using Measure;
using CDI.Zone;
using NetWork;
using System.IO;

namespace CDI
{
    /// <summary>
    /// 电芯相关对象集合
    /// </summary>
    /// <typeparam name="CELLITEM"></typeparam>
    public class CellCollection<CELLITEM> : Dictionary<EnumCellIndex, CELLITEM>
    {
        public void Add(int cellindex, CELLITEM value)
        {
            try
            {
                Add((EnumCellIndex)cellindex, value);
            }
            catch (Exception e)
            {
                MessageBox.Show("CellCollection of " + typeof(CELLITEM) + " error when adding.");
                throw e;
            }
        }
        public CELLITEM this[int index]
        {
            get { return this[(EnumCellIndex)index]; }
            set { this[(EnumCellIndex)index] = value; }
        }
        public CELLITEM[] ToArray()
        {
            List<CELLITEM> cells = new List<CELLITEM>();
            foreach (CELLITEM cell in this.Values)
                cells.Add(cell);
            return cells.ToArray();
        }
    }
    public static partial class ClassCommonSetting
    {
        private static Process AOIProcess;
        private static TimeClass timer = new TimeClass();
        private static WinSock socket = new WinSock();
        public static WinSock SocketToAOI
        {
            get { return socket; }
        }
        public static DTS.AutoLine autoLine = new DTS.AutoLine();
        //public static double SetRandomData(string celltype, Cst.Struct_DataInfo StdData, double spec)
        //{
        //    double std = GetStdData(celltype, StdData);
        //    Random ran = new Random();
        //    return std + (ran.NextDouble() - 0.5) * spec * 2;
        //}
        /// <summary>
        /// 清除旧数据
        /// </summary>
        /// <param name="AOIMonthBefore">旧AOI图片的保留月份</param>
        /// <param name="DataMonthBefore">旧数据文件的保留月份</param>
        /// <param name="LogMonthBefore">旧日志文件的保留月份</param>
        public static void ClearOldData(int AOIDaysBefore = 15, int DataMonthBefore = 6, int LogMonthBefore = 1)
        {
            DateTime oldDateAOI = DateTime.Today.AddDays(-AOIDaysBefore);
            DateTime oldDateLog = DateTime.Today.AddMonths(-LogMonthBefore);
            DateTime oldDateData = DateTime.Today.AddMonths(-DataMonthBefore);
            //oldDateAOI = new DateTime(oldDateAOI.Year, oldDateAOI.Month, 1);
            //oldDateLog = new DateTime(oldDateLog.Year, oldDateLog.Month, 1);
            //oldDateData = new DateTime(oldDateData.Year, oldDateData.Month, 1);

            //Delete old AOI picture
            if (AOIDaysBefore > 0)
                if (Directory.Exists(SysParam.CCDSavePath))
                    foreach (string filename in Directory.GetFiles(SysParam.CCDSavePath))
                    {
                        FileInfo file = new FileInfo(filename);
                        DateTime createdate = file.CreationTime;
                        if (createdate < oldDateAOI)
                            File.Delete(filename);
                    }
            //Delete old log
            if (LogMonthBefore > 0)
            {
                if (Directory.Exists(LogFile.DefaultLogPath))
                {
                    foreach (string filename in Directory.GetFiles(LogFile.DefaultLogPath))
                    {
                        FileInfo file = new FileInfo(filename);
                        DateTime createdate = file.CreationTime;
                        if (createdate < oldDateLog)
                            File.Delete(filename);
                    }
                }
                if(Directory.Exists(SysParam.HistoryPath))
                {
                    foreach (string filename in Directory.GetFiles(SysParam.HistoryPath))
                    {
                        FileInfo file = new FileInfo(filename);
                        DateTime createdate = file.CreationTime;
                        if (createdate < oldDateLog)
                            File.Delete(filename);
                    }
                }
            }
            //Delete old Data
            if (DataMonthBefore > 0)
                if (Directory.Exists(SysParam.DataSavePath))
                {
                    foreach (string filename in Directory.GetFiles(SysParam.DataSavePath))
                    {
                        FileInfo file = new FileInfo(filename);
                        DateTime createdate = file.CreationTime;
                        if (createdate < oldDateData)
                            File.Delete(filename);
                    }
                }
        }

        public static void CommonInit()
        {
            ClearOldData();
            SysParam.LoadParameter();
            timer.ThreadTimerTickEvent += timerTime_Tick;
            OpenCDIVision();
        }
        public static void OpenCDIVision()
        {
            AOIProcess = StartAOIProcess();
            SocketToAOI.WaitingConnect();
            System.Threading.Thread.Sleep(2000);
            ClassCommonSetting.SocketToAOI.SendCommandProductChange(SysParam.CurrentProduct);
            timer.StartTimer(50, 50);
        }
        public static void CommonRelease()
        {
            timer.StopTimer();
            CloseCDIVision();
        }
        public static void CloseCDIVision()
        {
            timer.StopTimer();
            TimeClass.Delay(100);
            SocketToAOI.DisConnect();
            if (AOIProcess != null)
                if(!AOIProcess.HasExited)
                    AOIProcess.CloseMainWindow();
            AOIProcess = null;
        }
        private static void timerTime_Tick(object sender, EventArgs e)
        {
            SocketToAOI.GetData();
        }
        public static Process StartProcess(string exeFile)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = exeFile;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            proc.Start();
            return proc;
        }
        public static Process StartAOIProcess()
        {
            KillProcess("VisionDemo");
            return StartProcess(Application.StartupPath + "\\vision\\VisionDemo.exe");
        }
        public static void KillProcess(string processName)
        {
            foreach (Process thisproc in Process.GetProcessesByName(processName))
                thisproc.CloseMainWindow();
        }
        public static void SMLogInfo(string StateMachineName, string StateName, string Msg)
        {
            BaseStateMachine.StateMachineLogFile.LogInfo(String.Format("State {0} in {1}: {2}", StateName, StateMachineName, Msg));
        }
        public static void UpdateIOStatus(Control con, BaseIOPort port)
        {
            con.BackColor = port.status ? Color.Tomato : Color.ForestGreen;
        }
        public static double[] GetTopAlignZClampOffset(double partsize, double clampsize, double sideignore = 6)
        {
            List<double> offset = new List<double>();
            //Find start offset
            double start = (clampsize - partsize) / 2 + sideignore;
            offset.Add(start);
            double pos = start;
            do
            {
                pos += clampsize;
                if (pos < (partsize - clampsize) / 2 - sideignore)
                    //new offset pos
                    offset.Add(pos);
                else
                {
                    //End offset pos
                    pos = (partsize - clampsize) / 2 - sideignore;
                    offset.Add(pos);
                    break;
                }
            } while (offset.Count < 5);
            return offset.ToArray();
        }
        /// <summary>
        /// 检测方法执行是否超时。
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>执行方法正常没有超时返回true，否则返回false。</returns>
        public static bool CheckTimeOut(Func<bool> action, int timeout = ClassErrorHandle.TIMEOUT)
        {
            bool res;
            DateTime start = DateTime.Now;
            do
            {
                res = action();
                Application.DoEvents();
            } while (!res && (DateTime.Now - start).TotalMilliseconds < timeout);
            return res;
        }
        /// <summary>
        /// 检测方法执行是否超时。
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>执行方法正常没有超时返回true，否则返回false。</returns>
        public static string CheckTimeOut(Func<string> action, int timeout = ClassErrorHandle.TIMEOUT)
        {
            string res;
            DateTime start = DateTime.Now;
            do
            {
                res = action();
                Application.DoEvents();
            } while (res != "" && (DateTime.Now - start).TotalMilliseconds < timeout);
            return res;
        }
    }
}
