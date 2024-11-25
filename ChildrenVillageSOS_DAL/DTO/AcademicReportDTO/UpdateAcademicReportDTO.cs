using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.AcademicReportDTO
{
    public class UpdateAcademicReportDTO
    {
        public string Diploma { get; set; }

        public string ChildId { get; set; }

        public decimal? Gpa { get; set; }

    }
}
