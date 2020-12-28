using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MeetingSystem.Client;

namespace MeetingSystem
{
    class VoteDataPair : INotifyPropertyChanged
    {
        private VoteSettingDataItem voteSettingItem = null;
        private List<Object[]> dataItems = new List<Object[]>();
     

        public VoteDataPair(VoteSettingDataItem settingDataItem)
        {
            // TODO: Complete member initialization
            this.voteSettingItem = settingDataItem;
            voteSettingItem.PropertyChanged += OnPropertyChanged;
        }
        public VoteSettingDataItem.VoteSettingStatus ScoreSettingStatus
        {
            get { return voteSettingItem.Status; }
        }
        public List<Object[]> DataItems //ip 
        {
            get { return dataItems; }
            set
            {
                dataItems = value;
                OnPropertyChanged("ScoreDataItems");
            }
        }
        public int ScoreDataItemCount
        {
            get { return DataItems.Count; }
        }
        public void saveDataItem(MeetingClient client, VoteDataItem scoreDataItem)
        {
            DataItems.Add(new Object[] { client.IpAddr, scoreDataItem });
            OnPropertyChanged("ScoreDataItems");
        }

        public VoteSettingDataItem VoteSettingItem
        {
            get { return voteSettingItem; }
            set { voteSettingItem = value; }
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
