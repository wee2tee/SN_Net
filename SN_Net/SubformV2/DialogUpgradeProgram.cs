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
    public partial class DialogUpgradeProgram : Form
    {
        private MainForm main_form;
        private serial serial_from;
        private string sernum_to = string.Empty;
        private string version = string.Empty;
        private bool is_newrwt = false;
        private bool is_newrwt_job = false;
        private bool is_cdtraining = false;
        public serial serial_upgraded = null;

        public DialogUpgradeProgram(MainForm main_form, serial serial_from)
        {
            this.main_form = main_form;
            this.serial_from = serial_from;
            InitializeComponent();
        }

        private void DialogUpgradeProgram_Load(object sender, EventArgs e)
        {
            this.mskSernumFrom.Text = this.serial_from.sernum;
            this.ActiveControl = this.mskSernumTo;
        }

        private void mskSernumTo_Leave(object sender, EventArgs e)
        {
            var ser = ((MaskedTextBox)sender).Text;
            if(ser.Replace("-", "").Trim().Length == 0)
            {
                this.sernum_to = string.Empty;
                this.mskSernumTo.Focus();
                this.txtVersion.Text = string.Empty;
                this.btnOK.Enabled = false;
                return;
            }

            if (ValidateSN.Check(ser) == true)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (sn.serial.Where(s => s.flag == 0 && s.sernum.Trim() == ser.Trim()).FirstOrDefault() != null)
                    {
                        MessageAlert.Show("หมายเลข S/N " + ser + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        ((MaskedTextBox)sender).Focus();
                        this.sernum_to = string.Empty;
                        this.txtVersion.Text = string.Empty;
                        this.btnOK.Enabled = false;
                        return;
                    }
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
                this.sernum_to = ((MaskedTextBox)sender).Text;
                this.btnOK.Enabled = true;
            }
            else
            {
                ((MaskedTextBox)sender).Focus();
                this.sernum_to = string.Empty;
                this.txtVersion.Text = string.Empty;
                this.btnOK.Enabled = false;
            }
                
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            this.version = ((TextBox)sender).Text;
        }

        private void chkNewRwt_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.chkNewRwtJob.Checked = false;
            }
            this.is_newrwt = ((CheckBox)sender).Checked;
        }

        private void chkNewRwtJob_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.chkNewRwt.Checked = false;
            }
            this.is_newrwt_job = ((CheckBox)sender).Checked;
        }

        private void chkCDTraining_CheckedChanged(object sender, EventArgs e)
        {
            this.is_cdtraining = ((CheckBox)sender).Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (sn.serial.Where(s => s.flag == 0 && s.sernum == this.sernum_to).FirstOrDefault() != null)
                    {
                        MessageAlert.Show("หมายเลข S/N " + this.sernum_to + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    serial serial_to_update = sn.serial.Where(s => s.flag == 0 && s.id == this.serial_from.id).FirstOrDefault();
                    if (serial_to_update == null)
                    {
                        MessageAlert.Show("ค้นหา S/N " + this.serial_from.sernum + " นี้ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }

                    var v = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "0").FirstOrDefault();
                    if (this.is_newrwt)
                    {
                        v = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "1").FirstOrDefault();
                    }
                    if (this.is_newrwt_job)
                    {
                        v = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "2").FirstOrDefault();
                    }

                    string ver_ext_str = this.is_newrwt ? "-NewRwt" : (this.is_newrwt_job ? "-NewRwtJob" : "");
                    ver_ext_str += this.is_newrwt || this.is_newrwt_job || this.is_cdtraining ? " + แผ่น DVD สอนใช้งาน" : "";

                    istab p = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "UG").FirstOrDefault();

                    serial_to_update.sernum = this.sernum_to;
                    serial_to_update.oldnum = this.serial_from.sernum;
                    serial_to_update.version = this.version;
                    serial_to_update.expdat = this.is_cdtraining ? (DateTime?)DateTime.Now : null;
                    serial_to_update.verext_id = v != null ? (int?)v.id : null;
                    serial_to_update.verextdat = this.is_newrwt || this.is_newrwt_job ? (DateTime?)DateTime.Now : null;
                    serial_to_update.chgby_id = this.main_form.loged_in_user.id;
                    serial_to_update.chgdat = DateTime.Now;

                    List<serial> ref_sn = sn.serial.Where(s => s.flag == 0 && s.sernum == serial_to_update.refnum).ToList();
                    foreach (var item in ref_sn)
                    {
                        item.refnum = this.sernum_to;
                        item.chgby_id = this.main_form.loged_in_user.id;
                        item.chgdat = DateTime.Now;
                    }

                    problem prob = new problem
                    {
                        probdesc = "V." + this.serial_from.version + " (" + this.serial_from.sernum + ") to V." + this.version + " (" + this.sernum_to + ")" + ver_ext_str,
                        date = DateTime.Now,
                        time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                        name = string.Empty,
                        serial_id = serial_to_update.id,
                        probcod_id = p != null ? (int?)p.id : null,
                        creby_id = this.main_form.loged_in_user.id,
                        credat = DateTime.Now,
                        chgby_id = null,
                        chgdat = null,
                        flag = 0
                    };
                    sn.problem.Add(prob);

                    sn.SaveChanges();
                    this.serial_upgraded = serial_to_update;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
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
