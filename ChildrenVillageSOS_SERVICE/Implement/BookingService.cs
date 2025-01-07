using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
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
        public async Task<BookingResponse[]> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }
        public async Task<BookingResponse[]> GetBookingsWithSlotsByUserAsync(string userAccountId)
        {
            return await _bookingRepository.GetBookingsWithSlotsByUserAsync(userAccountId);
        }
        public async Task<bool> CreateBookingAsync(BookingRequest request)
        {
            
            var existingBooking = await _bookingRepository.GetBookingBySlotAsync(request.HouseId, request.Visitday, request.BookingSlotId);
            if (existingBooking != null)
            {
                throw new InvalidOperationException($"Slot {request.BookingSlotId} for {request.Visitday:yyyy-MM-dd} in house {request.HouseId} is already booked.");
            }

            var newBooking = new Booking
            {
                HouseId = request.HouseId,
                Visitday = request.Visitday,
                BookingSlotId = request.BookingSlotId,
                UserAccountId = request.UserAccountId,
                Status = "Pending",
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await _bookingRepository.AddAsync(newBooking);
            return true;
        }

        public async Task<Booking> CreateBooking(CreateBookingDTO createBooking)
        {
            var newBooking = new Booking
            {
                HouseId = createBooking.HouseId,
                UserAccountId = createBooking.UserAccountId,
                BookingSlotId = createBooking.BookingSlotId,
                Visitday = createBooking.Visitday,
                Status = "Pending",
                CreatedDate = DateTime.Now
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
            booking.IsDeleted = true;
            await _bookingRepository.UpdateAsync(booking);
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

        public async Task<Booking> RestoreBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }
            if (booking.IsDeleted == true) 
            {
                booking.IsDeleted = false;
                await _bookingRepository.UpdateAsync(booking);
            }
            return booking;
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
            booking.BookingSlotId = updateBooking.BookingSlotId;
            booking.Visitday = updateBooking.Visitday;
            booking.Status = updateBooking.Status;
            booking.ModifiedDate = DateTime.Now;
            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }
        public async Task<Booking> ConfirmBooking(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new Exception($"Booking with ID{id} is not found");
            }

            
            booking.Status = "Confirmed";
            booking.ModifiedDate = DateTime.Now;
            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }

        public async Task<List<Booking>> SearchBookings(SearchBookingDTO searchBookingDTO)
        {
            return await _bookingRepository.SearchBookings(searchBookingDTO);
        }
    }
}
