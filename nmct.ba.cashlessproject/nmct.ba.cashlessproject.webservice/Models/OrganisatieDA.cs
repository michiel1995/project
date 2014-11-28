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
    public class OrganisatieDA
    {
        public static List<PMOrganisatie> getOrganisaties()
        {
            List<PMOrganisatie> resultaat = new List<PMOrganisatie>();
            DbDataReader reader = Database.GetData("AdminConnection", "select id, OrganisationName from organisations");
            while (reader.Read())
            {
                PMOrganisatie kassa = CreateOrganisatie(reader);
                resultaat.Add(kassa);
            }
            reader.Close();
            return resultaat;
        }


        private static PMOrganisatie CreateOrganisatie(IDataRecord reader)
        {
            return new PMOrganisatie()
            {
                Id = Convert.ToInt32(reader["Id"].ToString()),
                Naam = reader["OrganisationName"].ToString()
            };
        }
    }
}