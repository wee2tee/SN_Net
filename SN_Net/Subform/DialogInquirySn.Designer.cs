namespace SN_Net.Subform
{
    partial class DialogInquirySn
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgv = new CC.XDatagrid();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sernum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_oldcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_compnam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dealer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_telnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_busityp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_busides = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_prenam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_zipcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_faxnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_purdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_expdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_manual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_upfree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_refnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_verextdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_howknown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_verext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_creby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_credat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_chgby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_chgdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 307);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 35);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(103, 307);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
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
            this.col_id,
            this.col_sernum,
            this.col_oldcod,
            this.col_version,
            this.col_compnam,
            this.col_contact,
            this.col_area,
            this.col_dealer,
            this.col_telnum,
            this.col_busityp,
            this.col_busides,
            this.col_position,
            this.col_prenam,
            this.col_addr01,
            this.col_addr02,
            this.col_addr03,
            this.col_zipcod,
            this.col_faxnum,
            this.col_purdat,
            this.col_expdat,
            this.col_branch,
            this.col_manual,
            this.col_upfree,
            this.col_refnum,
            this.col_remark,
            this.col_verextdat,
            this.col_howknown,
            this.col_verext,
            this.col_creby,
            this.col_credat,
            this.col_chgby,
            this.col_chgdat,
            this.col_serial});
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
            this.dgv.Location = new System.Drawing.Point(2, 2);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1232, 293);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            this.dgv.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgv_Scroll);
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_sernum
            // 
            this.col_sernum.DataPropertyName = "sernum";
            this.col_sernum.HeaderText = "SERNUM";
            this.col_sernum.MinimumWidth = 120;
            this.col_sernum.Name = "col_sernum";
            this.col_sernum.ReadOnly = true;
            this.col_sernum.Width = 120;
            // 
            // col_oldcod
            // 
            this.col_oldcod.DataPropertyName = "oldnum";
            this.col_oldcod.HeaderText = "OLDCOD";
            this.col_oldcod.MinimumWidth = 120;
            this.col_oldcod.Name = "col_oldcod";
            this.col_oldcod.ReadOnly = true;
            this.col_oldcod.Width = 120;
            // 
            // col_version
            // 
            this.col_version.DataPropertyName = "version";
            this.col_version.HeaderText = "VERSION";
            this.col_version.MinimumWidth = 70;
            this.col_version.Name = "col_version";
            this.col_version.ReadOnly = true;
            this.col_version.Width = 70;
            // 
            // col_compnam
            // 
            this.col_compnam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_compnam.DataPropertyName = "compnam";
            this.col_compnam.HeaderText = "COMPNAM";
            this.col_compnam.MinimumWidth = 200;
            this.col_compnam.Name = "col_compnam";
            this.col_compnam.ReadOnly = true;
            // 
            // col_contact
            // 
            this.col_contact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_contact.DataPropertyName = "contact";
            this.col_contact.FillWeight = 80F;
            this.col_contact.HeaderText = "CONTACT";
            this.col_contact.MinimumWidth = 140;
            this.col_contact.Name = "col_contact";
            this.col_contact.ReadOnly = true;
            // 
            // col_area
            // 
            this.col_area.DataPropertyName = "area";
            this.col_area.HeaderText = "AREA";
            this.col_area.MinimumWidth = 90;
            this.col_area.Name = "col_area";
            this.col_area.ReadOnly = true;
            this.col_area.Width = 90;
            // 
            // col_dealer
            // 
            this.col_dealer.DataPropertyName = "dealer";
            this.col_dealer.HeaderText = "DEALER";
            this.col_dealer.MinimumWidth = 90;
            this.col_dealer.Name = "col_dealer";
            this.col_dealer.ReadOnly = true;
            this.col_dealer.Width = 90;
            // 
            // col_telnum
            // 
            this.col_telnum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_telnum.DataPropertyName = "telnum";
            this.col_telnum.FillWeight = 80F;
            this.col_telnum.HeaderText = "TELNUM";
            this.col_telnum.Name = "col_telnum";
            this.col_telnum.ReadOnly = true;
            // 
            // col_busityp
            // 
            this.col_busityp.DataPropertyName = "busityp";
            this.col_busityp.HeaderText = "BUSITYP";
            this.col_busityp.MinimumWidth = 70;
            this.col_busityp.Name = "col_busityp";
            this.col_busityp.ReadOnly = true;
            this.col_busityp.Width = 70;
            // 
            // col_busides
            // 
            this.col_busides.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_busides.DataPropertyName = "busides";
            this.col_busides.FillWeight = 80F;
            this.col_busides.HeaderText = "BUSIDES";
            this.col_busides.Name = "col_busides";
            this.col_busides.ReadOnly = true;
            // 
            // col_position
            // 
            this.col_position.DataPropertyName = "position";
            this.col_position.HeaderText = "Position";
            this.col_position.Name = "col_position";
            this.col_position.ReadOnly = true;
            this.col_position.Visible = false;
            // 
            // col_prenam
            // 
            this.col_prenam.DataPropertyName = "prenam";
            this.col_prenam.HeaderText = "Prename";
            this.col_prenam.Name = "col_prenam";
            this.col_prenam.ReadOnly = true;
            this.col_prenam.Visible = false;
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
            // col_faxnum
            // 
            this.col_faxnum.DataPropertyName = "faxnum";
            this.col_faxnum.HeaderText = "Faxnum";
            this.col_faxnum.Name = "col_faxnum";
            this.col_faxnum.ReadOnly = true;
            this.col_faxnum.Visible = false;
            // 
            // col_purdat
            // 
            this.col_purdat.DataPropertyName = "purdat";
            this.col_purdat.HeaderText = "Purdat";
            this.col_purdat.Name = "col_purdat";
            this.col_purdat.ReadOnly = true;
            this.col_purdat.Visible = false;
            // 
            // col_expdat
            // 
            this.col_expdat.DataPropertyName = "expdat";
            this.col_expdat.HeaderText = "Expdat";
            this.col_expdat.Name = "col_expdat";
            this.col_expdat.ReadOnly = true;
            this.col_expdat.Visible = false;
            // 
            // col_branch
            // 
            this.col_branch.DataPropertyName = "branch";
            this.col_branch.HeaderText = "Branch";
            this.col_branch.Name = "col_branch";
            this.col_branch.ReadOnly = true;
            this.col_branch.Visible = false;
            // 
            // col_manual
            // 
            this.col_manual.DataPropertyName = "manual";
            this.col_manual.HeaderText = "Manual";
            this.col_manual.Name = "col_manual";
            this.col_manual.ReadOnly = true;
            this.col_manual.Visible = false;
            // 
            // col_upfree
            // 
            this.col_upfree.DataPropertyName = "upfree";
            this.col_upfree.HeaderText = "Up Free";
            this.col_upfree.Name = "col_upfree";
            this.col_upfree.ReadOnly = true;
            this.col_upfree.Visible = false;
            // 
            // col_refnum
            // 
            this.col_refnum.DataPropertyName = "refnum";
            this.col_refnum.HeaderText = "Refnum";
            this.col_refnum.Name = "col_refnum";
            this.col_refnum.ReadOnly = true;
            this.col_refnum.Visible = false;
            // 
            // col_remark
            // 
            this.col_remark.DataPropertyName = "remark";
            this.col_remark.HeaderText = "Remark";
            this.col_remark.Name = "col_remark";
            this.col_remark.ReadOnly = true;
            this.col_remark.Visible = false;
            // 
            // col_verextdat
            // 
            this.col_verextdat.DataPropertyName = "verextdat";
            this.col_verextdat.HeaderText = "Verextdat";
            this.col_verextdat.Name = "col_verextdat";
            this.col_verextdat.ReadOnly = true;
            this.col_verextdat.Visible = false;
            // 
            // col_howknown
            // 
            this.col_howknown.DataPropertyName = "howknown";
            this.col_howknown.HeaderText = "Howknown";
            this.col_howknown.Name = "col_howknown";
            this.col_howknown.ReadOnly = true;
            this.col_howknown.Visible = false;
            // 
            // col_verext
            // 
            this.col_verext.DataPropertyName = "verext";
            this.col_verext.HeaderText = "Verext";
            this.col_verext.Name = "col_verext";
            this.col_verext.ReadOnly = true;
            this.col_verext.Visible = false;
            // 
            // col_creby
            // 
            this.col_creby.DataPropertyName = "creby";
            this.col_creby.HeaderText = "Creby";
            this.col_creby.Name = "col_creby";
            this.col_creby.ReadOnly = true;
            this.col_creby.Visible = false;
            // 
            // col_credat
            // 
            this.col_credat.DataPropertyName = "credat";
            this.col_credat.HeaderText = "Credat";
            this.col_credat.Name = "col_credat";
            this.col_credat.ReadOnly = true;
            this.col_credat.Visible = false;
            // 
            // col_chgby
            // 
            this.col_chgby.DataPropertyName = "chgby";
            this.col_chgby.HeaderText = "Chgby";
            this.col_chgby.Name = "col_chgby";
            this.col_chgby.ReadOnly = true;
            this.col_chgby.Visible = false;
            // 
            // col_chgdat
            // 
            this.col_chgdat.DataPropertyName = "chgdat";
            this.col_chgdat.HeaderText = "Chgdat";
            this.col_chgdat.Name = "col_chgdat";
            this.col_chgdat.ReadOnly = true;
            this.col_chgdat.Visible = false;
            // 
            // col_serial
            // 
            this.col_serial.DataPropertyName = "serial";
            this.col_serial.HeaderText = "Serial";
            this.col_serial.Name = "col_serial";
            this.col_serial.ReadOnly = true;
            this.col_serial.Visible = false;
            // 
            // DialogInquirySn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 354);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogInquirySn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inquiry";
            this.Load += new System.EventHandler(this.DialogInquirySn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CC.XDatagrid dgv;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sernum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_oldcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_version;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_compnam;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_area;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dealer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_telnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_busityp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_busides;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_position;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_prenam;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr01;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr02;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr03;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_zipcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_faxnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_purdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_expdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_manual;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_upfree;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_refnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_verextdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_howknown;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_verext;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_creby;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_credat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_chgby;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_chgdat;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_serial;
    }
}