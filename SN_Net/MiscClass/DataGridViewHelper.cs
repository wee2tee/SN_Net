using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SN_Net.DataModels;
using SN_Net.MiscClass;

namespace SN_Net.MiscClass
{
    public static class DataGridViewHelper
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

        public static void SetRowSelectedBorder(this DataGridView datagrid, DataGridViewCellPaintingEventArgs e)
        {
            if (datagrid.Focused)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    if (datagrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == true)
                    {
                        using (Pen p = new Pen(Color.Red, 1f))
                        {
                            Rectangle rect = datagrid.GetRowDisplayRectangle(e.RowIndex, false);
                            e.Graphics.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Top);
                            e.Graphics.DrawLine(p, rect.Left, rect.Bottom -1, rect.Right, rect.Bottom - 1);
                        }

                        e.Handled = true;
                    }
                }
            }
        }

        public static void FillLine(this DataGridView datagrid, int line_exist)
        {
            // disable sort (by column header clicked)
            foreach (DataGridViewColumn column in datagrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // remove exist blank row
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                if (row.Tag is string)
                {
                    if ((string)row.Tag == "BLANK")
                    {
                        datagrid.Rows.RemoveAt(row.Index);
                    }
                }
            }

            //// fill blank row to match datagridview height
            int header_row_height = datagrid.Columns[0].HeaderCell.ContentBounds.Height;
            int maximum_row = Convert.ToInt32((datagrid.Bounds.Height - header_row_height) / 25);
            int line_to_fill = (maximum_row - line_exist)+2;

            for (int i = 0; i < line_to_fill; i++)
            {
                int r = datagrid.Rows.Add();
                DataGridViewRow row = datagrid.Rows[r];
                row.Tag = "BLANK";
                row.Height = 25;
                row.ReadOnly = true;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.BackColor = Color.White;
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.White;
                    cell.Style.SelectionForeColor = Color.Black;
                    if (cell.ValueType == typeof(bool))
                    {
                        cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                row.Cells[0].Tag = new DataRowIntention(DataRowIntention.TO_DO.READ);
            }

            // add 1 row in the bottom in case of problem data row is overflow the datagrid bounds.
            int row_index = datagrid.Rows.Add();
            DataGridViewRow last_row = datagrid.Rows[row_index];
            last_row.Tag = "BLANK";
            last_row.Height = 25;
            last_row.ReadOnly = true;

            foreach (DataGridViewCell cell in last_row.Cells)
            {
                cell.Style.BackColor = Color.White;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.White;
                cell.Style.SelectionForeColor = Color.Black;
                if (cell.ValueType == typeof(bool))
                {
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }
    }
}
