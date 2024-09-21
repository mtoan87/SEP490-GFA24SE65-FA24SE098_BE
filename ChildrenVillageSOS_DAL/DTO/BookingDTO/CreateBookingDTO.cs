using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingDTO
{
    public class CreateBookingDTO
    {
        public string? HouseId { get; set; }

        public string? UserAccountId { get; set; }

        public int? BookingSlotId { get; set; }

        public DateTime Visitday { get; set; }

        public string Status { get; set; } = null!;
    }
}
