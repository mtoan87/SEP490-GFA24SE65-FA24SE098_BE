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
    public class ChildRepository : RepositoryGeneric<Child>, IChildRepository
    {
        public ChildRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
        public async Task<List<Child>> GetChildByHouseIdAsync(string houseId)
        {
            return await _context.Children
                .Where(c => c.HouseId == houseId && (c.IsDeleted == null || c.IsDeleted == false))
                .ToListAsync();
        }
    }
}
