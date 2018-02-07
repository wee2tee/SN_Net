namespace SN_Net.Subform
{
    partial class DialogAbsentReportScope
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
            this.label4 = new System.Windows.Forms.Label();
            this.cbUserFrom = new System.Windows.Forms.ComboBox();
            this.cbUserTo = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtDateFrom = new CC.XDatePicker();
            this.dtDateTo = new CC.XDatePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "พนักงาน";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ถึง";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "วันที่ จาก";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "ถึง";
            // 
            // cbUserFrom
            // 
            this.cbUserFrom.FormattingEnabled = true;
            this.cbUserFrom.Location = new System.Drawing.Point(113, 23);
            this.cbUserFrom.Name = "cbUserFrom";
            this.cbUserFrom.Size = new System.Drawing.Size(103, 24);
            this.cbUserFrom.TabIndex = 0;
            this.cbUserFrom.SelectedIndexChanged += new System.EventHandler(this.cbUserFrom_SelectedIndexChanged);
            this.cbUserFrom.Leave += new System.EventHandler(this.cbUserFrom_Leave);
            // 
            // cbUserTo
            // 
            this.cbUserTo.FormattingEnabled = true;
            this.cbUserTo.Location = new System.Drawing.Point(262, 23);
            this.cbUserTo.Name = "cbUserTo";
            this.cbUserTo.Size = new System.Drawing.Size(103, 24);
            this.cbUserTo.TabIndex = 1;
            this.cbUserTo.Visible = false;
            this.cbUserTo.SelectedIndexChanged += new System.EventHandler(this.cbUserTo_SelectedIndexChanged);
            this.cbUserTo.Leave += new System.EventHandler(this.cbUserTo_Leave);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(122, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 33);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(213, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dtDateFrom
            // 
            this.dtDateFrom._ReadOnly = false;
            this.dtDateFrom._SelectedDate = null;
            this.dtDateFrom.BackColor = System.Drawing.Color.White;
            this.dtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDateFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateFrom.Location = new System.Drawing.Point(113, 54);
            this.dtDateFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Size = new System.Drawing.Size(103, 23);
            this.dtDateFrom.TabIndex = 2;
            this.dtDateFrom._SelectedDateChanged += new System.EventHandler(this.dtDateFrom_SelectedDateChanged);
            this.dtDateFrom._GotFocus += new System.EventHandler(this.dtDate__GotFocus);
            // 
            // dtDateTo
            // 
            this.dtDateTo._ReadOnly = false;
            this.dtDateTo._SelectedDate = null;
            this.dtDateTo.BackColor = System.Drawing.Color.White;
            this.dtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtDateTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateTo.Location = new System.Drawing.Point(262, 54);
            this.dtDateTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(103, 23);
            this.dtDateTo.TabIndex = 3;
            this.dtDateTo._SelectedDateChanged += new System.EventHandler(this.dtDateTo_SelectedDateChanged);
            this.dtDateTo._GotFocus += new System.EventHandler(this.dtDate__GotFocus);
            // 
            // DialogAbsentReportScope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 152);
            this.Controls.Add(this.dtDateTo);
            this.Controls.Add(this.dtDateFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbUserTo);
            this.Controls.Add(this.cbUserFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogAbsentReportScope";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "กำหนดขอบเขตการแสดงข้อมูล";
            this.Load += new System.EventHandler(this.DialogAbsentReportScope_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUserFrom;
        private System.Windows.Forms.ComboBox cbUserTo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private CC.XDatePicker dtDateFrom;
        private CC.XDatePicker dtDateTo;
    }
}