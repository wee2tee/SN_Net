using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogMacAddressAllowed : Form
    {
        private MainForm main_form;
        private BindingList<macallowedVM> mac_list;
        
        public DialogMacAddressAllowed(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void DialogMacAddressAllowed_Load(object sender, EventArgs e)
        {
            this.mac_list = new BindingList<macallowedVM>(GetMacList().ToViewModel());
            this.dgv.DataSource = this.mac_list;
        }

        public static List<mac_allowed> GetMacList()
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.mac_allowed.OrderBy(m => m.credat).ToList();
            }
        }

        private void btnAddMac_Click(object sender, EventArgs e)
        {
            if(this.txtMacAddress.Text.Trim().Length == 0)
            {
                MessageAlert.Show("Please specify mac address", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtMacAddress.Focus();
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                if (sn.mac_allowed.Where(mac => mac.mac_address == this.txtMacAddress.Text.Trim()).FirstOrDefault() != null)
                {
                    MessageAlert.Show("This MAC Address is already exist");
                    return;
                }

                mac_allowed m = new mac_allowed
                {
                    mac_address = this.txtMacAddress.Text.Trim(),
                    creby_id = this.main_form.loged_in_user.id,
                    credat = DateTime.Now,
                };
                sn.mac_allowed.Add(m);
                sn.SaveChanges();

                this.mac_list = null;
                this.mac_list = new BindingList<macallowedVM>(GetMacList().ToViewModel());
                this.dgv.DataSource = this.mac_list;

            }
        }

        private void btnAddCurrentMac_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if(sn.mac_allowed.Where(mac => mac.mac_address == this.main_form.mac_address).FirstOrDefault() != null)
                    {
                        MessageAlert.Show("This MAC Address is already exist");
                        return;
                    }

                    mac_allowed m = new mac_allowed
                    {
                        mac_address = this.main_form.mac_address,
                        creby_id = this.main_form.loged_in_user.id,
                        credat = DateTime.Now,
                    };
                    sn.mac_allowed.Add(m);
                    sn.SaveChanges();

                    this.mac_list = null;
                    this.mac_list = new BindingList<macallowedVM>(GetMacList().ToViewModel());
                    this.dgv.DataSource = this.mac_list;
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.InnerException.InnerException.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if(row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_mac_address.Name].Selected = true;
                    ContextMenu cm = new ContextMenu();
                    MenuItem m_delete = new MenuItem("ลบ");
                    m_delete.Click += delegate
                    {
                        ((XDatagrid)sender).Rows[row_index].DrawDeletingRowOverlay();

                        if(MessageAlert.Show("ลบรายการที่เลือก ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                        {
                            using (snEntities sn = DBX.DataSet())
                            {
                                try
                                {
                                    var mac = (mac_allowed)((XDatagrid)sender).Rows[row_index].Cells[this.col_mac_allowed.Name].Value;
                                    var mac_to_remove = sn.mac_allowed.Find(mac.id);

                                    if (mac_to_remove != null)
                                    {
                                        sn.mac_allowed.Remove(mac_to_remove);
                                        sn.SaveChanges();
                                    }

                                    this.mac_list = new BindingList<macallowedVM>(GetMacList().ToViewModel());
                                    this.dgv.DataSource = this.mac_list;
                                }
                                catch (Exception ex)
                                {
                                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK,
                                        MessageAlertIcons.ERROR);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            ((XDatagrid)sender).Rows[row_index].ClearDeletingRowOverlay();
                        }
                    };
                    cm.MenuItems.Add(m_delete);

                    cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
                }
            }
        }
    }
}
