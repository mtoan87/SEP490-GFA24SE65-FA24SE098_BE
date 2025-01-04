using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
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
    public class ActivityRepository : RepositoryGeneric<Activity>, IActivityRepository
    {
        public ActivityRepository(SoschildrenVillageDbContext dbContext) : base(dbContext) 
        { 

        }

        public async Task<IEnumerable<Activity>> GetAllNotDeletedAsync()
        {
            return await _context.Activities
                                 .Include(e => e.Images)
                                 .Where(e => !e.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<ActivityResponseDTO[]> GetAllActivityIsDeleteAsync()
        {
            return await _context.Activities
                .Where(a => a.IsDeleted)
                .Select(a => new ActivityResponseDTO
                {
                    Id = a.Id,
                    ActivityName = a.ActivityName,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Address = a.Address,
                    LocationId = a.VillageId,
                    ActivityType = a.ActivityType,
                    TargetAudience = a.TargetAudience,
                    Organizer = a.Organizer,
                    Status = a.Status,
                    EventId = a.EventId,
                    Budget = a.Budget,
                    Feedback = a.Feedback,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedDate = a.CreatedDate,
                    ModifiedBy = a.ModifiedBy,
                    ModifiedDate = a.ModifiedDate,
                    ImageUrls = a.Images
                                .Where(img => !img.IsDeleted)
                                .Select(img => img.UrlPath)
                                .ToArray()
                })
                .ToArrayAsync();
        }

        public ActivityResponseDTO GetActivityByIdWithImg(int activityId)
        {
            var activityDetails = _context.Activities
                .Where(a => a.Id == activityId && !a.IsDeleted) // Lọc Activity chưa bị xóa
                .Select(a => new ActivityResponseDTO
                {
                    Id = a.Id,
                    ActivityName = a.ActivityName,
                    Description = a.Description,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Address = a.Address,
                    LocationId = a.VillageId,
                    ActivityType = a.ActivityType,
                    TargetAudience = a.TargetAudience,
                    Organizer = a.Organizer,
                    Status = a.Status,
                    EventId = a.EventId,
                    Budget = a.Budget,
                    Feedback = a.Feedback,
                    IsDeleted = a.IsDeleted,
                    CreatedBy = a.CreatedBy,
                    CreatedDate = a.CreatedDate,
                    ModifiedBy = a.ModifiedBy,
                    ModifiedDate = a.ModifiedDate,
                    ImageUrls = a.Images
                                .Where(img => !img.IsDeleted)
                                .Select(img => img.UrlPath)
                                .ToArray()
                })
                .FirstOrDefault();

            return activityDetails;
        }
    }
}
