using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webapi.helperClass;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.webapi.Models
{
    public class ChangePassDA
    {
        public static int ChangePass(ChangePassword pass, IEnumerable<Claim> claims)
        {
            if(GetPassword(claims) == pass.Old)
            {
                ChangeLogin(claims, pass);
                return 1;
            }
            return 0;
        }

        private static void ChangeLogin(IEnumerable<Claim> claims, ChangePassword pas)
        {
            DbConnection con = GetConnection();

            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@oldpas", pas.Old);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@newpas", pas.New);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@dbname", dbname);
            string sql = "Update Organisations set Password = @newpas where Password = @oldpas AND DbName = @dbname ";
            Database.ModifyData(con, sql, par1, par3, par2);

        }
        public static string GetPassword ( IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection();
            string sql = "SELECT Password FROM Organisations WHERE DbLogin= @dbLogin AND DbPassword=@dbPassword AND DbName = @dbname";
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@dbname", dbname);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@dbPassword", dbpass);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@dbLogin", dblogin);
            DbDataReader reader = Database.GetData(con,sql, par1,par3,par2);
            reader.Read();
            return reader["Password"].ToString();
        }

        private static DbConnection GetConnection()
        {
            return Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MICHIEL\PROJECT", "Admin", "AdminConnection", "P@ssw0rd"));
        }
    }
}