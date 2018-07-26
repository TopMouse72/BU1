using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Colibri.CommonModule;
using Colibri.CommonModule.Forms;
using Colibri.CommonModule.Event;
using Colibri.CommonModule.State;

/// <summary>
/// 流程状态说明
/// </summary>
public enum WorkFlowStatus
{
    /// <summary>
    /// 流程没有运行（无报警正常停止）状态
    /// </summary>
    空闲,
    /// <summary>
    /// 流程正常运行的跑料状态
    /// </summary>
    运行,
    /// <summary>
    /// 流程正常运行的待料状态（上料传送带没有新物料超过5秒或是流程暂停）
    /// </summary>
    报警,
    /// <summary>
    /// 流程报警状态（可重试的报警级别错误和导致流程停止的错误级别错误。报警状态下即使流程停止，仍然是报警状态，直到主界面上的所有错误都被处理，列表为空）
    /// </summary>
    待料,
    /// <summary>
    /// 报警结束状态（报警结束状态并非真正的状态。在报警状态下，当主界面上错误列表中的所有错误都被处理，报警结束状态发出，设备进入待料状态）
    /// </summary>
    报警结束,
}
public enum ProductResult
{
    InNG,
    OK,
    OutNG,
}
/// <summary>
/// 发送统计数据
/// </summary>
/// <param name="Day">白班数据</param>
/// <param name="Night">晚班数据</param>
/// <param name="Total">合计数据</param>
public delegate void SendStatisticValue(double Day, double Night, double Total);
public class SystemEventStatistic : SystemSubscriber
{
    private DataTable _TimeTable = new DataTable();
    private DataTable _hisTimeTable = new DataTable();
    private DataGridView _gridView;
    private string _path;
    //private string _file;
    public SystemEventStatistic(string SavePath, DataGridView GridView)
    {
        _path = SavePath;
        _gridView = GridView;
        _TimeTable.Columns.Add("事件");
        _TimeTable.Columns.Add("时间");
        Load(DateTime.Now);
    }
    public void ResetTable()
    {
        _TimeTable.Clear();
    }
    public string SetFileName(DateTime date)
    {
        return _path + date.ToString("yyyyMMdd") + "Event.xml";
    }
    public void Load(DateTime SelDate)
    {
        string _file = SetFileName(SelDate);
        if (File.Exists(_file))
            if ((SelDate.Date - new TimeSpan(8, 0, 0)) == DateTime.Today)
            {
                _TimeTable.ReadXml(_file);
                if (_gridView != null) _gridView.DataSource = _TimeTable;
            }
            else
            {
                _hisTimeTable.ReadXml(_file);
                if (_gridView != null) _gridView.DataSource = _hisTimeTable;
            }
    }
    private object SaveLock = new object();
    private void Save()
    {
        lock(SaveLock)
        {
            _TimeTable.WriteXml(SetFileName(DateTime.Now));
        }
    }
    private void GetEventInfo(StateEventArgs e)
    {
        DataRow row = _TimeTable.NewRow();
        row[0] = e.eventName + ": " + e.eventInfo;
        row[1] = DateTime.Now.ToString("hh:mm:ss");
        _TimeTable.Rows.Add(row);
        Save();
    }
    public override void EStopHandler(BaseClass sender, StateEventArgs e)
    {
        GetEventInfo(e);
    }
    public override void PauseHandler(BaseClass sender, StateEventArgs e)
    {
        GetEventInfo(e);
    }
    public override void ResumeHandler(BaseClass sender, StateEventArgs e)
    {
        GetEventInfo(e);
    }
    public override void StartHandler(BaseClass sender, StateEventArgs e)
    {
        GetEventInfo(e);
    }
    public override void StopHandler(BaseClass sender, StateEventArgs e)
    {
        GetEventInfo(e);
    }
}
/// <summary>
/// 生产数据统计
/// </summary>
public static class Statistic
{
    //产量统计列
    private const string COL_PROD_TIME = "时间段";
    private const string COL_PROD_COUNT = "产量";
    private const string COL_PROD_IN_NG_COUNT = "进料不合格";
    private const string COL_PROD_OK_COUNT = "合格";
    private const string COL_PROD_NG_COUNT = "不合格";
    private const string COL_PROD_WORK_SPAN = "运行时间(分钟)";
    private const string COL_PROD_DOWN_SPAN = "报警时间(分钟)";
    private const string COL_PROD_IDLE_SPAN = "待料时间(分钟)";
    //报警记录列
    private const string COL_ALARM_TIME = "报警时间";
    private const string COL_ALARM_LEVEL = "报警级别";
    private const string COL_ALARM_SOURCE = "报警来源";
    private const string COL_ALARM_MESSAGE = "报警信息";
    //运行记录列
    private const string COL_WORKFLOW_TIME = "时间";
    private const string COL_WORKFLOW_STATUS = "运行状态";
    private const string COL_WORKFLOW_MEMO = "备注";
    //产量统计总表
    private const int ROW_TOTAL_INCOME_NG = 0;
    private const int ROW_TOTAL_OK = 1;
    private const int ROW_TOTAL_NG = 2;
    private const int ROW_TOTAL_COUNT = 3;
    private const int ROW_TOTAL_YIELD = 4;
    private const int DAYSHIFT = 0;
    private const int NIGHTSHIFT = 1;

