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

        public string type_desc
        {
            get
            {
                if (this.event_type == EVENT_TYPE_ABSENT_CAUSE)
                {
                    return "";
                }
                else if(this.event_type == EVENT_TYPE_SERVICE_CASE)
                {
                    return this.event_code;
                }
                else
                {
                    return this.event_code;
                }
            }
        }

        public const string EVENT_TYPE_ABSENT_CAUSE = "06";
        public const string EVENT_TYPE_SERVICE_CASE = "07";

        public enum EVENT_STATUS : int
        {
            WAIT = 0,
            CONFIRMED = 1,
            CANCELED = 2
        }
        //public const int EVENT_STATUS_WAIT = 0;
        //public const int EVENT_STATUS_CONFIRM = 1;
        //public const int EVENT_STATUS_CANCELED = 2;
    }
}
