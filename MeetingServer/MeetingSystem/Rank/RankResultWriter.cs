using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Score;
using MeetingSystem.Structure;

namespace MeetingSystem.Rank
{
    public class RankResultWriter : DataWriter
    {
        public RankResultWriter(string filePath)
        {
            this.filePath = filePath;
            excelUtil = new ExcelUtil(filePath, false, 1);
        }
        public RankResultWriter(string filePath,bool isVisible)
        {
            this.filePath = filePath;
            excelUtil = new ExcelUtil(filePath, isVisible, 1);
        }
        public void writeRankTicketWriter(string rankStartValue, int offset,List<Structure.RankDataItem.RankData> list)
        {
            int startCol, startrRow;
            gridToPos(rankStartValue, out startCol, out startrRow);
            int row = startrRow;
            foreach (RankDataItem.RankData data in list)
            {
                string name = data.Name;
                excelUtil.write(row, startCol, name.Remove(name.LastIndexOf(":")));
                excelUtil.write(row, startCol + 1, name.Substring(name.LastIndexOf(":") + 1));
                excelUtil.write(row, startCol + 2, data.Rank);
                row++;
            }

        }
        public void writeRankResult(string startValue, Dictionary<string, int> returnTable)
        {
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int row=startrRow;
            foreach (KeyValuePair<string, int> pair in returnTable)
            {
                // Console.WriteLine(pair.Value);
                string name = pair.Key;
                excelUtil.write(row, startCol, name.Remove(name.LastIndexOf(":")));
                excelUtil.write(row, startCol + 1, name.Substring(name.LastIndexOf(":") + 1));
                excelUtil.write(row++, startCol + 2, pair.Value.ToString());
            }

        }

        internal void writeRankStatistics(string startValue, int offset, string ip, List<RankDataItem.RankData> list)
        {
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int col = startCol;

            excelUtil.write(startrRow, startCol, ip); 
            col++;
            foreach (RankDataItem.RankData data in list)
            {
                string name = data.Name;
                if (excelUtil.read(1, col).Equals(name))
                    excelUtil.write(startrRow, col, data.Rank);
                else
                {
                    for(int i=1;i<=list.Count;i++)
                        if (excelUtil.read(1, i).Equals(name))
                            excelUtil.write(startrRow, i, data.Rank);
                }
               
                col++;
            }


        }

        public void writeRankStatisticsTitle(string startValue, int p,  List<RankDataItem.RankData> list)
        {
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int col = startCol;
            excelUtil.write(startrRow, col++, "委員編號");
            foreach (RankDataItem.RankData data in list)
            {
                string name = data.Name;
                excelUtil.write(startrRow, col++, name);
            }
        }
    }
}
