using ChildrenVillageSOS_DAL.DTO.ActivityDTO;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            var activities = await _activityService.GetAllActivities();
            return Ok(activities);
        }

        [HttpGet("GetActivityById/{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            try
            {
                var activity = await _activityService.GetActivityById(id);
                return Ok(activity);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("CreateActivity")]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDTO createActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var activity = await _activityService.CreateActivity(createActivity);
                return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateActivity/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDTO updateActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedActivity = await _activityService.UpdateActivity(id, updateActivity);
                return Ok(updatedActivity);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteActivity/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var deletedActivity = await _activityService.DeleteActivity(id);
                return Ok(deletedActivity);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("RestoreActivity/{id}")]
        public async Task<IActionResult> RestoreActivity(int id)
        {
            try
            {
                var restoredActivity = await _activityService.RestoreActivity(id);
                return Ok(restoredActivity);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
