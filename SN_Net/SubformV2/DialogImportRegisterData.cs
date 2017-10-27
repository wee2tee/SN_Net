using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class DialogImportRegisterData : Form
    {
        private MainForm main_form;
        private BindingList<importSerialVM> register_data;
        private FORM_MODE form_mode;
        private serial tmp_serial;
        private problem tmp_problem_email;

        public DialogImportRegisterData(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void DialogImportRegisterData_Load(object sender, EventArgs e)
        {
            this.ShowLoadingBox();
            try
            {
                registerDataResult reg_result;
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    WebRequest req = WebRequest.Create("http://www.esg.co.th/api/get_registered_sn.php");
                    string post_data = "validate_code=WeeTee&p_type=get_new_register_list";
                    byte[] byteArray = Encoding.UTF8.GetBytes(post_data);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = byteArray.Length;

                    Stream dataStream = req.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    WebResponse resp = req.GetResponse();
                    dataStream = resp.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    //Console.WriteLine(" ==> " + reader.ReadToEnd());
                    reg_result = JsonConvert.DeserializeObject<registerDataResult>(reader.ReadToEnd());
                    this.register_data = new BindingList<importSerialVM>(reg_result.register_data.ToViewModel());

                    reader.Close();
                    dataStream.Close();
                    resp.Close();
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.dgv.DataSource = this.register_data;
                    this.HideLoadingBox();
                };
                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageAlert.Show(ex.Message);
            }
        }

        private bool SendRecordedSign2Server(string server_db_record_id)
        {
            WebRequest req = WebRequest.Create("http://www.esg.co.th/api/get_registered_sn.php");
            string post_data = "validate_code=WeeTee&p_type=record_complete&id=" + server_db_record_id;
            byte[] byteArray = Encoding.UTF8.GetBytes(post_data);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byteArray.Length;

            Stream dataStream = req.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse resp = req.GetResponse();
            dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            //Console.WriteLine(" ==> " + reader.ReadToEnd());
            registerDataResult reg_result = JsonConvert.DeserializeObject<registerDataResult>(reader.ReadToEnd());
            reader.Close();
            dataStream.Close();
            resp.Close();
            if (reg_result.result > 0)
            {
                return true;
            }
            else
            {
                return this.SendRecordedSign2Server(server_db_record_id);
            }
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.txtPrenam.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtCompnam.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr01.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr02.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtAddr03.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtZipcod.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtTelnum.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtFaxnum.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtContact.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtPosition.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtEmail.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.txtBusides.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            //this.txtBusityp.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.brBusityp.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            //this.txtDealer.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.brDealer.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);

            this.btnGoSn.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.btnOK.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.btnCancel.SetControlState(new FORM_MODE[] { FORM_MODE.EDIT }, this.form_mode);
            this.dgv.SetControlState(new FORM_MODE[] { FORM_MODE.READ }, this.form_mode);
        }

        private void FillForm()
        {
            if(this.tmp_serial != null)
            {
                this.mskSernum._Text = this.tmp_serial.sernum;
                this.txtVersion._Text = this.tmp_serial.version;
                this.txtPrenam._Text = this.tmp_serial.prenam;
                this.txtCompnam._Text = this.tmp_serial.compnam;
                this.txtAddr01._Text = this.tmp_serial.addr01;
                this.txtAddr02._Text = this.tmp_serial.addr02;
                this.txtAddr03._Text = this.tmp_serial.addr03;
                this.txtZipcod._Text = this.tmp_serial.zipcod;
                this.txtTelnum._Text = this.tmp_serial.telnum;
                this.txtFaxnum._Text = this.tmp_serial.faxnum;
                this.txtContact._Text = this.tmp_serial.contact;
                this.txtPosition._Text = this.tmp_serial.position;
                this.brBusityp._Text = this.tmp_serial.busityp_id.HasValue ? this.tmp_serial.istab1.typcod : string.Empty;
                this.lblBusityp.Text = this.tmp_serial.busityp_id.HasValue ? this.tmp_serial.istab1.typdes_th : string.Empty;
                this.brDealer._Text = this.tmp_serial.dealer_id.HasValue ? this.tmp_serial.dealer.dealercod : string.Empty;
                this.lblDealer.Text = this.tmp_serial.dealer_id.HasValue ? this.tmp_serial.dealer.compnam : string.Empty;

                if (this.tmp_problem_email != null)
                    this.txtEmail._Text = this.tmp_problem_email.probdesc;
            }
            else
            {
                this.mskSernum._Text = " -   -      ";
                this.txtPrenam._Text = string.Empty;
                this.txtCompnam._Text = string.Empty;
                this.txtAddr01._Text = string.Empty;
                this.txtAddr02._Text = string.Empty;
                this.txtAddr03._Text = string.Empty;
                this.txtZipcod._Text = string.Empty;
                this.txtTelnum._Text = string.Empty;
                this.txtFaxnum._Text = string.Empty;
                this.txtContact._Text = string.Empty;
                this.txtPosition._Text = string.Empty;
                this.txtEmail._Text = string.Empty;

                this.txtBusityp._Text = string.Empty;
                this.txtDealer._Text = string.Empty;

                this.brBusityp._Text = string.Empty;
                this.brDealer._Text = string.Empty;
                this.lblBusityp.Text = string.Empty;
                this.lblDealer.Text = string.Empty;
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                if (e.ColumnIndex == this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_recorded.Name).First().Index)
                {
                    if ((bool)this.dgv.Rows[e.RowIndex].Cells[this.col_recorded.Name].Value == true)
                    {
                        e.CellStyle.BackColor = Color.WhiteSmoke;
                        e.CellStyle.SelectionBackColor = Color.WhiteSmoke;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground);
                        e.Graphics.DrawImage(SN_Net.Properties.Resources.check2_16, new PointF(e.CellBounds.X + 4, e.CellBounds.Y + 4));
                    }
                    else
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground);
                    }

                    e.Handled = true;
                    return;
                }

                if (e.ColumnIndex == this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_sn.Name).First().Index || e.ColumnIndex == this.dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_compname.Name).First().Index)
                {
                    if ((bool)this.dgv.Rows[e.RowIndex].Cells[this.col_recorded.Name].Value == true)
                    {
                        e.CellStyle.BackColor = Color.WhiteSmoke;
                        e.CellStyle.SelectionBackColor = Color.WhiteSmoke;
                        e.CellStyle.ForeColor = Color.SlateGray;
                        e.CellStyle.SelectionForeColor = Color.SlateGray;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool is_recorded = (bool)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_recorded.Name].Value;

            if (is_recorded || e.RowIndex == -1)
                return;

            importSerial im = (importSerial)((XDatagrid)sender).Rows[e.RowIndex].Cells[this.col_importSerial.Name].Value;

            this.ShowLoadingBox();
            BackgroundWorker wrk = new BackgroundWorker();
            wrk.DoWork += delegate
            {
                using (snEntities sn = DBX.DataSet())
                {
                    this.tmp_serial = sn.serial.Include("istab1").Include("dealer").Where(s => s.flag == 0 && s.sernum == im.sn).FirstOrDefault();

                    if(this.tmp_serial != null/* && (im.cont_email.Trim().Length > 0 || im.comp_email.Trim().Length > 0)*/)
                    {
                        istab prob = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "EM").FirstOrDefault();
                        this.tmp_problem_email = new problem
                        {
                            serial_id = this.tmp_serial.id,
                            date = null,
                            probcod_id = prob != null ? (int?)prob.id : null,
                            name = string.Empty,
                            probdesc = im.cont_email.Trim().Length > 0 ? (im.comp_email.Trim().Length > 0 ? im.comp_email + " , " + im.cont_email : im.cont_email) : string.Empty,
                            time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                            creby_id = this.main_form.loged_in_user.id,
                            credat = DateTime.Now,
                            flag = 0
                        };
                    }
                }
            };
            wrk.RunWorkerCompleted += delegate
            {
                this.HideLoadingBox();
                if (this.tmp_serial == null)
                {
                    MessageAlert.Show("ค้นหา S/N : " + im.sn + " ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    return;
                }
                else
                {
                    this.ResetFormState(FORM_MODE.EDIT);
                    this.txtBusityp._Text = im.comp_bus_type;
                    this.txtDealer._Text = im.purchase_from.Trim() == "Express" ? "Express" : (im.purchase_from.Trim() == "Dealer" ? "Dealer : " + im.purchase_from_desc : string.Empty);

                    this.tmp_serial.prenam = im.comp_prenam;
                    this.tmp_serial.compnam = im.comp_name;
                    this.tmp_serial.addr01 = im.comp_addr1;
                    this.tmp_serial.addr02 = im.comp_addr2;
                    this.tmp_serial.addr03 = im.comp_addr3;
                    this.tmp_serial.zipcod = im.comp_zipcod;
                    this.tmp_serial.telnum = im.comp_tel;
                    this.tmp_serial.faxnum = im.comp_fax;
                    this.tmp_serial.contact = im.cont_name;
                    this.tmp_serial.position = im.cont_position;
                    this.tmp_serial.busides = im.comp_bus_desc;
                    
                    this.FillForm();
                    this.txtPrenam.Focus();
                }
            };
            wrk.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ResetFormState(FORM_MODE.READ);
            this.tmp_serial = null;
            this.tmp_problem_email = null;
            this.FillForm();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.tmp_serial == null)
                return;

            if (!this.tmp_serial.busityp_id.HasValue)
            {
                MessageAlert.Show("Please select Business Type", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.brBusityp.Focus();
                return;
            }

            if (!this.tmp_serial.dealer_id.HasValue)
            {
                MessageAlert.Show("Please select Purchase From", "", MessageAlertButtons.OK, MessageAlertIcons.WARNING);
                this.brDealer.Focus();
                return;
            }

            if (MessageAlert.Show("บันทึกข้อมูล, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                return;

            this.ShowLoadingBox();
            try
            {
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += delegate
                {
                    using (snEntities sn = DBX.DataSet())
                    {
                        serial ser = sn.serial.Where(s => s.flag == 0 && s.id == this.tmp_serial.id).FirstOrDefault();
                        if (ser == null)
                        {
                            MessageAlert.Show("ค้นหา S/N : " + this.tmp_serial.sernum + " ไม่พบ");
                            return;
                        }

                        ser.prenam = this.tmp_serial.prenam;
                        ser.compnam = this.tmp_serial.compnam;
                        ser.addr01 = this.tmp_serial.addr01;
                        ser.addr02 = this.tmp_serial.addr02;
                        ser.addr03 = this.tmp_serial.addr03;
                        ser.zipcod = this.tmp_serial.zipcod;
                        ser.telnum = this.tmp_serial.telnum;
                        ser.faxnum = this.tmp_serial.faxnum;
                        ser.contact = this.tmp_serial.contact;
                        ser.position = this.tmp_serial.position;
                        ser.busides = this.tmp_serial.busides;
                        ser.busityp_id = this.tmp_serial.busityp_id;
                        ser.busityp = this.tmp_serial.busityp;
                        ser.dealer_id = this.tmp_serial.dealer_id;
                        ser.dealercod = this.tmp_serial.dealercod;

                        if (this.tmp_problem_email != null && this.tmp_problem_email.probdesc.Trim().Length > 0)
                            sn.problem.Add(this.tmp_problem_email);

                        sn.SaveChanges();

                        string server_db_record_id = (string)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_id.Name].Value;
                        if(this.SendRecordedSign2Server(server_db_record_id) == true)
                        {
                            this.dgv.Invoke(new Action(() =>
                            {
                                var bl = this.dgv.DataSource as BindingList<importSerialVM>;
                                bl.Where(b => b.id == server_db_record_id).First().recorded = true;
                                if(this.main_form.form_sn.form_mode == FORM_MODE.READ)
                                {
                                    this.main_form.form_sn.curr_serial = this.main_form.form_sn.GetSerial(this.tmp_serial.id);
                                    this.main_form.form_sn.FillForm(this.main_form.form_sn.curr_serial);
                                }
                                this.btnCancel.PerformClick();
                            }));
                        }
                    }
                };
                wrk.RunWorkerCompleted += delegate
                {
                    this.HideLoadingBox();
                };
                wrk.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                this.HideLoadingBox();
                MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void btnGoSn_Click(object sender, EventArgs e)
        {
            if(this.main_form.form_sn.form_mode != FORM_MODE.READ)
            {
                MessageAlert.Show("Cannot show your requested data in Add/Edit mode", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }

            this.main_form.form_sn.curr_serial = this.main_form.form_sn.GetSerial(this.tmp_serial.id);
            this.main_form.form_sn.FillForm(this.main_form.form_sn.curr_serial);
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

        private void txtAddr02_Load(object sender, EventArgs e)
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

        private void txtEmail__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_problem_email != null)
                this.tmp_problem_email.probdesc = ((XTextEdit)sender)._Text;
        }

        private void txtBusides__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_serial != null)
                this.tmp_serial.busides = ((XTextEdit)sender)._Text;
        }

        private void brBusityp__ButtonClick(object sender, EventArgs e)
        {
            string str = ((XBrowseBox)sender)._Text;
            using (snEntities sn = DBX.DataSet())
            {
                istab istab = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_BUSITYP && i.typcod.Trim() == str.Trim()).FirstOrDefault();

                DialogSelectIstab inq = new DialogSelectIstab(TABTYP.BUSITYP, istab);
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

        private void brDealer__ButtonClick(object sender, EventArgs e)
        {
            string str = ((XBrowseBox)sender)._Text;
            using (snEntities sn = DBX.DataSet())
            {
                dealer dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();

                DialogSelectDealer inq = new DialogSelectDealer(dealer);
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
        }

        private void brDealer__Leave(object sender, EventArgs e)
        {
            if (this.tmp_serial == null)
                return;

            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
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
                    if (dealer != null)
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

        protected override void OnClosing(CancelEventArgs e)
        {
            if(this.dgv.Rows.Count > 0)
            {
                int unrecorded_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (bool)r.Cells[this.col_recorded.Name].Value == false).AsEnumerable().Count();
                if (unrecorded_row > 0)
                {
                    if(MessageAlert.Show("มีบางรายการที่ยังไม่ได้บันทึกลงฐานข้อมูล S/N, \n\t- คลิก \"ตกลง\" เพื่อปิดหน้าต่างการทำงานนี้\n\t- คลิก \"ยกเลิก\" เพื่อทำงานนี้ต่อ", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) != DialogResult.OK)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            base.OnClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    if (this.btnOK.Focused || this.btnCancel.Focused)
                    {
                        return false;
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }

            if (keyData == Keys.F9)
            {
                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
