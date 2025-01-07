using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchools();
            return Ok(schools);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchSchoolDTO searchSchoolDTO)
        {
            if (string.IsNullOrEmpty(searchSchoolDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _schoolService.SearchSchools(searchSchoolDTO);
            return Ok(result);
        }

        [HttpGet("GetAllSchoolsIsDeleted")]
        public async Task<IActionResult> GetAllSchoolsIsDeleted()
        {
            var school = await _schoolService.GetAllSchoolsIsDeleted();
            return Ok(school);
        } 

        [HttpGet("GetAllSchoolWithImg")]
        public async Task<IActionResult> GetAllSchoolWithImg()
        {
            var school = await _schoolService.GetAllSchoolWithImg();
            return Ok(school);
        }

        [HttpGet("GetSchoolDetails/{schoolId}")]
        public async Task<IActionResult> GetSchoolDetails(string schoolId)
        {
            var schoolDetails = await _schoolService.GetSchoolDetails(schoolId);
            return Ok(schoolDetails);
        }

        [HttpGet("GetSchoolByIdWithImg")]
        public async Task<IActionResult> GetSchoolByIdWithImg(string schoolId)
        {
            var school = await _schoolService.GetSchoolByIdWithImg(schoolId);
            return Ok(school);
        }

        [HttpGet("GetSchoolById/{id}")]
        public async Task<IActionResult> GetSchoolById(string id)
        {
            var school = await _schoolService.GetSchoolById(id);
            if (school == null)
            {
                return NotFound($"School with ID {id} not found");
            }
            return Ok(school);
        }

        [HttpPost("CreateSchool")]
        public async Task<IActionResult> CreateSchool([FromForm] CreateSchoolDTO createSchool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSchool = await _schoolService.CreateSchool(createSchool);
            return CreatedAtAction(nameof(GetSchoolById), new { id = newSchool.Id }, newSchool);
        }

        [HttpPut("UpdateSchool/{id}")]
        public async Task<IActionResult> UpdateSchool(string id, [FromForm] UpdateSchoolDTO updateSchool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedSchool = await _schoolService.UpdateSchool(id, updateSchool);
                return Ok(updatedSchool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteSchool/{id}")]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            try
            {
                var deletedSchool = await _schoolService.DeleteSchool(id);
                return Ok(deletedSchool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("RestoreSchool/{id}")]
        public async Task<IActionResult> RestoreSchool(string id)
        {
            try
            {
                var restoredSchool = await _schoolService.RestoreSchool(id);
                return Ok(restoredSchool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
