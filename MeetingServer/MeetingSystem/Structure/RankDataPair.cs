using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeetingSystem.Structure
{
    class RankDataPair : INotifyPropertyChanged
    {
        private RankSettingDataItem settingDataItem = null;
        private List<Object[]> dataItems = new List<Object[]>();//已回傳client的ip及序位單


        public RankDataPair(RankSettingDataItem settingDataItem)
        {
            this.settingDataItem = settingDataItem;
            settingDataItem.PropertyChanged += OnPropertyChanged;
        }
        public RankSettingDataItem SettingDataItem
        {
            get { return settingDataItem; }
            set
            {
                settingDataItem = value;
                OnPropertyChanged("RankSettingDataItems");
            }
        }
        public RankSettingDataItem.RankSettingStatus RankSettingStatus
        {
            get { return settingDataItem.Status; }
        }
        public List<Object[]> DataItems //ip + 序位單
        {
            get { return dataItems; }
            set
            {
                dataItems = value;
                OnPropertyChanged("RankDataItems");
            }
        }
        public String TableName
        {
            get
            {
                String path = SettingDataItem == null ? "" : SettingDataItem.RankSourcePath;
                String name = path;
                if (name.Contains("."))
                    name = path.Substring(path.LastIndexOf("\\") + 1);
                if (name.Contains("."))
                    name = name.Remove(name.LastIndexOf("."));
                return name;
            }
        }
        public int RankId
        {
            get
            {
                return SettingDataItem.RankId;
            }
        }
        public String ButtonName
        {
            get
            {
                return settingDataItem.Status == (RankSettingDataItem.RankSettingStatus.COMPLETE) ? "開票" : "結算";
            }
        }
        public int RankDataItemCount
        {
            get
            {
                return dataItems.Count;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
        public void saveDataItem(Client.MeetingClient client, RankDataItem rankDataItem)
        {
            Console.Write(client.IpAddr + "   ");
            rankDataItem.print();
             Object[] existData = findByIp(client.IpAddr);
             if (existData != null)
             {
                 Console.WriteLine("####Error#### Repeat Score data");
                 rankDataItem.print();
                 existData[1] = rankDataItem;
             }
             else
             {
                 DataItems.Add(new Object[] { client.IpAddr, rankDataItem });
                 OnPropertyChanged("RankDataItems");
             }
        }
        public Object[] findByIp(Object ip)
        {
            for (int i = 0; i < DataItems.Count; i++)
            {
                Object[] data = DataItems.ElementAt(i);
                if (data[0].Equals(ip))
                    return data;
            }
            return null;
        }
    }
}
