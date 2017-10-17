using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;

namespace SN_Net.Subform
{
    public partial class FormUsers : Form
    {
        private MainForm main_form;
        private FORM_MODE form_mode;
        private BindingList<usersVM> users_list;
        private users tmp_users;
        
        public FormUsers(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            this.btnReload.PerformClick();
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.HideInlineForm();
            this.SetDropdownItem();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!(this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM))
            {
                if (MessageAlert.Show("ข้อมูลที่กำลังเพิ่ม/แก้ไข จะไม่ถูกบันทึก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }

            this.main_form.form_users = null;
            base.OnClosing(e);
        }

        public static List<users> GetUsersList()
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.users.OrderBy(u => u.username).ToList();
            }
        }

        public static users GetUsers(int users_id)
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.users.Find(users_id);
            }
        }

        private void SetDropdownItem()
        {
            this.inlineLevel._Items.Add(new XDropdownListItem { Value = 0, Text = "Support" });
            this.inlineLevel._Items.Add(new XDropdownListItem { Value = 1, Text = "Sales" });
            this.inlineLevel._Items.Add(new XDropdownListItem { Value = 2, Text = "Account" });
            this.inlineLevel._Items.Add(new XDropdownListItem { Value = 8, Text = "Supervisor" });
            if (this.main_form.loged_in_user.level == (int)USER_LEVEL.ADMIN)
                this.inlineLevel._Items.Add(new XDropdownListItem { Value = 9, Text = "Admin" });

            this.inlineGroup._Items.Add(new XDropdownListItem { Value = null, Text = "N/A" });
            using (snEntities sn = DBX.DataSet())
            {
                foreach (var i in FormIstab.GetIstabList(istabDbf.TABTYP_USERGROUP))
                {
                    this.inlineGroup._Items.Add(new XDropdownListItem { Value = i.id, Text = i.abbreviate_th });
                }
            }

            this.inlineStatus._Items.Add(new XDropdownListItem { Value = "N", Text = "ปกติ" });
            this.inlineStatus._Items.Add(new XDropdownListItem { Value = "X", Text = "ห้ามใช้" });

            this.inlineTrainer._Items.Add(new XDropdownListItem { Value = "N", Text = "N" });
            this.inlineTrainer._Items.Add(new XDropdownListItem { Value = "Y", Text = "Y" });
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnResetPassword.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnReload.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);

            this.dgv.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.inlineUserName.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM }, this.form_mode);
            this.inlineName.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineLevel.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineGroup.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineStatus.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineTrainer.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineMaxAbsent.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);

            this.btnResetPassword.Visible = this.main_form.loged_in_user.level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
        }

        private void ShowInlineForm(DataGridViewRow row)
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.tmp_users = (users)row.Cells[this.col_users.Name].Value;

            this.SetInlineFormPosition(row);
            this.inlineUserName._Text = this.tmp_users.username;
            this.inlineName._Text = this.tmp_users.name;
            this.inlineLevel._SelectedItem = this.inlineLevel._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_users.level).First();
            this.inlineGroup._SelectedItem = this.inlineGroup._Items.Cast<XDropdownListItem>().Where(i => (int?)i.Value == this.tmp_users.usergroup_id).First();
            this.inlineStatus._SelectedItem = this.inlineStatus._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_users.status).First();
            this.inlineTrainer._SelectedItem = this.inlineTrainer._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_users.training_expert).First();
            this.inlineMaxAbsent._Value = this.tmp_users.max_absent;
        }

        private void HideInlineForm()
        {
            this.inlineUserName.SetBounds(-99999, 0, this.inlineUserName.Width, this.inlineUserName.Height);
            this.inlineName.SetBounds(-99999, 0, this.inlineName.Width, this.inlineName.Height);
            this.inlineLevel.SetBounds(-99999, 0, this.inlineLevel.Width, this.inlineLevel.Height);
            this.inlineGroup.SetBounds(-99999, 0, this.inlineGroup.Width, this.inlineGroup.Height);
            this.inlineStatus.SetBounds(-99999, 0, this.inlineStatus.Width, this.inlineStatus.Height);
            this.inlineTrainer.SetBounds(-99999, 0, this.inlineTrainer.Width, this.inlineTrainer.Height);
            this.inlineMaxAbsent.SetBounds(-99999, 0, this.inlineMaxAbsent.Width, this.inlineMaxAbsent.Height);

            this.tmp_users = null;
        }

        private void SetInlineFormPosition(DataGridViewRow row)
        {
            if (this.tmp_users != null && (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
            {
                int col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_username.Name).First().Index;
                if (this.form_mode == FORM_MODE.ADD_ITEM)
                    this.inlineUserName.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_name.Name).First().Index;
                this.inlineName.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_level.Name).First().Index;
                this.inlineLevel.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_usergroup.Name).First().Index;
                this.inlineGroup.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_status.Name).First().Index;
                this.inlineStatus.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_training_expert.Name).First().Index;
                this.inlineTrainer.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

                col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_max_absent.Name).First().Index;
                this.inlineMaxAbsent.SetInlineControlPosition(row.DataGridView, row.Index, col_index);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            usersVM u = new users
            {
                id = -1,
                username = string.Empty,
                userpassword = string.Empty,
                name = string.Empty,
                email = string.Empty,
                level = 0,
                usergroup_id = null,
                status = "N",
                allowed_web_login = "N",
                training_expert = "N",
                max_absent = 10,
                create_at = DateTime.Now,
                last_use = null,
                rec_by = this.main_form.loged_in_user.name
            }.ToViewModel();

            ((BindingList<usersVM>)this.dgv.DataSource).Add(u);
            this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == u.id).First().Cells[this.col_username.Name].Selected = true;

            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm(this.dgv.Rows[this.dgv.CurrentCell.RowIndex]);
            this.inlineUserName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            var u = (users)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_users.Name].Value;
            if(u.level == (int)USER_LEVEL.ADMIN && this.main_form.loged_in_user.level < (int)USER_LEVEL.ADMIN)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgv.Rows[this.dgv.CurrentCell.RowIndex]);
            this.inlineName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            users u = (users)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_users.Name].Value;

            if(MessageAlert.Show("ลบรหัสผู้ใช้ " + u.username + " ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        var user_to_delete = sn.users.Find(u.id);
                        if(user_to_delete != null)
                        {
                            sn.users.Remove(user_to_delete);
                            sn.SaveChanges();
                            this.btnReload.PerformClick();
                        }
                        else
                        {
                            MessageAlert.Show("ค้นหารหัสผู้ใช้ " + u.username + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show("รหัสผู้ใช้ " + u.username + " เคยนำมาใช้งานแล้ว ไม่สามารถลบได้, \n\tหากต้องการระงับการใช้งานของผู้ใช้รายนี้ ให้ใช้วิธีเปลี่ยนสถานะเป็น \"ห้ามใช้\"", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            int focused_id = -1;
            if(this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                focused_id = (int)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_id.Name].Value;
            }

            this.HideInlineForm();
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.btnReload.PerformClick();

            if (this.dgv.Rows.Count == 0)
                return;

            var selected_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == focused_id).FirstOrDefault();
            if(selected_row != null)
            {
                selected_row.Cells[this.col_username.Name].Selected = true;
            }
            else
            {
                this.dgv.Rows[this.dgv.Rows.Count - 1].Cells[this.col_username.Name].Selected = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (this.form_mode == FORM_MODE.ADD_ITEM)
                    {
                        if (this.tmp_users.username.Trim().Length == 0)
                        {
                            this.inlineUserName.Focus();
                            return;
                        }

                        if (sn.users.Where(u => u.username.Trim() == this.tmp_users.username.Trim()).FirstOrDefault() != null)
                        {
                            MessageAlert.Show("รหัสผู้ใช้ " + this.tmp_users.username.Trim() + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }

                        this.tmp_users.userpassword = this.tmp_users.username.ToBytesString();
                        this.tmp_users.create_at = DateTime.Now;
                        this.tmp_users.rec_by = this.main_form.loged_in_user.username;
                        sn.users.Add(this.tmp_users);
                        sn.SaveChanges();

                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.btnAdd.PerformClick();
                        return;
                    }

                    if (this.form_mode == FORM_MODE.EDIT_ITEM)
                    {
                        var users_to_update = sn.users.Where(u => u.id == this.tmp_users.id).FirstOrDefault();
                        if (users_to_update != null)
                        {
                            users_to_update.name = this.tmp_users.name;
                            users_to_update.email = this.tmp_users.email;
                            users_to_update.level = this.tmp_users.level;
                            users_to_update.usergroup_id = this.tmp_users.usergroup_id;
                            users_to_update.status = this.tmp_users.status;
                            users_to_update.allowed_web_login = this.tmp_users.allowed_web_login;
                            users_to_update.training_expert = this.tmp_users.training_expert;
                            users_to_update.max_absent = this.tmp_users.max_absent;
                            users_to_update.rec_by = this.main_form.loged_in_user.username;

                            sn.SaveChanges();
                        }
                        else
                        {
                            MessageAlert.Show("ค้นหารหัสผู้ใช้ " + this.tmp_users.username + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        }
                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.users_list = new BindingList<usersVM>(GetUsersList().ToViewModel());
            this.dgv.DataSource = this.users_list;
        }

        private void inlineUserName__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.username = ((XTextEdit)sender)._Text;
        }

        private void inlineName__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.name = ((XTextEdit)sender)._Text;
        }

        private void inlineLevel__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.level = (int)((XDropdownList)sender)._SelectedItem.Value;
        }

        private void inlineGroup__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.usergroup_id = (int?)((XDropdownList)sender)._SelectedItem.Value;
        }

        private void inlineStatus__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.status = (string)((XDropdownList)sender)._SelectedItem.Value;
        }

        private void inlineTrainer__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.training_expert = (string)((XDropdownList)sender)._SelectedItem.Value;
        }

        private void inlineMaxAbsent__ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_users != null)
                this.tmp_users.max_absent = Convert.ToInt32(((XNumEdit)sender)._Value);
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnResetPassword.Enabled = false;
                return;
            }

            var u = (users)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells[this.col_users.Name].Value;

            if(this.main_form.loged_in_user.level < (int)USER_LEVEL.ADMIN && u.level == (int)USER_LEVEL.ADMIN)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnResetPassword.Enabled = false;
            }
            else
            {
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnResetPassword.Enabled = true;
            }
                
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                users user = null;
                if (row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_username.Name].Selected = true;
                    user = (users)((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex].Cells[this.col_users.Name].Value;
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("เพิ่ม <Alt + A>");
                m_add.Click += delegate
                {
                    this.btnAdd.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("แก้ไข <Alt + E>");
                m_edit.Click += delegate
                {
                    this.btnEdit.PerformClick();
                };
                if(row_index > -1 && user != null && user.level <= this.main_form.loged_in_user.level)
                {
                    m_edit.Enabled = true;
                }
                else
                {
                    m_edit.Enabled = false;
                }
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("ลบ <Alt + D>");
                m_delete.Click += delegate
                {
                    this.btnDelete.PerformClick();
                };
                cm.MenuItems.Add(m_delete);
                if (row_index > -1 && user != null && user.level <= this.main_form.loged_in_user.level)
                {
                    m_delete.Enabled = true;
                }
                else
                {
                    m_delete.Enabled = false;
                }
                cm.MenuItems.Add(m_delete);

                MenuItem m_reset = new MenuItem("รีเซ็ตรหัสผ่าน");
                m_reset.Click += delegate
                {
                    this.btnResetPassword.PerformClick();
                };
                if (row_index > -1 && user != null && user.level <= this.main_form.loged_in_user.level)
                {
                    m_reset.Enabled = true;
                }
                else
                {
                    m_reset.Enabled = false;
                }
                cm.MenuItems.Add(m_reset);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

            if(row_index == -1)
            {
                this.btnAdd.PerformClick();
            }
            else
            {
                this.btnEdit.PerformClick();
            }
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            if(((XDatagrid)sender).CurrentCell != null && (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM))
            {
                this.SetInlineFormPosition(((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex]);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            var user = (users)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_users.Name].Value;

            if(MessageAlert.Show("รีเซ็ตรหัสผ่านของผู้ใช้ " + user.username + " ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var u = sn.users.Find(user.id);

                    if(u != null)
                    {
                        if(u.level > this.main_form.loged_in_user.level)
                        {
                            MessageAlert.Show("รหัสผู้ใช้ " + u.username + " อยู่ในกลุ่มผู้ใช้ที่สูงกว่า ไม่สามารถรีเซ็ตรหัสผ่านได้", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }

                        u.userpassword = u.username.ToBytesString();
                        sn.SaveChanges();

                        MessageAlert.Show("เปลี่ยนรหัสผ่านของผู้ใช้ " + u.username + " เป็น \"" + u.username + "\" เรียบร้อย");
                        return;
                    }
                    else
                    {
                        MessageAlert.Show("ค้นหารหัสผู้ใช้ " + user.username + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.inlineMaxAbsent._Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }

            if(keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if(keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.A))
            {
                this.btnAdd.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.E))
            {
                this.btnEdit.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.D))
            {
                this.btnDelete.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.F5))
            {
                this.btnReload.PerformClick();
                return true;
            }

            if(keyData == Keys.Tab)
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if (this.dgv.CurrentCell == null)
                        return false;

                    using (snEntities sn = DBX.DataSet())
                    {
                        var u = (users)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_users.Name].Value;
                        var user = sn.users.Find(u.id);
                        var total_row = sn.users.AsEnumerable().Count();
                        
                        if (user == null)
                            return false;

                        DbInfo info = new DbInfo
                        {
                            DbName = sn.Database.Connection.Database,
                            TbName = "Users",
                            Expression = "Username",
                            CreBy = user.rec_by,
                            CreDat = user.create_at,
                            ChgBy = string.Empty,
                            ChgDat = null,
                            RecId = user.id,
                            TotalRec = total_row
                        };

                        DialogDataInfo d_info = new DialogDataInfo(info);
                        d_info.ShowDialog();
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
