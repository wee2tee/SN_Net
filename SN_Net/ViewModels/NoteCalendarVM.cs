using SN_Net.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SN_Net.ViewModels
{
    public class NoteCalendarVM
    {
        public NoteCalendar noteCalendar { get; set; }
        public int seq { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string rec_by { get; set; }

        public DateTime? _date
        {
            get
            {
                DateTime out_datetime;

                if (DateTime.TryParse(this.date, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out out_datetime))
                {
                    return out_datetime;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
