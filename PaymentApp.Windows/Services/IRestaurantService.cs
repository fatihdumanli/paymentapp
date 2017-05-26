using PaymentApp.Windows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantDetails(int id);
        Task<IEnumerable<Product>> GetRestaurantProducts(int id);
        Task<Product> GetProductByTransaction(int productId);
    }
}
