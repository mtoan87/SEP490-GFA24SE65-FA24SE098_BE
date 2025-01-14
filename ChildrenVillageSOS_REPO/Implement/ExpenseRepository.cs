using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
        public async Task<IEnumerable<Expense>> GetExpensesByHouseIdsAsync(IEnumerable<string> houseIds, string expenseType, string status)
        {
            return await _context.Expenses
                    .Where(e => houseIds.Contains(e.HouseId)
                    && e.ExpenseType.ToLower() == expenseType.ToLower()
                    && e.Status.ToLower() == status.ToLower()
                    && !e.IsDeleted)
        .ToListAsync();
        }
        public decimal GetTotalExpenseAmount()
        {
            return _context.Expenses
                .Where(e => !e.IsDeleted) // Optional: Exclude deleted records
                .Sum(e => e.ExpenseAmount);
        }
        public decimal GetTotalExpenseThisMonth()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return _context.Expenses
                .Where(e => !e.IsDeleted && e.Expenseday >= firstDayOfMonth)
                .Sum(e => e.ExpenseAmount);
        }

        public async Task ApproveHouseExpensesByVillageIdAsync(string villageId)
        {
            // Bước 1: Lấy các nhà thuộc villageId
            var housesInVillage = await _context.Houses
                .Where(h => h.VillageId == villageId && !h.IsDeleted)  // Lọc theo VillageId và nhà không bị xóa
                .ToListAsync();

            if (!housesInVillage.Any())
            {
                throw new InvalidOperationException("No houses found for this village.");
            }

            // Bước 2: Lấy các Expense thuộc các houseId của những nhà đó có status là "OnRequestToEvent" và ExpenseType là "Special"
            var houseExpenses = await _context.Expenses
                .Where(e => housesInVillage.Select(h => h.Id).Contains(e.HouseId)  // Lọc các Expense thuộc HouseId trong các nhà trên
                            && e.Status == "OnRequestToEvent"
                            && e.ExpenseType == "Special"
                            && !e.IsDeleted)  // Lọc các Expense không bị xóa
                .ToListAsync();

            if (!houseExpenses.Any())
            {
                throw new InvalidOperationException("No expenses found with status 'OnRequestToEvent' and 'Special'.");
            }

            // Bước 3: Cập nhật trạng thái của các Expense này thành "EventApproved"
            foreach (var houseExpense in houseExpenses)
            {
                houseExpense.Status = "EventApproved";  // Cập nhật trạng thái
                houseExpense.ModifiedDate = DateTime.Now;  // Cập nhật thời gian sửa đổi
                await UpdateAsync(houseExpense);  // Lưu lại thay đổi
            }
        }

        public async Task<List<Expense>> GetExpensesByVillageIdAsync(string villageId)
        {
            return await _context.Expenses
         .Where(e => e.VillageId == villageId
                     && e.Status == "OnRequestToEvent"
                     && !e.IsDeleted
                     && e.HouseId != null) // Lọc các Expense thuộc House và có trạng thái 'OnRequestToEvent'
         .ToListAsync();
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

        public  ExpenseResponseDTO[] GetUnComfirmHouseExpense()
        {
            var  expenses = _context.Expenses
                .Where(e => !e.IsDeleted && // Exclude deleted records
                            e.ExpenseType == "Special" && // Filter by ExpenseType
                            e.Status == "Pending" && // Filter by Status
                            !string.IsNullOrEmpty(e.HouseId)) // Ensure HouseId has a value
                .Select(e => new ExpenseResponseDTO
                {
                    Id = e.Id,
                    ExpenseAmount = e.ExpenseAmount,
                    Description = e.Description,
                    Expenseday = e.Expenseday,
                    ExpenseType = e.ExpenseType,
                    Status = e.Status,
                    RequestedBy = e.RequestedBy,
                    ApprovedBy = e.ApprovedBy,
                    SystemWalletId = e.SystemWalletId,
                    FacilitiesWalletId = e.FacilitiesWalletId,
                    FoodStuffWalletId = e.FoodStuffWalletId,
                    HealthWalletId = e.HealthWalletId,
                    NecessitiesWalletId = e.NecessitiesWalletId,
                    ChildId = e.ChildId,
                    VillageId = e.VillageId,
                    HouseId = e.HouseId,
                    CreatedDate = e.CreatedDate,
                    ModifiedDate = e.ModifiedDate
                })
                .ToArray(); // Convert to an array

            return expenses;
        }

        public ExpenseResponseDTO[] GetUnComfirmVillageExpense()
        {
            var expenses = _context.Expenses
                .Where(e => !e.IsDeleted && // Exclude deleted records
                            e.ExpenseType == "Special" && // Filter by ExpenseType
                            e.Status == "OnRequestToEvent" && // Filter by Status
                            !string.IsNullOrEmpty(e.VillageId)) // Ensure HouseId has a value
                .Select(e => new ExpenseResponseDTO
                {
                    Id = e.Id,
                    ExpenseAmount = e.ExpenseAmount,
                    Description = e.Description,
                    Expenseday = e.Expenseday,
                    ExpenseType = e.ExpenseType,
                    Status = e.Status,
                    RequestedBy = e.RequestedBy,
                    ApprovedBy = e.ApprovedBy,
                    SystemWalletId = e.SystemWalletId,
                    FacilitiesWalletId = e.FacilitiesWalletId,
                    FoodStuffWalletId = e.FoodStuffWalletId,
                    HealthWalletId = e.HealthWalletId,
                    NecessitiesWalletId = e.NecessitiesWalletId,
                    ChildId = e.ChildId,
                    VillageId = e.VillageId,
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

        public async Task<List<Expense>> SearchExpenses(SearchExpenseDTO searchExpenseDTO)
        {
            var query = _context.Expenses.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchExpenseDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchExpenseDTO.SearchTerm) ||
                     x.ExpenseAmount.ToString().Contains(searchExpenseDTO.SearchTerm) ||
                     x.Description.Contains(searchExpenseDTO.SearchTerm) ||
                     x.HouseId.Contains(searchExpenseDTO.SearchTerm) ||
                     x.Status.Contains(searchExpenseDTO.SearchTerm) ||
                     x.Expenseday.Value.ToString("yyyy-MM-dd").Contains(searchExpenseDTO.SearchTerm) ||
                     x.CreatedDate.Value.ToString("yyyy-MM-dd").Contains(searchExpenseDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }
    }
}
