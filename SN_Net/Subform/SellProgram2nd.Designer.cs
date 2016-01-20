namespace SN_Net.Subform
{
    partial class SellProgram2nd
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
            this.btnBrowseDealer = new System.Windows.Forms.Button();
            this.lblDealer_Compnam = new System.Windows.Forms.Label();
            this.txtDealer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.mskOldSernum = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.msk2Sernum = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseDealer
            // 
            this.btnBrowseDealer.Image = global::SN_Net.Properties.Resources.zoom;
            this.btnBrowseDealer.Location = new System.Drawing.Point(248, 98);
            this.btnBrowseDealer.Name = "btnBrowseDealer";
            this.btnBrowseDealer.Size = new System.Drawing.Size(25, 25);
            this.btnBrowseDealer.TabIndex = 64;
            this.btnBrowseDealer.TabStop = false;
            this.btnBrowseDealer.UseVisualStyleBackColor = true;
            this.btnBrowseDealer.Click += new System.EventHandler(this.btnBrowseDealer_Click);
            // 
            // lblDealer_Compnam
            // 
            this.lblDealer_Compnam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDealer_Compnam.Location = new System.Drawing.Point(271, 101);
            this.lblDealer_Compnam.Name = "lblDealer_Compnam";
            this.lblDealer_Compnam.Size = new System.Drawing.Size(244, 16);
            this.lblDealer_Compnam.TabIndex = 63;
            this.lblDealer_Compnam.Text = "DEALER->COMPNAM";
            // 
            // txtDealer
            // 
            this.txtDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDealer.Location = new System.Drawing.Point(129, 99);
            this.txtDealer.MaxLength = 20;
            this.txtDealer.Name = "txtDealer";
            this.txtDealer.Size = new System.Drawing.Size(120, 23);
            this.txtDealer.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(76, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "Dealer";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(272, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(186, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(129, 73);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 2;
            // 
            // mskOldSernum
            // 
            this.mskOldSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskOldSernum.Enabled = false;
            this.mskOldSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskOldSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskOldSernum.Location = new System.Drawing.Point(129, 12);
            this.mskOldSernum.Mask = ">A-AAA-AAAAAA";
            this.mskOldSernum.Name = "mskOldSernum";
            this.mskOldSernum.PromptChar = ' ';
            this.mskOldSernum.Size = new System.Drawing.Size(105, 23);
            this.mskOldSernum.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(70, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(69, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "Old S/N";
            // 
            // msk2Sernum
            // 
            this.msk2Sernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.msk2Sernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.msk2Sernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.msk2Sernum.Location = new System.Drawing.Point(129, 38);
            this.msk2Sernum.Mask = ">A-AAA-AAAAAA";
            this.msk2Sernum.Name = "msk2Sernum";
            this.msk2Sernum.PromptChar = ' ';
            this.msk2Sernum.Size = new System.Drawing.Size(105, 23);
            this.msk2Sernum.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(74, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 66;
            this.label2.Text = "#2 No.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProcess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 169);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(533, 22);
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProcess
            // 
            this.toolStripProcess.Image = global::SN_Net.Properties.Resources.process_bar;
            this.toolStripProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripProcess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcess.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcess.Name = "toolStripProcess";
            this.toolStripProcess.Size = new System.Drawing.Size(508, 17);
            this.toolStripProcess.Spring = true;
            this.toolStripProcess.Visible = false;
            // 
            // SellProgram2nd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 191);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.msk2Sernum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseDealer);
            this.Controls.Add(this.lblDealer_Compnam);
            this.Controls.Add(this.txtDealer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.mskOldSernum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SellProgram2nd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sell Program #2nd";
            this.Load += new System.EventHandler(this.SellProgram2nd_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseDealer;
        private System.Windows.Forms.Label lblDealer_Compnam;
        private System.Windows.Forms.TextBox txtDealer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.MaskedTextBox mskOldSernum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox msk2Sernum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProcess;
    }
}