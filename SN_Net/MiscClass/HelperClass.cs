using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;
using System.IO;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public static class HelperClass
    {
        public static string ToTabtypString(this Istab.TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case Istab.TABTYP.AREA:
                    return "01";
                case Istab.TABTYP.VEREXT:
                    return "02";
                case Istab.TABTYP.HOWKNOWN:
                    return "03";
                case Istab.TABTYP.BUSITYP:
                    return "04";
                case Istab.TABTYP.PROBLEM_CODE:
                    return "05";
                default:
                    return "00";
            }
        }

        public static void KeepLog(this Form form, string msg)
        {
            using (StreamWriter file = new StreamWriter("SN_Log.txt", true))
            {
                file.WriteLine(msg);
            }
        }
    }
}
