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

        public async Task<List<Image>> GetByChildIdAsync(string childId)
        {
            return await _dbContext.Images
                .Where(img => img.ChildId == childId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Image>> GetByEventIdAsync(int eventId)
        {
            return await _dbContext.Images
                .Where(img => img.EventId == eventId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Image>> GetByHouseIdAsync(string houseId)
        {
            return await _dbContext.Images
                .Where(img => img.HouseId == houseId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Image>> GetByUserAccountIdAsync(string userAccountId)
        {
            return await _dbContext.Images
                .Where(img => img.UserAccountId == userAccountId && img.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<List<Image>> GetByVillageIdAsync(string villageId)
        {
            return await _dbContext.Images
                .Where(img => img.VillageId == villageId && img.IsDeleted == false)
                .ToListAsync();
        }
    }
}
