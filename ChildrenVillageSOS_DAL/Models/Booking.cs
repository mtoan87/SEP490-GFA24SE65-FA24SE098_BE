using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Booking : ISoftDelete
{
    public int Id { get; set; }

    public string HouseId { get; set; }

    public string UserAccountId { get; set; }

    public int? BookingSlotId { get; set; }

    public DateTime Visitday { get; set; }

    public string Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BookingSlot BookingSlot { get; set; }

    public virtual House House { get; set; }

    public virtual UserAccount UserAccount { get; set; }
}
