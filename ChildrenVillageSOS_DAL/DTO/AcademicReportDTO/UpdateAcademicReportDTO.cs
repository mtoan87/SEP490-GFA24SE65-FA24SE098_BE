using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.AcademicReportDTO
{
    public class UpdateAcademicReportDTO
    {
        public string? Diploma { get; set; }

        public string? SchoolLevel { get; set; }

        public string? ChildId { get; set; }

        public string? SchoolId { get; set; }

        public decimal? Gpa { get; set; }

        public string? SchoolReport { get; set; }

        public string? Semester { get; set; }

        public string? AcademicYear { get; set; }

        public string? Remarks { get; set; }

        public string? Achievement { get; set; }

        public string? Status { get; set; }

        public string? Class { get; set; }

        public string? Feedback { get; set; }

        public string? ModifiedBy { get; set; }

    }
}
