using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

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
    }
}
