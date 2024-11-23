using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        public IncomesController(IIncomeService incomeService )
        {
            _incomeService = incomeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var exp = await _incomeService.GetAllIncomes();
            return Ok(exp);
        }
        [HttpGet("FormatedIncome")]
        public  IActionResult GetFormatedIncome()
        {
            var exp =  _incomeService.GetFormatedIncome();
            return Ok(exp);
        }
        [HttpGet("GetIncomeByFacilitiesWallet")]
        public IActionResult GetIncomeByFacilitiesWalletId(int Id)
        {
            var exp = _incomeService.GetIcomeByFaciWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByFoodWallet")]
        public IActionResult GetIncomeByFoodWalletId(int Id)
        {
            var exp = _incomeService.GetIcomeByFoodWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByHealthWallet")]
        public IActionResult GetIncomeByHealthWallet(int Id)
        {
            var exp = _incomeService.GetIcomeByHealthWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByNescilitiesWallet")]
        public IActionResult GetIncomeByNescilitiesWallet(int Id)
        {
            var exp = _incomeService.GetIcomeByNesWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeBySystemWallet")]
        public IActionResult GetIncomeBySystemWallet(int Id)
        {
            var exp = _incomeService.GetIcomeBySystemWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeById/{Id}")]
        public async Task<IActionResult> GetIncomeById(int Id)
        {
            var exp = await _incomeService.GetIncomeById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateIncome")]
        public async Task<ActionResult<Income>> CreateIncome([FromBody] CreateIncomeDTO expDTO)
        {
            var createExpense = await _incomeService.CreateIncome(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateIncome")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] UpdateIncomeDTO updateExp)
        {
            var rs = await _incomeService.UpdateIncome(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteIncome")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var rs = await _incomeService.DeleteIncome(id);
            return Ok(rs);
        }
        [HttpPut]
        [Route("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var rs = await _incomeService.SoftDelete(id);
            return Ok(rs);
        }
    }
}
