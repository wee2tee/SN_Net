namespace SN_Net.Subform
{
    partial class MAFormDialog
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
            this.maEmail = new SN_Net.MiscClass.CustomTextBox();
            this.maDateTo = new SN_Net.MiscClass.CustomDateTimePicker();
            this.maDateFrom = new SN_Net.MiscClass.CustomDateTimePicker();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // maEmail
            // 
            this.maEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maEmail.BackColor = System.Drawing.Color.White;
            this.maEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maEmail.CharUpperCase = false;
            this.maEmail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.maEmail.Location = new System.Drawing.Point(64, 73);
            this.maEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maEmail.MaxChar = 0;
            this.maEmail.Name = "maEmail";
            this.maEmail.Read_Only = false;
            this.maEmail.SelectionLength = 0;
            this.maEmail.SelectionStart = 0;
            this.maEmail.Size = new System.Drawing.Size(356, 23);
            this.maEmail.TabIndex = 2;
            this.maEmail.Texts = "";
            // 
            // maDateTo
            // 
            this.maDateTo.BackColor = System.Drawing.Color.White;
            this.maDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maDateTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.maDateTo.Location = new System.Drawing.Point(64, 47);
            this.maDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maDateTo.Name = "maDateTo";
            this.maDateTo.Read_Only = false;
            this.maDateTo.Size = new System.Drawing.Size(96, 23);
            this.maDateTo.TabIndex = 1;
            this.maDateTo.Texts = "22/08/2559";
            this.maDateTo.TextsMysql = "2016-08-22";
            this.maDateTo.ValDateTime = new System.DateTime(2016, 8, 22, 15, 42, 18, 484);
            // 
            // maDateFrom
            // 
            this.maDateFrom.BackColor = System.Drawing.Color.White;
            this.maDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maDateFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.maDateFrom.Location = new System.Drawing.Point(64, 21);
            this.maDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maDateFrom.Name = "maDateFrom";
            this.maDateFrom.Read_Only = false;
            this.maDateFrom.Size = new System.Drawing.Size(96, 23);
            this.maDateFrom.TabIndex = 0;
            this.maDateFrom.Texts = "22/08/2559";
            this.maDateFrom.TextsMysql = "2016-08-22";
            this.maDateFrom.ValDateTime = new System.DateTime(2016, 8, 22, 15, 42, 18, 486);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label35.Location = new System.Drawing.Point(21, 74);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(36, 16);
            this.label35.TabIndex = 150;
            this.label35.Text = "อีเมล์";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label34.Location = new System.Drawing.Point(21, 49);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(40, 16);
            this.label34.TabIndex = 149;
            this.label34.Text = "สิ้นสุด";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label33.Location = new System.Drawing.Point(21, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(26, 16);
            this.label33.TabIndex = 148;
            this.label33.Text = "เริ่ม";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(144, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(225, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // MAFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 151);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.maEmail);
            this.Controls.Add(this.maDateTo);
            this.Controls.Add(this.maDateFrom);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 190);
            this.Name = "MAFormDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MA. Service";
            this.Load += new System.EventHandler(this.MAFormDialog_Load);
            this.Shown += new System.EventHandler(this.MAFormDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        public MiscClass.CustomTextBox maEmail;
        public MiscClass.CustomDateTimePicker maDateTo;
        public MiscClass.CustomDateTimePicker maDateFrom;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnCancel;
    }
}