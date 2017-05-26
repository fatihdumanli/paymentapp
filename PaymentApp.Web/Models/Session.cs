using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApp.Web.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public int Slot { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime Ended { get; set; }


    }
}