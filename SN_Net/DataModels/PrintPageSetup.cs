using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class PrintPageSetup
    {
        public int id { get; set; }
        public string page_name { get; set; }
        public float margin_top { get; set; }
        public float margin_right { get; set; }
        public float margin_bottom { get; set; }
        public float margin_left { get; set; }
        public string font_name { get; set; }
        public float font_size { get; set; }
        public string font_style { get; set; }
        public string reserve { get; set; }
    }
}
