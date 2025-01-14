﻿using System;
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

    public string? EventCode { get; set; }

    public decimal? Amount { get; set; }

    public decimal? CurrentAmount { get; set; }

    public decimal? AmountLimit { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsDeleted { get; set; }

    public string? VillageId { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<ChildProgress> ChildProgresses { get; set; } = new List<ChildProgress>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual FacilitiesWallet? FacilitiesWallet { get; set; }

    public virtual FoodStuffWallet? FoodStuffWallet { get; set; }

    public virtual HealthWallet? HealthWallet { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual NecessitiesWallet? NecessitiesWallet { get; set; }

    public virtual SystemWallet? SystemWallet { get; set; }

    public virtual Village? Village { get; set; }
}
