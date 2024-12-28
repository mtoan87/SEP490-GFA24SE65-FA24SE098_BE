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
                .Select(img => new Image
                {
                    Id = img.Id,
                    UrlPath = img.UrlPath ?? string.Empty, // Handle potential null
                    EventId = img.EventId,
                    ModifiedDate = img.ModifiedDate,
                    IsDeleted = img.IsDeleted,
                })
                .ToListAsync();
        }

        public async Task<List<Image>> GetByInventoryIdAsync(int inventoryId)
        {
            return await _dbContext.Images
                .Where(img => img.InventoryId == inventoryId && img.IsDeleted == false)
                .Select(img => new Image
                {
                    Id = img.Id,
                    UrlPath = img.UrlPath ?? string.Empty, // Handle potential null
                    InventoryId = img.InventoryId,
                    ModifiedDate = img.ModifiedDate,
                    IsDeleted = img.IsDeleted,
                })
                .ToListAsync();
        }

        public async Task<List<Image>> GetByActivityIdAsync(int activityId)
        {
            return await _dbContext.Images
                .Where(img => img.ActivityId == activityId && img.IsDeleted == false)
                .Select(img => new Image
                {
                    Id = img.Id,
                    UrlPath = img.UrlPath ?? string.Empty, // Handle potential null
                    ActivityId = img.ActivityId,
                    ModifiedDate = img.ModifiedDate,
                    IsDeleted = img.IsDeleted,
                })
                .ToListAsync();
        }

        public async Task<List<Image>> GetByHealthReportIdAsync(int healthReportId)
        {
            return await _dbContext.Images
                .Where(img => img.HealthReportId == healthReportId && img.IsDeleted == false)
                .Select(img => new Image
                {
                    Id = img.Id,
                    UrlPath = img.UrlPath ?? string.Empty, // Handle potential null
                    HealthReportId = img.HealthReportId,
                    ModifiedDate = img.ModifiedDate,
                    IsDeleted = img.IsDeleted,
                })
                .ToListAsync();
        }

        public async Task<List<Image>> GetByAcademicReportIdAsync(int academicReportId)
        {
            return await _dbContext.Images
                .Where(img => img.AcademicReportId == academicReportId && img.IsDeleted == false)
                .Select(img => new Image
                {
                    Id = img.Id,
                    UrlPath = img.UrlPath ?? string.Empty, // Handle potential null
                    AcademicReportId = img.AcademicReportId,
                    ModifiedDate = img.ModifiedDate,
                    IsDeleted = img.IsDeleted,
                })
                .ToListAsync();
        }

        public async Task<List<Image>> GetBySchoolIdAsync(string schoolId)
        {
            return await _dbContext.Images
                .Where(img => img.HouseId == schoolId && img.IsDeleted == false)
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
