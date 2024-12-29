using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Donation
{
    public int Id { get; set; }

    public string? UserAccountId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public long? Phone { get; set; }

    public string? Address { get; set; }

    public int? EventId { get; set; }

    public string? ChildId { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? SystemWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public string? DonationType { get; set; }

    public DateTime DateTime { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; } 

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public string? RoleName { get; set; }

    public bool IsDeleted { get; set; }

    public string? EventCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child? Child { get; set; }

    public virtual Event? Event { get; set; }

    public virtual FacilitiesWallet? FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet? FoodStuffWallet { get; set; }

    public virtual HealthWallet? HealthWallet { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual NecessitiesWallet? NecessitiesWallet { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual SystemWallet? SystemWallet { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserAccount? UserAccount { get; set; }
}
