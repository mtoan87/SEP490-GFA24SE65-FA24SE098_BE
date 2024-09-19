using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child : ISoftDelete
{
    public string Id { get; set; }

    public string ChildName { get; set; }

    public string HealthStatus { get; set; }

    public string HouseId { get; set; }

    public string Gender { get; set; }

    public DateOnly Dob { get; set; }

    public string AcademicLevel { get; set; }

    public string Certificate { get; set; }

    public string Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual House House { get; set; }
}
