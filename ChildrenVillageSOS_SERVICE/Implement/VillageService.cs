using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable getVillage()
        {
            return _villageRepository.getVillage();
        }
        public  List<Village> GetVillagesDonatedByUser(string userAccountId)
        {
            return   _villageRepository.GetVillagesDonatedByUser(userAccountId);
        }

        public async Task<IEnumerable<Village>> GetAllVillage()
        {
            return await _villageRepository.GetAllNotDeletedAsync();
        }

        public Task<VillageResponseDTO[]> GetAllVillageIsDelete()
        {
            return _villageRepository.GetAllVillageIsDelete();
        }
        public Task<VillageResponseDTO[]> GetVillageByEventIDAsync(int eventId)
        {
            return _villageRepository.GetVillageByEventIDAsync(eventId);
        }

        public async Task<IEnumerable<VillageResponseDTO>> GetAllVillageWithImg()
        {
            // Lấy danh sách villages từ repository
            var villages = await _villageRepository.GetVillagesWithHousesAndChildrenAsync();

            // Chuyển đổi sang DTO
            var villageResponseDTOs = villages.Select(village => new VillageResponseDTO
            {
                Id = village.Id,
                VillageName = village.VillageName ?? string.Empty,
                EstablishedDate = village.EstablishedDate,
                TotalHouses = village.Houses.Count(h => !h.IsDeleted),
                TotalChildren = village.Houses
                        .SelectMany(h => h.Children)
                        .Count(c => !c.IsDeleted),
                ContactNumber = village.ContactNumber,
                Location = village.Location ?? string.Empty,
                Description = village.Description ?? string.Empty,
                Status = "Active",
                UserAccountId = village.UserAccountId,
                UserName = village.UserAccount?.UserName ?? "Unknown",
                CreatedBy = village.CreatedBy,
                ModifiedBy = village.ModifiedBy,
                IsDeleted = village.IsDeleted,
                CreatedDate = village.CreatedDate,
                ModifiedDate = village.ModifiedDate,
                ImageUrls = village.Images
                    .Where(img => !img.IsDeleted)
                    .Select(img => img.UrlPath)
                    .ToArray()
            }).ToArray();

            return villageResponseDTOs;
        }

        public async Task<VillageResponseDTO[]> SearchVillagesAsync(string searchTerm)
        {
            return await _villageRepository.SearchVillagesAsync(searchTerm);
        }

        public async Task<Village> GetVillageById(string villageId)
        {
            return await _villageRepository.GetByIdAsync(villageId);
        }

        public async Task<VillageDetailsDTO> GetVillageDetails(string villageId)
        {
            return await _villageRepository.GetVillageDetails(villageId);
        }

        public async Task<VillageResponseDTO> GetVillageByIdWithImg(string villageId)
        {
            return _villageRepository.GetVillageByIdWithImg(villageId);
        }

        public async Task<Village> CreateVillage(CreateVillageDTO createVillage)
        {
            var allVillageId = await _villageRepository.Entities().Select(v => v.Id).ToListAsync();
            string newVillageId = IdGenerator.GenerateId(allVillageId, "V");
            var newVillage = new Village
            {
                Id = newVillageId,
                VillageName = createVillage.VillageName,
                EstablishedDate = createVillage.EstablishedDate,
                Location = createVillage.Location,
                Description = createVillage.Description,
                Status = createVillage.Status,
                UserAccountId = createVillage.UserAccountId,
                CreatedBy = createVillage.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            await _villageRepository.AddAsync(newVillage);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadVillageImage(createVillage.Img, newVillage.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    VillageId = newVillage.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
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

            updaVillage.VillageName = updateVillage.VillageName;
            updaVillage.EstablishedDate = updateVillage.EstablishedDate;
            updaVillage.Location = updateVillage.Location;
            updaVillage.Description = updateVillage.Description;
            updaVillage.Status = updateVillage.Status;
            updaVillage.UserAccountId = updateVillage.UserAccountId;
            updaVillage.ContactNumber = updateVillage.ContactNumber;
            updaVillage.ModifiedBy = updateVillage.ModifiedBy;
            updaVillage.ModifiedDate = DateTime.Now;

            // Lấy danh sách ảnh hiện tại
            var existingImages = await _imageRepository.GetByVillageIdAsync(updaVillage.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateVillage.ImgToDelete != null && updateVillage.ImgToDelete.Any())
            {
                foreach (var imageIdToDelete in updateVillage.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "VillageImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateVillage.Img != null && updateVillage.Img.Any())
            {
                var newImageUrls = await _imageService.UploadVillageImage(updateVillage.Img, updaVillage.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        VillageId = updaVillage.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            // Lưu thông tin cập nhật
            await _villageRepository.UpdateAsync(updaVillage);
            return updaVillage;
        }

        public async Task<Village> DeleteVillage(string villageId)
        {
            var village = await _villageRepository.GetByIdAsync(villageId);
            if (village == null)
            {
                throw new Exception($"Village with ID {villageId} not found");
            }

            if (village.IsDeleted == true)
            {
                // Hard delete nếu đã bị soft delete
                await _villageRepository.RemoveAsync(village);
            }
            else
            {
                // Soft delete: đặt IsDeleted = true
                village.IsDeleted = true;
                await _villageRepository.UpdateAsync(village);
            }
            return village;
        }

        public async Task<Village> RestoreVillage(string villageId)
        {
            var village = await _villageRepository.GetByIdAsync(villageId);
            if (village == null)
            {
                throw new Exception($"Village with ID {villageId} not found");
            }

            if (village.IsDeleted == true) // Nếu đã bị soft delete
            {
                village.IsDeleted = false; // Khôi phục bằng cách đặt IsDeleted = false
                await _villageRepository.UpdateAsync(village);
            }
            return village;
        }

        public async Task<List<Village>> SearchVillages(SearchVillageDTO searchVillageDTO)
        {
            return await _villageRepository.SearchVillages(searchVillageDTO);
        }
    }
}
