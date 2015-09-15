using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using SN_Net.Subform;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.MiscClass
{
    public class DataResource
    {
        public List<Istab> LIST_AREA;
        public List<Istab> LIST_VEREXT;
        public List<Istab> LIST_HOWKNOWN;
        public List<Istab> LIST_BUSITYP;
        public List<Istab> LIST_PROBLEM_CODE;
        public List<Dealer> LIST_DEALER;

        public DataResource()
        {
            this.LIST_AREA = this.getIstabDataFromServer(Istab.TABTYP.AREA);
            this.LIST_VEREXT = this.getIstabDataFromServer(Istab.TABTYP.VEREXT);
            this.LIST_HOWKNOWN = this.getIstabDataFromServer(Istab.TABTYP.HOWKNOWN);
            this.LIST_BUSITYP = this.getIstabDataFromServer(Istab.TABTYP.BUSITYP);
            this.LIST_PROBLEM_CODE = this.getIstabDataFromServer(Istab.TABTYP.PROBLEM_CODE);
            this.LIST_DEALER = this.getDealerDataFromServer();
        }

        private List<Istab> getIstabDataFromServer(Istab.TABTYP tabtyp)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + tabtyp.ToTabtypString() + "&sort=typcod");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.istab.Count > 0)
                {
                    return sr.istab;
                }
                else
                {
                    return new List<Istab>();
                }
            }
            else
            {
                return new List<Istab>();
            }
        }

        private List<Dealer> getDealerDataFromServer()
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "dealer/get_list");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                if (sr.dealer.Count > 0)
                {
                    return sr.dealer;
                }
                else
                {
                    return new List<Dealer>();
                }
            }
            else
            {
                return new List<Dealer>();
            }
        }

        public DataResource Refresh()
        {
            this.LIST_AREA = this.getIstabDataFromServer(Istab.TABTYP.AREA);
            this.LIST_VEREXT = this.getIstabDataFromServer(Istab.TABTYP.VEREXT);
            this.LIST_HOWKNOWN = this.getIstabDataFromServer(Istab.TABTYP.HOWKNOWN);
            this.LIST_BUSITYP = this.getIstabDataFromServer(Istab.TABTYP.BUSITYP);
            this.LIST_PROBLEM_CODE = this.getIstabDataFromServer(Istab.TABTYP.PROBLEM_CODE);
            this.LIST_DEALER = this.getDealerDataFromServer();
            return this;
        }
    }
}
