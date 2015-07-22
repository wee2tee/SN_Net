using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.Subform
{
    public partial class DealerWindow : Form
    {
        public string State = FormState.FORM_STATE_EDIT;

        public DealerWindow()
        {
            InitializeComponent();
        }

        public void collapseToolstrip()
        {
            //this.toolStrip1.SetBounds(this.toolStrip1.Location.X, this.toolStrip1.Location.Y, this.toolStrip1.ClientSize.Width, 0);
        }

        private void DealerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.State == FormState.FORM_STATE_ADD || this.State == FormState.FORM_STATE_EDIT)
            //{
            //    if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "Warning", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.OK)
            //    {
            //        MainForm main_form = this.MdiParent as MainForm;
            //        main_form.dealer_wind = null;
            //        e.Cancel = false;
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
            //else
            //{
            //    MainForm main_form = this.MdiParent as MainForm;
            //    main_form.dealer_wind = null;
            //}

            MainForm main_form = this.MdiParent as MainForm;
            main_form.dealer_wind = null;
        }
    }
}
