using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
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
        public decimal GetTotalExpenseAmount()
        {
            return _context.Expenses
                .Where(e => !e.IsDeleted) // Optional: Exclude deleted records
                .Sum(e => e.ExpenseAmount);
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




        public decimal GetMonthlyExpense(int year, int month)
        {
            var totalExpense = _context.Expenses
                .Where(e => !e.IsDeleted &&
                            e.Expenseday.HasValue &&
                            e.Expenseday.Value.Year == year &&
                            e.Expenseday.Value.Month == month)
                .Sum(e => e.ExpenseAmount);

            return totalExpense;
        }
        public decimal GetBudgetUtilizationPercentage()
        {
            // Tính toán ngày bắt đầu và kết thúc cho giữa tháng hiện tại và tháng trước
            DateTime currentDate = DateTime.Now;

            DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime middleOfCurrentMonth = firstDayOfCurrentMonth.AddDays((currentDate.Day - 1) / 2);

            DateTime firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);
            DateTime middleOfPreviousMonth = firstDayOfPreviousMonth.AddDays((firstDayOfPreviousMonth.AddMonths(1).Day - 1) / 2);

            // Lấy tổng chi phí trong tháng hiện tại và tháng trước
            var currentMonthExpense = _context.Expenses
                .Where(exp => exp.Expenseday >= middleOfCurrentMonth && exp.Expenseday <= currentDate)
                .Sum(exp => exp.ExpenseAmount);

            var previousMonthExpense = _context.Expenses
                .Where(exp => exp.Expenseday >= middleOfPreviousMonth && exp.Expenseday <= firstDayOfPreviousMonth.AddMonths(1).AddDays(-1))
                .Sum(exp => exp.ExpenseAmount);

            // Tính tỷ lệ sử dụng ngân sách từ chi phí tháng hiện tại so với tháng trước
            decimal utilizationPercentage = (currentMonthExpense / previousMonthExpense) * 100;
            utilizationPercentage = Math.Round(utilizationPercentage, 2);

            return utilizationPercentage;
        }

        public decimal GetCostPerChild()
        {
            // Lọc bản ghi Expense có ExpenseType là "Regular" và ChildId không null, lấy bản ghi đầu tiên
            var regularExpense = _context.Expenses
                .Where(exp => exp.ExpenseType == "Regular" && exp.ChildId != null)
                .FirstOrDefault();

            // Nếu tìm thấy bản ghi, trả về ExpenseAmount, nếu không, trả về 0
            return regularExpense?.ExpenseAmount ?? 0;
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
