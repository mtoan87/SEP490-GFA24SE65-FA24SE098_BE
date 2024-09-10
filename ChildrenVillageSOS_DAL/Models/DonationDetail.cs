using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class DonationDetail
{
    public int DonationdetailId { get; set; }

    public int? DonationId { get; set; }

    public double Donation { get; set; }

    public DateTime Datetime { get; set; }

    public string Description { get; set; }

    public string VillageId { get; set; }

    public string HouseId { get; set; }

    public string Status { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Donation DonationNavigation { get; set; }

    public virtual House House { get; set; }

    public virtual Village Village { get; set; }
}
