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
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Test_Load(object sender, EventArgs e)
        {
            
        }

        public void Terminate()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
