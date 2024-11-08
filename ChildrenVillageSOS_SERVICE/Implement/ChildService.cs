using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ChildService : IChildService
    {
        private readonly IChildRepository _childRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public ChildService(IChildRepository childRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _childRepository = childRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        //public async Task<IEnumerable<Child>> GetAllChildren()
        //{
        //    return await _childRepository.GetAllAsync();
        //}

        public async Task<IEnumerable<Child>> GetAllChildren()
        {
            return await _childRepository.GetAllNotDeletedAsync();
        }

        public async Task<Child> GetChildById(string id)
        {
            return await _childRepository.GetByIdAsync(id);
        }

        public async Task<Child> CreateChild(CreateChildDTO createChild)
        {
            // Lấy toàn bộ danh sách ChildId hiện có
            var allChildIds = await _childRepository.Entities()
                                                    .Select(c => c.Id)
                                                    .ToListAsync();

            // Sử dụng hàm GenerateId từ IdGenerator
            string newChildId = IdGenerator.GenerateId(allChildIds, "C");

            var newChild = new Child
            {
                Id = newChildId,  // Gán ID mới
                ChildName = createChild.ChildName,
                HealthStatus = createChild.HealthStatus,
                HouseId = createChild.HouseId,
                Gender = createChild.Gender,
                Dob = createChild.Dob,
                Status = createChild.Status,
                IsDeleted = createChild.IsDeleted
                
            };
            await _childRepository.AddAsync(newChild);

            string url = await _imageService.UploadChildImage(createChild.Img, newChild.Id);
            var image = new Image
            {
                UrlPath = url,
                ChildId = newChild.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
            return newChild;
        }

        public async Task<Child> UpdateChild(string id, UpdateChildDTO updateChild)
        {
            var existingChild = await _childRepository.GetByIdAsync(id);
            if (existingChild == null)
            {
                throw new Exception($"Child with ID {id} not found!");
            }

            existingChild.ChildName = updateChild.ChildName;
            existingChild.HealthStatus = updateChild.HealthStatus;
            existingChild.HouseId = updateChild.HouseId;
            existingChild.Gender = updateChild.Gender;
            existingChild.Dob = updateChild.Dob;
            existingChild.Status = updateChild.Status;
            existingChild.IsDeleted = updateChild.IsDeleted;

            if (updateChild.Img != null)
            {
                var existingImage = await _imageRepository.GetByChildIdAsync(existingChild.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "ChildImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadChildImage(updateChild.Img, existingChild.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.Now;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadChildImage(updateChild.Img, existingChild.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        ChildId = existingChild.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _childRepository.UpdateAsync(existingChild);
            return existingChild;
        }

        public async Task<Child> DeleteChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true)
            {
                // Hard delete
                await _childRepository.RemoveAsync(child);
            }
            else
            {
                // Soft delete (đặt IsDeleted = true)
                child.IsDeleted = true;
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }

        public async Task<Child> RestoreChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true) // Nếu đã bị soft delete
            {
                child.IsDeleted = false;  // Khôi phục bằng cách đặt lại IsDeleted = false
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }
    }
}
