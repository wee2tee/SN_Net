using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class Serial
    {
        public int id { get; set; }
        public string sernum { get; set; }
        public string oldnum { get; set; }
        public string version { get; set; }
        public string contact { get; set; }
        public string position { get; set; }
        public string prenam { get; set; }
        public string compnam { get; set; }
        public string addr01 { get; set; }
        public string addr02 { get; set; }
        public string addr03 { get; set; }
        public string zipcod { get; set; }
        public string telnum { get; set; }
        public string faxnum { get; set; }
        public string busityp { get; set; }
        public string busides { get; set; }
        public string purdat { get; set; }
        public string expdat { get; set; }
        public string howknown { get; set; }
        public string area { get; set; }
        public string branch { get; set; }
        public string manual { get; set; }
        public string upfree { get; set; }
        public string refnum { get; set; }
        public string remark { get; set; }
        public string chgdat { get; set; }
        public string verext { get; set; }
        public string verextdat { get; set; }
        //public int users_id { get; set; }     <- Disable ไว้ชั่วคราว เนื่องจากใน DB ยังไม่ได้ update field users_id
        public string users_name { get; set; }
        //public int dealer_id { get; set; }    <- Disable ไว้ชั่วคราว เนื่องจากใน DB ยังไม่ได้ update field dealer_id
        public string dealer_dealer { get; set; }
    }
}
