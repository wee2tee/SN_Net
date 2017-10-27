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
using SN_Net.Model;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.Reflection;
using GetMac;

namespace SN_Net
{
    public enum FORM_MODE
    {
        READ,
        ADD,
        EDIT,
        READ_ITEM,
        ADD_ITEM,
        EDIT_ITEM
    }

    public partial class MainForm : Form
    {
        public FormUsers form_users;
        public FormSn form_sn;
        public FormDealer form_dealer;
        public FormIstab form_area;
        public FormIstab form_busityp;
        public FormIstab form_howknown;
        public FormIstab form_probcod;
        public FormIstab form_verext;
        public FormIstab form_usergroup;
        public FormNote form_note;
        public string mac_address = string.Empty;

        public SnWindow sn_wind;
        public DealerWindow dealer_wind;
        public SupportNoteWindow supportnote_wind;
        public SupportStatWindow supportstat_wind;
        public Calendar2 calendar_wind;
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
        public users loged_in_user;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.toolStripInfo.Text = " [ ที่เก็บโปรแกรม : " + AppDomain.CurrentDomain.BaseDirectory + " ]";

            /* Check Preference Value */
            PreferenceValue pref = DialogPreference.GetPreference(DialogPreference.PREF_TYPE.MAIN_SN_DATA);
            PreferenceValue pref_note = DialogPreference.GetPreference(DialogPreference.PREF_TYPE.NOTE_DATA);
            if ((pref == null || (pref != null && DBX.TestConnection(pref) == false)) || (pref_note == null || (pref_note != null && DBX.TestConnection(pref_note) == false)))
            {
                DialogPreference p = new DialogPreference(this);
                if (p.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }

            LoginForm login = new LoginForm(this);
            if (login.ShowDialog() == DialogResult.Cancel/* || login.loged_in == false*/)
            {
                Application.Exit();
            }
            else
            {
                this.loged_in_user = login.loged_in_user;
                this.toolStripUserInfo.Text = this.loged_in_user.username;
                //this.G = login.G;
                //this.toolStripUserInfo.Text = "Login as : " + this.G.loged_in_user_name;
                //if (this.G.loged_in_user_level < GlobalVar.USER_LEVEL_ADMIN)
                //{
                //    this.macAddressAllowedToolStripMenuItem.Visible = false;
                //}
                //if (this.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR)
                //{
                //    this.userInformationToolStripMenuItem.Visible = false;
                //    this.supportStatMenuItem.Visible = false;
                //    this.SearchHistoryMenuItem.Visible = false;
                //    this.usersGroupMenuItem.Visible = false;
                //    this.preferenceToolStripMenuItem.Enabled = false;
                //}

                //this.loadDataResource();
                //this.sNToolStripMenuItem.PerformClick();

                this.mnuSN2.PerformClick();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.mac_address = GetMac.GetMac.GetMACAddress().First().macAddress;
            this.lblTimeDuration.Visible = false;
            this.RePositionLabelDuration();

            this.lblTimeDuration.Click += delegate
            {
                this.supportnote_wind.Activate();
            };

            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            this.lblVersion.Text = "V." + ver.ToString();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.RePositionLabelDuration();
        }

        private void sNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.sn_wind == null)
            //{
            //    this.sn_wind = new SnWindow(this);
            //    this.sn_wind.G = this.G;
            //    this.sn_wind.MdiParent = this;
            //    this.sn_wind.WindowState = FormWindowState.Maximized;
            //    this.sn_wind.Show();
            //}
            //else
            //{
            //    this.sn_wind.Activate();
            //    if (sn_wind.current_focused_control != null)
            //    {
            //        sn_wind.current_focused_control.Focus();
            //    }
            //}
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
            //if (this.area_wind == null)
            //{
            //    this.area_wind = new IstabWindow(this, Istab.TABTYP.AREA);
            //    this.area_wind.MdiParent = this;
            //    this.area_wind.Show();
            //}
            //else
            //{
            //    this.area_wind.Activate();
            //}

            if (this.form_area == null)
            {
                this.form_area = new FormIstab(this, istabDbf.TABTYP_AREA);
                this.form_area.MdiParent = this;
                this.form_area.Show();
            }
            else
            {
                this.form_area.Activate();
            }
        }

