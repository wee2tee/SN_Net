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
    public partial class SearchHistory : Form
    {
        private MainForm main_form;
        private List<Users> list_users;
        private List<SpyLog> list_spylog = new List<SpyLog>();
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            PROCESSING,
            READING
        }

        public SearchHistory(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void SearchHistory_Load(object sender, EventArgs e)
        {
            this.txtDummy.Width = 0;
            this.BindingControlEvent();
            this.LoadDependenciesData();
            this.InitControl();
        }

        private void SearchHistory_Shown(object sender, EventArgs e)
        {
            this.FormReading();
            this.btnGo.PerformClick();
        }

        private void BindingControlEvent()
        {
            this.dgvHistory.Paint += delegate
            {
                if (this.dgvHistory.CurrentCell != null)
                {
                    Rectangle rect = this.dgvHistory.GetRowDisplayRectangle(this.dgvHistory.CurrentCell.RowIndex, true);

                    using(Pen p = new Pen(Color.Red))
                    {
                        this.dgvHistory.CreateGraphics().DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        this.dgvHistory.CreateGraphics().DrawLine(p, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                    }
                }
            };

            this.txtCompnam.Enter += delegate
            {
                if ((this.rbSernum.Checked == false) && (this.rbCompnam.Checked == false))
                {
                    this.rbSernum.Checked = true;
                }
            };

            this.cbUsers.Leave += delegate
            {
                if (this.cbUsers.Items.Cast<ComboboxItem>().Where(c => c.name.Length >= this.cbUsers.Text.Length).Where(c => c.name.Substring(0, this.cbUsers.Text.Length) == this.cbUsers.Text).Count<ComboboxItem>() > 0)
                {
                    this.cbUsers.SelectedItem = this.cbUsers.Items.Cast<ComboboxItem>().Where(c => c.name.Length >= this.cbUsers.Text.Length).Where(c => c.name.Substring(0, this.cbUsers.Text.Length) == this.cbUsers.Text).First<ComboboxItem>();
                }
                else
                {
                    this.cbUsers.Focus();
                    SendKeys.Send("{F6}");
                }
            };
        }

        private void LoadDependenciesData()
        {
            #region Get users(support) data from server and push into cbSupport
            //CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_support_users");
            CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_users = sr.users;
            }
            else
            {
                this.list_users = new List<Users>();
            }
            #endregion Get users(support) data from server and push into cbSupport
        }

        private void InitControl()
        {
            #region Initial cbSupport
            this.cbUsers.Items.Add(new ComboboxItem("* All", -1, "*") { Tag = new Users() });
            foreach (Users u in this.list_users)
            {
                this.cbUsers.Items.Add(new ComboboxItem(u.username + " : " + u.name, u.id, u.username){ Tag = u});
            }
            this.cbUsers.SelectedIndex = 0;
            #endregion Initial cbSupport

            #region Initial dtStart,dtEnd date
            this.dtStart.Value = DateTime.Now;
            this.dtEnd.Value = DateTime.Now;
            #endregion Initial dtStart,dtEnd date
        }

        private void FillDgvHistory()
        {
            this.dgvHistory.Rows.Clear();
            this.dgvHistory.Columns.Clear();

            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            { 
                Visible = false,
                Name = "col_id",
                HeaderText = "ID"
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_seq",
                HeaderText = "ลำดับ",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            { 
                Name = "col_date",
                HeaderText = "วันที่",
                Width = 80
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_time",
                HeaderText = "เวลา",
                Width = 80
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_support",
                HeaderText = "Support #",
                Width = 120
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_sernum",
                HeaderText = "ค้นหาด้วย S/N",
                Width = 120
            });
            this.dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "col_compnam",
                HeaderText = "ค้นหาด้วย ชื่อลูกค้า",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            int seq = 0;

            foreach (SpyLog log in this.list_spylog)
            {
                int r = this.dgvHistory.Rows.Add();
                this.dgvHistory.Rows[r].Tag = log;

                this.dgvHistory.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvHistory.Rows[r].Cells[0].Value = log.id;

                this.dgvHistory.Rows[r].Cells[1].ValueType = typeof(int);
                this.dgvHistory.Rows[r].Cells[1].Value = ++seq;

                this.dgvHistory.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[2].Value = log.date.M2WDate();

                this.dgvHistory.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[3].Value = log.time;

                this.dgvHistory.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[4].Value = log.users_name;

                this.dgvHistory.Rows[r].Cells[5].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[5].Value = log.serial_sernum;

                this.dgvHistory.Rows[r].Cells[6].ValueType = typeof(string);
                this.dgvHistory.Rows[r].Cells[6].Value = log.compnam;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            bool get_success = false;
            string err_msg = "";
            this.FormProcessing();

            string users_name = ((Users)((ComboboxItem)this.cbUsers.SelectedItem).Tag).username;
            string date_from = this.dtStart.Value.ToMysqlDate();
            string date_to = this.dtEnd.Value.ToMysqlDate();
            string sernum = (this.rbSernum.Checked ? this.txtCompnam.Texts : null);
            string compnam = (this.rbCompnam.Checked ? this.txtCompnam.Texts : null);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                CRUDResult get = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "spylog/get_log&users_name=" + users_name + "&date_from=" + date_from + "&date_to=" + date_to + (sernum != null ? "&sernum=" + sernum : "") + (compnam != null ? "&compnam=" + compnam : ""));
                ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);

                if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                {
                    get_success = true;
                    this.list_spylog = sr.spy_log;
                }
                else
                {
                    get_success = false;
                    err_msg = sr.message;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                this.FormReading();
                if (get_success)
                {
                    this.FillDgvHistory();
                }
                else
                {
                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            };
            worker.RunWorkerAsync();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.InitControl();
            this.btnGo.PerformClick();
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;

            this.toolStripProcessing.Visible = true;
            this.dgvHistory.Enabled = false;
            this.dtStart.Enabled = false;
            this.dtEnd.Enabled = false;
            this.cbUsers.Enabled = false;
            this.txtCompnam.Read_Only = true;
        }

        private void FormReading()
        {
            this.form_mode = FORM_MODE.READING;

            this.toolStripProcessing.Visible = false;
            this.dgvHistory.Enabled = true;
            this.dtStart.Enabled = true;
            this.dtEnd.Enabled = true;
            this.cbUsers.Enabled = true;
            this.txtCompnam.Read_Only = false;
            this.txtDummy.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.READING)
                {
                    if (!(this.btnGo.Focused || this.btnReset.Focused))
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }
            if (keyData == Keys.F5)
            {
                this.btnGo.PerformClick();
                return true;
            }
            if (keyData == (Keys.Control | Keys.F5))
            {
                this.btnReset.PerformClick();
                return true;
            }
            if (keyData == Keys.F6)
            {
                if (this.cbUsers.Focused)
                {
                    SendKeys.Send("{F4}");
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.main_form.searchhistory_wind = null;
            base.OnClosing(e);
        }
    }
}
