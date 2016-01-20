namespace SN_Net.Subform
{
    partial class SerialPasswordList
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
            this.dgvPassword = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPassword
            // 
            this.dgvPassword.AllowUserToAddRows = false;
            this.dgvPassword.AllowUserToDeleteRows = false;
            this.dgvPassword.AllowUserToResizeColumns = false;
            this.dgvPassword.AllowUserToResizeRows = false;
            this.dgvPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPassword.BackgroundColor = System.Drawing.Color.White;
            this.dgvPassword.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPassword.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPassword.ColumnHeadersVisible = false;
            this.dgvPassword.GridColor = System.Drawing.Color.White;
            this.dgvPassword.Location = new System.Drawing.Point(99, 10);
            this.dgvPassword.MultiSelect = false;
            this.dgvPassword.Name = "dgvPassword";
            this.dgvPassword.RowHeadersVisible = false;
            this.dgvPassword.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvPassword.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dgvPassword.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            this.dgvPassword.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Red;
            this.dgvPassword.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvPassword.RowTemplate.Height = 25;
            this.dgvPassword.RowTemplate.ReadOnly = true;
            this.dgvPassword.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPassword.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPassword.Size = new System.Drawing.Size(276, 106);
            this.dgvPassword.StandardTab = true;
            this.dgvPassword.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(23, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 109;
            this.label3.Text = "Password : ";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(157, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 110;
            this.button1.Text = "ปิด";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SerialPasswordList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvPassword);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "SerialPasswordList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password สำหรับขอใช้บริการ";
            this.Load += new System.EventHandler(this.SerialPasswordList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}