using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
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
    public class NecessitiesWalletService : INecessitiesWalletService
    {
        private readonly INecessitiesWalletRepository _repo;
        public NecessitiesWalletService(INecessitiesWalletRepository repo)
        {
            repo = _repo;
        }
        public async Task<IEnumerable<NecessitiesWallet>> GetNecessitiesWallets()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<NecessitiesWallet> GetNecessitiesWalletById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<decimal> GetTotalBudget()
        {
            var facilitiesWallets = await _repo.GetAllAsync();
            return facilitiesWallets.Sum(fw => fw.Budget);
        }
        public async Task<NecessitiesWallet> CreateNecessitiesWallet(CreateHealthWalletDTO createPayment)
        {
            var newPayment = new NecessitiesWallet
            {
                Budget = createPayment.Budget,
                UserAccountId = createPayment.UserAccountId,

            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<NecessitiesWallet> UpdateNecessitiesWalet(int id, UpdateHealthWalletDTO updatePayment)
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
        public async Task<NecessitiesWallet> DeleteNecessitiesWallet(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"NecessitiesWallet with ID{id} not found");
            }
            await _repo.RemoveAsync(pay);
            return pay;
        }
    }
}
