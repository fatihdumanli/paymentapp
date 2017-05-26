using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApp.Windows.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace PaymentApp.Windows.Services
{
    public class RestaurantService : IRestaurantService
    {
        private const string SERVICE_URL = "http://paymentapp1.azurewebsites.net";

        private RestaurantService() { }

        private static RestaurantService _instance;

        public static RestaurantService Instance
        {
            get
            {
                return _instance ?? new RestaurantService();
            }
        }


        public async Task<Restaurant> GetRestaurantDetails(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);
                var result = await client.GetStringAsync(String.Format("/Restaurants/Details/{0}", id));
                return JsonConvert.DeserializeObject<Restaurant>(result);
            }
        }

        public async Task<IEnumerable<Product>> GetRestaurantProducts(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);
                var result = await client.GetStringAsync(
                    String.Format("/Products/GetRestaurantProducts?restaurantId={0}", id));

                return JsonConvert.DeserializeObject<List<Product>>(result);
            }
        }

        public async Task<Product> GetProductByTransaction(int productId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);
                var result = await client.GetStringAsync(
                    String.Format("/Products/Details/{0}", productId));

                return JsonConvert.DeserializeObject<Model.Product>(result);
            }
        }
    }
}
