using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class TransferRequest
{
    public int Id { get; set; }

    public string ChildId { get; set; } = null!;

    public string? FromHouseId { get; set; } = null!;

    public string? ToHouseId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public string? DirectorNote { get; set; }

    public string? RequestReason { get; set; }

    public string? ApprovedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child Child { get; set; } = null!;

    public virtual House FromHouse { get; set; } = null!;

    public virtual House? ToHouse { get; set; }
}
