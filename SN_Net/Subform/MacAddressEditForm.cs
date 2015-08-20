using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.DataModels;
using Newtonsoft.Json;
using WebAPI;
using WebAPI.ApiResult;

namespace SN_Net.Subform
{
    public partial class MacAddressEditForm : Form
    {
        const int SAVE_SUCCESS = 9;
        const int SAVE_FAILED = 0;
        const int SAVE_FAILED_EXIST = 1;

        public GlobalVar G;
        public int editing_mac_id;

        public MacAddressEditForm()
        {
            InitializeComponent();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void MacAddressEditForm_Load(object sender, EventArgs e)
        {
            this.lblID.Text = this.editing_mac_id.ToString();
        }

        private void btnSubmitChangeMacAddress_Click(object sender, EventArgs e)
        {
            this.submitChangeMacAddress();
        }

        private void submitChangeMacAddress()
        {
            string json_data = "{\"id\": " + this.editing_mac_id.ToString() + ",";
            json_data += "\"mac_address\":\"" + this.txtMacAddress.Text.cleanString() + "\",";
            json_data += "\"create_by\":\"" + this.G.loged_in_user_name + "\"}";

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "macallowed/update", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
            if (post.result)
            {
                switch (sr.result)
                {
                    case ServerResult.SERVER_RESULT_SUCCESS:
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;

                    default:
                        MessageBox.Show(sr.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show(sr.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMacAddress_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    this.submitChangeMacAddress();
                    break;

                default:
                    break;
            }
        }
    }
}
