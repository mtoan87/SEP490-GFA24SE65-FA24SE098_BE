using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.AcademicReportDTO
{
    public class CreateAcademicReportDTO
    {
        public string Diploma { get; set; }

        public string ChildId { get; set; }

        public decimal? Gpa { get; set; }

    }
}
