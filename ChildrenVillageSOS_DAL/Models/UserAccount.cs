using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class UserAccount
{
    public string UserAccountId { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }

    public long Phone { get; set; }

    public string Address { get; set; }

    public DateOnly Dob { get; set; }

    public string Gender { get; set; }

    public string Country { get; set; }

    public int? RoleId { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Role Role { get; set; }

    public virtual ICollection<Village> Villages { get; set; } = new List<Village>();
}
