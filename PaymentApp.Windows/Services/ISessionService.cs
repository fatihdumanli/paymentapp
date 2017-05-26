using PaymentApp.Windows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApp.Windows.Services
{
    public interface ISessionService
    {
        Task<Session> StartSession(int customerId, int restaurantId, int tableId, int slot);
        Task<Transaction> AddTransaction(Transaction t);
        Task<List<Transaction>> GetSessionTransactions();
        Task Pay();
    }
}
