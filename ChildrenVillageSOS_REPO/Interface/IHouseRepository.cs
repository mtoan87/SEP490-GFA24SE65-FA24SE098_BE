﻿using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IHouseRepository : IRepositoryGeneric<House>
    {
        Task<List<House>> GetHousesByIdsAsync(List<string> houseIds);
        House? GetHouseByUserAccountId(string userAccountId);
        Task<string?> GetUserAccountIdByHouseId(string houseId);
        HouseResponseDTO GetHouseByIdWithImg(string houseId);
        Task<HouseResponseDTO[]> GetHouseByVillageIdAsync(string villageId);
        Task<string> GetHouseNameByIdAsync(string houseId);
        HouseNameDTO[] GetHouseByUserIdWithImg(string userId);
        //Task<HouseResponseDTO[]> GetAllHouseAsync();
        Task<HouseResponseDTO[]> GetAllHouseIsDeleteAsync();
        Task<HouseResponseDTO[]> SearchHousesAsync(string searchTerm);
        Task<House?> GetHouseByUserAccountIdAsync(string userAccountId);
        Task<IEnumerable<House>> GetHousesByVillageIdAsync(string villageId);
        DataTable getHouse();
        Task<HouseDetailsDTO> GetHouseDetails(string houseId);
        Task<List<House>> SearchHouses(SearchHouseDTO searchHouseDTO);
        Task<IEnumerable<House>> GetHousesWithRelationsAsync(IEnumerable<string> houseIds);
        Task<IEnumerable<House>> GetHousesByUserAccountIdAsync(string userAccountId);
    }
}
