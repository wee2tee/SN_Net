namespace SN_Net.Subform
{
    partial class PreferenceForm
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
            this.toolStripEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mskMainURL = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSearchDate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSearchMethod = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBreakTimeMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Web API main url :";
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEdit.Image = global::SN_Net.Properties.Resources.edit;
            this.toolStripEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(36, 40);
            this.toolStripEdit.Text = "toolStripButton1";
            this.toolStripEdit.Click += new System.EventHandler(this.toolStripEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // toolStripCancel
            // 
            this.toolStripCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCancel.Enabled = false;
            this.toolStripCancel.Image = global::SN_Net.Properties.Resources.stop;
            this.toolStripCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCancel.Name = "toolStripCancel";
            this.toolStripCancel.Size = new System.Drawing.Size(36, 40);
            this.toolStripCancel.Text = "toolStripButton2";
            this.toolStripCancel.Click += new System.EventHandler(this.toolStripCancel_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSave.Enabled = false;
            this.toolStripSave.Image = global::SN_Net.Properties.Resources.save;
            this.toolStripSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(36, 40);
            this.toolStripSave.Text = "toolStripButton3";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEdit,
            this.toolStripSeparator1,
            this.toolStripCancel,
            this.toolStripSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(633, 43);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mskMainURL
            // 
            this.mskMainURL.Enabled = false;
            this.mskMainURL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.mskMainURL.Location = new System.Drawing.Point(148, 67);
            this.mskMainURL.Mask = "http://CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC" +
    "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC";
            this.mskMainURL.Name = "mskMainURL";
            this.mskMainURL.PromptChar = ' ';
            this.mskMainURL.Size = new System.Drawing.Size(386, 23);
            this.mskMainURL.TabIndex = 3;
            this.mskMainURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mskMainURL_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(18, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "วิธีการกำหนดเวลาพักสาย :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbSearchDate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbSearchMethod);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbBreakTimeMethod);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 137);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "บันทึกการปฏิบัติงานประจำวัน";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(19, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 11);
            this.label7.TabIndex = 12;
            this.label7.Text = "[SEARCH_NOTE_DATE]";
            // 
            // cbSearchDate
            // 
            this.cbSearchDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchDate.Enabled = false;
            this.cbSearchDate.FormattingEnabled = true;
            this.cbSearchDate.Location = new System.Drawing.Point(197, 94);
            this.cbSearchDate.Name = "cbSearchDate";
            this.cbSearchDate.Size = new System.Drawing.Size(187, 24);
            this.cbSearchDate.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(18, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "ค้นหาประวัติการสนทนาในวันที่ :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(19, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 11);
            this.label6.TabIndex = 9;
            this.label6.Text = "[SEARCH_NOTE_METHOD]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(19, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 11);
            this.label5.TabIndex = 8;
            this.label5.Text = "[BREAK_TIME_METHOD]";
            // 
            // cbSearchMethod
            // 
            this.cbSearchMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchMethod.Enabled = false;
            this.cbSearchMethod.FormattingEnabled = true;
            this.cbSearchMethod.Location = new System.Drawing.Point(197, 58);
            this.cbSearchMethod.Name = "cbSearchMethod";
            this.cbSearchMethod.Size = new System.Drawing.Size(287, 24);
            this.cbSearchMethod.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(18, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "วิธีการค้นหาประวัติการสนทนา :";
            // 
            // cbBreakTimeMethod
            // 
            this.cbBreakTimeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBreakTimeMethod.Enabled = false;
            this.cbBreakTimeMethod.FormattingEnabled = true;
            this.cbBreakTimeMethod.Location = new System.Drawing.Point(197, 22);
            this.cbBreakTimeMethod.Name = "cbBreakTimeMethod";
            this.cbBreakTimeMethod.Size = new System.Drawing.Size(187, 24);
            this.cbBreakTimeMethod.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(14, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 11);
            this.label4.TabIndex = 6;
            this.label4.Text = "[MAIN_URL]";
            // 
            // PreferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 278);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mskMainURL);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferenceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferenceForm_FormClosing);
            this.Load += new System.EventHandler(this.PreferenceForm_Load);
            this.Shown += new System.EventHandler(this.PreferenceForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PreferenceForm_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripCancel;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton toolStripEdit;
        private System.Windows.Forms.MaskedTextBox mskMainURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbBreakTimeMethod;
        private System.Windows.Forms.ComboBox cbSearchMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbSearchDate;
        private System.Windows.Forms.Label label8;
    }
}