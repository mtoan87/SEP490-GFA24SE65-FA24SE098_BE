using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HealthReportDTO
{
    public class HealthReportSummaryDTO
    {
        public int Id { get; set; }
        public string NutritionalStatus { get; set; }
        public string MedicalHistory { get; set; }
        public string VaccinationStatus { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string HealthStatus { get; set; }
        public string Illnesses { get; set; }
        public string Allergies { get; set; }

    }
}
