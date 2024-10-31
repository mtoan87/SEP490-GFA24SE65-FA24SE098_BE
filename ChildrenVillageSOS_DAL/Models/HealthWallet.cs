using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class HealthWallet
{
    public int Id { get; set; }

    public decimal Budget { get; set; }

    public string UserAccountId { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserAccount UserAccount { get; set; }
}
