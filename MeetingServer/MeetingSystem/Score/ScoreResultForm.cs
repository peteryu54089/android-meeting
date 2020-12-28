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
    public partial class ScoreResultForm : Form, IBackgroundWorker
    {
        public ScoreResultForm()
        {
            InitializeComponent();
        }

        private void ScoreResultForm_Load(object sender, EventArgs e)
        {
            scoreResultBindingSource.DataSource = MessagePool.getMessage(ClientMessage.ScoreMessage);
        }

        private void ScoreResultDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void allbutton_Click(object sender, EventArgs e)
        {
            ScoreMessageHandler scoreMessageHandler = (ScoreMessageHandler)MessagePool.getMessage(ClientMessage.ScoreMessage);
            scoreMessageHandler.calcCurrentAllResult();
        }

        private void scoreResultBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        } 
        private void ScoreResultDataGridView_CellClick(object sender, EventArgs e)
        {

        } 
        private void ScoreResultDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string buttonName = ScoreResultDataGridView.CurrentRow.Cells["ButtonName"].Value.ToString();
                int id = Convert.ToInt32(ScoreResultDataGridView.CurrentRow.Cells["ScoreId"].Value);
                ScoreMessageHandler scoreMessageHandler = (ScoreMessageHandler)MessagePool.getMessage(ClientMessage.ScoreMessage); 
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
                }
                else if (buttonName.Equals("開票"))
                    scoreMessageHandler.openResultExcel(id);
            }
        }
        public void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (this)
            {
                ScoreMessageHandler scoreMessageHandler = (ScoreMessageHandler)MessagePool.getMessage(ClientMessage.ScoreMessage);
                int id = (int)e.Argument;
                scoreMessageHandler.calcVoteResult(id);
                e.Result = id;
            }

        }
        public void BackkgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lock (this)
            {
                ScoreMessageHandler scoreMessageHandler = (ScoreMessageHandler)MessagePool.getMessage(ClientMessage.ScoreMessage);
                int id = 0;
                if (e.Result == null)
                {
                    toolStripStatusLabel.Text = "";
                    Console.WriteLine("score result EventArgs NULL");
                    return;
                }
                else
                {
                    id = (int)e.Result;
                }
                scoreMessageHandler.setToCompleteDataItem(id);
                toolStripStatusLabel.Text = "";
            }
        }
        public void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (this)
            {
                toolStripStatusLabel.Text = "結算中.....";
            }
        }

    }
}
