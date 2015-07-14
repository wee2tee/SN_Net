namespace SN_Net.Subform
{
    partial class UsersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersList));
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelAddUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.cbWebLogin = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbUserStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbUserLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(12, 100);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(720, 399);
            this.dgvUsers.TabIndex = 1;
            this.dgvUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellDoubleClick);
            this.dgvUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvUsers_KeyDown);
            this.dgvUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvUsers_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelAddUser);
            this.groupBox1.Controls.Add(this.btnAddUser);
            this.groupBox1.Controls.Add(this.cbWebLogin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbUserStatus);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbUserLevel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "เพิ่มผู้ใช้งานระบบ";
            // 
            // btnCancelAddUser
            // 
            this.btnCancelAddUser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancelAddUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancelAddUser.Location = new System.Drawing.Point(603, 46);
            this.btnCancelAddUser.Name = "btnCancelAddUser";
            this.btnCancelAddUser.Size = new System.Drawing.Size(102, 27);
            this.btnCancelAddUser.TabIndex = 8;
            this.btnCancelAddUser.Text = "ยกเลิก";
            this.btnCancelAddUser.UseVisualStyleBackColor = true;
            this.btnCancelAddUser.Click += new System.EventHandler(this.btnCancelAddUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddUser.Location = new System.Drawing.Point(603, 16);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(102, 27);
            this.btnAddUser.TabIndex = 7;
            this.btnAddUser.Text = "เพิ่มข้อมูลผู้ใช้";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // cbWebLogin
            // 
            this.cbWebLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWebLogin.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbWebLogin.FormattingEnabled = true;
            this.cbWebLogin.Location = new System.Drawing.Point(340, 48);
            this.cbWebLogin.Name = "cbWebLogin";
            this.cbWebLogin.Size = new System.Drawing.Size(46, 24);
            this.cbWebLogin.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(201, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "ใช้งานผ่าน Web UI ได้ : ";
            // 
            // cbUserStatus
            // 
            this.cbUserStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbUserStatus.FormattingEnabled = true;
            this.cbUserStatus.Location = new System.Drawing.Point(506, 48);
            this.cbUserStatus.Name = "cbUserStatus";
            this.cbUserStatus.Size = new System.Drawing.Size(77, 24);
            this.cbUserStatus.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(428, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "สถานะผู้ใช้ : ";
            // 
            // cbUserLevel
            // 
            this.cbUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserLevel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbUserLevel.FormattingEnabled = true;
            this.cbUserLevel.Location = new System.Drawing.Point(81, 48);
            this.cbUserLevel.Name = "cbUserLevel";
            this.cbUserLevel.Size = new System.Drawing.Size(77, 24);
            this.cbUserLevel.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(13, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "ระดับผู้ใช้ : ";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtEmail.Location = new System.Drawing.Point(340, 17);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(243, 23);
            this.txtEmail.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(294, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "อีเมล์ : ";
            // 
            // txtUserName
            // 
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUserName.Location = new System.Drawing.Point(81, 17);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(153, 23);
            this.txtUserName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "ชื่อผู้ใช้ : ";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "key.png");
            // 
            // UsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UsersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "แฟ้มข้อมูลผู้ใช้งานระบบ";
            this.Load += new System.EventHandler(this.UsersList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUserLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbUserStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbWebLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnCancelAddUser;
        private System.Windows.Forms.ImageList imageList1;
    }
}