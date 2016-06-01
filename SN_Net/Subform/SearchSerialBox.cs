using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class SearchSerialBox : Form
    {
        public string search_sn;
        public enum SEARCH_MODE
        {
            SERNUM,
            CONTACT,
            COMPNAM,
            DEALER,
            OLDNUM,
            BUSITYP,
            AREA,
            USERGROUP
        }
        private SEARCH_MODE search_mode;

        public SearchSerialBox(SEARCH_MODE search_mode)
        {
            InitializeComponent();
            this.search_mode = search_mode;
            this.initializeSearchWind();
        }

        private void SearchSerialBox_Shown(object sender, EventArgs e)
        {
            if (this.mskSearchKey.Visible)
            {
                this.mskSearchKey.Focus();
                this.mskSearchKey.SelectionStart = 0;
                this.mskSearchKey.SelectionLength = this.mskSearchKey.Text.Length;
            }
            else
            {
                this.txtSearchKey.Focus();
                this.txtSearchKey.SelectionStart = 0;
                this.txtSearchKey.SelectionLength = this.txtSearchKey.Text.Length;
            }
        }

        private void initializeSearchWind()
        {
            this.txtSearchKey.SetBounds(this.txtSearchKey.Location.X, this.mskSearchKey.Location.Y, this.txtSearchKey.ClientSize.Width, this.txtSearchKey.ClientSize.Height);

            switch (this.search_mode)
            {
                case SEARCH_MODE.SERNUM:
                    this.lblSearchKey.Text = "Serial No.";
                    this.mskSearchKey.Visible = true;
                    this.txtSearchKey.Visible = false;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("en-US"));
                    break;
                case SEARCH_MODE.CONTACT:
                    this.lblSearchKey.Text = "Contact";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("th-TH"));
                    break;
                case SEARCH_MODE.COMPNAM:
                    this.lblSearchKey.Text = "Company";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("th-TH"));
                    break;
                case SEARCH_MODE.DEALER:
                    this.lblSearchKey.Text = "Dealer Code";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("en-US"));
                    break;
                case SEARCH_MODE.OLDNUM:
                    this.lblSearchKey.Text = "Old Serial";
                    this.mskSearchKey.Visible = true;
                    this.txtSearchKey.Visible = false;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("en-US"));
                    break;
                case SEARCH_MODE.BUSITYP:
                    this.lblSearchKey.Text = "Business Type Code";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("en-US"));
                    break;
                case SEARCH_MODE.AREA:
                    this.lblSearchKey.Text = "Area";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("th-TH"));
                    break;
                case SEARCH_MODE.USERGROUP:
                    this.lblSearchKey.Text = "Group Code";
                    this.mskSearchKey.Visible = false;
                    this.txtSearchKey.Visible = true;
                    this.SetInputLanguage(CultureInfo.GetCultureInfo("en-US"));
                    break;
                default:
                    break;
            }
        }

        private void SetInputLanguage(CultureInfo culture)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.Equals(culture))
                {
                    InputLanguage.CurrentInputLanguage = lang;
                }
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.submitSearch();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.cancelSearch();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.submitSearch();
        }

        private void submitSearch()
        {
            this.DialogResult = DialogResult.OK;
            this.search_sn = this.mskSearchKey.Text;
            this.Close();
        }

        private void cancelSearch()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
