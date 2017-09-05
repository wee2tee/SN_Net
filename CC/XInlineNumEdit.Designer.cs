namespace CC
{
    partial class XInlineNumEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.xNumEdit1 = new CC.XNumEdit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(1, 27);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(50, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(69, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // xNumEdit1
            // 
            this.xNumEdit1._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xNumEdit1._DecimalDigit = 2;
            this.xNumEdit1._MaximumValue = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            262144});
            this.xNumEdit1._MaxLength = 30;
            this.xNumEdit1._ReadOnly = false;
            this.xNumEdit1._SelectionLength = 0;
            this.xNumEdit1._SelectionStart = 4;
            this.xNumEdit1._Text = "0.00";
            this.xNumEdit1._TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.xNumEdit1._UseThoundsandSeparate = true;
            this.xNumEdit1._Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.xNumEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xNumEdit1.BackColor = System.Drawing.Color.White;
            this.xNumEdit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xNumEdit1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.xNumEdit1.Location = new System.Drawing.Point(2, 2);
            this.xNumEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xNumEdit1.Name = "xNumEdit1";
            this.xNumEdit1.Size = new System.Drawing.Size(116, 23);
            this.xNumEdit1.TabIndex = 0;
            // 
            // XInlineNumEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.xNumEdit1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XInlineNumEdit";
            this.Size = new System.Drawing.Size(120, 51);
            this.ResumeLayout(false);

        }

        #endregion

        private XNumEdit xNumEdit1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
