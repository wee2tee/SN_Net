using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class NoteCalendar
    {
        public enum NOTE_TYPE : int
        {
            WEEKDAY = 0,
            HOLIDAY = 1
        }

        public int id { get; set; }
        public string date { get; set; }
        public int type { get; set; }
        public string description { get; set; }
        public string group_maid { get; set; }
        public string group_weekend { get; set; }
        public int max_leave { get; set; }
        public string rec_by { get; set; }

        public DateTime _Date
        {
            get
            {
                return DateTime.Parse(this.date, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None);
            }
        }
    }
}
