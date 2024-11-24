using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? UserAccountId { get; set; }

    public int? PaymentId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment? Payment { get; set; }

    public virtual UserAccount? UserAccount { get; set; }
}
