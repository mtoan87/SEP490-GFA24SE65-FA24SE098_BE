using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class HealthReport
{
    public int Id { get; set; }

    public string ChildId { get; set; }

    public string HealthCertificate { get; set; }

    public string Weight { get; set; }

    public string Height { get; set; }

    public virtual Child Child { get; set; }
}
