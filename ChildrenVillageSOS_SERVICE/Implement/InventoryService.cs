using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventories()
        {
            return await _inventoryRepository.GetAllNotDeletedAsync();
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
