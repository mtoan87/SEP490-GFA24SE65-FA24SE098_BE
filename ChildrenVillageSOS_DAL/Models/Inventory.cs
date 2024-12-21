using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public string? Purpose { get; set; }

    public string BelongsTo { get; set; } = null!;

    public string BelongsToId { get; set; } = null!;

    public DateTime? PurchaseDate { get; set; }

    public DateTime? LastInspectionDate { get; set; }

    public string MaintenanceStatus { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
