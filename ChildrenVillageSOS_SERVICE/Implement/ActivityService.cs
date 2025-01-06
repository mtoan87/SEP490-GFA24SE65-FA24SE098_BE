using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
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
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public ActivityService(IActivityRepository activityRepository,
            IImageService imageService,
            IImageRepository imageRepository)
        {
            _activityRepository = activityRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<Activity>> GetAllActivities()
        {
            return await _activityRepository.GetAllNotDeletedAsync();
        }

        public Task<ActivityResponseDTO[]> GetAllActivityIsDeleteAsync()
        {
            return _activityRepository.GetAllActivityIsDeleteAsync();
        }

        public async Task<IEnumerable<ActivityResponseDTO>> GetAllActivityWithImg()
        {
            var activities = await _activityRepository.GetAllNotDeletedAsync();

            var activityResponseDTOs = activities.Select(a => new ActivityResponseDTO
            {
                Id = a.Id,
                ActivityName = a.ActivityName,
                Description = a.Description,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Address = a.Address,
                VillageId = a.VillageId,
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
            }).ToArray();

            return activityResponseDTOs;
        }

        public async Task<Activity> GetActivityById(int id)
        {
            return await _activityRepository.GetByIdAsync(id);
        }

        public async Task<ActivityResponseDTO> GetActivityByIdWithImg(int activityId)
        {
            return _activityRepository.GetActivityByIdWithImg(activityId);
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
                VillageId = createActivity.VillageId,
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

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadActivityImage(createActivity.Img, newActivity.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    ActivityId = newActivity.Id, // Liên kết với Activity
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }

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
            existingActivity.VillageId = updateActivity.VillageId;
            existingActivity.ActivityType = updateActivity.ActivityType;
            existingActivity.TargetAudience = updateActivity.TargetAudience;
            existingActivity.Organizer = updateActivity.Organizer;
            existingActivity.Status = updateActivity.Status;
            existingActivity.EventId = updateActivity.EventId;
            existingActivity.Budget = updateActivity.Budget;
            existingActivity.Feedback = updateActivity.Feedback;
            existingActivity.ModifiedBy = updateActivity.ModifiedBy;
            existingActivity.ModifiedDate = DateTime.Now;

            // Lấy danh sách ảnh hiện tại
            var existingImages = await _imageRepository.GetByActivityIdAsync(existingActivity.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateActivity.ImgToDelete != null && updateActivity.ImgToDelete.Any())
            {
                foreach (var imageUrlToDelete in updateActivity.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageUrlToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "ActivityImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateActivity.Img != null && updateActivity.Img.Any())
            {
                var newImageUrls = await _imageService.UploadActivityImage(updateActivity.Img, existingActivity.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        ActivityId = existingActivity.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

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
