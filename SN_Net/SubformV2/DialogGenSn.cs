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
    public partial class DialogGenSn : Form
    {
        private MainForm main_form;
        private string sernum = string.Empty;
        private string version = string.Empty;
        private int qty = 1;
        private bool is_newrwt = false;
        private bool is_newrwt_job = false;
        private int? dealer_id = null;
        private bool is_cdtraining = true;
        public serial serial_generated = null;

        public DialogGenSn(MainForm main_form)
        {
            this.main_form = main_form;
            InitializeComponent();
        }

        private void DialogGenSn_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.mskSernum;
        }

        private void mskSernum_Leave(object sender, EventArgs e)
        {
            string ser = ((MaskedTextBox)sender).Text;
            if(ValidateSN.Check(ser) == true)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if(sn.serial.Where(s => s.flag == 0 && s.sernum.Trim() == ser.Trim()).FirstOrDefault() != null)
                    {
                        MessageAlert.Show("หมายเลข S/N นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        this.sernum = string.Empty;
                        ((MaskedTextBox)sender).Focus();
                        this.txtVersion.Text = string.Empty;
                        this.btnOK.Enabled = false;
                        return;
                    }
                }

                if (ser.Substring(2,2) == "10")
                {
                    this.txtVersion.Text = "1.0";
                }
                if(ser.Substring(2,2) == "15")
                {
                    this.txtVersion.Text = "1.5";
                }
                if(ser.Substring(2,1) == "2")
                {
                    this.txtVersion.Text = "2.0";
                }
                if(ser.Substring(2,1) == "3")
                {
                    this.txtVersion.Text = "2.5";
                }
                this.sernum = ((MaskedTextBox)sender).Text;
                this.btnOK.Enabled = true;
            }
            else
            {
                ((MaskedTextBox)sender).Focus();
                this.txtVersion.Text = string.Empty;
                this.btnOK.Enabled = false;
            }
        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {
            this.qty = Convert.ToInt32(((NumericUpDown)sender).Value);
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

                if(dealer == null)
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

            if(str.Trim().Length == 0)
            {
                ((XBrowseBox)sender)._Text = string.Empty;
                this.lblDealer.Text = string.Empty;
                this.dealer_id = null;
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                var dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();
                if(dealer != null)
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

        private void chkCDTraining_CheckedChanged(object sender, EventArgs e)
        {
            this.is_cdtraining = ((CheckBox)sender).Checked;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    dealer d = sn.dealer.Find(this.dealer_id);
                    istab v = sn.istab.Where(t => t.flag == 0 && t.tabtyp == istabDbf.TABTYP_VEREXT && t.typcod.Trim() == "0").FirstOrDefault();
                    if (this.is_newrwt)
                    {
                        v = sn.istab.Where(t => t.flag == 0 && t.tabtyp == istabDbf.TABTYP_VEREXT && t.typcod.Trim() == "1").FirstOrDefault();
                    }
                    if (this.is_newrwt_job)
                    {
                        v = sn.istab.Where(t => t.flag == 0 && t.tabtyp == istabDbf.TABTYP_VEREXT && t.typcod.Trim() == "2").FirstOrDefault();
                    }

                    for (int i = 0; i < this.qty; i++)
                    {
                        string ser_num = string.Empty;
                        if (i == 0)
                        {
                            ser_num = this.sernum;
                        }
                        else
                        {
                            var ser_num_11 = this.sernum.Substring(0, 6) + (Convert.ToInt32(this.sernum.Substring(6, 5)) + i).FillLeadingZero(5);
                            ser_num = ser_num_11 + GetLastDigit(ser_num_11);
                        }
                        //Console.WriteLine(" ==> sernum[" + i + "] : " + ser_num);

                        if(sn.serial.Where(s => s.flag == 0 && s.sernum == ser_num).FirstOrDefault() != null)
                        {
                            DialogResult q = MessageAlert.Show("S/N " + ser_num + " นี้มีอยู่แล้ว,\n - คลิก \"ตกลง\" เพื่อดำเนินการส่วนที่เหลือต่อไป\n - คลิก \"ยกเลิก\" เพื่อหยุดการทำงานนี้", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION);
                            if(q == DialogResult.OK)
                            {
                                continue;
                            }
                            else if(q == DialogResult.Cancel)
                            {
                                break;
                            }
                        }

                        serial ser = new serial
                        {
                            sernum = ser_num,
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
                            expdat = this.is_cdtraining ? (DateTime?)DateTime.Now : null,
                            branch = string.Empty,
                            manual = null,
                            upfree = string.Empty,
                            refnum = string.Empty,
                            remark = string.Empty,
                            dealer_id = d != null ? (int?)d.id : null,
                            dealercod = d != null ? d.dealercod : string.Empty,
                            verextdat = this.is_newrwt || this.is_newrwt_job ? (DateTime?)DateTime.Now : null,
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
                        sn.serial.Add(ser);
                        sn.SaveChanges();
                        this.serial_generated = ser;
                    }

                    //MessageAlert.Show("S/N Generate completed.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private string GetLastDigit(string sernum_11_digit)
        {
            if(sernum_11_digit.Trim().Length != 11)
            {
                return string.Empty;
            }

            var ascii_code = Encoding.ASCII.GetBytes(sernum_11_digit);
            int[,] arr_num = new int[,] { { 3, 9, 6, 2, 7, 4, 8, 1, 5, 9 }, { 5, 2, 8, 0, 1, 4, 7, 6, 9, 3 } };

            int val = 0;
            for (int i = 0;  i < 11;  i++)
            {
                if(ascii_code[i] >= 65 && ascii_code[i] <= 90)
                {
                    switch (ascii_code[i])
                    {
                        case 87:
                            val += arr_num[i%2, 0];
                            break;

                        case 66:
                            val += arr_num[i % 2, 1];
                            break;

                        case 67:
                            val += arr_num[i % 2, 2];
                            break;

                        case 84:
                            val += arr_num[i % 2, 3];
                            break;

                        case 72:
                            val += arr_num[i % 2, 4];
                            break;

                        case 68:
                            val += arr_num[i % 2, 5];
                            break;
                        default:
                            continue;
                    }
                }
                else if(ascii_code[i] >= 48 && ascii_code[i] <= 57)
                {
                    val += arr_num[i % 2, Convert.ToInt32(sernum_11_digit[i].ToString())];
                }
                else
                {
                    continue;
                }
            }
            return (val % 10).ToString();
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
