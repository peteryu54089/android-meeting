using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MeetingSystem
{
    class ScoreSettingDataItem : INotifyPropertyChanged
    {
        private int scoreId;
        private String scoreSourcePath;
        private String scoreResultPath;
        private String scoreWebIp;
        private String webFileName;
        private ScoreSettingStatus status; 
        public  enum ScoreSettingStatus { ACTIVE, NONACTIVE, COMPLETE };

        public ScoreSettingDataItem(int scoreId, String scoreSourcePath, String scoreResultPath, String webFileName, int receiveCount = 0)
        {
            this.scoreId = scoreId;
            this.scoreSourcePath = scoreSourcePath;
            this.scoreResultPath = scoreResultPath;
            this.webFileName = webFileName;
            status = ScoreSettingStatus.NONACTIVE;
        }
        public ScoreSettingStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public String ScoreWebIp
        {
            get { return scoreWebIp; }
            set
            {
                scoreWebIp = value;
                OnPropertyChanged("ScoreWebIp");
            }
        } 
        public int ScoreId
        {
            get { return scoreId; }
            set
            {
                scoreId = value;
                OnPropertyChanged("ScoreId");
            }
        }

        public String ScoreSourcePath
        {
            get { return scoreSourcePath; }
            set
            {
                scoreSourcePath = value;
                OnPropertyChanged("ScoreSourcePath");
            }
        }

        public String ScoreResultPath
        {
            get { return scoreResultPath; }
            set
            {
                scoreResultPath = value;
                OnPropertyChanged("ScoreResultPath");
            }
        }

        public String WebFileName
        {
            get { return webFileName; }
            set
            {
                webFileName = value;
                OnPropertyChanged("WebSourePath");
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
