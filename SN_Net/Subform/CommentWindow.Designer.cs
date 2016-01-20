namespace SN_Net.Subform
{
    partial class CommentWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommentWindow));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDeletePath = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dgvComment = new System.Windows.Forms.DataGridView();
            this.dgvComplain = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveComment = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStopComment = new System.Windows.Forms.Button();
            this.btnEditComment = new System.Windows.Forms.Button();
            this.btnDeleteComment = new System.Windows.Forms.Button();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.btnSaveComplain = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStopComplain = new System.Windows.Forms.Button();
            this.btnEditComplain = new System.Windows.Forms.Button();
            this.btnDeleteComplain = new System.Windows.Forms.Button();
            this.btnAddComplain = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplain)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClose.Location = new System.Drawing.Point(462, 497);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 36);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "ปิด";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "ไฟล์เสียง";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFilePath.Location = new System.Drawing.Point(70, 394);
            this.txtFilePath.MaxLength = 255;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(878, 23);
            this.txtFilePath.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Audio Files | *.wav;*.mp3";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // btnDeletePath
            // 
            this.btnDeletePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePath.Image = global::SN_Net.Properties.Resources.remove;
            this.btnDeletePath.Location = new System.Drawing.Point(973, 393);
            this.btnDeletePath.Name = "btnDeletePath";
            this.btnDeletePath.Size = new System.Drawing.Size(26, 25);
            this.btnDeletePath.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btnDeletePath, "Remove file path");
            this.btnDeletePath.UseVisualStyleBackColor = true;
            this.btnDeletePath.Click += new System.EventHandler(this.btnDeletePath_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Image = global::SN_Net.Properties.Resources.zoom;
            this.btnBrowse.Location = new System.Drawing.Point(946, 393);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 25);
            this.btnBrowse.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnBrowse, "Browse file path");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dgvComment
            // 
            this.dgvComment.AllowUserToAddRows = false;
            this.dgvComment.AllowUserToDeleteRows = false;
            this.dgvComment.AllowUserToResizeColumns = false;
            this.dgvComment.AllowUserToResizeRows = false;
            this.dgvComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComment.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvComment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvComment.EnableHeadersVisualStyles = false;
            this.dgvComment.Location = new System.Drawing.Point(3, 30);
            this.dgvComment.MultiSelect = false;
            this.dgvComment.Name = "dgvComment";
            this.dgvComment.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComment.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvComment.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvComment.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvComment.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvComment.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvComment.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvComment.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvComment.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvComment.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvComment.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComment.RowTemplate.Height = 25;
            this.dgvComment.RowTemplate.ReadOnly = true;
            this.dgvComment.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComment.Size = new System.Drawing.Size(980, 153);
            this.dgvComment.StandardTab = true;
            this.dgvComment.TabIndex = 6;
            // 
            // dgvComplain
            // 
            this.dgvComplain.AllowUserToAddRows = false;
            this.dgvComplain.AllowUserToDeleteRows = false;
            this.dgvComplain.AllowUserToResizeColumns = false;
            this.dgvComplain.AllowUserToResizeRows = false;
            this.dgvComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComplain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvComplain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComplain.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvComplain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvComplain.EnableHeadersVisualStyles = false;
            this.dgvComplain.Location = new System.Drawing.Point(3, 29);
            this.dgvComplain.MultiSelect = false;
            this.dgvComplain.Name = "dgvComplain";
            this.dgvComplain.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComplain.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvComplain.RowHeadersVisible = false;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvComplain.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvComplain.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvComplain.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvComplain.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvComplain.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvComplain.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvComplain.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvComplain.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComplain.RowTemplate.Height = 25;
            this.dgvComplain.RowTemplate.ReadOnly = true;
            this.dgvComplain.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComplain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComplain.Size = new System.Drawing.Size(980, 153);
            this.dgvComplain.StandardTab = true;
            this.dgvComplain.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveComment);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnStopComment);
            this.splitContainer1.Panel1.Controls.Add(this.btnEditComment);
            this.splitContainer1.Panel1.Controls.Add(this.btnDeleteComment);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddComment);
            this.splitContainer1.Panel1.Controls.Add(this.dgvComment);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveComplain);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btnStopComplain);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditComplain);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteComplain);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddComplain);
            this.splitContainer1.Panel2.Controls.Add(this.dgvComplain);
            this.splitContainer1.Size = new System.Drawing.Size(986, 375);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 20;
            // 
            // btnSaveComment
            // 
            this.btnSaveComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSaveComment.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveComment.Image")));
            this.btnSaveComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveComment.Location = new System.Drawing.Point(853, 5);
            this.btnSaveComment.Name = "btnSaveComment";
            this.btnSaveComment.Size = new System.Drawing.Size(68, 23);
            this.btnSaveComment.TabIndex = 12;
            this.btnSaveComment.Text = "บันทึก";
            this.btnSaveComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveComment.UseVisualStyleBackColor = true;
            this.btnSaveComment.Click += new System.EventHandler(this.btnSaveComment_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Comment";
            // 
            // btnStopComment
            // 
            this.btnStopComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnStopComment.Image = global::SN_Net.Properties.Resources.stop1;
            this.btnStopComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopComment.Location = new System.Drawing.Point(785, 5);
            this.btnStopComment.Name = "btnStopComment";
            this.btnStopComment.Size = new System.Drawing.Size(68, 23);
            this.btnStopComment.TabIndex = 10;
            this.btnStopComment.Text = "ยกเลิก";
            this.btnStopComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopComment.UseVisualStyleBackColor = true;
            this.btnStopComment.Click += new System.EventHandler(this.btnStopComment_Click);
            // 
            // btnEditComment
            // 
            this.btnEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnEditComment.Image = global::SN_Net.Properties.Resources.edit1;
            this.btnEditComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditComment.Location = new System.Drawing.Point(723, 5);
            this.btnEditComment.Name = "btnEditComment";
            this.btnEditComment.Size = new System.Drawing.Size(62, 23);
            this.btnEditComment.TabIndex = 9;
            this.btnEditComment.Text = "แก้ไข";
            this.btnEditComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditComment.UseVisualStyleBackColor = true;
            this.btnEditComment.Click += new System.EventHandler(this.btnEditComment_Click);
            // 
            // btnDeleteComment
            // 
            this.btnDeleteComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteComment.Enabled = false;
            this.btnDeleteComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDeleteComment.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteComment.Image")));
            this.btnDeleteComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteComment.Location = new System.Drawing.Point(921, 5);
            this.btnDeleteComment.Name = "btnDeleteComment";
            this.btnDeleteComment.Size = new System.Drawing.Size(62, 23);
            this.btnDeleteComment.TabIndex = 8;
            this.btnDeleteComment.Text = "ลบ";
            this.btnDeleteComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteComment.UseVisualStyleBackColor = true;
            this.btnDeleteComment.Click += new System.EventHandler(this.btnDeleteComment_Click);
            // 
            // btnAddComment
            // 
            this.btnAddComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddComment.Image = global::SN_Net.Properties.Resources.plus;
            this.btnAddComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddComment.Location = new System.Drawing.Point(661, 5);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(62, 23);
            this.btnAddComment.TabIndex = 7;
            this.btnAddComment.Text = "เพิ่ม";
            this.btnAddComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // btnSaveComplain
            // 
            this.btnSaveComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSaveComplain.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveComplain.Image")));
            this.btnSaveComplain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveComplain.Location = new System.Drawing.Point(853, 4);
            this.btnSaveComplain.Name = "btnSaveComplain";
            this.btnSaveComplain.Size = new System.Drawing.Size(68, 23);
            this.btnSaveComplain.TabIndex = 15;
            this.btnSaveComplain.Text = "บันทึก";
            this.btnSaveComplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveComplain.UseVisualStyleBackColor = true;
            this.btnSaveComplain.Click += new System.EventHandler(this.btnSaveComplain_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Complain";
            // 
            // btnStopComplain
            // 
            this.btnStopComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnStopComplain.Image = global::SN_Net.Properties.Resources.stop1;
            this.btnStopComplain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStopComplain.Location = new System.Drawing.Point(785, 4);
            this.btnStopComplain.Name = "btnStopComplain";
            this.btnStopComplain.Size = new System.Drawing.Size(68, 23);
            this.btnStopComplain.TabIndex = 13;
            this.btnStopComplain.Text = "ยกเลิก";
            this.btnStopComplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStopComplain.UseVisualStyleBackColor = true;
            this.btnStopComplain.Click += new System.EventHandler(this.btnStopComplain_Click);
            // 
            // btnEditComplain
            // 
            this.btnEditComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnEditComplain.Image = global::SN_Net.Properties.Resources.edit1;
            this.btnEditComplain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditComplain.Location = new System.Drawing.Point(723, 4);
            this.btnEditComplain.Name = "btnEditComplain";
            this.btnEditComplain.Size = new System.Drawing.Size(62, 23);
            this.btnEditComplain.TabIndex = 12;
            this.btnEditComplain.Text = "แก้ไข";
            this.btnEditComplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditComplain.UseVisualStyleBackColor = true;
            this.btnEditComplain.Click += new System.EventHandler(this.btnEditComplain_Click);
            // 
            // btnDeleteComplain
            // 
            this.btnDeleteComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteComplain.Enabled = false;
            this.btnDeleteComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDeleteComplain.Image = global::SN_Net.Properties.Resources.trash1;
            this.btnDeleteComplain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteComplain.Location = new System.Drawing.Point(921, 4);
            this.btnDeleteComplain.Name = "btnDeleteComplain";
            this.btnDeleteComplain.Size = new System.Drawing.Size(62, 23);
            this.btnDeleteComplain.TabIndex = 11;
            this.btnDeleteComplain.Text = "ลบ";
            this.btnDeleteComplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteComplain.UseVisualStyleBackColor = true;
            this.btnDeleteComplain.Click += new System.EventHandler(this.btnDeleteComplain_Click);
            // 
            // btnAddComplain
            // 
            this.btnAddComplain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddComplain.Image = global::SN_Net.Properties.Resources.plus;
            this.btnAddComplain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddComplain.Location = new System.Drawing.Point(661, 4);
            this.btnAddComplain.Name = "btnAddComplain";
            this.btnAddComplain.Size = new System.Drawing.Size(62, 23);
            this.btnAddComplain.TabIndex = 10;
            this.btnAddComplain.Text = "เพิ่ม";
            this.btnAddComplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddComplain.UseVisualStyleBackColor = true;
            this.btnAddComplain.Click += new System.EventHandler(this.btnAddComplain_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(12, 423);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(986, 63);
            this.axWindowsMediaPlayer1.TabIndex = 18;
            // 
            // CommentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 545);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.btnDeletePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "CommentWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Comment / Complain";
            this.Load += new System.EventHandler(this.CommentWindow_Load);
            this.Shown += new System.EventHandler(this.CommentWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplain)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDeletePath;
        private System.Windows.Forms.ToolTip toolTip1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        public System.Windows.Forms.DataGridView dgvComment;
        public System.Windows.Forms.DataGridView dgvComplain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnEditComment;
        private System.Windows.Forms.Button btnDeleteComment;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.Button btnEditComplain;
        private System.Windows.Forms.Button btnDeleteComplain;
        private System.Windows.Forms.Button btnAddComplain;
        private System.Windows.Forms.Button btnStopComment;
        private System.Windows.Forms.Button btnStopComplain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveComment;
        private System.Windows.Forms.Button btnSaveComplain;

    }
}