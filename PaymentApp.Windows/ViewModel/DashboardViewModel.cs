using PaymentApp.Windows.Services;
using PaymentApp.Windows.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ZXing.Mobile;

namespace PaymentApp.Windows.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {

        private Command _cmdScan;
        public Command CmdScan
        {
            get { return _cmdScan = _cmdScan ?? new Command(Scan); }
        }

        private async void Scan()
        {

            MobileBarcodeScanner _scanner = new MobileBarcodeScanner(((Frame)Window.Current.Content).Dispatcher);
            _scanner.UseCustomOverlay = false;

            _scanner.TopText = "Hold camera up to QR code";

            _scanner.BottomText = "Camera will automatically scan QR code\r\n\rPress the 'Back' button to cancel";

            var result = await _scanner.Scan();
            var res = result.ToString();

            await SessionService.Instance.StartSession(AccountService.Customer.Id,
                Convert.ToInt32(res.Split('-')[0]),
                Convert.ToInt32(res.Split('-')[1]), Convert.ToInt32(res.Split('-')[2]));

            ((Frame)Window.Current.Content).Navigate(typeof(Restaurant));


        }
    }
}
