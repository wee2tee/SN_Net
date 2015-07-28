using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SN_Net.DataModels;
using WebAPI;
using WebAPI.ApiResult;
using Newtonsoft.Json;

namespace SN_Net.MiscClass
{
    public class PairTextBoxWithBrowseButton
    {
        public enum SELECTION_DATA
        {
            AREA,
            VEREXT,
            HOWKNOWN,
            BUSITYP,
            PROBLEM_CODE,
            DEALER
        }

        private List<TextBox> list_tb = new List<TextBox>();
        private List<Button> list_btn = new List<Button>();
        private List<Label> list_lbl = new List<Label>();
        private List<SELECTION_DATA> list_select = new List<SELECTION_DATA>();
        private List<Istab> list_area;
        private List<Istab> list_verext;
        private List<Istab> list_howknown;
        private List<Istab> list_busityp;
        private List<Istab> list_problem_code;
        private List<Dealer> list_dealer;

        private PairTextBoxWithBrowseButton(List<TextBox> tb, List<Button> btn, List<Label> lbl, List<SELECTION_DATA> selection_data, DataResource data_resource)
        {
            this.list_tb = tb;
            this.list_btn = btn;
            this.list_lbl = lbl;
            this.list_select = selection_data;
            this.list_area = data_resource.LIST_AREA;
            this.list_verext = data_resource.LIST_VEREXT;
            this.list_howknown = data_resource.LIST_HOWKNOWN;
            this.list_busityp = data_resource.LIST_BUSITYP;
            this.list_problem_code = data_resource.LIST_PROBLEM_CODE;
            this.list_dealer = data_resource.LIST_DEALER;
            this.addAction();
        }

        public static void Attach(List<TextBox> list_textbox, List<Button> list_button, List<Label> list_label, List<SELECTION_DATA> list_selection_data, DataResource data_resource)
        {
            PairTextBoxWithBrowseButton f6 = new PairTextBoxWithBrowseButton(list_textbox, list_button, list_label, list_selection_data, data_resource);
        }

        private void addAction()
        {
            foreach (TextBox tb in this.list_tb)
            {
                tb.KeyDown += new KeyEventHandler(this.KeyPressEventHandler);
                tb.EnabledChanged += new EventHandler(this.EnableChangeHandler);
                tb.ReadOnlyChanged += new EventHandler(this.ReadOnlyChangeHandler);
                //tb.Leave += new EventHandler(this.ValidateInputData);
            }

            foreach (Button btn in this.list_btn)
            {
                btn.TabIndex = 999;
                btn.TabStop = false;
            }
        }

        private void KeyPressEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                Control control = sender as Control;
                //int ndx = this.list_tb.FindIndex(new System.Predicate<TextBox>((value) => { return value == tb; }));
                //int ndx = this.list_tb.FindIndex(new Predicate<TextBox>((value) => { return value == tb; }));
                int ndx = this.list_tb.FindIndex( t => t.Equals(control));
                this.list_btn[ndx].PerformClick();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                string str_input = ((TextBox)sender).Text;
                int ndx = list_tb.FindIndex(t => t.Equals((TextBox)sender));
                switch (this.list_select[ndx])
                {
                    #region Case Area
                    case SELECTION_DATA.AREA:
                        if (str_input.Length > 0)
                        {
                            Istab area = this.list_area.Find(t => t.typcod == str_input);
                            if (area != null)
                            {
                                list_lbl[ndx].Text = area.typdes_th;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Area
                    #region Case Verext
                    case SELECTION_DATA.VEREXT:
                        if (str_input.Length > 0)
                        {
                            Istab verext = this.list_verext.Find(t => t.typcod == str_input);
                            if (verext != null)
                            {
                                list_lbl[ndx].Text = verext.typdes_th;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Verext
                    #region Case Howknown
                    case SELECTION_DATA.HOWKNOWN:
                        if (str_input.Length > 0)
                        {
                            Istab howknown = this.list_howknown.Find(t => t.typcod == str_input);
                            if (howknown != null)
                            {
                                list_lbl[ndx].Text = howknown.typdes_th;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Howknown
                    #region Case Busityp
                    case SELECTION_DATA.BUSITYP:
                        if (str_input.Length > 0)
                        {
                            Istab busityp = this.list_busityp.Find(t => t.typcod == str_input);
                            if (busityp != null)
                            {
                                list_lbl[ndx].Text = busityp.typdes_th;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Busityp
                    #region Case Problem_code
                    case SELECTION_DATA.PROBLEM_CODE:
                        if (str_input.Length > 0)
                        {
                            Istab problem_code = this.list_problem_code.Find(t => t.typcod == str_input);
                            if (problem_code != null)
                            {
                                list_lbl[ndx].Text = problem_code.typdes_th;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Problem_code
                    #region Case Dealer
                    case SELECTION_DATA.DEALER:
                        if (str_input.Length > 0)
                        {
                            Dealer dealer = this.list_dealer.Find(t => t.dealer == str_input);
                            if (dealer != null)
                            {
                                list_lbl[ndx].Text = dealer.prenam + " " + dealer.compnam;
                            }
                            else
                            {
                                this.list_btn[ndx].PerformClick();
                            }
                        }
                        else
                        {
                            this.list_lbl[ndx].Text = "";
                        }
                        break;
                    #endregion Case Dealer
                    default:
                        break;
                }
            }
        }

        private void EnableChangeHandler(object sender, EventArgs e)
        {
            int ndx = this.list_tb.FindIndex(t => t.Equals((Control)sender));

            if (((TextBox)sender).Enabled)
            {
                this.list_btn[ndx].Enabled = true;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.White;
                this.list_btn[ndx].Enabled = false;
            }
        }

        private void ReadOnlyChangeHandler(object sender, EventArgs e)
        {
            int ndx = this.list_tb.FindIndex(t => t.Equals((Control)sender));

            if (((TextBox)sender).ReadOnly)
            {
                ((TextBox)sender).BackColor = Color.White;
                this.list_btn[ndx].Enabled = false;
            }
            else
            {
                this.list_btn[ndx].Enabled = true;
            }
        }
    }
}
