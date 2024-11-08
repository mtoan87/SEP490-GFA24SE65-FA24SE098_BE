using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NecessitiesWalletController : ControllerBase
    {
        private readonly INecessitiesWalletService _service;
        public NecessitiesWalletController(INecessitiesWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetNecessitiesWallets()
        {
            var exp = await _service.GetNecessitiesWallets();
            return Ok(exp);
        }
        [HttpGet("GetNecessitiesWalletById/{Id}")]
        public async Task<IActionResult> GetNecessitiesWalletById(int Id)
        {
            var exp = await _service.GetNecessitiesWalletById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateNecessitiesWallet")]
        public async Task<ActionResult<HealthWallet>> CreateNecessitiesWallet([FromForm] CreateHealthWalletDTO expDTO)
        {
            var createExpense = await _service.CreateNecessitiesWallet(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateNecessitiesWalet")]
        public async Task<IActionResult> UpdateNecessitiesWalet(int id, [FromForm] UpdateHealthWalletDTO updateExp)
        {
            var rs = await _service.UpdateNecessitiesWalet(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteNecessitiesWallet")]
        public async Task<IActionResult> DeleteNecessitiesWallet(int id)
        {
            var rs = await _service.DeleteNecessitiesWallet(id);
            return Ok(rs);
        }
    }
}
