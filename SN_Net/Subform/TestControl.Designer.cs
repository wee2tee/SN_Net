namespace SN_Net.Subform
{
    partial class TestControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customDateEvent1 = new SN_Net.MiscClass.CustomDateEvent();
            this.customDateEvent2 = new SN_Net.MiscClass.CustomDateEvent();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.customDateEvent1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customDateEvent2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(28, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 272);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // customDateEvent1
            // 
            this.customDateEvent1.BackColor = System.Drawing.Color.White;
            this.customDateEvent1.Date = new System.DateTime(2015, 10, 21, 10, 16, 50, 605);
            this.customDateEvent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customDateEvent1.IsShown = true;
            this.customDateEvent1.Location = new System.Drawing.Point(1, 1);
            this.customDateEvent1.Margin = new System.Windows.Forms.Padding(1);
            this.customDateEvent1.Name = "customDateEvent1";
            this.customDateEvent1.Size = new System.Drawing.Size(192, 134);
            this.customDateEvent1.TabIndex = 0;
            // 
            // customDateEvent2
            // 
            this.customDateEvent2.BackColor = System.Drawing.Color.White;
            this.customDateEvent2.Date = new System.DateTime(2015, 10, 21, 10, 16, 53, 353);
            this.customDateEvent2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customDateEvent2.IsShown = true;
            this.customDateEvent2.Location = new System.Drawing.Point(195, 1);
            this.customDateEvent2.Margin = new System.Windows.Forms.Padding(1);
            this.customDateEvent2.Name = "customDateEvent2";
            this.customDateEvent2.Size = new System.Drawing.Size(192, 134);
            this.customDateEvent2.TabIndex = 1;
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 317);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestControl";
            this.Text = "TestControl";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MiscClass.CustomDateEvent customDateEvent1;
        private MiscClass.CustomDateEvent customDateEvent2;
    }
}