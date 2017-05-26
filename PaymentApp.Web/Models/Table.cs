using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApp.Web.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int Capacity { get; set; }

    }
}