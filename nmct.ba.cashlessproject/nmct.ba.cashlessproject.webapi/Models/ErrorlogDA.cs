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
    public class ErrorlogDA
    {
        public static int AddError(Errorlog error, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Error  VALUES (@Regist,@Time, @Mes, @Sta)";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Regist", error.Register.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Time", error.Timestamp);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Mes", error.Message);
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@Sta", error.Stacktrace);
            return Database.InsertData(con, sql, par1, par2, par3, par4);
        }
        private static DbConnection GetConnection(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            return Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MICHIEL\PROJECT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass)));
        }
    }
}