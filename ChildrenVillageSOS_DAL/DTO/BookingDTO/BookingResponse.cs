using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingDTO
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public string HouseId { get; set; }
        public string HouseName { get; set; } // Tên nhà
        public DateOnly? Visitday { get; set; }
        public int? BookingSlotId { get; set; }
        public string SlotStartTime { get; set; } // Thời gian bắt đầu
        public string SlotEndTime { get; set; }   // Thời gian kết thúc
        public string Status { get; set; }
       
    }
}
