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
using MeetingSystem.Structure;
using MeetingSystem.Server;
using MeetingSystem.Utils;

namespace MeetingSystem
{
    public partial class RankSourceForm : Form
    {
        private static int id = 0;
        private Random random = null;
        private string WEB_PATH;
        public RankSourceForm(string WEB_PATH)
        {
            random = new Random();
            InitializeComponent();
            this.WEB_PATH = WEB_PATH;
            scoreTableBindingSource.DataSource = MessagePool.getMessage(ClientMessage.RankMessage);
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel(.xls,.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dialog.FileName;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            const String HTML_EXT = ".html";
            String rankSource = rankSourceTextBox.Text;
            String rankResult = rankResultTextBox.Text;
            if (rankSource == "" || rankResult == "")
                return;
            Object parm = MessagePool.getMessage(ClientMessage.RankMessage).getParm();
            if (parm != null)
            {
                BindingList<RankSettingDataItem> list = (BindingList<RankSettingDataItem>)parm;
                String webSourePath = DateTime.Now.Ticks.ToString() + random.Next(100) + HTML_EXT;
                RankSettingDataItem item = new RankSettingDataItem(id++, rankSource, rankResult, webSourePath);
                item.RankWebIp = String.Format("http://{0}/rank.htm", MeetingServer.getInstance().IP);
                list.Add(item);
                rankSourceTextBox.Text = String.Empty;
                rankResultTextBox.Text = String.Empty;
            }
            else
                Console.WriteLine("RankSourceForm : parm is null.");

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Object parm = MessagePool.getMessage(ClientMessage.RankMessage).getParm();
            if (parm != null)
            {
                BindingList<RankSettingDataItem> list = (BindingList<RankSettingDataItem>)parm;
                foreach (RankSettingDataItem rank in list)
                {
                    if (rank.Status == RankSettingDataItem.RankSettingStatus.NONACTIVE)
                    {
                        String webPathDir = WEB_PATH + rank.WebFileName;
                        Xls2Html.xlsToHtml(rank.RankSourcePath, webPathDir);
                        rank.Status = RankSettingDataItem.RankSettingStatus.ACTIVE;
                    }
                }
            }
            Hide();
        }

        private void scoreDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                Object parm = MessagePool.getMessage(ClientMessage.RankMessage).getParm();
                BindingList<RankSettingDataItem> list = (BindingList<RankSettingDataItem>)parm;
                DataGridViewButtonCell button = (DataGridViewButtonCell)scoreDataGridView.CurrentRow.Cells["deleteButton"];
                list.RemoveAt(e.RowIndex);
            }
        }
    }
}
