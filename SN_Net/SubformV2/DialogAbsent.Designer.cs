namespace SN_Net.Subform
{
    partial class DialogAbsent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnItem = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numLeaveCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rdWeekday = new System.Windows.Forms.RadioButton();
            this.rdHoliday = new System.Windows.Forms.RadioButton();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCopyItem = new System.Windows.Forms.Button();
            this.inlineFine = new System.Windows.Forms.NumericUpDown();
            this.inlineRemark = new CC.XTextEdit();
            this.inlineTo = new CC.XTimePicker();
            this.inlineFrom = new CC.XTimePicker();
            this.inlineMedCert = new CC.XDropdownList();
            this.inlineStatus = new CC.XDropdownList();
            this.inlineReason = new CC.XDropdownList();
            this.dgv = new CC.XDatagrid();
            this.col_event_calendar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_users = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_code_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_time_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_time_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_med_cert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlGroupMaid = new CC.XDropdownList();
            this.dlGroupHoliday = new CC.XDropdownList();
            this.txtRemark = new CC.XTextEdit();
            this.txtHoliday = new CC.XTextEdit();
            this.inlineCodeName = new CC.XDropdownList();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeaveCount)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inlineFine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEdit,
            this.btnStop,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnItem});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(852, 43);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 40);
            this.btnEdit.Text = "Edit <Alt+E>";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = global::SN_Net.Properties.Resources.stop;
            this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(36, 40);
            this.btnStop.Text = "Cancel Add/Edit <ESC>";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SN_Net.Properties.Resources.save;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 40);
            this.btnSave.Text = "Save <F9>";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnItem
            // 
            this.btnItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItem.Image = global::SN_Net.Properties.Resources.item;
            this.btnItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItem.Margin = new System.Windows.Forms.Padding(2, 1, 1, 2);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(36, 40);
            this.btnItem.Text = "Entrance to item <F8>";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numLeaveCount);
            this.groupBox1.Controls.Add(this.dlGroupMaid);
            this.groupBox1.Controls.Add(this.dlGroupHoliday);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.txtHoliday);
            this.groupBox1.Controls.Add(this.rdWeekday);
            this.groupBox1.Controls.Add(this.rdHoliday);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 176);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // numLeaveCount
            // 
            this.numLeaveCount.Location = new System.Drawing.Point(452, 111);
            this.numLeaveCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numLeaveCount.Name = "numLeaveCount";
            this.numLeaveCount.Size = new System.Drawing.Size(51, 23);
            this.numLeaveCount.TabIndex = 5;
            this.numLeaveCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numLeaveCount.ValueChanged += new System.EventHandler(this.numLeaveCount_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "หมายเหตุ :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(504, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "คน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "ลางาน/ออกพบลูกค้าได้";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "ทำความสะอาด :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "วันหยุดพิเศษสำหรับ :";
            // 
            // rdWeekday
            // 
            this.rdWeekday.AutoSize = true;
            this.rdWeekday.Location = new System.Drawing.Point(15, 58);
            this.rdWeekday.Name = "rdWeekday";
            this.rdWeekday.Size = new System.Drawing.Size(99, 20);
            this.rdWeekday.TabIndex = 2;
            this.rdWeekday.TabStop = true;
            this.rdWeekday.Text = "วันทำการปกติ";
            this.rdWeekday.UseVisualStyleBackColor = true;
            this.rdWeekday.CheckedChanged += new System.EventHandler(this.rdWeekday_CheckedChanged);
            // 
            // rdHoliday
            // 
            this.rdHoliday.AutoSize = true;
            this.rdHoliday.Location = new System.Drawing.Point(15, 22);
            this.rdHoliday.Name = "rdHoliday";
            this.rdHoliday.Size = new System.Drawing.Size(64, 20);
            this.rdHoliday.TabIndex = 0;
            this.rdHoliday.TabStop = true;
            this.rdHoliday.Text = "วันหยุด";
            this.rdHoliday.UseVisualStyleBackColor = true;
            this.rdHoliday.CheckedChanged += new System.EventHandler(this.rdHoliday_CheckedChanged);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(8, 55);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(44, 23);
            this.btnDeleteItem.TabIndex = 1;
            this.btnDeleteItem.Text = "Delete";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(8, 31);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(44, 23);
            this.btnEditItem.TabIndex = 1;
            this.btnEditItem.Text = "Edit";
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(8, 7);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(44, 23);
            this.btnAddItem.TabIndex = 1;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 228);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(828, 296);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.inlineRemark);
            this.tabPage1.Controls.Add(this.inlineTo);
            this.tabPage1.Controls.Add(this.inlineFrom);
            this.tabPage1.Controls.Add(this.inlineMedCert);
            this.tabPage1.Controls.Add(this.inlineStatus);
            this.tabPage1.Controls.Add(this.inlineCodeName);
            this.tabPage1.Controls.Add(this.inlineReason);
            this.tabPage1.Controls.Add(this.inlineFine);
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Controls.Add(this.btnAddItem);
            this.tabPage1.Controls.Add(this.btnCopyItem);
            this.tabPage1.Controls.Add(this.btnDeleteItem);
            this.tabPage1.Controls.Add(this.btnEditItem);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(820, 267);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "พนักงานที่ลางาน / ออกพบลูกค้า <F8>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCopyItem
            // 
            this.btnCopyItem.Location = new System.Drawing.Point(8, 79);
            this.btnCopyItem.Name = "btnCopyItem";
            this.btnCopyItem.Size = new System.Drawing.Size(44, 23);
            this.btnCopyItem.TabIndex = 1;
            this.btnCopyItem.Text = "Copy";
            this.btnCopyItem.UseVisualStyleBackColor = true;
            this.btnCopyItem.Click += new System.EventHandler(this.btnCopyItem_Click);
            // 
            // inlineFine
            // 
            this.inlineFine.Location = new System.Drawing.Point(742, 32);
            this.inlineFine.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.inlineFine.Name = "inlineFine";
            this.inlineFine.Size = new System.Drawing.Size(75, 23);
            this.inlineFine.TabIndex = 15;
            this.inlineFine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.inlineFine.ValueChanged += new System.EventHandler(this.inlineFine_ValueChanged);
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
            this.inlineRemark.Location = new System.Drawing.Point(458, 32);
            this.inlineRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineRemark.Name = "inlineRemark";
            this.inlineRemark.Size = new System.Drawing.Size(170, 23);
            this.inlineRemark.TabIndex = 13;
            this.inlineRemark._TextChanged += new System.EventHandler(this.inlineRemark__TextChanged);
            // 
            // inlineTo
            // 
            this.inlineTo.CustomFormat = "HH:mm";
            this.inlineTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inlineTo.Location = new System.Drawing.Point(322, 32);
            this.inlineTo.Name = "inlineTo";
            this.inlineTo.ShowUpDown = true;
            this.inlineTo.Size = new System.Drawing.Size(55, 23);
            this.inlineTo.TabIndex = 11;
            this.inlineTo.ValueChanged += new System.EventHandler(this.inlineTo_ValueChanged);
            // 
            // inlineFrom
            // 
            this.inlineFrom.CustomFormat = "HH:mm";
            this.inlineFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inlineFrom.Location = new System.Drawing.Point(267, 32);
            this.inlineFrom.Name = "inlineFrom";
            this.inlineFrom.ShowUpDown = true;
            this.inlineFrom.Size = new System.Drawing.Size(55, 23);
            this.inlineFrom.TabIndex = 10;
            this.inlineFrom.ValueChanged += new System.EventHandler(this.inlineFrom_ValueChanged);
            // 
            // inlineMedCert
            // 
            this.inlineMedCert._ReadOnly = false;
            this.inlineMedCert._SelectedItem = null;
            this.inlineMedCert._Text = "";
            this.inlineMedCert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineMedCert.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineMedCert.Location = new System.Drawing.Point(629, 32);
            this.inlineMedCert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineMedCert.Name = "inlineMedCert";
            this.inlineMedCert.Size = new System.Drawing.Size(109, 23);
            this.inlineMedCert.TabIndex = 14;
            this.inlineMedCert._SelectedItemChanged += new System.EventHandler(this.inlineMedCert__SelectedItemChanged);
            // 
            // inlineStatus
            // 
            this.inlineStatus._ReadOnly = false;
            this.inlineStatus._SelectedItem = null;
            this.inlineStatus._Text = "";
            this.inlineStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineStatus.Location = new System.Drawing.Point(377, 32);
            this.inlineStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineStatus.Name = "inlineStatus";
            this.inlineStatus.Size = new System.Drawing.Size(80, 23);
            this.inlineStatus.TabIndex = 12;
            this.inlineStatus._SelectedItemChanged += new System.EventHandler(this.inlineStatus__SelectedItemChanged);
            // 
            // inlineReason
            // 
            this.inlineReason._ReadOnly = false;
            this.inlineReason._SelectedItem = null;
            this.inlineReason._Text = "";
            this.inlineReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineReason.Location = new System.Drawing.Point(160, 32);
            this.inlineReason.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineReason.Name = "inlineReason";
            this.inlineReason.Size = new System.Drawing.Size(106, 23);
            this.inlineReason.TabIndex = 9;
            this.inlineReason._SelectedItemChanged += new System.EventHandler(this.inlineReason__SelectedItemChanged);
            // 
            // dgv
            // 
            this.dgv.AllowSortByColumnHeaderClicked = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_event_calendar,
            this.col_users,
            this.col_seq,
            this.col_code_name,
            this.col_reason,
            this.col_time_from,
            this.col_time_to,
            this.col_status,
            this.col_remark,
            this.col_med_cert,
            this.col_fine});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.FillEmptyRow = false;
            this.dgv.FocusedRowBorderRedLine = true;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(820, 267);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            this.dgv.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // col_event_calendar
            // 
            this.col_event_calendar.DataPropertyName = "event_calendar";
            this.col_event_calendar.HeaderText = "Event Calendar";
            this.col_event_calendar.Name = "col_event_calendar";
            this.col_event_calendar.ReadOnly = true;
            this.col_event_calendar.Visible = false;
            // 
            // col_users
            // 
            this.col_users.DataPropertyName = "users";
            this.col_users.HeaderText = "Users";
            this.col_users.Name = "col_users";
            this.col_users.ReadOnly = true;
            this.col_users.Visible = false;
            // 
            // col_seq
            // 
            this.col_seq.DataPropertyName = "seq";
            this.col_seq.HeaderText = "ลำดับ";
            this.col_seq.MinimumWidth = 45;
            this.col_seq.Name = "col_seq";
            this.col_seq.ReadOnly = true;
            this.col_seq.Width = 45;
            // 
            // col_code_name
            // 
            this.col_code_name.DataPropertyName = "code_name";
            this.col_code_name.HeaderText = "รหัส / ชื่อ";
            this.col_code_name.MinimumWidth = 110;
            this.col_code_name.Name = "col_code_name";
            this.col_code_name.ReadOnly = true;
            this.col_code_name.Width = 110;
            // 
            // col_reason
            // 
            this.col_reason.DataPropertyName = "reason";
            this.col_reason.HeaderText = "เหตุผล";
            this.col_reason.MinimumWidth = 110;
            this.col_reason.Name = "col_reason";
            this.col_reason.ReadOnly = true;
            this.col_reason.Width = 110;
            // 
            // col_time_from
            // 
            this.col_time_from.DataPropertyName = "time_from";
            this.col_time_from.HeaderText = "จาก";
            this.col_time_from.MinimumWidth = 55;
            this.col_time_from.Name = "col_time_from";
            this.col_time_from.ReadOnly = true;
            this.col_time_from.Width = 55;
            // 
            // col_time_to
            // 
            this.col_time_to.DataPropertyName = "time_to";
            this.col_time_to.HeaderText = "ถึง";
            this.col_time_to.MinimumWidth = 55;
            this.col_time_to.Name = "col_time_to";
            this.col_time_to.ReadOnly = true;
            this.col_time_to.Width = 55;
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
            // col_remark
            // 
            this.col_remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_remark.DataPropertyName = "remark";
            this.col_remark.HeaderText = "หมายเหตุ/ชื่อลูกค้า";
            this.col_remark.Name = "col_remark";
            this.col_remark.ReadOnly = true;
            // 
            // col_med_cert
            // 
            this.col_med_cert.DataPropertyName = "med_cert";
            this.col_med_cert.HeaderText = "เอกสารอ้างอิง";
            this.col_med_cert.MinimumWidth = 110;
            this.col_med_cert.Name = "col_med_cert";
            this.col_med_cert.ReadOnly = true;
            this.col_med_cert.Width = 110;
            // 
            // col_fine
            // 
            this.col_fine.DataPropertyName = "fine";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_fine.DefaultCellStyle = dataGridViewCellStyle23;
            this.col_fine.HeaderText = "หักค่าคอมฯ";
            this.col_fine.MinimumWidth = 80;
            this.col_fine.Name = "col_fine";
            this.col_fine.ReadOnly = true;
            this.col_fine.Width = 80;
            // 
            // dlGroupMaid
            // 
            this.dlGroupMaid._ReadOnly = false;
            this.dlGroupMaid._SelectedItem = null;
            this.dlGroupMaid._Text = "";
            this.dlGroupMaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlGroupMaid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dlGroupMaid.Location = new System.Drawing.Point(164, 111);
            this.dlGroupMaid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dlGroupMaid.Name = "dlGroupMaid";
            this.dlGroupMaid.Size = new System.Drawing.Size(101, 23);
            this.dlGroupMaid.TabIndex = 4;
            this.dlGroupMaid._SelectedItemChanged += new System.EventHandler(this.dlGroupMaid__SelectedItemChanged);
            this.dlGroupMaid._DoubleClicked += new System.EventHandler(this.PerformEdit);
            // 
            // dlGroupHoliday
            // 
            this.dlGroupHoliday._ReadOnly = false;
            this.dlGroupHoliday._SelectedItem = null;
            this.dlGroupHoliday._Text = "";
            this.dlGroupHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlGroupHoliday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dlGroupHoliday.Location = new System.Drawing.Point(164, 84);
            this.dlGroupHoliday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dlGroupHoliday.Name = "dlGroupHoliday";
            this.dlGroupHoliday.Size = new System.Drawing.Size(101, 23);
            this.dlGroupHoliday.TabIndex = 3;
            this.dlGroupHoliday._SelectedItemChanged += new System.EventHandler(this.dlGroupHoliday__SelectedItemChanged);
            this.dlGroupHoliday._DoubleClicked += new System.EventHandler(this.PerformEdit);
            // 
            // txtRemark
            // 
            this.txtRemark._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtRemark._ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRemark._MaxLength = 50;
            this.txtRemark._PasswordChar = '\0';
            this.txtRemark._ReadOnly = false;
            this.txtRemark._SelectionLength = 0;
            this.txtRemark._SelectionStart = 0;
            this.txtRemark._Text = "";
            this.txtRemark._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRemark.BackColor = System.Drawing.Color.White;
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtRemark.Location = new System.Drawing.Point(164, 139);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(360, 23);
            this.txtRemark.TabIndex = 6;
            this.txtRemark._TextChanged += new System.EventHandler(this.txtRemark__TextChanged);
            this.txtRemark._DoubleClicked += new System.EventHandler(this.PerformEdit);
            // 
            // txtHoliday
            // 
            this.txtHoliday._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtHoliday._ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHoliday._MaxLength = 50;
            this.txtHoliday._PasswordChar = '\0';
            this.txtHoliday._ReadOnly = false;
            this.txtHoliday._SelectionLength = 0;
            this.txtHoliday._SelectionStart = 0;
            this.txtHoliday._Text = "";
            this.txtHoliday._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtHoliday.BackColor = System.Drawing.Color.White;
            this.txtHoliday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoliday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtHoliday.Location = new System.Drawing.Point(88, 20);
            this.txtHoliday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoliday.Name = "txtHoliday";
            this.txtHoliday.Size = new System.Drawing.Size(220, 23);
            this.txtHoliday.TabIndex = 1;
            this.txtHoliday._TextChanged += new System.EventHandler(this.txtHoliday__TextChanged);
            this.txtHoliday._DoubleClicked += new System.EventHandler(this.PerformEdit);
            // 
            // inlineCodeName
            // 
            this.inlineCodeName._ReadOnly = false;
            this.inlineCodeName._SelectedItem = null;
            this.inlineCodeName._Text = "";
            this.inlineCodeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineCodeName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineCodeName.Location = new System.Drawing.Point(52, 32);
            this.inlineCodeName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineCodeName.Name = "inlineCodeName";
            this.inlineCodeName.Size = new System.Drawing.Size(106, 23);
            this.inlineCodeName.TabIndex = 8;
            this.inlineCodeName._SelectedItemChanged += new System.EventHandler(this.inlineCodeName__SelectedItemChanged);
            // 
            // DialogAbsent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 536);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogAbsent";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "เพิ่ม/แก้ไข เหตการณ์ประจำวัน";
            this.Load += new System.EventHandler(this.DialogAbsent_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeaveCount)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inlineFine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numLeaveCount;
        private CC.XDropdownList dlGroupMaid;
        private CC.XDropdownList dlGroupHoliday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CC.XTextEdit txtRemark;
        private CC.XTextEdit txtHoliday;
        private System.Windows.Forms.RadioButton rdWeekday;
        private System.Windows.Forms.RadioButton rdHoliday;
        private CC.XDatagrid dgv;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_event_calendar;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_users;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_code_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_time_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_time_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_med_cert;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fine;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnCopyItem;
        private System.Windows.Forms.NumericUpDown inlineFine;
        private CC.XDropdownList inlineMedCert;
        private CC.XDropdownList inlineStatus;
        private CC.XDropdownList inlineReason;
        private CC.XTextEdit inlineRemark;
        private CC.XTimePicker inlineTo;
        private CC.XTimePicker inlineFrom;
        private CC.XDropdownList inlineCodeName;
    }
}