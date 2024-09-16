using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public double ExpenseAmount { get; set; }

    public string Description { get; set; }

    public DateTime Expenseday { get; set; }

    public string Status { get; set; }

    public string HouseId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual House House { get; set; }
}
