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
    public class HouseRepository : RepositoryGeneric<House>, IHouseRepository
    {
        public HouseRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<string?> GetUserAccountIdByHouseId(string houseId)
        {
            var house = await _context.Houses
                .Where(h => h.Id == houseId && (h.IsDeleted == false || h.IsDeleted == null))
                .Select(h => h.UserAccountId)
                .FirstOrDefaultAsync();

            return house;
        }
        public async Task<List<House>> GetHouseByVillageIdAsync(string villageId)
        {
            return await _context.Houses
                .Where(h => h.VillageId == villageId && (h.IsDeleted == null || h.IsDeleted == false))
                .ToListAsync();
        }
    }
}
