using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEvent()
        {
            var even = await _eventService.GetAllEvent();
            return Ok(even);
        }
        [HttpGet("GetEventById/{Id}")]
        public async Task<IActionResult> GetEventById(int Id)
        {
            var even = await _eventService.GetEventById(Id);
            return Ok(even);
        }
        [HttpPost]
        [Route("CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent([FromForm] CreateEventDTO creEvent)
        {
            var newEvent = await _eventService.CreateEvent(creEvent);
            return Ok(newEvent);    
        }
        [HttpPut]
        [Route("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int id, [FromForm] UpdateEventDTO updateEvent)
        {
            var editEvent = await _eventService.UpdateEvent(id, updateEvent);
            return Ok(editEvent);
        }
        [HttpDelete]
        [Route("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var even = await _eventService.DeleteEvent(id);
            return Ok(even);
        }
    }
}
