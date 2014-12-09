using Newtonsoft.Json;
using nmct.ba.cashlessproject.managment.ViewModel;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.managment
{
    public class servicelayer
    {
       
        private const string baseurl = "http://localhost:4730/api";

        #region Products
        public static async Task<ObservableCollection<Product>> getProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseurl + "/product");
                if(resp.IsSuccessStatusCode)
                {                 
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
                return null;
            }

        }
        public static async Task<Boolean> AddProduct(Product pro)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(pro);
                HttpResponseMessage resp = await client.PostAsync(baseurl + "/product",new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
        public static async Task<Boolean> PutProduct(Product pro)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(pro);
                HttpResponseMessage resp = await client.PutAsync(baseurl + "/product", new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
        public static async Task<Boolean> DeleteProduct(int pro)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.DeleteAsync(baseurl + "/product/" + pro);
                return resp.IsSuccessStatusCode;
            }
        }
        #endregion

        #region Employee
        public static async Task<ObservableCollection<Employee>> getEmployee()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseurl + "/employer");
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
                return null;
            }

        }
        public static async Task<Boolean> AddEmployee(Employee emp)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(emp);
                HttpResponseMessage resp = await client.PostAsync(baseurl + "/employer", new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
        public static async Task<Boolean> PutEmployee(Employee emp)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(emp);
                HttpResponseMessage resp = await client.PutAsync(baseurl + "/employer", new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
        public static async Task<Boolean> DeleteEmployee(int emp)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.DeleteAsync(baseurl + "/employer/" + emp);
                return resp.IsSuccessStatusCode;
            }
        }
        #endregion

        #region Customer
        public static async Task<ObservableCollection<Customer>> getCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseurl + "/customer");
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Customer>>(json);
                }
                return null;
            }

        }

        public static async Task<Boolean> PutCustomer(Customer cust)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(cust);
                HttpResponseMessage resp = await client.PutAsync(baseurl + "/customer", new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
        #endregion

        #region Kassa
        public static async Task<ObservableCollection<Register_Employee>> getKassas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage resp = await client.GetAsync(baseurl + "/register");
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Register_Employee>>(json);
                }
                return null;
            }

        }

        #endregion

        public static async Task<Boolean> ChangePass(ChangePassword pass)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string json = JsonConvert.SerializeObject(pass);
                HttpResponseMessage resp = await client.PutAsync(baseurl + "/changepass", new StringContent(json, Encoding.UTF8, "application/json"));
                return resp.IsSuccessStatusCode;
            }
        }
    }
}
