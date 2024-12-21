using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class SubjectDetail
{
    public int Id { get; set; }

    public int AcademicReportId { get; set; }

    public string? SubjectName { get; set; }

    public decimal? Score { get; set; }

    public string? Remarks { get; set; }

    public bool? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AcademicReport AcademicReport { get; set; } = null!;
}