        private void versionExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.form_verext == null)
            {
                this.form_verext = new FormIstab(this, istabDbf.TABTYP_VEREXT);
                this.form_verext.MdiParent = this;
                this.form_verext.Show();
            }
            else
            {
                this.form_verext.Activate();
            }
        }

        private void howToKnowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.form_howknown == null)
            {
                this.form_howknown = new FormIstab(this, istabDbf.TABTYP_HOWKNOW);
                this.form_howknown.MdiParent = this;
                this.form_howknown.Show();
            }
            else
            {
                this.form_howknown.Activate();
            }
        }

        private void businessTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.form_busityp == null)
            {
                this.form_busityp = new FormIstab(this, istabDbf.TABTYP_BUSITYP);
                this.form_busityp.MdiParent = this;
                this.form_busityp.Show();
            }
            else
            {
                this.form_busityp.Activate();
            }
        }

        private void problemCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.form_probcod == null)
            {
                this.form_probcod = new FormIstab(this, istabDbf.TABTYP_PROBCOD, true);
                this.form_probcod.MdiParent = this;
                this.form_probcod.Show();
            }
            else
            {
                this.form_probcod.Activate();
            }
        }

        private void mnuIstabUserGroup_Click(object sender, EventArgs e)
        {
            if(this.form_usergroup == null)
            {
                this.form_usergroup = new FormIstab(this, istabDbf.TABTYP_USERGROUP, false);
                this.form_usergroup.MdiParent = this;
                this.form_usergroup.Show();
            }
            else
            {
                this.form_usergroup.Activate();
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
            //CRUDResult res = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "macallowed/get_all");

            //if (res.result)
            //{
            //    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(res.data);
            //    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            //    {
            //        MacAddressList wind = new MacAddressList();
            //        wind.G = this.G;
            //        wind.mac_data = sr.macallowed;

            //        wind.ShowDialog();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show(StringResource.CANNOT_CONNECT_TO_SERVER, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            DialogMacAddressAllowed m = new DialogMacAddressAllowed(this);
            m.ShowDialog();
        }

        private void userInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UsersList wind = new UsersList(this);
            //wind.ShowDialog();

            if(this.form_users == null)
            {
                this.form_users = new FormUsers(this);
                this.form_users.MdiParent = this;
                this.form_users.Show();
            }
            else
            {
                this.form_users.Activate();
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ChangePasswordForm wind = new ChangePasswordForm();
            //wind.G = this.G;
            //if (wind.ShowDialog() == DialogResult.OK)
            //{
            //    MessageAlert.Show("เปลี่ยนรหัสผ่านเรียบร้อย\nกรุณาออกจากระบบ และ ล็อกอินเข้าระบบใหม่อีกครั้ง", "Information", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
            //}

            DialogChangePassword cpwd = new DialogChangePassword(this, this.loged_in_user);
            if(cpwd.ShowDialog() == DialogResult.OK)
            {
                MessageAlert.Show("ท่านจะต้องปิดโปรแกรม และ ล็อกอินเข้าโปรแกรมใหม่อีกครั้ง", "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);
                Application.Exit();
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

        private void loadDataResource()
        {
            this.data_resource = new DataResource();
        }

        //private void toolStripInfo_TextChanged(object sender, EventArgs e)
        //{

        //}

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
                this.calendar_wind = new Calendar2(this);
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

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Test t = new Test();
            t.MdiParent = this;
            t.Show();
        }

        private void mnuImportData_Click(object sender, EventArgs e)
        {
            FormImportData im = new FormImportData();
            im.ShowDialog();
        }

        private void mnuSN2_Click(object sender, EventArgs e)
        {
            if(this.form_sn == null)
            {
                this.form_sn = new FormSn(this);
                this.form_sn.MdiParent = this;
                this.form_sn.Show();
                this.form_sn.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.form_sn.Activate();
                this.form_sn.WindowState = FormWindowState.Maximized;
            }
        }

        private void mnuDealer2_Click(object sender, EventArgs e)
        {
            if(this.form_dealer == null)
            {
                this.form_dealer = new FormDealer(this);
                this.form_dealer.MdiParent = this;
                this.form_dealer.Show();
            }
            else
            {
                this.form_dealer.Activate();
                this.form_dealer.WindowState = FormWindowState.Maximized;
            }
        }

        private void mnuPreference_Click(object sender, EventArgs e)
        {
            DialogPreference pf = new DialogPreference(this);
            pf.ShowDialog();
        }
    }
}
