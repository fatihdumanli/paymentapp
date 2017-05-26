using PaymentApp.Windows.Model;
using PaymentApp.Windows.Services;
using PaymentApp.Windows.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaymentApp.Windows.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private Command _cmdRegister;
        private Customer _customerModel;

        public Command CmdRegister
        {
            get
            {
                return _cmdRegister = _cmdRegister ?? new Command(Register); 
            }
        }

        public Customer CustomerModel
        {
            get
            {
                return _customerModel = _customerModel ?? new Customer();
            }

            set
            {
                _customerModel = value;
                base.RaisePropertyChanged(nameof(CustomerModel));
            }
        }

        private async void Register()
        {
            await AccountService.Instance.Register(CustomerModel);

            ((Frame)Window.Current.Content).Navigate(typeof(Login));

        }
    }
}
