using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.BookingSlotDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotController : ControllerBase
    {
        private readonly IBookingSlotService _bookingSlotService;
        public BookingSlotController(IBookingSlotService bookingSlotService)
        {
            _bookingSlotService = bookingSlotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingSlots()
        {
            var slot = await _bookingSlotService.GetAllBookingSlots();
            return Ok(slot);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookingById(int Id)
        {
            var slot = await _bookingSlotService.GetBookingSlotById(Id);
            return Ok(slot);
        }

        [HttpPost]
        public async Task<ActionResult<BookingSlot>> CreateBookingSlot(CreateBookingSlotDTO createBookingSlotDTO)
        {
            var slot = await _bookingSlotService.CreateBookingSlot(createBookingSlotDTO);
            return Ok(slot);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBookingSlot(int Id, UpdateBookingSlotDTO updateBookingSlotDTO)
        {
            var slot = await _bookingSlotService.UpdateBookingSlot(Id, updateBookingSlotDTO);
            return Ok(slot);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreBookingSlot(int Id)
        {
            await _bookingSlotService.RestoreBookingSlot(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBookingSlot(int Id)
        {
            var booking = await _bookingSlotService.DeleteBookingSlot(Id);
            return Ok(booking);
        }
    }
}
