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
                            && b.Status == "Confirmed")
                .FirstOrDefaultAsync();
        }
    }
}
