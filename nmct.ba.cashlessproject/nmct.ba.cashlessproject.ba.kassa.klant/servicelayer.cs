using Newtonsoft.Json;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ba.kassa.klant
{
    public class servicelayer
    {
        private const string baseUrl = "http://localhost:4730/api/Customer";
        public static async Task<Boolean> AddCustomer(Customer cust)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(cust);
                HttpResponseMessage response = await client.PostAsync(baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        public static async Task<Customer> GetCustomer(int i )
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:4730/api/customer/123");
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
