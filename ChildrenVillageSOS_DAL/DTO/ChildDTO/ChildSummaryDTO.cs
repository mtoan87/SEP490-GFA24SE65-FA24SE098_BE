using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ChildDTO
{
    public class ChildSummaryDTO
    {
        public string Id { get; set; } = null!;

        public string? ChildName { get; set; }

        public string? HealthStatus { get; set; }

        public string? Gender { get; set; }

        public DateTime? Dob { get; set; }

        public string? AcademicYear { get; set; }

        public string? Semester { get; set; }

        public string? Class { get; set; }
    }
}
