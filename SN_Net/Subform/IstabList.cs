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
    public partial class IstabList : Form
    {
        private const string SORT_TYPCOD = "typcod";
        private const string SORT_TYPDES = "typdes";
        private string sort_by;
        private string tabtyp;
        private string selected_code;
        public Istab istab;

        public enum TITLE
        {
            AREA,
            BUSITYP,
            HOWKNOWN,
            PURCHASE_FROM,
            VEREXT
        }

        public IstabList(TITLE title_text, string selected_code)
        {
            InitializeComponent();
            
            this.setTitleText(title_text);
            this.setTabTyp(title_text);
            this.sort_by = SORT_TYPCOD;
            this.selected_code = selected_code;
        }

        private void setSelectedItem(){
            Console.WriteLine("selected_code is " + this.selected_code);
            foreach (DataGridViewRow row in this.dgvIstab.Rows)
	        {
                if ((string)row.Cells[1].Value == this.selected_code)
                {
                    row.Cells[1].Selected = true;
                }
	        }
            this.dgvIstab.Focus();
        }

        private void IstabList_Shown(object sender, EventArgs e)
        {
            this.dgvIstab.Focus();
            EscapeKeyToCloseDialog.ActiveEscToClose(this);
        }

        private void setTabTyp(TITLE title_text)
        {
            switch (title_text)
            {
                case TITLE.AREA:
                    this.tabtyp = "01";
                    break;
                case TITLE.BUSITYP:
                    this.tabtyp = "02";
                    break;
                case TITLE.HOWKNOWN:
                    this.tabtyp = "03";
                    break;
                case TITLE.PURCHASE_FROM:
                    this.tabtyp = "04";
                    break;
                case TITLE.VEREXT:
                    this.tabtyp = "05";
                    break;
                default:
                    this.tabtyp = "00";
                    break;
            }
        }

        private void setTitleText(TITLE title_text)
        {
            switch (title_text)
            {
                case TITLE.AREA:
                    this.Text = "Sales area";
                    break;
                case TITLE.BUSITYP:
                    this.Text = "Business type";
                    break;
                case TITLE.HOWKNOWN:
                    this.Text = "How to know Express";
                    break;
                case TITLE.PURCHASE_FROM:
                    this.Text = "Purchase from";
                    break;
                case TITLE.VEREXT:
                    this.Text = "Software Version(Extension)";
                    break;
                default:
                    this.Text = "Istab";
                    break;
            }
        }

        private void IstabList_Load(object sender, EventArgs e)
        {
            this.fillInDataGrid(this.loadIstabData(this.sort_by));
        }

        private List<Istab> loadIstabData(string sort_by)
        {
            List<Istab> istab = new List<Istab>();
            string url = ApiConfig.API_MAIN_URL + "istab/get_all&tabtyp=" + this.tabtyp + "&sort=" + sort_by;
            Console.WriteLine(url);
            CRUDResult get = ApiActions.GET(url);
            ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(get.data);
            if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                istab = sr.istab;
            }

            return istab;
        }

        private void fillInDataGrid(List<Istab> istabs)
        {
            // Clear old data
            this.dgvIstab.Columns.Clear();
            this.dgvIstab.Rows.Clear();
            this.dgvIstab.EnableHeadersVisualStyles = false;
            this.dgvIstab.ColumnHeadersDefaultCellStyle.BackColor = Color.YellowGreen;

            // Create column
            // ID
            DataGridViewTextBoxColumn text_col1 = new DataGridViewTextBoxColumn();
            text_col1.HeaderText = "ID.";
            text_col1.Width = 40;
            text_col1.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
            };
            text_col1.Visible = false;
            this.dgvIstab.Columns.Add(text_col1);

            DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
            text_col2.HeaderText = "รหัส";
            text_col2.Width = 100;
            text_col2.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
            };
            this.dgvIstab.Columns.Add(text_col2);

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            text_col3.HeaderText = "รายละเอียด";
            text_col3.Width = this.dgvIstab.ClientSize.Width - (text_col2.Width + 3);
            text_col3.HeaderCell.Style = new DataGridViewCellStyle()
            {
                Font = new Font("Tahoma", 9.75f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0, 3, 0, 3)
            };
            this.dgvIstab.Columns.Add(text_col3);

            foreach (Istab istab in istabs)
            {
                int r = this.dgvIstab.Rows.Add();
                this.dgvIstab.Rows[r].Tag = (int)istab.id;

                this.dgvIstab.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvIstab.Rows[r].Cells[0].Value = istab.id;
                this.dgvIstab.Rows[r].Cells[0].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    Alignment = DataGridViewContentAlignment.MiddleRight
                };

                this.dgvIstab.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[1].Value = istab.typcod;
                this.dgvIstab.Rows[r].Cells[1].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };


                this.dgvIstab.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[2].Value = istab.typdes_th;
                this.dgvIstab.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                {
                    Font = new Font("Tahoma", 9.75f),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                };
            }

            this.setSelectedItem();
        }

        private void IstabList_Resize(object sender, EventArgs e)
        {
            this.dgvIstab.Columns[2].Width = this.dgvIstab.ClientSize.Width - (this.dgvIstab.Columns[1].Width + 3);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IstabAddEditForm wind = new IstabAddEditForm(Istab.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.selected_code = wind.txtTypcod.Text;
                this.fillInDataGrid(this.loadIstabData(this.sort_by));
            }
        }

        private void dgvIstab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                int current_row_index = this.dgvIstab.CurrentCell.RowIndex;
                this.returnResultID(current_row_index);
            }
        }

        private void returnResultID(int row_index)
        {
            this.istab = new Istab();
            this.istab.id = (int)this.dgvIstab.Rows[row_index].Cells[0].Value;
            this.istab.tabtyp = this.tabtyp;
            this.istab.typcod = (string)this.dgvIstab.Rows[row_index].Cells[1].Value;
            this.istab.typdes_th = (string)this.dgvIstab.Rows[row_index].Cells[2].Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvIstab_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.returnResultID(e.RowIndex);
        }
    }
}
