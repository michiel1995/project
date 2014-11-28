﻿using Newtonsoft.Json;
using nmct.ba.cashlessproject.kassa.medewerker.ViewModel;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.kassa.medewerker
{
    class Servicelayer
    {
        private const string baseUrl = "http://localhost:4730/api";
        public static async Task<Customer> GetCustomer(int i)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync(baseUrl + "/customer/" + i);
                if (response.IsSuccessStatusCode)
                {
                    string custjson = await response.Content.ReadAsStringAsync();
                    Customer cust = JsonConvert.DeserializeObject<Customer>(custjson);
                    return cust;
                }
                return null;
            }
        }

        public static async Task<Employee> GetMedewerker(int i)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync(baseUrl + "/Employer/" + i);
                if (response.IsSuccessStatusCode)
                {
                    string custjson = await response.Content.ReadAsStringAsync();
                    Employee cust = JsonConvert.DeserializeObject<Employee>(custjson);
                    return cust;
                }
                return null;
            }
        }

        public static async Task<ObservableCollection<Product>> getProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseUrl + "/product");
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
                return null;
            }

        }


        public static async Task<Register> GetRegister(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseUrl + "/register/" + id);
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
