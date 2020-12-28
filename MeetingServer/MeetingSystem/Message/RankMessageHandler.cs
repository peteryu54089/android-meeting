using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.IO;
using System.ComponentModel;
using MeetingSystem.Structure;
using System.Collections;
using System.Configuration;
using MeetingSystem.Utils;
using MeetingSystem.Rank;

namespace MeetingSystem.Message
{
    class RankMessageHandler : Message
    {
        public enum RankMessageMode { RankRequest, RankResponse };
        private Hashtable rankDataItems = null;
        private BindingList<RankDataPair> rankDataPairs = new BindingList<RankDataPair>();
        private string rankTicketStartValue;
        private string rankResultStart;

        public RankMessageHandler()
        {
            parm = new BindingList<RankSettingDataItem>();
            SettingDataItems.ListChanged += new ListChangedEventHandler(RankSettingItems_ListChanged);
            rankTicketStartValue = MeetingSystem.Properties.Settings.Default.rankTicketStartValue;
            rankResultStart = MeetingSystem.Properties.Settings.Default.rankResultStart;
        }
        public BindingList<RankDataPair> RankDataPairs
        {
            get { return rankDataPairs; }
            set { rankDataPairs = value; }
        } 
        void RankSettingItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
                return;
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                rankSettingItem_Rmove(e.NewIndex);
                return;
            }
            rankSettingItem_Add();
        }

        private void rankSettingItem_Add()
        {
            foreach (RankSettingDataItem settingDataItem in SettingDataItems)
            {
                RankDataPair targetDataPair = RankDataPairs.FirstOrDefault(pair => pair.SettingDataItem == settingDataItem);
                bool isExist = (targetDataPair != null);
                if (!isExist) //不存在所以新增
                {
                    RankDataPair dataPair = new RankDataPair(settingDataItem);
                    RankDataPairs.Add(dataPair);
                }
            }
        }
        private void rankSettingItem_Rmove(int affectIndex)
        {
            if (affectIndex < RankDataPairs.Count)
            {
                RankSettingDataItem rankSettingDataItem = RankDataPairs[affectIndex].SettingDataItem;
                if (!SettingDataItems.Contains(rankSettingDataItem))
                    RankDataPairs.RemoveAt(affectIndex);

            }
        }
        public static void init()
        {
            MessagePool.registerMessage(ClientMessage.RankMessage, new RankMessageHandler());
        }
        public override void sendMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            String jsonStr = JsonTool.getInstance().Serialize(obj);
            String msg = "cmd:3" + "msg:" + jsonStr + "\n";
            writer.Write(msg);
            //Console.WriteLine(msg);
            //writer.Write((int)ClientMessage.RankMessage);
            //writer.Write(jsonStr);
        }
        public override void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            if (obj == null) //送出該使用者是尚未進行序位的獵表
                sendMessage(client, writer, getNonRankSettingItem(client));
            else
            {//將該使用者的序位結果儲存起來
                if(obj.GetType()==typeof(RankDataItem))
                    saveDataItem(client,(RankDataItem)obj);
            }
        }
        private void saveDataItem(MeetingClient client, RankDataItem rankDataItem)
        {
            lock (RankDataPairs)
            {
                RankDataPair dataPair = RankDataPairs.FirstOrDefault(pair => pair.SettingDataItem.RankId == rankDataItem.Id);
                if (dataPair != null && dataPair.RankSettingStatus == RankSettingDataItem.RankSettingStatus.ACTIVE)
                    dataPair.saveDataItem(client, rankDataItem);
                else
                    throw new Exception(String.Format("Score Id {0} : Missing ScoreDataPair", rankDataItem.Id));

            }
        }
        private object getNonRankSettingItem(MeetingClient client)
        {
            List<RankSettingDataItem> nonRankSettingItems = new List<RankSettingDataItem>(SettingDataItems);
            nonRankSettingItems.RemoveAll(item=>isExist(client,item.RankId));
            return nonRankSettingItems;
        }
        private bool isExist(MeetingClient client, int id)
        {
            RankDataPair dataPair = RankDataPairs.FirstOrDefault(pair => pair.SettingDataItem.RankId == id);
            List<Object[]> rankDataItems = dataPair == null ? null : (List<Object[]>)dataPair.DataItems;
            return (rankDataItems!=null && rankDataItems.Any(item=>item[0].Equals(client.IpAddr)))||(dataPair.RankSettingStatus!=RankSettingDataItem.RankSettingStatus.ACTIVE);
        }
        public BindingList<RankSettingDataItem> SettingDataItems
        {
            get { return (BindingList<RankSettingDataItem>)parm; }
            set { parm = value; }
        }
        public override Object receivedMessage(MeetingClient client, BinaryReader reader)
        {
            int cmdVal = reader.ReadInt32();
            if (Enum.IsDefined(typeof(RankMessageMode), cmdVal))
            {
                RankMessageMode rankMode = (RankMessageMode)cmdVal;
                switch (rankMode)
                {
                    case RankMessageMode.RankResponse:
                        String jsonStr = reader.ReadString();
                        //Console.WriteLine(jsonStr);
                        EventLog.Write(jsonStr);
                        RankDataItem rankDatatItem = (RankDataItem)JsonTool.getInstance().Deserialize(jsonStr, typeof(RankDataItem));
                        return rankDatatItem;
                    case RankMessageMode.RankRequest:
                        return null;
                }
            }else
                throw new Exception("Score Message Mode is not defined.");
            return null;
        }
        private void saveRankBallot(RankDataPair dataPair)
        {
            const int IP_INDEX = 0;
            const int DATA_INDEX = 1;
            char statisticsStart = 'A';
            int statisticsIndex = 1;
            String path = dataPair.SettingDataItem.RankSourcePath;
            String dir = path.Remove(path.LastIndexOf("."));
            String ext = path.Substring(path.LastIndexOf("."));
            String newTable = dir + "_選票檔" + ext;
            File.Copy(path, newTable, true);
            RankResultWriter writer = new RankResultWriter(newTable);
            writer.clone(dataPair.RankDataItemCount);
            for (int i = 1; i <= dataPair.DataItems.Count; i++)
            {  
                RankDataItem item = (RankDataItem)dataPair.DataItems[i-1][DATA_INDEX];//ip + RankDataItem 
                string ip=((System.Net.IPAddress)(dataPair.DataItems[i-1][IP_INDEX])).ToString();
                writer.setSheetIndex(i);
                writer.reNameSheet(ip.Substring(ip.LastIndexOf('.') + 1)); 
                writer.writeRankTicketWriter(rankTicketStartValue, 1, item.RankCandidateList);

            }

            /*****統計表******/
            EventLog.Write("統計表產出中");
            writer.setSheetIndex(dataPair.DataItems.Count+1);
            writer.reNameSheet("票數統整");
            RankDataItem titleItem = (RankDataItem)dataPair.DataItems[0][DATA_INDEX];//ip + RankDataItem 
            writer.writeRankStatisticsTitle(statisticsStart + statisticsIndex.ToString(), 1, titleItem.RankCandidateList);
            statisticsIndex++;
            for (int i = 1; i <= dataPair.DataItems.Count; i++)
            {
                RankDataItem item = (RankDataItem)dataPair.DataItems[i - 1][DATA_INDEX];//ip + RankDataItem 
                string ip = ((System.Net.IPAddress)(dataPair.DataItems[i - 1][IP_INDEX])).ToString();
                writer.writeRankStatistics(statisticsStart + statisticsIndex.ToString(), 1, ip.Substring(ip.LastIndexOf('.') + 1), item.RankCandidateList);
                statisticsIndex++;
            }


            writer.save();
            writer.close();
        }
        public void calcRankResult(int id)
        {
            lock (RankDataPairs)
            {
                RankDataPair dataPair = rankDataPairs.FirstOrDefault(pair => pair.SettingDataItem.RankId == id);
                if (dataPair.DataItems.Count <= 0)
                    return;
                saveRankResult(dataPair);
                saveRankBallot(dataPair);
                if (dataPair.SettingDataItem.TotalRankPath == "")
                    return;
                //FileStream fs = new FileStream(@dataPair.SettingDataItem.TotalRankPath, FileMode.Append);
                //StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
                ExcelUtil rank = new ExcelUtil(dataPair.SettingDataItem.RankResultPath, false, 1);
                ExcelUtil total = new ExcelUtil(dataPair.SettingDataItem.TotalRankPath, false, 1);

                for (int row = 3; rank.read(row, 4) != "" && row < 100; row++)
                {
                    string rankNo = rank.read(row, 4);
                    if (rankNo == "1")
                        for (int t_row = 4; t_row < 100 ; t_row++)
                        {
                            if (total.read(t_row, 1) == "")
                            {
                                total.write(t_row, 1, rank.read(row, 1));
                                total.write(t_row, 2, rank.read(row, 2));
                                break;
                            }
                        }
                }
                total.save();
                rank.close();
                total.close();
                /*
                for (int row = 3; excelUtil.read(row, 4) != ""; row++)
                {
                    string rank = excelUtil.read(row, 4);
                    if (rank == "1")
                        sw.WriteLine(excelUtil.read(row, 1) + "," + excelUtil.read(row, 2));
                }
                excelUtil.close();
                sw.Flush();
                sw.Close();
                fs.Close();*/
            }
        }

        private void saveRankResult(RankDataPair dataPair)
        {
            if (dataPair.DataItems.Count == 0)
                return;
            String path = dataPair.SettingDataItem.RankResultPath; 
            RankResultWriter writer = new RankResultWriter(path);
            RankDataItem item=(RankDataItem)dataPair.DataItems[0][1];
            Dictionary<string,int> returnTable=new  Dictionary<string,int>();
            foreach (RankDataItem.RankData rankData in item.RankCandidateList)//哪幾位候選人
            {
                int ticketCount = dataPair.DataItems.Sum(pair => SumTicket(pair[1], rankData.Name));
                returnTable.Add(rankData.Name, ticketCount); //票種 數目
            }
            writer.writeRankResult(rankResultStart, returnTable);
            writer.save();
            writer.close();
            
        }
        public void setToCompleteRankDataItem(int id)
        {
            RankDataPair dataPair = rankDataPairs.FirstOrDefault(pair => pair.SettingDataItem.RankId == id);
            dataPair.SettingDataItem.Status = RankSettingDataItem.RankSettingStatus.COMPLETE;
        }
        private int SumTicket(object obj, string name)
        {
            RankDataItem rankItem = (RankDataItem)obj;
            RankDataItem.RankData rankData = rankItem.RankCandidateList.FirstOrDefault(pair => pair.Name == name);
            return Convert.ToInt32(rankData.Rank);
        }

        public void calcAllRankResult()
        {
            foreach (RankDataPair dataPair in rankDataPairs)
            {
                if (dataPair.RankSettingStatus == RankSettingDataItem.RankSettingStatus.ACTIVE)
                    calcRankResult(dataPair.RankId);
            }
        }

        public void openResultExcel(int id)
        {
            RankDataPair dataPair = rankDataPairs.FirstOrDefault(pair => pair.SettingDataItem.RankId == id);
            String path = dataPair.SettingDataItem.RankResultPath;
            //RankResultWriter writer = new RankResultWriter(path,true);
            System.Diagnostics.Process.Start(path);
        }
    }
}
