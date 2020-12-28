using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MeetingSystem.Client;

namespace MeetingSystem.Structure
{
    class ScoreDataPair : INotifyPropertyChanged
    {
        private ScoreSettingDataItem settingDataItem = null;
        private List<Object[]> dataItems = new List<Object[]>();
        
        public ScoreDataPair(ScoreSettingDataItem settingDataItem)
        {
            this.settingDataItem = settingDataItem;
            settingDataItem.PropertyChanged += OnPropertyChanged;
        }

        public ScoreSettingDataItem SettingDataItem
        {
            get { return settingDataItem; }
            set 
            {
                settingDataItem = value;
                OnPropertyChanged("ScoreSettingDataItems");
            }
        }
        public ScoreSettingDataItem.ScoreSettingStatus ScoreSettingStatus
        {
            get { return settingDataItem.Status; }
        }
        public List<Object[]> DataItems //ip 分數列表
        {
            get { return dataItems; }
            set 
            {
                dataItems = value;
                OnPropertyChanged("ScoreDataItems");
            }
        }

        public String ButtonName
        {
            get
            {
                return settingDataItem.Status == (ScoreSettingDataItem.ScoreSettingStatus.COMPLETE) ? "開票" : "結算";
            }
        }
        public String TableName
        {
            get
            {
                String path = SettingDataItem == null? "" : SettingDataItem.ScoreSourcePath;
                String name = path;
                if(name.Contains("."))
                    name = path.Substring(path.LastIndexOf("\\") + 1);
                if (name.Contains("."))
                    name = name.Remove(name.LastIndexOf("."));
                return name;
            }
        }

        public int ScoreId
        {
            get
            {
                return SettingDataItem.ScoreId;
            }
        }

        public int ScoreDataItemCount
        {
            get { return DataItems.Count; }
        }
        public String ScoreStatus
        {
            get {
                switch (settingDataItem.Status)
                {
                    case ScoreSettingDataItem.ScoreSettingStatus.ACTIVE:
                        return "投票中";
                    case ScoreSettingDataItem.ScoreSettingStatus.NONACTIVE:
                        return "NONACTIVE";
                    case ScoreSettingDataItem.ScoreSettingStatus.COMPLETE:
                        return "結算完成";
                }
                return "";
            
            } 
        }
        public void saveDataItem(MeetingClient client, ScoreDataItem scoreDataItem)
        {
            Console.Write(client.IpAddr + "   ");
            scoreDataItem.print();
            Object[] existData = findByIp(client.IpAddr);
            if (existData != null)
            {
                Console.WriteLine("####Error#### Repeat Score data");
                scoreDataItem.print();
                existData[1] = scoreDataItem;
            }
            else
            {
                DataItems.Add(new Object[] { client.IpAddr, scoreDataItem });
            }
            OnPropertyChanged("ScoreDataItems");
        }

        public Object[] findByIp(Object ip)
        {
            for(int i =0; i < DataItems.Count; i++)
            {
                Object[] data = DataItems.ElementAt(i);
                if (data[0].Equals(ip))
                    return data;
            }
            return null;
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
    }
}
