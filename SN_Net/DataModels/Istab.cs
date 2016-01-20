using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI;
using WebAPI.ApiResult;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using Newtonsoft.Json;

namespace SN_Net.DataModels
{
    public class Istab
    {
        public int id { set; get; }
        public string tabtyp { set; get; }
        public string typcod { set; get; }
        public string abbreviate_en { set; get; }
        public string abbreviate_th { set; get; }
        public string typdes_en { set; get; }
        public string typdes_th { set; get; }

        public enum TABTYP
        {
            AREA,
            VEREXT,
            HOWKNOWN,
            BUSITYP,
            PROBLEM_CODE,
            ABSENT_CAUSE,
            SERVICE_CASE,
            USER_GROUP
        }

        public static string getTabtypString(Istab.TABTYP tabtyp)
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
                case Istab.TABTYP.ABSENT_CAUSE:
                    return "06";
                case Istab.TABTYP.SERVICE_CASE:
                    return "07";
                case Istab.TABTYP.USER_GROUP:
                    return "08";
                default:
                    return "00";
            }
        }

        public static string getTabtypTitle(Istab.TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case TABTYP.AREA:
                    return "Sales Area";
                case TABTYP.VEREXT:
                    return "Version Extension";
                case TABTYP.HOWKNOWN:
                    return "How to Know";
                case TABTYP.BUSITYP:
                    return "Business Type";
                case TABTYP.PROBLEM_CODE:
                    return "Problem Code";
                case TABTYP.ABSENT_CAUSE:
                    return "Absent Cause";
                case TABTYP.SERVICE_CASE:
                    return "Service Case";
                case TABTYP.USER_GROUP:
                    return "User Group";
                default:
                    return "Istab";
            }
        }
    }
}
