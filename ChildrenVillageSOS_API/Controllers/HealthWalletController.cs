using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthWalletController : ControllerBase
    {
        private readonly IHealthWalletService _service;
        public HealthWalletController(IHealthWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetHealthWallets()
        {
            var exp = await _service.GetHealthWallets();
            return Ok(exp);
        }
        [HttpGet("GetHealthWalletArray")]
        public IActionResult GetHealthWalletArray()
        {
            var exp =  _service.GetHealthWalletArray();
            return Ok(exp);
        }
        [HttpGet("GetHealthWalletById/{Id}")]
        public async Task<IActionResult> GetHealthWalletById(int Id)
        {
            var exp = await _service.GetHealthWalletById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateHealthWallet")]
        public async Task<ActionResult<HealthWallet>> CreateHealthWallet([FromForm] CreateHealthWalletDTO expDTO)
        {
            var createExpense = await _service.CreateHealthWallet(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateHealthWalet")]
        public async Task<IActionResult> UpdateHealthWalet(int id, [FromForm] UpdateHealthWalletDTO updateExp)
        {
            var rs = await _service.UpdateHealthWalet(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteHealthWallet")]
        public async Task<IActionResult> DeleteHealthWallet(int id)
        {
            var rs = await _service.DeleteHealthWallet(id);
            return Ok(rs);
        }
    }
}
