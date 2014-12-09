using Newtonsoft.Json;
using nmct.ba.cashlessproject.ba.kassa.klant.ViewModel;
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
                client.SetBearerToken(ApplicationVM.token.AccessToken);
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
                client.SetBearerToken(ApplicationVM.token.AccessToken);
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
        public static async Task<Boolean> UpdateCustomer(Customer cust)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(cust);
                HttpResponseMessage response = await client.PutAsync(baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        public static async void PostLog(Errorlog error)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(error);
                HttpResponseMessage response = await client.PostAsync("http://localhost:4730/api/errorlog", new StringContent(json, Encoding.UTF8, "application/json"));             
            }
        }


        public static async Task<Register> GetRegister(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync("http://localhost:4730/api/register/" + id);
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Register>(json);
                }
                return null;
            }
        }

    }
}
