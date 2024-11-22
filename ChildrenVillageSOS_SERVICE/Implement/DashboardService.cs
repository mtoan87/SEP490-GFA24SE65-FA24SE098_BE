using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class DashboardService : IDashboardService
    {
        private readonly IChildRepository _childRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVillageRepository _villageRepository;

        public DashboardService(IChildRepository childRepository, IUserAccountRepository userAccountRepository, IEventRepository eventRepository, 
            IVillageRepository villageRepository)
        {
            _childRepository = childRepository;
            _userAccountRepository = userAccountRepository;
            _eventRepository = eventRepository;
            _villageRepository = villageRepository;
        }

        //TopStatCards
        public async Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync()
        {
            return await _childRepository.GetActiveChildrenStatAsync();
        }

        public async Task<TotalEventsStatDTO> GetTotalEventsStatAsync()
        {
            return await _eventRepository.GetTotalEventsStatAsync();
        }

        public async Task<TotalUsersStatDTO> GetTotalUsersStatAsync()
        {
            return await _userAccountRepository.GetTotalUsersStatAsync();
        }

        //KPI

        //Charts

        //Phan bo lang va nha
        public async Task<IEnumerable<VillageHouseDistributionDTO>> GetVillageHouseDistribution()
        {
            var villages = await _villageRepository.GetVillagesWithHouses();

            return villages.Select(v => new VillageHouseDistributionDTO
            {
                VillageName = v.VillageName,
                HouseCount = v.Houses.Count(h => !h.IsDeleted) // Chỉ đếm những nhà chưa bị xóa
            })
            .OrderByDescending(v => v.HouseCount); // Sắp xếp theo số lượng nhà giảm dần
        }


        public async Task<List<ChildrenDemographicsDTO>> GetChildrenDemographics()
        {
            // Sử dụng method specific
            var children = await _childRepository.GetChildrenForDemographics();
            // Hoặc sử dụng method generic
            // var children = await _childRepository.GetAllAsync(x => !x.IsDeleted && x.Status == "Active");

            var demographics = Enumerable.Range(0, 4)
                .Select(ageGroup =>
                {
                    var childrenInGroup = children.Where(c =>
                        AgeCalculator.GetAgeGroup(c.Dob) == ageGroup);

                    return new ChildrenDemographicsDTO
                    {
                        AgeGroup = AgeCalculator.GetAgeGroupLabel(ageGroup),
                        MaleCount = childrenInGroup.Count(c => c.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase)),
                        FemaleCount = childrenInGroup.Count(c => c.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                    };
                })
                .ToList();

            return demographics;
        }
    }
}
