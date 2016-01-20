namespace SN_Net.Subform
{
    partial class MacAddressList
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
            this.dgvMacAddress = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSubmitMacAddress = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMacAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddCurrentMac = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusCurrentMac = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacAddress)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMacAddress
            // 
            this.dgvMacAddress.AllowUserToAddRows = false;
            this.dgvMacAddress.AllowUserToDeleteRows = false;
            this.dgvMacAddress.AllowUserToResizeRows = false;
            this.dgvMacAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMacAddress.Location = new System.Drawing.Point(12, 84);
            this.dgvMacAddress.MultiSelect = false;
            this.dgvMacAddress.Name = "dgvMacAddress";
            this.dgvMacAddress.ReadOnly = true;
            this.dgvMacAddress.RowHeadersVisible = false;
            this.dgvMacAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMacAddress.Size = new System.Drawing.Size(440, 386);
            this.dgvMacAddress.TabIndex = 0;
            this.dgvMacAddress.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMacAddress_CellDoubleClick);
            this.dgvMacAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvMacAddress_KeyDown);
            this.dgvMacAddress.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMacAddress_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSubmitMacAddress);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMacAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "เพิ่ม MAC Address ที่ต้องการด้วยตนเอง";
            // 
            // btnSubmitMacAddress
            // 
            this.btnSubmitMacAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSubmitMacAddress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSubmitMacAddress.Location = new System.Drawing.Point(252, 21);
            this.btnSubmitMacAddress.Name = "btnSubmitMacAddress";
            this.btnSubmitMacAddress.Size = new System.Drawing.Size(54, 24);
            this.btnSubmitMacAddress.TabIndex = 3;
            this.btnSubmitMacAddress.Text = "ตกลง";
            this.btnSubmitMacAddress.UseVisualStyleBackColor = true;
            this.btnSubmitMacAddress.Click += new System.EventHandler(this.btnSubmitMacAddress_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(91, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "( Format : XX-XX-XX-XX-XX-XX )";
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtMacAddress.Location = new System.Drawing.Point(95, 21);
            this.txtMacAddress.MaxLength = 50;
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(153, 23);
            this.txtMacAddress.TabIndex = 2;
            this.txtMacAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMacAddress_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "MAC Address : ";
            // 
            // btnAddCurrentMac
            // 
            this.btnAddCurrentMac.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnAddCurrentMac.Location = new System.Drawing.Point(337, 16);
            this.btnAddCurrentMac.Name = "btnAddCurrentMac";
            this.btnAddCurrentMac.Size = new System.Drawing.Size(114, 62);
            this.btnAddCurrentMac.TabIndex = 3;
            this.btnAddCurrentMac.Text = "เพิ่ม MAC Address ของเครื่องนี้";
            this.btnAddCurrentMac.UseVisualStyleBackColor = true;
            this.btnAddCurrentMac.Click += new System.EventHandler(this.btnAddCurrentMac_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(464, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusCurrentMac
            // 
            this.toolStripStatusCurrentMac.Name = "toolStripStatusCurrentMac";
            this.toolStripStatusCurrentMac.Size = new System.Drawing.Size(147, 17);
            this.toolStripStatusCurrentMac.Text = "toolStripStatusCurrentMac";
            // 
            // MacAddressList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 504);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnAddCurrentMac);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvMacAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MacAddressList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MAC Address ที่ได้รับอนุญาต";
            this.Load += new System.EventHandler(this.MacAddressList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacAddress)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMacAddress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmitMacAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMacAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddCurrentMac;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusCurrentMac;
    }
}