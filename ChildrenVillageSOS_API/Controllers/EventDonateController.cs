using ChildrenVillageSOS_DAL.DTO.ChildDTO;
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
        private readonly IChildService _childService;
        public EventDonateController(IEventService eventService, IChildService childService)
        {
            _eventService = eventService;
            _childService = childService;
        }

        [HttpPut("EventDonate")]
        public async Task<IActionResult> DonateEvent(int id,[FromBody] EventDonateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _eventService.DonateEvent(id,request);
            return Ok(new { url = paymentUrl });
        }

        [HttpPut("ChildDonate")]
        public async Task<IActionResult> DonateChild(string id, [FromBody] ChildDonateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var paymentUrl = await _childService.DonateChild(id, request);
            return Ok(new { url = paymentUrl });
        }
    }
}
