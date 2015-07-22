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
                default:
                    return "Istab";
            }
        }
        //public static Istab getIstabByTypcod(TABTYP tabtyp, string typcod)
        //{
        //    string tab_typ = string.Empty;
        //    switch (tabtyp)
        //    {
        //        case TABTYP.AREA:
        //            tab_typ = "01";
        //            break;
        //        case TABTYP.BUSITYP:
        //            tab_typ = "02";
        //            break;
        //        case TABTYP.HOWKNOWN:
        //            tab_typ = "03";
        //            break;
        //        case TABTYP.PURCHASE_FROM:
        //            tab_typ = "04";
        //            break;
        //        case TABTYP.VEREXT:
        //            tab_typ = "05";
        //            break;
        //        default:
        //            tab_typ = "00";
        //            break;
        //    }

        //    Istab istab = new Istab();

        //    CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "istab/get_by_typcod&tabtyp=" + tab_typ + "&typcod=" + typcod);
        //    Console.WriteLine(get.data);
            
        //    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            
        //    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
        //    {
        //        if (sr.istab != null)
        //        {
        //            istab = sr.istab.First<Istab>();
        //        }
        //    }

        //    return istab;
        //}
    }
}
