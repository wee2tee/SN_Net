using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SN_Net.DataModels
{
    public class RegisterData
    {

        //[JsonProperty("id")]
        public int id { get; set; }

        //[JsonProperty("sn")]
        public string sn { get; set; }

        //[JsonProperty("comp_prenam")]
        public string comp_prenam { get; set; }

        //[JsonProperty("comp_name")]
        public string comp_name { get; set; }

        //[JsonProperty("comp_addr")]
        public string comp_addr { get; set; }

        //[JsonProperty("comp_addr1")]
        public string comp_addr1 { get; set; }

        //[JsonProperty("comp_addr2")]
        public string comp_addr2 { get; set; }

        //[JsonProperty("comp_addr3")]
        public string comp_addr3 { get; set; }

        //[JsonProperty("comp_zipcod")]
        public string comp_zipcod { get; set; }

        //[JsonProperty("comp_email")]
        public string comp_email { get; set; }

        //[JsonProperty("comp_tel")]
        public string comp_tel { get; set; }

        //[JsonProperty("comp_fax")]
        public string comp_fax { get; set; }

        //[JsonProperty("comp_bus_type")]
        public string comp_bus_type { get; set; }

        //[JsonProperty("comp_bus_desc")]
        public string comp_bus_desc { get; set; }

        //[JsonProperty("comp_prod_type")]
        public string comp_prod_type { get; set; }

        //[JsonProperty("purchase_from")]
        public string purchase_from { get; set; }

        //[JsonProperty("purchase_from_desc")]
        public string purchase_from_desc { get; set; }

        //[JsonProperty("cont_name")]
        public string cont_name { get; set; }

        //[JsonProperty("cont_position")]
        public string cont_position { get; set; }

        //[JsonProperty("cont_email")]
        public string cont_email { get; set; }

        //[JsonProperty("cont_tel")]
        public string cont_tel { get; set; }

        //[JsonProperty("reg_time")]
        public string reg_time { get; set; }

        //[JsonProperty("recorded")]
        public string recorded { get; set; }

        //[JsonProperty("rec_time")]
        public string rec_time { get; set; }

        //[JsonProperty("exported")]
        public string exported { get; set; }

        //[JsonProperty("exported_file")]
        public string exported_file { get; set; }

        //[JsonProperty("reserve2")]
        public string reserve2 { get; set; }

        //extended field for recieve registered date
        public string reg_date { get; set; }
    }
}
