using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class BookingSlot
{
    public int Id { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Status { get; set; }

    public int? SlotTime { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
