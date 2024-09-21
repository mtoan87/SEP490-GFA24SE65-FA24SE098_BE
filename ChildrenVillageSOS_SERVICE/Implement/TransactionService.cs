using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.DTO.TransactionDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Transaction> CreateTransaction(CreateTransactionDTO createPayment)
        {
            var newPayment = new Transaction
            {
                SystemWalletId = createPayment.SystemWalletId,
                Amount = createPayment.Amount,
                DateTime = DateTime.Now,
                Status = createPayment.Status,
                DonationId = createPayment?.DonationId,
                IncomeId    = createPayment?.IncomeId,

            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<Transaction> UpdateTransaction(int id, UpdateTransactionDTO updatePayment)
        {
            var updaPayment = await _repo.GetByIdAsync(id);
            if (updaPayment == null)
            {
                throw new Exception($"Wallet with ID{id} not found!");
            }
           

            updaPayment.Amount = updatePayment.Amount;
            updaPayment.Status = updaPayment.Status;
            

            await _repo.UpdateAsync(updaPayment);
            return updaPayment;

        }
        public async Task<Transaction> DeleteTransaction(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"Transaction with ID{id} not found");
            }
            await _repo.RemoveAsync(pay);
            return pay;
        }
    }
}
