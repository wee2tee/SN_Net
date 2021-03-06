﻿using System;
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
    public partial class FormCalendar : Form
    {
        private MainForm main_form;
        //private int year = DateTime.Now.Year;
        //private int month = DateTime.Now.Month;
        //private DateTime first_date_of_month
        //{
        //    get
        //    {
        //        return DateTime.Parse(this.year.ToString() + "-" + this.month.ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));
        //    }
        //}
        private DateTime first_date_of_month;
        public DateTime current_date = DateTime.Now;
        private enum MONTH : int
        {
            มกราคม = 1,
            กุมภาพันธ์ = 2,
            มีนาคม = 3,
            เมษายน = 4,
            พฤษภาคม = 5,
            มิถุนายน = 6,
            กรกฎาคม = 7,
            สิงหาคม = 8,
            กันยายน = 9,
            ตุลาคม = 10,
            พฤศจิกายน = 11,
            ธันวาคม = 12
        }

        public FormCalendar(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void FormCalendar_Load(object sender, EventArgs e)
        {
            //this.year = DateTime.Now.Year;
            //this.month = DateTime.Now.Month;
            this.first_date_of_month = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));

            foreach (var month in Enum.GetValues(typeof(MONTH)))
            {
                this.cbMonth.Items.Add(month);
            }
            this.cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            for (int i = 5; i > -30; i--)
            {
                this.cbYear.Items.Add(DateTime.Now.Year + i);
            }
            this.cbYear.SelectedIndex = this.cbYear.Items.IndexOf(this.cbYear.Items.Cast<int>().Where(y => y == DateTime.Now.Year).First());

            this.btnAbsentRange.Visible = this.main_form.loged_in_user.level >= (int)USER_LEVEL.SUPERVISOR ? true : false;
            this.btnGo.PerformClick();
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ToolStripComboBox)sender).Items == null)
                return;
            this.first_date_of_month = DateTime.Parse(this.first_date_of_month.Year.ToString() + "-" + (((ToolStripComboBox)sender).SelectedIndex + 1).ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ToolStripComboBox)sender).Items == null)
                return;
            this.first_date_of_month = DateTime.Parse(((ToolStripComboBox)sender).Text + "-" + this.first_date_of_month.Month.ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Visible = false;

            DateTime first_date = DateTime.Parse(this.first_date_of_month.Year.ToString() + "-" + this.first_date_of_month.Month.ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));
            int days_in_month = DateTime.DaysInMonth((this.first_date_of_month.Year), this.first_date_of_month.Month);
            DateTime last_date = first_date.AddDays(days_in_month - 1);
            int first_day_of_week = first_date.GetDayIntOfWeek();

            //string from_date = DateTime.Parse(this.year.ToString() + "/" + this.month.ToString() + "/1", CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None).ToMysqlDate();
            //string to_date = DateTime.Parse(this.year.ToString() + "/" + this.month.ToString() + "/" + days_in_month.ToString(), CultureInfo.GetCultureInfo("th-TH"), DateTimeStyles.None).ToMysqlDate();

            //List<EventCalendar> event_cal;
            //List<TrainingCalendar> training_cal;
            //List<NoteCalendar> note_cal;

            //CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event&from_date=" + from_date + "&to_date=" + to_date);
            //ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            //if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            //{
            //    event_cal = sr.event_calendar;
            //    training_cal = sr.training_calendar;
            //    note_cal = sr.note_calendar;
            //}
            //else
            //{
            //    event_cal = new List<EventCalendar>();
            //    training_cal = new List<TrainingCalendar>();
            //    note_cal = new List<NoteCalendar>();
            //}

            //List<Istab> absent_cause = IstabWindow.GetIstab(Istab.getTabtypString(Istab.TABTYP.ABSENT_CAUSE));
            //List<Users> users_list = UsersList.GetUsers();

            List<event_calendar> event_cal;
            List<training_calendar> training_cal;
            List<note_calendar> note_cal;
            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                event_cal = sn_note.event_calendar.Include("note_istab").Where(c => c.date.CompareTo(first_date) >= 0 && c.date.CompareTo(last_date) <= 0).ToList();
                training_cal = sn_note.training_calendar.Where(c => c.date.CompareTo(first_date) >= 0 && c.date.CompareTo(last_date) <= 0).ToList();
                note_cal = sn_note.note_calendar.Where(c => c.date.CompareTo(first_date) >= 0 && c.date.CompareTo(last_date) <= 0).ToList();

                int increase_date = 0 + ((first_day_of_week - 1) * -1);
                for (int i = 1; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    for (int j = 0; j < this.tableLayoutPanel1.ColumnCount; j++)
                    {

                        // remove existing control
                        if (this.tableLayoutPanel1.GetControlFromPosition(j, i) != null)
                            this.tableLayoutPanel1.Controls.Remove(this.tableLayoutPanel1.GetControlFromPosition(j, i));

                        //// create new control
                        //NoteCalendar note = note_cal.Where(n => n.date == first_date.AddDays(increase_date).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))).FirstOrDefault();
                        //int max_leave = note != null ? note.max_leave : -1;
                        //List<AbsentVM> absent_list = event_cal.Where(ev => ev.date == first_date.AddDays(increase_date).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))).ToAbsentViewModel(absent_cause, users_list, max_leave);
                        //var trainer = training_cal.Where(t => t.date == first_date.AddDays(increase_date).ToString("yyyy-MM-dd", CultureInfo.GetCultureInfo("en-US"))).ToList();
                        ////var note = note_cal;

                        //CustomDateEvent2 de = new CustomDateEvent2(this.main_form, this, first_date.AddDays(increase_date), this.month, absent_list, absent_cause, trainer, note, users_list, max_leave);
                        var curr_date = first_date.AddDays(increase_date);
                        var event_list = event_cal.Where(c => c.date.CompareTo(curr_date) >= 0 && c.date.CompareTo(curr_date) <= 0).OrderBy(c => c.ToViewModel().users.level).ThenBy(c => c.id).ToList();
                        var note = note_cal.Where(c => c.date.CompareTo(curr_date) >= 0 && c.date.CompareTo(curr_date) <= 0).FirstOrDefault();
                        var training_list = training_cal.Where(c => c.date.CompareTo(curr_date) >= 0 && c.date.CompareTo(curr_date) <= 0).ToList();
                        //de = new CustomDateEvent3(this.main_form, curr_date, event_list, note, training_list);


                        CustomDateEvent3 de = new CustomDateEvent3(this.main_form, this, curr_date, first_date, event_list, note, training_list);
                        this.tableLayoutPanel1.Controls.Add(de, j, i);
                        increase_date++;
                    }
                }

                this.tableLayoutPanel1.Visible = true;
            }
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            this.first_date_of_month = this.first_date_of_month.AddMonths(-1);
            this.cbMonth.SelectedIndex = this.first_date_of_month.Month - 1;
            this.cbYear.Text = this.first_date_of_month.Year.ToString();

            this.btnGo.PerformClick();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            this.first_date_of_month = this.first_date_of_month.AddMonths(1);
            this.cbMonth.SelectedIndex = this.first_date_of_month.Month - 1;
            this.cbYear.Text = this.first_date_of_month.Year.ToString();

            this.btnGo.PerformClick();
        }

        private void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            this.cbYear.SelectedIndex = this.cbYear.Items.IndexOf(this.cbYear.Items.Cast<int>().Where(y => y == DateTime.Now.Year + 543).First());
            this.cbMonth.SelectedIndex = DateTime.Now.Month - 1;

            this.btnGo.PerformClick();
        }

        private void btnUserGroup_Click(object sender, EventArgs e)
        {
            if (this.main_form.usersgroup_wind == null)
            {
                this.main_form.usersgroup_wind = new UsersGroupWindow(this.main_form);
                this.main_form.usersgroup_wind.MdiParent = this.main_form;
                this.main_form.usersgroup_wind.Show();
            }
            else
            {
                this.main_form.usersgroup_wind.Activate();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //this.main_form.calendar_wind = null;
            this.main_form.form_calendar = null;
            base.OnClosing(e);
        }

        private void btnYearlyHoliday_Click(object sender, EventArgs e)
        {
            //YearSelectDialog ys = new YearSelectDialog(DateTime.Now.Year);
            //if (ys.ShowDialog() == DialogResult.OK)
            //{
            //    YearlyHolidayDialog yh = new YearlyHolidayDialog(this.main_form, this, ys.selected_year);
            //    yh.ShowDialog();
            //}
        }

        //public void RefreshAtDate(DateTime date)
        //{
        //    foreach (var ct in this.tableLayoutPanel1.Controls)
        //    {
        //        if (ct.GetType() != typeof(CustomDateEvent2))
        //            continue;

        //        CustomDateEvent2 de = ct as CustomDateEvent2;
        //        if (de.date.HasValue && de.date.Value == date)
        //        {
        //            de.RefreshData();
        //            de.RefreshView();
        //        }
        //    }
        //}

        public void RefreshViewDates(List<DateTime> dates)
        {
            foreach (var ct in this.tableLayoutPanel1.Controls)
            {
                if (ct.GetType() != typeof(CustomDateEvent3))
                    continue;

                if(dates.Where(d => d.Date == ((CustomDateEvent3)ct).curr_date.Date).Count() > 0 &&
                    ((CustomDateEvent3)ct).curr_date.ToString("MMyyyy") == this.first_date_of_month.ToString("MMyyyy"))
                {
                    ((CustomDateEvent3)ct).RefreshView();
                }
            }
        }

        private void btnKeptIstab_Click(object sender, EventArgs e)
        {
            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                try
                {
                    var event_cal = sn_note.event_calendar.ToList();
                    var istab = sn_note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT || i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).ToList();
                    foreach (var ev in event_cal)
                    {
                        var istab_event = istab.Where(i => i.typcod == ev.event_code).FirstOrDefault();
                        ev.event_code_id = istab_event != null ? (int?)istab_event.id : null;
                    }
                    sn_note.SaveChanges();
                    MessageAlert.Show("Rec. event_code_id completed.");
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message);
                }
            }
        }

        private void btnCurrentMonth_Click_1(object sender, EventArgs e)
        {
            this.first_date_of_month = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1", CultureInfo.GetCultureInfo("en-US"));
            this.cbMonth.SelectedIndex = this.first_date_of_month.Month - 1;
            this.cbYear.Text = this.first_date_of_month.Year.ToString();
            this.btnGo.PerformClick();
        }

        private void btnYearlyHoliday_Click_1(object sender, EventArgs e)
        {
            DialogSelectYear sel = new DialogSelectYear();
            if(sel.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(sel.selected_year.ToString());
                DialogYearlyHoliday hol = new DialogYearlyHoliday(this.main_form, sel.selected_year);
                hol.ShowDialog();
            }
        }

        private void btnAbsentRange_Click(object sender, EventArgs e)
        {
            DialogAbsentRange abs = new DialogAbsentRange(this.main_form);
            if(abs.ShowDialog() == DialogResult.OK)
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    abs.event_list.ForEach(ev => { note.event_calendar.Add(ev); });
                    note.SaveChanges();
                    this.RefreshViewDates(abs.event_list.Select(ev => ev.date).ToList());
                }
            }
        }
    }
}
