using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
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
    public class HealthWalletService : IHealthWalletService
    {
        private readonly IHealthWalletRepository _repo;
        public HealthWalletService(IHealthWalletRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<HealthWallet>> GetHealthWallets()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<HealthWallet> GetHealthWalletById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public HealthWallet[] GetHealthWalletArray()
        {
            return _repo.GetHealthWalletsArray();
        }
        public async Task<decimal> GetTotalBudget()
        {
            var facilitiesWallets = await _repo.GetAllAsync();
            return facilitiesWallets.Sum(fw => fw.Budget);
        }
        public async Task<HealthWallet> CreateHealthWallet(CreateHealthWalletDTO createPayment)
        {
            var newPayment = new HealthWallet
            {
                Budget = createPayment.Budget,
                UserAccountId = createPayment.UserAccountId,

            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<HealthWallet> UpdateHealthWalet(int id, UpdateHealthWalletDTO updatePayment)
        {
            var updaPayment = await _repo.GetByIdAsync(id);
            if (updaPayment == null)
            {
                throw new Exception($"Wallet with ID{id} not found!");
            }
            updaPayment.Budget = updatePayment.Budget;

            updaPayment.UserAccountId = updatePayment.UserAccountId;

            await _repo.UpdateAsync(updaPayment);
            return updaPayment;

        }
        public async Task<HealthWallet> DeleteHealthWallet(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"HealthWallet with ID{id} not found");
            }
            await _repo.RemoveAsync(pay);
            return pay;
        }
    }
}
