using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Expense
{
    public int Id { get; set; }

    public decimal ExpenseAmount { get; set; }

    public string? Description { get; set; }

    public DateTime? Expenseday { get; set; }

    public string? Status { get; set; }

    public int? SystemWalletId { get; set; }

    public string? ExpenseType { get; set; }

    public int? FacilitiesWalletId { get; set; }

    public int? FoodStuffWalletId { get; set; }

    public int? HealthWalletId { get; set; }

    public int? NecessitiesWalletId { get; set; }

    public string? RequestedBy { get; set; }

    public string? ApprovedBy { get; set; }

    public string? HouseId { get; set; }

    public string? ChildId { get; set; }

    public string? VillageId { get; set; }

    public int? EventId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Child? Child { get; set; }

    public virtual Event? Event { get; set; }

    public virtual FacilitiesWallet? FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet? FoodStuffWallet { get; set; }

    public virtual HealthWallet? HealthWallet { get; set; }

    public virtual House? House { get; set; }

    public virtual NecessitiesWallet? NecessitiesWallet { get; set; }

    public virtual Village? Village { get; set; }
}
