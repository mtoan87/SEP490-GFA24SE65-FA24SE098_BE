using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Response;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IDashboardService
    {
        //TopStatCards
        Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync();
        Task<TotalUsersStatDTO> GetTotalUsersStatAsync();
        Task<TotalEventsStatDTO> GetTotalEventsStatAsync();
        //KPI
        
        decimal GetCostPerChild();
        decimal GetBudgetUtilizationPercentage();
        object GetMonthlyEfficiency();
        //Charts
        Task<IEnumerable<VillageHouseDistributionDTO>> GetVillageHouseDistribution();
        Task<List<ChildrenDemographicsDTO>> GetChildrenDemographics();
        Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatistics();
        //Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatisticsByDateRange(DateTime startDate, DateTime endDate);
        Task<List<AcademicPerformanceDistributionDTO>> GetAcademicPerformanceDistribution();
        Task<ChildTrendResponseDTO> GetChildTrendsAsync();
        Task<IncomeExpenseChartDTO> GetIncomeExpenseComparisonAsync(int year);
        Task<WalletDistributionDTO> GetWalletDistributionAsync(string userAccountId);
        Task<BookingTrendsDTO> GetBookingTrendsAsync(string timeFrame);
        Task<DonationTrendsDTO> GetDonationTrendsByYear(int year);
    }
}
