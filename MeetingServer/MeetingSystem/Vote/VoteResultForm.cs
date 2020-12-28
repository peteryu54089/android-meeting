using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeetingSystem.Message;
using MeetingSystem.Client;

namespace MeetingSystem
{
    public partial class VoteResultForm : Form, IBackgroundWorker
    {
        public VoteResultForm()
        {
            InitializeComponent();
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            VoteMessageHandler voteMessageHandler = (VoteMessageHandler)MessagePool.getMessage(ClientMessage.VoteMessage);
            voteMessageHandler.calcAllVoteResult();
        }

        private void VoteResultForm_Load(object sender, EventArgs e)
        {
            voteBindingSource.DataSource = MessagePool.getMessage(ClientMessage.VoteMessage);
        }

        public void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (this)
            {
                toolStripStatusLabel.Text = "結算中.....";
            }
        }
        public void BackkgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this)
            {
                int id = 0;
                VoteMessageHandler voteMessageHandler = (VoteMessageHandler)MessagePool.getMessage(ClientMessage.VoteMessage);
                if (e.Result == null)
                {
                    toolStripStatusLabel.Text = "";
                    Console.WriteLine("vote result EventArgs NULL");
                    return;
                }
                else
                {
                    id = (int)e.Result;
                }
                voteMessageHandler.setToCompleteVoteDataItem(id);
                toolStripStatusLabel.Text = "";
            }
        }
        public void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (this)
            {
                VoteMessageHandler voteMessageHandler = (VoteMessageHandler)MessagePool.getMessage(ClientMessage.VoteMessage);
                int id = (int)e.Argument;
                voteMessageHandler.calcVoteResult(id);
                e.Result = id;
            }
        }
        private void voteResultDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ButtonName
            if (e.ColumnIndex == 4)
            {
                string buttonName = voteResultDataGridView.CurrentRow.Cells["ButtonName"].Value.ToString();
                int id = Convert.ToInt32(voteResultDataGridView.CurrentRow.Cells["VoteId"].Value);
                VoteMessageHandler voteMessageHandler = (VoteMessageHandler)MessagePool.getMessage(ClientMessage.VoteMessage);
                if (buttonName.Equals("結算"))
                {
                    toolStripStatusLabel.Text = "結算中.....";
                    BackgroundWorker tempBackGroundWorker = new BackgroundWorker();
                    tempBackGroundWorker.WorkerReportsProgress = true;
                    tempBackGroundWorker.WorkerSupportsCancellation = true;
                    tempBackGroundWorker.DoWork += BackgroundWorker_DoWork;
                    tempBackGroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                    tempBackGroundWorker.RunWorkerCompleted += BackkgroundWorker_RunWorkerCompleted;
                    tempBackGroundWorker.RunWorkerAsync(id);
                    //
                }
                else if (buttonName.Equals("開票"))
                    voteMessageHandler.openResultExcel(id);

            }
        }


    }
}
