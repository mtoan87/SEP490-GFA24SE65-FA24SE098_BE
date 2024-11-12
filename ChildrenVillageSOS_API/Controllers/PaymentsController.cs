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
            var paymentUrl = await _paymentService.CreatePayment(request);           
            return Ok(new { url = paymentUrl });
        }
        [HttpGet("return")]
        public async Task<IActionResult> Return([FromQuery] int vnp_TxnRef, [FromQuery] string vnp_ResponseCode, [FromQuery] string vnp_SecureHash)
        {
            try
            {
                // Kiểm tra chữ ký bảo mật (SecureHash) để xác minh tính hợp lệ của phản hồi
                
                var payLib = new VnPayLibrary();
               
                var url = _configuration.GetValue<string>("VNPay:Url");
                var returnUrl = _configuration.GetValue<string>("VNPay:ReturnUrl");
                var tmnCode = _configuration.GetValue<string>("VNPay:TmnCode");
                var hashSecret = _configuration.GetValue<string>("VNPay:HashSecret");

                var donation = await _donationRepository.GetByIdAsync(vnp_TxnRef);
                payLib.AddRequestData("vnp_Version", "2.1.0");
                payLib.AddRequestData("vnp_Command", "pay");
                payLib.AddRequestData("vnp_TmnCode", tmnCode);
                payLib.AddRequestData("vnp_Amount", (donation.Amount * 100).ToString()); // Multiply by 100 for VNPay
                payLib.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                payLib.AddRequestData("vnp_CurrCode", "VND");
                payLib.AddRequestData("vnp_IpAddr", "192.168.1.105");
                payLib.AddRequestData("vnp_Locale", "vn");
                payLib.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}");
                payLib.AddRequestData("vnp_OrderType", "donation");
                payLib.AddRequestData("vnp_ReturnUrl", returnUrl);
                payLib.AddRequestData("vnp_TxnRef", donation.Id.ToString());
                payLib.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
                string paymentUrl = payLib.CreateRequestUrl(url, hashSecret);



                if (donation == null)
                {
                    return NotFound("Donation not found.");
                }

                var payment = await _paymentService.GetPaymentByDonationIdAsync(donation.Id);
                if (payment == null)
                {
                    return NotFound("Payment not found.");
                }

                // Kiểm tra mã phản hồi
                if (vnp_ResponseCode == "00") // "00" là mã thanh toán thành công
                {
                    // Cập nhật trạng thái Donation và Payment thành "Paid"
                    donation.Status = "Paid";
                    payment.Status = "Paid";
                    await _donationRepository.UpdateAsync(donation);
                    await _paymentRepository.UpdateAsync(payment);

                    // Điều hướng đến trang thành công
                    return Ok(new
                    {
                        success = true,
                        message = "Payment successful.",
                        donationId = donation.Id,
                        paymentId = payment.Id,
                        amount = donation.Amount,
                        status = "Paid"
                    });
                }
                else
                {
                    // Cập nhật trạng thái Donation và Payment thành "Cancelled"
                    donation.Status = "Cancelled";
                    payment.Status = "Cancelled";
                    await _donationRepository.UpdateAsync(donation);
                    await _paymentRepository.UpdateAsync(payment);

                    // Điều hướng đến trang đơn hàng
                    return Ok(new
                    {
                        success = false,
                        message = "Payment failed.",
                        donationId = donation.Id,
                        paymentId = payment.Id,
                        amount = donation.Amount,
                        status = "Cancelled"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
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
        [HttpPut]
        [Route("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var rs = await _paymentService.SoftDelete(id);
            return Ok(rs);
        }
    }
}
