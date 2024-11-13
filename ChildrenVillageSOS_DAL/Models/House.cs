using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class House
{
    public string Id { get; set; }

    public string HouseName { get; set; }

    public int? HouseNumber { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public int? HouseMember { get; set; }

    public string HouseOwner { get; set; }

    public string Status { get; set; }

    public string UserAccountId { get; set; }

    public string VillageId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual UserAccount UserAccount { get; set; }

    public virtual Village Village { get; set; }
}
