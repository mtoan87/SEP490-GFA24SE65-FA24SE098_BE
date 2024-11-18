using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingSlotDTO
{
    public class CreateBookingSlotDTO
    {
        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Status { get; set; } = null!;

        public int SlotTime { get; set; }
    }
}
