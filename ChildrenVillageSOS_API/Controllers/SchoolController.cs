using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
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

        [HttpGet("GetSchoolById/{id}")]
        public async Task<IActionResult> GetSchoolById(int id)
        {
            var school = await _schoolService.GetSchoolById(id);
            if (school == null)
            {
                return NotFound($"School with ID {id} not found");
            }
            return Ok(school);
        }

        [HttpPost("CreateSchool")]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolDTO createSchool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSchool = await _schoolService.CreateSchool(createSchool);
            return CreatedAtAction(nameof(GetSchoolById), new { id = newSchool.Id }, newSchool);
        }

        [HttpPut("UpdateSchool/{id}")]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] UpdateSchoolDTO updateSchool)
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
        public async Task<IActionResult> DeleteSchool(int id)
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

        [HttpPost("RestoreSchool/{id}")]
        public async Task<IActionResult> RestoreSchool(int id)
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
