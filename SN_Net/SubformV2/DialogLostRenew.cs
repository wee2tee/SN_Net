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
    public partial class DialogLostRenew : Form
    {
        private MainForm main_form;
        private serial losted_serial;
        private string new_sernum = string.Empty;
        private string new_version = string.Empty;
        private bool is_new_report = false;
        private bool is_new_report_job = false;
        private bool is_cd_training = false;

        public DialogLostRenew(MainForm main_form, serial losted_serial)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.losted_serial = losted_serial;
        }

        private void DialogLostRenew_Load(object sender, EventArgs e)
        {
            this.mskLostSernum.Text = this.losted_serial.sernum;
            this.ActiveControl = this.mskNewSernum;
        }

        private void mskNewSernum_Leave(object sender, EventArgs e)
        {
            if(ValidateSN.Check(((MaskedTextBox)sender).Text) == false)
            {
                this.btnOK.Enabled = false;
                this.txtVersion.Text = string.Empty;
                ((MaskedTextBox)sender).Focus();
                return;
            }
            else
            {
                string new_sn = ((MaskedTextBox)sender).Text;
                using (snEntities sn = DBX.DataSet())
                {
                    var exist_sn = sn.serial.Where(s => s.flag == 0 && s.sernum == new_sn).FirstOrDefault();
                    if(exist_sn != null)
                    {
                        MessageAlert.Show("S/N นี้มีอยู่แล้ว, กรุณาป้อนใหม่", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        ((MaskedTextBox)sender).Focus();
                        return;
                    }

                    this.new_sernum = new_sn;
                }

                this.btnOK.Enabled = true;
                string ver = ((MaskedTextBox)sender).Text.Substring(2, 2);
                if(ver == "10")
                {
                    this.txtVersion.Text = "1.0";
                    return;
                }
                if(ver == "15")
                {
                    this.txtVersion.Text = "1.5";
                    return;
                }
                if(ver.Substring(0, 1) == "2")
                {
                    this.txtVersion.Text = "2.0";
                    return;
                }
                if (ver.Substring(0, 1) == "3")
                {
                    this.txtVersion.Text = "2.5";
                    return;
                }
            }
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            this.new_version = ((TextBox)sender).Text;
        }

        private void chkNewRwt_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.chkNewRwtJob.Checked = false;
            }
            this.is_new_report = ((CheckBox)sender).Checked;
        }

        private void chkNewRwtJob_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.chkNewRwt.Checked = false;
            }
            this.is_new_report_job = ((CheckBox)sender).Checked;
        }

        private void chkCDTraining_CheckedChanged(object sender, EventArgs e)
        {
            this.is_cd_training = ((CheckBox)sender).Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    var serial_to_update = sn.serial.Find(this.losted_serial.id);
                    if (serial_to_update == null)
                    {
                        MessageAlert.Show("ค้นหา S/N " + this.losted_serial.sernum + " นี้ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    int ver_normal = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "0").First().id;
                    int ver_newrwt = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "1").First().id;
                    int ver_newrwt_job = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "2").First().id;
                    int ver_cancel = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "9").First().id;
                    int probcod_id = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "UG").First().id;
                    string verext_string = this.is_new_report ? "-NewRwt" : (this.is_new_report_job ? "-NewRwtJob" : string.Empty);

                    if (!this.is_new_report && !this.is_new_report_job)
                    {
                        serial_to_update.verext_id = ver_normal;
                    }
                    else if (this.is_new_report)
                    {
                        serial_to_update.verext_id = ver_newrwt;
                    }
                    else if (this.is_new_report_job)
                    {
                        serial_to_update.verext_id = ver_newrwt_job;
                    }

                    if (this.is_cd_training)
                    {
                        serial_to_update.expdat = DateTime.Now;
                    }

                    serial_to_update.oldnum = serial_to_update.sernum;
                    serial_to_update.sernum = this.new_sernum;
                    serial_to_update.version = this.new_version;

                    serial keep_old_serial = new serial
                    {
                        sernum = losted_serial.sernum,
                        oldnum = losted_serial.oldnum,
                        version = losted_serial.version,
                        contact = losted_serial.contact,
                        position = losted_serial.position,
                        prenam = losted_serial.prenam,
                        compnam = losted_serial.compnam + "*****ยกเลิกแผ่นหาย*****",
                        addr01 = string.Empty,
                        addr02 = "********** ซื้อใหม่ " + this.new_sernum + " ***********",
                        addr03 = string.Empty,
                        zipcod = string.Empty,
                        telnum = string.Empty,
                        faxnum = string.Empty,
                        busides = losted_serial.busides,
                        purdat = losted_serial.purdat,
                        expdat = losted_serial.expdat,
                        branch = losted_serial.branch,
                        manual = losted_serial.manual,
                        upfree = losted_serial.upfree,
                        refnum = losted_serial.refnum,
                        remark = losted_serial.remark,
                        dealer_id = losted_serial.dealer_id,
                        dealercod = losted_serial.dealercod,
                        verextdat = losted_serial.verextdat,
                        area_id = losted_serial.area_id,
                        area = losted_serial.area,
                        busityp_id = losted_serial.busityp_id,
                        busityp = losted_serial.busityp,
                        howknown_id = losted_serial.howknown_id,
                        verext_id = ver_cancel,
                        creby_id = losted_serial.creby_id,
                        credat = losted_serial.credat,
                        chgby_id = this.main_form.loged_in_user.id,
                        chgdat = DateTime.Now,
                        flag = losted_serial.flag
                    };
                    sn.serial.Add(keep_old_serial);

                    problem keep_log = new problem
                    {
                        probdesc = "V." + this.losted_serial.version + " (" + this.losted_serial.sernum + ") to " + serial_to_update.version + " (" + serial_to_update.sernum + ")" + verext_string,
                        date = DateTime.Now,
                        time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                        name = "*แผ่นหาย*",
                        serial_id = serial_to_update.id,
                        probcod_id = probcod_id,
                        creby_id = this.main_form.loged_in_user.id,
                        credat = DateTime.Now,
                        chgby_id = null,
                        chgdat = null,
                        flag = 0
                    };
                    sn.problem.Add(keep_log);

                    //Console.WriteLine(" ==> losted serial : " + this.losted_serial.sernum);

                    List<serial> ref_sn = sn.serial.Where(s => s.flag == 0 && s.refnum == this.losted_serial.sernum).ToList();
                    //Console.WriteLine(" ==> ref s/n count : " + ref_sn.Count);
                    foreach (var item in ref_sn)
                    {
                        item.refnum = this.new_sernum;
                        item.chgby_id = this.main_form.loged_in_user.id;
                        item.chgdat = DateTime.Now;
                        //sn.SaveChanges();
                    }

                    sn.SaveChanges();
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
                if (this.btnOK.Focused || this.btnCancel.Focused)
                    return false;

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
