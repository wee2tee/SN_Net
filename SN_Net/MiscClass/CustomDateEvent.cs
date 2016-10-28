using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using SN_Net.DataModels;
using SN_Net.Subform;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.MiscClass
{
    public partial class CustomDateEvent : UserControl
    {
        public GlobalVar G;
        //public MainForm main_form;
        public CalendarWindow calendar_window;
        private CultureInfo cinfo_th = new CultureInfo("th-TH"); // for display in Control
        private CultureInfo cinfo_us = new CultureInfo("en-US"); // for calculate/process data
        private bool ready;
        public bool Ready
        {
            get
            {
                return this.ready;
            }
            set
            {
                this.ready = value;
                if (this.ready)
                {
                    this.SetVisulControl();
                    this.dgv.CurrentCell = null;
                }
                else
                {
                    this.SetVisulControl();
                }
            }
        }
        private bool read_only;
        public bool Read_Only
        {
            get
            {
                return this.read_only;
            }
            set
            {
                this.read_only = value;
                this.SetVisulControl();
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                this.lblDay.Text = this.date.Day.ToString();
                this.lblMontYear.Text = this.date.ToString("MMMM yyyy", cinfo_th.DateTimeFormat);
            }
        }
        private bool is_shown;
        public bool IsShown
        {
            get
            {
                return this.is_shown;
            }
            set
            {
                this.is_shown = value;
            }
        }
        private int target_month = 0;
        public int TargetMonth
        {
            get
            {
                return this.target_month;
            }
            set
            {
                this.target_month = value;
                this.SetVisulControl();
            }
        }
        private delegate void delegateRefreshView();
        public List<Users> list_users;
        public List<EventCalendar> event_list;
        public List<TrainingCalendar> training_list;
        public NoteCalendar note_calendar;
        public static Color color_light_purple = Color.FromArgb(128, 128, 255);
        public static Color color_light_blue = Color.FromArgb(205, 240, 255);
        public static Color color_light_red = Color.FromArgb(255, 222, 222);
        public enum EVENT_STATUS : int
        {
            WAIT_FOR_CONFIRM = 0,
            CONFIRMED = 1,
            CANCELED = 2
        }
        public enum NOTE_TYPE : int
        {
            NOTE = 0,
            HOLIDAY = 1
        }

        public CustomDateEvent()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            InitializeComponent();

            this.is_shown = true;

            this.date = DateTime.Now;
        }

        private void CustomDateEvent_Load(object sender, EventArgs e)
        {
            this.Margin = new Padding(1);
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.None;
            this.Ready = true;

            this.BindingControlEvent();
            this.SetVisulControl();

            this.Visible = this.is_shown;
            
        }

        private void BindingControlEvent()
        {
            #region Nulling current dgv current cell when it's leave
            this.dgv.Leave += delegate
            {
                this.dgv.CurrentCell = null;
            };
            #endregion Nulling current dgv current cell when it's leave

            #region Show dgv row context menu
            this.dgv.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (!this.read_only)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        this.dgv.Focus();
                        int row_index = this.dgv.HitTest(e.X, e.Y).RowIndex;
                        if (row_index > -1 && this.G.loged_in_user_level >= GlobalVar.USER_LEVEL_SUPERVISOR && this.dgv.Rows[row_index].Tag is EventCalendar)
                        {
                            this.dgv.Rows[row_index].Cells[1].Selected = true;
                            ContextMenu m = new ContextMenu();
                            MenuItem m_edit = new MenuItem("แก้ไข");
                            m_edit.Click += delegate
                            {
                                //DateEventWindow wind = new DateEventWindow(this, true, (EventCalendar)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Tag);
                                //wind.ShowDialog();
                            };
                            m.MenuItems.Add(m_edit);

                            MenuItem m_copy = new MenuItem("คัดลอกไปยังวันที่ ...");
                            m_copy.Click += delegate
                            {
                                DateSelectorDialog ds = new DateSelectorDialog(this.Date);
                                if (ds.ShowDialog() == DialogResult.OK)
                                {
                                    this.DoCopy(ds.selected_date, (EventCalendar)this.dgv.Rows[row_index].Tag);
                                }
                            };
                            m.MenuItems.Add(m_copy);

                            MenuItem m_delete = new MenuItem("ลบ");
                            m_delete.Click += delegate
                            {
                                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                                {
                                    this.Ready = false;

                                    bool delete_success = false;
                                    string err_msg = "";
                                    BackgroundWorker worker = new BackgroundWorker();
                                    worker.DoWork += delegate
                                    {
                                        CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "eventcalendar/delete&id=" + ((EventCalendar)this.dgv.Rows[row_index].Tag).id.ToString());
                                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                                        {
                                            delete_success = true;
                                        }
                                        else
                                        {
                                            delete_success = false;
                                            err_msg = sr.message;
                                        }
                                    };
                                    worker.RunWorkerCompleted += delegate
                                    {
                                        if (delete_success)
                                        {
                                            this.RefreshData();
                                            this.RefreshView();
                                            this.Ready = true;
                                        }
                                        else
                                        {
                                            this.RefreshData();
                                            this.RefreshView();
                                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                                        }
                                    };
                                    worker.RunWorkerAsync();
                                }
                            };
                            m.MenuItems.Add(m_delete);

                            m.Show(this.dgv, new Point(e.X, e.Y));
                        }
                    }
                }
            };
            #endregion Show dgv row context menu
        }

        private void DoCopy(DateTime date, EventCalendar event_calendar)
        {
            bool post_success = false;
            string err_msg = "";
            int inserted_id = -1;

            this.Ready = false;

            string json_data = "{\"users_name\":\"" + event_calendar.users_name + "\",";
            json_data += "\"date\":\"" + date.ToMysqlDate() + "\",";
            json_data += "\"from_time\":\"" + event_calendar.from_time + "\",";
            json_data += "\"to_time\":\"" + event_calendar.to_time + "\",";
            json_data += "\"event_type\":\"" + event_calendar.event_type + "\",";
            json_data += "\"event_code\":\"" + event_calendar.event_code + "\",";
            json_data += "\"customer\":\"" + event_calendar.customer + "\",";
            json_data += "\"status\":\"" + event_calendar.status.ToString() + "\",";
            json_data += "\"med_cert\":\"" + event_calendar.med_cert + "\",";
            json_data += "\"fine\":" + event_calendar.fine.ToString() + ",";
            json_data += "\"rec_by\":\"" + this.G.loged_in_user_name + "\"}";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "eventcalendar/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    inserted_id = Convert.ToInt32(sr.message);
                }
                else
                {
                    post_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    foreach (CustomDateEvent ct in this.Parent.Controls)
                    {
                        if (ct.Date.ToDMYDateValue() == date.ToDMYDateValue())
                        {
                            ct.RefreshData();
                            ct.RefreshView();
                        }
                    }
                    if (this.Date.ToDMYDateValue() == date.ToDMYDateValue())
                    {
                        this.RefreshData();
                        this.RefreshView();
                        this.dgv.Rows[this.event_list.FindIndex(t => t.id == inserted_id)].Cells[1].Selected = true;
                    }
                    this.Ready = true;
                }
                else
                {
                    if (MessageAlert.Show(err_msg, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                    {
                        this.DoCopy(date, event_calendar);
                    }
                    this.Ready = true;
                }
            };
            worker.RunWorkerAsync();
        }

        public void SetVisulControl()
        {
            if (this.ready)
            {
                this.splitContainer2.Visible = true;
                if (this.date.GetDayIntOfWeek() == 1)
                {
                    this.lblDay.BackColor = Color.LightGray;
                    this.lblMontYear.ForeColor = Color.LightGray;
                    this.btnDetail.Visible = false;
                    this.btnAdd.Visible = false;
                    this.btnTraining.Visible = false;
                    this.bottomLabel.Visible = false;
                }
                else
                {
                    if (this.date.GetDayIntOfWeek() == 7 && this.date.AddDays(7).Month != this.date.Month)
                    {
                        this.lblDay.BackColor = Color.LightGray;
                        this.lblMontYear.ForeColor = Color.LightGray;
                        this.btnDetail.Visible = false;
                        this.btnAdd.Visible = false;
                        this.btnTraining.Visible = false;
                        this.bottomLabel.Visible = false;
                    }
                    else
                    {
                        if (this.note_calendar != null && this.note_calendar.type == (int)NOTE_TYPE.HOLIDAY)
                        {
                            this.lblDay.BackColor = Color.LightGray;
                            this.lblMontYear.ForeColor = Color.LightGray;
                            this.btnDetail.Visible = (this.read_only ? false : true);
                            this.btnAdd.Visible = false;
                            this.btnTraining.Visible = false;
                            this.dgv.Visible = false;
                            this.lblWeekend.Visible = true;
                            this.lblWeekend.ForeColor = Color.Red;
                            this.lblWeekend.TextAlign = ContentAlignment.MiddleCenter;
                            this.bottomLabel.Visible = false;
                            using (Font font = new Font("tahoma", 14f, FontStyle.Bold))
                            {
                                this.lblWeekend.Font = font;
                            }
                        }
                        else
                        {
                            this.bottomLabel.SetVisualControl(this.note_calendar, this.training_list);
                            //this.lblDay.BackColor = (this.date.Month != target_month ? Color.LightGray : color_light_purple);
                            //this.lblMontYear.ForeColor = (this.date.Month != target_month ? Color.LightGray : color_light_purple);
                            this.btnDetail.Visible = (this.date.Month != target_month ? false : (this.read_only ? false : true));
                            this.btnAdd.Visible = (this.date.Month != target_month ? false : (this.read_only ? false : true));
                            this.btnTraining.Visible = (this.date.Month != target_month ? false : (this.read_only ? false : true));
                            this.bottomLabel.Visible = (this.date.Month != target_month ? false : (this.read_only ? false : true));
                            this.dgv.Visible = true;
                            this.lblWeekend.Visible = false;
                            //this.lblWeekend.ForeColor = Color.Black;

                            //using (Font font = new Font("tahoma", 8.25f, FontStyle.Bold))
                            //{
                            //    this.lblWeekend.Font = font;
                            //}
                        }
                    }
                }
            }
            else
            {
                this.splitContainer2.Visible = false;
                this.btnAdd.Visible = false;
                this.btnDetail.Visible = false;
                this.btnTraining.Visible = false;
                this.bottomLabel.Visible = false;
            }
        }

        public void RefreshView(List<Users> list_users, List<EventCalendar> list_event_calendar, List<TrainingCalendar> list_training_calendar, NoteCalendar note_calendar, int curr_month)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                this.list_users = list_users;
                this.event_list = list_event_calendar;
                this.training_list = list_training_calendar;
                this.note_calendar = note_calendar;
                this.target_month = curr_month;
                delegateRefreshView del = new delegateRefreshView(this.RefreshView);
                this.Invoke(del);
            };
            worker.RunWorkerCompleted += delegate
            {
                // Do nothing.
            };
            worker.RunWorkerAsync();
        }

        public void RefreshView() // refresh UI View
        {
            this.SetVisulControl();

            if (this.date.GetDayIntOfWeek() == 1)
            {
                this.Holiday();
            }
            else
            {
                if (this.date.GetDayIntOfWeek() == 7 && this.date.AddDays(7).Month != this.date.Month)
                {
                    this.Holiday();
                }
                else
                {
                    this.Ready = false;
                    this.SetEventList(this.event_list);
                    this.SetTrainingList(this.training_list);
                    this.Ready = true;
                    this.SetNoteCalendar();
                }
            }
        }

        public void RefreshData()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event&from_date=" + this.date.ToMysqlDate() + "&to_date=" + this.date.ToMysqlDate());
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.event_list = sr.event_calendar;
                this.training_list = sr.training_calendar;
                this.note_calendar = sr.note_calendar.Find(t => t.date == this.date.ToMysqlDate());
            }
        }

        public void SetEventList(List<EventCalendar> event_list)
        {
            this.event_list = event_list;
            this.FillDatagrid(this.dgv);
        }

        public void SetTrainingList(List<TrainingCalendar> training_list)
        {
            this.training_list = training_list;
        }

        public void SetNoteCalendar()
        {
            if (this.note_calendar != null)
            {
                if (this.note_calendar.type == (int)NOTE_TYPE.HOLIDAY)
                {
                    this.lblWeekend.Text = this.note_calendar.description;
                }
                else if (this.note_calendar.type == (int)NOTE_TYPE.NOTE)
                {
                    this.lblWeekend.Text = this.GetTrainerName() + (this.GetTrainerName().Length > 0 && this.note_calendar.description.Length > 0 ? ", " : "") + this.note_calendar.description;
                }
            }
            else if (this.note_calendar == null)
            {
                this.lblWeekend.Text = this.GetTrainerName();
            }
        }

        public void Holiday()
        {
            this.splitContainer2.Visible = true;
            this.dgv.Visible = false;
            this.lblDay.BackColor = Color.LightGray;
            this.lblMontYear.ForeColor = Color.LightGray;
            this.SetNoteCalendar();
        }

        public void FillDatagrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.Visible = false;
            dgv.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Width = 20;
            dgv.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Width = 40;
            dgv.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Width = 50;
            dgv.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add(col4);

            if (this.event_list != null)
            {
                int cnt = 0;

                List<EventCalendar> supervisor_list = new List<EventCalendar>();
                List<EventCalendar> support_list = new List<EventCalendar>();
                foreach (EventCalendar e in this.event_list)
                {
                    if (this.list_users.Where(u => u.username == e.users_name).Where(u => u.level >= GlobalVar.USER_LEVEL_SUPERVISOR).Count<Users>() > 0)
                    {
                        supervisor_list.Add(e);
                    }
                    else
                    {
                        support_list.Add(e);
                    }
                }

                foreach (EventCalendar e in support_list)
                {
                    int r = dgv.Rows.Add();
                    dgv.Rows[r].Tag = e;

                    dgv.Rows[r].Cells[0].ValueType = typeof(int);
                    dgv.Rows[r].Cells[0].Value = e.id;

                    cnt += (e.status == (int)EVENT_STATUS.CANCELED ? 0 : 1);
                    dgv.Rows[r].Cells[1].ValueType = typeof(int);
                    dgv.Rows[r].Cells[1].Value = (e.status == (int)EVENT_STATUS.CANCELED ? "" : cnt.ToString());
                    dgv.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Rows[r].Cells[1].Style.BackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[1].Style.SelectionBackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[1].Style.ForeColor = (this.date.Month != target_month ? Color.Gray : Color.Black);
                    dgv.Rows[r].Cells[1].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[2].ValueType = typeof(string);
                    dgv.Rows[r].Cells[2].Value = e.realname;
                    dgv.Rows[r].Cells[2].Style.BackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[2].Style.SelectionBackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[2].Style.ForeColor = (this.date.Month != target_month ? Color.Gray : Color.Black);
                    dgv.Rows[r].Cells[2].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[3].ValueType = typeof(string);
                    dgv.Rows[r].Cells[3].Value = this.GetEventAbbreviate(e.event_type, e.event_code);
                    dgv.Rows[r].Cells[3].Style.BackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[3].Style.SelectionBackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[3].Style.ForeColor = (this.date.Month != target_month ? Color.Gray : Color.Black);
                    dgv.Rows[r].Cells[3].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[4].ValueType = typeof(string);
                    dgv.Rows[r].Cells[4].Value = (e.customer.Length > 0 ? e.customer : this.GetTimeString(e));
                    dgv.Rows[r].Cells[4].Style.BackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[4].Style.SelectionBackColor = (e.status == (int)EVENT_STATUS.WAIT_FOR_CONFIRM ? color_light_blue : (e.status == (int)EVENT_STATUS.CANCELED ? color_light_red : Color.White));
                    dgv.Rows[r].Cells[4].Style.ForeColor = (this.date.Month != target_month ? Color.Gray : Color.Black);
                    dgv.Rows[r].Cells[4].Style.SelectionForeColor = Color.Red;
                }
                if (this.note_calendar != null && this.note_calendar.max_leave > -1)
                {
                    if (support_list.Where(e => e.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().Count < this.note_calendar.max_leave)
                    {
                        for (int i = support_list.Where(e => e.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().Count; i < this.note_calendar.max_leave; i++)
                        {
                            int r = dgv.Rows.Add();
                            dgv.Rows[r].Cells[1].ValueType = typeof(int);
                            dgv.Rows[r].Cells[1].Value = i + 1;
                            dgv.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                }

                foreach (EventCalendar e in supervisor_list)
                {
                    int r = dgv.Rows.Add();
                    dgv.Rows[r].Tag = e;

                    dgv.Rows[r].Cells[0].ValueType = typeof(int);
                    dgv.Rows[r].Cells[0].Value = e.id;

                    cnt += (e.status == (int)EVENT_STATUS.CANCELED ? 0 : 1);
                    dgv.Rows[r].Cells[1].ValueType = typeof(int);
                    dgv.Rows[r].Cells[1].Value = "";
                    dgv.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Rows[r].Cells[1].Style.BackColor = Color.Wheat;
                    dgv.Rows[r].Cells[1].Style.SelectionBackColor = Color.Wheat;
                    dgv.Rows[r].Cells[1].Style.ForeColor = Color.Black;
                    dgv.Rows[r].Cells[1].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[2].ValueType = typeof(string);
                    dgv.Rows[r].Cells[2].Value = e.realname;
                    dgv.Rows[r].Cells[2].Style.BackColor = Color.Wheat;
                    dgv.Rows[r].Cells[2].Style.SelectionBackColor = Color.Wheat;
                    dgv.Rows[r].Cells[2].Style.ForeColor = Color.Black;
                    dgv.Rows[r].Cells[2].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[3].ValueType = typeof(string);
                    dgv.Rows[r].Cells[3].Value = this.GetEventAbbreviate(e.event_type, e.event_code);
                    dgv.Rows[r].Cells[3].Style.BackColor = Color.Wheat;
                    dgv.Rows[r].Cells[3].Style.SelectionBackColor = Color.Wheat;
                    dgv.Rows[r].Cells[3].Style.ForeColor = Color.Black;
                    dgv.Rows[r].Cells[3].Style.SelectionForeColor = Color.Red;

                    dgv.Rows[r].Cells[4].ValueType = typeof(string);
                    dgv.Rows[r].Cells[4].Value = (e.customer.Length > 0 ? e.customer : this.GetTimeString(e));
                    dgv.Rows[r].Cells[4].Style.BackColor = Color.Wheat;
                    dgv.Rows[r].Cells[4].Style.SelectionBackColor = Color.Wheat;
                    dgv.Rows[r].Cells[4].Style.ForeColor = Color.Black;
                    dgv.Rows[r].Cells[4].Style.SelectionForeColor = Color.Red;
                }
                dgv.CurrentCell = null;
            }
            //Console.WriteLine(".. datagrid filled.");
        }

        private string GetTrainerName()
        {
            string trainer = "";
            if (this.training_list != null)
            {
                trainer += (this.training_list.Count > 0 ? "อบรม(" : "");

                int trainer_count = 0;
                foreach (TrainingCalendar t in this.training_list)
                {
                    CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_realname&username=" + this.training_list[trainer_count].trainer);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        trainer += (++trainer_count == 1 ? sr.users[0].name : "," + sr.users[0].name);
                    }
                    else
                    {
                        trainer += "";
                    }

                }
                trainer += (training_list.Count > 0 ? ")" : "");
            }
            
            return trainer;
        }

        public string GetEventAbbreviate(string event_type, string event_code)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_by_typcod&typcod=" + event_code + "&tabtyp=" + event_type);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                return sr.istab[0].abbreviate_th;
            }
            else
            {
                return "";
            }
        }

        public string GetTimeString(EventCalendar ev)
        {
            if (ev.customer.Length == 0)
            {
                TimeSpan to_time = new TimeSpan(Convert.ToInt32(ev.to_time.Split(':')[0]), Convert.ToInt32(ev.to_time.Split(':')[1]), 0);
                TimeSpan from_time = new TimeSpan(Convert.ToInt32(ev.from_time.Split(':')[0]), Convert.ToInt32(ev.from_time.Split(':')[1]), 0);
                DateTime event_date = DateTime.Parse(ev.date, cinfo_us, DateTimeStyles.None);

                if (from_time.Hours <= 12 && to_time.Hours >= 13)
                {
                    return ((to_time - from_time - TimeSpan.Parse("01:00:00")).Hours >= 1 ? (to_time - from_time - TimeSpan.Parse("01:00:00")).Hours.ToString() + ((to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes > 1 ? ":" + (to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes.ToString() + " ชม." : " ชม.") : (to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes.ToString() + " นาที") + ((event_date.GetDayIntOfWeek() >= 2 && event_date.GetDayIntOfWeek() <= 6) && (from_time.Equals(TimeSpan.Parse("08:30:00")) && to_time.Equals(TimeSpan.Parse("17:30:00"))) ? "(เต็มวัน)" : "(" + from_time.ToString().Substring(0, 5) + " - " + to_time.ToString().Substring(0, 5) + ")");
                }
                else
                {
                    return ((to_time - from_time).Hours >= 1 ? (to_time - from_time).Hours.ToString() + ((to_time - from_time).Minutes > 1 ? ":" + (to_time - from_time).Minutes.ToString() + " ชม." : " ชม.") : (to_time - from_time).Minutes.ToString() + " นาที") + ((event_date.GetDayIntOfWeek() >= 2 && event_date.GetDayIntOfWeek() <= 6) && (from_time.Equals(TimeSpan.Parse("08:30:00")) && to_time.Equals(TimeSpan.Parse("17:30:00"))) ? "(เต็มวัน)" : "(" + from_time.ToString().Substring(0, 5) + " - " + to_time.ToString().Substring(0, 5) + ")");
                }
            }
            else
            {
                return ev.customer;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            //DateEventWindow wind = new DateEventWindow(this);
            //wind.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //DateEventWindow wind = new DateEventWindow(this, true);
            //wind.ShowDialog();
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            TrainingExpertWindow wind = new TrainingExpertWindow(this);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData();
                this.RefreshView();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.date.ToString("dd-MM-yyyy", cinfo_th) == DateTime.Now.ToString("dd-MM-yyyy", cinfo_th))
            {
                this.lblDay.BackColor = Color.LimeGreen;
                this.lblMontYear.ForeColor = Color.LimeGreen;
            }
            else
            {
                this.lblDay.BackColor = (this.date.Month != target_month ? Color.LightGray : color_light_purple);
                this.lblMontYear.ForeColor = (this.date.Month != target_month ? Color.LightGray : color_light_purple);
            }
        }

        public void FillGrid(List<EventCalendar> cal_event)
        {
            this.dgv.DataSource = cal_event;
        }
    }
}
