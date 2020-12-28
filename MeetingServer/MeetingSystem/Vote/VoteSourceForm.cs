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
using MeetingSystem.Utils;

namespace MeetingSystem
{
    public partial class VoteSourceForm : Form
    {
        private Random random = null;
        private static int id = 0;
        private String WEB_PATH;
        public VoteSourceForm(String WEB_PATH)
        {
            random = new Random();
            InitializeComponent();
            this.WEB_PATH = WEB_PATH;
            scoreTableBindingSource.DataSource = MessagePool.getMessage(ClientMessage.VoteMessage);
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Object parm = MessagePool.getMessage(ClientMessage.VoteMessage).getParm();
            if (parm != null)
            {
                BindingList<VoteSettingDataItem> list = (BindingList<VoteSettingDataItem>)parm;
                foreach (VoteSettingDataItem dataItem in list)
                {
                    if (dataItem.Status == VoteSettingDataItem.VoteSettingStatus.NONACTIVE)
                    {
                        String webPathDir = WEB_PATH + dataItem.WebFileName;
                        Xls2Html.xlsToHtml(dataItem.VoteSourcePath, webPathDir);
                        dataItem.Status = VoteSettingDataItem.VoteSettingStatus.ACTIVE;
                    }
                }
            }
            Hide();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            
            const String HTML_EXT = ".html";
            String voteTable = voteTableTextBox.Text;
            String voteResult = voteResultTextBox.Text;
            bool order = OrderByCk.Checked;
            if (voteTable.Equals(String.Empty) || voteResult.Equals(String.Empty) )
                return;
            Object parm = MessagePool.getMessage(ClientMessage.VoteMessage).getParm();
            if (parm != null)
            {
                BindingList<VoteSettingDataItem> list = (BindingList<VoteSettingDataItem>)parm;
                String webSourcePath = DateTime.Now.Ticks.ToString() + random.Next(100) + HTML_EXT;
                String voteWebIp = String.Format("http://{0}/Vote.html", MeetingServer.getInstance().IP);
                VoteSettingDataItem item = new VoteSettingDataItem(id++, voteTable, voteResult, voteWebIp, webSourcePath, order);
                list.Add(item);
                voteTableTextBox.Text = String.Empty;
                voteResultTextBox.Text = String.Empty;
                OrderByCk.Checked = false;
            }
            else
            {
                Console.WriteLine("VoteSourceForm : parm is null.");
            }
        }
        private void voteTableTextBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel(.xls,.xlsx)|*.xls;*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
                ((TextBox)sender).Text = dialog.FileName;
        }
       
        private void scoreDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                Object parm = MessagePool.getMessage(ClientMessage.VoteMessage).getParm();
                BindingList<VoteSettingDataItem> list = (BindingList<VoteSettingDataItem>)parm;
                DataGridViewButtonCell button = (DataGridViewButtonCell)scoreDataGridView.CurrentRow.Cells["deleteButton"];
                list.RemoveAt(e.RowIndex);
            }
        }

       
    }
}
