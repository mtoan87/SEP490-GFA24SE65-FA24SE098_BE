using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class ChildProgress
{
    public int Id { get; set; }

    public string ChildId { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public string? Category { get; set; }

    public int? EventId { get; set; }

    public int? ActivityId { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual Child Child { get; set; } = null!;

    public virtual Event? Event { get; set; }
}
