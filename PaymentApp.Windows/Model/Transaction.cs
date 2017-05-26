using PaymentApp.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int SessionId { get; set; }
        public DateTime Created { get; set; }
        public bool IsPaid { get; set; }

        public Product Product
        {
            get
            {
                var res = Task.Run(async () => await RestaurantService.Instance.GetProductByTransaction(ProductId))
                    .Result;

                return res;
            }
        }
        
    }
}
