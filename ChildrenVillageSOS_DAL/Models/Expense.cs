using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public double ExpenseAmount { get; set; }

    public string Description { get; set; }

    public DateTime Expenseday { get; set; }

    public string HouseId { get; set; }

    public bool IsDelete { get; set; }

    public virtual House House { get; set; }
}
