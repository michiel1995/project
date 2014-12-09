using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webservice.helper;
using nmct.ba.cashlessproject.webservice.presentationModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webservice.Models
{
    public class VerenigingDA
    {
        public static int UpdateVereniging(ItOrganisation org)
        {
            string sql = "Update Organisations Set  OrganisationName = @OrganisationName, Address = @Address, Email = @Email, Phone = @Phone where Id = @Id";
            DbParameter par1 = Database.addParameter("AdminConnection", "@OrganisationName",org.OrganistionName);
            DbParameter par2 = Database.addParameter("AdminConnection", "@Address", org.Address);
            DbParameter par3 = Database.addParameter("AdminConnection", "@Email", org.Email);
            DbParameter par4 = Database.addParameter("AdminConnection", "@Phone", org.Phone);
            DbParameter par5 = Database.addParameter("AdminConnection", "@Id", org.Id);
            return Database.ModifyData("AdminConnection", sql, par1, par2, par3, par4, par5);
        }
        public static int NieuweVerenigingToevoegen(ItOrganisation org)
        {
            try
            {
                //wachtwoorden nog encrypteren
                string sql = "INSERT INTO Organisations (Login,Password,DbName,DbLogin,DbPassword,OrganisationName,Address,Email,Phone) VALUES (@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
                DbParameter par1 = Database.addParameter("AdminConnection", "@Login", org.Login);
                DbParameter par2 = Database.addParameter("AdminConnection", "@Password", org.Pass);
                DbParameter par3 = Database.addParameter("AdminConnection", "@DbName", org.DbName);
                DbParameter par4 = Database.addParameter("AdminConnection", "@DbPassword", org.DBpass);
                DbParameter par5 = Database.addParameter("AdminConnection", "@OrganisationName", org.OrganistionName);
                DbParameter par6 = Database.addParameter("AdminConnection", "@Address", org.Address);
                DbParameter par7 = Database.addParameter("AdminConnection", "@Email", org.Email);
                DbParameter par8 = Database.addParameter("AdminConnection", "@Phone", org.Phone);
                DbParameter par9 = Database.addParameter("AdminConnection", "@DbLogin", org.DbLogin);
                return Database.InsertData("AdminConnection", sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            }
            catch{
                return 0;
            }
        }
        public static int DeleteOrganisation(int i)
        {
            string sql = "Delete From Organisations where Id = @Id";
            DbParameter par1 = Database.addParameter("AdminConnection", "@Id", i);
            return Database.ModifyData("AdminConnection", sql, par1);
        }
        public static List<ItOrganisation> getVerenigingen()
        {
            List<ItOrganisation> resultaat = new List<ItOrganisation>();
            DbDataReader reader = Database.GetData("AdminConnection", "select Id,Login,Password,DbName,DbLogin,DbPassword,OrganisationName, Address, Email, Phone from organisations");
            while (reader.Read())
            {
                ItOrganisation org = CreateOrganisation(reader);
                resultaat.Add(org);
            }
            reader.Close();
            return resultaat;
        }

        public static ItOrganisation getVereniging(int id )
        {
            DbParameter par1 = Database.addParameter("AdminConnection", "@id", id);
            DbDataReader reader = Database.GetData("AdminConnection", "select Id,Login,Password,DbName,DbLogin,DbPassword,OrganisationName, Address, Email, Phone from organisations where Id = @id",par1);
            reader.Read();           
            return CreateOrganisation(reader);
        }

        private static ItOrganisation CreateOrganisation(IDataRecord reader)
        {
            return new ItOrganisation()
            {
                Address =reader["Address"].ToString(),
                DbLogin = Cryptography.Decrypt(reader["DbLogin"].ToString()),
                DbName = Cryptography.Decrypt(reader["DbName"].ToString()),
                DBpass = Cryptography.Decrypt(reader["DbPassword"].ToString()),
                Email = reader["Email"].ToString(),
                Id = Convert.ToInt32(reader["Id"].ToString()),
                Login = Cryptography.Decrypt(reader["Login"].ToString()),
                OrganistionName = reader["OrganisationName"].ToString(),
                Pass = Cryptography.Decrypt(reader["Password"].ToString()),
                Phone = reader["Phone"].ToString()
            };
        }
    }
}