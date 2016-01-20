using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SN_Net.MiscClass;
using SN_Net.Subform;
using SN_Net.DataModels;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net
{
    public partial class MainForm : Form
    {
        public SnWindow sn_wind;
        public DealerWindow dealer_wind;
        public SupportNoteWindow supportnote_wind;
        public SupportStatWindow supportstat_wind;
        public CalendarWindow calendar_wind;
        public LeaveWindow leave_wind;
        public IstabWindow area_wind;
        public IstabWindow verext_wind;
        public IstabWindow howknown_wind;
        public IstabWindow busityp_wind;
        public IstabWindow probcode_wind;
        public IstabWindow leavecause_wind;
        public IstabWindow servicecase_wind;
        public SearchHistory searchhistory_wind;
        public UsersGroupWindow usersgroup_wind;
        
        public string my_mac = string.Empty;
        public GlobalVar G;
        public DataResource data_resource;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            if (login.ShowDialog() == DialogResult.Cancel || login.loged_in == false)
            {
                Application.Exit();
            }
            else
            {
                this.G = login.G;
                this.toolStripUserInfo.Text = "Login as : " + this.G.loged_in_user_name;
                if (this.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
                {
                    this.macAddressAllowedToolStripMenuItem.Visible = false;
                }
                if (this.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR)
                {
                    this.userInformationToolStripMenuItem.Visible = false;
                    this.supportStatMenuItem.Visible = false;
                    this.SearchHistoryMenuItem.Visible = false;
                    this.usersGroupMenuItem.Visible = false;
                    this.preferenceToolStripMenuItem.Enabled = false;
                }

                this.loadDataResource();
                this.sNToolStripMenuItem.PerformClick();

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.lblTimeDuration.Visible = false;
            this.RePositionLabelDuration();

            this.lblTimeDuration.Click += delegate
            {
                this.supportnote_wind.Activate();
            };
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.RePositionLabelDuration();
        }

        private void sNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sn_wind == null)
            {
                this.sn_wind = new SnWindow(this);
                this.sn_wind.G = this.G;
                this.sn_wind.MdiParent = this;
                this.sn_wind.WindowState = FormWindowState.Maximized;
                this.sn_wind.Show();
            }
            else
            {
                this.sn_wind.Activate();
                if (sn_wind.current_focused_control != null)
                {
                    sn_wind.current_focused_control.Focus();
                }
            }
        }

        private void dealerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dealer_wind == null)
            {
                this.dealer_wind = new DealerWindow(this);
                this.dealer_wind.MdiParent = this;
                this.dealer_wind.WindowState = FormWindowState.Maximized;
                this.dealer_wind.Show();
            }
            else
            {
                this.dealer_wind.Activate();
            }
        }

        private void salesAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.area_wind == null)
            {
                this.area_wind = new IstabWindow(this, Istab.TABTYP.AREA);
                this.area_wind.MdiParent = this;
                this.area_wind.Show();
            }
            else
            {
                this.area_wind.Activate();
            }
        }

        private void versionExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.verext_wind == null)
            {
                this.verext_wind = new IstabWindow(this, Istab.TABTYP.VEREXT);
                this.verext_wind.MdiParent = this;
                this.verext_wind.Show();
            }
            else
            {
                this.verext_wind.Activate();
            }
        }

        private void howToKnowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.howknown_wind == null)
            {
                this.howknown_wind = new IstabWindow(this, Istab.TABTYP.HOWKNOWN);
                this.howknown_wind.MdiParent = this;
                this.howknown_wind.Show();
            }
            else
            {
                this.howknown_wind.Activate();
            }
        }

        private void businessTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.busityp_wind == null)
            {
                this.busityp_wind = new IstabWindow(this, Istab.TABTYP.BUSITYP);
                this.busityp_wind.MdiParent = this;
                this.busityp_wind.Show();
            }
            else
            {
                this.busityp_wind.Activate();
            }
        }

        private void problemCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.probcode_wind == null)
            {
                this.probcode_wind = new IstabWindow(this, Istab.TABTYP.PROBLEM_CODE);
                this.probcode_wind.MdiParent = this;
                this.probcode_wind.Show();
            }
            else
            {
                this.probcode_wind.Activate();
            }
        }


        private void leaveCauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.leavecause_wind == null)
            {
                this.leavecause_wind = new IstabWindow(this, Istab.TABTYP.ABSENT_CAUSE);
                this.leavecause_wind.MdiParent = this;
                this.leavecause_wind.Show();
            }
            else
            {
                this.leavecause_wind.Activate();
            }
        }

        private void serviceCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.servicecase_wind == null)
            {
                this.servicecase_wind = new IstabWindow(this, Istab.TABTYP.SERVICE_CASE);
                this.servicecase_wind.MdiParent = this;
                this.servicecase_wind.Show();
            }
            else
            {
                this.servicecase_wind.Activate();
            }
        }

        private void macAddressAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRUDResult res = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "macallowed/get_all");

            if (res.result)
            {
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(res.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    MacAddressList wind = new MacAddressList();
                    wind.G = this.G;
                    wind.mac_data = sr.macallowed;

                    wind.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(StringResource.CANNOT_CONNECT_TO_SERVER, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersList wind = new UsersList(this);
            wind.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm wind = new ChangePasswordForm();
            wind.G = this.G;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                MessageAlert.Show("เปลี่ยนรหัสผ่านเรียบร้อย\nกรุณาออกจากระบบ และ ล็อกอินเข้าระบบใหม่อีกครั้ง", "Information", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
            }
        }

        private void preferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferenceForm wind = new PreferenceForm();
            wind.ShowDialog();
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            //ToolStripManager.RevertMerge(this.toolStrip1);

            //if (this.ActiveMdiChild is SnWindow)
            //{
            //    SnWindow wind = ActiveMdiChild as SnWindow;
            //    if (wind != null)
            //    {
            //        wind.collapseToolstrip();
            //        ToolStripManager.Merge(wind.toolStrip1, this.toolStrip1);
            //    }
            //}
            //else if (this.ActiveMdiChild is DealerWindow)
            //{
            //    DealerWindow wind = ActiveMdiChild as DealerWindow;
            //    if (wind != null)
            //    {
            //        wind.collapseToolstrip();
            //        ToolStripManager.Merge(wind.toolStrip1, this.toolStrip1);
            //    }
            //}

            //if (this.toolStrip1.Items.Count > 0)
            //{
            //    this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 40);
            //}
            //else
            //{
            //    this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 0);
            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Console.WriteLine()

            //e.Cancel = true;
            //if (((MainForm)sender).MdiChildren.Count() > 0)
            //{
            //    foreach (Form mdichild in ((MainForm)sender).MdiChildren)
            //    {
            //        if (mdichild is SnWindow)
            //        {
            //            if (((SnWindow)mdichild).State != FormState.FORM_STATE_READY)
            //            {
            //                mdichild.Activate();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

        private void loadDataResource()
        {
            this.data_resource = new DataResource();
        }

        private void toolStripInfo_TextChanged(object sender, EventArgs e)
        {
            if (((ToolStripStatusLabel)sender).Text.Length > 0)
            {
                this.toolStripUserInfo.Visible = false;
            }
            else
            {
                this.toolStripUserInfo.Visible = true;
            }
        }

        private void RePositionLabelDuration()
        {
            this.lblTimeDuration.SetBounds(this.ClientSize.Width - (this.lblTimeDuration.Width), this.lblTimeDuration.Location.Y, this.lblTimeDuration.ClientSize.Width, this.lblTimeDuration.ClientSize.Height);
        }

        private void supportStatMenuItem_Click(object sender, EventArgs e)
        {
            if (this.supportstat_wind == null)
            {
                LeaveRangeDialog dlg = new LeaveRangeDialog(this);
                dlg.Text = "กำหนดขอบเขตการแสดงข้อมูลการปฏิบัติงาน(Support)";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SupportStatWindow wind = new SupportStatWindow(this, dlg.user_from, dlg.user_to, dlg.date_from, dlg.date_to);
                    wind.MdiParent = this;
                    wind.Show();
                }
            }
            else
            {
                this.supportstat_wind.Activate();
            }
        }

        private void leaveSummaryMenuItem_Click(object sender, EventArgs e)
        {
            if (this.leave_wind == null)
            {
                LeaveRangeDialog dlg = new LeaveRangeDialog(this);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LeaveWindow wind = new LeaveWindow(this, dlg.user_from, dlg.user_to, dlg.date_from, dlg.date_to);
                    wind.MdiParent = this;
                    wind.Show();
                }
            }
            else
            {
                this.leave_wind.Activate();
            }
        }

        private void SearchHistoryMenuItem_Click(object sender, EventArgs e)
        {
            if (this.searchhistory_wind == null)
            {
                this.searchhistory_wind = new SearchHistory(this);
                searchhistory_wind.MdiParent = this;
                searchhistory_wind.Show();
            }
            else
            {
                this.searchhistory_wind.Activate();
            }
        }

        private void calendarMenuItem_Click(object sender, EventArgs e)
        {
            if (this.calendar_wind == null)
            {
                this.calendar_wind = new CalendarWindow(this);
                this.calendar_wind.MdiParent = this;
                this.calendar_wind.Show();
            }
            else
            {
                this.calendar_wind.Activate();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TestControl w = new TestControl();
            //w.ShowDialog();
            UsersGroupWindow win = new UsersGroupWindow(this);
            win.ShowDialog();
        }

        private void usersGroupMenuItem_Click(object sender, EventArgs e)
        {
            if (this.usersgroup_wind == null)
            {
                this.usersgroup_wind = new UsersGroupWindow(this);
                this.usersgroup_wind.MdiParent = this;
                this.usersgroup_wind.Show();
            }
            else{
                this.usersgroup_wind.Activate();
            }
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLog cl = new ChangeLog();
            cl.ShowDialog();
        }
    }
}
