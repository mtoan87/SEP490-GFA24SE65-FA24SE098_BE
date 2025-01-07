using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IBookingService
    {
        Task<Booking> ConfirmBooking(int id);
        Task<bool> CreateBookingAsync(BookingRequest request);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking> GetBookingById(int id);
        Task<Booking> CreateBooking(CreateBookingDTO createBooking);
        Task<Booking> UpdateBooking(int id, UpdateBookingDTO updateBooking);
        Task<Booking> DeleteBooking(int id);
        Task<Booking> RestoreBooking(int id);
        Task<BookingResponse[]> GetBookingsWithSlotsByUserAsync(string userAccountId);

        Task<BookingResponse[]> GetAllBookingsAsync();
        Task<List<Booking>> SearchBookings(SearchBookingDTO searchBookingDTO);
    }
}
