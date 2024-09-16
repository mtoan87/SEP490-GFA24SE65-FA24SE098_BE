using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public int? DonationId { get; set; }

    public DateTime Receiveday { get; set; }

    public string Status { get; set; }

    public string UserAccountId { get; set; }

    public string HouseId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Donation Donation { get; set; }

    public virtual House House { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserAccount UserAccount { get; set; }
}
