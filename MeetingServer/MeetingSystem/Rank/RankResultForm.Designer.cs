namespace MeetingSystem.Rank
{
    partial class RankResultForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rankResultDataGridView = new System.Windows.Forms.DataGridView();
            this.ButtonName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RankId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataItemCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankSettingStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rankResultDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voteBindingSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rankResultDataGridView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 335);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rankResultDataGridView
            // 
            this.rankResultDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rankResultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rankResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rankResultDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RankId,
            this.tableNameDataGridViewTextBoxColumn,
            this.rankDataItemCountDataGridViewTextBoxColumn,
            this.rankSettingStatusDataGridViewTextBoxColumn,
            this.ButtonName});
            this.rankResultDataGridView.DataSource = this.voteBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rankResultDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.rankResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rankResultDataGridView.Location = new System.Drawing.Point(3, 3);
            this.rankResultDataGridView.Name = "rankResultDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rankResultDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.rankResultDataGridView.RowTemplate.Height = 24;
            this.rankResultDataGridView.Size = new System.Drawing.Size(754, 309);
            this.rankResultDataGridView.TabIndex = 1;
            this.rankResultDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.rankResultDataGridView_CellClick);
            this.rankResultDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.rankResultDataGridView_CellContentClick);
            // 
            // ButtonName
            // 
            this.ButtonName.DataPropertyName = "ButtonName";
            this.ButtonName.HeaderText = "";
            this.ButtonName.Name = "ButtonName";
            this.ButtonName.ReadOnly = true;
            // 
            // RankId
            // 
            this.RankId.DataPropertyName = "RankId";
            this.RankId.HeaderText = "RankId";
            this.RankId.Name = "RankId";
            this.RankId.ReadOnly = true;
            this.RankId.Visible = false;
            // 
            // tableNameDataGridViewTextBoxColumn
            // 
            this.tableNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tableNameDataGridViewTextBoxColumn.DataPropertyName = "TableName";
            this.tableNameDataGridViewTextBoxColumn.HeaderText = "序位單名稱";
            this.tableNameDataGridViewTextBoxColumn.Name = "tableNameDataGridViewTextBoxColumn";
            this.tableNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rankDataItemCountDataGridViewTextBoxColumn
            // 
            this.rankDataItemCountDataGridViewTextBoxColumn.DataPropertyName = "RankDataItemCount";
            this.rankDataItemCountDataGridViewTextBoxColumn.HeaderText = "人數";
            this.rankDataItemCountDataGridViewTextBoxColumn.Name = "rankDataItemCountDataGridViewTextBoxColumn";
            this.rankDataItemCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rankSettingStatusDataGridViewTextBoxColumn
            // 
            this.rankSettingStatusDataGridViewTextBoxColumn.DataPropertyName = "RankSettingStatus";
            this.rankSettingStatusDataGridViewTextBoxColumn.HeaderText = "狀態";
            this.rankSettingStatusDataGridViewTextBoxColumn.Name = "rankSettingStatusDataGridViewTextBoxColumn";
            this.rankSettingStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // voteBindingSource
            // 
            this.voteBindingSource.DataMember = "RankDataPairs";
            this.voteBindingSource.DataSource = typeof(MeetingSystem.Message.RankMessageHandler);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 315);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(760, 20);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 15);
            // 
            // RankResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 335);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RankResultForm";
            this.Text = "RankResultForm";
            this.Load += new System.EventHandler(this.RankResultForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rankResultDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voteBindingSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView rankResultDataGridView;
        private System.Windows.Forms.BindingSource voteBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn RankId;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataItemCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankSettingStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ButtonName;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}