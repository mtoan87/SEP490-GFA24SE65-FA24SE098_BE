using ChildrenVillageSOS_DAL.DTO.FacilitiesWalletDTO;
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
    public class FacilitiesWalletService : IFacilitiesWalletService
    {
        private readonly IFacilitiesWalletRepository _repo;
        public FacilitiesWalletService(IFacilitiesWalletRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<FacilitiesWallet>> GetFacilitiesWallets()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<FacilitiesWallet> GetFacilitiesWalletById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task<FacilitiesWallet> GetFacilitiesWalletByUserIdAsync(string userAccountId)
        {
          return await _repo.GetFacilitiesWalletByUserIdAsync(userAccountId);
        }

        public async Task<FacilitiesWallet> CreateFacilitiesWallet(CreateFacilitiesWalletDTO createPayment)
        {
            var newPayment = new FacilitiesWallet
            {
                Budget = createPayment.Budget,
                UserAccountId = createPayment.UserAccountId,

            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<FacilitiesWallet> UpdateFacilitiesWalet(int id, UpdateFacilitiesWalletDTO updatePayment)
        {
            var updaPayment = await _repo.GetByIdAsync(id);
            if (updaPayment == null)
            {
                throw new Exception($"FacilitiesWallet with ID{id} not found!");
            }
            updaPayment.Budget = updatePayment.Budget;

            updaPayment.UserAccountId = updatePayment.UserAccountId;

            await _repo.UpdateAsync(updaPayment);
            return updaPayment;

        }
        public async Task<FacilitiesWallet> DeleteFacilitiesWallet(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"FacilitiesWallet with ID{id} not found");
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
