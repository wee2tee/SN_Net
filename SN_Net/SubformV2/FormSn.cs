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
        private problem tmp_problem;
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

        private void FormSn_Load(object sender, EventArgs e)
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
            this.HideInlineForm();
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

            this.btnAddProblem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEditProblem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDeleteProblem.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
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
                this.brArea._Text = serial.area; //serial.istab != null ? serial.istab.typcod : string.Empty;
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
                this.brBusityp._Text = serial.busityp; //serial.istab1 != null ? serial.istab1.typcod : string.Empty;
                this.lblBusityp.Text = serial.istab1 != null ? serial.istab1.typdes_th : string.Empty;
                this.brDealer._Text = serial.dealercod; //serial.dealer != null ? serial.dealer.dealercod : string.Empty;
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
                if (serial.ma.Where(m => m.flag == 0).AsEnumerable().Count() > 0)
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
                if (serial.cloud_srv.Where(c => c.flag == 0).AsEnumerable().Count() > 0)
                {
                    DateTime expire_date = serial.cloud_srv.Where(c => c.flag == 0).First().end_date.Value;
                    this.lblCloudExpireWarning.Visible = expire_date.CompareTo(DateTime.Now.AddDays(15)) <= 0 ? true : false;
                }
                else
                {
                    this.lblCloudExpireWarning.Visible = false;
                }

                this.password_list = null;
                this.password_list = new BindingList<serialPasswordVM>(serial.serial_password.AsEnumerable().ToViewModel());
                this.dgvPassword.DataSource = this.password_list;

                this.problem_list = null;
                this.problem_list = new BindingList<problemVM>(serial.problem.OrderBy(p => p.date).ThenBy(p => p.time).ToViewModel());
                this.dgvProblem.DataSource = this.problem_list;

                /* Set Toolstrip Button State in Case No Data*/
                if (this.form_mode == FORM_MODE.READ)
                {
                    if (serial.id == -1)
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

                /* Set Problem Button State in Case No Problem Data */
                //if(this.form_mode == FORM_MODE.READ_ITEM)
                //{
                //    if(serial.problem.Count == 0)
                //    {
                //        this.btnEditProblem.Enabled = false;
                //        this.btnDeleteProblem.Enabled = false;
                //    }
                //}
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

        private void PerformEdit(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ)
            {
                this.btnEdit.PerformClick();
                ((Control)sender).Focus();
            }
        }

        private void KeepFocusedControl(object sender, EventArgs e)
        {
            this.focused_control = (Control)sender;
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
                            //.Include("problem.istab")
                            .Include("serial_password")
                            .Where(s => s.flag == 0 && s.id == id)
                            .FirstOrDefault();

                return serial;
            }
        }

        private bool ValidateData(serial serial)
        {
            if (ValidateSN.Check(serial.sernum) == false)
            {
                MessageAlert.Show("กรุณาป้อน S/N ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.mskSernum.Focus();
                return false;
            }

            if (serial.refnum.Replace("-", "").Trim().Length > 0 && ValidateSN.Check(serial.refnum) == false)
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

            if (serial.version.Trim().Length == 0)
            {
                MessageAlert.Show("กรุณาระบุ Version ให้ถูกต้อง", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                this.txtVersion.Focus();
                return false;
            }

            return true;
        }

        private void HideInlineForm()
        {
            this.inlineDate.SetBounds(-99999, 0, 0, 0);
            this.inlineName.SetBounds(-99999, 0, 0, 0);
            this.inlineProbcod.SetBounds(-99999, 0, 0, 0);
            this.inlineProbdesc.SetBounds(-99999, 0, 0, 0);
            this.tmp_problem = null;
        }

        private void ShowInlineForm(DataGridViewRow row)
        {
            if (row == null)
                return;

            this.tmp_problem = (problem)row.Cells[this.col_problem_problem.Name].Value;
            //using (snEntities sn = DBX.DataSet())
            //{
            //    this.tmp_problem.istab = sn.istab.Find(this.tmp_problem.probcod_id);
            //}

            this.SetInlineControlBound(row);
            this.inlineDate._SelectedDate = this.tmp_problem.date;
            this.inlineName._Text = this.tmp_problem.name;
            this.inlineProbcod._Text = this.tmp_problem.ToViewModel().probcod;
            this.inlineProbdesc.SetText(this.tmp_problem.probdesc);
        }

        private void SetInlineControlBound(DataGridViewRow row)
        {
            int col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_problem_date.Name).First().Index;
            this.inlineDate.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_problem_name.Name).First().Index;
            this.inlineName.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_problem_probcod.Name).First().Index;
            this.inlineProbcod.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_problem_probdesc.Name).First().Index;
            this.inlineProbdesc.SetInlineControlPosition(row.DataGridView, row.Index, col_index);
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                this.tmp_serial = sn.CreateTmpSerial(this.main_form);
                if (this.curr_serial != null)
                {
                    this.tmp_serial.istab = this.curr_serial.istab;
                    this.tmp_serial.area_id = this.curr_serial.area_id;
                    this.tmp_serial.area = this.curr_serial.area;

                    this.tmp_serial.prenam = this.curr_serial.prenam;
                    this.tmp_serial.compnam = this.curr_serial.compnam;
                    this.tmp_serial.addr01 = this.curr_serial.addr01;
                    this.tmp_serial.addr02 = this.curr_serial.addr02;
                    this.tmp_serial.addr03 = this.curr_serial.addr03;
                    this.tmp_serial.zipcod = this.curr_serial.zipcod;
                    this.tmp_serial.telnum = this.curr_serial.telnum;
                    this.tmp_serial.faxnum = this.curr_serial.faxnum;
                    this.tmp_serial.contact = this.curr_serial.contact;
                    this.tmp_serial.position = this.curr_serial.position;
                    this.tmp_serial.remark = this.curr_serial.remark;
                    this.tmp_serial.busides = this.curr_serial.busides;
                    this.tmp_serial.istab1 = this.curr_serial.istab1;
                    this.tmp_serial.busityp_id = this.curr_serial.busityp_id;
                    this.tmp_serial.busityp = this.curr_serial.busityp;
                    this.tmp_serial.dealer = this.curr_serial.dealer;
                    this.tmp_serial.dealer_id = this.curr_serial.dealer_id;
                    this.tmp_serial.dealercod = this.curr_serial.dealercod;
                    this.tmp_serial.howknown_id = this.curr_serial.howknown_id;
                    this.tmp_serial.verext_id = this.curr_serial.verext_id;

                }
                this.ResetFormState(FORM_MODE.ADD);
                this.FillForm(this.tmp_serial);
                this.mskSernum.Focus();
            }
        }

        private void toolStripEdit_Click(object sender, EventArgs e)
        {
            this.tmp_serial = this.GetSerial(this.curr_serial.id);

            if (this.tmp_serial != null)
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
                    if (sn_to_delete == null)
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
            if (this.form_mode == FORM_MODE.READ_ITEM)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.dgvProblem.Refresh();
                return;
            }

            if (this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
            {
                this.ResetFormState(FORM_MODE.READ);
                this.FillForm(this.curr_serial);
                this.tmp_serial = null;
            }

            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                var adding_row = ((BindingList<problemVM>)this.dgvProblem.DataSource).Where(i => i.id == -1).FirstOrDefault();
                this.ResetFormState(FORM_MODE.READ_ITEM);
                if(adding_row != null)
                {
                    ((BindingList<problemVM>)this.dgvProblem.DataSource).Remove(adding_row);
                }

                this.HideInlineForm();
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if(this.form_mode == FORM_MODE.ADD || this.form_mode == FORM_MODE.EDIT)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && d.id == this.tmp_serial.dealer_id).FirstOrDefault();
                    var area = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_serial.area_id).FirstOrDefault();
                    var busityp = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_serial.busityp_id).FirstOrDefault();
                    var howknown = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_serial.howknown_id).FirstOrDefault();
                    var verext = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_serial.verext_id).FirstOrDefault();

                    if (this.form_mode == FORM_MODE.ADD)
                    {
                        if (this.ValidateData(this.tmp_serial) == false)
                            return;

                        try
                        {
                            this.tmp_serial.creby_id = this.main_form.loged_in_user.id;
                            this.tmp_serial.credat = DateTime.Now;
                            this.tmp_serial.dealer_id = dealer != null ? (int?)dealer.id : null;
                            this.tmp_serial.dealercod = dealer != null ? dealer.dealercod : string.Empty;
                            this.tmp_serial.area_id = area != null ? (int?)area.id : null;
                            this.tmp_serial.area = area != null ? area.typcod : string.Empty;
                            this.tmp_serial.busityp_id = busityp != null ? (int?)busityp.id : null;
                            this.tmp_serial.busityp = busityp != null ? busityp.typcod : string.Empty;
                            this.tmp_serial.howknown_id = howknown != null ? (int?)howknown.id : null;
                            this.tmp_serial.verext_id = verext != null ? (int?)verext.id : null;

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

                    if (this.form_mode == FORM_MODE.EDIT)
                    {
                        if (this.ValidateData(this.tmp_serial) == false)
                            return;

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
                                serial_to_update.dealer_id = dealer != null ? (int?)dealer.id : null;
                                serial_to_update.dealercod = dealer != null ? dealer.dealercod : string.Empty;
                                serial_to_update.verextdat = this.tmp_serial.verextdat;
                                serial_to_update.area_id = area != null ? (int?)area.id : null;
                                serial_to_update.area = area != null ? area.typcod : string.Empty;
                                serial_to_update.busityp_id = busityp != null ? (int?)busityp.id : null;
                                serial_to_update.busityp = busityp != null ? busityp.typcod : string.Empty;
                                serial_to_update.howknown_id = howknown != null ? (int?)howknown.id : null;
                                serial_to_update.verext_id = verext != null ? (int?)verext.id : null;
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

            if(this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var prob = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_problem.probcod_id).FirstOrDefault();
                    if(prob == null)
                    {
                        this.inlineProbcod.Focus();
                        SendKeys.Send("{F6}");
                        return;
                    }

                    if(this.tmp_problem.id == -1) // Add problem
                    {
                        try
                        {
                            this.tmp_problem.credat = DateTime.Now;
                            this.tmp_problem.time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));
                            sn.problem.Add(this.tmp_problem);
                            sn.SaveChanges();

                            this.HideInlineForm();
                            this.ResetFormState(FORM_MODE.READ_ITEM);
                            this.btnAddProblem.PerformClick();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("==> " + ex.Message);
                        }
                    }
                    else // Edit problem
                    {
                        var prob_to_update = sn.problem.Where(p => p.flag == 0 && p.id == this.tmp_problem.id).FirstOrDefault();

                        if(prob_to_update != null) // update existing
                        {
                            prob_to_update.date = this.tmp_problem.date;
                            prob_to_update.time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));
                            prob_to_update.name = this.tmp_problem.name;
                            prob_to_update.probcod_id = this.tmp_problem.probcod_id;
                            prob_to_update.probdesc = this.tmp_problem.probdesc;
                            prob_to_update.chgby_id = this.main_form.loged_in_user.id;
                            prob_to_update.chgdat = DateTime.Now;
                            sn.SaveChanges();
                        }
                        else // otherwise will add new
                        {
                            this.tmp_problem.credat = DateTime.Now;
                            this.tmp_problem.time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH"));
                            sn.problem.Add(this.tmp_problem);
                            sn.SaveChanges();
                        }
                    }
                }
            }
        }

        private void toolStripFirst_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                SerialId sid = null;
                switch (this.sort_by)
                {
                    case DialogInquirySn.SORT_BY.SERNUM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.CONTACT:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.contact).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.COMPNAM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.compnam).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.DEALER:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.dealercod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.OLDNUM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.oldnum).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.BUSITYP:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.busityp).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.AREA:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.area).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    default:
                        sid = sn.serial.Where(s => s.flag == 0).OrderBy(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                }

                if (sid == null)
                    return;

                this.curr_serial = this.GetSerial(sid.id);
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripPrevious_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                if(this.curr_serial == null)
                {
                    this.btnFirst.PerformClick();
                    return;
                }

                SerialId sid = null;
                switch (this.sort_by)
                {
                    case DialogInquirySn.SORT_BY.SERNUM:
                        sid = sn.serial.Where(s => s.flag == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0).OrderByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.CONTACT:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.contact.CompareTo(this.curr_serial.contact) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.contact.CompareTo(this.curr_serial.contact) < 0))).OrderByDescending(s => s.contact).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, contact = s.contact, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.COMPNAM:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.compnam.CompareTo(this.curr_serial.compnam) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.compnam.CompareTo(this.curr_serial.compnam) < 0))).OrderByDescending(s => s.compnam).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, compnam = s.compnam, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.DEALER:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.dealercod.CompareTo(this.curr_serial.dealercod) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.dealercod.CompareTo(this.curr_serial.dealercod) < 0))).OrderByDescending(s => s.dealercod).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, dealercod = s.dealercod, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.OLDNUM:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.oldnum.CompareTo(this.curr_serial.oldnum) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.oldnum.CompareTo(this.curr_serial.oldnum) < 0))).OrderByDescending(s => s.oldnum).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, oldnum = s.oldnum, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.BUSITYP:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.busityp.CompareTo(this.curr_serial.busityp) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.busityp.CompareTo(this.curr_serial.busityp) < 0))).OrderByDescending(s => s.busityp).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, busityp = s.busityp, sernum = s.sernum }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.AREA:
                        sid = sn.serial.Where(s => s.flag == 0 && ((s.area.CompareTo(this.curr_serial.area) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0) || (s.area.CompareTo(this.curr_serial.area) < 0))).OrderByDescending(s => s.area).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, area = s.area, sernum = s.sernum }).FirstOrDefault();
                        break;
                    default:
                        sid = sn.serial.Where(s => s.flag == 0 && s.sernum.CompareTo(this.curr_serial.sernum) < 0).OrderByDescending(s => s.sernum).Select(s => new SerialId { id = s.id, sernum = s.sernum }).FirstOrDefault();
                        break;
                }

                if (sid == null)
                {
                    this.btnFirst.PerformClick();
                    return;
                }

                this.curr_serial = this.GetSerial(sid.id);
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripNext_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (this.curr_serial == null)
                    {
                        this.btnLast.PerformClick();
                        return;
                    }

                    SerialId sid = null;
                    string sql = string.Empty;
                    switch (this.sort_by)
                    {
                        case DialogInquirySn.SORT_BY.SERNUM:
                            sid = sn.serial.Where(s => s.flag == 0).Where(s => s.sernum.CompareTo(this.curr_serial.sernum) > 0).OrderBy(s => s.sernum).Take(1).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.CONTACT:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.contact.CompareTo(this.curr_serial.contact) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.contact.CompareTo(this.curr_serial.contact) > 0))).OrderBy(s => s.contact).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, contact = s.contact, sernum = s.sernum }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.COMPNAM:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.compnam.CompareTo(this.curr_serial.compnam) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.compnam.CompareTo(this.curr_serial.compnam) > 0))).OrderBy(s => s.compnam).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, compnam = s.compnam, sernum = s.sernum }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.DEALER:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.dealercod.CompareTo(this.curr_serial.dealercod) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.dealercod.CompareTo(this.curr_serial.dealercod) > 0))).OrderBy(s => s.dealercod).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, dealercod = s.dealercod, sernum = s.sernum }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.OLDNUM:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.oldnum.CompareTo(this.curr_serial.oldnum) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.oldnum.CompareTo(this.curr_serial.oldnum) > 0))).OrderBy(s => s.oldnum).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, oldnum = s.oldnum, sernum = s.sernum }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.BUSITYP:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.busityp.CompareTo(this.curr_serial.busityp) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.busityp.CompareTo(this.curr_serial.busityp) > 0))).OrderBy(s => s.busityp).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, busityp = s.busityp, sernum = s.sernum }).FirstOrDefault();
                            break;
                        case DialogInquirySn.SORT_BY.AREA:
                            sid = sn.serial.Where(s => s.flag == 0 && ((s.area.CompareTo(this.curr_serial.area) == 0 && s.sernum.CompareTo(this.curr_serial.sernum) > 0) || (s.area.CompareTo(this.curr_serial.area) > 0))).OrderBy(s => s.area).ThenBy(s => s.sernum).Select(s => new SerialId { id = s.id, area = s.area, sernum = s.sernum }).FirstOrDefault();
                            break;
                        default:
                            sid = sn.serial.Where(s => s.flag == 0).Where(s => s.sernum.CompareTo(this.curr_serial.sernum) > 0).OrderBy(s => s.sernum).Take(1).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                            break;
                    }

                    if (sid == null)
                    {
                        this.btnLast.PerformClick();
                        return;
                    }

                    this.curr_serial = this.GetSerial(sid.id);
                    this.FillForm(this.curr_serial);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void toolStripLast_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                SerialId sid = null;
                switch (this.sort_by)
                {
                    case DialogInquirySn.SORT_BY.SERNUM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.CONTACT:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.contact).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.COMPNAM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.compnam).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.DEALER:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.dealercod).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.OLDNUM:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.oldnum).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.BUSITYP:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.busityp).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    case DialogInquirySn.SORT_BY.AREA:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.area).ThenByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                    default:
                        sid = sn.serial.Where(s => s.flag == 0).OrderByDescending(s => s.sernum).Select(s => new SerialId { id = s.id }).FirstOrDefault();
                        break;
                }

                if (sid == null)
                    return;

                this.curr_serial = this.GetSerial(sid.id);
                this.FillForm(this.curr_serial);
            }
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.dgvProblem.Focus();
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.dgvProblem.Refresh();
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
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                .Where(s => String.Compare(s.contact, search.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                .OrderBy(s => s.contact)
                                .ThenBy(s => s.sernum)
                                .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (String.CompareOrdinal(sn_result.contact, search.keyword) != 0)
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
                                .Where(s => String.Compare(s.compnam, search.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                .OrderBy(s => s.compnam)
                                .ThenBy(s => s.sernum)
                                .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (String.CompareOrdinal(sn_result.compnam, search.keyword) != 0)
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
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Dealer Code", this.curr_serial.ToViewModel().dealer);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.DEALER;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                    .Where(s => String.Compare(s.dealercod, search.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .OrderBy(s => s.dealercod)
                                    .ThenBy(s => s.sernum)
                                    .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (String.CompareOrdinal(sn_result.dealercod, search.keyword) != 0)
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
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Business Type", this.curr_serial.ToViewModel().busityp);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.BUSITYP;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                    .Where(s => String.Compare(s.busityp, search.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .OrderBy(s => s.busityp)
                                    .ThenBy(s => s.sernum)
                                    .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (String.CompareOrdinal(sn_result.busityp, search.keyword) != 0)
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

        private void toolStripSearchArea_Click(object sender, EventArgs e)
        {
            DialogSimpleSearch search = new DialogSimpleSearch(false, null, "Area", this.curr_serial.ToViewModel().area);
            if (search.ShowDialog() == DialogResult.OK)
            {
                this.sort_by = DialogInquirySn.SORT_BY.AREA;

                using (snEntities sn = DBX.DataSet())
                {
                    var sn_result = sn.serial.Where(s => s.flag == 0)
                                    .Where(s => String.Compare(s.area, search.keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .OrderBy(s => s.area)
                                    .ThenBy(s => s.sernum)
                                    .FirstOrDefault();

                    if (sn_result == null)
                    {
                        MessageAlert.Show("ค้นหาไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    if (String.CompareOrdinal(sn_result.area, search.keyword) != 0)
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

        private void toolStripReload_Click(object sender, EventArgs e)
        {
            if (this.curr_serial == null)
            {
                this.btnLast.PerformClick();
                return;
            }

            if(this.curr_serial != null)
            {
                var ser = this.GetSerial(this.curr_serial.id);
                if(ser != null)
                {
                    this.curr_serial = ser;
                    this.FillForm(this.curr_serial);
                }
                else
                {
                    this.btnNext.PerformClick();
                }
            }
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
            if (this.form_mode != FORM_MODE.READ && this.focused_control != null)
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
            if (this.tmp_serial == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if(str.Trim().Length == 0)
            {
                this.tmp_serial.istab = null;
                this.tmp_serial.area_id = null;
                this.tmp_serial.area = string.Empty;
                this.lblArea.Text = string.Empty;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var area = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_AREA && i.typcod == str).FirstOrDefault();
                    if(area != null)
                    {
                        this.tmp_serial.area_id = area.id;
                        this.tmp_serial.area = area.typcod;
                        this.tmp_serial.istab = area;
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

        private void brBusityp__Leave(object sender, EventArgs e)
        {
            if (this.tmp_serial == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
            {
                this.tmp_serial.istab1 = null;
                this.tmp_serial.busityp_id = null;
                this.tmp_serial.busityp = string.Empty;
                this.lblBusityp.Text = string.Empty;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var busityp = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_BUSITYP && i.typcod == str).FirstOrDefault();
                    if (busityp != null)
                    {
                        this.tmp_serial.busityp_id = busityp.id;
                        this.tmp_serial.busityp = busityp.typcod;
                        this.tmp_serial.istab1 = busityp;
                        this.lblBusityp.Text = busityp.typdes_th;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void brDealer__Leave(object sender, EventArgs e)
        {
            if (this.tmp_serial == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if(str.Trim().Length == 0)
            {
                this.tmp_serial.dealer = null;
                this.tmp_serial.dealer_id = null;
                this.tmp_serial.dealercod = string.Empty;
                this.lblDealer.Text = string.Empty;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod == str).FirstOrDefault();
                    if(dealer != null)
                    {
                        this.tmp_serial.dealer = dealer;
                        this.tmp_serial.dealer_id = dealer.id;
                        this.tmp_serial.dealercod = dealer.dealercod;
                        this.lblDealer.Text = dealer.compnam;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void brHowknown__Leave(object sender, EventArgs e)
        {
            if (this.tmp_serial == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
            {
                this.tmp_serial.istab2 = null;
                this.tmp_serial.howknown_id = null;
                this.lblHowknown.Text = string.Empty;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var howhnown = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_HOWKNOW && i.typcod == str).FirstOrDefault();
                    if (howhnown != null)
                    {
                        this.tmp_serial.howknown_id = howhnown.id;
                        this.tmp_serial.istab2 = howhnown;
                        this.lblHowknown.Text = howhnown.typdes_th;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void brArea__ButtonClick(object sender, EventArgs e)
        {
            DialogInquiryIstab inq = new DialogInquiryIstab(TABTYP.AREA, this.tmp_serial.istab);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if(inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_istab.typcod;
                this.lblArea.Text = inq.selected_istab.typdes_th;
                this.tmp_serial.area_id = inq.selected_istab.id;
                this.tmp_serial.area = inq.selected_istab.typcod;
                this.tmp_serial.istab = inq.selected_istab;
            }
        }

        private void brBusityp__ButtonClick(object sender, EventArgs e)
        {
            DialogInquiryIstab inq = new DialogInquiryIstab(TABTYP.BUSITYP, this.tmp_serial.istab1);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_istab.typcod;
                this.lblBusityp.Text = inq.selected_istab.typdes_th;
                this.tmp_serial.busityp_id = inq.selected_istab.id;
                this.tmp_serial.busityp = inq.selected_istab.typcod;
                this.tmp_serial.istab1 = inq.selected_istab;
            }
        }

        private void brDealer__ButtonClick(object sender, EventArgs e)
        {
            DialogInquiryDealer inq = new DialogInquiryDealer(this.tmp_serial.dealer);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_dealer.dealercod;
                this.lblDealer.Text = inq.selected_dealer.compnam;
                this.tmp_serial.dealer_id = inq.selected_dealer.id;
                this.tmp_serial.dealercod = inq.selected_dealer.dealercod;
                this.tmp_serial.dealer = inq.selected_dealer;
            }
        }

        private void brHowknown__ButtonClick(object sender, EventArgs e)
        {
            DialogInquiryIstab inq = new DialogInquiryIstab(TABTYP.HOWKNOWN, this.tmp_serial.istab2);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_istab.typcod;
                this.lblHowknown.Text = inq.selected_istab.typdes_th;
                this.tmp_serial.howknown_id = inq.selected_istab.id;
                this.tmp_serial.istab2 = inq.selected_istab;
            }
        }

        private void dgvProblem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(this.form_mode == FORM_MODE.READ_ITEM || this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                if(e.RowIndex == -1)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 255);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    e.Handled = true;
                }
            }
        }

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

            if (keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                this.btnAdd.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                this.btnEdit.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D))
            {
                this.btnDelete.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Home))
            {
                this.btnFirst.PerformClick();
                return true;
            }

            if (keyData == Keys.PageUp)
            {
                this.btnPrev.PerformClick();
                return true;
            }

            if (keyData == Keys.PageDown)
            {
                this.btnNext.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.End))
            {
                this.btnLast.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                this.btnItem.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.S))
            {
                this.btnSearch.PerformButtonClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.L))
            {
                this.btnInquiryAll.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.L))
            {
                this.btnInquiryRest.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Alt | Keys.M))
            {
                this.btnInquiryMA.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Alt | Keys.C))
            {
                this.btnInquiryCloud.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D2))
            {
                this.btnSearchContact.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D3))
            {
                this.btnSearchCompany.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D4))
            {
                this.btnSearchDealer.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D5))
            {
                this.btnSearchOldnum.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D6))
            {
                this.btnSearchBusityp.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D7))
            {
                this.btnSearchArea.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.F5))
            {
                this.btnReload.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvProblem_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;
            int col_index = ((XDatagrid)sender).HitTest(e.X, e.Y).ColumnIndex;

            if(e.Button == MouseButtons.Right)
            {
                this.btnItem.PerformClick();
                ((XDatagrid)sender).Rows[row_index].Cells[this.col_problem_probcod.Name].Selected = true;

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("เพิ่ม <Alt + A>");
                m_add.Click += delegate
                {
                    this.btnAddProblem.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("แก้ไข <Alt + E>");
                m_edit.Click += delegate
                {
                    this.btnEditProblem.PerformClick();
                };
                m_edit.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("ลบ <Alt + D>");
                m_delete.Click += delegate
                {
                    this.btnDeleteProblem.PerformClick();
                };
                m_delete.Enabled = row_index == -1 ? false : true;
                cm.MenuItems.Add(m_delete);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void btnAddProblem_Click(object sender, EventArgs e)
        {
            problem p = null;

            if(this.dgvProblem.CurrentCell != null)
            {
                p = (problem)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Cells[this.col_problem_problem.Name].Value;
                
            }

            problem prob = new problem
            {
                id = -1,
                probdesc = p != null ? p.probdesc : string.Empty,
                date = DateTime.Now,
                time = string.Empty,
                name = p != null ? p.name : string.Empty,
                serial_id = this.curr_serial.id,
                probcod_id = p != null ? p.probcod_id : null,
                creby_id = this.main_form.loged_in_user.id,
                flag = 0
            };

            ((BindingList<problemVM>)this.dgvProblem.DataSource).Add(prob.ToViewModel());

            var row = this.dgvProblem.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_problem_id.Name].Value == -1).FirstOrDefault();
            if(row != null)
            {
                row.Cells[this.col_problem_probcod.Name].Selected = true;
                this.ResetFormState(FORM_MODE.ADD_ITEM);
                this.ShowInlineForm(row);
                this.inlineDate.Focus();
            }
        }

        private void btnEditProblem_Click(object sender, EventArgs e)
        {
            if (this.dgvProblem.CurrentCell == null)
                return;

            var row = this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex];
            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(row);
            this.inlineDate.Focus();
        }

        private void btnDeleteProblem_Click(object sender, EventArgs e)
        {
            if (this.dgvProblem.CurrentCell == null)
                return;

            this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].DrawDeletingRowOverlay();

            if (MessageAlert.Show("ลบรายการที่เลือก, ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var id = (int)this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].Cells[this.col_problem_id.Name].Value;
                    var problem_to_remove = sn.problem.Where(p => p.flag == 0 && p.id == id).FirstOrDefault();

                    if (problem_to_remove != null)
                    {
                        //sn.problem.Remove(problem_to_remove);
                        problem_to_remove.flag = problem_to_remove.id;
                        problem_to_remove.chgby_id = this.main_form.loged_in_user.id;
                        problem_to_remove.chgdat = DateTime.Now;
                        sn.SaveChanges();
                    }

                    ((BindingList<problemVM>)this.dgvProblem.DataSource).Remove(((BindingList<problemVM>)this.dgvProblem.DataSource).Where(o => o.id == id).First());
                }
            }
            else
            {
                this.dgvProblem.Rows[this.dgvProblem.CurrentCell.RowIndex].ClearDeletingRowOverlay();
            }
        }

        private void inlineDate__SelectedDateChanged(object sender, EventArgs e)
        {
            if (this.tmp_problem != null)
                this.tmp_problem.date = ((XDatePicker)sender)._SelectedDate;
        }

        private void inlineName__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_problem != null)
                this.tmp_problem.name = ((XTextEdit)sender)._Text;
        }

        private void inlineProbcod__ButtonClick(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                var prob = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_problem.probcod_id).FirstOrDefault();

                DialogInquiryIstab inq = new DialogInquiryIstab(TABTYP.PROBCOD, prob);
                Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
                inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
                if (inq.ShowDialog() == DialogResult.OK)
                {
                    ((XBrowseBox)sender)._Text = inq.selected_istab.typcod;
                    if (this.tmp_problem != null)
                        this.tmp_problem.probcod_id = inq.selected_istab.id;
                }
            }
        }

        private void inlineProbcod__Leave(object sender, EventArgs e)
        {
            var str = ((XBrowseBox)sender)._Text;
            if (str.Length == 0)
            {
                ((XBrowseBox)sender).Focus();
                SendKeys.Send("{F6}");
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var prob = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod == str).FirstOrDefault();
                    if(prob != null)
                    {
                        if (this.tmp_problem != null)
                            this.tmp_problem.probcod_id = prob.id;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void inlineProbdesc__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_problem != null)
                this.tmp_problem.probdesc = ((XTextEditWithMaskedLabel)sender)._TextAll;
        }
    }
}
