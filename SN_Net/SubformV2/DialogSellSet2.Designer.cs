namespace SN_Net.Subform
{
    partial class DialogSellSet2
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
            this.mskSernum2 = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDealer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.mskSernum1 = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.brDealer = new CC.XBrowseBox();
            this.SuspendLayout();
            // 
            // mskSernum2
            // 
            this.mskSernum2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernum2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernum2.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernum2.Location = new System.Drawing.Point(80, 45);
            this.mskSernum2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mskSernum2.Mask = ">A-AAA-AAAAAA";
            this.mskSernum2.Name = "mskSernum2";
            this.mskSernum2.PromptChar = ' ';
            this.mskSernum2.Size = new System.Drawing.Size(122, 23);
            this.mskSernum2.TabIndex = 1;
            this.mskSernum2.Leave += new System.EventHandler(this.mskSernum2_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(16, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 78;
            this.label2.Text = "#2 No.";
            // 
            // lblDealer
            // 
            this.lblDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDealer.Location = new System.Drawing.Point(202, 110);
            this.lblDealer.Name = "lblDealer";
            this.lblDealer.Size = new System.Drawing.Size(291, 20);
            this.lblDealer.TabIndex = 76;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(18, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 75;
            this.label4.Text = "Dealer";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(253, 148);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(172, 148);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(80, 81);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(46, 23);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // mskSernum1
            // 
            this.mskSernum1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernum1.Enabled = false;
            this.mskSernum1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernum1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernum1.Location = new System.Drawing.Point(80, 19);
            this.mskSernum1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mskSernum1.Mask = ">A-AAA-AAAAAA";
            this.mskSernum1.Name = "mskSernum1";
            this.mskSernum1.PromptChar = ' ';
            this.mskSernum1.Size = new System.Drawing.Size(122, 23);
            this.mskSernum1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(11, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 74;
            this.label3.Text = "Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 73;
            this.label1.Text = "Old S/N";
            // 
            // brDealer
            // 
            this.brDealer._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.brDealer._ReadOnly = false;
            this.brDealer._Text = "";
            this.brDealer._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brDealer._UseImage = true;
            this.brDealer.BackColor = System.Drawing.Color.White;
            this.brDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brDealer.Location = new System.Drawing.Point(80, 107);
            this.brDealer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brDealer.Name = "brDealer";
            this.brDealer.Size = new System.Drawing.Size(118, 23);
            this.brDealer.TabIndex = 3;
            this.brDealer._ButtonClick += new System.EventHandler(this.brDealer__ButtonClick);
            this.brDealer._Leave += new System.EventHandler(this.brDealer__Leave);
            // 
            // DialogSellSet2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 196);
            this.Controls.Add(this.brDealer);
            this.Controls.Add(this.mskSernum2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDealer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.mskSernum1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogSellSet2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sell Program #2nd";
            this.Load += new System.EventHandler(this.DialogSellSet2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mskSernum2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDealer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.MaskedTextBox mskSernum1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private CC.XBrowseBox brDealer;
    }
}