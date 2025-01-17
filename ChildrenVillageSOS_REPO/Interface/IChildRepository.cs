﻿using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IChildRepository : IRepositoryGeneric<Child>
    {
        Task<IEnumerable<Child>> GetAllAsync();
        ChildResponseDTO GetChildByIdWithImg(string childId);
        Task<List<Child>> GetChildByHouseIdAsync(string houseId);
        //Dashboard
        Task<List<Child>> GetChildrenByIdsAsync(List<string> childIds);
        Task<ChildResponseDTO[]> SearchChildrenAsync(string searchTerm);
        Task<IEnumerable<Child>> GetChildrenWithBadHealthAsync(string houseId);
        Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync();
        Task<IEnumerable<Child>> GetChildrenForDemographics();
        Task<ChildResponseDTO[]> GetAllChildIsDeleteAsync();
        Task<int> CountChildrenByHouseIdAsync(string houseId);
        Task<ChildDetailsDTO> GetChildDetails(string childId);
        Task<List<ChildTrendDTO>> GetChildTrendsByYearAsync(int year);
        Task<ChildResponseDTO[]> GetChildByHouseId(string houseId);
        Task<List<Child>> SearchChildren(SearchChildDTO searchChildDTO);
        Task<IEnumerable<Child>> GetChildrenWithRelationsAsync();
        Task<IEnumerable<Donation>> GetDonationsByChildIdAsync(string childId);
        Task<IEnumerable<string>> GetChildrenByHouseIdsAsync(IEnumerable<string> houseIds);
    }
}
