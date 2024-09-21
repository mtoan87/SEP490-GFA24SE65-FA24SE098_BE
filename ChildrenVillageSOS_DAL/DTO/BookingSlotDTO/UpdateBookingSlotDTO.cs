using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingSlotDTO
{
    public class UpdateBookingSlotDTO
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Status { get; set; } = null!;

        public int SlotTime { get; set; }
    }
}
