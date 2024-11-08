using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class UserAccountRepository : RepositoryGeneric<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(SoschildrenVillageDbContext context) : base(context)
        {

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


    }
}
