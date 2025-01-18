using ChildrenVillageSOS_DAL.DTO.UserDTO;
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
    public class VillageController : ControllerBase
    {
        private readonly IVillageService _villageService;
        public VillageController(IVillageService villageService)
        {
            _villageService = villageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVillage()
        {
            var vil = await _villageService.GetAllVillage();
            return Ok(vil);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchVillageDTO searchVillageDTO)
        {
            if (string.IsNullOrEmpty(searchVillageDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _villageService.SearchVillages(searchVillageDTO);
            return Ok(result);
        }
        [HttpGet("SearchVillage")]
        public async Task<IActionResult> SearchVillagesAsync([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _villageService.SearchVillagesAsync(searchTerm);
            return Ok(result);
        }

        [HttpGet("GetVillageByEventId")]
        public async Task<IActionResult> GetVillageByEventId(int eventId)
        {
            var vil = await _villageService.GetVillageByEventIDAsync(eventId);
            return Ok(vil);
        }

        [HttpGet("GetAllVillageIsDelete")]
        public async Task<IActionResult> GetAllVillageIsDelete()
        {
            var deletedVillages = await _villageService.GetAllVillageIsDelete();
            return Ok(deletedVillages);
        }

        //[HttpGet("GetAllVillageWithImg")]
        //public async Task<IActionResult> GetAllVillageWithImg()
        //{
        //    var vil = await _villageService.GetAllVillageWithImg();
        //    return Ok(vil);
        //}

        [HttpGet("GetAllVillageWithImg")]
        public async Task<IActionResult> GetAllVillageWithImg()
        {
            // Lấy userId và role từ JWT Token (hoặc từ claim trong token)
            var userId = User.FindFirst("userId")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            // Kiểm tra xem userId và role có tồn tại hay không
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                return Unauthorized("User is not authenticated.");
            }

            try
            {
                // Gọi service để lấy danh sách Village theo userId và role
                var villages = await _villageService.GetVillagesByRoleWithImg(userId, role);

                // Kiểm tra nếu không có dữ liệu village nào trả về
                if (villages == null || !villages.Any())
                {
                    return NotFound("No villages found for the given user.");
                }

                // Trả về kết quả dưới dạng OK nếu có dữ liệu
                return Ok(villages);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có (ví dụ: log lỗi)
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetVillageById/{Id}")]
        public async Task<IActionResult> GetVillageById(string villageId)
        {
            var vil = await _villageService.GetVillageById(villageId);
            return Ok(vil);
        }

        [HttpGet("GetVillageByIdWithImg")]
        public async Task<IActionResult> GetVillageByIdWithImg(string Id)
        {
            var vil = await _villageService.GetVillageByIdWithImg(Id);
            return Ok(vil);
        }

        [HttpGet("GetVillageDetails/{villageId}")]
        public async Task<IActionResult> GetVillageDetails(string villageId)
        {
            var villageDetails = await _villageService.GetVillageDetails(villageId);
            return Ok(villageDetails);
        }

        [HttpGet("GetVillagesDonatedByUser")]
        public IActionResult GetVillagesDonatedByUser(string userId)
        {
            var vil =  _villageService.GetVillagesDonatedByUser(userId);
            return Ok(vil);
        }
        [HttpGet("GetVillagesByUserId")]
        public IActionResult GetVillageByUserIdWithImg(string userId)
        {
            var vil = _villageService.GetVillageByUserIdWithImg(userId);
            return Ok(vil);
        }

        [HttpGet("GetVillagesByUser/{userAccountId}")]
        public IActionResult GetVillagesByUser(string userAccountId)
        {
            var villages = _villageService.GetVillagesDonatedByUser(userAccountId);

            if (!villages.Any())
            {
                return NotFound(new { Message = "No villages found for this user." });
            }

            return Ok(villages.Select(v => new
            {
                v.Id,
                v.VillageName,
                v.Location,
                v.Description
            }));
        }

        [HttpPost]
        [Route("CreateVillage")]
        public async Task<ActionResult<Village>> CreateVillage([FromForm] CreateVillageDTO crevilDTO)
        {
            var createVil = await _villageService.CreateVillage(crevilDTO);
            return Ok(createVil);
        }

        [HttpPut]
        [Route("UpdateVillage")]
        public async Task<IActionResult> UpdateVillage(string villageId, [FromForm] UpdateVillageDTO updavilDTO)
        {
            var vil = await _villageService.UpdateVillage(villageId, updavilDTO);
            return Ok(vil);
        }

        [HttpDelete]
        [Route("DeleteVillage")]
        public async Task<IActionResult> DeleteVillage(string villageId)
        {
            var vil = await _villageService.DeleteVillage(villageId);
            return Ok(vil);
        }

        [HttpPut("RestoreVillage/{id}")]
        public async Task<IActionResult> RestoreVillage(string id)
        {
            var restoredChild = await _villageService.RestoreVillage(id);
            return Ok(restoredChild);
        }
    }
}
