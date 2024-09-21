using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Transaction 
{
    public int Id { get; set; }

    public int? SystemWalletId { get; set; }

    public decimal Amount { get; set; }

    public DateTime DateTime { get; set; }

    public string Status { get; set; } = null!;

    public int? DonationId { get; set; }

    public int? IncomeId { get; set; }

    public virtual Donation? Donation { get; set; }

    public virtual Income? Income { get; set; }

    public virtual SystemWallet? SystemWallet { get; set; }
}
