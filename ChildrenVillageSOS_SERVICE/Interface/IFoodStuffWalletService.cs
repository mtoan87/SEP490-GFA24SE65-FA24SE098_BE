using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IFoodStuffWalletService
    {
        Task<IEnumerable<FoodStuffWallet>> GetFoodWallets();
        Task<FoodStuffWallet> GetFoodWalletById(int id);
        Task<FoodStuffWallet> CreateFoodWallet(CreateFoodWalletDTO createPayment);
        Task<FoodStuffWallet> UpdateFoodWalet(int id, UpdateFoodWalletDTO updatePayment);
        Task<FoodStuffWallet> DeleteFoodWallet(int id);
        Task<decimal> GetTotalBudget();
    }
}
