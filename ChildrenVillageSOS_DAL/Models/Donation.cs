using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Donation
{
    public int DonationId { get; set; }

    public string UserAccountId { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<DonationDetail> DonationDetails { get; set; } = new List<DonationDetail>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual UserAccount UserAccount { get; set; }
}
