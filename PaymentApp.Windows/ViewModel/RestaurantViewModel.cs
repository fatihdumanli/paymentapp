using PaymentApp.Windows.Model;
using PaymentApp.Windows.Services;
using PaymentApp.Windows.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaymentApp.Windows.ViewModel
{
    public class RestaurantViewModel : ViewModelBase
    {

        private Command _cmdPay;

        public Command CmdPay
        {
            get
            {
                return _cmdPay ?? new Command(GoPayment);
            }
        }

        private void GoPayment()
        {
            IsPopupOpen = false;
            ((Frame)Window.Current.Content).Navigate(typeof(Payment));
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; base.RaisePropertyChanged(nameof(SelectedIndex)); }
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
            foreach (var item in OrderList)
            {
                await SessionService.Session.InsertTransaction(SessionService.Session.CustomerId, item.Id);
            }

            OrderList.Clear();
            Sum = 0;

            IsPopupOpen = false;
            SelectedIndex = 0;

            GetTransactions();


        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; base.RaisePropertyChanged(nameof(IsPopupOpen)); }
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
            Transactions = new ObservableCollection<Transaction>(await SessionService.Instance.GetSessionTransactions());
            IsThereAnyTransaction = Transactions.Count > 0;
        }


        private bool _isThereAnyTransaction;
        public bool IsThereAnyTransaction
        {
            get { return _isThereAnyTransaction; }
            set { _isThereAnyTransaction = value; base.RaisePropertyChanged(nameof(IsThereAnyTransaction)); }
        }

        private Command _cmdInit;
        public Command CmdInit
        {
            get { return _cmdInit = _cmdInit ?? new Command(Init); }
        }
        private Model.Restaurant _restaurantModel;
        public Model.Restaurant RestaurantModel
        {
            get { return _restaurantModel; }
            set { _restaurantModel = value; base.RaisePropertyChanged(nameof(RestaurantModel)); }
        }
        private bool _isMenuLoading;
        public bool IsMenuLoading
        {
            get { return _isMenuLoading; }
            set { _isMenuLoading = value; base.RaisePropertyChanged(nameof(IsMenuLoading)); }
        }

        private Command _cmdGetMenu;
        public Command CmdGetMenu
        {
            get { return _cmdGetMenu = _cmdGetMenu ?? new Command(GetMenu); }
        }


        private ObservableCollection<Product> _products;
        
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products = _products ?? new ObservableCollection<Product>();
            }

            set
            {
                _products = value;
                base.RaisePropertyChanged(nameof(Products));
            }
        }

        private IEnumerable<IGrouping<int, Product>> _groups;

        public IEnumerable<IGrouping<int, Product>> Groups
        {
            get
            {
                return _groups;
            }

            set
            {
                _groups = value;
                base.RaisePropertyChanged(nameof(Groups));
            }
        }

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

        private ObservableCollection<Product> _orderList;
        public ObservableCollection<Product> OrderList
        {
            get
            {
                return _orderList = _orderList ?? new ObservableCollection<Product>();
            }

            set
            {
                _orderList = value;
                base.RaisePropertyChanged(nameof(OrderList));
            }
        }


        private Command<Product> _cmdAddProductToList;

        public Command<Product> CmdAddProductToList
        {
            get
            {
                return _cmdAddProductToList = _cmdAddProductToList ?? new Command<Product>(AddProductToList);
            }
        }
        private void AddProductToList(Product obj)
        {
            OrderList.Add(obj);
            Sum += obj.Price;
        }

        private Command<List<Product>> _cmdConfirmTheOrder;

        public Command<List<Product>> CmdConfirmTheOrder
        {
            get
            {
                return _cmdConfirmTheOrder = _cmdConfirmTheOrder ?? new Command<List<Product>>(ConfirmTheOrder);
            }
        }


        private void ConfirmTheOrder(List<Product> obj)
        {

            IsPopupOpen = !IsPopupOpen;

            //List<Product> _orderList = new List<Product>(OrderList);
            //Tuple<Session, List<Product>> param = new Tuple<Session, List<Product>>(SessionService.Session, _orderList);

            //((Frame)Window.Current.Content).Navigate(typeof(Order), param);
        }


        private async void GetMenu()
        {
            IsMenuLoading = true;
            //get the menu
            _products = new ObservableCollection<Product>(await RestaurantService.Instance
                .GetRestaurantProducts(Convert.ToInt32(SessionService.Session.RestaurantId)));

            var groups = from c in _products
                         group c by c.CatId;

            Groups = groups;
            IsMenuLoading = false;
        }

        public RestaurantViewModel()
        {
        }

        private async void Init()
        {
            


            RestaurantModel = await RestaurantService.Instance.
                GetRestaurantDetails(Convert.ToInt32(SessionService.Session.RestaurantId));
        }
      
    }
}
