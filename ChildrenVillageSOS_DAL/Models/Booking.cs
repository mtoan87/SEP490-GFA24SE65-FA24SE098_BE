using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string HouseId { get; set; }

    public string UserAccountId { get; set; }

    public int? BkslotId { get; set; }

    public DateTime Visitday { get; set; }

    public string Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual BookingSlot Bkslot { get; set; }

    public virtual House House { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
