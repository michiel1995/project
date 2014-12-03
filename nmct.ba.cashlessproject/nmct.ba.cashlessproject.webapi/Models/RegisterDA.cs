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
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", register.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Register", register.Registername);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Device", register.Device);
            return Database.ModifyData(con, sql, par1, par2, par3);
        }
        public static int DeleteRegister(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Delete from Registers  where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddRegister(Register_Employee register, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Register_employee (RegisterId, EMployeeId, FromTime, UntilTime) VALUES (@Register,@Employe, @from, @until)";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Register", register.Kassa.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Employe", register.Medewerker.Id);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@from", DateTimeToTimestamp(register.From));
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@until", DateTimeToTimestamp(register.Until));
            return Database.InsertData(con, sql, par1, par2,par3,par4);
        }


        public static List<Register_Employee> GetListRegisters(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Register_Employee> lijst = new List<Register_Employee>();
            DbDataReader reader = Database.GetData(con, "Select reem.UntilTime, reem.FromTime,emp.Address, emp.Email, emp.EmployeeName,emp.Id,emp.Phone, reg.Device,reg.Id as 'regId',reg.Registername from dbo.Register_Employee reem left outer join dbo.Employee emp on reem.EmployeeId = emp.Id left outer join dbo.Registers reg on reem.RegisterId = reg.Id");
            while (reader.Read())
            {
                Register_Employee re = CreateRegister_Employee(reader);
                lijst.Add(re);
            }
            return lijst;
        }
        public static Register GetRegister(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            Register reg = new Register();
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@id", id);
            DbDataReader reader = Database.GetData(con, "SELECT Id,Registername,Device FROM dbo.Registers where Id = @id ", par1);
            reader.Read();
            reg = CreateRegister(reader);
            return reg;
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

        private static Register_Employee CreateRegister_Employee(IDataRecord reader)
        {
            Register re = new Register()
            {
                Id = Convert.ToInt32(reader["regId"].ToString()),
                Registername = reader["Registername"].ToString(),
                Device = reader["Device"].ToString(),
            };
            Employee emp = new Employee(){
                Email =  reader["Email"].ToString(),
                Address =  reader["Address"].ToString(),
                Id = Convert.ToInt32( reader["Id"].ToString()),
                Name =  reader["EmployeeName"].ToString(),
                Phone =  reader["Phone"].ToString(),
            };
            return new Register_Employee()
            {
                Kassa = re,
                Medewerker = emp,
                From = TimestampToDateTime(Convert.ToInt32(reader["FromTime"].ToString())),
                Until = TimestampToDateTime(Convert.ToInt32(reader["UntilTime"].ToString()))
            };
        }

        private static DateTime TimestampToDateTime(int unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();          
            String.Format("{0:d/M/yyyy HH:mm:ss}", dtDateTime);
            return dtDateTime;
        }
        private static int DateTimeToTimestamp(DateTime dateTime)
        {
            return Convert.ToInt32((dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds);
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