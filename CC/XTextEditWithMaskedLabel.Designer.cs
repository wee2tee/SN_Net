namespace CC
{
    partial class XTextEditWithMaskedLabel
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
            this.txtTextEditable = new System.Windows.Forms.TextBox();
            this.lblTextPrefix = new System.Windows.Forms.Label();
            this.lblTextAll = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTextEditable
            // 
            this.txtTextEditable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextEditable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTextEditable.Location = new System.Drawing.Point(2, 2);
            this.txtTextEditable.Name = "txtTextEditable";
            this.txtTextEditable.Size = new System.Drawing.Size(243, 16);
            this.txtTextEditable.TabIndex = 1;
            this.txtTextEditable.Text = "TextBox";
            this.txtTextEditable.BackColorChanged += new System.EventHandler(this.txtTextEditable_BackColorChanged);
            this.txtTextEditable.TextChanged += new System.EventHandler(this.txtTextEditable_TextChanged);
            this.txtTextEditable.VisibleChanged += new System.EventHandler(this.txtTextEditable_VisibleChanged);
            // 
            // lblTextPrefix
            // 
            this.lblTextPrefix.Location = new System.Drawing.Point(2, 2);
            this.lblTextPrefix.Name = "lblTextPrefix";
            this.lblTextPrefix.Size = new System.Drawing.Size(0, 18);
            this.lblTextPrefix.TabIndex = 2;
            this.lblTextPrefix.Text = "Label Prefix Text";
            this.lblTextPrefix.UseMnemonic = false;
            this.lblTextPrefix.Visible = false;
            this.lblTextPrefix.TextChanged += new System.EventHandler(this.lblTextPrefix_TextChanged);
            this.lblTextPrefix.Click += new System.EventHandler(this.lblTextPrefix_Click);
            // 
            // lblTextAll
            // 
            this.lblTextAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextAll.Location = new System.Drawing.Point(4, 2);
            this.lblTextAll.Name = "lblTextAll";
            this.lblTextAll.Size = new System.Drawing.Size(605, 18);
            this.lblTextAll.TabIndex = 2;
            this.lblTextAll.Text = "Label All Text";
            this.lblTextAll.UseMnemonic = false;
            this.lblTextAll.Visible = false;
            this.lblTextAll.DoubleClick += new System.EventHandler(this.lblTextAll_DoubleClick);
            // 
            // XTextEditWithMaskedLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtTextEditable);
            this.Controls.Add(this.lblTextPrefix);
            this.Controls.Add(this.lblTextAll);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XTextEditWithMaskedLabel";
            this.Size = new System.Drawing.Size(248, 23);
            this.Load += new System.EventHandler(this.XTextEditWithMaskedLabel_Load);
            this.Enter += new System.EventHandler(this.XTextEditWithMaskedLabel_Enter);
            this.Leave += new System.EventHandler(this.XTextEditWithMaskedLabel_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtTextEditable;
        protected internal System.Windows.Forms.Label lblTextPrefix;
        protected internal System.Windows.Forms.Label lblTextAll;
    }
}
