using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IUserAccountRepository : IRepositoryGeneric<UserAccount>
    {
        Task<UserAccount> Login(string email, string password);
        Task<UserAccount> GetHighestIdUser();
        Task<UserAccount> GetUserWithImagesByIdAsync(string id);
        Task<TotalUsersStatDTO> GetTotalUsersStatAsync(); //Dashboard
        DataTable getUser();
    }
}
