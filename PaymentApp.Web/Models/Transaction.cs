using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApp.Web.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int SessionId { get; set; }
        public DateTime Created { get; set; }
        public bool IsPaid { get; set; }
        
    }
}