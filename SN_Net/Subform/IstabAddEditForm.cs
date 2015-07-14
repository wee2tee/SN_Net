using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class IstabAddEditForm : Form
    {
        private string tabtyp;
        public enum TABTYP
        {
            AREA,
            BUSITYP,
            HOWKNOWN,
            PURCHASE_FROM,
            VEREXT
        }

        public IstabAddEditForm(TABTYP tabtyp)
        {
            InitializeComponent();
            this.setTabTyp(tabtyp);
        }

        private void setTabTyp(TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case TABTYP.AREA:
                    this.tabtyp = "01";
                    break;
                case TABTYP.BUSITYP:
                    this.tabtyp = "02";
                    break;
                case TABTYP.HOWKNOWN:
                    this.tabtyp = "03";
                    break;
                case TABTYP.PURCHASE_FROM:
                    this.tabtyp = "04";
                    break;
                case TABTYP.VEREXT:
                    this.tabtyp = "05";
                    break;
                default:
                    this.tabtyp = "00";
                    break;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string json_data = "{\"tabtyp\":\"" + this.tabtyp + "\",";
            json_data += "\"typcod\":\"" + this.txtTypcod.Text + "\",";
            json_data += "\"typdes\":\"" + this.txtTypdes.Text + "\"}";
            
            Console.WriteLine(json_data);
            CRUDResult post = ApiActions.POST(ApiConfig.API_MAIN_URL + "istab/Create", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
            
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }
    }
}
