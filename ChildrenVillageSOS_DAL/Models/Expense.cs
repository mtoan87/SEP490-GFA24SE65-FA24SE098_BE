using ChildrenVillageSOS_DAL.Helpers;
using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Expense : BaseEntity
{
    public int Id { get; set; }

    public decimal ExpenseAmount { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Expenseday { get; set; }

    public string? Status { get; set; }

    public string? HouseId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual House? House { get; set; }
}
