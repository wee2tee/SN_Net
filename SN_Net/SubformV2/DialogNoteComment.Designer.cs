namespace SN_Net.Subform
{
    partial class DialogNoteComment
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inlineComment = new CC.XTextEdit();
            this.btnCommentAdd = new System.Windows.Forms.Button();
            this.dgvComment = new CC.XDatagrid();
            this.btnCommentSave = new System.Windows.Forms.Button();
            this.btnCommentStop = new System.Windows.Forms.Button();
            this.btnCommentDelete = new System.Windows.Forms.Button();
            this.btnCommentEdit = new System.Windows.Forms.Button();
            this.inlineComplain = new CC.XTextEdit();
            this.btnComplainAdd = new System.Windows.Forms.Button();
            this.dgvComplain = new CC.XDatagrid();
            this.btnComplainSave = new System.Windows.Forms.Button();
            this.btnComplainStop = new System.Windows.Forms.Button();
            this.btnComplainDelete = new System.Windows.Forms.Button();
            this.btnComplainEdit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.brPath = new CC.XBrowseBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.btnStopPath = new System.Windows.Forms.Button();
            this.btnEditPath = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPlay = new System.Windows.Forms.Button();
            this.col_complain_note_comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_complain_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_complain_recby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_complain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_comment_note_comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_comment_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_comment_recby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(306, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 36);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close <Esc>";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inlineComment);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommentAdd);
            this.splitContainer1.Panel1.Controls.Add(this.dgvComment);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommentSave);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommentStop);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommentDelete);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommentEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inlineComplain);
            this.splitContainer1.Panel2.Controls.Add(this.btnComplainAdd);
            this.splitContainer1.Panel2.Controls.Add(this.dgvComplain);
            this.splitContainer1.Panel2.Controls.Add(this.btnComplainSave);
            this.splitContainer1.Panel2.Controls.Add(this.btnComplainStop);
            this.splitContainer1.Panel2.Controls.Add(this.btnComplainDelete);
            this.splitContainer1.Panel2.Controls.Add(this.btnComplainEdit);
            this.splitContainer1.Size = new System.Drawing.Size(706, 253);
            this.splitContainer1.SplitterDistance = 122;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 7;
            // 
            // inlineComment
            // 
            this.inlineComment._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineComment._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineComment._ForeColor = System.Drawing.SystemColors.WindowText;
            this.inlineComment._MaxLength = 255;
            this.inlineComment._PasswordChar = '\0';
            this.inlineComment._ReadOnly = false;
            this.inlineComment._SelectionLength = 0;
            this.inlineComment._SelectionStart = 0;
            this.inlineComment._Text = "";
            this.inlineComment._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineComment.BackColor = System.Drawing.Color.White;
            this.inlineComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineComment.Location = new System.Drawing.Point(25, 45);
            this.inlineComment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineComment.Name = "inlineComment";
            this.inlineComment.Size = new System.Drawing.Size(142, 23);
            this.inlineComment.TabIndex = 2;
            this.inlineComment._TextChanged += new System.EventHandler(this.inlineComment__TextChanged);
            // 
            // btnCommentAdd
            // 
            this.btnCommentAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommentAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCommentAdd.Image = global::SN_Net.Properties.Resources.plus1;
            this.btnCommentAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCommentAdd.Location = new System.Drawing.Point(652, 2);
            this.btnCommentAdd.Name = "btnCommentAdd";
            this.btnCommentAdd.Size = new System.Drawing.Size(52, 25);
            this.btnCommentAdd.TabIndex = 1;
            this.btnCommentAdd.Text = "Add";
            this.btnCommentAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCommentAdd.UseVisualStyleBackColor = true;
            this.btnCommentAdd.Click += new System.EventHandler(this.btnCommentAdd_Click);
            // 
            // dgvComment
            // 
            this.dgvComment.AllowSortByColumnHeaderClicked = false;
            this.dgvComment.AllowUserToAddRows = false;
            this.dgvComment.AllowUserToDeleteRows = false;
            this.dgvComment.AllowUserToResizeColumns = false;
            this.dgvComment.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvComment.ColumnHeadersHeight = 28;
            this.dgvComment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvComment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_comment_note_comment,
            this.col_comment_id,
            this.col_comment_recby,
            this.col_comment});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComment.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvComment.EnableHeadersVisualStyles = false;
            this.dgvComment.FillEmptyRow = false;
            this.dgvComment.FocusedRowBorderRedLine = true;
            this.dgvComment.Location = new System.Drawing.Point(0, 0);
            this.dgvComment.Margin = new System.Windows.Forms.Padding(0);
            this.dgvComment.MultiSelect = false;
            this.dgvComment.Name = "dgvComment";
            this.dgvComment.ReadOnly = true;
            this.dgvComment.RowHeadersVisible = false;
            this.dgvComment.RowTemplate.Height = 26;
            this.dgvComment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComment.Size = new System.Drawing.Size(706, 122);
            this.dgvComment.StandardTab = true;
            this.dgvComment.TabIndex = 0;
            this.dgvComment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComment_CellDoubleClick);
            this.dgvComment.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvComment_CellPainting);
            this.dgvComment.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvComment_MouseClick);
            this.dgvComment.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // btnCommentSave
            // 
            this.btnCommentSave.Location = new System.Drawing.Point(243, 11);
            this.btnCommentSave.Name = "btnCommentSave";
            this.btnCommentSave.Size = new System.Drawing.Size(52, 23);
            this.btnCommentSave.TabIndex = 1;
            this.btnCommentSave.Text = "save";
            this.btnCommentSave.UseVisualStyleBackColor = true;
            this.btnCommentSave.Click += new System.EventHandler(this.btnCommentSave_Click);
            // 
            // btnCommentStop
            // 
            this.btnCommentStop.Location = new System.Drawing.Point(185, 11);
            this.btnCommentStop.Name = "btnCommentStop";
            this.btnCommentStop.Size = new System.Drawing.Size(52, 23);
            this.btnCommentStop.TabIndex = 1;
            this.btnCommentStop.Text = "stop";
            this.btnCommentStop.UseVisualStyleBackColor = true;
            this.btnCommentStop.Click += new System.EventHandler(this.btnCommentStop_Click);
            // 
            // btnCommentDelete
            // 
            this.btnCommentDelete.Location = new System.Drawing.Point(127, 11);
            this.btnCommentDelete.Name = "btnCommentDelete";
            this.btnCommentDelete.Size = new System.Drawing.Size(52, 23);
            this.btnCommentDelete.TabIndex = 1;
            this.btnCommentDelete.Text = "delete";
            this.btnCommentDelete.UseVisualStyleBackColor = true;
            this.btnCommentDelete.Click += new System.EventHandler(this.btnCommentDelete_Click);
            // 
            // btnCommentEdit
            // 
            this.btnCommentEdit.Location = new System.Drawing.Point(69, 11);
            this.btnCommentEdit.Name = "btnCommentEdit";
            this.btnCommentEdit.Size = new System.Drawing.Size(52, 23);
            this.btnCommentEdit.TabIndex = 1;
            this.btnCommentEdit.Text = "edit";
            this.btnCommentEdit.UseVisualStyleBackColor = true;
            this.btnCommentEdit.Click += new System.EventHandler(this.btnCommentEdit_Click);
            // 
            // inlineComplain
            // 
            this.inlineComplain._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineComplain._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineComplain._ForeColor = System.Drawing.SystemColors.WindowText;
            this.inlineComplain._MaxLength = 255;
            this.inlineComplain._PasswordChar = '\0';
            this.inlineComplain._ReadOnly = false;
            this.inlineComplain._SelectionLength = 0;
            this.inlineComplain._SelectionStart = 0;
            this.inlineComplain._Text = "";
            this.inlineComplain._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineComplain.BackColor = System.Drawing.Color.White;
            this.inlineComplain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineComplain.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineComplain.Location = new System.Drawing.Point(25, 43);
            this.inlineComplain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineComplain.Name = "inlineComplain";
            this.inlineComplain.Size = new System.Drawing.Size(142, 23);
            this.inlineComplain.TabIndex = 8;
            this.inlineComplain._TextChanged += new System.EventHandler(this.inlineComplain__TextChanged);
            // 
            // btnComplainAdd
            // 
            this.btnComplainAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplainAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnComplainAdd.Image = global::SN_Net.Properties.Resources.plus1;
            this.btnComplainAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnComplainAdd.Location = new System.Drawing.Point(652, 2);
            this.btnComplainAdd.Name = "btnComplainAdd";
            this.btnComplainAdd.Size = new System.Drawing.Size(52, 25);
            this.btnComplainAdd.TabIndex = 7;
            this.btnComplainAdd.Text = "Add";
            this.btnComplainAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnComplainAdd.UseVisualStyleBackColor = true;
            this.btnComplainAdd.Click += new System.EventHandler(this.btnComplainAdd_Click);
            // 
            // dgvComplain
            // 
            this.dgvComplain.AllowSortByColumnHeaderClicked = false;
            this.dgvComplain.AllowUserToAddRows = false;
            this.dgvComplain.AllowUserToDeleteRows = false;
            this.dgvComplain.AllowUserToResizeColumns = false;
            this.dgvComplain.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComplain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvComplain.ColumnHeadersHeight = 28;
            this.dgvComplain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvComplain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_complain_note_comment,
            this.col_complain_id,
            this.col_complain_recby,
            this.col_complain});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvComplain.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvComplain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComplain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvComplain.EnableHeadersVisualStyles = false;
            this.dgvComplain.FillEmptyRow = false;
            this.dgvComplain.FocusedRowBorderRedLine = true;
            this.dgvComplain.Location = new System.Drawing.Point(0, 0);
            this.dgvComplain.Margin = new System.Windows.Forms.Padding(0);
            this.dgvComplain.MultiSelect = false;
            this.dgvComplain.Name = "dgvComplain";
            this.dgvComplain.ReadOnly = true;
            this.dgvComplain.RowHeadersVisible = false;
            this.dgvComplain.RowTemplate.Height = 26;
            this.dgvComplain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComplain.Size = new System.Drawing.Size(706, 123);
            this.dgvComplain.StandardTab = true;
            this.dgvComplain.TabIndex = 2;
            this.dgvComplain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComplain_CellDoubleClick);
            this.dgvComplain.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvComplain_CellPainting);
            this.dgvComplain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvComplain_MouseClick);
            this.dgvComplain.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // btnComplainSave
            // 
            this.btnComplainSave.Location = new System.Drawing.Point(243, 13);
            this.btnComplainSave.Name = "btnComplainSave";
            this.btnComplainSave.Size = new System.Drawing.Size(52, 23);
            this.btnComplainSave.TabIndex = 3;
            this.btnComplainSave.Text = "save";
            this.btnComplainSave.UseVisualStyleBackColor = true;
            this.btnComplainSave.Click += new System.EventHandler(this.btnComplainSave_Click);
            // 
            // btnComplainStop
            // 
            this.btnComplainStop.Location = new System.Drawing.Point(185, 13);
            this.btnComplainStop.Name = "btnComplainStop";
            this.btnComplainStop.Size = new System.Drawing.Size(52, 23);
            this.btnComplainStop.TabIndex = 4;
            this.btnComplainStop.Text = "stop";
            this.btnComplainStop.UseVisualStyleBackColor = true;
            this.btnComplainStop.Click += new System.EventHandler(this.btnComplainStop_Click);
            // 
            // btnComplainDelete
            // 
            this.btnComplainDelete.Location = new System.Drawing.Point(127, 13);
            this.btnComplainDelete.Name = "btnComplainDelete";
            this.btnComplainDelete.Size = new System.Drawing.Size(52, 23);
            this.btnComplainDelete.TabIndex = 5;
            this.btnComplainDelete.Text = "delete";
            this.btnComplainDelete.UseVisualStyleBackColor = true;
            this.btnComplainDelete.Click += new System.EventHandler(this.btnComplainDelete_Click);
            // 
            // btnComplainEdit
            // 
            this.btnComplainEdit.Location = new System.Drawing.Point(69, 13);
            this.btnComplainEdit.Name = "btnComplainEdit";
            this.btnComplainEdit.Size = new System.Drawing.Size(52, 23);
            this.btnComplainEdit.TabIndex = 6;
            this.btnComplainEdit.Text = "edit";
            this.btnComplainEdit.UseVisualStyleBackColor = true;
            this.btnComplainEdit.Click += new System.EventHandler(this.btnComplainEdit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Audio file path";
            // 
            // brPath
            // 
            this.brPath._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brPath._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.brPath._ReadOnly = true;
            this.brPath._Text = "";
            this.brPath._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brPath._UseImage = true;
            this.brPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.brPath.BackColor = System.Drawing.Color.White;
            this.brPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brPath.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brPath.Location = new System.Drawing.Point(101, 268);
            this.brPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brPath.Name = "brPath";
            this.brPath.Size = new System.Drawing.Size(453, 23);
            this.brPath.TabIndex = 11;
            this.brPath._ButtonClick += new System.EventHandler(this.brPath__ButtonClick);
            this.brPath._DoubleClicked += new System.EventHandler(this.brPath__DoubleClicked);
            // 
            // btnSavePath
            // 
            this.btnSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePath.Image = global::SN_Net.Properties.Resources.save_16;
            this.btnSavePath.Location = new System.Drawing.Point(665, 267);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(35, 25);
            this.btnSavePath.TabIndex = 13;
            this.toolTip1.SetToolTip(this.btnSavePath, "Save Change");
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // btnStopPath
            // 
            this.btnStopPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopPath.Image = global::SN_Net.Properties.Resources.stop_16;
            this.btnStopPath.Location = new System.Drawing.Point(628, 267);
            this.btnStopPath.Name = "btnStopPath";
            this.btnStopPath.Size = new System.Drawing.Size(35, 25);
            this.btnStopPath.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnStopPath, "Discard Change");
            this.btnStopPath.UseVisualStyleBackColor = true;
            this.btnStopPath.Click += new System.EventHandler(this.btnStopPath_Click);
            // 
            // btnEditPath
            // 
            this.btnEditPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditPath.Image = global::SN_Net.Properties.Resources.edit_16;
            this.btnEditPath.Location = new System.Drawing.Point(592, 267);
            this.btnEditPath.Name = "btnEditPath";
            this.btnEditPath.Size = new System.Drawing.Size(35, 25);
            this.btnEditPath.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnEditPath, "Edit");
            this.btnEditPath.UseVisualStyleBackColor = true;
            this.btnEditPath.Click += new System.EventHandler(this.btnEditPath_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Image = global::SN_Net.Properties.Resources.play_media;
            this.btnPlay.Location = new System.Drawing.Point(556, 267);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(35, 25);
            this.btnPlay.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnPlay, "Play Audio File");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // col_complain_note_comment
            // 
            this.col_complain_note_comment.DataPropertyName = "note_comment";
            this.col_complain_note_comment.HeaderText = "Note Comment";
            this.col_complain_note_comment.Name = "col_complain_note_comment";
            this.col_complain_note_comment.ReadOnly = true;
            this.col_complain_note_comment.Visible = false;
            // 
            // col_complain_id
            // 
            this.col_complain_id.DataPropertyName = "id";
            this.col_complain_id.HeaderText = "ID";
            this.col_complain_id.Name = "col_complain_id";
            this.col_complain_id.ReadOnly = true;
            this.col_complain_id.Visible = false;
            // 
            // col_complain_recby
            // 
            this.col_complain_recby.DataPropertyName = "recby";
            this.col_complain_recby.HeaderText = "Rec. By";
            this.col_complain_recby.MinimumWidth = 80;
            this.col_complain_recby.Name = "col_complain_recby";
            this.col_complain_recby.ReadOnly = true;
            this.col_complain_recby.Width = 80;
            // 
            // col_complain
            // 
            this.col_complain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_complain.DataPropertyName = "description";
            this.col_complain.HeaderText = "Complain";
            this.col_complain.Name = "col_complain";
            this.col_complain.ReadOnly = true;
            // 
            // col_comment_note_comment
            // 
            this.col_comment_note_comment.DataPropertyName = "note_comment";
            this.col_comment_note_comment.HeaderText = "Note Comment";
            this.col_comment_note_comment.Name = "col_comment_note_comment";
            this.col_comment_note_comment.ReadOnly = true;
            this.col_comment_note_comment.Visible = false;
            // 
            // col_comment_id
            // 
            this.col_comment_id.DataPropertyName = "id";
            this.col_comment_id.HeaderText = "ID";
            this.col_comment_id.Name = "col_comment_id";
            this.col_comment_id.ReadOnly = true;
            this.col_comment_id.Visible = false;
            // 
            // col_comment_recby
            // 
            this.col_comment_recby.DataPropertyName = "recby";
            this.col_comment_recby.HeaderText = "Rec. By";
            this.col_comment_recby.MinimumWidth = 80;
            this.col_comment_recby.Name = "col_comment_recby";
            this.col_comment_recby.ReadOnly = true;
            this.col_comment_recby.Width = 80;
            // 
            // col_comment
            // 
            this.col_comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_comment.DataPropertyName = "description";
            this.col_comment.HeaderText = "Comment";
            this.col_comment.Name = "col_comment";
            this.col_comment.ReadOnly = true;
            // 
            // DialogNoteComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 354);
            this.ControlBox = false;
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.btnStopPath);
            this.Controls.Add(this.brPath);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnEditPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNoteComment";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Comment / Complain";
            this.Load += new System.EventHandler(this.DialogNoteComment_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComplain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CC.XDatagrid dgvComment;
        private System.Windows.Forms.Button btnCommentSave;
        private System.Windows.Forms.Button btnCommentStop;
        private System.Windows.Forms.Button btnCommentDelete;
        private System.Windows.Forms.Button btnCommentEdit;
        private System.Windows.Forms.Button btnCommentAdd;
        private CC.XDatagrid dgvComplain;
        private System.Windows.Forms.Button btnComplainSave;
        private System.Windows.Forms.Button btnComplainStop;
        private System.Windows.Forms.Button btnComplainDelete;
        private System.Windows.Forms.Button btnComplainEdit;
        private System.Windows.Forms.Button btnComplainAdd;
        private CC.XTextEdit inlineComment;
        private CC.XTextEdit inlineComplain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditPath;
        private CC.XBrowseBox brPath;
        private System.Windows.Forms.Button btnStopPath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_comment_note_comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_comment_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_comment_recby;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_complain_note_comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_complain_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_complain_recby;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_complain;
    }
}