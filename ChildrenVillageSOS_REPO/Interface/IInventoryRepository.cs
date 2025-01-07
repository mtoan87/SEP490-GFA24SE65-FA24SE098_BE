using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IInventoryRepository : IRepositoryGeneric<Inventory>
    {
        Task<InventoryResponseDTO[]> GetAllInventoryIsDeleteAsync();
        InventoryResponseDTO GetInventoryByIdWithImg(int inventoryId);
        Task<List<Inventory>> SearchInventories(SearchInventoryDTO searchInventoryDTO);
    }
}
