using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class UserAccountRepository : RepositoryGeneric<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public DataTable getUser()
        {
            DataTable dt = new DataTable();
            dt.TableName = "UserData";
            dt.Columns.Add("UserAccountId", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("UserEmail", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Phone", typeof(BigInteger));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Dob", typeof(DateTime));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("RoleId", typeof(int));
            var _list = this._context.UserAccounts.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.UserName,
                        item.UserEmail,
                        item.Password,
                        item.Phone,
                        item.Address,
                        item.Dob,
                        item.Gender,
                        item.Country,
                        item.Status,
                        item.RoleId);
                });
            }
            return dt;
        }
        public async Task<UserAccount> Login(string email, string password)
        {
            var user = await _context.UserAccounts.Where(x => x.UserEmail == email && x.Password == password).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserAccount> GetUserWithImagesByIdAsync(string id)
        {
            return await _dbSet
                .Include(u => u.Images) // Bao gồm hình ảnh liên kết
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserAccount> GetHighestIdUser()
        {
            return await _context.UserAccounts
                                 .OrderByDescending(u => u.Id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<TotalUsersStatDTO> GetTotalUsersStatAsync()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek); // Assuming week starts on Monday

            // Tổng số user là sponsor (RoleId = 2) hoặc donor (RoleId = 5) đang active
            var totalUsers = await _context.UserAccounts
                .Where(u => (u.RoleId == 2 || u.RoleId == 5) && !u.IsDeleted)
                .CountAsync();

            // Số lượng user mới trong tháng
            var newUsersThisMonth = await _context.UserAccounts
                .Where(u => (u.RoleId == 2 || u.RoleId == 5)
                        && !u.IsDeleted
                        && u.CreatedDate >= firstDayOfMonth)
                .CountAsync();

            // Số lượng user mới trong tuần
            var newUsersThisWeek = await _context.UserAccounts
                .Where(u => (u.RoleId == 2 || u.RoleId == 5)
                        && !u.IsDeleted
                        && u.CreatedDate >= firstDayOfWeek)
                .CountAsync();

            return new TotalUsersStatDTO
            {
                TotalUsers = totalUsers,
                NewUsersThisMonth = newUsersThisMonth,
                NewUsersThisWeek = newUsersThisWeek
            };
        }
    }
}
