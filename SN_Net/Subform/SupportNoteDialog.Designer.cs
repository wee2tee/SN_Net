namespace SN_Net.Subform
{
    partial class SupportNoteDialog
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dtWorkDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSernum = new SN_Net.MiscClass.CustomMaskedTextBox();
            this.txtContact = new SN_Net.MiscClass.CustomTextBox();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtEndTime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chTransferMkt = new System.Windows.Forms.CheckBox();
            this.chTraining = new System.Windows.Forms.CheckBox();
            this.chPeriod = new System.Windows.Forms.CheckBox();
            this.chYearEnd = new System.Windows.Forms.CheckBox();
            this.chSecure = new System.Windows.Forms.CheckBox();
            this.chAssets = new System.Windows.Forms.CheckBox();
            this.chRepExcel = new System.Windows.Forms.CheckBox();
            this.chForm = new System.Windows.Forms.CheckBox();
            this.chMailWait = new System.Windows.Forms.CheckBox();
            this.chPrint = new System.Windows.Forms.CheckBox();
            this.chStatement = new System.Windows.Forms.CheckBox();
            this.chFonts = new System.Windows.Forms.CheckBox();
            this.chInstall = new System.Windows.Forms.CheckBox();
            this.chError = new System.Windows.Forms.CheckBox();
            this.chMapDrive = new System.Windows.Forms.CheckBox();
            this.chStock = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.dtBreakStart = new System.Windows.Forms.DateTimePicker();
            this.dtBreakEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.txtRemark2 = new System.Windows.Forms.TextBox();
            this.rbTraining = new System.Windows.Forms.RadioButton();
            this.rbMeetCust = new System.Windows.Forms.RadioButton();
            this.txtSernum2 = new SN_Net.MiscClass.CustomMaskedTextBox();
            this.rbCorrectData = new System.Windows.Forms.RadioButton();
            this.rbQt = new System.Windows.Forms.RadioButton();
            this.rbToilet = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(353, 314);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 31);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(449, 314);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 31);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(8, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(873, 220);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSernum);
            this.tabPage1.Controls.Add(this.txtContact);
            this.tabPage1.Controls.Add(this.dtStartTime);
            this.tabPage1.Controls.Add(this.dtEndTime);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(865, 191);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "สายสนทนา";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dtWorkDate
            // 
            this.dtWorkDate.CustomFormat = "dd/MM/yyyy";
            this.dtWorkDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtWorkDate.Location = new System.Drawing.Point(85, 43);
            this.dtWorkDate.Name = "dtWorkDate";
            this.dtWorkDate.Size = new System.Drawing.Size(105, 23);
            this.dtWorkDate.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(17, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 16);
            this.label5.TabIndex = 146;
            this.label5.Text = "วันที่";
            // 
            // cbUser
            // 
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(85, 16);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(137, 24);
            this.cbUser.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(17, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 16);
            this.label7.TabIndex = 144;
            this.label7.Text = "พนักงาน";
            // 
            // txtSernum
            // 
            this.txtSernum.BackColor = System.Drawing.Color.White;
            this.txtSernum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSernum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSernum.Location = new System.Drawing.Point(86, 55);
            this.txtSernum.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSernum.MaskString = ">A-AAA-AAAAAA";
            this.txtSernum.Name = "txtSernum";
            this.txtSernum.Read_Only = false;
            this.txtSernum.SelectedStringBegin = 0;
            this.txtSernum.SelectedStringEnd = 0;
            this.txtSernum.Size = new System.Drawing.Size(137, 25);
            this.txtSernum.TabIndex = 4;
            this.txtSernum.Texts = "";
            // 
            // txtContact
            // 
            this.txtContact.BackColor = System.Drawing.Color.White;
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.CharUpperCase = false;
            this.txtContact.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtContact.Location = new System.Drawing.Point(86, 83);
            this.txtContact.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtContact.MaxChar = 20;
            this.txtContact.Name = "txtContact";
            this.txtContact.Read_Only = false;
            this.txtContact.SelectionLength = 0;
            this.txtContact.SelectionStart = 0;
            this.txtContact.Size = new System.Drawing.Size(189, 25);
            this.txtContact.TabIndex = 5;
            this.txtContact.Texts = "";
            // 
            // dtStartTime
            // 
            this.dtStartTime.CustomFormat = "HH:mm:ss";
            this.dtStartTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartTime.Location = new System.Drawing.Point(85, 13);
            this.dtStartTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.ShowUpDown = true;
            this.dtStartTime.Size = new System.Drawing.Size(83, 23);
            this.dtStartTime.TabIndex = 2;
            this.dtStartTime.Value = new System.DateTime(2015, 10, 2, 13, 52, 0, 0);
            // 
            // dtEndTime
            // 
            this.dtEndTime.CustomFormat = "HH:mm:ss";
            this.dtEndTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndTime.Location = new System.Drawing.Point(192, 13);
            this.dtEndTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.ShowUpDown = true;
            this.dtEndTime.Size = new System.Drawing.Size(83, 23);
            this.dtEndTime.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(175, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 16);
            this.label8.TabIndex = 141;
            this.label8.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(9, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 16);
            this.label11.TabIndex = 143;
            this.label11.Text = "Serial No.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(8, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 16);
            this.label15.TabIndex = 138;
            this.label15.Text = "เวลา";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.Location = new System.Drawing.Point(9, 87);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 16);
            this.label16.TabIndex = 142;
            this.label16.Text = "ชื่อผู้ติดต่อ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chTransferMkt);
            this.groupBox1.Controls.Add(this.chTraining);
            this.groupBox1.Controls.Add(this.chPeriod);
            this.groupBox1.Controls.Add(this.chYearEnd);
            this.groupBox1.Controls.Add(this.chSecure);
            this.groupBox1.Controls.Add(this.chAssets);
            this.groupBox1.Controls.Add(this.chRepExcel);
            this.groupBox1.Controls.Add(this.chForm);
            this.groupBox1.Controls.Add(this.chMailWait);
            this.groupBox1.Controls.Add(this.chPrint);
            this.groupBox1.Controls.Add(this.chStatement);
            this.groupBox1.Controls.Add(this.chFonts);
            this.groupBox1.Controls.Add(this.chInstall);
            this.groupBox1.Controls.Add(this.chError);
            this.groupBox1.Controls.Add(this.chMapDrive);
            this.groupBox1.Controls.Add(this.chStock);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(291, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 186);
            this.groupBox1.TabIndex = 112;
            this.groupBox1.TabStop = false;
            // 
            // chTransferMkt
            // 
            this.chTransferMkt.AutoSize = true;
            this.chTransferMkt.Location = new System.Drawing.Point(117, 111);
            this.chTransferMkt.Name = "chTransferMkt";
            this.chTransferMkt.Size = new System.Drawing.Size(115, 20);
            this.chTransferMkt.TabIndex = 21;
            this.chTransferMkt.Text = "โอนสายฝ่ายขาย";
            this.chTransferMkt.UseVisualStyleBackColor = true;
            // 
            // chTraining
            // 
            this.chTraining.AutoSize = true;
            this.chTraining.Location = new System.Drawing.Point(414, 14);
            this.chTraining.Name = "chTraining";
            this.chTraining.Size = new System.Drawing.Size(57, 20);
            this.chTraining.TabIndex = 11;
            this.chTraining.Text = "อบรม";
            this.chTraining.UseVisualStyleBackColor = true;
            // 
            // chPeriod
            // 
            this.chPeriod.AutoSize = true;
            this.chPeriod.Location = new System.Drawing.Point(254, 73);
            this.chPeriod.Name = "chPeriod";
            this.chPeriod.Size = new System.Drawing.Size(115, 20);
            this.chPeriod.TabIndex = 19;
            this.chPeriod.Text = "วันที่ไม่อยู่ในงวด";
            this.chPeriod.UseVisualStyleBackColor = true;
            // 
            // chYearEnd
            // 
            this.chYearEnd.AutoSize = true;
            this.chYearEnd.Location = new System.Drawing.Point(147, 73);
            this.chYearEnd.Name = "chYearEnd";
            this.chYearEnd.Size = new System.Drawing.Size(101, 20);
            this.chYearEnd.TabIndex = 18;
            this.chYearEnd.Text = "ปิดประมวลผล";
            this.chYearEnd.UseVisualStyleBackColor = true;
            // 
            // chSecure
            // 
            this.chSecure.AutoSize = true;
            this.chSecure.Location = new System.Drawing.Point(10, 73);
            this.chSecure.Name = "chSecure";
            this.chSecure.Size = new System.Drawing.Size(131, 20);
            this.chSecure.TabIndex = 17;
            this.chSecure.Text = "ระบบความปลอดภัย";
            this.chSecure.UseVisualStyleBackColor = true;
            // 
            // chAssets
            // 
            this.chAssets.AutoSize = true;
            this.chAssets.Location = new System.Drawing.Point(417, 50);
            this.chAssets.Name = "chAssets";
            this.chAssets.Size = new System.Drawing.Size(129, 20);
            this.chAssets.TabIndex = 16;
            this.chAssets.Text = "ทรัพย์สิน/ค่าเสื่อมฯ";
            this.chAssets.UseVisualStyleBackColor = true;
            // 
            // chRepExcel
            // 
            this.chRepExcel.AutoSize = true;
            this.chRepExcel.Location = new System.Drawing.Point(217, 50);
            this.chRepExcel.Name = "chRepExcel";
            this.chRepExcel.Size = new System.Drawing.Size(110, 20);
            this.chRepExcel.TabIndex = 14;
            this.chRepExcel.Text = "รายงาน->Excel";
            this.chRepExcel.UseVisualStyleBackColor = true;
            // 
            // chForm
            // 
            this.chForm.AutoSize = true;
            this.chForm.Location = new System.Drawing.Point(75, 50);
            this.chForm.Name = "chForm";
            this.chForm.Size = new System.Drawing.Size(134, 20);
            this.chForm.TabIndex = 13;
            this.chForm.Text = "แก้ไขฟอร์ม/รายงาน";
            this.chForm.UseVisualStyleBackColor = true;
            // 
            // chMailWait
            // 
            this.chMailWait.AutoSize = true;
            this.chMailWait.Location = new System.Drawing.Point(10, 111);
            this.chMailWait.Name = "chMailWait";
            this.chMailWait.Size = new System.Drawing.Size(91, 20);
            this.chMailWait.TabIndex = 20;
            this.chMailWait.Text = "Mail/รอสาย";
            this.chMailWait.UseVisualStyleBackColor = true;
            // 
            // chPrint
            // 
            this.chPrint.AutoSize = true;
            this.chPrint.Location = new System.Drawing.Point(324, 14);
            this.chPrint.Name = "chPrint";
            this.chPrint.Size = new System.Drawing.Size(53, 20);
            this.chPrint.TabIndex = 10;
            this.chPrint.Text = "Print";
            this.chPrint.UseVisualStyleBackColor = true;
            // 
            // chStatement
            // 
            this.chStatement.AutoSize = true;
            this.chStatement.Location = new System.Drawing.Point(337, 50);
            this.chStatement.Name = "chStatement";
            this.chStatement.Size = new System.Drawing.Size(74, 20);
            this.chStatement.TabIndex = 15;
            this.chStatement.Text = "สร้างงบฯ";
            this.chStatement.UseVisualStyleBackColor = true;
            // 
            // chFonts
            // 
            this.chFonts.AutoSize = true;
            this.chFonts.Location = new System.Drawing.Point(260, 14);
            this.chFonts.Name = "chFonts";
            this.chFonts.Size = new System.Drawing.Size(58, 20);
            this.chFonts.TabIndex = 9;
            this.chFonts.Text = "Fonts";
            this.chFonts.UseVisualStyleBackColor = true;
            // 
            // chInstall
            // 
            this.chInstall.AutoSize = true;
            this.chInstall.Location = new System.Drawing.Point(100, 14);
            this.chInstall.Name = "chInstall";
            this.chInstall.Size = new System.Drawing.Size(81, 20);
            this.chInstall.TabIndex = 7;
            this.chInstall.Text = "Install/Up";
            this.chInstall.UseVisualStyleBackColor = true;
            // 
            // chError
            // 
            this.chError.AutoSize = true;
            this.chError.Location = new System.Drawing.Point(192, 14);
            this.chError.Name = "chError";
            this.chError.Size = new System.Drawing.Size(56, 20);
            this.chError.TabIndex = 8;
            this.chError.Text = "Error";
            this.chError.UseVisualStyleBackColor = true;
            // 
            // chMapDrive
            // 
            this.chMapDrive.AutoSize = true;
            this.chMapDrive.Location = new System.Drawing.Point(10, 14);
            this.chMapDrive.Name = "chMapDrive";
            this.chMapDrive.Size = new System.Drawing.Size(84, 20);
            this.chMapDrive.TabIndex = 6;
            this.chMapDrive.Text = "Map Drive";
            this.chMapDrive.UseVisualStyleBackColor = true;
            // 
            // chStock
            // 
            this.chStock.AutoSize = true;
            this.chStock.Location = new System.Drawing.Point(10, 50);
            this.chStock.Name = "chStock";
            this.chStock.Size = new System.Drawing.Size(57, 20);
            this.chStock.TabIndex = 12;
            this.chStock.Text = "สินค้า";
            this.chStock.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.ForeColor = System.Drawing.Color.DarkGray;
            this.label9.Location = new System.Drawing.Point(80, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 116;
            this.label9.Text = "(สูงสุด 100 ตัวอักษร)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(6, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 115;
            this.label10.Text = "ปัญหาอื่น ๆ :";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(6, 154);
            this.txtRemark.MaxLength = 100;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(558, 26);
            this.txtRemark.TabIndex = 22;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.dtBreakStart);
            this.tabPage2.Controls.Add(this.dtBreakEnd);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(865, 191);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "พักสาย";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(193, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 16);
            this.label12.TabIndex = 117;
            this.label12.Text = "-";
            // 
            // dtBreakStart
            // 
            this.dtBreakStart.CustomFormat = "HH:mm:ss";
            this.dtBreakStart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtBreakStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBreakStart.Location = new System.Drawing.Point(108, 15);
            this.dtBreakStart.Name = "dtBreakStart";
            this.dtBreakStart.ShowUpDown = true;
            this.dtBreakStart.Size = new System.Drawing.Size(83, 23);
            this.dtBreakStart.TabIndex = 23;
            this.dtBreakStart.Value = new System.DateTime(2015, 10, 2, 13, 52, 0, 0);
            // 
            // dtBreakEnd
            // 
            this.dtBreakEnd.CustomFormat = "HH:mm:ss";
            this.dtBreakEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dtBreakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBreakEnd.Location = new System.Drawing.Point(207, 15);
            this.dtBreakEnd.Name = "dtBreakEnd";
            this.dtBreakEnd.ShowUpDown = true;
            this.dtBreakEnd.Size = new System.Drawing.Size(83, 23);
            this.dtBreakEnd.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(71, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 16);
            this.label13.TabIndex = 114;
            this.label13.Text = "เวลา";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbOther);
            this.groupBox2.Controls.Add(this.txtRemark2);
            this.groupBox2.Controls.Add(this.rbTraining);
            this.groupBox2.Controls.Add(this.rbMeetCust);
            this.groupBox2.Controls.Add(this.txtSernum2);
            this.groupBox2.Controls.Add(this.rbCorrectData);
            this.groupBox2.Controls.Add(this.rbQt);
            this.groupBox2.Controls.Add(this.rbToilet);
            this.groupBox2.Location = new System.Drawing.Point(108, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(744, 146);
            this.groupBox2.TabIndex = 113;
            this.groupBox2.TabStop = false;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(11, 116);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(52, 20);
            this.rbOther.TabIndex = 31;
            this.rbOther.Text = "อื่น ๆ";
            this.rbOther.UseVisualStyleBackColor = true;
            // 
            // txtRemark2
            // 
            this.txtRemark2.Location = new System.Drawing.Point(66, 116);
            this.txtRemark2.MaxLength = 100;
            this.txtRemark2.Name = "txtRemark2";
            this.txtRemark2.Size = new System.Drawing.Size(385, 23);
            this.txtRemark2.TabIndex = 32;
            // 
            // rbTraining
            // 
            this.rbTraining.AutoSize = true;
            this.rbTraining.Location = new System.Drawing.Point(11, 51);
            this.rbTraining.Name = "rbTraining";
            this.rbTraining.Size = new System.Drawing.Size(97, 20);
            this.rbTraining.TabIndex = 28;
            this.rbTraining.Text = "ผู้ช่วยฯ อบรม";
            this.rbTraining.UseVisualStyleBackColor = true;
            // 
            // rbMeetCust
            // 
            this.rbMeetCust.AutoSize = true;
            this.rbMeetCust.Location = new System.Drawing.Point(223, 17);
            this.rbMeetCust.Name = "rbMeetCust";
            this.rbMeetCust.Size = new System.Drawing.Size(87, 20);
            this.rbMeetCust.TabIndex = 27;
            this.rbMeetCust.Text = "ลูกค้ามาพบ";
            this.rbMeetCust.UseVisualStyleBackColor = true;
            // 
            // txtSernum2
            // 
            this.txtSernum2.BackColor = System.Drawing.Color.White;
            this.txtSernum2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSernum2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSernum2.Location = new System.Drawing.Point(170, 83);
            this.txtSernum2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSernum2.MaskString = ">A-AAA-AAAAAA";
            this.txtSernum2.Name = "txtSernum2";
            this.txtSernum2.Read_Only = false;
            this.txtSernum2.SelectedStringBegin = 0;
            this.txtSernum2.SelectedStringEnd = 0;
            this.txtSernum2.Size = new System.Drawing.Size(118, 23);
            this.txtSernum2.TabIndex = 30;
            this.txtSernum2.Texts = "";
            // 
            // rbCorrectData
            // 
            this.rbCorrectData.AutoSize = true;
            this.rbCorrectData.Location = new System.Drawing.Point(11, 84);
            this.rbCorrectData.Name = "rbCorrectData";
            this.rbCorrectData.Size = new System.Drawing.Size(158, 20);
            this.rbCorrectData.TabIndex = 29;
            this.rbCorrectData.Text = "แก้ไขข้อมูลให้ลูกค้า S/N";
            this.rbCorrectData.UseVisualStyleBackColor = true;
            // 
            // rbQt
            // 
            this.rbQt.AutoSize = true;
            this.rbQt.Location = new System.Drawing.Point(101, 17);
            this.rbQt.Name = "rbQt";
            this.rbQt.Size = new System.Drawing.Size(109, 20);
            this.rbQt.TabIndex = 26;
            this.rbQt.Text = "ทำใบเสนอราคา";
            this.rbQt.UseVisualStyleBackColor = true;
            // 
            // rbToilet
            // 
            this.rbToilet.AutoSize = true;
            this.rbToilet.Checked = true;
            this.rbToilet.Location = new System.Drawing.Point(11, 17);
            this.rbToilet.Name = "rbToilet";
            this.rbToilet.Size = new System.Drawing.Size(80, 20);
            this.rbToilet.TabIndex = 25;
            this.rbToilet.TabStop = true;
            this.rbToilet.Text = "เข้าห้องน้ำ";
            this.rbToilet.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(9, 56);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 16);
            this.label14.TabIndex = 112;
            this.label14.Text = "พักสายเนื่องจาก";
            // 
            // SupportNoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(887, 361);
            this.Controls.Add(this.dtWorkDate);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupportNoteDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "บันทึกการปฏิบัติงาน";
            this.Load += new System.EventHandler(this.SupportNoteDialog_Load);
            this.Shown += new System.EventHandler(this.SupportNoteDialog_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DateTimePicker dtWorkDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label7;
        private MiscClass.CustomMaskedTextBox txtSernum;
        private MiscClass.CustomTextBox txtContact;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.DateTimePicker dtEndTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtBreakStart;
        private System.Windows.Forms.DateTimePicker dtBreakEnd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.TextBox txtRemark2;
        private System.Windows.Forms.RadioButton rbTraining;
        private System.Windows.Forms.RadioButton rbMeetCust;
        private MiscClass.CustomMaskedTextBox txtSernum2;
        private System.Windows.Forms.RadioButton rbCorrectData;
        private System.Windows.Forms.RadioButton rbQt;
        private System.Windows.Forms.RadioButton rbToilet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chTransferMkt;
        private System.Windows.Forms.CheckBox chTraining;
        private System.Windows.Forms.CheckBox chPeriod;
        private System.Windows.Forms.CheckBox chYearEnd;
        private System.Windows.Forms.CheckBox chSecure;
        private System.Windows.Forms.CheckBox chAssets;
        private System.Windows.Forms.CheckBox chRepExcel;
        private System.Windows.Forms.CheckBox chForm;
        private System.Windows.Forms.CheckBox chMailWait;
        private System.Windows.Forms.CheckBox chPrint;
        private System.Windows.Forms.CheckBox chStatement;
        private System.Windows.Forms.CheckBox chFonts;
        private System.Windows.Forms.CheckBox chInstall;
        private System.Windows.Forms.CheckBox chError;
        private System.Windows.Forms.CheckBox chMapDrive;
        private System.Windows.Forms.CheckBox chStock;

    }
}