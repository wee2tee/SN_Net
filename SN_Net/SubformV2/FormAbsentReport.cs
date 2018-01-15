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
    public partial class FormAbsentReport : Form
    {
        private users users_from = null;
        private users users_to = null;
        private DateTime? date_from = null;
        private DateTime? date_to = null;

        public FormAbsentReport()
        {
            InitializeComponent();
        }

        private void FormAbsentReport_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
        }

        private void FormAbsentReport_Shown(object sender, EventArgs e)
        {
            if(this.users_from == null || this.users_to == null || this.date_from == null || this.date_to == null)
            {
                DialogAbsentReportScope scope = new DialogAbsentReportScope();
                if(scope.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                else
                {
                    this.users_from = scope.user_from;
                    this.users_to = scope.user_to;
                    this.date_from = scope.date_from;
                    this.date_to = scope.date_to;
                }
            }
            
        }
    }
}