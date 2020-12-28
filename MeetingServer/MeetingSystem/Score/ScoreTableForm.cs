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
using MeetingSystem.Server;
using Spire.Xls;
using System.IO;
using MeetingSystem.Utils;

namespace MeetingSystem
{
    public partial class ScoreTableForm : Form
    {
        private static int id = 0;
        private Random random = null;
        private string WEB_PATH;

        public ScoreTableForm(string WEB_PATH)
        {
            InitializeComponent();
            random = new Random();
            this.WEB_PATH = WEB_PATH;
            scoreTableBindingSource.DataSource = MessagePool.getMessage(ClientMessage.ScoreMessage);
        }

        private void scoreTableTextBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel(.xls,.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dialog.FileName;
        }

        private void scoreDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                Object parm = MessagePool.getMessage(ClientMessage.ScoreMessage).getParm();
                BindingList<ScoreSettingDataItem> list = (BindingList<ScoreSettingDataItem>)parm;
                DataGridViewButtonCell button = (DataGridViewButtonCell)scoreDataGridView.CurrentRow.Cells["deleteButton"];
                list.RemoveAt(e.RowIndex);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
           
            const String HTML_EXT = ".html";
            String scoreTable = scoreTableTextBox.Text;
            String scoreResult = scoreResultTextBox.Text;
            if (scoreTable == "" || scoreResult == "")
                return;
            Object parm = MessagePool.getMessage(ClientMessage.ScoreMessage).getParm();
            if (parm != null)
            {
                BindingList<ScoreSettingDataItem> list = (BindingList<ScoreSettingDataItem>)parm;
                String webSourePath = DateTime.Now.Ticks.ToString() + random.Next(100) + HTML_EXT;
                ScoreSettingDataItem item = new ScoreSettingDataItem(id++, scoreTable, scoreResult, webSourePath);
                item.ScoreWebIp = String.Format("http://{0}/score.htm", MeetingServer.getInstance().IP);
                list.Add(item);
                scoreTableTextBox.Text = String.Empty;
                scoreResultTextBox.Text = String.Empty;
            }
            else
                Console.WriteLine("ScoreTableForm : parm is null.");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Object parm = MessagePool.getMessage(ClientMessage.ScoreMessage).getParm();
            if (parm != null)
            {
                BindingList<ScoreSettingDataItem> list = (BindingList<ScoreSettingDataItem>)parm;
                foreach (ScoreSettingDataItem dataItem in list)
                {
                    if (dataItem.Status == ScoreSettingDataItem.ScoreSettingStatus.NONACTIVE)
                    {
                        String webPathDir = WEB_PATH + dataItem.WebFileName;
                        Xls2Html.xlsToHtml(dataItem.ScoreSourcePath, webPathDir);
                        dataItem.Status = ScoreSettingDataItem.ScoreSettingStatus.ACTIVE;
                    }
                }
            }
            Hide();
        }
    }
}
