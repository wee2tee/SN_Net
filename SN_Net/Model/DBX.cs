using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace SN_Net.Model
{
    public class DBX
    {
        private string server_name;
        public string ServerName { get { return this.server_name; } }
        //private string db_prefix;
        //public string DbPrefix { get { return this.db_prefix; } }
        private string db_name;
        public string DbName { get { return this.db_name; } }
        private string db_userid;
        public string DbUserId { get { return this.db_userid; } }
        private string db_password;
        public string DbPassword { get { return this.db_password; } }
        public int port_no;
        public int PortNo { get { return this.port_no; } }

        public DBX(/*string server_name, string db_userid, string db_password, string db_prefix, string db_name, int port_no = 3306*/)
        {
            this.server_name = "localhost";
            //this.db_prefix = db_prefix;
            this.db_name = "sn";
            this.db_userid = "root";
            this.db_password = "12345";
            this.port_no = 3306;
        }

        public snEntities GetDBEntities()
        {
            return new snEntities("metadata=res://*/Model.SNNetModel.csdl|res://*/Model.SNNetModel.ssdl|res://*/Model.SNNetModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"Data Source=" + this.server_name + ";Port=" + this.port_no.ToString() + ";Initial Catalog=" + this.db_name + ";Persist Security Info=True;User ID=" + this.db_userid + ";Password=" + this.db_password + ";charset=utf8\"");


            //"metadata=res://*/Model.SNNetModel.csdl|res://*/Model.SNNetModel.ssdl|res://*/Model.SNNetModel.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=localhost;user id=root;password=12345;persistsecurityinfo=True;database=sn&quot;"
        }

        public static snEntities DataSet()
        {
            //DbConnectionConfig config = new LocalDbConfig(working_express_db).ConfigValue;

            DBX db_context = new DBX();
            return db_context.GetDBEntities();
        }
    }
}
