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
    public partial class DialogAbsentReportScope : Form
    {
        public users user_from = null;
        public users user_to = null;
        public DateTime? date_from = null;
        public DateTime? date_to = null;

        public DialogAbsentReportScope(users selected_user = null, DateTime? selected_date_from = null, DateTime? selected_date_to = null)
        {
            this.user_from = selected_user;
            this.user_to = selected_user;
            this.date_from = selected_date_from;
            this.date_to = selected_date_to;
            InitializeComponent();
        }

        private void DialogAbsentReportScope_Load(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                sn.users.OrderBy(u => u.username).ToList().ForEach(u => { this.cbUserFrom.Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = u }); { this.cbUserTo.Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = u }); } });
                if(this.user_from != null)
                {
                    var u1 =this.cbUserFrom.Items.Cast<XDropdownListItem>().Where(i => ((users)i.Value).id == this.user_from.id).FirstOrDefault();
                    if (u1 != null)
                        this.cbUserFrom.SelectedItem = u1;

                    var u2 = this.cbUserTo.Items.Cast<XDropdownListItem>().Where(i => ((users)i.Value).id == this.user_to.id).FirstOrDefault();
                    if (u2 != null)
                        this.cbUserFrom.SelectedItem = u2;
                }
            }

            this.dtDateFrom.Value = this.date_from.HasValue ? this.date_from.Value : DateTime.Now;
            this.dtDateTo.Value = this.date_to.HasValue ? this.date_to.Value : DateTime.Now;
        }

        private void cbUserFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.user_from = (users)((XDropdownListItem)((ComboBox)sender).SelectedItem).Value;

            /** uncomment this if want to use user range **/
            this.cbUserTo.SelectedItem = this.cbUserTo.Items.Cast<XDropdownListItem>().Where(i => ((users)i.Value).id == this.user_from.id).FirstOrDefault();
        }

        private void cbUserTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.user_to = (users)((XDropdownListItem)((ComboBox)sender).SelectedItem).Value;
        }

        private void cbUserFrom_Leave(object sender, EventArgs e)
        {
            string txt = ((ComboBox)sender).Text;
            if(txt.Trim().Length > 0)
            {
                var selected_item = ((ComboBox)sender).Items.Cast<XDropdownListItem>().Where(i => i.Text.StartsWith(txt)).FirstOrDefault();

                if (selected_item != null)
                {
                    ((ComboBox)sender).SelectedItem = selected_item;
                }
                else
                {
                    ((ComboBox)sender).Focus();
                    SendKeys.Send("{F6}");
                }
            }
            else
            {
                ((ComboBox)sender).Focus();
                SendKeys.Send("{F6}");
            }
        }

        private void cbUserTo_Leave(object sender, EventArgs e)
        {
            string txt = ((ComboBox)sender).Text;
            if (txt.Trim().Length > 0)
            {
                var selected_item = ((ComboBox)sender).Items.Cast<XDropdownListItem>().Where(i => i.Text.StartsWith(txt)).FirstOrDefault();

                if (selected_item != null)
                {
                    ((ComboBox)sender).SelectedItem = selected_item;
                }
                else
                {
                    ((ComboBox)sender).Focus();
                    SendKeys.Send("{F6}");
                }
            }
            else
            {
                ((ComboBox)sender).Focus();
                SendKeys.Send("{F6}");
            }
        }

        private void dtDateFrom_ValueChanged(object sender, EventArgs e)
        {
            this.date_from = ((DateTimePicker)sender).Value.Date;
        }

        private void dtDateTo_ValueChanged(object sender, EventArgs e)
        {
            this.date_to = ((DateTimePicker)sender).Value.Date;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.user_from == null)
            {
                this.cbUserFrom.Focus();
                SendKeys.Send("{F6}");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F6)
            {
                if(this.cbUserFrom.Focused || this.cbUserTo.Focused || this.dtDateFrom.Focused || this.dtDateTo.Focused)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
            }

            if(keyData == Keys.Enter)
            {
                if(this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
