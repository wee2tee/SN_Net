using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        public IstabWindow area_wind;
        public IstabWindow verext_wind;
        public IstabWindow howknown_wind;
        public IstabWindow busityp_wind;
        public IstabWindow probcode_wind;

        public string my_mac = string.Empty;
        private GlobalVar G;
        
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
                this.toolStripStatusUserLogin.Text = "Login as : " + this.G.loged_in_user_name;
                if (this.G.loged_in_user_level < 9)
                {
                    this.userInformationToolStripMenuItem.Visible = false;
                    this.macAddressAllowedToolStripMenuItem.Visible = false;
                }

                this.sNToolStripMenuItem.PerformClick();
                //this.dealerToolStripMenuItem.PerformClick();

                this.MinimumSize = new Size(100, 100);
            }
        }

        private void sNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.sn_wind == null)
            {
                this.sn_wind = new SnWindow();
                this.sn_wind.MdiParent = this;
                this.sn_wind.WindowState = FormWindowState.Maximized;
                this.sn_wind.Show();
            }
            else
            {
                this.sn_wind.Activate();
            }
        }

        private void dealerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dealer_wind == null)
            {
                this.dealer_wind = new DealerWindow();
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
                this.area_wind = new IstabWindow(Istab.TABTYP.AREA);
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
                this.verext_wind = new IstabWindow(Istab.TABTYP.VEREXT);
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
                this.howknown_wind = new IstabWindow(Istab.TABTYP.HOWKNOWN);
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
                this.busityp_wind = new IstabWindow(Istab.TABTYP.BUSITYP);
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
                this.probcode_wind = new IstabWindow(Istab.TABTYP.PROBLEM_CODE);
                this.probcode_wind.MdiParent = this;
                this.probcode_wind.Show();
            }
            else
            {
                this.probcode_wind.Activate();
            }
        }

        private void macAddressAllowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRUDResult res = ApiActions.GET(ApiConfig.API_MAIN_URL + "macallowed/get_all");

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
            UsersList wind = new UsersList();
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

        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x10)
        //    {
        //        //if(this.dealer_wind.State != FormState.FORM_STATE_READY)

        //        //Application.Exit();
        //    }
        //    base.WndProc(ref m);
        //}
    }
}
