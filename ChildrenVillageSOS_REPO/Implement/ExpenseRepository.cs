using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Expense[] GetExpenseByFacilitiesWalletId(int id)
        {
            return _context.Expenses
                .Where(i => i.FacilitiesWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Expense[] GetExpenseByFoodWalletId(int id)
        {
            return _context.Expenses
                .Where(i => i.FoodStuffWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Expense[] GetExpenseByHealthWalletId(int id)
        {
            return _context.Expenses
                .Where(i => i.HealthWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Expense[] GetExpenseByNecessilitiesWalletId(int id)
        {
            return _context.Expenses
                .Where(i => i.NecessitiesWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Expense[] GetExpenseBySystemWalletId(int id)
        {
            return _context.Expenses
                .Where(i => i.SystemWalletId == id && !i.IsDeleted)
                .ToArray();
        }

        public DataTable getExpense()
        {
            DataTable dt = new DataTable();
            dt.TableName = "ExpenseData";
            dt.Columns.Add("ExpenseID", typeof(int));
            dt.Columns.Add("ExpenseAmount(VND)", typeof(decimal));
            dt.Columns.Add("ExpenseDate(MM/DD/YYYY)", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("SystemWalletId", typeof(int));
            dt.Columns.Add("FacilitiesWalletId", typeof(int));
            dt.Columns.Add("FoodStuffWalletId", typeof(int));
            dt.Columns.Add("HealthWalletId", typeof(int));
            dt.Columns.Add("NecessitiesWalletId", typeof(int));
            dt.Columns.Add("HouseId", typeof(string));
            var _list = this._context.Expenses.ToList();
            if(_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id,item.ExpenseAmount,item.Expenseday,item.Status,item.SystemWalletId,item.FacilitiesWalletId,item.FoodStuffWallet,item.HealthWalletId,item.NecessitiesWalletId,item.HouseId);
                });
            }
            return dt;
        }

    }
}
