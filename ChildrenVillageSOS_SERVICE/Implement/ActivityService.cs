using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<IEnumerable<Activity>> GetAllActivities()
        {
            return await _activityRepository.GetAllNotDeletedAsync();
        }

        public async Task<Activity> GetActivityById(int id)
        {
            return await _activityRepository.GetByIdAsync(id);
        }

        public async Task<Activity> CreateActivity(CreateActivityDTO createActivity)
        {
            var newActivity = new Activity
            {
                ActivityName = createActivity.ActivityName,
                Description = createActivity.Description,
                StartDate = createActivity.StartDate,
                EndDate = createActivity.EndDate,
                Address = createActivity.Address,
                LocationId = createActivity.LocationId,
                ActivityType = createActivity.ActivityType,
                TargetAudience = createActivity.TargetAudience,
                Organizer = createActivity.Organizer,
                Status = createActivity.Status,
                EventId = createActivity.EventId,
                Budget = createActivity.Budget,
                Feedback = createActivity.Feedback,
                CreatedBy = createActivity.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _activityRepository.AddAsync(newActivity);
            return newActivity;
        }

        public async Task<Activity> UpdateActivity(int id, UpdateActivityDTO updateActivity)
        {
            var existingActivity = await _activityRepository.GetByIdAsync(id);
            if (existingActivity == null)
            {
                throw new Exception($"Activity with ID {id} not found!");
            }

            existingActivity.ActivityName = updateActivity.ActivityName;
            existingActivity.Description = updateActivity.Description;
            existingActivity.StartDate = updateActivity.StartDate;
            existingActivity.EndDate = updateActivity.EndDate;
            existingActivity.Address = updateActivity.Address;
            existingActivity.LocationId = updateActivity.LocationId;
            existingActivity.ActivityType = updateActivity.ActivityType;
            existingActivity.TargetAudience = updateActivity.TargetAudience;
            existingActivity.Organizer = updateActivity.Organizer;
            existingActivity.Status = updateActivity.Status;
            existingActivity.EventId = updateActivity.EventId;
            existingActivity.Budget = updateActivity.Budget;
            existingActivity.Feedback = updateActivity.Feedback;
            existingActivity.ModifiedBy = updateActivity.ModifiedBy;
            existingActivity.ModifiedDate = DateTime.Now;

            await _activityRepository.UpdateAsync(existingActivity);
            return existingActivity;
        }

        public async Task<Activity> DeleteActivity(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                throw new Exception($"Activity with ID {id} not found");
            }

            if (activity.IsDeleted)
            {
                // Hard delete
                await _activityRepository.RemoveAsync(activity);
            }
            else
            {
                // Soft delete (đặt IsDeleted = true)
                activity.IsDeleted = true;
                await _activityRepository.UpdateAsync(activity);
            }

            return activity;
        }

        public async Task<Activity> RestoreActivity(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                throw new Exception($"Activity with ID {id} not found");
            }

            if (activity.IsDeleted)
            {
                activity.IsDeleted = false;
                await _activityRepository.UpdateAsync(activity);
            }

            return activity;
        }
    }
}
