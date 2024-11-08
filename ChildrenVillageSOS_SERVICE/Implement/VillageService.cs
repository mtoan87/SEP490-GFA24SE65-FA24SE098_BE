using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
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
    public class VillageService : IVillageService
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public VillageService(IVillageRepository villageRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _villageRepository = villageRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }
        public async Task<IEnumerable<Village>> GetAllVillage()
        {
            return await _villageRepository.GetAllAsync();
        }
        public async Task<Village> GetVillageById(string villageId)
        {
            return await _villageRepository.GetByIdAsync(villageId);
        }
        public async Task<Village> CreateVillage(CreateVillageDTO createVillage)
        {
            var allVillageId = await _villageRepository.Entities().Select(v => v.Id).ToListAsync();
            string newVillageId = IdGenerator.GenerateId(allVillageId, "V");
            var newVillage = new Village
            {
                Id = newVillageId,
                VillageName = createVillage.VillageName,
                Location = createVillage.Location,
                Description = createVillage.Description,
                Status = createVillage.Status,
                UserAccountId = createVillage.UserAccountId,
            };
            await _villageRepository.AddAsync(newVillage);

            string url = await _imageService.UploadVillageImage(createVillage.Img, newVillage.Id);
            var image = new Image
            {
                UrlPath = url,
                VillageId = newVillage.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
            return newVillage;
        }
        public async Task<Village> UpdateVillage(string villageId, UpdateVillageDTO updateVillage)
        {
            var updaVillage = await _villageRepository.GetByIdAsync(villageId);
            if (updaVillage == null)
            {
                throw new Exception($"Expense with ID{villageId} not found!");
            }
            updaVillage.VillageName = updateVillage.VillageName;

            updaVillage.Location = updateVillage.Location;
            updaVillage.Description = updateVillage.Description;
            updaVillage.Status = updateVillage.Status;
            updaVillage.UserAccountId = updateVillage.UserAccountId;

            if (updateVillage.Img != null)
            {
                var existingImage = await _imageRepository.GetByVillageIdAsync(updaVillage.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "VillageImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadVillageImage(updateVillage.Img, updaVillage.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.UtcNow;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadVillageImage(updateVillage.Img, updaVillage.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        VillageId = updaVillage.Id,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _villageRepository.UpdateAsync(updaVillage);
            return updaVillage;

        }
        public async Task<Village> DeleteVillage(string villageId)
        {
            var vil = await _villageRepository.GetByIdAsync(villageId);
            if (vil == null)
            {
                throw new Exception($"Village with ID{villageId} not found");
            }
            await _villageRepository.RemoveAsync(vil);
            return vil;
        }
    }
}
