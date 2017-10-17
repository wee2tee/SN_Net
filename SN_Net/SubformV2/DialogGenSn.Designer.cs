namespace SN_Net.Subform
{
    partial class DialogGenSn
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
            this.lblDealer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
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
            this.brDealer = new CC.XBrowseBox();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDealer
            // 
            this.lblDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDealer.Location = new System.Drawing.Point(205, 138);
            this.lblDealer.Name = "lblDealer";
            this.lblDealer.Size = new System.Drawing.Size(287, 16);
            this.lblDealer.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(29, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 67;
            this.label4.Text = "Dealer";
            // 
            // numQty
            // 
            this.numQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numQty.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numQty.Location = new System.Drawing.Point(80, 41);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(48, 23);
            this.numQty.TabIndex = 1;
            this.numQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.ValueChanged += new System.EventHandler(this.numQty_ValueChanged);
            // 
            // chkNewRwt
            // 
            this.chkNewRwt.AutoSize = true;
            this.chkNewRwt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwt.Location = new System.Drawing.Point(155, 79);
            this.chkNewRwt.Name = "chkNewRwt";
            this.chkNewRwt.Size = new System.Drawing.Size(176, 20);
            this.chkNewRwt.TabIndex = 3;
            this.chkNewRwt.Text = "New RWT        (Pink Disc)";
            this.chkNewRwt.UseVisualStyleBackColor = true;
            this.chkNewRwt.CheckedChanged += new System.EventHandler(this.chkNewRwt_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(256, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(170, 203);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 29);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtVersion.Location = new System.Drawing.Point(80, 76);
            this.txtVersion.MaxLength = 4;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(40, 23);
            this.txtVersion.TabIndex = 2;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // mskSernum
            // 
            this.mskSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskSernum.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mskSernum.Location = new System.Drawing.Point(80, 15);
            this.mskSernum.Mask = ">A-AAA-AAAAAA";
            this.mskSernum.Name = "mskSernum";
            this.mskSernum.PromptChar = ' ';
            this.mskSernum.Size = new System.Drawing.Size(105, 23);
            this.mskSernum.TabIndex = 0;
            this.mskSernum.Leave += new System.EventHandler(this.mskSernum_Leave);
            // 
            // chkCDTraining
            // 
            this.chkCDTraining.AutoSize = true;
            this.chkCDTraining.Checked = true;
            this.chkCDTraining.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCDTraining.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkCDTraining.Location = new System.Drawing.Point(80, 164);
            this.chkCDTraining.Name = "chkCDTraining";
            this.chkCDTraining.Size = new System.Drawing.Size(94, 20);
            this.chkCDTraining.TabIndex = 6;
            this.chkCDTraining.Text = "CD Training";
            this.chkCDTraining.UseVisualStyleBackColor = true;
            this.chkCDTraining.CheckedChanged += new System.EventHandler(this.chkCDTraining_CheckedChanged);
            // 
            // chkNewRwtJob
            // 
            this.chkNewRwtJob.AutoSize = true;
            this.chkNewRwtJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkNewRwtJob.Location = new System.Drawing.Point(155, 105);
            this.chkNewRwtJob.Name = "chkNewRwtJob";
            this.chkNewRwtJob.Size = new System.Drawing.Size(175, 20);
            this.chkNewRwtJob.TabIndex = 4;
            this.chkNewRwtJob.Text = "New RWT+Job (Pink Disc)";
            this.chkNewRwtJob.UseVisualStyleBackColor = true;
            this.chkNewRwtJob.CheckedChanged += new System.EventHandler(this.chkNewRwtJob_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(22, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(48, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "Qty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(47, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 64;
            this.label1.Text = "S/N";
            // 
            // brDealer
            // 
            this.brDealer._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer._CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.brDealer._ReadOnly = false;
            this.brDealer._Text = "";
            this.brDealer._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brDealer._UseImage = true;
            this.brDealer.BackColor = System.Drawing.Color.White;
            this.brDealer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brDealer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brDealer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brDealer.Location = new System.Drawing.Point(80, 134);
            this.brDealer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brDealer.Name = "brDealer";
            this.brDealer.Size = new System.Drawing.Size(121, 23);
            this.brDealer.TabIndex = 5;
            this.brDealer._ButtonClick += new System.EventHandler(this.brDealer__ButtonClick);
            this.brDealer._Leave += new System.EventHandler(this.brDealer__Leave);
            // 
            // DialogGenSn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 251);
            this.Controls.Add(this.brDealer);
            this.Controls.Add(this.lblDealer);
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
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogGenSn";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate S/N";
            this.Load += new System.EventHandler(this.DialogGenSn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDealer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numQty;
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
        private CC.XBrowseBox brDealer;
    }
}