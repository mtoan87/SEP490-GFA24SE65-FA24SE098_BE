using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.BookingDTO
{
    public class BookingDetailsDTO
    {
        public string? VillageName { get; set; }
        public string? VillageLocation { get; set; }
        public string HouseName { get; set; } = null!;
        public string? HouseLocation { get; set; }
        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
        public DateOnly? Visitday { get; set; }
        public string? UserEmail { get; set; }
    }
}
