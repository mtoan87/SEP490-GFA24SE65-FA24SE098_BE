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
    public class FacilitiesWalletRepository : RepositoryGeneric<FacilitiesWallet>, IFacilitiesWalletRepository
    {
        public FacilitiesWalletRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
        public async Task<FacilitiesWallet> GetFacilitiesWalletByUserIdAsync(string userAccountId)
        {
            // Use Entity Framework to query the FacilitiesWallet by UserAccountId
            return await _context.FacilitiesWallets
                //.Include(fw => fw.Transactions) // Include related Transactions if needed
                .FirstOrDefaultAsync(fw => fw.UserAccountId == userAccountId);
        }
    }
}
