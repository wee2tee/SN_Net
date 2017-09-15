namespace SN_Net.Subform
{
    partial class DialogInquiryIstab
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgv = new CC.XDatagrid();
            this.col_istab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tabtyp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_abbreviate_th = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_abbreviate_en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typdes_th = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_typdes_en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_use_pattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(92, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(9, 158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 29);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.AllowSortByColumnHeaderClicked = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.col_abbreviate_th,
            this.col_abbreviate_en,
            this.col_typdes_th,
            this.col_typdes_en,
            this.col_use_pattern});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.FillEmptyRow = false;
            this.dgv.FocusedRowBorderRedLine = true;
            this.dgv.Location = new System.Drawing.Point(3, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(453, 153);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 4;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            // 
            // col_istab
            // 
            this.col_istab.DataPropertyName = "istab";
            this.col_istab.HeaderText = "ISTAB";
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
            this.col_typcod.MinimumWidth = 80;
            this.col_typcod.Name = "col_typcod";
            this.col_typcod.ReadOnly = true;
            this.col_typcod.Width = 80;
            // 
            // col_abbreviate_th
            // 
            this.col_abbreviate_th.DataPropertyName = "abbr_th";
            this.col_abbreviate_th.HeaderText = "ชื่อย่อ";
            this.col_abbreviate_th.MinimumWidth = 120;
            this.col_abbreviate_th.Name = "col_abbreviate_th";
            this.col_abbreviate_th.ReadOnly = true;
            this.col_abbreviate_th.Width = 120;
            // 
            // col_abbreviate_en
            // 
            this.col_abbreviate_en.DataPropertyName = "abbr_en";
            this.col_abbreviate_en.HeaderText = "ชื่อย่อ (En)";
            this.col_abbreviate_en.MinimumWidth = 120;
            this.col_abbreviate_en.Name = "col_abbreviate_en";
            this.col_abbreviate_en.ReadOnly = true;
            this.col_abbreviate_en.Visible = false;
            this.col_abbreviate_en.Width = 120;
            // 
            // col_typdes_th
            // 
            this.col_typdes_th.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_typdes_th.DataPropertyName = "typdes_th";
            this.col_typdes_th.HeaderText = "รายละเอียด";
            this.col_typdes_th.MinimumWidth = 140;
            this.col_typdes_th.Name = "col_typdes_th";
            this.col_typdes_th.ReadOnly = true;
            // 
            // col_typdes_en
            // 
            this.col_typdes_en.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_typdes_en.DataPropertyName = "typdes_en";
            this.col_typdes_en.HeaderText = "รายละเอียด (En)";
            this.col_typdes_en.MinimumWidth = 140;
            this.col_typdes_en.Name = "col_typdes_en";
            this.col_typdes_en.ReadOnly = true;
            this.col_typdes_en.Visible = false;
            // 
            // col_use_pattern
            // 
            this.col_use_pattern.DataPropertyName = "use_pattern";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_use_pattern.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_use_pattern.HeaderText = "Pattern";
            this.col_use_pattern.MinimumWidth = 60;
            this.col_use_pattern.Name = "col_use_pattern";
            this.col_use_pattern.ReadOnly = true;
            this.col_use_pattern.Width = 60;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(211, 158);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(77, 29);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(294, 158);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 29);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(377, 158);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(77, 29);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // DialogInquiryIstab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 194);
            this.ControlBox = false;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 150);
            this.Name = "DialogInquiryIstab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private CC.XDatagrid dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_istab;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tabtyp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_abbreviate_th;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_abbreviate_en;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typdes_th;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_typdes_en;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_use_pattern;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
    }
}