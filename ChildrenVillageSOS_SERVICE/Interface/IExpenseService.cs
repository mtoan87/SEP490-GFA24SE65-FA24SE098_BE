using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpenses();
        Task<Expense> GetExpenseById(int id);
        Task<Expense> CreateExpense(CreateExepenseDTO createExepense);
        ExpenseResponseDTO[] GetFormatedExpenses();
        Task<Expense> UpdateExpense(int id, UpdateExpenseDTO updateExpense);
        Task<Expense> DeleteExpense(int id);
        Task<Expense> ConfirmExpense(int id);
        Task<Expense> SoftDelete(int id);
        Expense[] GetExpenseByFacilitiesWalletId(int id);
        Expense[] GetExpenseByFoodWalletId(int id);
        Expense[] GetExpenseByHealthWalletId(int id);
        Expense[] GetExpenseByNesceWalletId(int id);
        Expense[] GetExpenseBySysWalletId(int id);
    }
}
