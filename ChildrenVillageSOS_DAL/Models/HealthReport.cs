using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class HealthReport
{
    public int Id { get; set; }

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

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child? Child { get; set; }
}
