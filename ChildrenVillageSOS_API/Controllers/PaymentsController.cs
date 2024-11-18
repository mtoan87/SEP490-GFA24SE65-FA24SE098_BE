using ChildrenVillageSOS_API.Helper;
using ChildrenVillageSOS_API.Model;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
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
        private readonly IWalletRepository _walletRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IChildRepository _childRepository;
        private readonly IFacilitiesWalletRepository _failitiesWalletRepository;
        private readonly IFoodStuffWalletRepository _foodStuffWalletRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletRepository;
        private readonly ISystemWalletRepository _systemWalletRepository;
        private readonly IHealthWalletRepository _healthWalletRepository;
        public PaymentsController(
            IPaymentService paymentService,
            IPaymentRepository paymentRepository,
            IDonationRepository donationRepository,
            IConfiguration configuration,
            IWalletRepository walletRepository,
            IEventRepository eventRepository,
            IChildRepository childRepository,
            IFacilitiesWalletRepository failitiesWalletRepository,
            IFoodStuffWalletRepository foodStuffWalletRepository,
            INecessitiesWalletRepository necessitiesWalletRepository,
            ISystemWalletRepository systemWalletRepository,
            IHealthWalletRepository healthWalletRepository)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
            _donationRepository = donationRepository;
            _configuration = configuration;
            _walletRepository = walletRepository;
            _eventRepository = eventRepository;
            _childRepository = childRepository;
            _failitiesWalletRepository = failitiesWalletRepository;
            _foodStuffWalletRepository = foodStuffWalletRepository;
            _necessitiesWalletRepository = necessitiesWalletRepository;
            _systemWalletRepository = systemWalletRepository;
            _healthWalletRepository = healthWalletRepository;
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
        public async Task<IActionResult> Return(
            [FromQuery] int vnp_TxnRef,
            [FromQuery] string vnp_ResponseCode,
            [FromQuery] int? eventId,
            [FromQuery] int walletId,
            [FromQuery] string? childId,
            [FromQuery] int? facilitiesWalletId,
            [FromQuery] int? necessitiesWalletId,
            [FromQuery] int? foodStuffWalletId,
            [FromQuery] int? systemWalletId,
            [FromQuery] int? healthWalletId)
        {
            try
            {                                                                    
                var donation = await _donationRepository.GetByIdAsync(vnp_TxnRef);                              
                if (donation == null)
                {
                    return NotFound($"Donate with Id:{vnp_TxnRef} not found!");
                }
                var payment = await _paymentService.GetPaymentByDonationIdAsync(donation.Id);
                if (payment == null)
                {
                    return NotFound($"Payment with Id:{eventId} not found!");
                }
               
                if (eventId.HasValue) 
                {
                    var editEvent = await _eventRepository.GetByIdAsync(eventId.Value);
                    decimal donationAmount = donation.Amount;
                    var newTotalAmount = (editEvent.CurrentAmount) + (donation.Amount);
                    editEvent.CurrentAmount = newTotalAmount;
                    await _eventRepository.UpdateAsync(editEvent);

                    if (editEvent == null)
                    {
                        return NotFound($"Event with Id:{eventId} not found!");
                    }
                    if (editEvent.FacilitiesWalletId == walletId)
                    {
                        await _walletRepository.UpdateFacilitiesWalletBudget(1, donationAmount);
                    }
                    else if (editEvent.FoodStuffWalletId == walletId)
                    {
                        await _walletRepository.UpdateFoodStuffWalletBudget(walletId, donationAmount);
                    }
                    else if (editEvent.NecessitiesWalletId == walletId)
                    {
                        await _walletRepository.UpdateNecessitiesWalletBudget(walletId, donationAmount);
                    }
                    else if (editEvent.HealthWalletId == walletId)
                    {
                        await _walletRepository.UpdateHealthWalletBudget(walletId, donationAmount);
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Invalid wallet ID." });
                    }
                }
                if (!string.IsNullOrEmpty(childId))
                {
                    var editChild = await _childRepository.GetByIdAsync(childId);
                    decimal donationAmountChild = donation.Amount;
                    var newTotalAmount = (editChild.CurrentAmount) + (donation.Amount);
                    editChild.CurrentAmount = newTotalAmount;
                    await _childRepository.UpdateAsync(editChild);
                    if (editChild == null)
                    {
                        return NotFound($"Child with Id:{childId} not found!");
                    }
                    if (editChild.FacilitiesWalletId == walletId)
                    {
                        await _walletRepository.UpdateFacilitiesWalletBudget(walletId, donationAmountChild);
                    }
                    else if (editChild.FoodStuffWalletId == walletId)
                    {
                        await _walletRepository.UpdateFoodStuffWalletBudget(walletId, donationAmountChild);
                    }
                    else if (editChild.NecessitiesWalletId == walletId)
                    {
                        await _walletRepository.UpdateNecessitiesWalletBudget(walletId, donationAmountChild);
                    }
                    else if (editChild.HealthWalletId == walletId)
                    {
                        await _walletRepository.UpdateHealthWalletBudget(walletId, donationAmountChild);
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Invalid wallet ID." });
                    }
                }
                if (facilitiesWalletId.HasValue)
                {
                    var facilitiesWallet = await _failitiesWalletRepository.GetByIdAsync(facilitiesWalletId.Value);
                    facilitiesWallet.Budget += donation.Amount;
                    await _failitiesWalletRepository.UpdateAsync(facilitiesWallet);
                }
                if (foodStuffWalletId.HasValue)
                {
                    var foodStuffWallet = await _foodStuffWalletRepository.GetByIdAsync(foodStuffWalletId.Value);
                    foodStuffWallet.Budget += donation.Amount;
                    await _foodStuffWalletRepository.UpdateAsync(foodStuffWallet);
                }
                if (necessitiesWalletId.HasValue)
                {
                    var necessitiesWallet = await _necessitiesWalletRepository.GetByIdAsync(necessitiesWalletId.Value);
                    necessitiesWallet.Budget += donation.Amount;
                    await _necessitiesWalletRepository.UpdateAsync(necessitiesWallet);
                }
                if (systemWalletId.HasValue)
                {
                    var systemWallet = await _systemWalletRepository.GetByIdAsync(systemWalletId.Value);
                    systemWallet.Budget += donation.Amount;
                    await _systemWalletRepository.UpdateAsync(systemWallet);
                }
                if (systemWalletId.HasValue)
                {
                    var systemWallet = await _systemWalletRepository.GetByIdAsync(systemWalletId.Value);
                    systemWallet.Budget += donation.Amount;
                    await _systemWalletRepository.UpdateAsync(systemWallet);
                }
                if (healthWalletId.HasValue)
                {
                    var healthWallet = await _healthWalletRepository.GetByIdAsync(healthWalletId.Value);
                    healthWallet.Budget += donation.Amount;
                    await _healthWalletRepository.UpdateAsync(healthWallet);
                }
                if (vnp_ResponseCode == "00") 
                {
                   
                    donation.Status = "Paid";
                    payment.Status = "Paid";
                    await _donationRepository.UpdateAsync(donation);
                    await _paymentRepository.UpdateAsync(payment);
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
                    donation.Status = "Cancelled";
                    payment.Status = "Cancelled";
                    await _donationRepository.UpdateAsync(donation);
                    await _paymentRepository.UpdateAsync(payment);                
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
