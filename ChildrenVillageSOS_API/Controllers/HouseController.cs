using ChildrenVillageSOS_DAL.DTO.House;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("FormatedHouse")]
        public async Task<IActionResult> GetAllHouseAsync()
        {
            var houses = await _houseService.GetAllHouseAsync();
            return Ok(houses);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHouses()
        {
            var houses = await _houseService.GetAllHouses();
            return Ok(houses);
        }

        [HttpGet("GetHouseById/{id}")]
        public async Task<IActionResult> GetHouseById(string id)
        {
            var house = await _houseService.GetHouseById(id);
            return Ok(house);
        }
        [HttpGet("GetHouseByVillageId/{id}")]
        public async Task<IActionResult> getHouseByVillageId(string id)
        {
            var house = await _houseService.getHouseByVillageId(id);
            return Ok(house);
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

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreHouse(string id)
        {
            var house = await _houseService.RestoreHouse(id);
            return Ok(house);
        }
    }
}
