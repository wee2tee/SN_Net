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
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class DialogAbsentRange : Form
    {
        private MainForm main_form;
        public List<event_calendar> event_list;

        public DialogAbsentRange(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void DialogAbsentRange_Load(object sender, EventArgs e)
        {
            this.SetDropdownItem();

            this.cbMedcert.SelectedItem = this.cbMedcert.Items.Cast<XDropdownListItem>().First();
            this.cbStatus.SelectedItem = this.cbStatus.Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == (int)CALENDAR_EVENT_STATUS.CONFIRMED).FirstOrDefault();
            this.ActiveControl = this.cbUser;
        }

        private void SetDropdownItem()
        {
            using (snEntities sn = DBX.DataSet())
            {
                sn.users.OrderBy(u => u.username).ToList().ForEach(u => this.cbUser.Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = new InlineAbsentUser { id = u.id, username = u.username, name = u.name } }));
            }
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT || i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).OrderBy(i => i.typcod).ToList().ForEach(i => this.cbReason.Items.Add(new XDropdownListItem { Text = i.abbreviate_th, Value = new InlineAbsentReason { id = i.id, tabtyp = i.tabtyp, typcod = i.typcod } }));
            }

            this.cbMedcert.Items.Add(new XDropdownListItem { Text = "N/A ไม่ระบุ", Value = CALENDAR_EVENT_MEDCERT.NOT_ASSIGN });
            this.cbMedcert.Items.Add(new XDropdownListItem { Text = "ไม่มีเอกสาร", Value = CALENDAR_EVENT_MEDCERT.NOT_HAVE_MEDCERT });
            this.cbMedcert.Items.Add(new XDropdownListItem { Text = "มีใบรับรองแพทย์", Value = CALENDAR_EVENT_MEDCERT.HAVE_MEDCERT });

            Enum.GetValues(typeof(CALENDAR_EVENT_STATUS)).Cast<CALENDAR_EVENT_STATUS>().ToList().ForEach(i => this.cbStatus.Items.Add(new XDropdownListItem { Text = i.ToString(), Value = (int)i }));
        }

        private void ckDeductCom_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.ckMonday.Enabled = true;
                this.ckTuesday.Enabled = true;
                this.ckWednesday.Enabled = true;
                this.ckThursday.Enabled = true;
                this.ckFriday.Enabled = true;
                this.ckSaturday.Enabled = true;
                this.numFine.Enabled = true;
            }
            else
            {
                this.ckMonday.Checked = false;
                this.ckTuesday.Checked = false;
                this.ckWednesday.Checked = false;
                this.ckThursday.Checked = false;
                this.ckFriday.Checked = false;
                this.ckSaturday.Checked = false;
                this.numFine.Value = 0;

                this.ckMonday.Enabled = false;
                this.ckTuesday.Enabled = false;
                this.ckWednesday.Enabled = false;
                this.ckThursday.Enabled = false;
                this.ckFriday.Enabled = false;
                this.ckSaturday.Enabled = false;
                this.numFine.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.cbUser.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุรหัสพนักงาน", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.cbUser.Focus();
                SendKeys.Send("{F4}");
                return;
            }
            if (this.cbReason.SelectedItem == null)
            {
                MessageAlert.Show("กรุณาระบุเหตุผล", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.cbReason.Focus();
                SendKeys.Send("{F4}");
                return;
            }

            if(MessageAlert.Show("บันทึกข้อมูล, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                this.event_list = new List<event_calendar>();

                List<DayOfWeek> fine_date = new List<DayOfWeek>();
                if (this.ckMonday.Checked)
                    fine_date.Add(DayOfWeek.Monday);
                if (this.ckTuesday.Checked)
                    fine_date.Add(DayOfWeek.Tuesday);
                if (this.ckWednesday.Checked)
                    fine_date.Add(DayOfWeek.Wednesday);
                if (this.ckThursday.Checked)
                    fine_date.Add(DayOfWeek.Thursday);
                if (this.ckFriday.Checked)
                    fine_date.Add(DayOfWeek.Friday);
                if (this.ckSaturday.Checked)
                    fine_date.Add(DayOfWeek.Saturday);

                List<DateTime> holidays;
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    holidays = note.note_calendar.Where(n => n.date.CompareTo(this.dtFrom.Value.Date) >= 0 && n.date.CompareTo(this.dtTo.Value.Date) <= 0 && n.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY).Select(n => n.date).ToList();
                }

                DateTime d = this.dtFrom.Value;
                string series = ((InlineAbsentUser)((XDropdownListItem)this.cbUser.SelectedItem).Value).username + "-" + DateTime.Now.GetUnixTimeStamp().ToString();
                do
                {
                    if (d.DayOfWeek == DayOfWeek.Sunday)
                    {
                        d = d.AddDays(1);
                        continue;
                    }

                    if (holidays.Where(h => h.Date.CompareTo(d.Date) == 0).Count() > 0)
                    {
                        d = d.AddDays(1);
                        continue;
                    }

                    if (d.IsLastSaturday())
                    {
                        d = d.AddDays(1);
                        continue;
                    }

                    this.event_list.Add(new event_calendar
                    {
                        id = -1,
                        customer = this.txtRemark._Text,
                        date = d,
                        event_code = ((InlineAbsentReason)((XDropdownListItem)this.cbReason.SelectedItem).Value).typcod,
                        event_code_id = ((InlineAbsentReason)((XDropdownListItem)this.cbReason.SelectedItem).Value).id,
                        event_type = ((InlineAbsentReason)((XDropdownListItem)this.cbReason.SelectedItem).Value).tabtyp,
                        fine = fine_date.Where(f => f == d.DayOfWeek).Count() > 0 ?  Convert.ToInt32(this.numFine.Value) : 0,
                        from_time = "08:30",
                        to_time = d.DayOfWeek == DayOfWeek.Saturday ? "12:00" : "17:30",
                        med_cert = (string)((XDropdownListItem)this.cbMedcert.SelectedItem).Value,
                        realname = ((InlineAbsentUser)((XDropdownListItem)this.cbUser.SelectedItem).Value).name,
                        users_name = ((InlineAbsentUser)((XDropdownListItem)this.cbUser.SelectedItem).Value).username,
                        rec_by = this.main_form.loged_in_user.username,
                        status = (int)((XDropdownListItem)this.cbStatus.SelectedItem).Value,
                        series = series
                    });
                    d = d.AddDays(1);
                } while (d.Date.CompareTo(this.dtTo.Value.Date) <= 0);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(this.btnOK.Focused || this.btnCancel.Focused || (this.ActiveControl is ComboBox && ((ComboBox)this.ActiveControl).DroppedDown))
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
                if (this.ActiveControl is ComboBox && ((ComboBox)this.ActiveControl).DroppedDown)
                    return false;

                this.btnCancel.PerformClick();
                return true;
            }

            if(keyData == Keys.F6)
            {
                if(this.ActiveControl is ComboBox)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cbUser_Leave(object sender, EventArgs e)
        {
            if(((ComboBox)sender).Text.Trim().Length == 0)
            {
                ((ComboBox)sender).Focus();
                SendKeys.Send("{F6}");
            }
            else
            {
                var selected_item = ((ComboBox)sender).Items.Cast<XDropdownListItem>().Where(i => i.Text.StartsWith(((ComboBox)sender).Text)).FirstOrDefault();
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
        }
    }
}
