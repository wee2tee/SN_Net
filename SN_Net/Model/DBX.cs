﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using SN_Net.Subform;

namespace SN_Net.Model
{
    public class DBX
    {
        private string server_name;
        public string ServerName { get { return this.server_name; } }
        private string db_name;
        public string DbName { get { return this.db_name; } }
        private string db_userid;
        public string DbUserId { get { return this.db_userid; } }
        private string db_password;
        public string DbPassword { get { return this.db_password; } }
        public int port_no;
        public int PortNo { get { return this.port_no; } }

        public DBX()
        {
            PreferenceValue pref = DialogPreference.GetPreference(DialogPreference.PREF_TYPE.MAIN_SN_DATA);

            this.server_name = pref != null ? pref.serverName : string.Empty; // "localhost";
            this.db_name = pref != null ? pref.dbName : string.Empty; // "sn";
            this.db_userid = pref != null ? pref.userId : string.Empty; // "root";
            this.db_password = pref != null ? pref.passWord : string.Empty; // "12345";

            //this.port_no = 3306;
            int port_num;
            if (pref != null && Int32.TryParse(pref.port, out port_num))
            {
                this.port_no = port_num;
            }
            else
            {
                this.port_no = 3306;
            }
        }

        private DBX(PreferenceValue pref)
        {
            this.server_name = pref != null ? pref.serverName : string.Empty; //"localhost";
            this.db_name = pref != null ? pref.dbName : string.Empty; //"sn";
            this.db_userid = pref != null ? pref.userId : string.Empty; //"root";
            this.db_password = pref != null ? pref.passWord : string.Empty; //"12345";

            int port_num;
            if (pref != null && Int32.TryParse(pref.port, out port_num))
            {
                this.port_no = port_num;
            }
            else
            {
                this.port_no = 3306;
            }
        }

        public snEntities GetDBEntities()
        {
            return new snEntities("metadata=res://*/Model.SNNetModel.csdl|res://*/Model.SNNetModel.ssdl|res://*/Model.SNNetModel.msl;provider=MySql.Data.MySqlClient;provider connection string=\"Data Source=" + this.server_name + ";Port=" + this.port_no.ToString() + ";Initial Catalog=" + this.db_name + ";Persist Security Info=True;User ID=" + this.db_userid + ";Password=" + this.db_password + ";charset=utf8\"");
        }

        public static snEntities DataSet()
        {
            DBX db_context = new DBX();
            return db_context.GetDBEntities();
        }

        public static bool TestConnection(PreferenceValue pref)
        {
            DBX db_context = new DBX(pref);
            snEntities sn = db_context.GetDBEntities();
            try
            {
                if (sn.Database.Exists())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
