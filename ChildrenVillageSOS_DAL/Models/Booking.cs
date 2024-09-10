using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string HouseId { get; set; }

    public string UserAccountId { get; set; }

    public DateTime Visitday { get; set; }

    public string Status { get; set; }

    public DateTime Starttime { get; set; }

    public DateTime Endtime { get; set; }

    public bool? IsDelete { get; set; }

    public virtual House House { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
