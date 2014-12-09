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
    public class KassaDA
    {
        public static int CreateKassa(ItRegister reg)
        {
            string sql = "INSERT INTO Registers (RegisterName,Device,purchaseDate,ExpiresDate)VALUES (@Register,@Device,@purchase, @expire)";
            DbParameter par1 = Database.addParameter("AdminConnection", "@Register", reg.RegisterName);
            DbParameter par2 = Database.addParameter("AdminConnection", "@Device", reg.Device);
            DbParameter par3 = Database.addParameter("AdminConnection", "@purchase", reg.PurchaseDate);
            DbParameter par4 = Database.addParameter("AdminConnection", "@expire", reg.ExpireDate);
            return Database.InsertData("AdminConnection", sql, par1, par2, par3,par4);
        }

        public static List<PMKassa> getKassas() {
            List<PMKassa> resultaat = new List<PMKassa>();
            DbDataReader reader = Database.GetData("AdminConnection", "select re.Id, re.RegisterName, re.Device, re.PurchaseDate, re.ExpiresDate, orre.UntilDate,orre.FromDate, org.OrganisationName,org.Id as 'IdOrg' from dbo.Registers re left outer join Organisation_Register orre on re.ID = orre.RegisterId left outer join Organisations org on orre.OrganisationId = org.Id");
            while (reader.Read())
            {
                PMKassa kassa = CreateKassa(reader);
                resultaat.Add(kassa);
            }
            reader.Close();
            return resultaat;
        }


        private static PMKassa CreateKassa(IDataRecord reader)
        {
            ItRegister nieuw = new ItRegister()
            {
                Id = Convert.ToInt32(reader["Id"].ToString()),
                RegisterName = reader["RegisterName"].ToString(),
                Device = reader["Device"].ToString(),
                PurchaseDate = Convert.ToInt32(reader["PurchaseDate"].ToString()),
                ExpireDate = Convert.ToInt32(reader["ExpiresDate"].ToString()),
            };
            PMKassa kassa =  new PMKassa();
            kassa.Kassa = nieuw;
            string org;
            if (reader["OrganisationName"].ToString() == "")
                 org = "/";
            else org = reader["OrganisationName"].ToString();

            kassa.OrganisationName = org ;
            if(reader["FromDate"].ToString() != "")
                kassa.Fromdate = Convert.ToInt32(reader["FromDate"].ToString());
            if (reader["UntilDate"].ToString() != "")
                kassa.UntilDate = Convert.ToInt32(reader["UntilDate"].ToString());
            if (reader["IdOrg"].ToString() != "")
                kassa.IdOrganisation = Convert.ToInt32(reader["Idorg"].ToString());
            return kassa;
        }
        public static int KoppelKassaAanOrganisatie(PMKassa kassa, int i)
        {
            try
            {
                string sql = "INSERT INTO Organisation_Register (OrganisationId,RegisterId,FromDate,UntilDate)VALUES (@org,@reg,@from, @Until)";
                DbParameter par1 = Database.addParameter("AdminConnection", "@org", kassa.IdOrganisation);
                DbParameter par2 = Database.addParameter("AdminConnection", "@reg", i);
                DbParameter par3 = Database.addParameter("AdminConnection", "@from", kassa.Fromdate);
                DbParameter par4 = Database.addParameter("AdminConnection", "@Until", kassa.UntilDate);
                return Database.InsertData("AdminConnection", sql, par1, par2, par3, par4);
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public static int ChangeKassa(ItRegister reg)
        {
            string sql = "Update Registers set RegisterName = @Register,Device = @Device,purchaseDate= @purchase,ExpiresDate= @expire where Id = @id";
            DbParameter par1 = Database.addParameter("AdminConnection", "@Register", reg.RegisterName);
            DbParameter par2 = Database.addParameter("AdminConnection", "@Device", reg.Device);
            DbParameter par3 = Database.addParameter("AdminConnection", "@purchase", reg.PurchaseDate);
            DbParameter par4 = Database.addParameter("AdminConnection", "@expire", reg.ExpireDate);
            DbParameter par5 = Database.addParameter("AdminConnection", "@id", reg.Id);
            return Database.ModifyData("AdminConnection", sql, par1, par2, par3, par4,par5);
        }
        public static int DeleteKassa(int i)
        {
            string sql = "Delete From Registers where Id = @Id";
            DbParameter par1 = Database.addParameter("AdminConnection", "@Id", i);
            return Database.ModifyData("AdminConnection", sql, par1);
        }
        public static int DeleteKoppelAanOrg(int i)
        {
            string sql = "Delete From Organisation_Register where RegisterId = @Id";
            DbParameter par1 = Database.addParameter("AdminConnection", "@Id", i);
            return Database.ModifyData("AdminConnection", sql, par1);
        }
    }
}