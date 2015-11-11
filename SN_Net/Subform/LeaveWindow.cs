using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.IO;

namespace SN_Net.Subform
{
    public partial class LeaveWindow : Form
    {
        private MainForm main_form;
        private Users current_user;
        private DateTime current_date_from;
        private DateTime current_date_to;
        private List<EventCalendar> event_calendar;
        private List<Users> users_list;
        private List<Istab> leave_cause;
        private List<EventCalendar> sorted_list = new List<EventCalendar>();
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            EDIT_ITEM,
            READING,
            PROCESSING
        }

        public LeaveWindow()
        {
            InitializeComponent();
        }

        public LeaveWindow(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
            this.main_form.leave_wind = this;
        }

        public LeaveWindow(MainForm main_form, Users user, DateTime date_from, DateTime date_to)
            : this()
        {
            this.main_form = main_form;
            this.main_form.leave_wind = this;

            this.current_user = user;
            this.current_date_from = date_from;
            this.current_date_to = date_to;
        }

        private void LeaveWindow_Load(object sender, EventArgs e)
        {
            #region Load users_list from server
            CRUDResult get_user = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_user = JsonConvert.DeserializeObject<ServerResult>(get_user.data);

            if (sr_user.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.users_list = sr_user.users;
            }
            #endregion Load users_list from server

            #region Add users_list to cbUsers
            foreach (Users u in this.users_list)
	        {
                ComboboxItem item = new ComboboxItem(u.username + " : " + u.name, u.id, u.username);
                item.Tag = u;
                this.cbUsers.Items.Add(item);
	        }
            #endregion Add users_list to cbUsers

            #region Load leave_cause from server
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.getTabtypString(Istab.TABTYP.ABSENT_CAUSE) + "&sort=typcod");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.leave_cause = sr_leave_cause.istab;
            }
            #endregion Load leave_cause from server

            this.lblSummaryTime.Text = "";
            this.BindingControlEventHandler();

            if (this.current_user != null && this.current_date_from != null && this.current_date_to != null)
            {
                if (this.main_form.G.loged_in_user_level < GlobalVar.USER_GROUP_SUPERVISOR)
                {
                    this.cbUsers.SelectedIndex = this.users_list.FindIndex(t => t.username == this.main_form.G.loged_in_user_name);
                }
                else
                {
                    this.cbUsers.SelectedIndex = this.users_list.FindIndex(t => t.id == this.current_user.id);
                }

                this.dtFrom.Value = this.current_date_from;
                this.dtTo.Value = this.current_date_to;
            }
            else
            {
                if (this.main_form.G.loged_in_user_level < GlobalVar.USER_GROUP_SUPERVISOR)
                {
                    this.cbUsers.SelectedIndex = this.users_list.FindIndex(t => t.username == this.main_form.G.loged_in_user_name);
                }
                this.dtFrom.Value = DateTime.Now;
                this.dtTo.Value = DateTime.Now;
                this.current_date_from = this.dtFrom.Value;
                this.current_date_to = this.dtTo.Value;
            }
        }

        private void LeaveWindow_Shown(object sender, EventArgs e)
        {
            this.FormReading();
        }

        private void BindingControlEventHandler()
        {
            this.cbUsers.SelectedIndexChanged += delegate
            {
                this.current_user = (Users)((ComboboxItem)this.cbUsers.SelectedItem).Tag;
                this.LoadEventAndFill();
            };

            this.dtFrom.ValueChanged += delegate
            {
                this.current_date_from = this.dtFrom.Value;
                if (this.cbUsers.SelectedIndex > -1)
                {
                    this.LoadEventAndFill();
                }
            };
            
            this.dtTo.ValueChanged += delegate
            {
                this.current_date_to = this.dtTo.Value;
                if (this.cbUsers.SelectedIndex > -1)
                {
                    this.LoadEventAndFill();
                }
            };

            this.dgvLeaveSummary.CellMouseClick += delegate(object sender, DataGridViewCellMouseEventArgs e)
            {
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        this.dgvLeaveSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !((bool)this.dgvLeaveSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        this.FillDataGridLeaveList();
                    }
                }
            };

            this.dgvLeaveList.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);
            this.dgvLeaveSummary.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);
            this.dgvLeaveList.CellDoubleClick += delegate
            {
                if (this.dgvLeaveList.CurrentCell != null)
                {
                    if (this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag is EventCalendar)
                    {
                        this.ShowInlineFormLeaveList();
                    }
                }
            };
            this.dgvLeaveList.Resize += delegate
            {
                this.SetPositionFormLeaveList();
            };
        }

        private void DrawSelectedRowBorder(object sender, PaintEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell != null)
            {
                Rectangle rect = ((DataGridView)sender).GetRowDisplayRectangle(((DataGridView)sender).CurrentCell.RowIndex, true);

                using (Pen p = new Pen(Color.Red))
                {
                    e.Graphics.DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                    e.Graphics.DrawLine(p, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                }
            }
        }

        public void CrossingCall(Users user, DateTime date_from, DateTime date_to)
        {
            this.cbUsers.SelectedIndex = this.users_list.FindIndex(t => t.id == user.id);
            this.dtFrom.Value = date_from;
            this.dtTo.Value = date_to;

            this.LoadEventAndFill();
        }

        private void LoadEventAndFill()
        {
            bool get_success = false;
            string err_msg = "";
            this.FormProcessing();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event_with_user&users_name=" + this.current_user.username + "&from_date=" + this.current_date_from.ToMysqlDate() + "&to_date=" + this.current_date_to.ToMysqlDate());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    get_success = true;
                    this.event_calendar = sr.event_calendar;
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
                    this.FillDataGridLeaveSummary();
                    this.FillDataGridLeaveList();
                    this.ClearInlineFormLeaveList();
                    this.FormReading();
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void FillDataGridLeaveSummary()
        {
            this.dgvLeaveSummary.Rows.Clear();
            this.dgvLeaveSummary.Columns.Clear();

            DataGridViewCheckBoxColumn col0 = new DataGridViewCheckBoxColumn();
            col0.HeaderText = "";
            col0.Width = 30;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            col0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveSummary.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "เหตุผล";
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveSummary.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "จำนวนวัน";
            col2.Width = 140;
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveSummary.Columns.Add(col2);

            foreach (Istab l in this.leave_cause)
            {
                int r = this.dgvLeaveSummary.Rows.Add();
                this.dgvLeaveSummary.Rows[r].Tag = l;
                
                string leave_day_string = this.event_calendar.Where<EventCalendar>(t => t.event_code == l.typcod).ToList<EventCalendar>().GetSummaryLeaveDayString();

                this.dgvLeaveSummary.Rows[r].Cells[0].ValueType = typeof(bool);
                this.dgvLeaveSummary.Rows[r].Cells[0].Value = (this.event_calendar.Where(t => t.event_code == l.typcod).ToList<EventCalendar>().Count > 0 ? true : false);
                this.dgvLeaveSummary.Rows[r].Cells[0].Style.BackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);
                this.dgvLeaveSummary.Rows[r].Cells[0].Style.SelectionBackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);

                this.dgvLeaveSummary.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvLeaveSummary.Rows[r].Cells[1].Value = l.typdes_th;
                this.dgvLeaveSummary.Rows[r].Cells[1].Style.BackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);
                this.dgvLeaveSummary.Rows[r].Cells[1].Style.SelectionBackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);

                this.dgvLeaveSummary.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvLeaveSummary.Rows[r].Cells[2].Value = leave_day_string;
                this.dgvLeaveSummary.Rows[r].Cells[2].Style.BackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);
                this.dgvLeaveSummary.Rows[r].Cells[2].Style.SelectionBackColor = (leave_day_string.Length == 0 ? Color.Gainsboro : Color.White);
            }
        }

        private void FillDataGridLeaveList()
        {
            this.dgvLeaveList.Rows.Clear();
            this.dgvLeaveList.Columns.Clear();

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.Visible = false;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvLeaveList.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Width = 40;
            col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            col1.HeaderText = "ลำดับ";
            col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Width = 80;
            col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            col2.HeaderText = "วันที่";
            col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Width = 100;
            col3.SortMode = DataGridViewColumnSortMode.NotSortable;
            col3.HeaderText = "ชื่อพนักงาน";
            col3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.Width = 120;
            col4.SortMode = DataGridViewColumnSortMode.NotSortable;
            col4.HeaderText = "เหตุผล";
            col4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.Width = 60;
            col5.SortMode = DataGridViewColumnSortMode.NotSortable;
            col5.HeaderText = "จาก";
            col5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col5);

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.Width = 60;
            col6.SortMode = DataGridViewColumnSortMode.NotSortable;
            col6.HeaderText = "ถึง";
            col6.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col6);

            DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
            col7.Width = 120;
            col7.SortMode = DataGridViewColumnSortMode.NotSortable;
            col7.HeaderText = "รวมเวลา";
            col7.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col7);

            DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
            col8.Width = 70;
            col8.SortMode = DataGridViewColumnSortMode.NotSortable;
            col8.HeaderText = "Confirmed";
            col8.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col8);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col9.SortMode = DataGridViewColumnSortMode.NotSortable;
            col9.HeaderText = "ชื่อลูกค้า";
            col9.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col9);

            int row_count = 0;
            List<EventCalendar> filter_event_calendar = new List<EventCalendar>();
            foreach (DataGridViewRow row in this.dgvLeaveSummary.Rows)
            {
                if ((bool)row.Cells[0].Value == true)
                {
                    filter_event_calendar = filter_event_calendar.Concat(this.event_calendar.Where<EventCalendar>(t => t.event_code == ((Istab)row.Tag).typcod).ToList<EventCalendar>()).ToList<EventCalendar>();
                }
            }

            sorted_list = filter_event_calendar.OrderBy(t => t.date).ToList<EventCalendar>();

            foreach (EventCalendar ev in sorted_list)
            {
                int r = this.dgvLeaveList.Rows.Add();
                this.dgvLeaveList.Rows[r].Tag = ev;

                this.dgvLeaveList.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvLeaveList.Rows[r].Cells[0].Value = ev.id;

                this.dgvLeaveList.Rows[r].Cells[1].ValueType = typeof(int);
                this.dgvLeaveList.Rows[r].Cells[1].Value = ++row_count;
                this.dgvLeaveList.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvLeaveList.Rows[r].Cells[1].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[1].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[2].pickedDate(ev.date);
                this.dgvLeaveList.Rows[r].Cells[2].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[2].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[3].Value = ev.realname;
                this.dgvLeaveList.Rows[r].Cells[3].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[3].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[4].Value = (this.leave_cause.Find(t => t.typcod == ev.event_code) != null ? this.leave_cause.Find(t => t.typcod == ev.event_code).typdes_th : "");
                this.dgvLeaveList.Rows[r].Cells[4].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[4].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[5].Value = ev.from_time.Substring(0, 5);
                this.dgvLeaveList.Rows[r].Cells[5].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[5].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[6].Value = ev.to_time.Substring(0, 5);
                this.dgvLeaveList.Rows[r].Cells[6].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[6].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[7].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[7].Value = sorted_list.Where(t => t.id == ev.id).ToList<EventCalendar>().GetSummaryLeaveDayString();
                this.dgvLeaveList.Rows[r].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.dgvLeaveList.Rows[r].Cells[7].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[7].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[8].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[8].Value = (ev.status == (int)CustomDateEvent.EVENT_STATUS.CONFIRMED ? "Y" : "N");
                this.dgvLeaveList.Rows[r].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvLeaveList.Rows[r].Cells[8].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[8].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);

                this.dgvLeaveList.Rows[r].Cells[9].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[9].Value = ev.customer;
                this.dgvLeaveList.Rows[r].Cells[9].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
                this.dgvLeaveList.Rows[r].Cells[9].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : Color.White);
            }

            this.lblSummaryTime.Text = (sorted_list.Count > 0 ? "(รวม" + sorted_list.GetSummaryLeaveDayString() + ")" : "");
        }

        private void ShowInlineFormLeaveList()
        {
            this.FormEditItem();
            CustomTimePicker inline_from_time = new CustomTimePicker();
            inline_from_time.Name = "inline_from_time";
            inline_from_time.Read_Only = false;
            this.dgvLeaveList.Parent.Controls.Add(inline_from_time);

            CustomTimePicker inline_to_time = new CustomTimePicker();
            inline_to_time.Name = "inline_to_time";
            inline_to_time.Read_Only = false;
            this.dgvLeaveList.Parent.Controls.Add(inline_to_time);

            this.SetPositionFormLeaveList();
            this.dgvLeaveSummary.Enabled = false;
            this.dgvLeaveList.Enabled = false;
            this.dgvLeaveList.SendToBack();
            inline_from_time.BringToFront();
            inline_to_time.BringToFront();

            if (this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag is EventCalendar)
            {
                string[] from = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).from_time.Split(':');
                inline_from_time.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(from[0]), Convert.ToInt32(from[1]), 0);
                string[] to = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).to_time.Split(':');
                inline_to_time.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(to[0]), Convert.ToInt32(to[1]), 0);
            }
        }

        private void SetPositionFormLeaveList()
        {
            if (this.dgvLeaveList.CurrentCell != null)
            {
                if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
                {
                    Rectangle rect_from = this.dgvLeaveList.GetCellDisplayRectangle(5, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).SetBounds(rect_from.X, rect_from.Y + 1, rect_from.Width, rect_from.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    Rectangle rect_to = this.dgvLeaveList.GetCellDisplayRectangle(6, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).SetBounds(rect_to.X, rect_to.Y + 1, rect_to.Width, rect_to.Height - 3);
                }
            }
        }

        private void ClearInlineFormLeaveList()
        {
            if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_from_time");
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_to_time");
            }
        }

        private void SubmitEditEvent()
        {
            if (this.dgvLeaveList.CurrentCell != null)
            {
                string json_data = "{\"id\":" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).id.ToString() + ",";
                json_data += "\"users_name\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).users_name + "\",";
                json_data += "\"date\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).date + "\",";
                json_data += "\"event_code\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).event_code + "\",";
                json_data += "\"customer\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).customer + "\",";
                json_data += "\"status\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).status + "\",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\",";

                if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
                {
                    json_data += "\"from_time\":\"" + ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Time.ToString("HH:mm", cinfo_th.DateTimeFormat) + "\",";
                }
                else
                {
                    json_data += "\"from_time\":\"\",";
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    json_data += "\"to_time\":\"" + ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Time.ToString("HH:mm", cinfo_th.DateTimeFormat) + "\"}";
                }
                else
                {
                    json_data += "\"to_time\":\"\"}";
                }

                bool post_success = false;
                string err_msg = "";
                this.FormProcessing();

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "eventcalendar/update", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
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
                        this.LoadEventAndFill();
                        this.dgvLeaveList.Enabled = true;
                        this.dgvLeaveSummary.Enabled = true;
                    }
                    else
                    {
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        this.FormEditItem();
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = false;
            page_setup.PageSettings.Margins = new Margins(0, 0, 0, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {

                };

                print_doc.PrintPage += delegate(object obj_sender, PrintPageEventArgs pe)
                {
                    bool is_new_page = true;
                    page_no++;

                    using (Font font = new Font("tahoma", 10f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            using (Pen p = new Pen(Color.LightGray))
                            {
                                int y_pos = 5;
                                #region declare column width
                                int col0_width = 40; // seq
                                int col1_width = 80; // date
                                int col2_width = 60; // from_time
                                int col3_width = 60; // to_time
                                int col4_width = 130; // duration
                                int col5_width = 130; // event_code
                                int col6_width = 30; // status
                                int col7_width = 210; // customer
                                int col8_width = 80; // rec_by
                                #endregion declare column width

                                StringFormat str_format_center = new StringFormat();
                                str_format_center.Alignment = StringAlignment.Center;
                                str_format_center.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_right = new StringFormat();
                                str_format_right.Alignment = StringAlignment.Far;
                                str_format_right.LineAlignment = StringAlignment.Center;

                                StringFormat str_format_left = new StringFormat();
                                str_format_left.Alignment = StringAlignment.Near;
                                str_format_left.LineAlignment = StringAlignment.Center;

                                y_pos += 5;
                                #region Report Header
                                pe.Graphics.DrawString("สรุปวันลา,ออกพบลูกค้า", new Font("tahoma", 11f, FontStyle.Bold), brush, new Rectangle(5, y_pos, 300, 20));
                                using (Font fontsmall = new Font("tahoma", 7f, FontStyle.Regular))
                                {
                                    pe.Graphics.DrawString("(" + DateTime.Now.ToString() + ")", fontsmall, brush, new Rectangle(5 + 600, y_pos, 180, 13), str_format_right);
                                }
                                y_pos += 25;
                                pe.Graphics.DrawString("รหัสพนักงาน : " + this.current_user.username, font, brush, new Rectangle(5, y_pos, 500, 25));
                                using (Font fontsmall = new Font("tahoma", 7f, FontStyle.Regular))
                                {
                                    pe.Graphics.DrawString("หน้า : " + page_no.ToString(), fontsmall, brush, new Rectangle(5 + 600, y_pos, 180, 13), str_format_right); // draw page no.
                                }
                                y_pos += 25;
                                pe.Graphics.DrawString("วันที่ : " + this.current_date_from.ToString("dd/MM/yy", cinfo_th) + " - " + this.current_date_to.ToString("dd/MM/yy", cinfo_th), font, brush, new Rectangle(5, y_pos, 500, 25));
                                y_pos += 25;
                                #endregion Report Header

                                #region Summay leave
                                if (page_no == 1)
                                {
                                    foreach (DataGridViewRow row in this.dgvLeaveSummary.Rows)
                                    {
                                        if ((bool)row.Cells[0].Value == true)
                                        {
                                            Istab leave_cause = (Istab)row.Tag;

                                            pe.Graphics.DrawString(leave_cause.typdes_th, font, brush, new Rectangle(20, y_pos, 130, 25));
                                            string leave_day = this.event_calendar.Where(t => t.event_code == leave_cause.typcod).ToList<EventCalendar>().GetSummaryLeaveDayString();
                                            pe.Graphics.DrawString(" : " + leave_day, font, brush, new Rectangle(150, y_pos, 200, 25));
                                            y_pos += 20;
                                        }
                                    }
                                }
                                y_pos += 10;
                                #endregion Summay leave

                                for (int i = row_num; i < this.sorted_list.Count; i++)
                                {
                                    int x_pos = 10;


                                    if (y_pos > pe.MarginBounds.Bottom)
                                    {
                                        pe.HasMorePages = true;
                                        return;
                                    }
                                    else
                                    {
                                        pe.HasMorePages = false;
                                    }

                                    #region draw column header
                                    if (is_new_page) // column header
                                    {
                                        using (Pen pen_darkgray = new Pen(Color.DarkGray))
                                        {
                                            pe.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), new RectangleF(x_pos, y_pos, 790, 25));

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 790, y_pos); // horizontal line upper

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect0 = new Rectangle(x_pos, y_pos, col0_width, 25);
                                            pe.Graphics.DrawString("ลำดับ", font, brush, header_rect0, str_format_center);
                                            x_pos += col0_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect1 = new Rectangle(x_pos, y_pos, col1_width, 25);
                                            pe.Graphics.DrawString("วันที่", font, brush, header_rect1, str_format_center);
                                            x_pos += col1_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect2 = new Rectangle(x_pos, y_pos, col2_width, 25);
                                            pe.Graphics.DrawString("จาก", font, brush, header_rect2, str_format_center);
                                            x_pos += col2_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect3 = new Rectangle(x_pos, y_pos, col3_width, 25);
                                            pe.Graphics.DrawString("ถึง", font, brush, header_rect3, str_format_center);
                                            x_pos += col3_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect4 = new Rectangle(x_pos, y_pos, col4_width, 25);
                                            pe.Graphics.DrawString("รวมเวลา", font, brush, header_rect4, str_format_center);
                                            x_pos += col4_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect5 = new Rectangle(x_pos, y_pos, col5_width, 25);
                                            pe.Graphics.DrawString("เหตุผล", font, brush, header_rect5, str_format_center);
                                            x_pos += col5_width;

                                            //pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            //Rectangle header_rect6 = new Rectangle(x_pos, y_pos, col6_width, 25);
                                            //pe.Graphics.DrawString("สถานะ", font, brush, header_rect6, str_format_center);
                                            //x_pos += col6_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect7 = new Rectangle(x_pos, y_pos, col7_width, 25);
                                            pe.Graphics.DrawString("ชื่อลูกค้า", font, brush, header_rect7, str_format_center);
                                            x_pos += col7_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect8 = new Rectangle(x_pos, y_pos, col8_width, 25);
                                            pe.Graphics.DrawString("บันทึกโดย", font, brush, header_rect8, str_format_center);
                                            x_pos += col8_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator

                                            x_pos = 10; // set x_pos again after use in header
                                            y_pos += 25;
                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + 790, y_pos); // horizontal line below
                                        }

                                        y_pos += 7;
                                        is_new_page = false;
                                    }
                                    #endregion draw column header

                                    #region draw row data
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    Rectangle rect0 = new Rectangle(x_pos, y_pos, col0_width, 18);
                                    pe.Graphics.DrawString((row_num + 1).ToString(), font, brush, rect0, str_format_right);
                                    x_pos += col0_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    Rectangle rect1 = new Rectangle(x_pos, y_pos, col1_width, 18);
                                    pe.Graphics.DrawString(DateTime.Parse(this.sorted_list[i].date).ToString("dd/MM/yy", cinfo_th), font, brush, rect1, str_format_center);
                                    x_pos += col1_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    Rectangle rect2 = new Rectangle(x_pos, y_pos, col2_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].from_time.Substring(0, 5), font, brush, rect2, str_format_center);
                                    x_pos += col2_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect3 = new Rectangle(x_pos, y_pos, col3_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].to_time.Substring(0, 5), font, brush, rect3, str_format_center);
                                    x_pos += col3_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect4 = new Rectangle(x_pos, y_pos, col4_width, 18);
                                    string time_duration = this.sorted_list.Where(t => t.id == this.sorted_list[i].id).ToList<EventCalendar>().GetSummaryLeaveDayString();
                                    pe.Graphics.DrawString(time_duration, font, brush, rect4, str_format_center);
                                    x_pos += col4_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect5 = new Rectangle(x_pos, y_pos, col5_width, 18);
                                    string leave_cause = (this.leave_cause.Find(t => t.typcod == this.sorted_list[i].event_code) != null ? this.leave_cause.Find(t => t.typcod == this.sorted_list[i].event_code).typdes_th : "");
                                    pe.Graphics.DrawString(leave_cause, font, brush, rect5, str_format_left);
                                    x_pos += col5_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    //Rectangle rect6 = new Rectangle(x_pos, y_pos, col6_width, 18);
                                    //string status = (this.sorted_list[i].status == (int)CustomDateEvent.EVENT_STATUS.CONFIRMED ? "Y" : "N");
                                    //pe.Graphics.DrawString(status, font, brush, rect6, str_format_center);
                                    //x_pos += col6_width;
                                    //pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect7 = new Rectangle(x_pos, y_pos, col7_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].customer, font, brush, rect7, str_format_left);
                                    x_pos += col7_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect8 = new Rectangle(x_pos, y_pos, col8_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].rec_by, font, brush, rect8, str_format_left);
                                    x_pos += col8_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 20, x_pos + 790, y_pos + 20);
                                    #endregion draw row data

                                    row_num++;
                                    y_pos += 25;
                                }

                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString(this.toolStripInfo.Text, font, brush, new Rectangle(10, y_pos, 400, 15));
                            }
                        }
                    }
                };

                if (wind.output == PrintOutputSelection.OUTPUT.PRINTER)
                {
                    PrintDialog print_dialog = new PrintDialog();
                    print_dialog.Document = print_doc;
                    print_dialog.AllowSelection = false;
                    print_dialog.AllowSomePages = true;
                    print_dialog.AllowPrintToFile = false;
                    print_dialog.AllowCurrentPage = false;
                    print_dialog.UseEXDialog = true;
                    if (print_dialog.ShowDialog() == DialogResult.OK)
                    {
                        print_doc.Print();
                    }
                }

                if (wind.output == PrintOutputSelection.OUTPUT.SCREEN)
                {
                    PrintPreviewDialog preview_dialog = new PrintPreviewDialog();
                    preview_dialog.SetBounds(this.ClientRectangle.X + 5, this.ClientRectangle.Y + 5, this.ClientRectangle.Width - 10, this.ClientRectangle.Height - 10);
                    preview_dialog.Document = print_doc;
                    preview_dialog.MdiParent = this.main_form;
                    preview_dialog.Show();
                }

                if (wind.output == PrintOutputSelection.OUTPUT.FILE)
                {

                }
            }
            else
            {
                print_doc = null;
                page_setup = null;
            }
        }

        private void toolStripExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Comma separated value | *.csv";
            dlg.DefaultExt = "csv";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string destination_filename = dlg.FileName;

                DataTable dt = this.sorted_list.ToDataTable<EventCalendar>();

                StringBuilder sb = new StringBuilder();

                // Create column header as datatable header
                //string[] columnNames = dt.Columns.Cast<DataColumn>().
                //                                  Select(column => column.ColumnName).
                //                                  ToArray();
                //sb.AppendLine(string.Join(",", columnNames));

                // Create custom column header as we need
                sb.AppendLine("ลำดับ,วันที่,จาก,ถึง,รวมเวลา,เหตุผล,ชื่อลูกค้า,บันทึกโดย");

                int cnt = 0;
                foreach (DataRow row in dt.Rows)
                {
                    cnt++;
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();

                    // Append some column data as we needf
                    sb.AppendLine(cnt.ToString() + "," +
                                    fields[3] + "," +
                                    fields[4] + "," +
                                    fields[5] + "," +
                                    this.event_calendar.Where(t => t.id == Convert.ToInt32(fields[0])).ToList<EventCalendar>().GetSummaryLeaveDayString() + "," +
                                    this.leave_cause.Find(t => t.typcod ==  fields[6]).typdes_th + "," +
                                    fields[7] + "," +
                                    fields[9]);
                }
                this.SaveExportedFile(destination_filename, sb.ToString());
            }
        }

        private void SaveExportedFile(string destination_filename, string content)
        {
            try
            {
                File.WriteAllText(destination_filename, content, Encoding.Default);
            }
            catch (IOException ex)
            {
                if (MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.RETRY_CANCEL, MessageAlertIcons.ERROR) == DialogResult.Retry)
                {
                    this.SaveExportedFile(destination_filename, content);
                }
                else
                {
                    return;
                }
            }
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;

            this.toolStripExport.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.cbUsers.Enabled = false;
            this.dtFrom.Enabled = false;
            this.dtTo.Enabled = false;

            if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Read_Only = true;
            }
        }

        private void FormReading()
        {
            this.form_mode = FORM_MODE.READING;

            this.toolStripExport.Enabled = true;
            this.toolStripPrint.Enabled = true;
            this.cbUsers.Enabled = (this.main_form.G.loged_in_user_level < GlobalVar.USER_GROUP_SUPERVISOR ? false : true);
            this.dtFrom.Enabled = true;
            this.dtTo.Enabled = true;
        }

        private void FormEditItem()
        {
            this.form_mode = FORM_MODE.EDIT_ITEM;

            this.toolStripExport.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.cbUsers.Enabled = false;
            this.dtFrom.Enabled = false;
            this.dtTo.Enabled = false;

            if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Read_Only = false;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.main_form.leave_wind = null;
            base.OnClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    if (((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).dateTimePicker1.Focused)
                    {
                        this.SubmitEditEvent();
                        return true;
                    }
                }
                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.F9)
            {
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    this.SubmitEditEvent();
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    this.ClearInlineFormLeaveList();
                    this.dgvLeaveList.Enabled = true;
                    this.dgvLeaveSummary.Enabled = true;
                    this.FormReading();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
