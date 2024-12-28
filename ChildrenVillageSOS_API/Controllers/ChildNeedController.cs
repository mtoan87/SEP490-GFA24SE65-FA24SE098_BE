using ChildrenVillageSOS_DAL.DTO.ChildNeedsDTO;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildNeedController : ControllerBase
    {
        private readonly IChildNeedService _childNeedService;

        public ChildNeedController(IChildNeedService childNeedService)
        {
            _childNeedService = childNeedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildNeeds()
        {
            var childNeeds = await _childNeedService.GetAllChildNeeds();
            return Ok(childNeeds);
        }

        [HttpGet("GetChildNeedById/{id}")]
        public async Task<IActionResult> GetChildNeedById(int id)
        {
            var childNeed = await _childNeedService.GetChildNeedById(id);
            if (childNeed == null)
            {
                return NotFound($"ChildNeed with ID {id} not found");
            }
            return Ok(childNeed);
        }

        [HttpPost("CreateChildNeed")]
        public async Task<IActionResult> CreateChildNeed([FromForm] CreateChildNeedsDTO createChildNeed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newChildNeed = await _childNeedService.CreateChildNeed(createChildNeed);
            return CreatedAtAction(nameof(GetChildNeedById), new { id = newChildNeed.Id }, newChildNeed);
        }

        [HttpPut("UpdateChildNeed/{id}")]
        public async Task<IActionResult> UpdateChildNeed(int id, [FromForm] UpdateChildNeedsDTO updateChildNeed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedChildNeed = await _childNeedService.UpdateChildNeed(id, updateChildNeed);
                return Ok(updatedChildNeed);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteChildNeed/{id}")]
        public async Task<IActionResult> DeleteChildNeed(int id)
        {
            try
            {
                var deletedChildNeed = await _childNeedService.DeleteChildNeed(id);
                return Ok(deletedChildNeed);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("RestoreChildNeed/{id}")]
        public async Task<IActionResult> RestoreChildNeed(int id)
        {
            try
            {
                var restoredChildNeed = await _childNeedService.RestoreChildNeed(id);
                return Ok(restoredChildNeed);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
