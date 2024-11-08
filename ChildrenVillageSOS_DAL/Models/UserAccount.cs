using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class UserAccount
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long Phone { get; set; }

    public string Address { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Gender { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? RoleId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<FacilitiesWallet> FacilitiesWallets { get; set; } = new List<FacilitiesWallet>();

    public virtual ICollection<FoodStuffWallet> FoodStuffWallets { get; set; } = new List<FoodStuffWallet>();

    public virtual ICollection<HealthWallet> HealthWallets { get; set; } = new List<HealthWallet>();

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<NecessitiesWallet> NecessitiesWallets { get; set; } = new List<NecessitiesWallet>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<SystemWallet> SystemWallets { get; set; } = new List<SystemWallet>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Village> Villages { get; set; } = new List<Village>();
}
