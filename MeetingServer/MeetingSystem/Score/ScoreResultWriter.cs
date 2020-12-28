using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Structure;
using MeetingSystem.Score;
using System.Collections;

namespace MeetingSystem
{
    class ScoreResultWriter : DataWriter
    {

        public ScoreResultWriter(string path)
        {
            filePath = path;
            excelUtil = new ExcelUtil(path, false, 1); 
        }
        public ScoreResultWriter(string path,bool isVisible)
        {
            filePath = path;
            excelUtil = new ExcelUtil(path, isVisible, 1);
        }
   
        public void writeScore(String startPos, String endPos, int cellSpan, Array objList)
        {
            int startCol, startrRow;
            int endCol, endRow; 
            gridToPos(startPos, out startCol, out startrRow);
            gridToPos(endPos, out endCol, out endRow);
            int len=0;
            for (int i = startrRow; i <= endRow; i += cellSpan)
            {
                for (int j = startCol; j <= endCol; j += cellSpan)
                {
                    excelUtil.write(i, j,  objList.GetValue(len).ToString());
                    len++;
                }
            }
        }

        // read the 升等評分 file
        public string getTeacherInfo(string n) {
            
            int startCol, startrRow;
            gridToPos(n, out startCol, out startrRow); // A2 A:col 2:row
            
            string readName = excelUtil.read(startrRow, startCol); // A2
            string readDepartment = excelUtil.read(startrRow, startCol+2); // C2
            string readLevel = excelUtil.read(startrRow, startCol+7);// H2

            int nameStartIndex = readName.IndexOf('：') + 1;
            int departmentStartIndex = readDepartment.IndexOf('：') + 1;
            int levelStartIndex = readLevel.IndexOf('：') + 1;

            string name = readName.Substring(nameStartIndex).Trim();
            string department = readDepartment.Substring(departmentStartIndex).Trim();
            string promotionLevel = readLevel.Substring(levelStartIndex).Trim();
            string level = "";
            if (string.Compare(promotionLevel, "教授") == 0)
                level = "副教授";
            if (string.Compare(promotionLevel, "副教授") == 0)
                level = "助理教授";
            string teacherInfo = department + "     " + level + "     " + name + "     " + "升等" + promotionLevel + "評分統計表";
            return teacherInfo;
        }

        public string getRangeScore(string start, string end)
        {

            int startCol, startRow;
            int endCol, endRow;
            string s="";
            gridToPos(start, out startCol, out startRow); // I9 I:col 9:row
            gridToPos(end, out endCol, out endRow); // I13 I:col 13:row

            for(int i = startRow; i<=endRow; i+=2){
                for (int j = startCol ; j<=endCol ;j+=2) {
                    string score = excelUtil.read(i, j);
                    int startIndex = score.IndexOf('~');

                    string a = score.Substring(0, startIndex);
                    string b = score.Substring(startIndex+1);
                    s += a + "," + b + ",";
                }
            }
            return s;
        }

        public string getAverageScore(string pos) {
            int startCol, startRow; // H6
            gridToPos(pos, out startCol, out startRow); // H6 H:col 6:row
            string s = excelUtil.read(startRow, startCol);
            EventLog.Write(s);
            return s;
        }

        public void printTeacherInfo(string pos, string info, string date) {
           
            int startCol, startrRow;
            gridToPos(pos, out startCol, out startrRow); // A1 A:col 1:row
            string title = info;
     
            excelUtil.write(startrRow, startCol, title);
            excelUtil.write(startrRow, 6, date);
            excelUtil.font(startrRow, startCol);
            excelUtil.fontSize(startrRow, 6, 10);

        }

        public void printRangeScore(string start, string end, string score) {

            int startCol, startRow; // G6 
            int endCol, endRow;  // H8

            gridToPos(start, out startCol, out startRow); // G6  G:col 6:row
            gridToPos(end, out endCol, out endRow); // H8 H:col 9:row

            string[] s = score.Split(',');
            int len = 0;
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startCol; j <= endCol; j++)
                {
                    excelUtil.write(i, j, s[len]);
                    len++;
                }
            }     
        }

        public void writeRearrangement(string indexValue, string endValue, object collection)
        {
           
            int startCol, startRow; // A5 
            int endCol, endRow;  // F5
            string s = collection.ToString();
            string[] sa = s.Split(',');
            int len = 0;
            double finalScore = Convert.ToDouble(sa[5]);

            gridToPos(indexValue, out startCol, out startRow); // A5  A:col 5:row
            gridToPos(endValue, out endCol, out endRow); // F5 F:col 5:row
         
            for (int i = startRow; i <= endRow; i++) {
                for (int j = startCol; j <= endCol; j++) {
                    excelUtil.write(i, j, sa[len]);
                    len++;
                }
            }
            excelUtil.fontColor(startRow, endCol, finalScore);
        }

        //How many people have voted
        public void writeIpCount(String ipCount) {
            excelUtil.write(36, 3, ipCount);
        }

        public void writeReasons(string reasonStartValue, string reasonEndValue, int cellSpan, string[] reasons)
        {
            if (reasons.Length == 0)
                return;
            int startCol, startrRow;
            int endCol, endRow;
            gridToPos(reasonStartValue, out startCol, out startrRow);
            gridToPos(reasonEndValue, out endCol, out endRow);

            for (int i = startrRow; i <= endRow; i++)
            {
                for (int j = startCol; j <= endCol; j++)
                {
                    String cellStr =excelUtil.read(i,j);
                    if (cellStr.Length > 0)
                    {
                        String reason = cellStr.Substring(cellStr.IndexOf("□") + 1).Replace("___________________", "");
                        cellStr = cellStr.Replace("□", "☑").Replace("___________________", "");
                        if (reasons.Contains(reason))
                            excelUtil.write(i,j,cellStr);
                        else if (reasons[reasons.Length - 1].Contains(reason))
                        {
                            excelUtil.write(i, j, reasons[reasons.Length - 1].Replace("其他：（請敘明具體事實）", cellStr));
                        } 
                    }
                }
            } 
        }
    }
}
