using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class EventCalendar
    {
        public int id { get; set; }
        public string users_name { get; set; }
        public string realname { get; set; }
        public string date { get; set; }
        public string from_time { get; set; }
        public string to_time { get; set; }
        public string event_type { get; set; }
        public string event_code { get; set; }
        public string customer { get; set; }
        public int status { get; set; }
        public string med_cert { get; set; }
        public int fine { get; set; }
        public string rec_by { get; set; }


        public const string EVENT_TYPE_ABSENT_CAUSE = "06";
        public const string EVENT_TYPE_SERVICE_CASE = "07";
    }
}
