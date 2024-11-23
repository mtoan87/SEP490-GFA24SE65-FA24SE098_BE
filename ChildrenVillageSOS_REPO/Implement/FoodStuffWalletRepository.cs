using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
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
    public class FoodStuffWalletRepository : RepositoryGeneric<FoodStuffWallet>, IFoodStuffWalletRepository
    {
        public FoodStuffWalletRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<FoodStuffWallet> GetWalletByUserIdAsync(string userAccountId)
        {
            // Use Entity Framework to query the FacilitiesWallet by UserAccountId
            return await _context.FoodStuffWallets
                //.Include(fw => fw.Transactions) // Include related Transactions if needed
                .FirstOrDefaultAsync(fw => fw.UserAccountId == userAccountId);
        }
        public FoodWalletReponseDTO[] GetAllToArray()
        {
            var foodstuff = _context.FoodStuffWallets
                .Select(f => new FoodWalletReponseDTO
                {
                    Id = f.Id,
                    UserAccountId = f.UserAccountId,
                    Budget = f.Budget,
                }).ToArray();
            return foodstuff;
        }
    }
}
