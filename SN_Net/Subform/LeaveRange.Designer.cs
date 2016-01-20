namespace SN_Net.Subform
{
    partial class LeaveRange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeaveRange));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dtDateStart = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMedCert = new System.Windows.Forms.ComboBox();
            this.cbUsers = new System.Windows.Forms.ComboBox();
            this.cbReason = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.chSaturday = new System.Windows.Forms.CheckBox();
            this.chFriday = new System.Windows.Forms.CheckBox();
            this.chThursday = new System.Windows.Forms.CheckBox();
            this.chWednesday = new System.Windows.Forms.CheckBox();
            this.chTuesday = new System.Windows.Forms.CheckBox();
            this.chMonday = new System.Windows.Forms.CheckBox();
            this.numFine = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chIsFine = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.dtToTime = new SN_Net.MiscClass.CustomTimePicker();
            this.dtFromTime = new SN_Net.MiscClass.CustomTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFine)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "พนักงาน";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "เหตุผล";
            // 
            // dtDateEnd
            // 
            this.dtDateEnd.Checked = false;
            this.dtDateEnd.CustomFormat = "dddd, dd/MM/yyyy";
            this.dtDateEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateEnd.Location = new System.Drawing.Point(296, 68);
            this.dtDateEnd.Name = "dtDateEnd";
            this.dtDateEnd.Size = new System.Drawing.Size(167, 23);
            this.dtDateEnd.TabIndex = 3;
            this.dtDateEnd.Value = new System.DateTime(2015, 10, 9, 16, 11, 0, 0);
            // 
            // dtDateStart
            // 
            this.dtDateStart.Checked = false;
            this.dtDateStart.CustomFormat = "dddd, dd/MM/yyyy";
            this.dtDateStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateStart.Location = new System.Drawing.Point(92, 68);
            this.dtDateStart.Name = "dtDateStart";
            this.dtDateStart.Size = new System.Drawing.Size(167, 23);
            this.dtDateStart.TabIndex = 2;
            this.dtDateStart.Value = new System.DateTime(2015, 10, 9, 16, 11, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(267, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 16);
            this.label9.TabIndex = 123;
            this.label9.Text = "ถึง";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(20, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 122;
            this.label5.Text = "วันที่ จาก";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(20, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 124;
            this.label3.Text = "ใบรับรองแพทย์";
            // 
            // cbMedCert
            // 
            this.cbMedCert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMedCert.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbMedCert.FormattingEnabled = true;
            this.cbMedCert.Location = new System.Drawing.Point(134, 127);
            this.cbMedCert.Name = "cbMedCert";
            this.cbMedCert.Size = new System.Drawing.Size(144, 24);
            this.cbMedCert.TabIndex = 6;
            // 
            // cbUsers
            // 
            this.cbUsers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbUsers.FormattingEnabled = true;
            this.cbUsers.Location = new System.Drawing.Point(92, 12);
            this.cbUsers.Name = "cbUsers";
            this.cbUsers.Size = new System.Drawing.Size(122, 24);
            this.cbUsers.TabIndex = 0;
            // 
            // cbReason
            // 
            this.cbReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReason.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbReason.FormattingEnabled = true;
            this.cbReason.Location = new System.Drawing.Point(92, 40);
            this.cbReason.Name = "cbReason";
            this.cbReason.Size = new System.Drawing.Size(167, 24);
            this.cbReason.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.Location = new System.Drawing.Point(162, 319);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 31);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(244, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 31);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.chSaturday);
            this.panel1.Controls.Add(this.chFriday);
            this.panel1.Controls.Add(this.chThursday);
            this.panel1.Controls.Add(this.chWednesday);
            this.panel1.Controls.Add(this.chTuesday);
            this.panel1.Controls.Add(this.chMonday);
            this.panel1.Controls.Add(this.numFine);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(16, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 75);
            this.panel1.TabIndex = 131;
            this.panel1.TabStop = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(208, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 16);
            this.label7.TabIndex = 139;
            this.label7.Text = "บาท";
            // 
            // chSaturday
            // 
            this.chSaturday.AutoSize = true;
            this.chSaturday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chSaturday.Location = new System.Drawing.Point(382, 14);
            this.chSaturday.Name = "chSaturday";
            this.chSaturday.Size = new System.Drawing.Size(51, 20);
            this.chSaturday.TabIndex = 15;
            this.chSaturday.Text = "เสาร์";
            this.chSaturday.UseVisualStyleBackColor = true;
            // 
            // chFriday
            // 
            this.chFriday.AutoSize = true;
            this.chFriday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chFriday.Location = new System.Drawing.Point(328, 14);
            this.chFriday.Name = "chFriday";
            this.chFriday.Size = new System.Drawing.Size(49, 20);
            this.chFriday.TabIndex = 14;
            this.chFriday.Text = "ศุกร์";
            this.chFriday.UseVisualStyleBackColor = true;
            // 
            // chThursday
            // 
            this.chThursday.AutoSize = true;
            this.chThursday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chThursday.Location = new System.Drawing.Point(257, 14);
            this.chThursday.Name = "chThursday";
            this.chThursday.Size = new System.Drawing.Size(66, 20);
            this.chThursday.TabIndex = 13;
            this.chThursday.Text = "พฤหัสฯ";
            this.chThursday.UseVisualStyleBackColor = true;
            // 
            // chWednesday
            // 
            this.chWednesday.AutoSize = true;
            this.chWednesday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chWednesday.Location = new System.Drawing.Point(208, 14);
            this.chWednesday.Name = "chWednesday";
            this.chWednesday.Size = new System.Drawing.Size(43, 20);
            this.chWednesday.TabIndex = 12;
            this.chWednesday.Text = "พุธ";
            this.chWednesday.UseVisualStyleBackColor = true;
            // 
            // chTuesday
            // 
            this.chTuesday.AutoSize = true;
            this.chTuesday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chTuesday.Location = new System.Drawing.Point(142, 14);
            this.chTuesday.Name = "chTuesday";
            this.chTuesday.Size = new System.Drawing.Size(61, 20);
            this.chTuesday.TabIndex = 11;
            this.chTuesday.Text = "อังคาร";
            this.chTuesday.UseVisualStyleBackColor = true;
            // 
            // chMonday
            // 
            this.chMonday.AutoSize = true;
            this.chMonday.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chMonday.Location = new System.Drawing.Point(79, 14);
            this.chMonday.Name = "chMonday";
            this.chMonday.Size = new System.Drawing.Size(57, 20);
            this.chMonday.TabIndex = 10;
            this.chMonday.Text = "จันทร์";
            this.chMonday.UseVisualStyleBackColor = true;
            // 
            // numFine
            // 
            this.numFine.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numFine.Location = new System.Drawing.Point(141, 42);
            this.numFine.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFine.Name = "numFine";
            this.numFine.Size = new System.Drawing.Size(65, 23);
            this.numFine.TabIndex = 16;
            this.numFine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(13, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "จำนวนเงินที่หักต่อวัน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(13, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "เฉพาะวัน";
            // 
            // chIsFine
            // 
            this.chIsFine.AutoSize = true;
            this.chIsFine.Checked = true;
            this.chIsFine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chIsFine.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chIsFine.Location = new System.Drawing.Point(24, 221);
            this.chIsFine.Name = "chIsFine";
            this.chIsFine.Size = new System.Drawing.Size(88, 20);
            this.chIsFine.TabIndex = 9;
            this.chIsFine.Text = "หักค่าคอมฯ";
            this.chIsFine.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 362);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 132;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.BackColor = System.Drawing.Color.Transparent;
            this.toolStripProcessing.ForeColor = System.Drawing.Color.Green;
            this.toolStripProcessing.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcessing.Image")));
            this.toolStripProcessing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcessing.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProcessing.Size = new System.Drawing.Size(458, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(20, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 133;
            this.label8.Text = "เวลา จาก";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(178, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 16);
            this.label10.TabIndex = 134;
            this.label10.Text = "ถึง";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(20, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 16);
            this.label11.TabIndex = 135;
            this.label11.Text = "สถานะ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(20, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 16);
            this.label12.TabIndex = 136;
            this.label12.Text = "หมายเหตุ/ชื่อลูกค้า";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(134, 157);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(98, 24);
            this.cbStatus.TabIndex = 7;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCustomer.Location = new System.Drawing.Point(134, 187);
            this.txtCustomer.MaxLength = 40;
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(329, 23);
            this.txtCustomer.TabIndex = 8;
            // 
            // dtToTime
            // 
            this.dtToTime.BackColor = System.Drawing.Color.White;
            this.dtToTime.Location = new System.Drawing.Point(206, 98);
            this.dtToTime.Name = "dtToTime";
            this.dtToTime.Read_Only = false;
            this.dtToTime.Show_Second = false;
            this.dtToTime.Size = new System.Drawing.Size(75, 23);
            this.dtToTime.TabIndex = 5;
            this.dtToTime.Time = new System.DateTime(2015, 11, 30, 16, 7, 0, 0);
            // 
            // dtFromTime
            // 
            this.dtFromTime.BackColor = System.Drawing.Color.White;
            this.dtFromTime.Location = new System.Drawing.Point(92, 98);
            this.dtFromTime.Name = "dtFromTime";
            this.dtFromTime.Read_Only = false;
            this.dtFromTime.Show_Second = false;
            this.dtFromTime.Size = new System.Drawing.Size(75, 23);
            this.dtFromTime.TabIndex = 4;
            this.dtFromTime.Time = new System.DateTime(2015, 11, 30, 16, 7, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.ForeColor = System.Drawing.Color.Gray;
            this.label13.Location = new System.Drawing.Point(280, 130);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 137;
            this.label13.Text = "*กรณีลาป่วย";
            // 
            // LeaveRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 384);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dtToTime);
            this.Controls.Add(this.dtFromTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chIsFine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbReason);
            this.Controls.Add(this.cbUsers);
            this.Controls.Add(this.cbMedCert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDateEnd);
            this.Controls.Add(this.dtDateStart);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LeaveRange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ลางานเป็นช่วง";
            this.Load += new System.EventHandler(this.LeaveRange_Load);
            this.Shown += new System.EventHandler(this.LeaveRange_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFine)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMedCert;
        private System.Windows.Forms.ComboBox cbUsers;
        private System.Windows.Forms.ComboBox cbReason;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chIsFine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chSaturday;
        private System.Windows.Forms.CheckBox chFriday;
        private System.Windows.Forms.CheckBox chThursday;
        private System.Windows.Forms.CheckBox chWednesday;
        private System.Windows.Forms.CheckBox chTuesday;
        private System.Windows.Forms.CheckBox chMonday;
        private System.Windows.Forms.NumericUpDown numFine;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private MiscClass.CustomTimePicker dtFromTime;
        private MiscClass.CustomTimePicker dtToTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox txtCustomer;
        public System.Windows.Forms.DateTimePicker dtDateEnd;
        public System.Windows.Forms.DateTimePicker dtDateStart;
        private System.Windows.Forms.Label label13;
    }
}