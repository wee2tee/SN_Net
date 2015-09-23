namespace SN_Net.MiscClass
{
    partial class CustomTextBoxMaskedWithLabel
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
            this.txtEdit = new System.Windows.Forms.TextBox();
            this.txtStatic = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtEdit
            // 
            this.txtEdit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtEdit.Location = new System.Drawing.Point(73, 4);
            this.txtEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(204, 16);
            this.txtEdit.TabIndex = 1;
            // 
            // txtStatic
            // 
            this.txtStatic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtStatic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtStatic.Location = new System.Drawing.Point(1, 4);
            this.txtStatic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStatic.Name = "txtStatic";
            this.txtStatic.ReadOnly = true;
            this.txtStatic.Size = new System.Drawing.Size(69, 16);
            this.txtStatic.TabIndex = 2;
            // 
            // CustomTextBoxMaskedWithLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtStatic);
            this.Controls.Add(this.txtEdit);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomTextBoxMaskedWithLabel";
            this.Size = new System.Drawing.Size(325, 21);
            this.Load += new System.EventHandler(this.TextBoxMaskedWithLabel_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TextBoxMaskedWithLabel_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtEdit;
        public System.Windows.Forms.TextBox txtStatic;

    }
}
