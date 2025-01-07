using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ActivityDTO
{
    public class ActivitySummaryDTO
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Address { get; set; }
        public string? ActivityType { get; set; }
        public string? Organizer { get; set; }
        public string Status { get; set; }
    }
}
