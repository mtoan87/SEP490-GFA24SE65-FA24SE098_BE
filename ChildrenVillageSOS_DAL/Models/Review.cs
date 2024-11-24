using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Review
{
    public int Id { get; set; }

    public string? UserAccountId { get; set; }

    public int? OrderDetailId { get; set; }

    public double? Rating { get; set; }

    public string? Comment { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }

    public virtual UserAccount? UserAccount { get; set; }
}
