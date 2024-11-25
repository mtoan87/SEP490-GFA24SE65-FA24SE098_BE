using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
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
    public class IncomeRepository : RepositoryGeneric<Income> ,IIncomeRepository
    {
        public IncomeRepository(SoschildrenVillageDbContext context) : base(context)
        {
            
        }

        public DataTable getIncome()
        {
            DataTable dt = new DataTable();
            dt.TableName = "IncomeData";
            dt.Columns.Add("IncomeId", typeof(int));
            dt.Columns.Add("DonationAmount", typeof(decimal));        
            dt.Columns.Add("Wallet", typeof(string)); // Đổi thành string để chứa tên ví
            dt.Columns.Add("Receiveday", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("UserName", typeof(string));

            // Truy vấn dữ liệu với Include để nạp Donation
            var _list = this._context.Incomes
                .Include(i => i.Donation)
                .Include(u => u.UserAccount)// Eager loading Donation
                .ToList();
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
                        item.Donation?.Amount ?? 0, // Nếu Donation là null, trả về 0                    
                        walletName, // Gán tên ví vào cột Wallet
                        item.Receiveday,
                        item.Status,
                        item.UserAccount.UserName
                    );
                });
            }
            return dt;
        }
        public async Task<Income> GetIncomeByDonationIdAsync(int donationId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(p => p.DonationId == donationId);
        }

        public IncomeResponseDTO[] GetAllIncome()
        {
            var incomes = _context.Incomes
                .Where(i => !i.IsDeleted) // Exclude deleted records
                .Select(i => new IncomeResponseDTO
                {
                    Id = i.Id,
                    DonationId = i.DonationId,
                    Amount = i.Amount,
                    ReceiveDay = i.Receiveday,
                    Status = i.Status,
                    UserAccountId = i.UserAccountId,
                    FacilitiesWalletId = i.FacilitiesWalletId,
                    FoodStuffWalletId = i.FoodStuffWalletId,
                    HealthWalletId = i.HealthWalletId,
                    NecessitiesWalletId = i.NecessitiesWalletId,
                    SystemWalletId = i.SystemWalletId,
                    CreatedDate = i.CreatedDate,
                    ModifiedDate = i.ModifiedDate
                })
                .ToArray(); // Convert to an array

            return incomes;
        }
        public Income[] GetIncomeByFacilitiesWalletId(int id)
        {
            return _context.Incomes
                .Where(i => i.FacilitiesWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Income[] GetIncomeByFoodWalletId(int id)
        {
            return _context.Incomes
                .Where(i => i.FoodStuffWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Income[] GetIncomeByHealthWalletId(int id)
        {
            return _context.Incomes
                .Where(i => i.HealthWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Income[] GetIncomeByNecessilitiesWalletId(int id)
        {
            return _context.Incomes
                .Where(i => i.NecessitiesWalletId == id && !i.IsDeleted)
                .ToArray();
        }
        public Income[] GetIncomeBySystemWalletId(int id)
        {
            return _context.Incomes
                .Where(i => i.SystemWalletId == id && !i.IsDeleted)
                .ToArray();
        }
    }
}
