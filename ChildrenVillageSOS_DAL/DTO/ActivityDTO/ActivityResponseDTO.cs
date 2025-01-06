using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ActivityDTO
{
    public class ActivityResponseDTO
    {
        public int Id { get; set; }

        public string ActivityName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Address { get; set; }

        public string? VillageId { get; set; }

        public string? ActivityType { get; set; }

        public string? TargetAudience { get; set; }

        public string? Organizer { get; set; }

        public string Status { get; set; } = null!;

        public int? EventId { get; set; }

        public decimal? Budget { get; set; }

        public string? Feedback { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string[] ImageUrls { get; set; } = new string[0];
    }
}
