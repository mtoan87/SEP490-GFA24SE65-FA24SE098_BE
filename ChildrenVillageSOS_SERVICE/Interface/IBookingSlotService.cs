using ChildrenVillageSOS_DAL.DTO.BookingSlotDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IBookingSlotService
    {
        Task<IEnumerable<BookingSlot>> GetAllBookingSlots();
        Task<BookingSlot> GetBookingSlotById(int id);
        Task<BookingSlot> CreateBookingSlot(CreateBookingSlotDTO createBookingSlot);
        Task<BookingSlot> UpdateBookingSlot(int id, UpdateBookingSlotDTO updateBookingSlot);
        Task<BookingSlot> DeleteBookingSlot(int id);
        Task DeleteOrEnable(int id, bool isDeleted);
    }
}
