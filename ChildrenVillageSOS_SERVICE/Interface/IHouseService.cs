using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetAllHouses();
        Task<House> GetHouseById(string id);
        Task<House> CreateHouse(CreateHouseDTO createHouse);
        Task<House> UpdateHouse(string id, UpdateHouseDTO updateHouse);
        Task<House> DeleteHouse(string id);
        Task<House> RestoreHouse(string id);
        Task<string?> GetUserAccountIdByHouseId(string houseId);
        Task<List<House>> getHouseByVillageId(string villageId);
    }
}
