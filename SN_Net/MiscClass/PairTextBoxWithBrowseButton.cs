using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SN_Net.MiscClass
{
    public class PairTextBoxWithBrowseButton
    {
        private List<TextBox> list_tb = new List<TextBox>();
        private List<Button> list_btn = new List<Button>();

        private PairTextBoxWithBrowseButton(List<TextBox> tb, List<Button> btn)
        {
            this.list_tb = tb;
            this.list_btn = btn;
            this.addAction();
        }

        public static void Attach(List<TextBox> list_textbox, List<Button> list_button)
        {
            PairTextBoxWithBrowseButton f6 = new PairTextBoxWithBrowseButton(list_textbox, list_button);
        }

        private void addAction()
        {
            foreach (TextBox tb in this.list_tb)
            {
                tb.KeyDown += new KeyEventHandler(this.F6eventHandler);
            }

            foreach (Button btn in this.list_btn)
            {
                btn.TabIndex = 999;
                btn.TabStop = false;
            }
        }

        private void F6eventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                Control control = sender as Control;
                //int ndx = this.list_tb.FindIndex(new System.Predicate<TextBox>((value) => { return value == tb; }));
                //int ndx = this.list_tb.FindIndex(new Predicate<TextBox>((value) => { return value == tb; }));
                int ndx = this.list_tb.FindIndex( t => t.Equals(control));
                Console.WriteLine("this index : " + ndx.ToString());
                this.list_btn[ndx].PerformClick();
            }
        }
    }
}
