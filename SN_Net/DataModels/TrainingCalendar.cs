using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class TrainingCalendar
    {
        public int id { get; set; }
        public string date { get; set; }
        public int course_type { get; set; }
        public string trainer { get; set; }
        public int status { get; set; }
        public int term { get; set; }
        public string remark { get; set; }
        public string rec_by { get; set; }
    }
}
