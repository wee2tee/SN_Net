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
    public partial class SearchBox : Form
    {
        public SearchBox()
        {
            InitializeComponent();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
