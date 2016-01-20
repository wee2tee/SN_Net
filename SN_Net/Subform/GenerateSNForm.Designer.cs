namespace SN_Net.Subform
{
    partial class GenerateSNForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkNewRwt = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.mskSernum = new System.Windows.Forms.MaskedTextBox();
            this.chkCDTraining = new System.Windows.Forms.CheckBox();
            this.chkNewRwtJob = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDealer = new System.Windows.Forms.TextBox();
            this.lblDealer_Compnam = new System.Windows.Forms.Label();
            this.btnBrowseDealer = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProcess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 232);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(493, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProcess
            // 
            this.toolStripProcess.Image = global::SN_Net.Properties.Resources.process_bar;
            this.toolStripProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripProcess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcess.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcess.Name = "toolStripProcess";
            this.toolStripProcess.Size = new System.Drawing.Size(468, 17);
            this.toolStripProcess.Spring = true;
            this.toolStripProcess.Visible = false;
            // 
            // chkNewRwt
            // 
            this.chkNewRwt.AutoSize = true;
            this.chkNewRwt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwt.Location = new System.Drawing.Point(156, 74);
            this.chkNewRwt.Name = "chkNewRwt";
            this.chkNewRwt.Size = new System.Drawing.Size(176, 20);
            this.chkNewRwt.TabIndex = 4;
            this.chkNewRwt.Text = "New RWT        (Pink Disc)";
            this.chkNewRwt.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(251, 199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(165, 199);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(96, 71);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 3;
            // 
            // mskSernum
            // 
            this.mskSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernum.Location = new System.Drawing.Point(96, 10);
            this.mskSernum.Mask = ">A-AAA-AAAAAA";
            this.mskSernum.Name = "mskSernum";
            this.mskSernum.PromptChar = ' ';
            this.mskSernum.Size = new System.Drawing.Size(105, 23);
            this.mskSernum.TabIndex = 1;
            // 
            // chkCDTraining
            // 
            this.chkCDTraining.AutoSize = true;
            this.chkCDTraining.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkCDTraining.Location = new System.Drawing.Point(96, 159);
            this.chkCDTraining.Name = "chkCDTraining";
            this.chkCDTraining.Size = new System.Drawing.Size(94, 20);
            this.chkCDTraining.TabIndex = 7;
            this.chkCDTraining.Text = "CD Training";
            this.chkCDTraining.UseVisualStyleBackColor = true;
            // 
            // chkNewRwtJob
            // 
            this.chkNewRwtJob.AutoSize = true;
            this.chkNewRwtJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwtJob.Location = new System.Drawing.Point(156, 100);
            this.chkNewRwtJob.Name = "chkNewRwtJob";
            this.chkNewRwtJob.Size = new System.Drawing.Size(175, 20);
            this.chkNewRwtJob.TabIndex = 5;
            this.chkNewRwtJob.Text = "New RWT+Job (Pink Disc)";
            this.chkNewRwtJob.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(28, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(54, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Qty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(53, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "S/N";
            // 
            // numQty
            // 
            this.numQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numQty.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numQty.Location = new System.Drawing.Point(96, 36);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(48, 23);
            this.numQty.TabIndex = 2;
            this.numQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(35, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Dealer";
            // 
            // txtDealer
            // 
            this.txtDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDealer.Location = new System.Drawing.Point(96, 131);
            this.txtDealer.MaxLength = 20;
            this.txtDealer.Name = "txtDealer";
            this.txtDealer.Size = new System.Drawing.Size(120, 23);
            this.txtDealer.TabIndex = 6;
            // 
            // lblDealer_Compnam
            // 
            this.lblDealer_Compnam.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDealer_Compnam.Location = new System.Drawing.Point(238, 133);
            this.lblDealer_Compnam.Name = "lblDealer_Compnam";
            this.lblDealer_Compnam.Size = new System.Drawing.Size(244, 16);
            this.lblDealer_Compnam.TabIndex = 20;
            this.lblDealer_Compnam.Text = "DEALER->COMPNAM";
            // 
            // btnBrowseDealer
            // 
            this.btnBrowseDealer.Image = global::SN_Net.Properties.Resources.zoom;
            this.btnBrowseDealer.Location = new System.Drawing.Point(215, 130);
            this.btnBrowseDealer.Name = "btnBrowseDealer";
            this.btnBrowseDealer.Size = new System.Drawing.Size(25, 25);
            this.btnBrowseDealer.TabIndex = 54;
            this.btnBrowseDealer.TabStop = false;
            this.btnBrowseDealer.UseVisualStyleBackColor = true;
            this.btnBrowseDealer.Click += new System.EventHandler(this.btnBrowseDealer_Click);
            // 
            // GenerateSNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 254);
            this.Controls.Add(this.btnBrowseDealer);
            this.Controls.Add(this.lblDealer_Compnam);
            this.Controls.Add(this.txtDealer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numQty);
            this.Controls.Add(this.chkNewRwt);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.mskSernum);
            this.Controls.Add(this.chkCDTraining);
            this.Controls.Add(this.chkNewRwtJob);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateSNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate S/N";
            this.Shown += new System.EventHandler(this.GenerateSNForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProcess;
        private System.Windows.Forms.CheckBox chkNewRwt;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.MaskedTextBox mskSernum;
        private System.Windows.Forms.CheckBox chkCDTraining;
        private System.Windows.Forms.CheckBox chkNewRwtJob;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDealer;
        private System.Windows.Forms.Label lblDealer_Compnam;
        private System.Windows.Forms.Button btnBrowseDealer;
    }
}