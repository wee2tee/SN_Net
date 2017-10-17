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
    public enum PRINT_OUTPUT
    {
        SCREEN,
        PRINTER,
        FILE
    }

    public partial class DialogPrintDealerLabel : Form
    {
        public PRINT_OUTPUT output = PRINT_OUTPUT.SCREEN;
        public dealer dealer_from = null;
        public dealer dealer_to = null;
        public string condition = string.Empty;

        public DialogPrintDealerLabel(dealer dealer_from = null, dealer dealer_to = null, string condition = "")
        {
            this.dealer_from = dealer_from;
            this.dealer_to = dealer_to;
            this.condition = condition;
            InitializeComponent();
        }

        private void DialogPrintDealerLabel_Load(object sender, EventArgs e)
        {
            this.brFrom._Text = this.dealer_from != null ? this.dealer_from.dealercod : string.Empty;
            this.brTo._Text = this.dealer_to != null ? this.dealer_to.dealercod : string.Empty;
            this.txtCondition._Text = this.condition;
        }

        private void brFrom__ButtonClick(object sender, EventArgs e)
        {
            dealer d = null;
            using (snEntities sn = DBX.DataSet())
            {
                d = sn.dealer.Where(dl => dl.flag == 0 && string.Compare(dl.dealercod.Trim(), ((XBrowseBox)sender)._Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(dl => dl.dealercod).FirstOrDefault();
            }

            DialogSelectDealer inq = new DialogSelectDealer(d);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_dealer.dealercod;
                this.dealer_from = inq.selected_dealer;
            }
        }

        private void brFrom__Leave(object sender, EventArgs e)
        {
            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
            {
                this.dealer_from = null;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();
                    if (dealer != null)
                    {
                        this.dealer_from = dealer;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void brTo__ButtonClick(object sender, EventArgs e)
        {
            dealer d = null;
            using (snEntities sn = DBX.DataSet())
            {
                d = sn.dealer.Where(dl => dl.flag == 0 && string.Compare(dl.dealercod.Trim(), ((XBrowseBox)sender)._Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).OrderBy(dl => dl.dealercod).FirstOrDefault();
            }

            DialogSelectDealer inq = new DialogSelectDealer(d);
            Point p = ((XBrowseBox)sender).PointToScreen(Point.Empty);
            inq.Location = new Point(p.X + ((XBrowseBox)sender).Width, p.Y);
            if (inq.ShowDialog() == DialogResult.OK)
            {
                ((XBrowseBox)sender)._Text = inq.selected_dealer.dealercod;
                this.dealer_to = inq.selected_dealer;
            }
        }

        private void brTo__Leave(object sender, EventArgs e)
        {
            string str = ((XBrowseBox)sender)._Text.Trim();
            if (str.Trim().Length == 0)
            {
                this.dealer_to = null;
            }
            else
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var dealer = sn.dealer.Where(d => d.flag == 0 && d.dealercod.Trim() == str.Trim()).FirstOrDefault();
                    if (dealer != null)
                    {
                        this.dealer_to = dealer;
                    }
                    else
                    {
                        ((XBrowseBox)sender).Focus();
                        SendKeys.Send("{F6}");
                    }
                }
            }
        }

        private void txtCondition__TextChanged(object sender, EventArgs e)
        {
            this.condition = ((XTextEdit)sender)._Text;
        }

        private void rdScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                this.output = PRINT_OUTPUT.SCREEN;
        }

        private void rdPrinter_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                this.output = PRINT_OUTPUT.PRINTER;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                if(!(this.btnOK.Focused || this.btnCancel.Focused))
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
