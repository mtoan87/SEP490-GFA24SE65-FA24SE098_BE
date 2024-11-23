using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class SystemWalletService : ISystemWalletService
    {
        private readonly ISystemWalletRepository _repo;
        public SystemWalletService(ISystemWalletRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<SystemWallet>> GetSystemWallets()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<SystemWallet> GetWalletById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public SystemWallet[] GetSystemWalletsToArray()
        {
            return _repo.GetSystemWalletsArray();
        }
        public async Task<SystemWallet> CreateWallet(CreateSystemWalletDTO createPayment)
        {
            var newPayment = new SystemWallet
            {
                Budget = createPayment.Budget,
                UserAccountId = createPayment.UserAccountId,
               
            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<SystemWallet> UpdateWalet(int id, UpdateSystemWalletDTO updatePayment)
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
        public async Task<SystemWallet> DeleteWallet(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"Payment with ID{id} not found");
            }
            await _repo.RemoveAsync(pay);
            return pay;
        }
        public async Task<decimal> GetTotalBudget()
        {
            var facilitiesWallets = await _repo.GetAllAsync();
            return facilitiesWallets.Sum(fw => fw.Budget);
        }
    }
}
