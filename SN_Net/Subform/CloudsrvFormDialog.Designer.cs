namespace SN_Net.Subform
{
    partial class CloudsrvFormDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cloudEmail = new SN_Net.MiscClass.CustomTextBox();
            this.cloudDateTo = new SN_Net.MiscClass.CustomDateTimePicker();
            this.cloudDateFrom = new SN_Net.MiscClass.CustomDateTimePicker();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(225, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 155;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(144, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 154;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cloudEmail
            // 
            this.cloudEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudEmail.BackColor = System.Drawing.Color.White;
            this.cloudEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cloudEmail.CharUpperCase = false;
            this.cloudEmail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cloudEmail.Location = new System.Drawing.Point(64, 73);
            this.cloudEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cloudEmail.MaxChar = 0;
            this.cloudEmail.Name = "cloudEmail";
            this.cloudEmail.Read_Only = false;
            this.cloudEmail.SelectionLength = 0;
            this.cloudEmail.SelectionStart = 0;
            this.cloudEmail.Size = new System.Drawing.Size(356, 23);
            this.cloudEmail.TabIndex = 153;
            this.cloudEmail.Texts = "";
            // 
            // cloudDateTo
            // 
            this.cloudDateTo.BackColor = System.Drawing.Color.White;
            this.cloudDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cloudDateTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cloudDateTo.Location = new System.Drawing.Point(64, 47);
            this.cloudDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cloudDateTo.Name = "cloudDateTo";
            this.cloudDateTo.Read_Only = false;
            this.cloudDateTo.Size = new System.Drawing.Size(96, 23);
            this.cloudDateTo.TabIndex = 152;
            this.cloudDateTo.Texts = "22/08/2559";
            this.cloudDateTo.TextsMysql = "2016-08-22";
            this.cloudDateTo.ValDateTime = new System.DateTime(2016, 8, 22, 15, 44, 38, 917);
            // 
            // cloudDateFrom
            // 
            this.cloudDateFrom.BackColor = System.Drawing.Color.White;
            this.cloudDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cloudDateFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cloudDateFrom.Location = new System.Drawing.Point(64, 21);
            this.cloudDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cloudDateFrom.Name = "cloudDateFrom";
            this.cloudDateFrom.Read_Only = false;
            this.cloudDateFrom.Size = new System.Drawing.Size(96, 23);
            this.cloudDateFrom.TabIndex = 151;
            this.cloudDateFrom.Texts = "22/08/2559";
            this.cloudDateFrom.TextsMysql = "2016-08-22";
            this.cloudDateFrom.ValDateTime = new System.DateTime(2016, 8, 22, 15, 44, 38, 920);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label35.Location = new System.Drawing.Point(21, 74);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(36, 16);
            this.label35.TabIndex = 158;
            this.label35.Text = "อีเมล์";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label34.Location = new System.Drawing.Point(21, 49);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(40, 16);
            this.label34.TabIndex = 157;
            this.label34.Text = "สิ้นสุด";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label33.Location = new System.Drawing.Point(21, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(26, 16);
            this.label33.TabIndex = 156;
            this.label33.Text = "เริ่ม";
            // 
            // CloudsrvFormDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 151);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cloudEmail);
            this.Controls.Add(this.cloudDateTo);
            this.Controls.Add(this.cloudDateFrom);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CloudsrvFormDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cloud Service";
            this.Load += new System.EventHandler(this.CloudsrvFormDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnOK;
        public MiscClass.CustomTextBox cloudEmail;
        public MiscClass.CustomDateTimePicker cloudDateTo;
        public MiscClass.CustomDateTimePicker cloudDateFrom;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
    }
}