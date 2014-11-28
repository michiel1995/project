using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webapi.helperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using nmct.ba.cashlessproject.helper;

namespace nmct.ba.cashlessproject.webapi.Models
{
    
    public class CustomerDA
    {
        public static int ModifyCustomer(Customer cust, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            
            string sql = "Update Customers Set CustomerName = @CustomerName, Address = @Address, Balance = @Balance where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", cust.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@CustomerName", cust.Name);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Address", cust.Address);
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@Picture", cust.Image);
            DbParameter par5 = Database.AddParameter("System.Data.SqlClient", "@Balance", cust.Balance);
            return Database.ModifyData(con, sql, par1, par2, par3, par4, par5);
        }

        private static DbConnection GetConnection(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            return Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MICHIEL\PROJECT", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass)));
        }
        public static int DeleteCustomer(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Delete from Customer  where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddCustomer(Customer newCustomer, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Customers VALUES (@Id,@CustomerName,@Address, @Picture, @Balance)";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", newCustomer.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@CustomerName", newCustomer.Name);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Address", newCustomer.Address);
            DbParameter par4 = Database.AddParameter("System.Data.SqlClient", "@Picture", Convert.ToBase64String(newCustomer.Image));
            DbParameter par5 = Database.AddParameter("System.Data.SqlClient", "@Balance", newCustomer.Balance);
            return Database.InsertData(con, sql, par1, par2, par3, par4, par5);
        }

        public static Customer GetCustomer(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            Customer cust = new Customer();
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@id", id);
            DbDataReader reader = Database.GetData(con, "SELECT Id,CustomerName,Address,Picture,Balance FROM dbo.Customers where Id = @id ",par1);
            reader.Read();
            cust = CreateCustomer(reader);
            return cust;
        }

        public static List<Customer> GetListCustomers(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Customer> lijst = new List<Customer>();
            DbDataReader reader = Database.GetData(con, "SELECT Id,CustomerName,Address,Picture,Balance FROM dbo.Customers");
            while (reader.Read())
            {
                Customer cs = CreateCustomer(reader);
                lijst.Add(cs);
            }
            reader.Close();
            return lijst;
        }

        private static Customer CreateCustomer(IDataRecord reader)
        {
            try
            {
                return new Customer()
                {
                    Address = reader["address"].ToString(),
                    Balance = Convert.ToDouble(reader["Balance"].ToString()),
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Image = Convert.FromBase64String(reader["Picture"].ToString()),
                    Name = reader["CustomerName"].ToString()
                };
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}