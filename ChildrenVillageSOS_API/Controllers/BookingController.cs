//using ChildrenVillageSOS_DAL.DTO.BookingDTO;
//using ChildrenVillageSOS_DAL.Models;
//using ChildrenVillageSOS_SERVICE.Interface;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ChildrenVillageSOS_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookingController : ControllerBase
//    {
//        private readonly IBookingService _bookingService;
//        public BookingController(IBookingService bookingService)
//        {
//            _bookingService = bookingService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllBookings()
//        {
//            var booking = await _bookingService.GetAllBookings();
//            return Ok(booking);
//        }

//        [HttpGet("{Id}")]
//        public async Task<IActionResult> GetBookingById(int Id)
//        {
//            var booking = await _bookingService.GetBookingById(Id);
//            return Ok(booking);
//        }

//        [HttpPost]
//        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDTO createBookingDTO)
//        {
//            var booking = await _bookingService.CreateBooking(createBookingDTO);
//            return Ok(booking);
//        }

//        [HttpPut("{Id}")]
//        public async Task<IActionResult> UpdateBooking(int Id, UpdateBookingDTO updateBookingDTO)
//        {
//            var booking = await _bookingService.UpdateBooking(Id, updateBookingDTO);
//            return Ok(booking);
//        }

//        [HttpDelete("{Id}")]
//        public async Task<IActionResult> DeleteBooking(int Id)
//        {
//            var booking = await _bookingService.DeleteBooking(Id);
//            return Ok(booking);
//        }
//    }
//}
