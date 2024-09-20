using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemWalletController : ControllerBase
    {
        private readonly ISystemWalletService _service;
        public SystemWalletController(ISystemWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            var exp = await _service.GetSystemWallets();
            return Ok(exp);
        }
        [HttpGet("GetWalletById/{Id}")]
        public async Task<IActionResult> GetWalletById(int Id)
        {
            var exp = await _service.GetWalletById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateWallet")]
        public async Task<ActionResult<SystemWallet>> CreateWallet([FromForm] CreateSystemWalletDTO expDTO)
        {
            var createExpense = await _service.CreateWallet(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateWalet")]
        public async Task<IActionResult> UpdateWalet(int id, [FromForm] UpdateSystemWalletDTO updateExp)
        {
            var rs = await _service.UpdateWalet(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteWallet")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var rs = await _service.DeleteWallet(id);
            return Ok(rs);
        }
    }
}
