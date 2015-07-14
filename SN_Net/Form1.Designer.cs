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
            this.userManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macAddressAllowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusUserLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sNToolStripMenuItem,
            this.dealerToolStripMenuItem,
            this.userManagementToolStripMenuItem,
            this.macAddressAllowedToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.modeToolStripMenuItem.Text = "File";
            // 
            // sNToolStripMenuItem
            // 
            this.sNToolStripMenuItem.Name = "sNToolStripMenuItem";
            this.sNToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.sNToolStripMenuItem.Text = "S/N";
            this.sNToolStripMenuItem.Click += new System.EventHandler(this.sNToolStripMenuItem_Click);
            // 
            // dealerToolStripMenuItem
            // 
            this.dealerToolStripMenuItem.Name = "dealerToolStripMenuItem";
            this.dealerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dealerToolStripMenuItem.Text = "Dealer";
            this.dealerToolStripMenuItem.Click += new System.EventHandler(this.dealerToolStripMenuItem_Click);
            // 
            // userManagementToolStripMenuItem
            // 
            this.userManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userInformationToolStripMenuItem,
            this.changePasswordToolStripMenuItem});
            this.userManagementToolStripMenuItem.Name = "userManagementToolStripMenuItem";
            this.userManagementToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.userManagementToolStripMenuItem.Text = "User management";
            // 
            // userInformationToolStripMenuItem
            // 
            this.userInformationToolStripMenuItem.Name = "userInformationToolStripMenuItem";
            this.userInformationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.userInformationToolStripMenuItem.Text = "User information";
            this.userInformationToolStripMenuItem.Click += new System.EventHandler(this.userInformationToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem.Text = "Change password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // macAddressAllowedToolStripMenuItem
            // 
            this.macAddressAllowedToolStripMenuItem.Name = "macAddressAllowedToolStripMenuItem";
            this.macAddressAllowedToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.macAddressAllowedToolStripMenuItem.Text = "MAC address allowed";
            this.macAddressAllowedToolStripMenuItem.Click += new System.EventHandler(this.macAddressAllowedToolStripMenuItem_Click);
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
            this.toolStripStatusUserLogin});
            this.statusStrip1.Location = new System.Drawing.Point(0, 619);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(952, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusUserLogin
            // 
            this.toolStripStatusUserLogin.Name = "toolStripStatusUserLogin";
            this.toolStripStatusUserLogin.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusUserLogin.Text = "...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(952, 0);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 641);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(875, 680);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SN";
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dealerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusUserLogin;
        private System.Windows.Forms.ToolStripMenuItem userManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macAddressAllowedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}

