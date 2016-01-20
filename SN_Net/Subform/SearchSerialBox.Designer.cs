namespace SN_Net.Subform
{
    partial class SearchSerialBox
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
            this.mskSearchKey = new System.Windows.Forms.MaskedTextBox();
            this.lblSearchKey = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearchKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mskSearchKey
            // 
            this.mskSearchKey.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSearchKey.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSearchKey.Location = new System.Drawing.Point(120, 13);
            this.mskSearchKey.Mask = ">A-AAA-AAAAAA";
            this.mskSearchKey.Name = "mskSearchKey";
            this.mskSearchKey.PromptChar = ' ';
            this.mskSearchKey.Size = new System.Drawing.Size(155, 22);
            this.mskSearchKey.TabIndex = 76;
            // 
            // lblSearchKey
            // 
            this.lblSearchKey.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblSearchKey.Location = new System.Drawing.Point(4, 15);
            this.lblSearchKey.Name = "lblSearchKey";
            this.lblSearchKey.Size = new System.Drawing.Size(104, 16);
            this.lblSearchKey.TabIndex = 77;
            this.lblSearchKey.Text = "Serial No.";
            this.lblSearchKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(123, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 78;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearchKey
            // 
            this.txtSearchKey.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSearchKey.Location = new System.Drawing.Point(119, 18);
            this.txtSearchKey.Name = "txtSearchKey";
            this.txtSearchKey.Size = new System.Drawing.Size(192, 23);
            this.txtSearchKey.TabIndex = 79;
            // 
            // SearchSerialBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 78);
            this.Controls.Add(this.mskSearchKey);
            this.Controls.Add(this.txtSearchKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblSearchKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchSerialBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Search Data";
            this.Shown += new System.EventHandler(this.SearchSerialBox_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearchKey;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.MaskedTextBox mskSearchKey;
        public System.Windows.Forms.TextBox txtSearchKey;

    }
}