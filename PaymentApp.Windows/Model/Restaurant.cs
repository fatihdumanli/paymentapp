using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PaymentApp.Windows.Model
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PhoneNumber { get; set; }

        public string ImageUri { get; set; }

        private BitmapImage _image;

        public BitmapImage Image
        {
            get
            {
                if (ImageUri == null)
                    return new BitmapImage();

                return _image = _image ?? new BitmapImage(new Uri(ImageUri));
            }
        } 

    }
}
