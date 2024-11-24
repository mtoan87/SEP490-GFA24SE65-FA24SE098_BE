using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}
