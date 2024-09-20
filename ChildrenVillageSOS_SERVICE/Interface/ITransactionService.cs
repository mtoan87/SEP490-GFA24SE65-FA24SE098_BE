using ChildrenVillageSOS_DAL.DTO.TransactionDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<Transaction> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(CreateTransactionDTO createPayment);
        Task<Transaction> UpdateTransaction(int id, UpdateTransactionDTO updatePayment);
        Task<Transaction> DeleteTransaction(int id);
    }
}
