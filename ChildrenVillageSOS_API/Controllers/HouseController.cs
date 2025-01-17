﻿using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
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
    public class HousesController : ControllerBase
    {
        private readonly IHouseService _houseService;

        public HousesController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        //[HttpGet("FormatedHouse")]
        //public async Task<IActionResult> GetAllHouseAsync()
        //{
        //    var houses = await _houseService.GetAllHouseAsync();
        //    return Ok(houses);
        //}
        [HttpGet("GetAllHousesIsDelete")]
        public async Task<IActionResult> GetAllHouseIsDeleteAsync()
        {
            var houses = await _houseService.GetAllHouseIsDeleteAsync();
            return Ok(houses);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHouses()
        {
            var houses = await _houseService.GetAllHouses();
            return Ok(houses);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchHouseDTO searchHouseDTO)
        {
            if (string.IsNullOrEmpty(searchHouseDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _houseService.SearchHouses(searchHouseDTO);
            return Ok(result);
        }
        [HttpGet("SearchHouse")]
        public async Task<IActionResult> SearchHousesAsync([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _houseService.SearchHousesAsync(searchTerm);
            return Ok(result);
        }
        [HttpGet("GetHouseByAccountId")]
        public  IActionResult getHouseByAccountId(string userAccountId)
        {
            var houses =  _houseService.GetHouseByUserAccountId(userAccountId);
            return Ok(houses);
        }
        [HttpGet("GetHouseNameByAccountId")]
        public IActionResult GetHouseByUserIdWithImg(string userAccountId)
        {
            var houses = _houseService.GetHouseByUserIdWithImg(userAccountId);
            return Ok(houses);
        }

        [HttpGet("GetAllHousesWithImg")]
        public async Task<IActionResult> GetAllHousesWithImg()
        {
            var house = await _houseService.GetAllHousesWithImg();
            return Ok(house);
        }

        [HttpGet("GetHousesByRoleWithImg")]
        public async Task<IActionResult> GetHousesByRoleWithImg()
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
                // Gọi service để lấy danh sách Houses theo userId và role
                var houses = await _houseService.GetHousesByRoleWithImg(userId, role);

                // Kiểm tra nếu không có dữ liệu nhà nào trả về
                if (houses == null || !houses.Any())
                {
                    return NotFound("No houses found for the given user.");
                }

                // Trả về kết quả dưới dạng OK nếu có dữ liệu
                return Ok(houses);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có (ví dụ: log lỗi)
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetHouseById/{id}")]
        public async Task<IActionResult> GetHouseById(string id)
        {
            var house = await _houseService.GetHouseById(id);
            return Ok(house);
        }

        [HttpGet("GetHouseByIdWithImg/{id}")]
        public async Task<IActionResult> GetHouseByIdWithImg(string id)
        {
            var house = await _houseService.GetHouseByIdWithImg(id);
            return Ok(house);
        }

        [HttpGet("GetHouseByVillageId/{id}")]
        public async Task<IActionResult> getHouseByVillageId(string id)
        {
            var house = await _houseService.getHouseByVillageId(id);
            return Ok(house);
        }

        [HttpGet("GetHouseDetails/{houseId}")]
        public async Task<IActionResult> GetHouseDetails(string houseId)
        {
            var houseDetails = await _houseService.GetHouseDetails(houseId);
            return Ok(houseDetails);
        }

        [HttpPost]
        [Route("CreateHouse")]
        public async Task<ActionResult<House>> CreateHouse([FromForm] CreateHouseDTO createHouseDTO)
        {
            var newHouse = await _houseService.CreateHouse(createHouseDTO);
            return Ok(newHouse);
        }

        [HttpPut]
        [Route("UpdateHouse")]
        public async Task<IActionResult> UpdateHouse(string id, [FromForm] UpdateHouseDTO updateHouseDTO)
        {
            var updatedHouse = await _houseService.UpdateHouse(id, updateHouseDTO);
            return Ok(updatedHouse);
        }

        [HttpDelete]
        [Route("DeleteHouse")]
        public async Task<IActionResult> DeleteHouse(string id)
        {
            var deletedHouse = await _houseService.DeleteHouse(id);
            return Ok(deletedHouse);
        }

        [HttpPut("RestoreHouse/{id}")]
        public async Task<IActionResult> RestoreHouse(string id)
        {
            var house = await _houseService.RestoreHouse(id);
            return Ok(house);
        }
    }
}
