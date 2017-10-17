namespace SN_Net.Subform
{
    partial class FormIstab
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.inlinePattern = new CC.XDropdownList();
            this.inlineTypdesEn = new CC.XTextEdit();
            this.inlineTypdesTh = new CC.XTextEdit();
            this.inlineAbbrEn = new CC.XTextEdit();
            this.inlineAbbrTh = new CC.XTextEdit();
            this.inlineTypcod = new CC.XTextEdit();
            this.dgv = new CC.XDatagrid();
            this.col_istab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tabtyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_abbr_th = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_abbr_en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typdes_th = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typdes_en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_use_pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
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
            this.btnReload});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(945, 43);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SN_Net.Properties.Resources.add;
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
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
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
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
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
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
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = global::SN_Net.Properties.Resources.refresh;
            this.btnReload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(36, 40);
            this.btnReload.Text = "Reload data <F5>";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.inlinePattern);
            this.panel1.Controls.Add(this.inlineTypdesEn);
            this.panel1.Controls.Add(this.inlineTypdesTh);
            this.panel1.Controls.Add(this.inlineAbbrEn);
            this.panel1.Controls.Add(this.inlineAbbrTh);
            this.panel1.Controls.Add(this.inlineTypcod);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 527);
            this.panel1.TabIndex = 8;
            // 
            // inlinePattern
            // 
            this.inlinePattern._ReadOnly = false;
            this.inlinePattern._SelectedItem = null;
            this.inlinePattern._Text = "";
            this.inlinePattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlinePattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlinePattern.Location = new System.Drawing.Point(888, 52);
            this.inlinePattern.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlinePattern.Name = "inlinePattern";
            this.inlinePattern.Size = new System.Drawing.Size(54, 23);
            this.inlinePattern.TabIndex = 6;
            this.inlinePattern._SelectedItemChanged += new System.EventHandler(this.inlinePattern__SelectedItemChanged);
            // 
            // inlineTypdesEn
            // 
            this.inlineTypdesEn._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypdesEn._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineTypdesEn._MaxLength = 50;
            this.inlineTypdesEn._ReadOnly = false;
            this.inlineTypdesEn._SelectionLength = 0;
            this.inlineTypdesEn._SelectionStart = 0;
            this.inlineTypdesEn._Text = "";
            this.inlineTypdesEn._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineTypdesEn.BackColor = System.Drawing.Color.White;
            this.inlineTypdesEn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypdesEn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTypdesEn.Location = new System.Drawing.Point(651, 52);
            this.inlineTypdesEn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTypdesEn.Name = "inlineTypdesEn";
            this.inlineTypdesEn.Size = new System.Drawing.Size(235, 23);
            this.inlineTypdesEn.TabIndex = 5;
            this.inlineTypdesEn._TextChanged += new System.EventHandler(this.inlineTypdesEn__TextChanged);
            // 
            // inlineTypdesTh
            // 
            this.inlineTypdesTh._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypdesTh._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineTypdesTh._MaxLength = 50;
            this.inlineTypdesTh._ReadOnly = false;
            this.inlineTypdesTh._SelectionLength = 0;
            this.inlineTypdesTh._SelectionStart = 0;
            this.inlineTypdesTh._Text = "";
            this.inlineTypdesTh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineTypdesTh.BackColor = System.Drawing.Color.White;
            this.inlineTypdesTh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypdesTh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTypdesTh.Location = new System.Drawing.Point(413, 52);
            this.inlineTypdesTh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTypdesTh.Name = "inlineTypdesTh";
            this.inlineTypdesTh.Size = new System.Drawing.Size(235, 23);
            this.inlineTypdesTh.TabIndex = 4;
            this.inlineTypdesTh._TextChanged += new System.EventHandler(this.inlineTypdesTh__TextChanged);
            // 
            // inlineAbbrEn
            // 
            this.inlineAbbrEn._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineAbbrEn._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineAbbrEn._MaxLength = 20;
            this.inlineAbbrEn._ReadOnly = false;
            this.inlineAbbrEn._SelectionLength = 0;
            this.inlineAbbrEn._SelectionStart = 0;
            this.inlineAbbrEn._Text = "";
            this.inlineAbbrEn._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineAbbrEn.BackColor = System.Drawing.Color.White;
            this.inlineAbbrEn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineAbbrEn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineAbbrEn.Location = new System.Drawing.Point(253, 52);
            this.inlineAbbrEn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineAbbrEn.Name = "inlineAbbrEn";
            this.inlineAbbrEn.Size = new System.Drawing.Size(157, 23);
            this.inlineAbbrEn.TabIndex = 3;
            this.inlineAbbrEn._TextChanged += new System.EventHandler(this.inlineAbbrEn__TextChanged);
            // 
            // inlineAbbrTh
            // 
            this.inlineAbbrTh._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineAbbrTh._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineAbbrTh._MaxLength = 20;
            this.inlineAbbrTh._ReadOnly = false;
            this.inlineAbbrTh._SelectionLength = 0;
            this.inlineAbbrTh._SelectionStart = 0;
            this.inlineAbbrTh._Text = "";
            this.inlineAbbrTh._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineAbbrTh.BackColor = System.Drawing.Color.White;
            this.inlineAbbrTh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineAbbrTh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineAbbrTh.Location = new System.Drawing.Point(92, 52);
            this.inlineAbbrTh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineAbbrTh.Name = "inlineAbbrTh";
            this.inlineAbbrTh.Size = new System.Drawing.Size(157, 23);
            this.inlineAbbrTh.TabIndex = 2;
            this.inlineAbbrTh._TextChanged += new System.EventHandler(this.inlineAbbrTh__TextChanged);
            // 
            // inlineTypcod
            // 
            this.inlineTypcod._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypcod._CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.inlineTypcod._MaxLength = 10;
            this.inlineTypcod._ReadOnly = false;
            this.inlineTypcod._SelectionLength = 0;
            this.inlineTypcod._SelectionStart = 0;
            this.inlineTypcod._Text = "";
            this.inlineTypcod._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineTypcod.BackColor = System.Drawing.Color.White;
            this.inlineTypcod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTypcod.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTypcod.Location = new System.Drawing.Point(3, 52);
            this.inlineTypcod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTypcod.Name = "inlineTypcod";
            this.inlineTypcod.Size = new System.Drawing.Size(83, 23);
            this.inlineTypcod.TabIndex = 1;
            this.inlineTypcod._TextChanged += new System.EventHandler(this.inlineTypcod__TextChanged);
            this.inlineTypcod._Leave += new System.EventHandler(this.inlineTypcod__Leave);
            // 
            // dgv
            // 
            this.dgv.AllowSortByColumnHeaderClicked = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_istab,
            this.col_id,
            this.col_tabtyp,
            this.col_typcod,
            this.col_abbr_th,
            this.col_abbr_en,
            this.col_typdes_th,
            this.col_typdes_en,
            this.col_use_pattern});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.FillEmptyRow = false;
            this.dgv.FocusedRowBorderRedLine = true;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(945, 527);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DataBindingComplete);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            this.dgv.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // col_istab
            // 
            this.col_istab.DataPropertyName = "istab";
            this.col_istab.HeaderText = "Istab";
            this.col_istab.Name = "col_istab";
            this.col_istab.ReadOnly = true;
            this.col_istab.Visible = false;
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_tabtyp
            // 
            this.col_tabtyp.DataPropertyName = "tabtyp";
            this.col_tabtyp.HeaderText = "Tabtyp";
            this.col_tabtyp.Name = "col_tabtyp";
            this.col_tabtyp.ReadOnly = true;
            this.col_tabtyp.Visible = false;
            // 
            // col_typcod
            // 
            this.col_typcod.DataPropertyName = "typcod";
            this.col_typcod.HeaderText = "รหัส";
            this.col_typcod.MinimumWidth = 90;
            this.col_typcod.Name = "col_typcod";
            this.col_typcod.ReadOnly = true;
            this.col_typcod.Width = 90;
            // 
            // col_abbr_th
            // 
            this.col_abbr_th.DataPropertyName = "abbr_th";
            this.col_abbr_th.HeaderText = "ชื่อย่อ (ไทย)";
            this.col_abbr_th.MinimumWidth = 160;
            this.col_abbr_th.Name = "col_abbr_th";
            this.col_abbr_th.ReadOnly = true;
            this.col_abbr_th.Width = 160;
            // 
            // col_abbr_en
            // 
            this.col_abbr_en.DataPropertyName = "abbr_en";
            this.col_abbr_en.HeaderText = "ชื่อย่อ (Eng.)";
            this.col_abbr_en.MinimumWidth = 160;
            this.col_abbr_en.Name = "col_abbr_en";
            this.col_abbr_en.ReadOnly = true;
            this.col_abbr_en.Width = 160;
            // 
            // col_typdes_th
            // 
            this.col_typdes_th.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_typdes_th.DataPropertyName = "typdes_th";
            this.col_typdes_th.HeaderText = "ชื่อเต็ม (ไทย)";
            this.col_typdes_th.Name = "col_typdes_th";
            this.col_typdes_th.ReadOnly = true;
            // 
            // col_typdes_en
            // 
            this.col_typdes_en.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_typdes_en.DataPropertyName = "typdes_en";
            this.col_typdes_en.HeaderText = "ชื่อเต็ม (Eng.)";
            this.col_typdes_en.Name = "col_typdes_en";
            this.col_typdes_en.ReadOnly = true;
            // 
            // col_use_pattern
            // 
            this.col_use_pattern.DataPropertyName = "use_pattern";
            this.col_use_pattern.HeaderText = "pattern";
            this.col_use_pattern.MinimumWidth = 60;
            this.col_use_pattern.Name = "col_use_pattern";
            this.col_use_pattern.ReadOnly = true;
            this.col_use_pattern.Width = 60;
            // 
            // FormIstab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 570);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormIstab";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Istab";
            this.Load += new System.EventHandler(this.FormIstab_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.Panel panel1;
        private CC.XDatagrid dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_istab;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tabtyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_abbr_th;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_abbr_en;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typdes_th;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typdes_en;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_use_pattern;
        private CC.XDropdownList inlinePattern;
        private CC.XTextEdit inlineTypdesEn;
        private CC.XTextEdit inlineTypdesTh;
        private CC.XTextEdit inlineAbbrEn;
        private CC.XTextEdit inlineAbbrTh;
        private CC.XTextEdit inlineTypcod;
    }
}