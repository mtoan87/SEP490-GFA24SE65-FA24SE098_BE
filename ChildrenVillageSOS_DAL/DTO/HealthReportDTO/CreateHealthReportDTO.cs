using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.DTO.HealthReportDTO
{
    public class CreateHealthReportDTO
    {
        public string? ChildId { get; set; }

        public string? NutritionalStatus { get; set; }

        public string? MedicalHistory { get; set; }

        public string? VaccinationStatus { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public DateTime? CheckupDate { get; set; }

        public string? DoctorName { get; set; }

        public string? Recommendations { get; set; }

        public string? HealthStatus { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public string? Illnesses { get; set; }

        public string? Allergies { get; set; }

        public string? HealthCertificate { get; set; }

        public string? Status { get; set; }

        public string? CreatedBy { get; set; }

    }
}
