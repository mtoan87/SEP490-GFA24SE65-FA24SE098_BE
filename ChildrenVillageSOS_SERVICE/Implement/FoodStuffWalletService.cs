using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
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
    public class FoodStuffWalletService : IFoodStuffWalletService
    {
        private readonly IFoodStuffWalletRepository _repo;
        public FoodStuffWalletService(IFoodStuffWalletRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<FoodStuffWallet>> GetFoodWallets()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<FoodStuffWallet> GetFoodWalletById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<FoodStuffWallet> CreateFoodWallet(CreateFoodWalletDTO createPayment)
        {
            var newPayment = new FoodStuffWallet
            {
                Budget = createPayment.Budget,
                UserAccountId = createPayment.UserAccountId,

            };
            await _repo.AddAsync(newPayment);
            return newPayment;
        }
        public async Task<FoodStuffWallet> UpdateFoodWalet(int id, UpdateFoodWalletDTO updatePayment)
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
        public async Task<FoodStuffWallet> DeleteFoodWallet(int id)
        {
            var pay = await _repo.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"Payment with ID{id} not found");
            }
            await _repo.RemoveAsync(pay);
            return pay;
        }
    }
}
