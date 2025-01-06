using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class House
{
    public string Id { get; set; } = null!;

    public string HouseName { get; set; } = null!;

    public int? HouseNumber { get; set; }

    public decimal? ExpenseAmount { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public int? HouseMember { get; set; }

    public int? CurrentMembers { get; set; }

    public string? HouseOwner { get; set; }

    public string? Status { get; set; }

    public string? UserAccountId { get; set; }

    public string? VillageId { get; set; }

    public DateTime FoundationDate { get; set; }

    public DateTime? LastInspectionDate { get; set; }

    public string MaintenanceStatus { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<TransferHistory> TransferHistoryFromHouses { get; set; } = new List<TransferHistory>();

    public virtual ICollection<TransferHistory> TransferHistoryToHouses { get; set; } = new List<TransferHistory>();

    public virtual ICollection<TransferRequest> TransferRequestFromHouses { get; set; } = new List<TransferRequest>();

    public virtual ICollection<TransferRequest> TransferRequestToHouses { get; set; } = new List<TransferRequest>();

    public virtual UserAccount? UserAccount { get; set; }

    public virtual Village? Village { get; set; }
}
