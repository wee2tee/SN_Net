namespace CC
{
    partial class XDatePicker
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
            this.btnShowCalendar = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnShowCalendar
            // 
            this.btnShowCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowCalendar.Image = global::CC.Properties.Resources.calendar_16;
            this.btnShowCalendar.Location = new System.Drawing.Point(78, -1);
            this.btnShowCalendar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowCalendar.Name = "btnShowCalendar";
            this.btnShowCalendar.Size = new System.Drawing.Size(24, 23);
            this.btnShowCalendar.TabIndex = 2;
            this.btnShowCalendar.TabStop = false;
            this.btnShowCalendar.UseVisualStyleBackColor = true;
            this.btnShowCalendar.Click += new System.EventHandler(this.btnShowCalendar_Click);
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtDate.Location = new System.Drawing.Point(3, 2);
            this.txtDate.Mask = "00/00/0000";
            this.txtDate.Name = "txtDate";
            this.txtDate.PromptChar = ' ';
            this.txtDate.Size = new System.Drawing.Size(76, 16);
            this.txtDate.TabIndex = 3;
            this.txtDate.ValidatingType = typeof(System.DateTime);
            this.txtDate.TextChanged += new System.EventHandler(this.txtDate_TextChanged);
            this.txtDate.VisibleChanged += new System.EventHandler(this.txtDate_VisibleChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "  /  /    ";
            this.label1.UseMnemonic = false;
            this.label1.Visible = false;
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // XDatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnShowCalendar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDate);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "XDatePicker";
            this.Size = new System.Drawing.Size(103, 23);
            this.Load += new System.EventHandler(this.XDatePicker_Load);
            this.Enter += new System.EventHandler(this.XDatePicker_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnShowCalendar;
        public System.Windows.Forms.MaskedTextBox txtDate;
        private System.Windows.Forms.Label label1;
    }
}
