using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class ExpenseRepository : RepositoryGeneric<Expense>, IExpenseRepository
    {
        public ExpenseRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
        public ExpenseResponseDTO[] GetAllExpenses()
        {
            var expenses = _context.Expenses
                .Where(e => !e.IsDeleted) // Exclude deleted records
                .Select(e => new ExpenseResponseDTO
                {
                    Id = e.Id,
                    ExpenseAmount = e.ExpenseAmount,
                    Description = e.Description,
                    Expenseday = e.Expenseday,
                    Status = e.Status,
                    SystemWalletId = e.SystemWalletId,
                    FacilitiesWalletId = e.FacilitiesWalletId,
                    FoodStuffWalletId = e.FoodStuffWalletId,
                    HealthWalletId = e.HealthWalletId,
                    NecessitiesWalletId = e.NecessitiesWalletId,
                    HouseId = e.HouseId,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate
                })
                .ToArray(); // Convert to an array

            return expenses;
        }
    }
}
