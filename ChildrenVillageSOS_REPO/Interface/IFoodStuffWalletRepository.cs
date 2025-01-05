using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IFoodStuffWalletRepository : IRepositoryGeneric<FoodStuffWallet>
    {
        Task<FoodStuffWallet> GetWalletByUserIdAsync(string userAccountId);
        FoodWalletReponseDTO[] GetAllToArray();
        Task<decimal> GetWalletBudgetByUserIdAsync(string userAccountId);
    }
}
