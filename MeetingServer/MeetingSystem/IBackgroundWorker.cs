using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MeetingSystem
{
    interface IBackgroundWorker
    {
        void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e);
        void BackkgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e);
    }
}
