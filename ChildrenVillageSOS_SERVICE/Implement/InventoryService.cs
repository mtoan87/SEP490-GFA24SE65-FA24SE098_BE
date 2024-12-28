using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public InventoryService(IInventoryRepository inventoryRepository,
            IImageService imageService,
            IImageRepository imageRepository)
        {
            _inventoryRepository = inventoryRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await _inventoryRepository.GetAllNotDeletedAsync();
        }

        public Task<InventoryResponseDTO[]> GetAllInventoryIsDeleteAsync()
        {
            return _inventoryRepository.GetAllInventoryIsDeleteAsync();
        }

        public async Task<IEnumerable<InventoryResponseDTO>> GetAllInventoryWithImg()
        {
            var inventories = await _inventoryRepository.GetAllNotDeletedAsync();

            var inventoryResponseDTOs = inventories.Select(i => new InventoryResponseDTO
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Description = i.Description,
                Quantity = i.Quantity,
                Purpose = i.Purpose,
                BelongsTo = i.BelongsTo,
                BelongsToId = i.BelongsToId,
                PurchaseDate = i.PurchaseDate,
                LastInspectionDate = i.LastInspectionDate,
                MaintenanceStatus = i.MaintenanceStatus,
                IsDeleted = i.IsDeleted,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate,
                ModifiedBy = i.ModifiedBy,
                ModifiedDate = i.ModifiedDate,               
                ImageUrls = i.Images.Where(img => !img.IsDeleted)
                                     .Select(img => img.UrlPath)
                                     .ToArray()
            }).ToArray();

            return inventoryResponseDTOs;
        }

        public async Task<Inventory> GetInventoryById(int id)
        {
            return await _inventoryRepository.GetByIdAsync(id);
        }

        public async Task<InventoryResponseDTO> GetInventoryByIdWithImg(int inventoryId)
        {
            return _inventoryRepository.GetInventoryByIdWithImg(inventoryId);
        }

        public async Task<Inventory> CreateInventory(CreateInventoryDTO createInventory)
        {
            var newInventory = new Inventory
            {
                ItemName = createInventory.ItemName,
                Description = createInventory.Description,
                Quantity = createInventory.Quantity,
                Purpose = createInventory.Purpose,
                BelongsTo = createInventory.BelongsTo,
                BelongsToId = createInventory.BelongsToId,
                PurchaseDate = createInventory.PurchaseDate,
                LastInspectionDate = createInventory.LastInspectionDate,
                MaintenanceStatus = createInventory.MaintenanceStatus,
                IsDeleted = false,
                CreatedBy = createInventory.CreatedBy,
                CreatedDate = DateTime.Now               
            };

            await _inventoryRepository.AddAsync(newInventory);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadInventoryImage(createInventory.Img, newInventory.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    InventoryId = newInventory.Id, // Liên kết với Inventory
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }

            return newInventory;
        }

        public async Task<Inventory> UpdateInventory(int id, UpdateInventoryDTO updateInventory)
        {
            var existingInventory = await _inventoryRepository.GetByIdAsync(id);
            if (existingInventory == null)
            {
                throw new Exception($"Inventory with ID {id} not found!");
            }

            existingInventory.ItemName = updateInventory.ItemName;
            existingInventory.Description = updateInventory.Description;
            existingInventory.Quantity = updateInventory.Quantity;
            existingInventory.Purpose = updateInventory.Purpose;
            existingInventory.BelongsTo = updateInventory.BelongsTo;
            existingInventory.BelongsToId = updateInventory.BelongsToId;
            existingInventory.PurchaseDate = updateInventory.PurchaseDate;
            existingInventory.LastInspectionDate = updateInventory.LastInspectionDate;
            existingInventory.MaintenanceStatus = updateInventory.MaintenanceStatus;
            existingInventory.ModifiedBy = updateInventory.ModifiedBy;
            existingInventory.ModifiedDate = DateTime.Now;

            // Lấy danh sách ảnh hiện tại
            var existingImages = await _imageRepository.GetByInventoryIdAsync(existingInventory.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateInventory.ImgToDelete != null && updateInventory.ImgToDelete.Any())
            {
                foreach (var imageUrlToDelete in updateInventory.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageUrlToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "InventoryImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateInventory.Img != null && updateInventory.Img.Any())
            {
                var newImageUrls = await _imageService.UploadInventoryImage(updateInventory.Img, existingInventory.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        InventoryId = existingInventory.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _inventoryRepository.UpdateAsync(existingInventory);
            return existingInventory;
        }

        public async Task<Inventory> DeleteInventory(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
            {
                throw new Exception($"Inventory with ID {id} not found");
            }

            if (inventory.IsDeleted)
            {
                // Hard delete
                await _inventoryRepository.RemoveAsync(inventory);
            }
            else
            {
                // Soft delete
                inventory.IsDeleted = true;
                await _inventoryRepository.UpdateAsync(inventory);
            }

            return inventory;
        }

        public async Task<Inventory> RestoreInventory(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
            {
                throw new Exception($"Inventory with ID {id} not found");
            }

            if (inventory.IsDeleted)
            {
                inventory.IsDeleted = false;
                await _inventoryRepository.UpdateAsync(inventory);
            }

            return inventory;
        }
    }
}
