namespace SN_Net.Subform
{
    partial class DialogInquiryDealer
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
            this.dgv = new CC.XDatagrid();
            this.col_dealer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dealercod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_compnam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_zipcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_telnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_faxnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_busides = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
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
            this.col_dealer,
            this.col_id,
            this.col_dealercod,
            this.col_compnam,
            this.col_addr01,
            this.col_addr02,
            this.col_addr03,
            this.col_zipcod,
            this.col_telnum,
            this.col_faxnum,
            this.col_contact,
            this.col_position,
            this.col_busides,
            this.col_area});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.dgv.Size = new System.Drawing.Size(453, 150);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 10;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            // 
            // col_dealer
            // 
            this.col_dealer.DataPropertyName = "dealer";
            this.col_dealer.HeaderText = "DEALER";
            this.col_dealer.Name = "col_dealer";
            this.col_dealer.ReadOnly = true;
            this.col_dealer.Visible = false;
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_dealercod
            // 
            this.col_dealercod.DataPropertyName = "dealercod";
            this.col_dealercod.HeaderText = "รหัส";
            this.col_dealercod.MinimumWidth = 120;
            this.col_dealercod.Name = "col_dealercod";
            this.col_dealercod.ReadOnly = true;
            this.col_dealercod.Width = 120;
            // 
            // col_compnam
            // 
            this.col_compnam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_compnam.DataPropertyName = "compnam";
            this.col_compnam.HeaderText = "ชื่อ";
            this.col_compnam.MinimumWidth = 120;
            this.col_compnam.Name = "col_compnam";
            this.col_compnam.ReadOnly = true;
            // 
            // col_addr01
            // 
            this.col_addr01.DataPropertyName = "addr01";
            this.col_addr01.HeaderText = "Addr01";
            this.col_addr01.Name = "col_addr01";
            this.col_addr01.ReadOnly = true;
            this.col_addr01.Visible = false;
            // 
            // col_addr02
            // 
            this.col_addr02.DataPropertyName = "addr02";
            this.col_addr02.HeaderText = "Addr02";
            this.col_addr02.Name = "col_addr02";
            this.col_addr02.ReadOnly = true;
            this.col_addr02.Visible = false;
            // 
            // col_addr03
            // 
            this.col_addr03.DataPropertyName = "addr03";
            this.col_addr03.HeaderText = "Addr03";
            this.col_addr03.Name = "col_addr03";
            this.col_addr03.ReadOnly = true;
            this.col_addr03.Visible = false;
            // 
            // col_zipcod
            // 
            this.col_zipcod.DataPropertyName = "zipcod";
            this.col_zipcod.HeaderText = "Zipcod";
            this.col_zipcod.Name = "col_zipcod";
            this.col_zipcod.ReadOnly = true;
            this.col_zipcod.Visible = false;
            // 
            // col_telnum
            // 
            this.col_telnum.DataPropertyName = "telnum";
            this.col_telnum.HeaderText = "Telnum";
            this.col_telnum.Name = "col_telnum";
            this.col_telnum.ReadOnly = true;
            this.col_telnum.Visible = false;
            // 
            // col_faxnum
            // 
            this.col_faxnum.DataPropertyName = "faxnum";
            this.col_faxnum.HeaderText = "Faxnum";
            this.col_faxnum.Name = "col_faxnum";
            this.col_faxnum.ReadOnly = true;
            this.col_faxnum.Visible = false;
            // 
            // col_contact
            // 
            this.col_contact.DataPropertyName = "contact";
            this.col_contact.HeaderText = "Contact";
            this.col_contact.Name = "col_contact";
            this.col_contact.ReadOnly = true;
            this.col_contact.Visible = false;
            // 
            // col_position
            // 
            this.col_position.DataPropertyName = "position";
            this.col_position.HeaderText = "Position";
            this.col_position.Name = "col_position";
            this.col_position.ReadOnly = true;
            this.col_position.Visible = false;
            // 
            // col_busides
            // 
            this.col_busides.DataPropertyName = "busides";
            this.col_busides.HeaderText = "Busides";
            this.col_busides.Name = "col_busides";
            this.col_busides.ReadOnly = true;
            this.col_busides.Visible = false;
            // 
            // col_area
            // 
            this.col_area.DataPropertyName = "area";
            this.col_area.HeaderText = "Area";
            this.col_area.Name = "col_area";
            this.col_area.ReadOnly = true;
            this.col_area.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(375, 157);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(77, 29);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(292, 157);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 29);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(209, 157);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(77, 29);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(91, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(8, 157);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 29);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // DialogInquiryDealer
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
            this.Name = "DialogInquiryDealer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CC.XDatagrid dgv;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dealer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dealercod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_compnam;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr01;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr02;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr03;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_zipcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_telnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_faxnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_position;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_busides;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_area;
    }
}