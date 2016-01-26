using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        public string map_drive { get; set; }
        public string install { get; set; }
        public string error { get; set; }
        public string fonts { get; set; }
        public string print { get; set; }
        public string training { get; set; }
        public string stock { get; set; }
        public string form { get; set; }
        public string rep_excel { get; set; }
        public string statement { get; set; }
        public string asset { get; set; }
        public string secure { get; set; }
        public string year_end { get; set; }
        public string period { get; set; }
        public string mail_wait { get; set; }
        public string transfer_mkt { get; set; }
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
