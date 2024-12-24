using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class AcademicReport
{
    public int Id { get; set; }

    public string? Diploma { get; set; }

    public string? SchoolLevel { get; set; }

    public string? ChildId { get; set; }

    public string? SchoolId { get; set; }

    public decimal? Gpa { get; set; }

    public string? SchoolReport { get; set; }

    public string? Semester { get; set; }

    public string? AcademicYear { get; set; }

    public string? Remarks { get; set; }

    public string? Achievement { get; set; }

    public string? Status { get; set; }

    public string? Class { get; set; }

    public string? Feedback { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child? Child { get; set; }

    public virtual School? School { get; set; }

    public virtual ICollection<SubjectDetail> SubjectDetails { get; set; } = new List<SubjectDetail>();
}
