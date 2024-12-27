using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.AcademicReportDTO
{
    public class AcademicReportSummaryDTO
    {
        public int Id { get; set; }
        public string Diploma { get; set; }
        public string SchoolLevel { get; set; }
        public decimal? Gpa { get; set; }
        public string Semester { get; set; }
        public string AcademicYear { get; set; }
        public string Achievement { get; set; }
        public List<SubjectSummaryDTO> SubjectDetails { get; set; }
    }
}
