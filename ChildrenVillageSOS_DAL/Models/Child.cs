using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child : ISoftDelete
{
    public string Id { get; set; } = null!;

    public string ChildName { get; set; } = null!;

    public string HealthStatus { get; set; } = null!;

    public string? HouseId { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? BirthCertificate { get; set; }

    public string? CitizenIdentification { get; set; }

    public int? EventId { get; set; }

    public virtual ICollection<AcademicReport> AcademicReports { get; set; } = new List<AcademicReport>();

    public virtual Event? Event { get; set; }

    public virtual ICollection<HealthReport> HealthReports { get; set; } = new List<HealthReport>();

    public virtual House? House { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
