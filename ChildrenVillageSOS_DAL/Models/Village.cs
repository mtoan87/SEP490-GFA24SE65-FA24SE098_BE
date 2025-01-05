using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Village
{
    public string Id { get; set; } = null!;

    public string? VillageName { get; set; }

    public DateTime? EstablishedDate { get; set; }

    public decimal? ExpenseAmount { get; set; }

    public double? Area { get; set; }

    public int? TotalHouses { get; set; }

    public int? TotalChildren { get; set; }

    public string? ContactNumber { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? UserAccountId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public string? RoleName { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual UserAccount? UserAccount { get; set; }
}
