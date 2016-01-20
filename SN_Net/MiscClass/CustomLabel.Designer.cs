namespace SN_Net.MiscClass
{
    partial class CustomLabel
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
            this.components = new System.ComponentModel.Container();
            this.lblWeekend = new System.Windows.Forms.LinkLabel();
            this.picMaid = new System.Windows.Forms.PictureBox();
            this.picWeekend = new System.Windows.Forms.PictureBox();
            this.lblMaid = new System.Windows.Forms.LinkLabel();
            this.lblTrainer = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picMaid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWeekend)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWeekend
            // 
            this.lblWeekend.AutoSize = true;
            this.lblWeekend.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWeekend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblWeekend.Location = new System.Drawing.Point(18, 1);
            this.lblWeekend.Name = "lblWeekend";
            this.lblWeekend.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblWeekend.Size = new System.Drawing.Size(26, 16);
            this.lblWeekend.TabIndex = 1;
            this.lblWeekend.TabStop = true;
            this.lblWeekend.Text = "A";
            this.lblWeekend.Visible = false;
            // 
            // picMaid
            // 
            this.picMaid.Dock = System.Windows.Forms.DockStyle.Left;
            this.picMaid.Image = global::SN_Net.Properties.Resources.maid;
            this.picMaid.Location = new System.Drawing.Point(44, 1);
            this.picMaid.Name = "picMaid";
            this.picMaid.Size = new System.Drawing.Size(17, 16);
            this.picMaid.TabIndex = 2;
            this.picMaid.TabStop = false;
            this.toolTip1.SetToolTip(this.picMaid, "เวรทำความสะอาด");
            this.picMaid.Visible = false;
            // 
            // picWeekend
            // 
            this.picWeekend.Dock = System.Windows.Forms.DockStyle.Left;
            this.picWeekend.Image = global::SN_Net.Properties.Resources.smile;
            this.picWeekend.Location = new System.Drawing.Point(1, 1);
            this.picWeekend.Name = "picWeekend";
            this.picWeekend.Size = new System.Drawing.Size(17, 16);
            this.picWeekend.TabIndex = 0;
            this.picWeekend.TabStop = false;
            this.toolTip1.SetToolTip(this.picWeekend, "วันหยุดพิเศษ");
            this.picWeekend.Visible = false;
            // 
            // lblMaid
            // 
            this.lblMaid.AutoSize = true;
            this.lblMaid.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMaid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblMaid.Location = new System.Drawing.Point(61, 1);
            this.lblMaid.Name = "lblMaid";
            this.lblMaid.Size = new System.Drawing.Size(16, 16);
            this.lblMaid.TabIndex = 3;
            this.lblMaid.TabStop = true;
            this.lblMaid.Text = "B";
            this.lblMaid.Visible = false;
            // 
            // lblTrainer
            // 
            this.lblTrainer.AutoSize = true;
            this.lblTrainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTrainer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTrainer.ForeColor = System.Drawing.Color.Blue;
            this.lblTrainer.Location = new System.Drawing.Point(77, 1);
            this.lblTrainer.Name = "lblTrainer";
            this.lblTrainer.Size = new System.Drawing.Size(31, 13);
            this.lblTrainer.TabIndex = 4;
            this.lblTrainer.Text = "        ";
            this.lblTrainer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrainer.Visible = false;
            // 
            // CustomLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTrainer);
            this.Controls.Add(this.lblMaid);
            this.Controls.Add(this.picMaid);
            this.Controls.Add(this.lblWeekend);
            this.Controls.Add(this.picWeekend);
            this.Name = "CustomLabel";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(150, 18);
            ((System.ComponentModel.ISupportInitialize)(this.picMaid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWeekend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWeekend;
        private System.Windows.Forms.LinkLabel lblWeekend;
        private System.Windows.Forms.PictureBox picMaid;
        private System.Windows.Forms.LinkLabel lblMaid;
        private System.Windows.Forms.Label lblTrainer;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
