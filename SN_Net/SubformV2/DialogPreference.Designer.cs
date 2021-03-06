﻿namespace SN_Net.Subform
{
    partial class DialogPreference
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserid = new System.Windows.Forms.TextBox();
            this.txtDbname = new System.Windows.Forms.TextBox();
            this.txtServername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numPortNote = new System.Windows.Forms.NumericUpDown();
            this.btnTestNoteConnection = new System.Windows.Forms.Button();
            this.txtPasswordNote = new System.Windows.Forms.TextBox();
            this.txtUseridNote = new System.Windows.Forms.TextBox();
            this.txtDbnameNote = new System.Windows.Forms.TextBox();
            this.txtServernameNote = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPortNote)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numPort);
            this.groupBox1.Controls.Add(this.btnTestConnection);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserid);
            this.groupBox1.Controls.Add(this.txtDbname);
            this.groupBox1.Controls.Add(this.txtServername);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 220);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main SN Database Connection";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(147, 144);
            this.numPort.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(85, 23);
            this.numPort.TabIndex = 4;
            this.numPort.Value = new decimal(new int[] {
            3306,
            0,
            0,
            0});
            this.numPort.ValueChanged += new System.EventHandler(this.numPort_ValueChanged);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(147, 173);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(176, 29);
            this.btnTestConnection.TabIndex = 5;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(147, 116);
            this.txtPassword.MaxLength = 16;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(176, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUserid
            // 
            this.txtUserid.Location = new System.Drawing.Point(147, 89);
            this.txtUserid.MaxLength = 16;
            this.txtUserid.Name = "txtUserid";
            this.txtUserid.Size = new System.Drawing.Size(176, 23);
            this.txtUserid.TabIndex = 2;
            this.txtUserid.TextChanged += new System.EventHandler(this.txtUserid_TextChanged);
            // 
            // txtDbname
            // 
            this.txtDbname.Location = new System.Drawing.Point(147, 62);
            this.txtDbname.Name = "txtDbname";
            this.txtDbname.Size = new System.Drawing.Size(176, 23);
            this.txtDbname.TabIndex = 1;
            this.txtDbname.TextChanged += new System.EventHandler(this.txtDbname_TextChanged);
            // 
            // txtServername
            // 
            this.txtServername.Location = new System.Drawing.Point(147, 35);
            this.txtServername.Name = "txtServername";
            this.txtServername.Size = new System.Drawing.Size(176, 23);
            this.txtServername.TabIndex = 0;
            this.txtServername.TextChanged += new System.EventHandler(this.txtServername_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Port #";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "User ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(122, 475);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 29);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 29);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numPortNote);
            this.groupBox2.Controls.Add(this.btnTestNoteConnection);
            this.groupBox2.Controls.Add(this.txtPasswordNote);
            this.groupBox2.Controls.Add(this.txtUseridNote);
            this.groupBox2.Controls.Add(this.txtDbnameNote);
            this.groupBox2.Controls.Add(this.txtServernameNote);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(12, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 220);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Note Database Connection";
            // 
            // numPortNote
            // 
            this.numPortNote.Location = new System.Drawing.Point(147, 144);
            this.numPortNote.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numPortNote.Name = "numPortNote";
            this.numPortNote.Size = new System.Drawing.Size(85, 23);
            this.numPortNote.TabIndex = 4;
            this.numPortNote.Value = new decimal(new int[] {
            3306,
            0,
            0,
            0});
            this.numPortNote.ValueChanged += new System.EventHandler(this.numPortNote_ValueChanged);
            // 
            // btnTestNoteConnection
            // 
            this.btnTestNoteConnection.Location = new System.Drawing.Point(147, 173);
            this.btnTestNoteConnection.Name = "btnTestNoteConnection";
            this.btnTestNoteConnection.Size = new System.Drawing.Size(176, 29);
            this.btnTestNoteConnection.TabIndex = 5;
            this.btnTestNoteConnection.Text = "Test Connection";
            this.btnTestNoteConnection.UseVisualStyleBackColor = true;
            this.btnTestNoteConnection.Click += new System.EventHandler(this.btnTestNoteConnection_Click);
            // 
            // txtPasswordNote
            // 
            this.txtPasswordNote.Location = new System.Drawing.Point(147, 116);
            this.txtPasswordNote.MaxLength = 16;
            this.txtPasswordNote.Name = "txtPasswordNote";
            this.txtPasswordNote.PasswordChar = '*';
            this.txtPasswordNote.Size = new System.Drawing.Size(176, 23);
            this.txtPasswordNote.TabIndex = 3;
            this.txtPasswordNote.TextChanged += new System.EventHandler(this.txtPasswordNote_TextChanged);
            // 
            // txtUseridNote
            // 
            this.txtUseridNote.Location = new System.Drawing.Point(147, 89);
            this.txtUseridNote.MaxLength = 16;
            this.txtUseridNote.Name = "txtUseridNote";
            this.txtUseridNote.Size = new System.Drawing.Size(176, 23);
            this.txtUseridNote.TabIndex = 2;
            this.txtUseridNote.TextChanged += new System.EventHandler(this.txtUseridNote_TextChanged);
            // 
            // txtDbnameNote
            // 
            this.txtDbnameNote.Location = new System.Drawing.Point(147, 62);
            this.txtDbnameNote.Name = "txtDbnameNote";
            this.txtDbnameNote.Size = new System.Drawing.Size(176, 23);
            this.txtDbnameNote.TabIndex = 1;
            this.txtDbnameNote.TextChanged += new System.EventHandler(this.txtDbnameNote_TextChanged);
            // 
            // txtServernameNote
            // 
            this.txtServernameNote.Location = new System.Drawing.Point(147, 35);
            this.txtServernameNote.Name = "txtServernameNote";
            this.txtServernameNote.Size = new System.Drawing.Size(176, 23);
            this.txtServernameNote.TabIndex = 0;
            this.txtServernameNote.TextChanged += new System.EventHandler(this.txtServernameNote_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Port #";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "User ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Database Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Server Name";
            // 
            // DialogPreference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 518);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogPreference";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System Configuration";
            this.Load += new System.EventHandler(this.DialogPreference_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPortNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserid;
        private System.Windows.Forms.TextBox txtDbname;
        private System.Windows.Forms.TextBox txtServername;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numPortNote;
        private System.Windows.Forms.Button btnTestNoteConnection;
        private System.Windows.Forms.TextBox txtPasswordNote;
        private System.Windows.Forms.TextBox txtUseridNote;
        private System.Windows.Forms.TextBox txtDbnameNote;
        private System.Windows.Forms.TextBox txtServernameNote;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}