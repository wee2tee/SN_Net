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
        public int id;

        public IstabAddEditForm(Istab.TABTYP tabtyp)
        {
            InitializeComponent();
            this.setTabTyp(tabtyp);
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
            EnterKeyManager.Active(this);
            this.txtTypcod.Focus();
        }

        private void setTabTyp(Istab.TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case Istab.TABTYP.AREA:
                    this.tabtyp = "01";
                    break;
                case Istab.TABTYP.BUSITYP:
                    this.tabtyp = "02";
                    break;
                case Istab.TABTYP.HOWKNOWN:
                    this.tabtyp = "03";
                    break;
                case Istab.TABTYP.PURCHASE_FROM:
                    this.tabtyp = "04";
                    break;
                case Istab.TABTYP.VEREXT:
                    this.tabtyp = "05";
                    break;
                default:
                    this.tabtyp = "00";
                    break;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtTypcod.Text.Length > 0)
            {
                string json_data = "{\"tabtyp\":\"" + this.tabtyp + "\",";
                json_data += "\"typcod\":\"" + this.txtTypcod.Text.cleanString() + "\",";
                json_data += "\"abbreviate_en\":\"" + this.txtAbbreviate_en.Text.cleanString() + "\",";
                json_data += "\"abbreviate_th\":\"" + this.txtAbbreviate_th.Text.cleanString() + "\",";
                json_data += "\"typdes_en\":\"" + this.txtTypdes_en.Text.cleanString() + "\",";
                json_data += "\"typdes_th\":\"" + this.txtTypdes_th.Text.cleanString() + "\"}";

                Console.WriteLine(json_data);
                CRUDResult post = ApiActions.POST(ApiConfig.API_MAIN_URL + "istab/create", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                Console.WriteLine(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    //this.id = Convert.ToInt32(sr.message);
                    //Console.WriteLine("Last insert id is " + this.id.ToString());

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }

            }
            else
            {
                MessageAlert.Show(StringResource.PLEASE_FILL_CODE, "Warning", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.txtTypcod.Focus();
            }
        }
    }
}
