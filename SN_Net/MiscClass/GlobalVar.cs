using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public class GlobalVar
    {
        public const int USER_LEVEL_ADMIN = 9;
        public const int USER_LEVEL_SUPERVISOR = 8;
        public const int USER_LEVEL_SUPPORT = 0;
        public const int USER_LEVEL_SALES = 1;
        public const int USER_LEVEL_ACCOUNT = 2;

        public int loged_in_user_id { get; set; }
        public string loged_in_user_name { get; set; }
        public string loged_in_user_email { get; set; }
        public string loged_in_user_realname { get; set; }
        public int loged_in_user_level { get; set; }
        public string loged_in_user_status { get; set; }
        public string loged_in_user_allowed_web_login { get; set; }
        public bool loged_in_user_training_expert { get; set; }
        public string current_mac_address { get; set; }

        public static string GetUserLevelString(int user_level)
        {
            if (user_level == USER_LEVEL_ADMIN)
                return "Admin";
            if (user_level == USER_LEVEL_SUPERVISOR)
                return "Supervisor";
            if (user_level == USER_LEVEL_SUPPORT)
                return "Support";
            if (user_level == USER_LEVEL_SALES)
                return "Sales";
            if (user_level == USER_LEVEL_ACCOUNT)
                return "Account";

            return "";
        }
    }
}
