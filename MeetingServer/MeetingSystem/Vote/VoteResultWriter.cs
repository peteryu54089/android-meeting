using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Score;

namespace MeetingSystem.Vote
{
    public class VoteResultWriter:DataWriter
    {
        public VoteResultWriter(String path)
        {
            filePath = path;
            excelUtil = new ExcelUtil(path, false, 1);
        }
        public VoteResultWriter(String path,bool isVisible)
        {
            filePath = path;
            excelUtil = new ExcelUtil(path, isVisible, 1);
        }
        
        public void writeVote(string voteStartValue, int offset, List<VoteDataItem.VoteData> list)
        {
            int startCol, startrRow;
            gridToPos(voteStartValue, out startCol, out startrRow);
            int i = startrRow;
            foreach (VoteDataItem.VoteData data in list)
            {
                string name=data.Name;
                int j = startCol ;
                excelUtil.write(i, j++, name);
                if (!data.InvalidateChoose)
                {
                    j += (data.RejectChoose) ? 1 : 0;
                    excelUtil.write(i, j, "✓");
                }
                else
                {
                    j += 2;
                    excelUtil.write(i, j, "此為廢票");
                }
                i++;
            }
        }
        public void readPassPeople(string passStart, int num, List<String> returnList)
        {
            int startCol, startrRow;
            gridToPos(passStart, out startCol, out startrRow);
            for (int i = 0; i < num; i++)
            {
                if(excelUtil.read(startrRow+i,startCol).Equals("通過"))
                    returnList.Add(excelUtil.read(startrRow + i, 1) + ":" + excelUtil.read(startrRow + i, 2));
            } 
        }

        public void writeCandidateRank(string startValue, string teachers, string group, string title)
        {
            int startCol, startrRow;
            excelUtil.write(1, 1, title);
            gridToPos(startValue, out startCol, out startrRow);
            int len = startrRow;
            foreach (string name in teachers.Split(','))
            {
                excelUtil.write(len, startCol, group);
                excelUtil.write(len, startCol + 1, name);
                len++;
            }
        }
        
        // 讀同意票的標題
        public string readTitle() {
            string title = excelUtil.read(1,1);
            return title;
        }

        public void writeTitle(string titleName, string time) {
            excelUtil.write(1, 1, titleName);
            excelUtil.write(1, 6, time); // row:1 col:f
            excelUtil.fontSize(1, 6, 10);
        }
        // 寫入投票人數
        public void writeVoteAndVotedNumber(string votedNum, string voteNum, string passThershold) {
            excelUtil.write(26, 4, voteNum);
            excelUtil.write(26, 7, passThershold);
        }

        public void writeVoteTotal(string startValue, Dictionary<string, int[]> calcData, List<String> passOrNot)
        {
            const int AGREE = 0;
            const int REJECT = 1;
            //const int INVAIL = 2; //原本為廢票欄位，改為不使用
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int row = startrRow;
            int row2 = startrRow;

            foreach (KeyValuePair<string, int[]> pair in calcData)
            {
                excelUtil.write(row, startCol, pair.Key.Split(':')[0]);
                excelUtil.write(row, startCol + 1, pair.Key.Split(':')[1]);
                excelUtil.write(row, startCol + 2, pair.Value[AGREE].ToString());
                excelUtil.write(row, startCol + 3, pair.Value[REJECT].ToString());
                //excelUtil.write(row, startCol + 3, pair.Value[INVAIL].ToString());
                row++;
            }

            foreach (string myList in passOrNot) {
                excelUtil.write(row2, startCol + 4, myList);
                row2++;
            }
        }

        public void writeRankStatisticsTitle(string startValue, int offset, List<VoteDataItem.VoteData> list)
        {
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int col = startCol;
            excelUtil.write(startrRow, col++, "委員編號");
            foreach (VoteDataItem.VoteData data in list)
            {
                string name = data.Name;
                excelUtil.write(startrRow, col++, name);
            }
        }
        private bool isAgree(Object obj)
        {
            VoteDataItem.VoteData voteData = (VoteDataItem.VoteData)obj;
            return voteData.AgreeChoose == true;
        }

        private bool isReject(Object obj)
        {
            VoteDataItem.VoteData voteData = (VoteDataItem.VoteData)obj;
            return voteData.RejectChoose == true;
        }

        private bool isInvalid(Object obj)
        {
            VoteDataItem.VoteData voteData = (VoteDataItem.VoteData)obj;
            return voteData.InvalidateChoose == true;
        }
        public void writeRankStatistics(string startValue, int offset, string ip, List<VoteDataItem.VoteData> list)
        {
            int startCol, startrRow;
            gridToPos(startValue, out startCol, out startrRow);
            int col = startCol;

            excelUtil.write(startrRow, startCol, ip);
            col++;
            foreach (VoteDataItem.VoteData data in list)
            {
                string name = data.Name;
                if (excelUtil.read(1, col).Equals(name)) 
                    writeChoose(startrRow, col, data);
                else
                {
                    for (int i = 1; i <= list.Count; i++)
                        if (excelUtil.read(1, i).Equals(name))
                            writeChoose(startrRow, col, data); 
                } 
                col++; 
            }
        } 
        private void writeChoose(int startrRow, int col, VoteDataItem.VoteData data)
        {
            if (isAgree(data) == true)
                excelUtil.write(startrRow, col, "同意");
            else if (isReject(data) == true)
                excelUtil.write(startrRow, col, "不同意");
            else if (isInvalid(data) == true)
                excelUtil.write(startrRow, col, "棄權");
        }
    }
}
