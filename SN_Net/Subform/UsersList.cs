using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.DataModels;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public enum USER_LEVEL : int
    {
        SUPPORT = 0,
        SALES = 1,
        ACCOUNT = 2,
        SUPERVISOR = 8,
        ADMIN = 9
    }

    public partial class UsersList : Form
    {
        private List<Users> users;
        private MainForm main_form;

        public UsersList(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void UsersList_Load(object sender, EventArgs e)
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

            this.numMaxAbsent.Value = 10;
            this.numMaxAbsent.Enter += delegate
            {
                this.numMaxAbsent.Select(0, this.numMaxAbsent.Text.Length);
            };
            this.cbUserLevel.GotFocus += new EventHandler(this.ShowComboBoxItemOnFocused);
            this.cbUserStatus.GotFocus += new EventHandler(this.ShowComboBoxItemOnFocused);
            this.cbWebLogin.GotFocus += new EventHandler(this.ShowComboBoxItemOnFocused);

            this.loadUserListData();

            //foreach (Control ct in this.groupBox1.Controls)
            //{
            //    ct.KeyDown += new KeyEventHandler(this.enterKeyDetect);
            //}
        }

        private void ShowComboBoxItemOnFocused(object sender, EventArgs e)
        {
            SendKeys.Send("{F6}");
        }

        //private void enterKeyDetect(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        Control curr_control = sender as Control;
        //        int curr_index = curr_control.TabIndex;

        //        foreach (Control c in this.groupBox1.Controls)
        //        {
        //            if (c.TabIndex == curr_index + 1 && c.TabStop == true)
        //            {
        //                c.Focus();
        //                if (c is ComboBox)
        //                {
        //                    ComboBox combo = c as ComboBox;
        //                    combo.DroppedDown = true;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}

        private void btnCancelAddUser_Click(object sender, EventArgs e)
        {
            this.clearForm();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text.Length > 0 && this.txtEmail.Text.Length > 0)
            {
                string username = this.txtUserName.Text;
                string name = this.txtName.Text;
                string email = this.txtEmail.Text;
                int level = ((ComboboxItem)this.cbUserLevel.SelectedItem).int_value;
                string status = ((ComboboxItem)this.cbUserStatus.SelectedItem).string_value;
                string allowed_web_login = ((ComboboxItem)this.cbWebLogin.SelectedItem).string_value;
                string training_expert = this.chTrainingExpert.CheckState.ToYesOrNoString();
                int max_absent = (int)this.numMaxAbsent.Value;

                string json_data = "{\"username\":\"" + username.cleanString() + "\",";
                json_data += "\"name\":\"" + name.cleanString() + "\",";
                json_data += "\"email\":\"" + email.cleanString() + "\",";
                json_data += "\"level\":" + level + ",";
                json_data += "\"status\":\"" + status + "\",";
                json_data += "\"allowed_web_login\":\"" + allowed_web_login + "\",";
                json_data += "\"training_expert\":\"" + training_expert + "\",";
                json_data += "\"max_absent\":" + max_absent.ToString() + ",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.clearForm();
                    this.loadUserListData();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            else if(this.txtUserName.Text.Length == 0)
            {
                MessageAlert.Show("กรุณาป้อนชื่อผู้ใช้งาน", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.txtUserName.Focus();
            }
            else
            {
                MessageAlert.Show("กรุณาป้อนอีเมล์แอดเดรส", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.txtEmail.Focus();
            }
        }

        private void loadUserListData(int id = 0)
        {
            List<Users> users = new List<Users>();
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            switch (sr.result)
            {
                case ServerResult.SERVER_RESULT_SUCCESS:
                    this.users = sr.users;
                    // Clear old data
                    this.dgvUsers.Columns.Clear();
                    this.dgvUsers.Rows.Clear();

                    // Create column
                    // ID
                    DataGridViewTextBoxColumn text_col1 = new DataGridViewTextBoxColumn();
                    int c1 = this.dgvUsers.Columns.Add(text_col1);
                    this.dgvUsers.Columns[c1].HeaderText = "ID.";
                    this.dgvUsers.Columns[c1].Width = 40;
                    this.dgvUsers.Columns[c1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgvUsers.Columns[c1].Visible = false;

                    // username
                    DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
                    int c2 = this.dgvUsers.Columns.Add(text_col2);
                    this.dgvUsers.Columns[c2].HeaderText = "รหัสผู้ใช้";
                    this.dgvUsers.Columns[c2].Width = 110;
                    this.dgvUsers.Columns[c2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // name
                    DataGridViewTextBoxColumn text_col2_1 = new DataGridViewTextBoxColumn();
                    text_col2_1.HeaderText = "ชื่อ";
                    text_col2_1.Width = 110;
                    text_col2_1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgvUsers.Columns.Add(text_col2_1);

                    // email
                    DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
                    int c3 = this.dgvUsers.Columns.Add(text_col3);
                    this.dgvUsers.Columns[c3].HeaderText = "อีเมล์";
                    //this.dgvUsers.Columns[c3].Width = 160;
                    this.dgvUsers.Columns[c3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgvUsers.Columns[c3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    // level
                    DataGridViewTextBoxColumn text_col4 = new DataGridViewTextBoxColumn();
                    int c4 = this.dgvUsers.Columns.Add(text_col4);
                    this.dgvUsers.Columns[c4].HeaderText = "ระดับผู้ใช้";
                    this.dgvUsers.Columns[c4].Width = 100;
                    this.dgvUsers.Columns[c4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // status
                    DataGridViewTextBoxColumn text_col5 = new DataGridViewTextBoxColumn();
                    int c5 = this.dgvUsers.Columns.Add(text_col5);
                    this.dgvUsers.Columns[c5].HeaderText = "สถานะ";
                    this.dgvUsers.Columns[c5].Width = 50;
                    this.dgvUsers.Columns[c5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // allowed_web_login
                    DataGridViewTextBoxColumn text_col6 = new DataGridViewTextBoxColumn();
                    int c6 = this.dgvUsers.Columns.Add(text_col6);
                    this.dgvUsers.Columns[c6].HeaderText = "Web UI";
                    this.dgvUsers.Columns[c6].Width = 80;
                    this.dgvUsers.Columns[c6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // is training_expert
                    DataGridViewTextBoxColumn text_col7 = new DataGridViewTextBoxColumn();
                    int c7 = this.dgvUsers.Columns.Add(text_col7);
                    this.dgvUsers.Columns[c7].HeaderText = "วิทยากร";
                    this.dgvUsers.Columns[c7].Width = 50;
                    this.dgvUsers.Columns[c7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // max_absent
                    DataGridViewTextBoxColumn text_col8 = new DataGridViewTextBoxColumn();
                    text_col8.HeaderText = "วันลา";
                    text_col8.Width = 50;
                    text_col8.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvUsers.Columns.Add(text_col8);

                    // create_at
                    DataGridViewTextBoxColumn text_col9 = new DataGridViewTextBoxColumn();
                    int c9 = this.dgvUsers.Columns.Add(text_col9);
                    this.dgvUsers.Columns[c9].HeaderText = "สร้างเมื่อ";
                    this.dgvUsers.Columns[c9].Width = 140;
                    this.dgvUsers.Columns[c9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // update_at
                    DataGridViewTextBoxColumn text_col10 = new DataGridViewTextBoxColumn();
                    int c10 = this.dgvUsers.Columns.Add(text_col10);
                    this.dgvUsers.Columns[c10].HeaderText = "ใช้งานล่าสุด";
                    this.dgvUsers.Columns[c10].Width = 140;
                    this.dgvUsers.Columns[c10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Create data row
                    foreach (Users user in this.users)
                    {
                        int r = this.dgvUsers.Rows.Add();
                        this.dgvUsers.Rows[r].Tag = (int)user.id;

                        this.dgvUsers.Rows[r].Cells[0].ValueType = typeof(int);
                        this.dgvUsers.Rows[r].Cells[0].Value = user.id;
                        this.dgvUsers.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                        this.dgvUsers.Rows[r].Cells[1].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[1].Value = user.username;

                        this.dgvUsers.Rows[r].Cells[2].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[2].Value = user.name;

                        this.dgvUsers.Rows[r].Cells[3].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[3].Value = user.email;

                        this.dgvUsers.Rows[r].Cells[4].ValueType = typeof(int);
                        this.dgvUsers.Rows[r].Cells[4].Value = ComboboxItem.GetItemText(this.cbUserLevel, user.level);
                        this.dgvUsers.Rows[r].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        this.dgvUsers.Rows[r].Cells[5].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[5].Value = ComboboxItem.GetItemText(this.cbUserStatus, user.status);
                        this.dgvUsers.Rows[r].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        this.dgvUsers.Rows[r].Cells[6].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[6].Value = ComboboxItem.GetItemText(this.cbWebLogin, user.allowed_web_login);
                        this.dgvUsers.Rows[r].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        this.dgvUsers.Rows[r].Cells[7].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[7].Value = user.training_expert;
                        this.dgvUsers.Rows[r].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        this.dgvUsers.Rows[r].Cells[7].Style.ForeColor = (user.training_expert == "Y" ? Color.Black : Color.LightGray);

                        this.dgvUsers.Rows[r].Cells[8].ValueType = typeof(int);
                        this.dgvUsers.Rows[r].Cells[8].Value = user.max_absent;
                        this.dgvUsers.Rows[r].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                        this.dgvUsers.Rows[r].Cells[9].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[9].Value = user.create_at;

                        this.dgvUsers.Rows[r].Cells[10].ValueType = typeof(string);
                        this.dgvUsers.Rows[r].Cells[10].Value = user.last_use;
                    }

                    // Set selection item
                    if (id > 0)
                    {
                        foreach (DataGridViewRow row in this.dgvUsers.Rows)
                        {
                            if ((int)row.Tag == id)
                            {
                                row.Cells[1].Selected = true;
                            }
                        }
                    }
                    break;

                default:
                    DialogResult dlg_res = MessageAlert.Show(sr.message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR);
                    if (dlg_res == DialogResult.Retry)
                    {
                        this.loadUserListData();
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void clearForm()
        {
            this.txtUserName.Text = "";
            this.txtEmail.Text = "";
            this.cbUserLevel.SelectedItem = this.cbUserLevel.Items[2];
            this.cbUserStatus.SelectedItem = this.cbUserStatus.Items[0];
            this.cbWebLogin.SelectedItem = this.cbWebLogin.Items[0];
        }

        // Data grid view context menu
        private void dgvUsers_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = this.dgvUsers.HitTest(e.X, e.Y).RowIndex;
                this.dgvUsers.Rows[currentMouseOverRow].Selected = true;

                ContextMenu m = new ContextMenu();
                MenuItem mnu_edit = new MenuItem("แก้ไข");
                mnu_edit.Tag = (int)this.dgvUsers.Rows[currentMouseOverRow].Cells[0].Value;
                mnu_edit.Click += this.editUsers;
                m.MenuItems.Add(mnu_edit);

                MenuItem mnu_delete = new MenuItem("ลบ");
                mnu_delete.Tag = (int)this.dgvUsers.Rows[currentMouseOverRow].Cells[0].Value;
                mnu_delete.Click += this.deleteUser;
                m.MenuItems.Add(mnu_delete);

                MenuItem mnu_reset_pwd = new MenuItem("รีเซ็ตรหัสผ่านผู้ใช้รายนี้");
                mnu_reset_pwd.Tag = (int)this.dgvUsers.Rows[currentMouseOverRow].Tag;
                mnu_reset_pwd.Click += this.confirmResetPassword;
                m.MenuItems.Add(mnu_reset_pwd);

                //// Adding some phrase at the bottom of context menu
                //if (currentMouseOverRow >= 0)
                //{
                //    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                //}

                m.Show(this.dgvUsers, new Point(e.X, e.Y));
            }
        }

        private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
        {
            //int id = (int)this.dgvUsers.CurrentRow.Tag;
            int id = (int)this.dgvUsers.Rows[this.dgvUsers.CurrentCell.RowIndex].Tag;

            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Alt)
            {
                this.showEditForm(id);
            }
            else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Alt)
            {
                this.confirmDeleteUser(id);
            }
        }

        private void deleteUser(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            int id = (int)mi.Tag;
            this.confirmDeleteUser(id);
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int id = (int)this.dgvUsers.Rows[e.RowIndex].Tag;
                this.showEditForm(id);
            }
        }

        private void editUsers(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            int id = (int)mi.Tag;
            this.showEditForm(id);
        }

        private void showEditForm(int id)
        {
            UsersEditForm wind = new UsersEditForm(this.main_form);
            Console.WriteLine("id : " + id.ToString());
            wind.id = id;
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.loadUserListData(id);
            }
        }

        private void confirmDeleteUser(int id)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "users/delete&id=" + id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.loadUserListData();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void confirmResetPassword(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            int id = (int)mi.Tag;

            if (MessageAlert.Show("ต้องการรีเซ็ตรหัสผ่านผู้ใช้รายนี้ใช่หรือไม่?", "", MessageAlertButtons.YES_NO, MessageAlertIcons.QUESTION) == DialogResult.Yes)
            {
                string json_data = "{\"id\":" + id + "}";
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/reset_password", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    MessageAlert.Show(sr.message, "Process complete", MessageAlertButtons.OK);
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.btnAddUser.Focused || this.btnCancelAddUser.Focused))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.F6)
            {
                if (this.cbUserLevel.Focused || this.cbUserStatus.Focused || this.cbWebLogin.Focused)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static List<Users> GetUsers()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            return sr.users;
            //if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            //{
            //    return sr.users;
            //}
            //else
            //{
            //    return new List<Users>();
            //}
        }
    }
}
