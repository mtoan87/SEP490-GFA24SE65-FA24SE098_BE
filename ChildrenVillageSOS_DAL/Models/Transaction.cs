using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int? SystemWalletId { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DateTime { get; set; }

    public string Status { get; set; }

    public string UserAccountId { get; set; }

    public int? DonationId { get; set; }

    public int? IncomeId { get; set; }

    public virtual Donation Donation { get; set; }

    public virtual FacilitiesWallet FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet FoodStuffWallet { get; set; }

    public virtual HealthWallet HealthWallet { get; set; }

    public virtual Income Income { get; set; }

    public virtual NecessitiesWallet NecessitiesWallet { get; set; }

    public virtual SystemWallet SystemWallet { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
