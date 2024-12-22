using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.SubjectDetailsDTO
{
    public class UpdateSubjectDetailsDTO
    {
        public int AcademicReportId { get; set; }

        public string? SubjectName { get; set; }

        public decimal? Score { get; set; }

        public string? Remarks { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
