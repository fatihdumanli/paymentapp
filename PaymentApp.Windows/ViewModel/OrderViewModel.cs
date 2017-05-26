using PaymentApp.Windows.Model;
using PaymentApp.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaymentApp.Windows.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        private Session _session;

        public Session Session
        {
            get
            {
                return _session;
            }

            set
            {
                _session = value;
                base.RaisePropertyChanged(nameof(Session));
            }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get
            {
                return _products = _products ?? new List<Product>();
            }

            set
            {
                _products = value;
                base.RaisePropertyChanged(nameof(Products));
            }
        }

        private Command _cmdInit;
        public Command CmdInit
        {
            get
            {
                return _cmdInit = _cmdInit ?? new Command(Init);
            }
        }

        private Command _cmdConfirm;
        public Command CmdConfirm
        {
            get
            {
                return _cmdConfirm ?? new Command(Confirm);
            }
        }

        private async void Confirm()
        {
            foreach(var item in Products)
            {
                await Session.InsertTransaction(Session.CustomerId, item.Id);
            }

            ((Frame)Window.Current.Content).Navigate(typeof(PaymentApp.Windows.View.Restaurant));

        }

        private void Init()
        {

        }
    }
}
