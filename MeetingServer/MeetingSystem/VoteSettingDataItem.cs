using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeetingSystem
{
    public class VoteSettingDataItem : INotifyPropertyChanged
    {
        private int voteId;
        private String voteSourcePath;
        private String voteReslutPath;
        private String voteWebIp;
        private String webFileName;
        private VoteSettingStatus status;
        public enum VoteSettingStatus { ACTIVE, NONACTIVE, COMPLETE };
        public VoteSettingStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        
        public VoteSettingDataItem(int voteId, String voteSourcePath, String voteReslutPath, String voteWebIp, String webFileName)
        {
            this.voteId = voteId;
            this.voteSourcePath = voteSourcePath;
            this.voteReslutPath = voteReslutPath;
            this.voteWebIp = voteWebIp;
            this.webFileName = webFileName;
        }
        public String VoteSourcePath
        {
            get { return voteSourcePath; }
            set
            {
                voteSourcePath = value;
                OnPropertyChanged("voteSourcePath");
            }
        } 
        public String VoteReslutPath
        {
            get { return voteReslutPath; }
            set
            {
                voteReslutPath = value;
                OnPropertyChanged("voteReslutPath");
            }
        }
        public String VoteWebIp
        {
            get { return voteWebIp; }
            set
            {
                voteWebIp = value;
                OnPropertyChanged("voteWebIp");
            }
        }
        public String WebFileName
        {
            get { return webFileName; }
            set
            {
                webFileName = value;
                OnPropertyChanged("WebFileName");
            }
        }
        public int VoteId
        {
            get { return voteId; }
            set { voteId = value; OnPropertyChanged("VoteId"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
 

    }
}
