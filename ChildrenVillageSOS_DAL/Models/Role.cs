using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
