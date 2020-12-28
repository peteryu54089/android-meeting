using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeetingSystem.Structure
{
    public class RankSettingDataItem : INotifyPropertyChanged
    {
        public enum RankSettingStatus { ACTIVE, NONACTIVE, COMPLETE };
        private RankSettingStatus status;
        private int rankId;
        private String rankSourcePath;
        private String rankResultPath;
        private String totalRankPath;
        private String rankWebIp;
        private String webFileName;

        public RankSettingDataItem(int p, string rankSource, string rankResult, string webSourePath, string totalRankPath)
        {
            // TODO: Complete member initialization
            this.rankId = p;
            rankSourcePath = rankSource;
            rankResultPath = rankResult;
            webFileName = webSourePath;
            this.totalRankPath = totalRankPath;
            this.status = RankSettingStatus.NONACTIVE;
        }

        public int RankId
        {
            get { return rankId; }
            set { rankId = value;
            OnPropertyChanged("rankId");
            }
        }
        public String RankSourcePath
        {
            get { return rankSourcePath; }
            set { rankSourcePath = value;
            OnPropertyChanged("RankSourcePath");
            }
        }
        public String TotalRankPath
        {
            get { return totalRankPath; }
            set
            {
                totalRankPath = value;
                OnPropertyChanged("TotalRankPath");
            }
        }
        public String RankResultPath
        {
            get { return rankResultPath; }
            set { rankResultPath = value;
            OnPropertyChanged("RankResultPath");
            }
        }
        public String RankWebIp
        {
            get { return rankWebIp; }
            set { rankWebIp = value;
            OnPropertyChanged("RankWebIp");
            }
        }
        public String WebFileName
        {
            get { return webFileName; }
            set { webFileName = value;
            OnPropertyChanged("WebFileName");
            }
        }
        public RankSettingStatus Status
        {
            get { return status; }
            set { status = value;
                OnPropertyChanged("Status"); 
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
    }
}
