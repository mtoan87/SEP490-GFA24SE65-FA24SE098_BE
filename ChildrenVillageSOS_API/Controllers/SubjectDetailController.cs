using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
using ChildrenVillageSOS_DAL.DTO.SubjectDetailsDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectDetailController : ControllerBase
    {
        private readonly ISubjectDetailService _subjectDetailService;

        public SubjectDetailController(ISubjectDetailService subjectDetailService)
        {
            _subjectDetailService = subjectDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjectDetails()
        {
            var subjectDetails = await _subjectDetailService.GetAllSubjectDetails();
            return Ok(subjectDetails);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchSubjectDTO searchSubjectDTO)
        {
            if (string.IsNullOrEmpty(searchSubjectDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _subjectDetailService.SearchSubjects(searchSubjectDTO);
            return Ok(result);
        }

        [HttpGet("GetSubjectDetailById/{id}")]
        public async Task<IActionResult> GetSubjectDetailById(int id)
        {
            var subjectDetail = await _subjectDetailService.GetSubjectDetailById(id);
            if (subjectDetail == null)
            {
                return NotFound($"SubjectDetail with ID {id} not found");
            }
            return Ok(subjectDetail);
        }

        [HttpPost("CreateSubjectDetail")]
        public async Task<IActionResult> CreateSubjectDetail([FromForm] CreateSubjectDetailsDTO createSubjectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSubjectDetail = await _subjectDetailService.CreateSubjectDetail(createSubjectDetail);
            return CreatedAtAction(nameof(GetSubjectDetailById), new { id = newSubjectDetail.Id }, newSubjectDetail);
        }

        [HttpPut("UpdateSubjectDetail/{id}")]
        public async Task<IActionResult> UpdateSubjectDetail(int id, [FromForm] UpdateSubjectDetailsDTO updateSubjectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedSubjectDetail = await _subjectDetailService.UpdateSubjectDetail(id, updateSubjectDetail);
                return Ok(updatedSubjectDetail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteSubjectDetail/{id}")]
        public async Task<IActionResult> DeleteSubjectDetail(int id)
        {
            try
            {
                var deletedSubjectDetail = await _subjectDetailService.DeleteSubjectDetail(id);
                return Ok(deletedSubjectDetail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("RestoreSubjectDetail/{id}")]
        public async Task<IActionResult> RestoreSubjectDetail(int id)
        {
            try
            {
                var restoredSubjectDetail = await _subjectDetailService.RestoreSubjectDetail(id);
                return Ok(restoredSubjectDetail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
