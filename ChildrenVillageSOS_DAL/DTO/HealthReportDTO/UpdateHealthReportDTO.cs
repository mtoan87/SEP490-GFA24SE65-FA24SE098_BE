using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HealthReportDTO
{
    public class UpdateHealthReportDTO
    {
        public string ChildId { get; set; }

        public string NutritionalStatus { get; set; }

        public string MedicalHistory { get; set; }

        public string HealthCertificate { get; set; }

        public string VaccinationStatus { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }
    }
}
