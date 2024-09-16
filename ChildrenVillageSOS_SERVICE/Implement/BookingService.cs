using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<Booking> CreateBooking(CreateBookingDTO createBooking)
        {
            var newBooking = new Booking
            {
                HouseId = createBooking.HouseId,
                UserAccountId = createBooking.UserAccountId,
                Visitday = createBooking.Visitday,
                Status = createBooking.Status,
                Starttime = createBooking.Starttime,
                Endtime = createBooking.Endtime,
                IsDelete = createBooking.IsDelete,
            };
            await _bookingRepository.AddAsync(newBooking);
            return newBooking;
        }

        public async Task<Booking> DeleteBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }
            await _bookingRepository.RemoveAsync(booking);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _bookingRepository.GetByIdAsync(id);
        }

        public async Task<Booking> UpdateBooking(int id, UpdateBookingDTO updateBooking)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }
            
            booking.HouseId = updateBooking.HouseId;
            booking.UserAccountId = updateBooking.UserAccountId;
            booking.Visitday = updateBooking.Visitday;
            booking.Status = updateBooking.Status;
            booking.Starttime = updateBooking.Starttime;
            booking.Endtime = updateBooking.Endtime;
            booking.IsDelete = updateBooking.IsDelete;
            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }
    }
}
