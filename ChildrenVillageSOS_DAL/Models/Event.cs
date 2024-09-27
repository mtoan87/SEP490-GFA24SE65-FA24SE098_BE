using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Event : ISoftDelete
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public decimal? Amount { get; set; }

    public string? ChildId { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
