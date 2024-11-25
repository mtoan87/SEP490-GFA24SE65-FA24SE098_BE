using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HealthReportDTO
{
    public class CreateHealthReportDTO
    {
        public string ChildId { get; set; }

        public string NutritionalStatus { get; set; }

        public string MedicalHistory { get; set; }

        public string HealthCertificate { get; set; }

        public string VaccinationStatus { get; set; }

        public string Weight { get; set; }

        public string Height { get; set; }
    }
}
