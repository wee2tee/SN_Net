using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class ChangePasswordForm : Form
    {
        private int id;
        public GlobalVar G;

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void ChangePasswordForm_Shown(object sender, EventArgs e)
        {
            this.txtOldPassword.Focus();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            this.id = G.loged_in_user_id;
            this.txtUserName.Text = G.loged_in_user_name;

            EscapeKeyToCloseDialog.ActiveEscToClose(this);
            foreach (Control item in this.groupBox1.Controls)
            {
                item.KeyDown += new KeyEventHandler(this.enterKeyDetect);
            }
        }

        private void enterKeyDetect(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control curr_control = sender as Control;

                if (curr_control == this.txtNewPassword2)
                {
                    this.btnSubmitChangePassword.Focus();
                }
                else
                {
                    int curr_index = curr_control.TabIndex;

                    foreach (Control c in this.groupBox1.Controls)
                    {
                        if (c.TabIndex == curr_index + 1 && c.TabStop == true)
                        {
                            c.Focus();
                            if (c is ComboBox)
                            {
                                ComboBox combo = c as ComboBox;
                                combo.DroppedDown = true;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void btnSubmitChangePassword_Click(object sender, EventArgs e)
        {
            string old_password = this.txtOldPassword.Text;
            string new_password1 = this.txtNewPassword1.Text;
            string new_password2 = this.txtNewPassword2.Text;

            if (old_password.Length > 0 && new_password1.Length > 0 && new_password2.Length > 0)
            {
                if (new_password1 != new_password2)
                {
                    MessageAlert.Show("กรุณายืนยันรหัสผ่านใหม่ให้ถูกต้อง", "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                    this.txtNewPassword2.Focus();
                }
                else
                {
                    string json_data = "{\"id\":" + this.id + ",";
                    json_data += "\"old_password\":\"" + old_password.cleanString() + "\",";
                    json_data += "\"new_password1\":\"" + new_password1.cleanString() + "\",";
                    json_data += "\"new_password2\":\"" + new_password2.cleanString() + "\"}";

                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/change_password", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
            else
            {
                if (old_password.Length == 0)
                {
                    MessageAlert.Show("กรุณาป้อนรหัสผ่านเดิม", "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                    this.txtOldPassword.Focus();
                }
                else
                {
                    MessageAlert.Show("กรุณาป้อนรหัสผ่านใหม่ และ ยืนยันให้ถูกต้องตรงกัน", "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                    this.txtNewPassword1.Focus();
                }
            }
        }
    }
}
