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
        public enum PERFORM_ACTION
        {
            ADD,
            EDIT,
            NONE
        }

        private MainForm main_form;
        private FormCalendar calendar;
        private CustomDateEvent3 custom_date_event;
        private PERFORM_ACTION perform_action;
        private event_calendar perform_edit_item;
        private note_calendar curr_note;
        private BindingList<event_calendarVMFull> curr_event_list;
        private FORM_MODE form_mode;
        private note_calendar tmp_note_calendar;
        private event_calendar tmp_event_calendar;


        public DialogAbsent(MainForm main_form, FormCalendar calendar, CustomDateEvent3 custom_date_event, PERFORM_ACTION perform_action = PERFORM_ACTION.NONE, event_calendar perform_edit_item = null)
        {
            this.main_form = main_form;
            this.calendar = calendar;
            this.custom_date_event = custom_date_event;
            this.perform_action = perform_action;
            this.perform_edit_item = perform_edit_item;
            InitializeComponent();
        }

        private void DialogAbsent_Load(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ);
            this.RemoveInlineForm();

            this.groupBox1.Text = this.custom_date_event.curr_date.ToString("วันdddd ที่ dd MMMM yyyy", CultureInfo.GetCultureInfo("th-TH"));

            this.SetDropdownListItem();
            this.GetData();
            this.FillForm();
            
            if(this.perform_action == PERFORM_ACTION.ADD)
            {
                this.ResetFormState(FORM_MODE.READ_ITEM);
                this.btnAddItem.PerformClick();
            }
            if(this.perform_action == PERFORM_ACTION.EDIT && this.perform_edit_item != null)
            {
                this.ResetFormState(FORM_MODE.READ_ITEM);
                var editing_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((event_calendar)r.Cells[this.col_event_calendar.Name].Value).id == this.perform_edit_item.id).FirstOrDefault();
                if (editing_row != null)
                    editing_row.Cells[this.col_code_name.Name].Selected = true;
                this.btnEditItem.PerformClick();
            }
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
            this.dlGroupHoliday._Items.Add(new XDropdownListItem { Text = string.Empty, Value = string.Empty });
            this.dlGroupMaid._Items.Add(new XDropdownListItem { Text = string.Empty, Value = string.Empty });
            this.inlineCodeName._Items.Add(new XDropdownListItem { Text = string.Empty, Value = new InlineAbsentUser { id = null, username = null, name = null } });
            using (snEntities sn = DBX.DataSet())
            {
                sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_USERGROUP).OrderBy(i => i.typcod).ToList().ForEach(i => { this.dlGroupHoliday._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); this.dlGroupMaid._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); });
                sn.users.OrderBy(u => u.username).ToList().ForEach(u => this.inlineCodeName._Items.Add(new XDropdownListItem { Text = u.username + " : " + u.name, Value = new InlineAbsentUser { id = u.id, username = u.username, name = u.name } }));
            }

            this.inlineReason._Items.Add(new XDropdownListItem { Text = string.Empty, Value = new InlineAbsentReason { id = null, tabtyp = null, typcod = null } });
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT || i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).OrderBy(i => i.tabtyp).ThenBy(i => i.typcod).ToList().ForEach(i => this.inlineReason._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = new InlineAbsentReason { id = i.id, tabtyp = i.tabtyp, typcod = i.typcod } }));
            }
            Enum.GetValues(typeof(CALENDAR_EVENT_STATUS)).Cast<CALENDAR_EVENT_STATUS>().ToList().ForEach(i => this.inlineStatus._Items.Add(new XDropdownListItem { Text = i.ToString(), Value = (int)i }));
            this.inlineMedCert._Items.Add(new XDropdownListItem { Text = "N/A (ไม่ระบุ)", Value = CALENDAR_EVENT_MEDCERT.NOT_ASSIGN });
            this.inlineMedCert._Items.Add(new XDropdownListItem { Text = "ไม่มีเอกสาร", Value = CALENDAR_EVENT_MEDCERT.NOT_HAVE_MEDCERT });
            this.inlineMedCert._Items.Add(new XDropdownListItem { Text = "มีเอกสารอื่น ๆ", Value = CALENDAR_EVENT_MEDCERT.OTHER_DOCUMENT });
            this.inlineMedCert._Items.Add(new XDropdownListItem { Text = "มีใบรับรองแพทย์", Value = CALENDAR_EVENT_MEDCERT.HAVE_MEDCERT });
        }

        private void GetData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.curr_event_list = new BindingList<event_calendarVMFull>(note.event_calendar.Where(ev => ev.date.CompareTo(this.custom_date_event.curr_date) == 0).ToList().OrderBy(ev => ev.ToViewModel().users.level).ThenBy(ev => ev.id).ToViewModelFull());
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

        private void ShowInlineForm()
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.tmp_event_calendar = (event_calendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_event_calendar.Name].Value;
            this.SetInlineFormPosition();
        }

        private void SetInlineFormPosition()
        {
            if (this.dgv.CurrentCell == null)
                return;

            int col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_code_name.Name).FirstOrDefault().Index;
            this.inlineCodeName.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            var selected_user = this.inlineCodeName._Items.Cast<XDropdownListItem>().Where(i => ((InlineAbsentUser)i.Value).username == this.tmp_event_calendar.users_name).FirstOrDefault();
            this.inlineCodeName._SelectedItem = selected_user != null ? selected_user : this.inlineCodeName._Items.Cast<XDropdownListItem>().First();

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_reason.Name).FirstOrDefault().Index;
            this.inlineReason.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            var selected_reason = this.inlineReason._Items.Cast<XDropdownListItem>().Where(i => ((InlineAbsentReason)i.Value).id == this.tmp_event_calendar.event_code_id).FirstOrDefault();
            this.inlineReason._SelectedItem = selected_reason != null ? selected_reason : this.inlineReason._Items.Cast<XDropdownListItem>().First();

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_time_from.Name).FirstOrDefault().Index;
            this.inlineFrom.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineFrom.Text = this.tmp_event_calendar.from_time;

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_time_to.Name).FirstOrDefault().Index;
            this.inlineTo.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineTo.Text = this.tmp_event_calendar.to_time;

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_status.Name).FirstOrDefault().Index;
            this.inlineStatus.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            var selected_status = this.inlineStatus._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == this.tmp_event_calendar.status).FirstOrDefault();
            this.inlineStatus._SelectedItem = selected_status != null ? selected_status : this.inlineStatus._Items.Cast<XDropdownListItem>().First();

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_remark.Name).FirstOrDefault().Index;
            this.inlineRemark.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineRemark._Text = this.tmp_event_calendar.customer;

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_med_cert.Name).FirstOrDefault().Index;
            this.inlineMedCert.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            var selected_medcert = this.inlineMedCert._Items.Cast<XDropdownListItem>().Where(i => (string)i.Value == this.tmp_event_calendar.med_cert).FirstOrDefault();
            this.inlineMedCert._SelectedItem = selected_medcert != null ? selected_medcert : this.inlineMedCert._Items.Cast<XDropdownListItem>().First();

            col_index = this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_fine.Name).FirstOrDefault().Index;
            this.inlineFine.SetInlineControlPosition(this.dgv, this.dgv.CurrentCell.RowIndex, col_index);
            this.inlineFine.Value = Convert.ToDecimal(this.tmp_event_calendar.fine);
        }

        private void RemoveInlineForm()
        {
            this.inlineCodeName.SetBounds(-9999, -9999, 0, 0);
            this.inlineReason.SetBounds(-9999, -9999, 0, 0);
            this.inlineFrom.SetBounds(-9999, -9999, 0, 0);
            this.inlineTo.SetBounds(-9999, -9999, 0, 0);
            this.inlineStatus.SetBounds(-9999, -9999, 0, 0);
            this.inlineRemark.SetBounds(-9999, -9999, 0, 0);
            this.inlineMedCert.SetBounds(-9999, -9999, 0, 0);
            this.inlineFine.SetBounds(-9999, -9999, 0, 0);

            this.tmp_event_calendar = null;
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
            if (this.form_mode == FORM_MODE.EDIT)
            {
                this.GetData();
                this.FillForm();
                this.ResetFormState(FORM_MODE.READ);
                this.tmp_note_calendar = null;
            }

            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.GetData();
                this.FillForm();
                return;
            }

            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.RemoveInlineForm();
                this.GetData();
                this.FillForm();
                this.ResetFormState(FORM_MODE.READ_ITEM);
                this.dgv.Focus();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.EDIT)
            {
                try
                {
                    using (sn_noteEntities note = DBXNote.DataSet())
                    {
                        var nc = note.note_calendar.Where(n => n.date == this.custom_date_event.curr_date).FirstOrDefault();
                        if (nc != null) // already exist
                        {
                            nc.description = this.tmp_note_calendar.description;
                            nc.group_maid = this.tmp_note_calendar.group_maid;
                            nc.group_weekend = this.tmp_note_calendar.group_weekend;
                            nc.max_leave = this.tmp_note_calendar.max_leave;
                            nc.rec_by = this.tmp_note_calendar.rec_by;
                            nc.type = this.tmp_note_calendar.type;
                            note.SaveChanges();
                        }
                        else // not exist, create new
                        {
                            note.note_calendar.Add(this.tmp_note_calendar);
                            note.SaveChanges();
                        }

                        this.custom_date_event.RefreshView();
                        this.ResetFormState(FORM_MODE.READ);
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
                return;
            }

            if (this.form_mode == FORM_MODE.ADD_ITEM)
            {
                var validate = this.ValidateDataBeforeSave();
                if (!validate.passed)
                {
                    MessageAlert.Show(validate.message, "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    if (validate.invalid_control_data != null)
                    {
                        validate.invalid_control_data.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                }

                try
                {
                    using (sn_noteEntities note = DBXNote.DataSet())
                    {
                        note.event_calendar.Add(this.tmp_event_calendar);
                        note.SaveChanges();
                        this.RemoveInlineForm();
                        this.custom_date_event.RefreshView();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.GetData();
                        this.FillForm();
                        this.btnAddItem.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
                return;
            }

            if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                var validate = this.ValidateDataBeforeSave();
                if (!validate.passed)
                {
                    MessageAlert.Show(validate.message, "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    if (validate.invalid_control_data != null)
                    {
                        validate.invalid_control_data.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }
                }

                try
                {
                    using (sn_noteEntities note = DBXNote.DataSet())
                    {
                        var ev = note.event_calendar.Find(this.tmp_event_calendar.id);
                        if(ev != null) // update existing
                        {
                            ev.date = this.tmp_event_calendar.date;
                            ev.users_name = this.tmp_event_calendar.users_name;
                            ev.realname = this.tmp_event_calendar.realname;
                            ev.customer = this.tmp_event_calendar.customer;
                            ev.event_code = this.tmp_event_calendar.event_code;
                            ev.event_code_id = this.tmp_event_calendar.event_code_id;
                            ev.event_type = this.tmp_event_calendar.event_type;
                            ev.from_time = this.tmp_event_calendar.from_time;
                            ev.to_time = this.tmp_event_calendar.to_time;
                            ev.fine = this.tmp_event_calendar.fine;
                            ev.med_cert = this.tmp_event_calendar.med_cert;
                            ev.status = this.tmp_event_calendar.status;
                            ev.rec_by = this.main_form.loged_in_user.username;
                        }
                        else // add new
                        {
                            note.event_calendar.Add(this.tmp_event_calendar);
                        }
                        note.SaveChanges();
                        this.RemoveInlineForm();
                        this.custom_date_event.RefreshView();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.GetData();
                        this.FillForm();
                        this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((event_calendar)r.Cells[this.col_event_calendar.Name].Value).id == ev.id).FirstOrDefault().Cells[this.col_code_name.Name].Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
                return;
            }
        }

        private ValidateDataResult ValidateDataBeforeSave()
        {
            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                if (this.tmp_event_calendar == null)
                    return new ValidateDataResult { passed = false, invalid_control_data = null, message = "Unknow Error" };

                if (this.tmp_event_calendar.from_time == "00:00")
                    return new ValidateDataResult { passed = false, invalid_control_data = this.inlineFrom, message = "กรุณาระบุเวลาจาก" };
                if (this.tmp_event_calendar.to_time == "00:00")
                    return new ValidateDataResult { passed = false, invalid_control_data = this.inlineTo, message = "กรุณาระบุเวลาถึง" };
                if (this.tmp_event_calendar.users_name == null)
                    return new ValidateDataResult { passed = false, invalid_control_data = this.inlineCodeName, message = "กรุณาระบุรหัสพนักงาน" };
                if (this.tmp_event_calendar.event_code_id == null)
                    return new ValidateDataResult { passed = false, invalid_control_data = this.inlineReason, message = "กรุณาระบุเหตุผล" };
            }

            return new ValidateDataResult { passed = true, invalid_control_data = null, message = string.Empty };
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.dgv.Focus();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ((BindingList<event_calendarVMFull>)this.dgv.DataSource).Add(new event_calendar
            {
                id = -1,
                date = this.custom_date_event.curr_date,
                from_time = "00:00",
                to_time = "00:00",
                users_name = null,
                realname = null,
                customer = string.Empty,
                fine = 0,
                med_cert = "X",
                status = 1,
                event_code = null,
                event_code_id = null,
                event_type = null,
                rec_by = this.main_form.loged_in_user.username,
            }.ToViewModelFull());

            this.dgv.Rows.Cast<DataGridViewRow>().Where(r => ((event_calendar)r.Cells[this.col_event_calendar.Name].Value).id == -1).FirstOrDefault().Cells[this.col_code_name.Name].Selected = true;

            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm();
            this.inlineCodeName.Focus();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm();
            this.inlineCodeName.Focus();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.dgv.Rows[this.dgv.CurrentCell.RowIndex].DrawDeletingRowOverlay();

            if(MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                try
                {
                    using (sn_noteEntities note = DBXNote.DataSet())
                    {
                        var event_to_delete = note.event_calendar.Find(((event_calendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_event_calendar.Name].Value).id);
                        if (event_to_delete != null)
                        {
                            if (event_to_delete.series != null)
                            {
                                DialogConfirmDeleteAbsentRange conf = new DialogConfirmDeleteAbsentRange();
                                if (conf.ShowDialog() == DialogResult.OK)
                                {
                                    if (conf.delete_method == DialogConfirmDeleteAbsentRange.DELETE_METHOD.ALL)
                                    {
                                        var notes_to_delete = note.event_calendar.Where(n => n.series == event_to_delete.series).ToList();
                                        notes_to_delete.ForEach(n => { note.event_calendar.Remove(n); });
                                        note.SaveChanges();

                                        this.GetData();
                                        this.FillForm();
                                        this.calendar.RefreshViewDates(notes_to_delete.Select(n => n.date).ToList());

                                    }
                                    if (conf.delete_method == DialogConfirmDeleteAbsentRange.DELETE_METHOD.ONE)
                                    {
                                        note.event_calendar.Remove(event_to_delete);
                                        note.SaveChanges();
                                        this.custom_date_event.RefreshView();
                                        this.GetData();
                                        this.FillForm();
                                    }
                                }
                                else
                                {
                                    this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                                }
                            }
                            else
                            {
                                note.event_calendar.Remove(event_to_delete);
                                note.SaveChanges();
                                this.custom_date_event.RefreshView();
                                this.GetData();
                                this.FillForm();
                            }
                        }
                        else
                        {
                            MessageAlert.Show("ข้อมูลที่ต้องการลบไม่มีอยู่ในระบบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                }
            }
            else
            {
                this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
            }
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
            this.ResetFormState(FORM_MODE.READ_ITEM);
            
            int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;
            Console.WriteLine(" ==> " + row_index);
            if(row_index > -1)
            {
                this.btnEditItem.PerformClick();
            }
            if(row_index == -1)
            {
                this.btnAddItem.PerformClick();
            }
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SetInlineFormPosition();
            }
        }

        private void PerformEdit(object sender, EventArgs e)
        {
            this.btnEdit.PerformClick();
        }

        private void rdHoliday_CheckedChanged(object sender, EventArgs e)
        {
            if(this.tmp_note_calendar != null)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.tmp_note_calendar.type = (int)CALENDAR_NOTE_TYPE.HOLIDAY;
                    this.tmp_note_calendar.description = this.txtHoliday._Text;
                    this.txtHoliday._ReadOnly = false;
                    this.txtRemark._ReadOnly = true;
                    this.dlGroupHoliday._ReadOnly = true;
                    this.dlGroupMaid._ReadOnly = true;
                    this.numLeaveCount.Enabled = false;
                }
            }
        }

        private void rdWeekday_CheckedChanged(object sender, EventArgs e)
        {
            if(this.tmp_note_calendar != null)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.tmp_note_calendar.type = (int)CALENDAR_NOTE_TYPE.WEEKDAY;
                    this.tmp_note_calendar.description = this.txtRemark._Text;
                    this.txtHoliday._ReadOnly = true;
                    this.txtRemark._ReadOnly = false;
                    this.dlGroupHoliday._ReadOnly = false;
                    this.dlGroupMaid._ReadOnly = false;
                    this.numLeaveCount.Enabled = true;
                }
            }
        }

        private void txtHoliday__TextChanged(object sender, EventArgs e)
        {
            if(this.tmp_note_calendar != null)
            {
                if (this.tmp_note_calendar.type == (int)CALENDAR_NOTE_TYPE.HOLIDAY)
                    this.tmp_note_calendar.description = ((XTextEdit)sender)._Text;
            }
        }

        private void dlGroupHoliday__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_note_calendar != null)
                this.tmp_note_calendar.group_weekend = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void dlGroupMaid__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_note_calendar != null)
                this.tmp_note_calendar.group_maid = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void numLeaveCount_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_note_calendar != null)
                this.tmp_note_calendar.max_leave = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void txtRemark__TextChanged(object sender, EventArgs e)
        {
            if(this.tmp_note_calendar != null)
            {
                if (this.tmp_note_calendar.type == (int)CALENDAR_NOTE_TYPE.WEEKDAY)
                    this.tmp_note_calendar.description = ((XTextEdit)sender)._Text;
            }
        }

        private void inlineCodeName__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
            {
                this.tmp_event_calendar.users_name = ((InlineAbsentUser)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value).username;
                this.tmp_event_calendar.realname = ((InlineAbsentUser)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value).name;
            }
        }

        private void inlineReason__SelectedItemChanged(object sender, EventArgs e)
        {
            if(this.tmp_event_calendar != null)
            {
                this.tmp_event_calendar.event_code_id = ((InlineAbsentReason)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value).id;
                this.tmp_event_calendar.event_type = ((InlineAbsentReason)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value).tabtyp;
                this.tmp_event_calendar.event_code = ((InlineAbsentReason)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value).typcod;
            }
        }

        private void inlineFrom_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.from_time = ((XTimePicker)sender).Text;
        }

        private void inlineTo_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.to_time = ((XTimePicker)sender).Text;
        }

        private void inlineStatus__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.status = (int)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineRemark__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.customer = ((XTextEdit)sender)._Text;
        }

        private void inlineMedCert__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.med_cert = (string)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void inlineFine_ValueChanged(object sender, EventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.fine = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void inlineFine_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.tmp_event_calendar != null)
                this.tmp_event_calendar.fine = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if ((this.form_mode == FORM_MODE.EDIT && this.txtRemark._Focused) ||
                        ((this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM) && this.inlineFine.Focused))
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

            if (keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                this.btnItem.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnAddItem.PerformClick();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnEdit.PerformClick();
                    return true;
                }
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnEditItem.PerformClick();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.D))
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnDeleteItem.PerformClick();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.C))
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnCopyItem.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                try
                {
                    int user_level = ((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).ToViewModel().users.level;
                    if (user_level >= (int)USER_LEVEL.SUPERVISOR)
                    {
                        e.CellStyle.BackColor = Color.Bisque;
                        e.CellStyle.SelectionBackColor = Color.Bisque;
                        e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                        e.Handled = true;
                    }
                    else if (user_level < (int)USER_LEVEL.SUPERVISOR)
                    {
                        if (((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).status == (int)CALENDAR_EVENT_STATUS.WAIT)
                        {
                            e.CellStyle.BackColor = Color.Lavender;
                            e.CellStyle.SelectionBackColor = Color.Lavender;
                            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                            e.Handled = true;
                        }
                        else if (((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).status == (int)CALENDAR_EVENT_STATUS.CANCELED)
                        {
                            e.CellStyle.BackColor = Color.MistyRose;
                            e.CellStyle.SelectionBackColor = Color.MistyRose;
                            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                            e.Handled = true;
                        }
                        else
                        {
                            if(((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_event_calendar.Name].Value).id > -1)
                            {
                                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_seq.Name].Value = e.RowIndex + 1;
                                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                                e.Handled = true;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                    e.Handled = true;
                }
            }
            else
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                e.Handled = true;
            }
        }
    }

    public class InlineAbsentReason
    {
        public int? id { get; set; }
        public string tabtyp { get; set; }
        public string typcod { get; set; }
    }

    public class InlineAbsentUser
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
    }

    public class ValidateDataResult
    {
        public bool passed { get; set; }
        public Control invalid_control_data { get; set; }
        public string message { get; set; }
    }
}
