using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class School
{
    public string Id { get; set; } = null!;

    public string SchoolName { get; set; } = null!;

    public string? Address { get; set; }

    public string? SchoolType { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AcademicReport> AcademicReports { get; set; } = new List<AcademicReport>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();
}
