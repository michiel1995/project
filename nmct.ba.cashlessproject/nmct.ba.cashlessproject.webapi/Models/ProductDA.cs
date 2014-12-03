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
    public class ProductDA
    {
        public static int ModifyProduct(Product product, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Update Products Set ProductName = @ProductName, Price = @Price where Id = @Id";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", product.Id);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@ProductName", product.ProductName);
            DbParameter par3 = Database.AddParameter("System.Data.SqlClient", "@Price", product.Price);
            return Database.ModifyData(con, sql, par1, par2, par3);
        }
        public static int DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "Update Products set available = 0 where  Id = @Id ";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Id", id);
            return Database.ModifyData(con, sql, par1);
        }

        public static int AddProduct(Product product, IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            string sql = "INSERT INTO Products (ProductName,Price,availabe) VALUES (@Name,@Price,1)";
            DbParameter par1 = Database.AddParameter("System.Data.SqlClient", "@Name", product.ProductName);
            DbParameter par2 = Database.AddParameter("System.Data.SqlClient", "@Price", product.Price);
            return Database.InsertData(con, sql, par1, par2);
        }


        public static List<Product> GetListProduct(IEnumerable<Claim> claims)
        {
            DbConnection con = GetConnection(claims);
            List<Product> lijst = new List<Product>();
            DbDataReader reader = Database.GetData(con, "SELECT * FROM dbo.Products where available = 1");
            while (reader.Read())
            {
                Product pr = CreateProduct(reader);
                lijst.Add(pr);
            }
            return lijst;
        }

        private static Product CreateProduct(IDataRecord reader)
        {
            return new Product()
            {
                Id = Convert.ToInt32(reader["Id"].ToString()),
                ProductName = reader["ProductName"].ToString(),
                Price = Convert.ToDouble(reader["Price"].ToString()),
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