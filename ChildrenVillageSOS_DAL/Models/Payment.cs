using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime DateTime { get; set; }

    public int? DonationId { get; set; }

    public string Status { get; set; }

    public decimal Amount { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Donation Donation { get; set; }
}
