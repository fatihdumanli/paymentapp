using PaymentApp.Windows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Services
{
    public interface IAccountService
    {
        Task<string> Register(Customer c);
        Task<bool> Login(string email, string password);
        Task<Customer> GetCustomerByEmailAddress(string email);
    }
}
