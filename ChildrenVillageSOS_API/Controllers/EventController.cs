using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
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

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEventDTO searchEventDTO)
        {
            if (string.IsNullOrEmpty(searchEventDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _eventService.SearchEvents(searchEventDTO);
            return Ok(result);
        }

        [HttpGet("GetAllEventsIsDelete")]
        public async Task<IActionResult> GetAllEventsIsDelete()
        {
            var deletedEvents = await _eventService.GetAllEventIsDeleteAsync();
            return Ok(deletedEvents);
        }

        [HttpGet("GetEventDetails/{eventId}")]
        public async Task<IActionResult> GetEventDetails(int eventId)
        {
            var eventDetails = await _eventService.GetEventDetails(eventId);
            return Ok(eventDetails);
        }

        [HttpGet("GetEventById/{id}")]
        public async Task<ActionResult<EventResponseDTO>> GetEventById(int id)
        {
            var eventResponseDTO = await _eventService.GetEventById(id);
            if (eventResponseDTO == null)
            {
                return NotFound();  // Trả về 404 nếu không tìm thấy sự kiện
            }
            return Ok(eventResponseDTO);  // Trả về sự kiện với mã 200
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

        [HttpPut("RestoreEvent/{id}")]
        public async Task<IActionResult> RestoreEvent(int id)
        {
            var restoredChild = await _eventService.RestoreEvent(id);
            return Ok(restoredChild);
        }
    }
}
