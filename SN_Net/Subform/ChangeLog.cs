using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SN_Net.Subform
{
    public partial class ChangeLog : Form
    {
        string[] log_lines;

        public ChangeLog()
        {
            InitializeComponent();
        }

        private void ChangeLog_Load(object sender, EventArgs e)
        {
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/changeLog.txt"))
            {
                log_lines = System.IO.File.ReadAllLines( AppDomain.CurrentDomain.BaseDirectory + "/changeLog.txt", Encoding.UTF8);
                this.rtbLog.Lines = log_lines;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
