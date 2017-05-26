using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApp.Web.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}