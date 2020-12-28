using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Score
{
    public abstract class DataWriter
    {
        protected String filePath;
        protected ExcelUtil excelUtil;
        protected void gridToPos(string grid, out int col, out int row)
        {
            col = grid[0] - 'A' + 1;
            row = Int32.Parse(grid.Substring(1));
        }
        public void setSheetIndex(int sheetIndex)
        {
            excelUtil.setSheet(sheetIndex);
        }
        public void save()
        {
            excelUtil.save();
        }
        public void close()
        {
            excelUtil.close();
        }
        public void setRangeColumnWidth(string range, int width)
        {
            excelUtil.setColumnWidth(range, width);
        }
        public void writeValue(String pos, int cellSpan, string data)
        {
            int startCol, startrRow;
            gridToPos(pos, out startCol, out startrRow);
            excelUtil.write(startrRow, startCol, data);
        }
        public void clone(int p)
        {
            excelUtil.clone(p);
        }

        public void delete(int n, string status) {
            excelUtil.delete(n, status);
        }

        public void reNameSheet(string p)
        {
            excelUtil.reNameSheet(p);
        }
    }
}
