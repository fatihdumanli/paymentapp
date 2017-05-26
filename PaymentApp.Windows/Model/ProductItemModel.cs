using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PaymentApp.Windows.Model
{
    public class ProductItemModel
    {
        public int Id { get; set; }
        public Image Image { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public int CatId { get; set; }

    }
}
