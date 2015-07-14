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
    public partial class DealerWindow : Form
    {
        public DealerWindow()
        {
            InitializeComponent();
        }

        public void collapseToolstrip()
        {
            this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 0);
        }

    }
}
