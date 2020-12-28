namespace MeetingSystem
{
    partial class MeetingSystemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeetingSystemForm));
            this.meetinSystemContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoreTableSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showScoreTableStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startVoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顯示投票結果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.序位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RankSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRankResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cleanDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.roomNameTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.meetingSystemNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.meetinSystemContextMenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // meetinSystemContextMenuStrip
            // 
            this.meetinSystemContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendFileToolStripMenuItem,
            this.scoreToolStripMenuItem,
            this.voteToolStripMenuItem,
            this.序位ToolStripMenuItem,
            this.toolStripSeparator2,
            this.cleanDataToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.meetinSystemContextMenuStrip.Name = "meetinSystemContextMenuStrip";
            this.meetinSystemContextMenuStrip.Size = new System.Drawing.Size(159, 142);
            // 
            // sendFileToolStripMenuItem
            // 
            this.sendFileToolStripMenuItem.Name = "sendFileToolStripMenuItem";
            this.sendFileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.sendFileToolStripMenuItem.Text = "傳送檔案";
            this.sendFileToolStripMenuItem.Click += new System.EventHandler(this.sendFileToolStripMenuItem_Click);
            // 
            // scoreToolStripMenuItem
            // 
            this.scoreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoreTableSettingToolStripMenuItem,
            this.showScoreTableStatusToolStripMenuItem});
            this.scoreToolStripMenuItem.Name = "scoreToolStripMenuItem";
            this.scoreToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.scoreToolStripMenuItem.Text = "教評會-評分";
            // 
            // scoreTableSettingToolStripMenuItem
            // 
            this.scoreTableSettingToolStripMenuItem.Name = "scoreTableSettingToolStripMenuItem";
            this.scoreTableSettingToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.scoreTableSettingToolStripMenuItem.Text = "評分表設定";
            this.scoreTableSettingToolStripMenuItem.Click += new System.EventHandler(this.scoreTableSettingToolStripMenuItem_Click);
            // 
            // showScoreTableStatusToolStripMenuItem
            // 
            this.showScoreTableStatusToolStripMenuItem.Name = "showScoreTableStatusToolStripMenuItem";
            this.showScoreTableStatusToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.showScoreTableStatusToolStripMenuItem.Text = "顯示評分狀態";
            this.showScoreTableStatusToolStripMenuItem.Click += new System.EventHandler(this.showScoreTableStatusToolStripMenuItem_Click);
            // 
            // voteToolStripMenuItem
            // 
            this.voteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startVoteToolStripMenuItem,
            this.顯示投票結果ToolStripMenuItem});
            this.voteToolStripMenuItem.Name = "voteToolStripMenuItem";
            this.voteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.voteToolStripMenuItem.Text = "投票";
            // 
            // startVoteToolStripMenuItem
            // 
            this.startVoteToolStripMenuItem.Name = "startVoteToolStripMenuItem";
            this.startVoteToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.startVoteToolStripMenuItem.Text = "投票單設定";
            this.startVoteToolStripMenuItem.Click += new System.EventHandler(this.startVoteToolStripMenuItem_Click);
            // 
            // 顯示投票結果ToolStripMenuItem
            // 
            this.顯示投票結果ToolStripMenuItem.Name = "顯示投票結果ToolStripMenuItem";
            this.顯示投票結果ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.顯示投票結果ToolStripMenuItem.Text = "顯示投票結果";
            this.顯示投票結果ToolStripMenuItem.Click += new System.EventHandler(this.showVoteToolStripMenuItem_Click);
            // 
            // 序位ToolStripMenuItem
            // 
            this.序位ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RankSourceToolStripMenuItem,
            this.ShowRankResultToolStripMenuItem});
            this.序位ToolStripMenuItem.Name = "序位ToolStripMenuItem";
            this.序位ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.序位ToolStripMenuItem.Text = "序位";
            // 
            // RankSourceToolStripMenuItem
            // 
            this.RankSourceToolStripMenuItem.Name = "RankSourceToolStripMenuItem";
            this.RankSourceToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.RankSourceToolStripMenuItem.Text = "序位單設定";
            this.RankSourceToolStripMenuItem.Click += new System.EventHandler(this.RankSourceToolStripMenuItem_Click);
            // 
            // ShowRankResultToolStripMenuItem
            // 
            this.ShowRankResultToolStripMenuItem.Name = "ShowRankResultToolStripMenuItem";
            this.ShowRankResultToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ShowRankResultToolStripMenuItem.Text = "顯示序位結果";
            this.ShowRankResultToolStripMenuItem.Click += new System.EventHandler(this.ShowRankResultToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // cleanDataToolStripMenuItem
            // 
            this.cleanDataToolStripMenuItem.Name = "cleanDataToolStripMenuItem";
            this.cleanDataToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.cleanDataToolStripMenuItem.Text = "清除平板的資料";
            this.cleanDataToolStripMenuItem.Click += new System.EventHandler(this.cleanDataToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "離開";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 62);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.roomNameTextBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.5283F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 25);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "會議名稱";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roomNameTextBox
            // 
            this.roomNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomNameTextBox.Location = new System.Drawing.Point(142, 3);
            this.roomNameTextBox.Name = "roomNameTextBox";
            this.roomNameTextBox.Size = new System.Drawing.Size(133, 22);
            this.roomNameTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.okButton.Location = new System.Drawing.Point(104, 35);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "確認";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // meetingSystemNotifyIcon
            // 
            this.meetingSystemNotifyIcon.ContextMenuStrip = this.meetinSystemContextMenuStrip;
            this.meetingSystemNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("meetingSystemNotifyIcon.Icon")));
            this.meetingSystemNotifyIcon.Text = "會議系統";
            this.meetingSystemNotifyIcon.Visible = true;
            // 
            // MeetingSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 62);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MeetingSystemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "會議系統";
            this.meetinSystemContextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip meetinSystemContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem sendFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scoreTableSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showScoreTableStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startVoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cleanDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomNameTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NotifyIcon meetingSystemNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem 顯示投票結果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 序位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RankSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowRankResultToolStripMenuItem;
    }
}