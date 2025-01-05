using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class SystemWalletRepository : RepositoryGeneric<SystemWallet> , ISystemWalletRepository
    {
        public SystemWalletRepository(SoschildrenVillageDbContext context) : base(context)
        {           
        }
        public async Task<SystemWallet> GetWalletByUserIdAsync(string userAccountId)
        {
            // Use Entity Framework to query the FacilitiesWallet by UserAccountId
            return await _context.SystemWallets
                //.Include(fw => fw.Transactions) // Include related Transactions if needed
                .FirstOrDefaultAsync(fw => fw.UserAccountId == userAccountId);
        }
        public SystemWallet[] GetSystemWalletsArray()
        {
            return _context.SystemWallets.ToArray();
        }
        public async Task<decimal> GetWalletBudgetByUserIdAsync(string userAccountId)
        {
            return await _context.FoodStuffWallets
                .Where(w => w.UserAccountId == userAccountId)
                .Select(w => w.Budget)
                .FirstOrDefaultAsync();
        }
    }
}
