﻿using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IExpenseRepository : IRepositoryGeneric<Expense>
    {
        decimal GetTotalExpenseAmount();
        decimal GetCostPerChild();
        decimal GetBudgetUtilizationPercentage();
        decimal GetMonthlyExpense(int year, int month);
        ExpenseResponseDTO[] GetAllExpenses();
        Expense[] GetExpenseByFacilitiesWalletId(int id);
        Expense[] GetExpenseByFoodWalletId(int id);
        Expense[] GetExpenseByHealthWalletId(int id);
        Expense[] GetExpenseByNecessilitiesWalletId(int id);
        Expense[] GetExpenseBySystemWalletId(int id);
        DataTable getExpense();
        Task<IEnumerable<Expense>> GetExpensesByYear(int year);
    }
}
