using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public static class SearchInDatagridview
    {
        public static void Search(this DataGridView datagrid, string keyword, int col_index)
        {

            foreach (DataGridViewRow row in datagrid.Rows)
            {
                string typcod = (string)row.Cells[col_index].Value;
                if (typcod.Length >= keyword.Length)
                {
                    if (typcod.Substring(0, keyword.Length) == keyword)
                    {
                        row.Cells[col_index].Selected = true;
                        datagrid.Focus();
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
