using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class SystemWallet
{
    public int Id { get; set; }

    public decimal Budget { get; set; }

    public string UserAccountId { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual UserAccount UserAccount { get; set; }
}
