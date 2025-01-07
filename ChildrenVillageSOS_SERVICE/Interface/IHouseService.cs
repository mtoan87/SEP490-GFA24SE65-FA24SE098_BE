using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IHouseService
    {
        House? GetHouseByUserAccountId(string userAccountId);
        Task<IEnumerable<House>> GetAllHouses();
        Task<House> GetHouseById(string id);
        Task<House> CreateHouse(CreateHouseDTO createHouse);
        Task<House> UpdateHouse(string id, UpdateHouseDTO updateHouse);
        Task<House> DeleteHouse(string id);
        Task<string?> GetUserAccountIdByHouseId(string houseId);
        Task<HouseResponseDTO[]> getHouseByVillageId(string villageId);
        //Task<HouseResponseDTO[]> GetAllHouseAsync();
        Task<string> GetHouseNameByIdAsync(string houseId);
        Task<IEnumerable<HouseResponseDTO>> GetAllHousesWithImg();
        //Task<House> SoftDelete(string id);
        Task<House> RestoreHouse(string id);
        //Task<House> SoftRestoreHouse(string id);
        Task<HouseResponseDTO[]> GetAllHouseIsDeleteAsync();
        Task<HouseResponseDTO> GetHouseByIdWithImg(string houseId);
        DataTable getHouse();
        Task<HouseDetailsDTO> GetHouseDetails(string houseId);
        Task<List<House>> SearchHouses(SearchHouseDTO searchHouseDTO);
    }
}
