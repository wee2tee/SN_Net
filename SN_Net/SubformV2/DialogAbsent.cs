using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.Model;
using System.Globalization;
using SN_Net.MiscClass;
using SN_Net.Model;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogAbsent : Form
    {
        private MainForm main_form;
        private CustomDateEvent3 custom_date_event;
        private bool perform_add;
        private note_calendar curr_note;
        private BindingList<event_calendarVMFull> curr_event_list;
        private FORM_MODE form_mode;
        private note_calendar tmp_note_calendar;
        private event_calendar tmp_event_calendar;

        public DialogAbsent(MainForm main_form, CustomDateEvent3 custom_date_event, bool perform_add = false)
        {
            this.main_form = main_form;
            this.custom_date_event = custom_date_event;
            this.perform_add = perform_add;
            InitializeComponent();
        }

        private void DialogAbsent_Load(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ);

            this.groupBox1.Text = this.custom_date_event.curr_date.ToString("วันdddd ที่ dd MMMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            if (this.perform_add)
                this.btnAddItem.PerformClick();

            this.SetDropdownListItem();
            this.GetData();
            this.FillForm();
            
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT, FORM_MODE.READ_ITEM, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            this.rdHoliday.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.rdWeekday.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtHoliday.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtRemark.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.dlGroupHoliday.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.dlGroupMaid.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.numLeaveCount.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.dgv.SetControlState(new FORM_MODE[] { FORM_MODE.READ, FORM_MODE.READ_ITEM }, this.form_mode);

            this.btnAddItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEditItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDeleteItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
        }

        private void SetDropdownListItem()
        {
            using (snEntities sn = DBX.DataSet())
            {
                sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_USERGROUP).OrderBy(i => i.typcod).ToList().ForEach(i => { this.dlGroupHoliday._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); this.dlGroupMaid._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); });
            }
        }

        private void GetData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.curr_event_list = new BindingList<event_calendarVMFull>(note.event_calendar.Where(ev => ev.date.CompareTo(this.custom_date_event.curr_date) == 0).ToViewModelFull());
                if(this.custom_date_event.note_cal != null)
                {
                    var note_cal = note.note_calendar.Where(n => n.id == this.custom_date_event.note_cal.id).FirstOrDefault();
                    this.curr_note = note_cal != null ? note_cal : new note_calendar { date = this.custom_date_event.curr_date, description = string.Empty, group_maid = string.Empty, group_weekend = string.Empty, max_leave = -1, rec_by = this.main_form.loged_in_user.username, type = (int)CALENDAR_NOTE_TYPE.WEEKDAY };
                }
                else
                {
                    this.curr_note = new note_calendar { date = this.custom_date_event.curr_date, description = string.Empty, group_maid = string.Empty, group_weekend = string.Empty, max_leave = -1, rec_by = this.main_form.loged_in_user.username, type = (int)CALENDAR_NOTE_TYPE.WEEKDAY };
                }
            }
        }

        private void FillForm(note_calendar note_cal_to_fill = null, BindingList<event_calendarVMFull> event_cal_to_fill = null)
        {
            note_calendar note_cal = note_cal_to_fill != null ? note_cal_to_fill : this.curr_note;
            BindingList<event_calendarVMFull> event_list = event_cal_to_fill != null ? event_cal_to_fill : this.curr_event_list;

            this.dgv.DataSource = event_list;
            this.rdHoliday.Checked = note_cal.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY ? true : false;
            this.rdWeekday.Checked = note_cal.type == (int)CALENDAR_NOTE_TYPE.WEEKDAY ? true : false;
            this.txtHoliday._Text = note_cal.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY ? note_cal.description : string.Empty;
            this.txtRemark._Text = note_cal.type == (int)CALENDAR_NOTE_TYPE.WEEKDAY ? note_cal.description : string.Empty;
            this.numLeaveCount.Value = note_cal.max_leave;
            var selected_group_holiday = this.dlGroupHoliday._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == note_cal.group_weekend).FirstOrDefault();
            if (selected_group_holiday != null)
                this.dlGroupHoliday._SelectedItem = selected_group_holiday;

            var selected_group_maid = this.dlGroupMaid._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == note_cal.group_maid).FirstOrDefault();
            if (selected_group_maid != null)
                this.dlGroupMaid._SelectedItem = selected_group_maid;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                if(this.custom_date_event.note_cal != null)
                {
                    var note_cal = note.note_calendar.Find(this.custom_date_event.note_cal.id);
                    this.tmp_note_calendar = note_cal != null ? note_cal : new note_calendar { date = this.custom_date_event.curr_date, description = string.Empty, group_maid = string.Empty, group_weekend = string.Empty, max_leave = -1, rec_by = this.main_form.loged_in_user.username, type = (int)CALENDAR_NOTE_TYPE.WEEKDAY };
                }
                else
                {
                    this.tmp_note_calendar = new note_calendar { date = this.custom_date_event.curr_date, description = string.Empty, group_maid = string.Empty, group_weekend = string.Empty, max_leave = -1, rec_by = this.main_form.loged_in_user.username, type = (int)CALENDAR_NOTE_TYPE.WEEKDAY };
                }

                this.FillForm(this.tmp_note_calendar);

                this.ResetFormState(FORM_MODE.EDIT);
                
                if(this.tmp_note_calendar.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY)
                {
                    this.txtHoliday.Focus();
                }
                else
                {
                    this.dlGroupHoliday.Focus();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.ResetFormState(FORM_MODE.READ);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void btnCopyItem_Click(object sender, EventArgs e)
        {

        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                this.ResetFormState(FORM_MODE.READ_ITEM);

                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if (row_index > -1)
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_code_name.Name].Selected = true;

                ContextMenu cm = new ContextMenu();
                MenuItem mnu_add = new MenuItem("เพิ่ม <Alt + A>");
                mnu_add.Click += delegate
                {
                    this.btnAddItem.PerformClick();
                };
                cm.MenuItems.Add(mnu_add);

                MenuItem mnu_edit = new MenuItem("แก้ไข <Alt + E>");
                mnu_edit.Click += delegate
                {
                    this.btnEditItem.PerformClick();
                };
                mnu_edit.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(mnu_edit);

                MenuItem mnu_delete = new MenuItem("ลบ <Alt + D>");
                mnu_delete.Click += delegate
                {
                    this.btnDeleteItem.PerformClick();
                };
                mnu_delete.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(mnu_delete);

                MenuItem mnu_copy = new MenuItem("คัดลอกไปยังวันที่อื่น <Alt + C>");
                mnu_copy.Click += delegate
                {
                    this.btnCopyItem.PerformClick();
                };
                mnu_copy.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(mnu_copy);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void txtHoliday__DoubleClicked(object sender, EventArgs e)
        {

        }

        private void dlGroupHoliday__DoubleClicked(object sender, EventArgs e)
        {

        }

        private void dlGroupMaid__DoubleClicked(object sender, EventArgs e)
        {

        }

        private void txtRemark__DoubleClicked(object sender, EventArgs e)
        {

        }
    }
}
