using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Child
{
    public string Id { get; set; }

    public string ChildName { get; set; }

    public string HealthStatus { get; set; }

    public string HouseId { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? SystemWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? CurrentAmount { get; set; }

    public decimal? AmountLimit { get; set; }

    public string Gender { get; set; }

    public DateTime Dob { get; set; }

    public string Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? BirthCertificate { get; set; }

    public string? CitizenIdentification { get; set; }

    public virtual ICollection<AcademicReport> AcademicReports { get; set; } = new List<AcademicReport>();

    public virtual FacilitiesWallet FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet FoodStuffWallet { get; set; }

    public virtual ICollection<HealthReport> HealthReports { get; set; } = new List<HealthReport>();

    public virtual HealthWallet HealthWallet { get; set; }

    public virtual House House { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual NecessitiesWallet NecessitiesWallet { get; set; }

    public virtual SystemWallet SystemWallet { get; set; }
}
