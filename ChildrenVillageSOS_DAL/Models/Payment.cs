using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Payment : BaseEntity
{
    public int Id { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public int? DonationId { get; set; }

    public string Status { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Donation? Donation { get; set; }
}
