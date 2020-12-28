using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Xls;

namespace MeetingSystem.Utils
{
    class Xls2Html
    {
        public static void xlsToHtml(string xlsPath, string webPath)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(xlsPath);
            //convert Excel to HTML
            Worksheet sheet = workbook.Worksheets[0];
            foreach (CellRange row in sheet.Rows)
            {
                foreach (CellRange cell in row.Cells)
                {
                    if (cell.Cells[0].FormulaValue != null)
                    {
                        String text = cell.Cells[0].FormulaValue.ToString();
                        if (text != "#VALUE!")
                            cell.Cells[0].Text = text;
                        else
                            cell.Cells[0].Text = "";
                    }
                }
            }
            sheet.SaveToHtml(webPath);
        }
    }
}
