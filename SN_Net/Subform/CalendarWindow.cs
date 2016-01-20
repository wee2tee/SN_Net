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
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class CalendarWindow : Form
    {
        private MainForm main_form;
        private int curr_year;
        private int curr_month;
        //private int curr_day;
        private DateTime first_day;
        private DateTime last_day;
        private List<EventCalendar> month_event = new List<EventCalendar>();
        private List<TrainingCalendar> month_training = new List<TrainingCalendar>();
        private List<NoteCalendar> month_note = new List<NoteCalendar>();
        public List<Users> list_users = new List<Users>();
        CultureInfo cinfo_th = new CultureInfo("th-TH"); // for display in UI
        CultureInfo cinfo_us = new CultureInfo("en-US"); // for calculate/processing data
        private delegate void delegateUpdateDateEventUI(CustomDateEvent de, List<EventCalendar> list_event_calendar, List<TrainingCalendar> list_training_calendar, NoteCalendar note_calendar, int target_month);
        private delegate void delegateRefreshDateEventUI();

        public CalendarWindow()
        {
            InitializeComponent();
        }

        public CalendarWindow(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
        }

        private void CalendarWindow_Load(object sender, EventArgs e)
        {
            this.LoadDependenciesData();

            #region Load Month name to cbMonth
            this.cbMonth.Items.Add(new ComboboxItem("มกราคม", 1, "01"));
            this.cbMonth.Items.Add(new ComboboxItem("กุมภาพันธ์", 2, "02"));
            this.cbMonth.Items.Add(new ComboboxItem("มีนาคม", 3, "03"));
            this.cbMonth.Items.Add(new ComboboxItem("เมษายน", 4, "04"));
            this.cbMonth.Items.Add(new ComboboxItem("พฤษภาคม", 5, "05"));
            this.cbMonth.Items.Add(new ComboboxItem("มิถุนายน", 6, "06"));
            this.cbMonth.Items.Add(new ComboboxItem("กรกฎาคม", 7, "07"));
            this.cbMonth.Items.Add(new ComboboxItem("สิงหาคม", 8, "08"));
            this.cbMonth.Items.Add(new ComboboxItem("กันยายน", 9, "09"));
            this.cbMonth.Items.Add(new ComboboxItem("ตุลาคม", 10, "10"));
            this.cbMonth.Items.Add(new ComboboxItem("พฤศจิกายน", 11, "11"));
            this.cbMonth.Items.Add(new ComboboxItem("ธันวาคม", 12, "12"));

            this.cbMonth.SelectedIndex = DateTime.Now.Month - 1;
            #endregion Load Month name to cbMonth

            #region Load Year to cbYear
            for(int i = 2400 ; i < 2700 ; i++ ){
                ComboboxItem item = new ComboboxItem(i.ToString(), i - 543, i.ToString());
                this.cbYear.Items.Add(item);
                if (item.int_value == DateTime.Now.Year)
                {
                    this.cbYear.SelectedItem = item;
                }
            }
            #endregion Load Year to cbYear

            #region Binding event handler to cbMonth,cbYear
            this.cbMonth.SelectedIndexChanged += delegate
            {
                //this.LoadCalendar(((ComboboxItem)this.cbMonth.SelectedItem).int_value, ((ComboboxItem)this.cbYear.SelectedItem).int_value);
                this.btnLoadCalendar.PerformClick();
            };
            this.cbYear.SelectedIndexChanged += delegate
            {
                //this.LoadCalendar(((ComboboxItem)this.cbMonth.SelectedItem).int_value, ((ComboboxItem)this.cbYear.SelectedItem).int_value);
                this.btnLoadCalendar.PerformClick();
            };
            #endregion Binding event handler to cbMonth,cbYear
        }

        private void CalendarWindow_Shown(object sender, EventArgs e)
        {
            this.LoadCalendar(DateTime.Now.Month, DateTime.Now.Year);
            this.toolStripRangeLeave.Visible = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : true);
            this.toolStripUsersGroup.Visible = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : true);
        }

        private void LoadDependenciesData()
        {
            CRUDResult get_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_users = JsonConvert.DeserializeObject<ServerResult>(get_users.data);

            if (sr_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_users = sr_users.users;
            }
        }

        private void LoadCalendar(int month, int year)
        {
            DateTime out_firstday;
            if (DateTime.TryParse(year.ToString() + "/" + month.ToString() + "/1", cinfo_us, DateTimeStyles.None, out out_firstday))
            {
                this.btnPrevMonth.Enabled = false;
                this.btnNextMonth.Enabled = false;
                this.cbMonth.Enabled = false;
                this.cbYear.Enabled = false;

                #region Set calendar ready to false
                for (int i = 0; i <= 5; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        CustomDateEvent de = (CustomDateEvent)this.tableLayout1.GetControlFromPosition(j, i);
                        de.Ready = false;
                    }
                }
                #endregion Set calendar ready to false

                #region Create calendar
                this.first_day = out_firstday;
                this.curr_year = out_firstday.Year;
                this.curr_month = out_firstday.Month;

                int days_in_month = DateTime.DaysInMonth(year, month);
                for (int i = out_firstday.GetDayIntOfWeek() - 1; i > 0; i--)
                {
                    CustomDateEvent de = (CustomDateEvent)this.tableLayout1.GetControlFromPosition(i - 1, 0);
                    de.Date = out_firstday.AddDays(i - out_firstday.GetDayIntOfWeek());
                    de.TargetMonth = out_firstday.Month;
                    de.G = this.main_form.G;
                    de.calendar_window = this;
                    de.btnAdd.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                    de.btnDetail.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                    de.btnTraining.Enabled = (this.main_form.G.loged_in_user_training_expert == true || this.main_form.G.loged_in_user_level >= GlobalVar.USER_LEVEL_SUPERVISOR ? true : false);
                    //de.RefreshView();
                }

                int row_index = 0;
                for (int i = 0; i < days_in_month; i++)
                {
                    int col_index = out_firstday.AddDays(i).GetDayIntOfWeek();
                    CustomDateEvent de = (CustomDateEvent)this.tableLayout1.GetControlFromPosition(col_index - 1, row_index);
                    de.Date = out_firstday.AddDays(i);
                    de.TargetMonth = out_firstday.Month;
                    de.G = this.main_form.G;
                    de.calendar_window = this;
                    de.btnAdd.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                    de.btnDetail.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                    de.btnTraining.Enabled = (this.main_form.G.loged_in_user_training_expert == true || this.main_form.G.loged_in_user_level >= GlobalVar.USER_LEVEL_SUPERVISOR ? true : false);
                    // increase row_index
                    row_index += (out_firstday.AddDays(i).GetDayIntOfWeek() == 7 ? 1 : 0);
                }

                int add_date = 0;
                int day_of_week = out_firstday.AddDays(days_in_month).GetDayIntOfWeek(); // first day of next month
                for (int i = row_index; i < 6; i++)
                {
                    for (int j = day_of_week; j <= 7; j++)
                    {
                        int col_index = j - 1;
                        add_date++;
                        CustomDateEvent de = (CustomDateEvent)this.tableLayout1.GetControlFromPosition(col_index, row_index);
                        de.Date = out_firstday.AddDays(days_in_month + (add_date - 1));
                        de.TargetMonth = out_firstday.Month;
                        de.G = this.main_form.G;
                        de.calendar_window = this;
                        de.btnAdd.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                        de.btnDetail.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : Enabled);
                        de.btnTraining.Enabled = (this.main_form.G.loged_in_user_training_expert == true || this.main_form.G.loged_in_user_level >= GlobalVar.USER_LEVEL_SUPERVISOR ? true : false);
                    }
                    row_index++;
                    day_of_week = 1;
                }
                #endregion Create calendar

                #region Set event_list for each date
                string from_date = DateTime.Parse(year.ToString() + "/" + month.ToString() + "/1", cinfo_us, DateTimeStyles.None).ToMysqlDate();
                string to_date = DateTime.Parse(year.ToString() + "/" + month.ToString() + "/" + days_in_month.ToString(), cinfo_us, DateTimeStyles.None).ToMysqlDate();

                bool get_success = false;
                string err_msg = "";

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event&from_date=" + from_date + "&to_date=" + to_date);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        get_success = true;
                        this.month_event = sr.event_calendar;
                        this.month_training = sr.training_calendar;
                        this.month_note = sr.note_calendar;
                    }
                    else
                    {
                        get_success = false;
                        err_msg = sr.message;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (get_success)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            for (int j = 0; j <= 6; j++)
                            {
                                CustomDateEvent de = (CustomDateEvent)this.tableLayout1.GetControlFromPosition(j, i);
                                BackgroundWorker subworker = new BackgroundWorker();
                                subworker.DoWork += delegate
                                {
                                    de.RefreshView(this.list_users, this.month_event.Where<EventCalendar>(t => t.date == de.Date.ToMysqlDate()).ToList<EventCalendar>(), this.month_training.Where<TrainingCalendar>(t => t.date == de.Date.ToMysqlDate()).ToList<TrainingCalendar>(), this.month_note.Find(t => t.date == de.Date.ToMysqlDate()), this.curr_month);
                                };
                                subworker.RunWorkerCompleted += delegate
                                {

                                };
                                subworker.RunWorkerAsync();
                                
                                /******************/

                                //de.list_users = this.list_users;
                                //de.SetEventList(this.month_event.Where<EventCalendar>(t => t.date == de.Date.ToMysqlDate()).ToList<EventCalendar>());
                                //de.SetTrainingList(this.month_training.Where<TrainingCalendar>(t => t.date == de.Date.ToMysqlDate()).ToList<TrainingCalendar>());
                                //de.note_calendar = this.month_note.Find(t => t.date == de.Date.ToMysqlDate());
                                //de.TargetMonth = this.curr_month;
                                //de.RefreshView();
                            }
                        }
                        this.btnPrevMonth.Enabled = true;
                        this.btnNextMonth.Enabled = true;
                        this.cbMonth.Enabled = true;
                        this.cbYear.Enabled = true;
                    }
                    else
                    {
                        this.btnPrevMonth.Enabled = true;
                        this.btnNextMonth.Enabled = true;
                        this.cbMonth.Enabled = true;
                        this.cbYear.Enabled = true;
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();

                #endregion Set event_list for each date
            }
        }
        private void UpdateDateEventUI(CustomDateEvent de, List<Users> list_users, List<EventCalendar> list_event_calendar, List<TrainingCalendar> list_training_calendar, NoteCalendar note_calendar, int current_month)
        //private void UpdateDateEventUI()
        {
            de.list_users = list_users;
            de.SetEventList(list_event_calendar);
            de.SetTrainingList(list_training_calendar);
            de.note_calendar = note_calendar;
            de.TargetMonth = current_month;
            de.RefreshView();
            //Console.WriteLine(list_users[0]);
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            DateTime prev_mon = this.first_day.AddMonths(-1);
            this.cbMonth.SelectedIndex = prev_mon.Month - 1;
            foreach (ComboboxItem item in this.cbYear.Items)
            {
                if (item.int_value == prev_mon.Year)
                {
                    this.cbYear.SelectedItem = item;
                }
            }
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            DateTime next_mon = this.first_day.AddMonths(1);
            this.cbMonth.SelectedIndex = next_mon.Month - 1;
            foreach (ComboboxItem item in this.cbYear.Items)
            {
                if (item.int_value == next_mon.Year)
                {
                    this.cbYear.SelectedItem = item;
                }
            }
        }

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            this.LoadCalendar(this.curr_month, this.curr_year);
        }

        private void toolStripRangeLeave_Click(object sender, EventArgs e)
        {
            LeaveRange wind = new LeaveRange(this.main_form);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                MessageAlert.Show("บันทึกข้อมูลเรียบร้อย", "", MessageAlertButtons.OK, MessageAlertIcons.INFORMATION);

                if ((wind.dtDateStart.Value.Year == this.curr_year && wind.dtDateStart.Value.Month == this.curr_month) || (wind.dtDateEnd.Value.Year == this.curr_year && wind.dtDateEnd.Value.Month == this.curr_month))
                {
                    this.LoadCalendar(this.curr_month, this.curr_year);
                }
            }
        }

        private void toolStripUsersGroup_Click(object sender, EventArgs e)
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
            this.main_form.calendar_wind = null;
            base.OnClosing(e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.btnLoadCalendar.Enabled = false;
            this.cbMonth.SelectedItem = this.cbMonth.Items.Cast<ComboboxItem>().Where(i => i.int_value == DateTime.Now.Month).First<ComboboxItem>();
            this.cbYear.SelectedItem = this.cbYear.Items.Cast<ComboboxItem>().Where(i => i.int_value == DateTime.Now.Year).First<ComboboxItem>();
            this.btnLoadCalendar.Enabled = true;
            this.btnLoadCalendar.PerformClick();
        }

        private void btnLoadCalendar_Click(object sender, EventArgs e)
        {
            this.LoadCalendar(((ComboboxItem)this.cbMonth.SelectedItem).int_value, ((ComboboxItem)this.cbYear.SelectedItem).int_value);
        }
    }
}
