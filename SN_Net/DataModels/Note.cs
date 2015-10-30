using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class Note
    {
        public SupportNote supportnote { get; set; }
        public int id { get; set; }
        public string is_break { get; set; }
        public string seq { get; set; }
        public string users_name { get; set; }
        public string date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string sernum { get; set; }
        public string contact { get; set; }

        #region problem
        public bool map_drive { get; set; }
        public bool install { get; set; }
        public bool error { get; set; }
        public bool fonts { get; set; }
        public bool print { get; set; }
        public bool training { get; set; }
        public bool stock { get; set; }
        public bool form { get; set; }
        public bool rep_excel { get; set; }
        public bool statement { get; set; }
        public bool asset { get; set; }
        public bool secure { get; set; }
        public bool year_end { get; set; }
        public bool period { get; set; }
        public bool mail_wait { get; set; }
        #endregion problem

        public string remark { get; set; }

        #region break reason
        public string reason { get; set; }
        //public bool toilet { get; set; }
        //public bool qt { get; set; }
        //public bool meet_cust { get; set; }
        //public bool train { get; set; }
        //public bool correct_data { get; set; }
        //public string break_reason { get; set; }
        #endregion break reason

    }
}
