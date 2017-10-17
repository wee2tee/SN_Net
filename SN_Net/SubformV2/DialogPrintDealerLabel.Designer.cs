namespace SN_Net.Subform
{
    partial class DialogPrintDealerLabel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.brTo = new CC.XBrowseBox();
            this.brFrom = new CC.XBrowseBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCondition = new CC.XTextEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdPrinter = new System.Windows.Forms.RadioButton();
            this.rdScreen = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.brTo);
            this.groupBox1.Controls.Add(this.brFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Scope";
            // 
            // brTo
            // 
            this.brTo._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brTo._CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.brTo._ReadOnly = false;
            this.brTo._Text = "";
            this.brTo._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brTo._UseImage = true;
            this.brTo.BackColor = System.Drawing.Color.White;
            this.brTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brTo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brTo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brTo.Location = new System.Drawing.Point(72, 55);
            this.brTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brTo.Name = "brTo";
            this.brTo.Size = new System.Drawing.Size(166, 23);
            this.brTo.TabIndex = 2;
            this.brTo._ButtonClick += new System.EventHandler(this.brTo__ButtonClick);
            this.brTo._Leave += new System.EventHandler(this.brTo__Leave);
            // 
            // brFrom
            // 
            this.brFrom._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brFrom._CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.brFrom._ReadOnly = false;
            this.brFrom._Text = "";
            this.brFrom._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.brFrom._UseImage = true;
            this.brFrom.BackColor = System.Drawing.Color.White;
            this.brFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brFrom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.brFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.brFrom.Location = new System.Drawing.Point(72, 28);
            this.brFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brFrom.Name = "brFrom";
            this.brFrom.Size = new System.Drawing.Size(166, 23);
            this.brFrom.TabIndex = 1;
            this.brFrom._ButtonClick += new System.EventHandler(this.brFrom__ButtonClick);
            this.brFrom._Leave += new System.EventHandler(this.brFrom__Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtCondition);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 65);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condition";
            // 
            // txtCondition
            // 
            this.txtCondition._BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCondition._CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtCondition._MaxLength = 32767;
            this.txtCondition._ReadOnly = false;
            this.txtCondition._SelectionLength = 0;
            this.txtCondition._SelectionStart = 0;
            this.txtCondition._Text = "";
            this.txtCondition._TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondition.BackColor = System.Drawing.Color.White;
            this.txtCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCondition.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCondition.Location = new System.Drawing.Point(13, 26);
            this.txtCondition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(398, 23);
            this.txtCondition.TabIndex = 4;
            this.txtCondition._TextChanged += new System.EventHandler(this.txtCondition__TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdPrinter);
            this.groupBox3.Controls.Add(this.rdScreen);
            this.groupBox3.Location = new System.Drawing.Point(294, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(140, 96);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // rdPrinter
            // 
            this.rdPrinter.AutoSize = true;
            this.rdPrinter.Location = new System.Drawing.Point(20, 55);
            this.rdPrinter.Name = "rdPrinter";
            this.rdPrinter.Size = new System.Drawing.Size(64, 20);
            this.rdPrinter.TabIndex = 7;
            this.rdPrinter.Text = "Printer";
            this.rdPrinter.UseVisualStyleBackColor = true;
            this.rdPrinter.CheckedChanged += new System.EventHandler(this.rdPrinter_CheckedChanged);
            // 
            // rdScreen
            // 
            this.rdScreen.AutoSize = true;
            this.rdScreen.Checked = true;
            this.rdScreen.Location = new System.Drawing.Point(20, 29);
            this.rdScreen.Name = "rdScreen";
            this.rdScreen.Size = new System.Drawing.Size(66, 20);
            this.rdScreen.TabIndex = 6;
            this.rdScreen.TabStop = true;
            this.rdScreen.Text = "Screen";
            this.rdScreen.UseVisualStyleBackColor = true;
            this.rdScreen.CheckedChanged += new System.EventHandler(this.rdScreen_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(146, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(227, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DialogPrintDealerLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 258);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DialogPrintDealerLabel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Scope";
            this.Load += new System.EventHandler(this.DialogPrintDealerLabel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdPrinter;
        private System.Windows.Forms.RadioButton rdScreen;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CC.XBrowseBox brFrom;
        private CC.XBrowseBox brTo;
        private CC.XTextEdit txtCondition;
    }
}