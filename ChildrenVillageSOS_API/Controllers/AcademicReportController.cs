using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicReportController : ControllerBase
    {
        private readonly IAcademicReportService _academicReportService;

        public AcademicReportController(IAcademicReportService academicReportService)
        {
            _academicReportService = academicReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAcademicReports()
        {
            var reports = await _academicReportService.GetAllAcademicReports();
            return Ok(reports);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchAcademicReportDTO searchAcademicReportDTO)
        {
            if (string.IsNullOrEmpty(searchAcademicReportDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _academicReportService.SearchAcademicReports(searchAcademicReportDTO);
            return Ok(result);
        }

        [HttpGet("GetAllAcademicReportIsDelete")]
        public async Task<IActionResult> GetAllAcademicReportIsDeleteAsync()
        {
            var reports = await _academicReportService.GetAllAcademicReportIsDeleteAsync();
            return Ok(reports);
        }

        //[HttpGet("GetAllAcademicReportWithImg")]
        //public async Task<IActionResult> GetAllAcademicReportWithImg()
        //{
        //    var reports = await _academicReportService.GetAllAcademicReportWithImg();
        //    return Ok(reports);
        //}

        [HttpGet("GetAllAcademicReportWithImg")]
        //[Authorize]
        public async Task<IActionResult> GetAllAcademicReportWithImg()
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
                var reports = await _academicReportService.GetAllAcademicReportWithImg(userId, role);

                return Ok(reports);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về phản hồi
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAcademicReportByIdWithImg/{id}")]
        public async Task<IActionResult> GetAcademicReportByIdWithImg(int inventoryId)
        {
            var reports = await _academicReportService.GetAcademicReportByIdWithImg(inventoryId);
            return Ok(reports);
        }

        [HttpGet("GetAcademicReportById/{id}")]
        public async Task<IActionResult> GetAcademicReportById(int id)
        {
            var report = await _academicReportService.GetAcademicReportById(id);
            if (report == null)
            {
                return NotFound($"Academic report with ID {id} not found.");
            }
            return Ok(report);
        }

        [HttpPost]
        [Route("CreateAcademicReport")]
        public async Task<ActionResult<AcademicReport>> CreateAcademicReport([FromForm] CreateAcademicReportDTO createReport)
        {
            var newReport = await _academicReportService.CreateAcademicReport(createReport);
            return Ok(newReport);
        }

        [HttpPut]
        [Route("UpdateAcademicReport/{id}")]
        public async Task<IActionResult> UpdateAcademicReport(int id, [FromForm] UpdateAcademicReportDTO updateReport)
        {
            try
            {
                var updatedReport = await _academicReportService.UpdateAcademicReport(id, updateReport);
                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteAcademicReport/{id}")]
        public async Task<IActionResult> DeleteAcademicReport(int id)
        {
            try
            {
                var deletedReport = await _academicReportService.DeleteAcademicReport(id);
                return Ok(deletedReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("RestoreAcademicReport/{id}")]
        public async Task<IActionResult> RestoreAcademicReport(int id)
        {
            try
            {
                var restoredAcademicReport = await _academicReportService.RestoreAcademicReport(id);
                return Ok(restoredAcademicReport);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
