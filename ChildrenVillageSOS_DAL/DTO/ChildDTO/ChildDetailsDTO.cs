using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.ChildDTO
{
    public class ChildDetailsDTO
    {
        public string Id { get; set; }
        public string ChildName { get; set; }
        public string HealthStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Status { get; set; }
        public string HouseName { get; set; }
        public string SchoolName { get; set; }
        public List<HealthReportSummaryDTO> HealthReports { get; set; }
        public List<AcademicReportSummaryDTO> AcademicReports { get; set; }
        public List<ChildProgressSummaryDTO> ChildProgresses { get; set; }
    }
}
