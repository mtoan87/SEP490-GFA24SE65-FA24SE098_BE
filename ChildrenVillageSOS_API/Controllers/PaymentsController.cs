using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
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
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IConfiguration _configuration;
        
        public PaymentsController(IPaymentService paymentService, IPaymentRepository paymentRepository, IDonationRepository donationRepository, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
            _donationRepository = donationRepository;
            _configuration = configuration;
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
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Tạo URL thanh toán VNPay
            var paymentUrl = await _paymentService.CreatePayment(request);

            // Trả về URL để người dùng redirect đến VNPay
            return Ok(new { url = paymentUrl });
        }
        //[HttpGet("return")]
        //public async Task<IActionResult> VNPayReturn()
        //{
        //    // Step 1: Extract parameters from the query string
        //    var vnp_TxnRef = Request.Query["vnp_TxnRef"].ToString();
        //    var vnp_ResponseCode = Request.Query["vnp_ResponseCode"].ToString();
        //    var vnp_SecureHash = Request.Query["vnp_SecureHash"].ToString();

        //    // Step 2: Verify the secure hash using VNPayLibrary
        //    var vnpay = new VnPayLibrary();
        //    foreach (var key in Request.Query.Keys)
        //    {
        //        if (!key.Equals("vnp_SecureHash"))
        //        {
        //            vnpay.AddResponseData(key, Request.Query[key]);
        //        }
        //    }
        //    var hashSecret = _configuration["VNPay:HashSecret"];
        //    var isValidSignature = vnpay.ValidateSignature(vnp_SecureHash, hashSecret);

        //    if (!isValidSignature)
        //    {
        //        return BadRequest("Invalid VNPay signature.");
        //    }

        //    // Step 3: Check the transaction status
        //    if (vnp_ResponseCode == "00") // Success response code
        //    {
        //        var donationId = int.Parse(vnp_TxnRef);

        //        // Step 4: Update Payment and Donation status
        //        var payment = await _paymentRepository.GetPaymentByDonationIdAsync(donationId);
        //        if (payment != null)
        //        {
        //            payment.Status = "Paid";
        //            await _paymentRepository.UpdateAsync(payment);
        //        }

        //        var donation = await _donationRepository.GetByIdAsync(donationId);
        //        if (donation != null)
        //        {
        //            donation.Status = "Paid";
        //            await _donationRepository.UpdateAsync(donation);
        //        }

        //        return Ok("Payment and Donation updated successfully.");
        //    }
        //    else
        //    {
        //        return BadRequest("Payment was not successful.");
        //    }
        //}
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
