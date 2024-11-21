using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class AcademicReport
{
    public int Id { get; set; }

    public string Diploma { get; set; }

    public string ChildId { get; set; }

    public decimal? Gpa { get; set; }

    public string SchoolReport { get; set; }

    public virtual Child Child { get; set; }
}
