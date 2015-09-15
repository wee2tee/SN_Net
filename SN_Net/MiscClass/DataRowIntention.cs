using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public class DataRowIntention
    {
        public enum TO_DO
        {
            READ,
            ADD,
            EDIT,
            DELETE
        }

        public TO_DO to_do { get; set; }

        public DataRowIntention(TO_DO to_do)
        {
            this.to_do = to_do;
        }
    }
}
