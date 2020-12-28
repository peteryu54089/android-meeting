using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeetingSystem.Client;
using MeetingSystem.Message;
using MeetingSystem.Server;

namespace MeetingSystem
{
    public partial class FileChooseForm : Form
    {
        string WEB_PATH = "";
        public FileChooseForm(string WEB_PATH)
        {
            InitializeComponent();
            fileBindingSource.DataSource = MessagePool.getMessage(ClientMessage.FileMessage);
            this.WEB_PATH = WEB_PATH+"file\\";
        }
        private string getFileName(string filePath)
        {
            int index=filePath.LastIndexOf('/');
            return filePath.Substring(index);
        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Object parm = MessagePool.getMessage(ClientMessage.FileMessage).getParm();
            BindingList<FileDataItem> fileList = (BindingList<FileDataItem>)parm;
            if (!System.IO.Directory.Exists(WEB_PATH))
            {
                System.IO.Directory.CreateDirectory(WEB_PATH);
            }
            
            foreach (FileDataItem fileData in fileList)
            {
                string fileName = getFileName(fileData.WebPath);
                string dest = WEB_PATH + fileName;
                System.IO.File.Copy(fileData.FilePath, dest, true);
                while (!System.IO.File.Exists(dest)) ;
            }
            Hide();
        }

        private void fileSourceTextBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "文件|*.pdf;*.doc;*.ppt;*.pptx;*.docx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String text = "";
                foreach (String fileName in dialog.FileNames)
                {
                    text += fileName + ";";
                }
                ((TextBox)sender).Text = text.Remove(text.Length - 1);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (fileSourceTextBox.Text == "")
                return;
            /*if (System.IO.File.Exists(fileSourceTextBox.Text) == false)
                return;*/
            Object parm = MessagePool.getMessage(ClientMessage.FileMessage).getParm();
            if (parm != null)
            {
                int flowNum = 0;
                String[] paths = fileSourceTextBox.Text.Split(';');
                foreach (String path in paths)
                {
                    String fileName = path.Substring(path.LastIndexOf(@"\")).Replace("\\", "");
                    String ext = fileName.Substring(fileName.LastIndexOf("."));
                    BindingList<FileDataItem> list = (BindingList<FileDataItem>)parm;
                    FileDataItem file = new FileDataItem();
                    file.FilePath = path;
                    //String path = String.Format("http://{0}/web/file/{1}", MeetingServer.getInstance().IP, fileName);
                    file.WebPath = String.Format("http://{0}/web/file/{1}", MeetingServer.getInstance().IP, System.DateTime.Now.ToString("yyyyMMdd") + (flowNum++) + ext);
                    list.Add(file);
                    EventLog.Write("file name update "+file.WebPath);
                }
                fileSourceTextBox.Text = String.Empty;
            }
            else
            {
                Console.WriteLine("FileChooseForm:parm is null");
                EventLog.Write("FileChooseForm:parm is null");
            }
        }

        private void fileDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Object parm = MessagePool.getMessage(ClientMessage.FileMessage).getParm();
                if (parm != null)
                {
                    BindingList<FileDataItem> list = (BindingList<FileDataItem>)parm;
                    list.RemoveAt(e.RowIndex);
                }
            }
        } 
       
    }
}
