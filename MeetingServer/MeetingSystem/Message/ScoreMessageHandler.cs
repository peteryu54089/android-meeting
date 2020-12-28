using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.IO;
using MeetingSystem.Utils;
using System.ComponentModel;
using MeetingSystem.Structure;
using System.Collections;
using System.Globalization;

namespace MeetingSystem.Message
{
    class ScoreMessageHandler : Message
    {
        public enum ScoreMessageMode { ScoreRequest, ScoreResponse };
        public string teacherInfo, score, average;
        public ArrayList collection = new ArrayList(); // save the ip and score which rearrangement
        private Hashtable scoreDataItems = null;
        private BindingList<ScoreDataPair> scoreDataPairs = new BindingList<ScoreDataPair>();
        Random random = new Random();

        private ScoreMessageHandler()
        {
            parm = new BindingList<ScoreSettingDataItem>(); 
            SettingDataItems.ListChanged += new ListChangedEventHandler(ScoreSettingItems_ListChanged);
           
        }
        void ScoreSettingItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                scoreSettingItem_Rmove(e.NewIndex);
                return;
            }
            scoreSettingItem_Add();
        }

        private void scoreSettingItem_Add()
        {
            foreach (ScoreSettingDataItem settingDataItem in SettingDataItems)
            {
                ScoreDataPair targetDataPair = scoreDataPairs.FirstOrDefault(pair => pair.SettingDataItem == settingDataItem);
                bool isExist = (targetDataPair != null);
                if (!isExist) //不存在所以新增
                {
                    ScoreDataPair dataPair = new ScoreDataPair(settingDataItem);
                    scoreDataPairs.Add(dataPair);
                }
            }
        }

        private void scoreSettingItem_Rmove(int affectIndex)
        {
            if (affectIndex < scoreDataPairs.Count)
            {
                ScoreSettingDataItem scoreSettingDataItem = scoreDataPairs[affectIndex].SettingDataItem;
                if (!SettingDataItems.Contains(scoreSettingDataItem))
                    scoreDataPairs.RemoveAt(affectIndex);

            }
        }

        public static void init()
        {
            MessagePool.registerMessage(ClientMessage.ScoreMessage, new ScoreMessageHandler());
        }
        public void calcVoteResult(int id)
        {
            lock (scoreDataPairs)
            {
                try
                {
                    ScoreDataPair dataPair = scoreDataPairs.FirstOrDefault(pair => pair.SettingDataItem.ScoreId == id);
                    if (dataPair.DataItems.Count <= 0)
                        return;
                    DateTime time_start = DateTime.Now;
                    saveTeacherInfo(dataPair);
                    saveScoreBallot(dataPair);//存每一張票
                    DateTime time_saveBallot = DateTime.Now;
                    saveScoreResult(dataPair);//存投票結果
                    DateTime time_saveResult = DateTime.Now;
                    EventLog.Write("存每一張票的時間 " + ((TimeSpan)(time_saveBallot - time_start)).TotalMilliseconds.ToString() + " 存投票結果時間 " + ((TimeSpan)(time_saveResult - time_saveBallot)).TotalMilliseconds.ToString());
                    
                  //  setToCompleteDataItem(dataPair); 
                }
                catch (Exception e)
                {
                    NotifyStatus(e.Message);
                    Console.WriteLine(e.Message);
                    EventLog.Write(e.Message);
                }
            }
        }

        public string date() {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
            string today = string.Format("{0}.{1}.{2}",
                                taiwanCalendar.GetYear(DateTime.Now),
                                DateTime.Now.Month,
                                DateTime.Now.Day);
            return today;
        }

        // save teacher info
        private void saveTeacherInfo(ScoreDataPair dataPair) {

            const string name = "A2";
            const string averageScore = "H6";
            const string startScore = "I9";
            const string endScore = "I13";
      
            String path = dataPair.SettingDataItem.ScoreSourcePath; //rank file
            ScoreResultWriter reader = new ScoreResultWriter(path); //read teacher info 
           
            teacherInfo = reader.getTeacherInfo(name);
            score = reader.getRangeScore(startScore, endScore);
            average = reader.getAverageScore(averageScore);

            reader.close();
        } 

        private void saveScoreResult(ScoreDataPair dataPair)
        {
            const string startValue = "C5";
            //const string endValue = "E5";
            const string endValue = "F5";
            const string indexValue = "A5";
            const string resultPos = "A1";
            const string startPrintScore = "G6";
            const string endPrintScore = "H8";
            const string averageScore = "B5";

            const int ipIndex = 0;
            const int scoreListIndex = 1;
            int startRow = Convert.ToInt32(startValue.Substring(1));
            int startIndex = 5;
            int people  = dataPair.DataItems.Count;

            string ipCount = Convert.ToString(dataPair.DataItems.Count);
            String path = dataPair.SettingDataItem.ScoreResultPath;


            Hashtable calResult = new Hashtable();
            ScoreResultWriter writer = new ScoreResultWriter(path);
            for (int i = 0; i < dataPair.DataItems.Count; i++)
            {
                String fullIp = ((System.Net.IPAddress)(dataPair.DataItems[i][ipIndex])).ToString();//dataItem
                string ip =fullIp.Substring(fullIp.LastIndexOf('.') + 1);
                ScoreDataItem item = (ScoreDataItem)(dataPair.DataItems[i][scoreListIndex]);//分數list
               // writer.writeValue(indexValue[0] + startRow.ToString(), 1, ip.Substring(ip.LastIndexOf('.') + 1));
                //writer.writeScore(startValue[0]+startRow.ToString(), endValue[0]+startRow.ToString(), 1, item.Scores);//將分數寫入
               // writer.writeValue(averageScore[0] + startRow.ToString(), 1, average);
                combineDtaToBeAList(ip, average, item.Scores);
                startRow++;
                //calcReasonResult(item.Reasons, Convert.ToInt32(ip.Substring(ip.LastIndexOf('.') + 1)), calResult);//統計未通過的理由數量
            }

            //重新排列
           for (int i = 0; i < collection.Count; i++) { 
                int j = random.Next(0, people);
                object x = collection[i];
                collection[i] = collection[j];
                collection[j] = x;         
            }

            //排列後寫入
            for (int i = 0; i < dataPair.DataItems.Count; i++) {
                writer.writeRearrangement(indexValue[0] + startIndex.ToString(), endValue[0] + startIndex.ToString(), collection[i]);
                startIndex++;
            }

            // Count how many people have voted.
            EventLog.Write(ipCount + "  ip has voted " + path);
            writer.writeIpCount(ipCount);
            writer.printTeacherInfo(resultPos, teacherInfo, date());
            writer.printRangeScore(startPrintScore, endPrintScore, score);
            writer.delete(people, "scoreType");
            collection.Clear();


            //將未通過的理由數量寫入投票結excel中的下一頁
            if(calResult.Count!=0)
                writeResultForResult(calResult, writer);
            writer.save();
            writer.close();
        }

        public void combineDtaToBeAList(string ip, string average, float[] scores)
        {
            float sum = 0;
            float[] userscore ={0,0,0,0,0,0};
            string[] ss = score.Split(',');

            for (int i = 0; i < 6; i++) {
                userscore[i]=float.Parse(ss[i]);
            }

            if (scores[0] < userscore[0] || scores[0] > userscore[1] || scores[1] < userscore[2] || scores[1] > userscore[3] || scores[2] < userscore[4] || scores[2] > userscore[5])
            {
                EventLog.Write("score Fail");
                sum = 0;
            }
       
            else
            {
                sum = float.Parse(average) + scores[0] + scores[1] + scores[2];
            }

            string s = ip+","+average+","+scores[0]+","+scores[1]+","+scores[2]+","+sum.ToString();
            collection.Add(s);
        }

        private static void writeResultForResult(Hashtable calResult, ScoreResultWriter writer)
        {
            writer.setSheetIndex(2);
            String range = "A:A";
            writer.setRangeColumnWidth(range, 80);
            range = "B:B";
            writer.setRangeColumnWidth(range, 20);
            int index = 1;
            foreach (object key in calResult.Keys)
            {
                List<String> str = new List<String>();
                List<int> reasonId = (List<int>)calResult[key];
                int count = reasonId.Count;
                String reason = (String)key;
                str.Add(reasonsIdToString(reasonId));
                str.Add(reason);
                str.Add(count.ToString());
                writer.writeScore("A" + index.ToString(), "C" + index.ToString(), 1, str.ToArray());
                index++;
            }
        }
        private void saveScoreBallot(ScoreDataPair dataPair)//存每張票
        {
            const string scoreStartValue = "I9";
            const string scoreEndValue = "I13";
            const string reasonStartValue = "A17";
            const string reasonEndValue = "G20";
            const int ipIndex = 0;
            const int scoreListIndex = 1;//分數資料
            String path = dataPair.SettingDataItem.ScoreSourcePath;
            String dir = path.Remove(path.LastIndexOf("."));
            String ext = path.Substring(path.LastIndexOf("."));
            String newTable = dir + "_選票檔" + ext;
            File.Copy(path, newTable, true);
            ScoreResultWriter writer = new ScoreResultWriter(newTable);
            writer.clone(dataPair.DataItems.Count);
            for (int i = 1; i <= dataPair.DataItems.Count; i++)
            {
                String ip =((System.Net.IPAddress)(dataPair.DataItems[i-1][ipIndex])).ToString();//dataItem
                ScoreDataItem item = (ScoreDataItem)(dataPair.DataItems[i-1][scoreListIndex]);//dataItem
                writer.setSheetIndex(i);
                writer.reNameSheet(ip.Substring(ip.LastIndexOf('.') + 1)); 
                writer.writeScore(scoreStartValue, scoreEndValue, 2, item.Scores);
                writer.writeReasons(reasonStartValue, reasonEndValue, 1, item.Reasons);
            }
            writer.save();
            writer.close();
            EventLog.Write(newTable+ "選票檔完成"); 
        }
        private void calcReasonResult(string[] reasons, int id, Hashtable calcResult)
        {
            foreach (String reason in reasons)
            {
                if (calcResult[reason] == null)
                {
                    calcResult[reason] = new List<int>();
                }
                ((List<int>)calcResult[reason]).Add(id);
                //calcResult[reason] = calcResult[reason] == null ? 1 : (int)calcResult[reason] + 1;
            }
        }
        private static String reasonsIdToString(List<int> reasonId)
        {
            String ids = "";
            foreach (int id in reasonId)
            {
                ids += "  " + id;
            }
            return ids;
        }
        public void setToCompleteDataItem(int id)
        {
            ScoreDataPair dataPair = scoreDataPairs.FirstOrDefault(pair => pair.SettingDataItem.ScoreId == id);
            dataPair.SettingDataItem.Status = ScoreSettingDataItem.ScoreSettingStatus.COMPLETE;
        }

        public override void sendMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            try
            {
                String jsonStr = JsonTool.getInstance().Serialize(obj);
                String msg = "cmd:0" + "msg:" + jsonStr + "\n";
                //writer.Write((int)ClientMessage.ScoreMessage + jsonStr + "\n");
                writer.Write(msg);
                //Console.WriteLine(msg);
                //writer.Write(jsonStr);
            } catch(System.IO.IOException e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        } 
        public override void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            if (obj == null) //送出該使用者尚未進行評分的列表
                sendMessage(client, writer, getNonScoreSettingItem(client));
            else
            {   //儲存使用者的評分結果
                if (obj.GetType() == typeof(ScoreDataItem))
                    saveDataItem(client, (ScoreDataItem)obj);
            }
        }

        public override Object receivedMessage(MeetingClient client, BinaryReader reader)
        {
            int cmdVal = reader.ReadInt32();
            if (Enum.IsDefined(typeof(ScoreMessageMode), cmdVal))
            {
                ScoreMessageMode scoreMode = (ScoreMessageMode)cmdVal;
                switch (scoreMode)
                {
                    case ScoreMessageMode.ScoreResponse:
                        String jsonStr = reader.ReadString();
                        EventLog.Write(jsonStr);
                        Console.WriteLine(jsonStr);
                        ScoreDataItem scoreDataItem = (ScoreDataItem)JsonTool.getInstance().Deserialize(jsonStr, typeof(ScoreDataItem));
                        return scoreDataItem;
                    case ScoreMessageMode.ScoreRequest:
                        return null;
                }
            }
            else
                throw new Exception("Score Message Mode is not defined.");
            return null;
        }

        private void saveDataItem(MeetingClient client, ScoreDataItem dataItem)
        {
            lock (scoreDataPairs)
            {
                ScoreDataPair dataPair = scoreDataPairs.FirstOrDefault(pair => pair.SettingDataItem.ScoreId == dataItem.Id);
                if (dataPair != null && dataPair.ScoreSettingStatus == ScoreSettingDataItem.ScoreSettingStatus.ACTIVE)
                    dataPair.saveDataItem(client, dataItem);
                else
                    throw new Exception(String.Format("Score Id {0} : Missing ScoreDataPair", dataItem.Id));
            }
        }

        private Object getNonScoreSettingItem(MeetingClient client)
        {
            List<ScoreSettingDataItem> nonScoreSettingItems = new List<ScoreSettingDataItem>(SettingDataItems);
            nonScoreSettingItems.RemoveAll(item => isExist(client, item.ScoreId));
            return nonScoreSettingItems;
        }

        private bool isExist(MeetingClient client, int id)
        {
            ScoreDataPair dataPair = scoreDataPairs.FirstOrDefault(pair => pair.SettingDataItem.ScoreId == id);
            List<Object[]> scoreDataItems = dataPair == null ? null : (List<Object[]>)dataPair.DataItems;
            return (scoreDataItems != null && scoreDataItems.Any(item => item[0].Equals(client.IpAddr)) )|| (dataPair.ScoreSettingStatus != ScoreSettingDataItem.ScoreSettingStatus.ACTIVE);
        }
        public BindingList<ScoreSettingDataItem> SettingDataItems
        {
            get { return (BindingList<ScoreSettingDataItem>)parm; }
            set { parm = value; }
        }

        public Hashtable ScoreDataItems
        {
            get { return scoreDataItems; }
            set { scoreDataItems = value; }
        }

        public BindingList<ScoreDataPair> ScoreDataPairs
        {
            get { return scoreDataPairs; }
            set { scoreDataPairs = value; }
        } 
        public void calcCurrentAllResult()
        {
           // throw new NotImplementedException();
            foreach (ScoreDataPair dataPair in scoreDataPairs)
            {
                if (dataPair.ScoreSettingStatus == ScoreSettingDataItem.ScoreSettingStatus.ACTIVE) 
                    calcVoteResult(dataPair.ScoreId); 
            }
        }

        public void openResultExcel(int id)
        {
            //VoteDataPair dataPair = voteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId == id);
            //string path = dataPair.DataResult;
            //VoteResultWriter writer = new VoteResultWriter(path, true);     
            ScoreDataPair dataPair = scoreDataPairs.FirstOrDefault(pair => pair.ScoreId == id);
            string path = dataPair.SettingDataItem.ScoreResultPath;
            //ScoreResultWriter writer = new ScoreResultWriter(path, true);
            System.Diagnostics.Process.Start(path);
        }
    }
}
