namespace MeetingSystem
{
    partial class VoteResultForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.voteResultDataGridView = new System.Windows.Forms.DataGridView();
            this.VoteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataSourceNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voteDataItemCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voteSettingStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voteResultDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.voteResultDataGridView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(677, 323);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // voteResultDataGridView
            // 
            this.voteResultDataGridView.AutoGenerateColumns = false;
            this.voteResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.voteResultDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VoteId,
            this.dataSourceNameDataGridViewTextBoxColumn,
            this.voteDataItemCountDataGridViewTextBoxColumn,
            this.voteSettingStatusDataGridViewTextBoxColumn,
            this.ButtonName});
            this.voteResultDataGridView.DataSource = this.voteBindingSource;
            this.voteResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.voteResultDataGridView.Location = new System.Drawing.Point(3, 3);
            this.voteResultDataGridView.Name = "voteResultDataGridView";
            this.voteResultDataGridView.RowTemplate.Height = 24;
            this.voteResultDataGridView.Size = new System.Drawing.Size(671, 297);
            this.voteResultDataGridView.TabIndex = 1;
            this.voteResultDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.voteResultDataGridView_CellClick);
            // 
            // VoteId
            // 
            this.VoteId.DataPropertyName = "VoteId";
            this.VoteId.HeaderText = "VoteId";
            this.VoteId.Name = "VoteId";
            this.VoteId.ReadOnly = true;
            this.VoteId.Visible = false;
            // 
            // ButtonName
            // 
            this.ButtonName.DataPropertyName = "ButtonName";
            this.ButtonName.HeaderText = "";
            this.ButtonName.Name = "ButtonName";
            this.ButtonName.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(677, 20);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 15);
            // 
            // dataSourceNameDataGridViewTextBoxColumn
            // 
            this.dataSourceNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataSourceNameDataGridViewTextBoxColumn.DataPropertyName = "DataSourceName";
            this.dataSourceNameDataGridViewTextBoxColumn.HeaderText = "投票單名稱";
            this.dataSourceNameDataGridViewTextBoxColumn.Name = "dataSourceNameDataGridViewTextBoxColumn";
            this.dataSourceNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // voteDataItemCountDataGridViewTextBoxColumn
            // 
            this.voteDataItemCountDataGridViewTextBoxColumn.DataPropertyName = "VoteDataItemCount";
            this.voteDataItemCountDataGridViewTextBoxColumn.HeaderText = "人數";
            this.voteDataItemCountDataGridViewTextBoxColumn.Name = "voteDataItemCountDataGridViewTextBoxColumn";
            this.voteDataItemCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // voteSettingStatusDataGridViewTextBoxColumn
            // 
            this.voteSettingStatusDataGridViewTextBoxColumn.DataPropertyName = "VoteSettingStatus";
            this.voteSettingStatusDataGridViewTextBoxColumn.HeaderText = "狀態";
            this.voteSettingStatusDataGridViewTextBoxColumn.Name = "voteSettingStatusDataGridViewTextBoxColumn";
            this.voteSettingStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // voteBindingSource
            // 
            this.voteBindingSource.DataMember = "VoteDataPairs";
            this.voteBindingSource.DataSource = typeof(MeetingSystem.Message.VoteMessageHandler);
            // 
            // VoteResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 323);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VoteResultForm";
            this.Text = "VoteResultForm";
            this.Load += new System.EventHandler(this.VoteResultForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voteResultDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView voteResultDataGridView;
        private System.Windows.Forms.BindingSource voteBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataSourceNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voteDataItemCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voteSettingStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ButtonName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}