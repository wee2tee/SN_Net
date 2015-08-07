using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.Subform
{
    public partial class SplashPreventMenustripActive : Form
    {
        public SplashPreventMenustripActive()
        {
            InitializeComponent();
        }

        private void SplashPreventMenustripActive_Shown(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
