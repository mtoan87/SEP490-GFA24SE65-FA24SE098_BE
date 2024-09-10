using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMenthod { get; set; }

    public DateTime Datetime { get; set; }

    public string UserAccountId { get; set; }

    public string Status { get; set; }

    public double Amount { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
