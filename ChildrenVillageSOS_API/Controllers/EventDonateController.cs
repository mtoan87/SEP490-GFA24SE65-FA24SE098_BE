using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDonateController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventDonateController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPut("EventDonateFacilitiesWallet")]
        public async Task<IActionResult> DonateFacilitiesWallet(int id,[FromBody] EventDonateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _eventService.DonateEvent(id,request);
            return Ok(new { url = paymentUrl });
        }
    }
}
