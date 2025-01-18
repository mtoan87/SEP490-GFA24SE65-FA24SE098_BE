using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildrenController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildren()
        {
            var children = await _childService.GetAllChildren();
            return Ok(children);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchChildDTO searchChildDTO)
        {
            if (string.IsNullOrEmpty(searchChildDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _childService.SearchChildren(searchChildDTO);
            return Ok(result);
        }

        [HttpGet("SearchChildren")]
        public async Task<IActionResult> SearchChildrenAsync([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _childService.SearchChildrenAsync(searchTerm);
            return Ok(result);
        }
        [HttpGet("GetChildrenBadStatusByUserId")]
        public async Task<IActionResult> GetChildrenBadStatusByUserId([FromQuery] string userAccountId)
        {
            var children = await _childService.GetChildrenBadStatusByUserId(userAccountId);
            if (!children.Any())
            {
                return NotFound("No children found with bad health status.");
            }

            return Ok(children);
        }


        [HttpGet("GetAllChildIsDelete")]
        public async Task<IActionResult> GetAllChildIsDeleteAsync()
        {
            var children = await _childService.GetAllChildIsDeleteAsync();
            return Ok(children);
        }

        //[HttpGet("GetAllChildWithImg")]
        //public async Task<IActionResult> GetAllChildrenImage()
        //{
        //    var children = await _childService.GetAllChildrenWithImg();
        //    return Ok(children);
        //}

        [HttpGet("GetAllChildWithImg")]
        public async Task<IActionResult> GetAllChildrenImage()
        {
            // Lấy userAccountId từ JWT Token hoặc HttpContext
            var userId = User.FindFirst("userId")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                return Unauthorized("User is not authenticated.");
            }
            try
            {
                // Gọi service để lấy danh sách Children theo userAccountId và role
                var children = await _childService.GetChildrenByUserAsync(userId, role);

                if (children == null || !children.Any())
                {
                    return NotFound("No children found for the given user.");
                }

                return Ok(children);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllChildWithHealthStatusBad")]
        public async Task<IActionResult> GetAllChildrenBad()
        {
            var children = await _childService.GetAllChildrenWithHealthStatusBad();
            return Ok(children);
        }

        [HttpGet("GetChildWithImg")]
        public async Task<IActionResult> GetChildWithImg(string id)
        {
            var child = await _childService.GetChildByIdWithImg(id);
            return Ok(child);
        }

        [HttpGet("GetChildById/{id}")]
        public async Task<IActionResult> GetChildById(string id)
        {
            var child = await _childService.GetChildById(id);
            return Ok(child);
        }

        [HttpGet("GetChildDetails/{childId}")]
        public async Task<IActionResult> GetChildDetails(string childId)
        {
            var childDetails = await _childService.GetChildDetails(childId);
            return Ok(childDetails);
        }

        [HttpGet("GetChildByHouseId/{id}")]
        public async Task<IActionResult> GetChildByHouseIdAsync(string id)
        {
            var child = await _childService.GetChildByHouseIdAsync(id);
            return Ok(child);
        }
        [HttpGet("GetChildByHouseIdArray/{id}")]
        public async Task<IActionResult> GetChildByHouseId(string id)
        {
            var child = await _childService.GetChildByHouseId(id);
            return Ok(child);
        }

        [HttpPost]
        [Route("CreateChild")]
        public async Task<ActionResult<Child>> CreateChild([FromForm] CreateChildDTO createChildDTO)
        {
            var newChild = await _childService.CreateChild(createChildDTO);
            return Ok(newChild);
        }

        [HttpPut]
        [Route("UpdateChild/{id}")]
        public async Task<IActionResult> UpdateChild(string id, [FromForm] UpdateChildDTO updateChildDTO)
        {
            var updatedChild = await _childService.UpdateChild(id, updateChildDTO);
            return Ok(updatedChild);
        }

        [HttpDelete]
        [Route("DeleteChild/{id}")]
        public async Task<IActionResult> DeleteChild(string id)
        {
            var deletedChild = await _childService.DeleteChild(id);
            return Ok(deletedChild);
        }

        [HttpPut("RestoreChild/{id}")]
        public async Task<IActionResult> RestoreChild(string id)
        {
            var restoredChild = await _childService.RestoreChild(id);
            return Ok(restoredChild);
        }

        [HttpGet("Child-donations/{childId}")]
        public async Task<IActionResult> GetChildDonations(string childId)
        {
            if (string.IsNullOrWhiteSpace(childId))
            {
                return BadRequest("Child ID cannot be null or empty.");
            }

            var donations = await _childService.GetDonationsForChildAsync(childId);

            if (!donations.Any())
            {
                return NotFound($"No donations found for child with ID: {childId}");
            }

            return Ok(donations);
        }
    }
}
