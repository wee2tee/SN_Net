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
        private Users current_user;
        private Control current_focused_control;
        private MainForm main_form;

        public UsersEditForm(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void UsersEditForm_Load(object sender, EventArgs e)
        {
            // Adding users level selection
            this.cbUserLevel.Items.Add(new ComboboxItem("ADMIN", 9, ""));
            this.cbUserLevel.Items.Add(new ComboboxItem("SUPERVISOR", 8, ""));
            this.cbUserLevel.Items.Add(new ComboboxItem("SUPPORT", 0, ""));
            this.cbUserLevel.Items.Add(new ComboboxItem("SALES", 1, ""));
            this.cbUserLevel.Items.Add(new ComboboxItem("ACCOUNT", 2, ""));
            this.cbUserLevel.SelectedItem = this.cbUserLevel.Items[2];

            // Adding users status selection
            this.cbUserStatus.Items.Add(new ComboboxItem("ปกติ", 0, "N"));
            this.cbUserStatus.Items.Add(new ComboboxItem("ห้ามใช้", 0, "X"));
            this.cbUserStatus.SelectedItem = this.cbUserStatus.Items[0];

            // Adding allow web login selection
            this.cbWebLogin.Items.Add(new ComboboxItem("No", 0, "N"));
            this.cbWebLogin.Items.Add(new ComboboxItem("Yes", 0, "Y"));
            this.cbWebLogin.SelectedItem = this.cbWebLogin.Items[0];

            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_at&id=" + this.id);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.current_user = sr.users.First<Users>();
                Users user = sr.users.First<Users>();

                this.txtUserName.Text = user.username;
                this.txtName.Text = user.name;
                this.txtEmail.Text = user.email;
                this.cbUserLevel.SelectedItem = this.cbUserLevel.Items[ComboboxItem.GetItemIndex(this.cbUserLevel, user.level)];
                this.cbUserStatus.SelectedItem = this.cbUserStatus.Items[ComboboxItem.GetItemIndex(this.cbUserStatus, user.status)];
                this.cbWebLogin.SelectedItem = this.cbWebLogin.Items[ComboboxItem.GetItemIndex(this.cbWebLogin, user.allowed_web_login)];
                this.chTrainingExpert.CheckState = (user.training_expert == "Y" ? CheckState.Checked : CheckState.Unchecked);
                this.numMaxAbsent.Value = user.max_absent;
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
                ct.GotFocus += delegate
                {
                    this.current_focused_control = ct;
                };
            }
            this.btnSubmitChangeUser.GotFocus += delegate
            {
                this.current_focused_control = this.btnSubmitChangeUser;
            };
            this.btnCancelSubmitChangeUser.GotFocus += delegate
            {
                this.current_focused_control = this.btnCancelSubmitChangeUser;
            };
            this.numMaxAbsent.GotFocus += delegate
            {
                this.numMaxAbsent.Select(0, this.numMaxAbsent.Text.Length);
            };
        }

        private void btnCancelSubmitChangeUser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSubmitChangeUser_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text;
            string name = this.txtName.Text;
            string email = this.txtEmail.Text;
            int level = ((ComboboxItem)this.cbUserLevel.SelectedItem).int_value;
            string status = ((ComboboxItem)this.cbUserStatus.SelectedItem).string_value;
            string allowed_web_login = ((ComboboxItem)this.cbWebLogin.SelectedItem).string_value;
            string training_expert = this.chTrainingExpert.CheckState.ToYesOrNoString();
            int max_absent = (int)this.numMaxAbsent.Value;

            string json_data = "{\"id\":" + this.id + ",";
            json_data += "\"username\":\"" + username.cleanString() + "\",";
            json_data += "\"name\":\"" + name.cleanString() + "\",";
            json_data += "\"email\":\"" + email.cleanString() + "\",";
            json_data += "\"level\":" + level + ",";
            json_data += "\"usergroup\":\"" + this.current_user.usergroup + "\",";
            json_data += "\"status\":\"" + status + "\",";
            json_data += "\"allowed_web_login\":\"" + allowed_web_login + "\",";
            json_data += "\"training_expert\":\"" + training_expert + "\",";
            json_data += "\"max_absent\":" + max_absent.ToString() + ",";
            json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/update", json_data);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.current_focused_control is Button))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
