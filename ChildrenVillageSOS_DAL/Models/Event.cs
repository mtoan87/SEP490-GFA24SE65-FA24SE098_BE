using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? SystemWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? AmountLimit { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? ChildId { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual FacilitiesWallet? FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet? FoodStuffWallet { get; set; }

    public virtual HealthWallet? HealthWallet { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual NecessitiesWallet? NecessitiesWallet { get; set; }

    public virtual SystemWallet? SystemWallet { get; set; }
}
