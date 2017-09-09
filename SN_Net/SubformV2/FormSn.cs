using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.Model;
using SN_Net.Subform;
using CC;
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class FormSn : Form
    {
        private MainForm main_form;
        private FORM_MODE form_mode;
        private serial curr_serial;
        private serial tmp_serial;
        private BindingList<serialPasswordVM> password_list;
        private BindingList<problemVM> problem_list;
        private Control focused_control;
        private DialogInquirySn.SORT_BY sort_by;

        public FormSn()
        {
            InitializeComponent();
        }

        public FormSn(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
        }

        private void SnWindow2_Load(object sender, EventArgs e)
        {
            this.sort_by = DialogInquirySn.SORT_BY.SERNUM;
            this.ResetFormState(FORM_MODE.READ);

            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            this.SetVerextSelection();

            this.password_list = new BindingList<serialPasswordVM>();
            this.dgvPassword.DataSource = this.password_list;

            this.problem_list = new BindingList<problemVM>();
            this.dgvProblem.DataSource = this.problem_list;

            this.ActiveControl = this.toolStrip1;
            this.btnLast.PerformClick();
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            /* ToolStrip */
            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM, FORM_MODE.ADD, FORM_MODE.EDIT, FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnFirst.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPrev.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnNext.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnLast.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnItem.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnImport.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnGenSn.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnUpgrade.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnBook.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSet2.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearch.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchArea.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchBusityp.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchCompany.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchContact.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchDealer.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchOldnum.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSearchSN.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryAll.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryCloud.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryMA.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnInquiryRest.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnReload.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            /* Control */
            this.mskSernum.SetControlState(new FORM_MODE[] { FORM_MODE.ADD }, this.form_mode);
            this.txtVersion.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.brArea.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.mskRefSn.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.btnSwithToRefnum.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.txtPrenam.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtCompnam.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.chkIMOnly.SetControlState(new FORM_MODE[] { FORM_MODE.READ, FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnLostRenew.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnCD.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnUP.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnUPNewRwt.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnUPNewRwtJob.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSupportNote.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnSupportViewNote.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);

            this.txtAddr01.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr02.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr03.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtZipcod.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtTelnum.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtFaxnum.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtContact.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtPosition.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.mskOldSn.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtRemark.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtBusides.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.brBusityp.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.brDealer.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.brHowknown.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.dtPurdat.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.dtExpdat.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.txtUpfree.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.dtManual.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.dlVerext.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);
            this.dtVerextDat.SetControlState(new FORM_MODE[] { FORM_MODE.ADD, FORM_MODE.EDIT }, this.form_mode);

            this.btnPasswordAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnPasswordRemove.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnEditMA.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnDeleteMA.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnEditCloud.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
            this.btnDeleteCloud.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
        }

        private void FillForm(serial serial_to_fill = null)
        {
            serial serial;
            if (serial_to_fill == null)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    serial = sn.CreateTmpSerial(this.main_form);
                }
            }
            else
            {
                serial = serial_to_fill;
            }

            try
            {
                this.mskSernum._Text = serial.sernum;
                this.txtVersion._Text = serial.version;
                this.brArea._Text = serial.istab != null ? serial.istab.typcod : string.Empty;
                this.lblArea.Text = serial.istab != null ? serial.istab.typdes_th : string.Empty;
                this.mskRefSn._Text = serial.refnum;
                this.txtPrenam._Text = serial.prenam;
                this.txtCompnam._Text = serial.compnam;

                this.txtAddr01._Text = serial.addr01;
                this.txtAddr02._Text = serial.addr02;
                this.txtAddr03._Text = serial.addr03;
                this.txtZipcod._Text = serial.zipcod;
                this.txtTelnum._Text = serial.telnum;
                this.txtFaxnum._Text = serial.faxnum;
                this.txtContact._Text = serial.contact;
                this.txtPosition._Text = serial.position;
                this.mskOldSn._Text = serial.oldnum;

                this.txtRemark._Text = serial.remark;
                this.txtBusides._Text = serial.busides;
                this.brBusityp._Text = serial.istab1 != null ? serial.istab1.typcod : string.Empty;
                this.lblBusityp.Text = serial.istab1 != null ? serial.istab1.typdes_th : string.Empty;
                this.brDealer._Text = serial.dealer != null ? serial.dealer.dealercod : string.Empty;
                this.lblDealer.Text = serial.dealer != null ? serial.dealer.compnam : string.Empty;
                this.brHowknown._Text = serial.istab2 != null ? serial.istab2.typcod : string.Empty;
                this.lblHowknown.Text = serial.istab2 != null ? serial.istab2.typdes_th : string.Empty;
                this.dtPurdat._SelectedDate = serial.purdat;
                this.dtExpdat._SelectedDate = serial.expdat;
                this.txtUpfree._Text = serial.upfree;
                this.dtManual._SelectedDate = serial.manual;
                this.dtVerextDat._SelectedDate = serial.verextdat;

                XDropdownListItem verext = this.dlVerext._Items.Cast<XDropdownListItem>().Where(i => (int)i.Value == serial.verext_id).FirstOrDefault();
                this.dlVerext._SelectedItem = verext != null ? verext : this.dlVerext._Items.Cast<XDropdownListItem>().First();

                this.txtExpdat2._Text = serial.expdat.HasValue ? serial.expdat.Value.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH")) : "";
                this.txtTelnum2._Text = serial.telnum;
                this.txtContact2._Text = serial.contact;
                this.txtUpfree2._Text = serial.upfree;

                this.dtMaFrom._SelectedDate = serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? serial.ma.Where(m => m.flag == 0).First().start_date : null;
                this.dtMaTo._SelectedDate = serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? serial.ma.Where(m => m.flag == 0).First().end_date : null;
                this.txtMaEmail._Text = serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? serial.ma.Where(m => m.flag == 0).First().email : string.Empty;
                if(serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0)
                {
                    DateTime expire_date = serial.ma.Where(m => m.flag == 0).First().end_date.Value;
                    this.lblMAExpireWarning.Visible = expire_date.CompareTo(DateTime.Now.AddDays(15)) <= 0 ? true : false;
                }
                else
                {
                    this.lblMAExpireWarning.Visible = false;
                }
                

                this.dtCloudFrom._SelectedDate = serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? serial.cloud_srv.Where(c => c.flag == 0).First().start_date : null;
                this.dtCloudTo._SelectedDate = serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? serial.cloud_srv.Where(c => c.flag == 0).First().end_date : null;
                this.txtCloudEmail._Text = serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? serial.cloud_srv.Where(c => c.flag == 0).First().email : string.Empty;
                if(serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0)
                {
                    DateTime expire_date = serial.cloud_srv.Where(c => c.flag == 0).First().end_date.Value;
                    this.lblCloudExpireWarning.Visible = expire_date.CompareTo(DateTime.Now.AddDays(15)) <= 0 ? true: false;
                }
                else
                {
                    this.lblCloudExpireWarning.Visible = false;
                }

                this.password_list = null;
                this.password_list = new BindingList<serialPasswordVM>(serial.serial_password.AsEnumerable().ToViewModel());
                this.dgvPassword.DataSource = this.password_list;

                this.problem_list = null;
                this.problem_list = new BindingList<problemVM>(serial.problem.ToViewModel());
                this.dgvProblem.DataSource = this.problem_list;

                /* Set Toolstrip Button State in Case No Data*/
                if(this.form_mode == FORM_MODE.READ)
                {
                    if(serial.id == -1)
                    {
                        this.btnEdit.Enabled = false;
                        this.btnDelete.Enabled = false;
                        this.btnFirst.Enabled = false;
                        this.btnPrev.Enabled = false;
                        this.btnNext.Enabled = false;
                        this.btnLast.Enabled = false;
                        this.btnItem.Enabled = false;
                        this.btnUpgrade.Enabled = false;
                        this.btnBook.Enabled = false;
                        this.btnSet2.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message, "Error");
            }
        }

        private void SetVerextSelection()
        {
            this.dlVerext._Items.Clear();

            using (snEntities sn = DBX.DataSet())
            {
                var verext = sn.istab
                            .Where(i => i.flag == 0)
                            .Where(i => i.tabtyp == istabDbf.TABTYP_VEREXT)
                            .OrderBy(i => i.typcod)
                            .ToList();

                foreach (var item in verext)
                {
                    this.dlVerext._Items.Add(new XDropdownListItem { Value = item.id, Text = item.typcod + " : " + item.typdes_th });
                }
            }
        }

        private void KeepFocusedControl(object sender, EventArgs e)
        {
            this.focused_control = (Control)sender;
            //Console.WriteLine(" == >> focused control : " + this.focused_control.Name);
        }

        private serial GetSerial(int id)
        {
            using (snEntities sn = DBX.DataSet())
            {
                var serial = sn.serial
                            .Include("istab")
                            .Include("istab1")
                            .Include("istab2")
                            .Include("istab3")
                            .Include("dealer")
                            .Include("users")
                            .Include("users1")
                            .Include("cloud_srv")
                            .Include("ma")
                            .Include("problem")
                            .Include("serial_password")
                            .Where(s => s.flag == 0 && s.id == id)
                            .FirstOrDefault();

                return serial;
            }
        }

        private bool ValidateData(serial serial)
        {
            if(ValidateSN.Check(serial.sernum) == false)
            {
                MessageAlert.Show("กรุณาป้อน S/N ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.mskSernum.Focus();
                return false;
            }

            if(serial.refnum.Replace("-", "").Trim().Length > 0 && ValidateSN.Check(serial.refnum) == false)
            {
                MessageAlert.Show("กรุณาป้อน Ref. S/N ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.mskRefSn.Focus();
                return false;
            }

            if (serial.oldnum.Replace("-", "").Trim().Length > 0 && ValidateSN.Check(serial.oldnum) == false)
            {
                MessageAlert.Show("กรุณาป้อน Old Serial ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.mskOldSn.Focus();
                return false;
            }

            if(serial.version.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุ Version ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtVersion.Focus();
                return false;
            }

            return true;
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                this.tmp_serial = sn.CreateTmpSerial(this.main_form);
                this.ResetFormState(FORM_MODE.ADD);
                this.FillForm(this.tmp_serial);
                this.mskSernum.Focus();
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.tmp_serial = this.GetSerial(this.curr_serial.id);

            if(this.tmp_serial != null)
            {
                this.ResetFormState(FORM_MODE.EDIT);
                this.FillForm(this.tmp_serial);
                this.txtVersion.Focus();
            }
            else
            {
                MessageAlert.Show("ข้อมูลที่ท่านต้องการแก้ไขไม่มีอยู่ในระบบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
            }
        }

        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (this.curr_serial == null)
                return;

            if (MessageAlert.Show("ลบ S/N " + this.curr_serial.sernum + " ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var sn_to_delete = sn.serial.Find(this.curr_serial.id);
                    if(sn_to_delete == null)
                    {
                        MessageAlert.Show("ข้อมูลที่ต้องการลบไม่มีอยู่ในระบบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                    else
                    {
                        sn_to_delete.flag = sn_to_delete.id;
                        sn_to_delete.chgby_id = this.main_form.loged_in_user.id;
                        sn_to_delete.chgdat = DateTime.Now;
                        sn.SaveChanges();

                        var prev_sn = sn.serial.Where(s => s.flag == 0 && s.sernum.CompareTo(sn_to_delete.sernum) < 0).OrderByDescending(s => s.sernum).FirstOrDefault();
                        this.FillForm(prev_sn);
                    }

                }
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.ResetFormState(FORM_MODE.READ);
                return;
            }

            if(this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.FillForm(this.curr_serial);
                this.tmp_serial = null;
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.ADD)
            {
                if (this.ValidateData(this.tmp_serial) == false)
                    return;

                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        this.tmp_serial.credat = DateTime.Now;
                        sn.serial.Add(this.tmp_serial);
                        sn.SaveChanges();

                        this.curr_serial = this.GetSerial(this.tmp_serial.id);
                        this.ResetFormState(FORM_MODE.READ);
                        this.FillForm(this.curr_serial);
                        this.btnAdd.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }

            if(this.form_mode == FORM_MODE.EDIT)
            {
                if (this.ValidateData(this.tmp_serial) == false)
                    return;

                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        var serial_to_update = sn.serial.Find(this.tmp_serial.id);

                        if (serial_to_update == null)
                        {
                            MessageAlert.Show("ข้อมูลที่ท่านต้องการแก้ไขไม่มีอยู่ในระบบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            return;
                        }
                        else
                        {
                            serial_to_update.oldnum = this.tmp_serial.oldnum;
                            serial_to_update.version = this.tmp_serial.version;
                            serial_to_update.contact = this.tmp_serial.contact;
                            serial_to_update.position = this.tmp_serial.position;
                            serial_to_update.prenam = this.tmp_serial.prenam;
                            serial_to_update.compnam = this.tmp_serial.compnam;
                            serial_to_update.addr01 = this.tmp_serial.addr01;
                            serial_to_update.addr02 = this.tmp_serial.addr02;
                            serial_to_update.addr03 = this.tmp_serial.addr03;
                            serial_to_update.zipcod = this.tmp_serial.zipcod;
                            serial_to_update.telnum = this.tmp_serial.telnum;
                            serial_to_update.faxnum = this.tmp_serial.faxnum;
                            serial_to_update.busides = this.tmp_serial.busides;
                            serial_to_update.purdat = this.tmp_serial.purdat;
                            serial_to_update.expdat = this.tmp_serial.expdat;
                            serial_to_update.branch = this.tmp_serial.branch;
                            serial_to_update.manual = this.tmp_serial.manual;
                            serial_to_update.upfree = this.tmp_serial.upfree;
                            serial_to_update.refnum = this.tmp_serial.refnum;
                            serial_to_update.remark = this.tmp_serial.remark;
                            serial_to_update.dealer_id = this.tmp_serial.dealer_id;
                            serial_to_update.verextdat = this.tmp_serial.verextdat;
                            serial_to_update.area_id = this.tmp_serial.area_id;
                            serial_to_update.busityp_id = this.tmp_serial.busityp_id;
                            serial_to_update.howknown_id = this.tmp_serial.howknown_id;
                            serial_to_update.verext_id = this.tmp_serial.verext_id;
                            serial_to_update.creby_id = this.tmp_serial.creby_id;
                            serial_to_update.credat = this.tmp_serial.credat;
                            serial_to_update.chgby_id = this.main_form.loged_in_user.id;
                            serial_to_update.chgdat = DateTime.Now;

                            sn.SaveChanges();
                            this.curr_serial = this.GetSerial(this.tmp_serial.id);
                            this.ResetFormState(FORM_MODE.READ);
                            this.FillForm(this.curr_serial);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                this.curr_serial = sn.serial
                                    .Include("istab")
                                    .Include("istab1")
                                    .Include("istab2")
                                    .Include("istab3")
                                    .Include("dealer")
                                    .Include("users")
                                    .Include("users1")
                                    .Include("cloud_srv")
                                    .Include("ma")
                                    .Include("problem")
                                    .Include("serial_password")
                                    .Where(s => s.flag == 0)
                                    .OrderBy(s => s.sernum).FirstOrDefault();

                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripPrevious_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                var sr = sn.serial
                        .Include("istab")
                        .Include("istab1")
                        .Include("istab2")
                        .Include("istab3")
                        .Include("dealer")
                        .Include("users")
                        .Include("users1")
                        .Include("cloud_srv")
                        .Include("ma")
                        .Include("problem")
                        .Include("serial_password")
                        .Where(s => s.flag == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0)
                        .OrderByDescending(s => s.sernum).FirstOrDefault();

                if(sr != null)
                {
                    this.curr_serial = sr;
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                var sr = sn.serial
                        .Include("istab")
                        .Include("istab1")
                        .Include("istab2")
                        .Include("istab3")
                        .Include("dealer")
                        .Include("users")
                        .Include("users1")
                        .Include("cloud_srv")
                        .Include("ma")
                        .Include("problem")
                        .Include("serial_password")
                        .Where(s => s.flag == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0)
                        .OrderBy(s => s.sernum).FirstOrDefault();

                if(sr != null)
                {
                    this.curr_serial = sr;
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                this.curr_serial = sn.serial
                                    .Include("istab")
                                    .Include("istab1")
                                    .Include("istab2")
                                    .Include("istab3")
                                    .Include("dealer")
                                    .Include("users")
                                    .Include("users1")
                                    .Include("cloud_srv")
                                    .Include("ma")
                                    .Include("problem")
                                    .Include("serial_password")
                                    .Where(s => s.flag == 0)
                                    .OrderByDescending(s => s.sernum).FirstOrDefault();

                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.dgvProblem.Focus();
            this.ResetFormState(FORM_MODE.READ_ITEM);
        }

        private void toolStripImport_Click(object sender, EventArgs e)
        {

        }

        private void toolStripGenSN_Click(object sender, EventArgs e)
        {

        }

        private void toolStripUpgrade_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBook_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSet2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSearch_ButtonClick(object sender, EventArgs e)
        {
            this.btnSearchSN.PerformClick();
        }

        private void toolStripInquiryAll_Click(object sender, EventArgs e)
        {
            DialogInquirySn inq;
            switch (this.sort_by)
            {
                case DialogInquirySn.SORT_BY.SERNUM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM);
                    break;
                case DialogInquirySn.SORT_BY.CONTACT:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.CONTACT);
                    break;
                case DialogInquirySn.SORT_BY.COMPNAM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.COMPNAM);
                    break;
                case DialogInquirySn.SORT_BY.DEALER:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.DEALER);
                    break;
                case DialogInquirySn.SORT_BY.OLDNUM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.OLDNUM);
                    break;
                case DialogInquirySn.SORT_BY.BUSITYP:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.BUSITYP);
                    break;
                case DialogInquirySn.SORT_BY.AREA:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.AREA);
                    break;
                default:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM);
                    break;
            }

            if (inq.ShowDialog() == DialogResult.OK)
            {
                var ser = this.GetSerial(inq.selected_serial.id);
                if (ser == null)
                {
                    MessageAlert.Show("ค้นหาข้อมูลไม่พบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                this.curr_serial = ser;
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripInquiryRest_Click(object sender, EventArgs e)
        {
            DialogInquirySn inq;
            switch (this.sort_by)
            {
                case DialogInquirySn.SORT_BY.SERNUM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.CONTACT:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.CONTACT, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.COMPNAM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.COMPNAM, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.DEALER:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.DEALER, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.OLDNUM:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.OLDNUM, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.BUSITYP:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.BUSITYP, this.curr_serial);
                    break;
                case DialogInquirySn.SORT_BY.AREA:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.AREA, this.curr_serial);
                    break;
                default:
                    inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM, this.curr_serial);
                    break;
            }

            if (inq.ShowDialog() == DialogResult.OK)
            {
                var ser = this.GetSerial(inq.selected_serial.id);
                if(ser == null)
                {
                    MessageAlert.Show("ค้นหาข้อมูลไม่พบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                this.curr_serial = ser;
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripInquiryMA_Click(object sender, EventArgs e)
        {
            DialogInquirySn inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM, this.curr_serial, DialogInquirySn.INQUIRY_FILTER.MA);

            if (inq.ShowDialog() == DialogResult.OK)
            {
                var ser = this.GetSerial(inq.selected_serial.id);
                if (ser == null)
                {
                    MessageAlert.Show("ค้นหาข้อมูลไม่พบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                this.curr_serial = ser;
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripInquiryCloud_Click(object sender, EventArgs e)
        {
            DialogInquirySn inq = new DialogInquirySn(DialogInquirySn.SORT_BY.SERNUM, this.curr_serial, DialogInquirySn.INQUIRY_FILTER.CLOUD);

            if (inq.ShowDialog() == DialogResult.OK)
            {
                var ser = this.GetSerial(inq.selected_serial.id);
                if (ser == null)
                {
                    MessageAlert.Show("ค้นหาข้อมูลไม่พบ, อาจมีผู้ใช้รายอื่นลบออกไปแล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                this.curr_serial = ser;
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripSearchSN_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(true, null, "S/N", this.curr_serial.sernum);
            if(search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.SERNUM;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                .Where(s => s.sernum.CompareTo(search.keyword) >= 0)
                                .OrderBy(s => s.sernum)
                                .FirstOrDefault();

                    if(sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if(sn_result.sernum.CompareTo(search.keyword) != 0)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_serial = this.GetSerial(sn_result.id);
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripSearchContact_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Contact", this.curr_serial.contact);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.CONTACT;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.OrderBy(s => s.contact).Where(s => s.flag == 0)
                                .Where(s => s.contact.CompareTo(search.keyword) >= 0)
                                .OrderBy(s => s.contact)
                                .ThenBy(s => s.sernum)
                                .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (sn_result.contact.ToLower().CompareTo(search.keyword.ToLower()) != 0)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_serial = this.GetSerial(sn_result.id);
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripSearchCompany_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Comp. Name", this.curr_serial.compnam);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.COMPNAM;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                .Where(s => s.compnam.CompareTo(search.keyword) >= 0)
                                .OrderBy(s => s.compnam)
                                .ThenBy(s => s.sernum)
                                .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (sn_result.compnam.ToLower().CompareTo(search.keyword.ToLower()) != 0)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_serial = this.GetSerial(sn_result.id);
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripSearchDealer_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Dealer", this.curr_serial.dealer.dealercod);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.DEALER;
                
                List<SerialId> ids = DialogInquirySn.GetSerialIdList(DialogInquirySn.SORT_BY.DEALER, DialogInquirySn.INQUIRY_FILTER.ALL);
                ids = ids.Where(s => s.dealercod != null).Where(s => s.dealercod.CompareTo(search.keyword) >= 0).ToList();

                if (ids.Count == 0)
                {
                    MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    return;
                }

                if(ids.First().dealercod.CompareTo(search.keyword) > 0)
                {
                    if(MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                    {
                        return;
                    }
                }

                this.curr_serial = this.GetSerial(ids.First().id);
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripSearchOldnum_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(true, null, "Old S/N", this.curr_serial.oldnum);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.OLDNUM;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                .Where(s => s.oldnum.CompareTo(search.keyword) >= 0)
                                .OrderBy(s => s.oldnum)
                                .ThenBy(s => s.sernum)
                                .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (sn_result.oldnum.CompareTo(search.keyword) != 0)
                    {
                        if (MessageAlert.Show("ค้นหาไม่พบ, ต้องการข้อมูลถัดไปหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                        {
                            return;
                        }
                    }

                    this.curr_serial = this.GetSerial(sn_result.id);
                    this.FillForm(this.curr_serial);
                }
            }
        }

        private void toolStripSearchBusityp_Click(object sender, EventArgs e)
        {
            this.sort_by = DialogInquirySn.SORT_BY.BUSITYP;
        }

        private void toolStripSearchArea_Click(object sender, EventArgs e)
        {
            this.sort_by = DialogInquirySn.SORT_BY.AREA;
        }

        private void toolStripReload_Click(object sender, EventArgs e)
        {

        }

        /* Exclusive for admin. start */
        private void chkIMOnly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLostRenew_Click(object sender, EventArgs e)
        {

        }

        private void btnCD_Click(object sender, EventArgs e)
        {

        }

        private void btnUP_Click(object sender, EventArgs e)
        {

        }

        private void btnUPNewRwt_Click(object sender, EventArgs e)
        {

        }

        private void btnUPNewRwtJob_Click(object sender, EventArgs e)
        {

        }
        /**************/


        /* Password start */
        private void btnPasswordAdd_Click(object sender, EventArgs e)
        {
            DialogPasswordSn pwd = new DialogPasswordSn();
            if(pwd.ShowDialog() == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        serial_password p = new serial_password
                        {
                            serial_id = this.curr_serial.id,
                            pass_word = pwd.password,
                            creby_id = this.main_form.loged_in_user.id,
                            credat = DateTime.Now,
                            flag = 0
                        };
                        sn.serial_password.Add(p);
                        sn.SaveChanges();

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void btnPasswordRemove_Click(object sender, EventArgs e)
        {
            if (this.curr_serial.serial_password.AsEnumerable().Count() == 0)
                return;

            //serial_password serial_password = (serial_password)this.dgvPassword.Rows[this.dgvPassword.CurrentCell.RowIndex].Cells[this.col_password_serial_password.Name].Value;
            string pass = (string)this.dgvPassword.Rows[this.dgvPassword.CurrentCell.RowIndex].Cells[this.col_password_password.Name].Value;
            int id = (int)this.dgvPassword.Rows[this.dgvPassword.CurrentCell.RowIndex].Cells[this.col_password_id.Name].Value;

            if (MessageAlert.Show("ลบรหัสผ่าน '" + pass + "' , ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        serial_password pwd_to_remove = sn.serial_password.Where(s => s.flag == 0).Where(s => s.id == id).FirstOrDefault();
                        if (pwd_to_remove != null)
                        {
                            sn.serial_password.Remove(pwd_to_remove);
                            sn.SaveChanges();
                        }

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }
        /**************/


        /* MA start */
        private void btnEditMA_Click(object sender, EventArgs e)
        {
            DateTime? date_from = this.curr_serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.ma.Where(m => m.flag == 0).First().start_date : null;
            DateTime? date_to = this.curr_serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.ma.Where(m => m.flag == 0).First().end_date : null;
            string email = this.curr_serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.ma.Where(m => m.flag == 0).First().email : string.Empty;

            DialogMaCloud dma = new DialogMaCloud(date_from, date_to, email);
            if(dma.ShowDialog() == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        ma ma = sn.ma.Where(m => m.flag == 0).Where(m => m.serial_id == this.curr_serial.id).FirstOrDefault();
                        if (ma != null) // update
                        {
                            ma.start_date = dma.date_from;
                            ma.end_date = dma.date_to;
                            ma.email = dma.email;
                            ma.chgby_id = this.main_form.loged_in_user.id;
                            ma.chgdat = DateTime.Now;
                            sn.SaveChanges();
                        }
                        else // add
                        {
                            ma tmp_ma = new ma
                            {
                                serial_id = this.curr_serial.id,
                                start_date = dma.date_from,
                                end_date = dma.date_to,
                                email = dma.email,
                                creby_id = this.main_form.loged_in_user.id,
                                credat = DateTime.Now,
                                flag = 0
                            };
                            sn.ma.Add(tmp_ma);
                            sn.SaveChanges();
                        }

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void btnDeleteMA_Click(object sender, EventArgs e)
        {
            if(MessageAlert.Show("ลบรายละเอียดการให้บริการ MA, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        ma ma_to_remove = sn.ma.Where(m => m.flag == 0).Where(m => m.serial_id == this.curr_serial.id).FirstOrDefault();
                        if(ma_to_remove != null)
                        {
                            sn.ma.Remove(ma_to_remove);
                            sn.SaveChanges();
                        }

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }
        /*************/


        /* CLOUD start */
        private void btnEditCloud_Click(object sender, EventArgs e)
        {
            DateTime? date_from = this.curr_serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.cloud_srv.Where(c => c.flag == 0).First().start_date : null;
            DateTime? date_to = this.curr_serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.cloud_srv.Where(c => c.flag == 0).First().end_date : null;
            string email = this.curr_serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0 ? this.curr_serial.cloud_srv.Where(c => c.flag == 0).First().email : string.Empty;

            DialogMaCloud dma = new DialogMaCloud(date_from, date_to, email);
            if (dma.ShowDialog() == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        cloud_srv cl = sn.cloud_srv.Where(c => c.flag == 0).Where(c => c.serial_id == this.curr_serial.id).FirstOrDefault();
                        if (cl != null) // update
                        {
                            cl.start_date = dma.date_from;
                            cl.end_date = dma.date_to;
                            cl.email = dma.email;
                            cl.chgby_id = this.main_form.loged_in_user.id;
                            cl.chgdat = DateTime.Now;
                            sn.SaveChanges();
                        }
                        else // add
                        {
                            cloud_srv tmp_cl = new cloud_srv
                            {
                                serial_id = this.curr_serial.id,
                                start_date = dma.date_from,
                                end_date = dma.date_to,
                                email = dma.email,
                                creby_id = this.main_form.loged_in_user.id,
                                credat = DateTime.Now,
                                flag = 0
                            };
                            sn.cloud_srv.Add(tmp_cl);
                            sn.SaveChanges();
                        }

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }

        private void btnDeleteCloud_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("ลบรายละเอียดการให้บริการ Cloud, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        cloud_srv cl_to_remove = sn.cloud_srv.Where(m => m.flag == 0).Where(m => m.serial_id == this.curr_serial.id).FirstOrDefault();
                        if (cl_to_remove != null)
                        {
                            sn.cloud_srv.Remove(cl_to_remove);
                            sn.SaveChanges();
                        }

                        this.curr_serial = this.GetSerial(this.curr_serial.id);
                        this.FillForm(this.curr_serial);
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                }
            }
        }
        /*************/

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.form_mode != FORM_MODE.READ && this.focused_control != null)
            {
                e.Cancel = true;
                this.focused_control.Focus();
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.form_mode != FORM_MODE.READ)
            {
                this.focused_control.Focus();
            }
        }

        private void mskSernum__Leave(object sender, EventArgs e)
        {
            if(ValidateSN.Check(this.tmp_serial.sernum) == false)
            {
                this.mskSernum.Focus();
                MessageAlert.Show("กรุณาป้อน S/N ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
            }
        }

        private void mskSernum__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.sernum = ((XTextEditMasked)sender)._Text;
        }

        private void txtVersion__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.version = ((XTextEdit)sender)._Text;
        }

        private void mskRefSn__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.refnum = ((XTextEditMasked)sender)._Text;
        }

        private void txtPrenam__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.prenam = ((XTextEdit)sender)._Text;
        }

        private void txtCompnam__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.compnam = ((XTextEdit)sender)._Text;
        }

        private void txtAddr01__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.addr01 = ((XTextEdit)sender)._Text;
        }

        private void txtAddr02__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.addr02 = ((XTextEdit)sender)._Text;
        }

        private void txtAddr03__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.addr03 = ((XTextEdit)sender)._Text;
        }

        private void txtZipcod__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.zipcod = ((XNumTextEdit)sender)._Text;
        }

        private void txtTelnum__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.telnum = ((XTextEdit)sender)._Text;
        }

        private void txtFaxnum__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.faxnum = ((XTextEdit)sender)._Text;
        }

        private void txtContact__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.contact = ((XTextEdit)sender)._Text;
        }

        private void txtPosition__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.position = ((XTextEdit)sender)._Text;
        }

        private void mskOldSn__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.oldnum = ((XTextEditMasked)sender)._Text;
        }

        private void txtRemark__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.remark = ((XTextEdit)sender)._Text;
        }

        private void txtBusides__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.busides = ((XTextEdit)sender)._Text;
        }

        private void dtPurdat__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.purdat = ((XDatePicker)sender)._SelectedDate;
        }

        private void dtExpdat__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.expdat = ((XDatePicker)sender)._SelectedDate;
        }

        private void dlVerext__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.verext_id = (int)((XDropdownListItem)((XDropdownList)sender)._SelectedItem).Value;
        }

        private void txtUpfree__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.upfree = ((XTextEdit)sender)._Text;
        }

        private void dtManual__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.manual = ((XDatePicker)sender)._SelectedDate;
        }

        private void dtVerextDat__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.verextdat = ((XDatePicker)sender)._SelectedDate;
        }

        private void brArea__Leave(object sender, EventArgs e)
        {

        }

        private void brBusityp__Leave(object sender, EventArgs e)
        {

        }

        private void brDealer__Leave(object sender, EventArgs e)
        {

        }

        private void brHowknown__Leave(object sender, EventArgs e)
        {

        }
    }
}
