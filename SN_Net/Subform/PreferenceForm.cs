using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SN_Net.MiscClass;

namespace SN_Net.Subform
{
    public partial class PreferenceForm : Form
    {
        public PreferenceForm()
        {
            InitializeComponent();
        }

        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            this.loadPreferenceSettings();
        }

        private void loadPreferenceSettings()
        {
            if (File.Exists("SN_Net.pref"))
            {
                MessageBox.Show("have preference file");
            }
            else
            {
                MessageBox.Show("not have preference file");
            }
        }

        private void savePreferenceSettings()
        {

        }
    }
}
