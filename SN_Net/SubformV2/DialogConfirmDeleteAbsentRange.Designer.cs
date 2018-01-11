namespace SN_Net.Subform
{
    partial class DialogConfirmDeleteAbsentRange
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
            this.btnDeleteOne = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "มีรายการอื่น ๆ ที่เกี่ยวโยงกันกับรายการนี้ (จากการบันทึกการลางานเป็นช่วง), ท่านต้" +
    "องการปฏิบัติอย่างไร?";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteOne
            // 
            this.btnDeleteOne.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteOne.Location = new System.Drawing.Point(32, 81);
            this.btnDeleteOne.Name = "btnDeleteOne";
            this.btnDeleteOne.Size = new System.Drawing.Size(120, 49);
            this.btnDeleteOne.TabIndex = 1;
            this.btnDeleteOne.Text = "ลบเฉพาะรายการนี้ <&O>";
            this.btnDeleteOne.UseVisualStyleBackColor = true;
            this.btnDeleteOne.Click += new System.EventHandler(this.btnDeleteOne_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteAll.Location = new System.Drawing.Point(166, 81);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(120, 49);
            this.btnDeleteAll.TabIndex = 2;
            this.btnDeleteAll.Text = "ลบรายการทั้งหมดที่เกี่ยวโยงกัน <&A>";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(300, 81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 49);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ยกเลิก <Esc>";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DialogConfirmDeleteAbsentRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 153);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDeleteOne);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogConfirmDeleteAbsentRange";
            this.ShowIcon = false;
            this.Text = "ลบรายการที่เกี่ยวโยงกัน";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteOne;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnCancel;
    }
}