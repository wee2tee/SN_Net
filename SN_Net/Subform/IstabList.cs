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

        public enum TITLE
        {
            AREA,
            BUSITYP,
            HOWKNOWN,
            PURCHASE_FROM,
            VEREXT
        }

        public IstabList(TITLE title_text)
        {
            InitializeComponent();
            this.setTitleText(title_text);
            this.setTabTyp(title_text);
            this.sort_by = SORT_TYPCOD;
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

            // Create column
            // ID
            DataGridViewTextBoxColumn text_col1 = new DataGridViewTextBoxColumn();
            int c1 = this.dgvIstab.Columns.Add(text_col1);
            this.dgvIstab.Columns[c1].HeaderText = "ID.";
            this.dgvIstab.Columns[c1].Width = 40;
            this.dgvIstab.Columns[c1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn text_col2 = new DataGridViewTextBoxColumn();
            int c2 = this.dgvIstab.Columns.Add(text_col2);
            this.dgvIstab.Columns[c2].HeaderText = "รหัส";
            this.dgvIstab.Columns[c2].Width = 100;
            this.dgvIstab.Columns[c2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn text_col3 = new DataGridViewTextBoxColumn();
            int c3 = this.dgvIstab.Columns.Add(text_col3);
            this.dgvIstab.Columns[c3].HeaderText = "รายละเอียด";
            this.dgvIstab.Columns[c3].Width = this.dgvIstab.ClientSize.Width - (this.dgvIstab.Columns[c1].Width + this.dgvIstab.Columns[c2].Width + 4);

            foreach (Istab istab in istabs)
            {
                int r = this.dgvIstab.Rows.Add();
                this.dgvIstab.Rows[r].Tag = (int)istab.id;

                this.dgvIstab.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvIstab.Rows[r].Cells[0].Value = istab.id;
                this.dgvIstab.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.dgvIstab.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[1].Value = istab.typcod;
                this.dgvIstab.Rows[r].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                this.dgvIstab.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvIstab.Rows[r].Cells[2].Value = istab.typdes;
                this.dgvIstab.Rows[r].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                this.dgvIstab.Rows[r].Cells[2].Style = new DataGridViewCellStyle()
                {
                    BackColor = Color.White,
                    Font = new Font("Tahoma", 9.75f),
                    ForeColor = SystemColors.WindowText,
                    SelectionBackColor = Color.Blue,
                    SelectionForeColor = Color.White,
                };
            }
        }

        private void IstabList_Resize(object sender, EventArgs e)
        {
            this.dgvIstab.Columns[2].Width = this.dgvIstab.ClientSize.Width - (this.dgvIstab.Columns[0].Width + this.dgvIstab.Columns[1].Width + 4);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IstabAddEditForm wind = new IstabAddEditForm(IstabAddEditForm.TABTYP.BUSITYP);
            if (wind.ShowDialog() == DialogResult.OK)
            {
                this.fillInDataGrid(this.loadIstabData(this.sort_by));
            }
        }
    }
}
