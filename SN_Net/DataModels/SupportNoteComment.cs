using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class SupportNoteComment
    {
        public int id { get; set; }
        public string date { get; set; }
        public int note_id { get; set; }
        public int type { get; set; }
        public string description { get; set; }
        public string file_path { get; set; }
        public string rec_by { get; set; }

        public enum COMMENT_TYPE : int
        {
            COMMENT = 1,
            COMPLAIN = 2
        }
    }
}
