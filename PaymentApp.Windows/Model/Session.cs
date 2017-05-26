using PaymentApp.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Model
{
    public class Session
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public int Slot { get; set; }
        public int CustomerId { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }


        public async Task<Transaction> InsertTransaction(int customerId, int productId)
        {
            return await SessionService.Instance.AddTransaction(new Transaction()
            {
                CustomerId = customerId,
                ProductId = productId,
                SessionId = this.Id,
                Created = DateTime.Now
            });
        }
    }
}
