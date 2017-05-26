using PaymentApp.Windows.Services;
using PaymentApp.Windows.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace PaymentApp.Windows.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
      
        private Command _cmdNavToRegister;
        private Command _cmdLogin;
        private string _email;
        private string _password;
        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                base.RaisePropertyChanged(nameof(IsLoading));
            }
        }

        public Command CmdNavToRegister
        {
            get
            {
                return _cmdNavToRegister = _cmdNavToRegister ?? new Command(NavToRegister);
            }
        }
        public Command CmdLogin
        {
            get { return _cmdLogin = _cmdLogin ?? new Command(LoginF); }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                base.RaisePropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                base.RaisePropertyChanged(nameof(Password));
            }
        }

        private async void LoginF()
        {
            IsLoading = true;
            var res = await AccountService.Instance.Login(Email, Password);
            IsLoading = false;
            if (res)
                ((Frame)Window.Current.Content).Navigate(typeof(Dashboard), new DrillInNavigationTransitionInfo());
            else
            {
                IsLoading = false;
                MessageDialog md = new MessageDialog("The user doesn't exist!");
                await md.ShowAsync();
            }



        }

        private void NavToRegister()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(Register));
        }
    }
}
