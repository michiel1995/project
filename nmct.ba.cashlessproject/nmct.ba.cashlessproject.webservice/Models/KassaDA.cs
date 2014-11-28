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
            kassa.OrganisationName = reader["OrganisationName"].ToString();
            if(reader["FromDate"].ToString() != "")
                kassa.Fromdate = Convert.ToInt32(reader["FromDate"].ToString());
            if (reader["UntilDate"].ToString() != "")
                kassa.UntilDate = Convert.ToInt32(reader["UntilDate"].ToString());
            if (reader["IdOrg"].ToString() != "")
                kassa.IdOrganisation = Convert.ToInt32(reader["Idorg"].ToString());
            return kassa;
        }

    }
}