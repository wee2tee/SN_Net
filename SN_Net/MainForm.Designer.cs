namespace SN_Net
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dealerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportStatMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeaveSummaryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salesAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToKnowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.businessTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.problemCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveCauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.userManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macAddressAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.preferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProcessing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblTimeDuration = new System.Windows.Forms.Label();
            this.SearchHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.helpToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(868, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sNToolStripMenuItem,
            this.dealerToolStripMenuItem,
            this.calendarMenuItem,
            this.supportStatMenuItem,
            this.LeaveSummaryMenuItem,
            this.SearchHistoryMenuItem,
            this.toolStripSeparator2,
            this.tableToolStripMenuItem,
            this.toolStripSeparator1,
            this.userManagementToolStripMenuItem,
            this.macAddressAllowedToolStripMenuItem,
            this.toolStripSeparator3,
            this.preferenceToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem,
            this.testToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.modeToolStripMenuItem.Text = "File";
            // 
            // sNToolStripMenuItem
            // 
            this.sNToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sNToolStripMenuItem.Name = "sNToolStripMenuItem";
            this.sNToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.sNToolStripMenuItem.Text = "S/N";
            this.sNToolStripMenuItem.Click += new System.EventHandler(this.sNToolStripMenuItem_Click);
            // 
            // dealerToolStripMenuItem
            // 
            this.dealerToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dealerToolStripMenuItem.Name = "dealerToolStripMenuItem";
            this.dealerToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.dealerToolStripMenuItem.Text = "Dealer";
            this.dealerToolStripMenuItem.Click += new System.EventHandler(this.dealerToolStripMenuItem_Click);
            // 
            // calendarMenuItem
            // 
            this.calendarMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarMenuItem.Name = "calendarMenuItem";
            this.calendarMenuItem.Size = new System.Drawing.Size(335, 22);
            this.calendarMenuItem.Text = "ปฏิธินวันหยุดประจำปี, วันลา, จองวันออกพบลูกค้า";
            this.calendarMenuItem.Click += new System.EventHandler(this.calendarMenuItem_Click);
            // 
            // supportStatMenuItem
            // 
            this.supportStatMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supportStatMenuItem.Name = "supportStatMenuItem";
            this.supportStatMenuItem.Size = new System.Drawing.Size(335, 22);
            this.supportStatMenuItem.Text = "ข้อมูลการปฏิบัติงาน (Support)";
            this.supportStatMenuItem.Click += new System.EventHandler(this.supportStatMenuItem_Click);
            // 
            // LeaveSummaryMenuItem
            // 
            this.LeaveSummaryMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeaveSummaryMenuItem.Name = "LeaveSummaryMenuItem";
            this.LeaveSummaryMenuItem.Size = new System.Drawing.Size(335, 22);
            this.LeaveSummaryMenuItem.Text = "สรุปวันลา,ออกพบลูกค้า";
            this.LeaveSummaryMenuItem.Click += new System.EventHandler(this.leaveSummaryMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(332, 6);
            // 
            // tableToolStripMenuItem
            // 
            this.tableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salesAreaToolStripMenuItem,
            this.versionExtensionToolStripMenuItem,
            this.howToKnowToolStripMenuItem,
            this.businessTypeToolStripMenuItem,
            this.problemCodeToolStripMenuItem,
            this.leaveCauseToolStripMenuItem});
            this.tableToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            this.tableToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.tableToolStripMenuItem.Text = "Table";
            // 
            // salesAreaToolStripMenuItem
            // 
            this.salesAreaToolStripMenuItem.Name = "salesAreaToolStripMenuItem";
            this.salesAreaToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.salesAreaToolStripMenuItem.Text = "01 - Sales Area";
            this.salesAreaToolStripMenuItem.Click += new System.EventHandler(this.salesAreaToolStripMenuItem_Click);
            // 
            // versionExtensionToolStripMenuItem
            // 
            this.versionExtensionToolStripMenuItem.Name = "versionExtensionToolStripMenuItem";
            this.versionExtensionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.versionExtensionToolStripMenuItem.Text = "02 - Version Extension";
            this.versionExtensionToolStripMenuItem.Click += new System.EventHandler(this.versionExtensionToolStripMenuItem_Click);
            // 
            // howToKnowToolStripMenuItem
            // 
            this.howToKnowToolStripMenuItem.Name = "howToKnowToolStripMenuItem";
            this.howToKnowToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.howToKnowToolStripMenuItem.Text = "03 - How to Know";
            this.howToKnowToolStripMenuItem.Click += new System.EventHandler(this.howToKnowToolStripMenuItem_Click);
            // 
            // businessTypeToolStripMenuItem
            // 
            this.businessTypeToolStripMenuItem.Name = "businessTypeToolStripMenuItem";
            this.businessTypeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.businessTypeToolStripMenuItem.Text = "04 - Business Type";
            this.businessTypeToolStripMenuItem.Click += new System.EventHandler(this.businessTypeToolStripMenuItem_Click);
            // 
            // problemCodeToolStripMenuItem
            // 
            this.problemCodeToolStripMenuItem.Name = "problemCodeToolStripMenuItem";
            this.problemCodeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.problemCodeToolStripMenuItem.Text = "05 - Problem Code";
            this.problemCodeToolStripMenuItem.Click += new System.EventHandler(this.problemCodeToolStripMenuItem_Click);
            // 
            // leaveCauseToolStripMenuItem
            // 
            this.leaveCauseToolStripMenuItem.Name = "leaveCauseToolStripMenuItem";
            this.leaveCauseToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.leaveCauseToolStripMenuItem.Text = "06 - Absent Cause";
            this.leaveCauseToolStripMenuItem.Click += new System.EventHandler(this.leaveCauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(332, 6);
            // 
            // userManagementToolStripMenuItem
            // 
            this.userManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userInformationToolStripMenuItem,
            this.changePasswordToolStripMenuItem});
            this.userManagementToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userManagementToolStripMenuItem.Name = "userManagementToolStripMenuItem";
            this.userManagementToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.userManagementToolStripMenuItem.Text = "User management";
            // 
            // userInformationToolStripMenuItem
            // 
            this.userInformationToolStripMenuItem.Name = "userInformationToolStripMenuItem";
            this.userInformationToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.userInformationToolStripMenuItem.Text = "User information";
            this.userInformationToolStripMenuItem.Click += new System.EventHandler(this.userInformationToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.changePasswordToolStripMenuItem.Text = "Change password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // macAddressAllowedToolStripMenuItem
            // 
            this.macAddressAllowedToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macAddressAllowedToolStripMenuItem.Name = "macAddressAllowedToolStripMenuItem";
            this.macAddressAllowedToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.macAddressAllowedToolStripMenuItem.Text = "MAC address allowed";
            this.macAddressAllowedToolStripMenuItem.Click += new System.EventHandler(this.macAddressAllowedToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(332, 6);
            // 
            // preferenceToolStripMenuItem
            // 
            this.preferenceToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferenceToolStripMenuItem.Name = "preferenceToolStripMenuItem";
            this.preferenceToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.preferenceToolStripMenuItem.Text = "Preference";
            this.preferenceToolStripMenuItem.Click += new System.EventHandler(this.preferenceToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(332, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(335, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.helpToolStripMenuItem.Text = "Help?";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUserInfo,
            this.toolStripInfo,
            this.toolStripProcessing});
            this.statusStrip1.Location = new System.Drawing.Point(0, 659);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(868, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripUserInfo
            // 
            this.toolStripUserInfo.Name = "toolStripUserInfo";
            this.toolStripUserInfo.Size = new System.Drawing.Size(16, 17);
            this.toolStripUserInfo.Text = "...";
            this.toolStripUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.Size = new System.Drawing.Size(22, 17);
            this.toolStripInfo.Text = "     ";
            this.toolStripInfo.TextChanged += new System.EventHandler(this.toolStripInfo_TextChanged);
            // 
            // toolStripProcessing
            // 
            this.toolStripProcessing.ForeColor = System.Drawing.Color.Green;
            this.toolStripProcessing.Image = ((System.Drawing.Image)(resources.GetObject("toolStripProcessing.Image")));
            this.toolStripProcessing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripProcessing.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStripProcessing.Name = "toolStripProcessing";
            this.toolStripProcessing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProcessing.Size = new System.Drawing.Size(805, 17);
            this.toolStripProcessing.Spring = true;
            this.toolStripProcessing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripProcessing.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripProcessing.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(868, 0);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblTimeDuration
            // 
            this.lblTimeDuration.BackColor = System.Drawing.Color.Red;
            this.lblTimeDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTimeDuration.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblTimeDuration.ForeColor = System.Drawing.Color.White;
            this.lblTimeDuration.Location = new System.Drawing.Point(0, 24);
            this.lblTimeDuration.Name = "lblTimeDuration";
            this.lblTimeDuration.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTimeDuration.Size = new System.Drawing.Size(160, 42);
            this.lblTimeDuration.TabIndex = 7;
            this.lblTimeDuration.Text = "00:00:00";
            this.lblTimeDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SearchHistoryMenuItem
            // 
            this.SearchHistoryMenuItem.Name = "SearchHistoryMenuItem";
            this.SearchHistoryMenuItem.Size = new System.Drawing.Size(335, 22);
            this.SearchHistoryMenuItem.Text = "ประวัติการค้นหา S/N";
            this.SearchHistoryMenuItem.Click += new System.EventHandler(this.SearchHistoryMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 681);
            this.Controls.Add(this.lblTimeDuration);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SN";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dealerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem userManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macAddressAllowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToKnowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem businessTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem problemCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem salesAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionExtensionToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripProcessing;
        private System.Windows.Forms.ToolStripMenuItem preferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripStatusLabel toolStripInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripUserInfo;
        public System.Windows.Forms.Label lblTimeDuration;
        private System.Windows.Forms.ToolStripMenuItem supportStatMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calendarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaveCauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LeaveSummaryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SearchHistoryMenuItem;
    }
}

