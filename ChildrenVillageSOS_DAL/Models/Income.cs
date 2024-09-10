using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public int? DonationId { get; set; }

    public DateTime Receiveday { get; set; }

    public string UserAccountId { get; set; }

    public string HouseId { get; set; }

    public bool IsDelete { get; set; }

    public virtual Donation Donation { get; set; }

    public virtual House House { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
