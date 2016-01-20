using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class SerialPassword
    {
        public int id { get; set; }
        public string sernum { get; set; }
        public string pass_word { get; set; }
        public string rec_by { get; set; }
        public string rec_date { get; set; }
    }
}
