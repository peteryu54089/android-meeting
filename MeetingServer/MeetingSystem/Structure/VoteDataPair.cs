using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MeetingSystem.Client;

namespace MeetingSystem
{
    public class VoteDataPair : INotifyPropertyChanged
    {
        private VoteSettingDataItem voteSettingItem = null;
        private List<Object[]> dataItems = new List<Object[]>();
        public VoteDataPair(VoteSettingDataItem settingDataItem)
        {
            this.voteSettingItem = settingDataItem;
            voteSettingItem.PropertyChanged += OnPropertyChanged;
        }

        public String ButtonName
        {
            get {
                return VoteSettingStatus == (VoteSettingDataItem.VoteSettingStatus.COMPLETE) ? "開票" : "結算";
            }
        }
        public VoteSettingDataItem.VoteSettingStatus VoteSettingStatus
        {
            get { return voteSettingItem.Status; }
        }
        public int VoteId
        {
            get { return voteSettingItem.VoteId; }
        }
        public String DataSource
        {
            get
            {
                String path = VoteSettingItem == null ? "" : VoteSettingItem.VoteSourcePath;
                return path;
            }
        }
        public String DataResult
        {
            get
            {
                String path = VoteSettingItem == null ? "" : VoteSettingItem.VoteReslutPath;
                return path;
            }
        }
        public String DataSourceName
        {
            get
            {
                String path = VoteSettingItem == null ? "" : VoteSettingItem.VoteSourcePath;
                String name = path;
                if (name.Contains("."))
                    name = path.Substring(path.LastIndexOf("\\") + 1);
                if (name.Contains("."))
                    name = name.Remove(name.LastIndexOf("."));
                return name;
            }
        }
   
        public List<Object[]> DataItems //ip + VoteDataItem
        {
            get { return dataItems; }
            set
            {
                dataItems = value;
                OnPropertyChanged("DataItems");
            }
        }
        public int VoteDataItemCount
        {
            get { return DataItems.Count; }
        }

        public void saveDataItem(MeetingClient client, VoteDataItem voteDataItem)
        {
            Console.Write(client.IpAddr + "   ");
            voteDataItem.print();
             Object[] existData = findByIp(client.IpAddr);
             if (existData != null)
             {
                 Console.WriteLine("####Error#### Repeat Vote data");
                 voteDataItem.print();
                 existData[1] = voteDataItem;
             }
             else
             {
                 DataItems.Add(new Object[] { client.IpAddr, voteDataItem });
             }
            OnPropertyChanged("VoteDataItemCount");
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
        public VoteSettingDataItem VoteSettingItem
        {
            get { return voteSettingItem; }
            set
            {
                voteSettingItem = value;
                OnPropertyChanged("VoteSettingItem");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private VoteSettingDataItem settingDataItem;
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
