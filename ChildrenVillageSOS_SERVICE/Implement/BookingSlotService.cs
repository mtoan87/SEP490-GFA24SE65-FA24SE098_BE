using ChildrenVillageSOS_DAL.DTO.BookingSlotDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class BookingSlotService : IBookingSlotService
    {
        private readonly IBookingSlotRepository _bookingSlotRepository;
        public BookingSlotService(IBookingSlotRepository bookingSlotRepository)
        {
            _bookingSlotRepository = bookingSlotRepository;
        }

        public async Task<BookingSlot> CreateBookingSlot(CreateBookingSlotDTO createBookingSlot)
        {
            var slot = new BookingSlot
            {
                StartTime = createBookingSlot.StartTime,
                EndTime = createBookingSlot.EndTime,
                Status = createBookingSlot.Status,
                SlotTime = createBookingSlot.SlotTime,
            };
            await _bookingSlotRepository.AddAsync(slot);
            return slot;
        }

        public async Task<BookingSlot> DeleteBookingSlot(int id)
        {
            var slot = await _bookingSlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                throw new Exception($"Booking slot with ID{id} is not found");
            }
            await _bookingSlotRepository.RemoveAsync(slot);
            return slot;
        }

        public async Task DeleteOrEnable(int id, bool isDeleted)
        {
            var slot = await _bookingSlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                throw new Exception($"Booking slot with ID{id} is not found");
            }
            slot.IsDeleted = isDeleted;
            await _bookingSlotRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingSlot>> GetAllBookingSlots()
        {
            return await _bookingSlotRepository.GetAllAsync();
        }

        public async Task<BookingSlot> GetBookingSlotById(int id)
        {
            return await _bookingSlotRepository.GetByIdAsync(id);
        }

        public async Task<BookingSlot> UpdateBookingSlot(int id, UpdateBookingSlotDTO updateBookingSlot)
        {
            var slot = await _bookingSlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                throw new Exception($"Booking slot with ID{id} is not found");
            }
            slot.StartTime = updateBookingSlot.StartTime;
            slot.EndTime = updateBookingSlot.EndTime;
            slot.SlotTime = updateBookingSlot.SlotTime;
            slot.Status = updateBookingSlot.Status;

            await _bookingSlotRepository.UpdateAsync(slot);
            return slot;
        }
    }
}
