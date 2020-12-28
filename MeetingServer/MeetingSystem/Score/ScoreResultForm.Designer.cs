namespace MeetingSystem
{
    partial class ScoreResultForm
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
            this.ScoreResultDataGridView = new System.Windows.Forms.DataGridView();
            this.ScoreId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scoreDataItemCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.scoreResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreResultDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreResultBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ScoreResultDataGridView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(673, 285);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ScoreResultDataGridView
            // 
            this.ScoreResultDataGridView.AllowUserToAddRows = false;
            this.ScoreResultDataGridView.AllowUserToDeleteRows = false;
            this.ScoreResultDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScoreResultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ScoreResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoreResultDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ScoreId,
            this.tableNameDataGridViewTextBoxColumn,
            this.scoreDataItemCountDataGridViewTextBoxColumn,
            this.ScoreStatus,
            this.ButtonName});
            this.ScoreResultDataGridView.DataSource = this.scoreResultBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ScoreResultDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ScoreResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScoreResultDataGridView.Location = new System.Drawing.Point(3, 3);
            this.ScoreResultDataGridView.Name = "ScoreResultDataGridView";
            this.ScoreResultDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScoreResultDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ScoreResultDataGridView.RowHeadersVisible = false;
            this.ScoreResultDataGridView.RowTemplate.Height = 24;
            this.ScoreResultDataGridView.Size = new System.Drawing.Size(667, 259);
            this.ScoreResultDataGridView.TabIndex = 1;
            this.ScoreResultDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScoreResultDataGridView_CellContentClick);
            this.ScoreResultDataGridView.Click += new System.EventHandler(this.ScoreResultDataGridView_CellClick);
            // 
            // ScoreId
            // 
            this.ScoreId.DataPropertyName = "ScoreId";
            this.ScoreId.HeaderText = "ScoreId";
            this.ScoreId.Name = "ScoreId";
            this.ScoreId.ReadOnly = true;
            this.ScoreId.Visible = false;
            // 
            // tableNameDataGridViewTextBoxColumn
            // 
            this.tableNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tableNameDataGridViewTextBoxColumn.DataPropertyName = "TableName";
            this.tableNameDataGridViewTextBoxColumn.FillWeight = 65F;
            this.tableNameDataGridViewTextBoxColumn.HeaderText = "評分表名稱";
            this.tableNameDataGridViewTextBoxColumn.Name = "tableNameDataGridViewTextBoxColumn";
            this.tableNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scoreDataItemCountDataGridViewTextBoxColumn
            // 
            this.scoreDataItemCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.scoreDataItemCountDataGridViewTextBoxColumn.DataPropertyName = "ScoreDataItemCount";
            this.scoreDataItemCountDataGridViewTextBoxColumn.FillWeight = 20F;
            this.scoreDataItemCountDataGridViewTextBoxColumn.HeaderText = "已投票人數";
            this.scoreDataItemCountDataGridViewTextBoxColumn.Name = "scoreDataItemCountDataGridViewTextBoxColumn";
            this.scoreDataItemCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ScoreStatus
            // 
            this.ScoreStatus.DataPropertyName = "ScoreStatus";
            this.ScoreStatus.HeaderText = "狀態";
            this.ScoreStatus.Name = "ScoreStatus";
            this.ScoreStatus.ReadOnly = true;
            // 
            // ButtonName
            // 
            this.ButtonName.DataPropertyName = "ButtonName";
            this.ButtonName.FillWeight = 15F;
            this.ButtonName.HeaderText = "";
            this.ButtonName.Name = "ButtonName";
            this.ButtonName.ReadOnly = true;
            this.ButtonName.Text = "";
            // 
            // scoreResultBindingSource
            // 
            this.scoreResultBindingSource.DataMember = "ScoreDataPairs";
            this.scoreResultBindingSource.DataSource = typeof(MeetingSystem.Message.ScoreMessageHandler);
            this.scoreResultBindingSource.CurrentChanged += new System.EventHandler(this.scoreResultBindingSource_CurrentChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(673, 20);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 15);
            // 
            // ScoreResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 285);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ScoreResultForm";
            this.Text = "ScoreResultForm";
            this.Load += new System.EventHandler(this.ScoreResultForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreResultDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreResultBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource scoreResultBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView ScoreResultDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreId;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scoreDataItemCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreStatus;
        private System.Windows.Forms.DataGridViewButtonColumn ButtonName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;

    }
}