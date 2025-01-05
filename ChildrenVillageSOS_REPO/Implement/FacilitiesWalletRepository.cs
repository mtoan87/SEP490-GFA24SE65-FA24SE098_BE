using ChildrenVillageSOS_DAL.DTO.FacilitiesWalletDTO;
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
        public FacilitiesWalletResponseDTO[] GetAllToArray()
        {
            var facwallet = _context.FacilitiesWallets
                .Select(f => new FacilitiesWalletResponseDTO
                {
                    Id = f.Id,
                    UserAccountId = f.UserAccountId,
                    Budget = f.Budget,
                }).ToArray();
            return facwallet;
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
