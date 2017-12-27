using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.Model;
using System.Globalization;

namespace SN_Net.Subform
{
    public partial class DialogAbsent : Form
    {
        private MainForm main_form;
        private CustomDateEvent3 custom_date_event;
        private bool perform_add;

        public DialogAbsent(MainForm main_form, CustomDateEvent3 custom_date_event, bool perform_add = false)
        {
            this.main_form = main_form;
            this.custom_date_event = custom_date_event;
            this.perform_add = perform_add;
            InitializeComponent();
        }

        private void DialogAbsent_Load(object sender, EventArgs e)
        {
            this.groupBox1.Text = this.custom_date_event.curr_date.ToString("วันdddd ที่ dd MMMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            if (this.perform_add)
                this.btnAddItem.PerformClick();
                
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {

        }
    }
}
