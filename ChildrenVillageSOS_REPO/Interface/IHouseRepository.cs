using ChildrenVillageSOS_DAL.DTO.HouseDTO;
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
        Task<string?> GetUserAccountIdByHouseId(string houseId);
        HouseResponseDTO GetHouseByIdWithImg(string houseId);
        Task<HouseResponseDTO[]> GetHouseByVillageIdAsync(string villageId);
        Task<string> GetHouseNameByIdAsync(string houseId);
        //Task<HouseResponseDTO[]> GetAllHouseAsync();
        Task<HouseResponseDTO[]> GetAllHouseIsDeleteAsync();
        DataTable getHouse();
    }
}
