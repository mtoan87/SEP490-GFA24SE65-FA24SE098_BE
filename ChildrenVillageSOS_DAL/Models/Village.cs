using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Village
{
    public string Id { get; set; }

    public string VillageName { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string UserAccountId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual UserAccount UserAccount { get; set; }
}
