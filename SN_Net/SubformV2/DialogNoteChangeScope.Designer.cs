namespace SN_Net.Subform
{
    partial class DialogNoteChangeScope
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
            this.dtDate = new CC.XDatePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.drUser = new CC.XDropdownList();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtDateTo = new CC.XDatePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "พนักงาน";
            // 
            // dtDate
            // 
            this.dtDate._ReadOnly = false;
            this.dtDate._SelectedDate = null;
            this.dtDate.BackColor = System.Drawing.Color.White;
            this.dtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDate.Location = new System.Drawing.Point(119, 52);
            this.dtDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(103, 23);
            this.dtDate.TabIndex = 1;
            this.dtDate._SelectedDateChanged += new System.EventHandler(this.dtDate__SelectedDateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "วันที่";
            // 
            // drUser
            // 
            this.drUser._DroppedDown = false;
            this.drUser._ReadOnly = false;
            this.drUser._SelectedItem = null;
            this.drUser._Text = "";
            this.drUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drUser.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.drUser.Location = new System.Drawing.Point(119, 24);
            this.drUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.drUser.Name = "drUser";
            this.drUser.Size = new System.Drawing.Size(182, 23);
            this.drUser.TabIndex = 0;
            this.drUser._SelectedItemChanged += new System.EventHandler(this.drUser__SelectedItemChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(106, 98);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 33);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(192, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 33);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dtDateTo
            // 
            this.dtDateTo._ReadOnly = false;
            this.dtDateTo._SelectedDate = null;
            this.dtDateTo.BackColor = System.Drawing.Color.White;
            this.dtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDateTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateTo.Location = new System.Drawing.Point(239, 52);
            this.dtDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(103, 23);
            this.dtDateTo.TabIndex = 2;
            this.dtDateTo._SelectedDateChanged += new System.EventHandler(this.dtDateTo__SelectedDateChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(225, 55);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(13, 16);
            this.lblTo.TabIndex = 0;
            this.lblTo.Text = "-";
            // 
            // DialogNoteChangeScope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 152);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.drUser);
            this.Controls.Add(this.dtDateTo);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNoteChangeScope";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Scope";
            this.Load += new System.EventHandler(this.DialogNoteChangeScope_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CC.XDatePicker dtDate;
        private System.Windows.Forms.Label label2;
        private CC.XDropdownList drUser;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private CC.XDatePicker dtDateTo;
        private System.Windows.Forms.Label lblTo;
    }
}