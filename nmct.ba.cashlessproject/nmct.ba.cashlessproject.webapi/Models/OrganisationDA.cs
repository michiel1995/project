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
    public class OrganisationDA
    {
        public static ItOrganisation GetOrganisationByLoginAndPassword(string username, string password)
        {
            DbConnection con = GetConnection();
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Login", username);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Password", password);

            try
            {

                DbDataReader reader = Database.GetData(con, sql, par1, par2);
                reader.Read();
                return new ItOrganisation()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Pass = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DBpass = reader["DbPassword"].ToString(),
                    OrganistionName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private static DbConnection GetConnection()
        {
            return Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MICHIEL\PROJECT", "Admin", "AdminConnection", "P@ssw0rd"));
        }
    }
}