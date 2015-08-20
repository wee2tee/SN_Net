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
        private Istab.TABTYP tabtyp;
        public Istab istab;
        private FORM_MODE mode;

        public enum FORM_MODE
        {
            ADD,
            EDIT
        }
        /// <summary>
        /// Form for Add/Edit istab data
        /// </summary>
        /// <param name="mode">Specify mode (add or edit)</param>
        /// <param name="tabtyp">Tabtyp value</param>
        /// <param name="istab_data">istab data to edit(for edit mode only)</param>
        public IstabAddEditForm(FORM_MODE mode, Istab.TABTYP tabtyp, Istab istab_data = null)
        {
            InitializeComponent();
            this.istab = istab_data;
            this.tabtyp = tabtyp;

            EscapeKeyToCloseDialog.ActiveEscToClose(this);
            EnterKeyManager.Active(this);

            this.mode = mode;

            this.initializeForm();
        }

        private void IstabAddEditForm_Load(object sender, EventArgs e)
        {
            if (this.mode == FORM_MODE.ADD)
            {
                this.Text = "Add data : " + Istab.getTabtypTitle(this.tabtyp);
                this.txtTypcod.Focus();
            }
            else
            {
                this.Text = "Edit data : " + Istab.getTabtypTitle(this.tabtyp);
                this.txtTypcod.Enabled = false;
                this.txtAbbreviate_th.Focus();

                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_by_id&id=" + this.istab.id);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.istab != null)
                    {
                        this.istab = sr.istab.First<Istab>();
                        this.txtTypcod.Text = this.istab.typcod;
                        this.txtAbbreviate_en.Text = this.istab.abbreviate_en;
                        this.txtAbbreviate_th.Text = this.istab.abbreviate_th;
                        this.txtTypdes_en.Text = this.istab.typdes_en;
                        this.txtTypdes_th.Text = this.istab.typdes_th;
                    }
                }
            }
        }

        private void initializeForm()
        {
            switch (this.tabtyp)
            {
                case Istab.TABTYP.AREA:
                    this.txtTypcod.MaxLength = 10;
                    break;
                case Istab.TABTYP.VEREXT:
                    this.txtTypcod.MaxLength = 1;
                    break;
                case Istab.TABTYP.HOWKNOWN:
                    this.txtTypcod.MaxLength = 4;
                    break;
                case Istab.TABTYP.BUSITYP:
                    this.txtTypcod.MaxLength = 4;
                    break;
                case Istab.TABTYP.PROBLEM_CODE:
                    this.txtTypcod.MaxLength = 2;
                    break;
                default:
                    break;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtTypcod.Text.Length > 0)
            {
                CRUDResult post;
                if (this.mode == FORM_MODE.ADD) // Add
                {
                    Console.WriteLine("current tabtyp = " + Istab.getTabtypString(this.tabtyp));
                    string json_data = "{\"tabtyp\":\"" + Istab.getTabtypString(this.tabtyp) + "\",";
                    json_data += "\"typcod\":\"" + this.txtTypcod.Text.cleanString() + "\",";
                    json_data += "\"abbreviate_en\":\"" + this.txtAbbreviate_en.Text.cleanString() + "\",";
                    json_data += "\"abbreviate_th\":\"" + this.txtAbbreviate_th.Text.cleanString() + "\",";
                    json_data += "\"typdes_en\":\"" + this.txtTypdes_en.Text.cleanString() + "\",";
                    json_data += "\"typdes_th\":\"" + this.txtTypdes_th.Text.cleanString() + "\"}";

                    Console.WriteLine(json_data);
                    post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/create", json_data);
                }
                else // Edit
                {
                    string json_data = "{\"id\":" + this.istab.id.ToString() + ",";
                    json_data += "\"tabtyp\":\"" + this.istab.tabtyp + "\",";
                    json_data += "\"typcod\":\"" + this.istab.typcod + "\",";
                    json_data += "\"abbreviate_en\":\"" + this.txtAbbreviate_en.Text.cleanString() + "\",";
                    json_data += "\"abbreviate_th\":\"" + this.txtAbbreviate_th.Text.cleanString() + "\",";
                    json_data += "\"typdes_en\":\"" + this.txtTypdes_en.Text.cleanString() + "\",";
                    json_data += "\"typdes_th\":\"" + this.txtTypdes_th.Text.cleanString() + "\"}";

                    Console.WriteLine(json_data);
                    post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/submit_change", json_data);
                }
                
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                Console.WriteLine(post.data);
                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    if (sr.istab != null)
                    {
                        this.istab = sr.istab[0];
                    }
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
