using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
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
        ExpenseResponseDTO[] GetAllExpense();
        Task<Expense?> GetExpenseByEventAndStatusAsync(string villageId, int eventId, IEnumerable<string> statuses);
        ExpenseResponseDTO[] GetUnComfirmVillageExpense();
        ExpenseResponseDTO[] GetUnComfirmHouseExpense();
        Task ApproveHouseExpensesByVillageIdAsync(string villageId);
        Task<List<Expense>> GetExpensesByVillageIdAsync(string villageId);
        decimal GetTotalExpenseAmount();
        Task<IEnumerable<Expense>> GetExpensesByHouseIdsAsync(IEnumerable<string> houseIds, string expenseType, string status);
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
        Task<List<Expense>> SearchExpenses(SearchExpenseDTO searchExpenseDTO);
        decimal GetTotalExpenseThisMonth();
    }
}
