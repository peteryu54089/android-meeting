using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.IO;
using MeetingSystem.Utils;
using System.ComponentModel;
using System.Collections;
using MeetingSystem.Vote;
using MeetingSystem.Structure;
using MeetingSystem.Server;
using System.Globalization;

namespace MeetingSystem.Message
{
    public class VoteMessageHandler : Message
    {
        public enum VoteMessageMode { VoteRequest, VoteResponse };
        public string title = "";//global variable
        private Hashtable voteDataItems = null;
        private BindingList<VoteDataPair> voteDataPairs = new BindingList<VoteDataPair>();
        private string rankTicketStartValue = MeetingSystem.Properties.Settings.Default.rankTicketStartValue;
        private string voteTicketStart = MeetingSystem.Properties.Settings.Default.voteTicketStart;
        private string voteResultStart = MeetingSystem.Properties.Settings.Default.voteResultStart;
        private string passPersonRead = MeetingSystem.Properties.Settings.Default.passPersonRead;
        public BindingList<VoteDataPair> VoteDataPairs
        {
            get { return voteDataPairs; }
        }
        private VoteMessageHandler()
        {
            parm = new BindingList<VoteSettingDataItem>();
            VoteSettingDataItems.ListChanged += new ListChangedEventHandler(VoteSettingItems_ListChanged);
        }
        private void VoteSettingItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                voteSettingItem_Rmove(e.NewIndex);
                return;
            }
            voteSettingItem_Add();
        }
        private void voteSettingItem_Rmove(int affectIndex)
        {
            if (affectIndex < VoteDataPairs.Count)
            {
                VoteSettingDataItem voteSettingDataItem = VoteDataPairs[affectIndex].VoteSettingItem;
                if (!VoteSettingDataItems.Contains(voteSettingDataItem))
                    VoteDataPairs.RemoveAt(affectIndex);
            }
        }
        private void voteSettingItem_Add()
        {
            foreach (VoteSettingDataItem settingDataItem in VoteSettingDataItems)
            {
                bool isExist = VoteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem == settingDataItem) != null;
                if (!isExist)
                {
                    VoteDataPair dataPair = new VoteDataPair(settingDataItem);
                    VoteDataPairs.Add(dataPair);
                }
            }
        }
        public static void init()
        {
            MessagePool.registerMessage(ClientMessage.VoteMessage, new VoteMessageHandler());
        }
        public override void sendMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            //List<int> testDatas = new List<int> { 1, 2, 3 };
            String jsonStr = JsonTool.getInstance().Serialize(obj);
            //writer.Write((int)ClientMessage.VoteMessage);
            //writer.Write(jsonStr);

            String msg = "cmd:1" + "msg:" + jsonStr + "\n";
            writer.Write(msg);
           // Console.WriteLine(msg);
        }

        public override void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            if (obj == null)
                sendMessage(client, writer, getNonVoteSettingItem(client));
            else
            {
                if (obj.GetType() == typeof(VoteDataItem))
                    saveDataItem(client, (VoteDataItem)obj);
            }
                
        }
        private void saveDataItem(MeetingClient client, VoteDataItem voteDataItem)
        {
            lock (VoteDataPairs)
            {
                VoteDataPair dataPair = VoteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId == voteDataItem.Id);
                if (dataPair != null && dataPair.VoteSettingStatus == VoteSettingDataItem.VoteSettingStatus.ACTIVE)
                    dataPair.saveDataItem(client, voteDataItem);
                else 
                    throw new Exception(String.Format("Vote Id {0}:Missing VoteDataPair",voteDataItem.Id));
            }
        }
        private Object getNonVoteSettingItem(MeetingClient client)
        {
            List<VoteSettingDataItem> nonVoteSettingItems = new List<VoteSettingDataItem>(VoteSettingDataItems);
            nonVoteSettingItems.RemoveAll(item => isExist(client, item.VoteId));
            return nonVoteSettingItems;
        }
        private bool isExist(MeetingClient client, int id)
        {
            VoteDataPair dataPair = VoteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId== id);
            List<Object[]> scoreDataItems = dataPair == null ? null : (List<Object[]>)dataPair.DataItems;
            return (scoreDataItems != null && scoreDataItems.Any(item => item[0].Equals(client.IpAddr))) || (dataPair.VoteSettingStatus != VoteSettingDataItem.VoteSettingStatus.ACTIVE);
        } 

        public override Object receivedMessage(MeetingClient client, BinaryReader reader)
        {
            int cmdVal = reader.ReadInt32();
            if (Enum.IsDefined(typeof(VoteMessageMode), cmdVal))
            {
                VoteMessageMode scoreMode = (VoteMessageMode)cmdVal;
                switch (scoreMode)
                {
                    case VoteMessageMode.VoteResponse:
                        String jsonStr = reader.ReadString();
                        Console.WriteLine(jsonStr);
                        VoteDataItem voteDataItem = (VoteDataItem)JsonTool.getInstance().Deserialize(jsonStr, typeof(VoteDataItem));
                        return voteDataItem;
                    case VoteMessageMode.VoteRequest:
                        return null;
                }
            }else
                throw new Exception("Vote Message Mode is not defined.");
            return null;
        }
        public BindingList<VoteSettingDataItem> VoteSettingDataItems
        {
            get { return (BindingList<VoteSettingDataItem>)parm; }
            set { parm = value; }
        }

        public void calcVoteResult(int id)
        {
            lock (voteDataPairs)
            {
                VoteDataPair dataPair = voteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId == id);
                if (dataPair.DataItems.Count <= 0)
                    return;
                saveVoteBallot(dataPair);//每一章選票 
                saveVoteResult(dataPair);
            }
        }

        private void generateOrderTicket(VoteDataPair dataPair, List<String> returnList)
        {
            String OrderPath = dataPair.VoteSettingItem.OrderPath;
            String OrderResultPath = dataPair.VoteSettingItem.OrderResultPath;
            String VoteRPath = dataPair.VoteSettingItem.VoteReslutPath;
            String TotalRankPath = Path.GetDirectoryName(VoteRPath) + "\\本校「傑出教學獎」推薦名單案序位投票.xlsx";
            String TotalRankRPath = Path.GetDirectoryName(VoteRPath) + "\\本校「傑出教學獎」推薦名單案序位投票結果.xlsx";
            if (!dataPair.VoteSettingItem.OrderBy)
                return;
            Dictionary<string, string> rankGroup = new Dictionary<string, string>();
            foreach (string name in returnList)
            {
                string key = name.Substring(0, name.LastIndexOf(":"));
                string value = name.Substring(name.LastIndexOf(":") + 1);
                if (rankGroup.ContainsKey(key))
                    rankGroup[key] = rankGroup[key] + "," + value;
                else
                    rankGroup.Add(key, value);
            }
            File.Delete(@TotalRankPath);
            File.Delete(@TotalRankRPath);
            File.Copy("./序位投票.xlsx", @TotalRankPath, true);
            File.Copy("./序位投票結果.xlsx", @TotalRankRPath, true);
            ExcelUtil VoteExcel = new ExcelUtil(@dataPair.VoteSettingItem.VoteSourcePath, false, 1);
            ExcelUtil excelUtil = new ExcelUtil(@TotalRankPath, false, 1);
            ExcelUtil TRankRExcel = new ExcelUtil(@TotalRankRPath, false, 1);
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
            string voteTitle = VoteExcel.read(1, 1);
            string today = string.Format("{0}.{1}.{2}",
                                 taiwanCalendar.GetYear(DateTime.Now),
                                 DateTime.Now.Month,
                                 DateTime.Now.Day);
            VoteExcel.close();
            TRankRExcel.write(1, 1, "本校「傑出教學獎」推薦名單案      " + today);
            TRankRExcel.save();
            TRankRExcel.close();


            excelUtil.write(1, 1, "本校「傑出教學獎」推薦名單案    " + today);
            foreach (KeyValuePair<string, string> kvp in rankGroup)
            {
                if (kvp.Value.Split(',').Count() < 2)
                {
                    for (int row = 4;row < 100; row++)
                    {
                        if (excelUtil.read(row, 1) == "")
                        {
                            excelUtil.write(row, 1, kvp.Key);
                            excelUtil.write(row, 2, kvp.Value);
                            break;
                        }
                    }
                    continue;
                }
                string newOrderPath = VoteRPath.Remove(VoteRPath.LastIndexOf("\\")) + "\\序位投票_" + kvp.Key + ".xlsx";
                string newOrderResultPath = VoteRPath.Remove(VoteRPath.LastIndexOf("\\")) + "\\序位投票結果_" + kvp.Key + ".xlsx";


                EventLog.Write(newOrderResultPath + "  hello " + newOrderPath);
                File.Copy("./序位投票.xlsx", newOrderPath, true);
                File.Copy("./序位投票結果.xlsx", newOrderResultPath, true);
                ExcelUtil OrderResult = new ExcelUtil(newOrderResultPath, false, 1);
                OrderResult.write(1, 1, voteTitle + "   " + today);
                Console.Write(voteTitle + "   " + today);
                OrderResult.save();
                OrderResult.close();
                VoteResultWriter writer = new VoteResultWriter(newOrderPath);
                writer.writeCandidateRank(rankTicketStartValue, kvp.Value, kvp.Key, voteTitle + "   " +today);
                writer.save();
                writer.close();
                const String HTML_EXT = ".html";
                String rankSource = newOrderPath;
                String rankResult = newOrderResultPath;
                if (rankSource == "" || rankResult == "")
                    return;
                Object parm = MessagePool.getMessage(ClientMessage.RankMessage).getParm();
                Random random = new Random();
                if (parm != null)
                {
                    BindingList<RankSettingDataItem> list = (BindingList<RankSettingDataItem>)parm;
                    String webSourePath = DateTime.Now.Ticks.ToString() + random.Next(100) + HTML_EXT;
                    RankSettingDataItem RSitem = new RankSettingDataItem(list.Count(), rankSource, rankResult, webSourePath, TotalRankPath);
                    RSitem.RankWebIp = String.Format("http://{0}/Rank.html", MeetingServer.getInstance().IP);
                    list.Add(RSitem);
                }
                else
                    Console.WriteLine("RankSourceForm : parm is null.");
            }
            excelUtil.save();
            excelUtil.close();

        }
        public void setToCompleteVoteDataItem(int id)
        {
            VoteDataPair dataPair = voteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId == id);
            dataPair.VoteSettingItem.Status = VoteSettingDataItem.VoteSettingStatus.COMPLETE;
        }
        private void saveVoteResult(VoteDataPair dataPair)
        {
            //要修改
            String path = dataPair.VoteSettingItem.VoteReslutPath;
            int voteCount = dataPair.DataItems.Count; //投票人數
            double passThreshold = (double)voteCount/3*2; //應通過門檻
            List<string> passOrNot  = new List<string>(); //存入通過或未通過
            //計算每位候選人，同意、不同意、廢票的票數
            if (voteCount == 0)//如果沒人投票，就不計算
                return;
            VoteResultWriter writer = new VoteResultWriter(path);
            VoteDataItem item = (VoteDataItem)(dataPair.DataItems[0][1]);
            Dictionary<string,int[]> calcData=new Dictionary<string,int[]>();
            foreach (VoteDataItem.VoteData voteData in item.CandidateList)//哪幾位候選人
            {
                int agreeCount = dataPair.DataItems.Count(objects => isAgree(objects[1], voteData.Name));//同意票的票數
                int rejectCount = dataPair.DataItems.Count(objects => isReject(objects[1], voteData.Name));//不同意票的票數
                int invalidCount = dataPair.DataItems.Count(objects => isInvalid(objects[1], voteData.Name));//廢票的票數
                if (agreeCount >= Math.Ceiling(passThreshold))
                {
                    passOrNot.Add("通過");
                }
                else {
                    passOrNot.Add("不通過");
                }
                calcData.Add(voteData.Name, new int[] { agreeCount, rejectCount, invalidCount }); 
            }
            writer.writeVoteTotal(voteResultStart, calcData, passOrNot);
            writer.writeTitle(title, date());
            writer.writeVoteAndVotedNumber(item.CandidateList.Count.ToString(), voteCount.ToString(), Math.Ceiling(passThreshold).ToString()); //被投，投票, 通過門檻
            List<String> returnList = new List<String>();
            writer.readPassPeople(passPersonRead, item.CandidateList.Count, returnList);
            generateOrderTicket(dataPair, returnList);

            writer.delete(item.CandidateList.Count, "voteType");
            writer.save();
            writer.close();
        }

        public string date()
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
            string today = string.Format("{0}.{1}.{2}",
                                taiwanCalendar.GetYear(DateTime.Now),
                                DateTime.Now.Month,
                                DateTime.Now.Day);
            return today;
        }

        private bool isAgree(Object obj, String name)
        {
            VoteDataItem voteData = (VoteDataItem)obj;
            return voteData.CandidateList.FirstOrDefault(data => name == data.Name && data.AgreeChoose) != null;//該候選人是否為同意票
        }

        private bool isReject(Object obj, String name)
        {
            VoteDataItem voteData = (VoteDataItem)obj;
            return voteData.CandidateList.FirstOrDefault(data => name == data.Name && data.RejectChoose) != null;
        }

        private bool isInvalid(Object obj, String name)
        {
            VoteDataItem voteData = (VoteDataItem)obj;
            return voteData.CandidateList.FirstOrDefault(data => name == data.Name && data.InvalidateChoose) != null;
        }
        private void saveVoteBallot(VoteDataPair dataPair)
        {

            const int IP_INDEX = 0;
            const int DATA_INDEX = 1;
            char statisticsStart = 'A';
            int statisticsIndex = 1;
            String path = dataPair.DataSource;
            String dir = path.Remove(path.LastIndexOf("."));         
            String ext = path.Substring(path.LastIndexOf("."));
            EventLog.Write(path + "  hello3 " + dir+" hellpo "+ext);

            String newTable = dir +"_選票檔" + ext;
            File.Copy(path, newTable, true);
            VoteResultWriter writer = new VoteResultWriter(newTable);
            writer.clone(dataPair.DataItems.Count);
            for (int i = 1; i <= dataPair.DataItems.Count; i++)
            {
                VoteDataItem item = (VoteDataItem)(dataPair.DataItems[i - 1][DATA_INDEX]);//0 是 ip，1 是VoteDataItem
                String ip = ((System.Net.IPAddress)(dataPair.DataItems[i - 1][IP_INDEX])).ToString();//dataItem
                writer.setSheetIndex(i);
                writer.reNameSheet(ip.Substring(ip.LastIndexOf('.') + 1)); 
                writer.writeVote(voteTicketStart, 1, item.CandidateList); 
            }
            VoteResultWriter agreement = new VoteResultWriter(path); //同意票檔案
            title = agreement.readTitle();
            /*******選票檔****/
            EventLog.Write("統計表產出中");
            writer.setSheetIndex(dataPair.DataItems.Count + 1);
            writer.reNameSheet("選票統整");
            VoteDataItem titleItem = (VoteDataItem)dataPair.DataItems[0][DATA_INDEX];
            writer.writeRankStatisticsTitle(statisticsStart + statisticsIndex.ToString(), 1, titleItem.CandidateList);
            statisticsIndex++;
            for (int i = 1; i <= dataPair.DataItems.Count; i++)
            {
                VoteDataItem item = (VoteDataItem)dataPair.DataItems[i - 1][DATA_INDEX];
                string ip = ((System.Net.IPAddress)(dataPair.DataItems[i - 1][IP_INDEX])).ToString();

                writer.writeRankStatistics(statisticsStart + statisticsIndex.ToString(), 1, ip.Substring(ip.LastIndexOf('.') + 1), item.CandidateList);
                statisticsIndex++;
            }


            writer.save();
            writer.close();
            agreement.close();
        }
        public void calcAllVoteResult()
        {
            foreach (VoteDataPair pair in voteDataPairs)
            {
                if (pair.VoteSettingStatus == VoteSettingDataItem.VoteSettingStatus.ACTIVE)
                    calcVoteResult(pair.VoteId);
            }
        }

        public void openResultExcel(int id)
        {
            VoteDataPair dataPair = voteDataPairs.FirstOrDefault(pair => pair.VoteSettingItem.VoteId == id);
            string path=dataPair.DataResult;
           // VoteResultWriter writer = new VoteResultWriter(path, true);
            System.Diagnostics.Process.Start(path);    
        }
    }
}
