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

namespace MeetingSystem.Rank
{
    public partial class RankResultForm : Form, IBackgroundWorker
    {
        public RankResultForm()
        {
            InitializeComponent();
            voteBindingSource.DataSource = MessagePool.getMessage(ClientMessage.RankMessage);
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            RankMessageHandler rankMessageHandler = (RankMessageHandler)MessagePool.getMessage(ClientMessage.RankMessage);
            rankMessageHandler.calcAllRankResult();
        }
        private void rankResultDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string buttonName = rankResultDataGridView.CurrentRow.Cells["ButtonName"].Value.ToString();  
                int id = Convert.ToInt32(rankResultDataGridView.CurrentRow.Cells["RankId"].Value);
                RankMessageHandler rankMessageHandler=(RankMessageHandler)MessagePool.getMessage(ClientMessage.RankMessage);

                if (buttonName.Equals("結算"))
                {
                    BackgroundWorker tempBackGroundWorker = new BackgroundWorker();
                    tempBackGroundWorker.WorkerReportsProgress = true;
                    tempBackGroundWorker.WorkerSupportsCancellation = true;
                    tempBackGroundWorker.DoWork += BackgroundWorker_DoWork;
                    tempBackGroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                    tempBackGroundWorker.RunWorkerCompleted += BackkgroundWorker_RunWorkerCompleted;
                    tempBackGroundWorker.RunWorkerAsync(id); 
                  
                    //rankMessageHandler.calcRankResult(id);
                }
                else if(buttonName.Equals("開票"))
                    rankMessageHandler.openResultExcel(id);
            }
        }

        private void rankResultDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RankResultForm_Load(object sender, EventArgs e)
        {

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
                RankMessageHandler rankMessageHandler = (RankMessageHandler)MessagePool.getMessage(ClientMessage.RankMessage);
                int id = 0;
                if (e.Result == null)
                {
                    toolStripStatusLabel.Text = "";
                    Console.WriteLine("rank result EventArgs NULL");
                    return;
                }
                else
                {
                    id = (int)e.Result;
                    DataGridViewRow dr = rankResultDataGridView.CurrentRow;
                }
                rankMessageHandler.setToCompleteRankDataItem(id);
                toolStripStatusLabel.Text = "";
            }
        }
        public void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) 
        {
            lock (this)
            {
                RankMessageHandler rankMessageHandler = (RankMessageHandler)MessagePool.getMessage(ClientMessage.RankMessage);
                int id = (int)e.Argument;
                rankMessageHandler.calcRankResult(id);
                e.Result = id;
            }
        }
    }
}
