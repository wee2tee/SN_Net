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
    public partial class SerialPasswordList : Form
    {
        private List<SerialPassword> list_password;

        public SerialPasswordList(List<SerialPassword> list_password)
        {
            InitializeComponent();
            this.list_password = list_password;
        }

        private void SerialPasswordList_Load(object sender, EventArgs e)
        {
            this.FillDgvPassword();
        }

        private void FillDgvPassword()
        {
            this.dgvPassword.Rows.Clear();
            this.dgvPassword.Columns.Clear();
            this.dgvPassword.Tag = HelperClass.DGV_TAG.READ;

            this.dgvPassword.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Visible = false
            });

            this.dgvPassword.Columns.Add(new DataGridViewTextBoxColumn()
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            if (this.list_password == null)
                return;

            foreach (SerialPassword sp in this.list_password)
            {
                int r = this.dgvPassword.Rows.Add();
                this.dgvPassword.Rows[r].Tag = sp;

                this.dgvPassword.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvPassword.Rows[r].Cells[0].Value = sp.id;

                this.dgvPassword.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvPassword.Rows[r].Cells[1].Value = sp.pass_word;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == Keys.Enter)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
