using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;

namespace CDI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName("CDI");
            if (myProcesses.Length > 1)
            //bool flag;
            //Mutex mainMutex = new Mutex(true, "CDIMutex", out flag);
            //if (!flag)
            {
                MessageBox.Show("CDI程序正在运行。如果确认CDI程序已经退出，请检查任务管理器。");
                return;
            }
            //if (args.Length > 0 && args[0] == "/Simulation")
            //    HardwarePool.SystemHardware.instance.IsSimulation = true;

            CommonFunction.DataPath = "d:\\CDI CONFIG\\";
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "Main", "程序启动");
            Application.Run(MainForm.instance);
            ClassCommonSetting.ProgramLog(LogFile.Level.Info, "Main", "程序退出");

        }
    }
}
