namespace SN_Net.Subform
{
    partial class UsersGroupWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersGroupWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnItem = new System.Windows.Forms.ToolStripSplitButton();
            this.btnItemF8 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnItemF7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
            this.btnInquiryAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInquiryRest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearchTypcod = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMemberCount = new SN_Net.MiscClass.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTypdes = new SN_Net.MiscClass.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTypcod = new SN_Net.MiscClass.CustomTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMember = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvUnGroup = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMember)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripInfo,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 483);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(781, 22);
            this.statusStrip1.TabIndex = 2;
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
            this.toolStripProcessing.Size = new System.Drawing.Size(734, 17);
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
            this.btnAdd,
            this.btnEdit,
            this.btnDelete,
            this.btnStop,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnFirst,
            this.btnPrevious,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator2,
            this.btnItem,
            this.toolStripSeparator5,
            this.btnSearch,
            this.toolStripSeparator4,
            this.btnReload});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(781, 43);
            this.toolStrip1.TabIndex = 119;
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
            this.btnAdd.Text = "Add <Alt+A>";
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
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::SN_Net.Properties.Resources.trash;
            this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 40);
            this.btnDelete.Text = "Delete <Alt+D>";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = global::SN_Net.Properties.Resources.first;
            this.btnFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(36, 40);
            this.btnFirst.Text = "First record <Ctrl+Home>";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = global::SN_Net.Properties.Resources.previous;
            this.btnPrevious.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(36, 40);
            this.btnPrevious.Text = "Previous record <Page Up>";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::SN_Net.Properties.Resources.next;
            this.btnNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(36, 40);
            this.btnNext.Text = "Next record <Page Down>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = global::SN_Net.Properties.Resources.last;
            this.btnLast.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(36, 40);
            this.btnLast.Text = "Last record <Ctrl+End>";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // btnItem
            // 
            this.btnItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnItemF8,
            this.btnItemF7});
            this.btnItem.Image = global::SN_Net.Properties.Resources.item;
            this.btnItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(48, 40);
            this.btnItem.Text = "toolStripSplitButton1";
            this.btnItem.ButtonClick += new System.EventHandler(this.btnItem_ButtonClick);
            // 
            // btnItemF8
            // 
            this.btnItemF8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemF8.Name = "btnItemF8";
            this.btnItemF8.Size = new System.Drawing.Size(225, 22);
            this.btnItemF8.Text = "พนักงานในกลุ่ม <F8>";
            this.btnItemF8.Click += new System.EventHandler(this.btnItemF8_Click);
            // 
            // btnItemF7
            // 
            this.btnItemF7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemF7.Name = "btnItemF7";
            this.btnItemF7.Size = new System.Drawing.Size(225, 22);
            this.btnItemF7.Text = "พนักงานที่ยังไม่จัดเข้ากลุ่ม <F7>";
            this.btnItemF7.Click += new System.EventHandler(this.btnItemF7_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 43);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInquiryAll,
            this.btnInquiryRest,
            this.toolStripSeparator3,
            this.btnSearchTypcod});
            this.btnSearch.Image = global::SN_Net.Properties.Resources.search;
            this.btnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1, 1, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 40);
            this.btnSearch.Text = "ค้นหาจากรหัสกลุ่ม <Alt+S>";
            this.btnSearch.ButtonClick += new System.EventHandler(this.btnSearch_ButtonClick);
            // 
            // btnInquiryAll
            // 
            this.btnInquiryAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInquiryAll.Name = "btnInquiryAll";
            this.btnInquiryAll.Size = new System.Drawing.Size(245, 22);
            this.btnInquiryAll.Text = "เรียกดูข้อมูล ตั้งแต่ต้น <Ctrl+L>";
            this.btnInquiryAll.Click += new System.EventHandler(this.btnInquiryAll_Click);
            // 
            // btnInquiryRest
            // 
            this.btnInquiryRest.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInquiryRest.Name = "btnInquiryRest";
            this.btnInquiryRest.Size = new System.Drawing.Size(245, 22);
            this.btnInquiryRest.Text = "เรียกดูข้อมูล ตั้งแต่รายการนี้ <Alt+L>";
            this.btnInquiryRest.Click += new System.EventHandler(this.btnInquiryRest_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(242, 6);
            // 
            // btnSearchTypcod
            // 
            this.btnSearchTypcod.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchTypcod.Name = "btnSearchTypcod";
            this.btnSearchTypcod.Size = new System.Drawing.Size(245, 22);
            this.btnSearchTypcod.Text = "ค้นหาจากรหัสกลุ่ม <Alt+S>";
            this.btnSearchTypcod.Click += new System.EventHandler(this.btnSearchTypcod_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 43);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = global::SN_Net.Properties.Resources.refresh;
            this.btnReload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(36, 40);
            this.btnReload.Text = "Reload current data <F5>";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtMemberCount);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtTypdes);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtTypcod);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.splitContainer1.Size = new System.Drawing.Size(781, 440);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 120;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(150, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "คน";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(22, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "จำนวนสมาชิก";
            // 
            // txtMemberCount
            // 
            this.txtMemberCount.BackColor = System.Drawing.Color.White;
            this.txtMemberCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberCount.CharUpperCase = true;
            this.txtMemberCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtMemberCount.Location = new System.Drawing.Point(107, 69);
            this.txtMemberCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMemberCount.MaxChar = 10;
            this.txtMemberCount.Name = "txtMemberCount";
            this.txtMemberCount.Read_Only = true;
            this.txtMemberCount.SelectionLength = 0;
            this.txtMemberCount.SelectionStart = 0;
            this.txtMemberCount.Size = new System.Drawing.Size(40, 23);
            this.txtMemberCount.TabIndex = 4;
            this.txtMemberCount.Texts = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(22, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "คำอธิบาย";
            // 
            // txtTypdes
            // 
            this.txtTypdes.BackColor = System.Drawing.Color.White;
            this.txtTypdes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTypdes.CharUpperCase = false;
            this.txtTypdes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTypdes.Location = new System.Drawing.Point(107, 42);
            this.txtTypdes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTypdes.MaxChar = 50;
            this.txtTypdes.Name = "txtTypdes";
            this.txtTypdes.Read_Only = true;
            this.txtTypdes.SelectionLength = 0;
            this.txtTypdes.SelectionStart = 0;
            this.txtTypdes.Size = new System.Drawing.Size(446, 23);
            this.txtTypdes.TabIndex = 1;
            this.txtTypdes.Texts = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "รหัสกลุ่ม";
            // 
            // txtTypcod
            // 
            this.txtTypcod.BackColor = System.Drawing.Color.White;
            this.txtTypcod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTypcod.CharUpperCase = true;
            this.txtTypcod.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTypcod.Location = new System.Drawing.Point(107, 15);
            this.txtTypcod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTypcod.MaxChar = 10;
            this.txtTypcod.Name = "txtTypcod";
            this.txtTypcod.Read_Only = true;
            this.txtTypcod.SelectionLength = 0;
            this.txtTypcod.SelectionStart = 0;
            this.txtTypcod.Size = new System.Drawing.Size(115, 23);
            this.txtTypcod.TabIndex = 0;
            this.txtTypcod.Texts = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(777, 334);
            this.tabControl1.TabIndex = 119;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMember);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(769, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "พนักงานในกลุ่ม <F8>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMember
            // 
            this.dgvMember.AllowUserToAddRows = false;
            this.dgvMember.AllowUserToDeleteRows = false;
            this.dgvMember.AllowUserToResizeColumns = false;
            this.dgvMember.AllowUserToResizeRows = false;
            this.dgvMember.BackgroundColor = System.Drawing.Color.White;
            this.dgvMember.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMember.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMember.ColumnHeadersHeight = 28;
            this.dgvMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMember.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMember.EnableHeadersVisualStyles = false;
            this.dgvMember.Location = new System.Drawing.Point(3, 3);
            this.dgvMember.MultiSelect = false;
            this.dgvMember.Name = "dgvMember";
            this.dgvMember.ReadOnly = true;
            this.dgvMember.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvMember.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMember.RowTemplate.Height = 25;
            this.dgvMember.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMember.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMember.Size = new System.Drawing.Size(763, 299);
            this.dgvMember.StandardTab = true;
            this.dgvMember.TabIndex = 118;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvUnGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(769, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "พนักงานที่ยังไม่จัดเข้ากลุ่ม <F7>";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvUnGroup
            // 
            this.dgvUnGroup.AllowUserToAddRows = false;
            this.dgvUnGroup.AllowUserToDeleteRows = false;
            this.dgvUnGroup.AllowUserToResizeColumns = false;
            this.dgvUnGroup.AllowUserToResizeRows = false;
            this.dgvUnGroup.BackgroundColor = System.Drawing.Color.White;
            this.dgvUnGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUnGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUnGroup.ColumnHeadersHeight = 28;
            this.dgvUnGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUnGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnGroup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUnGroup.EnableHeadersVisualStyles = false;
            this.dgvUnGroup.Location = new System.Drawing.Point(3, 3);
            this.dgvUnGroup.MultiSelect = false;
            this.dgvUnGroup.Name = "dgvUnGroup";
            this.dgvUnGroup.ReadOnly = true;
            this.dgvUnGroup.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvUnGroup.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUnGroup.RowTemplate.Height = 25;
            this.dgvUnGroup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUnGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnGroup.Size = new System.Drawing.Size(763, 299);
            this.dgvUnGroup.StandardTab = true;
            this.dgvUnGroup.TabIndex = 119;
            // 
            // UsersGroupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(781, 505);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Name = "UsersGroupWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "จัดกลุ่มพนักงาน";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsersGroupWindow_FormClosing);
            this.Load += new System.EventHandler(this.UsersGroupWindow_Load);
            this.Shown += new System.EventHandler(this.UsersGroupWindow_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMember)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripInfo;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSplitButton btnSearch;
        private System.Windows.Forms.ToolStripMenuItem btnInquiryAll;
        private System.Windows.Forms.ToolStripMenuItem btnInquiryRest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnSearchTypcod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private MiscClass.CustomTextBox txtTypdes;
        private System.Windows.Forms.Label label1;
        private MiscClass.CustomTextBox txtTypcod;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvMember;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private MiscClass.CustomTextBox txtMemberCount;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripSplitButton btnItem;
        private System.Windows.Forms.ToolStripMenuItem btnItemF8;
        private System.Windows.Forms.ToolStripMenuItem btnItemF7;
        private System.Windows.Forms.DataGridView dgvUnGroup;
    }
}