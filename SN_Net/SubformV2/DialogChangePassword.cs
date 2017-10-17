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

namespace SN_Net.Subform
{
    public partial class DialogChangePassword : Form
    {
        public MainForm main_form;
        private string old_pass = string.Empty;
        private string new_pass = string.Empty;
        private string new_pass_confirm = string.Empty;
        private users user;

        public DialogChangePassword(MainForm main_form, users user_to_set_password)
        {
            this.main_form = main_form;
            this.user = user_to_set_password;
            InitializeComponent();
        }

        private void DialogChangePassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtOldPassword;
            this.txtUsername.Text = this.user.username;
        }

        private void txtOldPassword_TextChanged(object sender, EventArgs e)
        {
            this.old_pass = ((TextBox)sender).Text;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            this.new_pass = ((TextBox)sender).Text;
        }

        private void txtNewPassword2_TextChanged(object sender, EventArgs e)
        {
            this.new_pass_confirm = ((TextBox)sender).Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.new_pass.CompareTo(this.new_pass_confirm) != 0)
            {
                MessageAlert.Show("กรุณายืนยันรหัสผ่านใหม่ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.txtNewPassword2.Focus();
                return;
            }

            if(MessageAlert.Show("เปลี่ยนรหัสผ่านของผู้ใช้ " + this.user.username + " ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        var u = sn.users.Find(this.user.id);

                        if (u != null)
                        {
                            if (u.userpassword.ExtractBytesString() != this.old_pass)
                            {
                                MessageAlert.Show("กรุณาป้อนรหัสผ่านเดิมให้ถูกต้อง", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                                return;
                            }

                            u.userpassword = this.new_pass.ToBytesString();
                            sn.SaveChanges();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageAlert.Show("รหัสผู้ใช้ " + this.user.username + " ไม่มีอยู่ในระบบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }
                else
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
