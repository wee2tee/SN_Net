namespace SN_Net.Subform
{
    partial class DialogUpNewRwt
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
            this.chkPinkdisc = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkGreendisc = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkPinkdisc
            // 
            this.chkPinkdisc.AutoSize = true;
            this.chkPinkdisc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkPinkdisc.Location = new System.Drawing.Point(80, 47);
            this.chkPinkdisc.Name = "chkPinkdisc";
            this.chkPinkdisc.Size = new System.Drawing.Size(158, 20);
            this.chkPinkdisc.TabIndex = 1;
            this.chkPinkdisc.Text = "CD Training (Pink Disc)";
            this.chkPinkdisc.UseVisualStyleBackColor = true;
            this.chkPinkdisc.CheckedChanged += new System.EventHandler(this.chkPinkdisc_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(172, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(86, 82);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkGreendisc
            // 
            this.chkGreendisc.AutoSize = true;
            this.chkGreendisc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkGreendisc.Location = new System.Drawing.Point(80, 21);
            this.chkGreendisc.Name = "chkGreendisc";
            this.chkGreendisc.Size = new System.Drawing.Size(169, 20);
            this.chkGreendisc.TabIndex = 0;
            this.chkGreendisc.Text = "CD Training (Green Disc)";
            this.chkGreendisc.UseVisualStyleBackColor = true;
            this.chkGreendisc.CheckedChanged += new System.EventHandler(this.chkGreendisc_CheckedChanged);
            // 
            // DialogUpNewRwt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 131);
            this.Controls.Add(this.chkPinkdisc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkGreendisc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogUpNewRwt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gen \'Up New Rwt\' line";
            this.Load += new System.EventHandler(this.DialogUpNewRwt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPinkdisc;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkGreendisc;
    }
}