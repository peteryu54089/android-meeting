using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace MeetingSystem
{
    public class ExcelUtil
    {
        Microsoft.Office.Interop.Excel.Application _Excel = null;
        //引用活頁簿類別 
        _Workbook book = null;
        //引用工作表類別
        _Worksheet sheetWork = null;
        //引用Range類別 
        Range range = null;
        bool isOpenView;
        public ExcelUtil(String filePath, bool isOpenView, int sheetIndex)
        {
            this.isOpenView = isOpenView;
            _Excel = new Microsoft.Office.Interop.Excel.Application();
            book = _Excel.Workbooks.Open(filePath);
            _Excel.Visible = isOpenView;
            sheetWork = book.Sheets[sheetIndex];
        }
        public void setSheet(int sheetIndex)
        {
            // test it
            if (sheetIndex > book.Sheets.Count)
            {
                book.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                sheetWork = book.Sheets[1];//產生在最前面
                sheetWork.Move(After: book.Sheets[book.Sheets.Count]);//移到最後面

            }
            sheetWork = book.Sheets[sheetIndex];
        }
        public void write(int row, int col, string data)
        {
            sheetWork.Cells[row, col] = data;
        }
        public string read(int row, int col)
        {
            return ((Range)sheetWork.Cells[row, col]).Text;
        }

        public void fontSize(int row, int col, int size) {
            sheetWork.Cells[row, col].Font.Size = size;
        }

        public void font(int row, int col)
        {
            sheetWork.Cells[row, col].Font.Name = "標楷體";
        }

        public void fontColor(int row, int col, double score) {
            if (score>=75)
                sheetWork.Cells[row, col].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            else
                sheetWork.Cells[row, col].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
        }

        public void save()
        {
            book.Save();
        }
        public void close()
        {
            if (!isOpenView)
            {
                book.Close();
                _Excel.Quit();
                releaseObject(sheetWork);
                releaseObject(book);
                releaseObject(_Excel);
            }
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        internal void setColumnWidth(string range, int width)
        {
            sheetWork.get_Range(range).EntireColumn.ColumnWidth = width;
        }

        internal void clone(int p)
        {
            for (int i = 1; i < p; i++)
            {
                sheetWork.Copy(book.Sheets[1]);
               
            }
        }

        internal void delete(int n, string status) {

            int startRow =0;  
            int endRow=0; 
            if (status == "scoreType") {
                startRow = 5 + n;
                endRow = 34;
            }
            else if (status == "voteType") {
                startRow = 3 + n;
                endRow = 22;
            }
            for (int i = startRow; i <= endRow; i++) {
                ((Range)sheetWork.Rows[startRow]).Delete();    
            }
        }

        internal void deleteVote(int n) {
            int startRow = 3 + n;
            int endRow = 22;
            for (int i = startRow; i <= endRow; i++) {
                ((Range)sheetWork.Rows[startRow]).Delete();    
            }
        }

        internal void reNameSheet(string p)
        {
            sheetWork.Name = p;
        }
    }
}
