using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildProgressController : ControllerBase
    {
        private readonly IChildProgressService _childProgressService;

        public ChildProgressController(IChildProgressService childProgressService)
        {
            _childProgressService = childProgressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildProgress>>> GetAllChildProgresses()
        {
            var childProgresses = await _childProgressService.GetAllChildProgresses();
            return Ok(childProgresses);
        }

        [HttpGet("GetChildProgressById/{id}")]
        public async Task<ActionResult<ChildProgress>> GetChildProgressById(int id)
        {
            var childProgress = await _childProgressService.GetChildProgressById(id);
            if (childProgress == null)
            {
                return NotFound();
            }
            return Ok(childProgress);
        }

        [HttpPost("CreateChildProgress")]
        public async Task<ActionResult<ChildProgress>> CreateChildProgress([FromForm] CreateChildProgressDTO createChildProgress)
        {
            var childProgress = await _childProgressService.CreateChildProgress(createChildProgress);
            return CreatedAtAction(nameof(GetChildProgressById), new { id = childProgress.Id }, childProgress);
        }

        [HttpPut("UpdateChildProgress/{id}")]
        public async Task<IActionResult> UpdateChildProgress(int id, [FromForm] UpdateChildProgressDTO updateChildProgress)
        {
            try
            {
                var updatedChildProgress = await _childProgressService.UpdateChildProgress(id, updateChildProgress);
                return Ok(updatedChildProgress);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteChildProgress/{id}")]
        public async Task<IActionResult> DeleteChildProgress(int id)
        {
            try
            {
                await _childProgressService.DeleteChildProgress(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("RestoreChildProgress/{id}")]
        public async Task<IActionResult> RestoreChildProgress(int id)
        {
            try
            {
                var restoredChildProgress = await _childProgressService.RestoreChildProgress(id);
                return Ok(restoredChildProgress);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
