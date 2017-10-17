using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using SN_Net.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GetMac;
using System.IO;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using SN_Net.Model;

namespace SN_Net.Subform
{
    public partial class LoginForm : Form
    {
        const int LOGIN_FAILED_USER_PASSWORD_INCORRECT = 100;
        const int LOGIN_FAILED_MAC_DENIED = 101;
        const int LOGIN_FAILED_USER_FORBIDDEN = 102;
        const int LOGIN_SUCCESS = 103;

        private MainForm main_form;
        public Boolean loged_in = false;
        public GlobalVar G = new GlobalVar();
        private string system_path;
        private string appdata_path;
        private Control current_focused_control;
        public users loged_in_user;

        public LoginForm(MainForm main_form = null)
        {
            this.main_form = main_form;
            InitializeComponent();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);

            this.ActiveControl = this.txtUser;

            //system_path = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            //appdata_path = Path.Combine(system_path, "SN_Net\\");
            //Console.WriteLine(appdata_path);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.txtUser.Enter += delegate
            {
                this.txtUser.SelectionStart = 0;
                this.txtUser.SelectionLength = this.txtUser.Text.Length;
            };
            this.txtPassword.Enter += delegate
            {
                this.txtPassword.SelectionStart = 0;
                this.txtPassword.SelectionLength = this.txtPassword.Text.Length;
            };

            this.txtUser.GotFocus += delegate
            {
                this.current_focused_control = this.txtUser;
            };
            this.txtPassword.GotFocus += delegate
            {
                this.current_focused_control = this.txtPassword;
            };
            this.btnLoginSubmit.GotFocus += delegate
            {
                this.current_focused_control = this.btnLoginSubmit;
            };
            this.btnLoginCancel.GotFocus += delegate
            {
                this.current_focused_control = this.btnLoginCancel;
            };
            this.btnPreference.GotFocus += delegate
            {
                this.current_focused_control = this.btnPreference;
            };
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            //if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SN_pref.txt")))
            //{
            //    this.txtUser.Focus();

            //    List<ModelMacData> mac = GetMac.GetMac.GetMACAddress();
            //    this.G.current_mac_address = mac.First<ModelMacData>().macAddress.Replace(":", "-");
            //}
            //else
            //{
            //    ApiMainUrlFirstSetting wind = new ApiMainUrlFirstSetting();
            //    if (wind.ShowDialog() == DialogResult.OK)
            //    {
            //        List<ModelMacData> mac = GetMac.GetMac.GetMACAddress();
            //        this.G.current_mac_address = mac.First<ModelMacData>().macAddress.Replace(":", "-");
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
            //}
        }

        private void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            //this.submitLogin();
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    var user = sn.users.Where(u => u.status == "N" && u.username == this.txtUser.Text.Trim()).FirstOrDefault();

                    if (user != null && user.userpassword.ExtractBytesString() == this.txtPassword.Text.Trim())
                    {
                        if (user.level < (int)USER_LEVEL.ADMIN)
                        {
                            if (DialogMacAddressAllowed.GetMacList().Where(m => m.mac_address.Trim() == this.main_form.mac_address).FirstOrDefault() == null)
                            {
                                MessageAlert.Show("คอมพิวเตอร์เครื่องนี้ยังไม่ได้รับอนุญาตให้เข้าระบบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                return;
                            }
                        }

                        this.loged_in_user = user;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageAlert.Show("รหัสผู้ใช้/รหัสผ่าน ไม่ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.InnerException.InnerException.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        //private void submitLogin()
        //{
        //    string json_data = "{\"username\":\"" + this.txtUser.Text.cleanString() + "\",";
        //    json_data += "\"userpassword\":\"" + this.txtPassword.Text.cleanString() + "\",";
        //    json_data += "\"mac_address\":\"" + this.G.current_mac_address + "\"}";

        //    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/validate_login", json_data);
        //    ServerResult res = JsonConvert.DeserializeObject<ServerResult>(post.data);

        //    switch (res.result)
        //    {
        //        case LOGIN_SUCCESS:
        //            Users user = res.users.First<Users>();

        //            Console.WriteLine("Login success");

        //            this.G.loged_in_user_id = user.id;
        //            this.G.loged_in_user_name = user.username;
        //            this.G.loged_in_user_realname = user.name;
        //            this.G.loged_in_user_email = user.email;
        //            this.G.loged_in_user_status = user.status;
        //            this.G.loged_in_user_level = user.level;
        //            this.G.loged_in_user_allowed_web_login = user.allowed_web_login;
        //            this.G.loged_in_user_training_expert = (user.training_expert == "Y" ? true : false);

        //            this.loged_in = true;
        //            this.DialogResult = DialogResult.OK;
        //            this.Close();
        //            break;

        //        case LOGIN_FAILED_MAC_DENIED:
        //            MessageAlert.Show(res.message, "Forbidden", MessageAlertButtons.OK, MessageAlertIcons.STOP);
        //            break;

        //        case LOGIN_FAILED_USER_FORBIDDEN:
        //            MessageAlert.Show(res.message, "Forbidden", MessageAlertButtons.OK, MessageAlertIcons.STOP);
        //            break;

        //        case LOGIN_FAILED_USER_PASSWORD_INCORRECT:
        //            MessageAlert.Show(res.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
        //            break;

        //        default:
        //            MessageAlert.Show(res.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
        //            break;
        //    }
        //}

        private void btnPreference_Click(object sender, EventArgs e)
        {
            PreferenceForm wind = new PreferenceForm();
            wind.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.current_focused_control is Button))
                {
                    if (this.current_focused_control == this.txtPassword)
                    {
                        //this.submitLogin();
                        this.btnLoginSubmit.PerformClick();
                        return true;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
