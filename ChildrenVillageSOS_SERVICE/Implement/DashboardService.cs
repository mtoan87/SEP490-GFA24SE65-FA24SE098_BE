using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Response;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAcademicReportRepository _academicReportRepository;

        public DashboardService(IChildRepository childRepository, IUserAccountRepository userAccountRepository, IEventRepository eventRepository, 
            IVillageRepository villageRepository, IPaymentRepository paymentRepository, IAcademicReportRepository academicReportRepository)
        {
            _childRepository = childRepository;
            _userAccountRepository = userAccountRepository;
            _eventRepository = eventRepository;
            _villageRepository = villageRepository;
            _paymentRepository = paymentRepository;
            _academicReportRepository = academicReportRepository;
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

        public async Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatistics()
        {
            return await _paymentRepository.GetPaymentMethodStatistics();
        }

        //public async Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatisticsByDateRange(DateTime startDate, DateTime endDate)
        //{
        //    return await _paymentRepository.GetPaymentMethodStatisticsByDateRange(startDate, endDate);
        //}

        // Academic Performance Distribution Dashboard
        public async Task<List<AcademicPerformanceDistributionDTO>> GetAcademicPerformanceDistribution()
        {
            var reports = await _academicReportRepository.GetAcademicPerformanceDistribution();

            var primaryReports = reports.Where(r => r.SchoolLevel == "Elementary School").ToList();
            var secondaryReports = reports.Where(r => r.SchoolLevel == "Middle  School").ToList();
            var highSchoolReports = reports.Where(r => r.SchoolLevel == "High School").ToList();

            var result = new List<AcademicPerformanceDistributionDTO>
        {
            CalculateDistribution(primaryReports, "Elementary School"),
            CalculateDistribution(secondaryReports, "Middle School"),
            CalculateDistribution(highSchoolReports, "High School")
        };

            return result;
        }

        private AcademicPerformanceDistributionDTO CalculateDistribution(List<AcademicReport> reports, string schoolLevel)
        {
            int total = reports.Count;
            if (total == 0) return new AcademicPerformanceDistributionDTO
            {
                SchoolLevel = schoolLevel,
                ExcellentCount = 0,
                VeryGoodCount = 0,
                GoodCount = 0,
                AverageCount = 0,
                BelowAverageCount = 0,
                ExcellentPercentage = 0,
                VeryGoodPercentage = 0,
                GoodPercentage = 0,
                AveragePercentage = 0,
                BelowAveragePercentage = 0
            };

            var excellent = reports.Count(r => r.SchoolReport == "Excellent");
            var veryGood = reports.Count(r => r.SchoolReport == "Very Good");
            var good = reports.Count(r => r.SchoolReport == "Good");
            var average = reports.Count(r => r.SchoolReport == "Average");
            var belowAverage = reports.Count(r => r.SchoolReport == "Below Average");

            return new AcademicPerformanceDistributionDTO
            {
                SchoolLevel = schoolLevel,
                ExcellentCount = excellent,
                VeryGoodCount = veryGood,
                GoodCount = good,
                AverageCount = average,
                BelowAverageCount = belowAverage,
                ExcellentPercentage = Math.Round((double)excellent / total * 100, 2),
                VeryGoodPercentage = Math.Round((double)veryGood / total * 100, 2),
                GoodPercentage = Math.Round((double)good / total * 100, 2),
                AveragePercentage = Math.Round((double)average / total * 100, 2),
                BelowAveragePercentage = Math.Round((double)belowAverage / total * 100, 2)
            };
        }

        public async Task<ChildTrendResponseDTO> GetChildTrendsAsync()
        {
            var data2022 = await _childRepository.GetChildTrendsByYearAsync(2022);
            var data2023 = await _childRepository.GetChildTrendsByYearAsync(2023);
            var data2024 = await _childRepository.GetChildTrendsByYearAsync(2024);

            return new ChildTrendResponseDTO
            {
                Data2022 = data2022,
                Data2023 = data2023,
                Data2024 = data2024
            };
        }
    }
}
