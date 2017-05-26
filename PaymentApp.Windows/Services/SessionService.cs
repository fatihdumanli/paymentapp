using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentApp.Windows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Services
{
    public class SessionService : ISessionService
    {
        private const string SERVICE_URL = "http://paymentapp1.azurewebsites.net";


        private static Session _session;
        public static Session Session
        {
            get
            {
                return _session;
            }

            set
            {
                _session = value;
            }
        }


        private SessionService() { }
        private static SessionService _instance;
        public static SessionService Instance
        {
            get
            {
                return _instance ?? new SessionService();
            }
        }

        public async Task<Session> StartSession(int customerId, int restaurantId, int tableId, int slot)
        {
            using(HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("restaurantId", restaurantId);
                jo.Add("tableId", tableId);
                jo.Add("slot", slot);
                jo.Add("started", DateTime.Now);
                jo.Add("customerId", customerId);
              
                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Sessions/Create", stringContent);
                var result = await res.Content.ReadAsStringAsync();
                Session = JsonConvert.DeserializeObject<Session>(result);
                return JsonConvert.DeserializeObject<Session>(result);
            }

        }

        public async Task<Transaction> AddTransaction(Transaction t)
        {

            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("customerId", t.CustomerId);
                jo.Add("productId", t.ProductId);
                jo.Add("sessionId", t.SessionId);
                jo.Add("created", t.Created);

                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Transactions/Create", stringContent);
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Transaction>(result);
            }

        }

        public async Task<List<Transaction>> GetSessionTransactions()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("sessionId", Session.Id);

                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Sessions/GetTransactionsBySessionId", stringContent);
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Transaction>>(result);
            }
        }

        public async Task Pay()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);

                JObject jo = new JObject();
                jo.Add("id", Session.Id);

                var stringContent = new StringContent(jo.ToString());
                stringContent.Headers.Remove("content-type");
                stringContent.Headers.Add("content-type", "application/json");
                var res = await client.PostAsync("/Sessions/Pay", stringContent);
                var result = await res.Content.ReadAsStringAsync();
            }

        }
    }
}
