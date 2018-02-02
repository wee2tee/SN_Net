using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SN_Net.Subform;
using System.Globalization;
using SN_Net.MiscClass;
//using System.ComponentModel;

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

    public class NoteProblem
    {
        public const string MAP_DRIVE = "{MAP_DRIVE}";
        public const string INSTALL_UPDATE = "{INSTALL_UPDATE}";
        public const string ERROR = "{ERROR}";
        public const string FONTS = "{FONTS}";
        public const string PRINT = "{PRINT}";
        public const string STOCK = "{STOCK}";
        public const string EDIT_FORM = "{EDIT_FORM}";
        public const string REPORT_EXCEL = "{REPORT_EXCEL}";
        public const string STATEMENT = "{STATEMENT}";
        public const string ASSETS = "{ASSETS}";
        public const string SECURE = "{SECURE}";
        public const string YEAR_END = "{YEAR_END}";
        public const string PERIOD = "{PERIOD}";
        public const string MAIL_WAIT = "{MAIL_WAIT}";
        public const string TRAINING = "{TRAINING}";
        public const string TRANSFER_MKT = "{TRANSFER_MKT}";
        public const string OTHER = "{OTHER}";
    }

    public class NoteReason
    {
        public const string CORRECT_DATA = "{CORRECT_DATA}";
        public const string MEET_CUST = "{MEET_CUST}";
        public const string OTHER = "{OTHER}";
        public const string QT = "{QT}";
        public const string TOILET = "{TOILET}";
        public const string TRAINING_ASSIST = "{TRAINING_ASSIST}";
        public const string TRAINING_TRAINER = "{TRAINING_TRAINER}";

        public static string GetDesc(string reason_code)
        {
            switch (reason_code)
            {
                case CORRECT_DATA:
                    return "แก้ไขข้อมูลให้ลูกค้า";
                case MEET_CUST:
                    return "ลูกค้ามาพบ";
                case OTHER:
                    return "อื่น ๆ";
                case QT:
                    return "ทำใบเสนอราคา";
                case TOILET:
                    return "เข้าห้องน้ำ";
                case TRAINING_ASSIST:
                    return "เข้าห้องอบรม(ผู้ช่วย)";
                case TRAINING_TRAINER:
                    return "เข้าห้องอบรม(วิทยากร)";
                default:
                    return string.Empty;
            }
        }
    }

    public class noteVM
    {
        public note note { get; set; }
        public string seq { get; set; }
        public bool has_comment
        {
            get
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    if (note.note_comment.Where(n => n.type == (int)DialogNoteComment.NOTE_COMMENT_TYPE.COMMENT && n.note_id == this.note.id).FirstOrDefault() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool has_complain
        {
            get
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    if (note.note_comment.Where(n => n.type == (int)DialogNoteComment.NOTE_COMMENT_TYPE.COMPLAIN && n.note_id == this.note.id).FirstOrDefault() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public string has_comment_complain
        {
            get
            {
                if (this.has_comment && this.has_complain)
                    return "Both";
                if (this.has_comment && !this.has_complain)
                    return "Comment";
                if (!this.has_comment && this.has_complain)
                    return "Complain";

                return string.Empty;
            }
        }
        public string username { get { return this.note.users_name; } }
        public DateTime date { get { return this.note.date; } }
        public string start_time { get { return this.note.start_time; } }
        public string end_time { get { return this.note.end_time; } }
        public string duration {
            get
            {
                return this.note.duration;
            }
        }
        public string sernum { get { return this.note.sernum; } }
        public string contact { get { return this.note.is_break == "N" ? this.note.contact : "** " + NoteReason.GetDesc(this.note.reason); } }
        public bool is_mapdrive { get { return this.note.problem.Contains(NoteProblem.MAP_DRIVE) ? true : false; } }
        public bool is_installupdate { get { return this.note.problem.Contains(NoteProblem.INSTALL_UPDATE) ? true : false; } }
        public bool is_error { get { return this.note.problem.Contains(NoteProblem.ERROR) ? true : false; } }
        public bool is_font { get { return this.note.problem.Contains(NoteProblem.FONTS) ? true : false; } }
        public bool is_print { get { return this.note.problem.Contains(NoteProblem.PRINT) ? true : false; } }
        public bool is_training { get { return this.note.problem.Contains(NoteProblem.TRAINING) ? true : false; } }
        public bool is_stock { get { return this.note.problem.Contains(NoteProblem.STOCK) ? true : false; } }
        public bool is_form { get { return this.note.problem.Contains(NoteProblem.EDIT_FORM) ? true : false; } }
        public bool is_reportexcel { get { return this.note.problem.Contains(NoteProblem.REPORT_EXCEL) ? true : false; } }
        public bool is_statement { get { return this.note.problem.Contains(NoteProblem.STATEMENT) ? true : false; } }
        public bool is_asset { get { return this.note.problem.Contains(NoteProblem.ASSETS) ? true : false; } }
        public bool is_secure { get { return this.note.problem.Contains(NoteProblem.SECURE) ? true : false; } }
        public bool is_yearend { get { return this.note.problem.Contains(NoteProblem.YEAR_END) ? true : false; } }
        public bool is_period { get { return this.note.problem.Contains(NoteProblem.PERIOD) ? true : false; } }
        public bool is_mail { get { return this.note.problem.Contains(NoteProblem.MAIL_WAIT) ? true : false; } }
        public bool is_transfer { get { return this.note.problem.Contains(NoteProblem.TRANSFER_MKT) ? true : false; } }
        public bool is_other { get { return /*this.note.problem.Contains(NoteProblem.OTHER)*/ this.note.remark.Contains("{problem}") ? true : false; } }
        public string remark { get { return this.note.remark.Replace("{problem}", ""); } }
    }

    public class note_commentVM
    {
        // type 1 = comment, 2 = complain
        public note_comment note_comment { get; set; }
        public int id { get { return this.note_comment.id; } }
        public string recby { get { return this.note_comment.rec_by; } }
        public string description { get { return this.note_comment.description; } }
    }

    public class event_calendarVM
    {
        public event_calendar event_calendar { get; set; }
        public users users
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.users.Where(u => u.username == this.event_calendar.users_name).FirstOrDefault();
                }
            }
        }
        public int? seq { get; set; }
        public string realname { get { return this.event_calendar.realname; } }
        //public string event_type { get { return this.event_calendar.event_type; } }
        //public string event_code { get { return this.event_calendar.event_code; } }
        public string type
        {
            get
            {
                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    var istab = sn_note.note_istab.Find(this.event_calendar.event_code_id);
                    return istab != null ? istab.abbreviate_th : string.Empty;
                }
            }
        }
        public string description
        {
            get
            {
                if(this.event_calendar.event_type == CALENDAR_EVENT_TYPE.ABSENT)
                {
                    if(TimeSpan.Parse(this.event_calendar.from_time).GetDayTimeSpan(TimeSpan.Parse(this.event_calendar.to_time)).TotalHours != 8)
                    {
                        return TimeSpan.Parse(this.event_calendar.from_time).GetDayTimeSpan(TimeSpan.Parse(this.event_calendar.to_time)).GetTimeSpanShortString() + " (" + this.event_calendar.from_time + " - " + this.event_calendar.to_time + ")";
                    }
                    else
                    {
                        return TimeSpan.Parse(this.event_calendar.from_time).GetDayTimeSpan(TimeSpan.Parse(this.event_calendar.to_time)).GetTimeSpanShortString();
                    }
                }
                else if(this.event_calendar.event_type == CALENDAR_EVENT_TYPE.MEET_CUST)
                {
                    return this.event_calendar.customer;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }

    public class event_calendarVMFull
    {
        public event_calendar event_calendar { get; set; }
        public users users
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    return sn.users.Where(u => u.username == this.event_calendar.users_name).FirstOrDefault();
                }
            }
        }
        public int? seq { get; set; }
        public string code_name { get { return (this.users != null ? users.username : "  ") + " : " + this.event_calendar.realname; } }
        public string reason
        {
            get
            {
                using (sn_noteEntities sn_note = DBXNote.DataSet())
                {
                    var istab = sn_note.note_istab.Find(this.event_calendar.event_code_id);
                    return istab != null ? istab.typdes_th : string.Empty;
                }
            }
        }
        public string time_from { get { return this.event_calendar.from_time; } }
        public string time_to { get { return this.event_calendar.to_time; } }
        public string status { get { return Enum.GetValues(typeof(CALENDAR_EVENT_STATUS)).Cast<CALENDAR_EVENT_STATUS>().Where(ev => (int)ev == this.event_calendar.status).FirstOrDefault().ToString(); } }
        public string remark { get { return this.event_calendar.customer; } }
        public string med_cert
        {
            get
            {
                switch (this.event_calendar.med_cert)
                {
                    case CALENDAR_EVENT_MEDCERT.NOT_ASSIGN:
                        return string.Empty;

                    case CALENDAR_EVENT_MEDCERT.NOT_HAVE_MEDCERT:
                        return "ไม่มีเอกสาร";

                    case CALENDAR_EVENT_MEDCERT.OTHER_DOCUMENT:
                        return "มีเอกสารอื่น ๆ";

                    case CALENDAR_EVENT_MEDCERT.HAVE_MEDCERT:
                        return "มีใบรับรองแพทย์";

                    default:
                        return string.Empty;
                }
            }
        }
        public string fine { get { return this.event_calendar.fine > 0 ? string.Format("{0:0.00}", this.event_calendar.fine) : string.Empty; } }
    }

    public class training_calendarVM
    {
        public training_calendar training_calendar { get; set; }
        public DateTime date { get { return this.training_calendar.date; } }
        public string course_type { get { return this.training_calendar.course_type == 1 ? "Basic" : "Advanced"; } }
        public string trainer { get { return this.training_calendar.trainer; } }
        public string name
        {
            get
            {
                using (snEntities sn = DBX.DataSet())
                {
                    users u = sn.users.Where(us => us.username.Trim() == this.training_calendar.trainer.Trim()).FirstOrDefault();
                    return u != null ? u.name : string.Empty;
                }
            }
        }
        public string code_name { get { return this.training_calendar.trainer + " : " + this.name; } }
        public string status { get { return this.training_calendar.status == 1 ? "วิทยากร" : "ผู้ช่วย"; } }
        public string term { get { return this.training_calendar.term == 1 ? "เช้า" : "บ่าย"; } }
        public string remark { get { return this.training_calendar.remark; } }
        //public string rec_by { get { return this.training_calendar.rec_by; } }
    }
    
    public class trainer_monthly_statVM
    {
        public users user { get; set; }
        public DateTime first_date_of_month { get; set; }
        public DateTime last_date_of_month { get; set; }
        public string code_name { get { return this.user.username + " : " + this.user.name; } }
        public List<DateTime> train_dates
        {
            get
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    var dates = note.training_calendar.Where(t =>
                        t.trainer == user.username &&
                        t.status == 1 /*1 = Trainer*/ &&
                        /*t.date.ToString("MM-yyyy", CultureInfo.GetCultureInfo("th-TH")) == curr_date.ToString("MM-yyyy", CultureInfo.GetCultureInfo("th-TH"))*/
                        t.date.CompareTo(this.first_date_of_month) >= 0 &&
                        t.date.CompareTo(this.last_date_of_month) <= 0).Select(t => t.date).ToList();
                    return dates;
                }
            }
        }
        public List<DateTime> assist_dates
        {
            get
            {
                using (sn_noteEntities note = DBXNote.DataSet())
                {
                    var dates = note.training_calendar.Where(t =>
                        t.trainer == user.username &&
                        t.status == 2 /*2 = Assist*/ &&
                        t.date.CompareTo(this.first_date_of_month) >= 0 &&
                        t.date.CompareTo(this.last_date_of_month) <= 0).Select(t => t.date).ToList();
                    return dates;
                }
            }
        }
        public string train_dates_str
        {
            get
            {
                var d = this.train_dates.Select(t => t.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"))).ToArray<string>();
                return string.Join(", ", d);
            }
        }
        public string assist_dates_str
        {
            get
            {
                var d = this.assist_dates.Select(t => t.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("th-TH"))).ToArray<string>();
                return string.Join(", ", d);
            }
        }
    }

    public class YearlyHolidayVM
    {
        public note_calendar note_calendar { get; set; }
        public string day { get { return this.note_calendar.date.ToString("dddd"); } }
        public DateTime date { get { return this.note_calendar.date; } }
        public string description { get { return this.note_calendar.description; } }
    }

    public class AbsentCauseVM
    {
        public note_istab istab { get; set; }
        public bool enabled { get; set; }
        public bool selected { get; set; }
        public string description { get { return this.istab.typdes_th; } }
        public string stat { get; set; }
    }

    public class AbsentPersonStatVM
    {
        private event_calendarVMFull event_vm_full { get { return this.event_calendar.ToViewModelFull(); } }
        public event_calendar event_calendar { get; set; }
        public int? seq { get; set; }
        public DateTime date { get { return this.event_calendar.date; } }
        public string user_name { get { return this.event_calendar.users_name; } }
        public string reason { get { return this.event_vm_full.reason; } }
        public string time_from { get { return this.event_calendar.from_time.Substring(0, 5); } }
        public string time_to { get { return this.event_calendar.to_time.Substring(0, 5); } }
        public string duration
        {
            get
            {
                var from = TimeSpan.Parse(this.time_from);
                var to = TimeSpan.Parse(this.time_to);

                TimeSpan duration = to.Subtract(from);
                if(from.CompareTo(TimeSpan.Parse("12:00")) < 0 && to.CompareTo(TimeSpan.Parse("13:00")) > 0)
                {
                    duration = duration.Subtract(TimeSpan.Parse("01:00"));
                }

                return duration.GetTimeSpanString();  //.ToString(@"hh\:mm");
            }
        }
        public string status { get { return this.event_vm_full.status; } } //return Enum.GetValues(typeof(CALENDAR_EVENT_STATUS)).Cast<CALENDAR_EVENT_STATUS>().Where(st => (int)st == this.event_calender.status).FirstOrDefault().ToString(); } }
        public string customer { get { return this.event_calendar.customer; } }
        public string medcert { get { return this.event_vm_full.med_cert; } }
        public string fine { get { return this.event_vm_full.fine; } }
    }

    public class CALENDAR_EVENT_TYPE
    {
        public const string ABSENT = "06";
        public const string MEET_CUST = "07";
    }

    public class CALENDAR_EVENT_MEDCERT
    {
        public const string NOT_ASSIGN = "X";
        public const string HAVE_MEDCERT = "Y";
        public const string NOT_HAVE_MEDCERT = "N";
        public const string OTHER_DOCUMENT = "O";
    }

    public enum CALENDAR_EVENT_STATUS : int
    {
        WAIT = 0,
        CONFIRMED = 1,
        CANCELED = 2
    }

    public enum CALENDAR_NOTE_TYPE : int
    {
        WEEKDAY = 0,
        HOLIDAY = 1
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

        public static noteVM ToViewModel(this note note)
        {
            if (note == null)
                return null;

            noteVM n = new noteVM
            {
                note = note,
                seq = ""
            };
            return n;
        }

        public static List<noteVM> ToViewModel(this IEnumerable<note> notes)
        {
            List<noteVM> n = new List<noteVM>();
            foreach (var item in notes)
            {
                n.Add(item.ToViewModel());
            }

            return n;
        }

        public static note_commentVM ToViewModel(this note_comment nc)
        {
            if (nc == null)
                return null;

            note_commentVM n = new note_commentVM
            {
                note_comment = nc
            };

            return n;
        }

        public static List<note_commentVM> ToViewModel(this IEnumerable<note_comment> nc)
        {
            List<note_commentVM> n = new List<note_commentVM>();
            foreach (var item in nc)
            {
                n.Add(item.ToViewModel());
            }

            return n;
        }

        public static event_calendarVM ToViewModel(this event_calendar ev)
        {
            if (ev == null)
                return null;

            event_calendarVM e = new event_calendarVM
            {
                event_calendar = ev
            };

            return e;
        }

        public static List<event_calendarVM> ToViewModel(this IEnumerable<event_calendar> ev)
        {
            List<event_calendarVM> e = new List<event_calendarVM>();
            foreach (var item in ev)
            {
                e.Add(item.ToViewModel());
            }
            return e;
        }

        public static event_calendarVMFull ToViewModelFull(this event_calendar ev)
        {
            if (ev == null)
                return null;

            event_calendarVMFull e = new event_calendarVMFull
            {
                event_calendar = ev
            };

            return e;
        }

        public static List<event_calendarVMFull> ToViewModelFull(this IEnumerable<event_calendar> ev)
        {
            List<event_calendarVMFull> e = new List<event_calendarVMFull>();
            foreach (var item in ev)
            {
                e.Add(item.ToViewModelFull());
            }
            return e;
        }
        public static training_calendarVM ToViewModel(this training_calendar tr)
        {
            if (tr == null)
                return null;

            training_calendarVM t = new training_calendarVM
            {
                training_calendar = tr
            };
            return t;
        }

        public static List<training_calendarVM> ToViewModel(this IEnumerable<training_calendar> tr)
        {
            List<training_calendarVM> t = new List<training_calendarVM>();
            foreach (var item in tr)
            {
                t.Add(item.ToViewModel());
            }
            return t;
        }

        public static trainer_monthly_statVM ToViewModelTrainerStat(this users user, DateTime any_date_of_month)
        {
            if (user == null)
                return null;

            trainer_monthly_statVM t = new trainer_monthly_statVM
            {
                user = user,
                first_date_of_month = any_date_of_month.GetFirstDateOfMonth(),
                last_date_of_month = any_date_of_month.GetLastDateOfMonth() 
            };
            return t;
        }

        public static List<trainer_monthly_statVM> ToViewModelTrainerStat(this IEnumerable<users> user, DateTime any_date_in_month)
        {
            List<trainer_monthly_statVM> t = new List<trainer_monthly_statVM>();

            foreach (var item in user)
            {
                t.Add(item.ToViewModelTrainerStat(any_date_in_month));
            }
            return t;
        }

        public static YearlyHolidayVM ToViewModelYearlyHoliday(this note_calendar note)
        {
            if (note == null)
                return null;

            YearlyHolidayVM y = new YearlyHolidayVM
            {
                note_calendar = note
            };
            return y;
        }

        public static List<YearlyHolidayVM> ToViewModelYearlyHoliday(this IEnumerable<note_calendar> notes)
        {
            List<YearlyHolidayVM> y = new List<Model.YearlyHolidayVM>();
            foreach (var item in notes)
            {
                y.Add(item.ToViewModelYearlyHoliday());
            }

            return y;
        }

        public static AbsentCauseVM ToAbsentCauseVM(this note_istab istab)
        {
            if (istab == null)
                return null;

            AbsentCauseVM a = new AbsentCauseVM
            {
                istab = istab,
                selected = true,
                stat = string.Empty
            };

            return a;
        }

        public static List<AbsentCauseVM> ToAbsentCauseVM(this IEnumerable<note_istab> istab)
        {
            List<AbsentCauseVM> a = new List<Model.AbsentCauseVM>();
            foreach (var item in istab)
            {
                a.Add(item.ToAbsentCauseVM());
            }

            return a;
        }

        public static AbsentPersonStatVM ToAbsentPersonStatVM(this event_calendar event_cal)
        {
            if (event_cal == null)
                return null;

            AbsentPersonStatVM a = new AbsentPersonStatVM
            {
                event_calendar = event_cal,
                seq = null
            };
            return a;
        }

        public static List<AbsentPersonStatVM> ToAbsentPersonStatVM(this IEnumerable<event_calendar> event_cal)
        {
            List<AbsentPersonStatVM> a = new List<AbsentPersonStatVM>();

            foreach (var item in event_cal)
            {
                a.Add(item.ToAbsentPersonStatVM());
            }

            return a;
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
