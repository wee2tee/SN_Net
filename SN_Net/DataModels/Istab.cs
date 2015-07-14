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
            BUSITYP,
            HOWKNOWN,
            PURCHASE_FROM,
            VEREXT
        }

        public static Istab getIstabByTypcod(TABTYP tabtyp, string typcod)
        {
            string tab_typ = string.Empty;
            switch (tabtyp)
            {
                case TABTYP.AREA:
                    tab_typ = "01";
                    break;
                case TABTYP.BUSITYP:
                    tab_typ = "02";
                    break;
                case TABTYP.HOWKNOWN:
                    tab_typ = "03";
                    break;
                case TABTYP.PURCHASE_FROM:
                    tab_typ = "04";
                    break;
                case TABTYP.VEREXT:
                    tab_typ = "05";
                    break;
                default:
                    tab_typ = "00";
                    break;
            }

            Istab istab = new Istab();

            CRUDResult get = ApiActions.GET(ApiConfig.API_MAIN_URL + "istab/get_by_typcod&tabtyp=" + tab_typ + "&typcod=" + typcod);
            Console.WriteLine(get.data);
            
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.istab != null)
                {
                    istab = sr.istab.First<Istab>();
                }
            }

            return istab;
        }
    }
}
