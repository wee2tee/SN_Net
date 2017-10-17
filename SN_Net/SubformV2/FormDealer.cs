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
using System.Globalization;
using System.Drawing.Printing;

namespace SN_Net.Subform
{
    public partial class FormDealer : Form
    {
        private MainForm main_form;
        private dealer curr_dealer;
        private dealer tmp_dealer;
        private d_msg tmp_dmsg;
        private BindingList<d_msgVM> dmsg_list = new BindingList<d_msgVM>();
        private BindingList<serialItemVM> serial_list = new BindingList<serialItemVM>();
        private FORM_MODE form_mode;
        private DialogInquiryDealer.SORT_BY sort_by = DialogInquiryDealer.SORT_BY.DEALERCOD;
        private Control focused_control = null;
        private string inquiry_condtion = string.Empty;
        private string print_condition = string.Empty;

        public FormDealer(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void FormDealer_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            this.ResetFormState(FORM_MODE.READ);
            this.HideInlineForm();
            this.dgvDmsg.DataSource = this.dmsg_list;
            this.dgvSoldSn.DataSource = this.serial_list;
            this.btnFirst.PerformClick();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!(this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM))
            {
                if(MessageAlert.Show("ข้อมูลที่กำลังเพิ่ม/แก้ไข จะไม่ถูกบันทึก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }

            this.main_form.form_dealer = null;
            base.OnClosing(e);
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM, FORM_MODE.ADD, FORM_MODE.EDIT, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnFirst.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrev.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnNext.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnLast.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnItemF8.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnItemF7.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryAll.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryCondition.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryRest.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearch.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchArea.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchCode.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchContact.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchName.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrint.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrintBigEnv.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrintLabel2Col.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrintLabel3Col.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrintLittleEnv.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnReload.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            this.txtDealercod.SetControlState(new FORM_MODE[] { FORM_MODE.ADD }, this.form_mode);
            this.txtPrenam.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtCompnam.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr1.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr2.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr3.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtZipcod.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtTelnum.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtFaxnum.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtContact.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtPosition.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtBusides.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.brArea.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtRemark.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);

            this.dgvDmsg.SetControlState(new FORM_MODE[] { FORM_MODE.READ, FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnAddMsg.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEditMsg.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDeleteMsg.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
        }

        private void FillForm(dealer dealer_to_fill = null)
        {
            if (dealer_to_fill != null)
            {
                this.txtDealercod._Text = dealer_to_fill.dealercod;
                this.txtPrenam._Text = dealer_to_fill.prenam;
                this.txtCompnam._Text = dealer_to_fill.compnam;
                this.txtAddr1._Text = dealer_to_fill.addr01;
                this.txtAddr2._Text = dealer_to_fill.addr02;
                this.txtAddr3._Text = dealer_to_fill.addr03;
                this.txtZipcod._Text = dealer_to_fill.zipcod;
                this.txtTelnum._Text = dealer_to_fill.telnum;
                this.txtFaxnum._Text = dealer_to_fill.faxnum;
                this.txtContact._Text = dealer_to_fill.contact;
                this.txtPosition._Text = dealer_to_fill.position;
                this.txtBusides._Text = dealer_to_fill.busides;
                this.txtRemark._Text = dealer_to_fill.remark;
                this.brArea._Text = dealer_to_fill.istab != null ? dealer_to_fill.istab.typcod : string.Empty;
                this.lblArea.Text = dealer_to_fill.istab != null ? dealer_to_fill.istab.typdes_th : string.Empty;

                this.dmsg_list = null;
                this.dmsg_list = new BindingList<d_msgVM>(dealer_to_fill.d_msg.Where(d => d.flag == 0).OrderBy(d => d.date).ThenBy(d => d.time).ToViewModel());
                this.dgvDmsg.DataSource = this.dmsg_list;

                this.serial_list = null;
                this.serial_list = new BindingList<serialItemVM>(dealer_to_fill.serial.Where(d => d.flag == 0).OrderBy(s => s.purdat).ThenBy(s => s.sernum).ToSerialItemVM());
                this.dgvSoldSn.DataSource = this.serial_list;

                this.txtTelnum2._Text = dealer_to_fill.telnum;
                this.txtContact2._Text = dealer_to_fill.contact;
            }
            else
            {
                this.txtDealercod._Text = string.Empty;
                this.txtPrenam._Text = string.Empty;
                this.txtCompnam._Text = string.Empty;
                this.txtAddr1._Text = string.Empty;
                this.txtAddr2._Text = string.Empty;
                this.txtAddr3._Text = string.Empty;
                this.txtZipcod._Text = string.Empty;
                this.txtTelnum._Text = string.Empty;
                this.txtFaxnum._Text = string.Empty;
                this.txtContact._Text = string.Empty;
                this.txtPosition._Text = string.Empty;
                this.txtBusides._Text = string.Empty;
                this.txtRemark._Text = string.Empty;
                this.brArea._Text = string.Empty;
                this.lblArea.Text = string.Empty;

                ((BindingList<d_msg>)this.dgvDmsg.DataSource).Clear();
                ((BindingList<serialVM>)this.dgvSoldSn.DataSource).Clear();

                this.txtTelnum2._Text = string.Empty;
                this.txtContact2._Text = string.Empty;
            }
        }

        private void ShowInlineForm()
        {
            if (this.dgvDmsg.CurrentCell == null)
                return;

            this.tmp_dmsg = (d_msg)this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].Cells[this.col_d_msg.Name].Value;
            this.SetInlineControlPosition();
            this.inlineDate._SelectedDate = this.tmp_dmsg.date;
            this.inlineName._Text = this.tmp_dmsg.name;
            this.inlineDesc._Text = this.tmp_dmsg.description;
        }

        private void SetInlineControlPosition()
        {
            if (!(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM) || this.dgvDmsg.CurrentCell == null)
                return;

            int col_index = this.dgvDmsg.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_date.Name).First().Index;
            this.inlineDate.SetInlineControlPosition(this.dgvDmsg, this.dgvDmsg.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDmsg.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_name.Name).First().Index;
            this.inlineName.SetInlineControlPosition(this.dgvDmsg, this.dgvDmsg.CurrentCell.RowIndex, col_index);

            col_index = this.dgvDmsg.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_description.Name).First().Index;
            this.inlineDesc.SetInlineControlPosition(this.dgvDmsg, this.dgvDmsg.CurrentCell.RowIndex, col_index);
        }

