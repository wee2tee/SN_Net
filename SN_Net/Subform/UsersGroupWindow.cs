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
    public partial class UsersGroupWindow : Form
    {
        private MainForm main_form;
        private List<Istab> list_group;
        private List<Users> list_users;
        private CustomComboBox inline_user;
        private Istab current_group;
        private Istab processing_group;
        private Control focused_control;
        private FORM_MODE form_mode;
        private enum FORM_MODE
        {
            READ,
            ADD_GROUP,
            EDIT_GROUP,
            READ_F7,
            READ_F8,
            ADD_F8,
            PROCESSING
        }

        public UsersGroupWindow()
        {
            InitializeComponent();
        }

        public UsersGroupWindow(MainForm main_form)
            : this()
        {
            this.main_form = main_form;
        }

        private void UsersGroupWindow_Load(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            this.BindingControlEventHandler();
            this.InitControl();
        }

        private void UsersGroupWindow_Shown(object sender, EventArgs e)
        {
            if (this.list_group.Count > 0)
            {
                this.current_group = this.list_group.First<Istab>();
                this.FillForm(this.current_group);
            }

            this.FillDgvMember(this.current_group);
            this.FillDgvUngroup();
            this.FormRead();
        }

        private void LoadDependenciesData()
        {
            this.LoadGroupData();
            this.LoadUsersData();
        }

        private void LoadGroupData()
        {
            CRUDResult get_group = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "istab/get_all&tabtyp=" + Istab.TABTYP.USER_GROUP.ToTabtypString() + "&sort=typcod");
            ServerResult sr_group = JsonConvert.DeserializeObject<ServerResult>(get_group.data);
            if (sr_group.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_group = sr_group.istab;
            }
        }

        private void LoadUsersData()
        {
            CRUDResult get_users = ApiActions.GET(PreferenceForm.API_MAIN_URL() + "users/get_all");
            ServerResult sr_users = JsonConvert.DeserializeObject<ServerResult>(get_users.data);
            if (sr_users.result == ServerResult.SERVER_RESULT_SUCCESS)
            {
                this.list_users = sr_users.users;
            }
        }

        private void BindingControlEventHandler()
        {
            this.dgvMember.DrawDgvRowBorder();
            this.dgvUnGroup.DrawDgvRowBorder();
            this.txtTypcod.textBox1.GotFocus += new EventHandler(this.SetControlFocused);
            this.txtTypdes.textBox1.GotFocus += new EventHandler(this.SetControlFocused);

            this.txtTypcod.label1.DoubleClick += delegate
            {
                if (this.current_group != null)
                {
                    this.btnEdit.PerformClick();
                }
            };

            this.txtTypdes.label1.DoubleClick += delegate
            {
                if (this.current_group != null)
                {
                    this.btnEdit.PerformClick();
                }
            };

            this.dgvMember.Resize += delegate
            {
                if (this.dgvMember.CurrentCell != null)
                {
                    if (this.current_group != null)
                    {

                        this.dgvMember.FillLine(this.list_users.Where(u => u.usergroup == this.current_group.typcod).ToList<Users>().Count);
                        return;
                    }
                    this.dgvMember.FillLine(0);
                }
            };
            this.dgvUnGroup.Resize += delegate
            {
                this.dgvUnGroup.FillLine(this.list_users.Where(u => u.usergroup == string.Empty).ToList<Users>().Count);
            };

            this.dgvMember.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (this.current_group == null)
                        return;

                    int row_index = this.dgvMember.HitTest(e.X, e.Y).RowIndex;

                    if (row_index > -1)
                    {
                        this.FormReadF8();
                        this.dgvMember.Rows[row_index].Cells[0].Selected = true;

                        ContextMenu c = new ContextMenu();

                        MenuItem m_add = new MenuItem("เพิ่ม <Alt+A>");
                        m_add.Click += delegate
                        {
                            this.ShowInlineForm(FORM_MODE.ADD_F8);
                            this.FormAddF8();
                        };
                        c.MenuItems.Add(m_add);

                        MenuItem m_remove = new MenuItem("นำออกจากกลุ่ม <Alt+D>");
                        m_remove.Click += delegate
                        {
                            this.RemoveFromGroup();
                        };
                        m_remove.Enabled = (this.dgvMember.Rows[row_index].Tag is Users ? true : false);
                        c.MenuItems.Add(m_remove);

                        c.Show(this.dgvMember, new Point(e.X, e.Y));
                    }
                }
            };

            this.dgvMember.MouseDoubleClick += delegate(object sender, MouseEventArgs e)
            {
                if (this.current_group == null)
                    return;

                int row_index = this.dgvMember.HitTest(e.X, e.Y).RowIndex;
                if (row_index > -1)
                {
                    this.ShowInlineForm(FORM_MODE.ADD_F8);
                    this.FormAddF8();
                }
            };

            this.dgvUnGroup.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int row_index = this.dgvUnGroup.HitTest(e.X, e.Y).RowIndex;

                    if (row_index > -1)
                    {
                        this.dgvUnGroup.Rows[row_index].Cells[0].Selected = true;

                        ContextMenu c = new ContextMenu();
                        MenuItem m_sendto = new MenuItem("จัดเข้าในกลุ่ม...");
                        m_sendto.Click += delegate
                        {
                            IstabList gwind = new IstabList(this.main_form, "", Istab.TABTYP.USER_GROUP, list_group);
                            gwind.btnAdd.Visible = false;
                            gwind.btnEdit.Visible = false;
                            if (gwind.ShowDialog() == DialogResult.OK)
                            {
                                string json_data = "{\"id\":" + ((Users)this.dgvUnGroup.Rows[this.dgvUnGroup.CurrentCell.RowIndex].Tag).id + ",";
                                json_data += "\"usergroup\":\"" + gwind.istab.typcod + "\",";
                                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";

                                bool post_success = false;
                                string err_msg = "";
                                this.FormProcessing();

                                BackgroundWorker worker = new BackgroundWorker();
                                worker.DoWork += delegate
                                {
                                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/change_group", json_data);
                                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                                    {
                                        post_success = true;
                                    }
                                    else
                                    {
                                        post_success = false;
                                        err_msg = sr.message;
                                    }
                                };
                                worker.RunWorkerCompleted += delegate
                                {
                                    if (post_success)
                                    {
                                        this.RefreshCurrentData();
                                        this.FillForm(this.current_group);
                                        this.FormReadF7();
                                        return;
                                    }

                                    this.FormReadF7();
                                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                                };
                                worker.RunWorkerAsync();
                            }
                        };
                        m_sendto.Enabled = (this.dgvUnGroup.Rows[row_index].Tag is Users ? true : false);
                        c.MenuItems.Add(m_sendto);

                        c.Show(this.dgvUnGroup, new Point(e.X, e.Y));
                    }
                }
            };

            this.tabControl1.Deselecting += delegate(object sender, TabControlCancelEventArgs e)
            {
                if (this.form_mode != FORM_MODE.READ)
                {
                    e.Cancel = true;
                    if (this.focused_control != null)
                    {
                        this.focused_control.Focus();
                    }
                    return;
                }
                if (this.current_group == null)
                {
                    e.Cancel = true;
                    return;
                }
            };

            this.tabControl1.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (this.form_mode != FORM_MODE.READ)
                {
                    if (this.focused_control != null)
                    {
                        this.focused_control.Focus();
                    }
                    return;
                }
            };
        }

        private void InitControl()
        {
            this.txtMemberCount.textBox1.TextAlign = HorizontalAlignment.Right;
        }

        private void SetControlFocused(object sender, EventArgs e)
        {
            this.focused_control = (Control)sender;
        }

        private void RefreshCurrentData()
        {
            this.LoadDependenciesData();
            if (this.list_group.Count > 0)
            {
                this.current_group = (this.list_group.Find(g => g.id == this.current_group.id) != null ? this.list_group.Find(g => g.id == this.current_group.id) : this.list_group.First<Istab>());
            }
            else
            {
                this.current_group = null;
            }
        }

        private void GetGroupByTypcod(string typcod)
        {
            Istab target_group = null;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                this.LoadDependenciesData();

                if(this.list_group.Count == 0) // if list_group is null
                    return;
                 
                if (this.list_group.Where(g => g.typcod.Length >= typcod.Length).Where(g => g.typcod.Substring(0, typcod.Length) == typcod).Count<Istab>() > 0) // if atleast 1 match
                {
                    target_group = this.list_group.Where(g => g.typcod.Length >= typcod.Length).Where(g => g.typcod.Substring(0, typcod.Length) == typcod).First<Istab>();
                    return;
                }

                if (true) // if no one match, compare with any group.typcod
                {
                    List<Istab> tmp_list = this.list_group.OrderBy(g => g.typcod, new CompareStrings()).ToList<Istab>();
                    foreach (Istab g in tmp_list)
                    {
                        if (g.typcod.CompareTo(typcod) > 0)
                        {
                            target_group = g;
                            break;
                        }
                    }

                    return;
                }
            };
            worker.RunWorkerCompleted += delegate
            {
                if (target_group != null)
                {
                    this.current_group = target_group;
                    this.FillForm(this.current_group);
                    return;
                }

                MessageAlert.Show(StringResource.DATA_NOT_FOUND, "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
            };
            worker.RunWorkerAsync();
            
        }

        private void FillForm(Istab group)
        {
            this.txtTypcod.Texts = (group != null ? group.typcod : "");
            this.txtTypdes.Texts = (group != null ? group.typdes_th : "");
            this.txtMemberCount.Texts = (group != null ? this.list_users.Where(u => u.usergroup == group.typcod).ToList<Users>().Count.ToString() : "");

            this.FillDgvMember(group);
            this.FillDgvUngroup();
        }

        private void FillDgvMember(Istab group, Users selected_user = null)
        {
            this.dgvMember.Rows.Clear();
            this.dgvMember.Columns.Clear();
            this.dgvMember.Tag = HelperClass.DGV_TAG.READ;

            this.dgvMember.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ลำดับ",
                Width = 40
            });
            this.dgvMember.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รหัส",
                Width = 120
            });
            this.dgvMember.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ชื่อ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvMember.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ระดับผู้ใช้",
                Width = 120
            });
            this.dgvMember.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "สถานะผู้ใช้",
                Width = 120
            });

            if (group != null)
            {
                int count = 0;
                foreach (Users u in this.list_users.Where(u => u.usergroup == group.typcod).ToList<Users>())
                {
                    int r = this.dgvMember.Rows.Add();
                    this.dgvMember.Rows[r].Tag = u;

                    this.dgvMember.Rows[r].Cells[0].ValueType = typeof(int);
                    this.dgvMember.Rows[r].Cells[0].Value = ++count;
                    this.dgvMember.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvMember.Rows[r].Cells[0].Style.ForeColor = Color.Gray;
                    this.dgvMember.Rows[r].Cells[0].Style.SelectionForeColor = Color.Gray;

                    this.dgvMember.Rows[r].Cells[1].ValueType = typeof(string);
                    this.dgvMember.Rows[r].Cells[1].Value = u.username;

                    this.dgvMember.Rows[r].Cells[2].ValueType = typeof(string);
                    this.dgvMember.Rows[r].Cells[2].Value = u.name;

                    this.dgvMember.Rows[r].Cells[3].ValueType = typeof(string);
                    this.dgvMember.Rows[r].Cells[3].Value = GlobalVar.GetUserLevelString(u.level);

                    this.dgvMember.Rows[r].Cells[4].ValueType = typeof(string);
                    this.dgvMember.Rows[r].Cells[4].Value = (u.status == "N" ? "ปกติ" : (u.status == "X" ? "ห้ามใช้" : ""));
                }
                
                this.dgvMember.FillLine(this.list_users.Where(u => u.usergroup == group.typcod).ToList<Users>().Count);
            }

            this.dgvMember.FillLine(0);
        }

        private void FillDgvUngroup(Users selected_user = null)
        {
            this.dgvUnGroup.Rows.Clear();
            this.dgvUnGroup.Columns.Clear();
            this.dgvUnGroup.Tag = HelperClass.DGV_TAG.READ;

            this.dgvUnGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ลำดับ",
                Width = 40
            });
            this.dgvUnGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "รหัส",
                Width = 120
            });
            this.dgvUnGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ชื่อ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            this.dgvUnGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "ระดับผู้ใช้",
                Width = 120
            });
            this.dgvUnGroup.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "สถานะผู้ใช้",
                Width = 120
            });

            int count = 0;
            foreach (Users u in this.list_users.Where(u => u.usergroup == string.Empty).ToList<Users>())
            {
                int r = this.dgvUnGroup.Rows.Add();
                this.dgvUnGroup.Rows[r].Tag = u;

                this.dgvUnGroup.Rows[r].Cells[0].ValueType = typeof(int);
                this.dgvUnGroup.Rows[r].Cells[0].Value = ++count;
                this.dgvUnGroup.Rows[r].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvUnGroup.Rows[r].Cells[0].Style.ForeColor = Color.Gray;
                this.dgvUnGroup.Rows[r].Cells[0].Style.SelectionForeColor = Color.Gray;

                this.dgvUnGroup.Rows[r].Cells[1].ValueType = typeof(string);
                this.dgvUnGroup.Rows[r].Cells[1].Value = u.username;

                this.dgvUnGroup.Rows[r].Cells[2].ValueType = typeof(string);
                this.dgvUnGroup.Rows[r].Cells[2].Value = u.name;

                this.dgvUnGroup.Rows[r].Cells[3].ValueType = typeof(string);
                this.dgvUnGroup.Rows[r].Cells[3].Value = GlobalVar.GetUserLevelString(u.level);

                this.dgvUnGroup.Rows[r].Cells[4].ValueType = typeof(string);
                this.dgvUnGroup.Rows[r].Cells[4].Value = (u.status == "N" ? "ปกติ" : (u.status == "X" ? "ห้ามใช้" : ""));
            }

            this.dgvUnGroup.FillLine(this.list_users.Where(u => u.usergroup == string.Empty).ToList<Users>().Count);
        }

        private void ShowInlineForm(FORM_MODE mode)
        {
            if (this.current_group != null)
            {
                this.inline_user = new CustomComboBox();
                this.inline_user.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                this.inline_user.comboBox1.Font = new Font("tahoma", 9.75f);
                this.inline_user.label1.Font = new Font("tahoma", 9.75f);
                this.inline_user.BorderStyle = BorderStyle.None;
                this.inline_user.Read_Only = false;
                this.inline_user.comboBox1.GotFocus += new EventHandler(this.SetControlFocused);
                this.RefreshCurrentData();
                this.FillForm(this.current_group);
                foreach (Users u in this.list_users.Where(u => u.usergroup == string.Empty).ToList<Users>())
                {
                    this.inline_user.AddItem(new ComboboxItem(u.username + " : " + u.name, u.id, u.username) { Tag = u });
                }

                if (mode == FORM_MODE.ADD_F8)
                {
                    this.dgvMember.Rows[this.list_users.Where(u => u.usergroup == this.current_group.typcod).ToList<Users>().Count].Cells[0].Selected = true;
                    this.SetInlineFormPosition();
                }

                this.dgvMember.Parent.Controls.Add(this.inline_user);
                this.dgvMember.SendToBack();
                this.inline_user.BringToFront();
                this.inline_user.comboBox1.Focus();
                SendKeys.Send("{F4}");
            }
            else
            {
                MessageAlert.Show("ท่านต้องสร้างกลุ่มขึ้นมาก่อนที่เพิ่มสมาชิกในกลุ่ม", "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
            }
        }

        private void SetInlineFormPosition()
        {
            Rectangle rect = this.dgvMember.GetCellDisplayRectangle(1, this.list_users.Where(u => u.usergroup == this.current_group.typcod).ToList<Users>().Count, true);
            this.inline_user.SetBounds(rect.X + 3, rect.Y + 4, rect.Width - 1, rect.Height - 3);
        }

        private void ClearInlineForm()
        {
            if (this.inline_user != null)
            {
                this.inline_user.Dispose();
                this.inline_user = null;
            }
        }

        private void RemoveFromGroup()
        {
            if (this.dgvMember.Rows[this.dgvMember.CurrentCell.RowIndex].Tag is Users)
            {
                this.dgvMember.Tag = HelperClass.DGV_TAG.DELETE;

                if (MessageAlert.Show(StringResource.CONFIRM_DELETE, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
                {
                    string json_data = "{\"id\":" + ((Users)this.dgvMember.Rows[this.dgvMember.CurrentCell.RowIndex].Tag).id.ToString() + ",";
                    json_data += "\"usergroup\":\"\",";
                    json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";


                    bool post_success = false;
                    string err_msg = "";

                    this.FormProcessing();
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/change_group", json_data);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);

                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            post_success = true;
                        }
                        else
                        {
                            post_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (post_success)
                        {
                            this.RefreshCurrentData();
                            this.FillForm(this.current_group);
                            this.FormReadF8();
                            this.dgvMember.Tag = HelperClass.DGV_TAG.READ;
                            this.dgvMember.Focus();
                            return;
                        }

                        this.FormReadF8();
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    };
                    worker.RunWorkerAsync();
                    return;
                }

                this.dgvMember.Tag = HelperClass.DGV_TAG.READ;
                this.dgvMember.Refresh();
            }
        }

        private void FormRead()
        {
            this.form_mode = FORM_MODE.READ;
            this.focused_control = null;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = true;
            this.btnEdit.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnDelete.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnStop.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnFirst.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnPrevious.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnNext.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnLast.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnItem.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnItemF7.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnItemF8.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnSearch.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnInquiryAll.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnInquiryRest.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnSearchTypcod.Enabled = (this.list_group.Count > 0 ? true : false);
            this.btnReload.Enabled = true;

            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = true;
            this.dgvMember.Enabled = true;
            this.dgvUnGroup.Enabled = true;
        }

        private void FormAddGroup()
        {
            this.form_mode = FORM_MODE.ADD_GROUP;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;

            this.txtTypcod.Read_Only = false;
            this.txtTypdes.Read_Only = false;
            this.dgvMember.Enabled = false;
            this.dgvUnGroup.Enabled = false;
        }

        private void FormEditGroup()
        {
            this.form_mode = FORM_MODE.EDIT_GROUP;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;

            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = false;
            this.dgvMember.Enabled = false;
            this.dgvUnGroup.Enabled = false;
        }

        private void FormReadF7()
        {
            this.form_mode = FORM_MODE.READ_F7;
            this.focused_control = null;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;

            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = true;
            this.dgvMember.Enabled = true;
            this.dgvUnGroup.Enabled = true;
        }

        private void FormReadF8()
        {
            this.form_mode = FORM_MODE.READ_F8;
            this.focused_control = null;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;

            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = true;

            this.dgvMember.Enabled = true;
            this.ClearInlineForm();
            this.dgvMember.Enabled = true;
            this.dgvUnGroup.Enabled = true;
        }

        private void FormAddF8()
        {
            this.form_mode = FORM_MODE.ADD_F8;
            this.toolStripProcessing.Visible = false;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;

            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = true;

            this.dgvMember.Enabled = false;
            this.inline_user.Read_Only = false;
            this.dgvMember.Enabled = false;
            this.dgvUnGroup.Enabled = true;
        }

        private void FormProcessing()
        {
            this.form_mode = FORM_MODE.PROCESSING;
            this.toolStripProcessing.Visible = true;

            this.btnAdd.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnStop.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnItem.Enabled = false;
            this.btnItemF7.Enabled = false;
            this.btnItemF8.Enabled = false;
            this.btnSearch.Enabled = false;
            this.btnInquiryAll.Enabled = false;
            this.btnInquiryRest.Enabled = false;
            this.btnSearchTypcod.Enabled = false;
            this.btnReload.Enabled = false;
        
            this.txtTypcod.Read_Only = true;
            this.txtTypdes.Read_Only = true;
            this.dgvMember.Enabled = false;
            this.dgvUnGroup.Enabled = false;
            if (this.inline_user != null)
            {
                this.inline_user.Read_Only = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.FormAddGroup();
            this.processing_group = new Istab()
            {
                id = -1
            };
            this.FillForm(this.processing_group);
            this.txtTypcod.textBox1.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.RefreshCurrentData();
            this.FormEditGroup();
            this.processing_group = new Istab()
            {
                id = this.current_group.id,
                tabtyp = this.current_group.tabtyp,
                typcod = this.current_group.typcod,
                abbreviate_th = this.current_group.abbreviate_th,
                abbreviate_en = this.current_group.abbreviate_en,
                typdes_th = this.current_group.typdes_th,
                typdes_en = this.current_group.typdes_en
            };
            this.FillForm(this.processing_group);
            this.txtTypdes.textBox1.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageAlert.Show("ลบกลุ่มผู้ใช้นี้ , สมาชิกในกลุ่มจะถูกนำออกจากกลุ่มด้วย\nดำเนินการต่อ?", "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.QUESTION) == DialogResult.OK)
            {
                bool delete_success = false;
                string err_msg = "";
                int deleted_list_index = this.list_group.FindIndex(g => g.id == this.current_group.id);

                this.FormProcessing();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult delete = ApiActions.DELETE(PreferenceForm.API_MAIN_URL() + "istab/delete_users_group&id=" + this.current_group.id.ToString());
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(delete.data);

                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        delete_success = true;
                    }
                    else
                    {
                        delete_success = false;
                        err_msg = sr.message;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (delete_success)
                    {
                        this.LoadDependenciesData();
                        this.current_group = (this.list_group.Count == 0 ? null : (deleted_list_index < this.list_group.Count - 1 ? this.list_group[deleted_list_index] : this.list_group.First<Istab>()));
                        this.FillForm(this.current_group);
                        this.FormRead();
                    }
                    else
                    {
                        this.FormRead();
                        MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.form_mode == FORM_MODE.ADD_GROUP || this.form_mode == FORM_MODE.EDIT_GROUP)
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CANCEL_ADD_EDIT, "", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.NONE) == DialogResult.OK)
                {
                    this.FormRead();
                    this.FillForm(this.current_group);
                }
                return;
            }

            if (this.form_mode == FORM_MODE.READ_F7 || this.form_mode == FORM_MODE.READ_F8)
            {
                this.FormRead();
                return;
            }

            if (this.form_mode == FORM_MODE.ADD_F8)
            {
                this.ClearInlineForm();
                this.FormReadF8();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Add/Edit Group
            if (this.form_mode == FORM_MODE.ADD_GROUP || this.form_mode == FORM_MODE.EDIT_GROUP)
            {
                string json_data = "{\"id\":" + this.processing_group.id.ToString() + ",";
                json_data += "\"tabtyp\":\"" + Istab.TABTYP.USER_GROUP.ToTabtypString() + "\",";
                json_data += "\"typcod\":\"" + this.txtTypcod.Texts.cleanString() + "\",";
                json_data += "\"abbreviate_th\":\"\",";
                json_data += "\"abbreviate_en\":\"\",";
                json_data += "\"typdes_th\":\"" + this.txtTypdes.Texts.cleanString() + "\",";
                json_data += "\"typdes_en\":\"\"}";

                //Console.WriteLine(" >> " + json_data);
                bool post_success = false;
                string err_msg = "";
                Istab inserted_group = null;

                if (this.form_mode == FORM_MODE.ADD_GROUP)
                {
                    this.FormProcessing();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/create", json_data);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            post_success = true;
                            inserted_group = sr.istab[0];
                        }
                        else
                        {
                            post_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (post_success)
                        {
                            this.LoadDependenciesData();
                            this.current_group = inserted_group;
                            this.FillForm(this.current_group);
                            this.FormRead();
                        }
                        else
                        {
                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            this.FormAddGroup();
                            this.txtTypdes.textBox1.Focus();
                        }
                    };
                    worker.RunWorkerAsync();
                }

                if (this.form_mode == FORM_MODE.EDIT_GROUP)
                {
                    this.FormProcessing();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += delegate
                    {
                        Console.WriteLine(" >> " + json_data);
                        CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "istab/submit_change", json_data);
                        ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                        if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                        {
                            post_success = true;
                            inserted_group = sr.istab[0];
                        }
                        else
                        {
                            post_success = false;
                            err_msg = sr.message;
                        }
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        if (post_success)
                        {
                            this.LoadDependenciesData();
                            this.current_group = inserted_group;
                            this.FillForm(this.current_group);
                            this.FormRead();
                        }
                        else
                        {
                            MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                            this.FormAddGroup();
                            this.txtTypdes.textBox1.Focus();
                        }
                    };
                    worker.RunWorkerAsync();
                }
            }

            #endregion Add/Edit Group

            #region Add Member
            if (this.form_mode == FORM_MODE.ADD_F8)
            {
                if (this.inline_user.comboBox1.SelectedItem == null)
                {
                    MessageAlert.Show("กรุณาเลือกรหัสพนักงาน", "", MessageAlertButtons.OK, MessageAlertIcons.NONE);
                    return;
                }

                string json_data = "{\"id\":" + ((Users)((ComboboxItem)this.inline_user.comboBox1.SelectedItem).Tag).id.ToString() + ",";
                json_data += "\"usergroup\":\"" + this.current_group.typcod + "\",";
                json_data += "\"rec_by\":\"" + this.main_form.G.loged_in_user_name + "\"}";
                
                bool post_success = false;
                string err_msg = "";
                Users edited_user = null;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    CRUDResult post = ApiActions.POST(PreferenceForm.API_MAIN_URL() + "users/change_group", json_data);
                    ServerResult sr = JsonConvert.DeserializeObject<ServerResult>(post.data);
                    if (sr.result == ServerResult.SERVER_RESULT_SUCCESS)
                    {
                        post_success = true;
                        edited_user = sr.users[0];
                    }
                    else
                    {
                        post_success = false;
                        err_msg = sr.message;
                    }
                };
                worker.RunWorkerCompleted += delegate
                {
                    if (post_success)
                    {
                        this.RefreshCurrentData();
                        this.FillForm(this.current_group);
                        this.FormReadF8();
                        this.ShowInlineForm(FORM_MODE.ADD_F8);
                        this.FormAddF8();
                        return;
                    }

                    MessageAlert.Show(err_msg, "Error", MessageAlertButtons.OK, MessageAlertIcons.ERROR);
                    this.FormAddF8();
                };
                worker.RunWorkerAsync();
            }
            #endregion Add Member
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            this.current_group = this.list_group.First<Istab>();
            this.FillForm(this.current_group);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            if (this.list_group.Find(g => g.id == this.current_group.id) != null)
            {
                int curr_index = this.list_group.FindIndex(g => g.id == this.current_group.id);
                this.current_group = (curr_index > 0 ? this.list_group[curr_index - 1] : this.list_group.First<Istab>());
            }
            else
            {
                this.current_group = this.list_group.First<Istab>();
            }

            this.FillForm(this.current_group);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            if (this.list_group.Find(g => g.id == this.current_group.id) != null)
            {
                int curr_index = this.list_group.FindIndex(g => g.id == this.current_group.id);
                this.current_group = (curr_index < this.list_group.Count - 1 ? this.list_group[curr_index + 1] : this.list_group.Last<Istab>());
            }
            else
            {
                this.current_group = this.list_group.Last<Istab>();
            }

            this.FillForm(this.current_group);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.LoadDependenciesData();
            this.current_group = this.list_group.Last<Istab>();
            this.FillForm(this.current_group);
        }

        private void btnItem_ButtonClick(object sender, EventArgs e)
        {
            this.btnItemF8.PerformClick();
        }

        private void btnItemF8_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage1;
            this.dgvMember.Focus();
            this.FormReadF8();
        }

        private void btnItemF7_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;
            this.dgvUnGroup.Focus();
            this.FormReadF7();
        }

        private void btnSearch_ButtonClick(object sender, EventArgs e)
        {
            this.btnSearchTypcod.PerformClick();
        }

        private void btnInquiryAll_Click(object sender, EventArgs e)
        {
            IstabList gwind = new IstabList(this.main_form, "", Istab.TABTYP.USER_GROUP, this.list_group);
            gwind.btnAdd.Visible = false;
            gwind.btnEdit.Visible = false;
            if (gwind.ShowDialog() == DialogResult.OK)
            {
                this.GetGroupByTypcod(gwind.istab.typcod);
            }
        }

        private void btnInquiryRest_Click(object sender, EventArgs e)
        {
            IstabList gwind = new IstabList(this.main_form, this.current_group.typcod, Istab.TABTYP.USER_GROUP, this.list_group);
            gwind.btnAdd.Visible = false;
            gwind.btnEdit.Visible = false;
            if (gwind.ShowDialog() == DialogResult.OK)
            {
                this.GetGroupByTypcod(gwind.istab.typcod);
            }
        }

        private void btnSearchTypcod_Click(object sender, EventArgs e)
        {
            SearchSerialBox swind = new SearchSerialBox(SearchSerialBox.SEARCH_MODE.USERGROUP);
            swind.txtSearchKey.CharacterCasing = CharacterCasing.Upper;
            if (swind.ShowDialog() == DialogResult.OK)
            {
                this.GetGroupByTypcod(swind.txtSearchKey.Text);
            }

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.RefreshCurrentData();
            this.FillForm(this.current_group);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.form_mode == FORM_MODE.ADD_GROUP || this.form_mode == FORM_MODE.EDIT_GROUP)
                {
                    if (this.txtTypdes.textBox1.Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }
                }

                if (this.form_mode == FORM_MODE.ADD_F8)
                {
                    if (this.inline_user.comboBox1.Focused)
                    {
                        this.btnSave.PerformClick();
                        return true;
                    }
                }

                SendKeys.Send("{TAB}");
                return true;
            }

            if (keyData == Keys.Escape)
            {
                if (this.form_mode == FORM_MODE.ADD_F8 && this.inline_user != null && this.inline_user.item_shown)
                {
                    return false;
                }

                this.btnStop.PerformClick();
                return true;
            }

            if (keyData == Keys.F5)
            {
                this.btnReload.PerformClick();
                return true;
            }

            if (keyData == Keys.F7)
            {
                this.btnItemF7.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                this.btnItemF8.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                this.btnSave.PerformClick();
                return true;
            }

            if (keyData == Keys.PageUp)
            {
                this.btnPrevious.PerformClick();
                return true;
            }

            if (keyData == Keys.PageDown)
            {
                this.btnNext.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.Home))
            {
                this.btnFirst.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.End))
            {
                this.btnLast.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.S))
            {
                this.btnSearchTypcod.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.L))
            {
                this.btnInquiryRest.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.L))
            {
                this.btnInquiryAll.PerformClick();
                return true;
            }

            if (keyData == (Keys.Alt | Keys.A))
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.btnAdd.PerformClick();
                    return true;
                }
                if (this.form_mode == FORM_MODE.READ_F8)
                {
                    this.ShowInlineForm(FORM_MODE.ADD_F8);
                    this.FormAddF8();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.E))
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.btnEdit.PerformClick();
                    return true;
                }
            }

            if (keyData == (Keys.Alt | Keys.D))
            {
                if (this.form_mode == FORM_MODE.READ)
                {
                    this.btnDelete.PerformClick();
                    return true;
                }
                if (this.form_mode == FORM_MODE.READ_F8)
                {
                    this.RemoveFromGroup();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UsersGroupWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if((e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.UserClosing) && (this.form_mode != FORM_MODE.READ && this.form_mode != FORM_MODE.READ_F7 && this.form_mode != FORM_MODE.READ_F8))
            {
                if (MessageAlert.Show(StringResource.CONFIRM_CLOSE_WINDOW, "SN_Net", MessageAlertButtons.OK_CANCEL, MessageAlertIcons.WARNING) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            this.main_form.usersgroup_wind = null;
        }
    }
}
