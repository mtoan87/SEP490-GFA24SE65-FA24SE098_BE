using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class AcademicReport
{
    public int Id { get; set; }

    public string? Diploma { get; set; }

    public string? SchoolLevel { get; set; }

    public string? ChildId { get; set; }

    public decimal? Gpa { get; set; }

    public string? SchoolReport { get; set; }

    public string Semester { get; set; } = null!;

    public int AcademicYear { get; set; }

    public string? Remarks { get; set; }

    public string? Achievement { get; set; }

    public string? SubjectsReport { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child? Child { get; set; }
}
