using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        [HttpGet("FormatedExpenses")]
        public  IActionResult GetFormatedExpenses()
        {
            var exp =  _expenseService.GetFormatedExpenses();
            return Ok(exp);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var exp = await _expenseService.GetAllExpenses();
            return Ok(exp);
        }
        [HttpGet("GetExpenseById/{Id}")]
        public async Task<IActionResult> GetExpenseById(int Id)
        {
            var exp = await _expenseService.GetExpenseById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateExpense")]
        public async Task<ActionResult<Expense>> CreateExoense([FromForm] CreateExepenseDTO expDTO)
        {
            var createExpense = await _expenseService.CreateExpense(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense(int id, [FromForm] UpdateExpenseDTO updateExp)
        {
            var rs = await _expenseService.UpdateExpense(id, updateExp);
            return Ok(rs);
        }
        [HttpPut]
        [Route("ConfirmExpense")]
        public async Task<IActionResult> UpdateExpense(int id)
        {
            var rs = await _expenseService.ConfirmExpense(id);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var rs = await _expenseService.DeleteExpense(id);
            return Ok(rs);
        }
        [HttpPut]
        [Route("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var rs = await _expenseService.SoftDelete(id);
            return Ok(rs);
        }
    }
}