        private void HideInlineForm()
        {
            this.inlineDate.SetBounds(-99999, 0, this.inlineDate.Width, this.inlineDate.Height);
            this.inlineName.SetBounds(-99999, 0, this.inlineName.Width, this.inlineName.Height);
            this.inlineDesc.SetBounds(-99999, 0, this.inlineDesc.Width, this.inlineDesc.Height);

            this.tmp_dmsg = null;
        }

        public static dealer GetDealer(int dealer_id)
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.dealer.Include("d_msg").Include("istab").Include("serial").Include("users").Include("users1").Where(d => d.flag == 0 && d.id == dealer_id).FirstOrDefault();
            }
        }

        private void dgvDmsg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if (row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_date.Name].Selected = true;
                    this.btnItemF8.PerformClick();
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("เพิ่ม <Alt + A>");
                m_add.Click += delegate
                {
                    this.btnItemF8.PerformClick();
                    this.btnAddMsg.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("แก้ไข <Alt + E>");
                m_edit.Click += delegate
                {
                    this.btnItemF8.PerformClick();
                    this.btnEditMsg.PerformClick();
                };
                m_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("ลบ <Alt + D>");
                m_delete.Click += delegate
                {
                    this.btnItemF8.PerformClick();
                    this.btnDeleteMsg.PerformClick();
                };
                m_delete.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_delete);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void btnAddMsg_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab != this.tabPage1)
                return;

            d_msgVM d = new d_msg
            {
                id = -1,
                date = DateTime.Now,
                name = string.Empty,
                description = string.Empty,
                dealer_id = this.curr_dealer.id,
                creby_id = this.main_form.loged_in_user.id,
                chgby_id = null,
                chgdat = null,
                flag = 0
            }.ToViewModel();

            ((BindingList<d_msgVM>)this.dgvDmsg.DataSource).Add(d);
            this.dgvDmsg.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == d.id).First().Cells[this.col_date.Name].Selected = true;
            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm();
            this.inlineDate.Focus();
        }

        private void btnEditMsg_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab != this.tabPage1)
                return;

            if (this.dgvDmsg.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm();
            this.inlineDate.Focus();
        }

        private void btnDeleteMsg_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab != this.tabPage1)
                return;

            if (this.dgvDmsg.CurrentCell == null)
                return;

            this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].DrawDeletingRowOverlay();
            if (MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                try
                {
                    int id = (int)this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].Cells[this.col_id.Name].Value;
                    using (snEntities sn = DBX.DataSet())
                    {
                        var msg_to_delete = sn.d_msg.Find(id);
                        if (msg_to_delete != null)
                        {
                            msg_to_delete.flag = msg_to_delete.id;
                            msg_to_delete.chgby_id = this.main_form.loged_in_user.id;
                            msg_to_delete.chgdat = DateTime.Now;

                            sn.SaveChanges();
                        }
                    }

                    this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                    ((BindingList<d_msgVM>)this.dgvDmsg.DataSource).Remove(((BindingList<d_msgVM>)this.dgvDmsg.DataSource).Where(i => i.id == id).First());
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
            else
            {
                this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].ClearDeletingRowOverlay();
            }
        }

        private void dgvDmsg_Resize(object sender, EventArgs e)
        {
            this.SetInlineControlPosition();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.tmp_dealer = new dealer
            {
                id = -1,
                dealercod = this.curr_dealer != null ? this.curr_dealer.dealercod : string.Empty,
                prenam = this.curr_dealer != null ? this.curr_dealer.prenam : string.Empty,
                compnam = this.curr_dealer != null ? this.curr_dealer.compnam : string.Empty,
                addr01 = this.curr_dealer != null ? this.curr_dealer.addr01 : string.Empty,
                addr02 = this.curr_dealer != null ? this.curr_dealer.addr02 : string.Empty,
                addr03 = this.curr_dealer != null ? this.curr_dealer.addr03 : string.Empty,
                zipcod = this.curr_dealer != null ? this.curr_dealer.zipcod : string.Empty,
                telnum = this.curr_dealer != null ? this.curr_dealer.telnum : string.Empty,
                faxnum = this.curr_dealer != null ? this.curr_dealer.faxnum : string.Empty,
                contact = this.curr_dealer != null ? this.curr_dealer.contact : string.Empty,
                position = this.curr_dealer != null ? this.curr_dealer.position : string.Empty,
                busides = this.curr_dealer != null ? this.curr_dealer.busides : string.Empty,
                area_id = this.curr_dealer != null ? this.curr_dealer.area_id : null,
                area = this.curr_dealer != null ? this.curr_dealer.area : string.Empty,
                remark = this.curr_dealer != null ? this.curr_dealer.remark : string.Empty,
                creby_id = this.main_form.loged_in_user.id,
                flag = 0
            };

            this.ResetFormState(FORM_MODE.ADD);
            this.FillForm(this.tmp_dealer);
            this.txtDealercod.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.curr_dealer == null)
                return;

            this.tmp_dealer = GetDealer(this.curr_dealer.id);

            if(this.tmp_dealer != null)
            {
                this.ResetFormState(FORM_MODE.EDIT);
                this.FillForm(this.tmp_dealer);
                this.txtPrenam.Focus();
            }
            else
            {
                MessageAlert.Show("ค้นหารหัส " + this.curr_dealer.dealercod + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.curr_dealer == null)
                return;

            if(MessageAlert.Show("ลบรหัส " + this.curr_dealer.dealercod + ", ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        if (sn.serial.Where(s => s.flag == 0 && s.dealer_id == this.curr_dealer.id).AsEnumerable().Count() > 0)
                        {
                            MessageAlert.Show("ไม่สามารถลบรหัส " + this.curr_dealer.dealercod + " เนื่องจากยังมี S/N ที่อ้างถึงอยู่", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }

                        sn.d_msg.Where(d => d.flag == 0 && d.dealer_id == this.curr_dealer.id).ToList().ForEach(d => { d.flag = d.id; d.chgby_id = this.main_form.loged_in_user.id; d.chgdat = DateTime.Now; });

                        sn.dealer.Where(d => d.flag == 0 && d.id == this.curr_dealer.id).ToList().ForEach(d => { d.flag = d.id; d.chgby_id = this.main_form.loged_in_user.id; d.chgdat = DateTime.Now; });

                        sn.SaveChanges();
                        this.btnNext.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.btnReload.PerformClick();
                return;
            }

            if(this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.tmp_dealer = null;
                if(this.curr_dealer != null)
                {
                    this.FillForm(this.curr_dealer);
                }
                else
                {
                    this.FillForm();
                }
                return;
            }
            
            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.HideInlineForm();
                if(this.form_mode == FORM_MODE.ADD_ITEM)
                {
                    ((BindingList<d_msgVM>)this.dgvDmsg.DataSource).Remove(((BindingList<d_msgVM>)this.dgvDmsg.DataSource).Where(i => i.id == -1).First());
                }
                this.ResetFormState(FORM_MODE.READ_ITEM);
                this.dgvDmsg.Refresh();
                this.dgvDmsg.Focus();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                    {
                        istab area = null;
                        if (this.tmp_dealer.area_id.HasValue)
                            area = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_AREA && i.id == this.tmp_dealer.area_id).FirstOrDefault();

                        if(this.form_mode == FORM_MODE.ADD)
                        {
                            if(sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == this.tmp_dealer.dealercod.Trim()).FirstOrDefault() != null)
                            {
                                MessageAlert.Show("รหัส " + this.tmp_dealer.dealercod + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                return;
                            }

                            this.tmp_dealer.creby_id = this.main_form.loged_in_user.id;
                            this.tmp_dealer.credat = DateTime.Now;
                            sn.dealer.Add(this.tmp_dealer);
                            sn.SaveChanges();
                            this.ResetFormState(FORM_MODE.READ);
                            this.curr_dealer = GetDealer(this.tmp_dealer.id);
                            this.FillForm(this.curr_dealer);
                            this.btnAdd.PerformClick();
                        }

                        if(this.form_mode == FORM_MODE.EDIT)
                        {
                            var dealer_to_update = sn.dealer.Where(d => d.flag == 0 && d.id == this.tmp_dealer.id).FirstOrDefault();

                            if(dealer_to_update == null)
                            {
                                MessageAlert.Show("ค้นหารหัส " + this.tmp_dealer.dealercod + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                return;
                            }
                            else
                            {
                                dealer_to_update.prenam = this.tmp_dealer.prenam;
                                dealer_to_update.compnam = this.tmp_dealer.compnam;
                                dealer_to_update.addr01 = this.tmp_dealer.addr01;
                                dealer_to_update.addr02 = this.tmp_dealer.addr02;
                                dealer_to_update.addr03 = this.tmp_dealer.addr03;
                                dealer_to_update.zipcod = this.tmp_dealer.zipcod;
                                dealer_to_update.telnum = this.tmp_dealer.telnum;
                                dealer_to_update.faxnum = this.tmp_dealer.faxnum;
                                dealer_to_update.contact = this.tmp_dealer.contact;
                                dealer_to_update.position = this.tmp_dealer.position;
                                dealer_to_update.busides = this.tmp_dealer.busides;
                                dealer_to_update.area_id = this.tmp_dealer.area_id;
                                dealer_to_update.area = area != null ? area.typcod : string.Empty;
                                dealer_to_update.remark = this.tmp_dealer.remark;
                                dealer_to_update.chgby_id = this.main_form.loged_in_user.id;
                                dealer_to_update.chgdat = DateTime.Now;

                                sn.SaveChanges();
                                this.ResetFormState(FORM_MODE.READ);
                                this.curr_dealer = GetDealer(this.tmp_dealer.id);
                                this.FillForm(this.curr_dealer);
                            }
                        }
                    }

                    if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                    {
                        if (this.form_mode == FORM_MODE.ADD_ITEM)
                        {
                            this.tmp_dmsg.creby_id = this.main_form.loged_in_user.id;
                            this.tmp_dmsg.credat = DateTime.Now;
                            this.tmp_dmsg.time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));
                            sn.d_msg.Add(this.tmp_dmsg);
                            sn.SaveChanges();

                            this.HideInlineForm();
                            this.ResetFormState(FORM_MODE.READ_ITEM);
                            this.btnAddMsg.PerformClick();
                        }

                        if (this.form_mode == FORM_MODE.EDIT_ITEM)
                        {
                            var msg_to_update = sn.d_msg.Where(d => d.flag == 0 && d.id == this.tmp_dmsg.id).FirstOrDefault();

                            if (msg_to_update != null) // update if exist
                            {
                                msg_to_update.date = this.tmp_dmsg.date;
                                msg_to_update.name = this.tmp_dmsg.name;
                                msg_to_update.description = this.tmp_dmsg.description;
                                msg_to_update.chgby_id = this.main_form.loged_in_user.id;
                                msg_to_update.chgdat = DateTime.Now;
                            }
                            else // insert if not exist
                            {
                                this.tmp_dmsg.creby_id = this.main_form.loged_in_user.id;
                                this.tmp_dmsg.credat = DateTime.Now;
                                sn.d_msg.Add(this.tmp_dmsg);
                            }
                            sn.SaveChanges();
                            this.HideInlineForm();
                            this.ResetFormState(FORM_MODE.READ_ITEM);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                dealer first_dealer = sn.dealer.Where(d => d.flag == 0).OrderBy(d => d.dealercod).FirstOrDefault();
                if (first_dealer == null)
                    return;

                this.curr_dealer = GetDealer(first_dealer.id);
                this.FillForm(this.curr_dealer);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(this.curr_dealer == null)
            {
                this.btnFirst.PerformClick();
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                dealer prev_dealer;
                switch (this.sort_by)
                {
                    case DialogInquiryDealer.SORT_BY.DEALERCOD:
                        prev_dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) < 0).OrderByDescending(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.COMPNAM:
                        prev_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.compnam, this.curr_dealer.compnam, StringComparison.OrdinalIgnoreCase) < 0 || (string.Compare(d.compnam, this.curr_dealer.compnam, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) < 0))).OrderByDescending(d => d.compnam).ThenByDescending(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.AREA:
                        prev_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.area, this.curr_dealer.area, StringComparison.OrdinalIgnoreCase) < 0 || (string.Compare(d.area, this.curr_dealer.area, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) < 0))).OrderByDescending(d => d.area).ThenByDescending(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.CONTACT:
                        prev_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.contact, this.curr_dealer.contact, StringComparison.OrdinalIgnoreCase) < 0 || (string.Compare(d.contact, this.curr_dealer.contact, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) < 0))).OrderByDescending(d => d.contact).ThenByDescending(d => d.dealercod).FirstOrDefault();
                        break;
                    default:
                        prev_dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) < 0).OrderByDescending(d => d.dealercod).FirstOrDefault();
                        break;
                }

                if (prev_dealer != null)
                {
                    this.curr_dealer = GetDealer(prev_dealer.id);
                    this.FillForm(this.curr_dealer);
                }
                else
                {
                    this.btnFirst.PerformClick();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(this.curr_dealer == null)
            {
                this.btnLast.PerformClick();
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                dealer next_dealer;
                switch (this.sort_by)
                {
                    case DialogInquiryDealer.SORT_BY.DEALERCOD:
                        next_dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) > 0).OrderBy(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.COMPNAM:
                        next_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.compnam, this.curr_dealer.compnam, StringComparison.OrdinalIgnoreCase) > 0 || (string.Compare(d.compnam, this.curr_dealer.compnam, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) > 0))).OrderBy(d => d.compnam).ThenBy(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.AREA:
                        next_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.area, this.curr_dealer.area, StringComparison.OrdinalIgnoreCase) > 0 || (string.Compare(d.area, this.curr_dealer.area, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) > 0))).OrderBy(d => d.area).ThenBy(d => d.dealercod).FirstOrDefault();
                        break;
                    case DialogInquiryDealer.SORT_BY.CONTACT:
                        next_dealer = sn.dealer.Where(d => d.flag == 0 && (string.Compare(d.contact, this.curr_dealer.contact, StringComparison.OrdinalIgnoreCase) > 0 || (string.Compare(d.contact, this.curr_dealer.contact, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) > 0))).OrderBy(d => d.contact).ThenBy(d => d.dealercod).FirstOrDefault();
                        break;
                    default:
                        next_dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod, this.curr_dealer.dealercod, StringComparison.OrdinalIgnoreCase) > 0).OrderBy(d => d.dealercod).FirstOrDefault();
                        break;
                }

                if(next_dealer != null)
                {
                    this.curr_dealer = GetDealer(next_dealer.id);
                    this.FillForm(this.curr_dealer);
                }
                else
                {
                    this.btnLast.PerformClick();
                }
            }
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                dealer last_dealer = sn.dealer.Where(d => d.flag == 0).OrderByDescending(d => d.dealercod).FirstOrDefault();
                if (last_dealer == null)
                    return;

                this.curr_dealer = GetDealer(last_dealer.id);
                this.FillForm(this.curr_dealer);
            }
        }

        private void btnSearch_ButtonClick(object sender, EventArgs e)
        {
            this.btnSearchCode.PerformClick();          
        }

        private void btnItem_ButtonClick(object sender, EventArgs e)
        {
            this.btnItemF8.PerformClick();
        }

        private void btnItemF8_Click(object sender, EventArgs e)
        {
            this.dgvDmsg.Focus();
            this.tabControl1.SelectedTab = this.tabPage1;
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.dgvDmsg.Refresh();
        }

        private void btnItemF7_Click(object sender, EventArgs e)
        {
            this.dgvSoldSn.Focus();
            this.tabControl1.SelectedTab = this.tabPage2;
            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void btnInquiryAll_Click(object sender, EventArgs e)
        {
            DialogInquiryDealer inq = new DialogInquiryDealer(this.sort_by);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                var d = GetDealer(inq.selected_dealer.id);
                if (d != null)
                {
                    this.curr_dealer = d;
                    this.FillForm(this.curr_dealer);
                }
                else
                {
                    MessageAlert.Show("ค้นหารหัส " + d.dealercod + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }
            }
        }

        private void btnInquiryRest_Click(object sender, EventArgs e)
        {
            DialogInquiryDealer inq = new DialogInquiryDealer(this.sort_by, this.curr_dealer);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                var d = GetDealer(inq.selected_dealer.id);
                if (d != null)
                {
                    this.curr_dealer = d;
                    this.FillForm(this.curr_dealer);
                }
                else
                {
                    MessageAlert.Show("ค้นหารหัส " + d.dealercod + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }
            }
        }

        private void btnInquiryCondition_Click(object sender, EventArgs e)
        {
            DialogSearchCondition cond = new DialogSearchCondition(this.inquiry_condtion);
            if(cond.ShowDialog() == DialogResult.OK)
            {
                this.inquiry_condtion = cond.condition_string;

                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        List<dealer> dealers = sn.Database.SqlQuery<dealer>("Select * From dealer Where flag=0 and (" + this.inquiry_condtion + ")").ToList();

                        DialogInquiryDealer inq = new DialogInquiryDealer(dealers);
                        if(inq.ShowDialog() == DialogResult.OK)
                        {
                            var dealer = GetDealer(inq.selected_dealer.id);
                            if(dealer != null)
                            {
                                this.curr_dealer = dealer;
                                this.FillForm(this.curr_dealer);
                            }
                            else
                            {
                                MessageAlert.Show("ค้นหารหัส " + inq.selected_dealer.dealercod + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void btnSearchCode_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch s = new Subform.DialogSimpleSearch(false, "ป้อนข้อมูลที่ต้องการค้นหา", "Dealer Code", this.curr_dealer.dealercod);
            if(s.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquiryDealer.SORT_BY.DEALERCOD;
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod, s.keyword, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(d => d.dealercod).ToList();

                    if(dealer.Count == 0)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    var match_dealer = dealer.Where(d => string.Compare(d.dealercod, s.keyword, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                    if (match_dealer == null)
                    {
                        if(MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_dealer = GetDealer(dealer.First().id);
                    this.FillForm(this.curr_dealer);
                }
            }
        }

        private void btnSearchContact_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch s = new Subform.DialogSimpleSearch(false, "ป้อนข้อมูลที่ต้องการค้นหา", "Contact", this.curr_dealer.contact);
            if (s.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquiryDealer.SORT_BY.CONTACT;
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.contact, s.keyword, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(d => d.contact).ThenBy(d => d.dealercod).ToList();

                    if (dealer.Count == 0)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    var match_dealer = dealer.Where(d => string.Compare(d.contact, s.keyword, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                    if (match_dealer == null)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_dealer = GetDealer(dealer.First().id);
                    this.FillForm(this.curr_dealer);
                }
            }
        }

        private void btnSearchName_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch s = new Subform.DialogSimpleSearch(false, "ป้อนข้อมูลที่ต้องการค้นหา", "Comp. Name", this.curr_dealer.compnam);
            if (s.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquiryDealer.SORT_BY.COMPNAM;
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.compnam, s.keyword, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(d => d.compnam).ThenBy(d => d.dealercod).ToList();

                    if (dealer.Count == 0)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    var match_dealer = dealer.Where(d => string.Compare(d.compnam, s.keyword, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                    if (match_dealer == null)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_dealer = GetDealer(dealer.First().id);
                    this.FillForm(this.curr_dealer);
                }
            }
        }

        private void btnSearchArea_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch s = new Subform.DialogSimpleSearch(false, "ป้อนข้อมูลที่ต้องการค้นหา", "Area", this.curr_dealer.area);
            if (s.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquiryDealer.SORT_BY.AREA;
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.area, s.keyword, StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(d => d.area).ThenBy(d => d.dealercod).ToList();

                    if (dealer.Count == 0)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    var match_dealer = dealer.Where(d => string.Compare(d.area, s.keyword, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                    if (match_dealer == null)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_dealer = GetDealer(dealer.First().id);
                    this.FillForm(this.curr_dealer);
                }
            }
        }

        private void btnPrint_ButtonClick(object sender, EventArgs e)
        {
            this.btnPrintLabel3Col.PerformClick();
        }

        private void btnPrintLabel3Col_Click(object sender, EventArgs e)
        {
            DialogPrintDealerLabel p = new DialogPrintDealerLabel(this.curr_dealer, this.curr_dealer, this.print_condition);
            if(p.ShowDialog() == DialogResult.OK)
            {
                this.print_condition = p.condition;
                string dealercod_from = p.dealer_from != null ? p.dealer_from.dealercod : string.Empty;
                string dealercod_to = p.dealer_to != null ? p.dealer_to.dealercod : string.Empty;

                List<dealer> dealers = null;
                this.ShowLoadingBox();
                XPrintPreview xp = null;
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    using (snEntities sn = DBX.DataSet())
                    {
                        if (this.print_condition.Trim().Length > 0)
                        {
                            dealers = sn.Database.SqlQuery<dealer>("Select * From dealer Where flag = 0 and (" + this.print_condition + ")").ToList();
                            dealers = dealers.Where(d => d.flag == 0 && string.Compare(d.dealercod.Trim(), p.dealer_from.dealercod, StringComparison.OrdinalIgnoreCase) >= 0 && string.Compare(d.dealercod.Trim(), p.dealer_to.dealercod, StringComparison.OrdinalIgnoreCase) <= 0).OrderBy(d => d.dealercod).ToList();
                        }
                        else
                        {
                            dealers = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod.Trim(), p.dealer_from.dealercod, StringComparison.OrdinalIgnoreCase) >= 0 && string.Compare(d.dealercod.Trim(), p.dealer_to.dealercod, StringComparison.OrdinalIgnoreCase) <= 0).OrderBy(d => d.dealercod).ToList();
                        }

                        PrintDocument pdoc = this.PreparePrintLabel(dealers, LABEL_COLS.COLS_3);
                        int total_page = XPrintPreview.GetTotalPageCount(pdoc);
                        xp = new XPrintPreview(this.PreparePrintLabel(dealers, LABEL_COLS.COLS_3), total_page, PRINT_AUTHORIZE_STATE.READY_TO_PRINT);
                    }
                };
                wrk.RunWorkerCompleted += delegate
                {
                    if (p.output == PRINT_OUTPUT.SCREEN)
                    {
                        xp.MdiParent = this.main_form;
                        xp.Show();
                    }

                    if (p.output == PRINT_OUTPUT.PRINTER)
                    {
                        xp.btnPrint.PerformClick();
                    }

                    this.HideLoadingBox();
                };
                wrk.RunWorkerAsync();
            }
        }

        private void btnPrintLabel2Col_Click(object sender, EventArgs e)
        {
            DialogPrintDealerLabel p = new DialogPrintDealerLabel(this.curr_dealer, this.curr_dealer, this.print_condition);
            if (p.ShowDialog() == DialogResult.OK)
            {
                this.print_condition = p.condition;
                string dealercod_from = p.dealer_from != null ? p.dealer_from.dealercod : string.Empty;
                string dealercod_to = p.dealer_to != null ? p.dealer_to.dealercod : string.Empty;

                List<dealer> dealers = null;
                this.ShowLoadingBox();
                XPrintPreview xp = null;
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    using (snEntities sn = DBX.DataSet())
                    {
                        if (this.print_condition.Trim().Length > 0)
                        {
                            dealers = sn.Database.SqlQuery<dealer>("Select * From dealer Where flag = 0 and (" + this.print_condition + ")").ToList();
                            dealers = dealers.Where(d => d.flag == 0 && string.Compare(d.dealercod.Trim(), p.dealer_from.dealercod, StringComparison.OrdinalIgnoreCase) >= 0 && string.Compare(d.dealercod.Trim(), p.dealer_to.dealercod, StringComparison.OrdinalIgnoreCase) <= 0).OrderBy(d => d.dealercod).ToList();
                        }
                        else
                        {
                            dealers = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod.Trim(), p.dealer_from.dealercod, StringComparison.OrdinalIgnoreCase) >= 0 && string.Compare(d.dealercod.Trim(), p.dealer_to.dealercod, StringComparison.OrdinalIgnoreCase) <= 0).OrderBy(d => d.dealercod).ToList();
                        }

                        PrintDocument pdoc = this.PreparePrintLabel(dealers, LABEL_COLS.COLS_2);
                        int total_page = XPrintPreview.GetTotalPageCount(pdoc);
                        xp = new XPrintPreview(this.PreparePrintLabel(dealers, LABEL_COLS.COLS_2), total_page, PRINT_AUTHORIZE_STATE.READY_TO_PRINT);
                    }
                };
                wrk.RunWorkerCompleted += delegate
                {
                    if (p.output == PRINT_OUTPUT.SCREEN)
                    {
                        xp.MdiParent = this.main_form;
                        xp.Show();
                    }

                    if (p.output == PRINT_OUTPUT.PRINTER)
                    {
                        xp.btnPrint.PerformClick();
                    }

                    this.HideLoadingBox();
                };
                wrk.RunWorkerAsync();
            }
        }

        private void btnPrintLittleEnv_Click(object sender, EventArgs e)
        {
            DialogSimplePrintOutput p = new DialogSimplePrintOutput();
            if (p.ShowDialog() == DialogResult.OK)
            {
                this.ShowLoadingBox();

                XPrintPreview xp = null;
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    xp = new XPrintPreview(this.PreparePrintEnvelope(this.curr_dealer, false), 1);
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.HideLoadingBox();
                    if (xp == null)
                    {
                        MessageAlert.Show("Cannot create print document!", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }
                        
                    if(p.output == PRINT_OUTPUT.SCREEN)
                    {
                        xp.MdiParent = this.main_form;
                        xp.Show();
                    }

                    if(p.output == PRINT_OUTPUT.PRINTER)
                    {
                        xp.btnPrint.PerformClick();
                    }
                };
                wrk.RunWorkerAsync();
            }
        }

        private void btnPrintBigEnv_Click(object sender, EventArgs e)
        {
            DialogSimplePrintOutput p = new DialogSimplePrintOutput();
            if (p.ShowDialog() == DialogResult.OK)
            {
                this.ShowLoadingBox();

                XPrintPreview xp = null;
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    xp = new XPrintPreview(this.PreparePrintEnvelope(this.curr_dealer, true), 1);
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.HideLoadingBox();
                    if (xp == null)
                    {
                        MessageAlert.Show("Cannot create print document!", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                        return;
                    }

                    if (p.output == PRINT_OUTPUT.SCREEN)
                    {
                        xp.MdiParent = this.main_form;
                        xp.Show();
                    }

                    if (p.output == PRINT_OUTPUT.PRINTER)
                    {
                        xp.btnPrint.PerformClick();
                    }
                };
                wrk.RunWorkerAsync();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (this.curr_dealer == null)
                return;

            var d = GetDealer(this.curr_dealer.id);
            if(d != null)
            {
                this.curr_dealer = d;
                this.FillForm(this.curr_dealer);
            }
            else
            {
                this.btnNext.PerformClick();
            }

        }

        private void inlineDate__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_dmsg != null)
                this.tmp_dmsg.date = ((XDatePicker)sender)._SelectedDate;
        }

        private void inlineName__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dmsg != null)
                this.tmp_dmsg.name = ((XTextEdit)sender)._Text;
        }

        private void inlineDesc__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dmsg != null)
                this.tmp_dmsg.description = ((XTextEdit)sender)._Text;
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if(!(this.form_mode == FORM_MODE.READ) && this.focused_control != null)
            {
                e.Cancel = true;
                this.focused_control.Focus();
                return;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (!(this.form_mode == FORM_MODE.READ) && this.focused_control != null)
            {
                this.focused_control.Focus();
                return;
            }
        }

        private void PerformEdit(Object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ)
            {
                this.btnEdit.PerformClick();
                ((Control)sender).Focus();
            }
        }

        private void KeepFocusedControl(object sender, EventArgs e)
        {
            this.focused_control = ((Control)sender);
        }

        private void txtDealercod__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.dealercod = ((XTextEdit)sender)._Text;
        }

        private void txtPrenam__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.prenam = ((XTextEdit)sender)._Text;
        }

        private void txtCompnam__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.compnam = ((XTextEdit)sender)._Text;
        }

        private void txtAddr1__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.addr01 = ((XTextEdit)sender)._Text;
        }

        private void txtAddr2__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.addr02 = ((XTextEdit)sender)._Text;
        }

        private void txtAddr3__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.addr03 = ((XTextEdit)sender)._Text;
        }

        private void txtZipcod__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.zipcod = ((XTextEdit)sender)._Text;
        }

        private void txtTelnum__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.telnum = ((XTextEdit)sender)._Text;
        }

        private void txtFaxnum__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.faxnum = ((XTextEdit)sender)._Text;
        }

        private void txtContact__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.contact = ((XTextEdit)sender)._Text;
        }

        private void txtPosition__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.position = ((XTextEdit)sender)._Text;
        }

        private void txtBusides__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.busides = ((XTextEdit)sender)._Text;
        }

        private void txtRemark__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_dealer != null)
                this.tmp_dealer.remark = ((XTextEdit)sender)._Text;
        }

        private void brArea__ButtonClick(object sender, EventArgs e)
        {
            istab area = null;
            using (snEntities sn = DBX.DataSet())
            {
                if (this.tmp_dealer.area_id.HasValue)
                    area = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_AREA && i.id == this.tmp_dealer.area_id).FirstOrDefault();
            }

            DialogSelectIstab inq = new DialogSelectIstab(TABTYP.AREA, area);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_istab.typcod;
                this.lblArea.Text = inq.selected_istab.typdes_th;
                this.tmp_dealer.area_id = inq.selected_istab.id;
                this.tmp_dealer.area = inq.selected_istab.typcod;
                //this.tmp_dealer.istab = inq.selected_istab;
            }
        }

        private void brArea__Leave(object sender, EventArgs e)
        {
            if (this.tmp_dealer == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
            {
                //this.tmp_dealer.istab = null;
                this.tmp_dealer.area_id = null;
                this.tmp_dealer.area = string.Empty;
                this.lblArea.Text = string.Empty;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var area = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_AREA && i.typcod == str).FirstOrDefault();
                    if (area != null)
                    {
                        this.tmp_dealer.area_id = area.id;
                        this.tmp_dealer.area = area.typcod;
                        //this.tmp_dealer.istab = area;
                        this.lblArea.Text = area.typdes_th;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void txtDealercod__Leave(object sender, EventArgs e)
        {
            if (this.form_mode != FORM_MODE.ADD || this.tmp_dealer == null)
                return;

            using (snEntities sn = DBX.DataSet())
            {
                if(sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == this.tmp_dealer.dealercod.Trim()).FirstOrDefault() != null)
                {
                    MessageAlert.Show("รหัส " + this.tmp_dealer.dealercod + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    this.txtDealercod.Focus();
                    return;
                }
                    
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.txtRemark._Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }

                if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.inlineDesc._Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }

                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnAdd.PerformClick();
                    return true;
                }
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnAddMsg.PerformClick();
                    return true;
                }
            }

            if(keyData == (Keys.Alt | Keys.E))
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnEdit.PerformClick();
                    return true;
                }
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnEditMsg.PerformClick();
                    return true;
                }
            }

            if(keyData == (Keys.Alt | Keys.D))
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnDelete.PerformClick();
                    return true;
                }
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnDeleteMsg.PerformClick();
                    return true;
                }
            }

            if(keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.Home))
            {
                this.btnFirst.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.End))
            {
                this.btnLast.PerformClick();
                return true;
            }

            if(keyData == Keys.PageUp)
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnPrev.PerformClick();
                    return true;
                }
            }

            if(keyData == Keys.PageDown)
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.btnNext.PerformClick();
                    return true;
                }
            }

            if(keyData == Keys.F7)
            {
                this.btnItemF7.PerformClick();
                return true;
            }

            if(keyData == Keys.F8)
            {
                this.btnItemF8.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.L))
            {
                this.btnInquiryAll.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.L))
            {
                this.btnInquiryRest.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.K))
            {
                this.btnInquiryCondition.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.S))
            {
                this.btnSearchCode.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.D2))
            {
                this.btnSearchContact.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.D3))
            {
                this.btnSearchName.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.D4))
            {
                this.btnSearchArea.PerformClick();
                return true;
            }

            if(keyData == (Keys.Alt | Keys.P))
            {
                this.btnPrintLabel3Col.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.P))
            {
                this.btnPrintLittleEnv.PerformClick();
                return true;
            }

            if(keyData == (Keys.Control | Keys.F5))
            {
                this.btnReload.PerformClick();
                return true;
            }

            if(keyData == Keys.F3)
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    return true;
                }
            }

            if(keyData == Keys.F4)
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    this.tabControl1.SelectedTab = this.tabPage2;
                    return true;
                }
            }

            if(keyData == Keys.Tab)
            {
                if(this.form_mode == FORM_MODE.READ)
                {
                    if (this.curr_dealer == null)
                        return false;

                    using (snEntities sn = DBX.DataSet())
                    {
                        var dealer = sn.dealer.Include("users").Include("users1").Where(d => d.flag == 0 && d.id == this.curr_dealer.id).FirstOrDefault();
                        var total_row = sn.dealer.Where(d => d.flag == 0).Select(d => d.id).AsEnumerable().Count();
                        string expression = string.Empty;
                        switch (this.sort_by)
                        {
                            case DialogInquiryDealer.SORT_BY.DEALERCOD:
                                expression = "Dealercod";
                                break;
                            case DialogInquiryDealer.SORT_BY.COMPNAM:
                                expression = "Compnam+Dealercod";
                                break;
                            case DialogInquiryDealer.SORT_BY.AREA:
                                expression = "Area+Dealercod";
                                break;
                            case DialogInquiryDealer.SORT_BY.CONTACT:
                                expression = "Contact+Dealercod";
                                break;
                            default:
                                expression = "Dealercod";
                                break;
                        }

                        DbInfo info = new DbInfo
                        {
                            DbName = sn.Database.Connection.Database,
                            TbName = "Dealer",
                            Expression = expression,
                            CreBy = dealer.creby_id.HasValue ? dealer.users1.username : string.Empty,
                            CreDat = dealer.credat,
                            ChgBy = dealer.chgby_id.HasValue ? dealer.users.username : string.Empty,
                            ChgDat = dealer.chgdat,
                            RecId = dealer.id,
                            TotalRec = total_row
                        };
                        DialogDataInfo d_info = new DialogDataInfo(info);
                        d_info.ShowDialog();
                        return true;
                    }
                }

                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if (this.dgvDmsg.CurrentCell == null)
                        return false;

                    int id = (int)this.dgvDmsg.Rows[this.dgvDmsg.CurrentCell.RowIndex].Cells[this.col_id.Name].Value;
                    using (snEntities sn = DBX.DataSet())
                    {
                        var dmsg = sn.d_msg.Include("users").Include("users1").Where(d => d.flag == 0 && d.id == id).FirstOrDefault();
                        int total_row = sn.d_msg.Where(d => d.flag == 0).Select(d => d.id).AsEnumerable().Count();

                        if(dmsg != null)
                        {
                            DbInfo info = new DbInfo
                            {
                                DbName = sn.Database.Connection.Database,
                                TbName = "D_msg",
                                Expression = "Dealer_id+Date+Time",
                                CreBy = dmsg.creby_id.HasValue ? dmsg.users1.username : string.Empty,
                                CreDat = dmsg.credat,
                                ChgBy = dmsg.chgby_id.HasValue ? dmsg.users.username : string.Empty,
                                ChgDat = dmsg.chgdat,
                                RecId = dmsg.id,
                                TotalRec = total_row
                            };
                            DialogDataInfo d_info = new DialogDataInfo(info);
                            d_info.ShowDialog();
                            return true;
                        }
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvDmsg_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                if (e.RowIndex == -1)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 255);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    e.Handled = true;
                }
            }
        }

        private void dgvDmsg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

            if (row_index == -1)
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnItemF8.PerformClick();
                    this.btnAddMsg.PerformClick();
                }

            }
            else
            {
                if (this.form_mode == FORM_MODE.READ || this.form_mode == FORM_MODE.READ_ITEM)
                {
                    this.btnItemF8.PerformClick();
                    this.btnEditMsg.PerformClick();
                }
            }
        }


        private enum LABEL_COLS : int
        {
            COLS_2 = 2,
            COLS_3 = 3
        }
        private PrintDocument PreparePrintLabel(List<dealer> dealers, LABEL_COLS columns = LABEL_COLS.COLS_2)
        {
            try
            {
                if (dealers == null)
                    return null;

                Font fnt = new Font("angsana new", 16f, FontStyle.Regular);
                SolidBrush brush = new SolidBrush(Color.Black);
                int line_height = fnt.Height + 2;

                StringFormat format_left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };
                StringFormat format_right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };
                StringFormat format_center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };

                int item_count = 0;
                int col_count = 0;
                int max_cols = columns == LABEL_COLS.COLS_3 ? 3 : (columns == LABEL_COLS.COLS_2 ? 2 : 1);

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = new Margins(20, 20, 30, 30);
                pd.DefaultPageSettings.Landscape = false;
                //pd.DefaultPageSettings.PaperSize = pd.DefaultPageSettings.PrinterSettings.PaperSizes.Cast<PaperSize>().Where(p => p.PaperName == "A4").First();

                pd.BeginPrint += delegate (object sender, PrintEventArgs e)
                {
                    PaperSize p_size = null;
                    if (columns == LABEL_COLS.COLS_3)
                        p_size = new PaperSize("Sticker3Cols", 1250, 1195);
                    if (columns == LABEL_COLS.COLS_2)
                        p_size = new PaperSize("Sticker2Cols", 825, 1165);

                    p_size.RawKind = (int)PaperKind.Custom;
                    ((PrintDocument)sender).DefaultPageSettings.PaperSize = p_size;

                    item_count = 0;
                    col_count = 0;
                };
                pd.PrintPage += delegate (object sender, PrintPageEventArgs e)
                {
                    int block_width = columns == LABEL_COLS.COLS_3 ? Convert.ToInt32(Math.Floor((decimal)(e.MarginBounds.Right - e.MarginBounds.Left) / 3)) : (columns == LABEL_COLS.COLS_2 ? Convert.ToInt32(Math.Floor((decimal)(e.MarginBounds.Right - e.MarginBounds.Left) / 2)) : e.MarginBounds.Right - e.MarginBounds.Left);
                    int block_height = line_height * 5;
                    int y = e.MarginBounds.Top;
                    

                    for (int i = item_count; i < dealers.Count; i++)
                    {
                        int x = e.MarginBounds.Left + (col_count * block_width);
                        Rectangle send_rect = new Rectangle(x, y, TextRenderer.MeasureText("ส่ง", fnt).Width, line_height);
                        e.Graphics.DrawString("ส่ง", fnt, brush, send_rect);
                        e.Graphics.DrawString(dealers[i].contact, fnt, brush, new Rectangle(x + send_rect.Width, y, block_width - send_rect.Width, line_height));
                        e.Graphics.DrawString(dealers[i].prenam + " " + dealers[i].compnam, fnt, brush, new Rectangle(x + send_rect.Width, y + line_height, block_width - send_rect.Width, line_height));
                        e.Graphics.DrawString(dealers[i].addr01, fnt, brush, new Rectangle(x + send_rect.Width, y + (line_height * 2), block_width - send_rect.Width, line_height));
                        e.Graphics.DrawString(dealers[i].addr02 + " " + dealers[i].addr03 + " " + dealers[i].zipcod, fnt, brush, new Rectangle(x + send_rect.Width, y + (line_height * 3), block_width - send_rect.Width, line_height));
                        item_count++;
                        col_count++;

                        if(col_count == max_cols)
                        {
                            y += block_height;
                            col_count = 0;
                        }

                        if(y + block_height > e.MarginBounds.Bottom)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                    }
                };

                return pd;
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return null;
            }
        }

        private PrintDocument PreparePrintEnvelope(dealer dealer_to_print, bool big_envelope = false)
        {
            try
            {
                dealer dealer = null;
                using (snEntities sn = DBX.DataSet())
                {
                    dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == dealer_to_print.dealercod.Trim()).FirstOrDefault();
                }

                if (dealer == null)
                {
                    return null;
                }

                Font fnt = new Font("angsana new", 16f, FontStyle.Regular);
                SolidBrush brush = new SolidBrush(Color.Black);
                int line_height = fnt.Height + 2;

                StringFormat format_left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };
                StringFormat format_right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };
                StringFormat format_center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap };

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins = big_envelope ? new Margins(220, 20, 200, 30) : new Margins(220, 20, 120, 30);
                pd.DefaultPageSettings.Landscape = false;
                //pd.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4;
                if (!big_envelope)
                    pd.DefaultPageSettings.PaperSize = new PaperSize("LittleEnvelope", 910, 425);
                if (big_envelope)
                    pd.DefaultPageSettings.PaperSize = new PaperSize("BigEnvelope", 890, 600);

                pd.BeginPrint += delegate (object sender, PrintEventArgs e)
                {
                    
                };
                pd.PrintPage += delegate (object sender, PrintPageEventArgs e)
                {
                    Rectangle send_rect = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, TextRenderer.MeasureText("กรุณาส่ง", fnt).Width, line_height);
                    e.Graphics.DrawString("กรุณาส่ง", fnt, brush, new Rectangle(e.MarginBounds.X, e.MarginBounds.Top, send_rect.Width, line_height));
                    e.Graphics.DrawString(dealer.contact, fnt, brush, new Rectangle(e.MarginBounds.Left + send_rect.Width + 5, e.MarginBounds.Top + line_height, TextRenderer.MeasureText(dealer.contact, fnt).Width + 30, line_height));
                    e.Graphics.DrawString(dealer.prenam + " " + dealer.compnam, fnt, brush, new Rectangle(e.MarginBounds.Left + send_rect.Width + 5, e.MarginBounds.Top + (line_height * 2), TextRenderer.MeasureText(dealer.prenam + " " + dealer.compnam, fnt).Width + 30, line_height));
                    e.Graphics.DrawString(dealer.addr01, fnt, brush, new Rectangle(e.MarginBounds.Left + send_rect.Width + 5, e.MarginBounds.Top + (line_height * 3), TextRenderer.MeasureText(dealer.addr01, fnt).Width + 30, line_height));
                    e.Graphics.DrawString(dealer.addr02 + " " + dealer.addr03, fnt, brush, new Rectangle(e.MarginBounds.Left + send_rect.Width + 5, e.MarginBounds.Top + (line_height * 4), TextRenderer.MeasureText(dealer.addr02 + " " + dealer.addr03, fnt).Width + 30, line_height));
                    e.Graphics.DrawString(dealer.zipcod, fnt, brush, new Rectangle(e.MarginBounds.Left + send_rect.Width + 5, e.MarginBounds.Top + (line_height * 5), TextRenderer.MeasureText(dealer.zipcod, fnt).Width + 30, line_height));

                };

                return pd;
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                return null;
            }
        }
    }
}
