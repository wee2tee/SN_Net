using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class AbsentVM
    {
        public EventCalendar event_calendar { get; set; }
        public string seq { get; set; }
        public string name { get; set; }
        public string event_code { get; set; }
        public string event_desc { get; set; }
        public int countable_leave_person { get; set; } // 0 or 1
    }
}
