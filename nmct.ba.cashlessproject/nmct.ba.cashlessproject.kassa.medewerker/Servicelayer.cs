using Newtonsoft.Json;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.kassa.medewerker
{
    class Servicelayer
    {
        public static async Task<Customer> GetCustomer(int i)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:4730/api/customer/" + i);
                if (response.IsSuccessStatusCode)
                {
                    string custjson = await response.Content.ReadAsStringAsync();
                    Customer cust = JsonConvert.DeserializeObject<Customer>(custjson);
                    return cust;
                }
                return null;
            }
        }

        public static async Task<Customer> GetMedewerker(int i)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:4730/api/employer/123");
                if (response.IsSuccessStatusCode)
                {
                    string custjson = await response.Content.ReadAsStringAsync();
                    Customer cust = JsonConvert.DeserializeObject<Customer>(custjson);
                    return cust;
                }
                return null;
            }
        }

    }
}
