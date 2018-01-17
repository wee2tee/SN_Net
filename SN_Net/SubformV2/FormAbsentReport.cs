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
    public partial class FormAbsentReport : Form
    {
        private users users_from = null;
        private users users_to = null;
        private DateTime? date_from = null;
        private DateTime? date_to = null;

        private BindingList<AbsentCauseVM> cause1_list;
        private BindingList<AbsentCauseVM> cause2_list;
        private BindingList<AbsentPersonStatVM> absent_person_list;
        private BindingList<AbsentPersonStatVM> absent_summary_list;
        private TimeSpan yearly_absent;
        private TimeSpan yearly_cust;

        public FormAbsentReport()
        {
            InitializeComponent();
        }

        private void FormAbsentReport_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
        }

        private void FormAbsentReport_Shown(object sender, EventArgs e)
        {
            if(this.users_from == null || this.users_to == null || this.date_from == null || this.date_to == null)
            {
                DialogAbsentReportScope scope = new DialogAbsentReportScope();
                if(scope.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                else
                {
                    this.users_from = scope.user_from;
                    this.users_to = scope.user_to;
                    this.date_from = scope.date_from;
                    this.date_to = scope.date_to;

                    this.GetData();
                    this.FillForm();
                }
            }
            
        }

        private void GetData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var person_absent = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() && ev.date.CompareTo(this.date_from.Value) >= 0 && ev.date.CompareTo(this.date_to.Value) <= 0).OrderBy(ev => ev.date).ThenBy(ev => ev.from_time).ToAbsentPersonStatVM();
                var p_seq = 0;
                person_absent.ForEach(ev => { p_seq++; ev.seq = p_seq; });
                this.absent_person_list = new BindingList<AbsentPersonStatVM>(person_absent);
                

                var all_absent = note.event_calendar.Where(ev => ev.date.CompareTo(this.date_from.Value) >= 0 && ev.date.CompareTo(this.date_to.Value) <= 0).ToList();
                this.absent_summary_list = new BindingList<AbsentPersonStatVM>(all_absent.ToAbsentPersonStatVM());

                DateTime first_date_of_year = DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-01-01", CultureInfo.GetCultureInfo("en-US"));
                DateTime last_date_of_year = DateTime.Parse(this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("en-US")) + "-12-31", CultureInfo.GetCultureInfo("en-US"));

                this.yearly_absent = TimeSpan.Parse("00:00");
                var year_abs = note.event_calendar.Where(ev => ev.users_name.Trim() == this.users_from.username.Trim() &&
                                ev.event_type == CALENDAR_EVENT_TYPE.ABSENT && 
                                ev.status != (int)CALENDAR_EVENT_STATUS.CANCELED &&
                                ev.date.CompareTo(first_date_of_year.Date) >= 0 && ev.date.CompareTo(last_date_of_year.Date) <= 0).Select(ev => new EventCalYearlyTimeSpan { event_calendar = ev }).ToList();
                year_abs.ForEach(ev => { this.yearly_absent = this.yearly_absent.Add(ev.time_span); });
            }
        }

        private void FillForm()
        {
            this.grpYear.Text = "สะสมจากต้นปี (ปี " + this.date_to.Value.ToString("yyyy", CultureInfo.GetCultureInfo("th-TH")) + ")";
            this.lblUserFrom.Text = this.users_from.username + " : " + this.users_from.name;
            this.lblUserTo.Text = this.users_to.username + " : " + this.users_to.name;
            this.lblDateFrom.Text = this.date_from.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            this.lblDateTo.Text = this.date_to.Value.ToString("dddd  d MMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            
            TimeSpan ts = TimeSpan.Parse("00:00");
            this.absent_person_list.Where(i => i.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED && i.event_calendar.event_type == CALENDAR_EVENT_TYPE.ABSENT).ToList().ForEach(i =>
            {
                TimeSpan t = TimeSpan.Parse(i.time_to).Subtract(TimeSpan.Parse(i.time_from));
                if (TimeSpan.Parse(i.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(i.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    t = t.Subtract(TimeSpan.Parse("01:00"));
                }
                ts = ts.Add(t);
            });
            this.lblPtdAbsent.Text = ts.GetTimeSpanString();

            ts = TimeSpan.Parse("00:00");
            this.absent_person_list.Where(i => i.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED && i.event_calendar.event_type == CALENDAR_EVENT_TYPE.MEET_CUST).ToList().ForEach(i =>
            {
                TimeSpan t = TimeSpan.Parse(i.time_to).Subtract(TimeSpan.Parse(i.time_from));
                if (TimeSpan.Parse(i.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(i.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    t = t.Subtract(TimeSpan.Parse("01:00"));
                }
                ts = ts.Add(t);
            });
            this.lblPtdCust.Text = ts.GetTimeSpanString();

            this.lblYearAbsent.Text = this.yearly_absent.GetTimeSpanString() + " (Max. " + this.users_from.max_absent.ToString() + " วัน)";
            this.lblYearAbsent.ForeColor = this.yearly_absent.GetTotalDays() > this.users_from.max_absent ? Color.Red : Color.Black;

            this.dtYearAbsentFrom.Value = DateTime.Now;
            this.dtYearAbsentTo.Value = DateTime.Now;

            this.dgvDetail.DataSource = this.absent_person_list;
            this.dgvSum.DataSource = this.absent_summary_list;
            this.SetCauseList();
        }

        private void SetCauseList()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                var list1 = note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.ABSENT).OrderBy(i => i.typdes_th).ToAbsentCauseVM();
                list1.ForEach(i =>
                {
                    TimeSpan ts = TimeSpan.Parse("00:00");
                    this.absent_person_list.Where(a => a.event_calendar.event_code_id == i.istab.id && a.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED).ToList().ForEach(j =>
                    {
                        TimeSpan day_prd = TimeSpan.Parse(j.time_to).Subtract(TimeSpan.Parse(j.time_from));
                        if(TimeSpan.Parse(j.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(j.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                        {
                            day_prd = day_prd.Subtract(TimeSpan.Parse("01:00"));
                        }
                        ts = ts.Add(day_prd);
                    });
                    i.stat = ts.GetTimeSpanString();
                });
                this.cause1_list = new BindingList<AbsentCauseVM>(list1);
                
                this.dgvAbsent.DataSource = this.cause1_list;

                var list2 = note.note_istab.Where(i => i.tabtyp == CALENDAR_EVENT_TYPE.MEET_CUST).OrderBy(i => i.typdes_th).ToAbsentCauseVM();
                list2.ForEach(i =>
                {
                    TimeSpan ts = TimeSpan.Parse("00:00");
                    this.absent_person_list.Where(a => a.event_calendar.event_code_id == i.istab.id && a.event_calendar.status != (int)CALENDAR_EVENT_STATUS.CANCELED).ToList().ForEach(j =>
                    {
                        TimeSpan day_prd = TimeSpan.Parse(j.time_to).Subtract(TimeSpan.Parse(j.time_from));
                        if (TimeSpan.Parse(j.time_from).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(j.time_to).CompareTo(TimeSpan.Parse("13:00")) > 0)
                        {
                            day_prd = day_prd.Subtract(TimeSpan.Parse("01:00"));
                        }
                        ts = ts.Add(day_prd);
                    });
                    i.stat = ts.GetTimeSpanString();
                });
                this.cause2_list = new BindingList<AbsentCauseVM>(list2);
                this.dgvCust.DataSource = this.cause2_list;
            }
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeScope_Click(object sender, EventArgs e)
        {
            DialogAbsentReportScope scope = new DialogAbsentReportScope(this.users_from, this.date_from, this.date_to);
            if (scope.ShowDialog() == DialogResult.OK)
            {
                this.users_from = scope.user_from;
                this.users_to = scope.user_to;
                this.date_from = scope.date_from;
                this.date_to = scope.date_to;

                this.GetData();
                this.FillForm();
            }
        }

        private void dgvAbsent_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int cause_id = ((note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_istab.Name].Value).id;

            if(this.absent_person_list.Where(i => i.event_calendar.event_code_id == cause_id && i.event_calendar.status == (int)CALENDAR_EVENT_STATUS.CONFIRMED).Count() > 0) // person absent list has this cause
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_selected.Name].Value = true;
            }
            else
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c1_selected.Name].Value = false;
                e.CellStyle.BackColor = Color.LightGray;
                e.CellStyle.SelectionBackColor = Color.LightGray;
                e.CellStyle.ForeColor = Color.DimGray;
                e.CellStyle.SelectionForeColor = Color.DimGray;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvCust_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int cause_id = ((note_istab)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_istab.Name].Value).id;

            if (this.absent_person_list.Where(i => i.event_calendar.event_code_id == cause_id && i.event_calendar.status == (int)CALENDAR_EVENT_STATUS.CONFIRMED).Count() > 0) // person absent list has this cause
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_selected.Name].Value = true;
            }
            else
            {
                ((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c2_selected.Name].Value = false;
                e.CellStyle.BackColor = Color.LightGray;
                e.CellStyle.SelectionBackColor = Color.LightGray;
                e.CellStyle.ForeColor = Color.DimGray;
                e.CellStyle.SelectionForeColor = Color.DimGray;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            int status = ((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c3_event_calendar.Name].Value).status.Value;
            DayOfWeek dow = ((event_calendar)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_c3_event_calendar.Name].Value).date.DayOfWeek;

            if (status == (int)CALENDAR_EVENT_STATUS.WAIT)
            {
                e.CellStyle.BackColor = Color.Lavender;
                e.CellStyle.SelectionBackColor = Color.Lavender;
            }
            else if(status == (int)CALENDAR_EVENT_STATUS.CANCELED)
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.SelectionBackColor = Color.MistyRose;
            }
            else if(dow == DayOfWeek.Saturday)
            {
                e.CellStyle.BackColor = Color.Beige;
                e.CellStyle.SelectionBackColor = Color.Beige;
            }

            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
            e.Handled = true;
        }

        private void dgvAbsent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if(e.ColumnIndex == ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_c1_selected.Name).FirstOrDefault().Index)
            {
                
            }
        }

        private void btnOKYearAbsent_Click(object sender, EventArgs e)
        {
            TimeSpan ts = TimeSpan.Parse("17:13");
            Console.WriteLine(" === > " + ts.GetTimeSpanString());
            //Console.WriteLine(" => total hours : " + ts.TotalHours);
            //Console.WriteLine(" => total minutes : " + ts.TotalMinutes);
        }
    }

    public class EventCalYearlyTimeSpan
    {
        public event_calendar event_calendar { get; set; }
        public TimeSpan time_span
        {
            get
            {
                TimeSpan ts = TimeSpan.Parse(this.event_calendar.to_time).Subtract(TimeSpan.Parse(this.event_calendar.from_time));

                if(TimeSpan.Parse(this.event_calendar.from_time).CompareTo(TimeSpan.Parse("12:00")) < 0 && TimeSpan.Parse(this.event_calendar.to_time).CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    ts = ts.Subtract(TimeSpan.Parse("01:00"));
                }
                return ts;
            }
        }
    }
}