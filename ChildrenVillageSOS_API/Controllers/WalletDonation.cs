using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletDonation : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public WalletDonation(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("DonateFacilitiesWallet")]
        public async Task<IActionResult> DonateFacilitiesWallet([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }       
            var paymentUrl = await _paymentService.CreateFacilitiesWalletPayment(request);           
            return Ok(new { url = paymentUrl });
        }
    }
}
