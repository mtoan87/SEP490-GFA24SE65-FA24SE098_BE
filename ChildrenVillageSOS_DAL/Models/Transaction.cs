using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? SystemWalletId { get; set; }

    public double Amount { get; set; }

    public DateTime DateTime { get; set; }

    public string Status { get; set; }

    public int? DonationId { get; set; }

    public int? IncomeId { get; set; }

    public virtual Donation Donation { get; set; }

    public virtual Income Income { get; set; }

    public virtual SystemWallet SystemWallet { get; set; }
}
