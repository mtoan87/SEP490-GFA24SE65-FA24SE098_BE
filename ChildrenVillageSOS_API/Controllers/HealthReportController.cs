﻿using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthReportController : ControllerBase
    {
        private readonly IHealthReportService _healthReportService;

        public HealthReportController(IHealthReportService healthReportService)
        {
            _healthReportService = healthReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHealthReports()
        {
            var reports = await _healthReportService.GetAllHealthReports();
            return Ok(reports);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchHealthReportDTO searchHealthReportDTO)
        {
            if (string.IsNullOrEmpty(searchHealthReportDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _healthReportService.SearchHealthReports(searchHealthReportDTO);
            return Ok(result);
        }

        [HttpGet("GetAllHealthReportIsDelete")]
        public async Task<IActionResult> GetAllHealthReportIsDeleteAsync()
        {
            var inventory = await _healthReportService.GetAllHealthReportIsDeleteAsync();
            return Ok(inventory);
        }

        //[HttpGet("GetAllHealthReportWithImg")]
        //public async Task<IActionResult> GetAllHealthReportWithImg()
        //{
        //    var inventory = await _healthReportService.GetAllHealthReportWithImg();
        //    return Ok(inventory);
        //}

        [HttpGet("GetAllHealthReportWithImg")]
        //[Authorize]
        public async Task<IActionResult> GetAllHealthReportWithImg()
        {
            try
            {
                // Lấy thông tin userId và role từ token
                var userId = User.FindFirst("userId")?.Value;
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
                {
                    return Unauthorized("User information is missing in the token.");
                }

                // Gọi service để lấy danh sách Academic Report theo role
                var reports = await _healthReportService.GetAllHealthReportWithImg(userId, role);

                return Ok(reports);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetHealthReportByIdWithImg/{id}")]
        public async Task<IActionResult> GetHealthReportByIdWithImg(int id)
        {
            var inventory = await _healthReportService.GetHealthReportByIdWithImg(id);
            return Ok(inventory);
        }

        [HttpGet("GetHealthReportById/{id}")]
        public async Task<IActionResult> GetHealthReportById(int id)
        {
            var report = await _healthReportService.GetHealthReportById(id);
            if (report == null)
            {
                return NotFound($"Health report with ID {id} not found.");
            }
            return Ok(report);
        }

        [HttpPost]
        [Route("CreateHealthReport")]
        public async Task<ActionResult<HealthReport>> CreateHealthReport([FromForm] CreateHealthReportDTO createReport)
        {
            var newReport = await _healthReportService.CreateHealthReport(createReport);
            return Ok(newReport);
        }

        [HttpPut]
        [Route("UpdateHealthReport/{id}")]
        public async Task<IActionResult> UpdateHealthReport(int id, [FromForm] UpdateHealthReportDTO updateReport)
        {
            try
            {
                var updatedReport = await _healthReportService.UpdateHealthReport(id, updateReport);
                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteHealthReport/{id}")]
        public async Task<IActionResult> DeleteHealthReport(int id)
        {
            try
            {
                var deletedReport = await _healthReportService.DeleteHealthReport(id);
                return Ok(deletedReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("RestoreHealthReport/{id}")]
        public async Task<IActionResult> RestoreHealthReport(int id)
        {
            try
            {
                var restoredHealthReport = await _healthReportService.RestoreHealthReport(id);
                return Ok(restoredHealthReport);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
