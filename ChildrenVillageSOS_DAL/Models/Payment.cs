using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime Datetime { get; set; }

    public int? DonationId { get; set; }

    public string Status { get; set; }

    public double Amount { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Donation Donation { get; set; }
}
