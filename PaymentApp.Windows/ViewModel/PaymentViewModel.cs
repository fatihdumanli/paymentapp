using PaymentApp.Windows.Model;
using PaymentApp.Windows.Services;
using PaymentApp.Windows.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaymentApp.Windows.ViewModel
{
    public class PaymentViewModel : ViewModelBase
    {


        private double _sum;

        public double Sum
        {
            get
            {
                return _sum;
            }

            set
            {
                _sum = value;
                base.RaisePropertyChanged(nameof(Sum));
            }
        }
        private Command _cmdNextStep;
        public Command CmdNextStep
        {
            get
            {
                return _cmdNextStep = _cmdNextStep ?? new Command(NextStep);
            }
        }

        private void NextStep()
        {
            paymentStep = PaymentStep.Checkout;
        }

        public enum PaymentStep
        {
            Summary = 1,
            Checkout = 2
        }


        private PaymentStep _paymentStep;

        public PaymentStep paymentStep
        {
            get
            {
                return _paymentStep;
            }

            set
            {
                _paymentStep = value;
                base.RaisePropertyChanged(nameof(paymentStep));
            }
        }

        private bool _isProcessing;

        public bool IsProcessing
        {
            get
            {
                return _isProcessing;
            }

            set
            {
                _isProcessing = value;
                base.RaisePropertyChanged(nameof(IsProcessing));
            }
        }

        private Command _cmdPay;
        public Command CmdPay
        {
            get
            {
                return _cmdPay = _cmdPay ?? new Command(Pay);
            }
        }

        private async void Pay()
        {
            IsProcessing = true;
            await SessionService.Instance.Pay();
            IsProcessing = false;

            MessageDialog md = new MessageDialog("Payment completed succeesfully.");
            await md.ShowAsync();

            SessionService.Session = null;
            ((Frame)Window.Current.Content).Navigate(typeof(Login));

        }

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }

            set
            {
                _transactions = value;
                base.RaisePropertyChanged(nameof(Transactions));
            }
        }

        private Command _cmdGetTransactions;
        public Command CmdGetTransactions
        {
            get
            {
                return _cmdGetTransactions = _cmdGetTransactions ?? new Command(GetTransactions);
            }
        }

        private async void GetTransactions()
        {
            paymentStep = PaymentStep.Summary;
            Transactions = new ObservableCollection<Transaction>(await SessionService.Instance.GetSessionTransactions());

            foreach (var item in Transactions)
                Sum += item.Product.Price;
        }
    }
}
