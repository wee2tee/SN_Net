namespace SN_Net.Subform
{
    partial class FormUsers
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResetPassword = new System.Windows.Forms.ToolStripButton();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.inlineMaxAbsent = new CC.XNumEdit();
            this.inlineTrainer = new CC.XDropdownList();
            this.inlineStatus = new CC.XDropdownList();
            this.inlineGroup = new CC.XDropdownList();
            this.inlineLevel = new CC.XDropdownList();
            this.inlineName = new CC.XTextEdit();
            this.inlineUserName = new CC.XTextEdit();
            this.dgv = new CC.XDatagrid();
            this.col_users = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_userpassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_usergroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_allowed_web_login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_training_expert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_max_absent = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnResetPassword,
            this.btnReload});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(871, 43);
            this.toolStrip1.TabIndex = 8;
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
            // btnResetPassword
            // 
            this.btnResetPassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResetPassword.Image = global::SN_Net.Properties.Resources.key_32;
            this.btnResetPassword.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnResetPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(36, 40);
            this.btnResetPassword.Text = "toolStripButton1";
            this.btnResetPassword.ToolTipText = "Reset Password";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
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
            this.btnReload.Text = "Reload data <Ctrl + F5>";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.inlineMaxAbsent);
            this.panel1.Controls.Add(this.inlineTrainer);
            this.panel1.Controls.Add(this.inlineStatus);
            this.panel1.Controls.Add(this.inlineGroup);
            this.panel1.Controls.Add(this.inlineLevel);
            this.panel1.Controls.Add(this.inlineName);
            this.panel1.Controls.Add(this.inlineUserName);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 509);
            this.panel1.TabIndex = 9;
            // 
            // inlineMaxAbsent
            // 
            this.inlineMaxAbsent._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineMaxAbsent._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineMaxAbsent._DecimalDigit = 0;
            this.inlineMaxAbsent._ForeColorReadOnlyState = System.Drawing.SystemColors.ControlText;
            this.inlineMaxAbsent._MaximumValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.inlineMaxAbsent._MaxLength = 30;
            this.inlineMaxAbsent._ReadOnly = false;
            this.inlineMaxAbsent._SelectionLength = 0;
            this.inlineMaxAbsent._SelectionStart = 1;
            this.inlineMaxAbsent._Text = "0";
            this.inlineMaxAbsent._TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.inlineMaxAbsent._UseThoundsandSeparate = false;
            this.inlineMaxAbsent._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.inlineMaxAbsent.BackColor = System.Drawing.Color.White;
            this.inlineMaxAbsent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineMaxAbsent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineMaxAbsent.Location = new System.Drawing.Point(795, 67);
            this.inlineMaxAbsent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineMaxAbsent.Name = "inlineMaxAbsent";
            this.inlineMaxAbsent.Size = new System.Drawing.Size(71, 23);
            this.inlineMaxAbsent.TabIndex = 7;
            this.inlineMaxAbsent._ValueChanged += new System.EventHandler(this.inlineMaxAbsent__ValueChanged);
            // 
            // inlineTrainer
            // 
            this.inlineTrainer._ReadOnly = false;
            this.inlineTrainer._SelectedItem = null;
            this.inlineTrainer._Text = "";
            this.inlineTrainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineTrainer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineTrainer.Location = new System.Drawing.Point(735, 67);
            this.inlineTrainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineTrainer.Name = "inlineTrainer";
            this.inlineTrainer.Size = new System.Drawing.Size(58, 23);
            this.inlineTrainer.TabIndex = 6;
            this.inlineTrainer._SelectedItemChanged += new System.EventHandler(this.inlineTrainer__SelectedItemChanged);
            // 
            // inlineStatus
            // 
            this.inlineStatus._ReadOnly = false;
            this.inlineStatus._SelectedItem = null;
            this.inlineStatus._Text = "";
            this.inlineStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineStatus.Location = new System.Drawing.Point(672, 67);
            this.inlineStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineStatus.Name = "inlineStatus";
            this.inlineStatus.Size = new System.Drawing.Size(58, 23);
            this.inlineStatus.TabIndex = 5;
            this.inlineStatus._SelectedItemChanged += new System.EventHandler(this.inlineStatus__SelectedItemChanged);
            // 
            // inlineGroup
            // 
            this.inlineGroup._ReadOnly = false;
            this.inlineGroup._SelectedItem = null;
            this.inlineGroup._Text = "";
            this.inlineGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineGroup.Location = new System.Drawing.Point(609, 67);
            this.inlineGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineGroup.Name = "inlineGroup";
            this.inlineGroup.Size = new System.Drawing.Size(58, 23);
            this.inlineGroup.TabIndex = 4;
            this.inlineGroup._SelectedItemChanged += new System.EventHandler(this.inlineGroup__SelectedItemChanged);
            // 
            // inlineLevel
            // 
            this.inlineLevel._ReadOnly = false;
            this.inlineLevel._SelectedItem = null;
            this.inlineLevel._Text = "";
            this.inlineLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineLevel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineLevel.Location = new System.Drawing.Point(491, 67);
            this.inlineLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineLevel.Name = "inlineLevel";
            this.inlineLevel.Size = new System.Drawing.Size(115, 23);
            this.inlineLevel.TabIndex = 3;
            this.inlineLevel._SelectedItemChanged += new System.EventHandler(this.inlineLevel__SelectedItemChanged);
            // 
            // inlineName
            // 
            this.inlineName._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineName._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.inlineName._MaxLength = 32767;
            this.inlineName._ReadOnly = false;
            this.inlineName._SelectionLength = 0;
            this.inlineName._SelectionStart = 0;
            this.inlineName._Text = "";
            this.inlineName._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineName.BackColor = System.Drawing.Color.White;
            this.inlineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineName.Location = new System.Drawing.Point(153, 67);
            this.inlineName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineName.Name = "inlineName";
            this.inlineName.Size = new System.Drawing.Size(335, 23);
            this.inlineName.TabIndex = 2;
            this.inlineName._TextChanged += new System.EventHandler(this.inlineName__TextChanged);
            // 
            // inlineUserName
            // 
            this.inlineUserName._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineUserName._CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.inlineUserName._MaxLength = 32767;
            this.inlineUserName._ReadOnly = false;
            this.inlineUserName._SelectionLength = 0;
            this.inlineUserName._SelectionStart = 0;
            this.inlineUserName._Text = "";
            this.inlineUserName._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.inlineUserName.BackColor = System.Drawing.Color.White;
            this.inlineUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inlineUserName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inlineUserName.Location = new System.Drawing.Point(4, 67);
            this.inlineUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inlineUserName.Name = "inlineUserName";
            this.inlineUserName.Size = new System.Drawing.Size(146, 23);
            this.inlineUserName.TabIndex = 1;
            this.inlineUserName._TextChanged += new System.EventHandler(this.inlineUserName__TextChanged);
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
            this.col_users,
            this.col_id,
            this.col_username,
            this.col_userpassword,
            this.col_name,
            this.col_email,
            this.col_level,
            this.col_usergroup,
            this.col_status,
            this.col_allowed_web_login,
            this.col_training_expert,
            this.col_max_absent});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
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
            this.dgv.Size = new System.Drawing.Size(871, 509);
            this.dgv.StandardTab = true;
            this.dgv.TabIndex = 0;
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            this.dgv.Resize += new System.EventHandler(this.dgv_Resize);
            // 
            // col_users
            // 
            this.col_users.DataPropertyName = "users";
            this.col_users.HeaderText = "Users";
            this.col_users.Name = "col_users";
            this.col_users.ReadOnly = true;
            this.col_users.Visible = false;
            // 
            // col_id
            // 
            this.col_id.DataPropertyName = "id";
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            // 
            // col_username
            // 
            this.col_username.DataPropertyName = "username";
            this.col_username.HeaderText = "รหัสผู้ใช้";
            this.col_username.MinimumWidth = 150;
            this.col_username.Name = "col_username";
            this.col_username.ReadOnly = true;
            this.col_username.Width = 150;
            // 
            // col_userpassword
            // 
            this.col_userpassword.DataPropertyName = "userpassword";
            this.col_userpassword.HeaderText = "Password";
            this.col_userpassword.Name = "col_userpassword";
            this.col_userpassword.ReadOnly = true;
            this.col_userpassword.Visible = false;
            // 
            // col_name
            // 
            this.col_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_name.DataPropertyName = "name";
            this.col_name.HeaderText = "ชื่อผู้ใช้";
            this.col_name.MinimumWidth = 20;
            this.col_name.Name = "col_name";
            this.col_name.ReadOnly = true;
            // 
            // col_email
            // 
            this.col_email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_email.DataPropertyName = "email";
            this.col_email.HeaderText = "Email";
            this.col_email.Name = "col_email";
            this.col_email.ReadOnly = true;
            this.col_email.Visible = false;
            // 
            // col_level
            // 
            this.col_level.DataPropertyName = "level";
            this.col_level.HeaderText = "กลุ่มผู้ใช้";
            this.col_level.MinimumWidth = 120;
            this.col_level.Name = "col_level";
            this.col_level.ReadOnly = true;
            this.col_level.Width = 120;
            // 
            // col_usergroup
            // 
            this.col_usergroup.DataPropertyName = "usergroup";
            this.col_usergroup.HeaderText = "กลุ่ม";
            this.col_usergroup.MinimumWidth = 60;
            this.col_usergroup.Name = "col_usergroup";
            this.col_usergroup.ReadOnly = true;
            this.col_usergroup.Width = 60;
            // 
            // col_status
            // 
            this.col_status.DataPropertyName = "status";
            this.col_status.HeaderText = "สถานะ";
            this.col_status.MinimumWidth = 60;
            this.col_status.Name = "col_status";
            this.col_status.ReadOnly = true;
            this.col_status.Width = 60;
            // 
            // col_allowed_web_login
            // 
            this.col_allowed_web_login.DataPropertyName = "allowed_web_login";
            this.col_allowed_web_login.HeaderText = "Web Login";
            this.col_allowed_web_login.Name = "col_allowed_web_login";
            this.col_allowed_web_login.ReadOnly = true;
            this.col_allowed_web_login.Visible = false;
            // 
            // col_training_expert
            // 
            this.col_training_expert.DataPropertyName = "training_expert";
            this.col_training_expert.HeaderText = "วิทยากร";
            this.col_training_expert.MinimumWidth = 60;
            this.col_training_expert.Name = "col_training_expert";
            this.col_training_expert.ReadOnly = true;
            this.col_training_expert.Width = 60;
            // 
            // col_max_absent
            // 
            this.col_max_absent.DataPropertyName = "max_absent";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_max_absent.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_max_absent.HeaderText = "วันลา(Max.)";
            this.col_max_absent.MinimumWidth = 90;
            this.col_max_absent.Name = "col_max_absent";
            this.col_max_absent.ReadOnly = true;
            this.col_max_absent.Width = 90;
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormUsers";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Users Information";
            this.Load += new System.EventHandler(this.FormUsers_Load);
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
        private CC.XDropdownList inlineTrainer;
        private CC.XDropdownList inlineStatus;
        private CC.XDropdownList inlineGroup;
        private CC.XDropdownList inlineLevel;
        private CC.XTextEdit inlineName;
        private CC.XTextEdit inlineUserName;
        private CC.XNumEdit inlineMaxAbsent;
        private System.Windows.Forms.ToolStripButton btnResetPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_users;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_userpassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_email;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_level;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_usergroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_allowed_web_login;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_training_expert;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_max_absent;
    }
}