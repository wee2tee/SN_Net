using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public class GlobalVar
    {
        public int loged_in_user_id { get; set; }
        public string loged_in_user_name { get; set; }
        public string loged_in_user_email { get; set; }
        public int loged_in_user_level { get; set; }
        public string loged_in_user_status { get; set; }
        public string loged_in_user_allowed_web_login { get; set; }
        public string current_mac_address { get; set; }
    }
}
