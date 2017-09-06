namespace SN_Net.Subform
{
    partial class DialogSimpleSearch
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
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mskKeyword = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // lblKey
            // 
            this.lblKey.Location = new System.Drawing.Point(12, 38);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(124, 20);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "ค้นหา";
            this.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(142, 37);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(194, 23);
            this.txtKeyword.TabIndex = 0;
            this.txtKeyword.TextChanged += new System.EventHandler(this.txtKeyword_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(108, 82);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(189, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // mskKeyword
            // 
            this.mskKeyword.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskKeyword.Location = new System.Drawing.Point(142, 37);
            this.mskKeyword.Mask = ">A-AAA-AAAAAA";
            this.mskKeyword.Name = "mskKeyword";
            this.mskKeyword.PromptChar = ' ';
            this.mskKeyword.Size = new System.Drawing.Size(139, 23);
            this.mskKeyword.TabIndex = 3;
            this.mskKeyword.TextChanged += new System.EventHandler(this.mskKeyword_TextChanged);
            // 
            // DialogSimpleSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 128);
            this.Controls.Add(this.mskKeyword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKey);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogSimpleSearch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ป้อนข้อมูลที่ต้องการค้นหา";
            this.Load += new System.EventHandler(this.DialogSimpleSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.MaskedTextBox mskKeyword;
    }
}