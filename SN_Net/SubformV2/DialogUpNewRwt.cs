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
    public partial class DialogUpNewRwt : Form
    {
        private bool is_greendisc = false;
        private bool is_pinkdisc = false;
        private serial serial_to_update;
        private MainForm main_form;
        private UPTYPE up_type;
        public enum UPTYPE
        {
            NEWRWT,
            NEWRWT_JOB
        }

        public DialogUpNewRwt(MainForm main_form, serial serial_to_update, UPTYPE up_type)
        {
            InitializeComponent();
            this.main_form = main_form;
            this.serial_to_update = serial_to_update;
            this.up_type = up_type;
        }

        private void DialogUpNewRwt_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.chkGreendisc;
        }

        private void chkGreendisc_CheckedChanged(object sender, EventArgs e)
        {
            this.is_greendisc = ((CheckBox)sender).Checked;
            this.ValidateForm();
        }

        private void chkPinkdisc_CheckedChanged(object sender, EventArgs e)
        {
            this.is_pinkdisc = ((CheckBox)sender).Checked;
            this.ValidateForm();
        }

        private void ValidateForm()
        {
            if(this.is_greendisc || is_pinkdisc)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(!this.is_greendisc && !this.is_pinkdisc)
            {
                MessageAlert.Show("Please select at least one choice", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    var ser = sn.serial.Where(s => s.flag == 0 && s.id == this.serial_to_update.id).FirstOrDefault();
                    var probcod = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "UG").FirstOrDefault();
                    istab verext = null;

                    if (ser != null)
                    {
                        string probdesc = string.Empty;
                        if(this.up_type == UPTYPE.NEWRWT)
                        {
                            verext = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "1").FirstOrDefault();
                            if (this.is_pinkdisc)
                            {
                                probdesc = "NewRWT + แผ่น DVD สอนใช้งาน";
                            }
                            else
                            {
                                probdesc = "NewRWT";
                            }
                        }
                        else if(this.up_type == UPTYPE.NEWRWT_JOB)
                        {
                            verext = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_VEREXT && i.typcod.Trim() == "2").FirstOrDefault();
                            if (this.is_pinkdisc)
                            {
                                probdesc = "NewRWT+Job + แผ่น DVD สอนใช้งาน";
                            }
                            else
                            {
                                probdesc = "NewRWT+Job";
                            }
                        }

                        ser.verext_id = verext != null ? (int?)verext.id : null;
                        ser.verextdat = DateTime.Now;

                        problem prob = new problem
                        {
                            probdesc = probdesc,
                            date = DateTime.Now,
                            time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                            name = this.main_form.loged_in_user.name,
                            serial_id = ser.id,
                            probcod_id = probcod != null ? (int?)probcod.id : null,
                            creby_id = this.main_form.loged_in_user.id,
                            credat = DateTime.Now,
                            chgby_id = null,
                            chgdat = null,
                            flag = 0
                        };
                        sn.problem.Add(prob);
                        sn.SaveChanges();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageAlert.Show("ค้นหา S/N " + this.serial_to_update.sernum + " ไม่พบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                    }
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
                else
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
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
