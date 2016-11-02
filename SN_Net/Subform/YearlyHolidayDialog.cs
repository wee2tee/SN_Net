using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using SN_Net.DataModels;
using WebAPI.ApiResult;
using WebAPI;
using SN_Net.MiscClass;
using Newtonsoft.Json;
using SN_Net.ViewModels;

namespace SN_Net.Subform
{
    public partial class YearlyHolidayDialog : Form
    {
        private MainForm main_form;
        private Calendar2 calendar;
        private int current_year;
        public List<NoteCalendarVM> holidays;
        public BindingSource bs;

        public YearlyHolidayDialog(MainForm main_form, Calendar2 calendar, int year)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();

            this.main_form = main_form;
            this.calendar = calendar;
            this.current_year = year;
        }

        private void YearlyHolidayDialog_Load(object sender, EventArgs e)
        {
            this.Text += " " + (this.current_year + 543).ToString();

            this.holidays = GetNoteCalendarList(this.current_year).ToHolidayViewModel();

            this.bs = new BindingSource();
            this.bs.DataSource = this.holidays;

            this.dgv.DataSource = this.bs;
            this.dgv.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("th-TH");

            this.btnAdd.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
        }

        public static List<NoteCalendar> GetNoteCalendarList(int year)
        {
            string url = PreferenceForm.API_MAIN_URL() + "notecalendar/get_holiday_note&year=" + year.ToString();
            CRUDResult get = ApiActions.GET(url);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.note_calendar;
            }
            else
            {
                return null;
            }
        }

        public static NoteCalendar GetSingleNoteCalendar(DateTime date)
        {
            string url = PreferenceForm.API_MAIN_URL() + "notecalendar/get_single_note&date=" + date.ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"));
            CRUDResult get = ApiActions.GET(url);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.note_calendar.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static bool CreateHolidayNote(NoteCalendar note)
        {
            string url = PreferenceForm.API_MAIN_URL() + "notecalendar/create_holiday_note";
            string json_string = "{\"date\":\"" + note.date + "\",";
            json_string += "\"type\":" + note.type.ToString() + ",";
            json_string += "\"description\":\"" + note.description + "\",";
            json_string += "\"group_maid\":\"" + note.group_maid + "\",";
            json_string += "\"group_weekend\":\"" + note.group_weekend + "\",";
            json_string += "\"max_leave\":" + note.max_leave.ToString() + ",";
            json_string += "\"rec_by\":\"" + note.rec_by + "\"}";
            
            CRUDResult get = ApiActions.POST(url, json_string);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return true;
            }
            else if (sr.result == ServerResult.SERVER_CREATE_RESULT_FAILED_EXIST)
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return false;
            }
            else
            {
                if (MessageAlert.Show(sr.message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    return CreateHolidayNote(note);
                }

                return false;
            }
        }

        public static bool UpdateHolidayNote(NoteCalendar note)
        {
            string url = PreferenceForm.API_MAIN_URL() + "notecalendar/update_holiday_note";
            string json_string = "{\"id\":" + note.id.ToString() + ",";
            json_string += "\"date\":\"" + note.date + "\",";
            json_string += "\"type\":" + note.type.ToString() + ",";
            json_string += "\"description\":\"" + note.description + "\",";
            json_string += "\"group_maid\":\"" + note.group_maid + "\",";
            json_string += "\"group_weekend\":\"" + note.group_weekend + "\",";
            json_string += "\"max_leave\":" + note.max_leave.ToString() + ",";
            json_string += "\"rec_by\":\"" + note.rec_by + "\"}";

            CRUDResult get = ApiActions.POST(url, json_string);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return true;
            }
            else
            {
                if (MessageAlert.Show(sr.message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    return UpdateHolidayNote(note);
                }

                return false;
            }
        }

        public static bool DeleteHolidayNote(NoteCalendar note)
        {
            string url = PreferenceForm.API_MAIN_URL() + "notecalendar/delete_holiday_note&id=" + note.id.ToString() + "&rec_by=" + note.rec_by;
            CRUDResult get = ApiActions.DELETE(url);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return true;
            }
            else
            {
                if (MessageAlert.Show(sr.message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    return DeleteHolidayNote(note);
                }

                return false;
            }
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell == null)
                return;

            Rectangle rect = ((DataGridView)sender).GetRowDisplayRectangle(((DataGridView)sender).CurrentCell.RowIndex, true);
            using (Pen p = new Pen(Color.Red))
            {
                e.Graphics.DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                e.Graphics.DrawLine(p, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).CurrentCell == null)
                return;

            if (((DataGridView)sender).Rows.Count > 0 && ((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells["colNoteCalendar"].Value.GetType() == typeof(NoteCalendar))
            {
                this.btnEdit.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
                this.btnDelete.Enabled = this.main_form.G.loged_in_user_level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
            }
            else
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            YearlyHolidayAddEditDialog add_form = new YearlyHolidayAddEditDialog(this.main_form);
            if (add_form.ShowDialog() == DialogResult.OK)
            {
                if (CreateHolidayNote(add_form.note_calendar) == true)
                {
                    this.bs.ResetBindings(true);
                    this.bs.DataSource = GetNoteCalendarList(this.current_year).ToHolidayViewModel();
                    this.calendar.RefreshAtDate(add_form.note_calendar._Date);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            NoteCalendar note_calendar = (NoteCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["colNoteCalendar"].Value;
            YearlyHolidayAddEditDialog edit_form = new YearlyHolidayAddEditDialog(this.main_form, note_calendar);
            if (edit_form.ShowDialog() == DialogResult.OK)
            {
                if (UpdateHolidayNote(edit_form.note_calendar) == true)
                {
                    this.bs.ResetBindings(true);
                    this.bs.DataSource = GetNoteCalendarList(this.current_year).ToHolidayViewModel();
                    this.calendar.RefreshAtDate(edit_form.note_calendar._Date);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NoteCalendar note_calendar = (NoteCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["colNoteCalendar"].Value;
            note_calendar.rec_by = this.main_form.G.loged_in_user_name;
            if (MessageAlert.Show("ลบวันที่ " + note_calendar._Date.ToString("d MMM yy", CultureInfo.GetCultureInfo("th-TH")) + " ออกจากว้นหยุดประจำปี, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                if (DeleteHolidayNote(note_calendar) == true)
                {
                    this.bs.ResetBindings(true);
                    this.bs.DataSource = GetNoteCalendarList(this.current_year).ToHolidayViewModel();
                    this.calendar.RefreshAtDate(note_calendar._Date);
                }
            }
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int row_index = ((DataGridView)sender).HitTest(e.X, e.Y).RowIndex;
                if (row_index < 0)
                    return;

                ((DataGridView)sender).Rows[row_index].Cells[1].Selected = true;

                ContextMenu cm = new ContextMenu();
                MenuItem mnu_add = new MenuItem();
                mnu_add.Text = "เพิ่ม";
                mnu_add.Click += delegate
                {
                    this.btnAdd.PerformClick();
                };
                cm.MenuItems.Add(mnu_add);

                MenuItem mnu_edit = new MenuItem();
                mnu_edit.Text = "แก้ไข";
                mnu_edit.Click += delegate
                {
                    this.btnEdit.PerformClick();
                };
                cm.MenuItems.Add(mnu_edit);

                MenuItem mnu_delete = new MenuItem();
                mnu_delete.Text = "ลบ";
                mnu_delete.Click += delegate
                {
                    this.btnDelete.PerformClick();
                };
                cm.MenuItems.Add(mnu_delete);

                cm.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            ((DataGridView)sender).Rows[e.RowIndex].Cells[1].Selected = true;
            this.btnEdit.PerformClick();
        }
    }
}
