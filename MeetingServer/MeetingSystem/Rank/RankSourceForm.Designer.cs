namespace MeetingSystem
{
    partial class RankSourceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rankResultTextBox = new System.Windows.Forms.TextBox();
            this.rankSourceTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.scoreDataGridView = new System.Windows.Forms.DataGridView();
            this.rankSourcePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankResultPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.scoreTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTableBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.rankResultTextBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.rankSourceTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.addButton, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(797, 25);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "序位表";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(365, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "序位結果";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rankResultTextBox
            // 
            this.rankResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rankResultTextBox.Location = new System.Drawing.Point(434, 3);
            this.rankResultTextBox.Name = "rankResultTextBox";
            this.rankResultTextBox.Size = new System.Drawing.Size(286, 22);
            this.rankResultTextBox.TabIndex = 2;
            this.rankResultTextBox.Click += new System.EventHandler(this.TextBox_Click);
            // 
            // rankSourceTextBox
            // 
            this.rankSourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rankSourceTextBox.Location = new System.Drawing.Point(53, 3);
            this.rankSourceTextBox.Name = "rankSourceTextBox";
            this.rankSourceTextBox.Size = new System.Drawing.Size(306, 22);
            this.rankSourceTextBox.TabIndex = 3;
            this.rankSourceTextBox.Click += new System.EventHandler(this.TextBox_Click);
            // 
            // addButton
            // 
            this.addButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addButton.Location = new System.Drawing.Point(726, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(68, 19);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "新增";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // scoreDataGridView
            // 
            this.scoreDataGridView.AllowUserToAddRows = false;
            this.scoreDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoreDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.scoreDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scoreDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rankSourcePathDataGridViewTextBoxColumn,
            this.rankResultPathDataGridViewTextBoxColumn,
            this.deleteButton});
            this.scoreDataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.scoreDataGridView.DataSource = this.scoreTableBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scoreDataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.scoreDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scoreDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.scoreDataGridView.Location = new System.Drawing.Point(3, 34);
            this.scoreDataGridView.MultiSelect = false;
            this.scoreDataGridView.Name = "scoreDataGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scoreDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.scoreDataGridView.RowHeadersVisible = false;
            this.scoreDataGridView.RowTemplate.Height = 24;
            this.scoreDataGridView.Size = new System.Drawing.Size(797, 379);
            this.scoreDataGridView.TabIndex = 1;
            this.scoreDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.scoreDataGridView_CellClick);
            // 
            // rankSourcePathDataGridViewTextBoxColumn
            // 
            this.rankSourcePathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rankSourcePathDataGridViewTextBoxColumn.DataPropertyName = "RankSourcePath";
            this.rankSourcePathDataGridViewTextBoxColumn.HeaderText = "序位表";
            this.rankSourcePathDataGridViewTextBoxColumn.Name = "rankSourcePathDataGridViewTextBoxColumn";
            // 
            // rankResultPathDataGridViewTextBoxColumn
            // 
            this.rankResultPathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.rankResultPathDataGridViewTextBoxColumn.DataPropertyName = "RankResultPath";
            this.rankResultPathDataGridViewTextBoxColumn.HeaderText = "序位結果";
            this.rankResultPathDataGridViewTextBoxColumn.Name = "rankResultPathDataGridViewTextBoxColumn";
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
            this.scoreTableBindingSource.DataMember = "SettingDataItems";
            this.scoreTableBindingSource.DataSource = typeof(MeetingSystem.Message.RankMessageHandler);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(0, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(797, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "確定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(797, 31);
            this.panel1.TabIndex = 2;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 453);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 25);
            this.panel2.TabIndex = 3;
            // 
            // RankSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RankSourceForm";
            this.Text = "RankSourceForm";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scoreDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoreTableBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rankResultTextBox;
        private System.Windows.Forms.TextBox rankSourceTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridView scoreDataGridView;
        private System.Windows.Forms.BindingSource scoreTableBindingSource;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankSourcePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankResultPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
    }
}