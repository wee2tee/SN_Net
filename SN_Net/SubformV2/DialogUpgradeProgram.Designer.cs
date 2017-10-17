namespace SN_Net.Subform
{
    partial class DialogUpgradeProgram
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
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.mskSernumTo = new System.Windows.Forms.MaskedTextBox();
            this.mskSernumFrom = new System.Windows.Forms.MaskedTextBox();
            this.chkCDTraining = new System.Windows.Forms.CheckBox();
            this.chkNewRwtJob = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkNewRwt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(199, 163);
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
            this.btnOK.Location = new System.Drawing.Point(113, 163);
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
            this.txtVersion.Location = new System.Drawing.Point(103, 74);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // mskSernumTo
            // 
            this.mskSernumTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernumTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernumTo.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernumTo.Location = new System.Drawing.Point(103, 47);
            this.mskSernumTo.Mask = ">A-AAA-AAAAAA";
            this.mskSernumTo.Name = "mskSernumTo";
            this.mskSernumTo.PromptChar = ' ';
            this.mskSernumTo.Size = new System.Drawing.Size(105, 23);
            this.mskSernumTo.TabIndex = 1;
            this.mskSernumTo.Leave += new System.EventHandler(this.mskSernumTo_Leave);
            // 
            // mskSernumFrom
            // 
            this.mskSernumFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernumFrom.Enabled = false;
            this.mskSernumFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernumFrom.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernumFrom.Location = new System.Drawing.Point(103, 20);
            this.mskSernumFrom.Mask = ">A-AAA-AAAAAA";
            this.mskSernumFrom.Name = "mskSernumFrom";
            this.mskSernumFrom.PromptChar = ' ';
            this.mskSernumFrom.Size = new System.Drawing.Size(105, 23);
            this.mskSernumFrom.TabIndex = 0;
            // 
            // chkCDTraining
            // 
            this.chkCDTraining.AutoSize = true;
            this.chkCDTraining.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkCDTraining.Location = new System.Drawing.Point(156, 130);
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
            this.chkNewRwtJob.Location = new System.Drawing.Point(156, 104);
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
            this.label3.Location = new System.Drawing.Point(41, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(69, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(54, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "From";
            // 
            // chkNewRwt
            // 
            this.chkNewRwt.AutoSize = true;
            this.chkNewRwt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwt.Location = new System.Drawing.Point(156, 78);
            this.chkNewRwt.Name = "chkNewRwt";
            this.chkNewRwt.Size = new System.Drawing.Size(148, 20);
            this.chkNewRwt.TabIndex = 3;
            this.chkNewRwt.Text = "New RWT (Pink Disc)";
            this.chkNewRwt.UseVisualStyleBackColor = true;
            this.chkNewRwt.CheckedChanged += new System.EventHandler(this.chkNewRwt_CheckedChanged);
            // 
            // DialogUpgradeProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 210);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.mskSernumTo);
            this.Controls.Add(this.mskSernumFrom);
            this.Controls.Add(this.chkCDTraining);
            this.Controls.Add(this.chkNewRwtJob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkNewRwt);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogUpgradeProgram";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upgrade Program";
            this.Load += new System.EventHandler(this.DialogUpgradeProgram_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.MaskedTextBox mskSernumTo;
        private System.Windows.Forms.MaskedTextBox mskSernumFrom;
        private System.Windows.Forms.CheckBox chkCDTraining;
        private System.Windows.Forms.CheckBox chkNewRwtJob;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkNewRwt;
    }
}