using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SN_Net.Model;
using SN_Net.MiscClass;
using CC;

namespace SN_Net.Subform
{
    public partial class FormIstab : Form
    {
        private MainForm main_form;
        private BindingList<istabVM> istab_list;
        private istab tmp_istab;
        private string tabtyp;
        private FORM_MODE form_mode;
        private bool show_use_pattern_column;

        public FormIstab(MainForm main_form, string tabtyp, bool show_use_pattern_column = false)
        {
            this.main_form = main_form;
            this.tabtyp = tabtyp;
            this.show_use_pattern_column = show_use_pattern_column;
            InitializeComponent();
        }

        private void FormIstab_Load(object sender, EventArgs e)
        {
            this.SetPatternSelection();
            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.HideInlineForm();

            switch (this.tabtyp)
            {
                case istabDbf.TABTYP_AREA:
                    this.Text += " [Area]";
                    break;
                case istabDbf.TABTYP_BUSITYP:
                    this.Text += " [Business Type]";
                    break;
                case istabDbf.TABTYP_HOWKNOW:
                    this.Text += " [How Known]";
                    break;
                case istabDbf.TABTYP_PROBCOD:
                    this.Text += " [Problem Code]";
                    break;
                case istabDbf.TABTYP_VEREXT:
                    this.Text += " [Version Extension]";
                    break;
                case istabDbf.TABTYP_USERGROUP:
                    this.Text += " [User Group]";
                    break;
                default:
                    break;
            }

            this.btnReload.PerformClick();
        }

