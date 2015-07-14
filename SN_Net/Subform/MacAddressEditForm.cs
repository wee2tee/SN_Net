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

            CRUDResult post = ApiActions.POST(ApiConfig.API_MAIN_URL + "macallowed/update", json_data);
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

            /*
            string check_exist = ApiActions.GET(ApiConfig.API_SITE + "macallowed/check_exist?id=" + this.editing_mac_id + "&mac_address=" + this.txtMacAddress.Text);
            MacAllowed mac_exist = JsonConvert.DeserializeObject<MacAllowed>(check_exist);
            Console.WriteLine(mac_exist.id.ToString());
            if (mac_exist.id > 0)
            {
                //Console.WriteLine("is exist");
                MessageBox.Show("MAC Address " + this.txtMacAddress.Text + " นี้มีอยู่แล้วในระบบ", "MAC Address editing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Console.WriteLine("is not exist");
                string json_data = "{\"mac_address\":\"" + this.txtMacAddress.Text + "\",";
                json_data += "\"create_by\":\"" + this.G.loged_in_user_name + "\"}";
                Console.WriteLine(json_data);

                string result = ApiActions.PATCH(ApiConfig.API_SITE + "macallowed/" + this.editing_mac_id.ToString(), json_data);
                MacAllowed res = JsonConvert.DeserializeObject<MacAllowed>(result);

                if (res.id == this.editing_mac_id && res.mac_address == this.txtMacAddress.Text)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("MAC Address : " + this.txtMacAddress.Text + " นี้มีอยู่แล้วในระบบ", "MAC Address editing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */
        }

        private void txtMacAddress_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    this.submitChangeMacAddress();
                    break;

                case 27:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;

                default:
                    break;
            }
        }
    }
}
