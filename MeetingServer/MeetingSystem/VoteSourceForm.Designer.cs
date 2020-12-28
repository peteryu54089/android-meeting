namespace MeetingSystem
{
    partial class VoteSourceForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.voteResultTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.scoreDataGridView = new System.Windows.Forms.DataGridView();
            this.voteSourcePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voteReslutPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.scoreTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.voteTableTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTableBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 433);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 33);
            this.panel1.TabIndex = 2;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(342, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(112, 24);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "確定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "投票檔";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(370, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "投票結果";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // voteResultTextBox
            // 
            this.voteResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.voteResultTextBox.Location = new System.Drawing.Point(440, 3);
            this.voteResultTextBox.Name = "voteResultTextBox";
            this.voteResultTextBox.Size = new System.Drawing.Size(289, 22);
            this.voteResultTextBox.TabIndex = 2;
            this.voteResultTextBox.Click += new System.EventHandler(this.scoreResultTextBox_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.scoreDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 469);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // scoreDataGridView
            // 
            this.scoreDataGridView.AllowUserToAddRows = false;
            this.scoreDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoreDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.scoreDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scoreDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.voteSourcePathDataGridViewTextBoxColumn,
            this.voteReslutPathDataGridViewTextBoxColumn,
            this.deleteButton});
            this.scoreDataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.scoreDataGridView.DataSource = this.scoreTableBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scoreDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.scoreDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoreDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.scoreDataGridView.Location = new System.Drawing.Point(3, 35);
            this.scoreDataGridView.MultiSelect = false;
            this.scoreDataGridView.Name = "scoreDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoreDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.scoreDataGridView.RowHeadersVisible = false;
            this.scoreDataGridView.RowTemplate.Height = 24;
            this.scoreDataGridView.Size = new System.Drawing.Size(807, 392);
            this.scoreDataGridView.TabIndex = 1;
            this.scoreDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.scoreDataGridView_CellClick);
            // 
            // voteSourcePathDataGridViewTextBoxColumn
            // 
            this.voteSourcePathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.voteSourcePathDataGridViewTextBoxColumn.DataPropertyName = "VoteSourcePath";
            this.voteSourcePathDataGridViewTextBoxColumn.HeaderText = "投票檔";
            this.voteSourcePathDataGridViewTextBoxColumn.Name = "voteSourcePathDataGridViewTextBoxColumn";
            // 
            // voteReslutPathDataGridViewTextBoxColumn
            // 
            this.voteReslutPathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.voteReslutPathDataGridViewTextBoxColumn.DataPropertyName = "VoteReslutPath";
            this.voteReslutPathDataGridViewTextBoxColumn.HeaderText = "投票結果";
            this.voteReslutPathDataGridViewTextBoxColumn.Name = "voteReslutPathDataGridViewTextBoxColumn";
            // 
            // deleteButton
            // 
            this.deleteButton.HeaderText = "動作";
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Text = "刪除";
            this.deleteButton.UseColumnTextForButtonValue = true;
            // 
            // scoreTableBindingSource
            // 
            this.scoreTableBindingSource.DataMember = "VoteSettingDataItems";
            this.scoreTableBindingSource.DataSource = typeof(MeetingSystem.Message.VoteMessageHandler);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(807, 26);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.332454F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.05013F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.707124F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.54354F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.voteResultTextBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.voteTableTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.addButton, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(807, 26);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // voteTableTextBox
            // 
            this.voteTableTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.voteTableTextBox.Location = new System.Drawing.Point(54, 3);
            this.voteTableTextBox.Name = "voteTableTextBox";
            this.voteTableTextBox.Size = new System.Drawing.Size(310, 22);
            this.voteTableTextBox.TabIndex = 3;
            this.voteTableTextBox.Click += new System.EventHandler(this.voteTableTextBox_Click);
            // 
            // addButton
            // 
            this.addButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addButton.Location = new System.Drawing.Point(735, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(69, 20);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "新增";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // VoteSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VoteSourceForm";
            this.Text = "VoteSourceForm";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scoreDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTableBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox voteResultTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView scoreDataGridView;
        private System.Windows.Forms.BindingSource scoreTableBindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox voteTableTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn voteSourcePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn voteReslutPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
    }
}