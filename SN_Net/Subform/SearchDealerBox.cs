using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;

namespace SN_Net.Subform
{
    public partial class SearchDealerBox : Form
    {
        public SEARCH_TYPE search_type;
        public enum SEARCH_TYPE
        {
            DEALER,
            CONTACT,
            NAME,
            AREA
        }

        public SearchDealerBox()
        {
            InitializeComponent();
        }

        public SearchDealerBox(SEARCH_TYPE search_type)
            : this()
        {
            this.search_type = search_type;
        }

        private void SearchDealerBox_Load(object sender, EventArgs e)
        {
            this.SetText();
            //this.txtKeyWord.Enter += delegate
            //{
            //    this.txtKeyWord.BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
            //    this.txtKeyWord.ForeColor = Color.Black;
            //};
            //this.txtKeyWord.GotFocus += delegate
            //{
            //    this.txtKeyWord.SelectionStart = this.txtKeyWord.Text.Length;
            //};
            //this.txtKeyWord.Leave += delegate
            //{
            //    this.txtKeyWord.BackColor = Color.White;
            //    this.txtKeyWord.ForeColor = Color.Black;
            //};
        }

        private void SearchDealerBox_Shown(object sender, EventArgs e)
        {
            this.txtKeyWord.Focus();
        }

        private void SetText()
        {
            switch (this.search_type)
            {
                case SEARCH_TYPE.DEALER:
                    this.lblSearchBy.Text = "Dealer Code";
                    break;
                case SEARCH_TYPE.CONTACT:
                    this.lblSearchBy.Text = "Contact Name";
                    break;
                case SEARCH_TYPE.NAME:
                    this.lblSearchBy.Text = "Name";
                    break;
                case SEARCH_TYPE.AREA:
                    this.lblSearchBy.Text = "Area Code";
                    break;
                default:
                    this.lblSearchBy.Text = "";
                    break;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.btnGo.PerformClick();
            }
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
