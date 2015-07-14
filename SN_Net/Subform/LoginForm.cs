﻿using System;
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

namespace SN_Net.Subform
{
    public partial class LoginForm : Form
    {
        const int LOGIN_FAILED_USER_PASSWORD_INCORRECT = 100;
        const int LOGIN_FAILED_MAC_DENIED = 101;
        const int LOGIN_FAILED_USER_FORBIDDEN = 102;
        const int LOGIN_SUCCESS = 103;

        public Boolean loged_in = false;
        public GlobalVar G = new GlobalVar();

        public LoginForm()
        {
            InitializeComponent();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.txtUser.Focus();

            List<ModelMacData> mac = GetMac.GetMac.GetMACAddress();
            this.G.current_mac_address = mac.First<ModelMacData>().macAddress.Replace(":", "-");
        }

        private void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            this.submitLogin();
        }

        private void submitLogin()
        {
            string json_data = "{\"username\":\"" + this.txtUser.Text.cleanString() + "\",";
            json_data += "\"userpassword\":\"" + this.txtPassword.Text.cleanString() + "\",";
            json_data += "\"mac_address\":\"" + this.G.current_mac_address + "\"}";

            CRUDResult post = ApiActions.POST(ApiConfig.API_MAIN_URL + "users/validate_login", json_data);
            ServerResult res = JsonConvert.DeserializeObject<ServerResult>(post.data);

            switch (res.result)
            {
                case LOGIN_SUCCESS:
                    Users user = res.users.First<Users>();

                    Console.WriteLine("Login success");

                    this.G.loged_in_user_id = user.id;
                    this.G.loged_in_user_name = user.username;
                    this.G.loged_in_user_email = user.email;
                    this.G.loged_in_user_status = user.status;
                    this.G.loged_in_user_level = user.level;
                    this.G.loged_in_user_allowed_web_login = user.allowed_web_login;

                    this.loged_in = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case LOGIN_FAILED_MAC_DENIED:
                    MessageAlert.Show(res.message, "Forbidden", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    break;

                case LOGIN_FAILED_USER_FORBIDDEN:
                    MessageAlert.Show(res.message, "Forbidden", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    break;

                case LOGIN_FAILED_USER_PASSWORD_INCORRECT:
                    MessageAlert.Show(res.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    break;

                default:
                    MessageAlert.Show(res.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    break;
            }
        }



        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.submitLogin();
            }
        }

    }
}
