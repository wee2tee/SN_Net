using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class Dealer
    {
        public int id { get; set; }
        public string dealer { get; set; }
        public string prenam { get; set; }
        public string compnam { get; set; }
        public string addr01 { get; set; }
        public string addr02 { get; set; }
        public string addr03 { get; set; }
        public string zipcod { get; set; }
        public string telnum { get; set; }
        public string faxnum { get; set; }
        public string contact { get; set; }
        public string position { get; set; }
        public string busides { get; set; }
        public string area { get; set; }
        public string remark { get; set; }
        public string chgdat { get; set; }
        //public int users_id { get; set; }
        public string users_name { get; set; }
    }
}
