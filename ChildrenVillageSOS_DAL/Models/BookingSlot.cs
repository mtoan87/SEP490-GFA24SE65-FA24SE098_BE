using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class BookingSlot
{
    public int BkslotId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Status { get; set; }

    public int SlotTime { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
