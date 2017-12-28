using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.MiscClass;
using SN_Net.Model;
using System.Globalization;
using SN_Net.MiscClass;
using SN_Net.Model;
using CC;

namespace SN_Net.Subform
{
    public partial class DialogAbsent : Form
    {
        private MainForm main_form;
        private CustomDateEvent3 custom_date_event;
        private bool perform_add;
        private BindingList<event_calendarVMFull> event_list;

        public DialogAbsent(MainForm main_form, CustomDateEvent3 custom_date_event, bool perform_add = false)
        {
            this.main_form = main_form;
            this.custom_date_event = custom_date_event;
            this.perform_add = perform_add;
            InitializeComponent();
        }

        private void DialogAbsent_Load(object sender, EventArgs e)
        {
            this.groupBox1.Text = this.custom_date_event.curr_date.ToString("วันdddd ที่ dd MMMM yyyy", CultureInfo.GetCultureInfo("th-TH"));
            if (this.perform_add)
                this.btnAddItem.PerformClick();

            this.SetDropdownListItem();
            this.GetData();

            this.dgv.DataSource = this.event_list;
        }

        private void SetDropdownListItem()
        {
            using (snEntities sn = DBX.DataSet())
            {
                sn.istab.Where(i => i.tabtyp == istabDbf.TABTYP_USERGROUP).OrderBy(i => i.typcod).ToList().ForEach(i => { this.dlGroupHoliday._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); this.dlGroupMaid._Items.Add(new XDropdownListItem { Text = i.typdes_th, Value = i.typcod }); });
                
            }
        }

        private void GetData()
        {
            using (sn_noteEntities note = DBXNote.DataSet())
            {
                this.event_list = new BindingList<event_calendarVMFull>(note.event_calendar.Where(ev => ev.date.CompareTo(this.custom_date_event.curr_date) == 0).ToViewModelFull());
                //foreach (var item in this.event_list)
                //{
                //    Console.WriteLine(" ==> " + item.med_cert + " .... [" + item.fine + "]");
                //}
                //Console.WriteLine("xxx" + this.event_list.Count);

            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {

        }
    }
}
