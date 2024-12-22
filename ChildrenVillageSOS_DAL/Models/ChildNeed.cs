using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class ChildNeed
{
    public int Id { get; set; }

    public string ChildId { get; set; } = null!;

    public string? NeedDescription { get; set; }

    public string? NeedType { get; set; }

    public string? Priority { get; set; }

    public DateTime? FulfilledDate { get; set; }

    public string? Remarks { get; set; }

    public string? Status { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child Child { get; set; } = null!;
}
