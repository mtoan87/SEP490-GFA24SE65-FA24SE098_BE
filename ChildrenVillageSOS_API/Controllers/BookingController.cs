using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("GetAllBookingsWithSlotsInformation")]
        public async Task<IActionResult> GetAllBookingsWithSlotsInformation()
        {
            var booking = await _bookingService.GetAllBookingsAsync();
            return Ok(booking);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var booking = await _bookingService.GetAllBookings();
            return Ok(booking);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBookingById(int Id)
        {
            var booking = await _bookingService.GetBookingById(Id);
            return Ok(booking);
        }

        [HttpGet("GetBookingsWithSlotsByUserAccountId")]
        public async Task<IActionResult> GetBookingsWithSlots(string userId)
        {
            var bookings = await _bookingService.GetBookingsWithSlotsByUserAsync(userId);

            if (bookings == null || !bookings.Any())
            {
                return NotFound(new
                {
                    success = false,
                    message = "No bookings found for this user."
                });
            }

            return Ok(new
            {
                success = true,
                data = bookings
            });
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            try
            {
                var result = await _bookingService.CreateBookingAsync(request);
                if (result)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Booking created successfully."
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = "Failed to create booking."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking([FromForm]CreateBookingDTO createBookingDTO)
        {
            var booking = await _bookingService.CreateBooking(createBookingDTO);
            return Ok(booking);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBooking(int Id, UpdateBookingDTO updateBookingDTO)
        {
            var booking = await _bookingService.UpdateBooking(Id, updateBookingDTO);
            return Ok(booking);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreBooking(int Id)
        {
            await _bookingService.RestoreBooking(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBooking(int Id)
        {
            var booking = await _bookingService.DeleteBooking(Id);
            return Ok(booking);
        }
        [HttpPut("ConfirmBooking")]
        public async Task<IActionResult> ConfirmBooking(int Id)
        {
            var booking = await _bookingService.ConfirmBooking(Id);
            return Ok(booking);
        }
    }
}
