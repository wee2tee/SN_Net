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
    public partial class DialogSellBook : Form
    {
        private MainForm main_form;
        private serial serial_to_sell;
        private int qty = 1;
        private string version = string.Empty;
        private DateTime? date = null;

        public DialogSellBook(MainForm main_form, serial serial_to_sell)
        {
            this.main_form = main_form;
            this.serial_to_sell = serial_to_sell;
            InitializeComponent();
        }

        private void DialogSellBook_Load(object sender, EventArgs e)
        {
            this.txtVersion.Text = this.serial_to_sell.version;
            this.dtDate._SelectedDate = DateTime.Now;
            this.ActiveControl = this.numQty;
            this.numQty.Select(0, this.numQty.Value.ToString().Length);
        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {
            this.qty = Convert.ToInt32(((NumericUpDown)sender).Value);
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {
            this.version = ((TextBox)sender).Text;
        }

        private void dtDate__SelectedDateChanged(object sender, EventArgs e)
        {
            this.date = ((XDatePicker)sender)._SelectedDate;
        }

        private void dtDate__Leave(object sender, EventArgs e)
        {

            if (this.date.HasValue)
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
            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    istab p = sn.istab.Where(i => i.flag == 0 && i.tabtyp == istabDbf.TABTYP_PROBCOD && i.typcod.Trim() == "--").FirstOrDefault();

                    problem prob = new problem
                    {
                        probdesc = "Manual was sold " + this.qty.ToString() + " book(s) (V." + this.version + ")",
                        date = this.date,
                        time = DateTime.Now.ToString("HH:mm", CultureInfo.GetCultureInfo("th-TH")),
                        name = "     Book",
                        serial_id = this.serial_to_sell.id,
                        probcod_id = p != null ? (int?)p.id : null,
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
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.btnOK.Focused || this.btnCancel.Focused)
                {
                    return false;
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
