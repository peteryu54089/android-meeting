using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeetingSystem.Server;
using MeetingSystem.Client;
using MeetingSystem.Sync;
using System.Threading;
using MeetingSystem.Message;
using Spire.Xls;
using MeetingSystem.Utils;
using MeetingSystem.Rank;

namespace MeetingSystem
{
    public partial class MeetingSystemForm : Form
    {
        private const int BALLOON_INTERVAL = 1000;
        private const int BROADCAST_PORT = 3389;
        private const int SCREEN_SERVICE_PORT = 8080;
        private const int MEETING_SERVICE_PORT = 8081;
        private const string WEB_PATH = @"C:\inetpub\meetingWeb\web\";

        public MeetingSystemForm()
        {
            InitializeComponent();
            MeetingClient.initMessage();
            Adapter.Initialize();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(roomNameTextBox.Text))
                MessageBox.Show("請輸入會議名稱");
            else
            {
                MeetingServer.getInstance().Name = roomNameTextBox.Text;
                MeetingServer.getInstance().clientConnectedListener += onClientConnected;
                MeetingServer.getInstance().clientDisconnecteddListener += onClientDisconnected;
                MeetingServer.getInstance().start();
                this.Hide();
            }
        }
 
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // CloseAndClean();
            MeetingServer.getInstance().stop();
            MeetingServer.getInstance().release();
            EventLog.Write("close complete");
            Close();
        }

        private void onClientDisconnected(MeetingSystem.Client.Client client)
        {
            displayBubble("Client : " + client.IpAddr.ToString() + " Disconnected.");
        }

        private void onClientConnected(MeetingSystem.Client.Client client)
        {
            displayBubble("Client : " + client.IpAddr.ToString() + " Connected.");
        }

        private void displayBubble(String msg)
        {
            new Thread(new ThreadStart(() =>
            {
                Adapter.Invoke(new SendOrPostCallback(o =>
                {
                    meetingSystemNotifyIcon.BalloonTipText = msg;
                    meetingSystemNotifyIcon.ShowBalloonTip(BALLOON_INTERVAL);
                }), null);
            })) { IsBackground = true }.Start();
        }

        private void scoreTableSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreTableForm scoreTableForm = new ScoreTableForm(WEB_PATH);
            if (scoreTableForm.ShowDialog() == DialogResult.OK)
            {
                ((MeetingSystem.Message.Message)MessagePool.getMessage(ClientMessage.ScoreMessage)).StatusListener += displayBubble;
            }
        }

        private void sendFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileChooseForm fileForm = new FileChooseForm(WEB_PATH);
            if (fileForm.ShowDialog() == DialogResult.OK)
            {
                MeetingServer.getInstance().send(ClientMessage.FileMessage);
            }
        }

        private void showScoreTableStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreResultForm scoreResultForm = new ScoreResultForm();
            scoreResultForm.Show();
        }
        private void showVoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VoteResultForm voteResult = new VoteResultForm();
            voteResult.ShowDialog();
        }

        private void startVoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VoteSourceForm voteForm = new VoteSourceForm(WEB_PATH);
            if (voteForm.ShowDialog() == DialogResult.OK)
            {
                ((MeetingSystem.Message.Message)MessagePool.getMessage(ClientMessage.VoteMessage)).StatusListener += displayBubble;
            }
        }

        private void RankSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RankSourceForm rankForm = new RankSourceForm(WEB_PATH);
            rankForm.ShowDialog();
        }

        private void ShowRankResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RankResultForm resultForm = new RankResultForm();
            resultForm.ShowDialog();
        }

        private void cleanDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeetingServer.getInstance().cleanLocalFile(WEB_PATH);
            MeetingServer.getInstance().send(ClientMessage.CloseMessage); 
        }       
    }
}
