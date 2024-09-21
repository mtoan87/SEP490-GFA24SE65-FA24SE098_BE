using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child : BaseEntity
{
    public string Id { get; set; } = null!;

    public string ChildName { get; set; } = null!;

    public string HealthStatus { get; set; } = null!;

    public string? HouseId { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string AcademicLevel { get; set; } = null!;

    public string Certificate { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual House? House { get; set; }
}
