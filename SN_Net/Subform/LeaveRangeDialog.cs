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
    public partial class LeaveRangeDialog : Form
    {
        private MainForm main_form;
        private List<Users> list_users;
        public Users user_from;
        public Users user_to;
        public DateTime date_from;
        public DateTime date_to;

        public LeaveRangeDialog(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        public LeaveRangeDialog(MainForm main_form, Users user_from, Users user_to, DateTime date_from, DateTime date_to)
            : this(main_form)
        {
            this.user_from = user_from;
            this.user_to = user_to;
            this.date_from = date_from;
            this.date_to = date_to;
        }

        private void LeaveRangeDialog_Load(object sender, EventArgs e)
        {
            this.BindingControlEventHandler();
            this.LoadDependenciesData();
            this.InitControl();

            this.cbUsersFrom.SelectedItem = (this.user_from != null ? this.cbUsersFrom.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).id == this.user_from.id).First<ComboboxItem>() : null);
            this.cbUsersTo.SelectedItem = (this.user_to != null ? this.cbUsersTo.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).id == this.user_to.id).First<ComboboxItem>() : null);
            this.dtFrom.Value = (this.date_from >= this.dtFrom.MinDate && this.date_from <= this.dtFrom.MaxDate ? this.date_from : DateTime.Now);
            this.dtTo.Value = (this.date_to >= this.dtTo.MinDate && this.date_to <= this.dtTo.MaxDate ? this.date_to : DateTime.Now);
        }

        private void LeaveRangeDialog_Shown(object sender, EventArgs e)
        {
            this.cbUsersFrom.Focus();
            if (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR)
            {
                this.cbUsersFrom.SelectedItem = this.cbUsersFrom.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).id == this.main_form.G.loged_in_user_id).First<ComboboxItem>();
                this.cbUsersTo.SelectedItem = this.cbUsersTo.Items.Cast<ComboboxItem>().Where(i => ((Users)i.Tag).id == this.main_form.G.loged_in_user_id).First<ComboboxItem>();
                this.cbUsersFrom.Enabled = false;
                this.cbUsersTo.Enabled = false;
            }
        }

        private void LoadDependenciesData()
        {
            #region Load users_list from server
            CRUDResult get_user = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_user = JsonConvert.DeserializeObject<ServerResult>(get_user.data);

            if (sr_user.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_users = sr_user.users;
            }
            #endregion Load users_list from server

            this.BindingControlEventHandler();
        }

        private void InitControl()
        {
            #region Add users_list to cbUsers
            foreach (Users u in this.list_users)
            {
                this.cbUsersFrom.Items.Add(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
                this.cbUsersTo.Items.Add(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
            }
            #endregion Add users_list to cbUsers
        }

        private void BindingControlEventHandler()
        {
            this.cbUsersFrom.Leave += new EventHandler(this.OnComboboxUsersLeave);

            this.cbUsersFrom.SelectedIndexChanged += delegate
            {
                this.user_from = (Users)((ComboboxItem)this.cbUsersFrom.SelectedItem).Tag;
            };

            this.cbUsersTo.SelectedIndexChanged += delegate
            {
                this.user_to = (Users)((ComboboxItem)this.cbUsersTo.SelectedItem).Tag;
            };

            this.cbUsersTo.Leave += new EventHandler(this.OnComboboxUsersLeave);

            this.dtFrom.ValueChanged += new EventHandler(this.OnDatetimeLeave);

            this.dtTo.ValueChanged += new EventHandler(this.OnDatetimeLeave);
        }

        private void OnComboboxUsersLeave(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Items.Cast<ComboboxItem>().Where(i => i.name == ((ComboBox)sender).Text.Trim()).Count<ComboboxItem>() > 0)
            {
                ((ComboBox)sender).SelectedItem = ((ComboBox)sender).Items.Cast<ComboboxItem>().Where(i => i.name == ((ComboBox)sender).Text.Trim()).First<ComboboxItem>();
                return;
            }

            if (((ComboBox)sender).Items.Cast<ComboboxItem>().Where(i => i.name.Length >= ((ComboBox)sender).Text.Trim().Length).Where(i => i.name.Substring(0, ((ComboBox)sender).Text.Trim().Length) == ((ComboBox)sender).Text.Trim().ToUpper()).Count<ComboboxItem>() > 0)
            {
                ((ComboBox)sender).SelectedItem = ((ComboBox)sender).Items.Cast<ComboboxItem>().Where(i => i.name.Length >= ((ComboBox)sender).Text.Trim().Length).Where(i => i.name.Substring(0, ((ComboBox)sender).Text.Trim().Length) == ((ComboBox)sender).Text.Trim().ToUpper()).First<ComboboxItem>();
            }
            else
            {
                ((ComboBox)sender).Focus();
                ((ComboBox)sender).DroppedDown = true;
            }
        }

        private void OnDatetimeLeave(object sender, EventArgs e)
        {
            if ((DateTimePicker)sender == this.dtFrom)
            {
                this.date_from = ((DateTimePicker)sender).Value;
                return;
            }
            if ((DateTimePicker)sender == this.dtTo)
            {
                this.date_to = ((DateTimePicker)sender).Value;
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.user_from == null)
            {
                MessageAlert.Show("กรุณาระบุรหัสพนักงาน จาก");
                return;
            }
            if (this.user_to == null)
            {
                MessageAlert.Show("กรุณาระบุรหัสพนักงาน ถึง");
                return;
            }
            if (this.date_from == null)
            {
                MessageAlert.Show("กรุณาระบุวันที่ จาก");
                return;
            }
            if (this.date_to == null)
            {
                MessageAlert.Show("กรุณาระบุวันที่ ถึง");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.F6)
            {
                if (this.cbUsersFrom.Focused || this.cbUsersTo.Focused || this.dtFrom.Focused || this.dtTo.Focused)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if (this.cbUsersFrom.DroppedDown || this.cbUsersTo.DroppedDown)
                {
                    return false;
                }

                this.btnCancel.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
