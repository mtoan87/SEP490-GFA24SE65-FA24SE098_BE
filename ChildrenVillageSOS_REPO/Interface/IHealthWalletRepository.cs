﻿using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IHealthWalletRepository : IRepositoryGeneric<HealthWallet>
    {
        Task<HealthWallet> GetHealthWalletByUserIdAsync(string userAccountId);
        HealthWallet[] GetHealthWalletsArray();
        Task<decimal> GetWalletBudgetByUserIdAsync(string userAccountId);
    }
}
