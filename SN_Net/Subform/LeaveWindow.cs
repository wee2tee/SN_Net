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
        private Users current_user_from;
        private Users current_user_to;
        private DateTime current_date_from;
        private DateTime current_date_to;
        private List<Istab> leave_cause;
        private List<EventCalendar> event_calendar = new List<EventCalendar>();
        private List<EventCalendar> sorted_list = new List<EventCalendar>();
        private List<Users> users_list = new List<Users>();
        private CultureInfo cinfo_th = new CultureInfo("th-TH");
        private CultureInfo cinfo_us = new CultureInfo("en-US");
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            READ_ITEM,
            EDIT_ITEM,
            PROCESSING
        }

        //public LeaveWindow()
        //{
        //    InitializeComponent();
        //}

        //public LeaveWindow(MainForm main_form)
        //    : this()
        //{
        //    this.main_form = main_form;
        //    this.main_form.leave_wind = this;
        //}

        public LeaveWindow(MainForm main_form, Users user_from, Users user_to, DateTime date_from, DateTime date_to)
        {
            InitializeComponent();

            this.main_form = main_form;
            this.main_form.leave_wind = this;

            this.current_user_from = user_from;
            this.current_user_to = user_to;
            this.current_date_from = date_from;
            this.current_date_to = date_to;
        }

        private void LeaveWindow_Load(object sender, EventArgs e)
        {
            this.lblPeriodAbsent.Text = "";
            this.lblPeriodServ.Text = "";
            this.lblTotalAbsent.Text = "";
            this.lblTotalServ.Text = "";

            this.LoadDependenciesData();
            this.BindingControlEventHandler();
        }

        private void LeaveWindow_Shown(object sender, EventArgs e)
        {
            this.toolStripPrintSummary.Visible = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : true);
            this.toolStripExportSummary.Visible = (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR ? false : true);

            this.PrepareSummaryDgv();
            this.LoadEventAndFill();
        }

        private void LoadDependenciesData()
        {
            #region Load leave_cause from server
            CRUDResult get_leave_cause = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_leave_cause");
            ServerResult sr_leave_cause = JsonConvert.DeserializeObject<ServerResult>(get_leave_cause.data);

            if (sr_leave_cause.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.leave_cause = sr_leave_cause.istab;
            }
            #endregion Load leave_cause from server
        }

        private void BindingControlEventHandler()
        {
            this.dgvAbsentSummary.CellMouseClick += new DataGridViewCellMouseEventHandler(this.ToggleCheckboxCell);
            this.dgvServiceSummary.CellMouseClick += new DataGridViewCellMouseEventHandler(this.ToggleCheckboxCell);

            this.dgvLeaveList.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);
            this.dgvAbsentSummary.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);
            this.dgvServiceSummary.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);
            this.dgvLeaveGroup.Paint += new PaintEventHandler(this.DrawSelectedRowBorder);

            this.dgvLeaveList.CellDoubleClick += delegate
            {
                if (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR)
                    return;

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
            this.dgvLeaveList.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (this.main_form.G.loged_in_user_level < GlobalVar.USER_LEVEL_SUPERVISOR)
                        return;

                    int row_index = this.dgvLeaveList.HitTest(e.X, e.Y).RowIndex;

                    if (row_index > -1)
                    {
                        this.dgvLeaveList.Rows[row_index].Cells[1].Selected = true;

                        ContextMenu c = new ContextMenu();
                        MenuItem m_edit = new MenuItem("แก้ไข <Alt+E>");
                        m_edit.Click += delegate
                        {
                            this.ShowInlineFormLeaveList();
                        };
                        c.MenuItems.Add(m_edit);

                        c.Show(this.dgvLeaveList, new Point(e.X, e.Y));
                    }
                }
            };

            this.btnAbsentSelAll.Click += delegate
            {
                foreach (DataGridViewRow row in this.dgvAbsentSummary.Rows)
                {
                    row.Cells[0].Value = true;
                }
                this.FillDataGridLeaveList();
                this.FilllDataGridLeaveGroup();
            };
            this.btnAbsentSelNone.Click += delegate
            {
                foreach (DataGridViewRow row in this.dgvAbsentSummary.Rows)
                {
                    row.Cells[0].Value = false;
                }
                this.FillDataGridLeaveList();
                this.FilllDataGridLeaveGroup();
            };
            this.btnServiceSelAll.Click += delegate
            {
                foreach (DataGridViewRow row in this.dgvServiceSummary.Rows)
                {
                    row.Cells[0].Value = true;
                }
                this.FillDataGridLeaveList();
                this.FilllDataGridLeaveGroup();
            };
            this.btnServiceSelNone.Click += delegate
            {
                foreach (DataGridViewRow row in this.dgvServiceSummary.Rows)
                {
                    row.Cells[0].Value = false;
                }
                this.FillDataGridLeaveList();
                this.FilllDataGridLeaveGroup();
            };

            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    e.Cancel = true;
                    return;
                }
            };
        }

        private void ToggleCheckboxCell(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 0)
                {
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !((bool)((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    this.FillDataGridLeaveList();
                    this.FilllDataGridLeaveGroup();
                }
            }
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

        public void CrossingCall(Users user_from, Users user_to, DateTime date_from, DateTime date_to)
        {
            this.current_user_from = user_from;
            this.current_user_to = user_to;
            this.current_date_from = date_from;
            this.current_date_to = date_to;

            this.LoadEventAndFill();
        }

        private void PrepareSummaryDgv()
        {
            this.dgvAbsentSummary.Rows.Clear();
            this.dgvAbsentSummary.Columns.Clear();

            DataGridViewCheckBoxColumn abs_col0 = new DataGridViewCheckBoxColumn();
            abs_col0.HeaderText = "";
            abs_col0.Width = 30;
            abs_col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            abs_col0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvAbsentSummary.Columns.Add(abs_col0);

            DataGridViewTextBoxColumn abs_col1 = new DataGridViewTextBoxColumn();
            abs_col1.HeaderText = "เหตุผล";
            abs_col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            abs_col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            abs_col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvAbsentSummary.Columns.Add(abs_col1);

            DataGridViewTextBoxColumn abs_col2 = new DataGridViewTextBoxColumn();
            abs_col2.HeaderText = "จำนวนวัน";
            abs_col2.Width = 150;
            abs_col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            abs_col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvAbsentSummary.Columns.Add(abs_col2);


            foreach (Istab l in this.leave_cause.Where(t => t.tabtyp == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<Istab>())
            {
                int r = this.dgvAbsentSummary.Rows.Add();
                this.dgvAbsentSummary.Rows[r].Tag = l;

                this.dgvAbsentSummary.Rows[r].Cells[0].ValueType = typeof(bool);
                this.dgvAbsentSummary.Rows[r].Cells[0].Value = true;

                this.dgvAbsentSummary.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvAbsentSummary.Rows[r].Cells[1].Value = l.typdes_th;

                this.dgvAbsentSummary.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvAbsentSummary.Rows[r].Cells[2].Value = "";
            }

            this.dgvServiceSummary.Rows.Clear();
            this.dgvServiceSummary.Columns.Clear();

            DataGridViewCheckBoxColumn srv_col0 = new DataGridViewCheckBoxColumn();
            srv_col0.HeaderText = "";
            srv_col0.Width = 30;
            srv_col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            srv_col0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvServiceSummary.Columns.Add(srv_col0);

            DataGridViewTextBoxColumn srv_col1 = new DataGridViewTextBoxColumn();
            srv_col1.HeaderText = "เหตุผล";
            srv_col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            srv_col1.SortMode = DataGridViewColumnSortMode.NotSortable;
            srv_col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvServiceSummary.Columns.Add(srv_col1);

            DataGridViewTextBoxColumn srv_col2 = new DataGridViewTextBoxColumn();
            srv_col2.HeaderText = "จำนวนวัน";
            srv_col2.Width = 150;
            srv_col2.SortMode = DataGridViewColumnSortMode.NotSortable;
            srv_col2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvServiceSummary.Columns.Add(srv_col2);

            foreach (Istab l in this.leave_cause.Where(t => t.tabtyp == EventCalendar.EVENT_TYPE_SERVICE_CASE).ToList<Istab>())
            {
                int r = this.dgvServiceSummary.Rows.Add();
                this.dgvServiceSummary.Rows[r].Tag = l;

                this.dgvServiceSummary.Rows[r].Cells[0].ValueType = typeof(bool);
                this.dgvServiceSummary.Rows[r].Cells[0].Value = true;

                this.dgvServiceSummary.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvServiceSummary.Rows[r].Cells[1].Value = l.typdes_th;

                this.dgvServiceSummary.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvServiceSummary.Rows[r].Cells[2].Value = "";
            }
        }

        private void LoadEventAndFill(EventCalendar event_calendar = null)
        {
            this.lblUserFrom.Text = this.current_user_from.username;
            this.lblUserTo.Text = this.current_user_to.username;
            this.lblDateFrom.Text = this.current_date_from.ToString("dd/MM/yy", cinfo_th);
            this.lblDateTo.Text = this.current_date_to.ToString("dd/MM/yy", cinfo_th);

            bool get_success = false;
            string err_msg = "";
            this.FormProcessing();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_event_with_user&username_from=" + this.current_user_from.username + "&username_to=" + this.current_user_to.username + "&from_date=" + this.current_date_from.ToMysqlDate() + "&to_date=" + this.current_date_to.ToMysqlDate());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    get_success = true;
                    this.event_calendar = sr.event_calendar;
                    this.users_list = sr.users;
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
                    this.FillDgvAbsentSummary();
                    this.FillDgvServiceSummary();
                    this.FillDataGridLeaveList(event_calendar);
                    this.FilllDataGridLeaveGroup();
                    //this.ClearInlineFormLeaveList();
                    this.FormReadItem();
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void FillDgvAbsentSummary()
        {
            foreach (Istab l in this.leave_cause.Where(t => t.tabtyp == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<Istab>())
            {
                if (this.event_calendar.Where(t => t.event_type == l.tabtyp && t.event_code == l.typcod).ToList<EventCalendar>().Count > 0)
                {
                    string leave_day_string = this.event_calendar.Where<EventCalendar>(t => t.event_code == l.typcod && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryLeaveDayString();

                    this.dgvAbsentSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells[2].Value = (this.current_user_from.username == this.current_user_to.username ? leave_day_string : "-");
                    foreach (DataGridViewCell cell in this.dgvAbsentSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.SelectionBackColor = Color.White;
                    }

                }
                else
                {
                    this.dgvAbsentSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells[2].Value = "";
                    foreach (DataGridViewCell cell in this.dgvAbsentSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells)
                    {
                        cell.Style.BackColor = Color.Gainsboro;
                        cell.Style.SelectionBackColor = Color.Gainsboro;
                    }
                }
            }
        }

        private void FillDgvServiceSummary()
        {
            foreach (Istab l in this.leave_cause.Where(t => t.tabtyp == EventCalendar.EVENT_TYPE_SERVICE_CASE).ToList<Istab>())
            {
                if (this.event_calendar.Where(t => t.event_type == l.tabtyp && t.event_code == l.typcod).ToList<EventCalendar>().Count > 0)
                {
                    string leave_day_string = this.event_calendar.Where<EventCalendar>(t => t.event_code == l.typcod && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryLeaveDayString();

                    this.dgvServiceSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells[2].Value = (this.current_user_from.username == this.current_user_to.username ? leave_day_string : "-");
                    foreach (DataGridViewCell cell in this.dgvServiceSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.SelectionBackColor = Color.White;
                    }
                }
                else
                {
                    this.dgvServiceSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells[2].Value = "";
                    foreach (DataGridViewCell cell in this.dgvServiceSummary.Rows.Cast<DataGridViewRow>().Where(r => ((Istab)r.Tag).typcod == l.typcod).First<DataGridViewRow>().Cells)
                    {
                        cell.Style.BackColor = Color.Gainsboro;
                        cell.Style.SelectionBackColor = Color.Gainsboro;
                    }
                }
            }
        }

        private void FillDataGridLeaveList(EventCalendar event_calendar = null)
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
            col8.Width = 90;
            col8.SortMode = DataGridViewColumnSortMode.NotSortable;
            col8.HeaderText = "สภานะ";
            col8.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col8);

            DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
            col9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col9.SortMode = DataGridViewColumnSortMode.NotSortable;
            col9.HeaderText = "หมายเหตุ/ชื่อลูกค้า";
            col9.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col9);

            DataGridViewTextBoxColumn col10 = new DataGridViewTextBoxColumn();
            col10.Width = 120;
            col10.SortMode = DataGridViewColumnSortMode.NotSortable;
            col10.HeaderText = "ใบรับรองแพทย์";
            col10.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col10);

            DataGridViewTextBoxColumn col11 = new DataGridViewTextBoxColumn();
            col11.Width = 100;
            col11.SortMode = DataGridViewColumnSortMode.NotSortable;
            col11.HeaderText = "หักค่าคอมฯ";
            col11.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvLeaveList.Columns.Add(col11);

            int row_count = 0;
            List<EventCalendar> filter_event_calendar = new List<EventCalendar>();
            foreach (DataGridViewRow row in this.dgvAbsentSummary.Rows)
            {
                if ((bool)row.Cells[0].Value == true)
                {
                    //List<EventCalendar> tmp1 = this.event_calendar.Where<EventCalendar>(t => t.event_type == ((Istab)row.Tag).tabtyp && t.event_code == ((Istab)row.Tag).typcod).ToList<EventCalendar>().ConvertAll(t => t).ToList<EventCalendar>();
                    //filter_event_calendar = filter_event_calendar.Concat(tmp1).ToList<EventCalendar>();
                    filter_event_calendar = filter_event_calendar.Concat(this.event_calendar.Where<EventCalendar>(t => t.event_type == ((Istab)row.Tag).tabtyp && t.event_code == ((Istab)row.Tag).typcod).ToList<EventCalendar>()).ToList<EventCalendar>();
                }
            }
            foreach (DataGridViewRow row in this.dgvServiceSummary.Rows)
            {
                if ((bool)row.Cells[0].Value == true)
                {
                    //List<EventCalendar> tmp2 = this.event_calendar.Where<EventCalendar>(t => t.event_type == ((Istab)row.Tag).tabtyp && t.event_code == ((Istab)row.Tag).typcod).ToList<EventCalendar>().ConvertAll(t => t).ToList<EventCalendar>();
                    //filter_event_calendar = filter_event_calendar.Concat(tmp2).ToList<EventCalendar>();
                    filter_event_calendar = filter_event_calendar.Concat(this.event_calendar.Where<EventCalendar>(t => t.event_type == ((Istab)row.Tag).tabtyp && t.event_code == ((Istab)row.Tag).typcod).ToList<EventCalendar>()).ToList<EventCalendar>();
                }
            }

            this.sorted_list = filter_event_calendar.OrderBy(t => t.users_name + t.date).ToList<EventCalendar>();
            //foreach (EventCalendar ev in this.event_calendar)
            //{
            //    int r = this.dgvLeaveList.Rows.Add();
            //    this.dgvLeaveList.Rows[r].Tag = ev;

            //    this.dgvLeaveList.Rows[r].Cells[0].ValueType = typeof(int);
            //    this.dgvLeaveList.Rows[r].Cells[0].Value = ev.id;
            //    this.dgvLeaveList.Rows[r].Cells[1].ValueType = typeof(string);
            //    this.dgvLeaveList.Rows[r].Cells[1].Value = "count";
            //    this.dgvLeaveList.Rows[r].Cells[2].ValueType = typeof(string);
            //    this.dgvLeaveList.Rows[r].Cells[2].Value = ev.date;
            //    this.dgvLeaveList.Rows[r].Cells[3].ValueType = typeof(string);
            //    this.dgvLeaveList.Rows[r].Cells[3].Value = ev.realname;
            //}
            foreach (EventCalendar ev in this.sorted_list)
            {
                int r = this.dgvLeaveList.Rows.Add();
                this.dgvLeaveList.Rows[r].Tag = ev;

                this.dgvLeaveList.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvLeaveList.Rows[r].Cells[0].Value = ev.id;

                this.dgvLeaveList.Rows[r].Cells[1].ValueType = typeof(int);
                this.dgvLeaveList.Rows[r].Cells[1].Value = ++row_count;
                this.dgvLeaveList.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvLeaveList.Rows[r].Cells[1].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[1].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[2].pickedDate(ev.date);
                this.dgvLeaveList.Rows[r].Cells[2].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[2].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[3].Value = ev.realname;
                this.dgvLeaveList.Rows[r].Cells[3].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[3].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[4].Value = (this.leave_cause.Find(t => t.typcod == ev.event_code) != null ? this.leave_cause.Find(t => t.typcod == ev.event_code).typdes_th : "");
                this.dgvLeaveList.Rows[r].Cells[4].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[4].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[5].Value = ev.from_time.Substring(0, 5);
                this.dgvLeaveList.Rows[r].Cells[5].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[5].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[6].Value = ev.to_time.Substring(0, 5);
                this.dgvLeaveList.Rows[r].Cells[6].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[6].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[7].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[7].Value = this.sorted_list.Where(t => t.id == ev.id).ToList<EventCalendar>().GetSummaryLeaveDayString();
                this.dgvLeaveList.Rows[r].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.dgvLeaveList.Rows[r].Cells[7].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[7].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[8].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[8].Value = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? "WAIT" : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? "CANCELED" : "CONFIRMED"));
                this.dgvLeaveList.Rows[r].Cells[8].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[8].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[9].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[9].Value = ev.customer;
                this.dgvLeaveList.Rows[r].Cells[9].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[9].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));

                this.dgvLeaveList.Rows[r].Cells[10].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[10].Value = (ev.med_cert == "Y" ? "มีใบรับรองแพทย์" : (ev.med_cert == "N" ? "ไม่มีใบรับรองแพทย์" : ""));
                this.dgvLeaveList.Rows[r].Cells[10].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[10].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[10].Style.ForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);
                this.dgvLeaveList.Rows[r].Cells[10].Style.SelectionForeColor = (ev.med_cert == "N" ? Color.Red : Color.Black);

                this.dgvLeaveList.Rows[r].Cells[11].ValueType = typeof(string);
                this.dgvLeaveList.Rows[r].Cells[11].Value = (ev.fine > 0 ? ev.fine.ToString() : "");
                this.dgvLeaveList.Rows[r].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvLeaveList.Rows[r].Cells[11].Style.BackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
                this.dgvLeaveList.Rows[r].Cells[11].Style.SelectionBackColor = (ev.status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? CustomDateEvent.color_light_blue : (ev.status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? CustomDateEvent.color_light_red : Color.White));
            }

            this.groupTotal.Text = " สะสมจากต้นปี (" + DateTime.Parse(this.current_date_from.Year.ToString() + "-01-01", cinfo_us).ToString("dd/MM/yy", cinfo_th.DateTimeFormat) + " - " + DateTime.Parse(this.current_date_from.Year.ToString() + "-12-31", cinfo_us).ToString("dd/MM/yy", cinfo_th.DateTimeFormat) + ") ";
            this.groupPeriod.Text = "  สรุปตามช่วงวันที่ ที่กำหนด (" + this.current_date_from.ToString("dd/MM/yy", cinfo_th) + " - " + this.current_date_to.ToString("dd/MM/yy", cinfo_th) + ") ";

            int max_absent = 0;
            List<EventCalendar> users_year_event = null;
            if (this.current_user_from != null && this.current_user_to != null)
            {
                CRUDResult get_max_absent = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "eventcalendar/get_user_year_leave_data&id=" + this.current_user_from.id.ToString() + "&year=" + this.current_date_from.Year.ToString());
                ServerResult sr_max_absent = JsonConvert.DeserializeObject<ServerResult>(get_max_absent.data);
                if (sr_max_absent.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    max_absent = sr_max_absent.users[0].max_absent;
                    users_year_event = sr_max_absent.event_calendar;
                }
                else
                {
                    users_year_event = new List<EventCalendar>();
                }
                List<EventCalendar> period_absent_day = this.sorted_list.Where(t => t.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>();
                List<EventCalendar> period_service_day = this.sorted_list.Where(t => t.event_type == EventCalendar.EVENT_TYPE_SERVICE_CASE && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>();
                List<EventCalendar> total_absent_day = users_year_event.Where(t => t.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>();
                List<EventCalendar> total_service_day = users_year_event.Where(t => t.event_type == EventCalendar.EVENT_TYPE_SERVICE_CASE && t.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>();


                if (this.current_user_from.username == this.current_user_to.username)
                {
                    this.lblPeriodAbsent.Text = (period_absent_day != null ? period_absent_day.GetSummaryLeaveDayString() : "-");
                    this.lblPeriodServ.Text = (period_service_day != null ? period_service_day.GetSummaryLeaveDayString() : "-");
                    this.lblTotalAbsent.Text = (total_absent_day.Count > 0 ? total_absent_day.GetSummaryLeaveDayString() : "0 วัน") + " / (max. = " + max_absent.ToString() + " วัน/ปี)";
                    this.lblTotalAbsent.ForeColor = (total_absent_day.GetSummaryTimeSpan().TotalSeconds >= (max_absent * 28800) ? Color.Red : Color.Black);
                    this.lblTotalAbsent.Font = (total_absent_day.GetSummaryTimeSpan().TotalSeconds >= (max_absent * 28800) ? new Font("tahoma", 9.75f, FontStyle.Bold) : new Font("tahoma", 9.75f, FontStyle.Regular));
                    this.lblTotalServ.Text = (total_service_day != null ? total_service_day.GetSummaryLeaveDayString() : "-");
                }
                else
                {
                    this.lblPeriodAbsent.Text = "-";
                    this.lblPeriodServ.Text = "-";
                    this.lblTotalAbsent.Text = "-";
                    this.lblTotalAbsent.ForeColor = Color.Black;
                    this.lblTotalAbsent.Font = new Font("tahoma", 9.75f);
                    this.lblTotalServ.Text = "-";
                }
            }
            if (event_calendar != null)
            {
                if (this.dgvLeaveList.Rows.Count > -1)
                {
                    if (this.dgvLeaveList.Rows.Cast<DataGridViewRow>().Where(r => ((EventCalendar)r.Tag).id == event_calendar.id).Count<DataGridViewRow>() > 0)
                    {
                        this.dgvLeaveList.Rows.Cast<DataGridViewRow>().Where(r => ((EventCalendar)r.Tag).id == event_calendar.id).First<DataGridViewRow>().Cells[1].Selected = true;
                    }
                    else
                    {
                        this.dgvLeaveList.Rows[0].Cells[1].Selected = true;
                    }
                }
            }
            this.dgvLeaveList.Focus();
        }

        private void FilllDataGridLeaveGroup()
        {
            this.dgvLeaveGroup.Rows.Clear();
            this.dgvLeaveGroup.Columns.Clear();

            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 40,
                HeaderText = "ลำดับ",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 80,
                HeaderText = "รหัสพนักงาน",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 120,
                HeaderText = "ชื่อ",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            { 
                Width = 180,
                HeaderText = "จำนวนวันลา(จริง)",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 180,
                HeaderText = "จำนวนวันลา(คิดค่าคอมฯ)",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Width = 80,
                HeaderText = "หักค่าคอมฯ",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            this.dgvLeaveGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "หมายเหตุ",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            int count = 0;
            foreach (Users user in this.users_list)
            {
                int r = this.dgvLeaveGroup.Rows.Add();

                this.dgvLeaveGroup.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvLeaveGroup.Rows[r].Cells[0].Value = ++count;
                this.dgvLeaveGroup.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvLeaveGroup.Rows[r].Cells[0].Style.ForeColor = Color.Gray;
                this.dgvLeaveGroup.Rows[r].Cells[0].Style.SelectionForeColor = Color.Gray;

                this.dgvLeaveGroup.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvLeaveGroup.Rows[r].Cells[1].Value = user.username;

                this.dgvLeaveGroup.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvLeaveGroup.Rows[r].Cells[2].Value = user.name;

                this.dgvLeaveGroup.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvLeaveGroup.Rows[r].Cells[3].Value = this.sorted_list.Where(s => s.users_name == user.username && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString() && s.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryLeaveDayString();

                this.dgvLeaveGroup.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvLeaveGroup.Rows[r].Cells[4].Value = this.sorted_list.Where(s => s.users_name == user.username && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString() && s.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryLeaveDayStringForCommission();

                this.dgvLeaveGroup.Rows[r].Cells[5].ValueType = typeof(string);
                int fine = this.sorted_list.Where(s => s.users_name == user.username && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString() && s.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryFine();
                this.dgvLeaveGroup.Rows[r].Cells[5].Value = (fine > 0 ? fine.ToString() : "");
                this.dgvLeaveGroup.Rows[r].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgvLeaveGroup.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvLeaveGroup.Rows[r].Cells[6].Value = this.sorted_list.Where(s => s.users_name == user.username && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString() && s.status != (int)CustomDateEvent.EVENT_STATUS.CANCELED).ToList<EventCalendar>().GetSummaryMedCertRemark();
            }
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

            CustomComboBox inline_status = new CustomComboBox();
            inline_status.Name = "inline_status";
            inline_status.Read_Only = false;
            inline_status.BorderStyle = BorderStyle.None;
            inline_status.AddItem(new ComboboxItem("WAIT", (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM, "WAIT"));
            inline_status.AddItem(new ComboboxItem("CONFIRMED", (int)CustomDateEvent.EVENT_STATUS.CONFIRMED, "CONFIRMED"));
            inline_status.AddItem(new ComboboxItem("CANCELED", (int)CustomDateEvent.EVENT_STATUS.CANCELED, "CANCELED"));
            this.dgvLeaveList.Parent.Controls.Add(inline_status);

            CustomTextBox inline_customer = new CustomTextBox();
            inline_customer.Name = "inline_customer";
            inline_customer.Read_Only = false;
            inline_customer.BorderStyle = BorderStyle.None;
            inline_customer.MaxChar = 40;
            this.dgvLeaveList.Parent.Controls.Add(inline_customer);

            CustomComboBox inline_medcert = new CustomComboBox();
            inline_medcert.Name = "inline_medcert";
            inline_medcert.Read_Only = false;
            inline_medcert.BorderStyle = BorderStyle.None;
            inline_medcert.AddItem(new ComboboxItem("N/A (ไม่ระบุ)", 9, "X"));
            inline_medcert.AddItem(new ComboboxItem("ไม่มีใบรับรองแพทย์", 0, "N"));
            inline_medcert.AddItem(new ComboboxItem("มีใบรับรองแพทย์", 1, "Y"));
            this.dgvLeaveList.Parent.Controls.Add(inline_medcert);

            NumericUpDown inline_fine = new NumericUpDown();
            inline_fine.Name = "inline_fine";
            inline_fine.Font = new Font("tahoma", 9.75f);
            inline_fine.Maximum = 1000;
            inline_fine.Minimum = 0;
            inline_fine.BorderStyle = BorderStyle.None;
            inline_fine.TextAlign = HorizontalAlignment.Right;
            inline_fine.GotFocus += delegate
            {
                inline_fine.Select(0, inline_fine.Text.Length);
            };
            this.dgvLeaveList.Parent.Controls.Add(inline_fine);

            this.SetPositionFormLeaveList();
            this.dgvAbsentSummary.Enabled = false;
            this.dgvLeaveList.Enabled = false;
            this.dgvLeaveList.SendToBack();
            inline_from_time.BringToFront();
            inline_to_time.BringToFront();
            inline_status.BringToFront();
            inline_customer.BringToFront();
            inline_medcert.BringToFront();
            inline_fine.BringToFront();

            if (this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag is EventCalendar)
            {
                string[] from = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).from_time.Split(':');
                inline_from_time.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(from[0]), Convert.ToInt32(from[1]), 0);
                string[] to = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).to_time.Split(':');
                inline_to_time.Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(to[0]), Convert.ToInt32(to[1]), 0);
                inline_status.comboBox1.SelectedItem = inline_status.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.int_value == ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).status).First<ComboboxItem>();
                inline_customer.Texts = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).customer;
                inline_medcert.comboBox1.SelectedItem = inline_medcert.comboBox1.Items.Cast<ComboboxItem>().Where(i => i.string_value == ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).med_cert).First<ComboboxItem>();
                inline_fine.Value = ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).fine;
            }
        }

        private void SetPositionFormLeaveList()
        {
            if (this.dgvLeaveList.CurrentCell != null)
            {
                if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
                {
                    Rectangle rect_from = this.dgvLeaveList.GetCellDisplayRectangle(5, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).SetBounds(rect_from.X + 2, rect_from.Y + 4, rect_from.Width, rect_from.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    Rectangle rect_to = this.dgvLeaveList.GetCellDisplayRectangle(6, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).SetBounds(rect_to.X + 2, rect_to.Y + 4, rect_to.Width, rect_to.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_status", true).Length > 0)
                {
                    Rectangle rect_status = this.dgvLeaveList.GetCellDisplayRectangle(8, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_status", true)[0]).SetBounds(rect_status.X + 2, rect_status.Y + 4, rect_status.Width, rect_status.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_customer", true).Length > 0)
                {
                    Rectangle rect_customer = this.dgvLeaveList.GetCellDisplayRectangle(9, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomTextBox)this.dgvLeaveList.Parent.Controls.Find("inline_customer", true)[0]).SetBounds(rect_customer.X + 3, rect_customer.Y + 4, rect_customer.Width - 1, rect_customer.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true).Length > 0)
                {
                    Rectangle rect_medcert = this.dgvLeaveList.GetCellDisplayRectangle(10, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true)[0]).SetBounds(rect_medcert.X + 2, rect_medcert.Y + 4, rect_medcert.Width, rect_medcert.Height - 3);
                }
                if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
                {
                    Rectangle rect_fine = this.dgvLeaveList.GetCellDisplayRectangle(11, this.dgvLeaveList.CurrentCell.RowIndex, true);
                    ((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).SetBounds(rect_fine.X + 3, rect_fine.Y + 4, rect_fine.Width - 1, rect_fine.Height - 3);
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
            if (this.dgvLeaveList.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_status");
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_customer");
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_medcert");
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
            {
                this.dgvLeaveList.Parent.Controls.RemoveByKey("inline_fine");
            }
            this.dgvLeaveList.Enabled = true;
        }

        private void SubmitEditEvent()
        {
            if (this.dgvLeaveList.CurrentCell != null)
            {
                string json_data = "{\"id\":" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).id.ToString() + ",";
                json_data += "\"users_name\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).users_name + "\",";
                json_data += "\"date\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).date + "\",";
                json_data += "\"event_type\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).event_type + "\",";
                json_data += "\"event_code\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).event_code + "\",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\",";

                // from_time
                if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
                {
                    json_data += "\"from_time\":\"" + ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Time.ToString("HH:mm", cinfo_th.DateTimeFormat) + "\",";
                }
                else
                {
                    json_data += "\"from_time\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).from_time + "\",";
                }
                // to_time
                if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
                {
                    json_data += "\"to_time\":\"" + ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Time.ToString("HH:mm", cinfo_th.DateTimeFormat) + "\",";
                }
                else
                {
                    json_data += "\"to_time\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).to_time + "\",";
                }
                // status
                if (this.dgvLeaveList.Parent.Controls.Find("inline_status", true).Length > 0)
                {
                    json_data += "\"status\":" + ((ComboboxItem)((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_status", true)[0]).comboBox1.SelectedItem).int_value.ToString() + ",";
                }
                else
                {
                    json_data += "\"status\":" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).status + ",";
                }
                // customer
                if (this.dgvLeaveList.Parent.Controls.Find("inline_customer", true).Length > 0)
                {
                    json_data += "\"customer\":\"" + ((CustomTextBox)this.dgvLeaveList.Parent.Controls.Find("inline_customer", true)[0]).Texts.cleanString() + "\",";
                }
                else
                {
                    json_data += "\"customer\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).customer + "\",";
                }
                // med_cert
                if (this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true).Length > 0)
                {
                    json_data += "\"med_cert\":\"" + ((ComboboxItem)((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true)[0]).comboBox1.SelectedItem).string_value + "\",";
                }
                else
                {
                    json_data += "\"med_cert\":\"" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).med_cert + "\",";
                }
                // fine
                if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
                {
                    string fine = (((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).Text.Trim().Length == 0 ? "0" : ((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).Value.ToString());
                    json_data += "\"fine\":" + fine + "}";
                }
                else
                {
                    json_data += "\"fine\":" + ((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag).fine + "}";
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
                        this.ClearInlineFormLeaveList();
                        this.LoadEventAndFill((EventCalendar)this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag);
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

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.FormReadItem();
                return;
            }
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.FormRead();
                return;
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SubmitEditEvent();
                return;
            }
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.FormRead();
                return;
            }

        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = true;
            page_setup.PageSettings.Margins = new Margins(0, 0, 10, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {
                    row_num = 0;
                    page_no = 0;
                };

                print_doc.PrintPage += delegate(object obj_sender, PrintPageEventArgs pe)
                {
                    bool is_new_page = true;
                    page_no++;

                    using (Font font = new Font("tahoma", 8f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            using (Pen p = new Pen(Color.LightGray))
                            {
                                int y_pos = pe.MarginBounds.Top;
                                #region declare column width
                                int col0_width = 40; // seq
                                int col1_width = 70; // users_name
                                int col2_width = 70; // date
                                int col3_width = 50; // from_time
                                int col4_width = 50; // to_time
                                int col5_width = 110; // duration
                                int col6_width = 155; // event_code
                                int col7_width = 80; // status
                                int col8_width = 270; // customer
                                int col9_width = 100; // med_cert
                                int col10_width = 80; // fine
                                int col11_width = 70; // rec_by
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
                                using (Font h_font = new Font("tahoma", 11f, FontStyle.Bold))
                                {
                                    pe.Graphics.DrawString("รายละเอียดวันลา/ออกพบลูกค้า", h_font, brush, new Rectangle(10, y_pos, 300, 20));
                                }
                                using (Font p_font = new Font("tahoma", 7f))
                                {
                                    pe.Graphics.DrawString("หน้า : " + page_no.ToString(), p_font, brush, new Rectangle(1000, y_pos, 150, 20), str_format_right);
                                }
                                y_pos += 25;
                                pe.Graphics.DrawString("รหัสพนักงาน จาก ", font, brush, new Rectangle(10, y_pos, 100, 20));
                                pe.Graphics.DrawString(" : " + this.lblUserFrom.Text, font, brush, new Rectangle(110, y_pos, 100, 20));
                                pe.Graphics.DrawString(" ถึง ", font, brush, new Rectangle(210, y_pos, 30, 20));
                                pe.Graphics.DrawString(" : " + this.lblUserTo.Text, font, brush, new Rectangle(240, y_pos, 100, 20));

                                y_pos += 20;
                                pe.Graphics.DrawString("วันที่ จาก ", font, brush, new Rectangle(10, y_pos, 100, 20));
                                pe.Graphics.DrawString(" : " + this.lblDateFrom.Text, font, brush, new Rectangle(110, y_pos, 100, 20));
                                pe.Graphics.DrawString(" ถึง ", font, brush, new Rectangle(210, y_pos, 30, 20));
                                pe.Graphics.DrawString(" : " + this.lblDateTo.Text, font, brush, new Rectangle(240, y_pos, 100, 20));

                                y_pos += 20;
                                #endregion Report Header

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
                                            pe.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), new RectangleF(x_pos, y_pos, /*790*/ 1145, 25));

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + /*790*/ 1145, y_pos); // horizontal line upper

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect0 = new Rectangle(x_pos, y_pos, col0_width, 25);
                                            pe.Graphics.DrawString("ลำดับ", font, brush, header_rect0, str_format_center);
                                            x_pos += col0_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("รหัสพนักงาน", font, brush, new Rectangle(x_pos, y_pos, col1_width, 25), str_format_center);
                                            x_pos += col1_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect1 = new Rectangle(x_pos, y_pos, col2_width, 25);
                                            pe.Graphics.DrawString("วันที่", font, brush, header_rect1, str_format_center);
                                            x_pos += col2_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect2 = new Rectangle(x_pos, y_pos, col3_width, 25);
                                            pe.Graphics.DrawString("จาก", font, brush, header_rect2, str_format_center);
                                            x_pos += col3_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect3 = new Rectangle(x_pos, y_pos, col4_width, 25);
                                            pe.Graphics.DrawString("ถึง", font, brush, header_rect3, str_format_center);
                                            x_pos += col4_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect4 = new Rectangle(x_pos, y_pos, col5_width, 25);
                                            pe.Graphics.DrawString("รวมเวลา", font, brush, header_rect4, str_format_center);
                                            x_pos += col5_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect5 = new Rectangle(x_pos, y_pos, col6_width, 25);
                                            pe.Graphics.DrawString("เหตุผล", font, brush, header_rect5, str_format_center);
                                            x_pos += col6_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect6 = new Rectangle(x_pos, y_pos, col7_width, 25);
                                            pe.Graphics.DrawString("สถานะ", font, brush, header_rect6, str_format_center);
                                            x_pos += col7_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect7 = new Rectangle(x_pos, y_pos, col8_width, 25);
                                            pe.Graphics.DrawString("ชื่อลูกค้า", font, brush, header_rect7, str_format_center);
                                            x_pos += col8_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("ใบรับรองแพทย์", font, brush, new Rectangle(x_pos, y_pos, col9_width, 25), str_format_center);
                                            x_pos += col9_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("หักค่าคอมฯ", font, brush, new Rectangle(x_pos, y_pos, col10_width, 25), str_format_center);
                                            x_pos += col10_width;


                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            Rectangle header_rect8 = new Rectangle(x_pos, y_pos, col11_width, 25);
                                            pe.Graphics.DrawString("บันทึกโดย", font, brush, header_rect8, str_format_center);
                                            x_pos += col11_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator

                                            x_pos = 10; // set x_pos again after use in header
                                            y_pos += 25;
                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos + /*790*/ 1145, y_pos); // horizontal line below
                                        }

                                        y_pos += 7;
                                        is_new_page = false;
                                    }
                                    #endregion draw column header

                                    #region draw row data
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    Rectangle rect0 = new Rectangle(x_pos, y_pos, col0_width - 5, 18);
                                    pe.Graphics.DrawString((row_num + 1).ToString(), font, brush, rect0, str_format_right);
                                    x_pos += col0_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    pe.Graphics.DrawString(this.sorted_list[i].users_name, font, brush, new Rectangle(x_pos + 5, y_pos, col1_width - 5, 18), str_format_left);
                                    x_pos += col1_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    Rectangle rect1 = new Rectangle(x_pos, y_pos, col2_width, 18);
                                    pe.Graphics.DrawString(DateTime.Parse(this.sorted_list[i].date).ToString("dd/MM/yy", cinfo_th), font, brush, rect1, str_format_center);
                                    x_pos += col2_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    Rectangle rect2 = new Rectangle(x_pos, y_pos, col3_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].from_time.Substring(0, 5), font, brush, rect2, str_format_center);
                                    x_pos += col3_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect3 = new Rectangle(x_pos, y_pos, col4_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].to_time.Substring(0, 5), font, brush, rect3, str_format_center);
                                    x_pos += col4_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect4 = new Rectangle(x_pos, y_pos, col5_width, 18);
                                    string time_duration = this.sorted_list.Where(t => t.id == this.sorted_list[i].id).ToList<EventCalendar>().GetSummaryLeaveDayString();
                                    pe.Graphics.DrawString(time_duration, font, brush, rect4, str_format_center);
                                    x_pos += col5_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect5 = new Rectangle(x_pos, y_pos, col6_width, 18);
                                    string leave_cause = (this.leave_cause.Find(t => t.tabtyp == this.sorted_list[i].event_type && t.typcod == this.sorted_list[i].event_code) != null ? this.leave_cause.Find(t => t.tabtyp == this.sorted_list[i].event_type && t.typcod == this.sorted_list[i].event_code).typdes_th : "");
                                    pe.Graphics.DrawString(leave_cause, font, brush, rect5, str_format_left);
                                    x_pos += col6_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect6 = new Rectangle(x_pos, y_pos, col7_width, 18);
                                    string status = (this.sorted_list[i].status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? "WAIT" : (this.sorted_list[i].status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? "CANCELED" : "CONFIRMED"));
                                    using (SolidBrush status_brush = (this.sorted_list[i].status == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? new SolidBrush(Color.Blue) : (this.sorted_list[i].status == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? new SolidBrush(Color.Red) : new SolidBrush(Color.Black))))
                                    {
                                        pe.Graphics.DrawString(status, font, status_brush, rect6, str_format_left);
                                    }
                                    x_pos += col7_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    Rectangle rect7 = new Rectangle(x_pos, y_pos, col8_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].customer, font, brush, rect7, str_format_left);
                                    x_pos += col8_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    string med_cert = (this.sorted_list[i].med_cert == "Y" ? "มีใบรับรองแพทย์" : "");
                                    pe.Graphics.DrawString(med_cert, font, brush, new Rectangle(x_pos + 5, y_pos, col9_width - 5, 18), str_format_left);
                                    x_pos += col9_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);
                                    string fine = (this.sorted_list[i].fine > 0 ? this.sorted_list[i].fine.ToString() : "");
                                    pe.Graphics.DrawString(fine, font, brush, new Rectangle(x_pos + 5, y_pos, col10_width - 5, 18), str_format_right);
                                    x_pos += col10_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    Rectangle rect8 = new Rectangle(x_pos, y_pos, col11_width, 18);
                                    pe.Graphics.DrawString(this.sorted_list[i].rec_by, font, brush, rect8, str_format_left);
                                    x_pos += col11_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 20, x_pos + /*790*/ 1145, y_pos + 20);
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

                                y_pos += 10;
                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                using (Font bold_font = new Font("tahoma", 8f, FontStyle.Bold))
                                {
                                    pe.Graphics.DrawString(this.groupPeriod.Text.Trim(), bold_font, brush, new Rectangle(10, y_pos, 400, 15));
                                    pe.Graphics.DrawString(this.groupTotal.Text.Trim(), bold_font, brush, new Rectangle(420, y_pos, 400, 15));
                                }

                                y_pos += 20;
                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString("ลางาน", font, brush, new Rectangle(10, y_pos, 80, 15));
                                pe.Graphics.DrawString(" : " + this.lblPeriodAbsent.Text, font, brush, new Rectangle(90, y_pos, 320, 15));
                                pe.Graphics.DrawString("ลางาน", font, brush, new Rectangle(420, y_pos, 80, 15));
                                pe.Graphics.DrawString(" : " + this.lblTotalAbsent.Text, font, brush, new Rectangle(500, y_pos, 320, 15));

                                y_pos += 20;
                                if (y_pos > pe.MarginBounds.Bottom)
                                {
                                    pe.HasMorePages = true;
                                    return;
                                }
                                else
                                {
                                    pe.HasMorePages = false;
                                }
                                pe.Graphics.DrawString("ออกพบลูกค้า", font, brush, new Rectangle(10, y_pos, 80, 15));
                                pe.Graphics.DrawString(" : " + this.lblPeriodServ.Text, font, brush, new Rectangle(90, y_pos, 320, 15));
                                pe.Graphics.DrawString("ออกพบลูกค้า", font, brush, new Rectangle(420, y_pos, 80, 15));
                                pe.Graphics.DrawString(" : " + this.lblTotalServ.Text, font, brush, new Rectangle(500, y_pos, 320, 15));
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

        private void toolStripPrintSummary_Click(object sender, EventArgs e)
        {
            PrintDocument print_doc = new PrintDocument();

            PageSetupDialog page_setup = new PageSetupDialog();
            page_setup.Document = print_doc;
            page_setup.PageSettings.PaperSize = new PaperSize("A4", 825, 1165);
            page_setup.PageSettings.Landscape = false;
            page_setup.PageSettings.Margins = new Margins(0, 0, 10, 40);

            PrintOutputSelection wind = new PrintOutputSelection();
            if (wind.ShowDialog() == DialogResult.OK)
            {
                int row_num = 0;
                int page_no = 0;
                print_doc.BeginPrint += delegate(object obj_sender, PrintEventArgs pe)
                {
                    row_num = 0;
                    page_no = 0;
                };

                print_doc.PrintPage += delegate(object obj_sender, PrintPageEventArgs pe)
                {
                    bool is_new_page = true;
                    page_no++;

                    using (Font font = new Font("tahoma", 8f))
                    {
                        using (SolidBrush brush = new SolidBrush(Color.Black))
                        {
                            using (Pen p = new Pen(Color.LightGray))
                            {
                                int y_pos = pe.MarginBounds.Top;
                                #region declare column width
                                int col0_width = 40; // seq
                                int col1_width = 70; // users_name
                                int col2_width = 100; // real_name
                                //int col3_width = 50; // from_time
                                //int col4_width = 50; // to_time
                                int col3_width = 160; // actual leave duration
                                int col4_width = 160; // comm_deduct leave duration
                                //int col6_width = 155; // event_code
                                //int col7_width = 80; // status
                                //int col8_width = 270; // customer
                                //int col4_width = 100; // med_cert
                                int col5_width = 100; // fine
                                int col6_width = 160; // remark
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
                                using (Font h_font = new Font("tahoma", 11f, FontStyle.Bold))
                                {
                                    pe.Graphics.DrawString("สรุปวันลา (สำหรับคิดค่าคอมฯ)", h_font, brush, new Rectangle(10, y_pos, 300, 20));
                                }
                                using (Font p_font = new Font("tahoma", 7f))
                                {
                                    pe.Graphics.DrawString("หน้า : " + page_no.ToString(), p_font, brush, new Rectangle(640, y_pos, 150, 20), str_format_right);
                                }
                                y_pos += 25;
                                pe.Graphics.DrawString("รหัสพนักงาน จาก ", font, brush, new Rectangle(10, y_pos, 100, 20));
                                pe.Graphics.DrawString(" : " + this.lblUserFrom.Text, font, brush, new Rectangle(110, y_pos, 100, 20));
                                pe.Graphics.DrawString(" ถึง ", font, brush, new Rectangle(210, y_pos, 30, 20));
                                pe.Graphics.DrawString(" : " + this.lblUserTo.Text, font, brush, new Rectangle(240, y_pos, 100, 20));

                                y_pos += 20;
                                pe.Graphics.DrawString("วันที่ จาก ", font, brush, new Rectangle(10, y_pos, 100, 20));
                                pe.Graphics.DrawString(" : " + this.lblDateFrom.Text, font, brush, new Rectangle(110, y_pos, 100, 20));
                                pe.Graphics.DrawString(" ถึง ", font, brush, new Rectangle(210, y_pos, 30, 20));
                                pe.Graphics.DrawString(" : " + this.lblDateTo.Text, font, brush, new Rectangle(240, y_pos, 100, 20));

                                y_pos += 20;
                                #endregion Report Header

                                for (int i = row_num; i < this.users_list.Count; i++)
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
                                            pe.Graphics.DrawString("ลำดับ", font, brush, new Rectangle(x_pos, y_pos, col0_width, 25), str_format_center);
                                            x_pos += col0_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("รหัสพนักงาน", font, brush, new Rectangle(x_pos, y_pos, col1_width, 25), str_format_center);
                                            x_pos += col1_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("ชื่อ", font, brush, new Rectangle(x_pos, y_pos, col2_width, 25), str_format_center);
                                            x_pos += col2_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("จำนวนวันลา (จริง)", font, brush, new Rectangle(x_pos, y_pos, col3_width, 25), str_format_center);
                                            x_pos += col3_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("จำนวนวันลา (คิดค่าคอมฯ)", font, brush, new Rectangle(x_pos, y_pos, col4_width, 25), str_format_center);
                                            x_pos += col4_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("หักค่าคอมฯ (บาท)", font, brush, new Rectangle(x_pos, y_pos, col5_width, 25), str_format_center);
                                            x_pos += col5_width;

                                            pe.Graphics.DrawLine(pen_darkgray, x_pos, y_pos, x_pos, y_pos + 25); // column separator
                                            pe.Graphics.DrawString("หมายเหตุ", font, brush, new Rectangle(x_pos, y_pos, col6_width, 25), str_format_center);
                                            x_pos += col6_width;

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
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    using (SolidBrush brush_gray = new SolidBrush(Color.Gray))
                                    {
                                        pe.Graphics.DrawString((row_num + 1).ToString(), font, brush_gray, new Rectangle(x_pos, y_pos, col0_width - 5, 18), str_format_right);
                                        x_pos += col0_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator
                                    }

                                    using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                    {
                                        pe.Graphics.DrawString(this.users_list[i].username, font_bold, brush, new Rectangle(x_pos + 5, y_pos, col1_width - 5, 18), str_format_left);
                                        x_pos += col1_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator
                                    }

                                    pe.Graphics.DrawString(this.users_list[i].name, font, brush, new Rectangle(x_pos + 5, y_pos, col2_width - 5, 18), str_format_left);
                                    x_pos += col2_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20); // column separator

                                    //using (SolidBrush brush_gray = new SolidBrush(Color.Gray))
                                    //{
                                        pe.Graphics.DrawString(this.sorted_list.Where(s => s.users_name == this.users_list[i].username).Where(s => s.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>().GetSummaryLeaveDayString(), font, brush, new Rectangle(x_pos, y_pos, col3_width, 18), str_format_center);
                                        x_pos += col3_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator
                                    //}

                                    using (Font font_bold = new Font("tahoma", 8f, FontStyle.Bold))
                                    {
                                        pe.Graphics.DrawString(this.sorted_list.Where(s => s.users_name == this.users_list[i].username).Where(s => s.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>().GetSummaryLeaveDayStringForCommission(), font_bold, brush, new Rectangle(x_pos, y_pos, col4_width, 18), str_format_center);
                                        x_pos += col4_width;
                                        pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator
                                    }

                                    string fine = (this.sorted_list.Where(s => s.users_name == this.users_list[i].username).Where(s => s.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>().GetSummaryFine() == 0 ? "" : this.sorted_list.Where(s => s.users_name == this.users_list[i].username).Where(s => s.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>().GetSummaryFine().ToString());
                                    pe.Graphics.DrawString(fine, font, brush, new Rectangle(x_pos, y_pos, col5_width - 5, 18), str_format_right);
                                    x_pos += col5_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    pe.Graphics.DrawString(this.sorted_list.Where(s => s.users_name == this.users_list[i].username).Where(s => s.event_type == EventCalendar.EVENT_TYPE_ABSENT_CAUSE).ToList<EventCalendar>().GetSummaryMedCertRemark(), font, brush, new Rectangle(x_pos + 5, y_pos, col6_width - 5, 18), str_format_left);
                                    x_pos += col6_width;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos - 6, x_pos, y_pos + 20);  // column separator

                                    // Horizontal line
                                    x_pos = 10;
                                    pe.Graphics.DrawLine(p, x_pos, y_pos + 20, x_pos + 790, y_pos + 20);
                                    #endregion draw row data

                                    row_num++;
                                    y_pos += 25;
                                }
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
                sb.AppendLine("ลำดับ,รหัสพนักงาน,ชื่อ,วันที่,จาก,ถึง,รวมเวลา,เหตุผล,ชื่อลูกค้า,สถานะ,ใบรับรองแพทย์,หักค่าคอมฯ,บันทึกโดย");

                int cnt = 0;
                foreach (DataRow row in dt.Rows)
                {
                    cnt++;
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();

                    // Append some column data as we need
                    sb.AppendLine(cnt.ToString() + "," +
                                    fields[1] + "," +
                                    fields[2] + "," +
                                    fields[3] + "," +
                                    fields[4] + "," +
                                    fields[5] + "," +
                                    this.event_calendar.Where(t => t.id == Convert.ToInt32(fields[0])).ToList<EventCalendar>().GetSummaryLeaveDayString().Replace(",", " : ") + "," +
                                    this.leave_cause.Find(t => t.tabtyp == fields[6] && t.typcod == fields[7]).typdes_th + "," +
                                    fields[8] + "," +
                                    (Convert.ToInt32(fields[9]) == (int)CustomDateEvent.EVENT_STATUS.WAIT_FOR_CONFIRM ? "WAIT" : (Convert.ToInt32(fields[9]) == (int)CustomDateEvent.EVENT_STATUS.CANCELED ? "CANCELED" : "CONFIRMED")) + "," +
                                    (fields[10] == "Y" ? "มีใบรับรองแพทย์" : "") + "," +
                                    (fields[11] == "0" ? "" : fields[11]) + "," +
                                    fields[12]);
                }
                this.SaveExportedFile(destination_filename, sb.ToString());
            }
        }

        private void toolStripExportSummary_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Comma separated value | *.csv";
            dlg.DefaultExt = "csv";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string destination_filename = dlg.FileName;

                DataTable dt = this.users_list.ToDataTable<Users>();

                StringBuilder sb = new StringBuilder();

                // Create column header as datatable header
                //string[] columnNames = dt.Columns.Cast<DataColumn>().
                //                                  Select(column => column.ColumnName).
                //                                  ToArray();
                //sb.AppendLine(string.Join(",", columnNames));

                // Create custom column header as we need
                sb.AppendLine("ลำดับ,รหัสพนักงาน,ชื่อ,จำนวนวันลา(จริง),จำนวนวันลา(คิดค่าคอมฯ),หักค่าคอมฯ,หมายเหตุ");

                int cnt = 0;
                foreach (DataRow row in dt.Rows)
                {
                    cnt++;
                    string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();

                    // Append some column data as we need
                    sb.AppendLine(cnt.ToString() + "," +
                                    fields[1] + "," +
                                    fields[3] + "," +
                                    this.sorted_list.Where(s => s.users_name == fields[1] && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString()).ToList<EventCalendar>().GetSummaryLeaveDayString().Replace(",", " : ") + "," +
                                    this.sorted_list.Where(s => s.users_name == fields[1] && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString()).ToList<EventCalendar>().GetSummaryLeaveDayStringForCommission().Replace(",", " : ") + "," +
                                    this.sorted_list.Where(s => s.users_name == fields[1] && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString()).ToList<EventCalendar>().GetSummaryFine().ToString().Replace(",", " : ") + "," +
                                    this.sorted_list.Where(s => s.users_name == fields[1] && s.event_type == Istab.TABTYP.ABSENT_CAUSE.ToTabtypString()).ToList<EventCalendar>().GetSummaryMedCertRemark());
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

        private void toolStripRange_Click(object sender, EventArgs e)
        {
            LeaveRangeDialog dlg = new LeaveRangeDialog(this.main_form, this.current_user_from, this.current_user_to, this.current_date_from, this.current_date_to);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.current_user_from = dlg.user_from;
                this.current_user_to = dlg.user_to;
                this.current_date_from = dlg.date_from;
                this.current_date_to = dlg.date_to;

                this.LoadEventAndFill();
            }
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;

            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintDetail.Enabled = false;
            this.toolStripPrintSummary.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripExportDetail.Enabled = false;
            this.toolStripExportSummary.Enabled = false;
            this.toolStripRange.Enabled = false;

            if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_status", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                ((CustomTextBox)this.dgvLeaveList.Parent.Controls.Find("inline_customer", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true)[0]).Read_Only = true;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
            {
                ((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).Enabled = false;
            }
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;

            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripPrint.Enabled = true;
            this.toolStripPrintDetail.Enabled = true;
            this.toolStripPrintSummary.Enabled = true;
            this.toolStripExport.Enabled = true;
            this.toolStripExportDetail.Enabled = true;
            this.toolStripExportSummary.Enabled = true;
            this.toolStripRange.Enabled = true;
        }

        private void FormReadItem()
        {
            this.form_mode = FORM_MODE.READ_ITEM;

            this.toolStripStop.Enabled = false;
            this.toolStripSave.Enabled = false;
            this.toolStripPrint.Enabled = true;
            this.toolStripPrintDetail.Enabled = true;
            this.toolStripPrintSummary.Enabled = true;
            this.toolStripExport.Enabled = true;
            this.toolStripExportDetail.Enabled = true;
            this.toolStripExportSummary.Enabled = true;
            this.toolStripRange.Enabled = true;

            this.ClearInlineFormLeaveList();
            this.dgvAbsentSummary.Enabled = true;
            this.dgvServiceSummary.Enabled = true;
            this.dgvLeaveList.Enabled = true;
            this.dgvLeaveList.Focus();
        }

        private void FormEditItem()
        {
            this.form_mode = FORM_MODE.EDIT_ITEM;

            this.toolStripStop.Enabled = true;
            this.toolStripSave.Enabled = true;
            this.toolStripPrint.Enabled = false;
            this.toolStripPrintDetail.Enabled = false;
            this.toolStripPrintSummary.Enabled = false;
            this.toolStripExport.Enabled = false;
            this.toolStripExportDetail.Enabled = false;
            this.toolStripExportSummary.Enabled = false;
            this.toolStripRange.Enabled = false;

            if (this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_from_time", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true).Length > 0)
            {
                ((CustomTimePicker)this.dgvLeaveList.Parent.Controls.Find("inline_to_time", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_status", true).Length > 0)
            {
                ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_status", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_customer", true).Length > 0)
            {
                ((CustomTextBox)this.dgvLeaveList.Parent.Controls.Find("inline_customer", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true).Length > 0)
            {
                ((CustomComboBox)this.dgvLeaveList.Parent.Controls.Find("inline_medcert", true)[0]).Read_Only = false;
            }
            if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
            {
                ((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).Enabled = true;
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
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM || this.form_mode == FORM_MODE.PROCESSING)
                    return true;

                if (this.dgvLeaveList.Parent.Controls.Find("inline_fine", true).Length > 0)
                {
                    if (((NumericUpDown)this.dgvLeaveList.Parent.Controls.Find("inline_fine", true)[0]).Focused)
                    {
                        this.SubmitEditEvent();
                        return true;
                    }
                }
                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.F7)
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    this.dgvLeaveGroup.Focus();
                    return true;
                }
            }

            if (keyData == Keys.F8)
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    //this.toolStripItem.PerformClick();
                    this.tabControl1.SelectedTab = this.tabPage1;
                    this.dgvLeaveList.Focus();
                    return true;
                }
            }

            if (keyData == Keys.F9)
            {
                this.toolStripSave.PerformClick();
                return true;
            }

            if (keyData == Keys.F12)
            {
                this.toolStripExportDetail.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.F12))
            {
                this.toolStripExportSummary.PerformClick();
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.toolStripStop.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.dgvLeaveList.Focused && (this.dgvLeaveList.Rows[this.dgvLeaveList.CurrentCell.RowIndex].Tag is EventCalendar) && (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM))
                {
                    this.FormReadItem();
                    this.ShowInlineFormLeaveList();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.P))
            {
                this.toolStripPrintDetail.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.P))
            {
                this.toolStripPrintSummary.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}