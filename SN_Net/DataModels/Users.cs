using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; } // sensitive data not retrieve from server
        public string name { get; set; }
        public string email { get; set; }
        public int level { get; set; }
        public string usergroup { get; set; }
        public string status { get; set; }
        public string allowed_web_login { get; set; }
        public string training_expert { get; set; }
        public int max_absent { get; set; }
        public string create_at { get; set; }
        public string last_use { get; set; }
    }
}
