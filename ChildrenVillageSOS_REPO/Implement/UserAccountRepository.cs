using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.DTO.UserDTO;
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
        public string? GetRoleNameById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
            return role?.RoleName;
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
        public UserResponseDTO GetUserByIdArray(string userid)
        {
            var userDetails = _context.UserAccounts
                .Where(u => u.Id == userid && !u.IsDeleted)
                .Select(u => new UserResponseDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    UserEmail = u.UserEmail,
                    Password = u.Password,
                    Phone = u.Phone,
                    Address = u.Address,
                    Dob = u.Dob,
                    Gender = u.Gender,
                    Country = u.Country,
                    Status = u.Status,
                    RoleId = u.RoleId,
                    IsDeleted = u.IsDeleted,
                    CreatedDate = u.CreatedDate,
                    ModifiedDate = u.ModifiedDate
                })
                .FirstOrDefault();
            return userDetails;
        }
        public async Task<UserResponseDTO[]> GetAllUserIsDeletedAsync()
        {
            return await _context.UserAccounts
                .Where(x => x.IsDeleted) // Lọc các tài khoản đã bị xóa
                .Select(x => new UserResponseDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Password = x.Password,
                    Phone = x.Phone,
                    Address = x.Address,
                    Dob = x.Dob,
                    Gender = x.Gender,
                    Country = x.Country,
                    Status = x.Status,
                    RoleId = x.RoleId,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate
                })
                .ToArrayAsync(); // Chuyển kết quả truy vấn thành mảng bất đồng bộ
        }
        public async Task<UserResponseDTO[]> SearchUserArrayAsync(string searchTerm)
        {
            var query = _context.UserAccounts
                .Where(x => !x.IsDeleted); // Filter out deleted accounts

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Split the searchTerm into an array of terms
                string[] searchTerms = searchTerm.Split(' ').ToArray();

                // Apply search filters based on the array of search terms
                query = query.Where(x =>
                    searchTerms.All(term =>
                        (x.RoleId.ToString().Contains(term) ||
                         x.Status.Contains(term) ||
                         x.Country.Contains(term) ||
                         x.Gender.Contains(term) ||
                         x.Address.Contains(term) ||
                         x.Phone.Contains(term) ||
                         x.UserEmail.Contains(term) ||
                         x.UserName.Contains(term) ||
                         x.Id.Contains(term)
                        )
                    )
                );
            }

            return await query
                .Select(x => new UserResponseDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Password = x.Password,
                    Phone = x.Phone,
                    Address = x.Address,
                    Dob = x.Dob,
                    Gender = x.Gender,
                    Country = x.Country,
                    Status = x.Status,
                    RoleId = x.RoleId,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate
                })
                .ToArrayAsync(); // Convert the results to an array asynchronously
        }

        public async Task<UserResponseDTO[]> GetAllUserArrayAsync()
        {
            return await _context.UserAccounts
                .Where(x => !x.IsDeleted) // Lọc các tài khoản đã bị xóa
                .Select(x => new UserResponseDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Password = x.Password,
                    Phone = x.Phone,
                    Address = x.Address,
                    Dob = x.Dob,
                    Gender = x.Gender,
                    Country = x.Country,
                    Status = x.Status,
                    RoleId = x.RoleId,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    ImageUrls = x.Images
                                .Where(img => !img.IsDeleted) // Lọc hình ảnh chưa bị xóa
                                .Select(img => img.UrlPath)
                                .ToArray() // Chuyển thành mảng
                })
                .ToArrayAsync(); // Chuyển kết quả truy vấn thành mảng bất đồng bộ
        }

        public async Task<TotalUsersStatDTO> GetTotalUsersStatAsync()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek);

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

        public async Task<List<UserAccount>> SearchUserAccounts(SearchUserDTO searchUserDTO)
        {
            var query = _context.UserAccounts.AsQueryable();

            // Check if SearchTerm is provided
            if (!string.IsNullOrEmpty(searchUserDTO.SearchTerm))
            {
                // Convert the search term into an array by splitting on spaces
                string[] searchTerms = searchUserDTO.SearchTerm.Split(' ').ToArray();

                // Search across all fields using multiple terms in the array
                query = query.Where(x =>
                    searchTerms.All(term =>
                        (x.RoleId.ToString().Contains(term) ||
                         x.Status.Contains(term) ||
                         x.Country.Contains(term) ||
                         x.Gender.Contains(term) ||
                         x.Address.Contains(term) ||
                         x.Phone.Contains(term) ||
                         x.UserEmail.Contains(term) ||
                         x.UserName.Contains(term) ||
                         x.Id.Contains(term)
                        )
                    )
                );
            }

            return await query.ToListAsync();
        }

    }
}
