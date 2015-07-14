using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebAPI;
using WebAPI.ApiResult;
using SN_Net.MiscClass;
using SN_Net.DataModels;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class UsersEditForm : Form
    {
        public int id;

        public UsersEditForm()
        {
            InitializeComponent();
        }

        private void UsersEditForm_Load(object sender, EventArgs e)
        {
            // Adding users level selection
            this.cbUserLevel.Items.Add(new ComboboxItem("ADMIN", 9, ""));
            this.cbUserLevel.Items.Add(new ComboboxItem("USER", 0, ""));
            this.cbUserLevel.SelectedItem = this.cbUserLevel.Items[1];

            // Adding users status selection
            this.cbUserStatus.Items.Add(new ComboboxItem("ปกติ", 0, "N"));
            this.cbUserStatus.Items.Add(new ComboboxItem("ห้ามใช้", 0, "X"));
            this.cbUserStatus.SelectedItem = this.cbUserStatus.Items[0];

            // Adding allow web login selection
            this.cbWebLogin.Items.Add(new ComboboxItem("No", 0, "N"));
            this.cbWebLogin.Items.Add(new ComboboxItem("Yes", 0, "Y"));
            this.cbWebLogin.SelectedItem = this.cbWebLogin.Items[0];


            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "users/get_at&id=" + this.id);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                Users user = sr.users.First<Users>();

                this.txtUserName.Text = user.username;
                this.txtEmail.Text = user.email;
                this.cbUserLevel.SelectedItem = this.cbUserLevel.Items[ComboboxItem.GetItemIndex(this.cbUserLevel, user.level)];
                this.cbUserStatus.SelectedItem = this.cbUserStatus.Items[ComboboxItem.GetItemIndex(this.cbUserStatus, user.status)];
                this.cbWebLogin.SelectedItem = this.cbWebLogin.Items[ComboboxItem.GetItemIndex(this.cbWebLogin, user.allowed_web_login)];
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            this.txtEmail.Focus();

            foreach (Control ct in this.groupBox1.Controls)
            {
                ct.KeyDown += new KeyEventHandler(this.enterKeyDetect);
            }

            EscapeKeyToCloseDialog.ActiveEscToClose(this);
            
        }

        private void enterKeyDetect(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control curr_control = sender as Control;
                int curr_index = curr_control.TabIndex;

                if(curr_control.Name == this.cbUserStatus.Name){
                    this.btnSubmitChangeUser.Select();
                }
                else
                {
                    foreach (Control c in this.groupBox1.Controls)
                    {
                        if (c.TabIndex == curr_index + 1 && c.TabStop == true)
                        {
                            c.Select();
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

        private void btnCancelSubmitChangeUser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSubmitChangeUser_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text;
            string email = this.txtEmail.Text;
            int level = ((ComboboxItem)this.cbUserLevel.SelectedItem).int_value;
            string status = ((ComboboxItem)this.cbUserStatus.SelectedItem).string_value;
            string allowed_web_login = ((ComboboxItem)this.cbWebLogin.SelectedItem).string_value;

            string json_data = "{\"id\":" + this.id + ",";
            json_data += "\"username\":\"" + username.cleanString() + "\",";
            json_data += "\"email\":\"" + email.cleanString() + "\",";
            json_data += "\"level\":" + level + ",";
            json_data += "\"status\":\"" + status + "\",";
            json_data += "\"allowed_web_login\":\"" + allowed_web_login + "\"}";

            CRUDResult post = ApiActions.POST(ApiConfig.API_MAIN_URL + "users/update", json_data);
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
}
