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
    public class EmployerDA
    {
        public static int ModifyEmployee(Employee newEmployee, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Update Employee Set EmployeeName = @Employee, Address = @Address,Email = @Email, Phone = @Phone where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", newEmployee.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Employee", newEmployee.Name);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Address", newEmployee.Address);
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@Email", newEmployee.Email);
            DbParameter par5 = Database.AddParameter("System.Data.SqlClient", "@Phone", newEmployee.Phone);
            return Database.ModifyData(con, sql, par1, par2, par3, par4, par5);
        }
        public static int DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Delete from Employee  where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddEmployee(Employee newEmployee, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Employee VALUES (@Id,@Employee,@Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", newEmployee.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Employee", newEmployee.Name);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Address", newEmployee.Address);
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@Email", newEmployee.Email);
            DbParameter par5 = Database.AddParameter("System.Data.SqlClient", "@Phone", newEmployee.Phone);
            return Database.InsertData(con, sql, par1, par2, par3, par4, par5);
        }
        public static Employee GetEmployee(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", id);
            DbDataReader reader = Database.GetData(con, "SELECT Id,EmployeeName,Address,Email,Phone FROM dbo.Employee where Id = @Id",par1);
            reader.Read();
            return CreateEmployee(reader);
        }

        public static List<Employee> GetListEmployee(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Employee> lijst = new List<Employee>();
            DbDataReader reader = Database.GetData(con, "SELECT * FROM dbo.Employee");
            while (reader.Read())
            {
                Employee em = CreateEmployee(reader);
                lijst.Add(em);
            }
            return lijst;
        }

        private static Employee CreateEmployee(IDataRecord reader)
        {
            try
            {
                return new Employee()
                {
                    Address = reader["Address"].ToString(),
                    Id = Convert.ToInt32(reader["id"].ToString()),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Name = reader["EmployeeName"].ToString()
                };
            }
            catch (Exception)
            {
                return null;
            }
           
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