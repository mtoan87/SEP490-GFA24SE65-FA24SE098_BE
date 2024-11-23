using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodStuffWalletController : ControllerBase
    {
        private readonly IFoodStuffWalletService _service;
        public FoodStuffWalletController(IFoodStuffWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetFoodWallets()
        {
            var exp = await _service.GetFoodWallets();
            return Ok(exp);
        }
        [HttpGet("FormatFoodWallet")]
        public IActionResult GetFoodWalletsToArray()
        {
            var exp = _service.GetFoodStuffWalletsToArray();
            return Ok(exp);
        }
        [HttpGet("GetFoodWalletById/{Id}")]
        public async Task<IActionResult> GetFoodWalletById(int Id)
        {
            var exp = await _service.GetFoodWalletById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateFoodWallet")]
        public async Task<ActionResult<FoodStuffWallet>> CreateFoodWallet([FromForm] CreateFoodWalletDTO expDTO)
        {
            var createExpense = await _service.CreateFoodWallet(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateFoodWalet")]
        public async Task<IActionResult> UpdateFoodWalet(int id, [FromForm] UpdateFoodWalletDTO updateExp)
        {
            var rs = await _service.UpdateFoodWalet(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteFoodWallet")]
        public async Task<IActionResult> DeleteFoodWallet(int id)
        {
            var rs = await _service.DeleteFoodWallet(id);
            return Ok(rs);
        }
    }
}
