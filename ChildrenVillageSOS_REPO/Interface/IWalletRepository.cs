using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IWalletRepository
    {
        Task UpdateFacilitiesWalletBudget(int walletId, decimal amount);
        Task UpdateFoodStuffWalletBudget(int walletId, decimal amount);
        Task UpdateHealthWalletBudget(int walletId, decimal amount);
        Task UpdateNecessitiesWalletBudget(int walletId, decimal amount);

        Task UpdateSystemWalletBudget(int walletId, decimal amount);

    }
}
