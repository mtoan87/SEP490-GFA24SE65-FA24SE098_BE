using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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

        public async Task<BookingDetailsDTO?> GetBookingDetailsAsync(int bookingId)
        {
            var result = await (from b in _context.Bookings
                                join h in _context.Houses on b.HouseId equals h.Id
                                join v in _context.Villages on h.VillageId equals v.Id
                                join bs in _context.BookingSlots on b.BookingSlotId equals bs.Id
                                join ua in _context.UserAccounts on b.UserAccountId equals ua.Id
                                where b.Id == bookingId
                                select new BookingDetailsDTO
                                {
                                    VillageName = v.VillageName,
                                    VillageLocation = v.Location,
                                    HouseName = h.HouseName,
                                    HouseLocation = h.Location,
                                    StartTime = bs.StartTime,
                                    EndTime = bs.EndTime,
                                    Visitday = b.Visitday,
                                    UserEmail = ua.UserEmail
                                }).FirstOrDefaultAsync();

            return result;
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

        public async Task<List<Booking>> SearchBookings(SearchBookingDTO searchBookingDTO)
        {
            var query = _context.Bookings.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchBookingDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchBookingDTO.SearchTerm) ||
                     x.HouseId.Contains(searchBookingDTO.SearchTerm) ||
                     x.Status.Contains(searchBookingDTO.SearchTerm) ||
                     x.Visitday.Value.ToString("yyyy-MM-dd").Contains(searchBookingDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }

    }
}
