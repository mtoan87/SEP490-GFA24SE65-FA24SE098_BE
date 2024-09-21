using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.DTO.TransactionDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var exp = await _service.GetTransactions();
            return Ok(exp);
        }
        [HttpGet("GetTransactionById/{Id}")]
        public async Task<IActionResult> GetTransactionById(int Id)
        {
            var exp = await _service.GetTransactionById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateTransaction")]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromForm] CreateTransactionDTO expDTO)
        {
            var createExpense = await _service.CreateTransaction(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromForm] UpdateTransactionDTO updateExp)
        {
            var rs = await _service.UpdateTransaction(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var rs = await _service.DeleteTransaction(id);
            return Ok(rs);
        }
    }
}
