using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class BookingRepository : RepositoryGeneric<Booking>, IBookingRepository
    {
        public BookingRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<Booking?> GetBookingBySlotAsync(string houseId, DateOnly visitDay, int bookingSlotId)
        {
            return await _context.Bookings
                .Where(b => b.HouseId == houseId
                            && b.Visitday.HasValue
                            && b.Visitday.Value == visitDay
                            && b.BookingSlotId == bookingSlotId
                            && (b.IsDeleted == false) // Explicitly check for non-deleted bookings
                            && (b.Status == "Confirmed" || b.Status == "Pending")) // Check for valid statuses
                .FirstOrDefaultAsync();
        }

        public async Task<BookingResponse[]> GetBookingsWithSlotsByUserAsync(string userAccountId)
        {
            var result =  (from booking in _context.Bookings
                                join slot in _context.BookingSlots
                                on booking.BookingSlotId equals slot.Id
                                where booking.UserAccountId == userAccountId
                                      && booking.IsDeleted == false
                                select new BookingResponse
                                {
                                    Id = booking.Id,
                                    HouseId = booking.HouseId,      
                                    HouseName = booking.House.HouseName,
                                    Visitday = booking.Visitday,
                                    BookingSlotId = booking.BookingSlotId,
                                    SlotStartTime = slot.StartTime.HasValue ? slot.StartTime.Value.ToString("hh\\:mm") : null,
                                    SlotEndTime = slot.EndTime.HasValue ? slot.EndTime.Value.ToString("hh\\:mm") : null,
                                    Status = booking.Status
                                }).ToArray();

            return result;
        }

        public async Task<BookingResponse[]> GetAllBookingsAsync()
        {
            var result = (from booking in _context.Bookings
                          join slot in _context.BookingSlots
                          on booking.BookingSlotId equals slot.Id
                          select new BookingResponse
                          {
                              Id = booking.Id,
                              HouseId = booking.HouseId,
                              HouseName = booking.House.HouseName,
                              Visitday = booking.Visitday,
                              BookingSlotId = booking.BookingSlotId,
                              SlotStartTime = slot.StartTime.HasValue ? slot.StartTime.Value.ToString("hh\\:mm") : null,
                              SlotEndTime = slot.EndTime.HasValue ? slot.EndTime.Value.ToString("hh\\:mm") : null,
                              Status = booking.Status
                          }).ToArray();

            return result;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            // Chuyển đổi DateTime thành DateOnly để so sánh
            var start = DateOnly.FromDateTime(startDate);
            var end = DateOnly.FromDateTime(endDate);

            return await _context.Bookings
                .Where(b => b.IsDeleted != true &&
                            b.Visitday.HasValue && // Chỉ kiểm tra nếu Visitday có giá trị
                            b.Visitday.Value >= start &&
                            b.Visitday.Value <= end)
                .ToListAsync();
        }
    }
}
