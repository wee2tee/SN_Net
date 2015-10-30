namespace SN_Net.Subform
{
    partial class SupportStatWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupportStatWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripPrint = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblLeaveRemark = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chProblem = new System.Windows.Forms.CheckBox();
            this.chBreak = new System.Windows.Forms.CheckBox();
            this.cbProblem = new System.Windows.Forms.ComboBox();
            this.btnViewNote = new System.Windows.Forms.Button();
            this.cbReason = new System.Windows.Forms.ComboBox();
            this.txtDummy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSupportCode = new System.Windows.Forms.ComboBox();
            this.dgvNote = new System.Windows.Forms.DataGridView();
            this.txtSernum = new SN_Net.MiscClass.CustomTextBox();
            this.dtDateEnd = new SN_Net.MiscClass.CustomDateTimePicker();
            this.dtDateStart = new SN_Net.MiscClass.CustomDateTimePicker();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripInfo,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 685);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(865, 22);
            this.statusStrip1.TabIndex = 30;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.BackColor = System.Drawing.Color.Transparent;
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.Size = new System.Drawing.Size(22, 17);
            this.toolStripInfo.Text = "     ";
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.BackColor = System.Drawing.Color.Transparent;
            this.toolStripProcessing.ForeColor = System.Drawing.Color.Green;
            this.toolStripProcessing.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcessing.Image")));
            this.toolStripProcessing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcessing.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProcessing.Size = new System.Drawing.Size(818, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEdit,
            this.toolStripSeparator2,
            this.toolStripStop,
            this.toolStripSave,
            this.toolStripSeparator1,
            this.toolStripPrint,
            this.toolStripExport});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(865, 43);
            this.toolStrip1.TabIndex = 31;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEdit.Enabled = false;
            this.toolStripEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.toolStripEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEdit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(36, 40);
            this.toolStripEdit.Text = "แก้ไขรายการ <Alt+E>";
            this.toolStripEdit.Click += new System.EventHandler(this.toolStripEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripStop
            // 
            this.toolStripStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStop.Enabled = false;
            this.toolStripStop.Image = global::SN_Net.Properties.Resources.stop;
            this.toolStripStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripStop.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripStop.Name = "toolStripStop";
            this.toolStripStop.Size = new System.Drawing.Size(36, 40);
            this.toolStripStop.Text = "ยกเลิกการแก้ไข <Esc>";
            this.toolStripStop.Click += new System.EventHandler(this.toolStripStop_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSave.Enabled = false;
            this.toolStripSave.Image = global::SN_Net.Properties.Resources.save;
            this.toolStripSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(36, 40);
            this.toolStripSave.Text = "บันทึกการแก้ไข <F9>";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripExport
            // 
            this.toolStripExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripExport.Enabled = false;
            this.toolStripExport.Image = global::SN_Net.Properties.Resources.export;
            this.toolStripExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExport.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripExport.Name = "toolStripExport";
            this.toolStripExport.Size = new System.Drawing.Size(36, 40);
            this.toolStripExport.Text = "Export to CSV <F12>";
            this.toolStripExport.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // toolStripPrint
            // 
            this.toolStripPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrint.Image = global::SN_Net.Properties.Resources.printer;
            this.toolStripPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPrint.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripPrint.Name = "toolStripPrint";
            this.toolStripPrint.Size = new System.Drawing.Size(36, 40);
            this.toolStripPrint.Text = "Print <Alt+P>";
            this.toolStripPrint.Click += new System.EventHandler(this.toolStripPrint_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 43);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblLeaveRemark);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtSernum);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.chProblem);
            this.splitContainer1.Panel1.Controls.Add(this.chBreak);
            this.splitContainer1.Panel1.Controls.Add(this.dtDateEnd);
            this.splitContainer1.Panel1.Controls.Add(this.cbProblem);
            this.splitContainer1.Panel1.Controls.Add(this.btnViewNote);
            this.splitContainer1.Panel1.Controls.Add(this.cbReason);
            this.splitContainer1.Panel1.Controls.Add(this.txtDummy);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dtDateStart);
            this.splitContainer1.Panel1.Controls.Add(this.cbSupportCode);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvNote);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.splitContainer1.Size = new System.Drawing.Size(865, 642);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 32;
            // 
            // lblLeaveRemark
            // 
            this.lblLeaveRemark.AutoSize = true;
            this.lblLeaveRemark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblLeaveRemark.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.lblLeaveRemark.LinkColor = System.Drawing.Color.Red;
            this.lblLeaveRemark.Location = new System.Drawing.Point(11, 84);
            this.lblLeaveRemark.Name = "lblLeaveRemark";
            this.lblLeaveRemark.Size = new System.Drawing.Size(256, 16);
            this.lblLeaveRemark.TabIndex = 130;
            this.lblLeaveRemark.TabStop = true;
            this.lblLeaveRemark.Text = "ลางาน,ออกพบลูกค้า xx วัน, xx ชั่วโมง, xx นาที";
            this.lblLeaveRemark.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLeaveRemark_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(352, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 128;
            this.label1.Text = "Serial No.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(179, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 16);
            this.label9.TabIndex = 119;
            this.label9.Text = "ถึง";
            // 
            // chProblem
            // 
            this.chProblem.AutoSize = true;
            this.chProblem.Checked = true;
            this.chProblem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chProblem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chProblem.Location = new System.Drawing.Point(336, 27);
            this.chProblem.Name = "chProblem";
            this.chProblem.Size = new System.Drawing.Size(129, 20);
            this.chProblem.TabIndex = 124;
            this.chProblem.Text = "ประเภทสายสนทนา";
            this.chProblem.UseVisualStyleBackColor = true;
            // 
            // chBreak
            // 
            this.chBreak.AutoSize = true;
            this.chBreak.Checked = true;
            this.chBreak.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBreak.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chBreak.Location = new System.Drawing.Point(336, 54);
            this.chBreak.Name = "chBreak";
            this.chBreak.Size = new System.Drawing.Size(127, 20);
            this.chBreak.TabIndex = 125;
            this.chBreak.Text = "ประเภทการพักสาย";
            this.chBreak.UseVisualStyleBackColor = true;
            // 
            // cbProblem
            // 
            this.cbProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProblem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbProblem.FormattingEnabled = true;
            this.cbProblem.Location = new System.Drawing.Point(468, 24);
            this.cbProblem.Name = "cbProblem";
            this.cbProblem.Size = new System.Drawing.Size(174, 24);
            this.cbProblem.TabIndex = 122;
            // 
            // btnViewNote
            // 
            this.btnViewNote.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnViewNote.Image = global::SN_Net.Properties.Resources.report;
            this.btnViewNote.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnViewNote.Location = new System.Drawing.Point(661, 21);
            this.btnViewNote.Name = "btnViewNote";
            this.btnViewNote.Size = new System.Drawing.Size(78, 57);
            this.btnViewNote.TabIndex = 116;
            this.btnViewNote.Text = "View stat.";
            this.btnViewNote.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnViewNote.UseVisualStyleBackColor = true;
            this.btnViewNote.Click += new System.EventHandler(this.btnViewNote_Click);
            // 
            // cbReason
            // 
            this.cbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbReason.FormattingEnabled = true;
            this.cbReason.Location = new System.Drawing.Point(468, 52);
            this.cbReason.Name = "cbReason";
            this.cbReason.Size = new System.Drawing.Size(174, 24);
            this.cbReason.TabIndex = 123;
            // 
            // txtDummy
            // 
            this.txtDummy.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtDummy.Location = new System.Drawing.Point(217, 28);
            this.txtDummy.Name = "txtDummy";
            this.txtDummy.Size = new System.Drawing.Size(57, 20);
            this.txtDummy.TabIndex = 115;
            this.txtDummy.Text = "txtDummy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(11, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 108;
            this.label3.Text = "วันที่ จาก";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(11, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 107;
            this.label2.Text = "Support #";
            // 
            // cbSupportCode
            // 
            this.cbSupportCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupportCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbSupportCode.FormattingEnabled = true;
            this.cbSupportCode.Location = new System.Drawing.Point(77, 27);
            this.cbSupportCode.Name = "cbSupportCode";
            this.cbSupportCode.Size = new System.Drawing.Size(96, 24);
            this.cbSupportCode.TabIndex = 0;
            // 
            // dgvNote
            // 
            this.dgvNote.AllowUserToAddRows = false;
            this.dgvNote.AllowUserToDeleteRows = false;
            this.dgvNote.AllowUserToResizeColumns = false;
            this.dgvNote.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNote.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNote.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNote.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvNote.EnableHeadersVisualStyles = false;
            this.dgvNote.Location = new System.Drawing.Point(3, 0);
            this.dgvNote.MultiSelect = false;
            this.dgvNote.Name = "dgvNote";
            this.dgvNote.ReadOnly = true;
            this.dgvNote.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvNote.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNote.RowTemplate.Height = 25;
            this.dgvNote.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNote.Size = new System.Drawing.Size(859, 525);
            this.dgvNote.TabIndex = 1;
            // 
            // txtSernum
            // 
            this.txtSernum.BackColor = System.Drawing.Color.White;
            this.txtSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSernum.CharUpperCase = false;
            this.txtSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSernum.Location = new System.Drawing.Point(468, 80);
            this.txtSernum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSernum.MaxChar = 0;
            this.txtSernum.Name = "txtSernum";
            this.txtSernum.ReadOnly = false;
            this.txtSernum.SelectionLength = 0;
            this.txtSernum.SelectionStart = 0;
            this.txtSernum.Size = new System.Drawing.Size(118, 23);
            this.txtSernum.TabIndex = 127;
            this.txtSernum.Texts = "";
            // 
            // dtDateEnd
            // 
            this.dtDateEnd.BackColor = System.Drawing.Color.White;
            this.dtDateEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDateEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateEnd.Location = new System.Drawing.Point(208, 55);
            this.dtDateEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDateEnd.Name = "dtDateEnd";
            this.dtDateEnd.Read_Only = false;
            this.dtDateEnd.Size = new System.Drawing.Size(96, 23);
            this.dtDateEnd.TabIndex = 118;
            this.dtDateEnd.Texts = "30/10/2558";
            this.dtDateEnd.TextsMysql = "2015-10-30";
            this.dtDateEnd.ValDateTime = new System.DateTime(2015, 10, 7, 15, 17, 35, 103);
            // 
            // dtDateStart
            // 
            this.dtDateStart.BackColor = System.Drawing.Color.White;
            this.dtDateStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDateStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateStart.Location = new System.Drawing.Point(77, 55);
            this.dtDateStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDateStart.Name = "dtDateStart";
            this.dtDateStart.Read_Only = false;
            this.dtDateStart.Size = new System.Drawing.Size(96, 23);
            this.dtDateStart.TabIndex = 1;
            this.dtDateStart.Texts = "30/10/2558";
            this.dtDateStart.TextsMysql = "2015-10-30";
            this.dtDateStart.ValDateTime = new System.DateTime(2015, 10, 7, 15, 17, 35, 103);
            // 
            // SupportStatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(865, 707);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Name = "SupportStatWindow";
            this.Text = "Support Statistics.";
            this.Load += new System.EventHandler(this.SupportStatWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripInfo;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnViewNote;
        private System.Windows.Forms.TextBox txtDummy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private MiscClass.CustomDateTimePicker dtDateStart;
        private System.Windows.Forms.ComboBox cbSupportCode;
        private System.Windows.Forms.DataGridView dgvNote;
        private System.Windows.Forms.ToolStripButton toolStripPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label9;
        private MiscClass.CustomDateTimePicker dtDateEnd;
        private System.Windows.Forms.ComboBox cbReason;
        private System.Windows.Forms.ComboBox cbProblem;
        private System.Windows.Forms.CheckBox chBreak;
        private System.Windows.Forms.CheckBox chProblem;
        private MiscClass.CustomTextBox txtSernum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripExport;
        private System.Windows.Forms.ToolStripButton toolStripEdit;
        private System.Windows.Forms.ToolStripButton toolStripStop;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.LinkLabel lblLeaveRemark;
    }
}