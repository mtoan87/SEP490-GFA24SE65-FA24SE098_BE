using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class InventoryRepository : RepositoryGeneric<Inventory>, IInventoryRepository
    {
        public InventoryRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Inventory>> GetAllNotDeletedAsync()
        {
            return await _context.Inventories
                                 .Include(e => e.Images)
                                 .Where(e => !e.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<InventoryResponseDTO[]> GetAllInventoryIsDeleteAsync()
        {
            return await _context.Inventories
                .Where(i => i.IsDeleted)
                .Select(i => new InventoryResponseDTO
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
                    ImageUrls = i.Images
                                .Where(img => !img.IsDeleted)
                                .Select(img => img.UrlPath)
                                .ToArray()
                })
                .ToArrayAsync();
        }

        public InventoryResponseDTO GetInventoryByIdWithImg(int inventoryId)
        {
            var inventoryDetails = _context.Inventories
                .Where(i => i.Id == inventoryId && !i.IsDeleted) // Lọc Inventory chưa bị xóa
                .Select(i => new InventoryResponseDTO
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
                    ImageUrls = i.Images
                        .Where(img => !img.IsDeleted)
                        .Select(img => img.UrlPath)
                        .ToArray()
                })
                .FirstOrDefault();

            return inventoryDetails;
        }

        public async Task<List<Inventory>> SearchInventories(SearchInventoryDTO searchInventoryDTO)
        {
            var query = _context.Inventories.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchInventoryDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchInventoryDTO.SearchTerm) ||
                     x.ItemName.Contains(searchInventoryDTO.SearchTerm) ||
                     x.Quantity.ToString().Contains(searchInventoryDTO.SearchTerm) ||
                     x.Purpose.Contains(searchInventoryDTO.SearchTerm) ||
                     x.BelongsTo.Contains(searchInventoryDTO.SearchTerm) ||
                     x.BelongsToId.Contains(searchInventoryDTO.SearchTerm) ||
                     x.MaintenanceStatus.Contains(searchInventoryDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }

    }
}
