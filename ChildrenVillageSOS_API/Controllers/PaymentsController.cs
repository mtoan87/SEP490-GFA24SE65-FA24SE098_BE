using ChildrenVillageSOS_API.Helper;
using ChildrenVillageSOS_API.Model;
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
        [HttpGet]
        [Route("Success")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] VNPayCallbackDto callback)
        {
            string hashSecret = _configuration["VNPay:HashSecret"];
            string returnUrl = _configuration["VNPay:ReturnUrl"];
            string tmnCode = _configuration["VNPay:TmnCode"];
            string url = _configuration["VNPay:Url"];
            // Bước 1: Xác thực chữ ký từ VNPay
            bool isValidSignature = VnPayHelper.VerifySignature(callback, hashSecret);
            if (!isValidSignature)
            {
                return BadRequest("Invalid signature.");
            }

            // Bước 2: Kiểm tra mã phản hồi từ VNPay
            if (callback.vnp_ResponseCode != "00")
            {
                return BadRequest("Payment failed.");
            }

            // Bước 3: Truy xuất Donation và Payment dựa trên vnp_TxnRef
            var donationId = int.Parse(callback.vnp_TxnRef);
            var donation = await _donationRepository.GetByIdAsync(donationId);
            var payment = await _paymentRepository.GetPaymentByDonationIdAsync(donationId);

            if (donation != null && payment != null)
            {
                // Cập nhật trạng thái Donation và Payment thành "Paid"
                donation.Status = "Paid";
                payment.Status = "Paid";
                await _donationRepository.UpdateAsync(donation);
                await _paymentRepository.UpdateAsync(payment);
            }

            return Redirect(""); // Hoặc trả về phản hồi JSON nếu cần
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