    private static int prodStatRow = 12;
    private static event SendStatisticValue sendYield;
    private static event SendStatisticValue sendDT;
    private static event EventHandler updateState;

    /// <summary>
    /// 发送优率数据事件
    /// </summary>
    public static event SendStatisticValue UpdateYield
    {
        add { sendYield -= value; sendYield += value; }
        remove { sendYield -= value; }
    }
    /// <summary>
    /// 发送Downtime百分比数据事件
    /// </summary>
    public static event SendStatisticValue UpdateDT
    {
        add { sendDT -= value; sendDT += value; }
        remove { sendDT -= value; }
    }
    /// <summary>
    /// 发送流程状态改变信息事件
    /// sender即为状态改变字符串
    /// </summary>
    public static event EventHandler UpdateState
    {
        add { updateState -= value; updateState += value; }
        remove { updateState -= value; }
    }
    private static int ShiftIndex = DAYSHIFT;
    private static string DataPath = "d:\\";
    private static DataTable[] product = new DataTable[2];
    private static DataTable alm, workflow;
    private static DataTable[] HistProduct = new DataTable[2];
    private static DataTable HistAlm, HistWorkflow;
    //private static DataTable[] OldProduct = new DataTable[2];
    //private static DataTable OldAlarm, OldWorkflow;
    private static DataTable TotalProduct, OldTotalProduct;
    private static DateTime selDate;
    private static DataGridView DispDay, DispNight, DispAlarm, DispWorkflow, DispTotal;
    public static bool IsWorkFlowRunning;
    public static double DayYield, NightYield, TotalYield;
    public static double DayDown, NightDown, TotalDown;
    private static WorkFlowStatus _wfStatus = WorkFlowStatus.空闲;
    private static WorkFlowStatus _lastStatus = WorkFlowStatus.空闲;
    /// <summary>
    /// 返回流程状态
    /// </summary>
    public static WorkFlowStatus WFStatus
    {
        get { return _wfStatus; }
    }
    /// <summary>
    /// 如果在线程中使用，则需要设置消息显示在哪个窗之前，否则消息有可能不会显示在最前面。
    /// </summary>
    public static IWin32Window MessageDisplayBeforeWindow;
    //public static bool Paused;
    private static TimeClass timer = new TimeClass();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="historyPath">指定统计数据保存的位置</param>
    /// <param name="gridDay">传入显示白班数据的表格</param>
    /// <param name="gridNight">传入显示夜班数据的表格</param>
    /// <param name="gridAlarm">传入显示报警记录的表格</param>
    /// <param name="gridWorkflow">传入显示流程状态的表格</param>
    /// <param name="gridTotal">传入显示总统计数据的表格</param>
    public static void Init(string historyPath, DataGridView gridDay, DataGridView gridNight, DataGridView gridAlarm, DataGridView gridWorkflow, DataGridView gridTotal)
    {
        DataPath = historyPath;
        TotalFile = DataPath + "Total.xml";
        DispDay = gridDay;
        DispNight = gridNight;
        DispAlarm = gridAlarm;
        DispWorkflow = gridWorkflow;
        DispTotal = gridTotal;
        if (!Directory.Exists(DataPath)) Directory.CreateDirectory(DataPath);
        timer.ThreadTimerTickEvent += TimerTick;
        //Create columns for all datatables
        for (int i = 0; i < product.Length; i++)
        {
            product[i] = new DataTable();
            product[i].Columns.Add(COL_PROD_TIME);
            product[i].Columns.Add(COL_PROD_COUNT);
            product[i].Columns.Add(COL_PROD_IN_NG_COUNT);
            product[i].Columns.Add(COL_PROD_OK_COUNT);
            product[i].Columns.Add(COL_PROD_NG_COUNT);
            product[i].Columns.Add(COL_PROD_WORK_SPAN);
            product[i].Columns.Add(COL_PROD_DOWN_SPAN);
            product[i].Columns.Add(COL_PROD_IDLE_SPAN);
            HistProduct[i] = new DataTable();
            HistProduct[i].Columns.Add(COL_PROD_TIME);
            HistProduct[i].Columns.Add(COL_PROD_COUNT);
            HistProduct[i].Columns.Add(COL_PROD_IN_NG_COUNT);
            HistProduct[i].Columns.Add(COL_PROD_OK_COUNT);
            HistProduct[i].Columns.Add(COL_PROD_NG_COUNT);
            HistProduct[i].Columns.Add(COL_PROD_WORK_SPAN);
            HistProduct[i].Columns.Add(COL_PROD_DOWN_SPAN);
            HistProduct[i].Columns.Add(COL_PROD_IDLE_SPAN);
            //OldProduct[i] = new DataTable();
            //OldProduct[i].Columns.Add(COL_PROD_TIME);
            //OldProduct[i].Columns.Add(COL_PROD_COUNT);
            //OldProduct[i].Columns.Add(COL_PROD_IN_NG_COUNT);
            //OldProduct[i].Columns.Add(COL_PROD_OK_COUNT);
            //OldProduct[i].Columns.Add(COL_PROD_NG_COUNT);
            //OldProduct[i].Columns.Add(COL_PROD_WORK_SPAN);
            //OldProduct[i].Columns.Add(COL_PROD_DOWN_SPAN);
            //OldProduct[i].Columns.Add(COL_PROD_IDLE_SPAN);
        }
        product[DAYSHIFT].TableName = "白班数据";
        product[NIGHTSHIFT].TableName = "晚班数据";
        HistProduct[DAYSHIFT].TableName = "白班数据";
        HistProduct[NIGHTSHIFT].TableName = "晚班数据";
        //OldProduct[DAYSHIFT].TableName = "DayShift";
        //OldProduct[NIGHTSHIFT].TableName = "NightShift";

        TotalProduct = new DataTable("生产总计");
        TotalProduct.Columns.Add();
        TotalProduct.Columns.Add();
        OldTotalProduct = new DataTable("TotalProduct");
        OldTotalProduct.Columns.Add();
        OldTotalProduct.Columns.Add();

        alm = new DataTable("报警记录");
        alm.Columns.Add(COL_ALARM_TIME);
        alm.Columns.Add(COL_ALARM_LEVEL);
        alm.Columns.Add(COL_ALARM_SOURCE);
        alm.Columns.Add(COL_ALARM_MESSAGE);
        HistAlm = new DataTable("报警记录");
        HistAlm.Columns.Add(COL_ALARM_TIME);
        HistAlm.Columns.Add(COL_ALARM_LEVEL);
        HistAlm.Columns.Add(COL_ALARM_SOURCE);
        HistAlm.Columns.Add(COL_ALARM_MESSAGE);
        //OldAlarm = new DataTable("AlarmRecord");
        //OldAlarm.Columns.Add(COL_ALARM_TIME);
        //OldAlarm.Columns.Add(COL_ALARM_LEVEL);
        //OldAlarm.Columns.Add(COL_ALARM_SOURCE);
        //OldAlarm.Columns.Add(COL_ALARM_MESSAGE);

        workflow = new DataTable("流程状态记录");
        workflow.Columns.Add(COL_WORKFLOW_TIME);
        workflow.Columns.Add(COL_WORKFLOW_STATUS);
        workflow.Columns.Add(COL_WORKFLOW_MEMO);
        HistWorkflow = new DataTable("流程状态记录");
        HistWorkflow.Columns.Add(COL_WORKFLOW_TIME);
        HistWorkflow.Columns.Add(COL_WORKFLOW_STATUS);
        HistWorkflow.Columns.Add(COL_WORKFLOW_MEMO);
        //OldWorkflow = new DataTable("WorkFlowRecord");
        //OldWorkflow.Columns.Add(COL_WORKFLOW_TIME);
        //OldWorkflow.Columns.Add(COL_WORKFLOW_STATUS);
        //OldWorkflow.Columns.Add(COL_WORKFLOW_MEMO);
        Load();
    }
    /// <summary>
    /// 白晚班生产数据初始化
    /// </summary>
    /// <param name="shift">指定班次</param>
    private static void ResetProdTable(int shift, DataTable table)
    {

        //Add rows for all datatables
        DataRow row;
        table.Clear();
        for (int i = 8; i < 20; i++)
        {
            row = table.NewRow();
            row[COL_PROD_TIME] = string.Format("{0:00}:00-{1:00}:00", (i + shift * 12) % 24, (i + shift * 12 + 1) % 24);
            row[COL_PROD_COUNT] = 0;
            row[COL_PROD_IN_NG_COUNT] = 0;
            row[COL_PROD_OK_COUNT] = 0;
            row[COL_PROD_NG_COUNT] = 0;
            row[COL_PROD_WORK_SPAN] = 0;
            row[COL_PROD_DOWN_SPAN] = 0;
            row[COL_PROD_IDLE_SPAN] = 0;
            table.Rows.Add(row);
        }
        row = table.NewRow();
        row[COL_PROD_TIME] = "统计";
        row[COL_PROD_COUNT] = 0;
        row[COL_PROD_IN_NG_COUNT] = 0;
        row[COL_PROD_OK_COUNT] = 0;
        row[COL_PROD_NG_COUNT] = 0;
        row[COL_PROD_WORK_SPAN] = 0;
        row[COL_PROD_DOWN_SPAN] = 0;
        row[COL_PROD_IDLE_SPAN] = 0;
        table.Rows.Add(row);
    }
    private static void ResetTotal()
    {
        TotalProduct.Clear();
        for (int i = 0; i < 5; i++)
            TotalProduct.Rows.Add(TotalProduct.NewRow());
        TotalProduct.Rows[ROW_TOTAL_INCOME_NG][0] = "上料NG数量"; TotalProduct.Rows[ROW_TOTAL_INCOME_NG][1] = 0;
        TotalProduct.Rows[ROW_TOTAL_OK][0] = "下料OK数量"; TotalProduct.Rows[ROW_TOTAL_OK][1] = 0;
        TotalProduct.Rows[ROW_TOTAL_NG][0] = "下料NG数量"; TotalProduct.Rows[ROW_TOTAL_NG][1] = 0;
        TotalProduct.Rows[ROW_TOTAL_COUNT][0] = "总数量"; TotalProduct.Rows[ROW_TOTAL_COUNT][1] = 0;
        TotalProduct.Rows[ROW_TOTAL_YIELD][0] = "优率"; TotalProduct.Rows[ROW_TOTAL_YIELD][1] = 0;
    }
    private static string DayFile, NightFile, AlarmFile, WorkflowFile, TotalFile;
    private static void SetFileName(DateTime date)
    {
        selDate = (date - new TimeSpan(8, 0, 0)).Date;
        string filename = DataPath + selDate.ToString("yyyyMMdd");
        DayFile = filename + "Day.xml";
        NightFile = filename + "Night.xml";
        AlarmFile = filename + "Alarm.xml";
        WorkflowFile = filename + "WorkFlow.xml";
    }
    private static bool isDispingError = false;
    private static void UpdateStatDataAsyncReturn(IAsyncResult result)
    {
        SendStatisticValue handler = (SendStatisticValue)result.AsyncState;
        try
        {
            handler.EndInvoke(result);
            result.AsyncWaitHandle.Close();
        }
        catch (Exception e)
        {
            if (!isDispingError)
            {
                isDispingError = true;
                BaseForm.DoInvokeRequired((Control)MessageDisplayBeforeWindow, () =>
                {
                    if (MessageDisplayBeforeWindow != null)
                        MessageBox.Show(MessageDisplayBeforeWindow, "更新统计值出错：" + e.Message);
                    else
                        MessageBox.Show("更新统计值出错：" + e.Message);
                });
                isDispingError = false;
            }
        }
    }
    private static void CalcYield(DataTable DayDT, DataTable NightDT)
    {
        int DayPart = int.Parse(DayDT.Rows[prodStatRow][COL_PROD_COUNT].ToString());
        int DayOKPart = int.Parse(DayDT.Rows[prodStatRow][COL_PROD_OK_COUNT].ToString());
        int NightPart = int.Parse(NightDT.Rows[prodStatRow][COL_PROD_COUNT].ToString());
        int NightOKPart = int.Parse(NightDT.Rows[prodStatRow][COL_PROD_OK_COUNT].ToString());
        DayYield = DayPart == 0 ? 0 : (double)DayOKPart / DayPart;
        NightYield = NightPart == 0 ? 0 : (double)NightOKPart / NightPart;
        TotalYield = (DayPart + NightPart) == 0 ? 0 : (double)(DayOKPart + NightOKPart) / (DayPart + NightPart);
        if (sendYield != null) sendYield.BeginInvoke(DayYield, NightYield, TotalYield, UpdateStatDataAsyncReturn, sendYield);
    }
    private static void CalcDT(DataTable DayDT, DataTable NightDT)
    {
        int DayRunTime = int.Parse(DayDT.Rows[prodStatRow][COL_PROD_WORK_SPAN].ToString());
        int DayDownTime = int.Parse(DayDT.Rows[prodStatRow][COL_PROD_DOWN_SPAN].ToString());
        int NightRunTime = int.Parse(NightDT.Rows[prodStatRow][COL_PROD_WORK_SPAN].ToString());
        int NightDownTime = int.Parse(NightDT.Rows[prodStatRow][COL_PROD_DOWN_SPAN].ToString());
        DayDown = DayDownTime == 0 ? 0 : (double)DayDownTime / (DayRunTime + DayDownTime);
        NightDown = NightDownTime == 0 ? 0 : (double)NightDownTime / (NightRunTime + NightDownTime);
        TotalDown = (DayDownTime + NightDownTime) == 0 ? 0 : (double)(DayDownTime + NightDownTime) / (DayRunTime + DayDownTime + NightRunTime + NightDownTime);
        if (sendDT != null) sendDT.BeginInvoke(DayDown, NightDown, TotalDown, UpdateStatDataAsyncReturn, sendDT);
    }
    public static void ClearTotal()
    {
        foreach (DataRow row in TotalProduct.Rows)
            row[1] = 0;
        Save(DataFile.Total);
    }
    private static object SaveLock = new object();
    private static void Save()
    {
        lock (SaveLock)
        {
            SetFileName(DateTime.Now);
            SaveTable(product[DAYSHIFT], DayFile);
            SaveTable(product[NIGHTSHIFT], NightFile);
            SaveTable(alm, AlarmFile);
            SaveTable(workflow, WorkflowFile);
            SaveTable(TotalProduct, TotalFile);
        }
    }
    private enum DataFile
    {
        Day,
        Night,
        Alarm,
        WorkFlow,
        Total,
    }
    private static bool Save(DataFile filetype)
    {
        lock (SaveLock)
        {
            switch (filetype)
            {
                case DataFile.Alarm:
                    return SaveTable(alm, AlarmFile);
                case DataFile.Day:
                    return SaveTable(product[DAYSHIFT], DayFile);
                case DataFile.Night:
                    return SaveTable(product[NIGHTSHIFT], NightFile);
                case DataFile.Total:
                    return SaveTable(TotalProduct, TotalFile);
                case DataFile.WorkFlow:
                    return SaveTable(workflow, WorkflowFile);
                default:
                    return true;
            }
        }
    }
    private static bool SaveTable(DataTable table, string filename)
    {
        string backfile = filename.ToLower().Replace(".xml", ".bak");
        try
        {
            if (File.Exists(filename))
            {
                if (File.Exists(backfile)) File.Delete(backfile);
                File.Move(filename, backfile);
            }
            table.WriteXml(filename);
            File.Delete(backfile);
            return true;
        }
        catch (Exception e)
        {
            if (!isDispingError)
            {
                isDispingError = true;
                BaseForm.DoInvokeRequired((Control)MessageDisplayBeforeWindow, () =>
                {
                    if (MessageDisplayBeforeWindow != null)
                        MessageBox.Show(MessageDisplayBeforeWindow, "保存" + table.TableName + "出错。检查文件是否在别的程序中打开。");
                    else
                        MessageBox.Show("保存" + table.TableName + "出错。检查文件是否在别的程序中打开。");
                });
                isDispingError = false;
            }
            return false;
        }
    }
    private static void ReadData(int shift, string datafile, DataTable table)
    {
        table.Clear();
        if (File.Exists(datafile))
            table.ReadXml(datafile);
        else
            ResetProdTable(shift, table);
        //try { oldtable.Clear(); oldtable.ReadXml(datafile); } catch { }
        //TransferOldData(oldtable, table, rowcount);
    }
    private static void ReadData(string datafile, DataTable table)
    {
        table.Clear();
        if (File.Exists(datafile))
            table.ReadXml(datafile);
    }
    //private static void TransferOldData(DataTable oldtable, DataTable newtable, int rowcount = 0)
    //{
    //    if (newtable.Rows.Count == 0 && oldtable.Rows.Count > 0)
    //        for (int i = 0; i < (rowcount == 0 ? oldtable.Rows.Count : rowcount); i++)
    //        {
    //            DataRow row = newtable.NewRow();
    //            for (int k = 0; k < newtable.Columns.Count; k++)
    //                row[k] = oldtable.Rows[i][k];
    //            newtable.Rows.Add(row);
    //        }
    //}
    public static void Load()
    {
        int index, row;
        GetShift(DateTime.Now, out index, out row);
        SetFileName(DateTime.Now);
        ReadData(DAYSHIFT, DayFile, product[DAYSHIFT]);
        ReadData(NIGHTSHIFT, NightFile, product[NIGHTSHIFT]);
        try { CalcYield(product[DAYSHIFT], product[NIGHTSHIFT]); } catch { }
        try { CalcDT(product[DAYSHIFT], product[NIGHTSHIFT]); } catch { }

        ReadData(AlarmFile, alm);
        ReadData(WorkflowFile, workflow);
        ReadData(TotalFile, TotalProduct);
        if (TotalProduct.Rows.Count == 0) ResetTotal();
        if (DispTotal != null) DispTotal.DataSource = TotalProduct;
        if (DispAlarm != null) DispAlarm.DataSource = alm;
        if (DispWorkflow != null) DispWorkflow.DataSource = workflow;
        if (DispDay != null) DispDay.DataSource = product[DAYSHIFT];
        if (DispNight != null) DispNight.DataSource = product[NIGHTSHIFT];
    }
    public static DateTime Load(DateTime date)
    {
        if ((date.Date - new TimeSpan(8, 0, 0)) == DateTime.Today)
            Load();
        else
        {
            SetFileName(date);
            ReadData(DAYSHIFT, DayFile, HistProduct[DAYSHIFT]);
            ReadData(NIGHTSHIFT, NightFile, HistProduct[NIGHTSHIFT]);
            CalcYield(HistProduct[DAYSHIFT], HistProduct[NIGHTSHIFT]);
            CalcDT(HistProduct[DAYSHIFT], HistProduct[NIGHTSHIFT]);
            ReadData(AlarmFile, HistAlm);
            ReadData(WorkflowFile, HistWorkflow);
            if (DispDay != null) DispDay.DataSource = HistProduct[DAYSHIFT];
            if (DispNight != null) DispNight.DataSource = HistProduct[NIGHTSHIFT];
            if (DispAlarm != null) DispAlarm.DataSource = HistAlm;
            if (DispWorkflow != null) DispWorkflow.DataSource = HistWorkflow;
        }
        return selDate;
    }
    private static object shiftlock = new object();
    private static void GetShift(DateTime time, out int dtIndex, out int rowIndex)
    {
        lock (shiftlock)
        {
            int hour = time.Hour;
            if (hour < 20 && hour >= 8)//Day shift
            {
                dtIndex = DAYSHIFT;
                rowIndex = hour - 8;
                if (ShiftIndex == NIGHTSHIFT)
                {
                    for (int j = 0; j < product.Length; j++)
                    {
                        foreach (DataRow row in product[j].Rows)
                        {
                            row[COL_PROD_COUNT] = 0;
                            row[COL_PROD_IN_NG_COUNT] = 0;
                            row[COL_PROD_OK_COUNT] = 0;
                            row[COL_PROD_NG_COUNT] = 0;
                            row[COL_PROD_WORK_SPAN] = 0;
                            row[COL_PROD_DOWN_SPAN] = 0;
                            row[COL_PROD_IDLE_SPAN] = 0;
                        }
                    }
                    alm.Rows.Clear();
                    workflow.Rows.Clear();
                }
            }
            else//Night shift
            {
                dtIndex = NIGHTSHIFT;
                rowIndex = ((hour + 12) % 24) - 8;
            }
            ShiftIndex = dtIndex;
        }
    }
    private static object GetProductInfoLock = new object();
    public static void GetProductInfo(DateTime GetTime, int Good, int NG)
    {
        lock (GetProductInfoLock)
        {
            int index, row;
            GetShift(GetTime, out index, out row);
            product[index].Rows[row][COL_PROD_COUNT] = int.Parse(product[index].Rows[row][COL_PROD_COUNT].ToString()) + Good + NG;
            product[index].Rows[row][COL_PROD_OK_COUNT] = int.Parse(product[index].Rows[row][COL_PROD_OK_COUNT].ToString()) + Good;
            product[index].Rows[row][COL_PROD_NG_COUNT] = int.Parse(product[index].Rows[row][COL_PROD_NG_COUNT].ToString()) + NG;
            product[index].Rows[prodStatRow][COL_PROD_COUNT] = int.Parse(product[index].Rows[prodStatRow][COL_PROD_COUNT].ToString()) + Good + NG;
            product[index].Rows[prodStatRow][COL_PROD_OK_COUNT] = int.Parse(product[index].Rows[prodStatRow][COL_PROD_OK_COUNT].ToString()) + Good;
            product[index].Rows[prodStatRow][COL_PROD_NG_COUNT] = int.Parse(product[index].Rows[prodStatRow][COL_PROD_NG_COUNT].ToString()) + NG;
            TotalProduct.Rows[ROW_TOTAL_OK][1] = int.Parse(TotalProduct.Rows[ROW_TOTAL_OK][1].ToString()) + Good;
            TotalProduct.Rows[ROW_TOTAL_NG][1] = int.Parse(TotalProduct.Rows[ROW_TOTAL_NG][1].ToString()) + NG;
            TotalProduct.Rows[ROW_TOTAL_COUNT][1] = int.Parse(TotalProduct.Rows[ROW_TOTAL_COUNT][1].ToString()) + Good + NG;
            int TotalPart = int.Parse(TotalProduct.Rows[ROW_TOTAL_COUNT][1].ToString());
            int TotalOKPart = int.Parse(TotalProduct.Rows[ROW_TOTAL_OK][1].ToString());
            TotalProduct.Rows[ROW_TOTAL_YIELD][1] = ((double)TotalOKPart / TotalPart).ToString("0.00%");
            CalcYield(product[DAYSHIFT], product[NIGHTSHIFT]);
        }
    }
    public static void GetProductInfo(DateTime GetTime, int IncomeNG)
    {
        lock (GetProductInfoLock)
        {
            int index, row;
            GetShift(GetTime, out index, out row);
            product[index].Rows[row][COL_PROD_COUNT] = int.Parse(product[index].Rows[row][COL_PROD_COUNT].ToString()) + IncomeNG;
            product[index].Rows[row][COL_PROD_IN_NG_COUNT] = int.Parse(product[index].Rows[row][COL_PROD_IN_NG_COUNT].ToString()) + IncomeNG;
            product[index].Rows[prodStatRow][COL_PROD_COUNT] = int.Parse(product[index].Rows[prodStatRow][COL_PROD_COUNT].ToString()) + IncomeNG;
            product[index].Rows[prodStatRow][COL_PROD_IN_NG_COUNT] = int.Parse(product[index].Rows[prodStatRow][COL_PROD_IN_NG_COUNT].ToString()) + IncomeNG;
            TotalProduct.Rows[ROW_TOTAL_INCOME_NG][1] = int.Parse(TotalProduct.Rows[ROW_TOTAL_INCOME_NG][1].ToString()) + IncomeNG;
            TotalProduct.Rows[ROW_TOTAL_COUNT][1] = int.Parse(TotalProduct.Rows[ROW_TOTAL_COUNT][1].ToString()) + IncomeNG;
            int TotalPart = int.Parse(TotalProduct.Rows[ROW_TOTAL_COUNT][1].ToString());
            int TotalOKPart = int.Parse(TotalProduct.Rows[ROW_TOTAL_OK][1].ToString());
            TotalProduct.Rows[ROW_TOTAL_YIELD][1] = ((double)TotalOKPart / TotalPart).ToString("0.00%");
            CalcYield(product[DAYSHIFT], product[NIGHTSHIFT]);
        }
    }
    public static void WorkFlowTimerStart(bool start)
    {
        if (start)
        {
            if (!timer.IsCounting)
                timer.StartTimer(60000, 60000);
        }
        else
        {
            timer.StopTimer();
            Save();
        }
    }
    private static void TimerTick(object sender, EventArgs e)
    {
        int index, row;
        GetShift(DateTime.Now, out index, out row);
        string col;
        switch (WFStatus)
        {
            case WorkFlowStatus.报警:
                col = COL_PROD_DOWN_SPAN;
                break;
            case WorkFlowStatus.待料:
                col = COL_PROD_IDLE_SPAN;
                break;
            case WorkFlowStatus.运行:
                col = COL_PROD_WORK_SPAN;
                break;
            default:
                return;
        }
        product[index].Rows[row][col] = int.Parse(product[index].Rows[row][col].ToString()) + 1;
        product[index].Rows[prodStatRow][col] = int.Parse(product[index].Rows[prodStatRow][col].ToString()) + 1;
        CalcDT(product[DAYSHIFT], product[NIGHTSHIFT]);
        Save();
    }
    private static void GetErrInfo(DateTime GetTime, ErrorLevel level, string source, string message)
    {
        int index, row;
        GetShift(GetTime, out index, out row);
        DataRow almrow = alm.NewRow();
        almrow[COL_ALARM_LEVEL] = ((ErrorLevelName)(int)level).ToString();
        almrow[COL_ALARM_MESSAGE] = message;
        almrow[COL_ALARM_SOURCE] = source;
        almrow[COL_ALARM_TIME] = GetTime.ToLongTimeString();
        alm.Rows.Add(almrow);
    }
    private static object GetWorkFlowLock = new object();
    public static void GetWorkFlowStatus(DateTime GetTime, WorkFlowStatus status, string message = "", bool ForceRecord = false)
    {
        lock (GetWorkFlowLock)
        {
            if (status == WorkFlowStatus.报警结束)
            {
                if (_wfStatus == WorkFlowStatus.报警)
                    _wfStatus = IsWorkFlowRunning ? WorkFlowStatus.待料 : WorkFlowStatus.空闲;
            }
            else if (_wfStatus != WorkFlowStatus.报警)
                _wfStatus = status;
            if (_lastStatus != _wfStatus || ForceRecord)
            {
                //int index, row;
                //GetShift(GetTime, out index, out row);
                DataRow workrow = workflow.NewRow();
                workrow[COL_WORKFLOW_TIME] = GetTime.ToLongTimeString();
                workrow[COL_WORKFLOW_STATUS] = _lastStatus + "->" + _wfStatus;
                workrow[COL_WORKFLOW_MEMO] = message;
                workflow.Rows.Add(workrow);
                _lastStatus = _wfStatus;
                if (updateState != null) updateState.BeginInvoke(_wfStatus, null, null, null);
            }
        }
    }
    private static object alarmlock = new object();
    public static void ShowErrorHandler(string Source, string ErrorMessage, ErrorLevel level, LogFile log, bool retry, bool pause, ErrorReturnHandler HandleFunc)
    {
        lock (alarmlock)
        {
            if (WFStatus == WorkFlowStatus.空闲) return;
            bool NeedRecord = false;
            switch (level)
            {
                case ErrorLevel.Alarm:
                    if (retry && HandleFunc != null)
                    {
                        //WFStatus = WorkFlowStatus.报警停机;
                        GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.报警, ErrorMessage, true);
                        NeedRecord = true;
                    }
                    if (pause) NeedRecord = true;
                    break;
                case ErrorLevel.Error:
                case ErrorLevel.Fatal:
                    //WFStatus = WorkFlowStatus.报警停机;
                    GetWorkFlowStatus(DateTime.Now, WorkFlowStatus.报警, ErrorMessage, true);
                    NeedRecord = true;
                    break;
            }
            if (NeedRecord)
                GetErrInfo(DateTime.Now, level, Source, ErrorMessage);
        }
    }
}