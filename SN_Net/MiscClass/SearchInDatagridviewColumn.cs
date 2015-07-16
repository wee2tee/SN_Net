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
                int compare_result;
                string str_source = (string)row.Cells[col_index].Value;
                
                if (str_source.Length > keyword.Length)
                {
                    compare_result = str_source.Substring(0, keyword.Length).CompareTo(keyword);
                }
                else
                {
                    compare_result = str_source.CompareTo(keyword);
                }
                
                if(compare_result == 0)
                {
                    row.Cells[col_index].Selected = true;
                    datagrid.Focus();
                    break;
                }
                else if(compare_result > 0)
                {
                    row.Cells[col_index].Selected = true;
                    datagrid.Focus();
                    break;
                }
                else
                {
                    datagrid.Rows[0].Cells[col_index].Selected = true;
                    continue;
                }
            }
        }
    }
}
