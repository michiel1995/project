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
    public class LogboekDA
    {
        public static int DeleteLog(int registerid, int timestamp)
        {
            DbParameter par1 = Database.addParameter("AdminConnection", "@time", timestamp);
            DbParameter par2 = Database.addParameter("AdminConnection", "@id", registerid);
            return Database.ModifyData("AdminConnection", "Delete From Errorlog where Timestamp = @time AND RegisterId= @id");
        }
        public static List<PMLogboek> getLogs()
        {
            List<PMLogboek> resultaat = new List<PMLogboek>();
            DbDataReader reader = Database.GetData("AdminConnection", "select org.OrganisationName, org.Address, org.Email, org.Phone, org.DbName, reg.RegisterName, reg.Device, reg.ExpiresDate, er.Message,er.RegisterId,er.Stacktrace,er.Timestamp from dbo.Errorlog er left outer join Registers reg on er.RegisterId = reg.Id left outer join Organisation_Register orgre on orgre.RegisterId = reg.Id left outer join Organisations org on orgre.OrganisationId = org.Id");
            while (reader.Read())
            {
                PMLogboek log = createlog(reader);
                resultaat.Add(log);
            }
            reader.Close();
            return resultaat;
        }

        private static PMLogboek createlog(IDataRecord reader)
        {
            ItErrorlog nieuw = new ItErrorlog()
            {
                Message = reader["Message"].ToString(),
                RegisterId = Convert.ToInt32(reader["RegisterId"].ToString()),
                Stacktrace = reader["Stacktrace"].ToString(),
                Timestamp = Convert.ToInt32(reader["Timestamp"].ToString())
            };
            return new PMLogboek()
            {
                RegisterName = reader["RegisterName"].ToString(),
                Address = reader["Address"].ToString(),
                Error = nieuw,
                DatabaseName = reader["DbName"].ToString(),
                Device = reader["Device"].ToString(),
                ExpiresDate = reader["ExpiresDate"].ToString(),
                Email = reader["Email"].ToString(),
                OrganisationName = reader["OrganisationName"].ToString(),
                Phone = reader["Phone"].ToString()
            };
        }

    }
}