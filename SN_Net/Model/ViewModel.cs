using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.Model
{
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
        public string area
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.area_id.HasValue)
                        return string.Empty;

                    var area = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.area_id.Value).FirstOrDefault();
                    return area != null ? area.typcod : string.Empty;
                }
            }
        }
        public string dealer
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.dealer_id.HasValue)
                        return string.Empty;

                    var dealer = sn.dealer.Where(d => d.flag == 0).Where(d => d.id == this.serial.dealer_id.Value).FirstOrDefault();
                    return dealer != null ? dealer.dealercod : string.Empty;
                }
            }
        }
        public Nullable<System.DateTime> verextdat { get { return this.serial.verextdat; } }
        public string telnum { get { return this.serial.telnum; } }
        public string faxnum { get { return this.serial.faxnum; } }
        public string busityp
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    if (!this.serial.busityp_id.HasValue)
                        return string.Empty;

                    var busityp = sn.istab.Where(i => i.flag == 0).Where(i => i.id == this.serial.busityp_id.Value).FirstOrDefault();
                    return busityp != null ? busityp.typcod : string.Empty;
                }
            }
        }
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

    public static class DataHelper
    {
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
    }
}
