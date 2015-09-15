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
    public partial class SellBookForm : Form
    {
        private GlobalVar G;
        private SnWindow parent_window;
        private Control current_focused_control;
        private bool is_show_calendar;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            EDIT,
            SAVING
        }

        public SellBookForm(SnWindow parent_window)
        {
            InitializeComponent();
            this.parent_window = parent_window;
            this.G = this.parent_window.G;
            this.form_mode = FORM_MODE.EDIT;

            List<MaskedTextBox> lmsk = new List<MaskedTextBox>();
            lmsk.Add(this.mskAsDate);
            List<DateTimePicker> ldtp = new List<DateTimePicker>();
            ldtp.Add(this.dpAsDate);
            PairDatePickerWithMaskedTextBox.Attach(lmsk, ldtp);
        }

        private void SellBookForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorResource.BACKGROUND_COLOR_BEIGE;
            this.txtVersion.Text = this.parent_window.serial.version;
            this.dpAsDate.Value = DateTime.Now;

            this.numQty.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.txtVersion.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.mskAsDate.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.btnOK.GotFocus += new EventHandler(this.onControlFocusedHandler);
            this.btnCancel.GotFocus += new EventHandler(this.onControlFocusedHandler);

            this.numQty.Enter += new EventHandler(this.onControlEnterHandler);
            this.txtVersion.Enter += new EventHandler(this.onControlEnterHandler);
            this.mskAsDate.Enter += new EventHandler(this.onControlEnterHandler);

            this.numQty.Leave += new EventHandler(this.onControlLeaveHandler);
            this.txtVersion.Leave += new EventHandler(this.onControlLeaveHandler);
            this.mskAsDate.Leave += new EventHandler(this.onControlLeaveHandler);

            this.numQty.Leave += delegate
            {
                if (this.numQty.Text.Length == 0)
                {
                    this.numQty.Text = "1";
                }
                Console.WriteLine(this.numQty.Value.ToString());
            };

            this.txtVersion.Leave += delegate
            {
                if (this.txtVersion.Text.Length == 0)
                {
                    this.txtVersion.Text = this.parent_window.serial.version;
                }
            };

            this.mskAsDate.Leave += delegate
            {
                if (!this.mskAsDate.Text.tryParseToDateTime())
                {
                    this.mskAsDate.pickedDate(dpAsDate.Value.Year.ToString() + "-" + dpAsDate.Value.Month.ToString() + "-" + dpAsDate.Value.Day.ToString());
                }
            };

            this.dpAsDate.DropDown += delegate
            {
                this.is_show_calendar = true;
            };

            this.dpAsDate.CloseUp += delegate
            {
                this.is_show_calendar = false;
                this.mskAsDate.Focus();
            };
        }
        
        private void onControlEnterHandler(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MaskedTextBox || sender is NumericUpDown)
            {
                ((Control)sender).BackColor = ColorResource.ACTIVE_CONTROL_BACKCOLOR;
                ((Control)sender).ForeColor = Color.Black;
            }
        }

        private void onControlFocusedHandler(object sender, EventArgs e)
        {
            this.current_focused_control = (Control)sender;

            if (sender is TextBox)
            {
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            if (sender is MaskedTextBox)
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            if (sender is NumericUpDown)
            {
                ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Text.Length);
            }
        }

        private void onControlLeaveHandler(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
            ((Control)sender).ForeColor = Color.Black;
        }

        private void formSaving()
        {
            this.form_mode = FORM_MODE.SAVING;
            this.numQty.Enabled = false;
            this.txtVersion.Enabled = false;
            this.mskAsDate.Enabled = false;
            this.dpAsDate.Enabled = false;
            this.btnOK.Enabled = false;
            this.btnCancel.Enabled = false;
            this.toolStripProcess.Visible = true;
        }

        private void formEdit()
        {
            this.form_mode = FORM_MODE.EDIT;
            this.numQty.Enabled = true;
            this.txtVersion.Enabled = true;
            this.mskAsDate.Enabled = true;
            this.dpAsDate.Enabled = true;
            this.btnOK.Enabled = true;
            this.btnCancel.Enabled = true;
            this.toolStripProcess.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool post_success = false;
            this.formSaving();

            BackgroundWorker workerSave = new BackgroundWorker();
            workerSave.DoWork += delegate
            {
                //string json_data = "{\"id\":" + this.parent_window.serial.id.ToString() + ",";
                string json_data = "{\"sernum\":\"" + this.parent_window.serial.sernum + "\",";
                json_data += "\"qty\":" + this.numQty.Value.ToString() + ",";
                json_data += "\"version\":\"" + this.txtVersion.Text + "\",";
                json_data += "\"asdate\":\"" + this.mskAsDate.Text.toMySQLDate() + "\",";
                json_data += "\"users_name\":\"" + this.G.loged_in_user_name + "\"}";

                Console.WriteLine(json_data);

                CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "problem/gen_sell_book", json_data);
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    post_success = true;
                    this.parent_window.problem = sr.problem;
                    this.parent_window.problem_im_only = (sr.problem.Count > 0 ? sr.problem.Where<Problem>(t => t.probcod == "IM").ToList<Problem>() : new List<Problem>());
                }
                else
                {
                    MessageAlert.Show(sr.message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    post_success = false;
                }
            };

            workerSave.RunWorkerCompleted += delegate
            {
                if (post_success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

            workerSave.RunWorkerAsync();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.EDIT)
                {
                    if (!(this.current_focused_control is Button))
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }
            if (keyData == Keys.F6)
            {
                if (this.form_mode == FORM_MODE.EDIT && this.current_focused_control == this.mskAsDate)
                {
                    this.dpAsDate.Focus();
                    SendKeys.Send("{F4}");
                }
            }
            if (keyData == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.EDIT && !this.is_show_calendar)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
