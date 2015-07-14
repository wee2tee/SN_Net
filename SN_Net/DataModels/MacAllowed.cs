using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SN_Net.DataModels
{
    public class MacAllowed
    {
        public int id { get; set; }
        public string mac_address { get; set; }
        public string create_by { get; set; }
        public string create_at { get; set; }
    }

}
