using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class NoteCalendar
    {
        public int id { get; set; }
        public string date { get; set; }
        public int type { get; set; }
        public string description { get; set; }
        public string group_maid { get; set; }
        public string group_weekend { get; set; }
        public int max_leave { get; set; }
        public string rec_by { get; set; }
    }
}
