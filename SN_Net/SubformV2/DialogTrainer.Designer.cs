namespace SN_Net.Subform
{
    partial class DialogTrainer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTrainer = new CC.XDatagrid();
            this.col_training_calendar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_course_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_trainer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_code_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_term = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStat = new CC.XDatagrid();
            this.col_stat_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_first_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_last_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_code_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_train_dates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_assist_dates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_train_dates_str = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_stat_assist_dates_str = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inlineCourseType = new CC.XDropdownList();
            this.inlineTrainer = new CC.XDropdownList();
            this.inlineStatus = new CC.XDropdownList();
            this.inlineTerm = new CC.XDropdownList();
            this.inlineRemark = new CC.XTextEdit();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 62);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(657, 352);
            this.tabControl1.TabIndex = 122;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.inlineRemark);
            this.tabPage1.Controls.Add(this.inlineTerm);
            this.tabPage1.Controls.Add(this.inlineStatus);
            this.tabPage1.Controls.Add(this.inlineTrainer);
            this.tabPage1.Controls.Add(this.inlineCourseType);
            this.tabPage1.Controls.Add(this.dgvTrainer);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(649, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "วิทยากร และ ผู้ช่วยในวันนี้ <F8>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvStat);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(649, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "สถิติรายบุคคลในเดือนนี้ <F7>";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtDate
            // 
            this.dtDate.Checked = false;
            this.dtDate.CustomFormat = "dddd, dd/MM/yyyy";
            this.dtDate.Enabled = false;
            this.dtDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDate.Location = new System.Drawing.Point(55, 19);
            this.dtDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(194, 23);
            this.dtDate.TabIndex = 121;
            this.dtDate.Value = new System.DateTime(2015, 10, 9, 16, 11, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 120;
            this.label1.Text = "วันที่";
            // 
            // dgvTrainer
            // 
            this.dgvTrainer.AllowSortByColumnHeaderClicked = false;
            this.dgvTrainer.AllowUserToAddRows = false;
            this.dgvTrainer.AllowUserToDeleteRows = false;
            this.dgvTrainer.AllowUserToResizeColumns = false;
            this.dgvTrainer.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTrainer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTrainer.ColumnHeadersHeight = 28;
            this.dgvTrainer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTrainer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_training_calendar,
            this.col_seq,
            this.col_date,
            this.col_course_type,
            this.col_trainer,
            this.col_name,
            this.col_code_name,
            this.col_status,
            this.col_term,
            this.col_remark});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTrainer.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTrainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrainer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvTrainer.EnableHeadersVisualStyles = false;
            this.dgvTrainer.FillEmptyRow = false;
            this.dgvTrainer.FocusedRowBorderRedLine = true;
            this.dgvTrainer.Location = new System.Drawing.Point(0, 0);
            this.dgvTrainer.MultiSelect = false;
            this.dgvTrainer.Name = "dgvTrainer";
            this.dgvTrainer.ReadOnly = true;
            this.dgvTrainer.RowHeadersVisible = false;
            this.dgvTrainer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTrainer.RowTemplate.Height = 26;
            this.dgvTrainer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrainer.Size = new System.Drawing.Size(649, 323);
            this.dgvTrainer.StandardTab = true;
            this.dgvTrainer.TabIndex = 119;
            this.dgvTrainer.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvTrainer_CellPainting);
            this.dgvTrainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvTrainer_MouseClick);
            // 
            // col_training_calendar
            // 
            this.col_training_calendar.DataPropertyName = "training_calendar";
            this.col_training_calendar.HeaderText = "Training Calendar";
            this.col_training_calendar.Name = "col_training_calendar";
            this.col_training_calendar.ReadOnly = true;
            this.col_training_calendar.Visible = false;
            // 
            // col_seq
            // 
            this.col_seq.HeaderText = "ลำดับ";
            this.col_seq.MinimumWidth = 45;
            this.col_seq.Name = "col_seq";
            this.col_seq.ReadOnly = true;
            this.col_seq.Width = 45;
            // 
            // col_date
            // 
            this.col_date.DataPropertyName = "date";
            this.col_date.HeaderText = "Date";
            this.col_date.Name = "col_date";
            this.col_date.ReadOnly = true;
            this.col_date.Visible = false;
            // 
            // col_course_type
            // 
            this.col_course_type.DataPropertyName = "course_type";
            this.col_course_type.HeaderText = "คอร์สอบรม";
            this.col_course_type.MinimumWidth = 90;
            this.col_course_type.Name = "col_course_type";
            this.col_course_type.ReadOnly = true;
            this.col_course_type.Width = 90;
            // 
            // col_trainer
            // 
            this.col_trainer.DataPropertyName = "trainer";
            this.col_trainer.HeaderText = "Trainer";
            this.col_trainer.Name = "col_trainer";
            this.col_trainer.ReadOnly = true;
            this.col_trainer.Visible = false;
            // 
            // col_name
            // 
            this.col_name.DataPropertyName = "name";
            this.col_name.HeaderText = "Name";
            this.col_name.Name = "col_name";
            this.col_name.ReadOnly = true;
            this.col_name.Visible = false;
            // 
            // col_code_name
            // 
            this.col_code_name.DataPropertyName = "code_name";
            this.col_code_name.HeaderText = "รหัส : ชื่อ";
            this.col_code_name.MinimumWidth = 110;
            this.col_code_name.Name = "col_code_name";
            this.col_code_name.ReadOnly = true;
            this.col_code_name.Width = 110;
            // 
            // col_status
            // 
            this.col_status.DataPropertyName = "status";
            this.col_status.HeaderText = "สถานะ";
            this.col_status.MinimumWidth = 80;
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Width = 80;
            // 
            // col_term
            // 
            this.col_term.DataPropertyName = "term";
            this.col_term.HeaderText = "ช่วงเวลา";
            this.col_term.MinimumWidth = 80;
            this.col_term.Name = "col_term";
            this.col_term.ReadOnly = true;
            this.col_term.Width = 80;
            // 
            // col_remark
            // 
            this.col_remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_remark.DataPropertyName = "remark";
            this.col_remark.HeaderText = "หมายเหตุ";
            this.col_remark.Name = "col_remark";
            this.col_remark.ReadOnly = true;
            // 
            // dgvStat
            // 
            this.dgvStat.AllowSortByColumnHeaderClicked = false;
            this.dgvStat.AllowUserToAddRows = false;
            this.dgvStat.AllowUserToDeleteRows = false;
            this.dgvStat.AllowUserToResizeColumns = false;
            this.dgvStat.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStat.ColumnHeadersHeight = 28;
            this.dgvStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_stat_user,
            this.col_stat_seq,
            this.col_stat_first_date,
            this.col_stat_last_date,
            this.col_stat_code_name,
            this.col_stat_train_dates,
            this.col_stat_assist_dates,
            this.col_stat_train_dates_str,
            this.col_stat_assist_dates_str});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStat.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStat.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvStat.EnableHeadersVisualStyles = false;
            this.dgvStat.FillEmptyRow = false;
            this.dgvStat.FocusedRowBorderRedLine = true;
            this.dgvStat.Location = new System.Drawing.Point(3, 4);
            this.dgvStat.MultiSelect = false;
            this.dgvStat.Name = "dgvStat";
            this.dgvStat.ReadOnly = true;
            this.dgvStat.RowHeadersVisible = false;
            this.dgvStat.RowTemplate.Height = 26;
            this.dgvStat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStat.Size = new System.Drawing.Size(643, 315);
            this.dgvStat.StandardTab = true;
            this.dgvStat.TabIndex = 0;
            this.dgvStat.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvStat_CellPainting);
            // 
            // col_stat_user
            // 
            this.col_stat_user.DataPropertyName = "user";
            this.col_stat_user.HeaderText = "User";
            this.col_stat_user.Name = "col_stat_user";
            this.col_stat_user.ReadOnly = true;
            this.col_stat_user.Visible = false;
            // 
            // col_stat_seq
            // 
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.col_stat_seq.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_stat_seq.HeaderText = "ลำดับ";
            this.col_stat_seq.MinimumWidth = 45;
            this.col_stat_seq.Name = "col_stat_seq";
            this.col_stat_seq.ReadOnly = true;
            this.col_stat_seq.Width = 45;
            // 
            // col_stat_first_date
            // 
            this.col_stat_first_date.DataPropertyName = "first_date_of_month";
            this.col_stat_first_date.HeaderText = "First date of month";
            this.col_stat_first_date.Name = "col_stat_first_date";
            this.col_stat_first_date.ReadOnly = true;
            this.col_stat_first_date.Visible = false;
            // 
            // col_stat_last_date
            // 
            this.col_stat_last_date.DataPropertyName = "last_date_of_month";
            this.col_stat_last_date.HeaderText = "Last date of month";
            this.col_stat_last_date.Name = "col_stat_last_date";
            this.col_stat_last_date.ReadOnly = true;
            this.col_stat_last_date.Visible = false;
            // 
            // col_stat_code_name
            // 
            this.col_stat_code_name.DataPropertyName = "code_name";
            this.col_stat_code_name.HeaderText = "รหัส : ชื่อ";
            this.col_stat_code_name.MinimumWidth = 110;
            this.col_stat_code_name.Name = "col_stat_code_name";
            this.col_stat_code_name.ReadOnly = true;
            this.col_stat_code_name.Width = 110;
            // 
            // col_stat_train_dates
            // 
            this.col_stat_train_dates.DataPropertyName = "train_dates";
            this.col_stat_train_dates.HeaderText = "Train dates";
            this.col_stat_train_dates.Name = "col_stat_train_dates";
            this.col_stat_train_dates.ReadOnly = true;
            this.col_stat_train_dates.Visible = false;
            // 
            // col_stat_assist_dates
            // 
            this.col_stat_assist_dates.DataPropertyName = "assist_dates";
            this.col_stat_assist_dates.HeaderText = "Assist dates";
            this.col_stat_assist_dates.Name = "col_stat_assist_dates";
            this.col_stat_assist_dates.ReadOnly = true;
            this.col_stat_assist_dates.Visible = false;
            // 
            // col_stat_train_dates_str
            // 
            this.col_stat_train_dates_str.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_stat_train_dates_str.DataPropertyName = "train_dates_str";
            this.col_stat_train_dates_str.HeaderText = "เป็นวิทยากร";
            this.col_stat_train_dates_str.Name = "col_stat_train_dates_str";
            this.col_stat_train_dates_str.ReadOnly = true;
            // 
            // col_stat_assist_dates_str
            // 
            this.col_stat_assist_dates_str.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_stat_assist_dates_str.DataPropertyName = "assist_dates_str";
            this.col_stat_assist_dates_str.HeaderText = "เป็นผู้ช่วย";
            this.col_stat_assist_dates_str.Name = "col_stat_assist_dates_str";
            this.col_stat_assist_dates_str.ReadOnly = true;
            // 
            // inlineCourseType
            // 
            this.inlineCourseType._ReadOnly = false;
            this.inlineCourseType._SelectedItem = null;
            this.inlineCourseType._Text = "";
            this.inlineCourseType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineCourseType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineCourseType.Location = new System.Drawing.Point(49, 54);
            this.inlineCourseType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineCourseType.Name = "inlineCourseType";
            this.inlineCourseType.Size = new System.Drawing.Size(90, 23);
            this.inlineCourseType.TabIndex = 120;
            // 
            // inlineTrainer
            // 
            this.inlineTrainer._ReadOnly = false;
            this.inlineTrainer._SelectedItem = null;
            this.inlineTrainer._Text = "";
            this.inlineTrainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTrainer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTrainer.Location = new System.Drawing.Point(141, 54);
            this.inlineTrainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTrainer.Name = "inlineTrainer";
            this.inlineTrainer.Size = new System.Drawing.Size(107, 23);
            this.inlineTrainer.TabIndex = 121;
            // 
            // inlineStatus
            // 
            this.inlineStatus._ReadOnly = false;
            this.inlineStatus._SelectedItem = null;
            this.inlineStatus._Text = "";
            this.inlineStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineStatus.Location = new System.Drawing.Point(250, 54);
            this.inlineStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineStatus.Name = "inlineStatus";
            this.inlineStatus.Size = new System.Drawing.Size(81, 23);
            this.inlineStatus.TabIndex = 122;
            // 
            // inlineTerm
            // 
            this.inlineTerm._ReadOnly = false;
            this.inlineTerm._SelectedItem = null;
            this.inlineTerm._Text = "";
            this.inlineTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTerm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTerm.Location = new System.Drawing.Point(333, 54);
            this.inlineTerm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTerm.Name = "inlineTerm";
            this.inlineTerm.Size = new System.Drawing.Size(76, 23);
            this.inlineTerm.TabIndex = 123;
            // 
            // inlineRemark
            // 
            this.inlineRemark._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineRemark._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineRemark._ForeColor = System.Drawing.SystemColors.WindowText;
            this.inlineRemark._MaxLength = 32767;
            this.inlineRemark._PasswordChar = '\0';
            this.inlineRemark._ReadOnly = false;
            this.inlineRemark._SelectionLength = 0;
            this.inlineRemark._SelectionStart = 0;
            this.inlineRemark._Text = "";
            this.inlineRemark._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineRemark.BackColor = System.Drawing.Color.White;
            this.inlineRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineRemark.Location = new System.Drawing.Point(411, 54);
            this.inlineRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineRemark.Name = "inlineRemark";
            this.inlineRemark.Size = new System.Drawing.Size(232, 23);
            this.inlineRemark.TabIndex = 124;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(11, 169);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(42, 23);
            this.btnAdd.TabIndex = 125;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(11, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(42, 23);
            this.btnSave.TabIndex = 125;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(11, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(42, 23);
            this.btnCancel.TabIndex = 125;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(11, 285);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(42, 23);
            this.btnDelete.TabIndex = 125;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(11, 198);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(42, 23);
            this.btnEdit.TabIndex = 125;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // DialogTrainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 420);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogTrainer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "วิทยากรอบรม และ ผู้ช่วยอบรม วันที่";
            this.Load += new System.EventHandler(this.DialogTrainer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label1;
        private CC.XDatagrid dgvTrainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_training_calendar;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_course_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_trainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_code_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_term;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_remark;
        private CC.XDatagrid dgvStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_first_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_last_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_code_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_train_dates;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_assist_dates;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_train_dates_str;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_stat_assist_dates_str;
        private CC.XTextEdit inlineRemark;
        private CC.XDropdownList inlineTerm;
        private CC.XDropdownList inlineStatus;
        private CC.XDropdownList inlineTrainer;
        private CC.XDropdownList inlineCourseType;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
    }
}