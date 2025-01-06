using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child
{
    public string Id { get; set; } = null!;

    public string? ChildName { get; set; }

    public string? HealthStatus { get; set; }

    public string? HouseId { get; set; }

    public string? SchoolId { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? SystemWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? CurrentAmount { get; set; }

    public decimal? AmountLimit { get; set; }

    public string? Gender { get; set; }

    public DateTime Dob { get; set; }

    public string? Status { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AcademicReport> AcademicReports { get; set; } = new List<AcademicReport>();

    public virtual ICollection<ChildNeed> ChildNeeds { get; set; } = new List<ChildNeed>();

    public virtual ICollection<ChildProgress> ChildProgresses { get; set; } = new List<ChildProgress>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual FacilitiesWallet? FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet? FoodStuffWallet { get; set; }

    public virtual ICollection<HealthReport> HealthReports { get; set; } = new List<HealthReport>();

    public virtual HealthWallet? HealthWallet { get; set; }

    public virtual House? House { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual NecessitiesWallet? NecessitiesWallet { get; set; }

    public virtual School? School { get; set; }

    public virtual SystemWallet? SystemWallet { get; set; }

    public virtual ICollection<TransferHistory> TransferHistories { get; set; } = new List<TransferHistory>();

    public virtual ICollection<TransferRequest> TransferRequests { get; set; } = new List<TransferRequest>();
}
