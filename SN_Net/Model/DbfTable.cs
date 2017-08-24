using SN_Net.Subform;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace SN_Net.Model
{
    public class DbfTable
    {
        public static DataTable istab(string data_path)
        {
            if (!(Directory.Exists(data_path) && File.Exists(data_path + @"\istab.dbf")))
            {
                MessageAlert.Show("ค้นหาแฟ้ม istab.dbf ในที่เก็บข้อมูล \"" + data_path + "\" ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return new DataTable();
            }

            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + data_path);

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                string mySQL = "select * from istab";

                OleDbCommand cmd = new OleDbCommand(mySQL, conn);
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

                DA.Fill(dt);

                conn.Close();
            }

            return dt;
        }

        public static DataTable serial(string data_path)
        {
            if (!(Directory.Exists(data_path) && File.Exists(data_path + @"\serial.dbf")))
            {
                MessageAlert.Show("ค้นหาแฟ้ม serial.dbf ในที่เก็บข้อมูล \"" + data_path + "\" ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return new DataTable();
            }

            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + data_path);

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                string mySQL = "select * from serial";

                OleDbCommand cmd = new OleDbCommand(mySQL, conn);
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

                DA.Fill(dt);

                conn.Close();
            }

            return dt;
        }

        public static DataTable problem(string data_path)
        {
            if (!(Directory.Exists(data_path) && File.Exists(data_path + @"\problem.dbf")))
            {
                MessageAlert.Show("ค้นหาแฟ้ม problem.dbf ในที่เก็บข้อมูล \"" + data_path + "\" ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return new DataTable();
            }

            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + data_path);

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                string mySQL = "select * from problem";

                OleDbCommand cmd = new OleDbCommand(mySQL, conn);
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

                DA.Fill(dt);

                conn.Close();
            }

            return dt;
        }

        public static DataTable dealer(string data_path)
        {
            if (!(Directory.Exists(data_path) && File.Exists(data_path + @"\dealer.dbf")))
            {
                MessageAlert.Show("ค้นหาแฟ้ม dealer.dbf ในที่เก็บข้อมูล \"" + data_path + "\" ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return new DataTable();
            }

            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + data_path);

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                string mySQL = "select * from dealer";

                OleDbCommand cmd = new OleDbCommand(mySQL, conn);
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

                DA.Fill(dt);

                conn.Close();
            }

            return dt;
        }

        public static DataTable d_msg(string data_path)
        {
            if (!(Directory.Exists(data_path) && File.Exists(data_path + @"\d_msg.dbf")))
            {
                MessageAlert.Show("ค้นหาแฟ้ม d_msg.dbf ในที่เก็บข้อมูล \"" + data_path + "\" ไม่พบ", "Error", MessageAlertButtons.OK, MessageAlertIcons.STOP);
                return new DataTable();
            }

            DataTable dt = new DataTable();

            OleDbConnection conn = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + data_path);

            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                string mySQL = "select * from d_msg";

                OleDbCommand cmd = new OleDbCommand(mySQL, conn);
                OleDbDataAdapter DA = new OleDbDataAdapter(cmd);

                DA.Fill(dt);

                conn.Close();
            }

            return dt;
        }
    }

    
    #region DbfTable Helper
    public static class DbfTableHelper
    {
        public static istabDbf ToIstabDbf(this DataRow row)
        {
            if (row == null)
                return null;

            istabDbf i = new istabDbf
            {
                tabtyp = row.Field<string>("tabtyp").Trim(),
                typcod = row.Field<string>("typcod").Trim(),
                shortnam = row.Field<string>("shortnam").Trim(),
                shortnam2 = row.Field<string>("shortnam2").Trim(),
                typdes = row.Field<string>("typdes").Trim(),
                typdes2 = row.Field<string>("typdes2").Trim(),
                fld01 = row.Field<string>("fld01").Trim(),
                fld02 = !row.IsNull("fld02") ? (double?)row.Field<double>("fld02") : null
            };
            return i;
        }

        public static List<istabDbf> ToIstabDbfList(this DataTable istabs)
        {
            List<istabDbf> i = new List<istabDbf>();
            foreach (DataRow row in istabs.Rows)
            {
                i.Add(row.ToIstabDbf());
            }
            return i;
        }

        public static dealerDbf ToDealerDbf(this DataRow row)
        {
            if (row == null)
                return null;

            dealerDbf d = new dealerDbf
            {
                dealer = row.Field<string>("dealer").Trim(),
                prenam = row.Field<string>("prenam").Trim(),
                compnam = row.Field<string>("compnam").Trim(),
                addr01 = row.Field<string>("addr01").Trim(),
                addr02 = row.Field<string>("addr02").Trim(),
                addr03 = row.Field<string>("addr03").Trim(),
                zipcod = row.Field<string>("zipcod").Trim(),
                telnum = row.Field<string>("telnum").Trim(),
                contact = row.Field<string>("contact").Trim(),
                position = row.Field<string>("position").Trim(),
                busides = row.Field<string>("busides").Trim(),
                area = row.Field<string>("area").Trim(),
                remark = row.Field<string>("remark").Trim(),
                userid = row.Field<string>("userid").Trim(),
                chgdat = !row.IsNull("chgdat") ? (DateTime?)row.Field<DateTime>("chgdat") : null
            };
            return d;
        }

        public static List<dealerDbf> ToDealerDbfList(this DataTable dealers)
        {
            List<dealerDbf> d = new List<dealerDbf>();
            foreach (DataRow row in dealers.Rows)
            {
                d.Add(row.ToDealerDbf());
            }
            return d;
        }

        public static d_msgDbf ToDmsgDbf(this DataRow row)
        {
            if (row == null)
                return null;

            d_msgDbf d = new d_msgDbf
            {
                dealer = row.Field<string>("dealer").Trim(),
                date = !row.IsNull("date") ? (DateTime?)row.Field<DateTime>("date") : null,
                time = row.Field<string>("time").Trim(),
                name = row.Field<string>("name").Trim(),
                descrp = row.Field<string>("descrp").Trim()
            };
            return d;
        }

        public static List<d_msgDbf> ToDmsgDbfList(this DataTable dmsgs)
        {
            List<d_msgDbf> d = new List<d_msgDbf>();

            foreach (DataRow row in dmsgs.Rows)
            {
                d.Add(row.ToDmsgDbf());
            }
            return d;
        }

        public static serialDbf ToSerialDbf(this DataRow row)
        {
            if (row == null)
                return null;

            serialDbf s = new serialDbf
            {
                sernum = row.Field<string>("sernum").Trim(),
                oldnum = row.Field<string>("oldnum").Trim(),
                version = row.Field<string>("version").Trim(),
                contact = row.Field<string>("contact").Trim(),
                position = row.Field<string>("position").Trim(),
                prenam = row.Field<string>("prenam").Trim(),
                compnam = row.Field<string>("compnam").Trim(),
                addr01 = row.Field<string>("addr01").Trim(),
                addr02 = row.Field<string>("addr02").Trim(),
                addr03 = row.Field<string>("addr03").Trim(),
                zipcod = row.Field<string>("zipcod").Trim(),
                telnum = row.Field<string>("telnum").Trim(),
                busides = row.Field<string>("busides").Trim(),
                purdat = !row.IsNull("purdat") ? (DateTime?)row.Field<DateTime>("purdat") : null,
                expdat = !row.IsNull("expdat") ? (DateTime?)row.Field<DateTime>("expdat") : null,
                branch = row.Field<string>("branch").Trim(),
                manual = !row.IsNull("manual") ? (DateTime?)row.Field<DateTime>("manual") : null,
                upfree = row.Field<string>("upfree").Trim(),
                refnum = row.Field<string>("refnum").Trim(),
                remark = row.Field<string>("remark").Trim(),
                verextdat = !row.IsNull("verextdat") ? (DateTime?)row.Field<DateTime>("verextdat") : null,
                userid = row.Field<string>("userid").Trim(),
                chgdat = !row.IsNull("chgdat") ? (DateTime?)row.Field<DateTime>("chgdat") : null,

                area = row.Field<string>("area").Trim(),
                busityp = row.Field<string>("busityp").Trim(),
                howknown = row.Field<string>("howknown").Trim(),
                verext = row.Field<string>("verext").Trim(),
                dealer = row.Field<string>("dealer").Trim()
            };
            return s;
        }

        public static List<serialDbf> ToSerialDbfList(this DataTable serials)
        {
            List<serialDbf> s = new List<serialDbf>();
            foreach (DataRow row in serials.Rows)
            {
                s.Add(row.ToSerialDbf());
            }
            return s;
        }

        public static problemDbf ToProblemDbf(this DataRow row)
        {
            if (row == null)
                return null;

            problemDbf p = new problemDbf
            {
                probcod = row.Field<string>("probcod").Trim(),
                probdesc = row.Field<string>("probdesc").Trim(),
                date = !row.IsNull("date") ? (DateTime?)row.Field<DateTime>("date") : null,
                name = row.Field<string>("name").Trim(),
                sernum = row.Field<string>("sernum").Trim(),
                time = row.Field<string>("time").Trim(),
                userid = row.Field<string>("userid").Trim()
            };
            return p;
        }

        public static List<problemDbf> ToProblemDbfList(this DataTable problems)
        {
            List<problemDbf> p = new List<problemDbf>();
            foreach (DataRow row in problems.Rows)
            {
                p.Add(row.ToProblemDbf());
            }
            return p;
        }
    }
    #endregion DbfTable Helper


    #region Table Class

    public class istabDbf
    {
        public string tabtyp { get; set; }
        public string typcod { get; set; }
        public string shortnam { get; set; }
        public string shortnam2 { get; set; }
        public string typdes { get; set; }
        public string typdes2 { get; set; }
        public string fld01 { get; set; }
        public double? fld02 { get; set; }
        public string depcod { get; set; }
        public string status { get; set; }

        public const string TABTYP_HOWKNOW = "03";
        public const string TABTYP_BUSITYP = "04";
        public const string TABTYP_PROBCOD = "05";
        public const string TABTYP_AREA = "06";
        public const string TABTYP_VEREXT = "07";
    }

    public class serialDbf
    {
        public string sernum { get; set; }
        public string oldnum { get; set; }
        public string version { get; set; }
        public string contact { get; set; }
        public string position { get; set; }
        public string prenam { get; set; }
        public string compnam { get; set; }
        public string addr01 { get; set; }
        public string addr02 { get; set; }
        public string addr03 { get; set; }
        public string zipcod { get; set; }
        public string telnum { get; set; }
        public string busides { get; set; }
        public DateTime? purdat { get; set; }
        public DateTime? expdat { get; set; }
        public string branch { get; set; }
        public DateTime? manual { get; set; }
        public string upfree { get; set; }
        public string refnum { get; set; }
        public string remark { get; set; }
        public DateTime? verextdat { get; set; }
        public string userid { get; set; }
        public DateTime? chgdat { get; set; }

        public string area { get; set; }
        public string busityp { get; set; }
        public string howknown { get; set; }
        public string verext { get; set; }
        public string dealer { get; set; }
    }

    public class problemDbf
    {
        public string probcod { get; set; }
        public string probdesc { get; set; }
        public DateTime? date { get; set; }
        public string time { get; set; }
        public string name { get; set; }
        public string sernum { get; set; }
        public string userid { get; set; }
    }

    public class dealerDbf
    {
        public string dealer { get; set; }
        public string prenam { get; set; }
        public string compnam { get; set; }
        public string addr01 { get; set; }
        public string addr02 { get; set; }
        public string addr03 { get; set; }
        public string zipcod { get; set; }
        public string telnum { get; set; }
        public string contact { get; set; }
        public string position { get; set; }
        public string busides { get; set; }
        public string area { get; set; }
        public string remark { get; set; }
        public string userid { get; set; }
        public DateTime? chgdat { get; set; }
    }

    public class d_msgDbf
    {
        public string dealer { get; set; }
        public string descrp { get; set; }
        public DateTime? date { get; set; }
        public string time { get; set; }
        public string name { get; set; }
    }
    #endregion Table Class
}
