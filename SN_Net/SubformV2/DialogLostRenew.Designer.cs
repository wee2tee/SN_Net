namespace SN_Net.Subform
{
    partial class DialogLostRenew
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
            this.chkNewRwt = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.mskNewSernum = new System.Windows.Forms.MaskedTextBox();
            this.mskLostSernum = new System.Windows.Forms.MaskedTextBox();
            this.chkCDTraining = new System.Windows.Forms.CheckBox();
            this.chkNewRwtJob = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkNewRwt
            // 
            this.chkNewRwt.AutoSize = true;
            this.chkNewRwt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwt.Location = new System.Drawing.Point(179, 77);
            this.chkNewRwt.Name = "chkNewRwt";
            this.chkNewRwt.Size = new System.Drawing.Size(148, 20);
            this.chkNewRwt.TabIndex = 3;
            this.chkNewRwt.Text = "New RWT (Pink Disc)";
            this.chkNewRwt.UseVisualStyleBackColor = true;
            this.chkNewRwt.CheckedChanged += new System.EventHandler(this.chkNewRwt_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(199, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(113, 167);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(123, 74);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // mskNewSernum
            // 
            this.mskNewSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskNewSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskNewSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskNewSernum.Location = new System.Drawing.Point(123, 45);
            this.mskNewSernum.Mask = ">A-AAA-AAAAAA";
            this.mskNewSernum.Name = "mskNewSernum";
            this.mskNewSernum.PromptChar = ' ';
            this.mskNewSernum.Size = new System.Drawing.Size(105, 23);
            this.mskNewSernum.TabIndex = 1;
            this.mskNewSernum.Leave += new System.EventHandler(this.mskNewSernum_Leave);
            // 
            // mskLostSernum
            // 
            this.mskLostSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskLostSernum.Enabled = false;
            this.mskLostSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskLostSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskLostSernum.Location = new System.Drawing.Point(123, 16);
            this.mskLostSernum.Mask = ">A-AAA-AAAAAA";
            this.mskLostSernum.Name = "mskLostSernum";
            this.mskLostSernum.PromptChar = ' ';
            this.mskLostSernum.Size = new System.Drawing.Size(105, 23);
            this.mskLostSernum.TabIndex = 0;
            // 
            // chkCDTraining
            // 
            this.chkCDTraining.AutoSize = true;
            this.chkCDTraining.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkCDTraining.Location = new System.Drawing.Point(179, 129);
            this.chkCDTraining.Name = "chkCDTraining";
            this.chkCDTraining.Size = new System.Drawing.Size(169, 20);
            this.chkCDTraining.TabIndex = 5;
            this.chkCDTraining.Text = "CD Training (Green Disc)";
            this.chkCDTraining.UseVisualStyleBackColor = true;
            this.chkCDTraining.CheckedChanged += new System.EventHandler(this.chkCDTraining_CheckedChanged);
            // 
            // chkNewRwtJob
            // 
            this.chkNewRwtJob.AutoSize = true;
            this.chkNewRwtJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwtJob.Location = new System.Drawing.Point(179, 103);
            this.chkNewRwtJob.Name = "chkNewRwtJob";
            this.chkNewRwtJob.Size = new System.Drawing.Size(183, 20);
            this.chkNewRwtJob.TabIndex = 4;
            this.chkNewRwtJob.Text = "New RWT + Job (Pink Disc)";
            this.chkNewRwtJob.UseVisualStyleBackColor = true;
            this.chkNewRwtJob.CheckedChanged += new System.EventHandler(this.chkNewRwtJob_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(55, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(32, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ReNew S/N";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(48, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Lost S/N";
            // 
            // DialogLostRenew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 214);
            this.Controls.Add(this.chkNewRwt);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.mskNewSernum);
            this.Controls.Add(this.mskLostSernum);
            this.Controls.Add(this.chkCDTraining);
            this.Controls.Add(this.chkNewRwtJob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogLostRenew";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lost + Renew";
            this.Load += new System.EventHandler(this.DialogLostRenew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkNewRwt;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.MaskedTextBox mskNewSernum;
        private System.Windows.Forms.MaskedTextBox mskLostSernum;
        private System.Windows.Forms.CheckBox chkCDTraining;
        private System.Windows.Forms.CheckBox chkNewRwtJob;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}