using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;
using SN_Net.Subform;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Reflection;
using SN_Net.ViewModels;
using CC;
using System.ComponentModel;
using SN_Net.Model;

namespace SN_Net.MiscClass
{
    public static class HelperClass
    {
        public enum DGV_TAG
        {
            READ,
            DELETE,
            LEAVE
        }

        public static string ToTabtypString(this Istab.TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case Istab.TABTYP.AREA:
                    return "01";
                case Istab.TABTYP.VEREXT:
                    return "02";
                case Istab.TABTYP.HOWKNOWN:
                    return "03";
                case Istab.TABTYP.BUSITYP:
                    return "04";
                case Istab.TABTYP.PROBLEM_CODE:
                    return "05";
                case Istab.TABTYP.ABSENT_CAUSE:
                    return "06";
                case Istab.TABTYP.SERVICE_CASE:
                    return "07";
                case Istab.TABTYP.USER_GROUP:
                    return "08";
                default:
                    return "00";
            }
        }

        public static void KeepLog(this Form form, string msg)
        {
            using (StreamWriter file = new StreamWriter("SN_Log.txt", true))
            {
                file.WriteLine(msg);
            }
        }

        public static string ToYesOrNoString(this CheckState check_state)
        {
            if (check_state == CheckState.Checked)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        public static string GetVerextSelectedString(this string verext, ComboBox cbVerext)
        {
            foreach (ComboboxItem item in cbVerext.Items)
            {
                if (item.string_value == verext)
                {
                    return item.ToString();
                }
            }
            return "";
        }

        public static void DrawLineEffect(this DataGridView datagrid)
        {
            datagrid.Paint += delegate
            {
                if (datagrid.Rows.Count > 0)
                {
                    Rectangle rect = datagrid.GetRowDisplayRectangle(datagrid.CurrentCell.RowIndex, true);
                    Graphics g = datagrid.CreateGraphics();
                    if (datagrid.Rows[datagrid.CurrentCell.RowIndex].Cells[0].Tag is DataRowIntention)
                    {
                        if (((DataRowIntention)datagrid.Rows[datagrid.CurrentCell.RowIndex].Cells[0].Tag).to_do == DataRowIntention.TO_DO.DELETE)
                        {
                            Pen p = new Pen(Color.Red, 1f);

                            for (int i = rect.Left - 16; i < rect.Right; i += 8)
                            {
                                g.DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
                            }
                        }
                        else
                        {
                            g.DrawLine(new Pen(Color.Red), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                            g.DrawLine(new Pen(Color.Red), rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                        }
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.Red), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        g.DrawLine(new Pen(Color.Red), rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                    }
                }
            };
        }

        public static void DrawDgvRowBorder(this DataGridView dgv)
        {
            //dgv.Enter += delegate(object sender, EventArgs e)
            //{
            //    ((DataGridView)sender).Tag = DGV_TAG.FOCUSED;
            //    ((DataGridView)sender).Refresh();
            //};

            //dgv.Leave += delegate(object sender, EventArgs e)
            //{
            //    ((DataGridView)sender).Tag = DGV_TAG.LEAVED;
            //    ((DataGridView)sender).Refresh();
            //};

            dgv.Paint += delegate
            {
                if (dgv.CurrentCell != null)
                {
                    Rectangle rect = dgv.GetRowDisplayRectangle(dgv.CurrentCell.RowIndex, false);
                    using (Pen p = new Pen(Color.Red))
                    {
                        if ((DGV_TAG)dgv.Tag == DGV_TAG.READ || (DGV_TAG)dgv.Tag == DGV_TAG.DELETE)
                        {
                            dgv.CreateGraphics().DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                            dgv.CreateGraphics().DrawLine(p, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);

                            if ((DGV_TAG)dgv.Tag == DGV_TAG.DELETE)
                            {
                                for (int i = rect.Left - 16; i < rect.Right; i += 8)
                                {
                                    dgv.CreateGraphics().DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
                                }
                            }
                        }
                    }
                }
            };
        }

        public static bool tryParseToDateTime(this String str_date)
        {
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            DateTime out_date;
            if (DateTime.TryParse(str_date, cinfo_th, DateTimeStyles.None, out out_date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string M2WDate(this string mysql_date)
        {
            string new_date = (Convert.ToInt32(mysql_date.Substring(0,4)) + 543).ToString() + mysql_date.Substring(4,6);

            CultureInfo cinfo_th = new CultureInfo("th-TH");
            DateTime out_date;
            if (DateTime.TryParse(new_date, cinfo_th, DateTimeStyles.None, out out_date))
            {
                return out_date.ToString("dd/MM/yyyy", cinfo_th.DateTimeFormat);
            }
            else
            {
                return "";
            }
        }

        public static string ToMysqlDate(this DateTime date)
        {
            CultureInfo cinfo_en = CultureInfo.CreateSpecificCulture("en-US"); // specify culture to US for retrieve the year in US culture format
            return date.ToString("yyyy-MM-dd", cinfo_en.DateTimeFormat);
        }

        public static string ToDMYDateValue(this DateTime date)
        {
            CultureInfo cinfo_th = CultureInfo.CreateSpecificCulture("th-TH");
            return date.ToString("dd/MM/yy", cinfo_th);
        }

        /**
         *  <summary>Get the user level string</summary>
         *  
         **/
        public static string ToUserLevelString(this int user_level)
        {
            return GlobalVar.GetUserLevelString(user_level);
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static string TF2YN(this string true_or_false, bool false2blank = false)
        {
            if (true_or_false == "True")
            {
                return "Y";
            }
            else if (true_or_false == "False")
            {
                if(false2blank){
                    return "";
                }
                else
                {
                    return "N";
                }
            }
            else
            {
                return "";
            }
        }

        public static int GetDayIntOfWeek(this DateTime date)
        {
            if (date.DayOfWeek.ToString() == "Sunday")
            {
                return 1;
            }
            else if (date.DayOfWeek.ToString() == "Monday")
            {
                return 2;
            }
            else if (date.DayOfWeek.ToString() == "Tuesday")
            {
                return 3;
            }
            else if (date.DayOfWeek.ToString() == "Wednesday")
            {
                return 4;
            }
            else if (date.DayOfWeek.ToString() == "Thursday")
            {
                return 5;
            }
            else if (date.DayOfWeek.ToString() == "Friday")
            {
                return 6;
            }
            else if (date.DayOfWeek.ToString() == "Saturday")
            {
                return 7;
            }
            else
            {
                return 8;
            }
        }

        public static DateTime TimeString2DateTime(this string time_string)
        {
            string[] time = time_string.Split(':');
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0);
        }

        public static string ThaiDayOfWeek(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "วันศุกร์";
                case DayOfWeek.Monday:
                    return "วันจันทร์";
                case DayOfWeek.Saturday:
                    return "วันเสาร์";
                case DayOfWeek.Sunday:
                    return "วันอาทิตย์";
                case DayOfWeek.Thursday:
                    return "วันพฤหัสบดี";
                case DayOfWeek.Tuesday:
                    return "วันอังคาร";
                case DayOfWeek.Wednesday:
                    return "วันพุธ";
                default:
                    return "";
            }
        }

        public static string GetSummaryLeaveDayString(this List<EventCalendar> list_event_calendar)
        {
            string leave_string = "";
            TimeSpan t = TimeSpan.Parse("00:00:00");
            foreach (EventCalendar ev in list_event_calendar)
            {
                TimeSpan t_from = TimeSpan.Parse(ev.from_time);
                TimeSpan t_to = TimeSpan.Parse(ev.to_time);

                if (t_from.Hours <= 12 && t_to.Hours >= 13) // ลางานคาบเกี่ยวช่วงเที่ยง
                {
                    t = t.Add(t_to - t_from - TimeSpan.Parse("01:00:00")); // ลบช่วงเวลาพักเที่ยงออก 1 ชม.
                }
                else
                {
                    t = t.Add(t_to - t_from);
                }
            }
            if (t.TotalMinutes > 0)
            {
                int days = Convert.ToInt32(Math.Floor((double)((t.TotalMinutes / 60) / 8)));
                int hours = Convert.ToInt32(Math.Floor((double)(t.TotalMinutes / 60))) - (days * 8);
                int minutes = Convert.ToInt32(t.TotalMinutes) - (days * 8 * 60) - (hours * 60);
                leave_string +=
                    (days > 0 ? " " + days.ToString() + " วัน" : "") +
                    (hours > 0 ? (days > 0 ? ", " + hours.ToString() : " " + hours.ToString()) + " ชั่วโมง" : "") +
                    (minutes > 0 ? (hours > 0 ? ", " + minutes.ToString() : " " + minutes.ToString()) + " นาที" : "");
            }

            return leave_string;
        }

        public static string GetSummaryLeaveDayStringForCommission(this List<EventCalendar> list_event_calendar)
        {
            string leave_string = "";
            TimeSpan t = TimeSpan.Parse("00:00:00");
            foreach (EventCalendar ev in list_event_calendar)
            {
                TimeSpan t_from = TimeSpan.Parse(ev.from_time);
                TimeSpan t_to = TimeSpan.Parse(ev.to_time);

                if (t_from.Hours <= 12 && t_to.Hours >= 13) // ลางานคาบเกี่ยวช่วงเที่ยง
                {
                    // ถ้ามีใบรับรองแพทย์ คิดวันลาครึ่งหนึ่ง
                    if (ev.med_cert == "Y")
                    {
                        t = t.Add(TimeSpan.FromSeconds((t_to - t_from - TimeSpan.Parse("01:00:00")).TotalSeconds / 2)); // ลบช่วงเวลาพักเที่ยงออก 1 ชม. แล้วนำมาหาร 2
                    }
                    else
                    {
                        t = t.Add(t_to - t_from - TimeSpan.Parse("01:00:00")); // ลบช่วงเวลาพักเที่ยงออก 1 ชม.
                    }
                }
                else
                {
                    if (ev.med_cert == "Y")
                    {
                        t = t.Add(TimeSpan.FromSeconds((t_to - t_from).TotalSeconds / 2));
                    }
                    else
                    {
                        t = t.Add(t_to - t_from);
                    }
                }
            }
            if (t.TotalMinutes > 0)
            {
                int days = Convert.ToInt32(Math.Floor((double)((t.TotalMinutes / 60) / 8)));
                int hours = Convert.ToInt32(Math.Floor((double)(t.TotalMinutes / 60))) - (days * 8);
                int minutes = Convert.ToInt32(t.TotalMinutes) - (days * 8 * 60) - (hours * 60);
                leave_string +=
                    (days > 0 ? " " + days.ToString() + " วัน" : "") +
                    (hours > 0 ? (days > 0 ? ", " + hours.ToString() : " " + hours.ToString()) + " ชั่วโมง" : "") +
                    (minutes > 0 ? (hours > 0 ? ", " + minutes.ToString() : " " + minutes.ToString()) + " นาที" : "");
            }

            return leave_string;
        }

        public static string GetSummaryHoursMinutesString(this List<EventCalendar> list_event_calendar)
        {
            string leave_string = "";
            TimeSpan t = TimeSpan.Parse("00:00:00");
            foreach (EventCalendar ev in list_event_calendar)
            {
                TimeSpan t_from = TimeSpan.Parse(ev.from_time);
                TimeSpan t_to = TimeSpan.Parse(ev.to_time);

                if (t_from.Hours <= 12 && t_to.Hours >= 13) // ลางานคาบเกี่ยวช่วงเที่ยง
                {
                    t = t.Add(t_to - t_from - TimeSpan.Parse("01:00:00")); // ลบช่วงเวลาพักเที่ยงออก 1 ชม.
                }
                else
                {
                    t = t.Add(t_to - t_from);
                }
            }
            if (t.TotalMinutes > 0)
            {
                //int days = Convert.ToInt32(Math.Floor((double)((t.TotalMinutes / 60) / 8)));
                int hours = Convert.ToInt32(Math.Floor((double)(t.TotalMinutes / 60)));
                int minutes = Convert.ToInt32(t.TotalMinutes) - (hours * 60);
                leave_string +=
                    //(days > 0 ? " " + days.ToString() + " วัน" : "") +
                    (hours > 0 ? hours.ToString() + " ชั่วโมง" : "") +
                    (minutes > 0 ? (hours > 0 ? ", " + minutes.ToString() + " นาที" : "" + minutes.ToString() + " นาที") : "");
            }

            return leave_string;
        }

        public static TimeSpan GetSummaryTimeSpan(this List<EventCalendar> list_event_calendar)
        {
            TimeSpan t = TimeSpan.Parse("00:00:00");
            foreach (EventCalendar ev in list_event_calendar)
            {
                TimeSpan t_from = TimeSpan.Parse(ev.from_time);
                TimeSpan t_to = TimeSpan.Parse(ev.to_time);

                if (t_from.Hours <= 12 && t_to.Hours >= 13) // ลางานคาบเกี่ยวช่วงเที่ยง
                {
                    t = t.Add(t_to - t_from - TimeSpan.Parse("01:00:00")); // ลบช่วงเวลาพักเที่ยงออก 1 ชม.
                }
                else
                {
                    t = t.Add(t_to - t_from);
                }
            }

            return t;
        }

        public static int GetSummaryFine(this List<EventCalendar> list_event_calendar)
        {
            int fine = 0;
            foreach (EventCalendar ev in list_event_calendar)
            {
                fine += ev.fine;
            }

            return fine;
        }

        public static string GetSummaryMedCertRemark(this List<EventCalendar> list_event_calendar)
        {
            string str = "";
            foreach (EventCalendar ev in list_event_calendar.Where(e => e.med_cert == "Y").ToList<EventCalendar>())
            {
                str = "ลาป่วยโดยมีใบรับรองแพทย์";
            }

            return str;
        }

        public static TimeSpan GetSummaryTalkTime(this List<Note> notes)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0);
            foreach (Note n in notes.Where(n => n.is_break == "N"))
            {
                ts = ts.Add(TimeSpan.Parse(n.duration));
            }
            return ts;
        }

        public static TimeSpan GetSummaryBreakTime(this List<Note> notes)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0);
            foreach (Note n in notes.Where(n => n.is_break == "Y"))
            {
                ts = ts.Add(TimeSpan.Parse(n.duration));
            }
            return ts;
        }

        // Distinc a List by some field value
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static List<AbsentVM> ToAbsentViewModel(this IEnumerable<EventCalendar> ev, List<Istab> absent_cause, List<Users> users_list, int max_leave_person)
        {
            List<AbsentVM> absent = new List<AbsentVM>();

            int seq = 0;
            foreach (var item in ev)
            {
                Users user = users_list.Where(u => u.username == item.users_name).FirstOrDefault();
                int countable = user != null && user.level >= (int)USER_LEVEL.SUPERVISOR ? 0 : 1;
                absent.Add(new AbsentVM
                {
                    event_calendar = item,
                    seq = (item.status == (int)EventCalendar.EVENT_STATUS.CANCELED || countable == 0 ? "" : (++seq).ToString()),
                    name = item.realname,
                    event_code = (item.event_type == EventCalendar.EVENT_TYPE_SERVICE_CASE ? item.event_code : absent_cause.Where(a => a.typcod == item.event_code).First().abbreviate_th),
                    event_desc = item.customer.Trim().Length > 0 ? item.customer : item.GetEventCalendarTimeString(),
                    countable_leave_person = countable
                });
            }
            for (int i = seq; i < max_leave_person; i++)
            {
                absent.Add(new AbsentVM
                {
                    event_calendar = null,
                    seq = (++seq).ToString(),
                    name = "",
                    event_code = "",
                    event_desc = "",
                    countable_leave_person = 1
                });
            }

            return absent.OrderByDescending(a => a.countable_leave_person).ToList();
        }

        public static List<EventCalendar> ExtractToEventCalendar(this IEnumerable<AbsentVM> absent_list)
        {
            List<EventCalendar> event_calendar = new List<EventCalendar>();
            foreach (var item in absent_list)
            {
                if (item.event_calendar != null)
                {
                    event_calendar.Add(item.event_calendar);
                }
            }

            return event_calendar;
        }

        public static string GetEventCalendarTimeString(this EventCalendar ev)
        {
            TimeSpan to_time = new TimeSpan(Convert.ToInt32(ev.to_time.Split(':')[0]), Convert.ToInt32(ev.to_time.Split(':')[1]), 0);
            TimeSpan from_time = new TimeSpan(Convert.ToInt32(ev.from_time.Split(':')[0]), Convert.ToInt32(ev.from_time.Split(':')[1]), 0);
            DateTime event_date = DateTime.Parse(ev.date, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);

            if (from_time.Hours <= 12 && to_time.Hours >= 13)
            {
                return ((to_time - from_time - TimeSpan.Parse("01:00:00")).Hours >= 1 ? (to_time - from_time - TimeSpan.Parse("01:00:00")).Hours.ToString() + ((to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes > 1 ? ":" + (to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes.ToString() + " ชม." : " ชม.") : (to_time - from_time - TimeSpan.Parse("01:00:00")).Minutes.ToString() + " นาที") + ((event_date.GetDayIntOfWeek() >= 2 && event_date.GetDayIntOfWeek() <= 6) && (from_time.Equals(TimeSpan.Parse("08:30:00")) && to_time.Equals(TimeSpan.Parse("17:30:00"))) ? "(เต็มวัน)" : "(" + from_time.ToString().Substring(0, 5) + " - " + to_time.ToString().Substring(0, 5) + ")");
            }
            else
            {
                return ((to_time - from_time).Hours >= 1 ? (to_time - from_time).Hours.ToString() + ((to_time - from_time).Minutes > 1 ? ":" + (to_time - from_time).Minutes.ToString() + " ชม." : " ชม.") : (to_time - from_time).Minutes.ToString() + " นาที") + ((event_date.GetDayIntOfWeek() >= 2 && event_date.GetDayIntOfWeek() <= 6) && (from_time.Equals(TimeSpan.Parse("08:30:00")) && to_time.Equals(TimeSpan.Parse("17:30:00"))) ? "(เต็มวัน)" : "(" + from_time.ToString().Substring(0, 5) + " - " + to_time.ToString().Substring(0, 5) + ")");
            }
        }

        public static List<NoteCalendarVM> ToHolidayViewModel(this List<NoteCalendar> note_list)
        {
            List<NoteCalendarVM> note = new List<NoteCalendarVM>();

            int count = 0;
            foreach (var n in note_list)
            {
                if (n.type == (int)NoteCalendar.NOTE_TYPE.WEEKDAY) // skip if it's normal weekday
                    continue;

                note.Add(new NoteCalendarVM // add holiday to list
                {
                    noteCalendar = n,
                    seq = ++count,
                    date = n.date,
                    description = n.description,
                    rec_by = n.rec_by
                });
            }

            return note;
        }

        public static void SetControlState(this Component comp, FORM_MODE[] form_mode_to_enable, FORM_MODE form_mode, string accessibility_by_scacclv = null)
        {
            if (form_mode_to_enable.ToList().Where(fm => fm == form_mode).Count() > 0)
            {
                if (comp is ToolStripButton)
                {
                    ((ToolStripButton)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((ToolStripButton)comp).Enabled = false;

                    return;
                }
                if (comp is ToolStripSplitButton)
                {
                    ((ToolStripSplitButton)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((ToolStripSplitButton)comp).Enabled = false;

                    return;
                }
                if (comp is ToolStripDropDownButton)
                {
                    ((ToolStripDropDownButton)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((ToolStripDropDownButton)comp).Enabled = false;

                    return;
                }
                if (comp is ToolStripMenuItem)
                {
                    ((ToolStripMenuItem)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((ToolStripMenuItem)comp).Enabled = false;

                    return;
                }
                if (comp is TabControl)
                {
                    ((TabControl)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((TabControl)comp).Enabled = false;

                    return;
                }
                if (comp is Button)
                {
                    ((Button)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((Button)comp).Enabled = false;

                    return;
                }
                if (comp is CheckBox)
                {
                    ((CheckBox)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((CheckBox)comp).Enabled = false;

                    return;
                }
                if (comp is DataGridView)
                {
                    ((DataGridView)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((DataGridView)comp).Enabled = false;

                    return;
                }
                if (comp is XTextEdit)
                {
                    ((XTextEdit)comp)._ReadOnly = false;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((XTextEdit)comp)._ReadOnly = true;

                    return;
                }
                if (comp is XDropdownList)
                {
                    ((XDropdownList)comp)._ReadOnly = false;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((XDropdownList)comp)._ReadOnly = true;

                    return;
                }
                if (comp is XDatePicker)
                {
                    ((XDatePicker)comp)._ReadOnly = false;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((XDatePicker)comp)._ReadOnly = true;

                    return;
                }
                if (comp is XBrowseBox)
                {
                    ((XBrowseBox)comp)._ReadOnly = false;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((XBrowseBox)comp)._ReadOnly = true;

                    return;
                }
                if (comp is XTextEditMasked)
                {
                    ((XTextEditMasked)comp)._ReadOnly = false;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((XTextEditMasked)comp)._ReadOnly = true;

                    return;
                }
                if (comp is TextBox)
                {
                    ((TextBox)comp).ReadOnly = false; ;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((TextBox)comp).ReadOnly = true;
                }
                if (comp is NumericUpDown)
                {
                    ((NumericUpDown)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((NumericUpDown)comp).ReadOnly = true;
                }
                if (comp is RadioButton)
                {
                    ((RadioButton)comp).Enabled = true;
                    if (accessibility_by_scacclv != null && accessibility_by_scacclv == "N")
                        ((RadioButton)comp).Enabled = false;
                }
            }
            else
            {
                if (comp is ToolStripButton)
                {
                    ((ToolStripButton)comp).Enabled = false; return;
                }
                if (comp is ToolStripSplitButton)
                {
                    ((ToolStripSplitButton)comp).Enabled = false; return;
                }
                if (comp is ToolStripDropDownButton)
                {
                    ((ToolStripDropDownButton)comp).Enabled = false; return;
                }
                if (comp is ToolStripMenuItem)
                {
                    ((ToolStripMenuItem)comp).Enabled = false; return;
                }
                if (comp is TabControl)
                {
                    ((TabControl)comp).Enabled = false; return;
                }
                if (comp is Button)
                {
                    ((Button)comp).Enabled = false; return;
                }
                if (comp is CheckBox)
                {
                    ((CheckBox)comp).Enabled = false; return;
                }
                if (comp is DataGridView)
                {
                    ((DataGridView)comp).Enabled = false; return;
                }
                if (comp is XTextEdit)
                {
                    ((XTextEdit)comp)._ReadOnly = true; return;
                }
                if (comp is XDropdownList)
                {
                    ((XDropdownList)comp)._ReadOnly = true; return;
                }
                if (comp is XDatePicker)
                {
                    ((XDatePicker)comp)._ReadOnly = true; return;
                }
                if (comp is XBrowseBox)
                {
                    ((XBrowseBox)comp)._ReadOnly = true; return;
                }
                if (comp is XTextEditMasked)
                {
                    ((XTextEditMasked)comp)._ReadOnly = true; return;
                }
                if (comp is TextBox)
                {
                    ((TextBox)comp).ReadOnly = true; return;
                }
                if (comp is NumericUpDown)
                {
                    ((NumericUpDown)comp).Enabled = false; return;
                }
                if (comp is RadioButton)
                {
                    ((RadioButton)comp).Enabled = false; return;
                }
            }
        }

        public static void SetControlVisibility(this Component comp, FORM_MODE[] form_mode_to_visible, FORM_MODE form_mode)
        {
            if (form_mode_to_visible.Where(fm => fm == form_mode).Count() > 0)
            {
                if (comp is Control)
                {
                    ((Control)comp).Visible = true; return;
                }
            }
            else
            {
                if (comp is Control)
                {
                    ((Control)comp).Visible = false; return;
                }
            }
        }

        public static void SetControlVisibilityByUserLevel(this Component comp, users current_user, USER_LEVEL[] user_level_to_visible)
        {
            if(user_level_to_visible.Where(l => (int)l == current_user.level).Count() > 0)
            {
                if(comp is Control)
                {
                    ((Control)comp).Visible = true; return;
                }
            }
            else
            {
                if(comp is Control)
                {
                    ((Control)comp).Visible = false; return;
                }
            }
        }

        public static void SetInlineControlPosition(this Control inline_control, DataGridView dgv, int row_index, int column_index)
        {
            if (inline_control != null)
            {
                Rectangle rect = dgv.GetCellDisplayRectangle(column_index, row_index, true);

                if(inline_control is CheckBox)
                {
                    var x = rect.X + Convert.ToInt32(Math.Floor((double)(rect.Width - inline_control.Width) / 2));
                    var y = rect.Y + Convert.ToInt32(Math.Floor((double)(rect.Height - inline_control.Height) / 2));

                    inline_control.SetBounds(x, y, inline_control.Width, inline_control.Height);
                }
                else
                {
                    inline_control.SetBounds(rect.X, rect.Y + 1, rect.Width - 1, rect.Height - 3);
                }
            }
        }

        public static string FillLeadingZero(this int source_string, int total_digit)
        {
            string result = string.Empty;

            for (int i = 0; i < total_digit - source_string.ToString().Length; i++)
            {
                result += "0";
            }

            return result + source_string.ToString();
        }

        public static IEnumerable<String> SplitInParts(this String s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }


        public static string ToBytesString(this string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string bytes_str = string.Empty;
            foreach (var b in bytes)
            {
                bytes_str += ((int)b).FillLeadingZero(4);
                //Console.WriteLine(" -- > " + (int)b);
            }

            return bytes_str;
        }

        public static string ExtractBytesString(this string bytes_string)
        {
            IEnumerable<string> str = bytes_string.SplitInParts(4);

            List<byte> b = new List<byte>();
            foreach (var s in str)
            {
                b.Add((byte)Convert.ToInt32(s));
            }

            return Encoding.UTF8.GetString(b.ToArray());
        }

        public static TimeSpan GetDifTimeInDate(this string from_time, string to_time)
        {
            try
            {
                TimeSpan from = TimeSpan.ParseExact(from_time, @"hh\:mm\:ss", CultureInfo.GetCultureInfo("th-TH"));
                TimeSpan to = TimeSpan.ParseExact(to_time, @"hh\:mm\:ss", CultureInfo.GetCultureInfo("th-TH"));
                
                return to - from;
            }
            catch (Exception)
            {
                return TimeSpan.ParseExact("00:00:00", @"hh\:mm\:ss", CultureInfo.InvariantCulture);
            }
        }

        public static bool HasComment(this note note)
        {
            using (sn_noteEntities sn_note = DBXNote.DataSet())
            {
                if (sn_note.note_comment.Where(c => c.note_id == note.id).AsEnumerable().Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static DateTime GetFirstDateOfMonth(this DateTime curr_date)
        {
            return curr_date.AddDays((curr_date.Day * -1) + 1);
        }

        public static DateTime GetLastDateOfMonth(this DateTime curr_date)
        {
            return curr_date.AddDays((curr_date.Day * -1) + 1).AddMonths(1).AddDays(-1);
        }

        public static bool IsLastSaturday(this DateTime curr_date)
        {
            if (curr_date.DayOfWeek != DayOfWeek.Saturday)
                return false;

            int curr_month = curr_date.Month;

            int next7day_month = curr_date.AddDays(7).Month;

            if (curr_month != next7day_month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGroupHolidayFor(this DateTime curr_dat, users user)
        {
            if (user == null)
                return false;

            using (snEntities sn = DBX.DataSet())
            {
                istab user_group = sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_USERGROUP && i.id == user.usergroup_id).FirstOrDefault();
                if (user_group == null)
                    return false;

                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    var note_cal = note.note_calendar.Where(n => n.date.CompareTo(curr_dat) == 0).FirstOrDefault();
                    if (note_cal == null)
                        return false;

                    if(user_group.typcod.Trim().Length > 0 && note_cal.group_weekend != null && user_group.typcod.Trim() == note_cal.group_weekend.Trim())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int GetUnixTimeStamp(this DateTime date)
        {
            return Convert.ToInt32(date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static string GetTimeSpanString(this TimeSpan ts)
        {
            string str = string.Empty;
            int days = ts.TotalHours / 8 >= 1 ? Convert.ToInt32(Math.Floor(ts.TotalHours / 8)) : 0;
            int hrs = Math.Floor(ts.TotalHours) - (days * 8) >= 1 ? Convert.ToInt32(Math.Floor(ts.TotalHours) - (days * 8)) : 0;
            int mins = ts.TotalMinutes - (days * 8 * 60) - (hrs * 60) >= 1 ? Convert.ToInt32(ts.TotalMinutes - (days * 8 * 60) - (hrs * 60)) : 0; // Convert.ToInt32(Math.Floor(ts.TotalMinutes - (ts.TotalHours * 60)));

            str += (days > 0 ? days.ToString() + " วัน  " : "") + (hrs > 0 ? hrs.ToString() + " ชั่วโมง  " : "") + (mins > 0 ? mins.ToString() + " นาที" : "");
            return str;
        }

        public static TimeSpan GetDayTimeSpan(this TimeSpan start_time, TimeSpan end_time)
        {
            TimeSpan ts = end_time.Subtract(start_time);

            if(start_time.CompareTo(TimeSpan.Parse("12:00")) < 0 && end_time.CompareTo(TimeSpan.Parse("13:00")) > 0)
            {
                ts = ts.Subtract(TimeSpan.Parse("01:00"));
            }

            return ts;
        }

        public static int GetTotalDays(this TimeSpan ts)
        {
            return ts.TotalHours / 8 >= 1 ? Convert.ToInt32(Math.Floor(ts.TotalHours / 8)) : 0;
        }
    }
}
