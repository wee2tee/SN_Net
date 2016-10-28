using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class CloudSrv
    {
        public int id { get; set; }
        public string sernum { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string email { get; set; }
        public string rec_by { get; set; }
        public string rec_date { get; set; }
    }
}
