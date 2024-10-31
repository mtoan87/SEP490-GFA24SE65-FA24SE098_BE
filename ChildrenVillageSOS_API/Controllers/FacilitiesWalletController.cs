using ChildrenVillageSOS_DAL.DTO.FacilitiesWalletDTO;
using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesWalletController : ControllerBase
    {
        private readonly IFacilitiesWalletService _service;
        public FacilitiesWalletController(IFacilitiesWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetFacilitiesWallets()
        {
            var exp = await _service.GetFacilitiesWallets();
            return Ok(exp);
        }
        [HttpGet("GetFacilitiesWalletById/{Id}")]
        public async Task<IActionResult> GetFacilitiesWalletById(int Id)
        {
            var exp = await _service.GetFacilitiesWalletById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateFacilitiesWallet")]
        public async Task<ActionResult<FacilitiesWallet>> CreateFacilitiesWallet([FromForm] CreateFacilitiesWalletDTO expDTO)
        {
            var createExpense = await _service.CreateFacilitiesWallet(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateFacilitiesWalet")]
        public async Task<IActionResult> UpdateFacilitiesWalet(int id, [FromForm] UpdateFacilitiesWalletDTO updateExp)
        {
            var rs = await _service.UpdateFacilitiesWalet(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteFacilitiesWallet")]
        public async Task<IActionResult> DeleteFacilitiesWallet(int id)
        {
            var rs = await _service.DeleteFacilitiesWallet(id);
            return Ok(rs);
        }
    }
}
