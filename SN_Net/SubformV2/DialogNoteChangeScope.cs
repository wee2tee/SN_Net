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
    public partial class DialogNoteChangeScope : Form
    {
        private MainForm main_form;
        public users selected_user = null;
        public DateTime? selected_date = null;

        public DialogNoteChangeScope(MainForm main_form, users current_user = null, DateTime? current_date = null)
        {
            this.main_form = main_form;
            this.selected_user = current_user;
            this.selected_date = current_date;
            InitializeComponent();
        }

        private void DialogNoteChangeScope_Load(object sender, EventArgs e)
        {
            this.LoadDropDownSelection();
            if(this.selected_user != null)
            {
                var item = this.drUser._Items.Cast<XDropdownListItem>().Where(i => ((users)i.Value).id == this.selected_user.id).FirstOrDefault();
                if (item != null)
                    this.drUser._SelectedItem = item;
            }
            if (this.selected_date.HasValue)
            {
                this.dtDate._SelectedDate = this.selected_date;
            }

            this.drUser._ReadOnly = this.main_form.loged_in_user.level < (int)USER_LEVEL.SUPERVISOR ? true : false;
        }

        private void LoadDropDownSelection()
        {
            this.drUser._Items.Clear();

            using (snEntities sn = DBX.DataSet())
            {
                var user = sn.users.Where(u => u.level == (int)USER_LEVEL.SUPPORT).OrderBy(u => u.username).ToList();

                foreach (var u in user)
                {
                    this.drUser._Items.Add(new XDropdownListItem { Value = u, Text = u.username + " : " + u.name });
                }
            }
        }

        private void drUser__SelectedItemChanged(object sender, EventArgs e)
        {
            this.selected_user = (users)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;

            this.btnOK.Enabled = this.selected_user != null && this.selected_date.HasValue ? true : false;
        }

        private void dtDate__SelectedDateChanged(object sender, EventArgs e)
        {
            this.selected_date = ((XDatePicker)sender)._SelectedDate;

            this.btnOK.Enabled = this.selected_user != null && this.selected_date.HasValue ? true : false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(this.btnOK.Focused || this.btnCancel.Focused)
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
