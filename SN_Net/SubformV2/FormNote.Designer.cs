namespace SN_Net.Subform
{
    partial class FormNote
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.inlineDuration = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWorkingDate = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCompnam = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWorkingDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMA = new System.Windows.Forms.Label();
            this.lblCloud = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.inlineBreakType = new CC.XDropdownList();
            this.inlineOther = new CC.XCheckBox();
            this.inlineTrainType = new CC.XDropdownList();
            this.inlineTransfer = new CC.XCheckBox();
            this.inlineMail = new CC.XCheckBox();
            this.inlinePeriod = new CC.XCheckBox();
            this.inlineYearend = new CC.XCheckBox();
            this.inlineSecure = new CC.XCheckBox();
            this.inlineAsset = new CC.XCheckBox();
            this.inlineStatement = new CC.XCheckBox();
            this.inlineReportExcel = new CC.XCheckBox();
            this.inlineForm = new CC.XCheckBox();
            this.inlineStock = new CC.XCheckBox();
            this.inlineTrain = new CC.XCheckBox();
            this.inlinePrint = new CC.XCheckBox();
            this.inlineFont = new CC.XCheckBox();
            this.inlineError = new CC.XCheckBox();
            this.inlineInstall = new CC.XCheckBox();
            this.inlineMapdrive = new CC.XCheckBox();
            this.inlineRemark = new CC.XTextEdit();
            this.inlineContact = new CC.XTextEdit();
            this.inlineSernum = new CC.XTextEditMasked();
            this.inlineEnd = new CC.XTimePicker();
            this.inlineStart = new CC.XTimePicker();
            this.dgvNote = new CC.XDatagrid();
            this.col_note_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_sernum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_mapdrive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_installupdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_installfonts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_print = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_training = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_form = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_reportexcel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_statement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_assets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_secure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_yearend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_mail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_transfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_is_other = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_note_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.inlineDuration);
            this.panel3.Controls.Add(this.inlineBreakType);
            this.panel3.Controls.Add(this.inlineOther);
            this.panel3.Controls.Add(this.inlineTrainType);
            this.panel3.Controls.Add(this.inlineTransfer);
            this.panel3.Controls.Add(this.inlineMail);
            this.panel3.Controls.Add(this.inlinePeriod);
            this.panel3.Controls.Add(this.inlineYearend);
            this.panel3.Controls.Add(this.inlineSecure);
            this.panel3.Controls.Add(this.inlineAsset);
            this.panel3.Controls.Add(this.inlineStatement);
            this.panel3.Controls.Add(this.inlineReportExcel);
            this.panel3.Controls.Add(this.inlineForm);
            this.panel3.Controls.Add(this.inlineStock);
            this.panel3.Controls.Add(this.inlineTrain);
            this.panel3.Controls.Add(this.inlinePrint);
            this.panel3.Controls.Add(this.inlineFont);
            this.panel3.Controls.Add(this.inlineError);
            this.panel3.Controls.Add(this.inlineInstall);
            this.panel3.Controls.Add(this.inlineMapdrive);
            this.panel3.Controls.Add(this.inlineRemark);
            this.panel3.Controls.Add(this.inlineContact);
            this.panel3.Controls.Add(this.inlineSernum);
            this.panel3.Controls.Add(this.inlineEnd);
            this.panel3.Controls.Add(this.inlineStart);
            this.panel3.Controls.Add(this.dgvNote);
            this.panel3.Location = new System.Drawing.Point(3, 191);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1270, 437);
            this.panel3.TabIndex = 1;
            // 
            // inlineDuration
            // 
            this.inlineDuration.BackColor = System.Drawing.Color.White;
            this.inlineDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineDuration.ForeColor = System.Drawing.Color.Red;
            this.inlineDuration.Location = new System.Drawing.Point(205, 55);
            this.inlineDuration.Name = "inlineDuration";
            this.inlineDuration.Size = new System.Drawing.Size(64, 23);
            this.inlineDuration.TabIndex = 5;
            this.inlineDuration.Text = "00:00:00";
            this.inlineDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.inlineDuration.TextChanged += new System.EventHandler(this.inlineDuration_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.toolStripSeparator1,
            this.btnStop,
            this.btnSave,
            this.toolStripSeparator2,
            this.btnSearch,
            this.toolStripSeparator3,
            this.btnWorkingDate});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 43);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SN_Net.Properties.Resources.add;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 40);
            this.btnAdd.Text = "เพิ่มสายสนทนากับลูกค้า <Alt+A>";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.btnEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 40);
            this.btnEdit.Text = "Edit <Alt+E>";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = global::SN_Net.Properties.Resources.stop;
            this.btnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
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
            this.btnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 40);
            this.btnSave.Text = "Save <F9>";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::SN_Net.Properties.Resources.search;
            this.btnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 40);
            this.btnSearch.Text = "ค้นหา <Ctrl+S>";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 43);
            // 
            // btnWorkingDate
            // 
            this.btnWorkingDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWorkingDate.Image = global::SN_Net.Properties.Resources.change_scope;
            this.btnWorkingDate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnWorkingDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWorkingDate.Name = "btnWorkingDate";
            this.btnWorkingDate.Size = new System.Drawing.Size(36, 40);
            this.btnWorkingDate.Text = "แก้ไขขอบแขตของรายการ <Ctrl+G>";
            this.btnWorkingDate.Click += new System.EventHandler(this.btnWorkingDate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(27, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "ชื่อลูกค้า :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(27, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "ที่อยู่ :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(27, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Software Version :";
            // 
            // lblCompnam
            // 
            this.lblCompnam.AutoSize = true;
            this.lblCompnam.Location = new System.Drawing.Point(108, 82);
            this.lblCompnam.Name = "lblCompnam";
            this.lblCompnam.Size = new System.Drawing.Size(32, 16);
            this.lblCompnam.TabIndex = 4;
            this.lblCompnam.Text = "      ";
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(108, 108);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(32, 16);
            this.lblAddr.TabIndex = 4;
            this.lblAddr.Text = "      ";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(162, 161);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(32, 16);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "      ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(27, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "วันที่ทำการ :";
            // 
            // lblWorkingDate
            // 
            this.lblWorkingDate.AutoSize = true;
            this.lblWorkingDate.Location = new System.Drawing.Point(108, 56);
            this.lblWorkingDate.Name = "lblWorkingDate";
            this.lblWorkingDate.Size = new System.Drawing.Size(32, 16);
            this.lblWorkingDate.TabIndex = 4;
            this.lblWorkingDate.Text = "      ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(340, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "MA. :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(562, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Cloud :";
            // 
            // lblMA
            // 
            this.lblMA.AutoSize = true;
            this.lblMA.Location = new System.Drawing.Point(387, 161);
            this.lblMA.Name = "lblMA";
            this.lblMA.Size = new System.Drawing.Size(32, 16);
            this.lblMA.TabIndex = 4;
            this.lblMA.Text = "      ";
            // 
            // lblCloud
            // 
            this.lblCloud.AutoSize = true;
            this.lblCloud.Location = new System.Drawing.Point(620, 161);
            this.lblCloud.Name = "lblCloud";
            this.lblCloud.Size = new System.Drawing.Size(32, 16);
            this.lblCloud.TabIndex = 4;
            this.lblCloud.Text = "      ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(27, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Password :";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPassword.ForeColor = System.Drawing.Color.Red;
            this.lblPassword.Location = new System.Drawing.Point(108, 134);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(0, 16);
            this.lblPassword.TabIndex = 4;
            // 
            // inlineBreakType
            // 
            this.inlineBreakType._ReadOnly = false;
            this.inlineBreakType._SelectedItem = null;
            this.inlineBreakType._Text = "";
            this.inlineBreakType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineBreakType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineBreakType.Location = new System.Drawing.Point(375, 117);
            this.inlineBreakType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineBreakType.Name = "inlineBreakType";
            this.inlineBreakType.Size = new System.Drawing.Size(94, 23);
            this.inlineBreakType.TabIndex = 6;
            this.inlineBreakType._SelectedItemChanged += new System.EventHandler(this.inlineBreakType__SelectedItemChanged);
            // 
            // inlineOther
            // 
            this.inlineOther._Checked = false;
            this.inlineOther._ReadOnly = false;
            this.inlineOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineOther.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineOther.Location = new System.Drawing.Point(1038, 55);
            this.inlineOther.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineOther.Name = "inlineOther";
            this.inlineOther.Size = new System.Drawing.Size(28, 23);
            this.inlineOther.TabIndex = 23;
            this.inlineOther._CheckedChanged += new System.EventHandler(this.inlineOther__CheckedChanged);
            // 
            // inlineTrainType
            // 
            this.inlineTrainType._ReadOnly = false;
            this.inlineTrainType._SelectedItem = null;
            this.inlineTrainType._Text = "";
            this.inlineTrainType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTrainType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTrainType.Location = new System.Drawing.Point(375, 86);
            this.inlineTrainType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTrainType.Name = "inlineTrainType";
            this.inlineTrainType.Size = new System.Drawing.Size(94, 23);
            this.inlineTrainType.TabIndex = 5;
            this.inlineTrainType._SelectedItemChanged += new System.EventHandler(this.inlineTrainType__SelectedItemChanged);
            // 
            // inlineTransfer
            // 
            this.inlineTransfer._Checked = false;
            this.inlineTransfer._ReadOnly = false;
            this.inlineTransfer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTransfer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTransfer.Location = new System.Drawing.Point(1002, 55);
            this.inlineTransfer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTransfer.Name = "inlineTransfer";
            this.inlineTransfer.Size = new System.Drawing.Size(28, 23);
            this.inlineTransfer.TabIndex = 22;
            this.inlineTransfer._CheckedChanged += new System.EventHandler(this.inlineTransfer__CheckedChanged);
            // 
            // inlineMail
            // 
            this.inlineMail._Checked = false;
            this.inlineMail._ReadOnly = false;
            this.inlineMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineMail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineMail.Location = new System.Drawing.Point(968, 55);
            this.inlineMail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineMail.Name = "inlineMail";
            this.inlineMail.Size = new System.Drawing.Size(28, 23);
            this.inlineMail.TabIndex = 21;
            this.inlineMail._CheckedChanged += new System.EventHandler(this.inlineMail__CheckedChanged);
            // 
            // inlinePeriod
            // 
            this.inlinePeriod._Checked = false;
            this.inlinePeriod._ReadOnly = false;
            this.inlinePeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlinePeriod.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlinePeriod.Location = new System.Drawing.Point(931, 55);
            this.inlinePeriod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlinePeriod.Name = "inlinePeriod";
            this.inlinePeriod.Size = new System.Drawing.Size(28, 23);
            this.inlinePeriod.TabIndex = 20;
            this.inlinePeriod._CheckedChanged += new System.EventHandler(this.inlinePeriod__CheckedChanged);
            // 
            // inlineYearend
            // 
            this.inlineYearend._Checked = false;
            this.inlineYearend._ReadOnly = false;
            this.inlineYearend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineYearend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineYearend.Location = new System.Drawing.Point(897, 55);
            this.inlineYearend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineYearend.Name = "inlineYearend";
            this.inlineYearend.Size = new System.Drawing.Size(28, 23);
            this.inlineYearend.TabIndex = 19;
            this.inlineYearend._CheckedChanged += new System.EventHandler(this.inlineYearend__CheckedChanged);
            // 
            // inlineSecure
            // 
            this.inlineSecure._Checked = false;
            this.inlineSecure._ReadOnly = false;
            this.inlineSecure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineSecure.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineSecure.Location = new System.Drawing.Point(861, 55);
            this.inlineSecure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineSecure.Name = "inlineSecure";
            this.inlineSecure.Size = new System.Drawing.Size(28, 23);
            this.inlineSecure.TabIndex = 18;
            this.inlineSecure._CheckedChanged += new System.EventHandler(this.inlineSecure__CheckedChanged);
            // 
            // inlineAsset
            // 
            this.inlineAsset._Checked = false;
            this.inlineAsset._ReadOnly = false;
            this.inlineAsset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineAsset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineAsset.Location = new System.Drawing.Point(828, 55);
            this.inlineAsset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineAsset.Name = "inlineAsset";
            this.inlineAsset.Size = new System.Drawing.Size(28, 23);
            this.inlineAsset.TabIndex = 17;
            this.inlineAsset._CheckedChanged += new System.EventHandler(this.inlineAsset__CheckedChanged);
            // 
            // inlineStatement
            // 
            this.inlineStatement._Checked = false;
            this.inlineStatement._ReadOnly = false;
            this.inlineStatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineStatement.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineStatement.Location = new System.Drawing.Point(792, 55);
            this.inlineStatement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineStatement.Name = "inlineStatement";
            this.inlineStatement.Size = new System.Drawing.Size(28, 23);
            this.inlineStatement.TabIndex = 16;
            this.inlineStatement._CheckedChanged += new System.EventHandler(this.inlineStatement__CheckedChanged);
            // 
            // inlineReportExcel
            // 
            this.inlineReportExcel._Checked = false;
            this.inlineReportExcel._ReadOnly = false;
            this.inlineReportExcel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineReportExcel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineReportExcel.Location = new System.Drawing.Point(758, 55);
            this.inlineReportExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineReportExcel.Name = "inlineReportExcel";
            this.inlineReportExcel.Size = new System.Drawing.Size(28, 23);
            this.inlineReportExcel.TabIndex = 15;
            this.inlineReportExcel._CheckedChanged += new System.EventHandler(this.inlineReportExcel__CheckedChanged);
            // 
            // inlineForm
            // 
            this.inlineForm._Checked = false;
            this.inlineForm._ReadOnly = false;
            this.inlineForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineForm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineForm.Location = new System.Drawing.Point(724, 55);
            this.inlineForm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineForm.Name = "inlineForm";
            this.inlineForm.Size = new System.Drawing.Size(28, 23);
            this.inlineForm.TabIndex = 14;
            this.inlineForm._CheckedChanged += new System.EventHandler(this.inlineForm__CheckedChanged);
            // 
            // inlineStock
            // 
            this.inlineStock._Checked = false;
            this.inlineStock._ReadOnly = false;
            this.inlineStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineStock.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineStock.Location = new System.Drawing.Point(689, 55);
            this.inlineStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineStock.Name = "inlineStock";
            this.inlineStock.Size = new System.Drawing.Size(28, 23);
            this.inlineStock.TabIndex = 13;
            this.inlineStock._CheckedChanged += new System.EventHandler(this.inlineStock__CheckedChanged);
            // 
            // inlineTrain
            // 
            this.inlineTrain._Checked = false;
            this.inlineTrain._ReadOnly = false;
            this.inlineTrain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTrain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTrain.Location = new System.Drawing.Point(650, 55);
            this.inlineTrain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTrain.Name = "inlineTrain";
            this.inlineTrain.Size = new System.Drawing.Size(28, 23);
            this.inlineTrain.TabIndex = 12;
            this.inlineTrain._CheckedChanged += new System.EventHandler(this.inlineTrain__CheckedChanged);
            // 
            // inlinePrint
            // 
            this.inlinePrint._Checked = false;
            this.inlinePrint._ReadOnly = false;
            this.inlinePrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlinePrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlinePrint.Location = new System.Drawing.Point(614, 55);
            this.inlinePrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlinePrint.Name = "inlinePrint";
            this.inlinePrint.Size = new System.Drawing.Size(28, 23);
            this.inlinePrint.TabIndex = 11;
            this.inlinePrint._CheckedChanged += new System.EventHandler(this.inlinePrint__CheckedChanged);
            // 
            // inlineFont
            // 
            this.inlineFont._Checked = false;
            this.inlineFont._ReadOnly = false;
            this.inlineFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineFont.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineFont.Location = new System.Drawing.Point(583, 55);
            this.inlineFont.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineFont.Name = "inlineFont";
            this.inlineFont.Size = new System.Drawing.Size(28, 23);
            this.inlineFont.TabIndex = 10;
            this.inlineFont._CheckedChanged += new System.EventHandler(this.inlineFont__CheckedChanged);
            // 
            // inlineError
            // 
            this.inlineError._Checked = false;
            this.inlineError._ReadOnly = false;
            this.inlineError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineError.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineError.Location = new System.Drawing.Point(548, 55);
            this.inlineError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineError.Name = "inlineError";
            this.inlineError.Size = new System.Drawing.Size(28, 23);
            this.inlineError.TabIndex = 9;
            this.inlineError._CheckedChanged += new System.EventHandler(this.inlineError__CheckedChanged);
            // 
            // inlineInstall
            // 
            this.inlineInstall._Checked = false;
            this.inlineInstall._ReadOnly = false;
            this.inlineInstall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineInstall.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineInstall.Location = new System.Drawing.Point(513, 55);
            this.inlineInstall.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineInstall.Name = "inlineInstall";
            this.inlineInstall.Size = new System.Drawing.Size(28, 23);
            this.inlineInstall.TabIndex = 8;
            this.inlineInstall._CheckedChanged += new System.EventHandler(this.inlineInstall__CheckedChanged);
            // 
            // inlineMapdrive
            // 
            this.inlineMapdrive._Checked = false;
            this.inlineMapdrive._ReadOnly = false;
            this.inlineMapdrive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineMapdrive.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineMapdrive.Location = new System.Drawing.Point(479, 55);
            this.inlineMapdrive.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineMapdrive.Name = "inlineMapdrive";
            this.inlineMapdrive.Size = new System.Drawing.Size(28, 23);
            this.inlineMapdrive.TabIndex = 7;
            this.inlineMapdrive._CheckedChanged += new System.EventHandler(this.inlineMapdrive__CheckedChanged);
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
            this.inlineRemark.Location = new System.Drawing.Point(1070, 55);
            this.inlineRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineRemark.Name = "inlineRemark";
            this.inlineRemark.Size = new System.Drawing.Size(139, 23);
            this.inlineRemark.TabIndex = 24;
            this.inlineRemark._TextChanged += new System.EventHandler(this.inlineRemark__TextChanged);
            // 
            // inlineContact
            // 
            this.inlineContact._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineContact._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineContact._ForeColor = System.Drawing.SystemColors.WindowText;
            this.inlineContact._MaxLength = 32767;
            this.inlineContact._PasswordChar = '\0';
            this.inlineContact._ReadOnly = false;
            this.inlineContact._SelectionLength = 0;
            this.inlineContact._SelectionStart = 0;
            this.inlineContact._Text = "";
            this.inlineContact._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineContact.BackColor = System.Drawing.Color.White;
            this.inlineContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineContact.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineContact.Location = new System.Drawing.Point(375, 55);
            this.inlineContact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineContact.Name = "inlineContact";
            this.inlineContact.Size = new System.Drawing.Size(98, 23);
            this.inlineContact.TabIndex = 4;
            this.inlineContact._TextChanged += new System.EventHandler(this.inlineContact__TextChanged);
            // 
            // inlineSernum
            // 
            this.inlineSernum._Mask = ">A-AAA-AAAAAA";
            this.inlineSernum._PromptChar = ' ';
            this.inlineSernum._ReadOnly = false;
            this.inlineSernum._SelectionLength = 0;
            this.inlineSernum._SelectionStart = 0;
            this.inlineSernum._Text = " -   -";
            this.inlineSernum._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineSernum.BackColor = System.Drawing.Color.White;
            this.inlineSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineSernum.Location = new System.Drawing.Point(274, 55);
            this.inlineSernum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineSernum.Name = "inlineSernum";
            this.inlineSernum.Size = new System.Drawing.Size(99, 23);
            this.inlineSernum.TabIndex = 3;
            this.inlineSernum._Leave += new System.EventHandler(this.inlineSernum__Leave);
            // 
            // inlineEnd
            // 
            this.inlineEnd.CustomFormat = "HH:mm:ss";
            this.inlineEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inlineEnd.Location = new System.Drawing.Point(124, 55);
            this.inlineEnd.Name = "inlineEnd";
            this.inlineEnd.ShowUpDown = true;
            this.inlineEnd.Size = new System.Drawing.Size(78, 23);
            this.inlineEnd.TabIndex = 2;
            this.inlineEnd.ValueChanged += new System.EventHandler(this.inlineEnd_ValueChanged);
            // 
            // inlineStart
            // 
            this.inlineStart.CustomFormat = "HH:mm:ss";
            this.inlineStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.inlineStart.Location = new System.Drawing.Point(42, 55);
            this.inlineStart.Name = "inlineStart";
            this.inlineStart.ShowUpDown = true;
            this.inlineStart.Size = new System.Drawing.Size(79, 23);
            this.inlineStart.TabIndex = 1;
            this.inlineStart.ValueChanged += new System.EventHandler(this.inlineStart_ValueChanged);
            // 
            // dgvNote
            // 
            this.dgvNote.AllowSortByColumnHeaderClicked = false;
            this.dgvNote.AllowUserToAddRows = false;
            this.dgvNote.AllowUserToDeleteRows = false;
            this.dgvNote.AllowUserToResizeColumns = false;
            this.dgvNote.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNote.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNote.ColumnHeadersHeight = 45;
            this.dgvNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_note_note,
            this.col_note_seq,
            this.col_note_start,
            this.col_note_end,
            this.col_note_duration,
            this.col_note_sernum,
            this.col_note_contact,
            this.col_note_is_mapdrive,
            this.col_note_is_installupdate,
            this.col_note_is_error,
            this.col_note_is_installfonts,
            this.col_note_is_print,
            this.col_note_is_training,
            this.col_note_is_stock,
            this.col_note_is_form,
            this.col_note_is_reportexcel,
            this.col_note_is_statement,
            this.col_note_is_assets,
            this.col_note_is_secure,
            this.col_note_is_yearend,
            this.col_note_is_period,
            this.col_note_is_mail,
            this.col_note_is_transfer,
            this.col_note_is_other,
            this.col_note_remark});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNote.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNote.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvNote.EnableHeadersVisualStyles = false;
            this.dgvNote.FillEmptyRow = false;
            this.dgvNote.FocusedRowBorderRedLine = true;
            this.dgvNote.Location = new System.Drawing.Point(0, 0);
            this.dgvNote.Margin = new System.Windows.Forms.Padding(0);
            this.dgvNote.MultiSelect = false;
            this.dgvNote.Name = "dgvNote";
            this.dgvNote.ReadOnly = true;
            this.dgvNote.RowHeadersVisible = false;
            this.dgvNote.RowTemplate.Height = 26;
            this.dgvNote.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNote.Size = new System.Drawing.Size(1270, 437);
            this.dgvNote.StandardTab = true;
            this.dgvNote.TabIndex = 2;
            this.dgvNote.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvNote_CellPainting);
            this.dgvNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvNote_MouseClick);
            this.dgvNote.Resize += new System.EventHandler(this.dgvNote_Resize);
            // 
            // col_note_note
            // 
            this.col_note_note.DataPropertyName = "note";
            this.col_note_note.HeaderText = "Note";
            this.col_note_note.Name = "col_note_note";
            this.col_note_note.ReadOnly = true;
            this.col_note_note.Visible = false;
            // 
            // col_note_seq
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_note_seq.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_note_seq.HeaderText = "ลำดับ";
            this.col_note_seq.MinimumWidth = 40;
            this.col_note_seq.Name = "col_note_seq";
            this.col_note_seq.ReadOnly = true;
            this.col_note_seq.Width = 40;
            // 
            // col_note_start
            // 
            this.col_note_start.DataPropertyName = "start_time";
            this.col_note_start.HeaderText = "รับสาย";
            this.col_note_start.MinimumWidth = 80;
            this.col_note_start.Name = "col_note_start";
            this.col_note_start.ReadOnly = true;
            this.col_note_start.Width = 80;
            // 
            // col_note_end
            // 
            this.col_note_end.DataPropertyName = "end_time";
            this.col_note_end.HeaderText = "วางสาย";
            this.col_note_end.MinimumWidth = 80;
            this.col_note_end.Name = "col_note_end";
            this.col_note_end.ReadOnly = true;
            this.col_note_end.Width = 80;
            // 
            // col_note_duration
            // 
            this.col_note_duration.DataPropertyName = "duration";
            this.col_note_duration.HeaderText = "ระยะเวลา";
            this.col_note_duration.MinimumWidth = 70;
            this.col_note_duration.Name = "col_note_duration";
            this.col_note_duration.ReadOnly = true;
            this.col_note_duration.Width = 70;
            // 
            // col_note_sernum
            // 
            this.col_note_sernum.DataPropertyName = "sernum";
            this.col_note_sernum.HeaderText = "S/N";
            this.col_note_sernum.MinimumWidth = 100;
            this.col_note_sernum.Name = "col_note_sernum";
            this.col_note_sernum.ReadOnly = true;
            // 
            // col_note_contact
            // 
            this.col_note_contact.DataPropertyName = "contact";
            this.col_note_contact.HeaderText = "ชื่อลูกค้า";
            this.col_note_contact.MinimumWidth = 180;
            this.col_note_contact.Name = "col_note_contact";
            this.col_note_contact.ReadOnly = true;
            this.col_note_contact.Width = 180;
            // 
            // col_note_is_mapdrive
            // 
            this.col_note_is_mapdrive.DataPropertyName = "is_mapdrive";
            this.col_note_is_mapdrive.HeaderText = "Map Drive";
            this.col_note_is_mapdrive.MinimumWidth = 35;
            this.col_note_is_mapdrive.Name = "col_note_is_mapdrive";
            this.col_note_is_mapdrive.ReadOnly = true;
            this.col_note_is_mapdrive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_note_is_mapdrive.Width = 35;
            // 
            // col_note_is_installupdate
            // 
            this.col_note_is_installupdate.DataPropertyName = "is_installupdate";
            this.col_note_is_installupdate.HeaderText = "Ins. / Up";
            this.col_note_is_installupdate.MinimumWidth = 35;
            this.col_note_is_installupdate.Name = "col_note_is_installupdate";
            this.col_note_is_installupdate.ReadOnly = true;
            this.col_note_is_installupdate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_note_is_installupdate.Width = 35;
            // 
            // col_note_is_error
            // 
            this.col_note_is_error.DataPropertyName = "is_error";
            this.col_note_is_error.HeaderText = "Error";
            this.col_note_is_error.MinimumWidth = 35;
            this.col_note_is_error.Name = "col_note_is_error";
            this.col_note_is_error.ReadOnly = true;
            this.col_note_is_error.Width = 35;
            // 
            // col_note_is_installfonts
            // 
            this.col_note_is_installfonts.DataPropertyName = "is_font";
            this.col_note_is_installfonts.HeaderText = "Ins. Fonts";
            this.col_note_is_installfonts.MinimumWidth = 35;
            this.col_note_is_installfonts.Name = "col_note_is_installfonts";
            this.col_note_is_installfonts.ReadOnly = true;
            this.col_note_is_installfonts.Width = 35;
            // 
            // col_note_is_print
            // 
            this.col_note_is_print.DataPropertyName = "is_print";
            this.col_note_is_print.HeaderText = "Print";
            this.col_note_is_print.MinimumWidth = 35;
            this.col_note_is_print.Name = "col_note_is_print";
            this.col_note_is_print.ReadOnly = true;
            this.col_note_is_print.Width = 35;
            // 
            // col_note_is_training
            // 
            this.col_note_is_training.DataPropertyName = "is_training";
            this.col_note_is_training.HeaderText = "อบรม";
            this.col_note_is_training.MinimumWidth = 35;
            this.col_note_is_training.Name = "col_note_is_training";
            this.col_note_is_training.ReadOnly = true;
            this.col_note_is_training.Width = 35;
            // 
            // col_note_is_stock
            // 
            this.col_note_is_stock.DataPropertyName = "is_stock";
            this.col_note_is_stock.HeaderText = "สินค้า";
            this.col_note_is_stock.MinimumWidth = 35;
            this.col_note_is_stock.Name = "col_note_is_stock";
            this.col_note_is_stock.ReadOnly = true;
            this.col_note_is_stock.Width = 35;
            // 
            // col_note_is_form
            // 
            this.col_note_is_form.DataPropertyName = "is_form";
            this.col_note_is_form.HeaderText = "Form Rep.";
            this.col_note_is_form.MinimumWidth = 35;
            this.col_note_is_form.Name = "col_note_is_form";
            this.col_note_is_form.ReadOnly = true;
            this.col_note_is_form.Width = 35;
            // 
            // col_note_is_reportexcel
            // 
            this.col_note_is_reportexcel.DataPropertyName = "is_reportexcel";
            this.col_note_is_reportexcel.HeaderText = "Rep.> Excel";
            this.col_note_is_reportexcel.MinimumWidth = 35;
            this.col_note_is_reportexcel.Name = "col_note_is_reportexcel";
            this.col_note_is_reportexcel.ReadOnly = true;
            this.col_note_is_reportexcel.Width = 35;
            // 
            // col_note_is_statement
            // 
            this.col_note_is_statement.DataPropertyName = "is_statement";
            this.col_note_is_statement.HeaderText = "สร้างงบฯ";
            this.col_note_is_statement.MinimumWidth = 35;
            this.col_note_is_statement.Name = "col_note_is_statement";
            this.col_note_is_statement.ReadOnly = true;
            this.col_note_is_statement.Width = 35;
            // 
            // col_note_is_assets
            // 
            this.col_note_is_assets.DataPropertyName = "is_asset";
            this.col_note_is_assets.HeaderText = "ท/ส ค่าเสื่อม";
            this.col_note_is_assets.MinimumWidth = 35;
            this.col_note_is_assets.Name = "col_note_is_assets";
            this.col_note_is_assets.ReadOnly = true;
            this.col_note_is_assets.Width = 35;
            // 
            // col_note_is_secure
            // 
            this.col_note_is_secure.DataPropertyName = "is_secure";
            this.col_note_is_secure.HeaderText = "Secure";
            this.col_note_is_secure.MinimumWidth = 35;
            this.col_note_is_secure.Name = "col_note_is_secure";
            this.col_note_is_secure.ReadOnly = true;
            this.col_note_is_secure.Width = 35;
            // 
            // col_note_is_yearend
            // 
            this.col_note_is_yearend.DataPropertyName = "is_yearend";
            this.col_note_is_yearend.HeaderText = "Year End";
            this.col_note_is_yearend.MinimumWidth = 35;
            this.col_note_is_yearend.Name = "col_note_is_yearend";
            this.col_note_is_yearend.ReadOnly = true;
            this.col_note_is_yearend.Width = 35;
            // 
            // col_note_is_period
            // 
            this.col_note_is_period.DataPropertyName = "is_period";
            this.col_note_is_period.HeaderText = "วันที่ไม่อยู่ในงวด";
            this.col_note_is_period.MinimumWidth = 35;
            this.col_note_is_period.Name = "col_note_is_period";
            this.col_note_is_period.ReadOnly = true;
            this.col_note_is_period.Width = 35;
            // 
            // col_note_is_mail
            // 
            this.col_note_is_mail.DataPropertyName = "is_mail";
            this.col_note_is_mail.HeaderText = "Mail รอสาย/หลุด";
            this.col_note_is_mail.MinimumWidth = 35;
            this.col_note_is_mail.Name = "col_note_is_mail";
            this.col_note_is_mail.ReadOnly = true;
            this.col_note_is_mail.Width = 35;
            // 
            // col_note_is_transfer
            // 
            this.col_note_is_transfer.DataPropertyName = "is_transfer";
            this.col_note_is_transfer.HeaderText = "โอนฝ่ายขาย";
            this.col_note_is_transfer.MinimumWidth = 35;
            this.col_note_is_transfer.Name = "col_note_is_transfer";
            this.col_note_is_transfer.ReadOnly = true;
            this.col_note_is_transfer.Width = 35;
            // 
            // col_note_is_other
            // 
            this.col_note_is_other.DataPropertyName = "is_other";
            this.col_note_is_other.HeaderText = "อื่น ๆ";
            this.col_note_is_other.MinimumWidth = 35;
            this.col_note_is_other.Name = "col_note_is_other";
            this.col_note_is_other.ReadOnly = true;
            this.col_note_is_other.Width = 35;
            // 
            // col_note_remark
            // 
            this.col_note_remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_note_remark.DataPropertyName = "remark";
            this.col_note_remark.HeaderText = "หมายเหตุ";
            this.col_note_remark.MinimumWidth = 100;
            this.col_note_remark.Name = "col_note_remark";
            this.col_note_remark.ReadOnly = true;
            // 
            // FormNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 631);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCloud);
            this.Controls.Add(this.lblMA);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblAddr);
            this.Controls.Add(this.lblWorkingDate);
            this.Controls.Add(this.lblCompnam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormNote";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Note";
            this.Load += new System.EventHandler(this.FormNote_Load);
            this.panel3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private CC.XDatagrid dgvNote;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCompnam;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWorkingDate;
        private System.Windows.Forms.ToolStripButton btnWorkingDate;
        private CC.XTimePicker inlineEnd;
        private CC.XTimePicker inlineStart;
        private CC.XTextEditMasked inlineSernum;
        private CC.XTextEdit inlineContact;
        private CC.XTextEdit inlineRemark;
        private CC.XCheckBox inlineMapdrive;
        private CC.XCheckBox inlineOther;
        private CC.XCheckBox inlineTransfer;
        private CC.XCheckBox inlineMail;
        private CC.XCheckBox inlinePeriod;
        private CC.XCheckBox inlineYearend;
        private CC.XCheckBox inlineSecure;
        private CC.XCheckBox inlineAsset;
        private CC.XCheckBox inlineStatement;
        private CC.XCheckBox inlineReportExcel;
        private CC.XCheckBox inlineForm;
        private CC.XCheckBox inlineStock;
        private CC.XCheckBox inlineTrain;
        private CC.XCheckBox inlinePrint;
        private CC.XCheckBox inlineFont;
        private CC.XCheckBox inlineError;
        private CC.XCheckBox inlineInstall;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_note;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_start;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_end;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_sernum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_mapdrive;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_installupdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_error;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_installfonts;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_print;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_training;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_form;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_reportexcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_statement;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_assets;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_secure;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_yearend;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_period;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_mail;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_transfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_is_other;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_note_remark;
        private CC.XDropdownList inlineTrainType;
        private CC.XDropdownList inlineBreakType;
        private System.Windows.Forms.Label inlineDuration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMA;
        private System.Windows.Forms.Label lblCloud;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPassword;
    }
}