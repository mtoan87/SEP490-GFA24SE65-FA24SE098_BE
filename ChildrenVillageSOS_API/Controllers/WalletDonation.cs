﻿using ChildrenVillageSOS_DAL.DTO.DonationDTO;
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
        public async Task<IActionResult> DonateFacilitiesWallet([FromBody] DonateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }       
            var paymentUrl = await _paymentService.CreateFacilitiesWalletPayment(request);           
            return Ok(new { url = paymentUrl });
        }
       
        [HttpPost("DonateNecessitiesWallet")]
        public async Task<IActionResult> DonateNecessitiesWallet([FromBody] DonateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _paymentService.CreateNecesstiesWalletPayment(request);
            return Ok(new { url = paymentUrl });
        }
        [HttpPost("DonateFoodStuffWallet")]
        public async Task<IActionResult> DonateFoodStuffWallet([FromBody] DonateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _paymentService.CreateFoodStuffWalletPayment(request);
            return Ok(new { url = paymentUrl });
        }
        [HttpPost("DonateHealthWallet")]
        public async Task<IActionResult> DonateHealthWallet([FromBody] DonateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _paymentService.CreateHealthWalletPayment(request);
            return Ok(new { url = paymentUrl });
        }
        [HttpPost("DonateSystemWallet")]
        public async Task<IActionResult> DonateSystemWallet([FromBody] DonateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _paymentService.CreateSystemWalletPayment(request);
            return Ok(new { url = paymentUrl });
        }
    }
}