        public static List<istab> GetIstabList(string tabtyp)
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.istab.Where(i => i.flag == 0 && i.tabtyp == tabtyp).OrderBy(i => i.typcod).ToList();
            }
        }

        public static istab GetIstab(int id)
        {
            using (snEntities sn = DBX.DataSet())
            {
                return sn.istab.Where(i => i.flag == 0 && i.id == id).FirstOrDefault();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(!(this.form_mode == FORM_MODE.READ_ITEM))
            {
                if (MessageAlert.Show("ข้อมูลที่กำลังเพิ่ม/แก้ไข จะไม่ถูกบันทึก, ทำต่อหรือไม่", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }

            switch (this.tabtyp)
            {
                case istabDbf.TABTYP_AREA:
                    this.main_form.form_area = null;
                    break;
                case istabDbf.TABTYP_BUSITYP:
                    this.main_form.form_busityp = null;
                    break;
                case istabDbf.TABTYP_HOWKNOW:
                    this.main_form.form_howknown = null;
                    break;
                case istabDbf.TABTYP_PROBCOD:
                    this.main_form.form_probcod = null;
                    break;
                case istabDbf.TABTYP_VEREXT:
                    this.main_form.form_verext = null;
                    break;
                case istabDbf.TABTYP_USERGROUP:
                    this.main_form.form_usergroup = null;
                    break;
                default:
                    break;
            }
            
            base.OnClosing(e);
        }

        private void SetPatternSelection()
        {
            this.inlinePattern._Items.Add(new XDropdownListItem { Value = false, Text = "N" });
            this.inlinePattern._Items.Add(new XDropdownListItem { Value = true, Text = "Y" });
        }

        private void ResetFormState(FORM_MODE form_mode)
        {
            this.form_mode = form_mode;

            this.btnAdd.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnEdit.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnDelete.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
            this.btnStop.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnSave.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.btnReload.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);

            this.inlineTypcod.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM }, this.form_mode);
            this.inlineAbbrTh.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineAbbrEn.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineTypdesTh.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlineTypdesEn.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.inlinePattern.SetControlState(new FORM_MODE[] { FORM_MODE.ADD_ITEM, FORM_MODE.EDIT_ITEM }, this.form_mode);
            this.dgv.SetControlState(new FORM_MODE[] { FORM_MODE.READ_ITEM }, this.form_mode);
        }

        private void ShowInlineForm(DataGridViewRow row)
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.tmp_istab = (istab)row.Cells[this.col_istab.Name].Value;
            this.SetInlineControlPosition(row);

            this.inlineTypcod._Text = this.tmp_istab.typcod;
            this.inlineAbbrTh._Text = this.tmp_istab.abbreviate_th;
            this.inlineAbbrEn._Text = this.tmp_istab.abbreviate_en;
            this.inlineTypdesTh._Text = this.tmp_istab.typdes_th;
            this.inlineTypdesEn._Text = this.tmp_istab.typdes_en;
            this.inlinePattern._SelectedItem = this.tmp_istab.use_pattern ? this.inlinePattern._Items.Cast<XDropdownListItem>().Where(i => (bool)i.Value == true).First() : this.inlinePattern._Items.Cast<XDropdownListItem>().Where(i => (bool)i.Value == false).First();
        }

        private void SetInlineControlPosition(DataGridViewRow row)
        {
            int col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_typcod.Name).First().Index;
            if (this.form_mode == FORM_MODE.ADD_ITEM)
                this.inlineTypcod.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_abbr_th.Name).First().Index;
            this.inlineAbbrTh.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_abbr_en.Name).First().Index;
            this.inlineAbbrEn.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_typdes_th.Name).First().Index;
            this.inlineTypdesTh.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_typdes_en.Name).First().Index;
            this.inlineTypdesEn.SetInlineControlPosition(row.DataGridView, row.Index, col_index);

            col_index = row.DataGridView.Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_use_pattern.Name).First().Index;
            this.inlinePattern.SetInlineControlPosition(row.DataGridView, row.Index, col_index);
        }

        private void HideInlineForm()
        {
            this.inlineTypcod.SetBounds(-99999, 0, this.inlineTypcod.Width, this.inlineTypcod.Height);
            this.inlineAbbrTh.SetBounds(-99999, 0, this.inlineAbbrTh.Width, this.inlineAbbrTh.Height);
            this.inlineAbbrEn.SetBounds(-99999, 0, this.inlineAbbrEn.Width, this.inlineAbbrEn.Height);
            this.inlineTypdesTh.SetBounds(-99999, 0, this.inlineTypdesTh.Width, this.inlineTypdesTh.Height);
            this.inlineTypdesEn.SetBounds(-99999, 0, this.inlineTypdesEn.Width, this.inlineTypdesEn.Height);
            this.inlinePattern.SetBounds(-99999, 0, this.inlinePattern.Width, this.inlinePattern.Height);

            this.tmp_istab = null;
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int row_index = ((XDatagrid)sender).HitTest(e.X, e.Y).RowIndex;

                if(row_index > -1)
                {
                    ((XDatagrid)sender).Rows[row_index].Cells[this.col_typcod.Name].Selected = true;
                }

                ContextMenu cm = new ContextMenu();
                MenuItem m_add = new MenuItem("เพิ่ม <Alt + A>");
                m_add.Click += delegate
                {
                    this.btnAdd.PerformClick();
                };
                cm.MenuItems.Add(m_add);

                MenuItem m_edit = new MenuItem("แก้ไข <Alt + E>");
                m_edit.Click += delegate
                {
                    this.btnEdit.PerformClick();
                };
                m_edit.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_edit);

                MenuItem m_delete = new MenuItem("ลบ <Alt + D>");
                m_delete.Click += delegate
                {
                    this.btnDelete.PerformClick();
                };
                m_delete.Enabled = row_index > -1 ? true : false;
                cm.MenuItems.Add(m_delete);

                cm.Show(((XDatagrid)sender), new Point(e.X, e.Y));
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!this.show_use_pattern_column)
            {
                ((XDatagrid)sender).Columns.Cast<DataGridViewColumn>().Where(c => c.Name == this.col_use_pattern.Name).First().Visible = false;
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.form_mode == FORM_MODE.READ_ITEM && e.RowIndex > -1)
            {
                this.btnEdit.PerformClick();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            istabVM i = new istab
            {
                id = -1,
                tabtyp = this.tabtyp,
                typcod = string.Empty,
                abbreviate_th = string.Empty,
                abbreviate_en = string.Empty,
                typdes_th = string.Empty,
                typdes_en = string.Empty,
                use_pattern = false,
                creby_id = this.main_form.loged_in_user.id,
                flag = 0
            }.ToViewModel();

            ((BindingList<istabVM>)this.dgv.DataSource).Add(i);

            var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == -1).FirstOrDefault();
            if (focused_row == null)
                return;

            focused_row.Cells[this.col_typcod.Name].Selected = true;
            this.ResetFormState(FORM_MODE.ADD_ITEM);
            this.ShowInlineForm(focused_row);
            this.inlineTypcod.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            this.ResetFormState(FORM_MODE.EDIT_ITEM);
            this.ShowInlineForm(this.dgv.Rows[this.dgv.CurrentCell.RowIndex]);
            this.inlineAbbrTh.Focus();
            this.inlineAbbrTh._SelectionStart = this.inlineAbbrTh._Text.Length;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgv.CurrentCell == null)
                return;

            var istab = (istab)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_istab.Name].Value;

            this.dgv.Rows[this.dgv.CurrentCell.RowIndex].DrawDeletingRowOverlay();
            if (MessageAlert.Show("ลบรหัส " + istab.typcod + " ทำต่อหรือไม่?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    try
                    {
                        var istab_to_remove = sn.istab.Include("dealer").Include("problem").Include("serial").Include("serial1").Include("serial2").Include("serial3").Where(i => i.flag == 0 && i.id == istab.id).FirstOrDefault();
                        if (istab_to_remove != null)
                        {
                            if (istab_to_remove.dealer.Where(d => d.flag == 0).Count() > 0
                                || istab_to_remove.problem.Where(p => p.flag == 0).Count() > 0
                                || istab_to_remove.serial.Where(s => s.flag == 0).Count() > 0
                                || istab_to_remove.serial1.Where(s => s.flag == 0).Count() > 0
                                || istab_to_remove.serial2.Where(s => s.flag == 0).Count() > 0
                                || istab_to_remove.serial3.Where(s => s.flag == 0).Count() > 0)
                            {
                                MessageAlert.Show("รหัส " + istab_to_remove.typcod + " มีการนำไปใช้งานแล้ว ห้ามลบ", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                                this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                                return;
                            }

                            istab_to_remove.flag = istab_to_remove.id;
                            istab_to_remove.chgby_id = this.main_form.loged_in_user.id;
                            istab_to_remove.chgdat = DateTime.Now;
                            sn.SaveChanges();
                        }

                        ((BindingList<istabVM>)this.dgv.DataSource).Remove(((BindingList<istabVM>)this.dgv.DataSource).Where(i => i.id == istab.id).First());
                    }
                    catch (Exception ex)
                    {
                        MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }

                    this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
                }
            }
            else
            {
                this.dgv.Rows[this.dgv.CurrentCell.RowIndex].ClearDeletingRowOverlay();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            int focused_id = -1;
            if(this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                focused_id = (int)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_id.Name].Value;
            }
            this.HideInlineForm();

            this.ResetFormState(FORM_MODE.READ_ITEM);
            this.btnReload.PerformClick();

            if (this.dgv.CurrentCell == null)
                return;

            var focused_row = this.dgv.Rows.Cast<DataGridViewRow>().Where(r => (int)r.Cells[this.col_id.Name].Value == focused_id).FirstOrDefault();
            if(focused_row != null)
            {
                focused_row.Cells[this.col_typcod.Name].Selected = true;
            }
            else
            {
                this.dgv.Rows[this.dgv.Rows.Count - 1].Cells[this.col_typcod.Name].Selected = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tmp_istab == null)
                return;

            if(this.tmp_istab.typcod.Trim().Length == 0)
            {
                this.inlineTypcod.Focus();
                return;
            }

            using (snEntities sn = DBX.DataSet())
            {
                try
                {
                    if (this.form_mode == FORM_MODE.ADD_ITEM)
                    {
                        if (sn.istab.Where(i => i.flag == 0 && i.tabtyp == this.tabtyp && i.typcod.Trim() == this.tmp_istab.typcod.Trim()).FirstOrDefault() != null)
                        {
                            MessageAlert.Show("รหัส " + this.tmp_istab.typcod + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                            this.inlineTypcod.Focus();
                            return;
                        }

                        this.tmp_istab.credat = DateTime.Now;

                        sn.istab.Add(this.tmp_istab);
                        sn.SaveChanges();
                        this.HideInlineForm();
                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.btnAdd.PerformClick();
                    }
                    if (this.form_mode == FORM_MODE.EDIT_ITEM)
                    {
                        var istab_to_update = sn.istab.Where(i => i.flag == 0 && i.id == this.tmp_istab.id).FirstOrDefault();

                        if(istab_to_update != null) // update if exist
                        {
                            istab_to_update.abbreviate_th = this.tmp_istab.abbreviate_th;
                            istab_to_update.abbreviate_en = this.tmp_istab.abbreviate_en;
                            istab_to_update.typdes_th = this.tmp_istab.typdes_th;
                            istab_to_update.typdes_en = this.tmp_istab.typdes_en;
                            istab_to_update.use_pattern = this.tmp_istab.use_pattern;
                            istab_to_update.chgby_id = this.main_form.loged_in_user.id;
                            istab_to_update.chgdat = DateTime.Now;

                            sn.SaveChanges();
                        }
                        else // insert if not exist
                        {
                            this.tmp_istab.creby_id = this.main_form.loged_in_user.id;
                            this.tmp_istab.credat = DateTime.Now;
                            sn.istab.Add(this.tmp_istab);
                            sn.SaveChanges();
                        }

                        this.ResetFormState(FORM_MODE.READ_ITEM);
                        this.HideInlineForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageAlert.Show(ex.Message, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.istab_list = new BindingList<istabVM>(GetIstabList(this.tabtyp).ToViewModel());
            this.dgv.DataSource = this.istab_list;
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            if (((XDatagrid)sender).CurrentCell == null)
                return;

            if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
            {
                this.SetInlineControlPosition(((XDatagrid)sender).Rows[((XDatagrid)sender).CurrentCell.RowIndex]);
            }
        }

        private void inlineTypcod__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.typcod = ((XTextEdit)sender)._Text;
        }

        private void inlineTypcod__Leave(object sender, EventArgs e)
        {
            if (this.tmp_istab == null)
                return;

            if(this.form_mode == FORM_MODE.ADD_ITEM)
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (sn.istab.Where(i => i.flag == 0 && i.tabtyp == this.tabtyp && i.typcod.Trim() == this.tmp_istab.typcod.Trim()).AsEnumerable().Count() > 0)
                    {
                        this.inlineTypcod.Focus();
                        MessageAlert.Show("รหัส " + this.tmp_istab.typcod + " นี้มีอยู่แล้ว", "", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                        return;
                    }
                }
            }
        }

        private void inlineAbbrTh__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.abbreviate_th = ((XTextEdit)sender)._Text;
        }

        private void inlineAbbrEn__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.abbreviate_en = ((XTextEdit)sender)._Text;
        }

        private void inlineTypdesTh__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.typdes_th = ((XTextEdit)sender)._Text;
        }

        private void inlineTypdesEn__TextChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.typdes_en = ((XTextEdit)sender)._Text;
        }

        private void inlinePattern__SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.tmp_istab != null)
                this.tmp_istab.use_pattern = (bool)((XDropdownList)sender)._SelectedItem.Value;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_ITEM || this.form_mode == FORM_MODE.EDIT_ITEM)
                {
                    if (this.show_use_pattern_column && this.inlinePattern._Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }
                    else if(!this.show_use_pattern_column && this.inlineTypdesEn._Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                        return true;
                    }
                }
            }

            if (keyData == Keys.Escape)
            {
                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                this.btnAdd.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                this.btnEdit.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.D))
            {
                this.btnDelete.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.F5))
            {
                this.btnReload.PerformClick();
                return true;
            }

            if(keyData == Keys.Tab)
            {
                if(this.form_mode == FORM_MODE.READ_ITEM)
                {
                    if(this.dgv.CurrentCell != null)
                    {
                        using (snEntities sn = DBX.DataSet())
                        {
                            var ist = (istab)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells[this.col_istab.Name].Value;
                            var istab = sn.istab.Include("users1").Include("users2").Where(i => i.flag == 0 && i.id == ist.id).FirstOrDefault();
                            var total_row = sn.istab.Where(i => i.flag == 0).Select(i => i.id).AsEnumerable().Count();

                            if (istab == null)
                                return false;

                            DbInfo info = new DbInfo
                            {
                                DbName = sn.Database.Connection.Database,
                                TbName = "Istab",
                                Expression = "Tabtyp+Typcod",
                                CreBy = istab.creby_id.HasValue ? istab.users2.username : string.Empty,
                                CreDat = istab.credat,
                                ChgBy = istab.chgby_id.HasValue ? istab.users1.username : string.Empty,
                                ChgDat = istab.chgdat,
                                RecId = istab.id,
                                TotalRec = total_row
                            };
                            DialogDataInfo d_info = new DialogDataInfo(info);
                            d_info.ShowDialog();
                        }
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
