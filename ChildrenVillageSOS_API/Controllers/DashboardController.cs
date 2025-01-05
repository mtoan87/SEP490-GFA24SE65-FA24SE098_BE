using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Response;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IFacilitiesWalletService _facilitiesWalletService;
        private readonly IHealthWalletService _healthWalletService;
        private readonly IFoodStuffWalletService _foodStuffWalletService;
        private readonly ISystemWalletService _systemWalletService;
        private readonly IDonationService _donationService;
        private readonly INecessitiesWalletService _necessitiesWalletService;

        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IFacilitiesWalletService facilitiesWalletService,
            IHealthWalletService healthWalletService,
            IFoodStuffWalletService foodStuffWalletService,
            ISystemWalletService systemWalletService,
            IDonationService donationService,
            INecessitiesWalletService necessitiesWalletService,
            IDashboardService dashboardService)
        {
            _facilitiesWalletService = facilitiesWalletService;
            _healthWalletService = healthWalletService;
            _foodStuffWalletService = foodStuffWalletService;
            _systemWalletService = systemWalletService;
            _donationService = donationService;
            _necessitiesWalletService = necessitiesWalletService;
            _dashboardService = dashboardService;
        }
       
        //[HttpGet("TotalFacilitiesWalletBudget")]
        //public async Task<IActionResult> TotalFacilitiesWalletBudget()
        //{
        //    var totalBudget = await _facilitiesWalletService.GetTotalBudget();
        //    return Ok(new { TotalBudget = totalBudget });
        //}

        //[HttpGet("TotalHealthWalletBudget")]
        //public async Task<IActionResult> TotalHealthWalletBudget()
        //{
        //    var totalBudget = await _healthWalletService.GetTotalBudget();
        //    return Ok(new { TotalBudget = totalBudget });
        //}

        //[HttpGet("TotalFoodStuffWalletBudget")]
        //public async Task<IActionResult> TotalFoodStuffWalletBudget()
        //{
        //    var totalBudget = await _foodStuffWalletService.GetTotalBudget();
        //    return Ok(new { TotalBudget = totalBudget });
        //}

        //[HttpGet("TotalSystemWalletBudget")]
        //public async Task<IActionResult> TotalSystemWalletBudget()
        //{
        //    var totalBudget = await _systemWalletService.GetTotalBudget();
        //    return Ok(new { TotalBudget = totalBudget });
        //}

        //[HttpGet("TotalNecessitiesWalletBudget")]
        //public async Task<IActionResult> TotalNecessitiesWalletBudget()
        //{
        //    var totalBudget = await _necessitiesWalletService.GetTotalBudget();
        //    return Ok(new { TotalBudget = totalBudget });
        //}

        //[HttpGet("MonthlyDonations")]
        //public async Task<IActionResult> GetMonthlyDonations()
        //{
        //    var monthlyDonations = await _donationService.GetMonthlyDonations();
        //    return Ok(monthlyDonations);
        //}

        //[HttpGet("TotalDonations/{year}")]
        //public async Task<IActionResult> GetTotalDonationsByYear(int year)
        //{
        //    var totalDonations = await _donationService.GetTotalDonationsByYear(year);
        //    return Ok(new { Year = year, TotalDonations = totalDonations });
        //}


        // Dashboard Top statistic card
        [HttpGet("active-children")]
        public async Task<IActionResult> GetActiveChildrenStat()
        {
            var stat = await _dashboardService.GetActiveChildrenStatAsync();
            return Ok(stat);
        }

        [HttpGet("total-users")]
        public async Task<IActionResult> GetTotalUsersStatAsync()
        {
            var stat = await _dashboardService.GetTotalUsersStatAsync();
            return Ok(stat);
        }

        [HttpGet("total-events")]
        public async Task<IActionResult> GetTotalEventsStatAsync()
        {
            var stat = await _dashboardService.GetTotalEventsStatAsync();
            return Ok(stat);
        }

        //Charts
        [HttpGet("village-house-distribution")]
        public async Task<ActionResult<IEnumerable<VillageHouseDistributionDTO>>> GetVillageHouseDistribution()
        {
            var result = await _dashboardService.GetVillageHouseDistribution();
            return Ok(result);
        }

        [HttpGet("children-demographics")]
        public async Task<IActionResult> GetChildrenDemographics()
        {
            var result = await _dashboardService.GetChildrenDemographics();
            return Ok(result);
        }

        [HttpGet("payment-methods")]
        public async Task<ActionResult<object>> GetPaymentMethodStatistics()
        {
            var statistics = await _dashboardService.GetPaymentMethodStatistics();
            return Ok(new
            {
                totalTransactions = statistics.Sum(x => x.NumberOfUses),
                totalAmount = statistics.Sum(x => x.TotalAmount),
                statistics = statistics
            });
        }

        // [HttpGet("payment-statistics/date-range")]
        // public async Task<ActionResult<object>> GetPaymentMethodStatisticsByDateRange(
        //[FromQuery] DateTime startDate,
        //[FromQuery] DateTime endDate)
        // {
        //     try
        //     {
        //         var statistics = await _dashboardService.GetPaymentMethodStatisticsByDateRange(startDate, endDate);
        //         return Ok(new
        //         {
        //             totalTransactions = statistics.Sum(x => x.NumberOfUses),
        //             totalAmount = statistics.Sum(x => x.TotalAmount),
        //             statistics = statistics
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        //     }
        // }

        [HttpGet("academic-performance-distribution")]
        public async Task<IActionResult> GetAcademicPerformanceDistribution()
        {
            var result = await _dashboardService.GetAcademicPerformanceDistribution();
            return Ok(result);
        }

        [HttpGet("child-trends")]
        public async Task<IActionResult> GetChildTrends()
        {
            var result = await _dashboardService.GetChildTrendsAsync();
            return Ok(result);
        }

        [HttpGet("income-expense/{year}")]
        public async Task<ActionResult<IncomeExpenseChartDTO>> GetIncomeExpenseComparison(int year)
        {
            try
            {
                var data = await _dashboardService.GetIncomeExpenseComparisonAsync(year);
                return Ok(new ApiResponse<IncomeExpenseChartDTO>
                {
                    Success = true,
                    Message = "Get data success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<IncomeExpenseChartDTO>
                {
                    Success = false,
                    Message = "Error occur when getting data",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("wallet-distribution")]
        public async Task<ActionResult<WalletDistributionDTO>> GetWalletDistribution(string userAccountId)
        {
            try
            {
                var data = await _dashboardService.GetWalletDistributionAsync(userAccountId);
                return Ok(new ApiResponse<WalletDistributionDTO>
                {
                    Success = true,
                    Message = "Get wallet distribution data success",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<WalletDistributionDTO>
                {
                    Success = false,
                    Message = "Error occur when getting wallet distribution data",
                    Error = ex.Message
                });
            }
        }
    }
}
