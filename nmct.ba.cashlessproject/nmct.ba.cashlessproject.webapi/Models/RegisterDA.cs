using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webapi.helperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.webapi.Models
{
    public class RegisterDA
    {
        public static int ModifyRegister(Register register, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Update Registers Set RegisterName = @Register, Device = @Device where Id = @Id";
            DbParameter par1 = Database.AddParameter("Test", "@Id", register.Id);
            DbParameter par2 = Database.AddParameter("Test", "@Register", register.Registername);
            DbParameter par3 = Database.AddParameter("Test", "@Device", register.Device);
            return Database.ModifyData(con, sql, par1, par2, par3);
        }
        public static int DeleteRegister(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Delete from Registers  where Id = @Id";
            DbParameter par1 = Database.AddParameter("Test", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddRegister(Register register, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Registers (RegisterName, Device) VALUES (@Register,@Device)";
            DbParameter par1 = Database.AddParameter("Test", "@Register", register.Registername);
            DbParameter par2 = Database.AddParameter("Test", "@Device", register.Device);
            return Database.InsertData(con, sql, par1, par2);
        }


        public static List<Register> GetListRegister(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Register> lijst = new List<Register>();
            DbDataReader reader = Database.GetData(con, "SELECT * FROM dbo.Registers");
            while (reader.Read())
            {
                Register re = CreateRegister(reader);
                lijst.Add(re);
            }
            return lijst;
        }

        private static Register CreateRegister(IDataRecord reader)
        {
            return new Register()
            {
                Id = Convert.ToInt32(reader["Id"].ToString()),
                Registername = reader["Registername"].ToString(),
                Device = reader["Device"].ToString(),
            };
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