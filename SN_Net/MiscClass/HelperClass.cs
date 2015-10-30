using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Data;
using System.Reflection;

namespace SN_Net.MiscClass
{
    public static class HelperClass
    {
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
                case Istab.TABTYP.LEAVE_CAUSE:
                    return "06";
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
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
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
                //leave_string += "ลางาน,ออกพบลูกค้า ";
                int days = Convert.ToInt32(Math.Floor((double)((t.TotalMinutes / 60) / 8)));
                int hours = Convert.ToInt32(Math.Floor((double)(t.TotalMinutes / 60))) - (days * 8);
                int minutes = Convert.ToInt32(t.TotalMinutes) - (days * 8 * 60) - (hours * 60);
                leave_string +=
                    (days > 0 ? days.ToString() + " วัน" : "") +
                    (hours > 0 ? " " + hours.ToString() + " ชั่วโมง" : "") +
                    (minutes > 0 ? " " + minutes.ToString() + " นาที" : "");
            }

            return leave_string;
        }
    }
}
