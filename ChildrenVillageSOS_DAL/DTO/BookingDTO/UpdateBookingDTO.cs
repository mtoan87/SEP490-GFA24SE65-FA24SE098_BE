using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingDTO
{
    public class UpdateBookingDTO
    {
        public string HouseId { get; set; }

        public string UserAccountId { get; set; }

        public DateTime Visitday { get; set; }

        public string Status { get; set; }

        public DateTime Starttime { get; set; }

        public DateTime Endtime { get; set; }

        public bool? IsDelete { get; set; }
    }
}
