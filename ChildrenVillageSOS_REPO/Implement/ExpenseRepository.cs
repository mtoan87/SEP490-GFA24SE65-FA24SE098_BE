using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
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
            dt.Columns.Add("Wallet", typeof(string));
            dt.Columns.Add("ExpenseOwner", typeof(string));// Cột để hiển thị tên ví
            dt.Columns.Add("HouseName", typeof(string));


            // Truy vấn danh sách Expense
            var _list = this._context.Expenses.Include(h => h.House).ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    // Kiểm tra ví nào có giá trị khác null và gán tên tương ứng
                    string walletName = string.Empty;
                    if (item.FacilitiesWalletId.HasValue)
                        walletName = "FacilitiesWallet";
                    else if (item.SystemWalletId.HasValue)
                        walletName = "SystemWallet";
                    else if (item.FoodStuffWalletId.HasValue)
                        walletName = "FoodStuffWallet";
                    else if (item.HealthWalletId.HasValue)
                        walletName = "HealthWallet";
                    else if (item.NecessitiesWalletId.HasValue)
                        walletName = "NecessitiesWallet";

                    dt.Rows.Add(
                        item.Id,
                        item.ExpenseAmount,
                        item.Expenseday,
                        item.Status,
                        walletName,
                        item.House.HouseOwner,
                        item.House.HouseName
                    );
                });
            }

            return dt;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByYear(int year)
        {
            return await _context.Expenses
                .Where(x => !x.IsDeleted && x.Expenseday.Value.Year == year)
                .ToListAsync();
        }
    }
}
