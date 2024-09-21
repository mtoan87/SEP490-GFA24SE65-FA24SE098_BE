using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class House : BaseEntity
{
    public string Id { get; set; } = null!;

    public string HouseName { get; set; } = null!;

    public int HouseNumber { get; set; }

    public string Location { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int HouseMember { get; set; }

    public string HouseOwner { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? UserAccountId { get; set; }

    public string? VillageId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual UserAccount? UserAccount { get; set; }

    public virtual Village? Village { get; set; }
}
