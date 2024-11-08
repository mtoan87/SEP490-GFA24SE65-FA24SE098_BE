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
    public class ImageRepository : RepositoryGeneric<Image>, IImageRepository
    {
        private readonly SoschildrenVillageDbContext _dbContext;
        public ImageRepository(SoschildrenVillageDbContext context) : base(context)
        {
            this._dbContext = context;
        }

        public async Task<Image?> GetByChildIdAsync(string childId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.ChildId == childId && img.IsDeleted == false);
        }

        public async Task<Image?> GetByEventIdAsync(int eventId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.EventId == eventId && img.IsDeleted == false);
        }

        public async Task<Image?> GetByHouseIdAsync(string houseId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.HouseId == houseId && img.IsDeleted == false);
        }

        public async Task<Image?> GetByUserAccountIdAsync(string userAccountId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.UserAccountId == userAccountId && img.IsDeleted == false);
        }

        public async Task<Image?> GetByVillageIdAsync(string villageId)
        {
            return await _dbContext.Images
                .FirstOrDefaultAsync(img => img.VillageId == villageId && img.IsDeleted == false);
        }
    }
}
