using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webservice.helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace nmct.ba.cashlessproject.webservice.Models
{
    public class OrganisatieCreateDatabase
    {
        
        //gemaakt door frederik duchi
        public static void CreateDatabase(ItOrganisation o)
        {
            try
            {
                // create the actual database
                string create = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/createDatabase/create.txt"));
                //string create = System.IO.File.ReadAllText(@"App_Data/createDatabase/create.txt");// only for desktop
                string sql = create.Replace("@@DbName", Cryptography.Decrypt(o.DbName)).Replace("@@DbLogin", Cryptography.Decrypt(o.DbLogin)).Replace("@@DbPassword", Cryptography.Decrypt(o.DBpass));
                foreach (string commandText in RemoveGo(sql))
                {
                    Database.ModifyData("AdminConnection", commandText);
                }
            }
            catch
            {

            }
            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction("AdminConnection");

                string fill = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/createDatabase/fill.txt"));
                //string fill = System.IO.File.ReadAllText(@"App_Data/createDatabase/fill.txt"); // only for desktop
                string sql2 = fill.Replace("@@DbName", Cryptography.Decrypt(o.DbName)).Replace("@@DbLogin", Cryptography.Decrypt(o.DbLogin)).Replace("@@DbPassword", Cryptography.Decrypt(o.DBpass));

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }
    }
}