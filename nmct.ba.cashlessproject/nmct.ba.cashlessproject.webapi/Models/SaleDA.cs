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
    public class SaleDA
    {
        public static int DeleteSale(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Delete from Sales  where Id = @Id";
            DbParameter par1 = Database.AddParameter("Test", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddSale(Sale sale, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO sales (Timestamp, CustomerId,RegisterId,ProductId,Amount,TotalPrice) VALUES (@Time,@Cust, @Register, @Product, @Amount,@Total)";
            DbParameter par1 = Database.AddParameter("Test", "@Time", sale.Timestamp);
            DbParameter par2 = Database.AddParameter("Test", "@Cust", sale.Customer.Id);
            DbParameter par3 = Database.AddParameter("Test", "@Register", sale.Register.Id);
            DbParameter par4 = Database.AddParameter("Test", "@Product", sale.Product.Id);
            DbParameter par5 = Database.AddParameter("Test", "@Amount", sale.Amount);
            DbParameter par6 = Database.AddParameter("Test", "@Total", sale.Price);
            return Database.InsertData(con, sql, par1, par2, par3, par4, par5, par6);
        }


        public static List<Sale> GetListSale(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Sale> lijst = new List<Sale>();
            DbDataReader reader = Database.GetData(con, "SELECT * FROM dbo.Sales");
            while (reader.Read())
            {
                Sale sa = CreateSale(reader);
                lijst.Add(sa);
            }
            return lijst;
        }

        private static Sale CreateSale(IDataRecord reader)
        {
            return new Sale()
            {
                Id = Convert.ToInt32(reader["Id"].ToString()),
                /*Customer = getCustomer(Convert.ToInt32(reader["CustomerId"].ToString())),
                Register = getRegister(Convert.ToInt32(reader["RegisterId"].ToString())),
                Product = getProduct(Convert.ToInt32(reader["ProductId"].ToString())),*/
                Amount = Convert.ToDouble(reader["Amount"].ToString()),
                Price = Convert.ToDouble(reader["TotalPrice"].ToString())
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