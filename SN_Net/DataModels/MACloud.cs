using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SN_Net.DataModels
{
    public class MACloud
    {
        public int SERIAL_ID { get; set; }
        public string SERNUM { get; set; }
        //public string OLDCOD { get; set; }
        //public string VERSION { get; set; }
        public string COMPNAM { get; set; }
        public string CONTACT { get; set; }
        //public string AREA { get; set; }
        //public string DEALER { get; set; }
        public string TELNUM { get; set; }
        //public string BUSITYP { get; set; }
        //public string BUSIDES { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public int REMAINING_DAYS
        {
            get
            {
                //Console.WriteLine(" .. > end date : " + END_DATE.ToString());
                //Console.WriteLine(" .. > now : " + DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
                int remaining_days = (int)(END_DATE - DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).TotalDays;
                return (remaining_days > 0 ? remaining_days : 0);
            }
        }
        public string EMAIL { get; set; }

        public string _START_DATE
        {
            get
            {
                return this.START_DATE.ToString("dd/MM/yy", CultureInfo.GetCultureInfo("th-TH"));
            }
        }

        public string _END_DATE
        {
            get
            {
                return this.END_DATE.ToString("dd/MM/yy", CultureInfo.GetCultureInfo("th-TH"));
            }
        }
    }
}