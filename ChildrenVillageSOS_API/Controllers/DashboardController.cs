﻿using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public DashboardController(IFacilitiesWalletService facilitiesWalletService, IHealthWalletService healthWalletService,IFoodStuffWalletService foodStuffWalletService, ISystemWalletService systemWalletService, IDonationService donationService, INecessitiesWalletService necessitiesWalletService)
        {
            _facilitiesWalletService = facilitiesWalletService;
            _healthWalletService = healthWalletService;
            _foodStuffWalletService = foodStuffWalletService;
            _systemWalletService = systemWalletService;
            _donationService = donationService;
            _necessitiesWalletService = necessitiesWalletService;   
        }

        [HttpGet("TotalFacilitiesWalletBudget")]
        public async Task<IActionResult> TotalFacilitiesWalletBudget()
        {
            var totalBudget = await _facilitiesWalletService.GetTotalBudget();
            return Ok(new { TotalBudget = totalBudget });
        }
        [HttpGet("TotalHealthWalletBudget")]
        public async Task<IActionResult> TotalHealthWalletBudget()
        {
            var totalBudget = await _healthWalletService.GetTotalBudget();
            return Ok(new { TotalBudget = totalBudget });
        }
        [HttpGet("TotalFoodStuffWalletBudget")]
        public async Task<IActionResult> TotalFoodStuffWalletBudget()
        {
            var totalBudget = await _foodStuffWalletService.GetTotalBudget();
            return Ok(new { TotalBudget = totalBudget });
        }
        [HttpGet("TotalSystemWalletBudget")]
        public async Task<IActionResult> TotalSystemWalletBudget()
        {
            var totalBudget = await _systemWalletService.GetTotalBudget();
            return Ok(new { TotalBudget = totalBudget });
        }
        [HttpGet("TotalNecessitiesWalletBudget")]
        public async Task<IActionResult> TotalNecessitiesWalletBudget()
        {
            var totalBudget = await _necessitiesWalletService.GetTotalBudget();
            return Ok(new { TotalBudget = totalBudget });
        }
        [HttpGet("MonthlyDonations")]
        public async Task<IActionResult> GetMonthlyDonations()
        {
            var monthlyDonations = await _donationService.GetMonthlyDonations();
            return Ok(monthlyDonations);
        }
        [HttpGet("TotalDonations/{year}")]
        public async Task<IActionResult> GetTotalDonationsByYear(int year)
        {
            var totalDonations = await _donationService.GetTotalDonationsByYear(year);
            return Ok(new { Year = year, TotalDonations = totalDonations });
        }

    }
}
