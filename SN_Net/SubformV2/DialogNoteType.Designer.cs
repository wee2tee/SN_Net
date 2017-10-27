namespace SN_Net.Subform
{
    partial class DialogNoteType
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
            this.btnBreak = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnTel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBreak
            // 
            this.btnBreak.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBreak.Image = global::SN_Net.Properties.Resources.pause_32;
            this.btnBreak.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBreak.Location = new System.Drawing.Point(161, 21);
            this.btnBreak.Name = "btnBreak";
            this.btnBreak.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnBreak.Size = new System.Drawing.Size(113, 88);
            this.btnBreak.TabIndex = 1;
            this.btnBreak.Text = "&2. บันทึกการพักสาย";
            this.btnBreak.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBreak.UseVisualStyleBackColor = true;
            this.btnBreak.Click += new System.EventHandler(this.btnBreak_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnTrain.Image = global::SN_Net.Properties.Resources.teacher;
            this.btnTrain.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTrain.Location = new System.Drawing.Point(300, 21);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnTrain.Size = new System.Drawing.Size(113, 88);
            this.btnTrain.TabIndex = 2;
            this.btnTrain.Text = "&3. บันทึกการเข้าห้องอบรม";
            this.btnTrain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnTel
            // 
            this.btnTel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnTel.Image = global::SN_Net.Properties.Resources.support;
            this.btnTel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTel.Location = new System.Drawing.Point(22, 21);
            this.btnTel.Name = "btnTel";
            this.btnTel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnTel.Size = new System.Drawing.Size(113, 88);
            this.btnTel.TabIndex = 0;
            this.btnTel.Text = "&1. บันทึกสายสนทนากับลูกค้า";
            this.btnTel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTel.UseVisualStyleBackColor = true;
            this.btnTel.Click += new System.EventHandler(this.btnTel_Click);
            // 
            // DialogNoteType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 130);
            this.ControlBox = false;
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnBreak);
            this.Controls.Add(this.btnTel);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNoteType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTel;
        private System.Windows.Forms.Button btnBreak;
        private System.Windows.Forms.Button btnTrain;
    }
}