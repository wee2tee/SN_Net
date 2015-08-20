using Newtonsoft.Json;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebAPI;
using WebAPI.ApiResult;

namespace SN_Net.Subform
{
    public partial class MacAddressList : Form
    {
        const int CREATE_SUCCESS = 9;
        const int CREATE_FAILED = 0;
        const int CREATE_FAILED_EXIST = 1;

        public GlobalVar G;
        public List<MacAllowed> mac_data;

        public MacAddressList()
        {
            InitializeComponent();
        }

        private void MacAddressList_Load(object sender, EventArgs e)
        {
            this.toolStripStatusCurrentMac.Text = "MAC Address ของเครื่องนี้ : " + this.G.current_mac_address;
            this.loadMacAddressData();
        }

        private void btnAddCurrentMac_Click(object sender, EventArgs e)
        {
            this.addCurrentMacAddress();
        }

        private void btnSubmitMacAddress_Click(object sender, EventArgs e)
        {
            if (this.txtMacAddress.Text.Length > 0)
            {
                this.addMacAddress(this.txtMacAddress.Text);
            }
            else
            {
                MessageAlert.Show("กรุณาป้อน MAC Address");
            }
        }

        private void txtMacAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.addMacAddress(this.txtMacAddress.Text);
            }
        }

        private void loadMacAddressData(int mac_id = 0)
        {
            if (this.mac_data == null)
            {
                CRUDResult res = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "macallowed/get_all");

                if (res.result)
                {
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(res.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        this.mac_data = sr.macallowed;
                    }
                }
            }

            // Clear old data
            this.dgvMacAddress.Columns.Clear();
            this.dgvMacAddress.Rows.Clear();

            // Create data column
            DataGridViewTextBoxColumn text_col1 = new DataGridViewTextBoxColumn();
            int c1 = this.dgvMacAddress.Columns.Add(text_col1);
            this.dgvMacAddress.Columns[c1].HeaderText = "ID.";
            this.dgvMacAddress.Columns[c1].Width = 40;
            this.dgvMacAddress.Columns[c1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
            int c2 = this.dgvMacAddress.Columns.Add(text_col2);
            this.dgvMacAddress.Columns[c2].HeaderText = "Mac Address";
            this.dgvMacAddress.Columns[c2].Width = 150;
            this.dgvMacAddress.Columns[c2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            int c3 = this.dgvMacAddress.Columns.Add(text_col3);
            this.dgvMacAddress.Columns[c3].HeaderText = "เพิ่ม/แก้ไข โดย";
            this.dgvMacAddress.Columns[c3].Width = 120;
            this.dgvMacAddress.Columns[c3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            DataGridViewTextBoxColumn text_col4 = new DataGridViewTextBoxColumn();
            int c4 = this.dgvMacAddress.Columns.Add(text_col4);
            this.dgvMacAddress.Columns[c4].HeaderText = "เพิ่ม/แก้ไข ล่าสุด";
            this.dgvMacAddress.Columns[c4].Width = 130;
            this.dgvMacAddress.Columns[c4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (MacAllowed mac in this.mac_data)
            {
                int r = this.dgvMacAddress.Rows.Add();
                this.dgvMacAddress.Rows[r].Tag = (int)mac.id;

                this.dgvMacAddress.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvMacAddress.Rows[r].Cells[0].Value = mac.id;
                this.dgvMacAddress.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgvMacAddress.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvMacAddress.Rows[r].Cells[1].Value = mac.mac_address;

                this.dgvMacAddress.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvMacAddress.Rows[r].Cells[2].Value = mac.create_by;

                this.dgvMacAddress.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvMacAddress.Rows[r].Cells[3].Value = mac.create_at;
            }

            // Set selected row
            if (mac_id > 0)
            {
                //this.dgvMacAddress.Rows[mac_id].Selected = true;
                foreach (DataGridViewRow row in this.dgvMacAddress.Rows)
                {
                    if ((int)row.Cells[0].Value == mac_id)
                    {
                        row.Cells[0].Selected = true;
                    }
                }
            }

            this.mac_data = null;
        }

        private void addMacAddress(string mac_address)
        {
            string json_data = "{\"mac_address\":\"" + mac_address.cleanString() + "\",";
            json_data += "\"create_by\":\"" + this.G.loged_in_user_name.cleanString() + "\"}";

            CRUDResult res = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "macallowed/create", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(res.data);
            switch (sr.result)
	        {
                case ServerResult.SERVER_RESULT_SUCCESS:
                    this.loadMacAddressData();
                    this.txtMacAddress.Text = "";
                    break;

                default:
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    break;
	        }
        }

        private void addCurrentMacAddress()
        {
            this.addMacAddress(this.G.current_mac_address);
        }

        private void dgvMacAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.editMacAddress(sender, e);
        }

        // Data grid view context menu
        private void dgvMacAddress_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = this.dgvMacAddress.HitTest(e.X, e.Y).RowIndex;
                this.dgvMacAddress.Rows[currentMouseOverRow].Selected = true;

                ContextMenu m = new ContextMenu();
                MenuItem mnu_edit = new MenuItem("แก้ไข");
                mnu_edit.Tag = (int)this.dgvMacAddress.Rows[currentMouseOverRow].Cells[0].Value;
                mnu_edit.Click += this.editMacAddress;
                m.MenuItems.Add(mnu_edit);

                MenuItem mnu_delete = new MenuItem("ลบ");
                mnu_delete.Tag = (int)this.dgvMacAddress.Rows[currentMouseOverRow].Cells[0].Value;
                mnu_delete.Click += this.deleteMacAddress;
                m.MenuItems.Add(mnu_delete);

                //// Adding some phrase at the bottom of context menu
                //if (currentMouseOverRow >= 0)
                //{
                //    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                //}

                m.Show(this.dgvMacAddress, new Point(e.X, e.Y));
            }
        }

        private void dgvMacAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Alt)
            {
                int row_index = this.dgvMacAddress.CurrentCell.RowIndex;
                int mac_id = (int)this.dgvMacAddress.Rows[row_index].Tag;
                this.showEditForm(mac_id);
            }
            else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Alt)
            {
                int row_index = this.dgvMacAddress.CurrentCell.RowIndex;
                int mac_id = (int)this.dgvMacAddress.Rows[row_index].Tag;
                this.confirmDelete(mac_id);
            }
        }

        private void editMacAddress(object sender, DataGridViewCellEventArgs e)
        {
            int row_index = e.RowIndex;
            int mac_id = (int)this.dgvMacAddress.Rows[row_index].Tag;
            this.showEditForm(mac_id);
        }

        private void editMacAddress(object sender, EventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            int mac_id = (int)mnu.Tag;
            this.showEditForm(mac_id);
        }

        private void showEditForm(int mac_id)
        {
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "macallowed/get_at&mac_id=" + mac_id.ToString());

            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (get.result)
            {
                MacAddressEditForm editForm = new MacAddressEditForm();
                editForm.G = this.G;

                int id = sr.macallowed.First<MacAllowed>().id;
                string mac_address = sr.macallowed.First<MacAllowed>().mac_address;
                editForm.editing_mac_id = id;
                editForm.txtMacAddress.Text = mac_address;
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    this.loadMacAddressData(mac_id);
                }
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void deleteMacAddress(object sender, EventArgs e)
        {
                MenuItem mnu = sender as MenuItem;
                int id = (int)mnu.Tag;
                this.confirmDelete(id);
        }

        private void confirmDelete(int id)
        {
            if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "macallowed/delete&id=" + id.ToString());
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    this.loadMacAddressData();
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }
    }
}
