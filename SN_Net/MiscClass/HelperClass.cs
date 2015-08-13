using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;

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

        //public static Array AsArray(this Serial_list serial_list)
        //{
        //    string[] array = new string[10];

        //    array[0] = serial_list.ID.ToString();
        //    array[1] = serial_list.SERNUM;
        //    array[2] = serial_list.OLDCOD;
        //    array[3] = serial_list.VERSION;
        //    array[4] = serial_list.COMPNAM;
        //    array[5] = serial_list.CONTACT;
        //    array[6] = serial_list.DEALER;
        //    array[7] = serial_list.TELNUM;
        //    array[8] = serial_list.BUSITYP;
        //    array[9] = serial_list.BUSIDES;
            
        //    return array;
        //}
    }
}
