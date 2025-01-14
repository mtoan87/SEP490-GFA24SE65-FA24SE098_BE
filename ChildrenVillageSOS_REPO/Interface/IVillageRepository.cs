﻿using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IVillageRepository : IRepositoryGeneric<Village>
    {
        List<Village> GetVillagesDonatedByUser(string userAccountId);
        VillageResponseDTO GetVillageByIdWithImg(string villageId);
        //Dashboard:
        Task<IEnumerable<Village>> GetVillagesWithHouses();
        Task<VillageResponseDTO[]> GetVillageByEventIDAsync(int eventId);
        DataTable getVillage();
        Task<VillageResponseDTO[]> SearchVillagesAsync(string searchTerm);
        Task<VillageResponseDTO[]> GetAllVillageIsDelete();
        //Task UpdateVillageStatistics(string villageId);
        Task<IEnumerable<Village>> GetVillagesWithHousesAndChildrenAsync();
        Task<VillageDetailsDTO> GetVillageDetails(string villageId);
        Task<List<Village>> SearchVillages(SearchVillageDTO searchVillageDTO);
    }
}
