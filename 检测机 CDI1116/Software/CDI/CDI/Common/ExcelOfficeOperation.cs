using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace MSOffice
{
    public class ExcelOfficeOperation
    {
        private Excel.Application m_Application;
        private Excel.Workbook m_WorkBook;

        private string _ModelFile;
        private string m_strFilePath;
        private Excel.Sheets m_excelSheets;
        private Excel.Worksheet m_WorkSheet;
        private bool _ShowExcel = false;
        private object missing = System.Reflection.Missing.Value;

        public ExcelOfficeOperation(string ModelFile, string strFilePath, bool ShowExcel = false)
        {
            bool bExist = File.Exists(ModelFile);
            //if (!bExist)
            //{
            //    return;
            //}
            m_Application = new Excel.Application();
            _ShowExcel = ShowExcel;
            m_Application.Visible = _ShowExcel;
            m_Application.DisplayAlerts = false;
            _ModelFile = ModelFile;
            m_strFilePath = strFilePath;
            if (_ModelFile == "")
                m_WorkBook = m_Application.Workbooks.Add(true);
            else
                m_WorkBook = m_Application.Workbooks.Open(_ModelFile, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing);
            //m_WorkBook = m_Application.Workbooks.Open(_ModelFile, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            m_excelSheets = m_WorkBook.Sheets;
        }

        public void Quit()
        {
            m_WorkBook.Close(false);
            m_Application.Quit();
            m_excelSheets = null;
            m_WorkSheet = null;
            m_WorkBook = null;
            m_Application = null;
        }

        public void BuildGRRSheet(string[] SheetNames)
        {
            //((Excel.Worksheet)m_excelSheets[3]).Delete();
            //((Excel.Worksheet)m_excelSheets[2]).Delete();
            for (int i = 1; i < SheetNames.Length; i++)
                ((Excel.Worksheet)m_excelSheets[1]).Copy(missing, m_excelSheets[1]);
            for (int i = 0; i < SheetNames.Length; i++)
                ((Excel.Worksheet)m_excelSheets[i + 1]).Name = SheetNames[i];
        }
        public void SheetRename(string oldsheetname, string newsheetname)
        {
            ((Excel.Worksheet)m_excelSheets[oldsheetname]).Name = newsheetname;
        }
        public void SheetRename(int sheetindex, string newsheetname)
        {
            ((Excel.Worksheet)m_excelSheets[sheetindex]).Name = newsheetname;
        }

        /// <summary>
        /// 设置单元格内容，单元格行、列索引均从1开始
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="strText"></param>
        /// <param name="strSheetName"></param>
        public void SetCellText(int iRow, int iColumn, string strText, string strSheetName)
        {
            m_WorkSheet = (Excel.Worksheet)m_excelSheets[strSheetName];
            m_WorkSheet.Cells[iRow, iColumn] = strText;
        }
        public void SetCellText(int iRow, int iColumn, string strText, int SheetIndex)
        {
            m_WorkSheet = (Excel.Worksheet)m_excelSheets[SheetIndex];
            m_WorkSheet.Cells[iRow, iColumn] = strText;
        }

        public void SaveAll()
        {
            m_WorkBook.SaveAs(m_strFilePath, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
        }
        /// <summary>
        /// 读取单元格内容，单元格行、列索引均从1开始
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="strSheetName"></param>
        /// <returns></returns>
        public string GetCellText(int iRow, int iColumn, string strSheetName)
        {
            m_WorkSheet = (Excel.Worksheet)m_excelSheets.get_Item(strSheetName);
            string strText = (string)((Excel.Range)m_WorkSheet.Cells[iRow, iColumn]).Text;
            return strText;
        }
        public string GetCellText(int iRow, int iColumn, int SheetIndex)
        {
            m_WorkSheet = (Excel.Worksheet)m_excelSheets.get_Item(SheetIndex);
            string strText = (string)((Excel.Range)m_WorkSheet.Cells[iRow, iColumn]).Text;
            return strText;
        }
    }
}