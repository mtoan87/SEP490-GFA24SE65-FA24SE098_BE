using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface ISystemWalletRepository : IRepositoryGeneric<SystemWallet>
    {
        Task<SystemWallet> GetWalletByUserIdAsync(string userAccountId);
        SystemWallet[] GetSystemWalletsArray();
        Task<decimal> GetWalletBudgetByUserIdAsync(string userAccountId);
    }
}
