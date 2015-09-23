using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.DataModels;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace SN_Net.MiscClass
{
    public static class HelperClass
    {
        public static string ToTabtypString(this Istab.TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case Istab.TABTYP.AREA:
                    return "01";
                case Istab.TABTYP.VEREXT:
                    return "02";
                case Istab.TABTYP.HOWKNOWN:
                    return "03";
                case Istab.TABTYP.BUSITYP:
                    return "04";
                case Istab.TABTYP.PROBLEM_CODE:
                    return "05";
                default:
                    return "00";
            }
        }

        public static void KeepLog(this Form form, string msg)
        {
            using (StreamWriter file = new StreamWriter("SN_Log.txt", true))
            {
                file.WriteLine(msg);
            }
        }

        public static string ToYesOrNoString(this CheckState check_state)
        {
            if (check_state == CheckState.Checked)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        public static string GetVerextSelectedString(this string verext, ComboBox cbVerext)
        {
            foreach (ComboboxItem item in cbVerext.Items)
            {
                if (item.string_value == verext)
                {
                    return item.ToString();
                }
            }
            return "";
        }

        public static void DrawLineEffect(this DataGridView datagrid)
        {
            datagrid.Paint += delegate
            {
                if (datagrid.Rows.Count > 0)
                {
                    Rectangle rect = datagrid.GetRowDisplayRectangle(datagrid.CurrentCell.RowIndex, true);
                    Graphics g = datagrid.CreateGraphics();
                    if (datagrid.Rows[datagrid.CurrentCell.RowIndex].Cells[0].Tag is DataRowIntention)
                    {
                        if (((DataRowIntention)datagrid.Rows[datagrid.CurrentCell.RowIndex].Cells[0].Tag).to_do == DataRowIntention.TO_DO.DELETE)
                        {
                            Pen p = new Pen(Color.Red, 1f);

                            for (int i = rect.Left - 16; i < rect.Right; i += 8)
                            {
                                g.DrawLine(p, i, rect.Bottom - 2, i + 23, rect.Top);
                            }
                        }
                        else
                        {
                            g.DrawLine(new Pen(Color.Red), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                            g.DrawLine(new Pen(Color.Red), rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                        }
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.Red), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        g.DrawLine(new Pen(Color.Red), rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                    }
                }
            };
        }

        public static bool tryParseToDateTime(this String str_date)
        {
            CultureInfo cinfo_th = new CultureInfo("th-TH");

            DateTime out_date;
            if (DateTime.TryParse(str_date, cinfo_th, DateTimeStyles.None, out out_date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ToMysqlDate(this DateTime date)
        {
            CultureInfo cinfo_en = CultureInfo.CreateSpecificCulture("en-US"); // specify culture to US for retrieve the year in US culture format
            return date.ToString("yyyy-MM-dd", cinfo_en.DateTimeFormat);
        }
    }
}
