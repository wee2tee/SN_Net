﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.Subform;

namespace SN_Net.Model
{
    public class macallowedVM
    {
        public mac_allowed mac_allowed { get; set; }
        public int id { get { return this.mac_allowed.id; } }
        public string mac_address { get { return this.mac_allowed.mac_address; } }
        public string creby
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    var u = sn.users.Find(this.mac_allowed.creby_id);
                    return u != null ? u.username : string.Empty;
                }
            }
        }
        public DateTime credat { get { return this.mac_allowed.credat; } }
    }

    public class usersVM
    {
        public users users { get; set; }
        public int id { get { return this.users.id; } }
        public string username { get { return this.users.username; } }
        public string userpassword { get { return this.users.userpassword; } }
        public string name { get { return this.users.name; } }
        public string email { get { return this.users.email; } }
        public string level
        {
            get
            {
                switch (this.users.level)
                {
                    case 0:
                        return "Support";
                    case 1:
                        return "Sales";
                    case 2:
                        return "Account";
                    case 8:
                        return "Supervisor";
                    case 9:
                        return "Admin";
                    default:
                        return "";
                }
            }
        }
        public string usergroup // กลุ่มหยุดงานวันเสาร์
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (this.users.usergroup_id.HasValue)
                    {
                        var usrgrp = sn.istab.Where(i => i.flag == 0 && i.id == this.users.usergroup_id.Value).FirstOrDefault();
                        return usrgrp != null ? usrgrp.abbreviate_th : string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
        public string status
        {
            get
            {
                switch (this.users.status)
                {
                    case "N":
                        return "ปกติ";
                    case "X":
                        return "ห้ามใช้";
                    default:
                        return "";
                }
            }
        }
        public string allowed_web_login { get { return this.users.allowed_web_login; } }
        public string training_expert { get { return this.users.training_expert; } }
        public int max_absent { get { return this.users.max_absent; } }
    }

    public class serialVM
    {
        public serial serial { get; set; }

        public int id { get { return this.serial.id; } }
        public string sernum { get { return this.serial.sernum; } }
        public string oldnum { get { return this.serial.oldnum; } }
        public string version { get { return this.serial.version; } }
        public string position { get { return this.serial.position; } }
        public string prenam { get { return this.serial.prenam; } }
        public string compnam { get { return this.serial.compnam; } }
        public string contact { get { return this.serial.contact; } }
        public string addr01 { get { return this.serial.addr01; } }
        public string addr02 { get { return this.serial.addr02; } }
        public string addr03 { get { return this.serial.addr03; } }
        public string zipcod { get { return this.serial.zipcod; } }
        public Nullable<System.DateTime> purdat { get { return this.serial.purdat; } }
        public Nullable<System.DateTime> expdat { get { return this.serial.expdat; } }
        public string branch { get { return this.serial.branch; } }
        public Nullable<System.DateTime> manual { get { return this.serial.manual; } }
        public string upfree { get { return this.serial.upfree; } }
        public string refnum { get { return this.serial.refnum; } }
        public string remark { get { return this.serial.remark; } }
        //public string area
        //{
        //    get
        //    {
        //        using (snEntities sn = DBX.DataSet())
        //        {
        //            if (!this.serial.area_id.HasValue)
        //                return string.Empty;

        //            var area = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.area_id.Value).FirstOrDefault();
        //            return area != null ? area.typcod : string.Empty;
        //        }
        //    }
        //}
        public string area { get { return this.serial.area; } }
        //public string dealer
        //{
        //    get
        //    {
        //        using (snEntities sn = DBX.DataSet())
        //        {
        //            if (!this.serial.dealer_id.HasValue)
        //                return string.Empty;

        //            var dealer = sn.dealer.Where(d => d.flag == 0).Where(d => d.id == this.serial.dealer_id.Value).FirstOrDefault();
        //            return dealer != null ? dealer.dealercod : string.Empty;
        //        }
        //    }
        //}
        public string dealer { get { return this.serial.dealercod; } }
        public Nullable<System.DateTime> verextdat { get { return this.serial.verextdat; } }
        public string telnum { get { return this.serial.telnum; } }
        public string faxnum { get { return this.serial.faxnum; } }
        //public string busityp
        //{
        //    get
        //    {
        //        using (snEntities sn = DBX.DataSet())
        //        {
        //            if (!this.serial.busityp_id.HasValue)
        //                return string.Empty;

        //            var busityp = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.busityp_id.Value).FirstOrDefault();
        //            return busityp != null ? busityp.typcod : string.Empty;
        //        }
        //    }
        //}
        public string busityp { get { return this.serial.busityp; } }
        public string howknown
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.howknown_id.HasValue)
                        return string.Empty;

                    var howknow = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.howknown_id.Value).FirstOrDefault();
                    return howknow != null ? howknow.typcod : string.Empty;
                }
            }
        }
        public string verext
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.verext_id.HasValue)
                        return string.Empty;

                    var verext = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.verext_id.Value).FirstOrDefault();
                    return verext != null ? verext.typcod : string.Empty;
                }
            }
        }
        public string busides { get { return this.serial.busides; } }
        public string creby
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.creby_id.HasValue)
                        return string.Empty;

                    var users = sn.users.Find(this.serial.creby_id.Value);
                    return users != null ? users.username : string.Empty;
                }
            }
        }
        public System.DateTime credat { get { return this.serial.credat; } }
        public string chgby
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.chgby_id.HasValue)
                        return string.Empty;

                    var users = sn.users.Find(this.serial.chgby_id.Value);
                    return users != null ? users.username : string.Empty;
                }
            }
        }
        public Nullable<System.DateTime> chgdat { get { return this.serial.chgdat; } }
    }

    public class serialPasswordVM
    {
        public serial_password serial_password;

        public int id { get { return this.serial_password.id; } }
        public string password { get { return this.serial_password.pass_word; } }
    }

    public class problemVM
    {
        public problem problem { get; set; }

        public int id { get { return this.problem.id; } }
        public string probcod
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    istab istab = sn.istab.Where(i => i.flag == 0 && i.id == this.problem.probcod_id).FirstOrDefault();
                    return istab != null ? istab.typcod : string.Empty;
                }
            }
        }
        public string probdesc { get { return this.problem.probdesc; } }
        public Nullable<System.DateTime> date { get { return this.problem.date; } }
        public string name { get { return this.problem.name; } }
    }

    public class istabVM
    {
        public istab istab { get; set; }
        public int id { get { return this.istab.id; } }
        public string tabtyp { get { return this.istab.tabtyp; } }
        public string typcod { get { return this.istab.typcod; } }
        public string abbr_en { get { return this.istab.abbreviate_en; } }
        public string abbr_th { get { return this.istab.abbreviate_th; } }
        public string typdes_en { get { return this.istab.typdes_en; } }
        public string typdes_th { get { return this.istab.typdes_th; } }
        public string use_pattern { get { return this.istab.use_pattern ? "Y" : "N"; } }
    }

    public class dealerVM
    {
        public dealer dealer { get; set; }
        public int id { get { return this.dealer.id; } }
        public string dealercod { get { return this.dealer.dealercod; } }
        public string compnam { get { return this.dealer.compnam; } }
        public string addr01 { get { return this.dealer.addr01; } }
        public string addr02 { get { return this.dealer.addr02; } }
        public string addr03 { get { return this.dealer.addr03; } }
        public string zipcod { get { return this.dealer.zipcod; } }
        public string telnum { get { return this.dealer.telnum; } }
        public string faxnum { get { return this.dealer.faxnum; } }
        public string contact { get { return this.dealer.contact; } }
        public string position { get { return this.dealer.position; } }
        public string busides { get { return this.dealer.busides; } }
        public string area { get { return this.dealer.area; } }
    }

    public class d_msgVM
    {
        public d_msg d_msg { get; set; }
        public int id { get { return this.d_msg.id; } }
        public DateTime? date { get { return this.d_msg.date; } }
        public string name { get { return this.d_msg.name; } }
        public string description { get { return this.d_msg.description; } }
    }

    public class serialItemVM
    {
        public serial serial { get; set; }
        public int id { get { return this.serial.id; } }
        public string sernum { get { return this.serial.sernum; } }
        public DateTime? purdat { get { return this.serial.purdat; } }
        public string compnam { get { return this.serial.compnam; } }
        public string area { get { return this.serial.area; } }
    }

    public class importSerial
    {
        public string id { get; set; }
        public string sn { get; set; }
        public string comp_prenam { get; set; }
        public string comp_name { get; set; }
        public string comp_addr1 { get; set; }
        public string comp_addr2 { get; set; }
        public string comp_addr3 { get; set; }
        public string comp_zipcod { get; set; }
        public string comp_email { get; set; }
        public string comp_tel { get; set; }
        public string comp_fax { get; set; }
        public string comp_bus_type { get; set; }
        public string comp_bus_desc { get; set; }
        public string comp_prod_type { get; set; }
        public string purchase_from { get; set; }
        public string purchase_from_desc { get; set; }
        public string cont_name { get; set; }
        public string cont_position { get; set; }
        public string cont_email { get; set; }
        public string cont_tel { get; set; }
        public string reg_time { get; set; }
        public string recorded { get; set; }
        public string rec_time { get; set; }
        public string exported { get; set; }
        public string exported_file { get; set; }
        public string reserve2 { get; set; }
    }

    public class importSerialVM
    {
        public importSerial importSerial { get; set; }
        public string id { get { return this.importSerial.id; } }
        public string sn { get { return this.importSerial.sn; } }
        public string compname
        {
            get
            {
                return this.importSerial.comp_prenam.Trim().Length > 0 ? this.importSerial.comp_prenam + " " + this.importSerial.comp_name : this.importSerial.comp_name;
            }
        }
        public bool recorded { get; set; }
    }

    public class registerDataResult
    {
        public int result { get; set; }
        public List<importSerial> register_data { get; set; }
    }

    public static class DataHelper
    {
        public static macallowedVM ToViewModel(this mac_allowed mac)
        {
            if (mac == null)
                return null;

            macallowedVM m = new macallowedVM
            {
                mac_allowed = mac
            };
            return m;
        }

        public static List<macallowedVM> ToViewModel(this IEnumerable<mac_allowed> macs)
        {
            List<macallowedVM> m = new List<macallowedVM>();
            foreach (var item in macs)
            {
                m.Add(item.ToViewModel());
            }

            return m;
        }

        public static usersVM ToViewModel(this users users)
        {
            if (users == null)
                return null;

            usersVM u = new usersVM
            {
                users = users
            };
            return u;
        }

        public static List<usersVM> ToViewModel(this IEnumerable<users> users)
        {
            List<usersVM> u = new List<usersVM>();
            foreach (var item in users)
            {
                u.Add(item.ToViewModel());
            }
            return u;
        }

        public static serialVM ToViewModel(this serial serial)
        {
            if (serial == null)
                return null;

            serialVM s = new serialVM
            {
                serial = serial
            };
            return s;
        }

        public static List<serialVM> ToViewModel(this IEnumerable<serial> serials)
        {
            List<serialVM> s = new List<serialVM>();

            foreach (var item in serials)
            {
                s.Add(item.ToViewModel());
            }

            return s;
        }

        public static serialPasswordVM ToViewModel(this serial_password sp)
        {
            if (sp == null)
                return null;

            serialPasswordVM s = new serialPasswordVM
            {
                serial_password = sp
            };

            return s;     
        }

        public static List<serialPasswordVM> ToViewModel(this IEnumerable<serial_password> sp)
        {
            List<serialPasswordVM> s = new List<serialPasswordVM>();
            foreach (var item in sp)
            {
                s.Add(item.ToViewModel());
            }

            return s;
        }

        public static problemVM ToViewModel(this problem problem)
        {
            if (problem == null)
                return null;

            problemVM p = new problemVM
            {
                problem = problem
            };

            return p;
        }

        public static List<problemVM> ToViewModel(this IEnumerable<problem> problems)
        {
            List<problemVM> p = new List<problemVM>();
            foreach (var item in problems)
            {
                p.Add(item.ToViewModel());
            }

            return p;
        }

        public static istabVM ToViewModel(this istab istab)
        {
            if (istab == null)
                return null;

            istabVM i = new istabVM
            {
                istab = istab
            };
            return i;
        }

        public static List<istabVM> ToViewModel(this IEnumerable<istab> istabs)
        {
            List<istabVM> i = new List<istabVM>();
            foreach (var item in istabs)
            {
                i.Add(item.ToViewModel());
            }
            return i;
        }

        public static dealerVM ToViewModel(this dealer dealer)
        {
            if (dealer == null)
                return null;

            dealerVM d = new dealerVM
            {
                dealer = dealer
            };
            return d;
        }

        public static List<dealerVM> ToViewModel(this IEnumerable<dealer> dealers)
        {
            List<dealerVM> d = new List<dealerVM>();
            foreach (var item in dealers)
            {
                d.Add(item.ToViewModel());
            }
            return d;
        }

        public static d_msgVM ToViewModel(this d_msg d_msg)
        {
            if (d_msg == null)
                return null;

            d_msgVM d = new d_msgVM
            {
                d_msg = d_msg
            };
            return d;
        }

        public static List<d_msgVM> ToViewModel(this IEnumerable<d_msg> d_msgs)
        {
            List<d_msgVM> d = new List<d_msgVM>();
            foreach (var item in d_msgs)
            {
                d.Add(item.ToViewModel());
            }
            return d;
        }

        public static serialItemVM ToSerialItemVM(this serial serial)
        {
            if (serial == null)
                return null;

            serialItemVM s = new serialItemVM
            {
                serial = serial
            };
            return s;
        }

        public static List<serialItemVM> ToSerialItemVM(this IEnumerable<serial> serials)
        {
            List<serialItemVM> s = new List<serialItemVM>();
            foreach (var item in serials)
            {
                s.Add(item.ToSerialItemVM());
            }
            return s;
        }

        public static importSerialVM ToViewModel(this importSerial im)
        {
            if (im == null)
                return null;

            importSerialVM i = new importSerialVM
            {
                importSerial = im
            };
            return i;
        }

        public static List<importSerialVM> ToViewModel(this IEnumerable<importSerial> ims)
        {
            List<importSerialVM> i = new List<importSerialVM>();
            foreach (var item in ims)
            {
                i.Add(item.ToViewModel());
            }

            return i;
        }

        public static serial CreateTmpSerial(this snEntities sn, MainForm main_form)
        {
            return new serial
            {
                id = -1,
                sernum = string.Empty,
                oldnum = string.Empty,
                version = string.Empty,
                contact = string.Empty,
                position = string.Empty,
                prenam = string.Empty,
                compnam = string.Empty,
                addr01 = string.Empty,
                addr02 = string.Empty,
                addr03 = string.Empty,
                zipcod = string.Empty,
                telnum = string.Empty,
                faxnum = string.Empty,
                busides = string.Empty,
                purdat = DateTime.Now,
                expdat = DateTime.Now,
                branch = string.Empty,
                manual = null,
                upfree = string.Empty,
                refnum = string.Empty,
                remark = string.Empty,
                dealer_id = null,
                verextdat = null,
                area_id = null,
                busityp_id = null,
                howknown_id = null,
                verext_id = null,
                creby_id = main_form.loged_in_user.id,
                chgby_id = null,
                chgdat = null,
                flag = 0
            };
        }

        public static string GetTabtypString(this TABTYP tabtyp)
        {
            switch (tabtyp)
            {
                case TABTYP.AREA:
                    return istabDbf.TABTYP_AREA;
                case TABTYP.BUSITYP:
                    return istabDbf.TABTYP_BUSITYP;
                case TABTYP.HOWKNOWN:
                    return istabDbf.TABTYP_HOWKNOW;
                case TABTYP.PROBCOD:
                    return istabDbf.TABTYP_PROBCOD;
                case TABTYP.VEREXT:
                    return istabDbf.TABTYP_VEREXT;
                default:
                    return string.Empty;
            }
        }
    }
}