namespace SN_Net.Subform
{
    partial class DialogImportRegisterData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new CC.XDatagrid();
            this.col_recorded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_importSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_compname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtZipcod = new CC.XNumTextEdit();
            this.txtAddr03 = new CC.XTextEdit();
            this.txtPosition = new CC.XTextEdit();
            this.txtContact = new CC.XTextEdit();
            this.txtFaxnum = new CC.XTextEdit();
            this.txtTelnum = new CC.XTextEdit();
            this.txtAddr02 = new CC.XTextEdit();
            this.txtAddr01 = new CC.XTextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.mskSernum = new CC.XTextEditMasked();
            this.txtCompnam = new CC.XTextEdit();
            this.txtPrenam = new CC.XTextEdit();
            this.txtVersion = new CC.XTextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDealer = new System.Windows.Forms.Label();
            this.lblBusityp = new System.Windows.Forms.Label();
            this.brDealer = new CC.XBrowseBox();
            this.brBusityp = new CC.XBrowseBox();
            this.txtBusides = new CC.XTextEdit();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBusityp = new CC.XTextEdit();
            this.txtDealer = new CC.XTextEdit();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new CC.XTextEdit();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGoSn = new System.Windows.Forms.Button();
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
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(207)))), ((int)(((byte)(179)))));
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_recorded,
            this.col_id,
            this.col_importSerial,
            this.col_sn,
            this.col_compname});
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle36;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.FillEmptyRow = false;
            this.dgv.FocusedRowBorderRedLine = true;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 26;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(324, 483);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            // 
            // col_recorded
            // 
            this.col_recorded.DataPropertyName = "recorded";
            this.col_recorded.Frozen = true;
            this.col_recorded.HeaderText = "";
            this.col_recorded.MinimumWidth = 25;
            this.col_recorded.Name = "col_recorded";
            this.col_recorded.ReadOnly = true;
            this.col_recorded.Width = 25;
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_importSerial
            // 
            this.col_importSerial.DataPropertyName = "importSerial";
            this.col_importSerial.HeaderText = "importSerial";
            this.col_importSerial.Name = "col_importSerial";
            this.col_importSerial.ReadOnly = true;
            this.col_importSerial.Visible = false;
            // 
            // col_sn
            // 
            this.col_sn.DataPropertyName = "sn";
            this.col_sn.HeaderText = "S/N";
            this.col_sn.MinimumWidth = 110;
            this.col_sn.Name = "col_sn";
            this.col_sn.ReadOnly = true;
            this.col_sn.Width = 110;
            // 
            // col_compname
            // 
            this.col_compname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_compname.DataPropertyName = "compname";
            this.col_compname.HeaderText = "Comp. Name";
            this.col_compname.MinimumWidth = 400;
            this.col_compname.Name = "col_compname";
            this.col_compname.ReadOnly = true;
            // 
            // txtZipcod
            // 
            this.txtZipcod._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZipcod._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtZipcod._MaxLength = 5;
            this.txtZipcod._ReadOnly = true;
            this.txtZipcod._Text = "";
            this.txtZipcod._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtZipcod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZipcod.BackColor = System.Drawing.Color.White;
            this.txtZipcod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZipcod.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtZipcod.Location = new System.Drawing.Point(823, 140);
            this.txtZipcod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtZipcod.Name = "txtZipcod";
            this.txtZipcod.Size = new System.Drawing.Size(67, 23);
            this.txtZipcod.TabIndex = 7;
            this.txtZipcod._TextChanged += new System.EventHandler(this.txtZipcod__TextChanged);
            // 
            // txtAddr03
            // 
            this.txtAddr03._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr03._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtAddr03._MaxLength = 30;
            this.txtAddr03._ReadOnly = true;
            this.txtAddr03._Text = "";
            this.txtAddr03._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAddr03.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddr03.BackColor = System.Drawing.Color.White;
            this.txtAddr03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr03.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAddr03.Location = new System.Drawing.Point(447, 140);
            this.txtAddr03.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAddr03.Name = "txtAddr03";
            this.txtAddr03.Size = new System.Drawing.Size(290, 23);
            this.txtAddr03.TabIndex = 6;
            this.txtAddr03._TextChanged += new System.EventHandler(this.txtAddr03__TextChanged);
            // 
            // txtPosition
            // 
            this.txtPosition._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPosition._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPosition._MaxLength = 50;
            this.txtPosition._ReadOnly = true;
            this.txtPosition._Text = "";
            this.txtPosition._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPosition.BackColor = System.Drawing.Color.White;
            this.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPosition.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPosition.Location = new System.Drawing.Point(447, 240);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(443, 23);
            this.txtPosition.TabIndex = 11;
            this.txtPosition._TextChanged += new System.EventHandler(this.txtPosition__TextChanged);
            // 
            // txtContact
            // 
            this.txtContact._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtContact._MaxLength = 100;
            this.txtContact._ReadOnly = true;
            this.txtContact._Text = "";
            this.txtContact._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContact.BackColor = System.Drawing.Color.White;
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtContact.Location = new System.Drawing.Point(447, 215);
            this.txtContact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(443, 23);
            this.txtContact.TabIndex = 10;
            this.txtContact._TextChanged += new System.EventHandler(this.txtContact__TextChanged);
            // 
            // txtFaxnum
            // 
            this.txtFaxnum._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFaxnum._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtFaxnum._MaxLength = 40;
            this.txtFaxnum._ReadOnly = true;
            this.txtFaxnum._Text = "";
            this.txtFaxnum._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFaxnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFaxnum.BackColor = System.Drawing.Color.White;
            this.txtFaxnum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFaxnum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFaxnum.Location = new System.Drawing.Point(447, 190);
            this.txtFaxnum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFaxnum.Name = "txtFaxnum";
            this.txtFaxnum.Size = new System.Drawing.Size(443, 23);
            this.txtFaxnum.TabIndex = 9;
            this.txtFaxnum._TextChanged += new System.EventHandler(this.txtFaxnum__TextChanged);
            // 
            // txtTelnum
            // 
            this.txtTelnum._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelnum._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtTelnum._MaxLength = 40;
            this.txtTelnum._ReadOnly = true;
            this.txtTelnum._Text = "";
            this.txtTelnum._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTelnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelnum.BackColor = System.Drawing.Color.White;
            this.txtTelnum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelnum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTelnum.Location = new System.Drawing.Point(447, 165);
            this.txtTelnum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTelnum.Name = "txtTelnum";
            this.txtTelnum.Size = new System.Drawing.Size(443, 23);
            this.txtTelnum.TabIndex = 8;
            this.txtTelnum._TextChanged += new System.EventHandler(this.txtTelnum__TextChanged);
            // 
            // txtAddr02
            // 
            this.txtAddr02._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr02._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtAddr02._MaxLength = 50;
            this.txtAddr02._ReadOnly = true;
            this.txtAddr02._Text = "";
            this.txtAddr02._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAddr02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddr02.BackColor = System.Drawing.Color.White;
            this.txtAddr02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr02.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAddr02.Location = new System.Drawing.Point(447, 115);
            this.txtAddr02.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAddr02.Name = "txtAddr02";
            this.txtAddr02.Size = new System.Drawing.Size(443, 23);
            this.txtAddr02.TabIndex = 5;
            this.txtAddr02.Load += new System.EventHandler(this.txtAddr02_Load);
            // 
            // txtAddr01
            // 
            this.txtAddr01._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr01._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtAddr01._MaxLength = 50;
            this.txtAddr01._ReadOnly = true;
            this.txtAddr01._Text = "";
            this.txtAddr01._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtAddr01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddr01.BackColor = System.Drawing.Color.White;
            this.txtAddr01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddr01.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAddr01.Location = new System.Drawing.Point(447, 90);
            this.txtAddr01.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAddr01.Name = "txtAddr01";
            this.txtAddr01.Size = new System.Drawing.Size(443, 23);
            this.txtAddr01.TabIndex = 4;
            this.txtAddr01._TextChanged += new System.EventHandler(this.txtAddr01__TextChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(765, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 16);
            this.label11.TabIndex = 48;
            this.label11.Text = "Zip Code";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label17.Location = new System.Drawing.Point(411, 193);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 16);
            this.label17.TabIndex = 47;
            this.label17.Text = "Fax.";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(391, 243);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 16);
            this.label14.TabIndex = 46;
            this.label14.Text = "Position";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(392, 218);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 16);
            this.label13.TabIndex = 45;
            this.label13.Text = "Contact";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(413, 168);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 16);
            this.label12.TabIndex = 44;
            this.label12.Text = "Tel.";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(389, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 16);
            this.label10.TabIndex = 43;
            this.label10.Text = "Address";
            // 
            // mskSernum
            // 
            this.mskSernum._Mask = ">A-AAA-AAAAAA";
            this.mskSernum._PromptChar = ' ';
            this.mskSernum._ReadOnly = true;
            this.mskSernum._SelectionLength = 0;
            this.mskSernum._SelectionStart = 0;
            this.mskSernum._Text = " -   -";
            this.mskSernum._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.mskSernum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mskSernum.BackColor = System.Drawing.Color.White;
            this.mskSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernum.Enabled = false;
            this.mskSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernum.Location = new System.Drawing.Point(447, 24);
            this.mskSernum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mskSernum.Name = "mskSernum";
            this.mskSernum.Size = new System.Drawing.Size(121, 23);
            this.mskSernum.TabIndex = 0;
            // 
            // txtCompnam
            // 
            this.txtCompnam._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompnam._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtCompnam._MaxLength = 100;
            this.txtCompnam._ReadOnly = true;
            this.txtCompnam._Text = "";
            this.txtCompnam._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCompnam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompnam.BackColor = System.Drawing.Color.White;
            this.txtCompnam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCompnam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCompnam.Location = new System.Drawing.Point(570, 49);
            this.txtCompnam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCompnam.Name = "txtCompnam";
            this.txtCompnam.Size = new System.Drawing.Size(322, 23);
            this.txtCompnam.TabIndex = 3;
            this.txtCompnam._TextChanged += new System.EventHandler(this.txtCompnam__TextChanged);
            // 
            // txtPrenam
            // 
            this.txtPrenam._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrenam._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtPrenam._MaxLength = 30;
            this.txtPrenam._ReadOnly = true;
            this.txtPrenam._Text = "";
            this.txtPrenam._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPrenam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrenam.BackColor = System.Drawing.Color.White;
            this.txtPrenam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrenam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPrenam.Location = new System.Drawing.Point(447, 49);
            this.txtPrenam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrenam.Name = "txtPrenam";
            this.txtPrenam.Size = new System.Drawing.Size(121, 23);
            this.txtPrenam.TabIndex = 2;
            this.txtPrenam._TextChanged += new System.EventHandler(this.txtPrenam__TextChanged);
            // 
            // txtVersion
            // 
            this.txtVersion._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtVersion._MaxLength = 4;
            this.txtVersion._ReadOnly = true;
            this.txtVersion._Text = "";
            this.txtVersion._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.BackColor = System.Drawing.Color.White;
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Enabled = false;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(692, 24);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(55, 23);
            this.txtVersion.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(379, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 170;
            this.label2.Text = "Company";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(639, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 171;
            this.label3.Text = "Version";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(379, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 169;
            this.label1.Text = "Serial No.";
            // 
            // lblDealer
            // 
            this.lblDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDealer.Location = new System.Drawing.Point(579, 410);
            this.lblDealer.Name = "lblDealer";
            this.lblDealer.Size = new System.Drawing.Size(311, 16);
            this.lblDealer.TabIndex = 180;
            // 
            // lblBusityp
            // 
            this.lblBusityp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusityp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblBusityp.Location = new System.Drawing.Point(550, 360);
            this.lblBusityp.Name = "lblBusityp";
            this.lblBusityp.Size = new System.Drawing.Size(340, 16);
            this.lblBusityp.TabIndex = 181;
            // 
            // brDealer
            // 
            this.brDealer._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.brDealer._ReadOnly = true;
            this.brDealer._Text = "";
            this.brDealer._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brDealer._UseImage = true;
            this.brDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brDealer.BackColor = System.Drawing.Color.White;
            this.brDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brDealer.Location = new System.Drawing.Point(447, 407);
            this.brDealer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brDealer.Name = "brDealer";
            this.brDealer.Size = new System.Drawing.Size(130, 23);
            this.brDealer.TabIndex = 17;
            this.brDealer._ButtonClick += new System.EventHandler(this.brDealer__ButtonClick);
            this.brDealer._Leave += new System.EventHandler(this.brDealer__Leave);
            // 
            // brBusityp
            // 
            this.brBusityp._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brBusityp._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.brBusityp._ReadOnly = true;
            this.brBusityp._Text = "";
            this.brBusityp._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brBusityp._UseImage = true;
            this.brBusityp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brBusityp.BackColor = System.Drawing.Color.White;
            this.brBusityp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brBusityp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brBusityp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brBusityp.Location = new System.Drawing.Point(447, 357);
            this.brBusityp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brBusityp.Name = "brBusityp";
            this.brBusityp.Size = new System.Drawing.Size(101, 23);
            this.brBusityp.TabIndex = 15;
            this.brBusityp._ButtonClick += new System.EventHandler(this.brBusityp__ButtonClick);
            this.brBusityp._Leave += new System.EventHandler(this.brBusityp__Leave);
            // 
            // txtBusides
            // 
            this.txtBusides._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusides._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtBusides._MaxLength = 40;
            this.txtBusides._ReadOnly = true;
            this.txtBusides._Text = "";
            this.txtBusides._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBusides.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusides.BackColor = System.Drawing.Color.White;
            this.txtBusides.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusides.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBusides.Location = new System.Drawing.Point(447, 307);
            this.txtBusides.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBusides.Name = "txtBusides";
            this.txtBusides.Size = new System.Drawing.Size(443, 23);
            this.txtBusides.TabIndex = 13;
            this.txtBusides._TextChanged += new System.EventHandler(this.txtBusides__TextChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label20.Location = new System.Drawing.Point(349, 385);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 16);
            this.label20.TabIndex = 179;
            this.label20.Text = "Purchase From";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label19.Location = new System.Drawing.Point(354, 335);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 16);
            this.label19.TabIndex = 178;
            this.label19.Text = "Business Type";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label18.Location = new System.Drawing.Point(351, 310);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 16);
            this.label18.TabIndex = 177;
            this.label18.Text = "Business Desc.";
            // 
            // txtBusityp
            // 
            this.txtBusityp._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusityp._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtBusityp._MaxLength = 40;
            this.txtBusityp._ReadOnly = true;
            this.txtBusityp._Text = "";
            this.txtBusityp._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBusityp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusityp.BackColor = System.Drawing.Color.White;
            this.txtBusityp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusityp.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtBusityp.Location = new System.Drawing.Point(447, 332);
            this.txtBusityp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBusityp.Name = "txtBusityp";
            this.txtBusityp.Size = new System.Drawing.Size(443, 23);
            this.txtBusityp.TabIndex = 14;
            // 
            // txtDealer
            // 
            this.txtDealer._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealer._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtDealer._MaxLength = 40;
            this.txtDealer._ReadOnly = true;
            this.txtDealer._Text = "";
            this.txtDealer._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDealer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDealer.BackColor = System.Drawing.Color.White;
            this.txtDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDealer.Location = new System.Drawing.Point(447, 382);
            this.txtDealer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDealer.Name = "txtDealer";
            this.txtDealer.Size = new System.Drawing.Size(443, 23);
            this.txtDealer.TabIndex = 16;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = global::SN_Net.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(506, 448);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnOK.Size = new System.Drawing.Size(124, 42);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "บันทึก <F9>";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::SN_Net.Properties.Resources.stop;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(640, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnCancel.Size = new System.Drawing.Size(132, 42);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "ยกเลิก <Esc>";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(399, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "E-mail";
            // 
            // txtEmail
            // 
            this.txtEmail._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEmail._MaxLength = 50;
            this.txtEmail._ReadOnly = true;
            this.txtEmail._Text = "";
            this.txtEmail._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtEmail.Location = new System.Drawing.Point(447, 265);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(443, 23);
            this.txtEmail.TabIndex = 12;
            this.txtEmail._TextChanged += new System.EventHandler(this.txtEmail__TextChanged);
            // 
            // btnGoSn
            // 
            this.btnGoSn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoSn.Enabled = false;
            this.btnGoSn.Image = global::SN_Net.Properties.Resources.lightning;
            this.btnGoSn.Location = new System.Drawing.Point(569, 23);
            this.btnGoSn.Name = "btnGoSn";
            this.btnGoSn.Size = new System.Drawing.Size(26, 25);
            this.btnGoSn.TabIndex = 182;
            this.btnGoSn.TabStop = false;
            this.toolTip1.SetToolTip(this.btnGoSn, "Go to this S/N <F6>");
            this.btnGoSn.UseVisualStyleBackColor = true;
            this.btnGoSn.Click += new System.EventHandler(this.btnGoSn_Click);
            // 
            // DialogImportRegisterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 507);
            this.Controls.Add(this.btnGoSn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblDealer);
            this.Controls.Add(this.lblBusityp);
            this.Controls.Add(this.brDealer);
            this.Controls.Add(this.brBusityp);
            this.Controls.Add(this.txtDealer);
            this.Controls.Add(this.txtBusityp);
            this.Controls.Add(this.txtBusides);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.mskSernum);
            this.Controls.Add(this.txtCompnam);
            this.Controls.Add(this.txtPrenam);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtZipcod);
            this.Controls.Add(this.txtAddr03);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.txtFaxnum);
            this.Controls.Add(this.txtTelnum);
            this.Controls.Add(this.txtAddr02);
            this.Controls.Add(this.txtAddr01);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogImportRegisterData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Registered Data";
            this.Load += new System.EventHandler(this.DialogImportRegisterData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CC.XDatagrid dgv;
        private CC.XNumTextEdit txtZipcod;
        private CC.XTextEdit txtAddr03;
        private CC.XTextEdit txtPosition;
        private CC.XTextEdit txtContact;
        private CC.XTextEdit txtFaxnum;
        private CC.XTextEdit txtTelnum;
        private CC.XTextEdit txtAddr02;
        private CC.XTextEdit txtAddr01;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private CC.XTextEditMasked mskSernum;
        private CC.XTextEdit txtCompnam;
        private CC.XTextEdit txtPrenam;
        private CC.XTextEdit txtVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDealer;
        private System.Windows.Forms.Label lblBusityp;
        private CC.XBrowseBox brDealer;
        private CC.XBrowseBox brBusityp;
        private CC.XTextEdit txtBusides;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private CC.XTextEdit txtBusityp;
        private CC.XTextEdit txtDealer;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private CC.XTextEdit txtEmail;
        private System.Windows.Forms.Button btnGoSn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_recorded;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_importSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_compname;
    }
}