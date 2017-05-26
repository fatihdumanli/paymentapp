using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApp.Windows.Model;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PaymentApp.Windows.Services
{
    public class AccountService : IAccountService
    {

        private const string SERVICE_URL = "http://paymentapp1.azurewebsites.net";
        private AccountService() { }

        private static AccountService _instance;

        public static AccountService Instance
        {
            get
            {
                return _instance ?? new AccountService();
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }

            set
            {
                _isLoggedIn = value;
            }
        }

        private bool _isLoggedIn;
        private static Customer _customer;
        public static Customer Customer
        {
            get
            {
                return _customer;
            }

            set
            {
                _customer = value;
            }
        }

        public async Task<string> Register(Customer c)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("firstname", c.FirstName);
                jo.Add("lastname", c.LastName);
                jo.Add("email", c.Email);
                jo.Add("phonenumber", c.PhoneNumber);
                jo.Add("password", c.Password);
                jo.Add("sex", c.Sex);


                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Customers/Create", stringContent);
                return res.ToString();
            }


        }

        public async Task<bool> Login(string email, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("email", email);
                jo.Add("password", password);

                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Customers/Login", stringContent);

                var result = await res.Content.ReadAsStringAsync();


                if (Convert.ToBoolean(result))
                {
                    Customer = await GetCustomerByEmailAddress(email);
                }

                return Convert.ToBoolean(result);
            }
        }

        public async Task<Customer> GetCustomerByEmailAddress(string email)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);
                JObject jo = new JObject();
                jo.Add("email", email);

                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Customers/GetCustomerByEmailAddress", stringContent);
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(result);
          
            }
        }
    }
}
