using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child
{
    public string Id { get; set; }

    public string ChildName { get; set; }

    public string HealthStatus { get; set; }

    public string HouseId { get; set; }

    public string Gender { get; set; }

    public DateTime Dob { get; set; }

    public string Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string BirthCertificate { get; set; }

    public string CitizenIdentification { get; set; }

    public int? EventId { get; set; }

    public virtual ICollection<AcademicReport> AcademicReports { get; set; } = new List<AcademicReport>();

    public virtual Event Event { get; set; }

    public virtual ICollection<HealthReport> HealthReports { get; set; } = new List<HealthReport>();

    public virtual House House { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
