namespace SN_Net.Subform
{
    partial class LostRenewForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkNewRwtJob = new System.Windows.Forms.CheckBox();
            this.chkCDTraining = new System.Windows.Forms.CheckBox();
            this.mskLostSernum = new System.Windows.Forms.MaskedTextBox();
            this.mskNewSernum = new System.Windows.Forms.MaskedTextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkNewRwt = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(37, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lost S/N";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(21, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "ReNew S/N";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(44, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Version";
            // 
            // chkNewRwtJob
            // 
            this.chkNewRwtJob.AutoSize = true;
            this.chkNewRwtJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwtJob.Location = new System.Drawing.Point(168, 94);
            this.chkNewRwtJob.Name = "chkNewRwtJob";
            this.chkNewRwtJob.Size = new System.Drawing.Size(183, 20);
            this.chkNewRwtJob.TabIndex = 3;
            this.chkNewRwtJob.Text = "New RWT + Job (Pink Disc)";
            this.chkNewRwtJob.UseVisualStyleBackColor = true;
            // 
            // chkCDTraining
            // 
            this.chkCDTraining.AutoSize = true;
            this.chkCDTraining.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkCDTraining.Location = new System.Drawing.Point(168, 120);
            this.chkCDTraining.Name = "chkCDTraining";
            this.chkCDTraining.Size = new System.Drawing.Size(169, 20);
            this.chkCDTraining.TabIndex = 4;
            this.chkCDTraining.Text = "CD Training (Green Disc)";
            this.chkCDTraining.UseVisualStyleBackColor = true;
            // 
            // mskLostSernum
            // 
            this.mskLostSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskLostSernum.Enabled = false;
            this.mskLostSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskLostSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskLostSernum.Location = new System.Drawing.Point(112, 7);
            this.mskLostSernum.Mask = ">A-AAA-AAAAAA";
            this.mskLostSernum.Name = "mskLostSernum";
            this.mskLostSernum.PromptChar = ' ';
            this.mskLostSernum.Size = new System.Drawing.Size(105, 23);
            this.mskLostSernum.TabIndex = 6;
            // 
            // mskNewSernum
            // 
            this.mskNewSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskNewSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskNewSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskNewSernum.Location = new System.Drawing.Point(112, 36);
            this.mskNewSernum.Mask = ">A-AAA-AAAAAA";
            this.mskNewSernum.Name = "mskNewSernum";
            this.mskNewSernum.PromptChar = ' ';
            this.mskNewSernum.Size = new System.Drawing.Size(105, 23);
            this.mskNewSernum.TabIndex = 0;
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(112, 65);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(132, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(218, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProcess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 194);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(434, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProcess
            // 
            this.toolStripProcess.Image = global::SN_Net.Properties.Resources.process_bar;
            this.toolStripProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripProcess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcess.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcess.Name = "toolStripProcess";
            this.toolStripProcess.Size = new System.Drawing.Size(409, 17);
            this.toolStripProcess.Spring = true;
            this.toolStripProcess.Visible = false;
            // 
            // chkNewRwt
            // 
            this.chkNewRwt.AutoSize = true;
            this.chkNewRwt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwt.Location = new System.Drawing.Point(168, 68);
            this.chkNewRwt.Name = "chkNewRwt";
            this.chkNewRwt.Size = new System.Drawing.Size(148, 20);
            this.chkNewRwt.TabIndex = 2;
            this.chkNewRwt.Text = "New RWT (Pink Disc)";
            this.chkNewRwt.UseVisualStyleBackColor = true;
            // 
            // LostRenewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 216);
            this.Controls.Add(this.chkNewRwt);
            this.Controls.Add(this.statusStrip1);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LostRenewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lost + Renew";
            this.Load += new System.EventHandler(this.LostRenewForm_Load);
            this.Shown += new System.EventHandler(this.LostRenewForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkNewRwtJob;
        private System.Windows.Forms.CheckBox chkCDTraining;
        private System.Windows.Forms.MaskedTextBox mskLostSernum;
        private System.Windows.Forms.MaskedTextBox mskNewSernum;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProcess;
        private System.Windows.Forms.CheckBox chkNewRwt;
    }
}