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
    public class HealthWalletRepository : RepositoryGeneric<HealthWallet>, IHealthWalletRepository
    {
        public HealthWalletRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
        public async Task<HealthWallet> GetHealthWalletByUserIdAsync(string userAccountId)
        {
            // Use Entity Framework to query the FacilitiesWallet by UserAccountId
            return await _context.HealthWallets
                //.Include(fw => fw.Transactions) // Include related Transactions if needed
                .FirstOrDefaultAsync(fw => fw.UserAccountId == userAccountId);
        }
        public HealthWallet[] GetHealthWalletsArray()
        {
            return _context.HealthWallets.ToArray();
        }

        public async Task<decimal> GetWalletBudgetByUserIdAsync(string userAccountId)
        {
            return await _context.HealthWallets
                .Where(w => w.UserAccountId == userAccountId)
                .Select(w => w.Budget)
                .FirstOrDefaultAsync();
        }
    }
}
