using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Village
{
    public string VillageId { get; set; }

    public string VillageName { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string UserAccountId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual UserAccount UserAccount { get; set; }
}
