using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentApp.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public double Price { get; set; }
        public string ImageUri { get; set; }
        public bool Availability { get; set; }
    }
}