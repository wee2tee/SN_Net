namespace SN_Net.Subform
{
    partial class MacAddressEditForm
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
            this.lblID = new System.Windows.Forms.Label();
            this.txtMacAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmitChangeMacAddress = new System.Windows.Forms.Button();
            this.btnCancelChangeMacAddress = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Controls.Add(this.txtMacAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblID.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblID.Location = new System.Drawing.Point(49, 39);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(22, 16);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "99";
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtMacAddress.Location = new System.Drawing.Point(110, 36);
            this.txtMacAddress.MaxLength = 50;
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(144, 23);
            this.txtMacAddress.TabIndex = 2;
            this.txtMacAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMacAddress_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(107, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "MAC Address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(49, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID.";
            // 
            // btnSubmitChangeMacAddress
            // 
            this.btnSubmitChangeMacAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSubmitChangeMacAddress.Location = new System.Drawing.Point(76, 92);
            this.btnSubmitChangeMacAddress.Name = "btnSubmitChangeMacAddress";
            this.btnSubmitChangeMacAddress.Size = new System.Drawing.Size(86, 30);
            this.btnSubmitChangeMacAddress.TabIndex = 1;
            this.btnSubmitChangeMacAddress.Text = "ตกลง";
            this.btnSubmitChangeMacAddress.UseVisualStyleBackColor = true;
            this.btnSubmitChangeMacAddress.Click += new System.EventHandler(this.btnSubmitChangeMacAddress_Click);
            // 
            // btnCancelChangeMacAddress
            // 
            this.btnCancelChangeMacAddress.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelChangeMacAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancelChangeMacAddress.Location = new System.Drawing.Point(189, 92);
            this.btnCancelChangeMacAddress.Name = "btnCancelChangeMacAddress";
            this.btnCancelChangeMacAddress.Size = new System.Drawing.Size(86, 30);
            this.btnCancelChangeMacAddress.TabIndex = 2;
            this.btnCancelChangeMacAddress.Text = "ยกเลิก";
            this.btnCancelChangeMacAddress.UseVisualStyleBackColor = true;
            // 
            // MacAddressEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 134);
            this.Controls.Add(this.btnCancelChangeMacAddress);
            this.Controls.Add(this.btnSubmitChangeMacAddress);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MacAddressEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "แก้ไข Mac address";
            this.Load += new System.EventHandler(this.MacAddressEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmitChangeMacAddress;
        private System.Windows.Forms.Button btnCancelChangeMacAddress;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtMacAddress;

    }
}