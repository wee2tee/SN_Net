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
    public partial class UpNewRwtLineForm : Form
    {
        private GlobalVar G;
        private SnWindow parent_form;
        private string verext;
        private DIALOG_TYPE dialog_type;
        private Control current_focused_control;
        private bool save_result = false;

        public enum DIALOG_TYPE
        {
            UP_NEWRWT,
            UP_NEWRWT_JOB
        }

        public UpNewRwtLineForm(SnWindow parent_form, DIALOG_TYPE dialog_type)
        {
            InitializeComponent();
            this.parent_form = parent_form;
            this.G = this.parent_form.G;
            this.dialog_type = dialog_type;
            verext = (this.dialog_type == DIALOG_TYPE.UP_NEWRWT ? "1" : (this.dialog_type == DIALOG_TYPE.UP_NEWRWT_JOB ? "2" : this.parent_form.serial.verext));
            this.Text = (this.dialog_type == DIALOG_TYPE.UP_NEWRWT_JOB ? "Gen 'Up New RWT + Job' line" : this.Text);
        }

        
        private void UpNewRwtLineForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;

            this.chkGreendisc.GotFocus += new EventHandler(this.keepCurrentFocusedControl);
            this.chkPinkdisc.GotFocus += new EventHandler(this.keepCurrentFocusedControl);
            this.btnOK.GotFocus += new EventHandler(this.keepCurrentFocusedControl);
            this.btnCancel.GotFocus += new EventHandler(this.keepCurrentFocusedControl);
        }

        private void UpNewRwtLineForm_Shown(object sender, EventArgs e)
        {
            this.btnOK.Enabled = true;
            this.chkGreendisc.Focus();
        }

        private void keepCurrentFocusedControl(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.formSaving();
            BackgroundWorker workerSave = new BackgroundWorker();
            workerSave.DoWork += new DoWorkEventHandler(this.workerSave_Dowork);
            workerSave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.workerSave_Complete);
            workerSave.RunWorkerAsync();
        }

        private void workerSave_Dowork(object sender, DoWorkEventArgs e)
        {
            string is_greendisc = this.chkGreendisc.CheckState.ToYesOrNoString();
            string is_pinkdisc = this.chkPinkdisc.CheckState.ToYesOrNoString();

            string json_data = "{\"id\":" + this.parent_form.serial.id.ToString() + ",";
            json_data += "\"verext\":\"" + this.verext + "\",";
            json_data += "\"is_greendisc\":\"" + is_greendisc + "\",";
            json_data += "\"is_pinkdisc\":\"" + is_pinkdisc + "\",";
            json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

            CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "serial/up_new_rwt_line", json_data);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.parent_form.serial = sr.serial[0];
                this.parent_form.problem = sr.problem;
                this.parent_form.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : new List<Problem>());
                this.parent_form.verext = sr.verext[0];
                this.save_result = true;
            }
            else
            {
                MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                this.save_result = false;
            }
        }

        private void workerSave_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.save_result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.formEdit();
            }
        }

        private void formSaving()
        {
            this.chkGreendisc.Enabled = false;
            this.chkPinkdisc.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.toolStripProcess.Visible = true;
        }

        private void formEdit()
        {
            this.chkGreendisc.Enabled = true;
            this.chkPinkdisc.Enabled = true;
            this.btnOK.Enabled = (this.chkGreendisc.Checked || this.chkPinkdisc.Checked ? true : false);
            this.btnCancel.Enabled = true;
            this.toolStripProcess.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (!(this.current_focused_control is Button))
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }
            }
            if (keyData == Keys.Escape)
            {
                this.btnCancel.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
