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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgv = new CC.XDatagrid();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dealer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_dealercod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_compnam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_addr03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_telnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_zipcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_faxnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_busides = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(101, 278);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(10, 278);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 35);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.GreenYellow;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_id,
            this.col_dealer,
            this.col_dealercod,
            this.col_compnam,
            this.col_area,
            this.col_addr01,
            this.col_addr02,
            this.col_addr03,
            this.col_telnum,
            this.col_contact,
            this.col_zipcod,
            this.col_faxnum,
            this.col_busides,
            this.col_position});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
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
            this.dgv.Size = new System.Drawing.Size(1217, 267);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 2;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            this.dgv.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_DataBindingComplete);
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_dealer
            // 
            this.col_dealer.DataPropertyName = "dealer";
            this.col_dealer.HeaderText = "Dealer";
            this.col_dealer.Name = "col_dealer";
            this.col_dealer.ReadOnly = true;
            this.col_dealer.Visible = false;
            // 
            // col_dealercod
            // 
            this.col_dealercod.DataPropertyName = "dealercod";
            this.col_dealercod.HeaderText = "DEALER";
            this.col_dealercod.MinimumWidth = 120;
            this.col_dealercod.Name = "col_dealercod";
            this.col_dealercod.ReadOnly = true;
            this.col_dealercod.Width = 120;
            // 
            // col_compnam
            // 
            this.col_compnam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_compnam.DataPropertyName = "compnam";
            this.col_compnam.HeaderText = "CompNam";
            this.col_compnam.MinimumWidth = 400;
            this.col_compnam.Name = "col_compnam";
            this.col_compnam.ReadOnly = true;
            // 
            // col_area
            // 
            this.col_area.DataPropertyName = "area";
            this.col_area.HeaderText = "AREA";
            this.col_area.MinimumWidth = 100;
            this.col_area.Name = "col_area";
            this.col_area.ReadOnly = true;
            // 
            // col_addr01
            // 
            this.col_addr01.DataPropertyName = "addr01";
            this.col_addr01.HeaderText = "ADDR01";
            this.col_addr01.MinimumWidth = 400;
            this.col_addr01.Name = "col_addr01";
            this.col_addr01.ReadOnly = true;
            this.col_addr01.Width = 400;
            // 
            // col_addr02
            // 
            this.col_addr02.DataPropertyName = "addr02";
            this.col_addr02.HeaderText = "ADDR02";
            this.col_addr02.MinimumWidth = 400;
            this.col_addr02.Name = "col_addr02";
            this.col_addr02.ReadOnly = true;
            this.col_addr02.Width = 400;
            // 
            // col_addr03
            // 
            this.col_addr03.DataPropertyName = "addr03";
            this.col_addr03.HeaderText = "ADDR03";
            this.col_addr03.MinimumWidth = 300;
            this.col_addr03.Name = "col_addr03";
            this.col_addr03.ReadOnly = true;
            this.col_addr03.Width = 300;
            // 
            // col_telnum
            // 
            this.col_telnum.DataPropertyName = "telnum";
            this.col_telnum.HeaderText = "TELNUM";
            this.col_telnum.MinimumWidth = 320;
            this.col_telnum.Name = "col_telnum";
            this.col_telnum.ReadOnly = true;
            this.col_telnum.Width = 320;
            // 
            // col_contact
            // 
            this.col_contact.DataPropertyName = "contact";
            this.col_contact.HeaderText = "CONTACT";
            this.col_contact.MinimumWidth = 320;
            this.col_contact.Name = "col_contact";
            this.col_contact.ReadOnly = true;
            this.col_contact.Width = 320;
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
            // col_busides
            // 
            this.col_busides.DataPropertyName = "busides";
            this.col_busides.HeaderText = "Busides";
            this.col_busides.Name = "col_busides";
            this.col_busides.ReadOnly = true;
            this.col_busides.Visible = false;
            // 
            // col_position
            // 
            this.col_position.DataPropertyName = "position";
            this.col_position.HeaderText = "Position";
            this.col_position.Name = "col_position";
            this.col_position.ReadOnly = true;
            this.col_position.Visible = false;
            // 
            // DialogInquiryDealer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 325);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogInquiryDealer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inquiry";
            this.Load += new System.EventHandler(this.DialogInquiryDealer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private CC.XDatagrid dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dealer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_dealercod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_compnam;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_area;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr01;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr02;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_addr03;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_telnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_zipcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_faxnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_busides;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_position;
    }
}