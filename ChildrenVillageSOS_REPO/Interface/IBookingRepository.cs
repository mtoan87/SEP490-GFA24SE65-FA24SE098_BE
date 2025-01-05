using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IBookingRepository : IRepositoryGeneric<Booking>
    {
        Task<Booking?> GetBookingBySlotAsync(string houseId, DateOnly visitDay, int bookingSlotId);
        Task<BookingResponse[]> GetBookingsWithSlotsByUserAsync(string userAccountId);

        Task<BookingResponse[]> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
    }
}
