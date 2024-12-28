using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory> GetInventoryById(int id);
        Task<Inventory> CreateInventory(CreateInventoryDTO createInventory);
        Task<Inventory> UpdateInventory(int id, UpdateInventoryDTO updateInventory);
        Task<Inventory> DeleteInventory(int id);
        Task<Inventory> RestoreInventory(int id);
        Task<IEnumerable<InventoryResponseDTO>> GetAllInventoryWithImg();
    }
}
