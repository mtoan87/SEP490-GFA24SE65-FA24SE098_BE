using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
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
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public HouseService(IHouseRepository houseRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _houseRepository = houseRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<House>> GetAllHouses()
        {
            // Chỉ lấy những nhà chưa bị soft delete (IsDeleted = false)
            return await _houseRepository.GetAllNotDeletedAsync();
        }

        public async Task<IEnumerable<HouseResponseDTO>> GetAllHousesWithImg()
        {
            // Lấy tất cả các House không bị xóa
            var houses = await _houseRepository.GetAllNotDeletedAsync();

            // Chuyển đổi dữ liệu thành HouseResponseDTO
            var houseResponseDTOs = houses.Select(house => new HouseResponseDTO
            {
                HouseId = house.Id,
                HouseName = house.HouseName,
                HouseNumber = house.HouseNumber,
                Location = house.Location,
                Description = house.Description,
                HouseMember = house.HouseMember,
                HouseOwner = house.HouseOwner,
                Status = house.Status,
                UserAccountId = house.UserAccountId,
                VillageId = house.VillageId,
                IsDeleted = house.IsDeleted,
                CreatedDate = house.CreatedDate,
                ModifiedDate = house.ModifiedDate,
                ImageUrls = house.Images.Where(img => !img.IsDeleted)  // Lọc các hình ảnh không bị xóa
                                        .Select(img => img.UrlPath)  // Lấy đường dẫn hình ảnh
                                        .ToArray()                  // Chuyển thành mảng
            }).ToArray();

            return houseResponseDTOs;
        }

        public async Task<House> GetHouseById(string id)
        {
            return await _houseRepository.GetByIdAsync(id);
        }

        public async Task<HouseResponseDTO> GetHouseByIdWithImg(string houseId)
        {
            return _houseRepository.GetHouseByIdWithImg(houseId);
        }

        public async Task<House> CreateHouse(CreateHouseDTO createHouse)
        {
            // Lấy toàn bộ danh sách HouseId hiện có
            var allHouseIds = await _houseRepository.Entities()
                                                    .Select(h => h.Id)
                                                    .ToListAsync();

            // Sử dụng hàm GenerateId từ IdGenerator
            string newHouseId = IdGenerator.GenerateId(allHouseIds, "H");

            var newHouse = new House
            {
                Id = newHouseId,
                HouseName = createHouse.HouseName,
                HouseNumber = createHouse.HouseNumber,
                Location = createHouse.Location,
                Description = createHouse.Description,
                HouseMember = createHouse.HouseMember,
                HouseOwner = createHouse.HouseOwner,
                Status = createHouse.Status,
                UserAccountId = createHouse.UserAccountId,
                VillageId = createHouse.VillageId,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };
            await _houseRepository.AddAsync(newHouse);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadHouseImage(createHouse.Img, newHouse.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    HouseId = newHouse.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
            return newHouse;
        }
        public async Task<HouseResponseDTO[]> getHouseByVillageId(string villageId)
        {
            return await _houseRepository.GetHouseByVillageIdAsync(villageId); // Await the async call
        }

        public async Task<string?> GetUserAccountIdByHouseId(string houseId)
        {
            return await _houseRepository.GetUserAccountIdByHouseId(houseId);
        }
        public async Task<House> UpdateHouse(string id, UpdateHouseDTO updateHouse)
        {
            var existingHouse = await _houseRepository.GetByIdAsync(id);
            if (existingHouse == null)
            {
                throw new Exception($"House with ID {id} not found!");
            }

            existingHouse.HouseName = updateHouse.HouseName;
            existingHouse.HouseNumber = updateHouse.HouseNumber;
            existingHouse.Location = updateHouse.Location;
            existingHouse.Description = updateHouse.Description;
            existingHouse.HouseMember = updateHouse.HouseMember;
            existingHouse.HouseOwner = updateHouse.HouseOwner;
            existingHouse.Status = updateHouse.Status;
            existingHouse.UserAccountId = updateHouse.UserAccountId;
            existingHouse.VillageId = updateHouse.VillageId;
            existingHouse.ModifiedDate = DateTime.Now;

            var existingImages = await _imageRepository.GetByHouseIdAsync(existingHouse.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateHouse.ImgToDelete != null && updateHouse.ImgToDelete.Any())
            {
                foreach (var imageIdToDelete in updateHouse.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "HouseImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateHouse.Img != null && updateHouse.Img.Any())
            {
                var newImageUrls = await _imageService.UploadHouseImage(updateHouse.Img, existingHouse.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        HouseId = existingHouse.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            // Lưu thông tin cập nhật
            await _houseRepository.UpdateAsync(existingHouse);
            return existingHouse;
        }

        public async Task<House> DeleteHouse(string id)
        {
            var house = await _houseRepository.GetByIdAsync(id);
            if (house == null)
            {
                throw new Exception($"House with ID {id} not found!");
            }

            if (house.IsDeleted == true)
            {
                // Hard delete nếu IsDeleted = true
                await _houseRepository.RemoveAsync(house);
            }
            else
            {
                // Soft delete (IsDeleted = true)
                house.IsDeleted = true;
                await _houseRepository.UpdateAsync(house);
            }
            return house;
        }

        public async Task<House> RestoreHouse(string id)
        {
            var house = await _houseRepository.GetByIdAsync(id);
            if (house == null)
            {
                throw new Exception($"House with ID {id} not found!");
            }

            if (house.IsDeleted == true) // Nếu đã bị soft delete
            {
                house.IsDeleted = false;  // Khôi phục bằng cách đặt lại IsDeleted = false
                await _houseRepository.UpdateAsync(house);
            }
            return house;
        }
        public Task<HouseResponseDTO[]> GetAllHouseAsync()
        {
            return _houseRepository.GetAllHouseAsync();
        }

        public async Task<string> GetHouseNameByIdAsync(string houseId)
        {
            return await _houseRepository.GetHouseNameByIdAsync(houseId);
        }
    }
}
