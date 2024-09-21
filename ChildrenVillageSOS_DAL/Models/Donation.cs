using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Donation : BaseEntity
{
    public int Id { get; set; }

    public string? UserAccountId { get; set; }

    public string DonationType { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserAccount? UserAccount { get; set; }
}
