using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var exp = await _paymentService.GetAllPayments();
            return Ok(exp);
        }
        [HttpGet("GetPaymentById/{Id}")]
        public async Task<IActionResult> GetPaymentById(int Id)
        {
            var exp = await _paymentService.GetPaymentById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<ActionResult<Income>> CreatePayment([FromForm] CreatePaymentDTO expDTO)
        {
            var createExpense = await _paymentService.CreatePayment(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(int id, [FromForm] UpdatePaymentDTO updateExp)
        {
            var rs = await _paymentService.UpdatePayment(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var rs = await _paymentService.DeletePayment(id);
            return Ok(rs);
        }
    }
}
