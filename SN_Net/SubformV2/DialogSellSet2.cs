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

namespace SN_Net.Subform
{
    public partial class DialogSellSet2 : Form
    {
        private MainForm main_form;
        private serial serial_set1;
        private string sernum_set2 = string.Empty;
        private string version = string.Empty;
        private int? dealer_id = null;
        public serial serial_set2 = null;

        public DialogSellSet2(MainForm main_form, serial serial_set1)
        {
            this.main_form = main_form;
            this.serial_set1 = serial_set1;
            InitializeComponent();
        }

        private void DialogSellSet2_Load(object sender, EventArgs e)
        {
            this.mskSernum1.Text = this.serial_set1.sernum;
            this.ActiveControl = this.mskSernum2;
        }

        private void mskSernum2_Leave(object sender, EventArgs e)
        {
            string ser = ((MaskedTextBox)sender).Text;

            if(ser.Replace("-", "").Trim().Length == 0 || ValidateSN.Check(ser) == false)
            {
                ((MaskedTextBox)sender).Focus();
                this.sernum_set2 = string.Empty;
                this.txtVersion.Text = string.Empty;
                this.btnOK.Enabled = false;
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                if(sn.serial.Where(s => s.flag == 0 && s.sernum == ser).FirstOrDefault() != null)
                {
                    MessageAlert.Show("หมายเลข S/N " + ser.Trim() + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    ((MaskedTextBox)sender).Focus();
                    this.sernum_set2 = string.Empty;
                    this.txtVersion.Text = string.Empty;
                    this.btnOK.Enabled = false;
                    return;
                }

                if (ser.Substring(2, 2) == "10")
                {
                    this.txtVersion.Text = "1.0";
                }
                if (ser.Substring(2, 2) == "15")
                {
                    this.txtVersion.Text = "1.5";
                }
                if (ser.Substring(2, 1) == "2")
                {
                    this.txtVersion.Text = "2.0";
                }
                if (ser.Substring(2, 1) == "3")
                {
                    this.txtVersion.Text = "2.5";
                }
                this.sernum_set2 = ((MaskedTextBox)sender).Text;
                this.btnOK.Enabled = true;
            }
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            this.version = ((TextBox)sender).Text;
        }

        private void brDealer__ButtonClick(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                string str = ((XBrowseBox)sender)._Text;

                dealer dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();

                if (dealer == null)
                {
                    dealer = sn.dealer.Where(d => d.flag == 0 && string.Compare(d.dealercod.Trim(), str.Trim(), StringComparison.OrdinalIgnoreCase) > 0).OrderBy(d => d.dealercod).FirstOrDefault();
                }


                DialogSelectDealer inq = new DialogSelectDealer(dealer);
                Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
                inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
                if (inq.ShowDialog() == DialogResult.OK)
                {
                    ((XBrowseBox)sender)._Text = inq.selected_dealer.dealercod;
                    this.lblDealer.Text = inq.selected_dealer.compnam;
                    this.dealer_id = inq.selected_dealer.id;
                }
            }
        }

        private void brDealer__Leave(object sender, EventArgs e)
        {
            string str = ((XBrowseBox)sender)._Text;

            if (str.Trim().Length == 0)
            {
                ((XBrowseBox)sender)._Text = string.Empty;
                this.lblDealer.Text = string.Empty;
                this.dealer_id = null;
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                var dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();
                if (dealer != null)
                {
                    ((XBrowseBox)sender)._Text = dealer.dealercod;
                    this.lblDealer.Text = dealer.compnam;
                    this.dealer_id = dealer.id;
                }
                else
                {
                    ((XBrowseBox)sender).PerformButtonClick();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (sn.serial.Where(s => s.flag == 0 && s.sernum == this.sernum_set2).FirstOrDefault() != null)
                    {
                        MessageAlert.Show("หมายเลข S/N " + this.sernum_set2 + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        this.mskSernum2.Focus();
                        return;
                    }

                    serial serial_to_update = sn.serial.Where(s => s.flag == 0 && s.id == this.serial_set1.id).FirstOrDefault();
                    if (serial_to_update == null)
                    {
                        MessageAlert.Show("ค้นหา S/N " + this.serial_set1.sernum + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                    serial_to_update.refnum = this.sernum_set2;
                    serial_to_update.chgby_id = this.main_form.loged_in_user.id;
                    serial_to_update.chgdat = DateTime.Now;

                    var d = sn.dealer.Where(dd => dd.flag == 0 && dd.id == this.dealer_id).FirstOrDefault();
                    var v = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "0").FirstOrDefault();
                    var p = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "TN").FirstOrDefault();

                    serial serial_to_add = new serial
                    {
                        sernum = this.sernum_set2,
                        oldnum = string.Empty,
                        version = this.version,
                        contact = string.Empty,
                        position = string.Empty,
                        prenam = string.Empty,
                        compnam = string.Empty,
                        addr01 = string.Empty,
                        addr02 = string.Empty,
                        addr03 = string.Empty,
                        zipcod = string.Empty,
                        telnum = string.Empty,
                        faxnum = string.Empty,
                        busides = string.Empty,
                        purdat = DateTime.Now,
                        expdat = null,
                        branch = string.Empty,
                        manual = null,
                        upfree = string.Empty,
                        refnum = this.serial_set1.sernum,
                        remark = string.Empty,
                        dealer_id = d != null ? (int?)d.id : null,
                        dealercod = d != null ? d.dealercod : string.Empty,
                        verextdat = null,
                        area_id = null,
                        area = string.Empty,
                        busityp_id = null,
                        busityp = string.Empty,
                        howknown_id = null,
                        verext_id = v != null ? (int?)v.id : null,
                        creby_id = this.main_form.loged_in_user.id,
                        credat = DateTime.Now,
                        chgby_id = null,
                        chgdat = null,
                        flag = 0
                    };
                    sn.serial.Add(serial_to_add);

                    problem prob = new problem
                    {
                        probdesc = "2nd of set, need to pay training fee.",
                        date = DateTime.Now,
                        time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                        name = this.main_form.loged_in_user.name,
                        serial_id = serial_to_add.id,
                        probcod_id = p != null ? (int?)p.id : null,
                        creby_id = this.main_form.loged_in_user.id,
                        credat = DateTime.Now,
                        chgby_id = null,
                        chgdat = null,
                        flag = 0
                    };
                    sn.problem.Add(prob);

                    sn.SaveChanges();

                    this.serial_set2 = serial_to_add;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    return;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            if(keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
